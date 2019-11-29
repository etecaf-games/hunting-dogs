using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfTheWorld : MonoBehaviour
{
    public Vector2[] Velocitys;
    public GameObject[] Enemys;
    public bool isActived = false;
    public bool CanUse = true;
    public float CDR = 30f;
    public float AD = 7; //AD= Ability Duration
    public float AT = 0; //AT = Actual Time
    public Dash dash;
    public bool canstop = false;
    public float Speed = 500;
    float Timesaved;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Enemys = GameObject.FindGameObjectsWithTag("Enemy");
        AT += Time.deltaTime;
        if (AT > CDR)
            CanUse = true;
        if (Input.GetButtonDown("Za Warudo") && CanUse)// && dash.isActived)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);

            AT = 0;
            canstop = false;
            isActived = true;
            CanUse = false;
            Debug.Log("Ativado");
            //Register();
            Timesaved = Time.fixedDeltaTime;
            
            GetComponent<Animator>().SetTrigger("ZW");
            this.gameObject.GetComponent<Rigidbody2D>().mass = 10000000000;
        }
        if (isActived)
        {
            if (canstop == false)
                StartCoroutine(SlowEverything());
            if (AT > AD)
                Back();
        }
    }


    #region Parar o tempo
    IEnumerator SlowEverything()
    {
        this.gameObject.GetComponent<MvtPlayer>().enabled = false; //Impossibilitar o Player de se mover
        this.gameObject.GetComponent<SmoothJump>().enabled = false; //Impossibilitar o Player de se mover
        //this.gameObject.GetComponent<Animator>().speed = 1 / Time.timeScale; //Deixar a nimação do player constante
        Debug.Log(AT + " " + Time.timeScale);
        Time.timeScale -= 0.005f;
        Time.fixedDeltaTime = Time.deltaTime * 0.02f; //Fazer o slow nao ficar zoadasso
        yield return new WaitForSeconds(1.65f); //1.65 segundos foi o melhor tempo que eu achei para fazer tal coisa
        //GetComponent<Animator>().SetTrigger("ZW");
        Time.timeScale = 1; //voltar o timeScale ao normal
        Time.fixedDeltaTime = Timesaved; //Voltar o delta time para o inicial
        Enemy(false, "Enemy", 1);
        Bullets(true);
        this.gameObject.GetComponent<SmoothJump>().enabled = true;
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = Time.timeScale;
        canstop = true; //Parar a chamada pela courantine
        this.gameObject.GetComponent<MvtPlayer>().enabled = true;
        this.gameObject.GetComponent<Animator>().speed = 1;
        this.gameObject.GetComponent<Rigidbody2D>().mass = 1;
        StopAllCoroutines(); //parar todos coroutines chamados
    }
    #endregion


    void Back()
    {
        Enemy(true, "Enemy", 1);
        Bullets(false);
        isActived = false;
        AT = 0;
    }

    void Enemy(bool Condition, string tagName, int EnemyScript)
    {
        Enemys = GameObject.FindGameObjectsWithTag(tagName);
        for (int i = 0; i < Enemys.Length; i++)
        {
            MonoBehaviour[] scripts = Enemys[i].gameObject.GetComponents<MonoBehaviour>();
            Enemys[i].GetComponent<Animator>().enabled = Condition;
            foreach (MonoBehaviour script in scripts)
            {
                Debug.Log("Arthur Corno");
                script.enabled = Condition;
            }
            Enemys[i].GetComponent<Life>().enabled = true;
            //Enemys[i].GetComponent<BarradeVidaInimigo>().enabled = true;
        }

    }

    void Bullets(bool Condition)
    {
        GameObject[] Bullet = GameObject.FindGameObjectsWithTag("Bullet");
        for (int i = 0; i < Bullet.Length; i++)
        {
            if (Condition == true)
                Bullet[i].gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            else
                Bullet[i].gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}

