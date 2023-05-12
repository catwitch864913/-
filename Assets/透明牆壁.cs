using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 透明牆壁 : MonoBehaviour
{
    public LayerMask 改變的LayerMask;
    public LayerMask 原來的LayerMask;

    public float 延遲還原LayerMask的時間=1f;
    private void Start()
    {
        //原來的LayerMask = GetComponent<LayerMask>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("bbbbb");
        if (collision.CompareTag("Player"))
        {
            print("aaaa");
            // 紀錄原始LayerMask
            原來的LayerMask = gameObject.layer;

            // 設置新的LayerMask
            gameObject.layer = 改變的LayerMask;

            // 延遲一段時間後恢復原始LayerMask
            StartCoroutine(重製LayerMask());
        }
    }
    IEnumerator 重製LayerMask()
    {
        yield return new WaitForSeconds(延遲還原LayerMask的時間);
        gameObject.layer = 原來的LayerMask;
    }
}
