using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenudePause : MonoBehaviour
{
    public bool IsPaused = false;
    public MvtPlayer Player;
    public PlayerAttack Player2;

    public GameObject PauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape))
        {
           if (IsPaused)
               Resume();
           else
               Pause();
        }
    }

        void Resume()
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1f;
            IsPaused = false;
        }


        void Pause()
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
            IsPaused = true;
        }
        public void VoltarproMenu()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        public void VoltaraoJogo()
        {
            Resume();
        }
    }

