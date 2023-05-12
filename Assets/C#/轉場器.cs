using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 轉場器 : SingletonMonoBehaviour<轉場器>
{
	bool 由名稱轉場或執行事件 = false; // false = 名稱轉場 true = 執行事件

	[SerializeField] Animator ani = null;
	string SceneName = "";
	public void 轉場(string name)
	{
		SceneName = name;
		ani.SetTrigger("離場");
		由名稱轉場或執行事件 = false;
	}
	void 切換關卡()
	{
		SceneManager.LoadScene(SceneName);
	}
	System.Action 畫面變黑之後要做的事情;
	public void 轉場(System.Action 畫面變黑之後要做的事情)
	{
		this.畫面變黑之後要做的事情 = 畫面變黑之後要做的事情;
		ani.SetTrigger("離場");
		由名稱轉場或執行事件 = true;
	}
	void 變黑做事()
	{
		//做他要做的事情
		if (畫面變黑之後要做的事情 != null)
			畫面變黑之後要做的事情.Invoke();
	}
	public void 動畫事件()
	{
		if (由名稱轉場或執行事件 == false)
		{
			切換關卡();
		}
		else
		{
			變黑做事();
		}

	}
}
