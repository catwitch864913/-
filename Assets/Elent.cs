using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elent : MonoBehaviour
{
    public float speed;
    public Vector3 direction = Vector3.zero;
    public Side side;
    public int Power = 20;
    public int lifeTime;

    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }
    void Update()
    {
        this.transform.position += speed * Time.deltaTime * direction;
        if (Screen.safeArea.Contains(Camera.main.WorldToScreenPoint(this.transform.position)) == false)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.GetComponent<²¾°Ê>().DamegePlayer(Power);
            Destroy(gameObject);
        }
    }
}
