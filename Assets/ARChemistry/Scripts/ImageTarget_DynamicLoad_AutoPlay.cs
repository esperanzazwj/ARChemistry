/**
* Copyright (c) 2015-2016 VisionStar Information Technology (Shanghai) Co., Ltd. All Rights Reserved.
* EasyAR is the registered trademark or trademark of VisionStar Information Technology (Shanghai) Co., Ltd in China
* and other countries for the augmented reality technology developed by VisionStar Information Technology (Shanghai) Co., Ltd.
*/

using UnityEngine;
using EasyAR;

namespace EasyARSample
{
    public class ImageTarget_DynamicLoad_AutoPlay : ImageTargetBehaviour
    {
		public bool onFound = false;
        private int nVideo = 2;
        private string []video=null;

        protected override void Awake()
        {
            base.Awake();
            TargetFound += OnTargetFound;
            TargetLost += OnTargetLost;
            TargetLoad += OnTargetLoad;
            TargetUnload += OnTargetUnload;

            video = new string[nVideo];
            video[0] = "H2O.mp4";
			video[1] = "video.mp4";
        }

        protected override void Start() 
        {
            base.Start();
            LoadVideo();
			HideObjects ();
        }

        public void LoadVideo()
        {
			for (int i = 0; i < nVideo; i++) {
				GameObject subGameObject = Instantiate (Resources.Load ("TransparentVideo", typeof(GameObject))) as GameObject;
				subGameObject.transform.parent = this.transform;
				subGameObject.transform.localPosition = new Vector3 (0, 0.225f, 0);
				subGameObject.transform.localRotation = new Quaternion ();
				subGameObject.transform.localScale = new Vector3 (0.8f, 0.45f, 0.45f);

				VideoPlayerBaseBehaviour videoPlayer = subGameObject.GetComponent<VideoPlayerBaseBehaviour> ();
				if (videoPlayer) {
					videoPlayer.Storage = StorageType.Assets;
					videoPlayer.Path = video [i];
					videoPlayer.EnableAutoPlay = true;
					videoPlayer.EnableLoop = true;
					videoPlayer.Open ();
				}
			}
        }
			
		void HideObjects()
		{
			for (int i = 1; i < transform.childCount; ++i) {
				Transform transChild = transform.GetChild (i);
				transChild.gameObject.SetActive (false);
			}
		}
        void OnTargetFound(ImageTargetBaseBehaviour behaviour)
        {
			onFound = true;
			transform.GetChild (1).gameObject.SetActive (true);
            Debug.Log("Found: " + Target.Id);
        }

        void OnTargetLost(ImageTargetBaseBehaviour behaviour)
        {
			onFound = false;
            HideObjects();
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
