using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : MonoBehaviour
{
    public float LimitTime = 5f;
    public float ActualTime;
    Rigidbody2D Player;
    EndOfTheWorld TW;
    PlayerAttack ATK;
    Dash Dash;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Rigidbody2D>();
        TW = GetComponent<EndOfTheWorld>();
        ATK = GetComponent<PlayerAttack>();
        Dash = GetComponent<Dash>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.velocity.x > 0 || Player.velocity.y > 0 || Dash.isActived || TW.isActived || ATK.isAttacking)
            ActualTime = 0;
        if (ActualTime > LimitTime)
        {
            GetComponent<Animator>().SetTrigger("Cafe =)");
            ActualTime = 0;
        }

        ActualTime += Time.deltaTime;
    }
}
