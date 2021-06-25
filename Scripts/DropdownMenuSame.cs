using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DropdownMenuSame : MonoBehaviour
{
    public Text DDLLabel;
    static public int index;
    static public string text;
    static public int IndexValue;
    static public string TextValue;

    public void Start()
    {
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }

        var dropdown = transform.GetComponent<Dropdown>();

        List<string> items = new List<string>();
        //items.Add("Corner 2 - 11 Feet");
        items.Add("Corner 3 - 11 Feet");
        //items.Add("Corner 4 - 7.5 Feet");
        items.Add("Corner 5 - 7.5 Feet");
        items.Add("Corner 6 - 11 Feet");
        //items.Add("Corner 7 - 11 Feet");
        items.Add("Corner 8 - 7.5 Feet");
        items.Add("Corner 9 - 11 Feet");
        //items.Add("Corner 10 - 22 Feet");
        items.Add("Corner 11 - 22 Feet");
        //items.Add("Corner 12 - 11 Feet");
        items.Add("Corner 13 - 11 Feet");
        items.Add("Corner 14 - 22 Feet");
        //items.Add("Corner 15 - 22 Feet");
        //items.Add("Corner 16 - 11 Feet");
        items.Add("Corner 17 - 14.5 Feet");
        //items.Add("Corner 18 - 22 Feet");
        items.Add("Corner 19 - 22 Feet");
        //items.Add("Corner 20 - 14.5 Feet");
        items.Add("Corner 21 - 14.5 Feet");
        items.Add("Corner 22 - 22 Feet");
        //items.Add("Corner 23 - 22 Feet");
        //items.Add("Corner 24 - 14.5 Feet");
        items.Add("Corner 25 - 29 Feet");
        //items.Add("Corner 26 - 18.5 Feet");
        items.Add("Corner 27 - 18.5 Feet");
        //items.Add("Corner 28 - 29 Feet");
        items.Add("Corner 29 - 29 Feet");
        //items.Add("Corner 30 - 18.5 Feet");
        items.Add("Corner 31 - 18.5 Feet");
        //items.Add("Corner 32 - 29 Feet");
        items.Add("Corner 33 - 29 Feet");
        //items.Add("Corner 34 - 28.5 Feet");
        items.Add("Corner 35 - 28.5 Feet");
        //items.Add("Corner 36 - 29 Feet");
        items.Add("Corner 37 - 29 Feet");
        //items.Add("Corner 38 - 28.5 Feet");
        items.Add("Corner 39 - 28.5 Feet");
        //items.Add("Corner 40 - 29 Feet");

        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }

        if (index == 0)
        {
            text = "Corner 3 - 11 Feet";
            DropdownItemSelected(dropdown);
        }
        DDLLabel.text = text;
        dropdown.value = index;

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    public void DropdownItemSelected(Dropdown dropdown)
    {
        index = dropdown.value;
        text = dropdown.options[index].text;
        SetValue(index, text);
    }

    public void SetValue(int index, string text)
    {
        if (index == 0)
        {
            IndexValue = index + 2;
            TextValue = text;
        }
        else if ((index == 1) || (index == 2))
        {
            IndexValue = index + 3;
            TextValue = text;
        }
        else if ((index == 3) || (index == 4))
        {
            IndexValue = index + 4;
            TextValue = text;
        }
        else if (index == 5)
        {
            IndexValue = index + 5;
            TextValue = text;
        }
        else if ((index == 6) || (index == 7))
        {
            IndexValue = index + 6;
            TextValue = text;
        }
        else if (index == 8)
        {
            IndexValue = index + 8;
            TextValue = text;
        }
        else if (index == 9)
        {
            IndexValue = index + 9;
            TextValue = text;
        }
        else if ((index == 10) || (index == 11))
        {
            IndexValue = index + 10;
            TextValue = text;
        }
        else if (index == 12)
        {
            IndexValue = index + 12;
            TextValue = text;
        }
        else if (index == 13)
        {
            IndexValue = index + 13;
            TextValue = text;
        }
        else if (index == 14)
        {
            IndexValue = index + 14;
            TextValue = text;
        }
        else if (index == 15)
        {
            IndexValue = index + 15;
            TextValue = text;
        }
        else if (index == 16)
        {
            IndexValue = index + 16;
            TextValue = text;
        }
        else if (index == 17)
        {
            IndexValue = index + 17;
            TextValue = text;
        }
        else if (index == 18)
        {
            IndexValue = index + 18;
            TextValue = text;
        }
        else
        {
            IndexValue = index + 19;
            TextValue = text;
        }
    }

    public int GetIndexValue()
    {
        return IndexValue;
    }

    public string GetTextValue()
    {
        return TextValue;
    }

}
