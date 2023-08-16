using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 pos_iniziale;

    public InputAction playerControls;

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 6f;
    private bool isFacingRight = true;
    //private bool canJump = false;
    private int nJumps = 0;
    private float mainAttack = 1;
    private float superAttack = 3;
    private float mainKnockback = 1; //Rinculo
    float newPositionX;
    private bool canAttack = true;
    private float health = 10;
    //private float superKnockback = 1; //Rinculo

    private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    //[SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        pos_iniziale = transform.position;
        newPositionX = transform.position.x;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontal * speed));
        //Debug.Log("Can attack: "+canAttack+", newPositionX: "+newPositionX+", rb velocity: "+rb.velocity+", object transform x: "+transform.position.x);

        // Resetta la posizione del PG se cade dallo schermo
        if (transform.position.y < -5.6f) transform.position = pos_iniziale;

        if (Input.GetButtonDown("Jump") && nJumps < 2 /*&& canJump*/)
        {
            nJumps++;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        movement();
        Flip();
    }

    private void FixedUpdate()
    {
        if (canAttack) rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Player "+rb.simulated.ToString());
        Debug.Log(collision.gameObject.name + ", tag: "+ collision.collider.tag + ", Layer: " + collision.gameObject.layer + "\n" + "Terrain position y: " + collision.transform.position.y + ", Dino y: " + rb.transform.position.y);
        if (collision.collider.tag == "Terrain")
        {
            nJumps = 0;
            //canJump = true;
        }
    }

    //************ Codice per far si che salti una sola volta usando una variabile booleana ************
    /*void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Terrain" && collision.gameObject.layer == 6)
        {
            canJump = false;
        }
    }*/

    /*private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundLayer);
    }*/

    private void Flip()
    {
        //Codice per cambiare la direzione dell'immagine del personaggio
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void movement() {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            float incDec;
            if (isFacingRight)
            {
                incDec = -1;
            } else {
                incDec = 1;
            }
            newPositionX = transform.position.x + incDec * getMainKnockback();
            canAttack = false;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right mouse clicked");
        }

        if (canAttack == false)
        {
            if (isFacingRight) {
                //incDec = -getMainKnockback();
                if (transform.position.x >= newPositionX)
                {
                    rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
                }
                else
                {
                    canAttack = true;
                }
            } else {
                //incDec = getMainKnockback();
                if (transform.position.x <= newPositionX)
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
                else
                {
                    canAttack = true;
                }
            }
        }
    }

    //Metodi getter e setter
    public float getMainAttack() { return mainAttack; }
    public void setMainAttack(float reduceMainAttack)
    {
        mainAttack -= reduceMainAttack;
    }
    public float getMainKnockback() { return mainKnockback; }
    public void setMainKnockback(float reduceKnockback) {
        mainKnockback -= reduceKnockback;
    }
    public float getSuperAttack() { return superAttack; }
    public void setSuperAttack(float reduceSuperAttack)
    {
        mainAttack -= reduceSuperAttack;
    }

    public void incrementHealth(float add)
    {
        health += add;
    }
    public void reduceHealth(float reduce) { 
        health -= reduce;
    }
    public float getHealth() {
        return health;
    }
}