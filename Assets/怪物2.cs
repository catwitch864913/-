using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 怪物2 : Unit
{
    [Header("怪物分類")]
    public InKitchenEnemy MonsterType;

    public 移動 playerhp;
    //public 魔族小兵 魔;
    public 生成敵人 creaftMonster;

    public float 調寶率;

    Collider2D 膠囊碰撞體;
    public GameObject 調寶位置;
    public GameObject Iteam, Iteam2;

    //public bool isDie;
    //AudioSource audiosource;

    private void Awake()
    {
        if (MonsterType == InKitchenEnemy.Up02)
        {
            creaftMonster = GameObject.Find("出怪點 (1)").GetComponent<生成敵人>();
        }
        else if (MonsterType == InKitchenEnemy.Up01)
        {
            creaftMonster = GameObject.Find("出怪點 (3)").GetComponent<生成敵人>();
        }
        else if(MonsterType == InKitchenEnemy.down02)
        {
            creaftMonster = GameObject.Find("出怪點 (2)").GetComponent<生成敵人>();
        }
        else if(MonsterType == InKitchenEnemy.down01)
        {
            creaftMonster = GameObject.Find("出怪點").GetComponent<生成敵人>();
        }
        膠囊碰撞體 = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        playerhp = GameObject.Find("主角").GetComponent<移動>();
        //魔 = GetComponent<魔族小兵>();

    }
    void Start()
    {
        //關閉rb的互動
        //rb.isKinematic = false;
        //關閉碰撞器 讓玩家箭矢無法在顯現時射中
        膠囊碰撞體.enabled = false;
        ani.SetTrigger("Show");
    }

    void Update()
    {

    }
    void 敵人血歸零()
    {
        調寶率 = Random.Range(0, 4);
        Debug.Log(調寶率);
        ani.Play("魔族小兵 死亡");
        //GameObject.Find("仙族小兵").GetComponent<AI>().enabled = false;
        damage = 0;
        死亡();
        //Invoke("死亡",1);
    }
    void 調寶()
    {
        if (調寶率 == 3)
        {
            Instantiate(Iteam, 調寶位置.transform.position, Quaternion.identity);
        }
        else if (調寶率 == 4)
        {
            Instantiate(Iteam2, 調寶位置.transform.position, Quaternion.identity);
        }
        else
        {
            return;
        }

    }

    void 當完全顯現後()
    {
        //開啟rb碰撞
        //rb.isKinematic = true;
        膠囊碰撞體.enabled = true;

    }
    void 死亡()
    {
        Destroy(gameObject, 1f);
    }
    public void TakeDamage(int damge)
    {
        hp -= damge;
        if (hp <= 0)
        {
            //魔族小兵.魔.Die();
            //魔.Die();
            敵人血歸零();
        }
        Instantiate(噴血特效, transform.position, Quaternion.identity);

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
    public void 快速刪除()
    {
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        //生成敵人.creaftMoster.當怪物被殺掉時();
        if (MonsterType == InKitchenEnemy.SB)
            return;
        creaftMonster.當怪物被殺掉時();
    }
}
