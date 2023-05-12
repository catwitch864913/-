using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 暫停腳本 : Windows<暫停腳本>
{
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpen == false)
            {
                Open();
            }
            else
            {
                Close();
            }
        }
        Time.timeScale = 1f-alpha;
    }
    public override void Open()
    {
        base.Open();
        //this.gameObject.SetActive(true);
        if (GameObject.Find("弓") != null)
        {
            GameObject.Find("弓").GetComponent<射箭腳本>().enabled = false;
        }
    }
    public override void Close()
    {
        base.Close();
        //this.gameObject.SetActive(false);
        if (GameObject.Find("弓") != null)
        {
            GameObject.Find("弓").GetComponent<射箭腳本>().enabled = true;
        }
    }
}