using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class 警報 : MonoBehaviour
{
    public static AudioSource audiosource;
    bool 碰撞=false;
    public GameObject 生成的怪物;
    public GameObject 左生成點, 右生成點;
    public GameObject 爆炸特效;
    public float 警報血量 = 10;
    private float 血量;
    public Image 血條;
    Animator animator;
    public float spawnTime = 2.5f;    // 生成時間間隔
    private float timer;
    int maxEnemies = 5;      // 最大敵人數量
    int enemyCount;         // 已生成敵人數量
    void Start()
    {
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        血量 = 警報血量;
        enemyCount = 0;
    }

    void Update()
    {
        if (enemyCount >= maxEnemies)
        {
            audiosource.Stop();
            Destroy(gameObject);
            return;
        }
        if (碰撞)
        {
            生成();
        }
        if (血量 <= 0)
        {
            Instantiate(爆炸特效, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    void 生成()
    {
        if (timer <= 0f)
        {
            float dropRate = Random.Range(0, 2);
            if (dropRate == 0)
            {
                GameObject enemy = Instantiate(生成的怪物, 左生成點.transform.position, Quaternion.identity);
                enemyCount++;
            }
            if (dropRate == 1)
            {
                GameObject enemy = Instantiate(生成的怪物, 右生成點.transform.position, Quaternion.identity);
                enemyCount++;
            }
            timer = spawnTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
    void 警報完()
    {
        audiosource.Stop();
        animator.Play("無");
        碰撞 = false;
    }
    public void 警報受傷(int damge)
    {
        血量 -= damge;
        血條.fillAmount = 血量 / 警報血量;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            audiosource.Play();
            animator.Play("警報");
            碰撞 = true;
        }
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        碰撞 = true;
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        Invoke("警報完", 0.5f);
    }
}
