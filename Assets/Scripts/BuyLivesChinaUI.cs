using System;
using System.Collections;
using EasyMobile;
using ITSoft;
using UnityEngine;
using UnityEngine.UI;

public class BuyLivesChinaUI : BaseUI
{
	public static BuyLivesChinaUI action;

	public GameObject ChinaLive_Obj;

	public GameObject ChinaLive_Father;

	public GameObject CloseBtn;

	public Text Time;

	public GameObject TimeRemark;

	public Text LoveText;

	public Text LoveFullText;

	public Text BuyLivesChinaUITitle;

	public Text NextLoveText;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.BuyLivesChinaUI;
	}

	private void Start()
	{
        AdsManager.ShowBanner();
        action = this;
		DataManager.bbeibaoFlay = false;
		Singleton<DataManager>.Instance.bexitGameScene = false;
        BaseUIAnimation.action.SetLanguageFont("BuyLivesChinaUITitle", BuyLivesChinaUITitle, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BuyLivesChinaUI1", LoveFullText, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BuyLivesChinaUI2", NextLoveText, string.Empty);
		for (int i = 1; i <= 3; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(ChinaLive_Obj);
			gameObject.transform.SetParent(ChinaLive_Father.transform, worldPositionStays: false);
			ChinaLiveObj component = gameObject.GetComponent<ChinaLiveObj>();
			component.InitData(i);
		}
		StartCoroutine(UpdateTime());
	}

	private IEnumerator UpdateTime()
	{
		bool b = true;
		while (b)
		{
			int iStar = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
			LoveText.text = iStar.ToString();
			if (iStar >= 5)
			{
				Time.gameObject.SetActive(value: false);
				LoveFullText.gameObject.SetActive(value: true);
				TimeRemark.gameObject.SetActive(value: false);
			}
			else
			{
				Time.gameObject.SetActive(value: true);
				LoveFullText.gameObject.SetActive(value: false);
				TimeRemark.gameObject.SetActive(value: true);
				int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_FullLoveTime");
				int nowTime = Util.GetNowTime();
				if (nowTime > @int)
				{
					Singleton<DataManager>.Instance.SaveUserDate("DB_LoveCount", Singleton<DataManager>.Instance.iLoveMaxAll);
					Singleton<DataManager>.Instance.SaveUserDate("DB_FullLoveTime", 0);
				}
				int num = @int - nowTime;
				int num2 = 0;
				while (num > Singleton<LevelManager>.Instance.ResTime)
				{
					num2++;
					num -= Singleton<LevelManager>.Instance.ResTime;
				}
				Singleton<DataManager>.Instance.SaveUserDate("DB_LoveCount", Singleton<DataManager>.Instance.iLoveMaxAll - num2 - 1);
				TimeSpan timeSpan = new TimeSpan(0, 0, num);
				int minutes = timeSpan.Minutes;
				int seconds = timeSpan.Seconds;
				string text = minutes + string.Empty;
				string text2 = seconds + string.Empty;
				if (minutes < 10)
				{
					text = "0" + text;
				}
				if (seconds < 10)
				{
					text2 = "0" + text2;
				}
				Time.text = text + ":" + text2;
			}
			yield return new WaitForSeconds(1f);
		}
	}

	public void _CloseBuyLivesChinaUI()
	{
		if (!DataManager.bbeibaoFlay && BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
			StartCoroutine(CallCloseUI());
		}
	}

	private IEnumerator CallCloseUI(bool bDouble = false, bool bExit = true)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		Singleton<DataManager>.Instance.bexitGameScene = true;
		CloseUI(bDouble);
	}

	private void Awake()
	{
		if (!GameUI.action && (bool)MapUI.action)
		{
			Canvas component = base.gameObject.transform.GetComponent<Canvas>();
			component.renderMode = RenderMode.ScreenSpaceCamera;
			component.worldCamera = MapUI.action.MapUISceneCamera.GetComponent<Camera>();
		}
		if ((bool)HuaGame.action)
		{
			Canvas component2 = base.gameObject.transform.GetComponent<Canvas>();
			component2.renderMode = RenderMode.ScreenSpaceCamera;
			component2.worldCamera = HuaGame.action.HuaCamera.GetComponent<Camera>();
		}
	}

	private void Update()
	{
	}
}
