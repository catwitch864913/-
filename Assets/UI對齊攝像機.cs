using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI對齊攝像機 : MonoBehaviour
{
    //public GameObject 魔族小兵;

    //private void LateUpdate()
    //{
    //    print("aaa");
    //    if (魔族小兵.transform.rotation.y == 180|| 魔族小兵.transform.rotation.y == -180)
    //    {
    //        print("ccc");
    //        this.transform.Rotate(0f, 180f, 0f);
    //    }
    //    else
    //    {
    //        print("bbb");
    //        this.transform.Rotate(0f, 0f, 0f);
    //    }
    //}
    private Transform monsterTransform; // 怪物的變換元件

    void Start()
    {
        // 取得怪物的變換元件
        monsterTransform = transform.parent;
    }

    void LateUpdate()
    {
        // 設定UI物件的位置和旋轉與怪物一致
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, monsterTransform.rotation.eulerAngles.z));
    }
}
