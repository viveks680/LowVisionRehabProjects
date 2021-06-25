using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCaster : MonoBehaviour
{
    public Camera cam;
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;
    
    public GameObject target;
    public GameObject Box1;
    //public GameObject source;
    //public int layermask = 1 << 8;
    public Text ResponseText;
    public GameObject mapperObject;
    public GameObject Dummy;
    public int cornerNumber;
    string text;

    void Start()
    {
        Box1.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.25f, 0f);

        cam = Camera.main;
        //layermask = ~layermask;
        //Debug.Log(layermask);
    }

    void Update()
    {
        //Ray ray = cam.ViewportPointToRay(Input.mousePosition);
        //Ray ray2 = new Ray(source.transform.position, Input.mousePosition);
        
        if (Input.mousePosition.x < 0) //left camera
            {
            cam = cam2;
        }
            else if (Input.mousePosition.x > 2560) //right camera
            {
            cam = cam3;
        }
            else if (Input.mousePosition.x >= 0 && Input.mousePosition.x <= 2560) //
            {
            cam = cam1;
            }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            //Direction modifier based on which screen is clicked
            
            //Vector3 worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Box1.SetActive(true);
            //cornerNumber = mapperObject.GetComponent<mapScenarios>().cornerNumber();
            //Debug.Log(cornerNumber);
            //Debug.Log(worldPosition);
            target = mapperObject.GetComponent<mapScenarios>().TargetObject;
            //Dummy.transform.position = target.transform.position;
            //Debug.Log(mapperObject.GetComponent<mapScenarios>().cornerNumber());
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 9))
            {
                Dummy.transform.position = hit.point;
                Debug.Log("Object hit on layer = " + hit.point);
                //Debug.Log("Target on layer = " + target.transform.gameObject.layer);
                //Debug.Log("Target on position = " + target.transform.position);

                if (hit.transform == target.transform)
                {
                    text = "\n (Target was hit)";
                }
                else
                    text = "";

                //Vector3 closestPoint = target.GetComponent<Collider>().ClosestPointOnBounds(target.transform.position);

                Debug.Log(hit.point + "    -    " + target.transform.position);
                //Vector3 dist = hit.point - target.transform.position;
                //ResponseText.text = "Distance of pointing from target : " + dist;

                float dist = Vector3.Distance(hit.point, target.transform.position);
                //ResponseText.text = "The distance away from object is " + (dist * 0.9f).ToString("n2")+" feet" + text;
                ResponseText.text = "The distance away from object is " + Input.mousePosition;
                
            }
        }
    }
}
