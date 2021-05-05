using UnityEngine;
using System.Collections;

public class MovimentoPacMan : MonoBehaviour
{

    Vector2 vecDireita = new Vector2(4, 0);
    Vector2 vecEsquerda = new Vector2(-4, 0);
    Vector2 vecCima = new Vector2(0, 4);
    Vector2 vecBaixo = new Vector2(0, -4);

    Vector2 rayX = new Vector2(1.5f, 0);
    Vector2 rayY = new Vector2(0, 1.5f);

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
            if (Input.GetKey(KeyCode.UpArrow) && valid(vecCima))
                dest = (Vector2)transform.position + Vector2.up;
            if (Input.GetKey(KeyCode.RightArrow) && valid(vecDireita))
                dest = (Vector2)transform.position + Vector2.right;
            if (Input.GetKey(KeyCode.DownArrow) && valid(vecBaixo))
                dest = (Vector2)transform.position - Vector2.up;
            if (Input.GetKey(KeyCode.LeftArrow) && valid(vecEsquerda))
                dest = (Vector2)transform.position - Vector2.right;
        }

        Vector2 dire = dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dire.x);
        GetComponent<Animator>().SetFloat("DirY", dire.y);

        bool valid(Vector2 dir)
        {
            Vector2 pos = transform.position;
            Debug.DrawRay(pos, dir, Color.red, 1.0f, false);
            Debug.DrawRay(pos + rayY, dir, Color.blue, 1.0f, false);
            Debug.DrawRay(pos - rayY, dir, Color.blue, 1.0f, false);
            Debug.DrawRay(pos + rayX, dir, Color.green, 1.0f, false);
            Debug.DrawRay(pos - rayX, dir, Color.green, 1.0f, false);
            RaycastHit2D hitCenter = Physics2D.Raycast(pos, dir, 2f);
            RaycastHit2D hitTop = Physics2D.Raycast(pos + rayY, dir, 1.5f);
            RaycastHit2D hitBottom = Physics2D.Raycast(pos - rayY, dir, 1.5f);
            RaycastHit2D hitRight = Physics2D.Raycast(pos + rayX, dir, 1.5f);
            RaycastHit2D hitLeft = Physics2D.Raycast(pos - rayX, dir, 1.5f);
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

        if (powerUpSpeedAtivo && (speed >= 0.5f && speed < 0.6f))
            speed += 0.7f;
        if (!powerUpSpeedAtivo && (speed >= 1.2f && speed < 1.3f))
            speed -= 0.7f;
        if (powerUpDestroyAtivo)
        {
            //Fly power up logic 
        }
    }

    public GameObject PowerUp;
    public int timer = 5;
    bool powerUpSpeedAtivo;
    bool powerUpDestroyAtivo;
    Coroutine cor;



    void OnTriggerEnter2D(Collider2D powerup)
    {
        if (powerup.name == "PowerUpSpeed")
        {
            Debug.Log("Power up de Velocidade adquirido!");
            if (cor != null)
            {
                Debug.Log("PowerUp Renovado!");
                StopCoroutine(cor);
                cor = null;
            }
            cor = StartCoroutine(SpeedPower());
        }
        
        if (powerup.name == "PowerUpDestroy")
        {
            if (cor != null)
            {
                StopCoroutine(cor);
                cor = null;
            }
            cor = StartCoroutine(DestroyPower());
        }

    }

    private IEnumerator SpeedPower()
    {
        powerUpSpeedAtivo = true;
        yield return new WaitForSecondsRealtime(timer);
        powerUpSpeedAtivo = false;
        Debug.Log("PowerUp de Velocidade acabou! (Tempo Excedido)");
    }

    private IEnumerator DestroyPower()
    {
        powerUpDestroyAtivo = true;
        yield return new WaitForSecondsRealtime(timer);
        powerUpDestroyAtivo = false;
    }

}