using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapa : MonoBehaviour
{

    public GameObject player;
    public GameObject Loading;
    public GameObject mapa;
    public GameObject MusicaF1;
    public GameObject Local;
    public GameObject Cam01;
    public GameObject Cam02;
    float tempo = 5;


    public void botão()
    {
        mapa.SetActive(false);
        Loading.SetActive(true);
        Cam01.SetActive(false);
        Cam02.SetActive(true);
        player.transform.position = Local.transform.position;

        for (int i = 10; i > 0; i--)
        {
            if (i == 0)
            {
                Loading.SetActive(false);
                MusicaF1.SetActive(true);
            }
        }

    }
}
