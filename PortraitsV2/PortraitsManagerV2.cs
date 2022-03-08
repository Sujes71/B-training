using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortraitsManagerV2 : MonoBehaviour
{

    public GameObject[] baraja;
    private GameObject[] copiaBaraja;
    public GameObject timer, panel, vida1, vida2, vida3, final, clon, cartaBase, timer2;
    public TextMeshProUGUI Puntos, nivelText;
    public Vector3[] localizaciones;
    public Vector3 zonaCero;
    GameObject cartaMomoria, timeClone, timer2Clone;
    public int nivel;
    public int score, vidas = 3;
    bool turnar, panelRojo = false, panelVerde = false, generado = false;
    public static bool PortraitsV2;
    private float tiempoPanel = 2, tiempoNivel = 3, tiempoFinal = 5;
    public float tiempoIntervalo = 5, tiempoRespuesta = 6;
    public bool timerInstantiate = false, timer2Instantiate = false;
    // Start is called before the first frame update
    void Start()
    {
        PortraitsV2 = true;
        nivel = 1;
        score = 0;
        turnar = true;
        copiaBaraja = copiar();
    }

    // Update is called once per frame
    void Update()
    {
        if (panelVerde)
            GreenScreen();
        if (panelRojo)
            RedScreen();
        if(vidas > 0)
        {
            if(tiempoNivel >= 0)
            {
                nivelText.gameObject.SetActive(true);
                tiempoNivel -= Time.deltaTime;
            }
            else
            {
                if (tiempoIntervalo >= 0)
                {
                    nivelText.gameObject.SetActive(false);
                    tiempoIntervalo -= Time.deltaTime;
                    if (!timerInstantiate)
                    {
                        timerInstantiate = true;
                        timeClone = Instantiate(timer);
                        timeClone.transform.parent = clon.transform;
                        timeClone.transform.localPosition = new Vector2(0,0);
                    }
                    if (!generado)
                    {
                        Shuffle();
                        int cogido = Random.Range(0, nivel);
                        cartaMomoria = Instantiate(baraja[cogido], localizaciones[0], Quaternion.identity);
                        cartaMomoria.transform.parent = clon.transform;
                        cartaMomoria.transform.localPosition = localizaciones[0];
                        cartaMomoria.GetComponent<Button>().enabled = false;
                        cartaBase.SetActive(false);
                        generado = true;
                    }
                }
                else
                {
                    if (turnar && nivel <= 6)
                    {
                        if (timerInstantiate)
                        {
                            Destroy(timeClone);
                            timerInstantiate = false;
                        }
                        cartaBase.SetActive(true);
                        turnar = false;
                        Mostrar_cartas();
                        FindObjectOfType<Temporizador>().tiempo = 5;  
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
                                timer2Clone.transform.localPosition = new Vector2(543, -188);
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
                    if (ProfileStorage.s_currentProfile.portraits2ScoreP < score)
                    {
                        ProfileStorage.StoragePlayerProfile("portraits2", score, nivel);
                    }   
                }
                else
                {
                    if (ProfileStorage.s_currentProfile.portraits2Score < score)
                    {
                        ProfileStorage.StoragePlayerProfile("portraits2", score, nivel);
                    }
                }
                PortraitsV2 = false;
                Debug.Log("Juego Terminado");
                SceneManager.LoadScene("JuegoRetratosv2");
            }
        }
    }
    public void Reset()
    {
        for(int i = 0; i < baraja.Length; i++)
        {
            baraja[i].transform.localPosition = zonaCero;
        }
        if (!JugarManager.toggleValue)
            tiempoIntervalo = 5;
            
        generado = false;
        turnar = true;
        timer2Instantiate = false;
        Destroy(timer2Clone);
        Destroy(cartaMomoria);
    }
    private GameObject[] copiar()
    {
        GameObject[] array = new GameObject[baraja.Length];
        for(int i = 0; i < baraja.Length; i++)
        {
            array[i] = baraja[i];
        }
        return array;
    }
    public void Shuffle()
    {
        for(int i = 0; i < 10; i++)
        {
            int c = Random.Range(0,baraja.Length);
            int c1= Random.Range(0, baraja.Length);
            GameObject aux = baraja[c];
            baraja[c] = baraja[c1];
            baraja[c1] = aux;
        }
    }

    public void Mostrar_cartas()
    {
        for(int i=0; i <= nivel; i++)
        {
            baraja[i].transform.localPosition = localizaciones[i + 1];
        }
    }
    public void ComprobarCorrecto1()
    {
        if (copiaBaraja[0].tag.Equals(cartaMomoria.tag))
        {
            panelVerde = true;
            score ++;
            Puntos.text = "Score: " + score;
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
    public void ComprobarCorrecto2()
    {
        if (copiaBaraja[1].tag.Equals(cartaMomoria.tag))
        {
            panelVerde = true;
            score++;
            Puntos.text = "Score: " + score;
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
    public void ComprobarCorrecto3()
    {
        if (copiaBaraja[2].tag.Equals(cartaMomoria.tag))
        {
            panelVerde = true;
            score++;
            Puntos.text = "Score: " + score;
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
    public void ComprobarCorrecto4()
    {
        if (copiaBaraja[3].tag.Equals(cartaMomoria.tag))
        {
            panelVerde = true;
            score++;
            Puntos.text = "Score: " + score;
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
    public void ComprobarCorrecto5()
    {
        if (copiaBaraja[4].tag.Equals(cartaMomoria.tag))
        {
            panelVerde = true;
            score++;
            Puntos.text = "Score: " + score;
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
    public void ComprobarCorrecto6()
    {
        if (copiaBaraja[5].tag.Equals(cartaMomoria.tag))
        {
            panelVerde = true;
            score++;
            Puntos.text = "Score: " + score;
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
    public void ComprobarCorrecto7()
    {
        if (copiaBaraja[6].tag.Equals(cartaMomoria.tag))
        {
            panelVerde = true;
            score++;
            Puntos.text = "Score: " + score;
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
            if (score == 5 || score == 10 ||score == 15 || score == 20 || score == 25)
                nivel--;

            score--;
            Puntos.text = "Score: " + score;
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
            tiempoIntervalo = 5;
            tiempoRespuesta = 6;
        }
        else if (score >= 5 && score < 10)
        {
            tiempoIntervalo = 4.5f;
            tiempoRespuesta = 5;
        }
        else if (score >= 10 && score < 15)
        {
            tiempoIntervalo = 4;
            tiempoRespuesta = 4;
        }
        else if (score >= 15 && score < 20)
        {
            tiempoIntervalo = 3.5f;
            tiempoRespuesta = 3;
        }
        else if (score >= 20 && score < 25)
        {
            tiempoIntervalo = 3;
            tiempoRespuesta = 2;
        }
        else if (score >= 25)
        {
            tiempoIntervalo = 2;
            tiempoRespuesta = 1;
        }
    }
}
