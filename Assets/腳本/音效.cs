using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 音效 : MonoBehaviour
{
    public static AudioSource audiosource;
    public static AudioClip 跳;
    public static AudioClip 射出;
    public static AudioClip 命中人;
    public static AudioClip 命中地板;
    public static AudioClip 撿起;
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        跳 = Resources.Load<AudioClip>("跳");
        射出 = Resources.Load<AudioClip>("箭矢射出");
        命中人 = Resources.Load<AudioClip>("命中人");
        命中地板 = Resources.Load<AudioClip>("命中地板");
        撿起 = Resources.Load<AudioClip>("撿拾");
    }
    public static void 跳起()
    {
        audiosource.PlayOneShot(跳);
    }
    public static void 射出箭()
    {
        audiosource.PlayOneShot(射出);
    }
    public static void 箭命中人()
    {
        audiosource.PlayOneShot(命中人);
    }
    public static void 箭命中地板()
    {
        audiosource.PlayOneShot(命中地板);
    }
    public static void 撿()
    {
        audiosource.PlayOneShot(撿起);
    }
}
