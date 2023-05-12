using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 轉換場景物件不刪 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
