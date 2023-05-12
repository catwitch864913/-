using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI追擊 : MonoBehaviour
{
    int state = 0;  //1: 追逐  2: 攻擊


    public Transform PlayerTransform;
    float distance_ = 0;
    Rigidbody2D rg;
    private float Speed = 5;
    private float 追擊 = 15;
    private float 功擊 = 2.3f;
    Animator animator;

    public bool 玩家躲入草叢怪物看不見 = false;
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (state == 1)
        {
            追();
        }
        else if (state == 2)
        {
            普攻();
        }
        else
        {
            掛機();
        }
        if (玩家躲入草叢怪物看不見)
            return;
        distance_ = Vector3.Distance(transform.position, PlayerTransform.transform.position);
        if (distance_ < 追擊 && distance_> 功擊)
        {
            state = 1;
        }
        else if(distance_<= 功擊)
        {
            state = 2;
        }
        else
        {
            state = 0;
        }
    }
    void 追()
    {
        if (PlayerTransform.transform.position.x < transform.position.x)
        {
            rg.velocity = new Vector2(-1 * Speed, 0);
            transform.localScale = new Vector3(1, 1, 1);
            animator.Play("移動");
        }
        else
        {
            rg.velocity = new Vector2(1 * Speed, 0);
            transform.localScale = new Vector3(-1, 1, 1);
            animator.Play("移動");
        }
    }
    void 掛機()
    {
        rg.velocity = new Vector2(0, 0);
        animator.Play("掛機");
    }
    void 普攻()
    {
        rg.velocity = new Vector2(0,0);
        animator.Play("敲擊");
    }
    public void 關閉移動速度()
    {
        rg.velocity = new Vector2(0, 0);
        animator.Play("掛機");
    }
}
