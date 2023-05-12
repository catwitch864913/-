using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 傳送到假房間 : MonoBehaviour
{
    // 記錄傳送目標位置的變數
    public GameObject 身上的按鈕;
    public Transform teleportTarget;
    public 假房間逃出口 逃出口腳本;

    private void Start()
    {
        teleportTarget = GameObject.Find("門 (8)").transform;
        逃出口腳本 = GameObject.Find("門 (8)").GetComponent<假房間逃出口>();
        身上的按鈕 = transform.GetChild(0).gameObject;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        身上的按鈕.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        身上的按鈕.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.R) && 身上的按鈕.activeSelf)
        {
            // 將當前傳送門的位置記錄在玩家身上
            other.GetComponent<移動>().teleportSource = transform.position;
            // 傳送玩家到目標位置
            other.transform.position = teleportTarget.position;
            //假房間逃出口.逃出口腳本.召喚怪物();
            逃出口腳本.召喚怪物();
        }
    }

}
