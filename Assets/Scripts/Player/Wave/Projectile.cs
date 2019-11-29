using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed = 650;

    private Rigidbody2D rgbd;

    private float direction;

    float onScreen = 0;

    private EndOfTheWorld ZW;


    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        ZW = GameObject.FindGameObjectWithTag("Player").GetComponent<EndOfTheWorld>();
        direction = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().localScale.x;
        transform.localScale = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().localScale;
    }

    void FixedUpdate()
    {
        rgbd.velocity = new Vector2(direction * speed * Time.deltaTime,0);
    }

    void Update()
    {
        if (ZW.isActived == false)
        {
            onScreen += Time.deltaTime;
        }

        if (onScreen >= .6f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Life enemyLife = other.gameObject.GetComponent<Life>();
            if (enemyLife.Lifes > 1)
            {
                enemyLife.Lifes -= 2;
            }
            if (enemyLife.Lifes <= 1)
            {
                enemyLife.Lifes -= 2;
            }
            
        }
    }
}
