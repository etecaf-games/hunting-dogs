using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Saw : MonoBehaviour
{
    [SerializeField]
    private float speed = 250;

    private Rigidbody2D rgbd;

    private float direction;

    public string bossName;

    float onScreen = 0;

    private EndOfTheWorld ZW;

    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        ZW = GameObject.FindGameObjectWithTag("Player").GetComponent<EndOfTheWorld>();
        direction = GameObject.Find(bossName).GetComponent<Transform>().localScale.x;
        transform.localScale = GameObject.Find(bossName).GetComponent<Transform>().localScale;
    }

    // Update is called once per frame
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
