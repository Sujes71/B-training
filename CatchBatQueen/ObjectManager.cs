using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectManager : MonoBehaviour
{
    public static float interval = 4, intervalKing = 1;
    private Animator animator;
    public static bool rachita = false, racha = false, rachon = false;
    public static int numRacha = 0;
    public static bool panelRojo = false;
    public static int vidas = 3;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 1)
            Desplazamientos();
        DestruccionPorTiempo();
        
    }
    private void OnMouseDown()
    {
        if (gameObject.tag == "Enemy")
        {
            animator.SetBool("vivo", false);
            if(gameObject.name == "BatKing(Clone)")
            {
                if(!racha && !rachita && !rachon)
                {
                    numRacha++;
                    CatchBatQueenManager.score += 15;
                    CatchBatQueenManager.masMenosPuntos = 15;
                }
                else if(rachita && !racha && !rachon)
                {
                    numRacha++;
                    CatchBatQueenManager.score += 15 * 1.5f;
                    CatchBatQueenManager.masMenosPuntos = 15 * 1.5f;
                }
                else if(rachita && racha && !rachon)
                {
                    numRacha++;
                    CatchBatQueenManager.score += 15 * 2;
                    CatchBatQueenManager.masMenosPuntos = 15 * 2;
                }
                else
                {
                    numRacha++;
                    CatchBatQueenManager.score += 15 * 2.5f;
                    CatchBatQueenManager.masMenosPuntos = 15 * 2.5f;
                }
            }
            else
            {
                if (!racha && !rachita && !rachon)
                {
                    numRacha++;
                    CatchBatQueenManager.score += 5;
                    CatchBatQueenManager.masMenosPuntos = 5;
                }
                else if (rachita && !racha && !rachon)
                {
                    numRacha++;
                    CatchBatQueenManager.score += 5 * 1.5f;
                    CatchBatQueenManager.masMenosPuntos = 5 * 1.5f;
                }
                else if (rachita && racha && !rachon)
                {
                    numRacha++;
                    CatchBatQueenManager.score += 5 * 2;
                    CatchBatQueenManager.masMenosPuntos = 5 * 2;
                }
                else
                {
                    numRacha++;
                    CatchBatQueenManager.score += 5 * 2.5f;
                    CatchBatQueenManager.masMenosPuntos = 5 * 2.5f;
                }
            }
        }
        else if (gameObject.tag == "Friend")
        {
            animator.SetBool("vivo", false);
            rachita = false;
            rachon = false;
            racha = false;
            numRacha = 0;
            CatchBatQueenManager.score -= 10;
            CatchBatQueenManager.masMenosPuntos = -10;
            panelRojo = true;
            CatchBatQueenManager.tiempoPanel = 2;
            if(vidas == 3)
            {
                Destroy(FindObjectOfType<CatchBatQueenManager>().vida3); 
            }
            else if(vidas == 2)
            {
                Destroy(FindObjectOfType<CatchBatQueenManager>().vida2);
            }
            else
                Destroy(FindObjectOfType<CatchBatQueenManager>().vida1);
            vidas--;
        }
        switch (numRacha)
        {
            case 5:
                rachita = true;
                CatchBatQueenManager.tiempoPanelRacha = 2;
                break;
            case 10:
                racha = true;
                CatchBatQueenManager.tiempoPanelRacha = 2;
                break;
            case 15:
                rachon = true;
                CatchBatQueenManager.tiempoPanelRacha = 2;
                break;
        }
        CatchBatQueenManager.anObjectIsInstantiate = false;
        Destroy(gameObject,0.7f);
        ControlInterval();
    }
    void DestruccionPorTiempo()
    {
        if(gameObject.name == "BatKing(Clone)")
        {
            if (intervalKing >= 0)
                intervalKing -= Time.deltaTime;
            else{
                
                Destroy(gameObject);
                rachita = false;
                rachon = false;
                racha = false;
                numRacha = 0;
                CatchBatQueenManager.anObjectIsInstantiate = false;
                ControlInterval();
            }
        }
        else
        {
            if (interval >= 0)
                interval -= Time.deltaTime;
            else
            {
                if (gameObject.tag == "Enemy")
                {
                    animator.SetBool("teleport", true);
                    Destroy(gameObject, 0.4f);
                    
                }
                else if (gameObject.tag == "Friend")
                {
                    animator.SetBool("teleport", true);
                    Destroy(gameObject, 0.4f);
                }
                if (gameObject.tag == "Enemy")
                {
                    rachita = false;
                    rachon = false;
                    racha = false;
                    numRacha = 0;
                }
                CatchBatQueenManager.anObjectIsInstantiate = false;
                ControlInterval();
            }
        }   
    }
    void ControlInterval()
    {
        
        if (JugarManager.toggleValue)
        {
            intervalKing = 0.5f;
            interval = 3;
            if (CatchBatQueenManager.gameTime >= 250)
                interval = 2.5f;
            else if (CatchBatQueenManager.gameTime < 250 && CatchBatQueenManager.gameTime >= 200)
                interval = 2f;
            else if (CatchBatQueenManager.gameTime < 200 && CatchBatQueenManager.gameTime >= 150)
                interval = 1.5f;
            else if (CatchBatQueenManager.gameTime < 150 && CatchBatQueenManager.gameTime >= 100)
                interval = 1f;
            else if (CatchBatQueenManager.gameTime < 100 && CatchBatQueenManager.gameTime >= 50)
                interval = 0.75f;
            else
                interval = 0.5f;
        }
        else
        {
            intervalKing = 1;
            if (CatchBatQueenManager.gameTime >= 250)
                interval = 4;
            else if (CatchBatQueenManager.gameTime < 250 && CatchBatQueenManager.gameTime >= 200)
                interval = 3;
            else if (CatchBatQueenManager.gameTime < 200 && CatchBatQueenManager.gameTime >= 150)
                interval = 2.5f;
            else if (CatchBatQueenManager.gameTime < 150 && CatchBatQueenManager.gameTime >= 100)
                interval = 2f;
            else if (CatchBatQueenManager.gameTime < 100 && CatchBatQueenManager.gameTime >= 50)
                interval = 1.5f;
            else
                interval = 1f;
        }
    }
    void Desplazamientos()
    {
        if (gameObject.tag == "Enemy")
        {
               if(gameObject.name == "BatQueenAtack1(Clone)")
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x - 3 * Time.deltaTime,gameObject.transform.position.y);
                }
                if (gameObject.name == "BatQueenAtack2(Clone)")
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 3 * Time.deltaTime);
                }
                if (gameObject.name == "BatQueenAtack3(Clone)")
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x - 3 * Time.deltaTime, gameObject.transform.position.y);
                }
            if (gameObject.name == "BatQueenAtack1 Variant(Clone)")
            {
                gameObject.transform.position = new Vector2(gameObject.transform.position.x + 3 * Time.deltaTime, gameObject.transform.position.y);
            }
            if (gameObject.name == "BatQueenAtack2 Variant(Clone)")
            {
                gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 3 * Time.deltaTime);
            }
            if (gameObject.name == "BatQueenAtack3 Variant(Clone)")
            {
                gameObject.transform.position = new Vector2(gameObject.transform.position.x + 3 * Time.deltaTime, gameObject.transform.position.y);
            }
        }
        else
        {
                if (gameObject.name == "BatFlying Variant(Clone)")
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x - 3 * Time.deltaTime, gameObject.transform.position.y);
                }
            if (gameObject.name == "BatFlying Variant2(Clone)")
            {
                gameObject.transform.position = new Vector2(gameObject.transform.position.x + 3 * Time.deltaTime, gameObject.transform.position.y);
            }
        }
    }
}
