using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public bool startTransitioning;
    public bool isTransitioning;
    public static bool isMainUIOpen = true;
    public static GameObject mainUI;
    public static bool isCharacterSheetUIOpen;
    public static GameObject characterSheetUI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isCharacterSheetUIOpen)
            {
                toggleCharacterSheetUI();
                toggleMainUI();
            }
        }
	}

    public void toggleCharacterSheetUI()
    {
        if(!isTransitioning)
        {
            isCharacterSheetUIOpen = !isCharacterSheetUIOpen;
            if (isCharacterSheetUIOpen)
            {
                characterSheetUI.GetComponent<Animator>().Play("FadeIn");
                characterSheetUI.transform.GetChild(4).GetComponent<ParticleSystem>().Play();
            }
            else
            {
                characterSheetUI.GetComponent<Animator>().Play("FadeOut");
                characterSheetUI.transform.GetChild(4).GetComponent<ParticleSystem>().Stop();
                //characterSheetUI.transform.GetChild(3).GetComponent<ParticleSystem>().Clear();
            }
            startTransitioning = true;
        }
    }

    public void toggleMainUI()
    {
        if(!isTransitioning)
        {
            isMainUIOpen = !isMainUIOpen;
            if (isMainUIOpen)
                mainUI.GetComponent<Animator>().Play("FadeIn");
            else
                mainUI.GetComponent<Animator>().Play("FadeOut");
            startTransitioning = true;
        }
    }

    public IEnumerator isTransitioningTimer()
    {
        isTransitioning = true;
        yield return new WaitForSeconds(1);
        isTransitioning = false;
    }

    public void LateUpdate()
    {
        if(startTransitioning)
        {
            StartCoroutine(isTransitioningTimer());
            startTransitioning = false;
        }
    }
}
