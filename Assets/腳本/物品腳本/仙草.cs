using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 仙草 : MonoBehaviour
{
    public GameObject target;
    bool isCollision;
    // Start is called before the first frame update
    void Start()
    {
        isCollision = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isCollision==true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                音效.撿();
                仙草數量.當前仙草數量 += 1;
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
