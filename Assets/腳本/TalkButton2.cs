using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton2 : MonoBehaviour
{
    public static TalkButton 全域腳本;
    public GameObject Button;
    public GameObject talkUI;
    public TextAsset textFile;
    public GameObject 弓;
    public GameObject 傳送點;
    private void Awake()
    {
        DialogSystem.TextFile = textFile;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Button.SetActive(false);
    }

    private void Update()
    {
        當按鈕顯示時();
    }
    public void 當按鈕顯示時()
    {
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            talkUI.SetActive(true);
            GameObject.Find("主角").GetComponent<移動>().關閉移動速度();
            GameObject.Find("主角").GetComponent<移動>().enabled = false;
            弓.SetActive(true);
            傳送點.SetActive(true);
            GameObject.Find("弓").GetComponent<射箭腳本>().enabled = true;

        }
    }
}
