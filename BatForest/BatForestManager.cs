using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BatForestManager : MonoBehaviour
{
    public Vector2[] positions;
    public GameObject player, fruit, fakeFruit, humo, cam, paredArriba, vida1, vida2 , vida3, final, panel, nivel;
    public bool comienzo = true, hayFruta = false;
    public Vector2 startPosition;
    public bool r1 = false, r2 = false, r3 = false, r4 = false, r5 = false, r6 = false, r7 = false, r8 = false, fake = false, panelRojo = false, panelVerde = false;
    public GameObject[] copiasFake;
    public TextMeshProUGUI score, nivelText;
    public static bool BatForest;
    public int numFakes = 2, vidas = 3, level;
    public float tiempoMovimiento = 2, tiempoFinal = 5, tiempoPanel = 2, tiempoNivel = 2;
    private GameObject copia;
    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        BatForest = true;
        copiasFake = new GameObject[numFakes];
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
            desplazamientoInicial();
            if (!comienzo)
            {
                if(tiempoNivel >= 0)
                {
                    nivel.SetActive(true);
                    tiempoNivel -= Time.deltaTime;
                }
                else
                {
                    nivel.SetActive(false);
                    lanzamientoFruta();
                    paredArriba.GetComponent<BoxCollider2D>().enabled = true;
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
                    if (ProfileStorage.s_currentProfile.batForestScoreP < FindObjectOfType<BatController>().points)
                    {
                        ProfileStorage.StoragePlayerProfile("batForest", FindObjectOfType<BatController>().points, level);
                    }
                }
                else
                {
                    if (ProfileStorage.s_currentProfile.batForestScore < FindObjectOfType<BatController>().points)
                    {
                        ProfileStorage.StoragePlayerProfile("batForest", FindObjectOfType<BatController>().points, level);
                    } 
                }
                BatForest = false;
                Debug.Log("Juego Terminado");
                SceneManager.LoadScene("JuegoForest");
            }
        }
    }
    void desplazamientoInicial()
    {
        Vector3 movimiento = Vector3.zero;
        if (comienzo)
        {
            if (player.transform.position.y >= 0)
            {
                movimiento.x = -1;
                movimiento.y = -1;
            }
            else
            {
                if (tiempoMovimiento >= 0)
                {
                    tiempoMovimiento -= Time.deltaTime;
                    if (tiempoMovimiento <= 0.25f)
                    {
                        humo.SetActive(true);
                    }
                    if (tiempoMovimiento <= 1)
                    {
                        cam.GetComponent<Animator>().SetBool("temblor", true);
                    }
                }   
                else
                {
                    movimiento.x = -1;
                    player.GetComponent<SpriteRenderer>().flipX = true;
                }
            }
        }
        if (player.transform.position.x >= -6.826174)
        {
            player.GetComponent<BatController>().Move(movimiento);
        }
        else
        {
            cam.GetComponent<Animator>().SetBool("temblor", false);
            comienzo = false;
        }
    }
    void lanzamientoFruta()
    {
        if (!hayFruta)
        {
            GameObject frutaAuxiliar;
            frutaAuxiliar = Instantiate(fruit, positions[Random.Range(0, 8)], Quaternion.identity);
            copia = frutaAuxiliar;
            startPosition = copia.transform.position;
            hayFruta = true;
        }
        else
        {
            if (!JugarManager.toggleValue)
            {
                //dificultad 1
                if (FindObjectOfType<BatController>().points <= 4)
                {
                    FindObjectOfType<BatController>().speed = 3;
                    if (copia.transform.position.x == positions[0].x && copia.transform.position.y == positions[0].y || r1)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 8 * Time.deltaTime, copia.transform.position.y - 8 * Time.deltaTime);
                        r1 = true;
                    }
                    else if (copia.transform.position.x == positions[1].x && copia.transform.position.y == positions[1].y || r2)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 8 * Time.deltaTime, copia.transform.position.y - 8 * Time.deltaTime);
                        r2 = true;
                    }
                    else if (copia.transform.position.x == positions[2].x && copia.transform.position.y == positions[2].y || r3)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 8 * Time.deltaTime, copia.transform.position.y + 8 * Time.deltaTime);
                        r3 = true;
                    }
                    else if (copia.transform.position.x == positions[3].x && copia.transform.position.y == positions[3].y || r4)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 8 * Time.deltaTime, copia.transform.position.y + 8 * Time.deltaTime);
                        r4 = true;
                    }
                    else if (copia.transform.position.x == positions[4].x && copia.transform.position.y == positions[4].y || r5)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 10 * Time.deltaTime, copia.transform.position.y);
                        r5 = true;
                    }
                    else if (copia.transform.position.x == positions[5].x && copia.transform.position.y == positions[5].y || r6)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 10 * Time.deltaTime, copia.transform.position.y);
                        r6 = true;
                    }
                    else if (copia.transform.position.x == positions[6].x && copia.transform.position.y == positions[6].y || r7)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x, copia.transform.position.y - 10 * Time.deltaTime);
                        r7 = true;
                    }
                    else if (copia.transform.position.x == positions[7].x && copia.transform.position.y == positions[7].y || r8)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x, copia.transform.position.y + 10 * Time.deltaTime);
                        r8 = true;
                    }
                }
                //dificultad 2
                else if (FindObjectOfType<BatController>().points > 4 && FindObjectOfType<BatController>().points <= 9)
                {
                    FindObjectOfType<BatController>().speed = 5;
                    if (copia.transform.position.x == positions[0].x && copia.transform.position.y == positions[0].y || r1)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 8.3f * Time.deltaTime, copia.transform.position.y - 8.3f * Time.deltaTime);
                        r1 = true;
                    }
                    else if (copia.transform.position.x == positions[1].x && copia.transform.position.y == positions[1].y || r2)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 8.3f * Time.deltaTime, copia.transform.position.y - 8.3f * Time.deltaTime);
                        r2 = true;
                    }
                    else if (copia.transform.position.x == positions[2].x && copia.transform.position.y == positions[2].y || r3)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 8.3f * Time.deltaTime, copia.transform.position.y + 8.3f * Time.deltaTime);
                        r3 = true;
                    }
                    else if (copia.transform.position.x == positions[3].x && copia.transform.position.y == positions[3].y || r4)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 8.3f * Time.deltaTime, copia.transform.position.y + 8.3f * Time.deltaTime);
                        r4 = true;
                    }
                    else if (copia.transform.position.x == positions[4].x && copia.transform.position.y == positions[4].y || r5)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 10.3f * Time.deltaTime, copia.transform.position.y);
                        r5 = true;
                    }
                    else if (copia.transform.position.x == positions[5].x && copia.transform.position.y == positions[5].y || r6)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 10.3f * Time.deltaTime, copia.transform.position.y);
                        r6 = true;
                    }
                    else if (copia.transform.position.x == positions[6].x && copia.transform.position.y == positions[6].y || r7)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x, copia.transform.position.y - 10.3f * Time.deltaTime);
                        r7 = true;
                    }
                    else if (copia.transform.position.x == positions[7].x && copia.transform.position.y == positions[7].y || r8)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x, copia.transform.position.y + 10.3f * Time.deltaTime);
                        r8 = true;
                    }
                }
                //dificultad 3
                else if (FindObjectOfType<BatController>().points > 9 && FindObjectOfType<BatController>().points <= 14)
                {
                    FindObjectOfType<BatController>().speed = 6;
                    if (copia.transform.position.x == positions[0].x && copia.transform.position.y == positions[0].y || r1)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 8.6f * Time.deltaTime, copia.transform.position.y - 8.6f * Time.deltaTime);
                        r1 = true;
                    }
                    else if (copia.transform.position.x == positions[1].x && copia.transform.position.y == positions[1].y || r2)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 8.6f * Time.deltaTime, copia.transform.position.y - 8.6f * Time.deltaTime);
                        r2 = true;
                    }
                    else if (copia.transform.position.x == positions[2].x && copia.transform.position.y == positions[2].y || r3)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 8.6f * Time.deltaTime, copia.transform.position.y + 8.6f * Time.deltaTime);
                        r3 = true;
                    }
                    else if (copia.transform.position.x == positions[3].x && copia.transform.position.y == positions[3].y || r4)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 8.6f * Time.deltaTime, copia.transform.position.y + 8.6f * Time.deltaTime);
                        r4 = true;
                    }
                    else if (copia.transform.position.x == positions[4].x && copia.transform.position.y == positions[4].y || r5)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 10.6f * Time.deltaTime, copia.transform.position.y);
                        r5 = true;
                    }
                    else if (copia.transform.position.x == positions[5].x && copia.transform.position.y == positions[5].y || r6)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 10.6f * Time.deltaTime, copia.transform.position.y);
                        r6 = true;
                    }
                    else if (copia.transform.position.x == positions[6].x && copia.transform.position.y == positions[6].y || r7)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x, copia.transform.position.y - 10.6f * Time.deltaTime);
                        r7 = true;
                    }
                    else if (copia.transform.position.x == positions[7].x && copia.transform.position.y == positions[7].y || r8)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x, copia.transform.position.y + 10.6f * Time.deltaTime);
                        r8 = true;
                    }
                }
                //dificultad 4
                else if (FindObjectOfType<BatController>().points > 14 && FindObjectOfType<BatController>().points <= 19)
                {
                    FindObjectOfType<BatController>().speed = 7;
                    if (copia.transform.position.x == positions[0].x && copia.transform.position.y == positions[0].y || r1)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 8.9f * Time.deltaTime, copia.transform.position.y - 8.9f * Time.deltaTime);
                        r1 = true;
                    }
                    else if (copia.transform.position.x == positions[1].x && copia.transform.position.y == positions[1].y || r2)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 8.9f * Time.deltaTime, copia.transform.position.y - 8.9f * Time.deltaTime);
                        r2 = true;
                    }
                    else if (copia.transform.position.x == positions[2].x && copia.transform.position.y == positions[2].y || r3)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 8.9f * Time.deltaTime, copia.transform.position.y + 8.9f * Time.deltaTime);
                        r3 = true;
                    }
                    else if (copia.transform.position.x == positions[3].x && copia.transform.position.y == positions[3].y || r4)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 8.9f * Time.deltaTime, copia.transform.position.y + 8.9f * Time.deltaTime);
                        r4 = true;
                    }
                    else if (copia.transform.position.x == positions[4].x && copia.transform.position.y == positions[4].y || r5)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 10.9f * Time.deltaTime, copia.transform.position.y);
                        r5 = true;
                    }
                    else if (copia.transform.position.x == positions[5].x && copia.transform.position.y == positions[5].y || r6)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 10.9f * Time.deltaTime, copia.transform.position.y);
                        r6 = true;
                    }
                    else if (copia.transform.position.x == positions[6].x && copia.transform.position.y == positions[6].y || r7)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x, copia.transform.position.y - 10.9f * Time.deltaTime);
                        r7 = true;
                    }
                    else if (copia.transform.position.x == positions[7].x && copia.transform.position.y == positions[7].y || r8)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x, copia.transform.position.y + 10.9f * Time.deltaTime);
                        r8 = true;
                    }
                }
                //dificultad 5
                else if (FindObjectOfType<BatController>().points > 19)
                {
                    FindObjectOfType<BatController>().speed = 8;
                    if (copia.transform.position.x == positions[0].x && copia.transform.position.y == positions[0].y || r1)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 9.2f * Time.deltaTime, copia.transform.position.y - 9.2f * Time.deltaTime);
                        r1 = true;
                    }
                    else if (copia.transform.position.x == positions[1].x && copia.transform.position.y == positions[1].y || r2)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 9.2f * Time.deltaTime, copia.transform.position.y - 9.2f * Time.deltaTime);
                        r2 = true;
                    }
                    else if (copia.transform.position.x == positions[2].x && copia.transform.position.y == positions[2].y || r3)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 9.2f * Time.deltaTime, copia.transform.position.y + 9.2f * Time.deltaTime);
                        r3 = true;
                    }
                    else if (copia.transform.position.x == positions[3].x && copia.transform.position.y == positions[3].y || r4)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 9.2f * Time.deltaTime, copia.transform.position.y + 9.2f * Time.deltaTime);
                        r4 = true;
                    }
                    else if (copia.transform.position.x == positions[4].x && copia.transform.position.y == positions[4].y || r5)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 11.2f * Time.deltaTime, copia.transform.position.y);
                        r5 = true;
                    }
                    else if (copia.transform.position.x == positions[5].x && copia.transform.position.y == positions[5].y || r6)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 11.2f * Time.deltaTime, copia.transform.position.y);
                        r6 = true;
                    }
                    else if (copia.transform.position.x == positions[6].x && copia.transform.position.y == positions[6].y || r7)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x, copia.transform.position.y - 11.2f * Time.deltaTime);
                        r7 = true;
                    }
                    else if (copia.transform.position.x == positions[7].x && copia.transform.position.y == positions[7].y || r8)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x, copia.transform.position.y + 11.2f * Time.deltaTime);
                        r8 = true;
                    }
                }
            }
            else
            {
                //dificultad 1
                if (FindObjectOfType<BatController>().points <= 4)
                {
                    FindObjectOfType<BatController>().speed = 5;
                    if (copia.transform.position.x == positions[0].x && copia.transform.position.y == positions[0].y || r1)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 8.5f * Time.deltaTime, copia.transform.position.y - 8.5f * Time.deltaTime);
                        r1 = true;
                    }
                    else if (copia.transform.position.x == positions[1].x && copia.transform.position.y == positions[1].y || r2)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 8.5f * Time.deltaTime, copia.transform.position.y - 8.5f * Time.deltaTime);
                        r2 = true;
                    }
                    else if (copia.transform.position.x == positions[2].x && copia.transform.position.y == positions[2].y || r3)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 8.5f * Time.deltaTime, copia.transform.position.y + 8.5f * Time.deltaTime);
                        r3 = true;
                    }
                    else if (copia.transform.position.x == positions[3].x && copia.transform.position.y == positions[3].y || r4)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 8.5f * Time.deltaTime, copia.transform.position.y + 8.5f * Time.deltaTime);
                        r4 = true;
                    }
                    else if (copia.transform.position.x == positions[4].x && copia.transform.position.y == positions[4].y || r5)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 10.5f * Time.deltaTime, copia.transform.position.y);
                        r5 = true;
                    }
                    else if (copia.transform.position.x == positions[5].x && copia.transform.position.y == positions[5].y || r6)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 10.5f * Time.deltaTime, copia.transform.position.y);
                        r6 = true;
                    }
                    else if (copia.transform.position.x == positions[6].x && copia.transform.position.y == positions[6].y || r7)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x, copia.transform.position.y - 10.5f * Time.deltaTime);
                        r7 = true;
                    }
                    else if (copia.transform.position.x == positions[7].x && copia.transform.position.y == positions[7].y || r8)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x, copia.transform.position.y + 10.5f * Time.deltaTime);
                        r8 = true;
                    }
                }
                //dificultad 2
                else if (FindObjectOfType<BatController>().points > 4 && FindObjectOfType<BatController>().points <= 9)
                {
                    FindObjectOfType<BatController>().speed = 6;
                    if (copia.transform.position.x == positions[0].x && copia.transform.position.y == positions[0].y || r1)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 9 * Time.deltaTime, copia.transform.position.y - 9 * Time.deltaTime);
                        r1 = true;
                    }
                    else if (copia.transform.position.x == positions[1].x && copia.transform.position.y == positions[1].y || r2)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 9 * Time.deltaTime, copia.transform.position.y - 9 * Time.deltaTime);
                        r2 = true;
                    }
                    else if (copia.transform.position.x == positions[2].x && copia.transform.position.y == positions[2].y || r3)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 9 * Time.deltaTime, copia.transform.position.y + 9 * Time.deltaTime);
                        r3 = true;
                    }
                    else if (copia.transform.position.x == positions[3].x && copia.transform.position.y == positions[3].y || r4)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 9 * Time.deltaTime, copia.transform.position.y + 9 * Time.deltaTime);
                        r4 = true;
                    }
                    else if (copia.transform.position.x == positions[4].x && copia.transform.position.y == positions[4].y || r5)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 11 * Time.deltaTime, copia.transform.position.y);
                        r5 = true;
                    }
                    else if (copia.transform.position.x == positions[5].x && copia.transform.position.y == positions[5].y || r6)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 11 * Time.deltaTime, copia.transform.position.y);
                        r6 = true;
                    }
                    else if (copia.transform.position.x == positions[6].x && copia.transform.position.y == positions[6].y || r7)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x, copia.transform.position.y - 11 * Time.deltaTime);
                        r7 = true;
                    }
                    else if (copia.transform.position.x == positions[7].x && copia.transform.position.y == positions[7].y || r8)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x, copia.transform.position.y + 11 * Time.deltaTime);
                        r8 = true;
                    }
                }
                //dificultad 3
                else if (FindObjectOfType<BatController>().points > 9 && FindObjectOfType<BatController>().points <= 14)
                {
                    FindObjectOfType<BatController>().speed = 7;
                    if (copia.transform.position.x == positions[0].x && copia.transform.position.y == positions[0].y || r1)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 9.5f * Time.deltaTime, copia.transform.position.y - 9.5f * Time.deltaTime);
                        r1 = true;
                    }
                    else if (copia.transform.position.x == positions[1].x && copia.transform.position.y == positions[1].y || r2)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 9.5f * Time.deltaTime, copia.transform.position.y - 9.5f * Time.deltaTime);
                        r2 = true;
                    }
                    else if (copia.transform.position.x == positions[2].x && copia.transform.position.y == positions[2].y || r3)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 9.5f * Time.deltaTime, copia.transform.position.y + 9.5f * Time.deltaTime);
                        r3 = true;
                    }
                    else if (copia.transform.position.x == positions[3].x && copia.transform.position.y == positions[3].y || r4)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 9.5f * Time.deltaTime, copia.transform.position.y + 9.5f * Time.deltaTime);
                        r4 = true;
                    }
                    else if (copia.transform.position.x == positions[4].x && copia.transform.position.y == positions[4].y || r5)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 11.5f * Time.deltaTime, copia.transform.position.y);
                        r5 = true;
                    }
                    else if (copia.transform.position.x == positions[5].x && copia.transform.position.y == positions[5].y || r6)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 11.5f * Time.deltaTime, copia.transform.position.y);
                        r6 = true;
                    }
                    else if (copia.transform.position.x == positions[6].x && copia.transform.position.y == positions[6].y || r7)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x, copia.transform.position.y - 11.5f * Time.deltaTime);
                        r7 = true;
                    }
                    else if (copia.transform.position.x == positions[7].x && copia.transform.position.y == positions[7].y || r8)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x, copia.transform.position.y + 11.5f * Time.deltaTime);
                        r8 = true;
                    }
                }
                //dificultad 4
                else if (FindObjectOfType<BatController>().points > 14 && FindObjectOfType<BatController>().points <= 19)
                {
                    FindObjectOfType<BatController>().speed = 8;
                    if (copia.transform.position.x == positions[0].x && copia.transform.position.y == positions[0].y || r1)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 10 * Time.deltaTime, copia.transform.position.y - 10 * Time.deltaTime);
                        r1 = true;
                    }
                    else if (copia.transform.position.x == positions[1].x && copia.transform.position.y == positions[1].y || r2)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 10 * Time.deltaTime, copia.transform.position.y - 10 * Time.deltaTime);
                        r2 = true;
                    }
                    else if (copia.transform.position.x == positions[2].x && copia.transform.position.y == positions[2].y || r3)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 10 * Time.deltaTime, copia.transform.position.y + 10 * Time.deltaTime);
                        r3 = true;
                    }
                    else if (copia.transform.position.x == positions[3].x && copia.transform.position.y == positions[3].y || r4)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 10 * Time.deltaTime, copia.transform.position.y + 10 * Time.deltaTime);
                        r4 = true;
                    }
                    else if (copia.transform.position.x == positions[4].x && copia.transform.position.y == positions[4].y || r5)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 12 * Time.deltaTime, copia.transform.position.y);
                        r5 = true;
                    }
                    else if (copia.transform.position.x == positions[5].x && copia.transform.position.y == positions[5].y || r6)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 12 * Time.deltaTime, copia.transform.position.y);
                        r6 = true;
                    }
                    else if (copia.transform.position.x == positions[6].x && copia.transform.position.y == positions[6].y || r7)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x, copia.transform.position.y - 12 * Time.deltaTime);
                        r7 = true;
                    }
                    else if (copia.transform.position.x == positions[7].x && copia.transform.position.y == positions[7].y || r8)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x, copia.transform.position.y + 12 * Time.deltaTime);
                        r8 = true;
                    }
                }
                //dificultad 5
                else if (FindObjectOfType<BatController>().points > 19)
                {
                    FindObjectOfType<BatController>().speed = 9;
                    if (copia.transform.position.x == positions[0].x && copia.transform.position.y == positions[0].y || r1)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 10.5f * Time.deltaTime, copia.transform.position.y - 10.5f * Time.deltaTime);
                        r1 = true;
                    }
                    else if (copia.transform.position.x == positions[1].x && copia.transform.position.y == positions[1].y || r2)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 10.5f * Time.deltaTime, copia.transform.position.y - 10.5f * Time.deltaTime);
                        r2 = true;
                    }
                    else if (copia.transform.position.x == positions[2].x && copia.transform.position.y == positions[2].y || r3)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 10.5f * Time.deltaTime, copia.transform.position.y + 10.5f * Time.deltaTime);
                        r3 = true;
                    }
                    else if (copia.transform.position.x == positions[3].x && copia.transform.position.y == positions[3].y || r4)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 10.5f * Time.deltaTime, copia.transform.position.y + 10.5f * Time.deltaTime);
                        r4 = true;
                    }
                    else if (copia.transform.position.x == positions[4].x && copia.transform.position.y == positions[4].y || r5)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x - 12.5f * Time.deltaTime, copia.transform.position.y);
                        r5 = true;
                    }
                    else if (copia.transform.position.x == positions[5].x && copia.transform.position.y == positions[5].y || r6)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x + 12.5f * Time.deltaTime, copia.transform.position.y);
                        r6 = true;
                    }
                    else if (copia.transform.position.x == positions[6].x && copia.transform.position.y == positions[6].y || r7)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x, copia.transform.position.y - 12.5f * Time.deltaTime);
                        r7 = true;
                    }
                    else if (copia.transform.position.x == positions[7].x && copia.transform.position.y == positions[7].y || r8)
                    {
                        copia.transform.position = new Vector2(copia.transform.position.x, copia.transform.position.y + 12.5f * Time.deltaTime);
                        r8 = true;
                    }
                }
            }
            if (copia.transform.position.x > -2.540081f && copia.transform.position.x < 2.540081f && copia.transform.position.y > -2.415279f && copia.transform.position.y < 2.415279f)
            {
                copia.GetComponent<BoxCollider2D>().enabled = true;
                if (FindObjectOfType<BatForestManager>().startPosition == FindObjectOfType<BatForestManager>().positions[0])
                {
                    if (!fake)
                    {
                        for (int i = 0; i < numFakes; i++)
                        {
                            copiasFake[i] = Instantiate(fakeFruit, new Vector2(copia.transform.position.x, copia.transform.position.y), Quaternion.identity);
                        }
                        fake = true;
                    }
                    else
                    {
                        //HACIA IZQUIERDA ABAJO
                        copiasFake[0].transform.position = new Vector2(copiasFake[0].transform.position.x - 0.3f * Time.deltaTime, copiasFake[0].transform.position.y);
                        copiasFake[1].transform.position = new Vector2(copiasFake[1].transform.position.x, copiasFake[1].transform.position.y - 0.3f * Time.deltaTime);
                        copia.transform.position = new Vector2(copia.transform.position.x + 7.7f * Time.deltaTime, copia.transform.position.y + 7.7f * Time.deltaTime);
                    }
                }
                else if (FindObjectOfType<BatForestManager>().startPosition == FindObjectOfType<BatForestManager>().positions[1])
                {
                    if (!fake)
                    {
                        for (int i = 0; i < numFakes; i++)
                        {
                            copiasFake[i] = Instantiate(fakeFruit, new Vector2(copia.transform.position.x, copia.transform.position.y), Quaternion.identity);
                        }
                        fake = true;
                    }
                    else
                    {
                        //HACIA DERECHA ABAJO
                        copiasFake[0].transform.position = new Vector2(copiasFake[0].transform.position.x + 0.3f * Time.deltaTime, copiasFake[0].transform.position.y);
                        copiasFake[1].transform.position = new Vector2(copiasFake[1].transform.position.x, copiasFake[1].transform.position.y - 0.3f * Time.deltaTime);
                        copia.transform.position = new Vector2(copia.transform.position.x - 7.7f * Time.deltaTime, copia.transform.position.y + 7.7f * Time.deltaTime);
                    }
                }
                else if (FindObjectOfType<BatForestManager>().startPosition == FindObjectOfType<BatForestManager>().positions[2])
                {
                    if (!fake)
                    {
                        for (int i = 0; i < numFakes; i++)
                        {
                            copiasFake[i] = Instantiate(fakeFruit, new Vector2(copia.transform.position.x, copia.transform.position.y), Quaternion.identity);
                        }
                        fake = true;
                    }
                    else
                    {
                        //HACIA DERECHA ARRIBA
                        copiasFake[0].transform.position = new Vector2(copiasFake[0].transform.position.x + 0.3f * Time.deltaTime, copiasFake[0].transform.position.y);
                        copiasFake[1].transform.position = new Vector2(copiasFake[1].transform.position.x, copiasFake[1].transform.position.y + 0.3f * Time.deltaTime);
                        copia.transform.position = new Vector2(copia.transform.position.x - 7.7f * Time.deltaTime, copia.transform.position.y - 7.7f * Time.deltaTime);
                    }
                }
                else if (FindObjectOfType<BatForestManager>().startPosition == FindObjectOfType<BatForestManager>().positions[3])
                {
                    if (!fake)
                    {
                        for (int i = 0; i < numFakes; i++)
                        {
                            copiasFake[i] = Instantiate(fakeFruit, new Vector2(copia.transform.position.x, copia.transform.position.y), Quaternion.identity);
                        }
                        fake = true;
                    }
                    else
                    {
                        //HACIA IZQUIERDA ARRIBA
                        copiasFake[0].transform.position = new Vector2(copiasFake[0].transform.position.x - 0.3f * Time.deltaTime, copiasFake[0].transform.position.y);
                        copiasFake[1].transform.position = new Vector2(copiasFake[1].transform.position.x, copiasFake[1].transform.position.y + 0.3f * Time.deltaTime);
                        copia.transform.position = new Vector2(copia.transform.position.x + 7.7f * Time.deltaTime, copia.transform.position.y - 7.7f * Time.deltaTime);
                    }
                }
                else if (FindObjectOfType<BatForestManager>().startPosition == FindObjectOfType<BatForestManager>().positions[4])
                {
                    if (!fake)
                    {
                        for (int i = 0; i < numFakes; i++)
                        {
                            copiasFake[i] = Instantiate(fakeFruit, new Vector2(copia.transform.position.x, copia.transform.position.y), Quaternion.identity);
                        }
                        fake = true;
                    }
                    else
                    {
                        //HACIA IZQUIERDA
                        copiasFake[0].transform.position = new Vector2(copiasFake[0].transform.position.x - 0.15f * Time.deltaTime, copiasFake[0].transform.position.y + 0.15f * Time.deltaTime);
                        copiasFake[1].transform.position = new Vector2(copiasFake[1].transform.position.x - 0.15f * Time.deltaTime, copiasFake[1].transform.position.y - 0.15f * Time.deltaTime);
                        copia.transform.position = new Vector2(copia.transform.position.x + 9.4f * Time.deltaTime, copia.transform.position.y);
                    }
                }
                else if (FindObjectOfType<BatForestManager>().startPosition == FindObjectOfType<BatForestManager>().positions[5])
                {
                    if (!fake)
                    {
                        for (int i = 0; i < numFakes; i++)
                        {
                            copiasFake[i] = Instantiate(fakeFruit, new Vector2(copia.transform.position.x, copia.transform.position.y), Quaternion.identity);
                        }
                        fake = true;
                    }
                    else
                    {
                        //HACIA DERECHA
                        copiasFake[0].transform.position = new Vector2(copiasFake[0].transform.position.x + 0.15f * Time.deltaTime, copiasFake[0].transform.position.y + 0.15f * Time.deltaTime);
                        copiasFake[1].transform.position = new Vector2(copiasFake[1].transform.position.x + 0.15f * Time.deltaTime, copiasFake[1].transform.position.y - 0.15f * Time.deltaTime);
                        copia.transform.position = new Vector2(copia.transform.position.x - 9.4f * Time.deltaTime, copia.transform.position.y);
                    }
                }
                else if (FindObjectOfType<BatForestManager>().startPosition == FindObjectOfType<BatForestManager>().positions[6])
                {
                    if (!fake)
                    {
                        for (int i = 0; i < numFakes; i++)
                        {
                            copiasFake[i] = Instantiate(fakeFruit, new Vector2(copia.transform.position.x, copia.transform.position.y), Quaternion.identity);
                        }
                        fake = true;
                    }
                    else
                    {
                        //HACIA ABAJO
                        copiasFake[0].transform.position = new Vector2(copiasFake[0].transform.position.x + 0.15f * Time.deltaTime, copiasFake[0].transform.position.y - 0.15f * Time.deltaTime);
                        copiasFake[1].transform.position = new Vector2(copiasFake[1].transform.position.x - 0.15f * Time.deltaTime, copiasFake[1].transform.position.y - 0.15f * Time.deltaTime);
                        copia.transform.position = new Vector2(copia.transform.position.x, copia.transform.position.y + 9.4f * Time.deltaTime);
                    }
                }
                else if (FindObjectOfType<BatForestManager>().startPosition == FindObjectOfType<BatForestManager>().positions[7])
                {
                    if (!fake)
                    {
                        for (int i = 0; i < numFakes; i++)
                        {
                            copiasFake[i] = Instantiate(fakeFruit, new Vector2(copia.transform.position.x, copia.transform.position.y), Quaternion.identity);
                        }
                        fake = true;
                    }
                    else
                    {
                        //HACIA ARRIBA
                        copiasFake[0].transform.position = new Vector2(copiasFake[0].transform.position.x + 0.15f * Time.deltaTime, copiasFake[0].transform.position.y + 0.15f * Time.deltaTime);
                        copiasFake[1].transform.position = new Vector2(copiasFake[1].transform.position.x - 0.15f * Time.deltaTime, copiasFake[1].transform.position.y + 0.15f * Time.deltaTime);
                        copia.transform.position = new Vector2(copia.transform.position.x, copia.transform.position.y - 9.4f * Time.deltaTime);
                    }
                }
            }
        }
        if (copia.transform.position.x <= -10.55f || copia.transform.position.y >= 10.55f || copia.transform.position.y >= 7.01f || copia.transform.position.y <= -7.01f)
        {
            foreach (GameObject item in copiasFake)
            {
                Destroy(item);
            }
            Destroy(copia);
            hayFruta = false;
            r1 = false;
            r2 = false;
            r3 = false;
            r4 = false;
            r5 = false;
            r6 = false;
            r7 = false;
            r8 = false;
            fake = false;
            Miss();
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
        if (FindObjectOfType<BatController>().points > 0)
        {
            if (FindObjectOfType<BatController>().points == 5 || FindObjectOfType<BatController>().points == 10 || FindObjectOfType<BatController>().points == 15 || FindObjectOfType<BatController>().points == 20 || FindObjectOfType<BatController>().points == 25)
                level--;
            FindObjectOfType<BatController>().points--;
            score.text = "Score: " + FindObjectOfType<BatController>().points;
        }
        vidas--;
    }
}
