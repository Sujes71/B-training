using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackController : MonoBehaviour
{
    public Textos textos;
    public static bool inicio = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!inicio)
        {
            inicio = true;
            FindObjectOfType<CartelController>().ActivarCartel(textos);
            
        }
    }
    private void OnMouseDown()
    {
        FindObjectOfType<CartelController>().ActivarCartel(textos);
    }
}
