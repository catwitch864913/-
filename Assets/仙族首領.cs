using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 仙族首領 : Unit
{
    [Header("Boss模式")]
    public BossState2 BossState2;
    public BossHurtOrNoHurt bossHurtOrNoHurt;
    [Header("程式")]
    public 移動 playerhp;

    [Header("抓取圖層")]
    public LayerMask groundLayer;

    [Header("位置")]
    public Transform 法杖位置;
    public Transform 仙族小兵出沒位置A, 仙族小兵出沒位置B;
    public Transform PlayerTransform;

    [Header("UI物件")]
    public Slider HpBar;
    public Text BossHpText;

    [Header("物件")]
    public GameObject 火球;
    public GameObject 雲朵;
    public GameObject 仙族小兵;

    [Header("組件")]
    public BoxCollider2D boss的腳;

    [Header("攻擊模式的時間")]
    public float 幾秒觸發技能攻擊 = 1f;
    public float 連續火球Time, 召喚仙球雨Time, 召喚小兵Time;
    [Header("數值")]
    public float Boss移動速度 = 1f;
    public float Boss跳躍力 = 2f;
    public float 對玩家進行攻擊 = 10f;

    public bool 開關rb;
    bool 技能1;
    bool 技能2;
    bool 技能3;
    bool 死亡;

    bool 被打到了 = false;

    Coroutine a;
    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponentInParent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        boss的腳 = GetComponent<BoxCollider2D>();
        playerhp = GameObject.Find("主角").GetComponent<移動>();
        開關rb = false;
        if (bossHurtOrNoHurt == BossHurtOrNoHurt.未受過傷)
        {
            hp = 1000;
        }
        else if (bossHurtOrNoHurt == BossHurtOrNoHurt.受過傷)
        {
            hp = 500;
        }
        //BossHpText.text = hp.ToString();
        a = StartCoroutine(Attack_loop());
    }

    void Update()
    {
        CheckGrounded();
        //HpBar.value = Mathf.Lerp(HpBar.value, hp, 0.01f);
        //BossHpText.text = hp.ToString();
        if (開關rb)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            if (技能1)
            {
                ani.SetTrigger("連續火球");
                技能1 = false;
                //開關rb = false;
            }
            if (技能2)
            {
                ani.SetTrigger("召喚仙球雨");
                技能2 = false;
                //開關rb = false;
            }
            if (技能3)
            {
                ani.SetTrigger("召喚小兵");
                技能3 = false;
                //開關rb = false;
            }
        }
        switch (BossState2)
        {
            case BossState2.Idle:
                Idle();
                break;
            case BossState2.Chase:
                Chase();
                break;
            case BossState2.Attack:
                Attack();
                break;
            case BossState2.Skill:
                Skill();
                break;
            case BossState2.Call:
                Call();
                break;
            case BossState2.Die:
                Die();
                break;
            default:
                break;
        }
    }
    //public bool isGrounded;
    private void FixedUpdate()
    {
        if (isGround)
        {
            ani.SetBool("Jump", false);
        }
        else
        {
            ani.SetBool("Jump", true);
        }
        // 檢查是否需要跳躍
        if (isGround && shouldJump())
        {
            Jump();
        }
    }

    bool shouldJump()
    {
        Vector2 origin = transform.position + new Vector3(0, 0.5f, 0); // 敵人頭部的位置
        Vector2 direction = Vector2.up; // 向上發射射線
        float distance = 10f; // 射線的長度
        int layerMask = LayerMask.GetMask("Player"); // 只檢測玩家層級的物體

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, layerMask);
        if (hit.collider != null)
        {
            // 如果射線碰到了玩家，則需要跳躍
            return true;
        }
        else
        {
            // 否則不需要跳躍
            return false;
        }
    }
    void Jump()
    {
        rb.AddForce(new Vector2(0f, Boss跳躍力), ForceMode2D.Impulse);
    }


    bool isGround = false;
    float 冷卻時間;
    void Idle()
    {
        float waitTime = Random.Range(1, 2);
        冷卻時間 += Time.deltaTime;
        ani.SetBool("Idle", true);
        ani.SetBool("Run", false);
        if (isGround && 冷卻時間 > waitTime)
        {
            冷卻時間 = 0;
            BossState2 = BossState2.Chase;
        }
    }

    void Chase()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Run", true);
        //float waitTime = Random.Range(1, 2);
        //冷卻時間 += Time.deltaTime;
        float distance = Vector2.Distance(transform.position, PlayerTransform.position);

        //if (distance < 對玩家進行攻擊 && BossState2 == BossState2.Chase && 冷卻時間 > waitTime)
        if (distance < 對玩家進行攻擊 && BossState2 == BossState2.Chase)
        {
            //冷卻時間 = 0;
            BossState2 = BossState2.Attack;
            rb.velocity = Vector2.zero;
        }
        else
        {
            Vector3 direction = (PlayerTransform.position - transform.position).normalized;
            transform.position += direction * Boss移動速度 * Time.deltaTime;
        }

        if (PlayerTransform.position.x < transform.position.x)
        {
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            this.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    bool 砍擊過 = false;
    void Attack()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Run", true);
        float distance = Vector2.Distance(transform.position, PlayerTransform.position);
        砍擊過 = true;
        if (砍擊過)
        {
            ani.SetTrigger("敲擊");
            rb.velocity = Vector2.zero;
            Invoke("攻擊完後", 1f);
            
        }
        Vector3 direction = (PlayerTransform.position - transform.position).normalized;
        transform.position += direction * Boss移動速度 * Time.deltaTime;
        if (PlayerTransform.position.x < transform.position.x)
        {
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            this.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    void Skill()
    {
        rb.velocity = Vector2.zero;
    }
    void Call()
    {
        if (被打到了)
        {
            ani.SetTrigger("被打斷");
            BossState2 = BossState2.Idle;
            rb.bodyType = RigidbodyType2D.Dynamic;
            開關rb = false;
            Invoke("關閉被打到了", 1f);
            print("aaaaa");
        }
    }
    void 關閉被打到了()
    {
        被打到了 = false;
    }
    void 攻擊完後()
    {
        //ani.SetBool("砍擊", false);
        砍擊過 = false;
        BossState2 = BossState2.Idle;
    }
    void CheckGrounded()                                                                    //檢測是否為地面
    {
        isGround = boss的腳.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
                   boss的腳.IsTouchingLayers(LayerMask.GetMask("穿越平台"));
    }
    #region 技能迴圈
    IEnumerator Attack_loop()
    {
        while (true)
        {
            連續火球Time += Time.deltaTime;
            召喚仙球雨Time += Time.deltaTime;
            召喚小兵Time += Time.deltaTime;
            if (連續火球Time > (10.1f / 幾秒觸發技能攻擊) && 開關rb == false)
            {
                開關rb = true;
                this.技能1 = true;
                BossState2 = BossState2.Skill;
                連續火球Time = 0;
            }
            if (召喚仙球雨Time > (25 / 幾秒觸發技能攻擊) && 開關rb == false)
            {
                開關rb = true;
                this.技能2 = true;
                BossState2 = BossState2.Skill;
                召喚仙球雨Time = 0;
            }
            if (召喚小兵Time > (50 / 幾秒觸發技能攻擊) && 開關rb == false)
            {
                開關rb = true;
                this.技能3 = true;
                BossState2 = BossState2.Call;
                召喚小兵Time = 0;
            }
            yield return null;
        }
    }
    #endregion

    public void TakeDamage(int damge)
    {
        hp -= damge;
        if (hp <= 0)
        {
            ani.Play("仙族首領 死亡");
            StopCoroutine(a);
            BossState2 = BossState2.Die;
            //Invoke("逃跑動畫", 1f);
        }
        if (BossState2 == BossState2.Call)
        {
            被打到了 = true;
        }
        Instantiate(噴血特效, transform.position, Quaternion.identity);
    }
    void Die()
    {
        rb.velocity = Vector2.zero;
        //觸發timeline;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerhp != null)
            {
                playerhp.DamegePlayer(damage);
            }
        }
        if (other.gameObject.CompareTag("死亡"))
        {
            Destroy(gameObject);
        }
    }
    void 強轉enum()
    {
        BossState2 = BossState2.Idle;
    }
    void 恢復重力()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        開關rb = false;
    }
    void 發射火球()
    {
        Instantiate(火球, 法杖位置.position, Quaternion.identity);
    }
    void 召喚仙球雨()
    {
        Instantiate(雲朵, 法杖位置.position, Quaternion.identity);
    }
    void 召喚小兵()
    {
        int a = Random.Range(0, 3);
        int b = Random.Range(1, 3);
        print(a);
        print(b);
        for (int i = 0; i < a; i++)
        {
            print("未進入for");
            if (b == 1)
            {
                print("進入衛生成");
                Instantiate(仙族小兵, 仙族小兵出沒位置A.position, Quaternion.identity);
            }
            if (b == 2)
            {
                print("進入沒生成");
                Instantiate(仙族小兵, 仙族小兵出沒位置B.position, Quaternion.identity);
            }

        }
    }
}
