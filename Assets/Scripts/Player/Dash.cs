using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    [Header("Bools")]
    public bool isActived = false;
    bool canUse = true;
    [Header("Player")]
    public MvtPlayer player;
    public float intensity = 10;
    int direction;
    bool entered = false;
    float inten;
    [Header("Time")]
    float currentTime;
    public float CDR = 5;
    public float Duration = 0.85f;


    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (Input.GetButtonUp("Dash") && canUse && gameObject.GetComponent<Rigidbody2D>().velocity.y == 0 &&! entered)
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
            }
            else
            {
                Desactive();
            }
        }

        if (!canUse && currentTime > CDR)
            canUse = true;


    }

    void Active()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Dash");
        player.enabled = false;
        inten = intensity;

        if (player.IsfacingRight)
            direction = 1;
        else
            direction = -1;

        currentTime = 0;
        canUse = false;
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
