using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    public float speed;
    public int points;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<BatForestManager>().comienzo)
        {
            Vector3 movementInput = Vector3.zero;
            if (Input.GetKey(KeyCode.UpArrow))
                movementInput.y = 1;
            else if (Input.GetKey(KeyCode.DownArrow))
                movementInput.y = -1;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movementInput.x = -1;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                movementInput.x = 1;
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            Move(movementInput);
        }
    }
    public void Move(Vector3 direccion)
    {
        transform.position += direccion.normalized * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (points == 4)
            FindObjectOfType<BatForestManager>().panelVerde = true;
        else if (points == 9)
            FindObjectOfType<BatForestManager>().panelVerde = true;
        else if (points == 14)
            FindObjectOfType<BatForestManager>().panelVerde = true;
        else if (points == 19)
            FindObjectOfType<BatForestManager>().panelVerde = true;
        else if (points == 24)
            FindObjectOfType<BatForestManager>().panelVerde = true;
        if (collision.tag == "Fruta")
        {
            if(!FindObjectOfType<BatForestManager>().comienzo)
                points += 1;
            FindObjectOfType<BatForestManager>().score.text = "Score: " + points;
            FindObjectOfType<BatForestManager>().hayFruta = false;
            FindObjectOfType<BatForestManager>().r1 = false;
            FindObjectOfType<BatForestManager>().r2 = false;
            FindObjectOfType<BatForestManager>().r3 = false;
            FindObjectOfType<BatForestManager>().r4 = false;
            FindObjectOfType<BatForestManager>().r5 = false;
            FindObjectOfType<BatForestManager>().r6 = false;
            FindObjectOfType<BatForestManager>().r7 = false;
            FindObjectOfType<BatForestManager>().r8 = false;
            FindObjectOfType<BatForestManager>().fake = false;
            foreach (GameObject item in FindObjectOfType<BatForestManager>().copiasFake)
            {
                Destroy(item);
            }
            Destroy(collision.gameObject);
        }
        LevelSystem();
    }
    void LevelSystem()
    {
        switch (FindObjectOfType<BatController>().points)
        {
            case 6:
               FindObjectOfType<BatForestManager>().tiempoNivel = 3;
                FindObjectOfType<BatForestManager>().level++;
                FindObjectOfType<BatForestManager>().nivelText.text = "Nivel 2";
                break;
            case 12:
                FindObjectOfType<BatForestManager>().tiempoNivel = 3;
                FindObjectOfType<BatForestManager>().level++;
                FindObjectOfType<BatForestManager>().nivelText.text = "Nivel 3";
                break;
            case 18:
                FindObjectOfType<BatForestManager>().tiempoNivel = 3;
                FindObjectOfType<BatForestManager>().level++;
                FindObjectOfType<BatForestManager>().nivelText.text = "Nivel 4";
                break;
            case 24:
                FindObjectOfType<BatForestManager>().tiempoNivel = 3;
                FindObjectOfType<BatForestManager>().level++;
                FindObjectOfType<BatForestManager>().nivelText.text = "Nivel 5";
                break;
            case 30:
                FindObjectOfType<BatForestManager>().tiempoNivel = 3;
                FindObjectOfType<BatForestManager>().level++;
                FindObjectOfType<BatForestManager>().nivelText.text = "Nivel 6";
                break;
        }
    }
}
