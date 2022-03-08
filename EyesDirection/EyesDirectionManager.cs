using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EyesDirectionManager : MonoBehaviour
{
    public GameObject[] caras;
    public Vector2[] posiciones;
    public GameObject timer, clon, arriba, abajo, izquierda, derecha, final, vida1, vida2, vida3, panel, centro, timer2;
    private GameObject copiaCara, copiaTimer, timer2Clone;
    public TextMeshProUGUI puntos, nivelText;
    public float tiempoInterval = 3, tiempoRespuesta = 6, tiempoNivel = 3, tiempoPanel = 2, tiempoFinal = 5;
    public int vidas = 3, nivel, score, face, pos;
    public bool faceIsInstantiate = false, timerInstantiate = false, panelRojo = false, panelVerde = false, pulsado = false, timer2Instantiate = false;
    public static bool EyesDirection;
    // Start is called before the first frame update
    void Start()
    {
        nivel = 1;
        EyesDirection = true;
        DesactivarBotones();
    }

    // Update is called once per frame
    void Update()
    {
        if (panelRojo)
            RedScreen();
        if (panelVerde)
            GreenScreen();
        if (vidas > 0)
        {
            if (centro.activeInHierarchy)
            {
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                    ComprobarArriba();
                }
                else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    ComprobarDerecha();
                }
                else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    ComprobarIzquierda();
                }
                else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    ComprobarAbajo();
                }
                else if (Input.GetKey(KeyCode.Space))
                {
                    ComprobarCentro();
                }
            }
            if (tiempoNivel >= 0)
            {
                nivelText.gameObject.SetActive(true);
                tiempoNivel -= Time.deltaTime;
            }
            else
            {
                nivelText.gameObject.SetActive(false);
                Ronda();
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
                    if (ProfileStorage.s_currentProfile.eyesScoreP < score)
                    {
                        ProfileStorage.StoragePlayerProfile("eyes", score, nivel);
                    }
                        
                }
                else
                {
                    if (ProfileStorage.s_currentProfile.eyesScore < score)
                    {
                        ProfileStorage.StoragePlayerProfile("eyes", score, nivel);
                    }
                }
                EyesDirection = false;
                Debug.Log("Juego Terminado");
                SceneManager.LoadScene("JuegoEyes");
            }
        }
    }
    void Ronda()
    {
        if (tiempoInterval >= 0)
        {
            tiempoInterval -= Time.deltaTime;
            if (!timerInstantiate)
            {
                timerInstantiate = true;
                DesactivarBotones();
                copiaTimer = Instantiate(timer);
                copiaTimer.transform.parent = clon.transform;
                copiaTimer.transform.localPosition = timer.transform.position;
            }
        }
        else
        {
            if (!faceIsInstantiate)
            {
                Destroy(copiaTimer);
                pos = Random.Range(0, 3);
                if (JugarManager.toggleValue)
                {
                    face = Random.Range(0, 5);
                }
                else
                {
                    if (nivel <= 3)
                        face = Random.Range(0, 3);
                    else
                        face = Random.Range(0, 5);
                }
                copiaCara = Instantiate(caras[face], posiciones[pos], Quaternion.identity);
                faceIsInstantiate = true;
                timerInstantiate = false;
                ActivarBotones();
            }
            if (JugarManager.toggleValue)
            {
                if (tiempoRespuesta >= 0)
                {
                    tiempoRespuesta -= Time.deltaTime;
                    if (!timer2Instantiate)
                    {
                        timer2Instantiate = true;
                        timer2Clone = Instantiate(timer2);
                        timer2Clone.transform.parent = clon.transform;
                        timer2Clone.transform.localPosition = new Vector2(802, 400);
                    }
                }
                else
                {
                    timer2Instantiate = false;
                    Destroy(timer2Clone);
                    Miss();
                    TimeController();
                    Reset();
                }
            }
        }
    }
    public void ComprobarIzquierda()
    {
        if (face == 2)
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
        if (JugarManager.toggleValue)
            TimeController();
        Reset();
    }
    public void ComprobarDerecha()
    {
        if (face == 1)
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
        if (JugarManager.toggleValue)
            TimeController();
        Reset();
    }
    public void ComprobarArriba()
    {
        if (face == 3)
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
        if (JugarManager.toggleValue)
            TimeController();
        Reset();
    }
    public void ComprobarAbajo()
    {
        if (face == 4)
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
        if (JugarManager.toggleValue)
            TimeController();
        Reset();
    }
    public void ComprobarCentro()
    {
        if (face == 0)
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
        if (JugarManager.toggleValue)
            TimeController();
        Reset();
    }
    public void Reset()
    {
        tiempoInterval = 3;
        timer2Instantiate = false;
        faceIsInstantiate = false;
        Destroy(copiaCara);
        Destroy(timer2Clone);
        DesactivarBotones();
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
    void GreenScreen()
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
    void DesactivarBotones()
    {
        arriba.SetActive(false);
        abajo.SetActive(false);
        derecha.SetActive(false);
        izquierda.SetActive(false);
        centro.SetActive(false);
    }
    void ActivarBotones()
    {
        if (JugarManager.toggleValue)
        {
            arriba.SetActive(true);
            abajo.SetActive(true);
            centro.SetActive(true);
            derecha.SetActive(true);
            izquierda.SetActive(true);
        }
        else
        {
            if (nivel <= 3)
            {
                derecha.SetActive(true);
                izquierda.SetActive(true);
                centro.SetActive(true);
            }
            else
            {
                arriba.SetActive(true);
                abajo.SetActive(true);
                centro.SetActive(true);
                derecha.SetActive(true);
                izquierda.SetActive(true);
            }
        }
    }
}