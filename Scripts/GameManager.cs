using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
* if count = 1 (or ODD number), N = Green, E = Red
* if count = 0 (or EVEN number), N = Red, E = Green
* 
*/


public class GameManager : MonoBehaviour
{
    public int count = 1;
    public string head_N = "Green";
    public string head_E = "Red";
    public float signalDuration = 12f;
    public static GameManager gm;

    private void Awake()
    {
        if(count == 1)
        {
            head_N = "Green";
            head_E = "Red";
        }
        else
        {
            head_E = "Green";
            head_N = "Red";
        }
        CheckInstanceStatus();
    }

    private void CheckInstanceStatus()
    {
        if (gm == null)
        {
            gm = this;
            DontDestroyOnLoad(gameObject);
        }

        if (gm != this)
        {
            Debug.Log($"Duplicate {nameof(gm)} found on {gameObject.name}. Destroying now.");
            Destroy(gameObject);
        }
    }
    IEnumerator change()
    {
        if(count%2 == 0)
        {
            
            head_N = "Yellow";
            yield return new WaitForSeconds(signalDuration / 6f);
            head_N = "Red"; head_E = "Green";

            Debug.Log("GM: North - Red Signal");
            Debug.Log("GM: East - Green Signal");
        }
        else
        {
            
            head_E = "Yellow";
            yield return new WaitForSeconds(signalDuration / 6f);
            head_E = "Red"; head_N = "Green";

            Debug.Log("GM: North - Green Signal");
            Debug.Log("GM: East - Red Signal");
            
        }
    }

    
    IEnumerator Looper()
    {
        yield return new WaitForSeconds(signalDuration);
        count++;
        
        StartCoroutine(change());
        StartCoroutine(Looper());
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }


    void Start()
    {
        //StartCoroutine(Looper());
    }
}
