using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DropdownCrossing : MonoBehaviour
{
    public Button EnterButton;
    public GameObject DDLSame;
    public Text DDLLabel;
    static public int index;
    static public string text;
    static public int SideIndexValue;
    static public string SideTextValue;

    void Start()
    {
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }

        var dropdown = transform.GetComponent<Dropdown>();

        List<string> items = new List<string>();
        items.Add("Same side");
        items.Add("Opposite side");

        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }

        if (index == 0)
        {
            text = "Same side";
            DropdownItemSelected(dropdown);            
            DDLSame.SetActive(true);
        }
        else if (index == 1)
            DDLSame.SetActive(false);

        DDLLabel.text = text;
        dropdown.value = index;

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });

        Button btn = EnterButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene("ScenarioSelect");
    }

    public void DropdownItemSelected(Dropdown dropdown)
    {
        index = dropdown.value;
        text = dropdown.options[index].text;
        SetValue(index, text);

        if (index == 0)
            DDLSame.SetActive(true);
        else if(index == 1)
            DDLSame.SetActive(false);
    }

    public void SetValue(int index, string text)
    {
        SideIndexValue = index;
        SideTextValue = text;
    }

    public int GetSideIndexValue()
    {
        return SideIndexValue;
    }

    public string GetSideTextValue()
    {
        return SideTextValue;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Application.Quit();
        }
    }
}