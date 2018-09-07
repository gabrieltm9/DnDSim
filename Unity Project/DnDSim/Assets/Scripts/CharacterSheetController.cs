using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheetController : MonoBehaviour
{
    void Awake()
    {
        GameController.characterSheetUI = gameObject;
    }
}
