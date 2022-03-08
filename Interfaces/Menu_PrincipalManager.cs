using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_PrincipalManager : MonoBehaviour
{
    public Text perfil;
    private void Start()
    {
        perfil.text = ProfileStorage.s_currentProfile.name;
    }
    public void BotonJugar()
    {
        SceneManager.LoadScene("JuegoQueen");
    }
    public void BotonEstadísticas()
    {
        SceneManager.LoadScene("Estadísticas");
    }
    public void BotonSalir()
    {
        SceneManager.LoadScene("NuevoPerfil");
    }
    public void BotonRanking()
    {
        SceneManager.LoadScene("Ranking");
    }
}
