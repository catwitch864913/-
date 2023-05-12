using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class 首領逃跑 : MonoBehaviour
{
    public PlayableDirector 播放;
    protected bool 觸發動畫;
    public GameObject 傳送點;
    void Update()
    {
        播放動畫();
    }

    void 播放動畫()
    {
        觸發動畫 = BOSS.動畫;
        if (觸發動畫)
        {
            播放.Play();
            Invoke("撥放完", ((float)播放.duration - 1f));
        }
    }

    void 撥放完()
    {
        BOSS.動畫=false;
        傳送點.SetActive(true);
    }
}
