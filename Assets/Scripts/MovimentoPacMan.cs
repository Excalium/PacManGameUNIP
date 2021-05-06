using UnityEngine;
using System.Collections;


public class MovimentoPacMan : MonoBehaviour
{

    //Variáveis para tratamento de Raycasts e movimentação

    //Movimentação
    Vector2 vecDireita = new Vector2(4, 0);
    Vector2 vecEsquerda = new Vector2(-4, 0);
    Vector2 vecCima = new Vector2(0, 4);
    Vector2 vecBaixo = new Vector2(0, -4);

    //Raycast
    Vector2 rayX = new Vector2(1.5f, 0);
    Vector2 rayY = new Vector2(0, 1.5f);

    //Variáveis gerais
    public float speed = 0.34f;
    Vector2 dest = Vector2.zero;
    void Start()
    {
        // Pega a posição inicial para dar início ao jogo
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

        //Realiza a animação do PacMan para virar de acordo com a direção
        GetComponent<Animator>().SetFloat("DirX", dire.x);
        GetComponent<Animator>().SetFloat("DirY", dire.y);

        bool valid(Vector2 dir)
        {
            Vector2 pos = transform.position;

            //DrawRay é utilizado para debugar o código, pois desenha os RayCasts.
            Debug.DrawRay(pos, dir, Color.red, 1.0f, false);
            Debug.DrawRay(pos + rayY, dir, Color.blue, 1.0f, false);
            Debug.DrawRay(pos - rayY, dir, Color.blue, 1.0f, false);
            Debug.DrawRay(pos + rayX, dir, Color.green, 1.0f, false);
            Debug.DrawRay(pos - rayX, dir, Color.green, 1.0f, false);

            //Raycast é utilizado para bloquear a movimentação do jogador (PacMan)
            //caso encontre uma parede (collider)

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
    }

    //Variáveis para PowerUps
    int timer = 10;
    public bool powerUpSpeedAtivo;
    public bool powerUpDestroyAtivo;
    Coroutine corSpeed;
    Coroutine corDestroy;


    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.name == "PowerUpSpeed")
        {
            if (corSpeed != null && powerUpSpeedAtivo)
            {
                StopCoroutine(corSpeed);
                corSpeed = null;
            }
            corSpeed = StartCoroutine(SpeedPower());
        }

        if (outro.name == "PowerUpDestroy")
        {
            if (corDestroy != null && powerUpDestroyAtivo)
            {
                StopCoroutine(corDestroy);
                corDestroy = null;
            }
            corDestroy = StartCoroutine(DestroyPower());
        }
    }

    private IEnumerator SpeedPower()
    {
        powerUpSpeedAtivo = true;
        yield return new WaitForSecondsRealtime(timer);
        powerUpSpeedAtivo = false;
    }

    private IEnumerator DestroyPower()
    {
        powerUpDestroyAtivo = true;
        StartCoroutine(Piscar());
        yield return new WaitForSecondsRealtime(timer);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private IEnumerator Piscar()
    {
        for (int i = 0; i < timer * 2; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSecondsRealtime(0.25f);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSecondsRealtime(0.25f);
        }
        powerUpDestroyAtivo = false;
    }

}