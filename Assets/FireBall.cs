using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Header("�ƭ�")]
    public float speed =1f;
    public float lifeTime =5f;
    public int damage= 30;

    [Header("���a��m")]
    public Transform target;

    [Header("���y�z���S��")]
    public GameObject Explosion;

    private void Start()
    {
        Destroy(this.gameObject, lifeTime);
        target = GameObject.Find("�D��").transform.GetChild(3);
    }
    private void Update()
    {
        if (target != null)
        {
            Vector3 dir = (target.position - this.transform.position).normalized;
            //Vector3 dir = target.position - transform.position;
            if (dir.magnitude < 0.1f)
            {
                target.GetComponent<����>().DamegePlayer(this.damage);
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
            collision.GetComponent<����>().DamegePlayer(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
