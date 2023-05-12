using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 法術 : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int damage;
    Transform PlayerTransform;
    Vector2 V1 = new Vector2(-1*(AI法術.direction), 0);
    void Start()
    {
        Invoke("消失", lifeTime);
    }
    void Update()
    {
            transform.Translate(V1 * speed * Time.deltaTime);
    }
    void 消失()
    {
        Destroy(gameObject);
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
