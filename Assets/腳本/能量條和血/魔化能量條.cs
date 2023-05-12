using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 魔化能量條 : MonoBehaviour
{
    public 射箭腳本 射箭腳本;

    public Text 能量;
    public static int 能量數;
    public int 初始能量;
    private float ftime;
    public bool 計時器;
    private Image 魔毒能量條;
    void Start()
    {
        //射箭腳本 = GameObject.Find("主角").transform.GetChild(3).GetComponent<aa>();
        射箭腳本 = GameObject.Find("弓").GetComponent<射箭腳本>();
        魔毒能量條 = GetComponent<Image>();
        能量數 = 初始能量 = 5;

    }
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
        扣();
        轉換();
        魔毒能量條.fillAmount = (float)能量數 / (float)初始能量;
        能量.text = 能量數.ToString() + "/" + 初始能量.ToString();
        if (能量數 >= 5) { 魔毒數量.當前魔毒數量 -= 0; 能量數 += 0; }
    }
    void 轉換()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (魔毒數量.當前魔毒數量 > 0)
            {
                魔毒數量.當前魔毒數量 -= 1;
                能量數 += 1;
            }
            else
            {
                魔毒數量.當前魔毒數量 -= 0;
                能量數 += 0;
            }

        }
    }
   void 扣()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (能量數 ==5)
            {
                射箭腳本.OpenDemonize = true;
                能量數 -= 5;
            }
        }
    }
}
