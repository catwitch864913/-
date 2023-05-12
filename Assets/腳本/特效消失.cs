using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 特效消失 : MonoBehaviour
{
    public float timeToDestroy=1f;
    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
