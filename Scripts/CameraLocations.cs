using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Camera positions in array (if everything goes right, check order in scene editor)
 * 0,1 -> SW -> N , E
 * 2,3 -> SE -> W , N
 * 4,5 -> NE -> S , W
 * 6,7 -> NW -> E , S
 * For example, spawnLocation[0] is SW corner, N. spawnLocation[5] is NE corner, W. 
 */

public class CameraLocations : MonoBehaviour
{
    public GameObject[] spawnLocations;
    public GameObject Camera;
    //public Text Location;
    
    
    private GameObject CurrScenario;
    public GameObject intersection1;
    
    //private string age, type, severity;
    
    public void Awake()
    {
        //Debug.Log("displays connected: " + Display.displays.Length);
        // Display.displays[0] is the primary, default display and is always ON, so start at index 1.
        // Check if additional displays are available and activate each.

        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
        // Not Zero, should take from traffic params
        //spawnLocations = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }
    

    

}
