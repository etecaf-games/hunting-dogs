using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int Life = 10;
    Animator animator;
    MvtPlayer player;
    EndOfTheWorld TW;
    Dash dash;
    public GameObject[] Enemies;
    public SpriteRenderer Sprite;
    #region Variables Damage
    [Header("Damage")]
    public float maxBlinkingTime = 1f;
    public float minBlinkingTime = 0.2f;
    public float actualBlinkingTime = 0f;
    public float actualTotalTime;
    public bool invulnerable = false;
    #endregion
    bool died = false;

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Life <= 0 &&! died)
            Death();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("died"))
        {
            //insira o hud de morte aqui
            //Debug.Log("AI");
        }
        if (invulnerable)
            Damage();
    }

    public void Damage()
    {
        actualBlinkingTime += Time.deltaTime;
        actualTotalTime += Time.deltaTime;
        if (maxBlinkingTime >= actualTotalTime)
        {
            if (actualBlinkingTime >= minBlinkingTime)
            {
                actualBlinkingTime = 0;
                Blinking();
            }

        }
        else
        {
            invulnerable = false;
            Sprite.enabled = true;
            actualBlinkingTime = 0;
            actualTotalTime = 0;
        }
    }

    void Blinking()
    {
        if (Sprite.enabled == true)
            Sprite.enabled = false;
        else
            Sprite.enabled = true;
    }


    // Update is called once per frame
    void Death()
    {
        animator.SetBool("Death", true);
        died = true;
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //for (int i = 0; i < Enemies.Length; i++)
        //{
        //    Enemies[i].GetComponent<Detect>().canDetect = false;
        //}
        MonoBehaviour[] Scripts = gameObject.GetComponents<MonoBehaviour>();
        foreach (var item in Scripts)
        {
            item.enabled = false;
        }
        gameObject.GetComponent<PlayerLife>().enabled = true;
    }
}
