using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 動畫切換文本並開啟 : MonoBehaviour
{
    public TextAsset[] Timeline播完時的劇情;
    public GameObject talkUI;
    private void Start()
    {
        //talkUI = GameObject.Find("對話框");
    }
    public void 文件切換()
    {
        DialogSystem.TextFile = Timeline播完時的劇情[0];
        Invoke("開啟對話框", 0.2f);
    }
    public void 文件切換1()
    {
        DialogSystem.TextFile = Timeline播完時的劇情[1];
        Invoke("開啟對話框", 0.2f);
    }
    public void 文件切換2()
    {
        DialogSystem.TextFile = Timeline播完時的劇情[2];
        Invoke("開啟對話框", 0.2f);
    }
    public void 文件切換3()
    {
        DialogSystem.TextFile = Timeline播完時的劇情[3];
        Invoke("開啟對話框", 0.2f);
    }



    public void 開啟對話框()
    {
        talkUI.SetActive(true);
    }
}
