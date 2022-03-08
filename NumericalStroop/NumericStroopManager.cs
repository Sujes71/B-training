using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NumericStroopManager : MonoBehaviour
{
    // Start is called before the first frame update
    public CartaBase c1;
    public CartaBase c2;
    public GameObject vida1, vida2, vida3, final, panel, timer, clon;
    private GameObject copiaTimer, timer2Clone;
    public TextMeshProUGUI nivelText, puntos;
    private bool generarNivel, panelVerde, panelRojo;
    public int[] listaNumeros;
    public int limite, vidas, score, nivel;
    public static bool numericStroop;
    public bool timerInstantiate, timer2Instantiate;
    public float tiempoNivel, tiempoRespuesta, tiempoFinal, tiempoPanel, tiempoInterval;
    void Start()
    {
        panelVerde = false;
        panelRojo = false;
        timer2Instantiate = false;
        timerInstantiate = false;
        tiempoInterval = 3;
        tiempoPanel = 2;
        tiempoFinal = 5;
        tiempoNivel = 3;
        tiempoRespuesta = 6;
        vidas = 3;
        numericStroop = true;
        nivel = 1;
        generarNivel = true;
        limite = listaNumeros.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (panelRojo)
            RedScreen();
        if (panelVerde)
            GreenScreen();
        if(vidas > 0)
        {
            if(tiempoNivel >= 0)
            {
                nivelText.gameObject.SetActive(true);
                tiempoNivel -= Time.deltaTime;
            }
            else
            {
                if (tiempoInterval >= 0)
                {
                    tiempoInterval -= Time.deltaTime;
                    if (!timerInstantiate)
                    {
                        nivelText.gameObject.SetActive(false);
                        copiaTimer = Instantiate(timer);
                        copiaTimer.transform.parent = clon.transform;
                        copiaTimer.transform.localPosition = timer.transform.position;
                        timerInstantiate = true;
                    }
                }
                else
                {
                    if (generarNivel)
                    {
                        c1.GetComponent<Button>().enabled = true;
                        c2.GetComponent<Button>().enabled = true;
                        Destroy(copiaTimer);
                        elegirEscala();
                        c1.generar = true;
                        c2.generar = true;
                        generarNivel = false;
                        limite = listaNumeros.Length;
                    }
                    if (JugarManager.toggleValue)
                    {
                        if (tiempoRespuesta >= 0)
                        {
                            tiempoRespuesta -= Time.deltaTime;
                            if (!timer2Instantiate && copiaTimer == null)
                            {
                                Destroy(copiaTimer);
                                timer2Instantiate = true;
                                timer2Clone = Instantiate(timer);
                                timer2Clone.transform.parent = clon.transform;
                                timer2Clone.transform.localPosition = timer.transform.position;
                            }
                        }
                        else
                        {
                            timer2Instantiate = false;
                            Destroy(timer2Clone);
                            Miss();
                            c1.eliminarElementos();
                            c2.eliminarElementos();
                            tiempoInterval = 3;
                            timerInstantiate = false;
                            generarNivel = true;
                            TimeController();
                        }
                    }
                }
            }
        }
        else
        {
            final.SetActive(true);
            if (tiempoFinal >= 0)
                tiempoFinal -= Time.deltaTime;
            else
            {
                if (JugarManager.toggleValue)
                {
                    if (ProfileStorage.s_currentProfile.stroopScoreP < score)
                    {
                        ProfileStorage.StoragePlayerProfile("stroop", score, nivel);
                    }    
                }
                else
                {
                    if (ProfileStorage.s_currentProfile.stroopScore < score)
                    {
                        ProfileStorage.StoragePlayerProfile("stroop", score, nivel);
                    } 
                }
                numericStroop = false;
                Debug.Log("Juego Terminado");
                SceneManager.LoadScene("JuegoStroop");
            }
        }
    }
    public void elegirEscala()
    {
        float random = Random.Range(1, 11);
        if (random <= 5)
        {
            c1.escala = 0.4f;
            c2.escala = 0.3f;
        }
        else {
            c2.escala = 0.4f;
            c1.escala = 0.3f;
        }

    }
    public void compararC1conC2()
    {
        if (c1.generar == false)
        {
            if (c1.valor >= c2.valor)
            {
                panelVerde = true;
                score++;
                puntos.text = "Score: " + score;
                LevelSystem();
            }
            else
            {
                Miss();
            }
            c1.eliminarElementos();
            c2.eliminarElementos();
        }
        if (JugarManager.toggleValue)
        {
            Destroy(timer2Clone);
            TimeController();
            timer2Instantiate = false;
        }
        c2.GetComponent<Button>().enabled = false;
        c1.GetComponent<Button>().enabled = false;
        tiempoInterval = 3;
        timerInstantiate = false;
        generarNivel = true;
    }
    public void compararC2conC1()
    {
        if (c2.generar == false)
        {
            if (c2.valor >= c1.valor)
            {
                panelVerde = true;
                score++;
                puntos.text = "Score: " + score;
                LevelSystem();
            }
            else
            {
                Miss();
            }
            c1.eliminarElementos();
            c2.eliminarElementos();
        }
        if (JugarManager.toggleValue)
        {
            Destroy(timer2Clone);
            TimeController();
            timer2Instantiate = false;
        }
        c2.GetComponent<Button>().enabled = false;
        c1.GetComponent<Button>().enabled = false;
        tiempoInterval = 3;
        timerInstantiate = false;
        generarNivel = true;
    }
    void Miss()
    {
        switch (vidas)
        {
            case 3:
                Destroy(vida1);
                break;
            case 2:
                Destroy(vida2);
                break;
            case 1:
                Destroy(vida3);
                break;
        }
        panelRojo = true;
        if (score > 0)
        {
            if (score == 5 || score == 10 || score == 15 || score == 20 || score == 25)
                nivel--;
            score--;
            puntos.text = "Score: " + score;
        }
        vidas--;
    }
    void LevelSystem()
    {
        switch (score)
        {
            case 6:
                tiempoNivel = 3;
                nivel++;
                nivelText.text = "Nivel 2";
                break;
            case 12:
                tiempoNivel = 3;
                nivel++;
                nivelText.text = "Nivel 3";
                break;
            case 18:
                tiempoNivel = 3;
                nivel++;
                nivelText.text = "Nivel 4";
                break;
            case 24:
                tiempoNivel = 3;
                nivel++;
                nivelText.text = "Nivel 5";
                break;
            case 30:
                tiempoNivel = 3;
                nivel++;
                nivelText.text = "Nivel 6";
                break;
        }
    }
    void TimeController()
    {
        if (score < 5)
        {
            tiempoRespuesta = 6;
        }
        else if (score >= 5 && score < 10)
        {
            tiempoRespuesta = 5;
        }
        else if (score >= 10 && score < 15)
        {
            tiempoRespuesta = 4;
        }
        else if (score >= 15 && score < 20)
        {
            tiempoRespuesta = 3;
        }
        else if (score >= 20 && score < 25)
        {
            tiempoRespuesta = 2;
        }
        else if (score >= 25)
        {
            tiempoRespuesta = 1;
        }
    }
    void RedScreen()
    {
        tiempoPanel -= Time.deltaTime;
        if (tiempoPanel >= 1.5f)
            panel.GetComponent<Image>().color = new Color32(255, 0, 40, 100);
        else if (tiempoPanel <= 1.5 && tiempoPanel > 1)
            panel.GetComponent<Image>().color = new Color32(255, 0, 40, 80);
        else if (tiempoPanel <= 1 && tiempoPanel > 0.5)
            panel.GetComponent<Image>().color = new Color32(255, 0, 40, 50);
        else if (tiempoPanel <= 0.5 && tiempoPanel > 0)
            panel.GetComponent<Image>().color = new Color32(255, 0, 40, 20);
        else
        {
            panel.GetComponent<Image>().color = new Color32(255, 0, 40, 0);
            panelRojo = false;
            tiempoPanel = 2;
        }
    }
    public void GreenScreen()
    {
        tiempoPanel -= Time.deltaTime;
        if (tiempoPanel >= 1.5)
            panel.GetComponent<Image>().color = new Color32(0, 255, 20, 100);
        else if (tiempoPanel <= 1.5 && tiempoPanel > 1)
            panel.GetComponent<Image>().color = new Color32(0, 255, 20, 80);
        else if (tiempoPanel <= 1 && tiempoPanel > 0.5)
            panel.GetComponent<Image>().color = new Color32(0, 255, 20, 50);
        else if (tiempoPanel <= 0.5 && tiempoPanel > 0)
            panel.GetComponent<Image>().color = new Color32(0, 255, 20, 20);
        else
        {
            panel.GetComponent<Image>().color = new Color32(0, 255, 20, 0);
            panelVerde = false;
            tiempoPanel = 2;
        }
    }
}
