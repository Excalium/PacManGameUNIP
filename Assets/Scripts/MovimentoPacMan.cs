using UnityEngine;

public class MovimentoPacMan : MonoBehaviour
{


    Vector2 rayDireita = new Vector2(4, 0);
    Vector2 rayEsquerda = new Vector2(-4, 0);
    Vector2 rayCima = new Vector2(0, 4);
    Vector2 rayBaixo = new Vector2(0, -4);

    public float speed = 0.5f;
    Vector2 dest = Vector2.zero;
    void Start()
    {
        dest = transform.position;

    }

    private void FixedUpdate()
    {


        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);

        if ((Vector2)transform.position == dest)
        {
            if (Input.GetKey(KeyCode.UpArrow) && valid(rayCima))
                dest = (Vector2)transform.position + Vector2.up;
            if (Input.GetKey(KeyCode.RightArrow) && valid(rayDireita))
                dest = (Vector2)transform.position + Vector2.right;
            if (Input.GetKey(KeyCode.DownArrow) && valid(rayBaixo))
                dest = (Vector2)transform.position - Vector2.up;
            if (Input.GetKey(KeyCode.LeftArrow) && valid(rayEsquerda))
                dest = (Vector2)transform.position - Vector2.right;
        }

        bool valid(Vector2 dir)
        {
            Vector2 pos = transform.position;
            Debug.DrawRay(pos, dir, Color.red, 1.0f, false);
            Debug.DrawRay(pos + Vector2.up, dir, Color.red, 1.0f, false);
            Debug.DrawRay(pos - Vector2.up, dir, Color.red, 1.0f, false);
            Debug.DrawRay(pos + Vector2.right, dir, Color.red, 1.0f, false);
            Debug.DrawRay(pos - Vector2.right, dir, Color.red, 1.0f, false);
            RaycastHit2D hitCenter = Physics2D.Raycast(pos, dir, 2f);
            RaycastHit2D hitTop = Physics2D.Raycast(pos + Vector2.up , dir, 2f);
            RaycastHit2D hitBottom = Physics2D.Raycast(pos - Vector2.up, dir, 2f);
            RaycastHit2D hitRight = Physics2D.Raycast(pos + Vector2.right, dir, 2f);
            RaycastHit2D hitLeft = Physics2D.Raycast(pos - Vector2.right, dir, 2f);
            if (hitCenter.collider != null || 
                hitTop.collider != null || 
                hitBottom.collider != null || 
                hitRight.collider != null || 
                hitLeft.collider != null)
            {

                Debug.Log("Hit Something!");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}