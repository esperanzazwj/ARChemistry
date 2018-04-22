using UnityEngine;  
using System.Collections;  
using System.Collections.Generic;  
using UnityEngine.Events;  
using UnityEngine.UI;  
using UnityEngine.SceneManagement;
using EasyAR;
public class ButtonClick : MonoBehaviour {

	private int mode=0;
	private ImageTrackerBehaviour ImageTracker_H2O;
	private ImageTrackerBehaviour ImageTracker_Element;
	private ImageTrackerBehaviour ImageTracker_HCL;
	private Text text;
	 void Start () {  
		Button btn = gameObject.GetComponent<Button> ();
		btn.onClick.AddListener(delegate() {  
			this.OnClick();   
		}); 
		text = GameObject.Find ("TextInfo").GetComponent<Text> ();
		ImageTracker_Element = GameObject.Find ("ImageTracker_Element").GetComponent ("ImageTrackerBehaviour") as ImageTrackerBehaviour;
		ImageTracker_H2O = GameObject.Find ("ImageTracker_H2O").GetComponent ("ImageTrackerBehaviour") as ImageTrackerBehaviour;
		ImageTracker_HCL = GameObject.Find ("ImageTracker_HCL").GetComponent ("ImageTrackerBehaviour") as ImageTrackerBehaviour;
		ImageTracker_H2O.StopTrack ();
		ImageTracker_HCL.StopTrack ();
		Debug.Log ("btnSuccess");
    }  
  
    public void OnClick()  
    {  
		Debug.Log ("onclick");
		if (mode == 0) {
			mode = 1;
			text.text = "识别化学符号组合";
			ImageTracker_Element.StopTrack ();
			ImageTracker_H2O.StartTrack ();
		} else if (mode == 1) {
			mode = 2;
			text.text = "识别化学式";
			ImageTracker_H2O.StopTrack ();
			ImageTracker_HCL.StartTrack ();
		} else if (mode == 2) {
			mode = 0;
			text.text = "化学元素";
			ImageTracker_HCL.StopTrack ();
			ImageTracker_Element.StartTrack ();
		}

		Debug.Log ("onclick check point");
		Debug.Log ("Cur mode=" + mode);
    }  
      
    // Update is called once per frame  
    void Update () {  
      
    }  
}  
