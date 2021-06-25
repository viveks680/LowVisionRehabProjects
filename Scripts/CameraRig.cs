using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public GameObject Camera;
    public GameObject[] spawnLocations;
    public void SpawnPlayer(int spawn)
    {

        spawnLocations = GameObject.FindGameObjectsWithTag("SpawnPoint");

        Camera.transform.position = spawnLocations[spawn].transform.position;
        Camera.transform.rotation = spawnLocations[spawn].transform.rotation;

    }

}
