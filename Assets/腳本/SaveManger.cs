using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManger
{
    static SaveManger _instance;
    public static SaveManger instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SaveManger();
            }
            return _instance;
        }
    }
    public PlayerData 存檔資料;

    public bool 有紀錄;
    public bool 繼續遊戲;

    public void 下載檔案()
    {
        string json = PlayerPrefs.GetString("PLAYER_DATA", "N");
        if (json != "N")
        {
            存檔資料 = JsonUtility.FromJson<PlayerData>(json);
            有紀錄 = true;
        }
        else
        {
            存檔資料 = new PlayerData();
            有紀錄 = false;
        }
    }
    public void 上傳檔案()
    {
        string json = JsonUtility.ToJson(存檔資料, true);
        Debug.Log(json);
        PlayerPrefs.SetString("PLAYER_DATA", json);
    }
}
[System.Serializable]
public struct PlayerData
{
    /// <summary>關卡</summary>
    public string LevelName;
    /// <summary>玩家位置</summary>
    public Vector3 PlayerPos;
    /// <summary>玩家血量</summary>
    public int PlayerHp;
    /// <summary>玩家魔化條</summary>
    public int PlayerDemonize;
    /// <summary>玩家仙化條</summary>
    public int PlayerXianhua;
    /// <summary>魔毒箭矢</summary>
    public int PoisonArrow;
    /// <summary>仙草箭矢數量</summary>
    public int GrassJellyArrow;
    /// <summary>魔毒數量</summary>
    public int Devilpoison;
    /// <summary>仙草數量</summary>
    public int GrassJelly;

    /// <summary>玩家的道具 </summary>
    //public List<GoodItem> stuffs;
    /// <summary>場景相關</summary>
    //public List<SceneData> sceneDatas;
}
[System.Serializable]
public struct SceneData
{
    public string key;
    public bool flag;
    public Vector3 pos;
    public string info;
}


