using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 地牢出口 : MonoBehaviour
{
    public GameObject 身上的canvas;
    private void Start()
    {
        身上的canvas = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        身上的canvas.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        身上的canvas.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.R) && 身上的canvas.activeSelf)
        {
            移動.move.TeleportBack();
        }
    }
}
