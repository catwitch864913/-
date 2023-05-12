using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Header("數值")]
    public float speed =1f;
    public float lifeTime =5f;
    public int damage= 30;

    [Header("玩家位置")]
    public Transform target;

    [Header("火球爆炸特效")]
    public GameObject Explosion;

    private void Start()
    {
        Destroy(this.gameObject, lifeTime);
        target = GameObject.Find("主角").transform.GetChild(3);
    }
    private void Update()
    {
        if (target != null)
        {
            Vector3 dir = (target.position - this.transform.position).normalized;
            //Vector3 dir = target.position - transform.position;
            if (dir.magnitude < 0.1f)
            {
                target.GetComponent<移動>().DamegePlayer(this.damage);
                Explode();
            }

            transform.rotation = Quaternion.FromToRotation(Vector3.left, dir);
            transform.position += dir * speed * Time.deltaTime;
        }
        //if (Screen.safeArea.Contains(Camera.main.WorldToScreenPoint(this.transform.position)) == false)
        //{
        //    Destroy(gameObject);
        //}
    }
    public void Explode()
    {
        Destroy(gameObject);
        Instantiate(Explosion, this.transform.position, Quaternion.identity, null);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<移動>().DamegePlayer(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
