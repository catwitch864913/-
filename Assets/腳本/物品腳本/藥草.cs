using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 藥草 : MonoBehaviour
{
    public GameObject target;
    bool isCollision = false;
    void Update()
    {
        if (isCollision == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                音效.撿();
                藥草數量.當前數量 += 1;
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            isCollision = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        isCollision = false;
    }
}
