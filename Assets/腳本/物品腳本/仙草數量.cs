using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 仙草數量 : MonoBehaviour
{
    //public int 初始;
    public Text 文字;

    public static int 當前仙草數量;

    void Start()
    {
        當前仙草數量 = 5;
    }

    void Update()
    {
        文字.text = 當前仙草數量.ToString();
    }
}
