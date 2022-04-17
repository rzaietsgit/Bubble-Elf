using System;
using System.Collections;
using EasyMobile;
using ITSoft;
using UnityEngine;

public class BuyLivesChinaUIPanel : BuyLivesChinaUIPanelBase
{
	public static BuyLivesChinaUIPanel panel;

	public override void InitUI()
	{
		AdsManager.ShowBanner();
		panel = this;
		DataManager.bbeibaoFlay = false;
		Singleton<DataManager>.Instance.bexitGameScene = false;
		BaseUIAnimation.action.SetLanguageFont("buylivefulltitle", detail.Text_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BuyLivesChinaUI1", detail.Remark2_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BuyLivesChinaUI2", detail.Remark_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("buylivefull", detail.remarkoveText_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("LoveFull", detail.Text2_Text, string.Empty);
		StartCoroutine(UpdateTime());
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
		if (@int >= 5)
		{
			detail.S_ButtonGray_Button.gameObject.SetActive(value: true);
			detail.S_Button_Button.gameObject.SetActive(value: false);
		}
		else
		{
			detail.S_ButtonGray_Button.gameObject.SetActive(value: false);
			detail.S_Button_Button.gameObject.SetActive(value: true);
		}
		if (Singleton<UserManager>.Instance.getLoveInfinite() > 0)
		{
		}
		string text = Singleton<DataManager>.Instance.dDataLanguage["ChinaShopUI4"][BaseUIAnimation.Language];
		text = text.Replace("A1", Singleton<DataManager>.Instance.dDataChinaPay["Fivelives"]["iMoneyiosen"].ToString());
		detail.money099_Text.text = text;
		if (Singleton<DataManager>.Instance.CommodityPricesDic != null)
		{
			string key = Singleton<DataManager>.Instance.dDataChinaPay["Fivelives"]["googlekey"];
			string text2 = Singleton<DataManager>.Instance.CommodityPricesDic[key];
			detail.money099_Text.text = text2;
		}
		text = Singleton<DataManager>.Instance.dDataLanguage["ChinaShopUI4"][BaseUIAnimation.Language];
		text = text.Replace("A1", Singleton<DataManager>.Instance.dDataChinaPay["TwoHourUnlimitedLives"]["iMoneyiosen"].ToString());
		detail.money199_Text.text = text;
		if (Singleton<DataManager>.Instance.CommodityPricesDic != null)
		{
			string key2 = Singleton<DataManager>.Instance.dDataChinaPay["TwoHourUnlimitedLives"]["googlekey"];
			string text3 = Singleton<DataManager>.Instance.CommodityPricesDic[key2];
			detail.money199_Text.text = text3;
		}
	}

	public void Refresh()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
		if (@int >= 5)
		{
		}
		if (@int >= 5)
		{
			detail.S_ButtonGray_Button.gameObject.SetActive(value: true);
			detail.S_Button_Button.gameObject.SetActive(value: false);
		}
		else
		{
			detail.S_ButtonGray_Button.gameObject.SetActive(value: false);
			detail.S_Button_Button.gameObject.SetActive(value: true);
		}
	}

	private IEnumerator UpdateTime()
	{
		bool b = true;
		while (b)
		{
			int iStar = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
			detail.LoveCount_Text.text = iStar.ToString();
			if (iStar >= Singleton<DataManager>.Instance.iLoveMaxAll)
			{
				detail.Time_Text.gameObject.SetActive(value: false);
				detail.Remark2_Text.gameObject.SetActive(value: true);
				detail.Remark_Text.gameObject.SetActive(value: false);
			}
			else
			{
				detail.Time_Text.gameObject.SetActive(value: true);
				detail.Remark2_Text.gameObject.SetActive(value: false);
				detail.Remark_Text.gameObject.SetActive(value: true);
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
				detail.Time_Text.text = text + ":" + text2;
			}
			yield return new WaitForSeconds(1f);
		}
	}

	public override void OnCloseButton()
	{
        AdsManager.HideBanner();
        UI.Instance.ClosePanel();
	}

	public override void OnS_Button()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
		if (@int < 5)
		{
			int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
			if (int2 >= 100)
			{
				BuyTiLi(100, 5 - @int);
			}
			else
			{
				UI.Instance.OpenPanel(UIPanelType.ChinaShop);
			}
		}
	}

	public override void OnS_Button_099()
	{
		base.OnS_Button_099();
		//InitAndroid.action.doChainePay("Fivelives");
        IAPManager.Purchase(EM_IAPConstants.Product_fivelives);
    }

	public override void OnS_Button_199()
	{
		base.OnS_Button_199();
        IAPManager.Purchase(EM_IAPConstants.Product_twohourunlimitedlives);
        //InitAndroid.action.doChainePay("TwoHourUnlimitedLives");
	}

	public void Buy()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		int num = 150;
		if (num > @int)
		{
			Singleton<DataManager>.Instance.ChinaShopOpendaoju = false;
			Singleton<DataManager>.Instance.ChinaShopOpenZuanshi = true;
			UI.Instance.OpenPanel(UIPanelType.ChinaShop);
			return;
		}
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
		int num2 = 5 - int2;
		ChinaPay.action.addRewardAll(1, num2, panel.gameObject, isShow: true, "buy", "love");
		PayManager.action.ExpendGDP(num, GDPType.BUYLOVE, 5);
		if ((bool)GameUI.action)
		{
			panel.OnCloseButton();
		}
		Refresh();
	}

	public void BuyTiLi(int money, int count)
	{
		ChinaPay.action.addRewardAll(1, count, panel.gameObject);
		PayManager.action.ExpendGDP(money, GDPType.BUYLOVE, count);
		Refresh();
	}

	private void OnBtn_100Gold_ClickHandle()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
		if (@int < 5)
		{
			int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
			if (int2 >= 100)
			{
				int2 -= 100;
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_GOLD", int2);
			}
			else
			{
				UI.Instance.OpenPanel(UIPanelType.ChinaShop);
			}
		}
	}
}
