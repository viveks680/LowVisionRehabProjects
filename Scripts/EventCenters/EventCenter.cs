using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EventCenter : MonoBehaviour
{
    public string direction;
    public GameObject[] EventSpawn;
    public GameObject EventVehicle;
    public float speed;
    public float variation;
    public float EventSpeed = 100f;
    

    private void Awake()
    {
        
        EventSpawn = GameObject.FindGameObjectsWithTag("EventSpawn"); ;
    }

    private void Update()
    {
        if (TrafficParameters.tp.EventCenterSpot == "NS")
        {
            Instantiate(EventVehicle, EventSpawn[0].transform);
        }
        else if (TrafficParameters.tp.EventCenterSpot == "NF")
        {
            Instantiate(EventVehicle, EventSpawn[1].transform);
        }
        else if (TrafficParameters.tp.EventCenterSpot == "SS")
        {
            Instantiate(EventVehicle, EventSpawn[2].transform);
        }
        else if (TrafficParameters.tp.EventCenterSpot == "SF")
        {
            Instantiate(EventVehicle, EventSpawn[3].transform);
        }
        else if (TrafficParameters.tp.EventCenterSpot == "ES")
        {
            Instantiate(EventVehicle, EventSpawn[4].transform);
        }
        else if (TrafficParameters.tp.EventCenterSpot == "EF")
        {
            Instantiate(EventVehicle, EventSpawn[5].transform);
        }
        else if (TrafficParameters.tp.EventCenterSpot == "WS")
        {
            Instantiate(EventVehicle, EventSpawn[6].transform);
        }
        else if (TrafficParameters.tp.EventCenterSpot == "WF")
        {
            Instantiate(EventVehicle, EventSpawn[7].transform);
        }

    }
}
