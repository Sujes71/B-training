using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptCinematic : MonoBehaviour
{
    public GameObject bat, c1, c2,c3, c4, c5, c6, c7, c8, c9, c10, title1, title2, aux1, aux2, aux3, aux4;
    float temp = 1, temp2 = 1, temp3 = 1, temp4 = 1;
    bool down = false, pressed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return) || pressed)
        {
            pressed = true;
            bat.transform.position = new Vector2(-0.09102109f, -0.242099f);
            aux1.SetActive(true);
            aux2.SetActive(true);
            aux3.SetActive(true);
            aux4.SetActive(true);
            c1.SetActive(true);
            c2.SetActive(true);
            c3.SetActive(true);
            c4.SetActive(true);
            c5.SetActive(true);
            c6.SetActive(true);
            c7.SetActive(true);
            c8.SetActive(true);
            c9.SetActive(true);
            c10.SetActive(true);
            title1.GetComponent<Animator>().enabled = false;
            title1.transform.localPosition = new Vector2(-0.93481f, 123);
            title1.SetActive(true);
            title2.SetActive(true);
            title2.GetComponent<Animator>().SetBool("parpadeo", true);
            title2.GetComponent<AudioSource>().enabled = true;
            if (temp4 >= 0)
                temp4 -= Time.deltaTime;
            else
            {
                SceneManager.LoadScene("NuevoPerfil");
            }
        }
        else
        {
            if (bat.transform.position.x > 0.3765943f)
                bat.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 25f * Time.deltaTime);
            else
            {
                if (temp >= 0)
                {
                    bat.GetComponent<SpriteRenderer>().flipX = true;
                    temp -= Time.deltaTime;
                }
                else
                {
                    bat.GetComponent<SpriteRenderer>().flipX = false;
                    title1.SetActive(true);
                    if (bat.transform.position.y > -1.83f && !down)
                    {
                        bat.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 80 * Time.deltaTime);
                    }
                    else
                    {
                        down = true;
                        if (temp2 >= 0)
                        {
                            temp2 -= Time.deltaTime;
                        }
                        else
                        {
                            title2.SetActive(true);
                            if (bat.transform.position.y < -1.5f)
                                bat.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 80f * Time.deltaTime);
                            else
                            {
                                title2.GetComponent<Animator>().SetBool("parpadeo", true);
                                if (temp3 >= 0)
                                    temp3 -= Time.deltaTime;
                                else
                                {
                                    aux1.SetActive(true);
                                    aux2.SetActive(true);
                                    aux3.SetActive(true);
                                    aux4.SetActive(true);
                                    c1.SetActive(true);
                                    c2.SetActive(true);
                                    c3.SetActive(true);
                                    c4.SetActive(true);
                                    c5.SetActive(true);
                                    c6.SetActive(true);
                                    c7.SetActive(true);
                                    c8.SetActive(true);
                                    c9.SetActive(true);
                                    c10.SetActive(true);
                                }
                            }

                        }
                    }
                }
            }
        }
    } 
}
