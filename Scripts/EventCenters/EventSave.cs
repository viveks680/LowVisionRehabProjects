using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class EventSave
{
    public static void SaveEvent(TrafficParameters tparams)
    {
        EventData ed = new EventData(tparams); 
        
        string json = JsonUtility.ToJson(ed, true);
        string path = "C:/Users/vivek/OneDrive/Desktop/GiFT Unity files/save.txt";
        File.WriteAllText(path, json);
    }
}
