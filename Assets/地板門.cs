using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 地板門 : MonoBehaviour
{
    public GameObject 身上的按鈕;
    public Transform teleportTarget;
    void Start()
    {
        身上的按鈕 = transform.GetChild(0).gameObject;
    }
    void Update()
    {
        
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
        }
    }
}
