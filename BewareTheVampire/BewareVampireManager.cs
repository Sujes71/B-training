using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BewareVampireManager : MonoBehaviour
{
    public GameObject[] objectsToInstantiate;
    public Vector2[] posiciones;
    public GameObject door, panel, vida1, vida2, vida3, final;
    public TextMeshProUGUI nivelText, puntos;
    public int i, pos;
    public bool objectIsInstantiate = false, fakeVampire = false, panelRojo = false, panelVerde = false, colision = false;
    public static bool BewareVampire;

    private GameObject[] selecciones;
    public int rnd, nivel, vidas, score, auxiliar, rndFake;
    private float tiempoPanel = 2, tiempoNivel = 3, tiempoFinal = 5;
    // Start is called before the first frame update
    void Start()
    {
        BewareVampire = true;
        score = 0;
        vidas = 3;
        i = 0;
        pos = 0;
        nivel = 1;
        selecciones = new GameObject[5];
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
                nivelText.gameObject.SetActive(false);
                Comienzo();
                if (objectIsInstantiate)
                {
                    Desplazamientos();
                    Check();
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
                    if (ProfileStorage.s_currentProfile.theVampireScoreP < score)
                    {
                        ProfileStorage.StoragePlayerProfile("theVampire", score, nivel);
                    }  
                }
                else
                {
                    if (ProfileStorage.s_currentProfile.theVampireScore < score)
                    {
                        ProfileStorage.StoragePlayerProfile("theVampire", score, nivel);
                    }
                }
                BewareVampire = false;
                Debug.Log("Juego Terminado");
                SceneManager.LoadScene("JuegoVampiro");
            }
        }
    }
    void Comienzo()
    {
        if (!objectIsInstantiate)
        {
            rnd = Random.Range(0, 5);
            if (rnd == 4 || i == 4)
            {
                rndFake = Random.Range(0, 4);
                if (JugarManager.toggleValue)
                {
                    if(nivel >= 3)
                    {
                        switch (rndFake)
                        {
                            case 0:
                                selecciones[i] = Instantiate(objectsToInstantiate[0]);
                                auxiliar = 0;
                                break;
                            case 1:
                                selecciones[i] = Instantiate(objectsToInstantiate[4]);
                                auxiliar = 4;
                                break;
                            case 2:
                                selecciones[i] = Instantiate(objectsToInstantiate[8]);
                                auxiliar = 8;
                                break;
                            case 3:
                                selecciones[i] = Instantiate(objectsToInstantiate[12]);
                                auxiliar = 12;
                                break;
                        }
                        fakeVampire = true;
                    }
                    else
                    {
                        switch (rndFake)
                        {
                            case 0:
                                selecciones[i] = Instantiate(objectsToInstantiate[3]);
                                auxiliar = 3;
                                break;
                            case 1:
                                selecciones[i] = Instantiate(objectsToInstantiate[7]);
                                auxiliar = 7;
                                break;
                            case 2:
                                selecciones[i] = Instantiate(objectsToInstantiate[11]);
                                auxiliar = 11;
                                break;
                            case 3:
                                selecciones[i] = Instantiate(objectsToInstantiate[15]);
                                auxiliar = 15;
                                break;
                        }
                    }
                }
                else
                {
                    if (nivel == 1)
                    {
                        selecciones[i] = Instantiate(objectsToInstantiate[16]);
                        auxiliar = 16;
                    }
                    else if (nivel == 2)
                    {
                        switch (rndFake)
                        {
                            case 0:
                                selecciones[i] = Instantiate(objectsToInstantiate[1]);
                                auxiliar = 1;
                                break;
                            case 1:
                                selecciones[i] = Instantiate(objectsToInstantiate[5]);
                                auxiliar = 5;
                                break;
                            case 2:
                                selecciones[i] = Instantiate(objectsToInstantiate[9]);
                                auxiliar = 9;
                                break;
                            case 3:
                                selecciones[i] = Instantiate(objectsToInstantiate[13]);
                                auxiliar = 13;
                                break;
                        }
                    }
                    else if (nivel == 3)
                    {
                        switch (rndFake)
                        {
                            case 0:
                                selecciones[i] = Instantiate(objectsToInstantiate[2]);
                                auxiliar = 2;
                                break;
                            case 1:
                                selecciones[i] = Instantiate(objectsToInstantiate[6]);
                                auxiliar = 6;
                                break;
                            case 2:
                                selecciones[i] = Instantiate(objectsToInstantiate[10]);
                                auxiliar = 10;
                                break;
                            case 3:
                                selecciones[i] = Instantiate(objectsToInstantiate[14]);
                                auxiliar = 14;
                                break;
                        }
                    }
                    else if (nivel == 4)
                    {
                        switch (rndFake)
                        {
                            case 0:
                                selecciones[i] = Instantiate(objectsToInstantiate[3]);
                                auxiliar = 3;
                                break;
                            case 1:
                                selecciones[i] = Instantiate(objectsToInstantiate[7]);
                                auxiliar = 7;
                                break;
                            case 2:
                                selecciones[i] = Instantiate(objectsToInstantiate[11]);
                                auxiliar = 11;
                                break;
                            case 3:
                                selecciones[i] = Instantiate(objectsToInstantiate[15]);
                                auxiliar = 15;
                                break;
                        }
                    }
                    else
                    {
                        switch (rndFake)
                        {
                            case 0:
                                selecciones[i] = Instantiate(objectsToInstantiate[0]);
                                auxiliar = 0;
                                break;
                            case 1:
                                selecciones[i] = Instantiate(objectsToInstantiate[4]);
                                auxiliar = 4;
                                break;
                            case 2:
                                selecciones[i] = Instantiate(objectsToInstantiate[8]);
                                auxiliar = 8;
                                break;
                            case 3:
                                selecciones[i] = Instantiate(objectsToInstantiate[12]);
                                auxiliar = 12;
                                break;
                        }
                        fakeVampire = true;
                    }
                }
            }
            else
            {
                switch (rnd)
                {
                    case 0:
                        selecciones[i] = Instantiate(objectsToInstantiate[0]);
                        auxiliar = 0;
                        break;
                    case 1:
                        selecciones[i] = Instantiate(objectsToInstantiate[4]);
                        auxiliar = 4;
                        break;
                    case 2:
                        selecciones[i] = Instantiate(objectsToInstantiate[8]);
                        auxiliar = 8;
                        break;
                    case 3:
                        selecciones[i] = Instantiate(objectsToInstantiate[12]);
                        auxiliar = 12;
                        break;
                }
            }
            objectIsInstantiate = true;
        }
    }
    void Desplazamientos()
    {
        if (objectIsInstantiate)
        {
            if (selecciones[i] != null)
            {
                if (JugarManager.toggleValue)
                {
                    if (nivel == 1)
                    {
                        if (!selecciones[i].GetComponent<Animator>().GetBool("go") && !selecciones[i].GetComponent<Animator>().GetBool("stay"))
                            selecciones[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(7f * Time.deltaTime, 0));

                        else if (selecciones[i].GetComponent<Animator>().GetBool("go"))
                            selecciones[i].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 7f * Time.deltaTime);
                        if (selecciones[i].transform.position.x >= 0.61f)
                        {
                            if (Input.GetKey(KeyCode.RightArrow))
                                selecciones[i].GetComponent<Rigidbody2D>().AddForce(Vector2.right * 20f * Time.deltaTime);
                        }
                    }
                    else if (nivel == 2 || nivel == 3)
                    {
                        if (!selecciones[i].GetComponent<Animator>().GetBool("go") && !selecciones[i].GetComponent<Animator>().GetBool("stay"))
                            selecciones[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(9f * Time.deltaTime, 0));

                        else if (selecciones[i].GetComponent<Animator>().GetBool("go"))
                            selecciones[i].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 9f * Time.deltaTime);
                        if (selecciones[i].transform.position.x >= 0.61f)
                        {
                            if (Input.GetKey(KeyCode.RightArrow))
                                selecciones[i].GetComponent<Rigidbody2D>().AddForce(Vector2.right * 20f * Time.deltaTime);
                        }
                    }
                    else if (nivel == 4)
                    {
                        if (!selecciones[i].GetComponent<Animator>().GetBool("go") && !selecciones[i].GetComponent<Animator>().GetBool("stay"))
                            selecciones[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(12f * Time.deltaTime, 0));

                        else if (selecciones[i].GetComponent<Animator>().GetBool("go"))
                            selecciones[i].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 12f * Time.deltaTime);
                        if (selecciones[i].transform.position.x >= 0.61f)
                        {
                            if (Input.GetKey(KeyCode.RightArrow))
                                selecciones[i].GetComponent<Rigidbody2D>().AddForce(Vector2.right * 20f * Time.deltaTime);
                        }
                    }
                    else if (nivel == 5)
                    {
                        if (!selecciones[i].GetComponent<Animator>().GetBool("go") && !selecciones[i].GetComponent<Animator>().GetBool("stay"))
                            selecciones[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(13f * Time.deltaTime, 0));

                        else if (selecciones[i].GetComponent<Animator>().GetBool("go"))
                            selecciones[i].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 13f * Time.deltaTime);
                        if (selecciones[i].transform.position.x >= 0.61f)
                        {
                            if (Input.GetKey(KeyCode.RightArrow))
                                selecciones[i].GetComponent<Rigidbody2D>().AddForce(Vector2.right * 20f * Time.deltaTime);
                        }
                    }
                    else if (nivel == 6)
                    {
                        if (!selecciones[i].GetComponent<Animator>().GetBool("go") && !selecciones[i].GetComponent<Animator>().GetBool("stay"))
                            selecciones[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(15f * Time.deltaTime, 0));

                        else if (selecciones[i].GetComponent<Animator>().GetBool("go"))
                            selecciones[i].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 15f * Time.deltaTime);
                        if (selecciones[i].transform.position.x >= 0.61f)
                        {
                            if (Input.GetKey(KeyCode.RightArrow))
                                selecciones[i].GetComponent<Rigidbody2D>().AddForce(Vector2.right * 20f * Time.deltaTime);
                        }
                    }
                }
                else
                {
                    if(nivel == 1)
                    {
                        if (!selecciones[i].GetComponent<Animator>().GetBool("go") && !selecciones[i].GetComponent<Animator>().GetBool("stay"))
                            selecciones[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(5f * Time.deltaTime, 0));

                        else if (selecciones[i].GetComponent<Animator>().GetBool("go"))
                            selecciones[i].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5f * Time.deltaTime);
                        if (selecciones[i].transform.position.x >= 0.61f)
                        {
                            if (Input.GetKey(KeyCode.RightArrow))
                                selecciones[i].GetComponent<Rigidbody2D>().AddForce(Vector2.right * 20f * Time.deltaTime);
                        }
                    }
                    else if(nivel == 2 || nivel == 3)
                    {
                        if (!selecciones[i].GetComponent<Animator>().GetBool("go") && !selecciones[i].GetComponent<Animator>().GetBool("stay"))
                            selecciones[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(6f * Time.deltaTime, 0));

                        else if (selecciones[i].GetComponent<Animator>().GetBool("go"))
                            selecciones[i].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 6f * Time.deltaTime);
                        if (selecciones[i].transform.position.x >= 0.61f)
                        {
                            if (Input.GetKey(KeyCode.RightArrow))
                                selecciones[i].GetComponent<Rigidbody2D>().AddForce(Vector2.right * 20f * Time.deltaTime);
                        }
                    }
                    else if (nivel == 4)
                    {
                        if (!selecciones[i].GetComponent<Animator>().GetBool("go") && !selecciones[i].GetComponent<Animator>().GetBool("stay"))
                            selecciones[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(8f * Time.deltaTime, 0));

                        else if (selecciones[i].GetComponent<Animator>().GetBool("go"))
                            selecciones[i].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 8f * Time.deltaTime);
                        if (selecciones[i].transform.position.x >= 0.61f)
                        {
                            if (Input.GetKey(KeyCode.RightArrow))
                                selecciones[i].GetComponent<Rigidbody2D>().AddForce(Vector2.right * 20f * Time.deltaTime);
                        }
                    }
                    else if (nivel == 5)
                    {
                        if (!selecciones[i].GetComponent<Animator>().GetBool("go") && !selecciones[i].GetComponent<Animator>().GetBool("stay"))
                            selecciones[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(10f * Time.deltaTime, 0));

                        else if (selecciones[i].GetComponent<Animator>().GetBool("go"))
                            selecciones[i].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10f * Time.deltaTime);
                        if (selecciones[i].transform.position.x >= 0.61f)
                        {
                            if (Input.GetKey(KeyCode.RightArrow))
                                selecciones[i].GetComponent<Rigidbody2D>().AddForce(Vector2.right * 20f * Time.deltaTime);
                        }
                    }
                    else if (nivel == 6)
                    {
                        if (!selecciones[i].GetComponent<Animator>().GetBool("go") && !selecciones[i].GetComponent<Animator>().GetBool("stay"))
                            selecciones[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(12f * Time.deltaTime, 0));

                        else if (selecciones[i].GetComponent<Animator>().GetBool("go"))
                            selecciones[i].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 12f * Time.deltaTime);
                        if (selecciones[i].transform.position.x >= 0.61f)
                        {
                            if (Input.GetKey(KeyCode.RightArrow))
                                selecciones[i].GetComponent<Rigidbody2D>().AddForce(Vector2.right * 20f * Time.deltaTime);
                        }
                    }
                }
            }
        }
    }
    void Check()
    {
        if (!colision)
        {
            if(fakeVampire && selecciones[i].transform.position.x >= -1.34f)
            {
                GameObject aux = selecciones[i];
                Destroy(selecciones[i]);
                selecciones[i] = Instantiate(objectsToInstantiate[16], aux.transform.position, aux.transform.rotation);
                auxiliar = 16;
                fakeVampire = false;
            }
            if (selecciones[i].transform.position.x >= -0.72f && selecciones[i].transform.position.x <= 0.61f)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                     if (auxiliar == 1 || auxiliar == 2 || auxiliar == 3 || auxiliar == 5 || auxiliar == 6 || auxiliar == 7 || auxiliar == 9 || auxiliar == 10 || auxiliar == 11 || auxiliar == 13 || auxiliar == 14 || auxiliar == 15)
                    {
                        GameObject aux = selecciones[i];
                        Destroy(selecciones[i]);
                        selecciones[i] = Instantiate(objectsToInstantiate[16], aux.transform.position, aux.transform.rotation);
                    }
                    selecciones[i].GetComponent<Animator>().SetBool("go", true);
                    door.GetComponent<Animator>().SetBool("door", true);
                }
            }
            if (selecciones[i].transform.position.y >= -1.492649f)
            {
                selecciones[i].GetComponent<Animator>().SetBool("go", false);
                if(auxiliar != 16)
                    selecciones[i].GetComponent<Animator>().SetBool("stay", true);
                door.GetComponent<Animator>().SetBool("door", false);

                if (auxiliar == 1 || auxiliar == 2 || auxiliar == 3 || auxiliar == 5 || auxiliar == 6 || auxiliar == 7 || auxiliar == 9 || auxiliar == 10 || auxiliar == 11 || auxiliar == 13 || auxiliar == 14 || auxiliar == 15 || auxiliar == 16)
                {
                    i++;
                    Miss();
                    Reset();
                    objectIsInstantiate = false;
                }
                else
                {
                    selecciones[i].GetComponent<BoxCollider2D>().enabled = false;
                    selecciones[i].transform.position = posiciones[pos];
                    i++;
                    pos++;
                    score++;
                    panelVerde = true;
                    puntos.text = "Score: " + score;
                    LevelSystem();
                    objectIsInstantiate = false;
                }
            }
        }
        else
        {
            if (auxiliar == 1 || auxiliar == 2 || auxiliar == 3 || auxiliar == 5 || auxiliar == 6 || auxiliar == 7 || auxiliar == 9 || auxiliar == 10 || auxiliar == 11 || auxiliar == 13 || auxiliar == 14 || auxiliar == 15 || auxiliar == 16)
            {
                panelVerde = true;
                score++;
                i++;
                puntos.text = "Score: " + score;
                LevelSystem();
                Reset();
                objectIsInstantiate = false;
            }
            else
            {
                i++;
                Miss();
                objectIsInstantiate = false;
            }
            if (i == 5)
                Reset();
        }
        colision = false;
    }
    private void Reset()
    {
        foreach (GameObject item in selecciones)
        {
            if (item != null)
                Destroy(item);
        }
        selecciones = new GameObject[5];
        i = 0;
        pos = 0;
        colision = false;
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
}
