using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 傳送到門外 : MonoBehaviour
{
    public GameObject 提示;
    private void Start()
    {
        提示.SetActive(false);
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
            Application.LoadLevel("房外");
        }
    }
}
