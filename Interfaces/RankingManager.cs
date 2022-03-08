using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    public int numero;
    public GameObject rankingPanel, Normalbtn, Pesadillabtn;
    public bool normal;
    public Button queen, jack, fly, fly2, forest, portraits, portraits2, eye, vampire, stroop, cementery;
    public Text perfil;
    private void Awake()
    {
        perfil.text = ProfileStorage.s_currentProfile.name;
        normal = true;
        Pesadillabtn.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f);
        numero = 1;
        queen.Select();
    }
    public void BotonHome()
    {
        SceneManager.LoadScene("Menu_Principal");
    }
    public void Normal()
    {
        normal = true;
        Pesadillabtn.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f);
        Normalbtn.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f);

        if (numero == 1)
        {
            Queen();
            queen.Select();
        }
        else if (numero == 2)
        {
            Jack();
            jack.Select();
        }
        else if (numero == 3)
        {
            Fly();
            fly.Select();
        }
        else if (numero == 4)
        {
            Fly2();
            fly2.Select();
        }
        else if (numero == 5)
        {
            Forest();
            forest.Select();
        }
        else if (numero == 6)
        {
            Portraits();
            portraits.Select();
        }
        else if (numero == 7)
        {
            Portraits2();
            portraits2.Select();
        }
        else if (numero == 8)
        {
            Eye();
            eye.Select();
        }
        else if (numero == 9)
        {
            Vampire();
            vampire.Select();
        }
        else if (numero == 10)
        {
            Stroop();
            stroop.Select();
        }
        else if (numero == 11)
        {
            Cementery();
            cementery.Select();
        }
    }
    public void Pesadilla()
    {
        normal = false;
        Pesadillabtn.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f);
        Normalbtn.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f);

        if (numero == 1)
        {
            Queen();
            queen.Select();
        }
        else if (numero == 2)
        {
            Jack();
            jack.Select();
        }
        else if (numero == 3)
        {
            Fly();
            fly.Select();
        }
        else if (numero == 4)
        {
            Fly2();
            fly2.Select();
        }
        else if (numero == 5)
        {
            Forest();
            forest.Select();
        }
        else if (numero == 6)
        {
            Portraits();
            portraits.Select();
        }
        else if (numero == 7)
        {
            Portraits2();
            portraits2.Select();
        }
        else if (numero == 8)
        {
            Eye();
            eye.Select();
        }
        else if (numero == 9)
        {
            Vampire();
            vampire.Select();
        }
        else if (numero == 10)
        {
            Stroop();
            stroop.Select();
        }
        else if (numero == 11)
        {
            Cementery();
            cementery.Select();
        }
    }
    public void Queen()
    {
        numero = 1;
        Reset();
    }
    public void Jack()
    {
        numero = 2;
        Reset();
    }
    public void Fly()
    {
        numero = 3;
        Reset();
    }
    public void Fly2()
    {
        numero = 4;
        Reset();
    }
    public void Forest()
    {
        numero = 5;
        Reset();
    }
    public void Portraits()
    {
        numero = 6;
        Reset();
    }
    public void Portraits2()
    {
        numero = 7;
        Reset();
    }
    public void Eye()
    {
        numero = 8;
        Reset();
    }
    public void Vampire()
    {
        numero = 9;
        Reset();
    }
    public void Stroop()
    {
        numero = 10;
        Reset();
    }
    public void Cementery()
    {
        numero = 11;
        Reset();
    }
    void ClearChildren()
    {
        foreach (Transform child in rankingPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }
    private void Reset()
    {
        FindObjectOfType<RankingList>().i = 0;
        ClearChildren();
        FindObjectOfType<RankingList>().listaPerfilesOrdenada.Clear();
        FindObjectOfType<RankingList>().finalizado = false;
    }
}
