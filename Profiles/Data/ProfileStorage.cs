using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public static class ProfileStorage
{
    public static ProfileData s_currentProfile;

    private static string s_indexPath = Application.streamingAssetsPath + "/Profiles/_ProfileIndex_.xml";

    public static void CreateNewGame(string profileName)
    {
        s_currentProfile = new ProfileData(profileName, true);
        string path = Application.streamingAssetsPath + "/Profiles/" + s_currentProfile.filename;
        SaveFile<ProfileData>(path, s_currentProfile);

        //Update index
        var index = GetProfileIndex();
        index.profileFileNames.Add(s_currentProfile.filename);

        //Save index
        SaveFile<ProfileIndex>(s_indexPath, index);
    }
    public static void LoadProfile(string filename)
    {
        var path = Application.streamingAssetsPath + "/Profiles/" + filename;
        s_currentProfile = LoadFile<ProfileData>(path);
    }
    //Cargar Perfil
    public static void StoragePlayerProfile(float score, int nivel)
    {
        if (JugarManager.toggleValue)
        {
            s_currentProfile.queenScoreP = score;
            s_currentProfile.nivelqueenP = nivel;
            s_currentProfile.generalP += nivel;
            s_currentProfile.coordinacionP += nivel;
            s_currentProfile.atencionP += nivel;
        }
        else
        {
            s_currentProfile.queenScore = score;
            s_currentProfile.nivelqueen = nivel;
            s_currentProfile.general += nivel;
            s_currentProfile.coordinacion += nivel;
            s_currentProfile.atencion += nivel;
        }
        s_currentProfile.newGame = false;

        var path = Application.streamingAssetsPath + "/Profiles/" + s_currentProfile.filename;
        SaveFile<ProfileData>(path, s_currentProfile);
    }
    public static ProfileData getProfile(string filename)
    {
        var path = Application.streamingAssetsPath + "/Profiles/" + filename;

        return LoadFile<ProfileData>(path);
    }
    public static void StoragePlayerProfile(string juego,int score, int nivel)
    {
        switch (juego)
        {
            case "batFlying":
                if (JugarManager.toggleValue)
                {
                    s_currentProfile.batFlyingScoreP = score;
                    s_currentProfile.nivelflyingP = nivel;
                    s_currentProfile.generalP += nivel;
                    s_currentProfile.coordinacionP += nivel;
                }
                else
                {
                    s_currentProfile.batFlyingScore = score;
                    s_currentProfile.nivelflying = nivel;
                    s_currentProfile.general += nivel;
                    s_currentProfile.coordinacion += nivel;
                }
                    
                break;
            case "batFlying2":
                if (JugarManager.toggleValue)
                {
                    s_currentProfile.batFlying2ScoreP = score;
                    s_currentProfile.nivelflying2P = nivel;
                    s_currentProfile.generalP += nivel;
                    s_currentProfile.coordinacionP += nivel;
                }
                else
                {
                    s_currentProfile.batFlying2Score = score;
                    s_currentProfile.nivelflying2 = nivel;
                    s_currentProfile.general += nivel;
                    s_currentProfile.coordinacion += nivel;
                }
                   
                break;
            case "batForest":
                if (JugarManager.toggleValue)
                {
                    s_currentProfile.batForestScoreP = score;
                    s_currentProfile.nivelforestP = nivel;
                    s_currentProfile.generalP += nivel;
                    s_currentProfile.atencionP += nivel;
                }
                else
                {
                    s_currentProfile.batForestScore = score;
                    s_currentProfile.nivelforest = nivel;
                    s_currentProfile.general += nivel;
                    s_currentProfile.atencion += nivel;
                }
                   
                break;
            case "theVampire":
                if (JugarManager.toggleValue)
                {
                    s_currentProfile.theVampireScoreP = score;
                    s_currentProfile.nivelvampireP = nivel;
                    s_currentProfile.generalP += nivel;
                    s_currentProfile.atencionP += nivel;
                    s_currentProfile.razonamientoP += nivel;
                }
                else
                {
                    s_currentProfile.theVampireScore = score;
                    s_currentProfile.nivelvampire = nivel;
                    s_currentProfile.general += nivel;
                    s_currentProfile.atencion += nivel;
                    s_currentProfile.razonamiento += nivel;
                }
                    
                break;
            case "cementery":
                if (JugarManager.toggleValue)
                {
                    s_currentProfile.cementeryScoreP = score;
                    s_currentProfile.nivelcementeryP = nivel;
                    s_currentProfile.generalP += nivel;
                    s_currentProfile.memoriaP += nivel;
                }
                else
                {
                    s_currentProfile.cementeryScore = score;
                    s_currentProfile.nivelcementery = nivel;
                    s_currentProfile.general += nivel;
                    s_currentProfile.memoria += nivel;
                }
                
                break;
            case "eyes":
                if (JugarManager.toggleValue)
                {
                    s_currentProfile.eyesScoreP = score;
                    s_currentProfile.niveleyeP = nivel;
                    s_currentProfile.generalP += nivel;
                    s_currentProfile.coordinacionP += nivel;
                    s_currentProfile.atencionP += nivel;
                }
                else
                {
                    s_currentProfile.eyesScore = score;
                    s_currentProfile.niveleye = nivel;
                    s_currentProfile.general += nivel;
                    s_currentProfile.coordinacion += nivel;
                    s_currentProfile.atencion += nivel;
                }
                    
                break;
            case "jack":
                if (JugarManager.toggleValue)
                {
                    s_currentProfile.jackScoreP = score;
                    s_currentProfile.niveljackP = nivel;
                    s_currentProfile.generalP += nivel;
                    s_currentProfile.razonamientoP += nivel;
                    s_currentProfile.memoriaP += nivel;
                }
                else
                {
                    s_currentProfile.jackScore = score;
                    s_currentProfile.niveljack = nivel;
                    s_currentProfile.general += nivel;
                    s_currentProfile.razonamiento += nivel;
                    s_currentProfile.memoria += nivel;
                }
                    
                break;
            case "stroop":
                if (JugarManager.toggleValue)
                {
                    s_currentProfile.stroopScoreP = score;
                    s_currentProfile.nivelstroopP = nivel;
                    s_currentProfile.generalP += nivel;
                    s_currentProfile.atencionP += nivel;
                    s_currentProfile.fluidezVerbalP += nivel;
                }
                else
                {
                    s_currentProfile.stroopScore = score;
                    s_currentProfile.nivelstroop = nivel;
                    s_currentProfile.general += nivel;
                    s_currentProfile.atencion += nivel;
                    s_currentProfile.fluidezVerbal += nivel;
                }
                   
                break;
            case "portraits":
                if (JugarManager.toggleValue)
                {
                    s_currentProfile.portraitsScoreP = score;
                    s_currentProfile.nivelportraitsP = nivel;
                    s_currentProfile.generalP += nivel;
                    s_currentProfile.fluidezVerbalP += nivel;
                }
                else
                {
                    s_currentProfile.portraitsScore = score;
                    s_currentProfile.nivelportraits = nivel;
                    s_currentProfile.general += nivel;
                    s_currentProfile.fluidezVerbal += nivel;
                }
                    
                break;
            case "portraits2":
                if (JugarManager.toggleValue)
                {
                    s_currentProfile.portraits2ScoreP = score;
                    s_currentProfile.nivelportraits2P = nivel;
                    s_currentProfile.generalP += nivel;
                    s_currentProfile.memoriaP += nivel;
                }
                else
                {
                    s_currentProfile.portraits2Score = score;
                    s_currentProfile.nivelportraits2 = nivel;
                    s_currentProfile.general += nivel;
                    s_currentProfile.memoria += nivel;
                }
                    
                break;
        }
        s_currentProfile.newGame = false;

        var path = Application.streamingAssetsPath + "/Profiles/" + s_currentProfile.filename;
        SaveFile<ProfileData>(path, s_currentProfile);
    }

    public static ProfileIndex GetProfileIndex()
    {
        if(File.Exists(s_indexPath) == false)
        {
            return new ProfileIndex();
        }
        return LoadFile<ProfileIndex>(s_indexPath);
    }
 
    static void SaveFile<T>(string path, T data)
    {
        var profileWriter = new StreamWriter(path);
        var profileSerializer = new XmlSerializer(typeof(T));
        profileSerializer.Serialize(profileWriter, data);
    }
    public static void DeleteProfile(string filename)
    {
        var path = Application.streamingAssetsPath + "/Profiles/" + filename;
        File.Delete(path);

        var index = LoadFile<ProfileIndex>(s_indexPath);
        index.profileFileNames.Remove(filename);

        SaveFile<ProfileIndex>(s_indexPath, index);
    }
    static T LoadFile<T>(string path)
    {
        var profileReader = new StreamReader(path);
        var serializer = new XmlSerializer(typeof(T));
        var obj = (T)serializer.Deserialize(profileReader);
        profileReader.Dispose();

        return obj;
    }
}
