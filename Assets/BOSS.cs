using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BOSS : Unit
{

    public BossState BossState;
    [Header("�{��")]
    public ���� playerhp;

    [Header("����ϼh")]
    public LayerMask groundLayer;

    [Header("��m")]
    public Transform �M��m;
    public Transform PlayerTransform;

    [Header("UI����")]
    public Slider HpBar;
    public Text BossHpText;

    [Header("����")]
    public GameObject �M�l;
    public GameObject �g�S��;
    public GameObject �C��;

    [Header("�ե�")]
    public BoxCollider2D boss���};

    [Header("�����Ҧ����ɶ�")]
    public float �X��Ĳ�o�ޯ���� = 1f;
    public float ���ѧ���Time, �W�O����Time, ���D���a�ϤMTime;
    [Header("�ƭ�")]
    public float Boss���ʳt�� = 1f;
    public float Boss���D�O = 2f;
    public float �缾�a�i����� = 10f;

    public bool �}��rb;
    bool �ޯ�1;
    bool �ޯ�2;
    bool �ޯ�3;
    bool ���`;

    public static bool �ʵe;

    Coroutine a;
    void Start()
    {
        �ʵe = false;
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponentInParent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        boss���} = GetComponent<BoxCollider2D>();
        playerhp = GameObject.Find("�D��").GetComponent<����>();
        �}��rb = false;
        hp = 500;
        BossHpText.text = hp.ToString();
        a = StartCoroutine(Attack_loop());
    }

    void Update()
    {
        CheckGrounded();
        HpBar.value = Mathf.Lerp(HpBar.value, hp, 0.01f);
        BossHpText.text = hp.ToString();
        if (�}��rb)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            //�}��rb = false;
            print(rb.bodyType);
            if (�ޯ�1)
            {
                ani.SetTrigger("���ѧ���");
                �ޯ�1 = false;
                �}��rb = false;
            }
            if (�ޯ�2)
            {
                ani.SetTrigger("�W�O����");
                �ޯ�2 = false;
                �}��rb = false;
            }
            if (�ޯ�3)
            {
                ani.SetTrigger("���D���a�ϤM");
                �ޯ�3 = false;
                �}��rb = false;
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

        // �ˬd�O�_�ݭn���D
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
        Vector2 origin = transform.position + new Vector3(0, 0.5f, 0); // �ĤH�Y������m
        Vector2 direction = Vector2.up; // �V�W�o�g�g�u
        float distance = 10f; // �g�u������
        int layerMask = LayerMask.GetMask("Player"); // �u�˴����a�h�Ū�����

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, layerMask);
        if (hit.collider != null)
        {
            // �p�G�g�u�I��F���a�A�h�ݭn���D
            return true;
        }
        else
        {
            // �_�h���ݭn���D
            return false;
        }
    }
    void Jump()
    {
        rb.AddForce(new Vector2(0f, Boss���D�O), ForceMode2D.Impulse);
    }

    bool isGround = false;
    float �N�o�ɶ�;
    void Idle()
    {
        float waitTime = Random.Range(1, 2);
        �N�o�ɶ� += Time.deltaTime;
        ani.SetBool("Idle", true);
        ani.SetBool("Run", false);
        if (isGround && �N�o�ɶ� > waitTime)
        {
            �N�o�ɶ� = 0;
            BossState = BossState.Chase;
        }
    }

    void Chase()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Run", true);
        float waitTime = Random.Range(1, 2);
        �N�o�ɶ� += Time.deltaTime;
        float distance = Vector2.Distance(transform.position, PlayerTransform.position);

        if (distance < �缾�a�i����� && BossState == BossState.Chase && �N�o�ɶ� > waitTime)
        {
            �N�o�ɶ� = 0;
            BossState = BossState.Attack;
            rb.velocity = Vector2.zero;
        }
        else
        {
            Vector3 direction = (PlayerTransform.position - transform.position).normalized;
            transform.position += direction * Boss���ʳt�� * Time.deltaTime;
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
    bool �����L = false;
    void Attack()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Run", true);
        float distance = Vector2.Distance(transform.position, PlayerTransform.position);
        �����L = true;
        if (�����L)
        {
            ani.SetBool("����", true);
            rb.velocity = Vector2.zero;
            Invoke("��������", 1f);
            �����L = false;
        }
        Vector3 direction = (PlayerTransform.position - transform.position).normalized;
        transform.position += direction * Boss���ʳt�� * Time.deltaTime;
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
    void ��������()
    {
        ani.SetBool("����", false);
        BossState = BossState.Idle;
    }
    void CheckGrounded()                                                                    //�˴��O�_���a��
    {
        isGround = boss���}.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
                   boss���}.IsTouchingLayers(LayerMask.GetMask("��V���x"));
    }

    IEnumerator Attack_loop()
    {

        while (true)
        {
            ���ѧ���Time += Time.deltaTime;
            �W�O����Time += Time.deltaTime;
            ���D���a�ϤMTime += Time.deltaTime;
            if (���ѧ���Time > (13 / �X��Ĳ�o�ޯ����))
            {
                �}��rb = true;
                this.�ޯ�1 = true;
                BossState = BossState.Skill;
                ���ѧ���Time = 0;
            }
            if (�W�O����Time > (5 / �X��Ĳ�o�ޯ����))
            {
                �}��rb = true;
                this.�ޯ�2 = true;
                BossState = BossState.Skill;
                �W�O����Time = 0;
            }

            if (���D���a�ϤMTime > (7 / �X��Ĳ�o�ޯ����))
            {
                �}��rb = true;
                this.�ޯ�3 = true;
                BossState = BossState.Skill;
                ���D���a�ϤMTime = 0;
            }

            yield return null;
        }
    }


    public void TakeDamage(int damge)
    {
        hp -= damge;
        if (hp <= 0)
        {
            ani.Play("�]�ڭ��� ���`");
            StopCoroutine(a);
            BossState = BossState.Die;
            Invoke("�k�]�ʵe", 1f);
        }
        Instantiate(�Q��S��, transform.position, Quaternion.identity);
    }
    void �k�]�ʵe()
    {
        �ʵe = true;
    }
    void Die()
    {
        rb.velocity = Vector2.zero;
        //Ĳ�otimeline;
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
        if (other.gameObject.CompareTag("���`"))
        {
            Destroy(gameObject);
        }
    }
    void �j��enum()
    {
        BossState = BossState.Idle;
    }
    void ��_���O()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    void �o�g�M�l()
    {
        Instantiate(�M�l, �M��m.position, Quaternion.identity);
    }
    void �o�g�C��()
    {
        Instantiate(�C��, �M��m.position, Quaternion.identity);
    }
    void �����a�O�S��()
    {
        if (isGround)
        {
            Instantiate(�g�S��, �M��m.position, Quaternion.identity);
        }
    }
}
