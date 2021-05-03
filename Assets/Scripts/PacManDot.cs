using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PacManDot : MonoBehaviour
{
    private MovimentoPacMan teste;

    void OnTriggerEnter2D(Collider2D co)
    {

        if (co.name == "PacMan")
        {
            Destroy(gameObject);
            //teste = GetComponent<MovimentoPacMan>().AddPonto();
            //co.GetComponent<MovimentoPacMan>().AddPonto();
        }
    }
}
