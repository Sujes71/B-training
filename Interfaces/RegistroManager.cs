using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegistroManager : MonoBehaviour
{
    public InputField profileInput;
    public Text placeholder;
    public void BotonConfirmar()
    {
        var index = ProfileStorage.GetProfileIndex();
        if (profileInput.text.Length >= 3 && index.GetNumOfElements() < 8)
        {
            string profileName = this.profileInput.text;
            ProfileStorage.CreateNewGame(profileName);
            SceneManager.LoadScene("Introducción");
        }
        else if(profileInput.text.Length < 3)
        {
            profileInput.text = "";
            placeholder.text = "Escribe al menos 3 caracteres";
            placeholder.color = Color.red;
        }
        else if (index.GetNumOfElements() >= 6)
        {
            profileInput.text = "";
            placeholder.text = "Superado el límite de 8 perfiles";
            placeholder.color = Color.red;
        }
    }
    public void BotonAtras()
    {
        SceneManager.LoadScene("NuevoPerfil");
    }
}
