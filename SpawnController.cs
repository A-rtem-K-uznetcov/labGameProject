using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private int numberCheck;
    private Vector3 spawnPos;
    public Vector3 SpawnPos { get => spawnPos; set => spawnPos = value; }
    // Start is called before the first frame update
    void Start()
    {
        numberCheck = 1;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Check")
        {
            if (numberCheck != Convert.ToInt16(collision.gameObject.name.Substring(gameObject.name.Length - 1)))
            {
                spawnPos = collision.transform.position;
                numberCheck = Convert.ToInt16(collision.gameObject.name.Substring(gameObject.name.Length - 1));
            }
        }
    }
}