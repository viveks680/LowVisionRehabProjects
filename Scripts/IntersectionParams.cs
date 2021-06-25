using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SocialPlatforms;

public class IntersectionParams : MonoBehaviour
{
    //public static IntersectionParams IP;
    public int ways, NSWays, EWWays;

    public int lanes, NSLanes, EWLanes;

    public bool divide, NSDivide, EWDivide;
    public int spawnLocation;
    //public List<string> perpendicularStreetWidth = new List<string>() { "Narrow", "Normal", "Wide" };
    public int ThisStreetWidth; // 0, 1, 2 from above list
    public int laneWidth = 10, divideWidth = 10, EWStreetWidthFeet, NSStreetWidthFeet;
    public int DivideNS, DivideEW;
    public int FDNW , EWWalkFDNWPhase , NSWalkFDNWPhase;
    private int FDNWdivisor;
    public int EWWalkInterval, EWFDNWInterval, EWDontWalkInterval, 
        EWGreenInterval, EWYellowInterval, EWRedInterval, 
        NSWalkInterval, NSFDNWInterval, NSDontWalkInterval, 
        NSGreenInterval, NSYellowInterval, NSRedInterval;
    public int greenPortion = 20, yellowPortion = 5, redPortion = 20;
    public int fixedWALKInterval = 7, walkSpeed = 3;
    public int cornerDir;
    public string corner, direction;
    public float perpendicularStreetWidth;



    public void CornerDirection(int spawn)
    {
        cornerDir = spawn;
        if (spawn == 0)
        {
            corner = "SW";
            direction = "N";
        }
        else if (spawn == 1)
        {
            corner = "SW";
            direction = "E";
        }
        else if (spawn == 2)
        {
            corner = "SE";
            direction = "W";
        }
        else if (spawn == 3)
        {
            corner = "SE";
            direction = "N";
        }
        else if (spawn == 4)
        {
            corner = "NE";
            direction = "S";
        }
        else if (spawn == 5)
        {
            corner = "NE";
            direction = "W";
        }
        else if (spawn == 6)
        {
            corner = "NW";
            direction = "E";
        }
        else if (spawn == 7)
        {
            corner = "SW";
            direction = "S";
        }
        ParametersLoad();
    }

    
    public void ParametersLoad()
    {
        //Debug.Log("Parameter loading function called");
        TrafficCycle();

        //Debug.Log("ThisStreetWidth is being calced");
        
        if (direction == "E" || direction == "W")
        {
            //Debug.Log("ThisStreetWidth for n or s");
            if (NSStreetWidthFeet <= 20)
            {
                perpendicularStreetWidth = NSStreetWidthFeet;
                ThisStreetWidth = 0;
            }
            if(20 < NSStreetWidthFeet && NSStreetWidthFeet <= 40)
            {
                perpendicularStreetWidth = NSStreetWidthFeet;
                ThisStreetWidth = 1;
            }
            if(40 < NSStreetWidthFeet && NSStreetWidthFeet <= 60)
            {
                perpendicularStreetWidth = NSStreetWidthFeet;
                ThisStreetWidth = 2;
            }
        }
        else if (direction == "N" || direction == "S")
        {
            //Debug.Log("ThisStreetWidth for e or w");
            if (EWStreetWidthFeet <= 20)
            {
                perpendicularStreetWidth = EWStreetWidthFeet;
                ThisStreetWidth = 0;
            }
            if (20 < EWStreetWidthFeet && EWStreetWidthFeet <= 40)
            {
                perpendicularStreetWidth = EWStreetWidthFeet;
                ThisStreetWidth = 1;
            }
            if (40 < EWStreetWidthFeet && EWStreetWidthFeet <= 60)
            {
                perpendicularStreetWidth = EWStreetWidthFeet;
                ThisStreetWidth = 2;
            }
        }
        //Debug.Log("ThisStreetWidth = " + ThisStreetWidth);


        
        FDNWdivisor = Random.Range(3,4);
        if (ThisStreetWidth == 0)
        {
            FDNW = (ThisStreetWidth + 1) * 10 / FDNWdivisor;
        }
        else if(ThisStreetWidth == 1)
        {
            FDNW = (ThisStreetWidth + 1) * 10 / FDNWdivisor;
        }
        else if (ThisStreetWidth == 2)
        {
            FDNW = (ThisStreetWidth + 1) * 10 / FDNWdivisor;
        }
        //Debug.Log("FDNW = " + FDNW);
        
    }


    public void TrafficCycle()
    {
        if (NSDivide == true)
        {
            DivideNS = 1;
        }
        else
        {
            DivideNS = 0;
        }
        if (EWDivide == true)
        {
            DivideEW = 1;
        }
        else
        {
            DivideEW = 0;
        }
        EWStreetWidthFeet = EWWays * EWLanes * laneWidth + DivideEW * divideWidth; // = 40 feet
        EWWalkInterval = fixedWALKInterval; // 7 sec;
        EWFDNWInterval = EWStreetWidthFeet / walkSpeed; // = 14 sec;
        EWWalkFDNWPhase = EWWalkInterval + EWFDNWInterval; //  = 7 + 14 = 21 sec
        NSGreenInterval = EWWalkFDNWPhase * greenPortion / (greenPortion + yellowPortion); // 5 = 16 sec
        NSYellowInterval = EWWalkFDNWPhase - NSGreenInterval; // = 5 sec
        // computer NS WALK and Flashing DON’T WALK phases, the sum of which is EW green light
        NSStreetWidthFeet = NSWays * NSLanes * laneWidth + DivideNS * divideWidth; // = 30 feet
        NSWalkInterval = fixedWALKInterval; // 7 sec;
        NSFDNWInterval = NSStreetWidthFeet / walkSpeed; // = 10 sec;
        NSWalkFDNWPhase = NSWalkInterval + NSFDNWInterval; //  = 7 + 10 = 17;
        EWGreenInterval = NSWalkFDNWPhase * greenPortion / (greenPortion + yellowPortion); // = 14 sec
        EWYellowInterval = NSWalkFDNWPhase - EWGreenInterval; // = 3 sec
        // compute the red intervals the two streets, which is the green + yellow phases of the other streets
        NSRedInterval = EWGreenInterval + EWYellowInterval; // = 17 sec
        EWRedInterval = NSGreenInterval + NSYellowInterval; //= 21 sec
        // compute the DON’T WALK interval, which is the Red interval of the same street
        //Debug.Log(EWRedInterval);
        //Debug.Log(NSRedInterval);
        NSDontWalkInterval = NSRedInterval; // = 21 sec;
        EWDontWalkInterval = EWRedInterval; // = 17 sec;
        //Debug.Log(NSDontWalkInterval);
        //Debug.Log(EWDontWalkInterval);
        //Debug.Log("Traffic cycle calcs have finished");
    }
}
