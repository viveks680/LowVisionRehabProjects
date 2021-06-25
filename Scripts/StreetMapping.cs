using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StreetMapping//: IComparable<StreetMapping>
{
    public GameObject intersect;
    public int spawnDir;
    public StreetMapping(GameObject intersection, int cornerDir)
    {
        intersect = intersection;
        spawnDir = cornerDir;
    }

}
