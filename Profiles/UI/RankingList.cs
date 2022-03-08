using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingList : MonoBehaviour
{
    public Transform profilesHolder;
    public int i = 0;
    public GameObject rankingUIPrefab;
    public LinkedList<ProfileData> listaPerfiles, listaPerfilesOrdenada;
    public bool finalizado;

    // Start is called before the first frame update
    void Start()
    {
        listaPerfiles = new LinkedList<ProfileData>();
        listaPerfilesOrdenada = new LinkedList<ProfileData>();
        var index = ProfileStorage.GetProfileIndex();
        foreach (var profileName in index.profileFileNames)
        {
            listaPerfiles.AddLast(ProfileStorage.getProfile(profileName));
        }
    }
    private void Update()
    {
        if(FindObjectOfType<RankingManager>().numero == 1 && !finalizado)
        {
            double auxiliar = 0;
            foreach (var profile in listaPerfiles)
            {
                if (FindObjectOfType<RankingManager>().normal)
                {
                    if (profile.queenScore > auxiliar)
                    {
                        listaPerfilesOrdenada.AddFirst(profile);
                    }
                    else
                    {
                        listaPerfilesOrdenada.AddLast(profile);
                    }
                    auxiliar = profile.queenScore;
                }
                else
                {
                    if (profile.queenScoreP > auxiliar)
                    {
                        listaPerfilesOrdenada.AddFirst(profile);
                    }
                    else
                    {
                        listaPerfilesOrdenada.AddLast(profile);
                    }
                    auxiliar = profile.queenScoreP;
                }
            }
            foreach (var perfil in listaPerfilesOrdenada)
            {
                i++;
                var go = Instantiate(this.rankingUIPrefab);
                var uiBox = go.GetComponent<RankingBox>();

                uiBox.nombre.text = perfil.name;
                if (FindObjectOfType<RankingManager>().normal)
                    uiBox.score.text = perfil.queenScore.ToString();
                else
                    uiBox.score.text = perfil.queenScoreP.ToString();
                uiBox.posicion.text = i + ".";

                go.transform.SetParent(this.profilesHolder, false);
            }
            finalizado = true;
        }
        if (FindObjectOfType<RankingManager>().numero == 2 && !finalizado)
        {
            int auxiliar = 0;
            foreach (var profile in listaPerfiles)
            {
                if (FindObjectOfType<RankingManager>().normal)
                {
                    if (profile.jackScore > auxiliar)
                    {
                        listaPerfilesOrdenada.AddFirst(profile);
                    }
                    else
                    {
                        listaPerfilesOrdenada.AddLast(profile);
                    }
                    auxiliar = profile.jackScore;
                }
                else
                {
                    if (profile.jackScoreP > auxiliar)
                    {
                        listaPerfilesOrdenada.AddFirst(profile);
                    }
                    else
                    {
                        listaPerfilesOrdenada.AddLast(profile);
                    }
                    auxiliar = profile.jackScoreP;
                }
            }
            foreach (var perfil in listaPerfilesOrdenada)
            {
                i++;
                var go = Instantiate(this.rankingUIPrefab);
                var uiBox = go.GetComponent<RankingBox>();

                uiBox.nombre.text = perfil.name;
                if (FindObjectOfType<RankingManager>().normal)
                    uiBox.score.text = perfil.jackScore.ToString();
                else
                    uiBox.score.text = perfil.jackScoreP.ToString();
                uiBox.posicion.text = i + ".";

                go.transform.SetParent(this.profilesHolder, false);
            }
            finalizado = true;
        }
        if (FindObjectOfType<RankingManager>().numero == 3 && !finalizado)
        {
            int auxiliar = 0;
            foreach (var profile in listaPerfiles)
            {
                if (FindObjectOfType<RankingManager>().normal)
                {
                    if (profile.batFlyingScore > auxiliar)
                    {
                        listaPerfilesOrdenada.AddFirst(profile);
                    }
                    else
                    {
                        listaPerfilesOrdenada.AddLast(profile);
                    }
                    auxiliar = profile.batFlyingScore;
                }
                else
                {
                    if (profile.batFlyingScoreP > auxiliar)
                    {
                        listaPerfilesOrdenada.AddFirst(profile);
                    }
                    else
                    {
                        listaPerfilesOrdenada.AddLast(profile);
                    }
                    auxiliar = profile.batFlyingScoreP;
                }
            }
            foreach (var perfil in listaPerfilesOrdenada)
            {
                i++;
                var go = Instantiate(this.rankingUIPrefab);
                var uiBox = go.GetComponent<RankingBox>();

                uiBox.nombre.text = perfil.name;
                if (FindObjectOfType<RankingManager>().normal)
                    uiBox.score.text = perfil.batFlyingScore.ToString();
                else
                    uiBox.score.text = perfil.batFlyingScoreP.ToString();
                uiBox.posicion.text = i + ".";

                go.transform.SetParent(this.profilesHolder, false);
            }
            finalizado = true;
        }
        if (FindObjectOfType<RankingManager>().numero == 4 && !finalizado)
        {
            int auxiliar = 0;
            foreach (var profile in listaPerfiles)
            {
                if (FindObjectOfType<RankingManager>().normal)
                {
                    if (profile.batFlying2Score > auxiliar)
                    {
                        listaPerfilesOrdenada.AddFirst(profile);
                    }
                    else
                    {
                        listaPerfilesOrdenada.AddLast(profile);
                    }
                    auxiliar = profile.batFlying2Score;
                }
                else
                {
                    if (profile.batFlying2ScoreP > auxiliar)
                    {
                        listaPerfilesOrdenada.AddFirst(profile);
                    }
                    else
                    {
                        listaPerfilesOrdenada.AddLast(profile);
                    }
                    auxiliar = profile.batFlying2ScoreP;
                }
            }
            foreach (var perfil in listaPerfilesOrdenada)
            {
                i++;
                var go = Instantiate(this.rankingUIPrefab);
                var uiBox = go.GetComponent<RankingBox>();

                uiBox.nombre.text = perfil.name;
                if (FindObjectOfType<RankingManager>().normal)
                    uiBox.score.text = perfil.batFlying2Score.ToString();
                else
                    uiBox.score.text = perfil.batFlying2ScoreP.ToString();
                uiBox.posicion.text = i + ".";

                go.transform.SetParent(this.profilesHolder, false);
            }
            finalizado = true;
        }
        if (FindObjectOfType<RankingManager>().numero == 5 && !finalizado)
        {
            int auxiliar = 0;
            foreach (var profile in listaPerfiles)
            {
                if (FindObjectOfType<RankingManager>().normal)
                {
                    if (profile.batForestScore > auxiliar)
                    {
                        listaPerfilesOrdenada.AddFirst(profile);
                    }
                    else
                    {
                        listaPerfilesOrdenada.AddLast(profile);
                    }
                    auxiliar = profile.batForestScore;
                }
                else
                {
                    if (profile.batForestScoreP > auxiliar)
                    {
                        listaPerfilesOrdenada.AddFirst(profile);
                    }
                    else
                    {
                        listaPerfilesOrdenada.AddLast(profile);
                    }
                    auxiliar = profile.batForestScoreP;
                }
            }
            foreach (var perfil in listaPerfilesOrdenada)
            {
                i++;
                var go = Instantiate(this.rankingUIPrefab);
                var uiBox = go.GetComponent<RankingBox>();

                uiBox.nombre.text = perfil.name;
                if (FindObjectOfType<RankingManager>().normal)
                    uiBox.score.text = perfil.batForestScore.ToString();
                else
                    uiBox.score.text = perfil.batForestScoreP.ToString();
                uiBox.posicion.text = i + ".";

                go.transform.SetParent(this.profilesHolder, false);
            }
            finalizado = true;
        }
        if (FindObjectOfType<RankingManager>().numero == 6 && !finalizado)
        {
            int auxiliar = 0;
            foreach (var profile in listaPerfiles)
            {
                if (FindObjectOfType<RankingManager>().normal)
                {
                    if (profile.portraitsScore > auxiliar)
                    {
                        listaPerfilesOrdenada.AddFirst(profile);
                    }
                    else
                    {
                        listaPerfilesOrdenada.AddLast(profile);
                    }
                    auxiliar = profile.portraitsScore;
                }
                else
                {
                    if (profile.portraitsScoreP > auxiliar)
                    {
                        listaPerfilesOrdenada.AddFirst(profile);
                    }
                    else
                    {
                        listaPerfilesOrdenada.AddLast(profile);
                    }
                    auxiliar = profile.portraitsScoreP;
                }
            }
            foreach (var perfil in listaPerfilesOrdenada)
            {
                i++;
                var go = Instantiate(this.rankingUIPrefab);
                var uiBox = go.GetComponent<RankingBox>();

                uiBox.nombre.text = perfil.name;
                if (FindObjectOfType<RankingManager>().normal)
                    uiBox.score.text = perfil.portraitsScore.ToString();
                else
                    uiBox.score.text = perfil.portraitsScoreP.ToString();
                uiBox.posicion.text = i + ".";

                go.transform.SetParent(this.profilesHolder, false);
            }
            finalizado = true;
        }
        if (FindObjectOfType<RankingManager>().numero == 7 && !finalizado)
        {
            int auxiliar = 0;
            foreach (var profile in listaPerfiles)
            {
                if (FindObjectOfType<RankingManager>().normal)
                {
                    if (profile.portraits2Score > auxiliar)
                    {
                        listaPerfilesOrdenada.AddFirst(profile);
                    }
                    else
                    {
                        listaPerfilesOrdenada.AddLast(profile);
                    }
                    auxiliar = profile.portraits2Score;
                }
                else
                {
                    if (profile.portraits2ScoreP > auxiliar)
                    {
                        listaPerfilesOrdenada.AddFirst(profile);
                    }
                    else
                    {
                        listaPerfilesOrdenada.AddLast(profile);
                    }
                    auxiliar = profile.portraits2ScoreP;
                }
            }
            foreach (var perfil in listaPerfilesOrdenada)
            {
                i++;
                var go = Instantiate(this.rankingUIPrefab);
                var uiBox = go.GetComponent<RankingBox>();

                uiBox.nombre.text = perfil.name;
                if (FindObjectOfType<RankingManager>().normal)
                    uiBox.score.text = perfil.portraits2Score.ToString();
                else
                    uiBox.score.text = perfil.portraits2ScoreP.ToString();
                uiBox.posicion.text = i + ".";

                go.transform.SetParent(this.profilesHolder, false);
            }
            finalizado = true;
        }
        if (FindObjectOfType<RankingManager>().numero == 8 && !finalizado)
        {
            int auxiliar = 0;
            foreach (var profile in listaPerfiles)
            {
                if (FindObjectOfType<RankingManager>().normal)
                {
                    if (profile.eyesScore > auxiliar)
                    {
                        listaPerfilesOrdenada.AddFirst(profile);
                    }
                    else
                    {
                        listaPerfilesOrdenada.AddLast(profile);
                    }
                    auxiliar = profile.eyesScore;
                }
                else
                {
                    if (profile.eyesScoreP > auxiliar)
                    {
                        listaPerfilesOrdenada.AddFirst(profile);
                    }
                    else
                    {
                        listaPerfilesOrdenada.AddLast(profile);
                    }
                    auxiliar = profile.eyesScoreP;
                }
            }
            foreach (var perfil in listaPerfilesOrdenada)
            {
                i++;
                var go = Instantiate(this.rankingUIPrefab);
                var uiBox = go.GetComponent<RankingBox>();

                uiBox.nombre.text = perfil.name;
                if (FindObjectOfType<RankingManager>().normal)
                    uiBox.score.text = perfil.eyesScore.ToString();
                else
                    uiBox.score.text = perfil.eyesScoreP.ToString();
                uiBox.posicion.text = i + ".";

                go.transform.SetParent(this.profilesHolder, false);
            }
            finalizado = true;
        }
        if (FindObjectOfType<RankingManager>().numero == 9 && !finalizado)
        {
            int auxiliar = 0;
            foreach (var profile in listaPerfiles)
            {
                if (FindObjectOfType<RankingManager>().normal)
                {
                    if (profile.theVampireScore > auxiliar)
                    {
                        listaPerfilesOrdenada.AddFirst(profile);
                    }
                    else
                    {
                        listaPerfilesOrdenada.AddLast(profile);
                    }
                    auxiliar = profile.theVampireScore;
                }
                else
                {
                    if (profile.theVampireScoreP > auxiliar)
                    {
                        listaPerfilesOrdenada.AddFirst(profile);
                    }
                    else
                    {
                        listaPerfilesOrdenada.AddLast(profile);
                    }
                    auxiliar = profile.theVampireScoreP;
                }
            }
            foreach (var perfil in listaPerfilesOrdenada)
            {
                i++;
                var go = Instantiate(this.rankingUIPrefab);
                var uiBox = go.GetComponent<RankingBox>();

                uiBox.nombre.text = perfil.name;
                if (FindObjectOfType<RankingManager>().normal)
                    uiBox.score.text = perfil.theVampireScore.ToString();
                else
                    uiBox.score.text = perfil.theVampireScoreP.ToString();
                uiBox.posicion.text = i + ".";

                go.transform.SetParent(this.profilesHolder, false);
            }
            finalizado = true;
        }
        if (FindObjectOfType<RankingManager>().numero == 10 && !finalizado)
        {
            int auxiliar = 0;
            foreach (var profile in listaPerfiles)
            {
                if (FindObjectOfType<RankingManager>().normal)
                {
                    if (profile.stroopScore > auxiliar)
                    {
                        listaPerfilesOrdenada.AddFirst(profile);
                    }
                    else
                    {
                        listaPerfilesOrdenada.AddLast(profile);
                    }
                    auxiliar = profile.stroopScore;
                }
                else
                {
                    if (profile.stroopScoreP > auxiliar)
                    {
                        listaPerfilesOrdenada.AddFirst(profile);
                    }
                    else
                    {
                        listaPerfilesOrdenada.AddLast(profile);
                    }
                    auxiliar = profile.stroopScoreP;
                }
            }
            foreach (var perfil in listaPerfilesOrdenada)
            {
                i++;
                var go = Instantiate(this.rankingUIPrefab);
                var uiBox = go.GetComponent<RankingBox>();

                uiBox.nombre.text = perfil.name;
                if (FindObjectOfType<RankingManager>().normal)
                    uiBox.score.text = perfil.stroopScore.ToString();
                else
                    uiBox.score.text = perfil.stroopScoreP.ToString();
                uiBox.posicion.text = i + ".";

                go.transform.SetParent(this.profilesHolder, false);
            }
            finalizado = true;
        }
        if (FindObjectOfType<RankingManager>().numero == 11 && !finalizado)
        {
            int auxiliar = 0;
            foreach (var profile in listaPerfiles)
            {
                if (FindObjectOfType<RankingManager>().normal)
                {
                    if (profile.cementeryScore > auxiliar)
                    {
                        listaPerfilesOrdenada.AddFirst(profile);
                    }
                    else
                    {
                        listaPerfilesOrdenada.AddLast(profile);
                    }
                    auxiliar = profile.cementeryScore;
                }
                else
                {
                    if (profile.cementeryScoreP > auxiliar)
                    {
                        listaPerfilesOrdenada.AddFirst(profile);
                    }
                    else
                    {
                        listaPerfilesOrdenada.AddLast(profile);
                    }
                    auxiliar = profile.cementeryScoreP;
                }
            }
            foreach (var perfil in listaPerfilesOrdenada)
            {
                i++;
                var go = Instantiate(this.rankingUIPrefab);
                var uiBox = go.GetComponent<RankingBox>();

                uiBox.nombre.text = perfil.name;
                if (FindObjectOfType<RankingManager>().normal)
                    uiBox.score.text = perfil.cementeryScore.ToString();
                else
                    uiBox.score.text = perfil.cementeryScoreP.ToString();
                uiBox.posicion.text = i + ".";

                go.transform.SetParent(this.profilesHolder, false);
            }
            finalizado = true;
        }
    }
}
