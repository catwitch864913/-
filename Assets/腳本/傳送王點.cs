using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 傳送王點 : MonoBehaviour
{
    bool 觸碰;
    // Start is called before the first frame update
    void Start()
    {
        觸碰 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (觸碰)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Application.LoadLevel("打王點");
            }
        }
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        觸碰 = true;
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        觸碰 = false;
    }
}
