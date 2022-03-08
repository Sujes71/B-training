using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScenaryManager : MonoBehaviour
{
    public GameObject ScenaryRespawn;
    public GameObject ScenaryTeleport;
    public float speed, tiempoNivel;
    public TextMeshProUGUI nivel;
    private bool tiempo = false;
    public int level = 1;
    // Update is called once per frame
    void Update()
    {
        ControlEscenario();
    }
    void ControlEscenario()
    {
        if (CatchBatQueenManager.gameTime >= 250)
        {
            if (!tiempo)
            {
                nivel.gameObject.SetActive(true);
                tiempoNivel = 3;
                tiempo = true;
            }
            if(tiempoNivel >= 0)
            {
                tiempoNivel -= Time.deltaTime;
            }
            else
                nivel.gameObject.SetActive(false);

            gameObject.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else if (CatchBatQueenManager.gameTime < 250 && CatchBatQueenManager.gameTime >= 200)
        {
            if (tiempo)
            {
                level++;
                nivel.text = "Nivel 2";
                nivel.gameObject.SetActive(true);
                tiempoNivel = 3;
                tiempo = false;
            }
            if (tiempoNivel >= 0)
            {
                tiempoNivel -= Time.deltaTime;
            }
            else
                nivel.gameObject.SetActive(false);

            gameObject.transform.position -= new Vector3((speed + 1.5f) * Time.deltaTime, 0, 0);
        }
        else if (CatchBatQueenManager.gameTime < 200 && CatchBatQueenManager.gameTime >= 150)
        {
            if (!tiempo)
            {
                level++;
                nivel.text = "Nivel 3";
                nivel.gameObject.SetActive(true);
                tiempoNivel = 3;
                tiempo = true;
            }
            if (tiempoNivel >= 0)
            {
                tiempoNivel -= Time.deltaTime;
            }
            else
                nivel.gameObject.SetActive(false);

            gameObject.transform.position -= new Vector3((speed + 3) * Time.deltaTime, 0, 0);
        }
            
        else if (CatchBatQueenManager.gameTime < 150 && CatchBatQueenManager.gameTime >= 100)
        {
            if (tiempo)
            {
                level++;
                nivel.text = "Nivel 4";
                nivel.gameObject.SetActive(true);
                tiempoNivel = 3;
                tiempo = false;
            }
            if (tiempoNivel >= 0)
            {
                tiempoNivel -= Time.deltaTime;
            }
            else
                nivel.gameObject.SetActive(false);

            gameObject.transform.position -= new Vector3((speed + 9) * Time.deltaTime, 0, 0);

            gameObject.transform.position -= new Vector3((speed + 6) * Time.deltaTime, 0, 0);
        }
            
        else if (CatchBatQueenManager.gameTime < 100 && CatchBatQueenManager.gameTime >= 50)
        {
            if (!tiempo)
            {
                level++;
                nivel.text = "Nivel 5";
                nivel.gameObject.SetActive(true);
                tiempoNivel = 3;
                tiempo = true;
            }
            if (tiempoNivel >= 0)
            {
                tiempoNivel -= Time.deltaTime;
            }
            else
                nivel.gameObject.SetActive(false);

            gameObject.transform.position -= new Vector3((speed + 9) * Time.deltaTime, 0, 0);
        }

        else
        {
            if (tiempo)
            {
                level++;
                nivel.text = "Nivel 6";
                nivel.gameObject.SetActive(true);
                tiempoNivel = 3;
                tiempo = false;
            }
            if (tiempoNivel >= 0)
            {
                tiempoNivel -= Time.deltaTime;
            }
            else
                nivel.gameObject.SetActive(false);

            gameObject.transform.position -= new Vector3((speed + 12) * Time.deltaTime, 0, 0);
        }

        if (gameObject.transform.position.x <= ScenaryTeleport.transform.position.x)
        {
            gameObject.transform.position = ScenaryRespawn.transform.position;
        }
    }
}
