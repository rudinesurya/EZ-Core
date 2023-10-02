using UnityEngine;
using System.Collections;
using EZ_Core;

#if UNITY_EDITOR
using UnityEditor;
#endif

//Script mainly used for UI that need to be swapped between portrait and landscape mode on device rotation.
//just attach this script on the root and referenced its landscape and portrait children.

namespace EZ_Core
{
    public class ChangeOnDeviceRotation : MonoBehaviour
    {
        public GameObject portrait;
        public GameObject landscape;
        
        private ScreenOrientation previousOr;

        void Update()
        {

#if UNITY_EDITOR

            float w = Handles.GetMainGameViewSize().x;
            float h = Handles.GetMainGameViewSize().y;

            float ratio = w / h;
            //DebugClass.Log("w : " + w + "\nh : " + h + "\nratio : " + ratio);
            //16 / 9 = 1.7778
            //4 / 3 = 1.3333
            if (ratio > 1.3333f)
            {
                //landscape
                portrait.SetActive(false);
                landscape.SetActive(true);
            }
            else
            {
                //portrait
                portrait.SetActive(true);
                landscape.SetActive(false);
            }
#else

        if (previousOr == Screen.orientation)
            return;

        switch (Screen.orientation)
        {
            case ScreenOrientation.Portrait:
            case ScreenOrientation.PortraitUpsideDown:
                portrait.SetActive(true);
                landscape.SetActive(false);
                break;

            case ScreenOrientation.LandscapeLeft:
            case ScreenOrientation.LandscapeRight:
                portrait.SetActive(false);
                landscape.SetActive(true);
                break;
        }

        previousOr = Screen.orientation;

#endif
        }
    }
}
