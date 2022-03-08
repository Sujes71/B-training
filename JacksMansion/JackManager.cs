using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JackManager : MonoBehaviour
{
    public GameObject[] objetsToInstantiate, corazones;
    public GameObject figura, figura1, figura2, temp, panel, score, bocadillo, final, nivel;
    public Button boton1, boton2, boton3;
    public static bool JacksCastle;
    public TextMeshProUGUI textoBocadillo, nivelText;

    private GameObject[] respuesta, alternativa_1, alternativa_2;
    private bool anObjectIsInstantiate = false, opcionesGeneradas = false, A = false, B = false, C = false, panelRojo = false, panelVerde = false, vuelta = false;
    public int rnd, rndObject, vidas = 3, aciertos = 0, level;
    private float rndX, rndY, tiempoPanel = 2, tiempoAnimacion = 2, tiempoAux = 5, tiempoNivel = 3;
    private float interval, contador;
    private List<int> randoms;
    private List<float> randomsPositionX, randomsPositionY;

    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        JackController.inicio = false;
        JacksCastle = true;
        rnd = Random.Range(5, 7);
        respuesta = new GameObject[rnd];
        alternativa_1 = new GameObject[rnd];
        alternativa_2 = new GameObject[rnd];
        randoms = new List<int>();
        randomsPositionX = new List<float>();
        randomsPositionY = new List<float>();
        if (JugarManager.toggleValue)
        {
            contador = 3;
            interval = 3;
        }
        else
        {
            contador = 5;
            interval = 5;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (vidas > 0)
        {
            if (panelRojo)
                RedScreen();
            if (panelVerde)
                GreenScreen();
            if (vuelta)
            {
                if (tiempoAnimacion >= 0)
                    tiempoAnimacion -= Time.deltaTime;
                else
                {
                    bocadillo.SetActive(false);
                    FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Enfado", false);
                    FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Sorpresa", false);
                    FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Desafio", false);
                    FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Burla", false);
                    vuelta = false;
                    tiempoAnimacion = 2;
                }
            }
            else
            {
                if (CartelController.finalize)
                {
                    if (contador >= 0)
                    {
                        contador -= Time.deltaTime;
                        temp.SetActive(true);
                        if (tiempoAnimacion >= 0)
                            tiempoAnimacion -= Time.deltaTime;
                        else
                        {
                            bocadillo.SetActive(true);
                            FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Burla", true);
                            textoBocadillo.text = "Suerte,la vas a necesitar...";
                            tiempoAnimacion = 2;
                        }
                        if (tiempoNivel >= 0)
                        {
                            nivel.SetActive(true);
                            tiempoNivel -= Time.deltaTime;
                        }
                        else
                        {
                            nivel.SetActive(false);
                        }
                        if (contador <= 1)
                            nivel.SetActive(false);
                    }
                    else
                    {
                        if (!anObjectIsInstantiate)
                        {
                            bocadillo.SetActive(false);
                            FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Burla", false);
                            if (JugarManager.toggleValue)
                                FindObjectOfType<Temporizador>().tiempo = 3;
                            else
                                FindObjectOfType<Temporizador>().tiempo = 5;

                            temp.SetActive(false);
                            LanzarCuestion();
                        }
                        else
                        {
                            if (FindObjectOfType<CartelController>().GetComponent<Animator>().GetBool("Cartel") == false)
                            {
                                if (interval >= 0)
                                {
                                    interval -= Time.deltaTime;
                                    temp.SetActive(true);

                                    if (tiempoAnimacion >= 0)
                                        tiempoAnimacion -= Time.deltaTime;
                                    else
                                    {
                                        bocadillo.SetActive(true);
                                        FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Desafio", true);
                                        textoBocadillo.text = "Serás capaz de recordar la figura?";
                                    }
                                }
                                else
                                {
                                    bocadillo.SetActive(false);
                                    FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Desafio", false);
                                    if (tiempoAux >= 0)
                                    {
                                        figura.SetActive(false);
                                        foreach (GameObject item in respuesta)
                                        {
                                            item.SetActive(false);
                                        }
                                        tiempoAux -= Time.deltaTime;
                                    }
                                    else
                                    {
                                        if (!opcionesGeneradas)
                                        {
                                            
                                            if (JugarManager.toggleValue)
                                                FindObjectOfType<Temporizador>().tiempo = 3;
                                            else
                                                FindObjectOfType<Temporizador>().tiempo = 5;
                                            temp.SetActive(false);

                                            figura.SetActive(true);
                                            foreach (GameObject item in respuesta)
                                            {
                                                item.SetActive(true);
                                            }
                                            GenerarOpciones();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
            
        else
        {
            bocadillo.SetActive(true);
            FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Burla", false);
            FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Enfado", false);
            FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Sorpresa", false);
            final.SetActive(true);
            if (tiempoAnimacion >= 0)
            {
                tiempoAnimacion -= Time.deltaTime;
                if (aciertos <= 5)
                {
                    FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Burla", true);
                    textoBocadillo.text = "Suerte la próxima vez!";
                }
               else if(aciertos <= 10)
                {
                    FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Desafio", true);
                    textoBocadillo.text = "Aún no es suficiente!";
                }
                else if (aciertos <= 15)
                {
                    FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Sorpresa", true);
                    textoBocadillo.text = "Me ha sorprendido tu habilidad!";
                }
                else if (aciertos >= 20)
                {
                    FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Enfado", true);
                    textoBocadillo.text = "Me has derrotado, me vengaré!";
                }
            } 
            else
            {
                if (JugarManager.toggleValue)
                {
                    if (ProfileStorage.s_currentProfile.jackScoreP < aciertos)
                    {
                        ProfileStorage.StoragePlayerProfile("jack", aciertos, level);
                    }
                        
                }
                else
                {
                    if (ProfileStorage.s_currentProfile.jackScore < aciertos)
                    {
                        ProfileStorage.StoragePlayerProfile("jack", aciertos, level);
                    }
                }
                JacksCastle = false;
                Debug.Log("Juego Terminado");
                SceneManager.LoadScene("JuegoJack");
            }
        }
        
    }
    void LanzarCuestion()
    {
        figura.GetComponent<SpriteRenderer>().enabled = true;
        for (int i = 0; i < rnd; i++)
        {
            rndObject = Random.Range(0, 3);
            rndX = Random.Range(-1.46f, 1.46f);
            rndY = Random.Range(0, -3.3f);

            randoms.Add(rndObject);
            randomsPositionX.Add(rndX);
            randomsPositionY.Add(rndY);

            respuesta[i] = Instantiate(objetsToInstantiate[rndObject].gameObject, new Vector2(rndX, rndY), objetsToInstantiate[rndObject].gameObject.transform.rotation);
        }
        anObjectIsInstantiate = true;
    }
    void GenerarOpciones()
    {
        for (int i = 0; i < rnd - 1; i++)
        {
            alternativa_1[i] = Instantiate(objetsToInstantiate[randoms[i]].gameObject, new Vector2(randomsPositionX[i], randomsPositionY[i]), objetsToInstantiate[randoms[i]].gameObject.transform.rotation);

            alternativa_2[i] = Instantiate(objetsToInstantiate[randoms[i]].gameObject, new Vector2(randomsPositionX[i], randomsPositionY[i]), objetsToInstantiate[randoms[i]].gameObject.transform.rotation);
        }
        int num = Random.Range(0, 3), opcionAleatoria = Random.Range(1, 4);
     
        while (num == randoms[rnd - 1])
        {
             num = Random.Range(0, 3);
        }
        alternativa_1[rnd - 1] = Instantiate(objetsToInstantiate[num].gameObject, new Vector2(randomsPositionX[rnd - 1], randomsPositionY[rnd - 1]), objetsToInstantiate[num].gameObject.transform.rotation);

        if (num == 0)
        {
            if (randoms[rnd - 1] !=  1)
            {
                alternativa_2[rnd - 1] = Instantiate(objetsToInstantiate[1].gameObject, new Vector2(randomsPositionX[rnd - 1], randomsPositionY[rnd - 1]), objetsToInstantiate[1].gameObject.transform.rotation);
            }
            else
            {
                alternativa_2[rnd - 1] = Instantiate(objetsToInstantiate[2].gameObject, new Vector2(randomsPositionX[rnd - 1], randomsPositionY[rnd - 1]), objetsToInstantiate[2].gameObject.transform.rotation);
            }
        }
        if (num == 1)
        {
            if(randoms[rnd - 1] != 0)
            {
                alternativa_2[rnd - 1] = Instantiate(objetsToInstantiate[0].gameObject, new Vector2(randomsPositionX[rnd - 1], randomsPositionY[rnd - 1]), objetsToInstantiate[0].gameObject.transform.rotation);
            }
            else
            {
                alternativa_2[rnd - 1] = Instantiate(objetsToInstantiate[2].gameObject, new Vector2(randomsPositionX[rnd - 1], randomsPositionY[rnd - 1]), objetsToInstantiate[2].gameObject.transform.rotation);

            }
        }
        if (num == 2)
        {
            if(randoms[rnd - 1] != 1)
            {
                alternativa_2[rnd - 1] = Instantiate(objetsToInstantiate[1].gameObject, new Vector2(randomsPositionX[rnd - 1], randomsPositionY[rnd - 1]), objetsToInstantiate[1].gameObject.transform.rotation);
            }
            else
            {
                alternativa_2[rnd - 1] = Instantiate(objetsToInstantiate[0].gameObject, new Vector2(randomsPositionX[rnd - 1], randomsPositionY[rnd - 1]), objetsToInstantiate[0].gameObject.transform.rotation);
            }
            
        }
        switch (opcionAleatoria)
        {
            case 1:
                Debug.Log("case1");
                figura.GetComponent<SpriteRenderer>().enabled = true;
                opcionAleatoria = Random.Range(2, 4);
                    foreach (GameObject objeto in respuesta)
                    {
                        objeto.transform.position = new Vector2(objeto.transform.position.x - 5, objeto.transform.position.y);
                    }
                A = true;
                switch (opcionAleatoria)
                {
                    case 2:
                            Debug.Log("case12");
                            figura1.GetComponent<SpriteRenderer>().enabled = true;
                            foreach (GameObject objeto in alternativa_1)
                            {
                            objeto.transform.position = new Vector2(objeto.transform.position.x + 5, objeto.transform.position.y);
                            }

                            figura2.GetComponent<SpriteRenderer>().enabled = true;
                            foreach (GameObject objeto in alternativa_2)
                            {
                            objeto.transform.position = new Vector2(objeto.transform.position.x, objeto.transform.position.y);
                            }
                        break;
                    case 3:
                            Debug.Log("case13");
                            figura1.GetComponent<SpriteRenderer>().enabled = true;
                            foreach (GameObject objeto in alternativa_1)
                            {
                            objeto.transform.position = new Vector2(objeto.transform.position.x, objeto.transform.position.y);
                            }
                           
                            figura2.GetComponent<SpriteRenderer>().enabled = true;
                            foreach (GameObject objeto in alternativa_2)
                            {
                            objeto.transform.position = new Vector2(objeto.transform.position.x + 5, objeto.transform.position.y);
                            }
                        break;
                    }
                    break;
            case 2:
                Debug.Log("case2");
                figura.GetComponent<SpriteRenderer>().enabled = true;
                opcionAleatoria = Random.Range(2, 4);
                foreach (GameObject objeto in respuesta)
                {
                    objeto.transform.position = new Vector2(objeto.transform.position.x, objeto.transform.position.y);
                }
                B = true;
                switch (opcionAleatoria)
                {
                    case 2:
                        Debug.Log("case22");
                        figura1.GetComponent<SpriteRenderer>().enabled = true;
                        foreach (GameObject objeto in alternativa_1)
                        {
                            objeto.transform.position = new Vector2(objeto.transform.position.x - 5, objeto.transform.position.y);
                        }

                        figura2.GetComponent<SpriteRenderer>().enabled = true;
                        foreach (GameObject objeto in alternativa_2)
                        {
                            objeto.transform.position = new Vector2(objeto.transform.position.x + 5, objeto.transform.position.y);
                        }
                        break;
                    case 3:
                        Debug.Log("case23");
                        figura1.GetComponent<SpriteRenderer>().enabled = true;
                        foreach (GameObject objeto in alternativa_1)
                        {
                            objeto.transform.position = new Vector2(objeto.transform.position.x + 5, objeto.transform.position.y);
                        }

                        figura2.GetComponent<SpriteRenderer>().enabled = true;
                        foreach (GameObject objeto in alternativa_2)
                        {
                            objeto.transform.position = new Vector2(objeto.transform.position.x - 5, objeto.transform.position.y);
                        }
                        break;
                    }
                    break;
            case 3:
                Debug.Log("case3");
                opcionAleatoria = Random.Range(2, 4);
                figura.GetComponent<SpriteRenderer>().enabled = true;
                foreach (GameObject objeto in respuesta)
                {
                    objeto.transform.position = new Vector2(objeto.transform.position.x + 5, objeto.transform.position.y);
                }
                C = true;
                switch (opcionAleatoria)
                {
                    case 2:
                        Debug.Log("case32");
                        figura1.GetComponent<SpriteRenderer>().enabled = true;
                        foreach (GameObject objeto in alternativa_1)
                        {
                            objeto.transform.position = new Vector2(objeto.transform.position.x, objeto.transform.position.y);
                        }

                        figura2.GetComponent<SpriteRenderer>().enabled = true;
                        foreach (GameObject objeto in alternativa_2)
                        {
                            objeto.transform.position = new Vector2(objeto.transform.position.x - 5, objeto.transform.position.y);
                        }
                        break;
                    case 3:
                        Debug.Log("case33");
                        figura1.GetComponent<SpriteRenderer>().enabled = true;
                        foreach (GameObject objeto in alternativa_1)
                        {
                            objeto.transform.position = new Vector2(objeto.transform.position.x - 5, objeto.transform.position.y);
                        }

                        figura2.GetComponent<SpriteRenderer>().enabled = true;
                        foreach (GameObject objeto in alternativa_2)
                        {
                            objeto.transform.position = new Vector2(objeto.transform.position.x, objeto.transform.position.y);

                        }
                        break;  
                }
                break;
        }
        boton1.gameObject.SetActive(true);
        boton2.gameObject.SetActive(true);
        boton3.gameObject.SetActive(true);
        opcionesGeneradas = true;
    }
    public void OpcionA()
    {

        if (A)
        {
            Debug.Log("HAS ACERTADO");
            panelVerde = true;
            aciertos++;
            score.GetComponent<TextMeshProUGUI>().text = "Score: " + aciertos;
            bocadillo.SetActive(true);
            FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Enfado", true);
            textoBocadillo.text = "No puede ser!, has acertado!";
            LevelSystem();
        } 
        else
        {
            Debug.Log("HAS FALLADO");
            Miss();

            bocadillo.SetActive(true);
            FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Desafio", true);
            textoBocadillo.text = "Tienes que entrenar más.";
        }
        ResetLevel();
    }
    public void OpcionB()
    {

        if (B)
        {
            Debug.Log("HAS ACERTADO");
            panelVerde = true;
            aciertos++;
            score.GetComponent<TextMeshProUGUI>().text = "Score: "+ aciertos;
            bocadillo.SetActive(true);
            FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Sorpresa", true);
            textoBocadillo.text = "Increible!";
            LevelSystem();
        }
        else
        {
            Debug.Log("HAS FALLADO");
            Miss();

            bocadillo.SetActive(true);
            FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Desafio", true);
            textoBocadillo.text = "Fíjate también en los colores...";
        }
        ResetLevel();
    }
    public void OpcionC()
    {

        if (C)
        {
            Debug.Log("HAS ACERTADO");
            panelVerde = true;
            aciertos++;
            score.GetComponent<TextMeshProUGUI>().text = "Score: " + aciertos;
            bocadillo.SetActive(true);
            FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Sorpresa", true);
            textoBocadillo.text = "Increible!, como lo hiciste?";
            LevelSystem();
        }
        else
        {
            Debug.Log("HAS FALLADO");
            Miss();

            bocadillo.SetActive(true);
            FindObjectOfType<JackController>().GetComponent<Animator>().SetBool("Burla", true);
            textoBocadillo.text = "Lo sabíaaa, JAJAJAJA.";
        }
        ResetLevel();
        
    }
    void ResetLevel()
    {
        foreach (GameObject item in respuesta)
        {
            Destroy(item);
        }
        foreach (GameObject item in alternativa_1)
        {
            Destroy(item);
        }
        foreach (GameObject item in alternativa_2)
        {
            Destroy(item);
        }
        if (JugarManager.toggleValue)
        {
            if (aciertos < 5)
                rnd = Random.Range(5, 7);
            else if (aciertos < 10)
                rnd = Random.Range(8, 10);
            else if (aciertos < 15)
                rnd = Random.Range(10, 13);
            else if (aciertos >= 15)
                rnd = Random.Range(13, 15);

            contador = 3;
            interval = 3;
        }
        else
        {
            if (aciertos < 5)
                rnd = Random.Range(4, 7);
            else if (aciertos < 10)
                rnd = Random.Range(7, 9);
            else if (aciertos < 15)
                rnd = Random.Range(9, 12);
            else if (aciertos >= 15)
                rnd = Random.Range(12, 14);

            contador = 5;
            interval = 5;
        }
        tiempoAux = 5;
        tiempoAnimacion = 3;
        vuelta = true;
        randoms.Clear();
        randomsPositionX.Clear();
        randomsPositionY.Clear();
        respuesta = new GameObject[rnd];
        alternativa_1 = new GameObject[rnd];
        alternativa_2 = new GameObject[rnd];
        C = false;
        A = false;
        B = false;
        opcionesGeneradas = false;
        boton1.gameObject.SetActive(false);
        boton2.gameObject.SetActive(false);
        boton3.gameObject.SetActive(false);
        figura1.GetComponent<SpriteRenderer>().enabled = false;
        figura2.GetComponent<SpriteRenderer>().enabled = false;
        figura.GetComponent<SpriteRenderer>().enabled = false;
        anObjectIsInstantiate = false;
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
    public static void ResetVariables()
    {
        CartelController.finalize = false;
    }
    void Miss()
    {
        switch (vidas)
        {
            case 3:
                Destroy(corazones[2]);
                break;
            case 2:
                Destroy(corazones[1]);
                break;
            case 1:
                Destroy(corazones[0]);
                break;
        }
        panelRojo = true;
        if (aciertos > 0)
        {
            if (aciertos == 5 || aciertos == 10 || aciertos == 15 || aciertos == 20 || aciertos == 25)
                level--; 
            aciertos--;
            score.GetComponent<TextMeshProUGUI>().text = "Score: " + aciertos;
        }
        vidas--;
    }
    void LevelSystem()
    {
        switch (aciertos)
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
