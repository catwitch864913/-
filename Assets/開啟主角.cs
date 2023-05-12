using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 開啟主角 : MonoBehaviour
{
    public GameObject 隱藏對象;
    void 開主角()
    {

        隱藏對象.SetActive(true);
    }
}
