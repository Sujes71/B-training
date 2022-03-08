using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EstadísticasManager : MonoBehaviour
{
    public Text perfil;
    public Button normal, pesadilla;
    public bool isNomal;
    private void Start()
    {
        perfil.text = ProfileStorage.s_currentProfile.name;
        pesadilla.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f);
        isNomal = true;
    }
    public void BotonHome()
    {
        SceneManager.LoadScene("Menu_Principal");
    }
    public void Normal()
    {
        normal.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f);
        pesadilla.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f);
        isNomal = true;
    }
    public void Pesadilla()
    {
        pesadilla.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f);
        normal.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f);
        isNomal = false;
    }
}
