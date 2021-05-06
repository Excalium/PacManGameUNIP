using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PowerUps : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D co)
    {

        if (co.name == "PacMan")
        {
            Destroy(gameObject);
            Score.pontos += 50;
            Debug.Log("Pontos: " + Score.pontos);
            Debug.Log("Dots: " + Score.contDots);
        }
    }
}
