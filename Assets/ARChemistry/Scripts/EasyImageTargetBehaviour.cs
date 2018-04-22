/**
* Copyright (c) 2015-2016 VisionStar Information Technology (Shanghai) Co., Ltd. All Rights Reserved.
* EasyAR is the registered trademark or trademark of VisionStar Information Technology (Shanghai) Co., Ltd in China
* and other countries for the augmented reality technology developed by VisionStar Information Technology (Shanghai) Co., Ltd.
*/

using UnityEngine;

namespace EasyAR
{
    public class EasyImageTargetBehaviour : ImageTargetBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            TargetFound += OnTargetFound;
            TargetLost += OnTargetLost;
            TargetLoad += OnTargetLoad;
            TargetUnload += OnTargetUnload;
        }

        protected override void Start()
        {
            base.Start();
			for (int i = 0; i < transform.childCount; i++)
            {
               // transform.GetChild(i).SendMessage("deactivateInfo");
                transform.GetChild(i).SendMessage("myPause");
                transform.GetChild(i).gameObject.SetActive(false);
            }
            //HideObjects(transform);
        }

        void HideObjects(Transform trans)
        {
            for (int i = 0; i < trans.childCount; ++i)
                HideObjects(trans.GetChild(i));
            if (transform != trans)
                gameObject.SetActive(false);
        }

        void ShowObjects(Transform trans)
        {
            for (int i = 0; i < trans.childCount; ++i)
                ShowObjects(trans.GetChild(i));
            if (transform != trans)
                gameObject.SetActive(true);
        }

        void OnTargetFound(ImageTargetBaseBehaviour behaviour)
        {
			for (int i=0;i<transform.childCount;i++)
				transform.GetChild (i).gameObject.SetActive (true);
			//transform.GetChild (0).GetChild (0).gameObject.SetActive (false);
         //   ShowObjects(transform);
            Debug.Log("Found: " + Target.Id);
        }

        void OnTargetLost(ImageTargetBaseBehaviour behaviour)
        {
			for (int i = 0; i < transform.childCount; i++)
            {
               // transform.GetChild(i).SendMessage("deactivateInfo");
                transform.GetChild(i).SendMessage("myPause");
                transform.GetChild(i).gameObject.SetActive(false);
            }		
          //  HideObjects(transform);
            Debug.Log("Lost: " + Target.Id);
        }

        void OnTargetLoad(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
        {
            Debug.Log("Load target (" + status + "): " + Target.Id + " (" + Target.Name + ") " + " -> " + tracker);
        }

        void OnTargetUnload(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
        {
            Debug.Log("Unload target (" + status + "): " + Target.Id + " (" + Target.Name + ") " + " -> " + tracker);
        }
    }
}
