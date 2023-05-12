using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 首頁角色此幀轉向 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void 到此幀時轉向180()
    {
        transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
    public void 到此幀時轉向0()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
