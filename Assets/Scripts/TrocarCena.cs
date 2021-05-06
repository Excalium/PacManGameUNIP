using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocarCena : MonoBehaviour
{

    public string nomeCena;

    public void MudarCena() {
        StartCoroutine("AbrirCena");
    }

    private IEnumerator AbrirCena()
    {
        SceneManager.LoadScene(nomeCena);
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(nomeCena));
    }

    public void SairJogo() {
        Application.Quit();
    }

}
