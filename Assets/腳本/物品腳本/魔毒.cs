using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 魔毒 : MonoBehaviour
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
        if (isCollision == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                音效.撿();
                魔毒數量.當前魔毒數量 += 1;
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
