using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class 仙族小兵 : MonoBehaviour
{
    public 移動 playerhp;
    [Header("怪物分類")]
    public InKitchenEnemy MonsterType;
    public Enemy_Type enemy_Type;

    [Header("怪物enum行動模式")]
    public EnemyState currentState;

    [Header("1.巡邏位置 2.玩家位置")]
    public Transform[] patrolPoints = new Transform[2];
    public Transform PlayerTransform;
    public Transform 木棒位置;

    [Header("障礙物")]
    //public LayerMask 阻擋物件;
    public LayerMask 監測圖層;

    [Header("組件")]
    public Animator ani;
    public Rigidbody2D rb;
    public BoxCollider2D 腳;

    [Header("物件")]
    public Image 血條;
    public GameObject 火球;
    public GameObject 補包, 血球, 噴血特效;
    public GameObject 掉落點;

    [Header("數值")]
    private float MaxHp = 20;
    public float hp;
    public float distanceToPlayer;
    private float EnemySeeYou = 10f;
    public float patrolSpeed = 1.0f;
    public float patrolThreshold = 0.2f;
    public int damage = 5;
    public float 跳躍力 = 5;

    public float 火球冷卻時間 = 0f;
    public float 對玩家施法距離 = 5f;
    private float 玩家逃離追逐距離 = 20f;

    private int currentPatrolPoint = 0;

    private bool isDie = false;
    public bool isGround = false;
    private bool isDrop = false;
    public bool 玩家躲入草叢怪物看不見 = false;
    private void Awake()
    { 
        playerhp = GameObject.Find("主角").GetComponent<移動>();
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        hp = MaxHp;
        ani.SetTrigger("Show");
        currentState = EnemyState.Patrol;
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemy_Type = Enemy_Type.Mozu;
    }
    bool isGroundedRight = false;
    bool isGroundedLift = false;
    private void FixedUpdate()
    {
        if (MonsterType == InKitchenEnemy.Up01 || MonsterType == InKitchenEnemy.Up02 || MonsterType == InKitchenEnemy.down01 || MonsterType == InKitchenEnemy.down02 || MonsterType == InKitchenEnemy.SB)
            return;
        CheckGrounded();
        isGroundedRight = Physics2D.Raycast(transform.position, Vector2.right, 2.5f, 監測圖層);
        isGroundedLift = Physics2D.Raycast(transform.position, Vector2.left, 2.5f, 監測圖層);

        if (isGround)
        {
            ani.SetBool("Jump", false);
        }
        else
        {
            ani.SetBool("Jump", true);
        }
        if (isGround && isGroundedRight || isGroundedLift)
        {
            Jump();
        }
    }
    void Update()
    {

        if (hp <= 0)
        {
            怪物死亡時();
        }
        if (isDie)
        {
            currentState = EnemyState.Die;
        }

        distanceToPlayer = Vector2.Distance(transform.position, PlayerTransform.position);

        switch (currentState)
        {
            case EnemyState.Idle:
                Idle();
                // 待機狀態，不做任何事情
                break;
            case EnemyState.Chase:
                // 追敵狀態，實現追敵行為
                Chase();
                break;
            case EnemyState.Patrol:
                // 巡邏狀態，實現巡邏行為
                Patrol();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Die:
                Die();
                break;
            default:
                break;
        }
    }
    bool hasRefreshedTime = false; // 是否重新刷新過時間
    void Idle()
    {
        ani.SetBool("Idle", true);
        ani.SetBool("Run", false);
        if (!hasRefreshedTime)
        {
            float A = Random.Range(0, 2);
            hasRefreshedTime = true;
            Invoke("SwitchToChaseState", A);
        }
    }
    void SwitchToChaseState()
    {
        hasRefreshedTime = false;
        currentState = EnemyState.Patrol;
    }
    private void Patrol()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Run", true);
        //ani.Play("Run");
        if (玩家躲入草叢怪物看不見)
        {
            // 計算敵人與目標巡邏點之間的距離
            float distance = Vector2.Distance(transform.position, patrolPoints[currentPatrolPoint].position);
            // 如果與目標巡邏點的距離小於閾值，則切換到下一個巡邏點
            if (distance < patrolThreshold)
            {
                currentPatrolPoint++;
                if (currentPatrolPoint >= patrolPoints.Length)
                {
                    currentPatrolPoint = 0;
                    currentState = EnemyState.Idle;
                }
            }
            if (patrolPoints[currentPatrolPoint].position.x < transform.position.x)
            {
                this.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                this.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            // 向目標巡邏點移動
            Vector3 direction = (patrolPoints[currentPatrolPoint].position - transform.position).normalized;
            transform.position += direction * patrolSpeed * Time.deltaTime;
        }
        else
        {
            float distance = Vector2.Distance(transform.position, patrolPoints[currentPatrolPoint].position);
            if (distance < patrolThreshold)
            {
                currentPatrolPoint++;
                if (currentPatrolPoint >= patrolPoints.Length)
                {
                    currentPatrolPoint = 0;
                    currentState = EnemyState.Idle;
                }
            }
            if (distanceToPlayer < EnemySeeYou)
            {
                currentState = EnemyState.Chase;
            }
            if (patrolPoints[currentPatrolPoint].position.x < transform.position.x)
            {
                this.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                this.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            Vector3 direction = (patrolPoints[currentPatrolPoint].position - transform.position).normalized;
            transform.position += direction * patrolSpeed * Time.deltaTime;
        }
    }
    void Chase()
    {
        if (玩家躲入草叢怪物看不見)
        {
            currentState = EnemyState.Idle;
        }
        ani.SetBool("Idle", false);
        ani.SetBool("Run", true);
        //ani.Play("Run");
        float distance = Vector2.Distance(transform.position, PlayerTransform.position);
        火球冷卻時間 += Time.deltaTime;

        if (distance > 玩家逃離追逐距離)
        {
            currentState = EnemyState.Idle;
        }
        if (distance > 對玩家施法距離 && 火球冷卻時間 >= 5)
        {
            ani.SetTrigger("蓄力砍");
            火球冷卻時間 = 0;
        }
        if (distance < 對玩家施法距離)
        {
            currentState = EnemyState.Attack;
        }
        Vector3 direction = (PlayerTransform.position - transform.position).normalized;
        transform.position += direction * patrolSpeed * Time.deltaTime;

        //面相玩家
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
        砍擊過 = true;
        if (砍擊過)
        { 
            ani.SetBool("敲擊", true);
            rb.velocity = Vector2.zero;
            Invoke("攻擊完1秒後", 1f);
        }
    }
    void 攻擊完1秒後()
    {
        砍擊過 = false;
        ani.SetBool("敲擊", false);
        currentState = EnemyState.Idle;
    }





    public void Die()
    {
        rb.velocity = Vector2.zero;
    }
    public void 怪物死亡時()
    {
        isDie = true;
        掉寶();
        damage = 0;
        ani.Play("死亡");
        Invoke("死亡", 1);
    }
    void 死亡()
    {
        Destroy(gameObject);
    }
    public void TakeDamage(int damge)
    {
        hp -= damge;
        血條.fillAmount = hp / MaxHp;
        Instantiate(噴血特效, transform.position, Quaternion.identity);
    }
    void CheckGrounded()
    {
        isGround = 腳.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
                   腳.IsTouchingLayers(LayerMask.GetMask("穿越平台"));
    }
    void Jump()
    {
        rb.AddForce(new Vector2(0f, 跳躍力), ForceMode2D.Impulse);
    }
    void 發射火球()
    {
        Instantiate(火球, 木棒位置.position, Quaternion.identity);
    }
    void 掉寶()
    {
        if (isDrop) return; // 如果已經掉落過寶物，直接返回
        float dropRate = Random.Range(0, 3);
        if (dropRate == 0)
        {
            Instantiate(補包, 掉落點.transform.position, Quaternion.identity);
        }
        else if (dropRate == 1)
        {
            Instantiate(血球, 掉落點.transform.position, Quaternion.identity);
        }
        isDrop = true; // 將標記設為 true
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
}
