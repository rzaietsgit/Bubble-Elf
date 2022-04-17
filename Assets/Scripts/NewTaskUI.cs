using System;
using System.Collections;
using EasyMobile;
using ITSoft;
using UnityEngine;
using UnityEngine.UI;

public class NewTaskUI : BaseUI
{
	public static NewTaskUI action;

	public GameObject CloseBtn;

	public GameObject TaskGameObj;

	public GameObject GroupObj;

	public Text TimeText;

	public Text TopRemarkText;

	public bool bbtnGo = true;

	public int iTaskCenter;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.NewTaskUI;
	}

	private void Start()
	{
		action = this;
		bbtnGo = true;
        AdsManager.ShowBanner();
        Singleton<DataManager>.Instance.bNewTaskOpenPlay = false;
		int num = 0;
		num = Singleton<DataManager>.Instance.dDataNewTaskList.Count;
		for (int i = 1; i <= num; i++)
		{
			string remark = Singleton<DataManager>.Instance.dDataNewTaskList[i.ToString()]["remark"];
			int levelID = int.Parse(Singleton<DataManager>.Instance.dDataNewTaskList[i.ToString()]["Level"]);
			GameObject gameObject = UnityEngine.Object.Instantiate(TaskGameObj);
			gameObject.transform.SetParent(GroupObj.transform, worldPositionStays: false);
			gameObject.SetActive(value: true);
			NewTaskobj component = gameObject.GetComponent<NewTaskobj>();
			component.InitData(i, remark, levelID);
		}
		if (num > 4)
		{
			RectTransform component2 = GroupObj.transform.GetComponent<RectTransform>();
			component2.sizeDelta = new Vector2(580f, num * 142);
			int num2 = (num * 142 - 568) / 2 * -1;
			RectTransform rectTransform = component2;
			Vector3 localPosition = component2.localPosition;
			float x = localPosition.x;
			float y = num2;
			Vector3 localPosition2 = component2.localPosition;
			rectTransform.localPosition = new Vector3(x, y, localPosition2.z);
			if (iTaskCenter > 3)
			{
				num2 += (iTaskCenter - 2) * 142;
				RectTransform rectTransform2 = component2;
				Vector3 localPosition3 = component2.localPosition;
				float x2 = localPosition3.x;
				float y2 = num2;
				Vector3 localPosition4 = component2.localPosition;
				rectTransform2.localPosition = new Vector3(x2, y2, localPosition4.z);
			}
		}
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_InitFistLoginGameDay");
		int nowTime = Util.GetNowTime();
		nowTime -= @int;
		if (nowTime <= 259200)
		{
			nowTime = 259200 - nowTime;
			Settime(nowTime);
			StartCoroutine(IESetTIme(nowTime));
		}
		else
		{
			TimeText.text = string.Empty;
			TopRemarkText.text = "加油吧少年~";
		}
	}

	public void Settime(int iTime)
	{
		TimeSpan timeSpan = new TimeSpan(0, 0, iTime);
		int minutes = timeSpan.Minutes;
		int num = timeSpan.Hours;
		int seconds = timeSpan.Seconds;
		int days = timeSpan.Days;
		if (days > 0)
		{
			num = days * 24 + num;
		}
		string text = minutes + string.Empty;
		string text2 = num + string.Empty;
		string text3 = seconds + string.Empty;
		if (minutes < 10)
		{
			text = "0" + text;
		}
		if (num < 10)
		{
			text2 = "0" + text2;
		}
		if (seconds < 10)
		{
			text3 = "0" + text3;
		}
		string text4 = text2 + ":" + text + ":" + text3;
		string text5 = string.Empty;
		for (int i = 0; i < text4.Length; i++)
		{
			text5 = text5 + text4.Substring(i, 1) + " ";
		}
		TimeText.text = text5;
	}

	private IEnumerator IESetTIme(int iTime)
	{
		yield return new WaitForSeconds(1f);
		while (iTime > 1)
		{
			iTime--;
			Settime(iTime);
			yield return new WaitForSeconds(1f);
		}
		TimeText.text = string.Empty;
		TopRemarkText.text = "继 续 通 关 吧 骚 年 ， 奖 励 拿 到 手 软 哦 ~";
	}

	private void Awake()
	{
		Canvas component = base.gameObject.transform.GetComponent<Canvas>();
		component.renderMode = RenderMode.ScreenSpaceCamera;
		component.worldCamera = MapUI.action.MapUISceneCamera.GetComponent<Camera>();
	}

	public void _CloseNewTaskUI()
	{
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
			StartCoroutine(CallCloseUI());
		}
	}

	private IEnumerator CallCloseUI(bool bDouble = false, bool bExit = true)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseUI(bDouble);
	}

	private void Update()
	{
	}
}
