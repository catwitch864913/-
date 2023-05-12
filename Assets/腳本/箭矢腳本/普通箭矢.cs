using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 普通箭矢 : MonoBehaviour
{
    public int damge;
    public float speed = 60f;
    public float lifeTime = 0.5f;
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        音效.射出箭();
    }
    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            怪物 mosterScript = collision.GetComponent<怪物>();
            魔族小兵 demonScript = collision.GetComponent<魔族小兵>();
            仙族小兵 FairyScript = collision.GetComponent<仙族小兵>();
            仙族首領 FairyLeader = collision.GetComponent<仙族首領>();
            if (mosterScript != null)
            {
                mosterScript.TakeDamage(damge);
            }
            else if (demonScript != null)
            {
                demonScript.TakeDamage(damge);
            }
            else if (FairyScript!=null)
            {
                FairyScript.TakeDamage(damge);
            }
            else if (FairyLeader != null)
            {
                FairyLeader.TakeDamage(damge);
            }
            Destroy(gameObject);
            音效.箭命中人();
        }
        if (collision.gameObject.tag == "Enemy2")
        {
            collision.GetComponent<怪物2>().TakeDamage(damge);
            Destroy(gameObject);
            音效.箭命中人();
        }
        if (collision.gameObject.tag == "Boss")
        {
            collision.GetComponent<BOSS>().TakeDamage(damge);
            Destroy(gameObject);
            音效.箭命中人();
        }
        if (collision.gameObject.tag == "陷阱")
        {
            警報 警報陷阱= collision.GetComponent<警報>();
            移動砲台 砲台 =collision.GetComponent<移動砲台>();
            if (警報陷阱 != null)
            {
                警報陷阱.警報受傷(damge);
            }
            if (砲台 != null)
            {
                砲台.砲台受傷(damge);
            }
            Destroy(gameObject);
            音效.箭命中地板();
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            音效.箭命中地板();
        }
    }
}