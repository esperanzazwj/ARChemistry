/**
* Copyright (c) 2015-2016 VisionStar Information Technology (Shanghai) Co., Ltd. All Rights Reserved.
* EasyAR is the registered trademark or trademark of VisionStar Information Technology (Shanghai) Co., Ltd in China
* and other countries for the augmented reality technology developed by VisionStar Information Technology (Shanghai) Co., Ltd.
*/

namespace EasyAR
{
    public class VideoPlayerBehaviour : VideoPlayerBaseBehaviour
    {
		 private bool isPlayed = false;

        public void myPlayPause()
        {
            if (isPlayed)
            {
                this.Pause();
                isPlayed = false;
            }
            else
            {
                this.Play();
                isPlayed = true;
            }
        }
        public void myPause()
        {
            if (isPlayed)
            {
                this.Pause();
                isPlayed = false;
            }
        }
    }
}
