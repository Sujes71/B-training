using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BatFlyingManager : MonoBehaviour
{
    public GameObject[] murcielagos;
    public GameObject[] keys;
    public GameObject paredAbajo, paredArriba, paredDerecha, paredIzquierda, vida1, vida2, vida3, arriba, abajo, derecha, izquierda, panel, nivel, final;
    public TextMeshProUGUI score, nivelText;

    public bool instantiated, panelRojo, panelVerde;
    public static bool BatFlying;
    public int rnd, level;
    public int flecha, respuesta, points, vidas;
    private float tiempoPanel, tiempoNivel, tiempoFinal;
    // Start is called before the first frame update
    void Start()
    {
        vidas = 3;
        tiempoPanel = 2;
        tiempoNivel = 3;
        tiempoFinal = 5;
        instantiated = false;
        panelRojo = false;
        panelVerde = false;
        level = 1;
        BatFlying = true;
        keys = new GameObject[5];
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
            
            if (BatWallController.num == 5)
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
            if(tiempoNivel >= 0)
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
                    if (ProfileStorage.s_currentProfile.batFlyingScoreP < points)
                    {
                        ProfileStorage.StoragePlayerProfile("batFlying", points, level);
                    }    
                }
                else
                {
                    if (ProfileStorage.s_currentProfile.batFlyingScore < points)
                    {
                        ProfileStorage.StoragePlayerProfile("batFlying", points, level);
                    }  
                }
                BatFlying = false;
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
            int randomKey = 0;
            switch (rnd)
            {
                case 0:
                    Debug.Log("Caso 0");
                    randomKey = Random.Range(0, 2);
                    paredArriba.SetActive(false);
                    keys[0] = Instantiate(murcielagos[0], new Vector2(0, 6), murcielagos[0].transform.rotation);
                    keys[1] = Instantiate(murcielagos[0], new Vector2(0, 8.13f), murcielagos[0].transform.rotation);
                    keys[2] = Instantiate(murcielagos[randomKey], new Vector2(0, 10.13f), murcielagos[randomKey].transform.rotation);
                    keys[3] = Instantiate(murcielagos[0], new Vector2(0, 12.13f), murcielagos[0].transform.rotation);
                    keys[4] = Instantiate(murcielagos[0], new Vector2(0, 14.13f), murcielagos[0].transform.rotation);

                    break;
                case 1:
                    Debug.Log("Caso 1");
                    randomKey = Random.Range(2, 4);
                    paredDerecha.SetActive(false);
                    keys[0] = Instantiate(murcielagos[3], new Vector2(10, 0), murcielagos[3].transform.rotation);
                    keys[1] = Instantiate(murcielagos[3], new Vector2(12, 0), murcielagos[3].transform.rotation);
                    keys[2] = Instantiate(murcielagos[randomKey], new Vector2(14, 0), murcielagos[randomKey].transform.rotation);
                    keys[3] = Instantiate(murcielagos[3], new Vector2(16, 0), murcielagos[3].transform.rotation);
                    keys[4] = Instantiate(murcielagos[3], new Vector2(18, 0), murcielagos[3].transform.rotation);
                    break;
                case 2:
                    Debug.Log("Caso 2");
                    randomKey = Random.Range(2, 4);
                    paredIzquierda.SetActive(false);
                    keys[0] = Instantiate(murcielagos[2], new Vector2(-10, 0), murcielagos[2].transform.rotation);
                    keys[1] = Instantiate(murcielagos[2], new Vector2(-12, 0), murcielagos[2].transform.rotation);
                    keys[2] = Instantiate(murcielagos[randomKey], new Vector2(-14, 0), murcielagos[randomKey].transform.rotation);
                    keys[3] = Instantiate(murcielagos[2], new Vector2(-16, 0), murcielagos[2].transform.rotation);
                    keys[4] = Instantiate(murcielagos[2], new Vector2(-18, 0), murcielagos[2].transform.rotation);
                    break;
                case 3:
                    Debug.Log("Caso 3");
                    randomKey = Random.Range(0, 2);
                    paredAbajo.SetActive(false);
                    keys[0] = Instantiate(murcielagos[1], new Vector2(0, -6), murcielagos[1].transform.rotation);
                    keys[1] = Instantiate(murcielagos[1], new Vector2(0, -8.13f), murcielagos[1].transform.rotation);
                    keys[2] = Instantiate(murcielagos[randomKey], new Vector2(0, -10.13f), murcielagos[randomKey].transform.rotation);
                    keys[3] = Instantiate(murcielagos[1], new Vector2(0, -12.13f), murcielagos[1].transform.rotation);
                    keys[4] = Instantiate(murcielagos[1], new Vector2(0, -14.13f), murcielagos[1].transform.rotation);
                    break;
            }
            instantiated = true;
            if (randomKey == 0)
                respuesta = 1;
            else if (randomKey == 1)
                respuesta = 2;
            else if (randomKey == 2)
                respuesta = 3;
            else
                respuesta = 4;
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
    public void FlechaAbajo()
    {
        flecha = 1;
    }
    public void FlechaArriba()
    {
        flecha = 2;
    }
    public void FlechaDerecha()
    {
        flecha = 3;
    }
    public void FlechaIzquierda()
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
