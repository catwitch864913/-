using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 對魔族草叢 : MonoBehaviour
{
    //public GameObject[] 場景上的怪物們;
    public List<GameObject> 場景上的怪物們 = new List<GameObject>();
    public Renderer[] 子物件們;
    private float lastPressTime = 0f;
    private float switchInterval = 0.5f; // 設定時間間隔

    public bool isPlayerInside = false; // 玩家是否在草丛内

    public float 目標透明度 = 0f; // 目標透明度（0 為完全透明，1 為完全不透明）
    void Start()
    {
        // 抓取玩家身上的所有子物件（包括子物件的子物件）
        子物件們 = GameObject.Find("主角").GetComponentsInChildren<Renderer>(true);
        //Renderer[] 子物件們 = GetComponentsInChildren<Renderer>(true);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            if (Time.time - lastPressTime >= switchInterval)
            {
                lastPressTime = Time.time;
                isPlayerInside = !isPlayerInside;
                if (isPlayerInside)
                {
                    移動.move.myRigidbody.velocity = Vector2.zero;
                    GameObject.Find("主角").GetComponent<移動>().enabled = false;
                    // 遍歷子物件陣列，設定透明度
                    foreach (Renderer 子物件 in 子物件們)
                    {
                        Color 原始顏色 = 子物件.material.color; // 取得子物件原始的顏色
                        Color 新顏色 = new Color(原始顏色.r, 原始顏色.g, 原始顏色.b, 目標透明度); // 設定新的顏色，僅調整透明度
                        子物件.material.color = 新顏色; // 設定子物件的新顏色
                    }

                    CapsuleCollider2D someComponent = GameObject.Find("玩家受傷碰撞器").GetComponentInChildren<CapsuleCollider2D>();
                    //other.gameObject.GetComponentInChildren<CapsuleCollider2D>().enabled = false;
                    if (someComponent != null)
                    {
                        someComponent.enabled = false;
                        GameObject.Find("弓").GetComponent<射箭腳本>().enabled = false;
                        
                    }
                    // 紀錄場景上所有的怪物
                    GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach (GameObject monster in monsters)
                    {
                        // 把怪物身上的腳本的看不到玩家設為 true
                        魔族小兵 monsterScript = monster.GetComponent<魔族小兵>();
                        if (monsterScript != null)
                        {
                            monsterScript.玩家躲入草叢怪物看不見 = true;
                            場景上的怪物們.Add(monster);
                        }
                    }

                }
                else
                {
                    GameObject.Find("主角").GetComponent<移動>().enabled = true;
                    foreach (Renderer 子物件 in 子物件們)
                    {
                        Color 原始顏色 = 子物件.material.color; // 取得子物件原始的顏色
                        Color 新顏色 = new Color(原始顏色.r, 原始顏色.g, 原始顏色.b, 1); // 設定新的顏色，僅調整透明度
                        子物件.material.color = 新顏色; // 設定子物件的新顏色
                    }
                    CapsuleCollider2D someComponent = GameObject.Find("玩家受傷碰撞器").GetComponentInChildren<CapsuleCollider2D>();
                    //other.gameObject.GetComponentInChildren<CapsuleCollider2D>().enabled = true;
                    if (someComponent != null)
                    {
                        someComponent.enabled = true;
                        GameObject.Find("弓").GetComponent<射箭腳本>().enabled = true;
                    }

                    GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach (GameObject monster in monsters)
                    {
                        // 把怪物身上的腳本的看不到玩家設為 true
                        魔族小兵 monsterScript = monster.GetComponent<魔族小兵>();
                        if (monsterScript != null)
                        {
                            monsterScript.玩家躲入草叢怪物看不見 = false;
                        }
                    }
                    場景上的怪物們.Clear();
                }
            }
        }
    }
    /*void OnTriggerEnter2D(Collider2D other)
    {
        print("aaa");
        if (other.gameObject.CompareTag("Player") )
        {
            isPlayerInside = !isPlayerInside;

            if (isPlayerInside)
            {
                print("bbb");
                // 遍歷子物件陣列，設定透明度
                foreach (Renderer 子物件 in 子物件們)
                {
                    Color 原始顏色 = 子物件.material.color; // 取得子物件原始的顏色
                    Color 新顏色 = new Color(原始顏色.r, 原始顏色.g, 原始顏色.b, 目標透明度); // 設定新的顏色，僅調整透明度
                    子物件.material.color = 新顏色; // 設定子物件的新顏色
                }
                //禁用玩家的一些行为，例如攻击
                other.gameObject.GetComponent<BoxCollider2D>().enabled = false;

                //通知敌人，玩家已经进入草丛内
                //BroadcastMessage("OnPlayerEnterGrass", SendMessageOptions.DontRequireReceiver);

            }
            else
            {
                print("ccc");
                foreach (Renderer 子物件 in 子物件們)
                {
                    Color 原始顏色 = 子物件.material.color; // 取得子物件原始的顏色
                    Color 新顏色 = new Color(原始顏色.r, 原始顏色.g, 原始顏色.b, 1); // 設定新的顏色，僅調整透明度
                    子物件.material.color = 新顏色; // 設定子物件的新顏色
                }
                //启用玩家的行为
                other.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                // 通知敌人，玩家已经离开草丛
                BroadcastMessage("OnPlayerExitGrass", SendMessageOptions.DontRequireReceiver);
            }
        }
    }*/

    /*void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInside = false;

            // 启用玩家的行为
            //other.gameObject.GetComponent<BoxCollider2D>().EnableActions();

            // 通知敌人，玩家已经离开草丛
            BroadcastMessage("OnPlayerExitGrass", SendMessageOptions.DontRequireReceiver);
        }
    }

    public bool IsPlayerInside()
    {
        return isPlayerInside;
    }*/
}
