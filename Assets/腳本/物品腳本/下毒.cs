using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 下毒 : MonoBehaviour
{
    public GameObject target;
    bool isCollision = false;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollision == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //音效.撿();
                //魔毒數量.當前數量 -= 1;
                animator.Play("下毒");
                草叢.鍋子下毒數 += 1;
                Debug.Log(草叢.鍋子下毒數);
                Debug.Log("下毒了");
                GetComponent<Collider2D>().enabled = false;
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
