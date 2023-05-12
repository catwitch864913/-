using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 射箭腳本 : MonoBehaviour
{
    [Header("組件")]
    public GameObject projectile;
    public GameObject DemonizeEffects;

    [Header("元件")]
    public Transform shotPoint, shotPoint2;

    [Header("數值")]
    public float timeBtwShots;
    public float startTimeBtwShots;
    public float closeDemonizeTime;
    public float DemonizeEffectsTime;

    public bool OpenDemonize;
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        if (OpenDemonize)
        {
            closeDemonizeTime += Time.deltaTime;
            DemonizeEffectsTime += Time.deltaTime;
            if (closeDemonizeTime > 5f)
            {
                OpenDemonize = false;
                closeDemonizeTime = 0f;
            }
            if (DemonizeEffectsTime > 0.5f)
            {
                Instantiate(DemonizeEffects, transform.position, Quaternion.Euler(0, 0, 0));
                DemonizeEffectsTime = 0f;
            }

            if (timeBtwShots <= 0.1f)
            {
                if (Input.GetMouseButton(0))
                {
                    //  GameObject se= Instantiate(shotEffect, shotPoint.position, Quaternion.identity)as GameObject;
                    //  Destroy(se, 0.5f);
                    Instantiate(projectile, shotPoint.position, transform.rotation);
                    Instantiate(projectile, shotPoint2.position, transform.rotation);
                    timeBtwShots = 0.5f;
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
        else
        {
            if (timeBtwShots <= 0.1f)
            {
                if (Input.GetMouseButton(0))
                {
                    //  GameObject se= Instantiate(shotEffect, shotPoint.position, Quaternion.identity)as GameObject;
                    //  Destroy(se, 0.5f);
                    Instantiate(projectile, shotPoint.position, transform.rotation);
                    timeBtwShots = startTimeBtwShots;
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }

        }

    }
}
