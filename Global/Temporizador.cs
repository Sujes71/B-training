using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Temporizador : MonoBehaviour
{
    public TextMeshProUGUI contador;
    public float tiempo;
    bool timevalue = false;
    // Start is called before the first frame update
    void Start()
    {
        if (JugarManager.toggleValue)
        {
            if (JackManager.JacksCastle)
                tiempo = 3;
        }
        else
        {
            tiempo = 5;
        }
        if (PortraitsManagerV2.PortraitsV2)
        {
            if (FindObjectOfType<PortraitsManagerV2>().timerInstantiate)
            {
                tiempo = FindObjectOfType<PortraitsManagerV2>().tiempoIntervalo;
            }
                
            if (FindObjectOfType<PortraitsManagerV2>().timer2Instantiate)
            {
                tiempo = FindObjectOfType<PortraitsManagerV2>().tiempoRespuesta;
            } 
        }
        if (PortraitsManager.Portraits)
        {
            if (FindObjectOfType<PortraitsManager>().timerInstantiate)
            {
                tiempo = FindObjectOfType<PortraitsManager>().tiempoIntervalo;
            }
                
            if (FindObjectOfType<PortraitsManager>().timer2Instantiate)
            {
                tiempo = FindObjectOfType<PortraitsManager>().tiempoRespuesta;
            }
                
        }
        if (EyesDirectionManager.EyesDirection)
        {
            if (FindObjectOfType<EyesDirectionManager>().timerInstantiate)
            {
                tiempo = FindObjectOfType<EyesDirectionManager>().tiempoInterval;
            }

            if (FindObjectOfType<EyesDirectionManager>().timer2Instantiate)
            {
                tiempo = FindObjectOfType<EyesDirectionManager>().tiempoRespuesta;
            }
        }
        if (UpdateManager.UpdateGame)
        {
            if (FindObjectOfType<UpdateManager>().timerInstantiate)
            {
                tiempo = FindObjectOfType<UpdateManager>().tiempoInterval;
            }
        }
        if (NumericStroopManager.numericStroop)
        {
            if (FindObjectOfType<NumericStroopManager>().timerInstantiate)
            {
                tiempo = FindObjectOfType<NumericStroopManager>().tiempoInterval;
            }
            if (FindObjectOfType<NumericStroopManager>().timer2Instantiate)
            {
                tiempo = FindObjectOfType<NumericStroopManager>().tiempoRespuesta;
            }
        }

        contador.text = " " + tiempo;
    }

    // Update is called once per frame
    void Update()
    {
        tiempo -= Time.deltaTime;
        if(tiempo > 0)
        {
            contador.text = " " + tiempo.ToString("f0");
        }
        else
        {
            if (!timevalue)
                tiempo = 5;
             contador.text = " " + tiempo.ToString("f0");
        }
    }
}
