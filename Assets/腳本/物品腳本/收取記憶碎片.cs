using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 收取記憶碎片 : MonoBehaviour
{
    public GameObject Button;
    public GameObject talkUI;

    public int 碎片編號;
    public int 文件編號;

    void Start()
    {
        Button.SetActive(false);
    }
    void Update()
    {
        當記憶碎片收集時();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        Button.SetActive(true);
    } 
    void OnTriggerExit2D(Collider2D other)
    {
        Button.SetActive(false);
    }
    public void 當記憶碎片收集時()
    {
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.R))
        {

            當記憶碎片取得時.instance.文件切換(文件編號);
            當記憶碎片取得時.instance.碎片收集(碎片編號);
            talkUI.SetActive(true);
            //AI.chameleonController.關閉移動速度();
            GameObject.Find("主角").GetComponent<移動>().關閉移動速度();
            GameObject.Find("主角").GetComponent<移動>().enabled = false;
            GameObject.Find("弓").GetComponent<射箭腳本>().enabled = false;
            音效.撿();
            Destroy(gameObject);
        }

    }
}
