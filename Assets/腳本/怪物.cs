using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 怪物 : MonoBehaviour
{
    public 魔族小兵 魔族怪物;
    public Monster_type 怪物種類;
    public 假房間逃出口 逃出口腳本;
    public int hp;
    public static int damage = 5;
    public 移動 playerhp;
    public GameObject 補包, 血球;
    public GameObject 掉落點;
    //AudioSource audiosource;
    Animator animator;
    public GameObject 噴血特效;
    private bool isDrop = false; // 是否已經掉落過寶物

    void Start()
    {
        playerhp = GameObject.Find("主角").GetComponent<移動>();
        animator = GetComponent<Animator>();
        if (怪物種類==Monster_type.假房間怪物)
        {
            逃出口腳本 = GameObject.Find("門 (8)").GetComponent<假房間逃出口>();
        }
    }

    void Update()
    {
        敵人血歸零();
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
    void 敵人血歸零()
    {
        if (hp <= 0)
        {

            掉寶();
            damage = 0;
            animator.Play("死亡");
            Invoke("死亡", 1);
            if (怪物種類 == Monster_type.假房間怪物)
            {
                //假房間逃出口.逃出口腳本.當怪物被殺();

                逃出口腳本.當怪物被殺();
            }
        }
    }
    void 死亡()
    {
        Destroy(gameObject);
    }
    public void TakeDamage(int damge)
    {
        hp -= damge;
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
}