using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BOSS : Unit
{

    public BossState BossState;
    [Header("程式")]
    public 移動 playerhp;

    [Header("抓取圖層")]
    public LayerMask groundLayer;

    [Header("位置")]
    public Transform 刀位置;
    public Transform PlayerTransform;

    [Header("UI物件")]
    public Slider HpBar;
    public Text BossHpText;

    [Header("物件")]
    public GameObject 刀子;
    public GameObject 土特效;
    public GameObject 劍氣;

    [Header("組件")]
    public BoxCollider2D boss的腳;

    [Header("攻擊模式的時間")]
    public float 幾秒觸發技能攻擊 = 1f;
    public float 飛天攻擊Time, 蓄力砍擊Time, 跳躍炸地反刀Time;
    [Header("數值")]
    public float Boss移動速度 = 1f;
    public float Boss跳躍力 = 2f;
    public float 對玩家進行攻擊 = 10f;

    public bool 開關rb;
    bool 技能1;
    bool 技能2;
    bool 技能3;
    bool 死亡;

    public static bool 動畫;

    Coroutine a;
    void Start()
    {
        動畫 = false;
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponentInParent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        boss的腳 = GetComponent<BoxCollider2D>();
        playerhp = GameObject.Find("主角").GetComponent<移動>();
        開關rb = false;
        hp = 500;
        BossHpText.text = hp.ToString();
        a = StartCoroutine(Attack_loop());
    }

    void Update()
    {
        CheckGrounded();
        HpBar.value = Mathf.Lerp(HpBar.value, hp, 0.01f);
        BossHpText.text = hp.ToString();
        if (開關rb)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            //開關rb = false;
            print(rb.bodyType);
            if (技能1)
            {
                ani.SetTrigger("飛天攻擊");
                技能1 = false;
                開關rb = false;
            }
            if (技能2)
            {
                ani.SetTrigger("蓄力砍擊");
                技能2 = false;
                開關rb = false;
            }
            if (技能3)
            {
                ani.SetTrigger("跳躍炸地反刀");
                技能3 = false;
                開關rb = false;
            }
        }
        switch (BossState)
        {
            case BossState.Idle:
                Idle();
                break;
            case BossState.Chase:
                Chase();
                break;
            case BossState.Attack:
                Attack();
                break;
            case BossState.Skill:
                Skill();
                break;
            case BossState.Die:
                Die();
                break;
            default:
                break;
        }
    }
    //public bool isGrounded;
    private void FixedUpdate()
    {
        //isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, groundLayer);
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

    /*bool shouldJump()
    {
        if (transform.position.y + 1f < PlayerTransform.position.y)
        {
            print("1");
            return true;
        }
        else
        {
            print("2");
            return false;
        }
    }*/
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
            BossState = BossState.Chase;
        }
    }

    void Chase()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Run", true);
        float waitTime = Random.Range(1, 2);
        冷卻時間 += Time.deltaTime;
        float distance = Vector2.Distance(transform.position, PlayerTransform.position);

        if (distance < 對玩家進行攻擊 && BossState == BossState.Chase && 冷卻時間 > waitTime)
        {
            冷卻時間 = 0;
            BossState = BossState.Attack;
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
            ani.SetBool("砍擊", true);
            rb.velocity = Vector2.zero;
            Invoke("攻擊完後", 1f);
            砍擊過 = false;
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
    void 攻擊完後()
    {
        ani.SetBool("砍擊", false);
        BossState = BossState.Idle;
    }
    void CheckGrounded()                                                                    //檢測是否為地面
    {
        isGround = boss的腳.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
                   boss的腳.IsTouchingLayers(LayerMask.GetMask("穿越平台"));
    }

    IEnumerator Attack_loop()
    {

        while (true)
        {
            飛天攻擊Time += Time.deltaTime;
            蓄力砍擊Time += Time.deltaTime;
            跳躍炸地反刀Time += Time.deltaTime;
            if (飛天攻擊Time > (13 / 幾秒觸發技能攻擊))
            {
                開關rb = true;
                this.技能1 = true;
                BossState = BossState.Skill;
                飛天攻擊Time = 0;
            }
            if (蓄力砍擊Time > (5 / 幾秒觸發技能攻擊))
            {
                開關rb = true;
                this.技能2 = true;
                BossState = BossState.Skill;
                蓄力砍擊Time = 0;
            }

            if (跳躍炸地反刀Time > (7 / 幾秒觸發技能攻擊))
            {
                開關rb = true;
                this.技能3 = true;
                BossState = BossState.Skill;
                跳躍炸地反刀Time = 0;
            }

            yield return null;
        }
    }


    public void TakeDamage(int damge)
    {
        hp -= damge;
        if (hp <= 0)
        {
            ani.Play("魔族首領 死亡");
            StopCoroutine(a);
            BossState = BossState.Die;
            Invoke("逃跑動畫", 1f);
        }
        Instantiate(噴血特效, transform.position, Quaternion.identity);
    }
    void 逃跑動畫()
    {
        動畫 = true;
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
        BossState = BossState.Idle;
    }
    void 恢復重力()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    void 發射刀子()
    {
        Instantiate(刀子, 刀位置.position, Quaternion.identity);
    }
    void 發射劍氣()
    {
        Instantiate(劍氣, 刀位置.position, Quaternion.identity);
    }
    void 打中地板特效()
    {
        if (isGround)
        {
            Instantiate(土特效, 刀位置.position, Quaternion.identity);
        }
    }
}
