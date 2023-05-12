using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 仙球雨 : MonoBehaviour
{
    [Header("雲朵預置物")]
    public GameObject attackGameObject;
    [Header("仙球雨預置物")]
    public GameObject fireball;
    [Header("玩家的位置")]
    public Transform attackPosition;

    private void Awake()
    {
        attackPosition = GameObject.Find("主角").transform;
    }
    void Start()
    {
        //出來後抓取到玩家當下的位置並移動到他頭頂
        Vector3 targetPosition = new Vector3(attackPosition.position.x, attackPosition.position.y + 10f, attackPosition.position.z);
        //多少時間到達目標位置
        float timeToReachTarget = 2f;
        StartCoroutine(MoveToPosition(attackGameObject.transform, targetPosition, timeToReachTarget));
    }

    IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToReachTarget)
    {
        Vector3 currentPos = transform.position;
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToReachTarget;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
        //當雲朵到達定點後
        if (t >= 1)
        {
            Coroutine a = StartCoroutine(下仙球雨());
        }
    }

    IEnumerator 下仙球雨()
    {
        float 下雨時間 = 0f;
        float 仙球出來時間 = 0f;
        while (下雨時間 < 5)
        {
            下雨時間 += Time.deltaTime;
            仙球出來時間 += Time.deltaTime;
            if (仙球出來時間 > 0.5f)
            {
                Instantiate(fireball, transform.position, Quaternion.identity);
                仙球出來時間 = 0;
            }
            if (下雨時間 >= 5f)
            {
                Destroy(gameObject);
            }
            yield return null;
        }
    }
}
