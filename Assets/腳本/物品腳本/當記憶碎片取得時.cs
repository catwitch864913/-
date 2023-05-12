using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 當記憶碎片取得時 : MonoBehaviour
{
    public static 當記憶碎片取得時 instance;
    public GameObject 存放碎片;
    public GameObject[] 顯示收集到的記憶碎片;
    public TextAsset[] 收集到記憶碎片時的劇情;
    public GameObject 對話框;
    public GameObject 開啟視窗;
    public bool 收集完成 = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void Start()
    {
        顯示收集到的記憶碎片 = new GameObject[存放碎片.transform.childCount];
        for (int i = 0; i < 顯示收集到的記憶碎片.Length; i++)
        {
            顯示收集到的記憶碎片[i] = 存放碎片.transform.GetChild(i).gameObject;
            顯示收集到的記憶碎片[i].SetActive(false);
        }
    }
    public void 碎片收集(int 碎片編號)
    {
        顯示收集到的記憶碎片[碎片編號].SetActive(true);

        for (int i = 0; i < 顯示收集到的記憶碎片.Length; i++)
        {
            if (!顯示收集到的記憶碎片[i].activeSelf)
            {
                return;
            }
        }
        if (顯示收集到的記憶碎片[碎片編號].activeSelf)
        {
            收集完成 = true;
        }
    }
    public void 呼叫()
    {
        if (收集完成 == true && !對話框.activeSelf)
        {
            開啟視窗.SetActive(true);
            收集完成 = false;
        }
    }
    public void 文件切換(int 文件編號)
    {
        DialogSystem.TextFile = 收集到記憶碎片時的劇情[文件編號];
    }
}
