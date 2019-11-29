using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{

    [SerializeField]
    private float speed = 150;

    private Rigidbody2D rgbd;

    private Vector2 direction;

    float onScreen = 0;

    private EndOfTheWorld ZW;


    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        ZW = GameObject.FindGameObjectWithTag("Player").GetComponent<EndOfTheWorld>();
    }

    void FixedUpdate()
    {
        rgbd.velocity = direction * speed * Time.deltaTime;
    }

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    void Update()
    {
        if (ZW.isActived == false)
        {
            onScreen += Time.deltaTime;
        }
          
        if (onScreen >= 3)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerLife playerLife = other.gameObject.GetComponent<PlayerLife>();
            if (!playerLife.invulnerable && playerLife.Life > 1)
            {
                playerLife.invulnerable = true;
                playerLife.Life -= 2;
            }
            if (!playerLife.invulnerable && playerLife.Life <= 1)
            {
                playerLife.Life -= 2;
            }
            Destroy(gameObject);
        }
    }
}
