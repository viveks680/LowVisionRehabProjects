using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EventData
{
    public string Patientage;
    public string Patienttype;
    public string Patientseverity;
    

    public EventData (TrafficParameters tparams)
    {
        Patientage = tparams.SelectedAge;
        Patienttype = tparams.SelectedType;
        Patientseverity = tparams.SelectedSeverity;
    }
}
