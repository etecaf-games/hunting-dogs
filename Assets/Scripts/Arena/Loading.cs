using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    public float tempo;
    public int TempoTotal = 0;
    public bool Ativador = false;
    public GameObject tela;
    public GameObject musica;
    void Start()
    {
        tempo = TempoTotal;
    }

    // Update is called once per frame
    void Update()
    {

        if (Ativador)
        {
            musica.SetActive(false);
            tela.SetActive(true);
            tempo -= Time.deltaTime;
        }

        if (tempo <= 0)
        {
            musica.SetActive(true);
            tela.SetActive(false);
            tempo = TempoTotal;
            Ativador = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Ativador = true;
        }
    }


    public void BotãodaFase()
    {
        Ativador = true;
    }
}

