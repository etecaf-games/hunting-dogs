using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy03 : MonoBehaviour
{
    #region Variables
    [Header("General Variables")]
    public Transform player;
    private Animator anim;
    private Rigidbody2D myRigidbody;
    public bool Close = false;
    public bool Walking = false;
    public bool Idle = true;
    public bool FacingRight;
    public PlayerLife detecting;
    public KilledAll KA;
    public float Speed = 0.04f;


    [Header("Attack and death Variables")]
    public float AttackTimer = 1.3f;
    public float Cooldown = 5f;
    bool CanAttack;
    public bool FirstAttackDone = false;
    //public int Damage = 1;
    Life Life;
    public float VanishTimer = 0;
    public Transform ShotPosition;
    public GameObject Bullet;
    public EndOfTheWorld ZW;
    //public PlayerLife playerLife;

    [Header ("UI Variables")]
    public Sprite[] barraInimigo3;
    public Image barradevidaUIInimigo3;

    #endregion

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Life = GetComponent<Life>();
        //ZW = GameObject.FindGameObjectWithTag("Player").GetComponent<EndOfTheWorld>();
        Life = GameObject.Find("Enemy").GetComponent<Life>();
        //KA++;
    }

    void Update()
    {
        Attack();
        Death();
        barradevidaUIInimigo3.sprite = barraInimigo3[Life.Lifes];

    }

    void FixedUpdate()
    {
        if (detecting.Life == 0)
        {
            Close = false;
            Walking = false;
            anim.SetInteger("Speed", 2);
        }
        else
        {
            #region ImClose;
            //Detects if the player is close from the enemy

            //Detect close left
            if (player.transform.position.x > transform.position.x && player.transform.position.x - transform.position.x < 3f)
            {
                Close = true;
                Walking = false;
            }

            //Detect close right
            if (player.transform.position.x < transform.position.x && player.transform.position.x - transform.position.x > -3f)
            {
                Close = true;
                Walking = false;
            }
            #endregion

            #region ImFar;
            //Detects if the player is far from the enemy

            //Detect far left
            if (player.transform.position.x > transform.position.x && player.transform.position.x - transform.position.x < 8f && player.transform.position.x - transform.position.x > 3f)
            {
                Close = false;
            }

            //Detect far right
            else if (player.transform.position.x < transform.position.x && player.transform.position.x - transform.position.x > -8f && player.transform.position.x - transform.position.x < -3f)
            {
                Close = false;
            }
            #endregion

            #region Walk
            //Makes the enemy moves

            //Right Walk
            if (player.transform.position.x > transform.position.x && player.transform.position.x - transform.position.x < 4f && Close == false && Life.Lifes > 0) //&& anim.GetInteger("Speed" == 2)
            {
                transform.Translate(Speed * (Time.timeScale * Time.deltaTime), 0, 0);
                anim.SetInteger("Speed", 1);
                Idle = false;
                Walking = true;
                AttackTimer += Time.deltaTime;
            }

            //Left Walk
            else if (player.transform.position.x < transform.position.x && player.transform.position.x - transform.position.x > -4f && Close == false && Life.Lifes > 0) //&& anim.GetInteger("Speed" == 2)
            {
                transform.Translate(-Speed * (Time.timeScale * Time.deltaTime), 0, 0);
                anim.SetInteger("Speed", 1);
                Idle = false;
                Walking = true;
                AttackTimer += Time.deltaTime;
            }

            //Makes Idle
            //Values indicate when enemy starts idle
            else if (player.transform.position.x > transform.position.x && player.transform.position.x - transform.position.x < 50f && player.transform.position.x - transform.position.x > 4.1f && Life.Lifes > 0 || player.transform.position.x < transform.position.x && player.transform.position.x - transform.position.x > -50f && player.transform.position.x - transform.position.x < -4.1f && Life.Lifes > 0)
            {
                Walking = false;
                Idle = true;
                anim.SetInteger("Speed", 2);
                AttackTimer += Time.deltaTime;
            }
            #endregion

            #region Flip
            //Flips the entire enemy according to player position

            //Right Flip
            if (player.transform.position.x > transform.position.x && player.transform.position.x - transform.position.x < 7f && Life.Lifes > 0)
            {
                FacingRight = true;
                transform.localScale = new Vector3(1, 1, 1);
            }

            //Left Flip
            else if (player.transform.position.x < transform.position.x && player.transform.position.x - transform.position.x > -7f && Life.Lifes > 0)
            {
                FacingRight = false;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            #endregion
        }
    }

    #region Attack
    void Attack()
    {
        if (Close == true)
        {
            anim.SetInteger("Speed", 0);
            if (!ZW.isActived)
                AttackTimer += Time.deltaTime;
            if (AttackTimer >= Cooldown)
            {
                CanAttack = true;
                AttackTimer = 0;
            }

            if (CanAttack == true)
            {
                CanAttack = false;
                anim.SetTrigger("Attack");
            }
        }
        if (Close == true && CanAttack == false)
        {
            anim.SetTrigger("WaitAttack");
        }

    }
    #endregion

    #region Shooting
    void Shoot()
    {
        if (FacingRight == true)
        {
            GameObject temp = (GameObject)Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            temp.GetComponent<Bullet>().Initialize(Vector2.right);
        }
        else
        {
            GameObject temp = (GameObject)Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))); //ta faltando mudar a pos do tiro
            temp.GetComponent<Bullet>().Initialize(Vector2.left);
        }
    }
    #endregion

    #region Death
    void Death()
    {
        if (Life.Lifes <= 0 && Walking == true || Life.Lifes <= 0 && Close == true || Life.Lifes <= 0 && Close == false && CanAttack == false)
        {
            anim.SetTrigger("Death2");
        }
        if (Life.Lifes <= 0 && Idle == true)
        {
            anim.SetTrigger("Death");
        }
        if (Life.Lifes <= 0)
        {
            VanishTimer += Time.deltaTime;
            Destroy(GetComponent<Rigidbody2D>()); //this shit is temporary
            GetComponent<Collider2D>().enabled = false;
        }
        if (VanishTimer >= 5f)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    void OnCollisionEnter2D(Collision2D collision) //test with collider later!
    {
        if (collision.gameObject.tag == "Player" && Life.Lifes <= 0)
        {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
        }
    }
    public void barradevida()
    {
        barradevidaUIInimigo3.enabled = false;
    }
}
