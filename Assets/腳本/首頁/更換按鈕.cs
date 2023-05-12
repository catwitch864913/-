using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 更換按鈕 : MonoBehaviour
{
    public Sprite 更換的按鈕圖;
    public Sprite 原本的按鈕圖;
    public Image UI按鈕;
    // Start is called before the first frame update
    void Start()
    {
        UI按鈕 = gameObject.GetComponent<Image>();
        UI按鈕.sprite= 原本的按鈕圖;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MouseEnter()
    { 
            UI按鈕.sprite = 更換的按鈕圖;
            
    }
    public void MouseExit()
    {
        UI按鈕.sprite = 原本的按鈕圖;
        
    }
}
