using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 血球 : MonoBehaviour
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
                Destroy(gameObject);
                移動.當前血量 += 5;
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
