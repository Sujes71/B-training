using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaBase : MonoBehaviour
{
    // Start is called before the first frame update
    public  int valor;
    int pos;
    public Vector3 posNumero;
    public Vector3 [] posMurcielagos;
    public Vector3[] posMurcielagos2;
    public GameObject bat;
    public GameObject [] numero;
    private GameObject num;
    public NumericStroopManager cm;
    public bool generar;
    public float escala;
    void Start()
    {  
        generar = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (generar)
        {
            pos =Random.Range(1,cm.limite);
            valor = cm.listaNumeros[pos];
            cm.listaNumeros[pos] = cm.listaNumeros[cm.limite - 1];
            cm.listaNumeros[cm.limite- 1] = valor;
            cm.limite--;
            switch (cm.nivel)
            {
                case 1:
                    generarNumero();
                    generarMurcielagos();
                    break;
                case 2:
                    generarNumero();
                    generarMurcielagos();
                    break;
                case 3:
                    generarNumero();
                    break;
                case 4:
                    generarNumero();
                    break;
                case 5:
                    generarMurcielagos();
                    break;
                case 6:
                    generarMurcielagos();
                    break;
            }
            generar = false;
        }
        
    }

    public void generarNumero()
    {
        num = Instantiate(numero[valor-1],posNumero,Quaternion.identity,gameObject.transform);
        num.transform.localPosition = posNumero; 
        num.transform.localScale*=escala;
    }
    public void generarMurcielagos()
    {
        Vector3[] arrayLocation;
        if (escala == 0.6f) arrayLocation = posMurcielagos2;
        else arrayLocation = posMurcielagos;
            for (int i = 0; i < valor; i++)
            {
                GameObject auxBat;
                auxBat = Instantiate(bat, arrayLocation[i], Quaternion.identity, gameObject.transform);
                auxBat.transform.localScale *= escala;
                auxBat.transform.localPosition =arrayLocation[i];
                
            }
    }
    public void eliminarElementos()
    {
       for(int i = 0; i < gameObject.transform.childCount; i++)
        {
             Destroy(gameObject.transform.GetChild(i).gameObject);
        }
        
    }
    
}
