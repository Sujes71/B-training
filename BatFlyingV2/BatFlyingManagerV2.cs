using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BatFlyingManagerV2 : MonoBehaviour
{
    public GameObject[] murcielagos;
    public GameObject[] keys;
    public GameObject paredAbajo, paredArriba, paredDerecha, paredIzquierda, vida1, vida2, vida3, arriba, abajo, derecha, izquierda, panel, nivel, final;
    public TextMeshProUGUI score, nivelText;
    public bool instantiated, panelRojo, panelVerde;
    public static bool BatFlyingV2;
    public int rnd;
    public int flecha, respuesta, points, vidas = 3, level;
    private float tiempoPanel, tiempoNivel, tiempoFinal;
    // Start is called before the first frame update
    void Start()
    {
        instantiated = false;
        panelRojo = false;
        panelVerde = false;
        vidas = 3;
        tiempoPanel = 2;
        tiempoNivel = 2;
        tiempoFinal = 5;
        level = 1;
        BatFlyingV2 = true;
        keys = new GameObject[10];
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
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                arriba.GetComponent<Button>().Select();
                FlechaArriba();
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                derecha.GetComponent<Button>().Select();
                FlechaDerecha();
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                izquierda.GetComponent<Button>().Select();
                FlechaIzquierda();
            }  
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                abajo.GetComponent<Button>().Select();
                FlechaAbajo();
            }
            
            if (BatWallController.num == 10)
            {
                if (flecha == respuesta)
                {
                    panelVerde = true;
                    points++;
                    score.text = "Score: " + points;
                    LevelSystem();
                }
                else
                {
                    Miss();
                }
                instantiated = false;
                BatWallController.num = 0;
            }
            if (tiempoNivel >= 0)
            {
                nivel.SetActive(true);
                tiempoNivel -= Time.deltaTime;
            }
            else
            {
                nivel.SetActive(false);
                Generar();
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
                    if (ProfileStorage.s_currentProfile.batFlying2ScoreP < points)
                    {
                        ProfileStorage.StoragePlayerProfile("batFlying2", points, level);
                    }   
                }
                else
                {
                    if (ProfileStorage.s_currentProfile.batFlying2Score < points)
                    {
                        ProfileStorage.StoragePlayerProfile("batFlying2", points, level);
                    }  
                }
                BatFlyingV2 = false;
                Debug.Log("Juego Terminado");
                SceneManager.LoadScene("JuegoFlying");
            }
        }
    }
    void Generar()
    {
        if (!instantiated)
        {
            paredAbajo.SetActive(true);
            paredArriba.SetActive(true);
            paredDerecha.SetActive(true);
            paredIzquierda.SetActive(true);

            rnd = Random.Range(0, 4);
            int randomKey = Random.Range(0, 4);
            switch (rnd)
            {
                case 0:
                    paredArriba.SetActive(false);
                    keys[0] = Instantiate(murcielagos[0], new Vector2(0.7600002f, 7.44f), murcielagos[0].transform.rotation);
                    keys[1] = Instantiate(murcielagos[0], new Vector2(-3.389999f, 8.7f), murcielagos[0].transform.rotation);
                    keys[2] = Instantiate(murcielagos[0], new Vector2(0.9899998f, 5.82f), murcielagos[0].transform.rotation);
                    keys[3] = Instantiate(murcielagos[0], new Vector2(-3.37f, 7.14f), murcielagos[0].transform.rotation);

                    keys[4] = Instantiate(murcielagos[randomKey], new Vector2(-1.4f, 8.2f), murcielagos[randomKey].transform.rotation);

                    keys[5] = Instantiate(murcielagos[0], new Vector2(-2.62f, 10f), murcielagos[0].transform.rotation);
                    keys[6] = Instantiate(murcielagos[0], new Vector2(-1.179999f, 6.69f), murcielagos[0].transform.rotation);
                    keys[7] = Instantiate(murcielagos[0], new Vector2(0.2800007f, 9.62f), murcielagos[0].transform.rotation);
                    keys[8] = Instantiate(murcielagos[0], new Vector2(-2.67f, 5.77f), murcielagos[0].transform.rotation);
                    keys[9] = Instantiate(murcielagos[0], new Vector2(1.84f, 8.58f), murcielagos[0].transform.rotation);
                    break;
                case 1:
                    paredDerecha.SetActive(false);
                    keys[0] = Instantiate(murcielagos[1], new Vector2(15.82f, -0.94f), murcielagos[1].transform.rotation);
                    keys[1] = Instantiate(murcielagos[1], new Vector2(11.67f, 0.32f), murcielagos[1].transform.rotation);
                    keys[2] = Instantiate(murcielagos[1], new Vector2(16.05f, -2.56f), murcielagos[1].transform.rotation);
                    keys[3] = Instantiate(murcielagos[1], new Vector2(10.7f, -0.95f), murcielagos[1].transform.rotation);

                    keys[4] = Instantiate(murcielagos[randomKey], new Vector2(13.61f, 0), murcielagos[randomKey].transform.rotation);

                    keys[5] = Instantiate(murcielagos[1], new Vector2(12.44f, 1.62f), murcielagos[1].transform.rotation);
                    keys[6] = Instantiate(murcielagos[1], new Vector2(13.88f, -1.73f), murcielagos[1].transform.rotation);
                    keys[7] = Instantiate(murcielagos[1], new Vector2(15.34f, 1.24f), murcielagos[1].transform.rotation);
                    keys[8] = Instantiate(murcielagos[1], new Vector2(12.08f, -2.37f), murcielagos[1].transform.rotation);
                    keys[9] = Instantiate(murcielagos[1], new Vector2(16.9f, 0.2f), murcielagos[1].transform.rotation);
                    break;
                case 2:
                    paredIzquierda.SetActive(false);
                    keys[0] = Instantiate(murcielagos[2], new Vector2(-15.82f, -0.94f), murcielagos[2].transform.rotation);
                    keys[1] = Instantiate(murcielagos[2], new Vector2(-11.67f, 0.32f), murcielagos[2].transform.rotation);
                    keys[2] = Instantiate(murcielagos[2], new Vector2(-16.05f, -2.56f), murcielagos[2].transform.rotation);
                    keys[3] = Instantiate(murcielagos[2], new Vector2(-10.7f, -0.95f), murcielagos[2].transform.rotation);

                    keys[4] = Instantiate(murcielagos[randomKey], new Vector2(-13.61f, 0), murcielagos[randomKey].transform.rotation);

                    keys[5] = Instantiate(murcielagos[2], new Vector2(-12.44f, 1.62f), murcielagos[2].transform.rotation);
                    keys[6] = Instantiate(murcielagos[2], new Vector2(-13.88f, -1.73f), murcielagos[2].transform.rotation);
                    keys[7] = Instantiate(murcielagos[2], new Vector2(-15.34f, 1.24f), murcielagos[2].transform.rotation);
                    keys[8] = Instantiate(murcielagos[2], new Vector2(-12.08f, -2.37f), murcielagos[2].transform.rotation);
                    keys[9] = Instantiate(murcielagos[2], new Vector2(-16.9f, 0.2f), murcielagos[2].transform.rotation); 
                    break;
                case 3:
                    paredAbajo.SetActive(false);
                    keys[0] = Instantiate(murcielagos[3], new Vector2(0.7600002f, -7.44f), murcielagos[3].transform.rotation);
                    keys[1] = Instantiate(murcielagos[3], new Vector2(-3.389999f, -8.7f), murcielagos[3].transform.rotation);
                    keys[2] = Instantiate(murcielagos[3], new Vector2(0.9899998f, -5.82f), murcielagos[3].transform.rotation);
                    keys[3] = Instantiate(murcielagos[3], new Vector2(-3.37f, -7.14f), murcielagos[3].transform.rotation);

                    keys[4] = Instantiate(murcielagos[randomKey], new Vector2(-1.4f, -8.2f), murcielagos[randomKey].transform.rotation);

                    keys[5] = Instantiate(murcielagos[3], new Vector2(-2.62f, -10f), murcielagos[3].transform.rotation);
                    keys[6] = Instantiate(murcielagos[3], new Vector2(-1.179999f, -6.69f), murcielagos[3].transform.rotation);
                    keys[7] = Instantiate(murcielagos[3], new Vector2(0.2800007f, -9.62f), murcielagos[3].transform.rotation);
                    keys[8] = Instantiate(murcielagos[3], new Vector2(-2.67f, -5.77f), murcielagos[3].transform.rotation);
                    keys[9] = Instantiate(murcielagos[3], new Vector2(1.84f, -8.58f), murcielagos[3].transform.rotation);
                    break;
            }
            instantiated = true;
            if (randomKey == 0)
                respuesta = 4;
            else if (randomKey == 1)
                respuesta = 3;
            else if (randomKey == 2)
                respuesta = 2;
            else
                respuesta = 1;
        }
        if (JugarManager.toggleValue)
        {
            if (points < 5)
            {
                if (rnd == 0)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 20f * Time.deltaTime);
                    }
                }
                else if (rnd == 1)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 20f * Time.deltaTime);
                    }
                }
                else if (rnd == 2)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 20f * Time.deltaTime);
                    }
                }
                else
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 20f * Time.deltaTime);
                    }
                }
            }
            else if (points >= 5 && points < 10)
            {
                if (rnd == 0)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 30f * Time.deltaTime);
                    }
                }
                else if (rnd == 1)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 30f * Time.deltaTime);
                    }
                }
                else if (rnd == 2)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 30f * Time.deltaTime);
                    }
                }
                else
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 30f * Time.deltaTime);
                    }
                }
            }
            else if (points >= 10 && points < 15)
            {
                if (rnd == 0)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 40f * Time.deltaTime);
                    }
                }
                else if (rnd == 1)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 40f * Time.deltaTime);
                    }
                }
                else if (rnd == 2)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 40f * Time.deltaTime);
                    }
                }
                else
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 40f * Time.deltaTime);
                    }
                }
            }
            else if (points >= 15 && points < 20)
            {
                if (rnd == 0)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 50f * Time.deltaTime);
                    }
                }
                else if (rnd == 1)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 50f * Time.deltaTime);
                    }
                }
                else if (rnd == 2)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 50f * Time.deltaTime);
                    }
                }
                else
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 50f * Time.deltaTime);
                    }
                }
            }
            else if (points >= 20 && points < 25)
            {
                if (rnd == 0)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 60f * Time.deltaTime);
                    }
                }
                else if (rnd == 1)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 60f * Time.deltaTime);
                    }
                }
                else if (rnd == 2)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 60f * Time.deltaTime);
                    }
                }
                else
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 60f * Time.deltaTime);
                    }
                }
            }
            else if (points >= 25)
            {
                if (rnd == 0)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 70f * Time.deltaTime);
                    }
                }
                else if (rnd == 1)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 70f * Time.deltaTime);
                    }
                }
                else if (rnd == 2)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 70f * Time.deltaTime);
                    }
                }
                else
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 70f * Time.deltaTime);
                    }
                }
            }
        }
        else
        {
            if (points < 5)
            {
                if (rnd == 0)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 10f * Time.deltaTime);
                    }
                }
                else if (rnd == 1)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10f * Time.deltaTime);
                    }
                }
                else if (rnd == 2)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10f * Time.deltaTime);
                    }
                }
                else
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10f * Time.deltaTime);
                    }
                }
            }
            else if (points >= 5 && points < 10)
            {
                if (rnd == 0)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 15f * Time.deltaTime);
                    }
                }
                else if (rnd == 1)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 15f * Time.deltaTime);
                    }
                }
                else if (rnd == 2)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 15f * Time.deltaTime);
                    }
                }
                else
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 15f * Time.deltaTime);
                    }
                }
            }
            else if (points >= 10 && points < 15)
            {
                if (rnd == 0)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 20f * Time.deltaTime);
                    }
                }
                else if (rnd == 1)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 20f * Time.deltaTime);
                    }
                }
                else if (rnd == 2)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 20f * Time.deltaTime);
                    }
                }
                else
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 20f * Time.deltaTime);
                    }
                }
            }
            else if (points >= 15 && points < 20)
            {
                if (rnd == 0)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 25f * Time.deltaTime);
                    }
                }
                else if (rnd == 1)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 25f * Time.deltaTime);
                    }
                }
                else if (rnd == 2)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 25f * Time.deltaTime);
                    }
                }
                else
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 25f * Time.deltaTime);
                    }
                }
            }
            else if (points >= 20 && points < 25)
            {
                if (rnd == 0)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 30f * Time.deltaTime);
                    }
                }
                else if (rnd == 1)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 30f * Time.deltaTime);
                    }
                }
                else if (rnd == 2)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 30f * Time.deltaTime);
                    }
                }
                else
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 30f * Time.deltaTime);
                    }
                }
            }
            else if (points >= 25)
            {
                if (rnd == 0)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 35f * Time.deltaTime);
                    }
                }
                else if (rnd == 1)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 35f * Time.deltaTime);
                    }
                }
                else if (rnd == 2)
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 35f * Time.deltaTime);
                    }
                }
                else
                {
                    foreach (GameObject item in keys)
                    {
                        if (item != null)
                            item.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 35f * Time.deltaTime);
                    }
                }
            }
        }
    }
    public void FlechaArriba()
    {
        flecha = 1;
    }
    public void FlechaDerecha()
    {
        flecha = 2;
    }
    public void FlechaIzquierda()
    {
        flecha = 3;
    }
    public void FlechaAbajo()
    {
        flecha = 4;
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
        if (points > 0)
        {
            if (points == 5 || points == 10 || points == 15 || points == 20 || points == 25)
                level--;
            points--;
            score.text = "Score: " + points;
        }
        vidas--;
    }
    void LevelSystem()
    {
        switch (points)
        {
            case 6:
                tiempoNivel = 3;
                level++;
                nivelText.text = "Nivel 2";
                break;
            case 12:
                tiempoNivel = 3;
                level++;
                nivelText.text = "Nivel 3";
                break;
            case 18:
                tiempoNivel = 3;
                level++;
                nivelText.text = "Nivel 4";
                break;
            case 24:
                tiempoNivel = 3;
                level++;
                nivelText.text = "Nivel 5";
                break;
            case 30:
                tiempoNivel = 3;
                level++;
                nivelText.text = "Nivel 6";
                break;
        }
    }
}
