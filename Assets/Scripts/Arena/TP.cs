using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP : MonoBehaviour
{
    public Transform Entrada;
    float contador = 1f;
    bool pode = false;
    GameObject player;
    public GameObject primeiro;
    public GameObject segundo;
    GameObject Outro;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pode)
        {
            contador -= 0.2f;
        }
        if (contador <= 0)
        {
            player.gameObject.transform.position = Entrada.position;
            player = null;
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            pode = true;
            primeiro.SetActive(false);
            segundo.SetActive(true);
            player = other.gameObject;
        }
    }


}
