using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class TrafficIntersections : MonoBehaviour
{
    public static TrafficIntersections ti;
    public Transform StreetMappingParent;
    public Transform PerpendicularStreetParent;
    public Transform AppropriateScenarioParent;
    public Transform AppropriateCWScenarioParent;
    public Transform AppropriateCCWScenarioParent;
    public Transform ConceptSelectManagerObj;

    public GameObject[] intersection;

    public List<StreetMapping> appropriateScenarios = new List<StreetMapping>();
    public GameObject[] appropriateScenario;


    public List<StreetMapping> appropriateCWScenarios = new List<StreetMapping>();
    public GameObject[] appropriateCWScenario;
    
    public List<StreetMapping> appropriateCCWScenarios = new List<StreetMapping>();
    public GameObject[] appropriateCCWScenario;

    public GameObject theScenario;

    //public GameObject[,] streetsMapping;
    public GameObject[] intersectionStreetMap;
    public List<StreetMapping> streetMapping = new List<StreetMapping>();

    public int count;
    public Text selectedParams, suggestion;

    public GameObject[] PerpendicularStreet;
    public List<StreetMapping> PerpendicularStreets = new List<StreetMapping>();

    private string corner, direction, crossDirType;
    private string age, type, severity;
    private int i = 0, j = 0; 
    public int cornerDir = 0;
    //private int[] odd= { 1, 3, 5, 7 };
    //private int[] even = { 0, 2, 4, 6 };

    public GameObject MainCam;

    
    
    public List<string> Age = new List<string>() { "Young", "Old" };
    public List<string> ImpairmentType = new List<string>() { "central vision loss", "peripheral vision loss" };
    public List<string> ImpairmentSeverity = new List<string>() { "Mild", "Moderate", "Serious" };
    public List<string> CrossingCorner = new List<string>() { "NW", "NE", "SW", "SE" };
    public List<string> CrossingDirection = new List<string>() { "N", "S", "E", "W" };
    public List<string> crossingType = new List<string>() { "ClockW", "CounterClock" };
    public List<string> LearnerState = new List<string>() { "learnerStateNovice", "learnerStateJourneyman" , "learnerStateExpert" };

    public float[] visualImpairment = { 20, 200, 600, 2000 };


    int CurrConcept;
    public int SelectedAge;
    public int SelectedState;
    public string SelectedType;
    public string SelectedSeverity;
    public string SelectedCorner;
    public string SelectedDirection;
    public string EventCenterSpot;
    public string SelectedCrossingType;
    public string SelectedPurpose;
    public float SelectedvisualImpairment;

    float carHeightMax = 4.42f;
    float carHeightMin = 1.5f;
    float psHeight = 0.254f;
    float psManipulation = 1f;

    float BCVA;
    float streetWidthSmallCarCutoff;
    float streetWidthLargeCarCutoff;
    float streetWidthPSCutoff;


    void Awake()
    {
        ti = this;
        count = 0;
        intersection = GameObject.FindGameObjectsWithTag("Intersection");
        MainCam = GameObject.FindGameObjectWithTag("MainCamera");

        SelectedAge = 1;

        SelectedType = ImpairmentType[Random.Range(0, 2)];
        SelectedSeverity = ImpairmentSeverity[Random.Range(0, 3)];
        SelectedCorner = CrossingCorner[Random.Range(0, 4)];
        SelectedDirection = CrossingDirection[Random.Range(0, 4)];
        SelectedCrossingType = crossingType[Random.Range(0, 2)];
        SelectedvisualImpairment = visualImpairment[Random.Range(0, visualImpairment.Length)];
        SelectedPurpose = "purposeInstruction";
        SelectedState = 0;
        CurrConcept = 1;
    }


    private void Start()
    {
        
        streetMapping.Clear();

        suggestion.text = "Select and submit parameters";
        count = 0;
        for (i = 0; i < intersection.Length; i++)
        {
            for (j = 0; j <= 7; j++)
            {
                streetMapping.Add(new StreetMapping(intersection[i], j));
            }

        }

        j = 0;
        foreach (StreetMapping street in streetMapping)
        {

            street.intersect.GetComponent<IntersectionParams>().CornerDirection(j);
            Instantiate(street.intersect, StreetMappingParent);
            j++;
            if (j > 7)
                j = 0;
            //Debug.Log("j =" + j);
            //Debug.Log("Street list item:" + street.intersect + "  " + street.spawnDir);
        }
        for (i = 0; i < StreetMappingParent.childCount; i++)
        {
            StreetMappingParent.GetChild(i).tag = "StreetMapping";
        }
        intersectionStreetMap = GameObject.FindGameObjectsWithTag("StreetMapping");
        for (count = 0; count < intersection.Length; count++)
        {
            intersection[count].SetActive(false);
        }

        
        
        type = SelectedType;
        severity = SelectedSeverity;
        corner = SelectedCorner;
        direction = SelectedDirection;
        crossDirType = SelectedCrossingType;
        
        Mapping();

        //Passing Concept number
        ConceptSelectManagerObj.GetComponent<ConceptSelect>().ConceptSelection(CurrConcept);
        
    }

    public void MapCW(int i)
    {
        //Debug.Log("Map cw" + i);
        appropriateCWScenarios.Add(new StreetMapping(intersectionStreetMap[i], intersectionStreetMap[i].GetComponent<IntersectionParams>().cornerDir));
        
    }

    public void MapCCW(int i)
    {
        //Debug.Log("Map ccw" + i);
        appropriateCCWScenarios.Add(new StreetMapping(intersectionStreetMap[i], intersectionStreetMap[i].GetComponent<IntersectionParams>().cornerDir));

    }


    public void Mapping()
    {
        
        BCVA = SelectedvisualImpairment;
        streetWidthSmallCarCutoff = carHeightMin / Mathf.Tan((BCVA / 240) * Mathf.PI / 180);
        // streetWidthLargeCarCutoff = carHeightMax / Mathf.Tan((BCVA / 240) * Mathf.PI / 180);

        streetWidthPSCutoff = psHeight * psManipulation / Mathf.Tan((BCVA / 240) * Mathf.PI / 180);

        Debug.Log("BCVA: " + BCVA);
        Debug.Log("streetWidthSmallCarCutoff: " + streetWidthSmallCarCutoff);
        Debug.Log("streetWidthPSCutoff: " + streetWidthPSCutoff);
        

         
        for (i = 0; i < intersectionStreetMap.Length; i++)
        {
            
            if (i % 2 == 0 && (intersectionStreetMap[i].GetComponent<IntersectionParams>().perpendicularStreetWidth >= streetWidthPSCutoff) && (intersectionStreetMap[i].GetComponent<IntersectionParams>().perpendicularStreetWidth <= streetWidthSmallCarCutoff))
            {
                MapCW(i);
            }
            
            else if (i % 2 == 1)
            {
                MapCCW(i);
            }
            
        }

        /*CW scenarios when less than 2, are staying less than 2 as the condition streetWidth cutoff isn"t changing
         * This is because street width is never under 5 feet.
         */


        if (appropriateCWScenarios.Count < 2)
        {
            psHeight = 0.5f * psHeight;
            streetWidthPSCutoff = psHeight * psManipulation / Mathf.Tan((BCVA / 240) * Mathf.PI / 180);
            Debug.Log("CW Scenarios under 2");
            //Debug.Log("streetWidthPSCutoff: " + streetWidthPSCutoff);
            for (i = 0; i < 4; i++)
            {

                if (i % 2 == 0) //&& (intersectionStreetMap[i].GetComponent<IntersectionParams>().perpendicularStreetWidth >= streetWidthPSCutoff))
                {
                    MapCW(i);
                }
            }
        }

        for (i = 0; i < intersectionStreetMap.Length; i++)
        {
            //intersectionStreetMap[i].SetActive(false);
        }

        foreach (StreetMapping street in appropriateCWScenarios)
        {

            Instantiate(street.intersect, AppropriateCWScenarioParent);

        }
        for (i = 0; i < AppropriateCWScenarioParent.childCount; i++)
        {
            AppropriateCWScenarioParent.GetChild(i).tag = "AppropriateCWScenario";
        }
        appropriateCWScenario = GameObject.FindGameObjectsWithTag("AppropriateCWScenario");






        foreach (StreetMapping street in appropriateCCWScenarios)
        {

            Instantiate(street.intersect, AppropriateCCWScenarioParent);

        }
        for (i = 0; i < AppropriateCCWScenarioParent.childCount; i++)
        {
            AppropriateCCWScenarioParent.GetChild(i).tag = "AppropriateCCWScenario";
        }
        appropriateCCWScenario = GameObject.FindGameObjectsWithTag("AppropriateCCWScenario");

    }




    //[Old Code, Will remove beyond this point]

     /*   public void Start()
    {
        streetMapping.Clear();
        
        suggestion.text = "Select and submit parameters";
        count = 0;
        for (i = 0; i < intersection.Length; i++)
        {
            for (j = 0; j <= 7; j++)
            {
                streetMapping.Add(new StreetMapping(intersection[i], j));
            }
            
        }
        
        j = 0;
        foreach(StreetMapping street in streetMapping)
        {
            
            street.intersect.GetComponent<IntersectionParams>().CornerDirection(j);
            Instantiate(street.intersect , StreetMappingParent);
            j++;
            if (j > 7)
                j = 0;
            //Debug.Log("j =" + j);
            //Debug.Log("Street list item:" + street.intersect + "  " + street.spawnDir);
        }
        for(i = 0; i<StreetMappingParent.childCount;i++)
        {
            StreetMappingParent.GetChild(i).tag = "StreetMapping";
        }
        intersectionStreetMap = GameObject.FindGameObjectsWithTag("StreetMapping");
        for(count = 0; count < intersection.Length; count++)
        {
            intersection[count].SetActive(false);
        }
        LoadIntersection();
        
    }

    public void Mapping()
    {
        BCVA = visualImpairment;
        streetWidthSmallCarCutoff = carHeightMin / Mathf.Tan((BCVA / 240) * Mathf.PI / 180);
        streetWidthLargeCarCutoff = carHeightMax / Mathf.Tan((BCVA / 240) * Mathf.PI / 180);

        streetWidthPSCutoff = psHeight * psManipulation / Mathf.Tan((BCVA / 240) * Mathf.PI / 180);


        if(crossDirType == "ClockW" && (intersectionStreetMap[i].GetComponent<IntersectionParams>().perpendicularStreetWidth >= streetWidthPSCutoff) && (intersectionStreetMap[i].GetComponent<IntersectionParams>().perpendicularStreetWidth <= streetWidthSmallCarCutoff))
        {
            
        }

        if(crossDirType == "CounterClock")
        {

        }

        Debug.Log("In Mapping function");

        if (severity == "Mild")
        {
            PerpendicularStreets.Clear();
            Debug.Log("Mild mapping  ");
            for(i=0;i<intersectionStreetMap.Length;i++)
            {
                PerpendicularStreets.Add(new StreetMapping(intersectionStreetMap[i], intersectionStreetMap[i].GetComponent<IntersectionParams>().cornerDir));
            }


        }
        else if (severity == "Moderate")
        {
            PerpendicularStreets.Clear();
            Debug.Log("Moderate mapping  ");
            for (i = 0; i < intersectionStreetMap.Length; i++)
            {
                if (intersectionStreetMap[i].GetComponent<IntersectionParams>().ThisStreetWidth == 0 || intersectionStreetMap[i].GetComponent<IntersectionParams>().ThisStreetWidth == 1)
                {
                    PerpendicularStreets.Add(new StreetMapping(intersectionStreetMap[i], intersectionStreetMap[i].GetComponent<IntersectionParams>().cornerDir));
                }
                
            }
            
            

        }
        else if (severity == "Serious")
        {
            PerpendicularStreets.Clear();
            Debug.Log("Serious mapping  ");
            for (i = 0; i < intersectionStreetMap.Length; i++)
            {
                if (intersectionStreetMap[i].GetComponent<IntersectionParams>().ThisStreetWidth == 0)
                {
                    PerpendicularStreets.Add(new StreetMapping(intersectionStreetMap[i], intersectionStreetMap[i].GetComponent<IntersectionParams>().cornerDir));
                }
                
            }

            
        }


        foreach (StreetMapping street in PerpendicularStreets)
        {
            //Debug.Log("Perpendicular list item:" + street.intersect + " Perpendicular ThisStWidth: " + street.intersect.GetComponent<IntersectionParams>().ThisStreetWidth + " CornerDir: " + street.spawnDir);
            Instantiate(street.intersect, PerpendicularStreetParent);
            
        }
        for (i = 0; i < PerpendicularStreetParent.childCount; i++)
        {
            PerpendicularStreetParent.GetChild(i).tag = "PerpendicularStreet";
        }
        PerpendicularStreet = GameObject.FindGameObjectsWithTag("PerpendicularStreet");
        

        for (count = 0; count < intersectionStreetMap.Length; count++)
        {
            intersectionStreetMap[count].SetActive(false);
        }
    }







    public void LoadIntersection()
    {



        age = SelectedAge;
        type = SelectedType;
        severity = SelectedSeverity;
        corner = SelectedCorner;
        direction = SelectedDirection;
        crossDirType = SelectedCrossingType;

        Mapping();
        
        







        

        if(crossDirType == "ClockW")
        {
            Debug.Log("Crossing Type is Clockwise");
            //cornerDir = 1, 3, 5, 7
            
            for (i = 0; i < PerpendicularStreet.Length; i++)
            {
                if (PerpendicularStreet[i].GetComponent<IntersectionParams>().cornerDir % 2 == 0)
                {
                    appropriateScenarios.Add(new StreetMapping(PerpendicularStreet[i], PerpendicularStreet[i].GetComponent<IntersectionParams>().cornerDir));
                }

                else
                    continue;

            }
            foreach (StreetMapping street in appropriateScenarios)
            {

                Instantiate(street.intersect, AppropriateScenarioParent);

            }
            for (i = 0; i < AppropriateScenarioParent.childCount; i++)
            {
                AppropriateScenarioParent.GetChild(i).tag = "AppropriateScenario";
            }
            appropriateScenario = GameObject.FindGameObjectsWithTag("AppropriateScenario");

            for(i=0;i<PerpendicularStreet.Length;i++)
            {
                PerpendicularStreet[i].SetActive(false);
            }
        }
        else if(crossDirType == "CounterClock")
        {
            Debug.Log("Crossing Type is CounterClock");
            //cornerDir = 0, 2, 4, 6  
            for (i = 0; i < PerpendicularStreet.Length; i++)
            {
                if (PerpendicularStreet[i].GetComponent<IntersectionParams>().cornerDir % 2 != 0)
                {
                    appropriateScenarios.Add(new StreetMapping(PerpendicularStreet[i], PerpendicularStreet[i].GetComponent<IntersectionParams>().cornerDir));
                }

                else
                    continue;

            }
            foreach (StreetMapping street in appropriateScenarios)
            {

                Instantiate(street.intersect, AppropriateScenarioParent);

            }
            for (i = 0; i < AppropriateScenarioParent.childCount; i++)
            {
                AppropriateScenarioParent.GetChild(i).tag = "AppropriateScenario";
            }
            appropriateScenario = GameObject.FindGameObjectsWithTag("AppropriateScenario");

            for (i = 0; i < PerpendicularStreet.Length; i++)
            {
                PerpendicularStreet[i].SetActive(false);
            }
        }
        Debug.Log("Appr scenario length = " + appropriateScenario.Length);
        theScenario = appropriateScenario[Random.Range(0, appropriateScenario.Length)];
        Debug.Log("Loading scenario = " + theScenario + theScenario.GetComponent<IntersectionParams>().corner + theScenario.GetComponent<IntersectionParams>().direction);
        Instantiate(theScenario);
        theScenario.tag = "CurrentScenario";


        for (i = 0; i < appropriateScenario.Length; i++)
        {
            //Debug.Log("Appropirate Scenarios are: " + appropriateScenario[i]);
            appropriateScenario[i].SetActive(false);
        }

        MainCam.GetComponent<CameraLocations>().SpawnPlayer(theScenario.GetComponent<IntersectionParams>().cornerDir);

        //Picking a random scenario



        /*if (corner == "SW")
        {
            if (direction == "N")
                j = 0;
            if (direction == "E")
                j = 1;
        }
        if (corner == "SE")
        {
            if (direction == "W")
                j = 2;
            if (direction == "N")
                j = 3;
        }
        if (corner == "NE")
        {
            if (direction == "S")
                j = 4;
            if (direction == "W")
                j = 5;
        }
        if (corner == "NW")
        {
            if (direction == "E")
                j = 6;
            if (direction == "S")
                j = 7;
        }
        
        
        if(age == "Young")
        {
            if(severity == "Serious")
            {
                count = Random.Range(0,1);
                suggestion.text = "Loading intersection["+ count + "] = " + intersection[count].name;
                intersection[count].SetActive(true);
            }
            else if(severity == "Moderate")
            {
                count = Random.Range(0, 3);
                suggestion.text = "Loading intersection[" + count + "] = " + intersection[count].name;
                intersection[count].SetActive(true);
            }
            else
            {
                count = Random.Range(0,4);
                suggestion.text = "Loading intersection[" + count + "] = " + intersection[count].name;
                intersection[count].SetActive(true);
            }

        }

        else
        {
            if (severity == "Serious")
            {
                count = Random.Range(0, 0);
                suggestion.text = "Loading intersection[" + count + "] = " + intersection[count].name;
                intersection[count].SetActive(true);
            }
            else if (severity == "Moderate")
            {
                count = Random.Range(0, 2);
                suggestion.text = "Loading intersection[" + count + "] = " + intersection[count].name;
                intersection[count].SetActive(true);
            }
            else
            {
                count = Random.Range(0, 3);
                suggestion.text = "Loading intersection[" + count + "] = " + intersection[count].name;
                intersection[count].SetActive(true);
            }
        }

        Debug.Log(count);
        
        if (Input.GetKeyDown("up"))
        {
            if (count == intersection.Length - 1)
                count = 0;
            else
                count++;
        }
        if (Input.GetKeyDown("down"))
        {
            if (count == 0)
                count = intersection.Length - 1;
            else
                count--;
        
    }*/
}
