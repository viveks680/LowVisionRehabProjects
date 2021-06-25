using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrafficParameters : MonoBehaviour
{
    /*public Dropdown dropdown1;
    public Dropdown dropdown2;
    public Dropdown dropdown3;
    public Dropdown dropdown4;
    public Dropdown dropdown5;*/
    public static TrafficParameters tp;

    public List<string> concept = new List<string>() { "ParaPerpTraffic","NLPT","NLPTStop","NLPTSurge","ConfirmNLPTS","conceptMakeDecision", "FinalEvaluation" };
    public List<string> learnerState = new List<string>() { "Novice", "JourneyMan", "Expert" };

    //public int cornerDir = 0;
    public int[] numLanes = { 1,2 };
    public int[] wayType = { 1,2 };
    public bool divide;
    public List<string> Age = new List<string>(){ "Young","Old" };
    public List<string> ImpairmentType = new List<string>(){ "central vision loss", "peripheral vision loss" };
    public List<string> ImpairmentSeverity = new List<string>() {"Mild", "Moderate", "Serious" };
    public List<string> CrossingCorner = new List<string>() {"NW", "NE", "SW", "SE" };
    public List<string> CrossingDirection = new List<string>() {"N", "S", "E", "W" };
    public List<string> perpendicularStreetWidth = new List<string>() { "Narrow", "Normal", "Wide" };
    public List<string> crossingType = new List<string>() { "ClockW", "CounterClock" };
    public List<string> EventCenter = new List<string>() { "NS", "NF", "SS", "SF", "ES", "EF", "WS", "WF" };
    public List<CorenerDirection> CorDir = new List<CorenerDirection>();

    public string SelectedAge;
    public string SelectedType;
    public string SelectedSeverity;
    public string SelectedCorner;
    public string SelectedDirection;
    public string EventCenterSpot;
    public string SelectedCrossingType;
    
    
    /*public void Dropdown_AgeChange(int index)
    {
        SelectedAge = Age[index];
    }
    public void Dropdown_TypeChange(int index)
    {
        SelectedType = ImpairmentType[index];
    }
    public void Dropdown_SeverityChange(int index)
    {
        SelectedSeverity = ImpairmentSeverity[index];
    }
    public void Dropdown_CrossingCorner(int index)
    {
        SelectedCorner = CrossingCorner[index];
    }
    public void Dropdown_CrossingDirection(int index)
    {
        SelectedDirection = CrossingDirection[index];
    }
    public void Dropdown_EventCenter(int index)
    {
        EventCenterSpot = EventCenter[index];
    }*/

    public void Randomizer()
    {
        
        SelectedAge = Age[Random.Range(0, 2)];
        SelectedType = ImpairmentType[Random.Range(0, 2)];
        SelectedSeverity = ImpairmentSeverity[Random.Range(0, 3)];
        SelectedCorner = CrossingCorner[Random.Range(0, 4)];
        SelectedDirection = CrossingDirection[Random.Range(0, 4)];
        SelectedCrossingType = crossingType[Random.Range(0, 2)];
        
        /*Debug.Log(SelectedAge);
        Debug.Log(SelectedType);
        Debug.Log(SelectedSeverity);
        Debug.Log(SelectedCorner);
        Debug.Log(SelectedDirection);
        Debug.Log(SelectedCrossingType);*/
    }


    private void Awake()
    {
        
        CheckInstanceStatus();
    }

    private void Start()
    {
        PopulateList();
        Randomizer();
        
    }
    void PopulateList()
    {
        CorDir.Add(new CorenerDirection("NW", "E"));
        CorDir.Add(new CorenerDirection("NW", "S"));
        CorDir.Add(new CorenerDirection("NE", "W"));
        CorDir.Add(new CorenerDirection("NE", "S"));
        CorDir.Add(new CorenerDirection("SW", "E"));
        CorDir.Add(new CorenerDirection("SW", "N"));
        CorDir.Add(new CorenerDirection("SE", "N"));
        CorDir.Add(new CorenerDirection("SE", "W"));

        /*dropdown1.AddOptions(Age);
        dropdown2.AddOptions(ImpairmentType);
        dropdown3.AddOptions(ImpairmentSeverity);
        dropdown4.AddOptions(CrossingCorner);
        dropdown5.AddOptions(CrossingDirection);*/
    }
    private void CheckInstanceStatus()
    {
        if (tp == null)
        {
            tp = this;
            DontDestroyOnLoad(gameObject);
        }

        if (tp != this)
        {
            Debug.Log($"Duplicate {nameof(tp)} found on {gameObject.name}. Destroying now.");
            Destroy(gameObject);
        }
    }

    public void SavePlayer()
    {
        EventSave.SaveEvent(this);
    }


}
