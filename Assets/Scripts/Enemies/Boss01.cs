using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss01 : MonoBehaviour
{
    #region Variables
    [Header("General Variables")]
    public Transform player;
    public Transform Position;
    private Animator anim;
    public LayerMask playerMask;
    private Rigidbody2D myRigidbody;
    public EndOfTheWorld ZW;
    public bool Close = false;
    public bool FacingRight = false;
    public bool JumpNow = false;
    public bool isRolling = false;
    public bool canDamage;
    public int FlipNow = 0;
    public int Flipped = 0;
    public int FlipState = 0;
    public int CanJump = 0;
    public float Speed = 0.08f;
    public float AlreadyJumped = 0;


    [Header("Attack and death Variables")]
    public float AttackTimer = 1.3f;
    public int damage = 3;
    public float Range = .40f;
    public float Cooldown = 5f;
    public float TimeStunned = 0;
    public int AttackType = 1;
    public int UltCharge = 0;
    public int Rolled = 0;
    public bool CanAttack;
    public Life Life;
    public Transform ShotPosition;
    public GameObject Missile;
    public GameObject Saw;

    [Header("UI Variables")]
    public Sprite[] barraInimigo;
    public Image barradevidaUIInimigo;
    #endregion

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //Life = GetComponent<Life>();
        //Life = GameObject.Find("Enemy").GetComponent<Life>();
    }

    void Update()
    {
        Attack();
        Death1();
        if (canDamage)
            Damage(GameObject.FindGameObjectWithTag("Player"));
        barradevidaUIInimigo.sprite = barraInimigo[Life.Lifes];
        


    }


    void FixedUpdate()
    {
        if (TimeStunned > 0)
            gameObject.tag = "Enemy";
        else
            gameObject.tag = "Stunned";
        #region ImClose;
        //Detects if the player is close from the enemy

        //Detect close left
        if (player.transform.position.x > transform.position.x && player.transform.position.x - transform.position.x < 7f)
        {
            Close = true;
        }

        //Detect close right
        if (player.transform.position.x < transform.position.x && player.transform.position.x - transform.position.x > -7f) //teste as linhas
        {
            Close = true;
        }
        #endregion

        #region ImFar;
        //Detects if the player is far from the enemy

        //Detect far left
        if (player.transform.position.x > transform.position.x && player.transform.position.x - transform.position.x < 12f && player.transform.position.x - transform.position.x > 7f)
        {
            Close = false;
        }

        //Detect far right
        else if (player.transform.position.x < transform.position.x && player.transform.position.x - transform.position.x > -12f && player.transform.position.x - transform.position.x < -7f) //teste depois a distância
        {
            Close = false;
        }
        #endregion

        #region Walk
        //Makes the boss moves

        //Right Walk
        if (UltCharge == 4 && Life.Lifes > 0 && FacingRight && AlreadyJumped >= 1)
        {
            transform.Translate(Speed * (Time.timeScale * Time.deltaTime), 0, 0);
            isRolling = true;
        }

        //Left Walk
        else if (UltCharge == 4 && Life.Lifes > 0 && !FacingRight && AlreadyJumped >= 1)
        {
            transform.Translate(-Speed * (Time.timeScale * Time.deltaTime), 0, 0);
            isRolling = true;
        }

        else
        {
            isRolling = false;
        }
        #endregion

        #region Flip
        //Flips the entire enemy according to player position

        //Right Flip
        if (Life.Lifes > 0 && FlipNow == 1) //tem que colocar as posições
        {
            FlipNow = 0;
            FlipState = 1;
            FacingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }

        //Left Flip
        else if (Life.Lifes > 0 && FlipNow == 2) //tem que colocar as posições
        {
            FlipNow = 0;
            FlipState = 2;
            FacingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        #endregion
    }

    #region Attack
    void Attack()
    {
        if (AttackType == 0)
        {
            AttackType = Random.RandomRange(1, 3);
        }

        if (JumpNow == true && CanJump > 1 && Life.Lifes > 0)
        {
            transform.Translate(0, 0.04f, 0);
        }

        #region Attacks Right
        if (FacingRight == true && Close == true && Life.Lifes > 0)
        {
            AttackTimer += Time.deltaTime;

            if (AttackTimer >= Cooldown)
            {
                CanAttack = true;
                AttackTimer = 0;
            }

            //Attack 1 Saw
            if (CanAttack == true && AttackType == 1 && UltCharge < 4)
            {
                CanAttack = false;
                AttackType++;
                anim.SetInteger("Parameter_Right", 1);
                Cooldown = 3.5f;
                UltCharge++;
                Rolled = 1;
            }

            //Attack 2 Missile
            if (CanAttack == true && AttackType == 2 && UltCharge < 4)
            {
                CanAttack = false;
                AttackType--;
                anim.SetInteger("Parameter_Right", 2);
                Cooldown = 3.5f;
                UltCharge++;
                Rolled = 1;
            }

            //Prepare to roll
            if (CanAttack == true && UltCharge == 4 && Flipped < 2)
            {
                CanAttack = false;
                AttackType = 0;
                Cooldown = 6f;
            }

            if (AlreadyJumped <= 1 && UltCharge == 4 && Flipped < 2)
            {
                anim.SetInteger("Parameter_Right", 3);
                AlreadyJumped += Time.deltaTime;
                transform.Translate(0, 0.04f, 0);
            }

            if (AlreadyJumped >= 1 && UltCharge == 4 && Flipped < 2 && Rolled == 1)
            {
                anim.SetInteger("Parameter_Right", 4);
                isRolling = true;
            }

        }

        //Stun Right
        if (Flipped >= 3 && FlipState == 2 && Rolled == 1 && Life.Lifes > 0)
        {
            isRolling = false;
            Cooldown = 1000;
            CanJump = 0;
            CanAttack = false;
            UltCharge = 0;
            anim.SetInteger("Parameter_Right", 5);
            if (!ZW.isActived)
                TimeStunned += Time.deltaTime;

            if (TimeStunned <= 1)
            {
                anim.SetInteger("Parameter_Right", 5);
            }

            if (TimeStunned >= 1.1)
            {
                anim.SetInteger("Parameter_Right", 0);
            }

            if (TimeStunned >= 5)
            {
                Flipped = -1;
                Cooldown = 3;
                AlreadyJumped = 0;
                TimeStunned = 0;
                AttackType = 0;
                Rolled = 0;
                isRolling = false;
            }
        }
        #endregion





        #region Attacks Left
        if (FacingRight == false && Close == true && Life.Lifes > 0)
        {
            AttackTimer += Time.deltaTime;

            if (AttackTimer >= Cooldown)
            {
                CanAttack = true;
                AttackTimer = 0;
            }

            //Attack 1 Saw
            if (CanAttack == true && AttackType == 1 && UltCharge < 4)
            {
                CanAttack = false;
                AttackType++;
                anim.SetInteger("Parameter_Left", 1);
                Cooldown = 3.5f;
                UltCharge++;
                Rolled = 2;
            }

            //Attack 2 Missile
            if (CanAttack == true && AttackType == 2 && UltCharge < 4)
            {
                CanAttack = false;
                AttackType--;
                anim.SetInteger("Parameter_Left", 2);
                Cooldown = 3.5f;
                UltCharge++;
                Rolled = 2;
            }

            //Prepare to roll
            if (CanAttack == true && UltCharge == 4 && Flipped < 2)
            {
                CanAttack = false;
                AttackType = 0;
                Cooldown = 6f;
                Rolled = 2;
            }

            if (AlreadyJumped <= 1 && UltCharge == 4 && Flipped < 2)
            {
                anim.SetInteger("Parameter_Left", 3);
                AlreadyJumped += Time.deltaTime;
                transform.Translate(0, 0.04f, 0);
            }

            if (AlreadyJumped >= 1 && UltCharge == 4 && Flipped < 2 && Rolled == 2)
            {
                anim.SetInteger("Parameter_Left", 4);
            }
        }

        //Stun Left
        if (Flipped >= 3 && FlipState == 1 && Rolled == 2 && Life.Lifes > 0)
        {
            isRolling = false;
            Cooldown = 1000;
            CanJump = 0;
            CanAttack = false;
            UltCharge = 0;
            if (!ZW.isActived)
                TimeStunned += Time.deltaTime;

            if (TimeStunned <= 1)
            {
                anim.SetInteger("Parameter_Left", 5);
            }

            if (TimeStunned >= 1.1)
            {
                anim.SetInteger("Parameter_Left", 0);
            }
            if (TimeStunned >= 5)
            {
                Flipped = -1;
                Cooldown = 3;
                AlreadyJumped = 0;
                TimeStunned = 0;
                AttackType = 0;
                Rolled = 0;
                isRolling = false;
            }
        }
        #endregion
    }
    #endregion

    #region Shooting Missile
    void ShootMissile()
    {
        if (FacingRight == true)
        {
            GameObject temp = (GameObject)Instantiate(Missile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            //temp.GetComponent<Missile>().Initialize(Vector2.right);
        }
        else
        {
            GameObject temp = (GameObject)Instantiate(Missile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))); //ta faltando mudar a pos do tiro
            //temp.GetComponent<Missile>().Initialize(Vector2.left);
        }
    }
    #endregion

    #region Shooting Saw
    void ShootSaw()
    {
        if (FacingRight == true)
        {
            GameObject temp = (GameObject)Instantiate(Saw, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            //temp.GetComponent<Missile>().Initialize(Vector2.right);
        }
        else
        {
            GameObject temp = (GameObject)Instantiate(Saw, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))); //ta faltando mudar a pos do tiro
            //temp.GetComponent<Missile>().Initialize(Vector2.left);
        }
    }
    #endregion

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PosBoss")
        {
            FlipNow = 1;
            Flipped += 1;
        }
        if (other.tag == "PosBoss2")
        {
            FlipNow = 2;
            Flipped += 1;
        }

        if (other.tag == "Ground")
        {
            JumpNow = true;
            CanJump++;
        }
    }

    void OnTriggerExit2D(Collider2D other2)
    {
        if (other2.tag == "Ground")
        {
            JumpNow = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
            canDamage = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
            canDamage = false;
    }

    void Damage(GameObject other)
    {
        if (isRolling)
        {
            Debug.Log("AIN PAI PARA");
            PlayerLife playerLife = other.GetComponent<PlayerLife>();
            if (!playerLife.invulnerable && playerLife.Life > 1)
            {
                playerLife.invulnerable = true;
                playerLife.Life -= damage;
            }
            if (!playerLife.invulnerable && playerLife.Life <= 1)
            {
                playerLife.Life -= damage;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Position.position, Range);
    }
    public void barradevida()
    {
        barradevidaUIInimigo.enabled = false;
    }

    #region Death
    void Death1()
    {
        if (Life.Lifes <= 0)
        {
            anim.SetTrigger("Death");
        }
        if (Life.Lifes <= 0)
        {
            Destroy(GetComponent<Rigidbody2D>());
            GetComponent<Collider2D>().enabled = false;
        }
    }
    #endregion
}