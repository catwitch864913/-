using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class 切換箭矢 : MonoBehaviour
{
    public Image 目前使用箭矢顯示;
    private int 目前使用箭矢 = 0;
    private int 目前使用箭矢數量 = 0;
    public GameObject[] 箭矢array;
    public Sprite[] 右上角箭矢顯示;
    public Text 箭矢數量顯示位置;
    public string 普通箭矢數量;
    public int 魔毒箭矢數量;
    public static int 當前魔毒箭矢數量;
    public static int 當前仙草箭矢數量;
    public int 仙草箭矢數量;

    /*public void Awake()
    {
        if (SaveManger.instance.繼續遊戲 == true)
        {
            
            SaveManger.instance.繼續遊戲 = false;
        }
        else
        {
            當前魔毒箭矢數量 = 3;
            當前仙草箭矢數量 = 2;
        }
    }*/
    private void Start()
    {
        目前使用箭矢顯示 = GameObject.Find("普通箭UI").GetComponent<Image>();
        箭矢數量顯示位置 = GameObject.Find("目前箭矢數量").GetComponent<Text>();
    }
    private void Update()
    {
        轉換();
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            目前使用箭矢++;
            目前使用箭矢數量++;
            if (目前使用箭矢 > 箭矢array.Length - 1)
            {
                目前使用箭矢 = 0;
                目前使用箭矢數量 = 0;
            }
            gameObject.GetComponentInChildren<射箭腳本>().projectile = 箭矢array[目前使用箭矢];
            目前使用箭矢顯示.sprite = 右上角箭矢顯示[目前使用箭矢];
            /*箭矢數量顯示位置.text = 各種箭矢數量.ToString(char)[目前使用箭矢數量];
            if (目前使用箭矢 == 0)
            {
                箭矢數量顯示位置.text = 普通箭矢數量;
            }*/
            if (目前使用箭矢數量 == 1)
            {
                箭矢數量顯示位置.text = 當前魔毒箭矢數量.ToString();
                if (當前魔毒箭矢數量 <= 0)
                {
                    當前魔毒箭矢數量 = 0;
                    GameObject.Find("弓").GetComponent<射箭腳本>().enabled = false;
                }
                else
                {
                    GameObject.Find("弓").GetComponent<射箭腳本>().enabled = true;
                }
            }
            else if (目前使用箭矢數量 == 2)
            {
                箭矢數量顯示位置.text = 仙草箭矢數量.ToString();
                if (當前仙草箭矢數量 <= 0)
                {
                    當前仙草箭矢數量 = 0;
                    GameObject.Find("弓").GetComponent<射箭腳本>().enabled = false;
                }
            }
            else
            {
                箭矢數量顯示位置.text = 普通箭矢數量;
                GameObject.Find("弓").GetComponent<射箭腳本>().enabled = true;
            }
        }
    }
    void 轉換()
    {
        if (目前使用箭矢數量 == 1)
        {
            箭矢數量顯示位置.text = 當前魔毒箭矢數量.ToString();
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (魔毒數量.當前魔毒數量 > 0)
                {
                    GameObject.Find("弓").GetComponent<射箭腳本>().enabled = true;
                    魔毒數量.當前魔毒數量 -= 1;
                    當前魔毒箭矢數量 += 5;
                }
                if (當前魔毒箭矢數量 >= 30)
                {
                    當前魔毒箭矢數量 = 30;
                }
            }
            if (當前魔毒箭矢數量 <= 0)
            {
                當前魔毒箭矢數量 = 0;
                GameObject.Find("弓").GetComponent<射箭腳本>().enabled = false;
            }
        }
        if (目前使用箭矢數量 == 2)
        {
            箭矢數量顯示位置.text = 當前仙草箭矢數量.ToString();
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (仙草數量.當前仙草數量 > 0)
                {
                    GameObject.Find("弓").GetComponent<射箭腳本>().enabled = true;
                    仙草數量.當前仙草數量 -= 1;
                    當前仙草箭矢數量 += 5;
                }
                if (當前仙草箭矢數量 >= 30)
                {
                    當前仙草箭矢數量 = 30;
                }
            }
            if (當前仙草箭矢數量 <= 0)
            {
                當前仙草箭矢數量 = 0;
                GameObject.Find("弓").GetComponent<射箭腳本>().enabled = false;
            }
        }
    }
}
