using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IntroducciónManager : MonoBehaviour
{
    public void BotonIniciarTest()
    {
        SceneManager.LoadScene("JuegoQueen");
    }
    public void BotonAtras()
    {
        SceneManager.LoadScene("Registro");
    }
}
