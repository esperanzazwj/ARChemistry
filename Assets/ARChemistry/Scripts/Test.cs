using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {
    public Transform target = null;
    public TextAsset txt = null;
    private bool isActivated = false;

	// Use this for initialization
	void Start () {
       /* if(target != null)
        {
            if (target.gameObject.activeSelf)
            {
                target.gameObject.SetActive(false);
                isActivated = false;
            }
               
        }*/
    }
	
	// Update is called once per frame
	void Update () {
        if (target != null && target.gameObject.activeSelf)
        {
            
        }
    }

    public void activateInfo()
    {
        if (target != null)
            if (false == target.gameObject.activeSelf)
            {
                target.gameObject.SetActive(true);
                activateText();
                isActivated = true;
            }
            else
            {
                activateText();
                isActivated = true;
            }
    }
    
    public void deactivateInfo()
    {
        if (target != null)
            if (true == target.gameObject.activeSelf)
            {
                target.gameObject.SetActive(false);
                deactivateText();
                isActivated = false;
            }
            else
            {
                deactivateText();
                isActivated = false;
            }
    }

    public void activateText()
    {
        if (txt != null)
            target.FindChild("Viewport").FindChild("Content")
                .gameObject.GetComponent<Text>()
                .text = txt.text;
    }
    public void deactivateText()
    {
        target.FindChild("Viewport").FindChild("Content")
            .gameObject.GetComponent<Text>()
            .text = "";
    }

    public void myPlayPause()
    {
        if(this.transform.name != "TransparentVideo")
            this.transform.parent.FindChild("TransparentVideo").gameObject.SendMessage("myPlayPause");
    }
}
