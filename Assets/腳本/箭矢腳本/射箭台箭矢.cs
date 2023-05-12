using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 射箭台箭矢 : MonoBehaviour
{
    public 射箭台種類 射箭台類別;
    int damge = 5;
    float speed = 15f;//速度
    float lifeTime = 0.5f;//幾秒消失
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        if (射箭台類別 == 射箭台種類.往左射)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        if(射箭台類別 == 射箭台種類.往右射)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            移動.當前血量 -= damge;
            Destroy(gameObject);
            音效.箭命中人();
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
