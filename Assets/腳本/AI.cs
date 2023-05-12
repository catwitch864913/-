using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public static AI chameleonController;
    private void Awake()
    {
        chameleonController = this;
    }

    Animator animator;
    Rigidbody2D rg;
    public float MoveSpeed = 0;
    float changeTime = 0;
    float time = 0;

    //随机时间
    [Header("走路時間")]
    public float minTime = 0;
    public float maxTime = 0;

    int direction = 1;
    public static float 朝向;
    float distance_ = 0;
    bool isChange = false;

    bool canHit = false;
    bool 放技 = false;

    Transform PlayerTransform;
    public float 普攻 = 0;
    public float 追擊 = 0;
    public float 法術;


    float Dwell_gap = 0; //停留时间
    float Dwell_Cumulative = 0; //停留时间累积
    [Header("停留時間")]
    public float DwellMinTime = 0;
    public float DwellMaxTime = 0;


    float moveTime = 0; //行走时间
    //行走的最大时间和最短时间设定
    [Header("追擊時間")]
    public float moveMinTime = 0;
    public float moveMaxTime = 0;

    float moveTimeCumulative = 0;

    bool isDwellRange = false;

    bool CanMove = false;
    bool CanCount = false;
    bool CanTimeCumulative = false;


    [Header("發現玩家")]
    public bool TrackPlayer = false;
    public float TrackSpeed;
    public float Speed;

    public float CurrentPlayer = 0;


    public float hitTime = 0;
    public GameObject 設線;

    GameObject gm;

    Vector2 beg;//射线起点
    private float radialLength = 0.5f;//射线的长度
    private float face;//朝向

    void Start()
    {
        beg = 設線.transform.position;
        face = 1;
        animator = GetComponent<Animator>();
        rg = GetComponent<Rigidbody2D>();
        CanMove = false;
        CanCount = true;
    }


    void Update()
    {
        放技和普攻();
        判斷普攻();
        判斷法術();
        判斷追擊();
        朝向 = direction;
        隱藏();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //得到与玩家的距离
        distance_ = Vector3.Distance(transform.position, PlayerTransform.transform.position);
        time = time + Time.deltaTime;

        if (isChange == false)
        {
            changeTime = Random.Range(minTime, maxTime);
            isChange = true;
        }

        transform.localScale = new Vector3(-direction, 1, 1);

        if (time > changeTime)
        {
            direction = -direction;
            transform.localScale = new Vector3(-direction, 1, 1);
            time = 0;
            isChange = false;
        }
        //随机暂停时间 开始暂停 播放停留动画 人物停止移动 生成暂停时间
        if (CanCount)
        {
            isDwellRange = true;
            CanTimeCumulative = true;
            CanCount = false;
        }

        if (isDwellRange) //设置停留动画 设置暂停时间 
        {
            Dwell_gap = Random.Range(DwellMinTime, DwellMaxTime);
            animator.Play("仙族小兵 掛機");
            CanMove = false;
            isDwellRange = false;
        }

        if (CanTimeCumulative)
        {
            Dwell_Cumulative = Dwell_Cumulative + Time.deltaTime;
        }

        if (Dwell_Cumulative > Dwell_gap) //超过暂停时间 停留结束 结束停留动画 生成行走时间 开始行走动画
        {
            animator.Play("仙族小兵 移動");
            moveTime = Random.Range(moveMinTime, moveMaxTime);
            CanMove = true;
            Dwell_Cumulative = 0;
            CanTimeCumulative = false;
        }

        //行走时间结束 
        if (CanMove)
        {

            moveTimeCumulative = moveTimeCumulative + Time.deltaTime;
            if (moveTimeCumulative > moveTime)
            {
                CanMove = false;
                moveTimeCumulative = 0;
                CanCount = true;
            }
        }
    }

    //判断是否在攻击距离之内
    private void 判斷普攻()
    {
        if (distance_ < 普攻)
        {
            canHit = true;
        }
        else
        {
            canHit = false;
        }
    }
    private void 判斷追擊()
    {
        if (distance_ < 追擊)
        {
            TrackPlayer = true;
        }
        else
        {
            TrackPlayer = false;
        }
    }
    private void 判斷法術()
    {
        if (distance_ < 法術)
        {
            放技 = true;
        }
        else
        {
            Invoke("移動", 1);
            放技 = false;
        }
    }
    void 放技和普攻()
    {
        if (canHit)
        {
            //判断玩家在怪物的哪边
            if (PlayerTransform.transform.position.x < transform.position.x)
            {
                direction = 1;
                animator.Play("仙族小兵 敲擊");
                transform.localScale = new Vector3(direction, 1, 1);
                TrackSpeed = 0;
            }
            else
            {
                TrackSpeed = 0;
                direction = -1;
                animator.Play("仙族小兵 敲擊");
                transform.localScale = new Vector3(direction, 1, 1);
            }
        }
        if (放技)
        {
            //判断玩家在怪物的哪边
            if (PlayerTransform.transform.position.x < transform.position.x)
            {
                TrackSpeed = 0;
                direction = 1;
                animator.Play("仙族小兵 蓄力法術");
                transform.localScale = new Vector3(direction, 1, 1);
            }
            else
            {
                TrackSpeed = 0;
                direction = -1;
                animator.Play("仙族小兵 蓄力法術");
                transform.localScale = new Vector3(direction, 1, 1);
            }
        }
    }
    private void FixedUpdate()
    {
        if (放技) return;
        beg = 設線.transform.position;//射线起点，这个要每帖更新
        //没有停留就可以移动
        if (CanMove)
        {
            MoveMent();
        }
        if (isBorder())//是否到边缘
        {
            rg.velocity = new Vector2(MoveSpeed*0, 0);
            Flip();
        }
        else //不到边缘就可以移动
        {
            MoveMent();
        }
    }
    bool isBorder()//判断是否已经抵达边境
    {
        
        // Debug.DrawLine(beg, beg + (new Vector2(face, 0) + down) * radialLength, Color.red);     //也可以采用Debug的方式可视化射线
        if (!Physics2D.Raycast(beg, Vector3.down, radialLength, LayerMask.GetMask("Ground", "穿越平台")))//抵达边境
        {
            return true;
        }
        return false;
    }
    void Flip()//翻转角色方向
    {
        face = (face == 1) ? -1 : 1;
        transform.localScale = new Vector2(face * (-1), 1);//乘-1是因为初始动画朝向是朝着左边的，但是初始坐标却是1，是相反的
    }
    public void 關閉移動速度()
    {
        rg.velocity = new Vector2(0, 0);
        animator.Play("仙族小兵 待機");
    }
    void MoveMent()
    {
        if (放技) return;
        if (TrackPlayer == false)
        {
            rg.velocity = new Vector2(1 * direction * MoveSpeed, 0);
        }
        else
        {
            if (distance_ < 追擊)
            {
                if (PlayerTransform.transform.position.x < transform.position.x)
                {
                    rg.velocity = new Vector2(-1 * TrackSpeed, 0);
                }
                else
                {
                    rg.velocity = new Vector2(1 * TrackSpeed, 0);
                }
            }

        }

    }
    public void 移動()
    {
        TrackSpeed = Speed;
        animator.Play("仙族小兵 移動");
    }

    void 隱藏()
    {
        if (草叢.被隱藏 == true)
        {
            TrackPlayer = false;
            
        }
        if (草叢.被隱藏 == false)
        {
            TrackPlayer = true;
        }
    }
}