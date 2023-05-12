using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 普攻 : MonoBehaviour
{
    public 移動 playerhp;
    // Start is called before the first frame update
    void Start()
    {
        playerhp = GameObject.Find("主角").GetComponent<移動>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerhp != null)
            {
                playerhp.DamegePlayer(怪物.damage);
            }
        }
    }
}
