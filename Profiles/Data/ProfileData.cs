using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileData
{
    public string filename;
    public string name;
    public bool newGame;
    public DateTime fechaCreacion;

    //Normales
    public int batFlyingScore, batFlying2Score, batForestScore, theVampireScore, cementeryScore, eyesScore, jackScore, stroopScore, portraitsScore, portraits2Score;
    public float queenScore;
    //Pesadilla
    public int batFlyingScoreP, batFlying2ScoreP, batForestScoreP, theVampireScoreP, cementeryScoreP, eyesScoreP, jackScoreP, stroopScoreP, portraitsScoreP, portraits2ScoreP;
    public float queenScoreP;

    //Niveles normales
    public int nivelflying, nivelflying2, nivelforest, nivelvampire, nivelcementery, niveleye, niveljack, nivelstroop, nivelportraits, nivelportraits2, nivelqueen;
    //niveles pesadilla
    public int nivelflyingP, nivelflying2P, nivelforestP, nivelvampireP, nivelcementeryP, niveleyeP, niveljackP, nivelstroopP, nivelportraitsP, nivelportraits2P, nivelqueenP;

    public float general, razonamiento, coordinacion, fluidezVerbal, memoria, atencion, generalP, razonamientoP, coordinacionP, fluidezVerbalP, memoriaP, atencionP;

    public ProfileData()
    {
        this.filename = "Invitado.xml";
        this.name = "Invitado";
        this.newGame = false;
        this.fechaCreacion = System.DateTime.Now;

        this.batFlyingScore = 0;
        batFlying2Score = 0;
        batForestScore = 0;
        theVampireScore = 0;
        queenScore = 0;
        cementeryScore = 0;
        eyesScore = 0;
        jackScore = 0;
        stroopScore = 0;
        portraitsScore = 0;
        portraits2Score = 0;

        this.batFlyingScoreP = 0;
        batFlying2ScoreP = 0;
        batForestScoreP = 0;
        theVampireScoreP = 0;
        queenScoreP = 0;
        cementeryScoreP = 0;
        eyesScoreP = 0;
        jackScoreP = 0;
        stroopScoreP = 0;
        portraitsScoreP = 0;
        portraits2ScoreP = 0;

        nivelqueen = 0;
        nivelflying = 0;
        nivelflying2 = 0;
        nivelforest = 0;
        nivelvampire = 0;
        nivelcementery = 0;
        niveleye = 0;
        niveljack = 0;
        nivelstroop = 0;
        nivelportraits = 0;
        nivelportraits2 = 0;

        nivelqueenP = 0;
        nivelflyingP = 0;
        nivelflying2P = 0;
        nivelforestP = 0;
        nivelvampireP = 0;
        nivelcementeryP = 0;
        niveleyeP = 0;
        niveljackP = 0;
        nivelstroopP = 0;
        nivelportraitsP = 0;
        nivelportraits2P = 0;

        general = 0;
        razonamiento = 0;
        coordinacion = 0;
        fluidezVerbal = 0;
        memoria = 0;
        atencion = 0;

        generalP = 0;
        razonamientoP = 0;
        coordinacionP = 0;
        fluidezVerbalP = 0;
        memoriaP = 0;
        atencionP = 0;
    }
    //Por ahora nada mas pero en un futuro las puntuaciones en todos los juegos.
    public ProfileData(string name,bool newGame)
    {
        this.filename = name.Replace(" ", "_") + ".xml";
        this.name = name;
        this.newGame = newGame;
        this.fechaCreacion = System.DateTime.Now;

        this.batFlyingScore = 0;
        batFlying2Score = 0;
        batForestScore = 0;
        theVampireScore = 0;
        queenScore = 0;
        cementeryScore = 0;
        eyesScore = 0;
        jackScore = 0;
        stroopScore = 0;
        portraitsScore = 0;
        portraits2Score = 0;

        this.batFlyingScoreP = 0;
        batFlying2ScoreP = 0;
        batForestScoreP = 0;
        theVampireScoreP = 0;
        queenScoreP = 0;
        cementeryScoreP = 0;
        eyesScoreP = 0;
        jackScoreP = 0;
        stroopScoreP = 0;
        portraitsScoreP = 0;
        portraits2ScoreP = 0;

        nivelqueen = 0;
        nivelflying = 0;
        nivelflying2 = 0;
        nivelforest = 0;
        nivelvampire = 0;
        nivelcementery = 0;
        niveleye = 0;
        niveljack = 0;
        nivelstroop = 0;
        nivelportraits = 0;
        nivelportraits2 = 0;

        nivelqueenP = 0;
        nivelflyingP = 0;
        nivelflying2P = 0;
        nivelforestP = 0;
        nivelvampireP = 0;
        nivelcementeryP = 0;
        niveleyeP = 0;
        niveljackP = 0;
        nivelstroopP = 0;
        nivelportraitsP = 0;
        nivelportraits2P = 0;

        general = 0;
        razonamiento = 0;
        coordinacion = 0;
        fluidezVerbal = 0;
        memoria = 0;
        atencion = 0;

        generalP = 0;
        razonamientoP = 0;
        coordinacionP = 0;
        fluidezVerbalP = 0;
        memoriaP = 0;
        atencionP = 0;
    }
}
