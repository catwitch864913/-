using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class �P��Buff : MonoBehaviour
{
    [Header("�^��q")]
    public int HpRecovery;

    [Header("�ե�")]
    public Animator ani;

    [Header("UI����")]
    public Image ImmortalIcon;

    [Header("�P�ƯS��")]
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

                ����.move.ReturnBlood();
                Instantiate(ImmortalEffect, transform.position, Quaternion.Euler(0, 0, 0));
                Timer = 0f;
            }
            if (BuffTimer == 55f)
            {
                ani.SetTrigger("�P�Ƨ֮���");

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
