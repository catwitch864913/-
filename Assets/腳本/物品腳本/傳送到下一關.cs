using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 傳送到下一關 : MonoBehaviour
{
    public GameObject 提示;
    //public 遊戲內UI介面 UIinterface;

    private void Start()
    {
        提示.SetActive(false);
        //UIinterface = GameObject.Find("UI").GetComponent<遊戲內UI介面>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        提示.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        提示.SetActive(false);
    }
    private void Update()
    {
        傳送門外();
    }
    public void 傳送門外()
    {
        if (提示.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            //Application.LoadLevel(Application..buildIndex + 1 + 1);
            //遊戲內UI介面.UIinterface.Save();
            //UIinterface.AutoSave();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
