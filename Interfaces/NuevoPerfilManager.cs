using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NuevoPerfilManager : MonoBehaviour
{
    public void BotonNuevoPerfil()
    {
        SceneManager.LoadScene("Registro");
    }
    public void BotonSalir()
    {
        Debug.Log("Aplicación cerrada");
        Application.Quit();
    }
}
