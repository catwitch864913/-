using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogSystem : MonoBehaviour
{
    public static DialogSystem text;
    public Text textLabel;
    public Image faceImage;
    //public GameObject ��ܮص���;
    //�奻���
    public static TextAsset TextFile;
    public int index;
    public float textSpeed;
    
    bool �����r�O�_����;
    bool �����@�Ӥ@�ӥ��r;

    //�Y��
    public Sprite face01,face02,face03, face04, face05, face06,face07;

    List<string> textList = new List<string>();

    void Awake()
    {
       // TalkButton.��e��� = TextFile;
    }
    private void OnEnable()
    {
        GetTextFormFile(TextFile);
        //textLabel.text = textList[index];
        //index++;
        �����r�O�_���� = true;
        StartCoroutine(SetTextUI());
        //if (��ܮص���.activeSelf)
        //{
        //    TalkButton.����}��.�R��������();
        //}
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            GameObject.Find("�D��").GetComponent<����>().enabled = true;
            if (GameObject.Find("�}")!=null)
            {
                GameObject.Find("�}").GetComponent<�g�b�}��>().enabled = true;
            }
            if (��O�иH�����o��.instance != null)
            {
                ��O�иH�����o��.instance.�I�s();
            }
            //�C����UI����.UIinterface.�P�w�_������();
            return;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (�����r�O�_���� && !�����@�Ӥ@�ӥ��r)
            {
                StartCoroutine(SetTextUI());
            }
            else if (!�����r�O�_���� && !�����@�Ӥ@�ӥ��r)
            {
                �����@�Ӥ@�ӥ��r = !�����@�Ӥ@�ӥ��r;
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
        �����r�O�_���� = false;
        textLabel.text = "";
        switch (textList[index])
        {
            case "�]��":
                faceImage.sprite = face01;
                index++;
                break;
            case "�]�H":
                faceImage.sprite = face02;
                index++;
                break;
            case "�]�]":
                faceImage.sprite = face03;
                index++;
                break;
            case "�]��":
                faceImage.sprite = face04;
                index++;
                break;
            case "�P�L":
                faceImage.sprite = face05;
                index++;
                break;
            case "��P":
                faceImage.sprite = face06;
                index++;
                break;
            case "��":
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
        while (!�����@�Ӥ@�ӥ��r && letter < textList[index].Length - 1)
        {
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }
        textLabel.text = textList[index];
        �����@�Ӥ@�ӥ��r = false;
        �����r�O�_���� = true;
        index++;
    }
}
