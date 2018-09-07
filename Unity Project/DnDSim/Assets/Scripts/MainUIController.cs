using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIController : MonoBehaviour
{
    private void Awake()
    {
        GameController.mainUI = gameObject;
    }
}
