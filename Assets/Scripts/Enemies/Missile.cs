using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Missile : MonoBehaviour
{
    public GameObject target;
    private Rigidbody2D rb;
    private Animator anim;    

    public float speed = 2f;
    public float speedRotate = 200f;
    public float VanishTimer = 0;
    float onScreen = 0;
    public bool Touched = false;

    private EndOfTheWorld ZW;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ZW = GameObject.FindGameObjectWithTag("Player").GetComponent<EndOfTheWorld>();
    }

    void FixedUpdate()
    {
            Vector2 pointDir = (Vector2)transform.position - (Vector2)target.transform.position;

            pointDir.Normalize();

            float value = Vector3.Cross(pointDir, transform.right).z;

            rb.angularVelocity = speedRotate * value;

            rb.velocity = transform.right * speed;

        if (Touched == true)
        {
            anim.SetTrigger("Explosion");
            speed = 0f;
            speedRotate = 0;
            VanishTimer += Time.deltaTime;
            GetComponent<Collider2D>().enabled = false;
        }

        if (VanishTimer >= 1)
        {
            Destroy(gameObject);
        }

        //if (ZW.isActived == false)
        //{
            onScreen += Time.deltaTime;
        //}

        if (onScreen >= 10)
        {
            Touched = true;
        }

        //if (value >= 0)
        //{
        //    rb.angularVelocity = speedRotate;
        //}
        //else if (value < 0)
        //{
        //    rb.angularVelocity = -speedRotate;
        //}
        //else
        //{
        //    speedRotate = 0;
        //}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Touched = true;
            PlayerLife playerLife = other.gameObject.GetComponent<PlayerLife>();
            if (!playerLife.invulnerable && playerLife.Life > 1)
            {
                playerLife.invulnerable = true;
                playerLife.Life -= 1;
            }
            if (!playerLife.invulnerable && playerLife.Life <= 1)
            {
                playerLife.Life -= 1;
            }
        }
    }
}
