using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClickBtnFun : MonoBehaviour
{
	public BtnType EnumUITypeType;

	public Image BtnImg;

	public Sprite Sp;

	public static ClickBtnFun _24Obj;

	public static ClickBtnFun _HaohuaObj;

	public static ClickBtnFun HuaBtnObj;

	public static ClickBtnFun AdUIObj;

	public Sprite[] ChangeSp;

	public Sprite[] ChangeText;

	public Text HuaTimeText;

	private GameObject ShipingObjIcon;

	public void ResAdUI()
	{
	}

	public void SetAdCounts()
	{
	}

	public void ResAdUIClose()
	{
		UnityEngine.Object.Destroy(base.transform.parent.gameObject);
	}

	public void ResShipingjiangli()
	{
		StartCoroutine(IEResShipingjiangli());
	}

	private IEnumerator IEResShipingjiangli()
	{
		yield return new WaitForSeconds(0.01f);
		while (true)
		{
			int iVideoTime = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_VideoNextTime");
			int itime = Util.GetNowTime();
			if (itime <= iVideoTime)
			{
				SetTextTime(iVideoTime - itime);
			}
			else
			{
				Text component = base.transform.Find("Text").gameObject.GetComponent<Text>();
				BaseUIAnimation.action.SetLanguageFont("IconName1", component, string.Empty);
			}
			yield return new WaitForSeconds(1f);
		}
	}

	public void SetTextTime(int itime)
	{
		TimeSpan timeSpan = new TimeSpan(0, 0, itime);
		int minutes = timeSpan.Minutes;
		int num = timeSpan.Hours;
		int seconds = timeSpan.Seconds;
		int days = timeSpan.Days;
		if (days > 0)
		{
			num = days * 24 + num;
		}
		string text = minutes + string.Empty;
		string str = num + string.Empty;
		string text2 = seconds + string.Empty;
		if (minutes < 10)
		{
			text = "0" + text;
		}
		if (num < 10)
		{
			str = "0" + str;
		}
		if (seconds < 10)
		{
			text2 = "0" + text2;
		}
		Text component = base.transform.Find("Text").gameObject.GetComponent<Text>();
		component.text = text + ":" + text2;
	}

	public void res24()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_24hReward");
		if (@int == 1 && EnumUITypeType == BtnType.Time24libao)
		{
			UnityEngine.Object.Destroy(base.transform.parent.gameObject);
		}
	}

	private void Start()
	{
		if (BtnImg != null)
		{
			BtnImg.gameObject.SetActive(value: false);
		}
		if (EnumUITypeType == BtnType.Time24libao)
		{
			int num = 86400;
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_24hRewardTime");
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_24hReward") == 0)
			{
				int nowTime = Util.GetNowTime();
				if (nowTime - @int > num && BtnImg != null)
				{
					BtnImg.gameObject.SetActive(value: true);
				}
			}
			_24Obj = this;
			res24();
		}
		if (EnumUITypeType == BtnType.shipingjiangli)
		{
			Text component = base.transform.Find("Text").gameObject.GetComponent<Text>();
			BaseUIAnimation.action.SetLanguageFont("IconName1", component, string.Empty);
			ResShipingjiangli();
			return;
		}
		if (EnumUITypeType == BtnType.qiandao)
		{
			int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iqiandao7Count", 1);
			if (int2 >= 7)
			{
				UnityEngine.Object.Destroy(base.transform.parent.gameObject);
			}
		}
		if (EnumUITypeType == BtnType.qiandao31)
		{
			Text component2 = base.transform.Find("Text").gameObject.GetComponent<Text>();
			BaseUIAnimation.action.SetLanguageFont("IconName5", component2, string.Empty);
		}
		else if (EnumUITypeType == BtnType.cdkey)
		{
			if (Singleton<DataManager>.Instance.bhanguoTestConfig)
			{
				Text component3 = base.transform.Find("Text").gameObject.GetComponent<Text>();
				BaseUIAnimation.action.SetLanguageFont("IconName6", component3, string.Empty);
			}
			else
			{
				base.transform.parent.gameObject.SetActive(value: false);
			}
		}
		else if (EnumUITypeType == BtnType.paihangbang)
		{
			Text component4 = base.transform.Find("Text").gameObject.GetComponent<Text>();
			BaseUIAnimation.action.SetLanguageFont("IconName2", component4, string.Empty);
		}
		else if (EnumUITypeType == BtnType.shangdian)
		{
			Text component5 = base.transform.Find("Text").gameObject.GetComponent<Text>();
			BaseUIAnimation.action.SetLanguageFont("IconName3", component5, string.Empty);
		}
		else if (EnumUITypeType == BtnType.beibao)
		{
			Text component6 = base.transform.Find("Text").gameObject.GetComponent<Text>();
			BaseUIAnimation.action.SetLanguageFont("IconName4", component6, string.Empty);
		}
	}

	private IEnumerator IEHuaUpdate()
	{
		yield return new WaitForSeconds(10f);
		while (true)
		{
			if (Singleton<UserManager>.Instance.bOpenHua() > 0)
			{
				HuaBtnObj.gameObject.SetActive(value: true);
			}
			else
			{
				HuaBtnObj.gameObject.SetActive(value: false);
			}
			yield return new WaitForSeconds(10f);
		}
	}

	public void NextLb24()
	{
		string nowTime_Day = Util.GetNowTime_Day();
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowDaySaleAd3" + nowTime_Day);
		UnityEngine.Debug.Log("iNowDaySaleAd3=" + @int);
		if (@int != 101)
		{
			if (@int == 0)
			{
				int value = UnityEngine.Random.Range(1, 7);
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_iNowDaySaleAd3" + nowTime_Day, value);
			}
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NextLb2", 100);
		}
		else if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NextLb2") == 0)
		{
			Singleton<UserManager>.Instance.NextLb2();
		}
		ResLbUI();
	}

	public void ResLbUI()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NextLb2");
		if (EnumUITypeType == BtnType.haohualibao)
		{
			@int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NextLb1");
		}
		if (@int == 100)
		{
			base.gameObject.GetComponent<Image>().sprite = ChangeSp[0];
			BtnImg.sprite = ChangeText[11];
		}
		else
		{
			if (@int == 0)
			{
				return;
			}
			if (@int == 2 || @int == 3 || @int == 8 || @int > 8)
			{
				base.gameObject.GetComponent<Image>().sprite = ChangeSp[0];
			}
			else
			{
				switch (@int)
				{
				case 4:
					base.gameObject.GetComponent<Image>().sprite = ChangeSp[1];
					break;
				case 5:
				case 6:
				case 7:
					base.gameObject.GetComponent<Image>().sprite = ChangeSp[2];
					break;
				}
			}
			if (@int > 8)
			{
				switch (@int)
				{
				case 9:
					BtnImg.sprite = ChangeText[7];
					break;
				case 10:
					BtnImg.sprite = ChangeText[8];
					break;
				default:
					BtnImg.sprite = ChangeText[10];
					break;
				}
			}
			else
			{
				BtnImg.sprite = ChangeText[@int - 2];
			}
		}
	}

	public void ClickBtn()
	{
		if (EnumUITypeType == BtnType.shipingjiangli)
		{
			UI.Instance.OpenPanel(UIPanelType.VideoRewardPanel);
		}
		else if (EnumUITypeType == BtnType.paihangbang)
		{
			UI.Instance.OpenPanel(UIPanelType.GooglePlay3Panel);
		}
		else if (EnumUITypeType == BtnType.cdkey)
		{
			UI.Instance.OpenPanel(UIPanelType.cdkeyUI);
		}
		else if (EnumUITypeType == BtnType.qiandao31)
		{
			UI.Instance.OpenPanel(UIPanelType.SignRewardUI);
		}
		else if (EnumUITypeType == BtnType.AdUI)
		{
			Singleton<DataManager>.Instance.bAdRewardPlay = false;
			Singleton<DataManager>.Instance.bAdRewardLose = false;
			Singleton<DataManager>.Instance.bAdRewardHome = true;
			InitAndroid.action.PlayVideoAd();
		}
		else
		{
			if (Singleton<DataManager>.Instance.bGrilMoveing || Util.GetbForced_guidance())
			{
				return;
			}
			if (EnumUITypeType == BtnType.beibao)
			{
				UI.Instance.OpenPanel(UIPanelType.PackSkillIconUI);
				return;
			}
			if (EnumUITypeType == BtnType.fengche)
			{
				UI.Instance.OpenPanel(UIPanelType.PackSkillIconUI);
				return;
			}
			if (EnumUITypeType == BtnType.shangdian)
			{
				Singleton<DataManager>.Instance.ChinaShopOpendaoju = true;
				Singleton<DataManager>.Instance.ChinaShopOpenZuanshi = false;
				UI.Instance.OpenPanel(UIPanelType.ChinaShop);
				return;
			}
			if (EnumUITypeType == BtnType.qiandao)
			{
				UnityEngine.Debug.Log("jyqiandao 1   BtnType.qiandao");
				UI.Instance.OpenPanel(UIPanelType.Qiandao7Panel);
				InitAndroid.action.GAEvent("clickbtn:SignRewardUI");
			}
			if (EnumUITypeType == BtnType.haohualibao)
			{
				int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NextLb1");
				if (@int > 8)
				{
					Singleton<DataManager>.Instance.bBuyLi1 = true;
					Singleton<DataManager>.Instance.bBuyLi2 = false;
					InitSaleAd2(@int);
					UI.Instance.OpenPanel(UIPanelType.SaleAd2UI);
				}
				else if (@int != 0)
				{
					Singleton<DataManager>.Instance.bBuyLi1 = true;
					Singleton<DataManager>.Instance.bBuyLi2 = false;
					DataManager.sale_adKey = "Bubble_LB" + @int;
					UI.Instance.OpenPanel(UIPanelType.SaleAdUI);
				}
				else
				{
					Singleton<DataManager>.Instance.bBuyLi1 = true;
					Singleton<DataManager>.Instance.bBuyLi2 = false;
					DataManager.sale_adKey = "Bubble_LB3";
					UI.Instance.OpenPanel(UIPanelType.SaleAdUI);
				}
			}
			if (EnumUITypeType == BtnType.Time24libao)
			{
				InitAndroid.action.GAEvent("clickbtn:Reward24UI");
				UI.Instance.OpenPanel(UIPanelType.Reward24UI);
			}
			if (EnumUITypeType == BtnType.zhuanpan)
			{
				UI.Instance.OpenPanel(UIPanelType.LotteryUI);
			}
		}
	}

	public void InitSaleAd2(int index)
	{
		switch (index)
		{
		case 9:
			DataManager.sale_adKey = "First_Pay";
			break;
		case 10:
			DataManager.sale_adKey = "Live_Pack";
			break;
		case 11:
			DataManager.sale_adKey = "Any_Way_Pack_1";
			break;
		case 12:
			DataManager.sale_adKey = "Any_Way_Pack_2";
			break;
		case 13:
			DataManager.sale_adKey = "Any_Way_Pack_3";
			break;
		}
	}

	private void Update()
	{
		if (EnumUITypeType != BtnType.qiandao)
		{
			return;
		}
		string nowTime_Day = Util.GetNowTime_Day();
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iqiandao7Count", 1);
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SignOK7" + nowTime_Day);
		if (@int > 7 || int2 == 1)
		{
			if (BtnImg != null)
			{
				BtnImg.gameObject.SetActive(value: false);
			}
		}
		else if (BtnImg != null)
		{
			BtnImg.gameObject.SetActive(value: true);
		}
	}
}
