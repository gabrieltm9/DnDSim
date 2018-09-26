using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject lmao;//meme
    public AudioSource lmao2;//meme

    public AudioSource gameAudio;

    private bool tokenSelected;//a bool for the game to know if the player is trying to move, attack, etc. with his token

    private float moveSpeed = 0.5f;

    public void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(0,0, moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(0, 0, -moveSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(-moveSpeed, 0,0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(moveSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            gameObject.transform.Translate(0, moveSpeed, 0);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            gameObject.transform.Translate(0, -moveSpeed, 0);
        }
        if (Input.GetKey(KeyCode.Tab))
        {
            Instantiate(lmao,new Vector3(Random.Range(0,20),-1, Random.Range(0, 20)),Quaternion.identity);
            Instantiate(lmao2, new Vector3(Random.Range(0, 20), 0, Random.Range(0, 20)), Quaternion.identity);
        }

    }
}
