using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAbility : MonoBehaviour
{
    [Header("Bools")]
    bool isActived = false;
    bool canUse = true;
    [Header("Player")]
    public MvtPlayer player;
    public Transform Position;
    public LayerMask whatIsEnemy;
    public float range = 0.2f;
    public float intensity = 10;
    public int damage = 2;
    public bool entered = false;
    int direction;
    float inten;
    [Header("Time")]
    public float currentTime;
    public float CDR = 5;
    public float Duration = 0.85f;


    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (Input.GetButtonUp("Dragon") && canUse && GetComponent<EndOfTheWorld>().canstop &&! entered && GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            Active();
        }

        if (isActived)
        {
            if (currentTime < Duration)
            {

                float X = transform.position.x;
                X += inten * direction;
                transform.position = new Vector2(X, transform.position.y);
                if (inten > 0)
                    inten -= 0.02f;
                Physics2D.IgnoreLayerCollision(10, 11, true);
                Attack();
            }
            else
            {
                Desactive();
            }
        }


        if (!canUse && currentTime > CDR)
            canUse = true;

    }

    public void Attack()
    {
        Collider2D[] Enemys = Physics2D.OverlapCircleAll(Position.position, range, whatIsEnemy);
        for (int i = 0; i < Enemys.Length; i++)
        {
            Enemys[i].GetComponent<Life>().Lifes -= damage;
            //Enemys[i].GetComponent<TomeiDanoNiisan>().Invoke("Nisan", 0);
        }

    }

    void Active()
    {
        inten = intensity;
        currentTime = 0;
        if (player.IsfacingRight)
            direction = 1;
        else
            direction = -1;
        player.enabled = false;
        gameObject.GetComponent<Animator>().SetTrigger("Arkazon");
        canUse = false;
        
        //isActived = true;
    }

    public void Arkazon()
    {
        //currentTime = 0;
        isActived = true;
    }

    void Desactive()
    {
        isActived = false;
        currentTime = 0;
        Physics2D.IgnoreLayerCollision(10, 11, false);
        player.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Desactive();
            entered = true;
            Debug.Log("mamaaaaaaaaaaaaa");
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
            if (collision.tag == "Wall")
            {
                Desactive();
                entered = false;
                Debug.Log("mamaaaaaaaaaaaaa");
            }
    }
}
