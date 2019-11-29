using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{

     public void JogarJogo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SairdoJogo()
     {
        Debug.Log("Saia saia do jogo agora!");
        Application.Quit();
     }
     public void VoltarproMenu()
     {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
     }
     public void PularVideo()
     {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
     }
}
