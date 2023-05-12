using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 道具欄 : Windows<道具欄>
{
	protected override void Start()
	{
		base.Start();
		//因為道具欄會用到道具資料庫 為了避免沒資料可以用 保險的呼叫對方
		//StuffManager.instance.Load();
		//初始化的時候刷新一次道具欄
		//UpdataUI();
		//登記道具發生變化時刷新
		//PlayerinfoManager.instance.道具發生變化 += UpdataUI;
	}
	private void OnDisable()
	{
		//刪除時取消訂閱
		//PlayerinfoManager.instance.道具發生變化 -= UpdataUI;
	}
	protected override void Update()
	{
		base.Update();
		if (Input.GetKeyDown(KeyCode.B))
		{
			if (isOpen == true)
				Close();
			else
				Open();
		}
		Time.timeScale = 1f - Mathf.Clamp01(道具欄.ins.alpha + 暫停腳本.ins.alpha);
	}
	public override void Open()
	{
		base.Open();
		Cursor.lockState = CursorLockMode.None;
	}
	public override void Close()
	{
		base.Close();
		Cursor.lockState = CursorLockMode.Locked;
	}
}
