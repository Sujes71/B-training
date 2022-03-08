using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivarAreaUIPanel : MonoBehaviour
{
    public Image general, razonamiento, coordinacion, memoria, fluidezVerbal, atencion;
    public Text generalTxt, razonamientoTxt, coordinacionTxt, memoriaTxt, fluidezVerbalTxt, atencionTxt;
    public ProfileData perfil;
    void Start()
    {
        perfil = ProfileStorage.s_currentProfile;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<EstadísticasManager>().isNomal)
        {
            general.fillAmount = (perfil.general / 11) / 6;
            razonamiento.fillAmount = (perfil.razonamiento / 2) / 6;
            coordinacion.fillAmount = (perfil.coordinacion / 4) / 6;
            memoria.fillAmount = (perfil.memoria / 3) / 6;
            fluidezVerbal.fillAmount = (perfil.fluidezVerbal / 2) / 6;
            atencion.fillAmount = (perfil.atencion / 5) / 6;
        }
        else
        {
            general.fillAmount = (perfil.generalP / 11) / 6;
            razonamiento.fillAmount = (perfil.razonamientoP / 2) / 6;
            coordinacion.fillAmount = (perfil.coordinacionP / 4) / 6;
            memoria.fillAmount = (perfil.memoriaP / 3) / 6;
            fluidezVerbal.fillAmount = (perfil.fluidezVerbalP / 2) / 6;
            atencion.fillAmount = (perfil.atencionP / 5) / 6;
        }
        generalTxt.text = (general.fillAmount * 100).ToString("F0");
        razonamientoTxt.text = (razonamiento.fillAmount * 100).ToString("F0");
        coordinacionTxt.text = (coordinacion.fillAmount * 100).ToString("F0");
        memoriaTxt.text = (memoria.fillAmount * 100).ToString("F0");
        fluidezVerbalTxt.text = (fluidezVerbal.fillAmount * 100).ToString("F0");
        atencionTxt.text = (atencion.fillAmount * 100).ToString("F0");
    }
}
