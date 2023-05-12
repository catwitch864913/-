using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 生成敵人 : MonoBehaviour
{
    //public static 生成敵人 creaftMoster;
    public float 幾秒後開始生成敵人 = 5f;
    public float 怪物生成時間 = 10f;

    public int 最大產生量 = 3;
    public int 當前產生量 = 0;

    public GameObject 生成的怪物, SB怪物;



    private void Start()
    {
        InvokeRepeating("開始生成", 幾秒後開始生成敵人, 怪物生成時間);
    }
    private void Update()
    {
        //InvokeRepeating("開始生成", 幾秒後開始生成敵人, 怪物生成時間);

    }

    public void 開始生成()
    {
        if (當前產生量 >= 最大產生量)
        {
            return;
        }
        else
        {
            float a = Random.Range(0, 3);
            if (a <= 1.5f)
            {
                Instantiate(生成的怪物, transform.position, Quaternion.identity, null);
            }
            else
            {
                Instantiate(SB怪物, transform.position, Quaternion.identity, null);
            }
            當前產生量++;
        }
    }
    public void 當怪物被殺掉時()
    {
        this.當前產生量 --;
    }
    
}
