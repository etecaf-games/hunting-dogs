using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public int Lifes = 0;
    public int storedLife;
    public EndOfTheWorld ZW;
    [Header("Boss Only")]
    public bool canBeDamaged = true;

    void Start()
    {
        //ZW = GameObject.FindGameObjectWithTag("Player").GetComponent<EndOfTheWorld>();
    }

    void Update()
    {
        if (!ZW.isActived && storedLife > 0)
        {
            Lifes -= storedLife;
            storedLife = 0;
        }
    }
}