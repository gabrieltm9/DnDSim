using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour {

    public GameObject gameController;

    public GameObject currentlyOpenPage;
    public GameObject mainPage;

    public void openSimulationUI()
    {
        if (currentlyOpenPage != null)
        {
            currentlyOpenPage.SetActive(false);
            currentlyOpenPage = null;
        }
        currentlyOpenPage = mainPage;
        mainPage.SetActive(true);
    }

    public void openPage(GameObject page)
    {

    }
}
