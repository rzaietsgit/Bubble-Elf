using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MapHuaTopUI : MonoBehaviour
{
	public static MapHuaTopUI action;

	public Text HuaBiText;

	public GameObject LoveInfiniteTimeObj;

	public GameObject LoveTimeObj;

	public GameObject TimeFull;

	public Text TimeText;

	public Text LoveText;

	public GameObject AddLoveBtn;

	public GameObject TopCenter;

	public GameObject TopLeft;

	private int iRtime;

	private void Start()
	{
		action = this;
		InitHuaBi();
		if (Singleton<DataManager>.Instance.bLiuhai)
		{
			TopCenter.transform.localPosition += new Vector3(50f, 0f, 0f);
			TopLeft.transform.localPosition -= new Vector3(60f, 0f, 0f);
		}
		if (Singleton<UserManager>.Instance.getLoveInfinite() <= 0)
		{
			LoveTimeObj.SetActive(value: true);
			LoadLove();
			CalcTime();
			LoadTime();
			AddLoveBtn.gameObject.SetActive(value: true);
			StartCoroutine(UpdateViewLove());
		}
		else
		{
			LoveInfiniteTimeObj.SetActive(value: true);
			LoveTimeObj.SetActive(value: false);
			LoadLoveInfiniteTime();
			StartCoroutine(UpdateLoveInfinite());
		}
	}

	public void LoadLove()
	{
		if (!Singleton<DataManager>.Instance.StarGameFlage)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
			LoveText.text = @int.ToString();
		}
	}

	public void _IEUpdateLoveInfinite()
	{
		StartCoroutine(UpdateLoveInfinite());
	}

	private IEnumerator UpdateLoveInfinite()
	{
		bool b = true;
		while (b)
		{
			yield return new WaitForSeconds(1f);
			LoadLoveInfiniteTime();
			LoveInfiniteTimeObj.SetActive(value: true);
			LoveTimeObj.SetActive(value: false);
			int _iLoveTime = Singleton<UserManager>.Instance.getLoveInfinite();
			if (_iLoveTime <= 0)
			{
				b = false;
				StartCoroutine(UpdateViewLove());
			}
		}
	}

	public void LoadLoveInfiniteTime()
	{
		int loveInfinite = Singleton<UserManager>.Instance.getLoveInfinite();
		TimeSpan timeSpan = new TimeSpan(0, 0, loveInfinite);
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
		LoveInfiniteTimeObj.transform.Find("Time").GetComponent<Text>().text = text2 + ":" + text + ":" + text3;
	}

	private IEnumerator UpdateViewLove()
	{
		bool b = true;
		while (b)
		{
			yield return new WaitForSeconds(1f);
			CalcTime();
			LoadTime();
			LoadLove();
			LoveInfiniteTimeObj.SetActive(value: false);
			LoveTimeObj.SetActive(value: true);
			int _iLoveTime = Singleton<UserManager>.Instance.getLoveInfinite();
			if (_iLoveTime > 0)
			{
				b = false;
				StartCoroutine(UpdateLoveInfinite());
			}
		}
	}

	private void CalcTime()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
		if (@int >= Singleton<DataManager>.Instance.iLoveMaxAll)
		{
			iRtime = 0;
			Singleton<DataManager>.Instance.SaveUserDate("DB_FullLoveTime", 0);
			return;
		}
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_FullLoveTime");
		int nowTime = Util.GetNowTime();
		if (nowTime > int2)
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_LoveCount", Singleton<DataManager>.Instance.iLoveMaxAll);
			Singleton<DataManager>.Instance.SaveUserDate("DB_FullLoveTime", 0);
			return;
		}
		iRtime = int2 - nowTime;
		int num = 0;
		while (iRtime > Singleton<LevelManager>.Instance.ResTime)
		{
			num++;
			iRtime -= Singleton<LevelManager>.Instance.ResTime;
		}
		Singleton<DataManager>.Instance.SaveUserDate("DB_LoveCount", Singleton<DataManager>.Instance.iLoveMaxAll - num - 1);
	}

	public void LoadTime()
	{
		if (iRtime <= 0)
		{
			TimeFull.gameObject.SetActive(value: true);
			TimeText.gameObject.SetActive(value: false);
			return;
		}
		TimeFull.gameObject.SetActive(value: false);
		TimeText.gameObject.SetActive(value: true);
		int seconds = iRtime;
		TimeSpan timeSpan = new TimeSpan(0, 0, seconds);
		int minutes = timeSpan.Minutes;
		int hours = timeSpan.Hours;
		int seconds2 = timeSpan.Seconds;
		int days = timeSpan.Days;
		string text = minutes + string.Empty;
		string str = hours + string.Empty;
		string text2 = seconds2 + string.Empty;
		if (minutes < 10)
		{
			text = "0" + text;
		}
		if (hours < 10)
		{
			str = "0" + str;
		}
		if (seconds2 < 10)
		{
			text2 = "0" + text2;
		}
		TimeText.text = text.ToString() + ":" + text2;
	}

	public void InitHuaBi()
	{
		HuaBiText.text = Singleton<UserManager>.Instance.GetHuaBi().ToString();
	}

	private void Update()
	{
	}

	public void ClickBuyLove()
	{
		Singleton<UIManager>.Instance.OpenUI(EnumUIType.BuyLivesChinaUI);
	}

	public void ClickBuyHua()
	{
		Singleton<UIManager>.Instance.OpenUI(EnumUIType.HuaShopUI);
	}
}
