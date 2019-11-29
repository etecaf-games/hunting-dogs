using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoEfeito : MonoBehaviour
{
    public GameObject som;
    public float tempo;
    public float TempoTotal = 0;
    bool ativado = false;

    public void Start()
    {

        tempo = TempoTotal;

    }

    public void Update()
    {

        if (ativado)
        {
            som.SetActive(true);
            tempo -= Time.deltaTime;
        }


        if (tempo <= 0)
        {
            ativado = false;
            tempo = TempoTotal;
            som.SetActive(false);
        }

    }

    public void MorteSong()
    {
        ativado = true;
    }
}
