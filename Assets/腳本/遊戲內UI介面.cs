using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class 遊戲內UI介面 : MonoBehaviour
{
    public static 遊戲內UI介面 UIinterface;
    //public GameObject 暫停介面;
    public GameObject 轉換箭矢介紹介面;
    public GameObject 選擇介面;
    public GameObject 對話框;

    public TextAsset[] 復仇文件;

    //bool 開關暫停;
    bool 轉換箭矢開關;
    bool 選擇介面判定;

    #region 讀取檔案
    public GameObject 讀取介面;
    bool 讀取開關;
    #endregion
    #region 設定
    public GameObject 設定介面;
    bool 按鈕開關;
    #endregion
    #region 聲音控制
    public AudioMixer 遊戲音樂控制器;
    //public AudioSource 設定按鈕音效元件;
    //public AudioSource 離開按鈕音效元件;
    //public AudioSource 讀取按鈕音效元件;
    public Toggle BGM勾取方塊;
    public Toggle SFX勾取方塊;
    bool 音樂開關;
    public Slider 聲音音量條;
    public Slider 音效音量條;
    #endregion
    #region 聲音貼圖
    public Sprite 音樂開啟的圖片;
    public Sprite 音樂關閉的圖片;
    public Image 音樂按鈕的照片;
    #endregion
    #region 解析度
    public Dropdown 螢幕解析度;
    #endregion

    public void Start()
    {
        音樂按鈕的照片.sprite = 音樂開啟的圖片;
    }
    private void Update()
    {
        //按ESC開起暫停();
        判定復仇介面();
    }
    /*public void 按ESC開起暫停()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            開關暫停 = true;
            暫停介面.SetActive(開關暫停);
            Time.timeScale = 0f;
            GameObject.Find("弓").GetComponent<aa>().enabled = false;
        }
    }
    public void 關閉暫停介面()
    {
        開關暫停 = false;
        暫停介面.SetActive(開關暫停);
        Time.timeScale = 1f;
        GameObject.Find("弓").GetComponent<aa>().enabled = true;
    }
    */
    public void 開關轉換箭矢介紹()
    {
        轉換箭矢開關 = !轉換箭矢開關;
        轉換箭矢介紹介面.SetActive(轉換箭矢開關);
        if (轉換箭矢開關)
        {
            Time.timeScale = 0f;
            GameObject.Find("弓").GetComponent<射箭腳本>().enabled = false;
        }
        else
        {
            Time.timeScale = 1f;
            GameObject.Find("弓").GetComponent<射箭腳本>().enabled = true;
        }
    }
    public void 判定復仇介面()
    {
        if (選擇介面判定 == true && !對話框.activeSelf)
        {
            ToChapterTwo();
        }
    }
    public void 選擇復仇()
    {
        選擇介面判定 = true;
        DialogSystem.TextFile = 復仇文件[0];
        對話框.SetActive(true);
        //AI.chameleonController.關閉移動速度();
        GameObject.Find("主角").GetComponent<移動>().關閉移動速度();
        GameObject.Find("主角").GetComponent<移動>().enabled = false;
        GameObject.Find("弓").GetComponent<射箭腳本>().enabled = false;
        選擇介面.SetActive(false);
    }
    public void 選擇不復仇()
    {
        選擇介面判定 = true;
        DialogSystem.TextFile = 復仇文件[1];
        對話框.SetActive(true);
        //AI.chameleonController.關閉移動速度();
        GameObject.Find("主角").GetComponent<移動>().關閉移動速度();
        GameObject.Find("主角").GetComponent<移動>().enabled = false;
        GameObject.Find("弓").GetComponent<射箭腳本>().enabled = false;
        選擇介面.SetActive(false);
    }
    public void ReadFile()
    {
        //讀取開關 = !讀取開關;
        ////讀取按鈕音效元件.enabled = 讀取開關;
        //讀取介面.SetActive(讀取開關);

    }
    //public void AutoSave()
    //{
    //    //SaveManger.instance.存檔資料.LevelName = SceneManager.GetActiveScene().name;
    //    SaveManger.instance.存檔資料.PlayerHp = 移動.move.當前血量;
    //    SaveManger.instance.存檔資料.PoisonArrow = 切換箭矢.當前仙草箭矢數量;
    //    SaveManger.instance.存檔資料.GrassJellyArrow = 切換箭矢.當前魔毒箭矢數量;
    //    SaveManger.instance.存檔資料.Devilpoison = 魔毒數量.當前魔毒數量;
    //    SaveManger.instance.存檔資料.GrassJelly = 仙草數量.當前仙草數量;
    //    SaveManger.instance.上傳檔案();
    //}
    public void Save()
    {
        SaveManger.instance.存檔資料.LevelName = SceneManager.GetActiveScene().name;
        SaveManger.instance.存檔資料.PlayerPos = 移動.move.transform.position;
        SaveManger.instance.存檔資料.PlayerHp = 移動.當前血量;
        SaveManger.instance.存檔資料.PoisonArrow = 切換箭矢.當前仙草箭矢數量;
        SaveManger.instance.存檔資料.GrassJellyArrow = 切換箭矢.當前魔毒箭矢數量;
        SaveManger.instance.存檔資料.Devilpoison = 魔毒數量.當前魔毒數量;
        SaveManger.instance.存檔資料.GrassJelly = 仙草數量.當前仙草數量;
        SaveManger.instance.上傳檔案();
    }
    public void Setting()
    {
        按鈕開關 = !按鈕開關;
        //設定按鈕音效元件.enabled = 按鈕開關;
        設定介面.SetActive(按鈕開關);

    }
    #region 聲音介面設定
    public void sound()
    {
        音樂開關 = !音樂開關;
        AudioListener.pause = 音樂開關;
        if (音樂開關)
        {
            音樂按鈕的照片.sprite = 音樂關閉的圖片;
        }
        else
        {
            音樂按鈕的照片.sprite = 音樂開啟的圖片;
        }

    }
    public void SoundSlider()
    {
        if (聲音音量條.value == 0)
        {
            遊戲音樂控制器.SetFloat("BGM", -80f);
        }
        else
        {
            遊戲音樂控制器.SetFloat("BGM", 0f);
        }
    }
    public void SFXSlider()
    {
        if (音效音量條.value == 0)
        {
            遊戲音樂控制器.SetFloat("SFX", -80f);
        }
        else
        {
            遊戲音樂控制器.SetFloat("SFX", 0f);
        }
    }
    public void BGMToggle()
    {
        //當BGM的Toggle 打勾 代表isOn為True
        if (BGM勾取方塊.isOn)
        {
            //AudioMixer 背景音樂值為0
            遊戲音樂控制器.SetFloat("BGM", 0f);
        }
        //當BGM的Toggle 不打勾 代表isOn為false
        else
        {
            //AudioMixer 背景音樂值為-80
            遊戲音樂控制器.SetFloat("BGM", -80f);
        }
    }
    public void SFXToggle()
    {
        //當BGM的Toggle 打勾 代表isOn為True
        if (SFX勾取方塊.isOn)
        {
            //AudioMixer 背景音樂值為0
            遊戲音樂控制器.SetFloat("SFX", 0f);
        }
        //當BGM的Toggle 不打勾 代表isOn為false
        else
        {
            //AudioMixer 背景音樂值為-80
            遊戲音樂控制器.SetFloat("SFX", -80f);
        }
    }
    public void 當BGM和SFX的勾選框都被關時()
    {
        if (BGM勾取方塊.isOn || SFX勾取方塊.isOn)
        {
            音樂按鈕的照片.sprite = 音樂開啟的圖片;
        }
        else
        {
            音樂按鈕的照片.sprite = 音樂關閉的圖片;
        }
    }
    public void ScreenSize()
    {
        switch (螢幕解析度.value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, false);
                break;
            case 1:
                Screen.SetResolution(1280, 720, false);
                break;
            case 2:
                Screen.SetResolution(800, 480, false);
                break;
        }
    }
    #endregion
    #region 跳轉視窗
    public void ToMenu()
    {
        SceneManager.LoadScene("遊戲首頁_柏勳");
        Time.timeScale = 1f;
    }
    public void ToChapterTwo()
    {
        SceneManager.LoadScene("第二章主地圖-2");
        Time.timeScale = 1f;
    }
    #endregion

}
