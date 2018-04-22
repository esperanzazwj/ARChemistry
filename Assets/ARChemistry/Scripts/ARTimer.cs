using UnityEngine;
using System.Collections;
using EasyARSample;

public class ARTimer : MonoBehaviour {

	private ImageTarget_DynamicLoad_AutoPlay father;
	// Use this for initialization
	void Start () {
		father = transform.parent.gameObject.GetComponent ("ImageTarget_DynamicLoad_AutoPlay") as ImageTarget_DynamicLoad_AutoPlay;
		Invoke("Timer", 1.0f);

	}

	void Timer() {
		if (father.onFound) {
			if (GameObject.Find ("H2O") != null) {
				father.transform.GetChild (1).gameObject.SetActive (false);
				father.transform.GetChild (2).gameObject.SetActive (true);
			}
		}
		//Debug.Log(string.Format("Timer3 is up !!! time=${0}", Time.time));
		Invoke("Timer", 1.0f);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
