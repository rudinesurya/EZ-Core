using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EZ_Core;

namespace EZ_PQ_Example
{
    [System.Serializable]
    public struct PQNode : System.IComparable<PQNode>
    {
        public int cost;
        public string name;

        //constructor
        public PQNode(int cost_, string name_)
        {
            cost = cost_;
            name = name_;
        }

        //must overload the operator so the priority queue can sort it
        public int CompareTo(PQNode other)
        {
            return this.cost - other.cost;
        }
    }

    public class PQExample : MonoBehaviour
    {
        public List<int> intList;
        public List<PQNode> customList;

        void Start()
        {
            Debug.Log("Inserting 'intList' into pq list");
            //insertion
            PriorityQueue<int> pq1 = new PriorityQueue<int>();

            for (int i = 0; i < intList.Count; ++i)
            {
                pq1.Enqueue(intList[i]);
            }

            Debug.Log("Poping 'intList' from smallest to biggest");
            //pop
            for (int i = 0; i < intList.Count; ++i)
            {
                Debug.Log(pq1.Dequeue());
            }

            Debug.Log("Inserting 'customList' into pq list");
            //insertion
            PriorityQueue<PQNode> pq2 = new PriorityQueue<PQNode>();

            for (int i = 0; i < customList.Count; ++i)
            {
                pq2.Enqueue(new PQNode(customList[i].cost, customList[i].name));
            }

            Debug.Log("Poping 'customList' from smallest to biggest");
            //pop
            for (int i = 0; i < customList.Count; ++i)
            {
                Debug.Log(pq2.Dequeue().cost);
            }
        }
    }
}