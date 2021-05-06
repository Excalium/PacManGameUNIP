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
            GetComponent<AudioSource>().Play();
            GetComponent<Renderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            Score.contDots += 1;
        }
    }
}
