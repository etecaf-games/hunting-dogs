using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effects : MonoBehaviour
{
    public GameObject[] som;
    public float tempo;
    public float TempoTotal = 0;
    bool ativado = false;
    public int numero;

    public void Start()
    {

        tempo = TempoTotal;

    }

    public void Update()
    {

        if (ativado)
        {
            som[numero].SetActive(true);
            tempo -= Time.deltaTime;
        }


        if (tempo <= 0)
        {
            ativado = false;
            tempo = TempoTotal;
            som[numero].SetActive(false);
        }

    }

    public void ArkasonSong()
    {
        numero = 0;
        ativado = true;
    }
    public void ZawarudoSong()
    {
        tempo = 5;
        numero = 1;
        ativado = true;
    }
    public void WaveSong()
    {
        numero = 2;
        ativado = true;
    }
    public void Ataque01Song()
    {
        numero = 3;
        ativado = true;
    }
    public void Ataque02Song()
    {
        numero = 4;
        ativado = true;
    }
    public void Ataque03Song()
    {
        numero = 5;
        ativado = true;
    }
    public void PuloSong()
    {
        numero = 6;
        ativado = true;
    }
    public void MorteSong()
    {
        numero = 7;
        ativado = true;
    }
    public void DashSong()
    {
        numero = 8;
        ativado = true;
    }
}
