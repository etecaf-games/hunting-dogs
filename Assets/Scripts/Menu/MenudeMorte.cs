using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenudeMorte : MonoBehaviour
{
    public bool Death = false;
    public PlayerLife player;

    public GameObject MenuMorte;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerLife>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Life <= 0)
        {
            Death = true;
        }
        if (Death)
        {
            Morto();
        }
    }

    void Morto()
    {
        MenuMorte.SetActive(true);
        Time.timeScale = 1f;
    }
    public void VoltarproMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void VoltarproJogo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }
}
