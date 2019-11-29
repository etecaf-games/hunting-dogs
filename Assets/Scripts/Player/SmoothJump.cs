using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothJump : MonoBehaviour
{

    public float Fall = 2.75f;
    public float Low = 1.75f;

    Rigidbody2D RbPlayer;

    // Use this for initialization
    void Start()
    {
        RbPlayer = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (RbPlayer.velocity.y < 0)
            RbPlayer.velocity += Vector2.up * Physics2D.gravity.y * (Fall - 1) * Time.deltaTime;

        else if(RbPlayer.velocity.y > 0 &&! Input.GetButton("Jump"))
            RbPlayer.velocity += Vector2.up * Physics2D.gravity.y * (Low - 1) * Time.deltaTime;
	}
}
