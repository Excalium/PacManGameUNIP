using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    static public int pontos;
    static public int contDots;

    void FixedUpdate()
    {
        
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Nivel 1")
        {
            if (Score.contDots == 358) //358
            {
                SceneManager.LoadScene("Nivel concluido");
                Score.contDots = 0;
            }
        }
        if (scene.name == "Nivel 2")
        {
            if (Score.contDots == 364) //364
            {
                SceneManager.LoadScene("Vitoria");
                Score.contDots = 0;
            }
        }
    }

}
