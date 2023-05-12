using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 魔族怪物 : MonoBehaviour
{
    public 魔族小兵 魔族;
    public 移動 playerhp;

    public Image 血條;

    private float MaxHp = 20;
    public float hp;
    public int damage = 5;
    public GameObject 補包, 血球;
    public GameObject 掉落點;
    Animator animator;

    public GameObject 噴血特效;
    private bool isDrop = false; // 是否已經掉落過寶物

    void Start()
    {
        hp = MaxHp;
        playerhp = GameObject.Find("主角").GetComponent<移動>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (hp <= 0)
        {
            敵人血歸零();
        }
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
        魔族.怪物死亡時();
        掉寶();
        damage = 0;
        animator.Play("死亡");
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
