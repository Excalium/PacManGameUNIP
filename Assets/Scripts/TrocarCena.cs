using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocarCena : MonoBehaviour
{

    public string nomeCena;

    public void MudarCena() {
        SceneManager.LoadScene(nomeCena);
    }

    public void SairJogo() {
        Application.Quit();
    }

}
