using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton2 : MonoBehaviour
{
    public static TalkButton ����}��;
    public GameObject Button;
    public GameObject talkUI;
    public TextAsset textFile;
    public GameObject �};
    public GameObject �ǰe�I;
    private void Awake()
    {
        DialogSystem.TextFile = textFile;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Button.SetActive(false);
    }

    private void Update()
    {
        ����s��ܮ�();
    }
    public void ����s��ܮ�()
    {
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            talkUI.SetActive(true);
            GameObject.Find("�D��").GetComponent<����>().�������ʳt��();
            GameObject.Find("�D��").GetComponent<����>().enabled = false;
            �}.SetActive(true);
            �ǰe�I.SetActive(true);
            GameObject.Find("�}").GetComponent<�g�b�}��>().enabled = true;

        }
    }
}
