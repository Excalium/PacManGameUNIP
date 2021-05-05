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
        }
    }
}
