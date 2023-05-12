using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class 仙化條 : MonoBehaviour
{
    public 仙化Buff 仙化buff;

    public Text 能量;
    public static int 能量數;
    public int 初始能量;
    private float ftime;
    public bool 計時器;

    private Image 仙化能量條;

    

    void Start()
    {
        仙化buff = GameObject.Find("主角").GetComponent<仙化Buff>();
        仙化能量條 = GetComponent<Image>();
        能量數 = 初始能量 = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (計時器)
        {
            ftime += Time.deltaTime;
            if (ftime >= 60f)
            {

                Debug.Log("1");
                能量數 += 1;
                ftime = 0f;
            }
            if (能量數 >= 5)
            {

                能量數 = 5;
                ftime = 0f;
            }
        }
        if (能量數 >= 5) { 仙草數量.當前仙草數量 -= 0; 能量數 += 0; }

        扣();
        轉換();
        仙化能量條.fillAmount = (float)能量數 / (float)初始能量;
        能量.text = 能量數.ToString() + "/" + 初始能量.ToString();
    }
    void 轉換()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (仙草數量.當前仙草數量 > 0) 
            {
                仙草數量.當前仙草數量 -= 1;
                能量數 += 1;
            }
            else
            {
                仙草數量.當前仙草數量 -= 0;
                能量數 += 0;
            }
            if (能量數 == 5)
            {
                仙草數量.當前仙草數量 -= 0;
                能量數 += 0;
            }
        } 
    }
    void 扣()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (能量數 == 5)
            {
                仙化buff.BuffOpen = true;
                能量數 -= 5;
            }
        }
    }
}


