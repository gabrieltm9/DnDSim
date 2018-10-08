using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToggleObjScript : MonoBehaviour {
    //Used in the create spell UI to enable/disable sub-input fields based on user choices
    public List<string> stringsToTest;
    public List<GameObject> objsToToggle;

    public List<string> stringsToTestIfNot;
    public GameObject objToToggleIfStringNot;

    public void testForToggleString(Text text)
    {
        for(int x = 0; x < stringsToTest.Count; x++)
        {
            if (text.text == stringsToTest[x])
                objsToToggle[x].SetActive(true);
            else
            {
                if (objsToToggle[x].activeSelf == true)
                    objsToToggle[x].SetActive(false);
            }
        }
    }

    public void testForToggleBool(Toggle toggle)
    {
        if (toggle.isOn)
            objsToToggle[1].SetActive(true);
        else if(objsToToggle[1].activeSelf == true)
            objsToToggle[1].SetActive(false);
    }

    public void testForToggleNotString(Text text)
    {
        objToToggleIfStringNot.SetActive(true);
        foreach (string str in stringsToTestIfNot)
            if (str == text.text)
                objToToggleIfStringNot.SetActive(false);
    }

    public void toggleObj(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }
}
