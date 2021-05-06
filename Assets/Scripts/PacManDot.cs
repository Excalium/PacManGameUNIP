using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PacManDot : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D co)
    {

        if (co.name == "PacMan")
        {
            Destroy(gameObject);
            Score.pontos += 10;
            Debug.Log("Pontos: " + Score.pontos);
        }
    }
}