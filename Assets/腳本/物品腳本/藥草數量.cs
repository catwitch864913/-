using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 藥草數量 : MonoBehaviour
{
    //public int 初始;
    public Text 文字;
    public static int 當前數量;
    void Start()
    {
        //當前數量 = 初始;
    }

    void Update()
    {
        文字.text = 當前數量.ToString();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            當前數量 -= 1;
            移動.當前血量 += 10;
        }
    }
}
