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
    public PlayerData �s�ɸ��;

    public bool ������;
    public bool �~��C��;

    public void �U���ɮ�()
    {
        string json = PlayerPrefs.GetString("PLAYER_DATA", "N");
        if (json != "N")
        {
            �s�ɸ�� = JsonUtility.FromJson<PlayerData>(json);
            ������ = true;
        }
        else
        {
            �s�ɸ�� = new PlayerData();
            ������ = false;
        }
    }
    public void �W���ɮ�()
    {
        string json = JsonUtility.ToJson(�s�ɸ��, true);
        Debug.Log(json);
        PlayerPrefs.SetString("PLAYER_DATA", json);
    }
}
[System.Serializable]
public struct PlayerData
{
    /// <summary>���d</summary>
    public string LevelName;
    /// <summary>���a��m</summary>
    public Vector3 PlayerPos;
    /// <summary>���a��q</summary>
    public int PlayerHp;
    /// <summary>���a�]�Ʊ�</summary>
    public int PlayerDemonize;
    /// <summary>���a�P�Ʊ�</summary>
    public int PlayerXianhua;
    /// <summary>�]�r�b��</summary>
    public int PoisonArrow;
    /// <summary>�P��b�ڼƶq</summary>
    public int GrassJellyArrow;
    /// <summary>�]�r�ƶq</summary>
    public int Devilpoison;
    /// <summary>�P��ƶq</summary>
    public int GrassJelly;

    /// <summary>���a���D�� </summary>
    //public List<GoodItem> stuffs;
    /// <summary>��������</summary>
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


