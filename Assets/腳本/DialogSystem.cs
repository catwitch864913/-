using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogSystem : MonoBehaviour
{
    public static DialogSystem text;
    public Text textLabel;
    public Image faceImage;
    //public GameObject 對話框視窗;
    //文本文件
    public static TextAsset TextFile;
    public int index;
    public float textSpeed;
    
    bool 此行文字是否打完;
    bool 取消一個一個打字;

    //頭像
    public Sprite face01,face02,face03, face04, face05, face06,face07;

    List<string> textList = new List<string>();

    void Awake()
    {
       // TalkButton.當前文件 = TextFile;
    }
    private void OnEnable()
    {
        GetTextFormFile(TextFile);
        //textLabel.text = textList[index];
        //index++;
        此行文字是否打完 = true;
        StartCoroutine(SetTextUI());
        //if (對話框視窗.activeSelf)
        //{
        //    TalkButton.全域腳本.刪除此物件();
        //}
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            GameObject.Find("主角").GetComponent<移動>().enabled = true;
            if (GameObject.Find("弓")!=null)
            {
                GameObject.Find("弓").GetComponent<射箭腳本>().enabled = true;
            }
            if (當記憶碎片取得時.instance != null)
            {
                當記憶碎片取得時.instance.呼叫();
            }
            //遊戲內UI介面.UIinterface.判定復仇介面();
            return;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (此行文字是否打完 && !取消一個一個打字)
            {
                StartCoroutine(SetTextUI());
            }
            else if (!此行文字是否打完 && !取消一個一個打字)
            {
                取消一個一個打字 = !取消一個一個打字;
            }
        }
    }
    void GetTextFormFile(TextAsset file)
    {
        textList.Clear();
        index = 0;
        var lineDate = file.text.Split("\r\n");
        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
    }
    IEnumerator SetTextUI()
    {
        此行文字是否打完 = false;
        textLabel.text = "";
        switch (textList[index])
        {
            case "夜雪":
                faceImage.sprite = face01;
                index++;
                break;
            case "夜寒":
                faceImage.sprite = face02;
                index++;
                break;
            case "夜焱":
                faceImage.sprite = face03;
                index++;
                break;
            case "魔荒":
                faceImage.sprite = face04;
                index++;
                break;
            case "仙無":
                faceImage.sprite = face05;
                index++;
                break;
            case "伏涯":
                faceImage.sprite = face06;
                index++;
                break;
            case "旁":
                faceImage.sprite = face07;
                index++;
                break;
        }

        //for (int i = 0; i < textList[index].Length; i++)
        //{
        //    textLabel.text += textList[index][i];

        //    yield return new WaitForSeconds(textSpeed);
        //}
        int letter = 0;
        while (!取消一個一個打字 && letter < textList[index].Length - 1)
        {
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }
        textLabel.text = textList[index];
        取消一個一個打字 = false;
        此行文字是否打完 = true;
        index++;
    }
}
