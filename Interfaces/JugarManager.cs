using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class JugarManager : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleRectTransform;
    [SerializeField] Color BackgroundActiveColor;
    [SerializeField] Color handleActiveColor;
    Image backgroundImage, handleImage;
    Color backgroundDefaulColor, handleDefaultColor;
    public Toggle toggle;
    public TextMeshProUGUI TXTScore;
    public Vector2 handlePosition;
    public static bool toggleValue = false;
    public GameObject panel;
    public Text perfilText;
    private void Start()
    {
        perfilText.text = ProfileStorage.s_currentProfile.name;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            FlechaDerecha();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            FlechaIzquierda();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            toggle.Select();
        }
        switch (gameObject.scene.name)
        {
            case "JuegoQueen":
                if (toggleValue)
                {
                    TXTScore.text = "Mejor puntuación: " + ProfileStorage.s_currentProfile.queenScoreP + " Nivel " + ProfileStorage.s_currentProfile.nivelqueenP;
                }
                else
                {
                    TXTScore.text = "Mejor puntuación: " + ProfileStorage.s_currentProfile.queenScore + " Nivel " + ProfileStorage.s_currentProfile.nivelqueen;
                }

                break;
            case "JuegoJack":
                if (toggleValue)
                {
                    TXTScore.text = "Mejor puntuación: " + ProfileStorage.s_currentProfile.jackScoreP + " Nivel " + ProfileStorage.s_currentProfile.niveljackP;
                }
                else
                {
                    TXTScore.text = "Mejor puntuación: " + ProfileStorage.s_currentProfile.jackScore + " Nivel " + ProfileStorage.s_currentProfile.niveljack;
                }

                break;
            case "JuegoForest":
                if (toggleValue)
                {
                    TXTScore.text = "Mejor puntuación: " + ProfileStorage.s_currentProfile.batForestScoreP + " Nivel " + ProfileStorage.s_currentProfile.nivelforestP;
                }
                else
                {
                    TXTScore.text = "Mejor puntuación: " + ProfileStorage.s_currentProfile.batForestScore + " Nivel " + ProfileStorage.s_currentProfile.nivelforest;
                }

                break;
            case "JuegoFlying":
                if (toggleValue)
                {
                    TXTScore.text = "Mejor puntuación: " + ProfileStorage.s_currentProfile.batFlyingScoreP + " Nivel " + ProfileStorage.s_currentProfile.nivelflyingP;
                }
                else
                {
                    TXTScore.text = "Mejor puntuación: " + ProfileStorage.s_currentProfile.batFlyingScore + " Nivel " + ProfileStorage.s_currentProfile.nivelflying;
                }

                break;
            case "JuegoFlyingv2":
                if (toggleValue)
                {
                    TXTScore.text = "Mejor puntuación: " + ProfileStorage.s_currentProfile.batFlying2ScoreP + " Nivel " + ProfileStorage.s_currentProfile.nivelflying2P;
                }
                else
                {
                    TXTScore.text = "Mejor puntuación: " + ProfileStorage.s_currentProfile.batFlying2Score + " Nivel " + ProfileStorage.s_currentProfile.nivelflying2;
                }

                break;
            case "JuegoRetratos":
                if (toggleValue)
                {
                    TXTScore.text = "Mejor puntuación: " + ProfileStorage.s_currentProfile.portraitsScoreP + " Nivel " + ProfileStorage.s_currentProfile.nivelportraitsP;
                }
                else
                {
                    TXTScore.text = "Mejor puntuación: " + ProfileStorage.s_currentProfile.portraitsScore + " Nivel " + ProfileStorage.s_currentProfile.nivelportraits;
                }

                break;
            case "JuegoRetratosv2":
                if (toggleValue)
                {
                    TXTScore.text = "Mejor puntuación: " + ProfileStorage.s_currentProfile.portraits2ScoreP + " Nivel " + ProfileStorage.s_currentProfile.nivelportraits2P;
                }
                else
                {
                    TXTScore.text = "Mejor puntuación: " + ProfileStorage.s_currentProfile.portraits2Score + " Nivel " + ProfileStorage.s_currentProfile.nivelportraits2;
                }

                break;
            case "JuegoEyes":
                if (toggleValue)
                {
                    TXTScore.text = "Mejor puntuación: " + ProfileStorage.s_currentProfile.eyesScoreP + " Nivel " + ProfileStorage.s_currentProfile.niveleyeP;
                }
                else
                {
                    TXTScore.text = "Mejor puntuación: " + ProfileStorage.s_currentProfile.eyesScore + " Nivel " + ProfileStorage.s_currentProfile.niveleye;
                }

                break;
            case "JuegoUpdate":
                if (toggleValue)
                {
                    TXTScore.text = "Mejor puntuación: " + ProfileStorage.s_currentProfile.cementeryScoreP + " Nivel " + ProfileStorage.s_currentProfile.nivelcementeryP;
                }
                else
                {
                    TXTScore.text = "Mejor puntuación: " + ProfileStorage.s_currentProfile.cementeryScore + " Nivel " + ProfileStorage.s_currentProfile.nivelcementery;
                }

                break;
            case "JuegoVampiro":
                if (toggleValue)
                {
                    TXTScore.text = "Mejor puntuación: " + ProfileStorage.s_currentProfile.theVampireScoreP + " Nivel " + ProfileStorage.s_currentProfile.nivelvampireP;
                }
                else
                {
                    TXTScore.text = "Mejor puntuación: " + ProfileStorage.s_currentProfile.theVampireScore + " Nivel " + ProfileStorage.s_currentProfile.nivelvampire;
                }

                break;
            case "JuegoStroop":
                if (toggleValue)
                {
                    TXTScore.text = "Mejor puntuación: " + ProfileStorage.s_currentProfile.stroopScoreP + " Nivel " + ProfileStorage.s_currentProfile.nivelstroopP;
                }
                else
                {
                    TXTScore.text = "Mejor puntuación: " + ProfileStorage.s_currentProfile.stroopScore + " Nivel " + ProfileStorage.s_currentProfile.nivelstroop;
                }

                break;
        }
        if (toggle.isOn)
        {
            panel.GetComponent<Image>().color = new Color32(255, 0, 40, 70);
            toggleValue = true;
        }
        else
        {
            panel.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
            toggleValue = false;
        }
           
    }
    public void BotonHome()
    {
        SceneManager.LoadScene("Menu_Principal");
    }
    public void BotonJugar()
    {
        switch (gameObject.scene.name)
        {
            case "JuegoQueen":
                SceneManager.LoadScene("CatchBatQueen");
                break;
            case "JuegoJack":
                SceneManager.LoadScene("JacksCastle");
                break;
            case "JuegoForest":
                SceneManager.LoadScene("BatForest");
                break;
            case "JuegoFlying":
                SceneManager.LoadScene("BatFlying");
                break;
            case "JuegoFlyingv2":
                SceneManager.LoadScene("BatFlyingV2");
                break;
            case "JuegoRetratos":
                SceneManager.LoadScene("Portraits");
                break;
            case "JuegoRetratosv2":
                SceneManager.LoadScene("PortraitsV2");
                break;
            case "JuegoEyes":
                SceneManager.LoadScene("EyesDirection");
                break;
            case "JuegoUpdate":
                SceneManager.LoadScene("CementeryUpdate");
                break;
            case "JuegoVampiro":
                SceneManager.LoadScene("BewareTheVampire");
                break;
            case "JuegoStroop":
                SceneManager.LoadScene("NumericalStroop");
                break;

        }     
    }
    public void FlechaDerecha()
    {
        switch (gameObject.scene.name)
        {
            case "JuegoQueen":
                SceneManager.LoadScene("JuegoJack");
                break;
            case "JuegoJack":
                SceneManager.LoadScene("JuegoForest");
                break;
            case "JuegoForest":
                SceneManager.LoadScene("JuegoFlying");
                break;
            case "JuegoFlying":
                SceneManager.LoadScene("JuegoFlyingv2");
                break;
            case "JuegoFlyingv2":
                SceneManager.LoadScene("juegoRetratos");
                break;
            case "JuegoRetratos":
                SceneManager.LoadScene("JuegoRetratosV2");
                break;
            case "JuegoRetratosv2":
                SceneManager.LoadScene("JuegoEyes");
                break;
            case "JuegoEyes":
                SceneManager.LoadScene("JuegoUpdate");
                break;
            case "JuegoUpdate":
                SceneManager.LoadScene("JuegoVampiro");
                break;
            case "JuegoVampiro":
                SceneManager.LoadScene("JuegoStroop");
                break;
            case "JuegoStroop":
                SceneManager.LoadScene("JuegoQueen");
                break;

        }
    }
    public void FlechaIzquierda()
    {
        switch (gameObject.scene.name)
        {
            case "JuegoQueen":
                SceneManager.LoadScene("JuegoStroop");
                break;
            case "JuegoJack":
                SceneManager.LoadScene("JuegoQueen");
                break;
            case "JuegoForest":
                SceneManager.LoadScene("JuegoJack");
                break;
            case "JuegoFlying":
                SceneManager.LoadScene("JuegoForest");
                break;
            case "JuegoFlyingv2":
                SceneManager.LoadScene("JuegoFlying");
                break;
            case "JuegoRetratos":
                SceneManager.LoadScene("JuegoFlyingv2");
                break;
            case "JuegoRetratosv2":
                SceneManager.LoadScene("JuegoRetratos");
                break;
            case "JuegoEyes":
                SceneManager.LoadScene("JuegoRetratosv2");
                break;
            case "JuegoUpdate":
                SceneManager.LoadScene("JuegoEyes");
                break;
            case "JuegoVampiro":
                SceneManager.LoadScene("JuegoUpdate");
                break;
            case "JuegoStroop":
                SceneManager.LoadScene("JuegoVampiro");
                break;
        }
    }
    private void Awake()
    {
        handlePosition = uiHandleRectTransform.anchoredPosition;

        backgroundImage = uiHandleRectTransform.parent.GetComponent<Image>();
        handleImage = uiHandleRectTransform.GetComponent<Image>();

        backgroundDefaulColor = backgroundImage.color;
        handleDefaultColor = handleImage.color;

        toggle.onValueChanged.AddListener(OnSwitch);
    }
    void OnSwitch(bool on)
    {
        uiHandleRectTransform.anchoredPosition = on ? handlePosition * -1 : handlePosition;
        backgroundImage.color = on ? BackgroundActiveColor : backgroundDefaulColor;
        handleImage.color = on ? handleActiveColor : handleDefaultColor;
    }
    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
