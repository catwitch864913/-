using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 仙球雨的火球 : 攻擊預置物Unit
{

    Vector3 抓取玩家與我的距離;
    public Vector3 dir;
    private void Start()
    {
        target = GameObject.Find("主角").transform.GetChild(3);
        抓取玩家與我的距離 = target.position - transform.position;
        Destroy(this.gameObject, lifeTime);
        Vector3 relative = transform.InverseTransformPoint(target.position);
        float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
    private void Update()
    {
        transform.position += 抓取玩家與我的距離 * speed * Time.deltaTime;
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
