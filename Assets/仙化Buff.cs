using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class 仙化Buff : MonoBehaviour
{
    [Header("回血量")]
    public int HpRecovery;

    [Header("組件")]
    public Animator ani;

    [Header("UI物件")]
    public Image ImmortalIcon;

    [Header("仙化特效")]
    public GameObject ImmortalEffect;

    public float Timer;
    public float BuffTimer;

    public bool BuffOpen;

    private void Start()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        TurnOnBuff();
    }
    void TurnOnBuff()
    {
        if (BuffOpen)
        {
            Timer += Time.deltaTime;
            BuffTimer += Time.deltaTime;
            if (Timer >= 5f)
            {

                移動.move.ReturnBlood();
                Instantiate(ImmortalEffect, transform.position, Quaternion.Euler(0, 0, 0));
                Timer = 0f;
            }
            if (BuffTimer == 55f)
            {
                ani.SetTrigger("仙化快消失");

            }
            else if (BuffTimer >= 60f)
            {
                FinishBuff();
                BuffTimer = 0f;
            }
        }
    }
    void FinishBuff()
    {
        BuffOpen = false;
    }
}
