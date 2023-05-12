using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 魔毒數量 : MonoBehaviour
{
    //public int 初始;
    public Text 文字;
    
    public static int 當前魔毒數量;
    /*private void Awake()
    {
        if (SaveManger.instance.繼續遊戲 == true)
        {
            魔毒數量.當前魔毒數量 = SaveManger.instance.存檔資料.Devilpoison;
            SaveManger.instance.繼續遊戲 = false;
        }
        else
        {
            當前魔毒數量 = 3;
        }   
    }*/
    void Start()
    {
        當前魔毒數量 = 5;
    }

    void Update()
    {
        文字.text = 當前魔毒數量.ToString();
    }
}
