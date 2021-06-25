using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using UnityEngine.Windows.Speech;

public class StartTimer : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    private float timeRemaining;
    public Text ResponseText;
    public GameObject Box;
    int InputFlag;

    private void Start()
    {
        InputFlag = 0;
        Box.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.3f, 0f);
        Box.SetActive(true);

        actions.Add("Now", Now);
        actions.Add("Go", Go);
        actions.Add("Here", Here);
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Car entered");
        ResponseText.text = "Give input when it is appropriate";
        
        StartTime();
        InputFlag = 1;
        StopAllCoroutines();
    }

    void StartTime()
    {
        timeRemaining =- Time.deltaTime;
    }

    void Update()
    {
        timeRemaining += Time.deltaTime;
        if (Input.GetKeyDown("space"))
        {
            if (InputFlag == 1)
                ResponseText.text = "Response Time: " + (Mathf.Round(timeRemaining * 1000f) / 1000f) + " seconds";
            else if (InputFlag == 0)
                ResponseText.text = "Input too early!";
            InputFlag = 2; StartCoroutine(TextClear());
        }
    }

    private void Go()
    {
        if (InputFlag == 1)
            ResponseText.text = "Response Time: " + (Mathf.Round(timeRemaining * 1000f) / 1000f) + " seconds";
        else if (InputFlag == 0)
            ResponseText.text = "Input too early!";
        Debug.Log("Go function is called"); InputFlag = 2; StartCoroutine(TextClear());
    }

    private void Now()
    {
        if (InputFlag == 1)
            ResponseText.text = "Response Time: " + (Mathf.Round(timeRemaining * 1000f) / 1000f) + " seconds";
        else if (InputFlag == 0)
            ResponseText.text = "Input too early!";
        Debug.Log("Now function is called"); InputFlag = 2; StartCoroutine(TextClear());
    }

    private void Here()
    {
        if (InputFlag == 1)
            ResponseText.text = "Response Time: " + (Mathf.Round(timeRemaining * 1000f) / 1000f) + " seconds";
        else if (InputFlag == 0)
            ResponseText.text = "Input too early!";
        Debug.Log("Here function is called"); InputFlag = 2;
        StartCoroutine(TextClear());
    }

    IEnumerator TextClear()
    {
        yield return new WaitForSeconds(10f);
        ResponseText.text = "Waiting for response";
        InputFlag = 0; //reset
        StopAllCoroutines();
    }
}
