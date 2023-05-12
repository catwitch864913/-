using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class 血量判定 : MonoBehaviour
{
    public Sprite 滿血瓶;
    public Sprite 空血瓶;
    public GameObject 血瓶物件;
    public GameObject[] 血瓶all;
    public int 血瓶血量;

    void Start()
    {
        血瓶all = new GameObject[20];
        for (int i = 0; i < 血瓶all.Length; i++)
        {
            GameObject obj = Instantiate(血瓶物件, transform);
            血瓶all[i] = obj;
        }
    }
    void Update()
    {
        for (int i = 0; i < 血瓶all.Length; i++)
        {
            if (i < 移動.當前血量 / 血瓶血量)
            {
                血瓶all[i].GetComponent<Image>().sprite = 滿血瓶;
            }
            else
            {
                血瓶all[i].GetComponent<Image>().sprite = 空血瓶;
                
            }
        }
    }

}