using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class ConceptSelect : MonoBehaviour
{


    /*
    conceptParaPerpTraffic = 1;
    conceptNLPT = 2;
    conceptNLPTStop = 3;
    conceptNLPTSurge = 4;
    conceptConfirmNLPTS = 5;
    conceptMakeDecision = 6;
    conceptFinalEvaluation = 7;
    */
    public GameObject IntersectionObject;

    public int concept;
    public List<string> conceptNames = new List<string>() { "conceptParaPerpTraffic" , "conceptNLPT" , "conceptNLPTStop" , "conceptNLPTSurge","conceptConfirmNLPTS","conceptMakeDecision","conceptFinalEvaluation"};
    public string currConcept;

    public int keyEvent;
    int keyEventNone = 0, keyEventParaRed2Green = 1, keyEventParaGreen2Red = 2;

    public int keyCar;
    int keyCarNone = 0, keyCarYes = 1;

    public int keyCarSE;
    int keyCarSENone = 0, keyCarSEGlow = 1 ,keyCarSEHonk = 2;

    public int firstCar;
    int firstCarAny = 0, firstCarStraightGoing = 1, firstCarRightTurn = 2;

    public int learnerResponse;
    int learnerResponseNone = 0, learnerResponsePoint = 1 , learnerResponseVerbalNow = 2 , learnerResponseVerbalGo = 3;

    int learnerState;
    int learnerStateNovice = 0, learnerStateJourneyman = 1, learnerStateExpert = 2;

    int age;
    int ageChild = 1, ageAdult = 2;

    int lane;
    int laneNLPT = 1, laneAll = 2;

    int street;
    int streetPerpendicular = 0, streetParallel = 1, streetBoth = 2;

    int rand, i, j, flagUsed = 0;
    int ln, st;
    int[] UsedIndex = { -1 };

    string purpose;

    public GameObject[] scenarioCW;
    public GameObject[] scenarioCCW;
    public GameObject[] q;
    public GameObject[] qq;

    
    public void ConceptSelection(int con)
    {
        concept = con - 1;
        currConcept = conceptNames[concept];

        if(currConcept == "conceptParaPerpTraffic" || currConcept == "conceptNLPT")
        {
            Debug.Log(currConcept);
            keyEvent = keyEventNone;
            keyCar = keyCarNone;
            firstCar = firstCarAny;
            learnerResponse = learnerResponsePoint;
            age = IntersectionObject.GetComponent<TrafficIntersections>().SelectedAge;
            learnerState = IntersectionObject.GetComponent<TrafficIntersections>().SelectedState;
            purpose = IntersectionObject.GetComponent<TrafficIntersections>().SelectedPurpose;
            if (currConcept == "conceptParaPerpTraffic")
            {
                runCPPT();
            }
            else
            {
                runCNLPT();
            }
        }
        
        else if(currConcept == "conceptNLPTStop")
        {
            keyEvent = keyEventParaGreen2Red;
            keyCar = keyCarYes;
            firstCar = firstCarAny;
            learnerResponse = learnerResponsePoint;

        }
        else if (currConcept == "conceptNLPTSurge")
        {
            keyEvent = keyEventParaRed2Green;
            keyCar = keyCarYes;
            learnerResponse = learnerResponseVerbalNow;
            firstCar = firstCarAny;

        }
        //In the pseudo code I have, 4 and 5 are same. But in email 4 and 5 are different

        else if (currConcept == "conceptConfirmNLPTS")
        {
            keyEvent = keyEventParaRed2Green;
            keyCar = keyCarYes;
            learnerResponse = learnerResponseVerbalNow;
            firstCar = Random.Range(1,3);

        }

        else if(currConcept == "conceptMakeDecision")
        {
            keyEvent = keyEventParaRed2Green;
            keyCar = keyCarYes;
            learnerResponse = learnerResponseVerbalGo;
            firstCar = Random.Range(1, 3);

        }
        else if(currConcept == "conceptFinalEvaluation")
        {
            keyEvent = keyEventNone;
            keyCar = keyCarYes;
            learnerResponse = learnerResponseVerbalGo;
            firstCar = Random.Range(1, 3);

        }

    }


    void Streets(int street)
    {
        if (street == 0)
        {
            GameObject.FindGameObjectWithTag("StreetPerpendicular").SetActive(true);
            GameObject.FindGameObjectWithTag("StreetParallel").SetActive(false);
        }
        else if (street == 1)
        {
            GameObject.FindGameObjectWithTag("StreetPerpendicular").SetActive(false);
            GameObject.FindGameObjectWithTag("StreetParallel").SetActive(true);
        }
        else if(street == 2)
        {
            GameObject.FindGameObjectWithTag("StreetPerpendicular").SetActive(true);
            GameObject.FindGameObjectWithTag("StreetParallel").SetActive(true);
        }    
    }

    void Lanes(int lane)
    {
        if (lane == 1)
        {
            GameObject.FindGameObjectWithTag("NLPTLane").SetActive(true);
            GameObject.FindGameObjectWithTag("BothLanes").SetActive(false);
        }
        if(lane == 2)
        {
            GameObject.FindGameObjectWithTag("NLPTLane").SetActive(false);
            GameObject.FindGameObjectWithTag("BothLanes").SetActive(true);
        }    
    }

    public int RandomIndex(int r)
    {
        return rand = Random.Range(0, r);
    }
    
    public void SelectScenarios(GameObject[] apprSc, int size, int[] used, GameObject[] Sc)
    {
        
        for (i = 0; i < size; i++)
        {
            flagUsed = 0;
            int ind = RandomIndex(apprSc.Length);
            Debug.Log("ind = "+ind);
            while(used.Contains(ind))
            {
                
                ind = RandomIndex(apprSc.Length);
                Debug.Log("ind = " + ind);
            }
            used[0] = ind;
            Sc[i] = apprSc[i]; 
            for (j = rand - 1; j < apprSc.Length; j++)
            {
                Debug.Log("Potato");
                apprSc[j] = apprSc[j + 1];
            }
            
            
            
        }


    }

    void runCPPT()
    {
        Debug.Log("Running concept 1");

        scenarioCW = GameObject.FindGameObjectsWithTag("AppropriateCWScenario");
        scenarioCCW = GameObject.FindGameObjectsWithTag("AppropriateCCWScenario");

        //in pseudo code it is scw = selectScenarios(apCWsc, size, used) but instead scw is being passed into the function as well


        //SelectScenarios(IntersectionObject.GetComponent<TrafficIntersections>().appropriateCWScenario, 2, UsedIndex, scenarioCW);
        //SelectScenarios(IntersectionObject.GetComponent<TrafficIntersections>().appropriateCCWScenario, 2, UsedIndex, scenarioCCW);
        //Debug.Log(IntersectionObject.GetComponent<TrafficIntersections>().appropriateCWScenario);

        //in pseudo code it is scw = selectScenarios(apCWsc, size, used) but instead scw is being passed into the function as well


        q = scenarioCW.Concat(scenarioCCW).ToArray();
        
        if (purpose == "purposeInstruction")
        {
            Debug.Log("Purpose is Instruction");
            if (learnerState == learnerStateNovice && age == ageChild)
            {
                for (i = 0; i<q.Length + 1;i++)
                {
                    if (i > 1)
                        st = 2;
                    else
                        st = i;
                    Debug.Log("st = " + st);
                    Streets(st);
                    Lanes(laneAll);

                    if (Random.Range(0,1) == 0)
                    {
                        if (st == 0)
                            Debug.Log("The Moving vehicle is Perpendicular Traffic");
                        else if(st == 1)
                            Debug.Log("The Moving vehicle is Parallel Traffic");
                    }
                    else
                    {
                        if (st == 0)
                            Debug.Log("The Moving vehicle is Perpendicular Traffic");
                        else if (st == 1)
                            Debug.Log("The Moving vehicle is Parallel Traffic");
                    }
                }
            }
            else 
            {
                for (i = 0; i < q.Length + 1; i++)
                {
                    if (i > 1)
                        st = 2;
                    else
                        st = i;
                    Debug.Log("st = " + st);
                    Streets(st);
                    Lanes(laneAll);

                    if (Random.Range(0, 1) == 0)
                    {
                        if (st == 0)
                            Debug.Log("The Moving vehicle is Perpendicular Traffic");
                        else if (st == 1)
                            Debug.Log("The Moving vehicle is Parallel Traffic");
                    }
                    else
                    {
                        if (st == 0)
                            Debug.Log("The Moving vehicle is Perpendicular Traffic");
                        else if (st == 1)
                            Debug.Log("The Moving vehicle is Parallel Traffic");
                    }
                }
            }
        }
        else if(purpose == "purposePractice" || purpose == "purposeAssessment")
        {
            
            for (i = 0; i < q.Length + 1; i++)
            {
                st = Random.Range(0, 2);
                Streets(st);
                Lanes(laneAll);

                if (Random.Range(0, 1) == 0)
                {
                    if (st == 0)
                        Debug.Log("The Moving vehicle is Perpendicular Traffic");
                    else if (st == 1)
                        Debug.Log("The Moving vehicle is Parallel Traffic");
                }
                else
                {
                    if (st == 0)
                        Debug.Log("The Moving vehicle is Perpendicular Traffic");
                    else if (st == 1)
                        Debug.Log("The Moving vehicle is Parallel Traffic");
                }
            }
        }

    }

    void runCNLPT()
    {

    }
    
}
