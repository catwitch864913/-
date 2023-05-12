using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 敵人生成 : MonoBehaviour
{
    public GameObject 生成點1;
    public GameObject 生成點2;
    public GameObject 生成點3;
    public GameObject 生成點4;
    public GameObject 敵人;
    public int enemyCount = 0;//敌人数量

    void Start()
    {
        
    }
    private float CreatTime = 1f;//第一次创建敌人的时间
    int 隨機點;
    void Update()
    {
        生成();
    }
    void 生成()
    {
        CreatTime -= Time.deltaTime;    //开始倒计时
        隨機點 = Random.Range(0, 4);
        if (CreatTime <= 0 && enemyCount < 7) //倒计时为0并且敌人数量小于5
        {
            if (隨機點 == 0)
            {
                Instantiate(敵人, 生成點1.transform.position, Quaternion.identity);
            }
            else if (隨機點 == 1)
            {
                Instantiate(敵人, 生成點2.transform.position, Quaternion.identity);
            }
            else if (隨機點 == 2)
            {
                Instantiate(敵人, 生成點3.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(敵人, 生成點4.transform.position, Quaternion.identity);
            }
            CreatTime = 1;//后续敌人每5秒出来一次
            enemyCount++;//敌人数量加1
        }
    }
}