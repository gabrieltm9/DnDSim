using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(0,0,1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(-1, 0,0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(1, 0, 0);
        }
    }
}
