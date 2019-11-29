using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Variables")]
    public float timeBtwAttacks;
    public Animator animator;
    public float defaultTimeBtwAttacks = 0.1f;
    public float attackRange = 0.5f;
    public int Damage;
    public int actualDamage;
    public int Animation;
    [Header("References")]
    public Transform attackPosition;
    public LayerMask Enemy1, bossLayer;
    public MvtPlayer Player;
    [Header("bool")]
    public bool isAttacking = false;

    void Start()
    {
        actualDamage = Damage;
    }

    void Update()
    {
        Damage = Animation + 1;
        if (Input.GetButtonDown("Fire1") && gameObject.GetComponent<Rigidbody2D>().velocity.x == 0 && gameObject.GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            if (timeBtwAttacks <= 0.23f && Animation == 0 || timeBtwAttacks <= 0.20f && Animation == 1 || timeBtwAttacks <= 0.25f && Animation == 2)
            {
                Attack();
                timeBtwAttacks = defaultTimeBtwAttacks;
            }
        }

        if (timeBtwAttacks <= 0.8 && isAttacking)
            Add();

        if (timeBtwAttacks < -0.1f && timeBtwAttacks < 0.1f || Animation > 2)
            Animation = 0;
        if (timeBtwAttacks >= 0.18f && timeBtwAttacks <= 0.2f && Animation == 0)
            Player.enabled = true;
        if (timeBtwAttacks >= 0.16f && timeBtwAttacks <= 0.18f && Animation == 1)
            Player.enabled = true;
        if (timeBtwAttacks >= 0.18f && timeBtwAttacks <= 0.2f && Animation == 2)
            Player.enabled = true;
        timeBtwAttacks -= Time.deltaTime;

        
    }

    void Add()
    {
        Animation++;
        isAttacking = false;
    }

    public void Attack()
    {
        Player.enabled = false;
        isAttacking = true;
        Collider2D[] Enemys = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, Enemy1);
        if (Enemys.Length > 0)
        {
            for (int i = 0; i < Enemys.Length; i++)
            {
                //Enemys[i].GetComponent<Enemy Health Script Here>.health -= damage;
                if (this.gameObject.GetComponent<EndOfTheWorld>().isActived)
                    Enemys[i].GetComponent<Life>().storedLife += Damage;
                else
                    Enemys[i].GetComponent<Life>().Lifes -= Damage;
            }
        }
        Collider2D[] Boss = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, bossLayer);
        if (Boss.Length > 0)
        {
            for (int i = 0; i < Boss.Length; i++)
            {
                if(Boss[i].gameObject.tag == "Enemy")
                {
                //Enemys[i].GetComponent<Enemy Health Script Here>.health -= damage;
                if (this.gameObject.GetComponent<EndOfTheWorld>().isActived)
                    Boss[i].GetComponent<Life>().storedLife += Damage;
                else
                    Boss[i].GetComponent<Life>().Lifes -= Damage;
                }
            }
        }
        animator.SetInteger("Attack", Animation);
        animator.SetTrigger("isAttacking");
        //Enemys.Length = new Collider2D(0);
        //Animation++;
        
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
