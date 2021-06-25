using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mapScenarios : MonoBehaviour
{
    public Text ResponseText;
    public GameObject Box;
    public GameObject Camera;
    public GameObject[] spawnLocations;
    int counter = 0;
    int[] crossingScenarios;
    bool loadingStarted = false;
    float secondsLeft = 0;
    float respWin;
    int Scores = 0;
    int Rewards = 0;
    bool RewardStatus = false;
    int sceneDuration;
    public int CNum = 0;
    public string CStr = "";
    public int VNum = 0;
    public string VStr = "";
    DropdownMenuOp O = new DropdownMenuOp();
    DropdownMenuSame S = new DropdownMenuSame();
    DropdownCrossing C = new DropdownCrossing();
    DropdownVehicle V = new DropdownVehicle();
    int SideIndex;
    string Side;
    int ic = 0;
    int lanes;
    float shiftEW = 0.0f;
    float shiftNS = 0.0f;
    int obj1 = 0;
    int obj2 = 0;
    int obj3 = 0;
    int obj4 = 0;
    int iobj = 0;

    public GameObject TargetObject;
    public GameObject Object1;
    public GameObject Object2;
    public GameObject Object3;
    public GameObject Object4;

    //Intersection1
    public GameObject Car5I1;
    public GameObject Car0I1;
    public GameObject Van5I1;
    public GameObject Van0I1;
    public GameObject Bus5I1;
    public GameObject Bus0I1;
    public GameObject Truck5I1;
    public GameObject Truck0I1;

    //Intersection2
    public GameObject Car10I2;
    public GameObject Car6I2;
    public GameObject Car1I2;
    public GameObject Van10I2;
    public GameObject Van6I2;
    public GameObject Van1I2;
    public GameObject Bus10I2;
    public GameObject Bus6I2;
    public GameObject Bus1I2;
    public GameObject Truck10I2;
    public GameObject Truck6I2;
    public GameObject Truck1I2;

    //Intersection3
    public GameObject Car11I3;
    public GameObject Car7I3;
    public GameObject Car2I3;
    public GameObject Van11I3;
    public GameObject Van7I3;
    public GameObject Van2I3;
    public GameObject Bus11I3;
    public GameObject Bus7I3;
    public GameObject Bus2I3;
    public GameObject Truck11I3;
    public GameObject Truck7I3;
    public GameObject Truck2I3;

    //Intersection4
    public GameObject Car12I4;
    public GameObject Car8I4;
    public GameObject Car3I4;
    public GameObject Car14I4;
    public GameObject Van12I4;
    public GameObject Van8I4;
    public GameObject Van3I4;
    public GameObject Van14I4;
    public GameObject Bus12I4;
    public GameObject Bus8I4;
    public GameObject Bus3I4;
    public GameObject Bus14I4;
    public GameObject Truck12I4;
    public GameObject Truck8I4;
    public GameObject Truck3I4;
    public GameObject Truck14I4;

    //Intersection5
    public GameObject Car13I5;
    public GameObject Car9I5;
    public GameObject Car4I5;
    public GameObject Car15I5;
    public GameObject Van13I5;
    public GameObject Van9I5;
    public GameObject Van4I5;
    public GameObject Van15I5;
    public GameObject Bus13I5;
    public GameObject Bus9I5;
    public GameObject Bus4I5;
    public GameObject Bus15I5;
    public GameObject Truck13I5;
    public GameObject Truck9I5;
    public GameObject Truck4I5;
    public GameObject Truck15I5;

    [SerializeField] GameObject[] intersections;

    public GameObject TrafficSignal1;
    public GameObject TrafficSignal2;
    public GameObject TrafficSignal3;
    public GameObject TrafficSignal4;
    public GameObject TrafficSignal5;

    public AudioSource audioSource;

    public AudioClip voice1;
    public AudioClip voice2;

    public AudioClip voice11;
    public AudioClip voice12;
    public AudioClip voice13;
    public AudioClip voice14;
    public AudioClip voice15;
    public AudioClip voice16;
    public AudioClip voice17;
    public AudioClip voice18;

    public AudioClip voice21;
    public AudioClip voice22;
    public AudioClip voice23;
    public AudioClip voice24;

    public AudioClip voice41;
    public AudioClip voice42;
    public AudioClip voice43;
    public AudioClip voice44;

    public AudioClip voice51;
    public AudioClip voice52;
    public AudioClip voice53;
    public AudioClip voice54;
    public AudioClip voice55;
    public AudioClip voice56;
    public AudioClip voice57;
    public AudioClip voice58;

    public AudioClip voice61;
    public AudioClip voice62;
    public AudioClip voice63;
    public AudioClip voice64;
    public AudioClip voice65;
    public AudioClip voice66;
    public AudioClip voice67;
    public AudioClip voice68;

    public AudioClip voice101;
    public AudioClip voice102;

    public AudioClip voice111;
    public AudioClip voice112;

    public AudioClip voice121;
    public AudioClip voice131;
    public AudioClip voice141;
    public AudioClip voice151;

    public AudioClip voice201;

    int i, j;

    double paraIndex;
    double perpIndex;
    double numWays;
    int crossIndex;
    int numCrossings;
    double cutoffWidthPS;
    double cutoffWidthCarMin;
    int[] oppositeUsed;
    int[] sameUsed;

    public int[] visualImpairment = { 20, 200, 300, 400, 500, 600, 2000 };

    int learnerID, concept, purpose, learnerState, age, BCVA;
    string[] strConcept = { "ParaPerpTraffic", "NLPT", "NLPTStop", "NLPTSurge", "ConfirmNLPTS", "MakeDecision", "FinalEvaluation" };
    string[] strAge = { "child", "adult" };
    string[] strLearnerState = { "novice", "journeyman", "expert" };
    string[] strPurpose = { "instruction", "practice", "assessment", "evaluation" };
    //string[] strRequest = "learnerID: " + learnerID + ", concept: " strConcept{concept}", purpose: " strPurpose{ purpose}"; learner stat: " strLearnerState{ learnerState}"; age: " strAge{ age}"; BCVA: 20/" num2str(BCVA)];

    double laneWidth = 3.5;
    double divideWidth = 3;
    double carHeightMax = 4.42;
    double carHeightMin = 1.5;
    double psHeight = 0.254;

    double streetDirectionNS = 2;
    double streetDirectionEW = 1;
    double laneDirectionN = 1;
    double laneDirectionS = 2;
    double laneDirectionE = 3;
    double laneDirectionW = 4;
    double streetCornerNE = 1;
    double streetCornerNW = 2;
    double streetCornerSW = 3;
    double streetCornerSE = 4;
    double parallelStreet = 1;
    double perpendicularStreet = 2;
    double crossingDirectionN = 1;
    double crossingDirectionS = 2;
    double crossingDirectionE = 3;
    double crossingDirectionW = 4;
    double crossingTypeCW = 1;
    double crossingTypeCCW = 2;
    double nearLaneSameSide = 1;
    double nearLaneOppositeSide = 2;

    double conceptParaPerpTraffic = 1;
    double conceptNLPT = 2;
    double conceptNLPTStop = 3;
    double conceptNLPTSurge = 4;
    double conceptConfirmNLPTS = 5;
    double conceptMakeDecision = 6;
    double conceptFinalEvaluation = 7;

    double purposeInstruction = 1;
    double purposePractice = 2;
    double purposeAssessment = 3;
    double purposeFinalEvaluation = 4;

    double learnerStateNovice = 1;
    double learnerStateJourneyman = 2;
    double learnerStateExpert = 3;

    double ageChild = 1;
    double ageAdult = 2;

    double motivationLow = 1;
    double motivationHigh = 2;

    //scenario parameters
    int streetPerpendicular = -1; //which street has traffic
    int streetParallel = 1;
    int streetBoth = 2;

    double laneNLPT = 1; // which lane has traffic
    double laneAll = 2;

    double crossingCCW = 1; // crossing types
    double crossingCW = 2;

    double keyCarNone = 0; // key car(designated straight going car)
    double keyCarYes = 1;

    double keyCarSENone = 0; // key car special effects
    double keyCarSEGlow = 1;
    double keyCarSEHunk = 2;

    double keyEventNone = 0; // key events
    double keyEventParaRed2Green = 1;
    double keyEventParaGreen2Red = 2;

    int firstCarAny = 0; // first car(the first car waiting for light change) behavior
    int firstCarStraightGoing = 1; // first car in NLPT at the red light is a straight going car.
    int firstCarRightTurn = 2; // first car in NLPT at the red light is a right turn car.The keyCar is second.

    int learnerResponseNone = 0;
    int learnerResponsePoint = 1;
    int learnerResponseVerbalNow = 2;
    int learnerResponseVerbalGo = 3;

    double feedbackNone = 0;
    double feedbackYes = 1;

    //verbal instruction indices
    //recordings for instruction phase range from 1 to 100 
    //concept 1: 1 to 10
    double instruct_parallelMoving = 1; //'The moving traffic is parallel Traffic'
    double instruct_perpendicularMoving = 2; //'The moving traffic is Perpendicular traffic'
    //concept 2: 11 to 20
    double instruct_NLPTSameSideMovingRight = 11; //'The near lane parallel traffic is over your RIGHT shoulder. It is now moving’;
    double instruct_NLPTSameSideStopRight = 12; //'The near lane parallel traffic is over your RIGHT shoulder. It is now stopped’;
    double instruct_NLPTSameSideMovingLeft = 13; //'The near lane parallel traffic is over your LEFT shoulder. It is now moving’;
    double instruct_NLPTSameSideStopLeft = 14; //'The near lane parallel traffic is over your LEFT shoulder. It is now stopped’;
    double instruct_NLPTOppositeSideMovingRight = 15; //'The near lane parallel traffic is across the street on your RIGHT side. It is now moving’;
    double instruct_NLPTOppositeSideStopRight = 16; //'The near lane parallel traffic is across the street on your RIGHT side. It is now stopped’;
    double instruct_NLPTOppositeSideMovingLeft = 17; //'The near lane parallel traffic is across the street on your LEFT side. It is now moving’;
    double instruct_NLPTOppositeSideStopLeft = 18; //'The near lane parallel traffic is across the street on your LEFT side. It is now stopped’;
    //concept 3: 21 to 30
    double instruct_NLPTSameSideStopWhereRight = 21; //'The near lane parallel traffic is over your RIGHT shoulder. It is now stopped. Notice when and where it stoppes.’;    
    double instruct_NLPTSameSideStopWhereLeft = 22; //'The near lane parallel traffic is over your LEFT shoulder. It is now stopped. Notice when and where it stoppes.’;    
    double instruct_NLPTOppositeSideStopWhereRight = 23; //'The near lane parallel traffic is across the street on your RIGHT side. It is now stopped. Notice when and where it stoppes’; 
    double instruct_NLPTOppositeSideStopWhereLeft = 24; //'The near lane parallel traffic is across the street on your LEFT side. It is now stopped. Notice when and where it stoppes’; 
    //concept 4: 41 to 50
    double instruct_NLPTSameSideSurgeRight = 41; //'The near lane parallel traffic is over your RIGHT shoulder. It will soon start to shiftEW. This is called a NLPT surge.’;
    double instruct_NLPTSameSideSurgeLeft = 42; //'The near lane parallel traffic is over your LEFT shoulder. It will soon start to shiftEW. This is called a NLPT surge.’;
    double instruct_NLPTOppositeSideSurgeRight = 43; //'The near lane parallel traffic is across the street on your RIGHT side. It will soon start to shiftEW. This is called a NLPT surge.’;
    double instruct_NLPTOppositeSideSurgeLeft = 44; //'The near lane parallel traffic is across the street on your LEFT side. It will soon start to shiftEW. This is called a NLPT surge.’;
    //concept 5: 51 to 60
    double instruct_NLPTSameSideStraightRight = 51; //'The first near lane parallel traffic car is over your RIGHT shoulder. It will go into the intersection. This means you can start to cross the street.’;
    double instruct_NLPTSameSideStraightLeft = 52; //'The first near lane parallel traffic car is over your LEFT shoulder. It will go into the intersection. This means you can start to cross the street.’;
    double instruct_NLPTSameSideTurnRight = 53; //'The first near lane parallel traffic car is over your RIGHT shoulder. It will make a right turn. You need to wait for a car entering the intersection.’;
    double instruct_NLPTSameSideTurnLeft = 54; //'The first near lane parallel traffic car is over your LEFT shoulder. It will make a right turn. You need to wait for a car entering the intersection.’;
    double instruct_NLPTOppositeSideStraightRight = 55; //'The first near lane parallel traffic car is across the street on your RIGHT side. It will go into the intersection. This means you can start to cross the street.’;
    double instruct_NLPTOppositeSideStraightLeft = 56; //'The first near lane parallel traffic car is across the street on your LEFT side. It will go into the intersection. This means you can start to cross the street.’;
    double instruct_NLPTOppositeSideTurnRight = 57; //'The first near lane parallel traffic car is across the street on your RIGHT side. It will make a right turn. You need to wait for a car entering the intersection.’;
    double instruct_NLPTOppositeSideTurnLeft = 58; //'The first near lane parallel traffic car is across the street on your LEFT side. It will make a right turn. You need to wait for a car entering the intersection.’;

    // 58 was not converted

    //concept 6: 61 to 70
    double instruct_NLPTSameSideStraightDecisionRight = 61; //'The first near lane parallel traffic car is over your RIGHT shoulder. It will go into the intersection. It is safe to start crossing the street as soon as you see it entering the intersection.’;
    double instruct_NLPTSameSideStraightDecisionLeft = 62; //'The first near lane parallel traffic car is over your LEFT shoulder. It will go into the intersection. It is safe to start crossing the street as soon as you see it entering the intersection.’;
    double instruct_NLPTSameSideTurnDecisionRight = 63; //'The first near lane parallel traffic car is over your RIGHT shoulder. It will make a right turn. It is not safe to start crossing the street. Wait until see a car entering the intersection.’;
    double instruct_NLPTSameSideTurnDecisionLeft = 64; //'The first near lane parallel traffic car is over your LEFT shoulder. It will make a right turn. It is not safe to start crossing the street. Wait until see a car entering the intersection.’;
    double instruct_NLPTOppositeSideStraightDecisionRight = 65; //'The first near lane parallel traffic car is across the street on your RIGHT side. It will go into the intersection. It is safe to start crossing the street as soon as you see it entering the intersection.’;
    double instruct_NLPTOppositeSideStraightDecisionLeft = 66; //'The first near lane parallel traffic car is across the street on your LEFT side. It will go into the intersection. It is safe to start crossing the street as soon as you see it entering the intersection.’;
    double instruct_NLPTOppositeSideTurnDecisionRight = 67; //'The first near lane parallel traffic car is across the street on your RIGHT side. It will make a right turn. It is not safe to start crossing the street. Wait until see a car entering the intersection.’;
    double instruct_NLPTOppositeSideTurnDecisionLeft = 68; //'The first near lane parallel traffic car is across the street on your LEFT side. It will make a right turn. It is not safe to start crossing the street. Wait until see a car entering the intersection.’;

    //recordings for practice/assessment phases range from 101 to 200
    //concept 1: 101 to 110
    double practice_point2Parallel = 101; // 'Use the pointer to point at the parallel traffic';
    double practice_point2Perpendicular = 102; //'Use the pointer to point at the Perpendicular traffic';
    //concept 2: 111 to 120
    double practic_Point2NLPTStop = 111; //'Point to the near lane parallel traffic when it stoppes.’;
    double practic_Point2NLPTMoving = 112; //'Point to the near lane parallel traffic when it is moving.’;
    //concept 3: 121 to 130
    double practic_Point2NLPTStopWhere = 121; //'Point to where the near lane parallel traffic stoppes.’;
    //concept 4: 131 to 140
    double practice_NLPTSurge = 131; //'Say 'Now' when you detect a NLPT surge.’;
    //concept 5: 141 to 150
    double practice_NLPTSurgeConfirm = 141; //'Say 'Now' when you see a NLPT car enters the intersection.’;
    //concept 6: 151 to 160
    double practice_crossingDecision = 151; //'Say 'Go' when it is safe to start crossing the street.’;

    //recordings for evaluation
    double evaluation_NLPTS = 201; //'Say 'Go' when it is safe to start crossing the street.’;

    double[,] voice = new double[10, 2];
    double[] streetWithTraffic = new double[10];
    double[] Lanes = new double[10];
    double[] startMovingTraffic = new double[10];
    double[] collectLearnerResponse = new double[10]; // leanrer does not give responces in the instruction phase
    double[] useKeyEvent = new double[10]; // no key event for this concept
    double[] useKeyCar = new double[10]; // no key car
    double[] keyCarSE = new double[10];  // no key car special effect
    double[] firstCar = new double[10];  // no need to define first car behavior
    double[] feedback = new double[10];

    public class Way
    {
        public double laneDirection = 0;

        public double lanes = 0;
        public Way(double laneDir, double lns)
        {
            laneDirection = laneDir;
            lanes = lns;
        }
    }

    public Way[] waytmp = new Way[2];

    public class Street
    {
        public double streetIntersection;
        public string direction;
        public double numWays;
        public Way[] ways = new Way[2];
        public double divide;
        public double width;
        int i;

        public Street(double stInter, string dir, double numW, Way[] ws, double div, double wdt)
        {
            streetIntersection = stInter;
            direction = dir;
            numWays = numW;
            for (i = 0; i < ws.Count(); i++)
            {
                ways[i] = ws[i];
            }

            divide = div;
            width = wdt;
        }
    }

    public Street[] streets = new Street[10];

    double[] intersectIndices = { 1.0, 1.0, 2.0, 2.0, 3.0, 3.0, 4.0, 4.0, 5.0, 5.0 };
    string[] streetDirections = { "streetDirectionNS", "streetDirectionEW", "streetDirectionNS", "streetDirectionEW", "streetDirectionNS", "streetDirectionEW", "streetDirectionNS", "streetDirectionEW", "streetDirectionNS", "streetDirectionEW" };

    public double[,] wayPara = new double[,] {{1.0, 1.0, 0.0, 0.0},
                                { 3.0, 2.0, 0.0, 0.0},
                                { 1.0, 2.0, 0.0, 0.0},
                                { 3.0, 2.0, 4.0, 2.0},
                                { 1.0, 3.0, 0.0, 0.0},
                                { 3.0, 2.0, 4.0, 2.0},
                                { 1.0, 3.0, 2.0, 3.0},
                                { 3.0, 2.0, 4.0, 1.0},
                                { 1.0, 3.0, 2.0, 3.0},
                                { 3.0, 3.0, 4.0, 2.0}};
    public double[] div = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0 };

    public class Crossings
    {
        public int intersectionIndex;
        public int cornerIndex;
        public int crossingNum;
        public int parallelStreet;
        public int perpendicularStreet;
        public int crossingDirection;
        public int type;
        public int nearLaneLoc;

        public Crossings(int ii, int ci, int cn, int pas, int pps, int cd, int tp, int nll)
        {
            intersectionIndex = ii;
            cornerIndex = ci;
            crossingNum = cn;
            parallelStreet = pas;
            perpendicularStreet = pps;
            crossingDirection = cd;
            type = tp;
            nearLaneLoc = nll;
        }
    }

    public Crossings[] crossings = new Crossings[40];

    int inter1status;
    int inter2status;
    int inter3status;
    int inter4status;
    int inter5status;

    private void Start()
    {
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }

        Box.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.1f, 0f);

        intersections = GameObject.FindGameObjectsWithTag("Intersection");
        counter = 0;

        for (i = 0; i < 10; i++)
        {
            //waytmp[0].laneDirection = wayPara[i, 0];
            //waytmp[0].lanes = wayPara[i, 1];

            waytmp[0] = new Way(wayPara[i, 0], wayPara[i, 1]);
            if (wayPara[i, 2] > 0)
            {
                //waytmp[1].laneDirection = wayPara[i, 2];
                //waytmp[1].lanes = wayPara[i, 3];
                waytmp[1] = new Way(wayPara[i, 2], wayPara[i, 3]);
                numWays = 2;
            }
            else
            {
                numWays = 1;
            }

            double sw = 0;
            double dv = div[i];

            for (j = 0; j < numWays; j++)
            {
                sw = sw + (waytmp[j].lanes + 1) * laneWidth + 0.5;
            }
            sw = sw + dv * divideWidth;

            //streets[i].streetIntersection = intersectIndices[i];
            //streets[i].direction = streetDirections[i];
            //streets[i].numWays = numWays; // because ways is now a fixate structure array(2x1), it is easier to specify number of lanes. 
            //streets[i].ways = waytmp[0];
            //streets[i].divide = dv;
            //streets[i].width = sw;

            streets[i] = new Street(intersectIndices[i], streetDirections[i], numWays, waytmp, dv, sw);
        }

        int[] perpStreets = new int[8] { 2, 1, 1, 2, 2, 1, 1, 2 };

        int[] paraStreets = new int[8] { 1, 2, 2, 1, 1, 2, 2, 1 };

        int[] crossDirs = new int[8] { 4, 2, 1, 4, 3, 1, 2, 3 };

        int[] crossTypes = new int[8] { 2, 1, 2, 1, 2, 1, 2, 1 };

        int[,] nlSides = new int[6, 8] {{2,2,1,2,1,1,2,1 },
                                      { 1,2,1,2,1,1,2,2 },
                                      { 1,2,1,2,1,1,2,2 },
                                      { 1,2,1,2,1,2,1,2 },
                                      { 1,2,1,2,1,2,1,2 },
                                      { 1,2,1,2,1,2,1,2 }};

        for (int intersection = 0; intersection < 5; intersection++)
        {
            int streettmpNS = intersection * 2;
            int streettmpEW = 2 * intersection + 1;
            //Debug.Log("streettmpNS: "+ streettmpNS);
            //Debug.Log("streettmpEW: " + streettmpEW);

            for (int crossing = 0; crossing < 8; crossing++)
            {
                if (paraStreets[crossing] == streetDirectionNS)
                {
                    paraIndex = streettmpNS;
                    //Debug.Log("paraIndex: " + paraIndex + "streettmpNS" + streettmpNS);
                    //Debug.Log("paraStreets[crossing]: " + paraStreets[crossing] + "streetDirectionNS: " + streetDirectionNS);
                }
                else if (paraStreets[crossing] == streetDirectionEW)
                {
                    paraIndex = streettmpEW;

                    //Debug.Log("paraIndex: " + paraIndex + "streettmpNS" + streettmpNS);
                    //Debug.Log("paraStreets[crossing]: " + paraStreets[crossing] + "streetDirectionEW: " + streetDirectionEW);
                }
                if (perpStreets[crossing] == streetDirectionNS)
                {
                    perpIndex = streettmpNS;
                    //Debug.Log("perpIndex: " + perpIndex + "streettmpNS" + streettmpNS);
                    //Debug.Log("perpStreets[crossing]: " + perpStreets[crossing] + "streetDirectionNS: " + streetDirectionNS);
                }
                else if (perpStreets[crossing] == streetDirectionEW)
                {
                    perpIndex = streettmpEW;
                    //Debug.Log("perpIndex: " + perpIndex + "streettmpEW" + streettmpEW);
                    //Debug.Log("perpStreets[crossing]: " + perpStreets[crossing] + "streetDirectionEW: " + streetDirectionEW);
                }
                crossIndex = intersection * 8 + crossing;

                crossings[crossIndex] = new Crossings(intersection, (int)Mathf.Ceil(crossing / 2), crossing, (int)paraIndex, (int)perpIndex, crossDirs[crossing], crossTypes[crossing], nlSides[intersection, crossing]);
            }
        }

        //Remove the following
        learnerID = Random.Range(1, 999);

        randomizer(learnerID);

        SideIndex = C.GetSideIndexValue();
        Side = C.GetSideTextValue();

        if (SideIndex == 0)
        {
            CNum = S.GetIndexValue();
            CStr = S.GetTextValue();
        }
        else if (SideIndex == 1) {
            CNum = O.GetIndexValue();
            CStr = O.GetTextValue();
        }

        VNum = V.GetVehicleIndexValue();
        VStr = V.GetVehicleTextValue();

        ResponseText.text = "The Street " + CStr + "\n Vehicle: " + VStr + " (" + Side+")";

        LoadScenario(CNum);

        //Intersection 1
        if (CNum == 0 || CNum == 1 || CNum == 2 || CNum == 3 || CNum == 4 || CNum == 5 || CNum == 6 || CNum == 7)
        {
            ic = -100000000;
            lanes = 1;
            shiftEW = 0;
            shiftNS = 0;

            if (VNum == 0)
            {
                Object1 = Car5I1;
                Object2 = Car0I1;
            }
            else if (VNum == 1)
            {
                Object1 = Van5I1;
                Object2 = Van0I1;
            }
            else if (VNum == 2)
            {
                Object1 = Bus5I1;
                Object2 = Bus0I1;
            }
            else if (VNum == 3)
            {
                Object1 = Truck5I1;
                Object2 = Truck0I1;
            }

            if (SideIndex == 1) {
                if (CNum == 0 || CNum == 3)
                    obj1 = 1;
                else if (CNum == 1 || CNum == 6)
                    obj2 = 1;
            }
            else if (SideIndex == 0) {
                if (CNum == 2 || CNum == 5)
                    obj2 = 1;
                else if (CNum == 4 || CNum == 7)
                    obj1 = 1;
            }
        }//End Intersection 1

        //Intersection 2
        else if (CNum == 8 || CNum == 9 || CNum == 10 || CNum == 11 || CNum == 12 || CNum == 13 || CNum == 14 || CNum == 15)
        {
            lanes = 2;
            shiftEW = 3;

            if (VNum == 0)
            {
                Object1 = Car10I2;
                Object2 = Car6I2;
                Object3 = Car1I2;
            }
            else if (VNum == 1)
            {
                Object1 = Van10I2;
                Object2 = Van6I2;
                Object3 = Van1I2;
            }
            else if (VNum == 2)
            {
                Object1 = Bus10I2;
                Object2 = Bus6I2;
                Object3 = Bus1I2;
            }
            else if (VNum == 3)
            {
                Object1 = Truck10I2;
                Object2 = Truck6I2;
                Object3 = Truck1I2;
            }

            if (SideIndex == 1)
            {
                if (CNum == 8 || CNum == 11)
                    obj2 = 1;
                else if (CNum == 9 || CNum == 14)
                {
                    shiftEW = 0;
                    shiftNS = 0;
                    obj3 = 1;
                }
                else if (CNum == 12 || CNum == 15)
                    obj1 = 1;
            }
            else if(SideIndex == 0) {
                if (CNum == 8 || CNum == 11)
                    obj1 = 1;
                else if (CNum == 10 || CNum == 13)
                {
                    shiftEW = 0;
                    shiftNS = 0;
                    obj3 = 1;
                }
                else if (CNum == 12 || CNum == 15)
                    obj2 = 1;
            }

        }// End Intersection 2

        //Intersection 3
        else if (CNum == 16 || CNum == 17 || CNum == 18 || CNum == 19 || CNum == 20 || CNum == 21 || CNum == 22 || CNum == 23)
        {
            lanes = 2;
            shiftEW = 3;
            shiftNS = 2;

            if (VNum == 0)
            {
                Object1 = Car11I3;
                Object2 = Car7I3;
                Object3 = Car2I3;
            }
            else if (VNum == 1)
            {
                Object1 = Van11I3;
                Object2 = Van7I3;
                Object3 = Van2I3;
            }
            else if (VNum == 2)
            {
                Object1 = Bus11I3;
                Object2 = Bus7I3;
                Object3 = Bus2I3;
            }
            else if (VNum == 3)
            {
                Object1 = Truck11I3;
                Object2 = Truck7I3;
                Object3 = Truck2I3;
            }

            if (SideIndex == 1)
            {
                if (CNum == 16 || CNum == 19)
                    obj2 = 1;
                else if (CNum == 17 || CNum == 22)
                    obj3 = 1;
                else if (CNum == 20 || CNum == 23)
                    obj1 = 1;
            }
            else if(SideIndex == 0) {
                if (CNum == 16 || CNum == 19)
                    obj1 = 1;
                else if (CNum == 18 || CNum == 21)
                    obj3 = 1;
                else if (CNum == 20 || CNum == 23)
                    obj2 = 1;
            }
        }//End Intersection 3

        //Intersection 4
        else if (CNum == 24 || CNum == 25 || CNum == 26 || CNum == 27 || CNum == 28 || CNum == 29 || CNum == 30 || CNum == 31)
        {
            lanes = 3;
            shiftEW = 3;
            shiftNS = 3;
            ic = -1;
            iobj = 1;

            if (VNum == 0)
            {
                Object1 = Car12I4;
                Object2 = Car8I4;
                Object3 = Car3I4;
                Object4 = Car14I4;
            }
            else if (VNum == 1)
            {
                Object1 = Van12I4;
                Object2 = Van8I4;
                Object3 = Van3I4;
                Object4 = Van14I4;
            }
            else if (VNum == 2)
            {
                Object1 = Bus12I4;
                Object2 = Bus8I4;
                Object3 = Bus3I4;
                Object4 = Bus14I4;
            }
            else if (VNum == 3)
            {
                Object1 = Truck12I4;
                Object2 = Truck8I4;
                Object3 = Truck3I4;
                Object4 = Truck14I4;
            }

            if (SideIndex == 1)
            {
                if (CNum == 24 || CNum == 27)
                    obj2 = 1;
                else if (CNum == 25 || CNum == 30)
                    obj3 = 1;
                else if (CNum == 26 || CNum == 29)
                    obj4 = 1;
                else if (CNum == 28 || CNum == 31)
                    obj1 = 1;
            }
            else if(SideIndex == 0) {
                if (CNum == 24 || CNum == 27)
                    obj1 = 1;
                else if (CNum == 25 || CNum == 30)
                    obj4 = 1;
                else if (CNum == 26 || CNum == 29)
                    obj3 = 1;
                else if (CNum == 28 || CNum == 31)
                    obj2 = 1;
            }
        }//End Intersection 4

        //Intersection 5
        else if (CNum == 32 || CNum == 33 || CNum == 34 || CNum == 35 || CNum == 36 || CNum == 37 || CNum == 38 || CNum == 39) 
        {
            lanes = 3;
            shiftEW = 2.5f;
            shiftNS = 3;
            ic = -1;

            if (VNum == 0)
            {
                Object1 = Car13I5;
                Object2 = Car9I5;
                Object3 = Car4I5;
                Object4 = Car15I5;
            }
            else if (VNum == 1)
            {
                Object1 = Van13I5;
                Object2 = Van9I5;
                Object3 = Van4I5;
                Object4 = Van15I5;
            }
            else if (VNum == 2)
            {
                Object1 = Bus13I5;
                Object2 = Bus9I5;
                Object3 = Bus4I5;
                Object4 = Bus15I5;
            }
            else if (VNum == 3)
            {
                Object1 = Truck13I5;
                Object2 = Truck9I5;
                Object3 = Truck4I5;
                Object4 = Truck15I5;
            }

            if (SideIndex == 1)
            {
                if (CNum == 32 || CNum == 35)
                    obj2 = 1;
                else if (CNum == 33 || CNum == 38)
                    obj3 = 1;
                else if (CNum == 34 || CNum == 37)
                    obj4 = 1;
                else if (CNum == 36 || CNum == 39)
                    obj1 = 1;
            }
            else if(SideIndex == 0) {
                if (CNum == 32 || CNum == 35)
                    obj1 = 1;
                else if (CNum == 33 || CNum == 38)
                    obj4 = 1;
                else if (CNum == 34 || CNum == 37)
                    obj3 = 1;
                else if (CNum == 36 || CNum == 39)
                    obj2 = 1;
            }
        }//End Intersection 5

        //Target object setting
        if (obj1 == 1)
        {
            Object1.SetActive(true);
            TargetObject = Object1;
        }
        else if (obj2 == 1)
        {
            Object2.SetActive(true);
            TargetObject = Object2;
        }
        else if (obj3 == 1)
        {
            Object3.SetActive(true);
            TargetObject = Object3;
        }
        else if (obj4 == 1)
        {
            Object4.SetActive(true);
            TargetObject = Object4;
        }
    }

    public void randomizer(int ID)
    {
        concept = Random.Range(1, 8);
        if (concept < 7)
            purpose = 1; //Random.Range(1, 4);
        else
            purpose = (int)purposeFinalEvaluation;  // purposeFinalEvaluation = 4; If concept = 7(final evaluation), purpose has to be purposeFinalEvaluation.

        learnerState = Random.Range(1, 4);
        age = Random.Range(1, 3);
        BCVA = visualImpairment[Random.Range(0, visualImpairment.Length)]; ;
        //Debug.Log("Learner ID = "+ ID);
        Debug.Log("concept = " + concept);
        Debug.Log("purpose = " + purpose);
        Debug.Log("learnerState = " + learnerState);
        Debug.Log("age = " + age);
        Debug.Log("BCVA = " + BCVA);

        mapScenario(ID, concept, purpose, learnerState, age, BCVA);
        //mapScenario(1003, 2, 2, 1, 1, 200);
        //mapScenario(55, 4, 2, 1, 2, 2000);
    }

    public void selectScenarios(int[] scenarioIDs, int n, int[] used, out int[] a, out int b)
    {
        used = new int[4] { 0, 0, 0, 0 };
        a = new int[n];
        b = 0;
        //Debug.Log("size of scenarioID = " + scenarioIDs.Length);

        int[] q = new int[scenarioIDs.Length];
        int scID = -1, usedID = -1;
        int exhausted;

        if (used.Length == 0)
        {
            q = random(scenarioIDs);
            for (i = 0; i < n; i++)
            {
                a[i] = q[i];
            }
            exhausted = 0;
        }
        else
        {
            int[] notUsed = new int[scenarioIDs.Length];
            notUsed = scenarioIDs;
            if (notUsed.Length < n)
            {
                q = random(scenarioIDs);
                //Debug.Log("size of output a = " + a.Length);
                //Debug.Log("size of q = " + q.Length);

                for (i = 0; i < n; i++)
                {
                    //Debug.Log("index i = " + i);

                    a[i] = q[i];
                }
                exhausted = 1;
            }
            else
            {
                q = random(notUsed);
                for (i = 0; i < n; i++)
                {
                    a[i] = q[i];
                }
                exhausted = 0;
            }
        }

        for (i = 0; i < a.Length; i++)
        {
            //Debug.Log("scene returned from selectScenarios function = " + a[i]);
        }

        //a = scenarioIDs;
        b = exhausted;
        //Debug.Log("Exhausted returned from selSce function =   " + b);
    }

    private int[] random(int[] input)
    {
        int temp;
        for (int i = 0; i < input.Length; i++)
        {

            int j = Random.Range(i, input.Length);

            temp = input[i];

            input[i] = input[j];

            input[j] = temp;
        }

        return input;
    }

    void mapScenario(int learnerID, int concept, int purpose, int learnerState, int age, int BCVA)
    {
        numCrossings = crossings.Count();
        //Debug.Log("numCrossings = " + numCrossings);
        double[,] crossingInfo = new double[numCrossings, 4];
        double[,] crossingNearLaneSameSide = new double[numCrossings / 2, 2];
        double[,] crossingNearLaneOppositeSide = new double[numCrossings / 2, 2];
        int sameCount = 0, oppositeCount = 0;

        for (i = 0; i < crossings.Count(); i++)
        {
            crossingInfo[i, 0] = i;
            crossingInfo[i, 1] = streets[crossings[i].perpendicularStreet].width;
            crossingInfo[i, 2] = crossings[i].type;
            crossingInfo[i, 3] = crossings[i].nearLaneLoc;
            if (crossings[i].nearLaneLoc == nearLaneSameSide)
            {
                if ((i == 5) || (i == 7) || (i == 13) || (i == 21))
                {
                    //Debug.Log("sameCount in if loop = " + sameCount);
                    crossingNearLaneSameSide[sameCount, 0] = -i;
                    crossingNearLaneSameSide[sameCount, 1] = streets[crossings[i].perpendicularStreet].width;
                }
                else
                {
                    //Debug.Log("sameCount in else loop = " + sameCount);
                    crossingNearLaneSameSide[sameCount, 0] = i;
                    crossingNearLaneSameSide[sameCount, 1] = streets[crossings[i].perpendicularStreet].width;
                }
                sameCount = sameCount + 1;
            }
            else
            {
                if ((i == 0) || (i == 6) || (i == 14) || (i == 22))// % if NLPT on the opposite side of perpendicular street but on the LEFT side, lable crossing indices as negative.
                {
                    crossingNearLaneOppositeSide[oppositeCount, 0] = -i;
                    crossingNearLaneOppositeSide[oppositeCount, 1] = streets[crossings[i].perpendicularStreet].width;
                } // 40 rows x 2 columns array. The two columns are crossing_index and perpendicular street width
                else
                {
                    crossingNearLaneOppositeSide[oppositeCount, 0] = i;
                    crossingNearLaneOppositeSide[oppositeCount, 1] = streets[crossings[i].perpendicularStreet].width;
                } // 40 rows x 2 columns array. The two columns are crossing_index and perpendicular street width
                oppositeCount = oppositeCount + 1;
            }
        }

        cutoffWidthPS = psHeight / Mathf.Tan(((float)BCVA / 240) * Mathf.PI / 180);
        cutoffWidthCarMin = carHeightMin / Mathf.Tan(((float)BCVA / 240) * Mathf.PI / 180);

        //Debug.Log("cutoffWidthPS = " + cutoffWidthPS);
        //Debug.Log("cutoffWidthCarMin = " + cutoffWidthCarMin);
        double[] q = new double[20];
        oppositeCount = 0;

        for (i = 0; i < crossingNearLaneOppositeSide.GetLength(0); i++)
        {

            //Debug.Log("crossingNearLaneOppositeSide.Length = " + crossingNearLaneOppositeSide.GetLength(0));Debug.Log("i = " + i);
            double w = crossingNearLaneOppositeSide[i, 1];
            //.Log("w = " + w);
            if ((w >= cutoffWidthPS) && (w <= cutoffWidthCarMin))
            {
                q[oppositeCount] = crossingNearLaneOppositeSide[i, 0];
                oppositeCount = oppositeCount + 1;
                //Debug.Log("oppositeCount = " + oppositeCount);
            }
        }

        int[] appropriateOppositeSide = new int[oppositeCount];
        for (i = 0; i < oppositeCount - 1; i++)
        {
            appropriateOppositeSide[i] = (int)q[i];
        }
        int[] appropriateSameSide = new int[20];
        for (i = 0; i < crossingNearLaneOppositeSide.GetLength(0); i++)
        {
            appropriateSameSide[i] = (int)crossingNearLaneSameSide[i, 0];
        }

        int[] scenariosOppositeSide;
        int oppositeExhausted, sameExhausted;
        int[] scenariosSameSide;
        int[] qu;

        // ***************Concept 1: Parallel and perpendicular traffic
        if (concept == conceptParaPerpTraffic)
        {
            int blockSize = 4;
            int numTrafficCycles = 2;
            int numSame = (int)Mathf.Ceil(blockSize / 2); // round rounds up numbers. round(5/2) = 3;
            int numOpposite = blockSize - numSame;
            int[] qq = new int[blockSize];

            selectScenarios(appropriateOppositeSide, numOpposite, oppositeUsed, out scenariosOppositeSide, out oppositeExhausted);  // the function returns indices of appropriate crossings and whether all crossings have been used.
            selectScenarios(appropriateSameSide, numSame, sameUsed, out scenariosSameSide, out sameExhausted);
            qu = scenariosOppositeSide.Concat(scenariosSameSide).ToArray(); // concatenate the two sets of crossing indices into one column vector. 
            crossingScenarios = random(qu);  //random(qu)qu is randomized qu randomize the order of the appropriate crossing indices. this is the block of crossing scenarios to be simulated. 
                                             // for instruction
            if (purpose == purposeInstruction)
            {
                // young subject first time receiving instruction
                if ((learnerState == learnerStateNovice) && (age == ageChild))
                {
                    qq = new int[4] { streetParallel, streetPerpendicular, streetBoth, streetBoth };
                }
                // which street has traffic. The first two scenarios have traffic only on one of the two streets.
                else  // other learnerState and age
                    qq = new int[4] { streetBoth, streetBoth, streetBoth, streetBoth }; //% traffic on both streets

                for (i = 0; i < blockSize; i++)
                {
                    streetWithTraffic[i] = qq[i]; // which street(s) will have cars
                    Lanes[i] = laneAll;    // all lanes have traffic
                    startMovingTraffic[i] = streetWithTraffic[i]; // which traffic is moving at the beginning of the simulation
                    if (startMovingTraffic[i] == streetBoth)  // if traffic on both streets, randomly select one for moving at the beginning of simulation
                    {
                        float rand = Random.Range(0.0f, 1.0f);
                        if (rand > 0.5)
                            startMovingTraffic[i] = streetParallel;
                        else
                            startMovingTraffic[i] = streetPerpendicular;
                    }

                    // define voice overlay. Recorded voice instructions need to
                    // agree with the traffic in the scenario. If a scenario has two
                    //% traffic cycles, appropriate voice instruction will be given
                    //% in both cycles. The values assigned to voice are the file
                    //% names of the voice recordings. 
                    if (startMovingTraffic[i] == streetPerpendicular)
                    {
                        voice[i, 0] = instruct_perpendicularMoving; //'The moving traffic is Perpendicular Traffic';
                        voice[i, 1] = instruct_parallelMoving;
                    } //'The moving traffic is parallel traffic';
                    else
                    {
                        voice[i, 0] = instruct_parallelMoving; //'The moving traffic is parallel Traffic';
                        voice[i, 1] = instruct_perpendicularMoving; //'The moving traffic is Perpendicular traffic';
                    }
                }

                // other settings for scenarios in the block
                for (i = 0; i < blockSize; i++)
                {
                    collectLearnerResponse[i] = learnerResponseNone;//leanrer does not give responces in the instruction phase
                    useKeyEvent[i] = keyEventNone; // no key event for this concept
                    useKeyCar[i] = keyCarNone; // no key car
                    keyCarSE[i] = keyCarSENone;  // no key car special effect
                    firstCar[i] = firstCarAny;  //% no need to define first car behavior  
                    feedback[i] = feedbackNone;
                } //% no feedback provided to the learner
            }
            //% for practice and assessment
            else if ((purpose == purposePractice) || (purpose == purposeAssessment)) //% practice or assessment
            {
                qq = new int[4] { streetBoth, streetBoth, streetBoth, streetBoth };
                for (i = 0; i < blockSize; i++)
                {

                    streetWithTraffic[i] = qq[i]; //% which street(s) will have cars
                    Lanes[i] = laneAll;    //% all lanes will have cars. Practice scenarios should have normal traffic
                    if (startMovingTraffic[i] == streetBoth)
                    {
                        float rand = UnityEngine.Random.Range(0.0f, 1.0f);
                        if (rand > 0.5)
                            startMovingTraffic[i] = streetParallel;
                        else
                            startMovingTraffic[i] = streetPerpendicular;
                    }

                    //%Instructions for responses
                    //% if parallel traffic is moving at the beginning of simulation, 
                    //% ask to point at parallel traffic in first cycle and point to
                    //% perpendicular traffic in second cycle.
                    //% Opposite for perpendicular traffic starting
                    if (startMovingTraffic[i] == streetParallel)
                    {
                        voice[i, 0] = practice_point2Parallel; //%'Use the pointer to point at the parallel traffic'; 
                        voice[i, 1] = practice_point2Perpendicular;
                    } //%'Use the pointer to point at the Perpendicular traffic';
                    else
                    {
                        voice[i, 0] = practice_point2Perpendicular; //%'Use the pointer to point at the Perpendicular traffic';
                        voice[i, 1] = practice_point2Parallel;
                    } //%'Use the pointer to point at the parallel traffic';

                }
                //% other settings for scenarios in the block
                for (i = 0; i < blockSize; i++)
                {
                    collectLearnerResponse[i] = learnerResponsePoint; //% learner makes pointing response in practice
                    useKeyEvent[i] = keyEventNone;
                    useKeyCar[i] = keyCarNone;
                    keyCarSE[i] = keyCarSENone;
                    firstCar[i] = firstCarAny;
                    if (purpose == purposeAssessment)
                        feedback[i] = feedbackNone;  //% no feedback provided for assessment
                    else
                        feedback[i] = feedbackYes;  //% feedback provided for practice (not implemented yet)
                }
            }
        }

        // ***************Concept 2 
        else if (concept == conceptNLPT)
        {
            int blockSize = 4;
            int numTrafficCycles = 2;
            int numSame = (int)Mathf.Ceil(blockSize / 2); // round rounds up numbers. round(5/2) = 3;
            int numOpposite = blockSize - numSame;

            selectScenarios(appropriateOppositeSide, numOpposite, oppositeUsed, out scenariosOppositeSide, out oppositeExhausted);  // the function returns indices of appropriate crossings and whether all crossings have been used.
            selectScenarios(appropriateSameSide, numSame, sameUsed, out scenariosSameSide, out sameExhausted);

            //for instruction
            if (purpose == purposeInstruction)
            {
                // this ensures equal exposure to the two types of NLPT in a block
                // NLPT on the same side of the perpendicular street 
                //and NLPT on the opposite side of perpendicular street.
                //The order is fixed because the first two scenarios may have traffic only
                //on the parallel street and only on the near lane.
                crossingScenarios = new int[4] { scenariosSameSide[0], scenariosOppositeSide[0], scenariosSameSide[1], scenariosOppositeSide[1] };

                //young subject first time receiving instruction
                if ((learnerState == learnerStateNovice) && (age == ageChild))
                {
                    //scenario 1
                    streetWithTraffic[0] = streetParallel;
                    Lanes[0] = laneNLPT; //There are only near lane parallel traffic in the first two scenarios
                    startMovingTraffic[0] = streetParallel;
                    if (crossingScenarios[0] < 0) //NLPT on the same side but over the RIGHT shoulder
                    {
                        voice[0, 0] = instruct_NLPTSameSideMovingRight; //'The near lane parallel traffic is over your right shoulder. It is now moving’;
                        voice[0, 1] = instruct_NLPTSameSideStopRight; //'The near lane parallel traffic is over your right shoulder. It is now stopped’;
                    }
                    else // NLPT on the same side, over the left shoulder 
                    {
                        voice[0, 0] = instruct_NLPTSameSideMovingLeft; //'The near lane parallel traffic is over your left shoulder. It is now moving’;
                        voice[0, 1] = instruct_NLPTSameSideStopLeft; //'The near lane parallel traffic is over your left shoulder. It is now stopped’;
                    }

                    //scenario 2
                    streetWithTraffic[1] = streetParallel;
                    Lanes[1] = laneNLPT;
                    startMovingTraffic[1] = streetParallel;
                    if (crossingScenarios[1] < 0) //NLPT on the opposite side of perpendicular street but the LEFT side
                    {
                        voice[1, 0] = instruct_NLPTOppositeSideMovingLeft; //'The near lane parallel traffic is across the street on your left side. It is now moving’;
                        voice[1, 1] = instruct_NLPTOppositeSideStopLeft; //'The near lane parallel traffic is across the street on your left side. It is now stopped’;
                    }
                    else
                    {
                        voice[1, 0] = instruct_NLPTOppositeSideMovingRight; //'The near lane parallel traffic is across the street on your right side. It is now moving’;
                        voice[1, 1] = instruct_NLPTOppositeSideStopRight; //'The near lane parallel traffic is across the street on your right side. It is now stopped’;                
                    }

                    //scenario 3
                    streetWithTraffic[2] = streetBoth;
                    Lanes[2] = laneAll; //There are traffic on both streets and all lanes in the last two scenarios
                    startMovingTraffic[2] = streetParallel;
                    if (crossingScenarios[2] < 0) //NLPT on the same side but over the RIGHT shoulder
                    {
                        voice[2, 0] = instruct_NLPTSameSideMovingRight; //'The near lane parallel traffic is over your right shoulder. It is now moving’;
                        voice[2, 1] = instruct_NLPTSameSideStopRight; //'The near lane parallel traffic is over your right shoulder. It is now stopped’;
                    }
                    else //NLPT on the same side, over the left shoulder shoulder
                    {
                        voice[2, 0] = instruct_NLPTSameSideMovingLeft; // 'The near lane parallel traffic is over your left shoulder. It is now moving’;

                        voice[2, 1] = instruct_NLPTSameSideStopLeft; //'The near lane parallel traffic is over your left shoulder. It is now stopped’;
                    }

                    //scenario 4
                    streetWithTraffic[3] = streetBoth;
                    Lanes[3] = laneAll;
                    startMovingTraffic[3] = streetParallel;
                    if (crossingScenarios[3] < 0) //NLPT on the opposite side of perpendicular street but the LEFT side
                    {
                        voice[3, 0] = instruct_NLPTOppositeSideMovingLeft; //'The near lane parallel traffic is across the street on your left side. It is now moving’;
                        voice[3, 1] = instruct_NLPTOppositeSideStopLeft; //'The near lane parallel traffic is across the street on your left side. It is now stopped’;
                    }
                    else
                    {
                        voice[3, 0] = instruct_NLPTOppositeSideMovingRight; //'The near lane parallel traffic is across the street on your right side. It is now moving’;
                        voice[3, 1] = instruct_NLPTOppositeSideStopRight; //'The near lane parallel traffic is across the street on your right side. It is now stopped’;                
                    }
                }

                //other learnerState and age
                else
                {
                    //scenario 1
                    streetWithTraffic[0] = streetBoth;
                    Lanes[0] = laneAll; //There are only near lane parallel traffic in the first two scenarios
                    startMovingTraffic[0] = streetParallel;
                    if (crossingScenarios[0] < 0) //NLPT on the same side but over the RIGHT shoulder
                    {
                        voice[0, 0] = instruct_NLPTSameSideMovingRight; //'The near lane parallel traffic is over your right shoulder. It is now moving’;
                        voice[0, 1] = instruct_NLPTSameSideStopRight; //'The near lane parallel traffic is over your right shoulder. It is now stopped’;
                    }
                    else //NLPT on the same side, over the left shoulder shoulder
                    {
                        voice[0, 0] = instruct_NLPTSameSideMovingLeft; //'The near lane parallel traffic is over your left shoulder. It is now moving’;
                        voice[0, 1] = instruct_NLPTSameSideStopLeft; //'The near lane parallel traffic is over your left shoulder. It is now stopped’;
                    }

                    //scenario 2
                    streetWithTraffic[1] = streetBoth;
                    Lanes[1] = laneAll;
                    startMovingTraffic[1] = streetParallel;
                    if (crossingScenarios[1] < 0) //NLPT on the opposite side of perpendicular street but the LEFT side
                    {
                        voice[1, 0] = instruct_NLPTOppositeSideMovingLeft; //'The near lane parallel traffic is across the street on your left side. It is now moving’;
                        voice[1, 1] = instruct_NLPTOppositeSideStopLeft; //'The near lane parallel traffic is across the street on your left side. It is now stopped’;
                    }
                    else
                    {
                        voice[1, 0] = instruct_NLPTOppositeSideMovingRight; //'The near lane parallel traffic is across the street on your right side. It is now moving’;
                        voice[1, 1] = instruct_NLPTOppositeSideStopRight; //'The near lane parallel traffic is across the street on your right side. It is now stopped’;    
                    }

                    //scenario 3
                    streetWithTraffic[2] = streetBoth;
                    Lanes[2] = laneAll; //There are only near lane parallel traffic in the first two scenarios
                    startMovingTraffic[2] = streetParallel;
                    if (crossingScenarios[2] < 0) //NLPT on the same side but over the RIGHT shoulder
                    {
                        voice[2, 0] = instruct_NLPTSameSideMovingRight; //'The near lane parallel traffic is over your right shoulder. It is now moving’;
                        voice[2, 1] = instruct_NLPTSameSideStopRight; //'The near lane parallel traffic is over your right shoulder. It is now stopped’;
                    }
                    else //NLPT on the same side, over the left shoulder shoulder
                    {
                        voice[2, 0] = instruct_NLPTSameSideMovingLeft; //'The near lane parallel traffic is over your left shoulder. It is now moving’;
                        voice[2, 1] = instruct_NLPTSameSideStopLeft; //'The near lane parallel traffic is over your left shoulder. It is now stopped’;
                    }

                    //scenario 4
                    streetWithTraffic[3] = streetBoth;
                    Lanes[3] = laneAll;
                    startMovingTraffic[3] = streetParallel;
                    if (crossingScenarios[3] < 0) //NLPT on the opposite side of perpendicular street but the LEFT side
                    {
                        voice[3, 0] = instruct_NLPTOppositeSideMovingLeft; //'The near lane parallel traffic is across the street on your left side. It is now moving’;
                        voice[3, 1] = instruct_NLPTOppositeSideStopLeft; //'The near lane parallel traffic is across the street on your left side. It is now stopped’;
                    }
                    else
                    {
                        voice[3, 0] = instruct_NLPTOppositeSideMovingRight; //'The near lane parallel traffic is across the street on your right side. It is now moving’;
                        voice[3, 1] = instruct_NLPTOppositeSideStopRight; //'The near lane parallel traffic is across the street on your right side. It is now stopped’;                
                    }
                }

                //other settings for scenarios in the block
                for (int ii = 0; ii < blockSize; ii++)
                {
                    collectLearnerResponse[ii] = learnerResponseNone;
                    useKeyEvent[ii] = keyEventNone;
                    useKeyCar[ii] = keyCarNone;
                    keyCarSE[ii] = keyCarSENone;
                    firstCar[ii] = firstCarAny;
                    feedback[ii] = feedbackNone;
                }
            }

            //for practice and assessment
            else if ((purpose == purposePractice) || (purpose == purposeAssessment)) //practice or assessment
            {
                //There is still equal exposure to the two types of crossing
                //scenarios. However, because all scenarios in the block have
                //traffic on both streets and on all lanes, we can randomize the
                //order of the two types.
                qu = scenariosOppositeSide.Concat(scenariosSameSide).ToArray(); //concatenate the two scenario sets. One set of scenario indices. The first half is NLPT on the opposite side and the second half NLPT on the same side.

                //this contains the indices of q, not the indices of real crossings. If q has 4 elements, qx contains 1 to 4 in random order.
                //This is useful because qx values 1 and 2 are NLPT on opposite
                //side and qx values 3 and 4 are NLPT on same side. This can be
                //used to determine the type of crossing.
                crossingScenarios = random(qu); //this contains the indices of the crossing scenarios to be used. 

                for (int ii = 0; ii < blockSize; ii++)
                {
                    streetWithTraffic[ii] = streetBoth; //which street(s) will have cars
                    Lanes[ii] = laneAll; //which lane(s) will have cars
                    float rand = UnityEngine.Random.Range(0.0f, 1.0f);
                    if (rand > 0.5)
                    {
                        startMovingTraffic[ii] = streetParallel;
                        voice[ii, 0] = practic_Point2NLPTMoving; //'Point to the near lane parallel traffic when it is moving.’;
                        voice[ii, 1] = practic_Point2NLPTStop; //'Point to the near lane parallel traffic when it stoppes.’;
                    }
                    else
                    {
                        startMovingTraffic[i] = streetPerpendicular;
                        voice[ii, 0] = practic_Point2NLPTStop; //'Point to the near lane parallel traffic when it stoppes.’;
                        voice[ii, 1] = practic_Point2NLPTMoving; //'Point to the near lane parallel traffic when it is moving.’;
                    }
                }

                //other settings for scenarios in the block
                for (int ii = 0; ii < blockSize; ii++)
                {
                    collectLearnerResponse[ii] = learnerResponsePoint;
                    useKeyEvent[ii] = keyEventNone;
                    useKeyCar[ii] = keyCarNone;
                    keyCarSE[ii] = keyCarSENone;
                    firstCar[ii] = firstCarAny;
                    if (purpose == purposeAssessment)
                    {
                        feedback[ii] = feedbackNone;
                    }
                    else
                    {
                        feedback[ii] = feedbackYes;
                    }
                }
            }
        }
        //End of concept 2

        //concept 3
        else if (concept == conceptNLPTStop)
        {
            int blockSize = 4;
            int numTrafficCycles = 0; // these are event-centered scenarios
            int numSame = (int)Mathf.Ceil(blockSize / 2); // round rounds up numbers.round(5/2) = 3;
            int numOpposite = blockSize - numSame;

            selectScenarios(appropriateOppositeSide, numOpposite, oppositeUsed, out scenariosOppositeSide, out oppositeExhausted);  // the function returns indices of appropriate crossings and whether all crossings have been used.
            selectScenarios(appropriateSameSide, numSame, sameUsed, out scenariosSameSide, out sameExhausted);
            
            // for instruction
            if (purpose == purposeInstruction)
            {
                crossingScenarios = new int[4] { scenariosSameSide[0], scenariosOppositeSide[0], scenariosSameSide[1], scenariosOppositeSide[1] };
                if ((learnerState == learnerStateNovice) && (age == ageChild))
                {
                    streetWithTraffic[0] = streetParallel;
                    Lanes[0] = laneNLPT; // There are only near lane parallel traffic in the first two scenarios
                    startMovingTraffic[0] = streetParallel;
                    if (crossingScenarios[0] < 0) // NLPT on the same side but over the RIGHT shoulder
                        voice[0, 0] = instruct_NLPTSameSideStopWhereRight;
                    else // NLPT on the same side, over the left shoulder
                        voice[0, 0] = instruct_NLPTSameSideStopWhereLeft;

                    streetWithTraffic[1] = streetParallel;
                    Lanes[1] = laneNLPT;
                    startMovingTraffic[1] = streetParallel;
                    if (crossingScenarios[1] < 0)
                    {
                        voice[1, 0] = instruct_NLPTOppositeSideStopWhereLeft;
                    }
                    else
                        voice[1, 0] = instruct_NLPTOppositeSideStopWhereRight;

                    streetWithTraffic[2] = streetBoth;
                    Lanes[2] = laneAll;
                    startMovingTraffic[2] = streetParallel;
                    if (crossingScenarios[2] < 0)
                    {
                        voice[2, 0] = instruct_NLPTSameSideStopWhereRight;
                    }
                    else
                        voice[2, 0] = instruct_NLPTSameSideStopWhereLeft;

                    streetWithTraffic[3] = streetBoth;
                    Lanes[3] = laneAll;
                    startMovingTraffic[3] = streetParallel;
                    if (crossingScenarios[3] < 0)
                    {
                        voice[3, 0] = instruct_NLPTOppositeSideStopWhereLeft;
                    }
                    else
                        voice[3, 0] = instruct_NLPTOppositeSideStopWhereRight;
                }
                else
                {
                    streetWithTraffic[0] = streetBoth;
                    Lanes[0] = laneAll; // There are only near lane parallel traffic in the first two scenarios
                    startMovingTraffic[0] = streetParallel;
                    if (crossingScenarios[0] < 0) // NLPT on the same side but over the RIGHT shoulder
                        voice[0, 0] = instruct_NLPTSameSideStopWhereRight;
                    else // NLPT on the same side, over the left shoulder
                        voice[0, 0] = instruct_NLPTSameSideStopWhereLeft;

                    streetWithTraffic[1] = streetBoth;
                    Lanes[1] = laneAll;
                    startMovingTraffic[1] = streetParallel;
                    if (crossingScenarios[1] < 0)
                    {
                        voice[1, 0] = instruct_NLPTOppositeSideStopWhereLeft;
                    }
                    else
                        voice[1, 0] = instruct_NLPTOppositeSideStopWhereRight;

                    streetWithTraffic[2] = streetBoth;
                    Lanes[2] = laneAll;
                    startMovingTraffic[2] = streetParallel;
                    if (crossingScenarios[2] < 0)
                    {
                        voice[2, 0] = instruct_NLPTSameSideStopWhereRight;
                    }
                    else
                        voice[2, 0] = instruct_NLPTSameSideStopWhereLeft;

                    streetWithTraffic[3] = streetBoth;
                    Lanes[3] = laneAll;
                    startMovingTraffic[3] = streetParallel;
                    if (crossingScenarios[3] < 0)
                    {
                        voice[3, 0] = instruct_NLPTOppositeSideStopWhereLeft;
                    }
                    else
                        voice[3, 0] = instruct_NLPTOppositeSideStopWhereRight;
                }
                for (int ii = 0; ii < blockSize; ii++)
                {
                    collectLearnerResponse[ii] = learnerResponseNone;
                    useKeyEvent[ii] = keyEventParaGreen2Red;
                    useKeyCar[ii] = keyCarNone;
                    keyCarSE[ii] = keyCarSENone;
                    firstCar[ii] = firstCarAny;
                    feedback[ii] = feedbackNone;
                }
            }
            else if ((purpose == purposePractice) || (purpose == purposeAssessment))
            {
                qu = scenariosOppositeSide.Concat(scenariosSameSide).ToArray();
                crossingScenarios = random(qu);



                for (int ii = 0; ii < blockSize; ii++)
                {
                    streetWithTraffic[ii] = streetBoth;
                    Lanes[ii] = laneAll;
                    startMovingTraffic[ii] = streetParallel;
                    voice[ii, 0] = practic_Point2NLPTStopWhere;
                }

                for (int ii = 0; ii < blockSize; ii++)
                {
                    collectLearnerResponse[ii] = learnerResponsePoint;
                    useKeyEvent[ii] = keyEventParaGreen2Red;
                    useKeyCar[ii] = keyCarNone;
                    keyCarSE[ii] = keyCarSENone;
                    firstCar[ii] = firstCarAny;
                    feedback[ii] = feedbackNone;
                }

            }
        }
        //end of concept 3

        //Concept 4
        else if (concept == conceptNLPTSurge)
        {

            int blockSize = 4;
            int numTrafficCycles = 0; // these are event-centered scenarios
            int numSame = (int)Mathf.Ceil(blockSize / 2); // round rounds up numbers.round(5/2) = 3;
            int numOpposite = blockSize - numSame;
            selectScenarios(appropriateOppositeSide, numOpposite, oppositeUsed, out scenariosOppositeSide, out oppositeExhausted);  // the function returns indices of appropriate crossings and whether all crossings have been used.
            selectScenarios(appropriateSameSide, numSame, sameUsed, out scenariosSameSide, out sameExhausted);

            // for instruction
            if (purpose == purposeInstruction)
            {
                int[] qx = scenariosOppositeSide.Concat(scenariosSameSide).ToArray();

                // concatenate the two scenario sets.One set of scenario indices.The first half is NLPT on the opposite side and the second half NLPT on the same side.
                // this contains the indices of q, not the indices of real crossings. If q has 4 elements, qx contains 1 to 4 in random order.
                // This is useful because qx values 1 and 2 are NLPT on opposite
                // side and qx values 3 and 4 are NLPT on same side.This can be
                // used to determine the type of crossing.

                crossingScenarios = random(qx);

                for (int ii = 0; ii < crossingScenarios.Length; ii++)
                {
                    streetWithTraffic[ii] = streetBoth;
                    Lanes[ii] = laneAll; // There are only near lane parallel traffic in the first two scenarios
                    startMovingTraffic[ii] = streetParallel;
                    if (ii < 3) // this scenario has NLPT on the opposite side of perpendicular street
                    {
                        if (crossingScenarios[ii] < 0)// NLPT on the opposite side of perpendicular street but the LEFT side
                            voice[ii, 0] = instruct_NLPTOppositeSideSurgeRight; //'The near lane parallel traffic is across the street on your right side. It will soon start to shiftEW. This is called a NLPT surge.’;                   
                        else
                            voice[ii, 0] = instruct_NLPTOppositeSideSurgeLeft; // 'The near lane parallel traffic is across the street on your left side. It will soon start to shiftEW. This is called a NLPT surge.’;                   
                    }
                    else  // this scenario has NLPT on the same side
                    {
                        if (crossingScenarios[ii] < 0)// NLPT on the same side but over the RIGHT shoulder
                            voice[ii, 0] = instruct_NLPTSameSideSurgeRight; // 'The near lane parallel traffic is over your right shoulder. It will soon start to shiftEW. This is called a NLPT surge.’;
                        else
                            voice[ii, 0] = instruct_NLPTSameSideSurgeLeft; // 'The near lane parallel traffic is over your left shoulder. It will soon start to shiftEW. This is called a NLPT surge.’;
                    }
                }
                for (int ii = 0; ii < blockSize; ii++)
                {
                    collectLearnerResponse[ii] = learnerResponseNone;
                    useKeyEvent[ii] = keyEventParaRed2Green;
                    useKeyCar[ii] = keyCarNone;
                    keyCarSE[ii] = keyCarSENone;
                    firstCar[ii] = firstCarAny;
                    feedback[ii] = feedbackNone;
                }
            }
            // other settings for scenarios in the block

            // for practice and assessment
            else if ((purpose == purposePractice) || (purpose == purposeAssessment)) // practice or assessment
            {
                int[] qx = scenariosOppositeSide.Concat(scenariosSameSide).ToArray(); // concatenate the two scenario sets. One set of scenario indices.The first half is NLPT on the opposite side and the second half NLPT on the same side.

                crossingScenarios = random(qx);  // this contains the indices of the crossing scenarios to be used. 

                for (int ii = 0; ii < crossingScenarios.Length; ii++)
                {
                    streetWithTraffic[ii] = streetBoth;
                    Lanes[ii] = laneAll; // There are only near lane parallel traffic in the first two scenarios
                    startMovingTraffic[ii] = streetParallel;
                    voice[ii, 0] = practice_NLPTSurge;
                } // 'Say 'Now' when you detect a NLPT surge.’;

                // other settings for scenarios in the block
                for (int ii = 0; ii < blockSize; ii++)
                {
                    collectLearnerResponse[ii] = learnerResponseVerbalNow;
                    useKeyEvent[ii] = keyEventParaRed2Green;
                    useKeyCar[ii] = keyCarNone;
                    keyCarSE[ii] = keyCarSENone;
                    firstCar[ii] = firstCarAny;
                    feedback[ii] = feedbackNone;
                }
            }
        }
        //end of concept 4

        //Concept 5
        else if (concept == conceptConfirmNLPTS)
        {
            int blockSize = 4;
            int numTrafficCycles = 0; // these are event-centered scenarios
            int numSame = (int)Mathf.Ceil(blockSize / 2); // round rounds up numbers.round(5/2) = 3;
            int numOpposite = blockSize - numSame;
           
            selectScenarios(appropriateOppositeSide, numOpposite, oppositeUsed, out scenariosOppositeSide, out oppositeExhausted);  // the function returns indices of appropriate crossings and whether all crossings have been used.
            selectScenarios(appropriateSameSide, numSame, sameUsed, out scenariosSameSide, out sameExhausted);
            
            int[] qx = scenariosOppositeSide.Concat(scenariosSameSide).ToArray();
            int[] p = new int[4] { firstCarStraightGoing, firstCarRightTurn, firstCarStraightGoing, firstCarRightTurn };
            crossingScenarios = random(qx);

            // for instruction
            for (i = 0; i < 4; i++)
            {
                Debug.Log("Crossing Scenario = " + crossingScenarios[i] + " qx = " + qx[i]);
            }

            if (purpose == purposeInstruction)
            {
                // this defines the behavior of the first NLPT car
                for (int ii = 0; ii < crossingScenarios.Length; ii++)
                {
                    streetWithTraffic[ii] = streetBoth;
                    Lanes[ii] = laneAll; // There are only near lane parallel traffic in the first two scenarios
                    startMovingTraffic[ii] = streetParallel;
                    if (ii < 3) // this scenario has NLPT on the opposite side of perpendicular street
                    {
                        if (p[ii] == firstCarStraightGoing)
                        {
                            if (crossingScenarios[ii] < 0) // NLPT on the opposite side of perpendicular street but the LEFT side
                                voice[ii, 0] = instruct_NLPTOppositeSideStraightLeft; //'The first near lane parallel traffic car is across the street on your left side. It will go into the intersection. This means you can start to cross the street.’;                        
                            else
                                voice[ii, 0] = instruct_NLPTOppositeSideStraightRight; //'The first near lane parallel traffic car is across the street on your right side. It will go into the intersection. This means you can start to cross the street.’;                        
                        }
                        else
                        {
                            if (crossingScenarios[ii] < 0)// NLPT on the opposite side of perpendicular street but the LEFT side
                                voice[ii, 0] = instruct_NLPTOppositeSideTurnLeft; //'The first near lane parallel traffic car is across the street on your LEFT side. It will make a right turn. You need to wait for a car entering the intersection.’;                        
                            else
                                voice[ii, 0] = instruct_NLPTOppositeSideTurnRight; //'The first near lane parallel traffic car is across the street on your right side. It will make a right turn. You need to wait for a car entering the intersection.’;                        
                        }
                    }
                    else  // this scenario has NLPT on the same side
                    {
                        if (p[ii] == firstCarStraightGoing)
                        {
                            if (crossingScenarios[ii] < 0) // NLPT on the same side but over the RIGHT shoulder
                                voice[ii, 0] = instruct_NLPTSameSideStraightRight; //'The first near lane parallel traffic car is over your right shoulder. It will go into the intersection. This means you can start to cross the street.’;
                            else
                                voice[ii, 0] = instruct_NLPTSameSideStraightLeft; //'The first near lane parallel traffic car is over your left shoulder. It will go into the intersection. This means you can start to cross the street.’;
                        }
                        else
                        {
                            if (crossingScenarios[ii] < 0)// NLPT on the same side but over the RIGHT shoulder
                                voice[ii, 0] = instruct_NLPTSameSideTurnRight; //'The first near lane parallel traffic car is over your right shoulder. It will make a right turn. You need to wait for a car entering the intersection.’;
                            else
                                voice[ii, 0] = instruct_NLPTSameSideTurnLeft; //'The first near lane parallel traffic car is over your left shoulder. It will make a right turn. You need to wait for a car entering the intersection.’;
                        }
                    }
                }
                for (int ii = 0; ii < blockSize; ii++)
                {
                    collectLearnerResponse[ii] = learnerResponseNone;
                    useKeyEvent[ii] = keyEventParaRed2Green;
                    useKeyCar[ii] = keyCarNone;
                    keyCarSE[ii] = keyCarSENone;
                    firstCar[ii] = p[ii];
                    feedback[ii] = feedbackNone;
                }
            }

            else if ((purpose == purposePractice) || (purpose == purposeAssessment))// practice or assessment
            {
                int[] pp = new int[4] { firstCarStraightGoing, firstCarRightTurn, firstCarStraightGoing, firstCarRightTurn };  //this defines the behavior of the first NLPT car
                p = random(pp);  // randomize the order of first car behavior
                for (int ii = 0; ii < crossingScenarios.Length; ii++)
                {
                    streetWithTraffic[ii] = streetBoth;
                    Lanes[ii] = laneAll; // There are only near lane parallel traffic in the first two scenarios
                    startMovingTraffic[ii] = streetParallel;
                    voice[ii, 0] = practice_NLPTSurgeConfirm; //'Say 'Now' when you see a NLPT car enters the intersection.’;
                }
                // other settings for scenarios in the block
                for (int ii = 0; ii < blockSize; ii++)
                {
                    collectLearnerResponse[ii] = learnerResponseVerbalNow;
                    useKeyEvent[ii] = keyEventParaRed2Green;
                    useKeyCar[ii] = keyCarNone;
                    keyCarSE[ii] = keyCarSENone;
                    firstCar[ii] = p[ii];
                    feedback[ii] = feedbackNone;
                }
            }
        }
        //End of Concept 5

        //Concept 6
        else if (concept == conceptMakeDecision)
        {
            int blockSize = 4;
            int numTrafficCycles = 0; // these are event-centered scenarios
            int numSame = (int)Mathf.Ceil(blockSize / 2); // round rounds up numbers.round(5/2) = 3;
            int numOpposite = blockSize - numSame;
            
            selectScenarios(appropriateOppositeSide, numOpposite, oppositeUsed, out scenariosOppositeSide, out oppositeExhausted);  // the function returns indices of appropriate crossings and whether all crossings have been used.
            selectScenarios(appropriateSameSide, numSame, sameUsed, out scenariosSameSide, out sameExhausted);
            
            int[] qx = scenariosOppositeSide.Concat(scenariosSameSide).ToArray();

            crossingScenarios = random(qx);

            for (i = 0; i < 4; i++)
            {
                Debug.Log("Crossing Scenario = " + crossingScenarios[i] + " qx = " + qx[i]);
            }

            // for instruction
            if (purpose == purposeInstruction)
            {
                int[] p = new int[4] { firstCarStraightGoing, firstCarRightTurn, firstCarStraightGoing, firstCarRightTurn };
                for (int ii = 0; ii < crossingScenarios.Length; ii++)
                {
                    streetWithTraffic[ii] = streetBoth;
                    Lanes[ii] = laneAll; //There are only near lane parallel traffic in the first two scenarios
                    startMovingTraffic[ii] = streetParallel;
                    if (ii < 3) // this scenario has NLPT on the opposite side of perpendicular street
                    {
                        if (p[ii] == firstCarStraightGoing)
                        {
                            if (crossingScenarios[ii] < 0) // NLPT on the opposite side of perpendicular street but the LEFT side
                                voice[ii, 0] = instruct_NLPTOppositeSideStraightDecisionLeft;  //'The first near lane parallel traffic car is across the street on your LEFT side. It will go into the intersection. It is safe to start crossing the street as soon as you see it entering the intersection.’;                       
                            else
                                voice[ii, 0] = instruct_NLPTOppositeSideStraightDecisionRight; // 'The first near lane parallel traffic car is across the street on your RIGHT side. It will go into the intersection. It is safe to start crossing the street as soon as you see it entering the intersection.’;                       
                        }

                        else
                        {
                            if (crossingScenarios[ii] < 0)// NLPT on the opposite side of perpendicular street but the LEFT side
                                voice[ii, 0] = instruct_NLPTOppositeSideTurnDecisionLeft; // 'The first near lane parallel traffic car is across the street on your LEFT side. It will make a right turn. It is not safe to start crossing the street. Wait until see a car entering the intersection.’;
                            else
                                voice[ii, 0] = instruct_NLPTOppositeSideTurnDecisionRight; // 'The first near lane parallel traffic car is across the street on your RIGHT side. It will make a right turn. It is not safe to start crossing the street. Wait until see a car entering the intersection.’;                       
                        }
                    }

                    else  // this scenario has NLPT on the same side
                    {
                        if (p[ii] == firstCarStraightGoing)
                        {
                            if (crossingScenarios[ii] < 0) //NLPT on the same side but over the RIGHT shoulder
                                voice[ii, 0] = instruct_NLPTSameSideStraightDecisionRight; // 'The first near lane parallel traffic car is over your right shoulder. It will go into the intersection. It is safe to start crossing the street as soon as you see it entering the intersection.’;
                            else
                                voice[ii, 0] = instruct_NLPTSameSideStraightDecisionLeft; // 'The first near lane parallel traffic car is over your left shoulder. It will go into the intersection. It is safe to start crossing the street as soon as you see it entering the intersection.’;
                        }
                        else
                        {
                            if (crossingScenarios[ii] < 0) // NLPT on the same side but over the RIGHT shoulder
                                voice[ii, 0] = instruct_NLPTSameSideTurnDecisionRight; //'The first near lane parallel traffic car is over your right shoulder. It will make a right turn. It is not safe to start crossing the street. Wait until see a car entering the intersection.’;
                            else
                                voice[ii, 0] = instruct_NLPTSameSideTurnDecisionLeft; // 'The first near lane parallel traffic car is over your left shoulder. It will make a right turn. It is not safe to start crossing the street. Wait until see a car entering the intersection.’;                       

                        }
                    }
                }
                // other settings for scenarios in the block
                for (int ii = 0; ii < blockSize; ii++)
                {
                    collectLearnerResponse[ii] = learnerResponseNone;
                    useKeyEvent[ii] = keyEventParaRed2Green;
                    if (ii < 3) // the first two scenarios have key car special effect. The key car is a straightgoing car.It may be the first car. If the first car is a turning car, then the key car is the second car.The special effect is a hunk of horn.
                    {
                        useKeyCar[ii] = keyCarYes;
                        keyCarSE[ii] = keyCarSEHunk;
                    } // key car hunks when entering the intersection
                    else  // the third and fourth scenarios do not have key car special effect
                    {
                        useKeyCar[ii] = keyCarNone;
                        keyCarSE[ii] = keyCarSENone;
                    }
                    firstCar[ii] = p[ii];
                    feedback[ii] = feedbackNone;
                }
            }
            else if ((purpose == purposePractice) || (purpose == purposeAssessment)) // practice or assessment
            {
                int[] pp = new int[4] { firstCarStraightGoing, firstCarRightTurn, firstCarStraightGoing, firstCarRightTurn };  //this defines the behavior of the first NLPT car
                int[] p = random(pp);

                for (int ii = 0; ii < crossingScenarios.Length; ii++)
                {
                    streetWithTraffic[ii] = streetBoth;
                    Lanes[ii] = laneAll; // There are only near lane parallel traffic in the first two scenarios
                    startMovingTraffic[ii] = streetParallel;
                    voice[ii, 0] = practice_crossingDecision; // 'Say 'Go' when it is safe to start crossing the street.’;
                }
                //other settings for scenarios in the block
                for (int ii = 0; ii < blockSize; ii++)
                {
                    collectLearnerResponse[ii] = learnerResponseVerbalGo;
                    useKeyEvent[ii] = keyEventParaRed2Green;
                    useKeyCar[ii] = keyCarNone;
                    keyCarSE[ii] = keyCarSENone;
                    firstCar[ii] = p[ii];
                    feedback[ii] = feedbackNone;
                }
            }

        }
        //end of concept 6

        //concept 7
        else if (concept == conceptFinalEvaluation)
        {
            int blockSize = 4;
            int numTrafficCycles = 0; // these are event-centered scenarios
            int numSame = (int)Mathf.Ceil(blockSize / 2); // round rounds up numbers.round(5/2) = 3;
            int numOpposite = blockSize - numSame;
            
            selectScenarios(appropriateOppositeSide, numOpposite, oppositeUsed, out scenariosOppositeSide, out oppositeExhausted);  // the function returns indices of appropriate crossings and whether all crossings have been used.
            selectScenarios(appropriateSameSide, numSame, sameUsed, out scenariosSameSide, out sameExhausted);
            
            int[] qx = scenariosOppositeSide.Concat(scenariosSameSide).ToArray();

            crossingScenarios = random(qx);

            //put corssingSCenarios in UsedScenarions
            // there is no instruction and assessment phases
            for (int ii = 0; ii < crossingScenarios.Length; ii++)
            {
                float rand = Random.Range(0.0f, 1.0f);
                if (rand > 0.5)
                    startMovingTraffic[ii] = streetParallel;
                else
                    startMovingTraffic[ii] = streetPerpendicular;

                voice[ii, 0] = evaluation_NLPTS; // 'Say 'Go' when it is safe to start crossing the street.’;
                collectLearnerResponse[ii] = learnerResponseVerbalGo;
                useKeyEvent[ii] = keyEventNone;
                useKeyCar[ii] = keyCarNone;
                keyCarSE[ii] = keyCarSENone;
                firstCar[ii] = firstCarAny;
                feedback[ii] = feedbackNone;
            }
        }

    }

    public void LoadScenario(int crossingScenario)
    {
        if (crossingScenario < 0)
        {
            crossingScenario = crossingScenario * -1;
        }
        int intersectionNumber, cornerNumber;
        intersectionNumber = crossings[crossingScenario].intersectionIndex;
        //crossings[crossIndex].cornerIndex = (int)Mathf.Ceil(crossing / 2); // round up.ceil(3 / 2) = 2; ceil(4 / 2) = 2; ceil(5 / 2) = 3; ... make 1 to 8 to 1 to 4.
        cornerNumber = crossings[crossingScenario].crossingNum;

        Debug.Log("Active Intersection is:  " + intersectionNumber);
        Debug.Log("Active Corner is:  " + cornerNumber);

        SpawnPlayer(crossingScenario);
    }

    public void PlayVoice(double index)
    {
        Debug.Log("Voice index = " + index);
        if (index == 1)
            audioSource.PlayOneShot(voice1);
        else if (index == 2)
            audioSource.PlayOneShot(voice2);
        else if (index == 11)
            audioSource.PlayOneShot(voice11);
        else if (index == 12)
            audioSource.PlayOneShot(voice12);
        else if (index == 13)
            audioSource.PlayOneShot(voice13);
        else if (index == 14)
            audioSource.PlayOneShot(voice14);
        else if (index == 15)
            audioSource.PlayOneShot(voice15);
        else if (index == 16)
            audioSource.PlayOneShot(voice16);
        else if (index == 17)
            audioSource.PlayOneShot(voice17);
        else if (index == 18)
            audioSource.PlayOneShot(voice18);

        else if (index == 21)
            audioSource.PlayOneShot(voice21);
        else if (index == 22)
            audioSource.PlayOneShot(voice22);
        else if (index == 23)
            audioSource.PlayOneShot(voice23);
        else if (index == 24)
            audioSource.PlayOneShot(voice24);

        else if (index == 41)
            audioSource.PlayOneShot(voice41);
        else if (index == 42)
            audioSource.PlayOneShot(voice42);
        else if (index == 43)
            audioSource.PlayOneShot(voice43);
        else if (index == 44)
            audioSource.PlayOneShot(voice44);

        else if (index == 51)
            audioSource.PlayOneShot(voice51);
        else if (index == 52)
            audioSource.PlayOneShot(voice52);
        else if (index == 53)
            audioSource.PlayOneShot(voice53);
        else if (index == 54)
            audioSource.PlayOneShot(voice54);
        else if (index == 55)
            audioSource.PlayOneShot(voice55);
        else if (index == 56)
            audioSource.PlayOneShot(voice56);
        else if (index == 57)
            audioSource.PlayOneShot(voice57);
        else if (index == 58)
            audioSource.PlayOneShot(voice58);

        else if (index == 61)
            audioSource.PlayOneShot(voice61);
        else if (index == 62)
            audioSource.PlayOneShot(voice62);
        else if (index == 63)
            audioSource.PlayOneShot(voice63);
        else if (index == 64)
            audioSource.PlayOneShot(voice64);
        else if (index == 65)
            audioSource.PlayOneShot(voice65);
        else if (index == 66)
            audioSource.PlayOneShot(voice66);
        else if (index == 67)
            audioSource.PlayOneShot(voice67);
        else if (index == 68)
            audioSource.PlayOneShot(voice68);

        else if (index == 101)
            audioSource.PlayOneShot(voice101);
        else if (index == 102)
            audioSource.PlayOneShot(voice102);
        else if (index == 111)
            audioSource.PlayOneShot(voice111);
        else if (index == 112)
            audioSource.PlayOneShot(voice112);


        else if (index == 121)
            audioSource.PlayOneShot(voice121);
        else if (index == 131)
            audioSource.PlayOneShot(voice131);
        else if (index == 141)
            audioSource.PlayOneShot(voice141);
        else if (index == 151)
            audioSource.PlayOneShot(voice151);
        else if (index == 201)
            audioSource.PlayOneShot(voice201);
    }

    public void SetActiveFirstLane(int index, double firstLane)
    {
        if (index < 0)
            index = index * -1;

        int intersectionNumber, cornerNumber;
        intersectionNumber = crossings[index].intersectionIndex;
        cornerNumber = crossings[index].crossingNum;

        if (firstLane == 1)
        {
            if (intersectionNumber == 0)
            {
                if (cornerNumber == 1 || cornerNumber == 2 || cornerNumber == 5 || cornerNumber == 6)
                {
                    TrafficSignal1.GetComponent<StandardSemaphoreSystem>().blockFirstWay = false;
                    Debug.Log("false");
                }
                else
                {
                    TrafficSignal1.GetComponent<StandardSemaphoreSystem>().blockFirstWay = true;
                    Debug.Log("True");
                }
            }
            else if (intersectionNumber == 1)
            {
                if (cornerNumber == 1 || cornerNumber == 2 || cornerNumber == 5 || cornerNumber == 6)
                {

                    TrafficSignal2.GetComponent<StandardSemaphoreSystem>().blockFirstWay = false;
                    Debug.Log("false");
                }
                else
                {
                    TrafficSignal2.GetComponent<StandardSemaphoreSystem>().blockFirstWay = true;
                    Debug.Log("True");
                }
            }
            else if (intersectionNumber == 2)
            {
                if (cornerNumber == 1 || cornerNumber == 2 || cornerNumber == 5 || cornerNumber == 6)
                {
                    TrafficSignal3.GetComponent<StandardSemaphoreSystem>().blockFirstWay = false;
                    Debug.Log("false");
                }
                else
                {
                    TrafficSignal3.GetComponent<StandardSemaphoreSystem>().blockFirstWay = true;
                    Debug.Log("True");
                }
            }
            else if (intersectionNumber == 3)
            {
                if (cornerNumber == 1 || cornerNumber == 2 || cornerNumber == 5 || cornerNumber == 6)
                {
                    TrafficSignal4.GetComponent<StandardSemaphoreSystem>().blockFirstWay = false;
                    Debug.Log("false");
                }
                else
                {

                    TrafficSignal4.GetComponent<StandardSemaphoreSystem>().blockFirstWay = true; Debug.Log("True");
                }
            }
            else if (intersectionNumber == 4)
            {
                if (cornerNumber == 1 || cornerNumber == 2 || cornerNumber == 5 || cornerNumber == 6)
                {

                    TrafficSignal5.GetComponent<StandardSemaphoreSystem>().blockFirstWay = false;
                    Debug.Log("false");
                }
                else
                {
                    TrafficSignal5.GetComponent<StandardSemaphoreSystem>().blockFirstWay = true;
                    Debug.Log("True");
                }
            }
        }
        if (firstLane == -1)
        {
            if (intersectionNumber == 0)
            {
                if (cornerNumber == 1 || cornerNumber == 2 || cornerNumber == 5 || cornerNumber == 6)
                {
                    TrafficSignal1.GetComponent<StandardSemaphoreSystem>().blockFirstWay = true;
                    //TrafficSignal1.GetComponent<StandardSemaphoreSystem>().SetFlow();
                }
                else
                {
                    TrafficSignal1.GetComponent<StandardSemaphoreSystem>().blockFirstWay = false;
                }
            }
            else if (intersectionNumber == 1)
            {
                if (cornerNumber == 1 || cornerNumber == 2 || cornerNumber == 5 || cornerNumber == 6)
                {
                    TrafficSignal2.GetComponent<StandardSemaphoreSystem>().blockFirstWay = true;
                }
                else
                {
                    TrafficSignal2.GetComponent<StandardSemaphoreSystem>().blockFirstWay = false;
                }
            }
            else if (intersectionNumber == 2)
            {
                if (cornerNumber == 1 || cornerNumber == 2 || cornerNumber == 5 || cornerNumber == 6)
                {
                    TrafficSignal3.GetComponent<StandardSemaphoreSystem>().blockFirstWay = true;
                }
                else
                {
                    TrafficSignal3.GetComponent<StandardSemaphoreSystem>().blockFirstWay = false;
                }
            }
            else if (intersectionNumber == 3)
            {
                if (cornerNumber == 1 || cornerNumber == 2 || cornerNumber == 5 || cornerNumber == 6)
                {
                    TrafficSignal4.GetComponent<StandardSemaphoreSystem>().blockFirstWay = true;
                }
                else
                {
                    TrafficSignal4.GetComponent<StandardSemaphoreSystem>().blockFirstWay = false;
                }
            }
            else if (intersectionNumber == 4)
            {
                if (cornerNumber == 1 || cornerNumber == 2 || cornerNumber == 5 || cornerNumber == 6)
                {
                    TrafficSignal5.GetComponent<StandardSemaphoreSystem>().blockFirstWay = true;
                }
                else
                {
                    TrafficSignal5.GetComponent<StandardSemaphoreSystem>().blockFirstWay = false;
                }
            }
        }
        Debug.Log("Block First way change");
    }

    public int cornerNumber()
    {
        return crossingScenarios[i];
    }

    

    public void SpawnPlayer(int spawn)
    {
        //spawnLocations = GameObject.FindGameObjectsWithTag("SpawnPoint");

        Camera.transform.position = spawnLocations[spawn].transform.position;
        Camera.transform.rotation = spawnLocations[spawn].transform.rotation;
    }

    private static string returnVoiceText(double input)
    {
        string output = "";
        if (input == 101) { output = "Use the pointer to point at the parallel traffic"; }
        else if (input == 102) { output = "Use the pointer to point at the Perpendicular traffic"; }
        else if (input == 111) { output = "Point to the near lane parallel traffic when it stoppes"; }
        else if (input == 112) { output = "Point to the near lane parallel traffic when it is moving"; }
        else if (input == 121) { output = "Point to where the near lane parallel traffic stoppes"; }
        else if (input == 131) { output = "Say 'Now' when you detect a NLPT surge"; }
        else if (input == 141) { output = "Say 'Now' when you see a NLPT car enters the intersection"; }
        else if (input == 151) { output = "Say 'Go' when it is safe to start crossing the street"; }
        return output;
    }

    private static string returnFileText(int intersectionIndex, int cornerIndex, int crossingNum, int parallelStreet, int perpendicularStreet, int crossingDirection, int type, int nearLaneLoc)
    {
        string output = "";
        if (intersectionIndex == 0) { output = "Purpose: 0"; }
        else if (intersectionIndex == 1) { output = "Purpose: Instruction"; }
        else if (intersectionIndex == 2) { output = "Purpose: Practice"; }
        else if (intersectionIndex == 3) { output = "Purpose: Assessment"; }
        else if (intersectionIndex == 4) { output = "Purpose: Final Evaluation"; }

        if (cornerIndex == 0) { output = output + ", Corner: North East"; }
        else if (cornerIndex == 1) { output = output + ", Corner: North West"; }
        else if (cornerIndex == 2) { output = output + ", Corner: South West"; }
        else if (cornerIndex == 3) { output = output + ", Corner: South East"; }

        if (crossingNum == 0) { output = output + ", Concept: 0"; }
        else if (crossingNum == 1) { output = output + ", Concept: Parallel and perpendicular traffic"; }
        else if (crossingNum == 2) { output = output + ", Concept: Near lane parallel traffic"; }
        else if (crossingNum == 3) { output = output + ", Concept: Near lane parallel traffic stop"; }
        else if (crossingNum == 4) { output = output + ", Concept: Near lane parallel traffic surge"; }
        else if (crossingNum == 5) { output = output + ", Concept: Confirm Near lane parallel traffic surge"; }
        else if (crossingNum == 6) { output = output + ", Concept: Make crossing decision"; }
        else if (crossingNum == 7) { output = output + ", Concept: Final Evaluation"; }

        if (parallelStreet == 0) { output = output + ", Parallel: 0"; }
        else if (parallelStreet == 1) { output = output + ", Parallel: 1"; }
        else if (parallelStreet == 2) { output = output + ", Parallel: 2"; }
        else if (parallelStreet == 3) { output = output + ", Parallel: 3"; }
        else if (parallelStreet == 4) { output = output + ", Parallel: 4"; }
        else if (parallelStreet == 5) { output = output + ", Parallel: 5"; }
        else if (parallelStreet == 6) { output = output + ", Parallel: 6"; }
        else if (parallelStreet == 7) { output = output + ", Parallel: 7"; }
        else if (parallelStreet == 8) { output = output + ", Parallel: 8"; }
        else if (parallelStreet == 9) { output = output + ", Parallel: 9"; }

        if (perpendicularStreet == 0) { output = output + ", Perpendicular: 0"; }
        else if (perpendicularStreet == 1) { output = output + ", Perpendicular: 1"; }
        else if (perpendicularStreet == 2) { output = output + ", Perpendicular: 2"; }
        else if (perpendicularStreet == 3) { output = output + ", Perpendicular: 3"; }
        else if (perpendicularStreet == 4) { output = output + ", Perpendicular: 4"; }
        else if (perpendicularStreet == 5) { output = output + ", Perpendicular: 5"; }
        else if (perpendicularStreet == 6) { output = output + ", Perpendicular: 6"; }
        else if (perpendicularStreet == 7) { output = output + ", Perpendicular: 7"; }
        else if (perpendicularStreet == 8) { output = output + ", Perpendicular: 8"; }
        else if (perpendicularStreet == 9) { output = output + ", Perpendicular: 9"; }

        if (crossingDirection == 1) { output = output + ", Direction: North"; }
        else if (crossingDirection == 2) { output = output + ", Direction: South"; }
        else if (crossingDirection == 3) { output = output + ", Direction: East"; }
        else if (crossingDirection == 4) { output = output + ", Direction: West"; }

        if (type == 1) { output = output + ", Type: CCW"; }
        else if (type == 2) { output = output + ", Type: CW"; }

        if (nearLaneLoc == 1) { output = output + ", Near lane: Same side"; }
        else if (nearLaneLoc == 2) { output = output + ", Near lane: Opposite side"; }

        return output;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            SceneManager.LoadScene("Home page");
        if (Input.GetKeyDown(KeyCode.RightArrow))
            Application.Quit();
    }

    int intersectionNumber = -1, corNum = -1;
}