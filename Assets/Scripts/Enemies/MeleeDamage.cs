using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    public Transform Position;
    public LayerMask playerMask;
    public float Range = 0.35f;
    public int Damage = 1;

    public void DamagePlayer()
    {
        Collider2D[] Player = Physics2D.OverlapCircleAll(Position.position, Range, playerMask);
        for (int i = 0; i < Player.Length; i++)
        {
            PlayerLife playerLife = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
            if (!playerLife.invulnerable && playerLife.Life > 1)
            {
                playerLife.invulnerable = true;
                playerLife.Life -= Damage;
            }
            if (!playerLife.invulnerable && playerLife.Life <= 1)
            {
                playerLife.Life -= Damage;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Position.position, Range);
    }
}
