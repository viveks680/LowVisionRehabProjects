using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianLights : MonoBehaviour
{
    public GameObject TrafficLightSemaphore;
    public GameObject GreenLightsNS;
    public GameObject GreenLightsEW;
    public GameObject RedLightsNS;
    public GameObject RedLightsEW;
    public GameObject GreyRedLightsNS;
    public GameObject GreyRedLightsEW;
    public float cycle;

    // Start is called before the first frame update
    private void Start()
    {
        cycle = TrafficLightSemaphore.GetComponent<StandardSemaphoreSystem>().getCycle();
        //StartCoroutine(lightchanger());
    }
    
    // Update is called once per frame
    IEnumerator lightchanger()
    {
        yield return new WaitForSeconds(3);
        //Debug.Log("Light changer called");
        if (TrafficLightSemaphore.GetComponent<StandardSemaphoreSystem>().blockFirstWay == false)
        {
            /*GreenLightsNS.GetComponent<MeshRenderer>().enabled = true;
            GreenLightsEW.GetComponent<MeshRenderer>().enabled = false;
            RedLightsNS.GetComponent<MeshRenderer>().enabled = false;
            RedLightsEW.GetComponent<MeshRenderer>().enabled = true;*/
            GreenLightsNS.SetActive(true);
            GreenLightsEW.SetActive(false);
            RedLightsNS.SetActive(false);
            RedLightsEW.SetActive(true);
            GreyRedLightsNS.SetActive(true);
            GreyRedLightsEW.SetActive(false);
            yield return new WaitForSeconds(cycle - 3);

            GreenLightsNS.SetActive(false);
            RedLightsNS.SetActive(true);
            GreyRedLightsNS.SetActive(false);
        }
        else
        {
            /*GreenLightsNS.GetComponent<MeshRenderer>().enabled = false;
            GreenLightsEW.GetComponent<MeshRenderer>().enabled = true;
            RedLightsNS.GetComponent<MeshRenderer>().enabled = true;
            RedLightsEW.GetComponent<MeshRenderer>().enabled = false;*/
            GreenLightsNS.SetActive(false);
            GreenLightsEW.SetActive(true);
            RedLightsNS.SetActive(true);
            RedLightsEW.SetActive(false);
            GreyRedLightsNS.SetActive(false);
            GreyRedLightsEW.SetActive(true);

            yield return new WaitForSeconds(cycle - 3);

            GreenLightsEW.SetActive(false);
            RedLightsEW.SetActive(true);
            GreyRedLightsEW.SetActive(false);
        }
        yield return new WaitForSeconds(3);
        StartCoroutine(lightchanger());
    }


}
