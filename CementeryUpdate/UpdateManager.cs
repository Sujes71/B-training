using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpdateManager : MonoBehaviour
{
    public GameObject[] objectsToInstantiate;
    public GameObject timer1, clon, abajo, derecha, izquierda, camara, captura, panel, vida1, vida2, vida3, message, final;
    public TextMeshProUGUI puntos, nivelText;
    private GameObject copiaTimer1, copiaRespuesta, copiaFake, copiaOption;
    public float tiempoInterval = 5, tiempoPanel = 2, tiempoNivel = 3, tiempoFinal = 5;
    public bool timerInstantiate = false, objectIsInstantiated = false, objectFakeIsInstantiated, objectOptionIsInstantiated, panelRojo = false, panelVerde = false, primerelse = false, segundoelse = false;
    public static bool UpdateGame;
    public int rnd = -1, rndFake = -1, rndOption = -1, rndOptionFake = -1, vidas = 3, score, nivel;

    void Start()
    {
        nivel = 1;
        UpdateGame = true;
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
            if (tiempoNivel >= 0)
            {
                nivelText.gameObject.SetActive(true);
                tiempoNivel -= Time.deltaTime;
            }
            else
            {
                nivelText.gameObject.SetActive(false);
                Desplazamientos();
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
                    if (ProfileStorage.s_currentProfile.cementeryScoreP < score)
                    {
                        ProfileStorage.StoragePlayerProfile("cementery", score, nivel);
                    }
                }
                else
                {
                    if (ProfileStorage.s_currentProfile.cementeryScore < score)
                    {
                        ProfileStorage.StoragePlayerProfile("cementery", score, nivel);
                    }   
                }
                UpdateGame = false;
                Debug.Log("Juego Terminado");
                SceneManager.LoadScene("JuegoUpdate");
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
                copiaTimer1 = Instantiate(timer1);
                copiaTimer1.transform.parent = clon.transform;
                copiaTimer1.transform.localPosition = timer1.transform.position;
            }
        }
        else
        {
            if (!objectIsInstantiated)
            {
                Destroy(copiaTimer1);
                objectIsInstantiated = true;
                timerInstantiate = false;
                objectFakeIsInstantiated = false;
                rnd = Random.Range(0, 9);
                copiaRespuesta = Instantiate(objectsToInstantiate[rnd]);
                
            }
            else if(ObjectDestructionManager.muerto || primerelse)
            {

                if (!objectFakeIsInstantiated)
                {
                    objectFakeIsInstantiated = true;
                    objectOptionIsInstantiated = false;
                    primerelse = true;
                    ObjectDestructionManager.muerto = false;
                    CrearFake();
                }
                else if(ObjectDestructionManager.muerto || segundoelse)
                {
                    if (!objectOptionIsInstantiated)
                    {
                        segundoelse = true;
                        captura.SetActive(true);
                        ObjectDestructionManager.muerto = false;
                        objectOptionIsInstantiated = true;
                        message.SetActive(true);
                        message.GetComponent<Animator>().enabled = true;
                        camara.GetComponent<Animator>().SetBool("Zoom", false);
                        crearOpcion();
                    }
                    else
                    {
                        if (!ObjectDestructionManager.muerto)
                        {
                            if (Input.GetKey(KeyCode.Space))
                            {
                                camara.GetComponent<Animator>().SetBool("Zoom", true);
                                if (rndOption == 2 || rndOption == 3)
                                {
                                    Miss();
                                }
                                else
                                {
                                    panelVerde = true;
                                    score++;
                                    puntos.text = "Score: " + score;
                                    LevelSystem();
                                }
                                Reset();
                            }
                            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                            {
                                if (rndOption == 1)
                                {
                                    Miss();
                                }
                                else
                                {
                                    panelVerde = true;
                                    score++;
                                    puntos.text = "Score: " + score;
                                    LevelSystem();
                                }
                                Reset();
                            }
                        }
                        else
                        {
                            if (rndOption == 1)
                            {
                                Miss();
                            }
                            else
                            {
                                panelVerde = true;
                                score++;
                                puntos.text = "Score: " + score;
                                LevelSystem();
                            }
                            Reset();
                        }
                    }
                }
            }
        }
    }
    void CrearFake()
    {
        rndFake = Random.Range(0, 9);
        while(rndFake == rnd)
            rndFake = Random.Range(0, 9);

        copiaFake = Instantiate(objectsToInstantiate[rndFake]);
    }
    void crearOpcion()
    {
        rndOptionFake = Random.Range(0, 9);
        rndOption = Random.Range(1, 3);
        if (JugarManager.toggleValue)
        {
            if (rnd == 0)
                rndOptionFake = 2;
            else if (rnd == 1)
                rndOptionFake = 2;
            else if (rnd == 2)
                rndOptionFake = 0;
            else if (rnd == 3)
                rndOptionFake = 4;
            else if (rnd == 4)
                rndOptionFake = 3;
            else if (rnd == 5)
                rndOptionFake = 6;
            else if (rnd == 6)
                rndOptionFake = 5;
            else if (rnd == 7)
                rndOptionFake = 8;
            else if (rnd == 8)
                rndOptionFake = 7;
        }
        else
        {
            while (rndOptionFake == rnd)
                rndOptionFake = Random.Range(0, 9);

            if (nivel >= 3)
            {
                if (rnd == 0)
                    rndOptionFake = 2;
                else if (rnd == 1)
                    rndOptionFake = 2;
                else if (rnd == 2)
                    rndOptionFake = 0;
                else if (rnd == 3)
                    rndOptionFake = 4;
                else if (rnd == 4)
                    rndOptionFake = 3;
                else if (rnd == 5)
                    rndOptionFake = 6;
                else if (rnd == 6)
                    rndOptionFake = 5;
                else if (rnd == 7)
                    rndOptionFake = 8;
                else if (rnd == 8)
                    rndOptionFake = 7;
            }
        }  
        if (rndOption == 1)
        {
            copiaOption = Instantiate(objectsToInstantiate[rnd]);
        } 
        else
        {
            copiaOption = Instantiate(objectsToInstantiate[rndOptionFake]);
        }
    }
    private void Reset()
    {
        if (copiaOption != null)
            Destroy(copiaOption);
        primerelse = false;
        segundoelse = false;
        rnd = -1;
        rndFake = -1;
        rndOption = -1;
        rndOptionFake = -1;
        ObjectDestructionManager.muerto = false;
        objectIsInstantiated = false;
        objectOptionIsInstantiated = false;
        tiempoInterval = 3;
        objectFakeIsInstantiated = false;
        captura.SetActive(false);
        message.SetActive(false);
        message.GetComponent<Animator>().enabled = false;
    }
    void Desplazamientos()
    {
        //modo pesadilla
        if (JugarManager.toggleValue)
        {
            if (nivel == 1)
            {
                switch (rnd)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6 * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6 * Time.deltaTime);
                        break;
                }
                switch (rndFake)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6 * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6 * Time.deltaTime);
                        break;
                }
                if (rndOption == 1)
                {
                    switch (rnd)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6 * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6 * Time.deltaTime);
                            break;
                    }
                }
                else if (rndOption == 2)
                {
                    switch (rndOptionFake)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6 * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6 * Time.deltaTime);
                            break;
                    }
                }
            }
            else if (nivel == 2)
            {
                switch (rnd)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7 * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7 * Time.deltaTime);
                        break;
                }
                switch (rndFake)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7 * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7 * Time.deltaTime);
                        break;
                }
                if (rndOption == 1)
                {
                    switch (rnd)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7 * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7 * Time.deltaTime);
                            break;
                    }
                }
                else if (rndOption == 2)
                {
                    switch (rndOptionFake)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7 * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7 * Time.deltaTime);
                            break;
                    }
                }
            }
            else if (nivel == 3)
            {
                switch (rnd)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8 * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8 * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8 * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8 * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8 * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8 * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8 * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 8 * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 8 * Time.deltaTime);
                        break;
                }
                switch (rndFake)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8 * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8 * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8 * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8 * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8 * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8 * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8 * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 8 * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 8 * Time.deltaTime);
                        break;
                }
                if (rndOption == 1)
                {
                    switch (rnd)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8 * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8 * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8 * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8 * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8 * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8 * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8 * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 8 * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 8 * Time.deltaTime);
                            break;
                    }
                }
                else if (rndOption == 2)
                {
                    switch (rndOptionFake)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8 * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8 * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8 * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8 * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8 * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8 * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8 * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 8 * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 8 * Time.deltaTime);
                            break;
                    }
                }
            }
            else if (nivel == 4)
            {
                switch (rnd)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 9 * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 9 * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 9 * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 9 * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 9 * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 9 * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 9 * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 9 * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 9 * Time.deltaTime);
                        break;
                }
                switch (rndFake)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 9 * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 9 * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 9 * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 9 * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 9 * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 9 * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 9 * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 9 * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 9 * Time.deltaTime);
                        break;
                }
                if (rndOption == 1)
                {
                    switch (rnd)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 9 * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 9 * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 9 * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 9 * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 9 * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 9 * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 9 * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 9 * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 9 * Time.deltaTime);
                            break;
                    }
                }
                else if (rndOption == 2)
                {
                    switch (rndOptionFake)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 9 * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 9 * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 9 * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 9 * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 9 * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 9 * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 9 * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 9 * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 9 * Time.deltaTime);
                            break;
                    }
                }
            }
            else if (nivel == 5)
            {
                switch (rnd)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10 * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10 * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10 * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10 * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10 * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10 * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10 * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 10 * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 10 * Time.deltaTime);
                        break;
                }
                switch (rndFake)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10 * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10 * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10 * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10 * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10 * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10 * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10 * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 10 * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 10 * Time.deltaTime);
                        break;
                }
                if (rndOption == 1)
                {
                    switch (rnd)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10 * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10 * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10 * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10 * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10 * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10 * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10 * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 10 * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 10 * Time.deltaTime);
                            break;
                    }
                }
                else if (rndOption == 2)
                {
                    switch (rndOptionFake)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10 * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10 * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10 * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10 * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10 * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10 * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10 * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 10 * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 10 * Time.deltaTime);
                            break;
                    }
                }
            }
            else if (nivel == 6)
            {
                switch (rnd)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 12f * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 12f * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 12f * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 12f * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 12f * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 12f * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 12f * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 12f * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 12f * Time.deltaTime);
                        break;
                }
                switch (rndFake)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 12f * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 12f * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 12f * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 12f * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 12f * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 12f * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 12f * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 12f * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 12f * Time.deltaTime);
                        break;
                }
                if (rndOption == 1)
                {
                    switch (rnd)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 12f * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 12f * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 12f * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 12f * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 12f * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 12f * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 12f * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 12f * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 12f * Time.deltaTime);
                            break;
                    }
                }
                else if (rndOption == 2)
                {
                    switch (rndOptionFake)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 12f * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 12f * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 12f * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 12f * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 12f * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 12f * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 12f * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 12f * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 12f * Time.deltaTime);
                            break;
                    }
                }
            }
        }
        //No modo pesadilla
        else
        {
            if (nivel == 1)
            {
                switch (rnd)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5 * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5 * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5 * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5 * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5 * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5 * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5 * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 5 * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 5 * Time.deltaTime);
                        break;
                }
                switch (rndFake)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5 * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5 * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5 * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5 * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5 * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5 * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5 * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 5 * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 5 * Time.deltaTime);
                        break;
                }
                if (rndOption == 1)
                {
                    switch (rnd)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5 * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5 * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5 * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5 * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5 * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5 * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5 * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 5 * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 5 * Time.deltaTime);
                            break;
                    }
                }
                else if (rndOption == 2)
                {
                    switch (rndOptionFake)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5 * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5 * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5 * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5 * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5 * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5 * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 5 * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 5 * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 5 * Time.deltaTime);
                            break;
                    }
                }
            }
            else if (nivel == 2)
            {
                switch (rnd)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6 * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6 * Time.deltaTime);
                        break;
                }
                switch (rndFake)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6 * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6 * Time.deltaTime);
                        break;
                }
                if (rndOption == 1)
                {
                    switch (rnd)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6 * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6 * Time.deltaTime);
                            break;
                    }
                }
                else if (rndOption == 2)
                {
                    switch (rndOptionFake)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6 * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6 * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6 * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6 * Time.deltaTime);
                            break;
                    }
                }
            }
            else if (nivel == 3)
            {
                switch (rnd)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6.5f * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6.5f * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6.5f * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6.5f * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6.5f * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6.5f * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6.5f * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6.5f * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6.5f * Time.deltaTime);
                        break;
                }
                switch (rndFake)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6.5f * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6.5f * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6.5f * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6.5f * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6.5f * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6.5f * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6.5f * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6.5f * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6.5f * Time.deltaTime);
                        break;
                }
                if (rndOption == 1)
                {
                    switch (rnd)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6.5f * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6.5f * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6.5f * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6.5f * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6.5f * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6.5f * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6.5f * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6.5f * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6.5f * Time.deltaTime);
                            break;
                    }
                }
                else if (rndOption == 2)
                {
                    switch (rndOptionFake)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6.5f * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6.5f * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6.5f * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6.5f * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 6.5f * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6.5f * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 6.5f * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6.5f * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 6.5f * Time.deltaTime);
                            break;
                    }
                }
            }
            else if (nivel == 4)
            {
                switch (rnd)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7 * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7 * Time.deltaTime);
                        break;
                }
                switch (rndFake)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7 * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7 * Time.deltaTime);
                        break;
                }
                if (rndOption == 1)
                {
                    switch (rnd)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7 * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7 * Time.deltaTime);
                            break;
                    }
                }
                else if (rndOption == 2)
                {
                    switch (rndOptionFake)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7 * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7 * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7 * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7 * Time.deltaTime);
                            break;
                    }
                }
            }
            else if (nivel == 5)
            {
                switch (rnd)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7.5f * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7.5f * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7.5f * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7.5f * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7.5f * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7.5f * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7.5f * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7.5f * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7.5f * Time.deltaTime);
                        break;
                }
                switch (rndFake)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7.5f * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7.5f * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7.5f * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7.5f * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7.5f * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7.5f * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7.5f * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7.5f * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7.5f * Time.deltaTime);
                        break;
                }
                if (rndOption == 1)
                {
                    switch (rnd)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7.5f * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7.5f * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7.5f * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7.5f * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7.5f * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7.5f * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7.5f * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7.5f * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7.5f * Time.deltaTime);
                            break;
                    }
                }
                else if (rndOption == 2)
                {
                    switch (rndOptionFake)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7.5f * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7.5f * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7.5f * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7.5f * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7.5f * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7.5f * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7.5f * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7.5f * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 7.5f * Time.deltaTime);
                            break;
                    }
                }
            }
            else if (nivel == 6)
            {
                switch (rnd)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8.5f);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8.5f * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8.5f * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8.5f * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8.5f * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8.5f * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8.5f * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 8.5f * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaRespuesta != null)
                            copiaRespuesta.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 8.5f * Time.deltaTime);
                        break;
                }
                switch (rndFake)
                {
                    case 0:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8.5f * Time.deltaTime);
                        break;
                    case 1:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8.5f * Time.deltaTime);
                        break;
                    case 2:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8.5f * Time.deltaTime);
                        break;
                    case 3:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8.5f * Time.deltaTime);
                        break;
                    case 4:
                        derecha.SetActive(false);
                        izquierda.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8.5f * Time.deltaTime);
                        break;
                    case 5:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8.5f * Time.deltaTime);
                        break;
                    case 6:
                        izquierda.SetActive(false);
                        derecha.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8.5f * Time.deltaTime);
                        break;
                    case 7:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 8.5f * Time.deltaTime);
                        break;
                    case 8:
                        abajo.SetActive(true);
                        if (copiaFake != null)
                            copiaFake.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 8.5f * Time.deltaTime);
                        break;
                }
                if (rndOption == 1)
                {
                    switch (rnd)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8.5f * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8.5f * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8.5f * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8.5f * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8.5f * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8.5f * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8.5f * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 8.5f * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 8.5f * Time.deltaTime);
                            break;
                    }
                }
                else if (rndOption == 2)
                {
                    switch (rndOptionFake)
                    {
                        case 0:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8.5f * Time.deltaTime);
                            break;
                        case 1:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8.5f * Time.deltaTime);
                            break;
                        case 2:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8.5f * Time.deltaTime);
                            break;
                        case 3:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8.5f * Time.deltaTime);
                            break;
                        case 4:
                            derecha.SetActive(false);
                            izquierda.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 8.5f * Time.deltaTime);
                            break;
                        case 5:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8.5f * Time.deltaTime);
                            break;
                        case 6:
                            izquierda.SetActive(false);
                            derecha.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 8.5f * Time.deltaTime);
                            break;
                        case 7:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 8.5f * Time.deltaTime);
                            break;
                        case 8:
                            abajo.SetActive(true);
                            if (copiaOption != null)
                                copiaOption.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 8.5f * Time.deltaTime);
                            break;
                    }
                }
            }
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
