using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("組件")]
    public Animator ani;
    public Rigidbody2D rb;

    [Header("物件")]
    public GameObject 噴血特效;

    [Header("數值")]
    public float hp;
    public int damage;
}
