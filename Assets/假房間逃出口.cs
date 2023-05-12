using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 假房間逃出口 : MonoBehaviour
{
    public static 假房間逃出口 逃出口腳本;
    public int 當前怪物量;
    public Transform 怪物出沒點;
    public GameObject 地下室之門;
    public GameObject 要召喚的怪物;
    public GameObject 身上的按鈕;
    public GameObject 對話框;
    public int 進入次數;
    int 觸發一次;

    public TextAsset 文本;

    void Start()
    {
        身上的按鈕 = transform.GetChild(0).gameObject;
    }
    void Update()
    {
        if (進入次數 >= 5 && 觸發一次 == 0)
        {
            文件切換();
            觸發一次++;
            對話框.SetActive(true);
            地下室之門.SetActive(true);
        }
    }
    public void 召喚怪物()
    {
        print("AAAA");
        當前怪物量 = Random.Range(2, 6);
        for (int i = 0; i < 當前怪物量; i++)
        {
            Instantiate(要召喚的怪物, 怪物出沒點.position, Quaternion.identity);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        身上的按鈕.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        身上的按鈕.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (當前怪物量 <= 0 && Input.GetKeyDown(KeyCode.R) && 身上的按鈕.activeSelf)
        {
            進入次數++;
            移動.move.TeleportBack();
        }
    }
    public void 當怪物被殺()
    {
        當前怪物量--;
    }
    public void 重製當前怪物數量()
    {
        當前怪物量 = 0;
    }
    public void 文件切換()
    {
        DialogSystem.TextFile = 文本;
    }

}
