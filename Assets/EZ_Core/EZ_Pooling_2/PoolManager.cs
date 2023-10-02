using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EZ_Core
{
    //Tweakable variable in the inspector
    [System.Serializable]
    public class PrefabPoolSettings
    {
        public ActorPooled sourcePrefab = null;
        public Transform parentTransform = null;
        public int preloadAmount = 1;
        public bool showDebugLog = false;

        //Editor usage
        public bool isPoolExpanded = true;
    }

    [System.Serializable]
    public class PrefabPool
    {
        public PrefabPoolSettings settings;
        public List<ActorPooled> spawnedList = new List<ActorPooled>();
        public List<ActorPooled> despawnedList = new List<ActorPooled>();


        public PrefabPool()
        {
        }

        public void PreloadPool()
        {
            //preload
            for (int i = 0; i < settings.preloadAmount; ++i)
            {
                var newItem = (Transform)Transform.Instantiate(settings.sourcePrefab.transform);
                var newActor = newItem.GetComponent<ActorPooled>();

                newActor.Initialize();
                newActor.cachedGameObject.name = settings.sourcePrefab.name;
                newActor.cachedTransform.SetParent(settings.parentTransform);
                newActor.cachedGameObject.SetActive(false);

                despawnedList.Add(newActor);
            }
        }

        public ActorPooled Spawn(ActorPooled transToSpawn, Vector3 position, Quaternion rotation)
        {
            if (despawnedList.Count == 0)
            {
                DebugClass.LogWarning(transToSpawn.name + " has used up all the free preallocated instances. Please increase your Preload Amount.");
                return null;
            }
            else
            {
                var freeObject = despawnedList[0];

                freeObject.cachedTransform.position = position;
                freeObject.cachedTransform.rotation = rotation;
                freeObject.cachedGameObject.SetActive(true);

                if (settings.showDebugLog)
                {
                    DebugClass.Log(string.Format("{0} spawned {1}", PoolManager.ASSET_NAME, transToSpawn.name));
                }

                freeObject.OnSpawned();

                despawnedList.Remove(freeObject);
                spawnedList.Add(freeObject);

                return freeObject;
            }
        }

        public void Despawn(ActorPooled transToDespawn)
        {
            transToDespawn.OnDespawned();

            transToDespawn.cachedTransform.SetParent(settings.parentTransform);
            transToDespawn.cachedGameObject.SetActive(false);

            if (settings.showDebugLog)
            {
                DebugClass.Log(string.Format("{0} despawned {1}", PoolManager.ASSET_NAME, transToDespawn.name));
            }

            spawnedList.Remove(transToDespawn);
            despawnedList.Add(transToDespawn);
        }

        public void ClearPool()
        {
            while (spawnedList.Count > 0)
            {
                Despawn(spawnedList[0]);
            }
        }
    }

    [AddComponentMenu("EZ Core/EZ Pooling 2/PoolManager")]
    public class PoolManager : MonoBehaviour
    {
        public const string ASSET_NAME = "EZ_Pooling_2";

        //Tweakable variables
        public bool showDebugLog = false;
        public bool autoAddMissingPrefabPool = false;
        public PrefabPoolSettings defaultSetting;

        public List<PrefabPoolSettings> prefabPoolSettings_List = new List<PrefabPoolSettings>();
        private List<PrefabPoolSettings> itemsMarkedForDeletion = new List<PrefabPoolSettings>();

        //Editor usage
        public bool isRootExpanded = true;

        //Pools
        public static Dictionary<string, PrefabPool> Pools = new Dictionary<string, PrefabPool>();

        private static PoolManager instance;

        public static PoolManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<PoolManager>();

                    if (instance == null)
                    {
                        GameObject go = new GameObject(ASSET_NAME);
                        instance = go.AddComponent<PoolManager>();
                    }
                }

                return instance;
            }
        }

        void Awake()
        {
            Pools.Clear();
            itemsMarkedForDeletion.Clear();

            // loop through all the pre-allocated pools and initialize all the pool
            for (var i = 0; i < prefabPoolSettings_List.Count; ++i)
            {
                var item = prefabPoolSettings_List[i];
                var prefabTransform = item.sourcePrefab;
                var name = prefabTransform.name;

                if (item.preloadAmount <= 0)
                {
                    itemsMarkedForDeletion.Add(item);
                    continue; // no need to pre-allocate any game obj, nothing else to do
                }

                if (prefabTransform == null)
                {
                    DebugClass.LogWarning("Item at index " + (i + 1) + " in the Pool Manager has no prefab !");
                    continue;
                }

                if (Pools.ContainsKey(name))
                {
                    DebugClass.LogError("Duplicates found in the Pool Manager : " + name);
                    Debug.Break();
                }

                var newPrefabPool = new PrefabPool();
                newPrefabPool.settings = item;
                newPrefabPool.PreloadPool();

                Pools.Add(name, newPrefabPool); //add the pool to the Dictionary
            }

            foreach (var item in itemsMarkedForDeletion)
            {
                prefabPoolSettings_List.Remove(item);
            }

            itemsMarkedForDeletion.Clear();
        }

        /// <summary>
        /// Creates new pool for the new object.
        /// </summary>
        /// <param name="actor"></param>
        private static void CreateMissingPrefabPool(ActorPooled actor)
        {
            var name = actor.name;
            var newPrefabPool = new PrefabPool();

            //Set the new pool options here
            PrefabPoolDefaultOverride gameObjSettingComponent = actor.GetComponent<PrefabPoolDefaultOverride>();
            if (gameObjSettingComponent == null)
                newPrefabPool.settings = instance.defaultSetting;
            else
                newPrefabPool.settings = gameObjSettingComponent.settings;

            newPrefabPool.settings.parentTransform = instance.transform;

            newPrefabPool.settings.sourcePrefab = actor;

            Pools.Add(name, newPrefabPool);
            newPrefabPool.PreloadPool();

            // for the Inspector only
            var newPrefabPoolSettings = new PrefabPoolSettings();
            newPrefabPoolSettings = newPrefabPool.settings;
            instance.prefabPoolSettings_List.Add(newPrefabPoolSettings);


            if (instance.showDebugLog)
            {
                DebugClass.Log(string.Format("{0} created Pool Item for missing item : {1}", ASSET_NAME, name));
            }
        }

        /// <summary>
        /// Spawn object using the pool, and return reference to the spawned object. If pool does not exists, 
        /// it will create a new pool automatically if the autoAddMissingPrefabPool is checked. Else it will return null.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public static ActorPooled Spawn(ActorPooled actor, Vector3 position, Quaternion rotation)
        {
            if (actor == null)
            {
                DebugClass.LogWarning("Nothing is passed to Spawn() !");
                return null;
            }

            var name = actor.name;
            if (!Pools.ContainsKey(name))
            {
                if (Instance.autoAddMissingPrefabPool)
                {
                    CreateMissingPrefabPool(actor);
                }
                else
                {
                    DebugClass.LogWarning(string.Format("{0} passed to Spawn() is not in the {1}.", name, ASSET_NAME));
                    return null;
                }
            }

            return Pools[name].Spawn(actor, position, rotation);
        }

        /// <summary>
        /// Despawn object using the pool. If pool does not exist, normal Destroy will be used.
        /// </summary>
        /// <param name="actor"></param>
        public static void Despawn(ActorPooled actor)
        {
            if (actor == null)
            {
                DebugClass.LogWarning("Nothing is passed to Despawn() !");
                return;
            }

            if (!actor.cachedGameObject.activeInHierarchy)
            {
                return;
            }

            var name = actor.name;
            if (!Pools.ContainsKey(name))
            {
                DebugClass.LogWarning(string.Format("{0} passed to Despawn() is not in the {1}.", name, ASSET_NAME));
                return;
            }

            Pools[name].Despawn(actor);
        }

        public static PrefabPool GetPool(string poolName)
        {
            if (!Pools.ContainsKey(poolName))
            {
                return null;
            }

            return Pools[poolName];
        }
    }
}