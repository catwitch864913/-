using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 地刺 : MonoBehaviour
{
    public int 傷害;
    public 移動 地刺傷害;
    void Start()
    {
        地刺傷害= GameObject.Find("主角").GetComponent<移動>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (地刺傷害 != null)
            {
                地刺傷害.地刺(傷害);
            }
        }
    }
}
