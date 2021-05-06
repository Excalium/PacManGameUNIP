using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoCarros : MonoBehaviour
{
    public Transform[] waypoints;
    bool carroDestruido = false;
    int cur = 0;
    public float speed = 0.5f;

    void FixedUpdate()
    {
        if (!carroDestruido)
        {
            if (transform.position != waypoints[cur].position)
            {
                Vector2 p = Vector2.MoveTowards(transform.position,
                                                waypoints[cur].position,
                                                speed);
                GetComponent<Rigidbody2D>().MovePosition(p);
            }
            else cur = (cur + 1) % waypoints.Length;
        }

        Vector2 dir = waypoints[cur].position - transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    void OnTriggerEnter2D(Collider2D co)
    {
        if (co.name == "PacMan" && co.transform.GetComponent<MovimentoPacMan>().powerUpDestroyAtivo)
            StartCoroutine(Destruido());

        if (co.name == "PacMan" && !co.transform.GetComponent<MovimentoPacMan>().powerUpDestroyAtivo)
            Destroy(co.gameObject);
    }

    private IEnumerator Destruido()
    {
        carroDestruido = true;
        GetComponent<Rigidbody2D>().MovePosition(Vector2.MoveTowards(transform.position, new Vector2(-355, -355), speed));
        yield return new WaitForSecondsRealtime(3);
        carroDestruido = false;
    }
}