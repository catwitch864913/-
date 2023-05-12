using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class 遊戲首頁UI : MonoBehaviour
{
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
    public AudioSource 設定按鈕音效元件;
    public AudioSource 開始按鈕音效元件;
    public AudioSource 離開按鈕音效元件;
    public AudioSource 讀取按鈕音效元件;
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

    private void Awake()
    {
        SaveManger.instance.下載檔案();
    }

    public void Start()
    {
        音樂按鈕的照片.sprite = 音樂開啟的圖片;
    }

    public void Setting()
    {
        按鈕開關 = !按鈕開關;
        設定按鈕音效元件.enabled = 按鈕開關;
        設定介面.SetActive(按鈕開關);

    }
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
            音樂開關 = false;
            sound();
        }
        else
        {
            音樂開關 = true;
            sound();
        }
    }
    public void SFXSlider()
    {
        if (音效音量條.value == 0)
        {
            遊戲音樂控制器.SetFloat("SFX", 0f);
        }
        else
        {
            遊戲音樂控制器.SetFloat("SFX", -80f);
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
    #region 跳轉視窗
    public void ToGame()
    {
        SaveManger.instance.繼續遊戲 = false;
        轉場器.ins.轉場(黑幕降下來之後);
    }
    void 黑幕降下來之後()
    {
        //Application.LoadLevel("房內");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ReadFile()
    {
        //讀取開關 = !讀取開關;
        //讀取按鈕音效元件.enabled = 讀取開關;
        //讀取介面.SetActive(讀取開關);
        if (SaveManger.instance.有紀錄 == false)
            return;
        SaveManger.instance.繼續遊戲 = true;
        轉場器.ins.轉場(SaveManger.instance.存檔資料.LevelName);
        //SceneManager.LoadScene(SaveManger.instance.存檔資料.LevelName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}
