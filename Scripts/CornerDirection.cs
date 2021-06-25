using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CorenerDirection//: IComparable<StreetMapping>
{
    public string corner, direction;
    public CorenerDirection(string cr, string dr)
    {
        corner = cr;
        direction = dr;
    }

}
