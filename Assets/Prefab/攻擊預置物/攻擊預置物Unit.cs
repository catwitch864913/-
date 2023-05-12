using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 攻擊預置物Unit : MonoBehaviour
{
    [Header("數值")]
    public float speed = 1f;
    public float lifeTime = 5f;
    public int damage = 30;

    [Header("玩家位置")]
    public Transform target;
}
