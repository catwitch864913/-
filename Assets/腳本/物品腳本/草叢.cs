using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class 草叢 : MonoBehaviour
{
    public string layerName = "怪物重生點";
    public GameObject 隱藏對象;
    public GameObject[] 怪物重生點;
    public 生成敵人[] myScripts;
    bool 碰撞;
    public static bool 被隱藏;
    public static int 鍋子下毒數 = 0;
    public PlayableDirector 播放;
    protected bool 觸發動畫;
    public GameObject 主;
    public GameObject 副;
    public GameObject 動畫;
    public GameObject 動畫敵人;
    public GameObject 傳送點;


    // Start is called before the first frame update
    void Start()
    {
        
        主.SetActive(true);
        副.SetActive(false);
        被隱藏 = false;
        隱藏對象 = GameObject.Find("主角");
        碰撞 = false;
        隱藏對象.SetActive(true);
        怪物重生點 = GameObject.FindGameObjectsWithTag(layerName);
        myScripts = new 生成敵人[怪物重生點.Length];
        for (int i = 0; i < 怪物重生點.Length; i++)
        {
            myScripts[i] = 怪物重生點[i].GetComponent<生成敵人>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //print(鍋子下毒數);
        隱藏();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            碰撞 = true;
        }

    }
    void OnTriggerStay2D(Collider2D collider)
    {
        碰撞 = true;
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        碰撞 = false;
        隱藏對象.SetActive(true);
    }
    void 下毒後()
    {
        if (鍋子下毒數 == 5)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy2");
            for (int i = 0; i < enemies.Length; i++)
            {
                Destroy(enemies[i]);
            }
            播放.Play();
            for (int i = 0; i < myScripts.Length; i++)
            {
                Destroy(myScripts[i]);
            }

            觸發動畫 = true;
            鍋子下毒數 = 0;
            Invoke("撥放完", ((float)播放.duration - 1f));

        }
    }
    void 開啟主角()
    {

        隱藏對象.SetActive(true);
    }
    void 撥放完()
    {
        傳送點.SetActive(true);
        Destroy(副);
        Destroy(動畫敵人);

    }
    void 隱藏()
    {
        if (碰撞 == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                被隱藏 = true;
                下毒後();
                隱藏對象.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            被隱藏 = false;
            隱藏對象.SetActive(true);
        }
    }
}
