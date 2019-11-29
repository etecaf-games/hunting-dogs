using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillWave : MonoBehaviour
{
    public GameObject Shoot;
    public Transform local;
    public MvtPlayer Player;
    public float velocity = 6000f;
    public float time = 0;
    public float cooldown = 0;
    public Slider timerSlider;

    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }

        if (time <= 0 && Input.GetButtonDown("Wave"))
        {
            time = 5f;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Animator>().SetTrigger("Wave");
            Player.enabled = false;
        }

    }
    void UpdateUI()
    {
        //For purpose of demo
        timerSlider.value = (time / cooldown);
    }

    public void Wave()
    {
        GameObject tirodisparado = (GameObject)Instantiate(Shoot, local.position, Quaternion.identity);
    }

    public void CanMove()
    {
        Player.enabled = true;
    }
}
