using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MvtPlayer : MonoBehaviour
{

    [Header("Componentes")]
    Rigidbody2D RbPlayer;
    public LayerMask WhatIsground;
    public Transform CheckGround1;
    public Transform CheckGround2;
    public Transform CheckGround3;
    public Animator Anim;

    [Header("variáveis")]
    public float Speed = 200;
    public float JumpForce = 5;
    float AxisX;
    float CheckContage = 0;
    public int vidaPlayer = 11;

    [Header("Bool")]
    public bool IsfacingRight = true;
    public bool check1 = false;
    public bool check2 = false;
    public bool check3 = false;
    public bool CanJumpAgain = false;
    //public bool Canmove = true;

    // Use this for initialization
    void Start()
    {
        RbPlayer = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        RbPlayer.velocity = new Vector2((AxisX * Speed) * Time.deltaTime, RbPlayer.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Canmove)
            AxisX = Input.GetAxisRaw("Horizontal");

        #region checks
        //Aumentar os checks
        check1 = Physics2D.OverlapCircle(CheckGround1.position, 0.02f, WhatIsground);
        check2 = Physics2D.OverlapCircle(CheckGround2.position, 0.02f, WhatIsground);
        check3 = Physics2D.OverlapCircle(CheckGround3.position, 0.02f, WhatIsground);
        #endregion

        if (AxisX > 0 && !IsfacingRight)
            Flip();
        if (AxisX < 0 && IsfacingRight)
            Flip();

        if (Input.GetButtonDown("Jump") && check1 && check2 && check3)
        {
            RbPlayer.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            CanJumpAgain = true;
        }
        if (Input.GetButtonDown("Jump") && CanJumpAgain && !check1 && !check2 && !check3)
        {
            RbPlayer.velocity = new Vector2(RbPlayer.velocity.x, 0);
            RbPlayer.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            CanJumpAgain = false;
        }
        

        Anim.SetFloat("Walk", Mathf.Abs(RbPlayer.velocity.x));
        Anim.SetFloat("JP", Mathf.Abs(RbPlayer.velocity.y));
        Anim.SetFloat("Jump", RbPlayer.velocity.y);
        

    }

    void Flip()
    {
        IsfacingRight = !IsfacingRight;
        float ScaleX = transform.localScale.x * -1;
        transform.localScale = new Vector3(ScaleX, transform.localScale.y, transform.localScale.z);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "PC")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //bota o tutorial ae e deleta o collider q eu botei
            }
        }

        if (other.tag == "Motel")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //bota o rolo compressor caindo do céu
            }
        }
    }

}
