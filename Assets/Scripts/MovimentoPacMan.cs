using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPacMan : MonoBehaviour
{
    public float speed = 0.5f;
    Vector2 dest = Vector2.zero;
    void Start()
    {
        dest = transform.position;
    }

    private void FixedUpdate()
    {

        // Vector2 up = new Vector2(0, 3);
        // Vector2 down = new Vector2(0, -3);
        // Vector2 right = new Vector2(3, 0);
        // Vector2 left = new Vector2(-3, 0);

        // Move closer to Destination
        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);

        // Check for Input if not moving
        if ((Vector2)transform.position == dest)
        {
            if (Input.GetKey(KeyCode.UpArrow) && valid(Vector2.up))
                dest = (Vector2)transform.position + Vector2.up;
            if (Input.GetKey(KeyCode.RightArrow) && valid(Vector2.right))
                dest = (Vector2)transform.position + Vector2.right;
            if (Input.GetKey(KeyCode.DownArrow) && valid(Vector2.down))
                dest = (Vector2)transform.position + Vector2.down;
            if (Input.GetKey(KeyCode.LeftArrow) && valid(Vector2.left))
                dest = (Vector2)transform.position + Vector2.left;
        }

        bool valid(Vector2 dir)
        {
            // Cast Line from 'next to Pac-Man' to 'Pac-Man'
            Vector2 pos = transform.position;
            RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
            return (hit.collider == GetComponent<Collider2D>());
        }
    }
}

