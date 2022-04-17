using System;
using System.Collections;
using EasyMobile;
using ITSoft;
using UnityEngine;
using UnityEngine.UI;

public class Reward24UI : BaseUI
{
	public static Reward24UI action;

	public Text Reward24UITime;

	public GameObject CloseBtn;

	public GameObject TimeObj;

	public GameObject ClickBtn;

	public Sprite[] btnIcon;

	public Text Reward24UIBtn;

	public Text Reward24UITitle;

	public Text Reward24UI1;

	public Text Reward24UI2;

	private bool bGet;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.Reward24UI;
	}

	private void Start()
	{
		action = this;
		ClickBtn.GetComponent<Button>().enabled = false;
		ClickBtn.GetComponent<Image>().sprite = btnIcon[0];
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_24hReward") == 0)
		{
			AutoTIme();
		}
        AdsManager.ShowBanner();

        BaseUIAnimation.action.SetLanguageFont("Reward24UIBtn", Reward24UIBtn, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("Reward24UITitle", Reward24UITitle, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("Reward24UI1", Reward24UI1, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("Reward24UI2", Reward24UI2, string.Empty);
	}

	private void AutoTIme()
	{
		int num = 86400;
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_24hRewardTime");
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_24hReward") == 0)
		{
			int nowTime = Util.GetNowTime();
			TimeObj.SetActive(value: false);
			bGet = true;
			ClickBtn.GetComponent<Button>().enabled = true;
			ClickBtn.GetComponent<Image>().sprite = btnIcon[1];
		}
		else
		{
			bGet = false;
			ClickBtn.GetComponent<Button>().enabled = false;
			ClickBtn.GetComponent<Image>().sprite = btnIcon[0];
		}
	}

	private IEnumerator IESetTime(int iTime)
	{
		yield return new WaitForSeconds(1f);
		bool b = true;
		while (b)
		{
			if (iTime < 0)
			{
				b = false;
				AutoTIme();
			}
			else
			{
				iTime--;
			}
			_SetTime(iTime);
			yield return new WaitForSeconds(1f);
		}
	}

	private void _SetTime(int iTime)
	{
		if (iTime < 0)
		{
			TimeObj.SetActive(value: false);
			return;
		}
		if (iTime < 1)
		{
			iTime = 1;
		}
		TimeSpan timeSpan = new TimeSpan(0, 0, iTime);
		int minutes = timeSpan.Minutes;
		int hours = timeSpan.Hours;
		int seconds = timeSpan.Seconds;
		string text = minutes + string.Empty;
		string text2 = hours + string.Empty;
		string text3 = seconds + string.Empty;
		if (minutes < 10)
		{
			text = "0" + text;
		}
		if (hours < 10)
		{
			text2 = "0" + text2;
		}
		if (seconds < 10)
		{
			text3 = "0" + text3;
		}
		Reward24UITime.text = text2 + ":" + text + ":" + text3;
	}

	private void Awake()
	{
		Canvas component = base.gameObject.transform.GetComponent<Canvas>();
		component.renderMode = RenderMode.ScreenSpaceCamera;
		component.worldCamera = MapUI.action.MapUISceneCamera.GetComponent<Camera>();
	}

	public void _CloseReward24UI()
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

	public void ClickLinquBtn()
	{
		if (bGet && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_24hReward") == 0)
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_24hReward", 1);
			Reward();
		}
	}

	private void Reward()
	{
		ChinaPay.action.CallReward24();
		if ((bool)ClickBtnFun._24Obj)
		{
			ClickBtnFun._24Obj.NextLb24();
		}
		//AdManager.action.opadshowcp(DataManager.PAGE_MAIN);
		ClickBtn.GetComponent<Button>().enabled = false;
		ClickBtn.GetComponent<Image>().sprite = btnIcon[0];
	}
}
