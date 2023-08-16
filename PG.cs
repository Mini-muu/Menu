using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PG : MonoBehaviour
{
    Vector2 pos_iniziale;
    Vector3 scale;

    float salto = 4f;
    float pos_y_pred;
    float orizontalMoovement = 3f;
    bool canJump = true;
    bool canRun = false;
    float positionBeforeJump = 0f;

    // private Rigidbody2D rb;
    // public float speed = 0.01f; // Velocitï¿½ di movimento
    [SerializeField] private Rigidbody2D rb;
    /*[SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;*/

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Ottieni il componente Rigidbody2D
        rb.freezeRotation = true; // Disabilita la rotazione del Rigidbody2D

        pos_iniziale = transform.position;
        pos_y_pred = transform.position.y;

        scale = transform.localScale;
    }

    void FixedUpdate()
    {}

    void Update()
    {
        // Resetta la posizione del PG se cade dallo schermo
        if (transform.position.y < -5.6f) transform.position = pos_iniziale;
        // Movimenti----------------------------------------------
        if (Input.GetKeyDown(KeyCode.V))
        {
            canRun = !canRun;
        }

        if (canRun)
        {
            orizontalMoovement = 10f;
        }
        else
        {
            orizontalMoovement = 3f;
        }

        if (Input.GetKey(KeyCode.A)) moovement(-orizontalMoovement);    // Aposto
        /*{
            //scale.x = -0.1f; //Nel mio caso rovina l'immagine
        }*/

        else if (Input.GetKey(KeyCode.D)) moovement(+orizontalMoovement);   //Aposto

        if (Input.GetButtonDown("Jump"))
        {
            if (positionBeforeJump >= transform.position.y)
            {
                canJump = false;
                positionBeforeJump = transform.position.y;
                rb.AddForce(Vector2.up * salto, ForceMode2D.Impulse);
                Debug.Log("canJump "+canJump);
            }
            if (positionBeforeJump >= transform.position.y)
            {
                Debug.Log("Position "+(positionBeforeJump >= transform.position.y));
                canJump = true;
            }
            Debug.Log((transform.position.y <= positionBeforeJump) + "\n" + "transform.position.y " + transform.position.y + ", positionBeforeJump " + positionBeforeJump + " canJump " + canJump);
        }

        /*if (canJump) {
            Debug.Log("Ground check");
        }*/

        //if (groundCheck) canJump = true;
        // -------------------------------------------------------
    }

    void moovement(float inc)
    {
        transform.localScale = scale;
        transform.position = new Vector2(transform.position.x + inc * Time.deltaTime, transform.position.y);
    }
}