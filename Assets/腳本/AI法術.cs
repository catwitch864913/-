using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI法術 : MonoBehaviour
{
    public GameObject 法術;
    public Transform 施放位置;
    public float 距離;
    Animator animator;
    Transform PlayerTransform;
    float distance_ = 0;
    bool 施法 = false;
    public static float direction = 1;

    public bool 玩家躲入草叢怪物看不見 = false;

    void Start()
    { 
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        distance_ = Vector3.Distance(transform.position, PlayerTransform.transform.position);
        if (玩家躲入草叢怪物看不見)
            return;
        法();
        判斷();
    }

    void 法()
    {
        if (施法)
        {
            if (PlayerTransform.transform.position.x < transform.position.x)
            {
                direction = 1;
                animator.Play("蓄力法術");
                transform.localScale = new Vector3(direction, 1, 1);
            }
            else
            {
                direction = -1;
                animator.Play("蓄力法術");
                transform.localScale = new Vector3(direction, 1, 1);
            }
        }
        else
        {
            animator.Play("掛機");
        }
    }

    void 判斷()
    {
        if (distance_ < 距離)
        {
            施法 = true;
        }
        else
        {
            施法 = false;
        }
    }

    void addFire()
    {
        GameObject obj = Instantiate(法術, 施放位置.position, Quaternion.identity, null);
    }
}