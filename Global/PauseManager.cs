using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image imagenMute;
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public AudioSource music;
    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = slider.value;
        RevisarSiMute();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
                Resume();
            else
                Pause();
        }
    }
    public void Resume()
    {
        music.Play();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    void Pause()
    {
        music.Pause();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void LoadMenu()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
        if (CatchBatQueenManager.CatchBatQueen)
        {
            if (JugarManager.toggleValue)
            {
                if (ProfileStorage.s_currentProfile.queenScoreP < CatchBatQueenManager.score)
                {
                    ProfileStorage.StoragePlayerProfile(CatchBatQueenManager.score, FindObjectOfType<ScenaryManager>().level);
                }

            }
            else
            {
                if (ProfileStorage.s_currentProfile.queenScore < CatchBatQueenManager.score)
                {
                    ProfileStorage.StoragePlayerProfile(CatchBatQueenManager.score, FindObjectOfType<ScenaryManager>().level);
                }
            }
            CatchBatQueenManager.ResetVariables();
            CatchBatQueenManager.CatchBatQueen = false;
        }
        if (JackManager.JacksCastle)
        {
            if (JugarManager.toggleValue)
            {
                if (ProfileStorage.s_currentProfile.jackScoreP < FindObjectOfType<JackManager>().aciertos)
                {
                    ProfileStorage.StoragePlayerProfile("jack", FindObjectOfType<JackManager>().aciertos, FindObjectOfType<JackManager>().level);
                }

            }
            else
            {
                if (ProfileStorage.s_currentProfile.jackScore < FindObjectOfType<JackManager>().aciertos)
                {
                    ProfileStorage.StoragePlayerProfile("jack", FindObjectOfType<JackManager>().aciertos, FindObjectOfType<JackManager>().level);
                }
            }
            JackController.inicio = false;
            CartelController.finalize = false;
            JackManager.ResetVariables();
            JackManager.JacksCastle = false;
        }
        if (BatForestManager.BatForest)
        {
            if (JugarManager.toggleValue)
            {
                if (ProfileStorage.s_currentProfile.batForestScoreP < FindObjectOfType<BatController>().points)
                {
                    ProfileStorage.StoragePlayerProfile("batForest", FindObjectOfType<BatController>().points, FindObjectOfType<BatForestManager>().level);
                }
            }
            else
            {
                if (ProfileStorage.s_currentProfile.batForestScore < FindObjectOfType<BatController>().points)
                {
                    ProfileStorage.StoragePlayerProfile("batForest", FindObjectOfType<BatController>().points, FindObjectOfType<BatForestManager>().level);
                }
            }
            BatForestManager.BatForest = false;
        }
        if (BatFlyingManager.BatFlying)
        {
            if (JugarManager.toggleValue)
            {
                if (ProfileStorage.s_currentProfile.batFlyingScoreP < FindObjectOfType<BatFlyingManager>().points)
                {
                    ProfileStorage.StoragePlayerProfile("batFlying", FindObjectOfType<BatFlyingManager>().points, FindObjectOfType<BatFlyingManager>().level);
                }
            }
            else
            {
                if (ProfileStorage.s_currentProfile.batFlyingScore < FindObjectOfType<BatFlyingManager>().points)
                {
                    ProfileStorage.StoragePlayerProfile("batFlying", FindObjectOfType<BatFlyingManager>().points, FindObjectOfType<BatFlyingManager>().level);
                }
            }
            BatFlyingManager.BatFlying = false;
            BatWallController.num = 0;
        }
        if (BatFlyingManagerV2.BatFlyingV2)
        {
            if (JugarManager.toggleValue)
            {
                if (ProfileStorage.s_currentProfile.batFlying2ScoreP < FindObjectOfType<BatFlyingManagerV2>().points)
                {
                    ProfileStorage.StoragePlayerProfile("batFlying2", FindObjectOfType<BatFlyingManagerV2>().points, FindObjectOfType<BatFlyingManagerV2>().level);
                }
            }
            else
            {
                if (ProfileStorage.s_currentProfile.batFlying2Score < FindObjectOfType<BatFlyingManagerV2>().points)
                {
                    ProfileStorage.StoragePlayerProfile("batFlying2", FindObjectOfType<BatFlyingManagerV2>().points, FindObjectOfType<BatFlyingManagerV2>().level);
                }
            }
            PortraitsManagerV2.PortraitsV2 = false;
            BatWallController.num = 0;
        }
        if (PortraitsManager.Portraits)
        {
            if (JugarManager.toggleValue)
            {
                if (ProfileStorage.s_currentProfile.portraitsScoreP < FindObjectOfType<PortraitsManager>().score)
                {
                    ProfileStorage.StoragePlayerProfile("portraits", FindObjectOfType<PortraitsManager>().score, FindObjectOfType<PortraitsManager>().nivel);
                }

            }
            else
            {
                if (ProfileStorage.s_currentProfile.portraitsScore < FindObjectOfType<PortraitsManager>().score)
                {
                    ProfileStorage.StoragePlayerProfile("portraits", FindObjectOfType<PortraitsManager>().score, FindObjectOfType<PortraitsManager>().nivel);
                }
            }
            PortraitsManager.Portraits = false;
        }
        if (PortraitsManagerV2.PortraitsV2)
        {
            if (JugarManager.toggleValue)
            {
                if (ProfileStorage.s_currentProfile.portraits2ScoreP < FindObjectOfType<PortraitsManagerV2>().score)
                {
                    ProfileStorage.StoragePlayerProfile("portraits2", FindObjectOfType<PortraitsManagerV2>().score, FindObjectOfType<PortraitsManagerV2>().nivel);
                }
            }
            else
            {
                if (ProfileStorage.s_currentProfile.portraits2Score < FindObjectOfType<PortraitsManagerV2>().score)
                {
                    ProfileStorage.StoragePlayerProfile("portraits2", FindObjectOfType<PortraitsManagerV2>().score, FindObjectOfType<PortraitsManagerV2>().nivel);
                }
            }
            PortraitsManagerV2.PortraitsV2 = false;
        }
        if (EyesDirectionManager.EyesDirection)
        {
            if (JugarManager.toggleValue)
            {
                if (ProfileStorage.s_currentProfile.eyesScoreP < FindObjectOfType<EyesDirectionManager>().score)
                {
                    ProfileStorage.StoragePlayerProfile("eyes", FindObjectOfType<EyesDirectionManager>().score, FindObjectOfType<EyesDirectionManager>().nivel);
                }

            }
            else
            {
                if (ProfileStorage.s_currentProfile.eyesScore < FindObjectOfType<EyesDirectionManager>().score)
                {
                    ProfileStorage.StoragePlayerProfile("eyes", FindObjectOfType<EyesDirectionManager>().score, FindObjectOfType<EyesDirectionManager>().nivel);
                }
            }
            EyesDirectionManager.EyesDirection = false;
        }
        if (UpdateManager.UpdateGame)
        {
            if (JugarManager.toggleValue)
            {
                if (ProfileStorage.s_currentProfile.cementeryScoreP < FindObjectOfType<UpdateManager>().score)
                {
                    ProfileStorage.StoragePlayerProfile("cementery", FindObjectOfType<UpdateManager>().score, FindObjectOfType<UpdateManager>().nivel);
                }
            }
            else
            {
                if (ProfileStorage.s_currentProfile.cementeryScore < FindObjectOfType<UpdateManager>().score)
                {
                    ProfileStorage.StoragePlayerProfile("cementery", FindObjectOfType<UpdateManager>().score, FindObjectOfType<UpdateManager>().nivel);
                }
            }
            UpdateManager.UpdateGame = false;
            ObjectDestructionManager.muerto = false;
        }

        if (BewareVampireManager.BewareVampire)
        {
            if (JugarManager.toggleValue)
            {
                if (ProfileStorage.s_currentProfile.theVampireScoreP < FindObjectOfType<BewareVampireManager>().score)
                {
                    ProfileStorage.StoragePlayerProfile("theVampire", FindObjectOfType<BewareVampireManager>().score, FindObjectOfType<BewareVampireManager>().nivel);
                }
            }
            else
            {
                if (ProfileStorage.s_currentProfile.theVampireScore < FindObjectOfType<BewareVampireManager>().score)
                {
                    ProfileStorage.StoragePlayerProfile("theVampire", FindObjectOfType<BewareVampireManager>().score, FindObjectOfType<BewareVampireManager>().nivel);
                }
            }
            BewareVampireManager.BewareVampire = false;
        }
        if (NumericStroopManager.numericStroop)
        {
            if (JugarManager.toggleValue)
            {
                if (ProfileStorage.s_currentProfile.stroopScoreP < FindObjectOfType<NumericStroopManager>().score)
                {
                    ProfileStorage.StoragePlayerProfile("stroop", FindObjectOfType<NumericStroopManager>().score, FindObjectOfType<NumericStroopManager>().nivel);
                }
            }
            else
            {
                if (ProfileStorage.s_currentProfile.stroopScore < FindObjectOfType<NumericStroopManager>().score)
                {
                    ProfileStorage.StoragePlayerProfile("stroop", FindObjectOfType<NumericStroopManager>().score, FindObjectOfType<NumericStroopManager>().nivel);
                }
            }
            NumericStroopManager.numericStroop = false;
        }
        SceneManager.LoadScene("Menu_Principal");
    }
    public void Exit()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
        if (CatchBatQueenManager.CatchBatQueen)
        {
            if (JugarManager.toggleValue)
            {
                if (ProfileStorage.s_currentProfile.queenScoreP < CatchBatQueenManager.score)
                {
                    ProfileStorage.StoragePlayerProfile(CatchBatQueenManager.score, FindObjectOfType<ScenaryManager>().level);
                }

            }
            else
            {
                if (ProfileStorage.s_currentProfile.queenScore < CatchBatQueenManager.score)
                {
                    ProfileStorage.StoragePlayerProfile(CatchBatQueenManager.score, FindObjectOfType<ScenaryManager>().level);
                }
            }
            CatchBatQueenManager.ResetVariables();
            CatchBatQueenManager.CatchBatQueen = false;
            SceneManager.LoadScene("JuegoQueen");
        }
        if (JackManager.JacksCastle)
        {
            if (JackManager.JacksCastle)
            {
                if (JugarManager.toggleValue)
                {
                    if (ProfileStorage.s_currentProfile.jackScoreP < FindObjectOfType<JackManager>().aciertos)
                    {
                        ProfileStorage.StoragePlayerProfile("jack", FindObjectOfType<JackManager>().aciertos, FindObjectOfType<JackManager>().level);
                    }

                }
                else
                {
                    if (ProfileStorage.s_currentProfile.jackScore < FindObjectOfType<JackManager>().aciertos)
                    {
                        ProfileStorage.StoragePlayerProfile("jack", FindObjectOfType<JackManager>().aciertos, FindObjectOfType<JackManager>().level);
                    }
                }
            }
            JackController.inicio = false;
            CartelController.finalize = false;
            JackManager.ResetVariables();
            JackManager.JacksCastle = false;
            SceneManager.LoadScene("JuegoJack");
        }
        if (BatForestManager.BatForest)
        {
            if (JugarManager.toggleValue)
            {
                if (ProfileStorage.s_currentProfile.batForestScoreP < FindObjectOfType<BatController>().points)
                {
                    ProfileStorage.StoragePlayerProfile("batForest", FindObjectOfType<BatController>().points, FindObjectOfType<BatForestManager>().level);
                }
            }
            else
            {
                if (ProfileStorage.s_currentProfile.batForestScore < FindObjectOfType<BatController>().points)
                {
                    ProfileStorage.StoragePlayerProfile("batForest", FindObjectOfType<BatController>().points, FindObjectOfType<BatForestManager>().level);
                }
            }
            BatForestManager.BatForest = false;
            SceneManager.LoadScene("JuegoForest");
        }
        if (BatFlyingManager.BatFlying)
        {
            if (JugarManager.toggleValue)
            {
                if (ProfileStorage.s_currentProfile.batFlyingScoreP < FindObjectOfType<BatFlyingManager>().points)
                {
                    ProfileStorage.StoragePlayerProfile("batFlying", FindObjectOfType<BatFlyingManager>().points, FindObjectOfType<BatFlyingManager>().level);
                }
            }
            else
            {
                if (ProfileStorage.s_currentProfile.batFlyingScore < FindObjectOfType<BatFlyingManager>().points)
                {
                    ProfileStorage.StoragePlayerProfile("batFlying", FindObjectOfType<BatFlyingManager>().points, FindObjectOfType<BatFlyingManager>().level);
                }
            }
            BatFlyingManager.BatFlying = false;
            BatWallController.num = 0;
            SceneManager.LoadScene("JuegoFlying");
        }
        if (BatFlyingManagerV2.BatFlyingV2)
        {
            if (JugarManager.toggleValue)
            {
                if (ProfileStorage.s_currentProfile.batFlying2ScoreP < FindObjectOfType<BatFlyingManagerV2>().points)
                {
                    ProfileStorage.StoragePlayerProfile("batFlying2", FindObjectOfType<BatFlyingManagerV2>().points, FindObjectOfType<BatFlyingManagerV2>().level);
                }
            }
            else
            {
                if (ProfileStorage.s_currentProfile.batFlying2Score < FindObjectOfType<BatFlyingManagerV2>().points)
                {
                    ProfileStorage.StoragePlayerProfile("batFlying2", FindObjectOfType<BatFlyingManagerV2>().points, FindObjectOfType<BatFlyingManagerV2>().level);
                }
            }
            BatFlyingManagerV2.BatFlyingV2 = false;
            BatWallController.num = 0;
            SceneManager.LoadScene("JuegoFlyingv2");
        }
        if (PortraitsManager.Portraits)
        {
            if (JugarManager.toggleValue)
            {
                if (ProfileStorage.s_currentProfile.portraitsScoreP < FindObjectOfType<PortraitsManager>().score)
                {
                    ProfileStorage.StoragePlayerProfile("portraits", FindObjectOfType<PortraitsManager>().score, FindObjectOfType<PortraitsManager>().nivel);
                }

            }
            else
            {
                if (ProfileStorage.s_currentProfile.portraitsScore < FindObjectOfType<PortraitsManager>().score)
                {
                    ProfileStorage.StoragePlayerProfile("portraits", FindObjectOfType<PortraitsManager>().score, FindObjectOfType<PortraitsManager>().nivel);
                }
            }
            PortraitsManager.Portraits = false;
            SceneManager.LoadScene("JuegoRetratos");
        }
        if (PortraitsManagerV2.PortraitsV2)
        {
            if (JugarManager.toggleValue)
            {
                if (ProfileStorage.s_currentProfile.portraits2ScoreP < FindObjectOfType<PortraitsManagerV2>().score)
                {
                    ProfileStorage.StoragePlayerProfile("portraits2", FindObjectOfType<PortraitsManagerV2>().score, FindObjectOfType<PortraitsManagerV2>().nivel);
                }
            }
            else
            {
                if (ProfileStorage.s_currentProfile.portraits2Score < FindObjectOfType<PortraitsManagerV2>().score)
                {
                    ProfileStorage.StoragePlayerProfile("portraits2", FindObjectOfType<PortraitsManagerV2>().score, FindObjectOfType<PortraitsManagerV2>().nivel);
                }
            }
            PortraitsManagerV2.PortraitsV2 = false;
            SceneManager.LoadScene("JuegoRetratosV2");
        }
        if (EyesDirectionManager.EyesDirection)
        {
            if (JugarManager.toggleValue)
            {
                if (ProfileStorage.s_currentProfile.eyesScoreP < FindObjectOfType<EyesDirectionManager>().score)
                {
                    ProfileStorage.StoragePlayerProfile("eyes", FindObjectOfType<EyesDirectionManager>().score, FindObjectOfType<EyesDirectionManager>().nivel);
                }

            }
            else
            {
                if (ProfileStorage.s_currentProfile.eyesScore < FindObjectOfType<EyesDirectionManager>().score)
                {
                    ProfileStorage.StoragePlayerProfile("eyes", FindObjectOfType<EyesDirectionManager>().score, FindObjectOfType<EyesDirectionManager>().nivel);
                }
            }
            EyesDirectionManager.EyesDirection = false;
            SceneManager.LoadScene("JuegoEyes");
        }
        if (UpdateManager.UpdateGame)
        {
            if (JugarManager.toggleValue)
            {
                if (ProfileStorage.s_currentProfile.cementeryScoreP < FindObjectOfType<UpdateManager>().score)
                {
                    ProfileStorage.StoragePlayerProfile("cementery", FindObjectOfType<UpdateManager>().score, FindObjectOfType<UpdateManager>().nivel);
                }
            }
            else
            {
                if (ProfileStorage.s_currentProfile.cementeryScore < FindObjectOfType<UpdateManager>().score)
                {
                    ProfileStorage.StoragePlayerProfile("cementery", FindObjectOfType<UpdateManager>().score, FindObjectOfType<UpdateManager>().nivel);
                }
            }
            ObjectDestructionManager.muerto = false;
            UpdateManager.UpdateGame = false;
            SceneManager.LoadScene("JuegoUpdate");
        }
        if (BewareVampireManager.BewareVampire)
        {
            if (JugarManager.toggleValue)
            {
                if (ProfileStorage.s_currentProfile.theVampireScoreP < FindObjectOfType<BewareVampireManager>().score)
                {
                    ProfileStorage.StoragePlayerProfile("theVampire", FindObjectOfType<BewareVampireManager>().score, FindObjectOfType<BewareVampireManager>().nivel);
                }
            }
            else
            {
                if (ProfileStorage.s_currentProfile.theVampireScore < FindObjectOfType<BewareVampireManager>().score)
                {
                    ProfileStorage.StoragePlayerProfile("theVampire", FindObjectOfType<BewareVampireManager>().score, FindObjectOfType<BewareVampireManager>().nivel);
                }
            }
            BewareVampireManager.BewareVampire = false;
            SceneManager.LoadScene("JuegoVampiro");
        }
        if (NumericStroopManager.numericStroop)
        {
            if (JugarManager.toggleValue)
            {
                if (ProfileStorage.s_currentProfile.stroopScoreP < FindObjectOfType<NumericStroopManager>().score)
                {
                    ProfileStorage.StoragePlayerProfile("stroop", FindObjectOfType<NumericStroopManager>().score, FindObjectOfType<NumericStroopManager>().nivel);
                }
            }
            else
            {
                if (ProfileStorage.s_currentProfile.stroopScore < FindObjectOfType<NumericStroopManager>().score)
                {
                    ProfileStorage.StoragePlayerProfile("stroop", FindObjectOfType<NumericStroopManager>().score, FindObjectOfType<NumericStroopManager>().nivel);
                }
            }
            NumericStroopManager.numericStroop = false;
            SceneManager.LoadScene("JuegoStroop");
        }
    }
    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = slider.value;
        RevisarSiMute();
    }
    public void RevisarSiMute()
    {
        if (sliderValue == 0)
            imagenMute.enabled = true;
        else
            imagenMute.enabled = false;
    }
}
