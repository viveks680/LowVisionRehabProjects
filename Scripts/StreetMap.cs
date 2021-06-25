using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetMap
{
    int crossingID;
    int intersectionID;
    int cornerID;
    int crossingNum;
    int perpendicularWidth;
    string crossingType;
    string NLPTloc;
    int numTrafficCycles;
    string streetWithTraffic;
    string laneWithTraffic;
    string movingTrafficStart;
    string keyEvent;
    string keyCar;
    string keyCarSE;
    string firstCarBehavior;
    string learnerResponse;
    string feedback;
    string voice1;
    string voice2;

    public StreetMap(int crossID, int intersectID, int corID, int crosNum, int perpWidth, string crossType, string NLPTlc, int numTrafCycles, string streetWithTraf, string laneWithTraf, string movingTrafficSt, string keyE, string keyC, string keyCSE, string firstCarB, string lResponse, string fdbk, string v1, string v2)
    {
         crossingID = crossID;
         intersectionID = intersectID;
         cornerID = corID;
         crossingNum = crosNum;
         perpendicularWidth = perpWidth;
         crossingType = crossType;
         NLPTloc = NLPTlc;
         numTrafficCycles = numTrafCycles;
         streetWithTraffic = streetWithTraf;
         laneWithTraffic = laneWithTraf;
         movingTrafficStart = movingTrafficSt;
         keyEvent = keyE;
         keyCar = keyC;
         keyCarSE = keyCSE;
         firstCarBehavior = firstCarB;
         learnerResponse = lResponse;
         feedback = fdbk;
         voice1 = v1;
         voice2 = v2 ;
    }
}
