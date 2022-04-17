using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignRewardUI : BaseUI
{
	public static SignRewardUI action;

	public GameObject CloseBtn;

	public GameObject SinginBtn;

	public GameObject sigimgObjF;

	public GameObject sigimgObj;

	public GameObject HeadIcon;

	public Sprite[] SigImgL;

	public GameObject MaxObj;

	public GameObject MinObj;

	public Text MaxText;

	public Text MinText;

	public Sprite SinOkImg;

	public Text SignRewardUI0;

	public Text SignRewardUI1;

	private string sTime = string.Empty;

	private int iTimeClose;

	private int aabb;

	private int iAllDay = 1;

	private int iNowDay;

	private int i31;

	private List<Vector3> btnPos;

	private bool b4;

	private bool bshowWindows;

	private List<int> LRewardOk;

	private int iLastId;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.SignRewardUI;
	}

	private void Awake()
	{
		Canvas component = base.gameObject.transform.GetComponent<Canvas>();
		component.renderMode = RenderMode.ScreenSpaceCamera;
		component.worldCamera = MapUI.action.MapUISceneCamera.GetComponent<Camera>();
	}

	public override void OnStart()
	{
		action = this;
		if (!Util.CheckOnline())
		{
			InitGame.Action.GetTimeNetTime();
			CloseUI();
			return;
		}
		InitBuyGoldSonObj();
		string interNetTime = Util.getInterNetTime();
		if (interNetTime != Util.GetNowTime_Day())
		{
		}
		sTime = Util.GetNowTime_Day();
		SignOver();
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SignRewardDay31" + sTime) == 0)
		{
			CloseBtn.SetActive(value: false);
		}
		BaseUIAnimation.action.SetLanguageFont("SignRewardUI0", SignRewardUI0, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SignRewardUI1", SignRewardUI1, string.Empty);
	}

	public void SignOver()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SignRewardDay31" + sTime);
		if (@int == 1)
		{
			SinginBtn.GetComponent<Image>().sprite = SinOkImg;
		}
	}

	public int getnowDay()
	{
		string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_FistLoginSignRewardDay", string.Empty);
		DateTime now = DateTime.Now;
		DateTime d = DateTime.Parse(@string + " 00:00:00");
		return (now - d).Days;
	}

	private IEnumerator autoTime()
	{
		while (true)
		{
			if (iTimeClose <= 0)
			{
				MaxObj.SetActive(value: false);
				MinObj.SetActive(value: false);
			}
			iTimeClose--;
			yield return new WaitForSeconds(1f);
		}
	}

	private void InitBuyGoldSonObj()
	{
		btnPos = new List<Vector3>();
		LRewardOk = new List<int>();
		iNowDay = getnowDay();
		iNowDay++;
		for (iAllDay = iNowDay; iNowDay > 31; iNowDay -= 31)
		{
			i31++;
		}
		while (iAllDay > 93)
		{
			i31 = 0;
			iAllDay -= 93;
		}
		for (int i = 1; i <= 31; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(sigimgObj);
			gameObject.transform.SetParent(sigimgObjF.transform, worldPositionStays: false);
			int num = int.Parse(Singleton<DataManager>.Instance.dDataSignmap31[i.ToString()]["x"]);
			int num2 = int.Parse(Singleton<DataManager>.Instance.dDataSignmap31[i.ToString()]["y"]);
			gameObject.transform.localPosition = new Vector3(num, 1024 - num2, 0f);
			if (i == 6)
			{
				gameObject.GetComponent<Image>().sprite = SigImgL[0];
				gameObject.GetComponent<Image>().SetNativeSize();
			}
			if (i == 14)
			{
				gameObject.GetComponent<Image>().sprite = SigImgL[1];
				gameObject.GetComponent<Image>().SetNativeSize();
			}
			if (i == 27)
			{
				gameObject.GetComponent<Image>().sprite = SigImgL[2];
				gameObject.GetComponent<Image>().SetNativeSize();
			}
			gameObject.SetActive(value: true);
			if (i == iNowDay)
			{
				HeadIcon.transform.SetParent(gameObject.transform, worldPositionStays: false);
				HeadIcon.transform.localPosition = new Vector3(0f, 88f, 0f);
			}
			sigimg component = gameObject.GetComponent<sigimg>();
			component.SetId(i);
			if (i > iNowDay)
			{
				component.SetNull();
			}
			else if (i < iNowDay)
			{
				string str = DateTime.Now.AddDays(-iNowDay + i).ToString("yyyyMMdd");
				string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_Sign31" + str, string.Empty);
				if (@string == string.Empty)
				{
					component.SetNull();
				}
				else
				{
					LRewardOk.Add(i);
				}
			}
			btnPos.Add(gameObject.transform.localPosition);
		}
	}

	public void SigninBtn()
	{
		if (!Util.CheckOnline())
		{
			InitGame.Action.GetTimeNetTime();
			CloseUI();
			return;
		}
		//AdManager.action.opadshowcp(DataManager.PAGE_MAIN);
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SignRewardDay31" + sTime);
		if (@int == 1)
		{
			CloseUI();
			return;
		}
		SignOver();
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_SignRewardDay31" + sTime, 1);
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		if (iAllDay == 6 || iAllDay == 14 || iAllDay == 27 || iAllDay == 37 || iAllDay == 45 || iAllDay == 58 || iAllDay == 68 || iAllDay == 76 || iAllDay == 89)
		{
			b4 = true;
			for (int i = 1; i <= 4; i++)
			{
				int num = int.Parse(Singleton<DataManager>.Instance.dDataSignmap31Reward[iAllDay.ToString()]["reward" + i]);
				int num2 = int.Parse(Singleton<DataManager>.Instance.dDataSignmap31Reward[iAllDay.ToString()]["count" + i]);
				list.Add(num);
				list2.Add(num2);
				ChinaPay.action.addRewardAll(num, num2, action.gameObject, isShow: false, "free", "signreward");
			}
		}
		else
		{
			int num3 = int.Parse(Singleton<DataManager>.Instance.dDataSignmap31Reward[iAllDay.ToString()]["reward1"]);
			int num4 = int.Parse(Singleton<DataManager>.Instance.dDataSignmap31Reward[iAllDay.ToString()]["count1"]);
			list.Add(num3);
			list2.Add(num4);
			ChinaPay.action.addRewardAll(num3, num4, action.gameObject, isShow: false, "free", "signreward");
		}
		BaseUIAnimation.action.ShowProp(list, list2, action.gameObject);
		string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_Sign31" + sTime, string.Empty);
		if (@string == string.Empty)
		{
		}
		Singleton<TestScript>.Instance.SetString(DataManager.SDBNO + "DB_Sign31" + sTime, "1");
		StartCoroutine(IESIGClose());
	}

	public IEnumerator IESIGClose()
	{
		yield return new WaitForSeconds(1.8f);
		if (b4)
		{
			yield return new WaitForSeconds(1f);
		}
		CloseUI();
	}

	public void SIGClose()
	{
		CloseUI();
	}

	public void CloseSigninUI(bool bClickClose = true)
	{
		if (bClickClose)
		{
			if (!BaseUIAnimation.bClickButton)
			{
				return;
			}
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
		}
		StartCoroutine(CallCloseUI());
	}

	private IEnumerator CallCloseUI(bool b = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseUI(b);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			bshowWindows = false;
		}
		if (Input.GetMouseButtonUp(0) && !bshowWindows)
		{
			MaxObj.SetActive(value: false);
			MinObj.SetActive(value: false);
		}
	}

	protected override void OnAwake()
	{
		base.OnAwake();
	}

	protected override void OnRelease()
	{
		base.OnRelease();
	}

	public void ClickSignShowBox(int index)
	{
		if (!Util.CheckOnline())
		{
			InitGame.Action.GetTimeNetTime();
			return;
		}
		bshowWindows = true;
		if (iLastId != 0)
		{
			if (iLastId == index)
			{
				iLastId = 0;
				bshowWindows = false;
			}
			else
			{
				iLastId = index;
			}
		}
		else
		{
			iLastId = index;
		}
		UnityEngine.Debug.Log("ClickSignShowBox   =  3");
		iTimeClose = 3;
		if (index == 6 || index == 14 || index == 27)
		{
			for (int i = 1; i <= 4; i++)
			{
				int num = int.Parse(Singleton<DataManager>.Instance.dDataSignmap31Reward[(index + i31 * 31).ToString()]["reward" + i]);
				int num2 = int.Parse(Singleton<DataManager>.Instance.dDataSignmap31Reward[(index + i31 * 31).ToString()]["count" + i]);
				MaxObj.transform.Find("icon" + i).gameObject.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num, 138, 114);
				MaxObj.transform.Find("text" + i).gameObject.GetComponent<Text>().text = "x" + num2;
				MaxObj.SetActive(value: true);
				MinObj.SetActive(value: false);
			}
			MaxObj.transform.localPosition = btnPos[index - 1];
			if (index >= 27)
			{
				MaxObj.transform.localPosition -= new Vector3(0f, 220f, 0f);
			}
			else
			{
				MaxObj.transform.localPosition += new Vector3(0f, 210f, 0f);
			}
		}
		else
		{
			int num3 = int.Parse(Singleton<DataManager>.Instance.dDataSignmap31Reward[(index + i31 * 31).ToString()]["reward1"]);
			int num4 = int.Parse(Singleton<DataManager>.Instance.dDataSignmap31Reward[(index + i31 * 31).ToString()]["count1"]);
			MinObj.transform.Find("icon1").gameObject.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num3, 138, 114);
			MinObj.transform.Find("text1").gameObject.GetComponent<Text>().text = "x" + num4;
			MinObj.SetActive(value: true);
			MaxObj.SetActive(value: false);
			MinObj.transform.localPosition = btnPos[index - 1];
			if (index >= 27)
			{
				MinObj.transform.localPosition -= new Vector3(0f, 100f, 0f);
			}
			else
			{
				MinObj.transform.localPosition += new Vector3(0f, 100f, 0f);
			}
		}
		SetShowText(index);
	}

	public void SetShowText(int iClickDay)
	{
		int num = 3;
		string text = "已领取了";
		if (iClickDay == iNowDay)
		{
			text = "今日可领";
			num = 4;
		}
		else if (iClickDay - 1 == iNowDay)
		{
			text = "明天可领";
			num = 2;
		}
		else if (iClickDay - 2 == iNowDay)
		{
			text = "后天可领";
			num = 5;
		}
		else
		{
			if (iClickDay > iNowDay)
			{
				text = iClickDay - iNowDay + "天后可领";
				text = Singleton<DataManager>.Instance.dDataLanguage["SignRewardUI7"][BaseUIAnimation.Language];
				text = text.Replace("A1", (iClickDay - iNowDay).ToString());
				MaxText.text = text;
				MinText.text = text;
				return;
			}
			text = "已过期了";
			num = 5;
			for (int i = 0; i < LRewardOk.Count; i++)
			{
				if (iClickDay == LRewardOk[i])
				{
					text = "已领取了";
					num = 3;
				}
			}
		}
		text = Singleton<DataManager>.Instance.dDataLanguage["SignRewardUI" + num][BaseUIAnimation.Language];
		MaxText.text = text;
		MinText.text = text;
	}
}
