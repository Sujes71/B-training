using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CatchBatQueenManager : MonoBehaviour
{
    public GameObject[] objetsToInstantiate;
    public static float interval;
    public static bool anObjectIsInstantiate = false, CatchBatQueen;
    public static float score = 0;
    public static float masMenosPuntos = 0;
    public TextMeshProUGUI TXTScore, TXTMas, TXTMenos, TXTFin;
    public static float gameTime = 300;
    public GameObject panel, vida1, vida2, vida3;
    public static float tiempoPanel = 2, tiempoPanelRacha = 2;
    private float intervalEnd = 5;
    public static ProfileData s_currentProfile;
    // Start is called before the first frame update
    void Start()
    {
        CatchBatQueen = true;
        interval = Random.Range(3, 6);
    }

    // Update is called once per frame
    void Update()
    {
        MuerteMurcielagoPanel();
        ActivaRacha();
        gameTime -= Time.deltaTime;
        if (gameTime <= 0 || ObjectManager.vidas <= 0)
        {
            TXTFin.gameObject.SetActive(true);
            if (intervalEnd >= 0)
                intervalEnd -= Time.deltaTime;
            else
            {
                if (JugarManager.toggleValue)
                {
                    if (ProfileStorage.s_currentProfile.queenScoreP < score)
                    {
                        ProfileStorage.StoragePlayerProfile(score, FindObjectOfType<ScenaryManager>().level);
                    }
                        
                }
                else
                {
                    if (ProfileStorage.s_currentProfile.queenScore < score)
                    {
                        ProfileStorage.StoragePlayerProfile(score, FindObjectOfType<ScenaryManager>().level);
                    }  
                }
                CatchBatQueen = false;
                ResetVariables();
                SceneManager.LoadScene("JuegoQueen");
            }
        }
        else
        {
            if (score < 0) score = 0;
            TXTScore.text = "Score: " + score;
            if (masMenosPuntos > 0)
                TXTMas.text = "+" + masMenosPuntos;
            else if (masMenosPuntos < 0)
                TXTMenos.text = masMenosPuntos.ToString();

            if (interval >= 0)
                interval -= Time.deltaTime;
            else
            {
                TXTMas.text = "";
                TXTMenos.text = "";
                Lanzar();
            }
        }
    }
    void Lanzar()
    {
        masMenosPuntos = 0;
        float posX = Random.Range(-7.14f, 7.14f);
        float posY = Random.Range(-4, 4);

        Vector2 posAleatoria = new Vector2(posX, posY);

        int n = Random.Range(0, 11);
        if (!anObjectIsInstantiate)
        {
            if(objetsToInstantiate[n].gameObject.name.Equals("BatSleeping Variant"))
                Instantiate(objetsToInstantiate[n].gameObject, new Vector2(posX, 4.27f), objetsToInstantiate[n].gameObject.transform.rotation);
            else if(objetsToInstantiate[n].gameObject.name.Equals("BatFlying Variant"))
                Instantiate(objetsToInstantiate[n].gameObject, new Vector2(Random.Range(-5.8f, 7.14f),posY), objetsToInstantiate[n].gameObject.transform.rotation);
            else if (objetsToInstantiate[n].gameObject.name.Equals("BatFlying Variant2"))
                Instantiate(objetsToInstantiate[n].gameObject, new Vector2(Random.Range(-7.14f, 5.8f), posY), objetsToInstantiate[n].gameObject.transform.rotation);
            else if (objetsToInstantiate[n].gameObject.name.Equals("BatQueenAtack1"))
                Instantiate(objetsToInstantiate[n].gameObject, new Vector2(Random.Range(-5.8f, 7.14f), posY), objetsToInstantiate[n].gameObject.transform.rotation);
            else if (objetsToInstantiate[n].gameObject.name.Equals("BatQueenAtack1 Variant"))
                Instantiate(objetsToInstantiate[n].gameObject, new Vector2(Random.Range(-7.14f, 5.8f), posY), objetsToInstantiate[n].gameObject.transform.rotation);
            else if (objetsToInstantiate[n].gameObject.name.Equals("BatQueenAtack2") || objetsToInstantiate[n].gameObject.name.Equals("BatQueenAtack2 Variant"))
                Instantiate(objetsToInstantiate[n].gameObject, new Vector2(posX, Random.Range(-4, 2.19f)), objetsToInstantiate[n].gameObject.transform.rotation);
            else if (objetsToInstantiate[n].gameObject.name.Equals("BatQueenAtack3"))
                Instantiate(objetsToInstantiate[n].gameObject, new Vector2(Random.Range(-5.8f, 7.14f), posY), objetsToInstantiate[n].gameObject.transform.rotation);
            else if (objetsToInstantiate[n].gameObject.name.Equals("BatQueenAtack3 Variant"))
                Instantiate(objetsToInstantiate[n].gameObject, new Vector2(Random.Range(-7.14f, 5.8f), posY), objetsToInstantiate[n].gameObject.transform.rotation);
            else
            {
                Instantiate(objetsToInstantiate[n].gameObject, posAleatoria, objetsToInstantiate[n].gameObject.transform.rotation);
            }
            anObjectIsInstantiate = true;
            interval = Random.Range(3, 6);
        }
    }
    public static void ResetVariables()
    {
        ObjectManager.vidas = 3;
        gameTime = 300;
        score = 0;
        masMenosPuntos = 0;
        anObjectIsInstantiate = false;
        interval = 0;
        ObjectManager.interval = 4;
        ObjectManager.intervalKing = 1;
        ObjectManager.panelRojo = false;
        ObjectManager.racha = false;
        ObjectManager.rachita = false;
        ObjectManager.rachon = false;
        ObjectManager.numRacha = 0;
        tiempoPanel = 0;
        tiempoPanelRacha = 0;
    }
    void ActivaRacha()
    {
        if (ObjectManager.rachita && !ObjectManager.racha && !ObjectManager.rachon)
        {
            tiempoPanelRacha -= Time.deltaTime;
            if (tiempoPanelRacha >= 1.5)
                panel.GetComponent<Image>().color = new Color32(0, 255, 20, 100);
            else if (tiempoPanelRacha <= 1.5 && tiempoPanelRacha > 1)
                panel.GetComponent<Image>().color = new Color32(0, 255, 20, 80);
            else if (tiempoPanelRacha <= 1 && tiempoPanelRacha > 0.5)
                panel.GetComponent<Image>().color = new Color32(0, 255, 20, 50);
            else if (tiempoPanelRacha <= 0.5 && tiempoPanelRacha > 0)
                panel.GetComponent<Image>().color = new Color32(0, 255, 20, 20);
            else
                panel.GetComponent<Image>().color = new Color32(0, 255, 20, 0);
        }
        else if (ObjectManager.rachita && ObjectManager.racha && !ObjectManager.rachon)
        {
            tiempoPanelRacha -= Time.deltaTime;
            if (tiempoPanelRacha >= 1.5)
                panel.GetComponent<Image>().color = new Color32(0, 255, 20, 100);
            else if (tiempoPanelRacha <= 1.5 && tiempoPanelRacha > 1)
                panel.GetComponent<Image>().color = new Color32(0, 255, 20, 80);
            else if (tiempoPanelRacha <= 1 && tiempoPanelRacha > 0.5)
                panel.GetComponent<Image>().color = new Color32(0, 255, 20, 50);
            else if (tiempoPanelRacha <= 0.5 && tiempoPanelRacha > 0)
                panel.GetComponent<Image>().color = new Color32(0, 255, 20, 20);
            else
                panel.GetComponent<Image>().color = new Color32(0, 255, 20, 0);
        }
        else if (ObjectManager.rachita && ObjectManager.racha && ObjectManager.rachon)
        {
            tiempoPanelRacha -= Time.deltaTime;
            if (tiempoPanelRacha >= 1.5)
                panel.GetComponent<Image>().color = new Color32(0, 255, 20, 100);
            else if (tiempoPanelRacha <= 1.5 && tiempoPanelRacha > 1)
                panel.GetComponent<Image>().color = new Color32(0, 255, 20, 80);
            else if (tiempoPanelRacha <= 1 && tiempoPanelRacha > 0.5)
                panel.GetComponent<Image>().color = new Color32(0, 255, 20, 50);
            else if (tiempoPanelRacha <= 0.5 && tiempoPanelRacha > 0)
                panel.GetComponent<Image>().color = new Color32(0, 255, 20, 20);
            else
                panel.GetComponent<Image>().color = new Color32(0, 255, 20, 0);
        }
    }
    void MuerteMurcielagoPanel()
    {
        if (ObjectManager.panelRojo)
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
                panel.GetComponent<Image>().color = new Color32(255, 0, 40, 0);
        }
    }
}
