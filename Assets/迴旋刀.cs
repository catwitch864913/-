using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 迴旋刀 : 攻擊預置物Unit
{
	Vector3 AAA;
	public Vector3 dir;
	private void Start()
	{
		target = GameObject.Find("主角").transform.GetChild(3);
		AAA = target.position - transform.position;
		Destroy(this.gameObject, lifeTime);
		Vector3 relative = transform.InverseTransformPoint(target.position);
		float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
		transform.eulerAngles = new Vector3(0, 0, angle);
	}
	private void Update()
	{
		transform.position += AAA * speed * Time.deltaTime;
		/*if (target != null)
		{
			//Vector3 dir = (target.position - this.transform.position).normalized;
		    dir = (target.position - transform.position).normalized;
			if (dir.magnitude < 0.3f)
			{
				target.GetComponent<移動>().DamegePlayer(this.damage);

			}

			transform.rotation = Quaternion.FromToRotation(Vector3.left, dir);
			transform.position += dir * speed * Time.deltaTime;
		}
		print(dir);
		print(target);*/
	}
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			collision.GetComponent<移動>().DamegePlayer(damage);
			Destroy(gameObject);
		}
		if (collision.gameObject.CompareTag("Ground"))
		{
			Destroy(gameObject);
		}
	}
}
