using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileList : MonoBehaviour
{
    public Transform profilesHolder;
    public GameObject profileUIBoxPrefab;
    public int deleteConfirm;
    string perfilBorrar;
    void Start()
    {
        var index = ProfileStorage.GetProfileIndex();
        foreach (var profileName in index.profileFileNames)
        {
            perfilBorrar = "";
            var perfil = ProfileStorage.getProfile(profileName);
            var go = Instantiate(this.profileUIBoxPrefab);
            var uiBox = go.GetComponent<ProfileBoxUI>();

            uiBox.nameLabel.text = perfil.name;
            uiBox.fecha.text = perfil.fechaCreacion.ToString();
            //Click Load Button
            uiBox.loadBtn.onClick.AddListener(() =>
            {
                ProfileStorage.LoadProfile(profileName);
                SceneManager.LoadScene("Menu_Principal");
            });
            uiBox.deleteBtn.onClick.AddListener(() =>
            {
                if (uiBox.deleteBtn.transform.GetChild(0).GetComponent<Text>().text == "X")
                {
                    ProfileStorage.DeleteProfile(profileName);
                    Destroy(go);
                }
                uiBox.deleteBtn.transform.GetChild(0).GetComponent<Text>().text = "X";
            });
            go.transform.SetParent(this.profilesHolder, false);
        }
    }
}
