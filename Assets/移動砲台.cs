using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class 移動砲台 : MonoBehaviour
{

    public float 砲台移動速度 = 1f;
    public float 砲台血量 = 100f;
    public float 砲台當前血量 = 0;
    public Transform target;
    public Transform bettery;
    public Transform 開火位置;

    public Image 砲台血量顯示;

    public GameObject 子彈預置物;
    public GameObject 爆炸特效;

    public float 開火時間 = 0f;

    Coroutine b;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.GetChild(3);
        砲台當前血量 = 砲台血量;
        Coroutine a = StartCoroutine(Fire());
        b = StartCoroutine(看時間移動());
    }
    void Update()
    {
        if (target != null)
        {
            Vector3 dir = (target.transform.position - bettery.position).normalized;
            bettery.transform.rotation = Quaternion.FromToRotation(Vector3.right, dir);
        }
        if (砲台當前血量 <= 0)
        {
            Instantiate(爆炸特效, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    IEnumerator Fire()
    {
        while (true)
        {
            GameObject go = Instantiate(子彈預置物, 開火位置.position, bettery.rotation);
            Elent bullet = go.GetComponent<Elent>();
            bullet.direction = (target.transform.position - 開火位置.position).normalized;
            yield return new WaitForSeconds(3f);
        }
    }
    public float 移動a;
    public float 移動b;
    public bool 移動完畢 = true;
    IEnumerator 看時間移動()
    {
        while (true)
        {
            移動a += Time.deltaTime;
            移動b += Time.deltaTime;
            if (移動a > 5 && 移動完畢)
            {
                移動完畢 = false;
                Coroutine c = StartCoroutine(MoveRight());
                移動a = 0f;
            }
            if (移動b > 11 && 移動完畢)
            {
                移動完畢 = false;
                Coroutine c = StartCoroutine(MoveLeft());
                移動b = 0f;
            }
            yield return null;
        }
    }
    IEnumerator MoveTo(Vector3 pos)
    {
        while (true)
        {
            Vector3 dir = (this.transform.position - pos).normalized;
            if (dir.magnitude < 0.1f)
            {
                移動完畢 = true;
                break;
            }
            this.transform.position = Vector3.MoveTowards(this.transform.position, pos, this.砲台移動速度 * Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator MoveRight()
    {
        float a = Random.Range(17, 23);
        yield return MoveTo(new Vector3(a, 4, 0));
    }
    IEnumerator MoveLeft()
    {
        float a = Random.Range(8, 16);
        yield return MoveTo(new Vector3(a, 4, 0));
    }
    public void 砲台受傷(int damge)
    {
        砲台當前血量 -= damge;
        砲台血量顯示.fillAmount = 砲台當前血量 / 砲台血量;
    }

    #region 不需要的
    //IEnumerator 開始射擊()
    //{
    //    開火時間 += Time.deltaTime;

    //}
    /*void Fire()
    {
        GameObject go = Instantiate(子彈預置物, 開火位置.position, bettery.rotation);
        Elent bullet = go.GetComponent<Elent>();
        bullet.direction = (target.transform.position - 開火位置.position).normalized;
    }*/
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "武器")
    //    {
    //        砲台血量 -= 5;
    //        Destroy(gameObject);
    //    }
    //}
    #endregion
}
