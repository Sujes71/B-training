using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CartelController : MonoBehaviour
{
    private Animator anim;
    private Queue<string> colaDialogos;
    Textos texto;
    public static bool finalize = false;
    [SerializeField] TextMeshProUGUI textoPantalla;
    // Start is called before the first frame update
    void Start()
    {
        texto = new Textos();
        colaDialogos = new Queue<string>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void ActivarCartel(Textos textoObjeto)
    {
        FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Desafio", true);
        anim.SetBool("Cartel", true);
        texto = textoObjeto;
    }
    public void ActivaTexto()
    {
        colaDialogos.Clear();
        foreach (string textoGuardar in texto.arrayTextos)
        {
            colaDialogos.Enqueue(textoGuardar);
        }
        SiguienteFrase();
    }
    public void SiguienteFrase()
    {
        if(colaDialogos.Count == 0)
        {
            anim.SetBool("Cartel", false);
            FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Desafio", false);
            textoPantalla.text = "";
            finalize = true;
        }
        else
        {
            string fraseActual = colaDialogos.Dequeue();
            textoPantalla.text = fraseActual;
            
        }
    }
}
