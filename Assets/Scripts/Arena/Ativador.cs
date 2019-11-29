using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ativador : MonoBehaviour
{
    public GameObject Fase01theme;
    public GameObject Bosstheme;
    public GameObject BossLife;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Fase01theme.SetActive(false);
            Bosstheme.SetActive(true);
            BossLife.SetActive(true);
            Destroy(gameObject);
        }
    }
}
