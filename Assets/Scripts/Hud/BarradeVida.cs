using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarradeVida : MonoBehaviour
{
    public Sprite[] barra;
    public Image barradevidaUI;
    private MvtPlayer player;
    private PlayerLife player2;


    // Start is called before the first frame update
    void Start()
    {
        player2 = GameObject.Find("Player").GetComponent<PlayerLife>();
    }

    // Update is called once per frame
    void Update()
    {
        barradevidaUI.sprite = barra[player2.Life];
    }
}
