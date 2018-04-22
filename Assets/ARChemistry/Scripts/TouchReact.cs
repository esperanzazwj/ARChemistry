using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchReact : MonoBehaviour {
    private GameObject selectObj = null;
    private bool isSelected = false;
    private bool isUISelected = false;
    private bool UISelect = false;
    private bool videoPlaySig = false;
    private int time = 0;
    private float oldL;
    private Vector2 pastP = new Vector2(0, 0);

	// Use this for initialization
	void Start () {
        Input.multiTouchEnabled = true;
        UISelect = true;
        videoPlaySig = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount < 1)
            return;

        if (Input.touchCount == 1) {
            UISelect = true;
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (UISelect == true && true == EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    isUISelected = true;
                    return;
                }
                else
                {
                    isUISelected = false;
                }
                    
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                if (Physics.Raycast(ray, out hit))
                {
                    isSelected = true;
                    selectObj = hit.collider.gameObject;
                    videoPlaySig = true;
                    pastP = Input.GetTouch(0).position;
                }
                else
                {
                    videoPlaySig = false;
                    isSelected = false;
                    selectObj.GetComponent<Test>().SendMessage("deactivateInfo");
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                videoPlaySig = false;
                if (isUISelected == true)
                    return;

                if (isSelected)
                {
                    Vector2 newP = Input.GetTouch(0).position;
                    Vector3 Screen = Camera.main.WorldToScreenPoint(selectObj.transform.position);
                    Vector3 offset = selectObj.transform.position -
                        Camera.main.ScreenToWorldPoint(new Vector3(pastP.x, pastP.y, Screen.z));
                    Vector3 curScreen = Camera.main.ScreenToWorldPoint(new Vector3(newP.x, newP.y, Screen.z));
                    selectObj.transform.position = offset + curScreen;
                    pastP = newP;
                }
            }else if(Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                if (isUISelected == true)
                    return;

                if (isSelected)
                {
                    time += 1;
                    if(time > 50)
                    {
                        selectObj.GetComponent<Test>().SendMessage("activateInfo");
                        videoPlaySig = false;
                        // here we activate info menu; and we deactivate in hit other non-object places.
                        // if we hit other object, no need deactivate
                        // todo: activate info menu
                        // todo: video-play
                        // todo: animation
                    }
                    else
                    {
                        videoPlaySig = videoPlaySig;
                    }
                    pastP = Input.GetTouch(0).position;
                }
            }else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                time = 0;
                if (isUISelected == true)
                    return;

                if (isSelected)
                {
                    if (videoPlaySig)
                    {
                        selectObj.SendMessage("myPlayPause");
                    }   
                }
            }
        }
        else if(Input.touchCount == 2){
            time = 0;
            videoPlaySig = false;
            if (true == isUISelected)
                return;

            UISelect = false;
            TouchPhase t1, t2;
            t1 = Input.GetTouch(0).phase;
            t2 = Input.GetTouch(1).phase;

            if (t1 == TouchPhase.Began && t2 == TouchPhase.Began)
            {
                oldL = calculateDis(Input.GetTouch(0).position, Input.GetTouch(1).position);
                if (isSelected)
                {
                    return;
                }
                else
                {
                    RaycastHit hit1, hit2;
                    Ray ray1 = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    Ray ray2 = Camera.main.ScreenPointToRay(Input.GetTouch(1).position);

                    if (Physics.Raycast(ray1, out hit1))
                    {
                        isSelected = true;
                        selectObj = hit1.collider.gameObject;                   
                       	Debug.Log(hit1.transform.name);
                        //Debug.Log(hit1.transform.tag);
                    }else if (Physics.Raycast(ray2, out hit2)){
                        isSelected = true;
                        selectObj = hit2.collider.gameObject;
                        Debug.Log(hit2.transform.name);
                        //Debug.Log(hit1.transform.tag);
                    }
                    else
                    {
                        isSelected = false;
                    }
                }
                
            }
            else if (t1 == TouchPhase.Moved && t2 == TouchPhase.Moved)
            {
                if (isSelected)
                {
                    Vector2 newP1 = Input.GetTouch(0).position;
                    Vector2 newP2 = Input.GetTouch(1).position;
                    if (Input.GetTouch(0).deltaPosition.SqrMagnitude() < 10.0 || Input.GetTouch(1).deltaPosition.SqrMagnitude() < 10.0)
                    {
                        oldL = calculateDis(Input.GetTouch(0).position, Input.GetTouch(1).position);
                        return;
                    }
                        
                    float newL = calculateDis(newP1, newP2);
                    if(newL < oldL)
                    {
                        float scale = Mathf.Abs(oldL - newL) / 500.0f;
                        
                        Vector3 newS = new Vector3(selectObj.transform.localScale.x - scale,
                            selectObj.transform.localScale.y - scale,
                            selectObj.transform.localScale.z - scale);

                        if (newS.x < 0.1f || newS.y < 0.1f || newS.z < 0.1f)
                        {
                            Vector3 minS = Vector3.Max(newS, new Vector3(0.1f, 0.1f, 0.1f));
                            selectObj.transform.localScale = minS;
                        }
                        else
                        {
                            selectObj.transform.localScale = newS;
                        }                        
                    }
                    else
                    {
                        float scale = Mathf.Abs(oldL - newL) / 500.0f;
                        Vector3 newS = new Vector3(selectObj.transform.localScale.x + scale,
                            selectObj.transform.localScale.y + scale,
                            selectObj.transform.localScale.z + scale);

                        if (newS.x > 3.0f || newS.y > 3.0f || newS.z > 3.0f)
                        {
                            Vector3 minS = Vector3.Min(newS, new Vector3(3.0f, 3.0f, 3.0f));
                            selectObj.transform.localScale = minS;
                        }
                        else
                        {
                            selectObj.transform.localScale = newS;
                        }
                    }

                    oldL = newL;
                }
            }
            else if (t1 == TouchPhase.Stationary && t2 == TouchPhase.Moved)
            {
                oldL = calculateDis(Input.GetTouch(0).position, Input.GetTouch(1).position);
                if (isSelected)
                {
                    Vector2 newP = Input.GetTouch(1).deltaPosition;
                    
                    float xRot = newP.x * 1.0f;
                    float yRot = newP.y * 1.0f;
                    Vector3 rotM = new Vector3(0, xRot, yRot);
                    selectObj.transform.Rotate(rotM);
                }
            }
            else if (t2 == TouchPhase.Stationary && t1 == TouchPhase.Moved)
            {
                oldL = calculateDis(Input.GetTouch(0).position, Input.GetTouch(1).position);
                if (isSelected)
                {
                    Vector2 newP = Input.GetTouch(0).deltaPosition;

                    float xRot = newP.x * 1.0f;
                    float yRot = newP.y * 1.0f;
                    Vector3 rotM = new Vector3(0, xRot, yRot);
                    selectObj.transform.Rotate(rotM);
                }
            }
            else if (t2 == TouchPhase.Stationary && t1 == TouchPhase.Stationary)
            {
                oldL = calculateDis(Input.GetTouch(0).position, Input.GetTouch(1).position);
                if (isSelected)
                {
                    float xRot = Input.GetTouch(0).deltaTime * 180.0f;
                    Vector3 rotM = new Vector3(xRot, 0, 0);
                    selectObj.transform.Rotate(rotM);
                }
            }
        }

    }
    float calculateDis(Vector2 newP1, Vector2 newP2)
    {
        float dis = 0.0f;
        dis = Mathf.Sqrt(Mathf.Pow(newP1.x - newP2.x, 2) + Mathf.Pow(newP1.y - newP2.y, 2));
        return dis;
    }
    int calculateDir(Vector2 newP)
    {
        int dir = -1;
        if (Mathf.Abs(newP.x) > Mathf.Abs(newP.y))
            dir = 0;    // x
        else
            dir = 1;    // y
        return dir;
    }
}
