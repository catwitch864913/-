using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI巡邏 : MonoBehaviour
{
    Animator animator;
    private Rigidbody2D rb;
    public float Speed;
    private float leftPoint, rightPoint;//左右终止点
    private bool faceleft = true;

    public bool 玩家躲入草叢怪物看不見 = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        leftPoint = transform.position.x - 5;
        rightPoint = transform.position.x + 5;

    }
    void Update()
    {
        if (玩家躲入草叢怪物看不見)
            return;
        Movement();
    }
    void Movement()
    {
        if (faceleft)
        {
            rb.velocity = new Vector2(-Speed, rb.velocity.y);
            if (transform.position.x < leftPoint)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceleft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(Speed, rb.velocity.y);
            if (transform.position.x > rightPoint)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceleft = true;
            }
        }
            animator.Play("移動");
    }
}
