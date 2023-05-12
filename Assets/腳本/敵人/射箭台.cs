using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum 射箭台種類
{
    往右射,
    往左射,
}
public class 射箭台 : MonoBehaviour
{
     public 射箭台種類 射箭台類別;
    private float ftime;
    bool 計時器;
    public GameObject 箭矢;
    public GameObject 射出點;
    void Start()
    {
        計時器 = true;
    }

    void Update()
    {
        if (計時器)
        {
            ftime += Time.deltaTime;
            if (ftime >= 2f)
            {
                Instantiate(箭矢, 射出點.transform.position, Quaternion.identity);
                ftime = 0f;
            }
        }
    }
}
