using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class InitAndroid : MonoBehaviour
{
	public static InitAndroid action;


	private string subject = "WORD-O-MAZE";

	private string body = "PLAY THIS AWESOME. GET IT ON THE PLAYSTORE";

	public bool InitOver;

	public bool IsHouTai;

	private bool bshop;

	private void Awake()
	{
		action = this;
		if (InitGame.bChinaVersion && Application.platform == RuntimePlatform.Android)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
	}

	public void SetversionCode(string versionCode)
	{
		Singleton<DataManager>.Instance.sversionCode = versionCode;
	}

	private void Start()
	{
		InitOver = true;
	}

	public void GameOnStart(string snull)
	{
		if ((bool)GameUI.action && UI.Instance.GetPanelCount() <= 0)
		{
			IsHouTai = true;
			GameUI.action.PauseBtn();
		}
	}
	public void AndroidLog(string str)
	{
	}

	public void ShowPayMask()
	{
		UI.Instance.OpenPanel(UIPanelType.PayMaskPanel);
		DDOLSingleton<CoroutineController>.Instance.StartCoroutine(IEHidePayMask());
	}

	public void HidePayMask()
	{
		if ((bool)PayMaskPanel.panel)
		{
			PayMaskPanel.panel.CloseMask();
		}
	}

	public IEnumerator IEHidePayMask()
	{
		yield return new WaitForSeconds(1f);
		HidePayMask();
	}

	public void doChainePay(string key)
	{
		UnityEngine.Debug.Log("DoChinaPay key = " + key);
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_PayLastTime");
			int nowTime = Util.GetNowTime();
			if (@int > 0)
			{
				int num = nowTime - @int;
				if (num < 60)
				{
					int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_PayFlag");
					if (int2 == 1)
					{
						return;
					}
				}
			}
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_PayFlag", 1);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_PayLastTime", nowTime);
		}
		if (key == "yiyuantehuilibao")
		{
			return;
		}
		//Analytics.Event("ClickPay_" + key);
		UnityEngine.Debug.Log("key=" + key);
		string text = Singleton<DataManager>.Instance.dDataChinaPay[key]["desc"];
		int num2 = int.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["inumber"]);
		// float num3 = float.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["iMoney"]);
		if (Singleton<DataManager>.Instance.bChinaIos)
		{
			// num3 = float.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["iMoneyios"]);
			// if (InitGame.bEnios)
			// {
			// 	var num3Double = double.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["iMoneyiosen"]);
			// 	num3 = (float) num3Double;
			// }
		}
		string text2 = Singleton<DataManager>.Instance.dDataChinaPay[key]["payid"];
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_coin");
		}
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	if (Singleton<DataManager>.Instance.bGooglePay)
		//	{
		//		action.ShowPayMask();
		//		//AndroidJavaObject @static = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//		Singleton<DataManager>.Instance.bPayState = true;
		//		UnityEngine.Debug.Log(" jy googlekey111 key" + key + "," + text2 + "," + text + "," + num2 + "," + num3);
		//		float num4 = float.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["iMoneyiosen"]);
		//		//@static.Call("Pay", Singleton<DataManager>.Instance.dDataChinaPay[key]["googlekey"], key, num4 + string.Empty);
		//	}
		//}
		//else if (Application.platform == RuntimePlatform.IPhonePlayer)
		//{
		//	if (InitGame.bChinaVersion && Singleton<DataManager>.Instance.bChinaIos)
		//	{
		//		addAdJust(key, 2, string.Empty);
		//		string s = Singleton<DataManager>.Instance.dDataChinaPay[key]["ioskey"];
		//		if (InitGame.bEnios)
		//		{
		//			s = Singleton<DataManager>.Instance.dDataChinaPay[key]["ioskeyus"];
		//		}
		//		PayManager.action._UBuyProduct(s);
		//	}
		//}
		//else
		//{
			//action.ShowPayMask();
			InitGame.Action.StartCoroutine(iewindows(key));
		//}
	}

	private IEnumerator iewindows(string key)
	{
		yield return new WaitForSeconds(0.5f);
		//action.ShowPayMask();
		onPaySuccessChina(key, string.Empty);
	}

	public void LiuHai(string sdata)
	{
		Singleton<DataManager>.Instance.bLiuhai = true;
	}

	public void YunbuKey(string sdata)
	{
		if ((bool)cdkeyUI.action)
		{
			cdkeyUI.action.CdkeyErrorText.text = "兑换成功";
			cdkeyUI.action.CdkeyErrorText.gameObject.SetActive(value: true);
		}
		if (sdata.LastIndexOf("open") >= 0)
		{
			int num = int.Parse(sdata.Split(' ')[1]);
			for (int i = 1; i <= num; i++)
			{
				int num2 = i;
				string s = "3";
				string s2 = "88888";
				Singleton<DataManager>.Instance.SaveUserDate("DB_LevelStar_" + num2, int.Parse(s));
				Singleton<DataManager>.Instance.SaveUserDate("DB_LevelScore_" + num2, int.Parse(s2));
				int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
				if (num2 <= @int)
				{
					continue;
				}
				Singleton<DataManager>.Instance.SaveUserDate("DB_iNowPassLevelID", num2);
				for (int j = 0; j < Singleton<DataManager>.Instance.LMapEndBtnID.Length; j++)
				{
					if (num2 == Singleton<DataManager>.Instance.LMapEndBtnID[j])
					{
						Singleton<UserManager>.Instance.GoNextMap();
					}
				}
			}
			Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.InitGame);
			return;
		}
		if (sdata.LastIndexOf("pay") >= 0)
		{
			int iGold = int.Parse(sdata.Split(' ')[1]);
			PayManager.action.AwardAddGold(iGold, "QIANDAO");
			if ((bool)PayManager.action)
			{
				PayManager.action.LoadGold();
			}
		}
		try
		{
			string text = string.Empty;
			foreach (string key in Singleton<DataManager>.Instance.dDataChinaPay.Keys)
			{
				int num3 = int.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["payid"]);
				if (num3 == int.Parse(sdata))
				{
					text = key;
					UnityEngine.Debug.Log("payid=" + num3);
					break;
				}
			}
			if (text != string.Empty)
			{
				action.ResCdKey(text);
			}
		}
		catch (Exception)
		{
			if ((bool)cdkeyUI.action)
			{
				cdkeyUI.action.cdkeyerr();
			}
		}
	}

	public void ReturnshowMarket(string str)
	{
		Singleton<DataManager>.Instance.bShowHaopingLogin = false;
		if ((bool)PlayPanel.panel)
		{
			PlayPanel.panel.CloseHaoping();
		}
		if ((bool)LosePanel.panel)
		{
			LosePanel.panel.CloseHaoping();
		}
		if ((bool)WinPanel.panel)
		{
			WinPanel.panel.CloseHaoping();
		}
	}

	public void ReturnshowMarketOpen(string str)
	{
		if ((bool)PlayPanel.panel)
		{
			PlayPanel.panel.OpenHaoping();
		}
		if ((bool)LosePanel.panel)
		{
			LosePanel.panel.OpenHaoping();
		}
		if ((bool)WinPanel.panel)
		{
			WinPanel.panel.OpenHaoping();
		}
	}

	public void ReturnYunboCdKey(string sData)
	{
		UnityEngine.Debug.Log("ReturnYunboCdKey sdata" + sData);
		YunbuKey(sData);
	}

	public void ReturnYunboCdKeyErr(string sData)
	{
		if ((bool)cdkeyUI.action)
		{
			cdkeyUI.action.CdkeyErrorText.text = "兑换失败";
			cdkeyUI.action.CdkeyErrorText.gameObject.SetActive(value: true);
		}
		UnityEngine.Debug.Log("ReturnYunboCdKeyErr errsdata" + sData);
	}

	public void ResCdKey(string key)
	{
		aliyunlog.PayLog(key, "cdkey");
		UnityEngine.Debug.Log("ResCdKey=" + key);
		onPaySuccessChina(key, string.Empty);
	}

	public void InitTimeChina(string str)
	{
		InitGame.Action.InitTimeChina(str);
	}

	public void ShowAdTip(string str)
	{
		if ((bool)SaleAd2UIPanel.panel)
		{
			SaleAd2UIPanel.panel.ShowAdTip();
		}
		if ((bool)SaleAdUIPanel.panel)
		{
			SaleAdUIPanel.panel.ShowAdTip();
		}
		if ((bool)ChinaShopPanel.panel)
		{
			ChinaShopPanel.panel.ShowAdTip();
		}
		if ((bool)NowBuyBubbleUIPanel.panel)
		{
			NowBuyBubbleUIPanel.panel.ShowAdTip();
		}
		if ((bool)BuyGangUIPanel.panel)
		{
			BuyGangUIPanel.panel.ShowAdTip();
		}
		if ((bool)BuySkillUIChinaPanel.panel)
		{
			BuySkillUIChinaPanel.panel.ShowAdTip();
		}
	}

	public void checkShowAdTip()
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	AndroidJavaObject @static = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//	@static.Call("AdShowAdTip");
		//}
		//if (Application.platform != RuntimePlatform.WindowsEditor)
		//{
		//}
	}

	public void showBannerHGHouTai()
	{
		if (Singleton<DataManager>.Instance.isrewardad)
		{
			return;
		}
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
		if (@int <= 10)
		{
			return;
		}
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_showBannerHGTimeHouTai");
		int nowTime = Util.GetNowTime();
		if (nowTime > int2 + 10 && ((bool)MapUI.action || (bool)GameUI.action))
		{
			//if (Application.platform == RuntimePlatform.Android)
			//{
			//	UnityEngine.Debug.Log("jy001 showBannerHGHouTai");
			//	AndroidJavaObject @static = unity.GetStatic<AndroidJavaObject>("currentActivity");
			//	@static.Call("showBannerHG");
			//	FirebaseController.PlayScreenAds();
			//}
			//if (Application.platform != RuntimePlatform.WindowsEditor)
			//{
			//}
		}
	}

	public void showBannerHG()
	{
		if (Singleton<TestScript>.Instance.GetInt("IsClearAd") != 0)
		{
			UnityEngine.Debug.Log("NoShowBannerHG");
			return;
		}
		UnityEngine.Debug.Log("ShowBannerHG");
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
		if (@int <= 10)
		{
			return;
		}
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_showBannerHGTime");
		int nowTime = Util.GetNowTime();
		if (nowTime > int2 + 120)
		{
			//if (Application.platform == RuntimePlatform.Android)
			//{
			//	UnityEngine.Debug.Log("jy001 showBannerHG");
			//	AndroidJavaObject @static = unity.GetStatic<AndroidJavaObject>("currentActivity");
			//	@static.Call("showBannerHG");
			//	FirebaseController.PlayScreenAds();
			//}
			//if (Application.platform == RuntimePlatform.WindowsEditor)
			//{
			//}
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_showBannerHGTime", nowTime);
		}
	}

	public void GAEvent(string str)
	{
	}

	public void GAEvent(string str, int icount)
	{
	}

	public void GAEventByGameStatus(int index)
	{
	}

	public void useMoney(float imoney)
	{
		float @float = Singleton<TestScript>.Instance.GetFloat(DataManager.SDBNO + "DB_PayGOLDCount");
		@float += imoney;
		FirebaseController.Buy((int)(imoney * 100f));
		Singleton<DataManager>.Instance.SaveUserDate("DB_PayGOLDCount", @float);
	}

	public void PayError(string key)
	{
		Singleton<DataManager>.Instance.bPayState = false;
		if ((bool)SaleAdUI.action)
		{
			SaleAdUI.action.PayError();
		}
	}

	public void onPaySuccessChinaID(string ID)
	{
		UnityEngine.Debug.Log("onPaySuccessChinaID jy 1 = ID" + ID);
		Singleton<DataManager>.Instance.SaveUserDate("BUFAPay", int.Parse(ID));
	}

	public void onPaySuccessChinaRewardID(string ID)
	{
		Singleton<DataManager>.Instance.SaveUserDate("BUFAPay", 0);
		UnityEngine.Debug.Log("onPaySuccessChinaRewardID jy 1 = ID" + ID);
		if (InitGame.bChinaVersion)
		{
			foreach (string key in Singleton<DataManager>.Instance.dDataChinaPay.Keys)
			{
				if (ID == Singleton<DataManager>.Instance.dDataChinaPay[key]["payid"])
				{
					UnityEngine.Debug.Log("onPaySuccessChinaRewardID jy 2 ID= " + ID);
					onPaySuccessChina(key, string.Empty);
				}
			}
		}
	}

	public void addAdJust(string skey, int iType = 1, string TransactionId = "")
	{
	}

	public void onPaySuccessChinas(string key)
	{
		onPaySuccessChina(key, string.Empty);
	}

	public void onPaySuccessChina(string key, string TransactionId = "")
	{
		//try
		//{
		//	aliyunlog.PayLog(key, "onlinepay");
		//}
		//catch (Exception)
		//{
		//}
		//action.HidePayMask();
		if (string.Equals(key, "Fivelives"))
		{
			if (BuyLivesChinaUIPanel.panel != null)
			{
				BuyLivesChinaUIPanel.panel.BuyTiLi(0, 5);
				FirebaseController.Buy(99);
			}
			return;
		}
		if (string.Equals(key, "TwoHourUnlimitedLives"))
		{
			if (BuyLivesChinaUIPanel.panel != null)
			{
				ChinaPay.action.addRewardAll(10, 2, BuyLivesChinaUIPanel.panel.gameObject);
				FirebaseController.Buy(199);
			}
			return;
		}
		if (string.Equals(key, "Adsfree"))
		{
			Singleton<TestScript>.Instance.SetInt("IsClearAd", 1);
			if (ChinaShopPanel.panel != null)
			{
				ChinaShopPanel.panel.UpdateClearAdBtn();
				FirebaseController.Buy(199);
			}
			return;
		}
		//GAEvent("payok" + key);
		//GAEvent("NewPayok:" + key + ":" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
		//GAEvent("payokAll:" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
		if (UI.Instance.GetTopPanelType() != UIPanelType.ChinaShop)
		{
			try
			{
				UI.Instance.ClosePanel();
			}
			catch (Exception)
			{
			}
		}
		if (key == "VipPlayer")
		{
			Singleton<DataManager>.Instance.RewardVip7(b: true);
			string sDate = DateTime.Now.AddDays(6.0).ToString("yyyyMMdd");
			Singleton<DataManager>.Instance.SaveUserDate("DB_Vip7", sDate);
			InitGame.bVip7 = true;
			FireBase.Action.UnityUpFaceBookVip7();
			return;
		}
		if (Singleton<DataManager>.Instance.bGooglePay)
		{
			float priceAmount = 0f;
			FaceBookApi.Action.FbEvPay(priceAmount, "USD", key);
		}
		UnityEngine.Debug.Log("onPaySuccessChina jy  key= " + key);
		if (InitGame.bChinaVersion && Singleton<DataManager>.Instance.bChinaIos && InitGame.bChinaVersion && Singleton<DataManager>.Instance.bChinaIos)
		{
			foreach (string key2 in Singleton<DataManager>.Instance.dDataChinaPay.Keys)
			{
				if (Singleton<DataManager>.Instance.bGooglePay)
				{
					if (key == Singleton<DataManager>.Instance.dDataChinaPay[key2]["googlekey"])
					{
						key = key2;
					}
				}
				else if (InitGame.bEnios)
				{
					if (key == Singleton<DataManager>.Instance.dDataChinaPay[key2]["ioskeyus"])
					{
						key = key2;
					}
				}
				else if (key == Singleton<DataManager>.Instance.dDataChinaPay[key2]["ioskey"])
				{
					key = key2;
				}
			}
		}
		if (InitGame.bEnios)
		{
			float priceAmount2 = 0f;
			FaceBookApi.Action.FbEvPay(priceAmount2, "USD", key);
		}
		int num = int.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["payid"]);
		if (key.LastIndexOf("Bubble_LB") >= 0 || num >= 33)
		{
			if (Singleton<DataManager>.Instance.bBuyLi1)
			{
				Singleton<UserManager>.Instance.NextLb1();
			}
			if (Singleton<DataManager>.Instance.bBuyLi2)
			{
				Singleton<UserManager>.Instance.NextLb2();
			}
			if ((bool)ChinaShopPanel.panel)
			{
				ChinaShopPanel.panel.ResLibao();
			}
		}
		if (num >= 38 && num <= 49)
		{
			string nowTime_Day = Util.GetNowTime_Day();
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_iNowDaySaleAd3" + nowTime_Day, 101);
			Singleton<UserManager>.Instance.NextLb2();
		}
		UnityEngine.Debug.Log("onPaySuccessChina jy2 = " + key);
		UnityEngine.Debug.Log("key4=" + key);
		//Analytics.Event("SuccessPay_" + key);
		Singleton<DataManager>.Instance.SaveUserDate("PayDay" + Util.GetNowTime_Day(), 1);
		Singleton<DataManager>.Instance.SaveUserDate("PAY" + key, 1);
		Singleton<DataManager>.Instance.SaveUserDate("PAY" + key + Util.GetNowTime_Day(), 1);
		addAdJust(key, 1, string.Empty);
		if (TransactionId != string.Empty)
		{
			addAdJust(key, 100, TransactionId);
		}
		if (key == "Bubble_LB8")
		{
			Singleton<DataManager>.Instance.SaveUserDate("PAYBubble_LB5", 0);
			Singleton<DataManager>.Instance.SaveUserDate("PAYBubble_LB3", 0);
		}
		else if (key == "Bubble_LB7")
		{
			Singleton<DataManager>.Instance.SaveUserDate("PAYBubble_LB7", 0);
			Singleton<DataManager>.Instance.SaveUserDate("PAYBubble_LB8", 0);
		}
		if (key == "First_Pay")
		{
			Singleton<DataManager>.Instance.SaveUserDate("First_Pay", 1);
		}
		else if (key == "Bubble_LB3")
		{
			Singleton<DataManager>.Instance.SaveUserDate("PAYBubble_LB6", 0);
		}
		else if (key == "yiyuantehuilibao")
		{
			ChinaPay.action.CallNovicepacks();
		}
		float num2 = float.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["iMoney"].Replace('.', ','));
		int num3 = int.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["iMoney"]);
		FireBase.Action.GAUnityGAEventPay(key, num2.ToString());
		if (key != "First_Pay" && (num < 38 || num > 49))
		{
			float iH = float.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["iLoveInfinite"]);
			Singleton<UserManager>.Instance.AddLoveInfinite(iH);
		}
		BaseUIAnimation.action.SetShowTime(b: true);
		if (Singleton<DataManager>.Instance.bChinaIos)
		{
			num2 = float.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["iMoneyios"].Replace('.', ','));
			if (InitGame.bEnios)
			{
				num2 = float.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["iMoneyiosen"].Replace('.', ','));
			}
		}
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "iMoneyLog");
		@int += num3;
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "iMoneyLog", @int);
		paylogwww(Singleton<UserManager>.Instance.iNowPassLevelID, key, num2);
		int num4 = int.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["iType"]);
		if (num4 != 1)
		{
			useMoney(num2);
		}
		Singleton<DataManager>.Instance.SaveUserDate("DB_CHONGZHI", 1);
		switch (num4)
		{
		case 1:
		{
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_DoublePay") == 0)
			{
				Singleton<DataManager>.Instance.SaveUserDate("DB_DoublePay", 1);
				if ((bool)ChinaShopUI.action)
				{
					ChinaShopUI.action.Clickzuanshiobj();
				}
				if ((bool)shangdian.action)
				{
					shangdian.action.BuyRes();
				}
			}
			int num15 = int.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["igold"]);
			if (num < 38 || num > 49)
			{
				AddGold(num15, num2);
			}
			if ((bool)MapUI.action)
			{
				if (num >= 38 && num <= 49)
				{
					List<int> list3 = new List<int>();
					List<int> list4 = new List<int>();
					int num16 = int.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["igold"]);
					if (num16 > 0)
					{
						list3.Add(3);
						list4.Add(num16);
					}
					for (int j = 1; j <= 3; j++)
					{
						int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SaleAd3_ID" + j);
						int int3 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SaleAd3_Count" + j);
						if (int2 != 0)
						{
							if (int2 == 1)
							{
								list3.Add(1);
								list4.Add(5 * int3);
							}
							if (int2 == 2)
							{
								list3.Add(1);
								list4.Add(15 * int3);
							}
							if (int2 == 3)
							{
								list3.Add(1);
								list4.Add(30 * int3);
							}
							if (int2 == 4)
							{
								list3.Add(6);
								list4.Add(int3);
							}
							if (int2 == 5)
							{
								list3.Add(5);
								list4.Add(int3);
							}
							if (int2 == 6)
							{
								list3.Add(7);
								list4.Add(int3);
							}
							if (int2 == 7)
							{
								list3.Add(8);
								list4.Add(int3);
							}
							if (int2 == 8)
							{
								list3.Add(9);
								list4.Add(int3);
							}
							if (int2 == 9)
							{
								list3.Add(17);
								list4.Add(int3);
							}
						}
					}
					for (int k = 0; k < list3.Count; k++)
					{
						ChinaPay.action.addRewardAll(list3[k], list4[k], null, isShow: false);
					}
					BaseUIAnimation.action.ShowProp(list3, list4, MapUI.action.gameObject);
				}
				else
				{
					ChinaPay.action.addReward3(num15, MapUI.action.gameObject, isShow: true, bAdd: false);
				}
			}
			else
			{
				ChinaPay.action.addReward3(num15, null, isShow: true, bAdd: false);
			}
			break;
		}
		case 2:
		case 3:
		{
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			int num5 = int.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["ilove"]);
			if (num5 > 0)
			{
				list.Add(1);
				list2.Add(num5);
			}
			int num6 = int.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["igold"]);
			if (num6 > 0)
			{
				list.Add(3);
				list2.Add(num6);
			}
			int num7 = int.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["skill1"]);
			if (num7 > 0)
			{
				list.Add(4);
				list2.Add(num7);
			}
			int num8 = int.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["skill2"]);
			if (num8 > 0)
			{
				list.Add(5);
				list2.Add(num8);
			}
			int num9 = int.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["skill3"]);
			if (num9 > 0)
			{
				list.Add(6);
				list2.Add(num9);
			}
			int num10 = int.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["skill4"]);
			if (num10 > 0)
			{
				list.Add(8);
				list2.Add(num10);
			}
			int num11 = int.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["skill5"]);
			if (num11 > 0)
			{
				list.Add(9);
				list2.Add(num11);
			}
			int num12 = int.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["skill6"]);
			if (num12 > 0)
			{
				list.Add(7);
				list2.Add(num12);
			}
			int num13 = int.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["skill7"]);
			if (num13 > 0)
			{
				list.Add(17);
				list2.Add(num13);
			}
			if (num4 != 1)
			{
				int num14 = int.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["iLoveInfinite"]);
				if (num14 > 0)
				{
					list.Add(10);
					list2.Add(num14);
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (num4 == 3)
				{
					if ((bool)BuySkillUIChinaPanel.panel)
					{
						ChinaPay.action.addRewardAll(list[i], list2[i], BuySkillUIChinaPanel.panel.gameObject, isShow: false, "pay", "gamebuy");
					}
					else
					{
						ChinaPay.action.addRewardAll(list[i], list2[i], null, isShow: false, "pay", "gamebuy");
					}
				}
				else
				{
					ChinaPay.action.addRewardAll(list[i], list2[i], null, isShow: false, "pay", "gamebuy");
				}
			}
			if (num4 == 3)
			{
				if ((bool)BuySkillUIChinaPanel.panel)
				{
					BaseUIAnimation.action.ShowProp(list, list2, BuySkillUIChinaPanel.panel.gameObject);
				}
				else
				{
					BaseUIAnimation.action.ShowProp(list, list2, null);
				}
				PayManager.action.LoadSkill(100);
				if ((bool)BuySkillUIChinaPanel.panel)
				{
					BuySkillUIChinaPanel.panel.OnCloseButton();
				}
			}
			else if ((bool)MapUI.action)
			{
				BaseUIAnimation.action.ShowProp(list, list2, MapUI.action.gameObject);
			}
			else if ((bool)ChinaShopUI.action)
			{
				BaseUIAnimation.action.ShowProp(list, list2, ChinaShopUI.action.gameObject);
			}
			else if ((bool)SaleAdUI.action)
			{
				BaseUIAnimation.action.ShowProp(list, list2, SaleAdUI.action.gameObject);
			}
			else
			{
				BaseUIAnimation.action.ShowProp(list, list2, null);
			}
			break;
		}
		case 4:
			if (key.LastIndexOf("BuyGang") >= 0)
			{
				BubbleSpawner.Instance.buyskillGang.GetComponent<MuTong>().AddSkill();
				//Analytics.Event("GangPay" + key.Split('g')[1]);
			}
			if (key.LastIndexOf("BuyBubble") < 0)
			{
				break;
			}
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "FirstBuyBubble") == 0)
			{
				action.GAEvent("FirstBuyBubble:" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "FirstBuyBubble", 1);
			}
			if (!(key == "BuyBubble2") && !(key == "BuyBubble1"))
			{
				break;
			}
			if ((bool)BuyBubbleUI.action)
			{
				BuyBubbleUI.action.PayBubbleChina();
			}
			if ((bool)NowBuyBubbleUIPanel.panel)
			{
				if (Singleton<LevelManager>.Instance.iBubbleCount == 0)
				{
					//Analytics.Event("PayBuyBubble2");
					Singleton<LevelManager>.Instance.iBubbleCount = 10;
					PassLevel.action.KillLiuhan();
					GameUI.action.LoadBubbleCount();
					BubbleSpawner.Instance.ready_1 = null;
					BubbleSpawner.Instance.ready_2 = null;
					BubbleSpawner.Instance.initReadyBubble();
					NowBuyBubbleUIPanel.panel.BuyOk();
					NowBuyBubbleUIPanel.panel.OnCloseButton();
				}
				else
				{
					//Analytics.Event("PayBuyBubble1");
					Singleton<LevelManager>.Instance.iBubbleCount = Singleton<LevelManager>.Instance.iBubbleCount + 10;
					PassLevel.action.KillLiuhan();
					GameUI.action.LoadBubbleCount();
					BubbleSpawner.Instance.initReadyBubble(isusekey: false);
					GameUI.action.KillNowBuyBubble();
					NowBuyBubbleUIPanel.panel.BuyOk();
					NowBuyBubbleUIPanel.panel.OnCloseButton();
				}
			}
			break;
		}
		if ((bool)SaleAdUI.action && (bool)GameUI.action)
		{
			SaleAdUI.action.CloseUI();
		}
		if ((bool)ChinaShopUI.action && (bool)GameUI.action)
		{
			ChinaShopUI.action.CloseUI();
		}
		if ((bool)yiyuanlibao.action)
		{
			yiyuanlibao.action.ResUIBtn();
		}
		if ((bool)OpenScript.action1)
		{
			OpenScript.action1.ResHaohuaBtnUI();
		}
		if ((bool)xinshou.action)
		{
			xinshou.action.ResUIBtn();
		}
		if ((bool)SaleAdUI.action)
		{
			SaleAdUI.action.CloseUI();
		}
		if (Application.platform == RuntimePlatform.Android && Singleton<DataManager>.Instance.bGooglePay && key != "Bubble_099_01" && key != "PACKAGE0199" && key != "PACKAGE299")
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "GooglePayfreeAD", 1);
		}
		if ((bool)ChinaShopPanel.panel)
		{
			ChinaShopPanel.panel.ResZs();
		}
	}

	public void paylogwww(int paylevel, string paykey, float paymoney)
	{
		string str = "http://jyerrorpaopao.unitygame8.com/updatepay.php";
		str = str + "?UserID=" + Singleton<UserManager>.Instance.initUserID();
		str = str + "&paylevel=" + paylevel;
		str = str + "&paykey=" + paykey;
		str = str + "&paymoney=" + paymoney;
		UnityEngine.Debug.Log("UpUrlStr=" + str);
		DDOLSingleton<CoroutineController>.Instance.StartCoroutine(IEUpdateUserLog(str));
	}

	private IEnumerator IEUpdateUserLog(string str)
	{
		yield return new WWW(str);
	}

	public void AddGold(int iGold, float iMoney)
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		int num = @int;
		@int += iGold;
		Singleton<DataManager>.Instance.SaveUserDate("DB_GOLD", @int);
		FirebaseController.AddOrSubStone(iGold);
		if (!InitGame.bChinaVersion && (bool)PayManager.action)
		{
			PayManager.action.LoadGold();
		}
		if ((bool)GameUI.action && (bool)ChinaShopUI.action)
		{
			ChinaShopUI.action.CloseUI();
		}
		if ((bool)ChinaShopUI.action && ChinaShopUI.action.bgodaoju)
		{
			ChinaShopUI.action.Clicklidaojuobj();
		}
		PayManager.action.AddPayCountMoney(float.Parse(iMoney + string.Empty));
		if (DataManager.ChannelId == "mi")
		{
			//GA.Pay(float.Parse(iMoney + string.Empty), GA.PaySource.mi, iGold);
		}
		else if (DataManager.ChannelId == "oppo")
		{
			//GA.Pay(float.Parse(iMoney + string.Empty), GA.PaySource.oppo, iGold);
		}
		if ((bool)ChinaShopPanel.panel)
		{
			ChinaShopPanel.panel.LoadDataShopUI();
		}
	}

	public void OpenQuit()
	{
		UnityEngine.Debug.Log("ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ1111111111");
		if (BubbleSpawner.IsWait)
		{
			UI.Instance.OpenPanel(UIPanelType.QuitUI);
		}
	}

	public void onPayFailure(string key)
	{

	}

	public void OnExit()
	{
		Application.Quit();
	}

	public void ShowQQQun(string sNull)
	{
		if ((bool)SettingPanelUI.action)
		{
			SettingPanelUI.action.ShowQQQun();
		}
		if ((bool)aboutChinaUI.action)
		{
			aboutChinaUI.action.Showqq();
		}
	}

	public void HideQQQun(string sNull)
	{
		if ((bool)SettingPanelUI.action)
		{
			SettingPanelUI.action.HideQQQun();
		}
	}

	public string CheckAd()
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	AndroidJavaObject @static = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//	string empty = string.Empty;
		//	return @static.Call<string>("CheckPlayVideoLeft", new object[0]);
		//}
		return string.Empty;
	}

	public void PLAY_FINISH(string sNull)
	{
		UnityEngine.Debug.Log("Jy-PLAY_FINISH");
		if ((bool)PlayPanel.panel)
		{
			PlayPanel.panel.HideYunbuAd();
		}
		AdReward(bvideo: true);
		if (Singleton<DataManager>.Instance.bAdRewardPlay)
		{
			Singleton<DataManager>.Instance.bAdRewardPlay = false;
		}
		if ((bool)LosePanel.panel)
		{
			LosePanel.panel.HideYunbuAd();
		}
	}

	public void PLAY_FINISH3(string sNull)
	{
		UnityEngine.Debug.Log("Jy-PLAY_FINISH3");
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_AdCount", 0);
		AdReward();
		if ((bool)PlayPanel.panel)
		{
			PlayPanel.panel.HideYunbuAd();
		}
		if (Singleton<DataManager>.Instance.bAdRewardPlay)
		{
			Singleton<DataManager>.Instance.bAdRewardPlay = false;
		}
		if ((bool)LosePanel.panel)
		{
			LosePanel.panel.HideYunbuAd();
		}
	}

	public void CLICK_AD(string sNull)
	{
		UnityEngine.Debug.Log("Jy-CLICK_AD");
		if ((bool)PlayPanel.panel)
		{
			PlayPanel.panel.HideYunbuAd();
		}
		if ((bool)LosePanel.panel)
		{
			LosePanel.panel.HideYunbuAd();
		}
	}

	public void CheckPlayVideo(bool bisplay)
	{
		if (Singleton<DataManager>.Instance.bGooglePay || !bPlayUIOpen(bisplay))
		{
			return;
		}
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	UnityEngine.Debug.Log("Jy-CheckPlayVideo1");
		//	AndroidJavaObject @static = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//	if (bisplay)
		//	{
		//		@static.Call("CheckPlayVideoplay");
		//	}
		//	else
		//	{
		//		@static.Call("CheckPlayVideolose");
		//	}
		//}
		//if (Application.platform == RuntimePlatform.IPhonePlayer)
		//{
		//}
		//if (Application.platform != RuntimePlatform.WindowsEditor)
		//{
		//}
	}

	public bool bPlayUIOpenHome()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_OpenADReward_Home" + Util.getInterNetTime());
		if (@int >= 3)
		{
			return false;
		}
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (int2 > 100)
		{
			return false;
		}
		int int3 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GB");
		if (int3 < 5000)
		{
			return true;
		}
		int num = UnityEngine.Random.Range(1, 101);
		if (num < 30)
		{
			return true;
		}
		return false;
	}

	public bool bPlayUIOpen(bool bisplay)
	{
		if (bisplay)
		{
			if (Singleton<UserManager>.Instance.iNowPassLevelID < 6)
			{
				return false;
			}
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_OpenADReward_Play" + Util.getInterNetTime());
			if (@int >= 3)
			{
				return false;
			}
			return true;
		}
		if (Singleton<LevelManager>.Instance.iFailure < 2)
		{
			return false;
		}
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_OpenADReward_Lose" + Util.getInterNetTime());
		if (int2 >= 3)
		{
			return false;
		}
		return true;
	}

	public void PlayVideoAd()
	{
		if (Singleton<DataManager>.Instance.bShowAdisVideo)
		{
			PlayVideoAdOK();
		}
		else
		{
			UI.Instance.OpenPanel(UIPanelType.YunbuAdShowPanel);
		}
	}

	public void ExityJyTest(int index)
	{
		//if (Application.platform != RuntimePlatform.Android)
		//{
		//	return;
		//}
		//AndroidJavaObject @static = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//if (Singleton<DataManager>.Instance.bAdRewardPlay)
		//{
		//	if (index == 1)
		//	{
		//		@static.Call("TestExit");
		//	}
		//	else
		//	{
		//		@static.Call("TestExit2");
		//	}
		//}
	}

	public void PlayVideoAdOK()
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
			//AndroidJavaObject @static = unity.GetStatic<AndroidJavaObject>("currentActivity");
			//if (Singleton<DataManager>.Instance.bAdRewardPlay)
			//{
			//	Analytics.Event("yunbuad:click:play");
			//	@static.Call("PlayVideoAdplay");
			//	Analytics.Event("yunbuad_click_play");
			//}
			//if (Singleton<DataManager>.Instance.bAdRewardLose)
			//{
			//	Analytics.Event("yunbuad:click:close");
			//	@static.Call("PlayVideoAdlose");
			//	Analytics.Event("yunbuad_click_close");
			//}
			//if (Singleton<DataManager>.Instance.bAdRewardHome)
			//{
			//	Analytics.Event("yunbuad:click:home");
			//	@static.Call("PlayVideoAdHome");
			//	Analytics.Event("yunbuad_click_home");
			//}
		//}
		if (Application.platform == RuntimePlatform.WindowsEditor)
		{
			PLAY_FINISH3(string.Empty);
		}
	}

	public void CheckPlayVideoFalse(string sNull)
	{
		UnityEngine.Debug.Log("Jy -CheckPlayVideoFalse");
	}

	public void CheckPlayVideoTrue(string sNull)
	{
		Singleton<DataManager>.Instance.bShowAdisVideo = false;
		if ((bool)PlayPanel.panel)
		{
			PlayPanel.panel.ShowYunbuAd();
		}
		if ((bool)NowBuyBubbleUIPanel.panel)
		{
			NowBuyBubbleUIPanel.panel.ShowYunbuAdVidoe();
		}
	}

	public void CheckPlayVideoTrueVIDEO(string sNull)
	{
		Singleton<DataManager>.Instance.bShowAdisVideo = true;
		UnityEngine.Debug.Log("Jy -CheckPlayVideoTrueVIDEO");
		if ((bool)PlayPanel.panel)
		{
			PlayPanel.panel.ShowYunbuAdVidoe();
		}
		if ((bool)NowBuyBubbleUIPanel.panel)
		{
			NowBuyBubbleUIPanel.panel.ShowYunbuAdVidoe();
		}
	}

	public void AdReward(bool bvideo = false)
	{
		SetAdCount();
		if ((bool)ClickBtnFun.AdUIObj)
		{
			ClickBtnFun.AdUIObj.ResAdUIClose();
		}
		int num = 0;
		if (Singleton<DataManager>.Instance.bAdRewardPlay)
		{
			if (bvideo)
			{
				num = GetRandom();
				int num2 = int.Parse(Singleton<DataManager>.Instance.dDataplayvideo[num.ToString()]["skiilid"].ToString());
				if (num2 == 0)
				{
					ChinaPay.action.addRewardAll(17, 1, null, isShow: true, "free", "ad");
				}
				if (num2 == 1)
				{
					ChinaPay.action.addRewardAll(4, 1, null, isShow: true, "free", "ad");
				}
				if (num2 == 2)
				{
					ChinaPay.action.addRewardAll(5, 1, null, isShow: true, "free", "ad");
				}
				if (num2 == 3)
				{
					ChinaPay.action.addRewardAll(6, 1, null, isShow: true, "free", "ad");
				}
				if (num2 <= 3)
				{
					Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_Skill_Select_" + num2, 1);
				}
				if (num2 == 4)
				{
					ChinaPay.action.addRewardAll(7, 1, null, isShow: true, "free", "ad");
				}
				if (num2 == 5)
				{
					ChinaPay.action.addRewardAll(8, 1, null, isShow: true, "free", "ad");
				}
			}
		}
		else if (Singleton<DataManager>.Instance.bAdRewardLose)
		{
			if (bvideo)
			{
				if (Singleton<LevelManager>.Instance.iBubbleCount == 0)
				{
					//Analytics.Event("PayBuyBubble2");
					Singleton<LevelManager>.Instance.iBubbleCount = 5;
					PassLevel.action.KillLiuhan();
					GameUI.action.LoadBubbleCount();
					BubbleSpawner.Instance.ready_1 = null;
					BubbleSpawner.Instance.ready_2 = null;
					BubbleSpawner.Instance.initReadyBubble();
					NowBuyBubbleUIPanel.panel.BuyOk();
					NowBuyBubbleUIPanel.panel.OnCloseButton();
				}
				else
				{
					//Analytics.Event("PayBuyBubble1");
					Singleton<LevelManager>.Instance.iBubbleCount = Singleton<LevelManager>.Instance.iBubbleCount + 5;
					PassLevel.action.KillLiuhan();
					GameUI.action.LoadBubbleCount();
					BubbleSpawner.Instance.initReadyBubble(isusekey: false);
					GameUI.action.KillNowBuyBubble();
					NowBuyBubbleUIPanel.panel.BuyOk();
					NowBuyBubbleUIPanel.panel.OnCloseButton();
				}
			}
		}
		else if (Singleton<DataManager>.Instance.bAdRewardHome && bvideo)
		{
			int num3 = UnityEngine.Random.Range(0, 100);
			if (num3 > 20)
			{
				ChinaPay.action.addRewardAll(3, 10, null, isShow: true, "free", "ad");
			}
			else if (num3 > 70)
			{
				ChinaPay.action.addRewardAll(2, 500, null, isShow: true, "free", "ad");
			}
			else
			{
				ChinaPay.action.addRewardAll(1, 5, null, isShow: true, "free", "ad");
			}
		}
		if (bvideo)
		{
			return;
		}
		if (Singleton<DataManager>.Instance.bAdRewardLose)
		{
			if (Singleton<LevelManager>.Instance.iBubbleCount == 0)
			{
				//Analytics.Event("PayBuyBubble2");
				Singleton<LevelManager>.Instance.iBubbleCount = 3;
				PassLevel.action.KillLiuhan();
				GameUI.action.LoadBubbleCount();
				BubbleSpawner.Instance.ready_1 = null;
				BubbleSpawner.Instance.ready_2 = null;
				BubbleSpawner.Instance.initReadyBubble();
				NowBuyBubbleUIPanel.panel.BuyOk();
				NowBuyBubbleUIPanel.panel.OnCloseButton();
			}
			else
			{
				//Analytics.Event("PayBuyBubble1");
				Singleton<LevelManager>.Instance.iBubbleCount = Singleton<LevelManager>.Instance.iBubbleCount + 3;
				PassLevel.action.KillLiuhan();
				GameUI.action.LoadBubbleCount();
				BubbleSpawner.Instance.initReadyBubble(isusekey: false);
				GameUI.action.KillNowBuyBubble();
				NowBuyBubbleUIPanel.panel.BuyOk();
				NowBuyBubbleUIPanel.panel.OnCloseButton();
			}
		}
		else
		{
			int num4 = UnityEngine.Random.Range(0, 100);
			if (num4 > 50)
			{
				ChinaPay.action.addRewardAll(4, 1, null);
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_Skill_Select_" + 1, 1);
			}
			else
			{
				ChinaPay.action.addRewardAll(5, 1, null);
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_Skill_Select_" + 2, 1);
			}
		}
	}

	public int GetRandom()
	{
		List<int> list = new List<int>();
		for (int i = 1; i <= 6; i++)
		{
			int item = int.Parse(Singleton<DataManager>.Instance.dDataplayvideo[i.ToString()]["irand"]);
			list.Add(item);
		}
		int num = 0;
		for (int j = 0; j < list.Count; j++)
		{
			num += list[j] + 1;
		}
		int num2 = UnityEngine.Random.Range(0, num);
		num = 0;
		for (int k = 0; k < list.Count; k++)
		{
			num += list[k] + 1;
			if (num2 <= num)
			{
				return k + 1;
			}
		}
		return 1;
	}

	public void SetAdCount()
	{
		string interNetTime = Util.getInterNetTime();
		if (Singleton<DataManager>.Instance.bAdRewardHome)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_OpenADReward_Home" + interNetTime);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_OpenADReward_Home" + interNetTime, @int + 1);
		}
		if (Singleton<DataManager>.Instance.bAdRewardLose)
		{
			int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_OpenADReward_Lose" + interNetTime);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_OpenADReward_Lose" + interNetTime, int2 + 1);
		}
		if (Singleton<DataManager>.Instance.bAdRewardPlay)
		{
			int int3 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_OpenADReward_Play" + interNetTime);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_OpenADReward_Play" + interNetTime, int3 + 1);
		}
	}

	public void CheckQQqun()
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	AndroidJavaObject @static = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//	@static.Call("checkqqqun");
		//}
	}

	public void ClickAddQQun()
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	AndroidJavaObject @static = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//	@static.Call("AddQun");
		//}
	}

	public void checkshowBuyButton()
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	AndroidJavaObject @static = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//	@static.Call("checkshowBuyButton");
		//}
	}

	public void ChangeText()
	{
		if ((bool)SaleAdUI.action)
		{
			SaleAdUI.action.ChangeText();
		}
		if ((bool)BuySkillUIChina.action)
		{
			BuySkillUIChina.action.ChangeText();
		}
		if ((bool)BuyGangUI.action)
		{
			BuyGangUI.action.ChangeText();
		}
		if ((bool)NowBuyBubbleUI.action)
		{
			NowBuyBubbleUI.action.ChangeText();
		}
		if ((bool)BuyBubbleUI.action)
		{
			BuyBubbleUI.action.ChangeText();
		}
	}

	public void PlayVideoHG(bool _bShop = false)
	{
		bshop = _bShop;
		//UnityEngine.Debug.Log("Jy PlayVideoHG  bShop" + bshop);
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	AndroidJavaObject @static = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//	Singleton<DataManager>.Instance.isrewardad = true;
		//	@static.Call("PlayVideoHG");
		//}
		//else if (Application.platform == RuntimePlatform.WindowsEditor)
		//{
			voidvideoReward();
		//}
		int nowTime = Util.GetNowTime();
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_showBannerHGTime", nowTime);
	}

	public void ReturnVideoHG(string sindex)
	{
		FirebaseController.PlayVideoAds();
		UnityEngine.Debug.Log("ReturnVideoHG1111111111");
		if ((bool)GameUI.action && GameUI.action.isZhiJieBoFang)
		{
			UnityEngine.Debug.Log("ReturnVideoHG2222222222222222");
			GameUI.action.isZhiJieBoFang = false;
			GameUI.action.buyBuyBuShu++;
			FirebaseController.VideoAddFiveStep();
			Singleton<LevelManager>.Instance.iBubbleCount = Singleton<LevelManager>.Instance.iBubbleCount + 5;
			Singleton<LevelManager>.Instance.iBubbleUseCount = 0;
			PassLevel.action.KillLiuhan();
			GameUI.action.LoadBubbleCount();
			BubbleSpawner.Instance.initReadyBubble(isusekey: false);
			GameUI.action.KillNowBuyBubble();
		}
		else
		{
			UnityEngine.Debug.Log("ReturnVideoHG55555555555555555");
			if (DataManager.isShopAd)
			{
				UnityEngine.Debug.Log("ReturnVideoHG3333333333333333");
				voidvideoReward();
			}
			else if ((bool)NowBuyBubbleUIPanel.panel)
			{
				UnityEngine.Debug.Log("ReturnVideoHG4444444444444");
				NowBuyBubbleUIPanel.panel.AdReward();
			}
		}
	}

	public void voidvideoReward()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_VideoIndex", 1);
		UnityEngine.Debug.Log("Jy iVideoIndex  =" + @int);
		if (@int > 0 && @int < 10)
		{
			int num = int.Parse(Singleton<DataManager>.Instance.dDatavideoReward[@int.ToString()]["sReward"].Split('|')[0]);
			int num2 = int.Parse(Singleton<DataManager>.Instance.dDatavideoReward[@int.ToString()]["sReward"].Split('|')[1]);
			if ((bool)MapUI.action)
			{
				ChinaPay.action.addRewardAll(num, num2, MapUI.action.gameObject, isShow: false, "free", "advideo");
				BaseUIAnimation.action.ShowProp(num, num2, MapUI.action.gameObject);
			}
			else if ((bool)GameUI.action)
			{
				ChinaPay.action.addRewardAll(num, num2, GameUI.action.gameObject, isShow: false, "free", "advideo");
				BaseUIAnimation.action.ShowProp(num, num2, GameUI.action.gameObject);
			}
			SetNextRewardTime();
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_VideoIndex", @int + 1);
			if (@int >= 9)
			{
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_VideoIndex", 1);
			}
			if ((bool)VideoRewardPanel.panel)
			{
				VideoRewardPanel.panel.OnCloseButton();
			}
		}
		if ((bool)ChinaShopPanel.panel)
		{
			ChinaShopPanel.panel.ResAdReward();
		}
	}

	public void SetNextRewardTime()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_VideoIndex", 1);
		int num = int.Parse(Singleton<DataManager>.Instance.dDatavideoReward[@int.ToString()]["itime"]);
		int value = Util.GetNowTime() + num;
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_VideoNextTime", value);
	}

	public void adreturn0()
	{
		Singleton<DataManager>.Instance.iNoticePanelType = 3;
		UI.Instance.OpenPanel(UIPanelType.NoticePanel);
	}

	public IEnumerator firebasereturnIE(int index)
	{
		UnityEngine.Debug.Log("firebasereturnIE= " + index);
		yield return new WaitForSeconds(0.5f);
		int icount = 10;
		while ((bool)NoticePanel.panel && icount > 0)
		{
			UnityEngine.Debug.Log("firebasereturnIE= while  )");
			icount--;
			yield return new WaitForSeconds(0.5f);
		}
		UnityEngine.Debug.Log("firebasereturn= " + index);
		if (index == 1)
		{
			Singleton<DataManager>.Instance.bNoticePanelType = false;
			Singleton<DataManager>.Instance.iNoticePanelType = 5;
			UI.Instance.OpenPanel(UIPanelType.NoticePanel);
		}
		if (index == 2)
		{
			Singleton<DataManager>.Instance.bNoticePanelType = false;
			Singleton<DataManager>.Instance.iNoticePanelType = 6;
			UI.Instance.OpenPanel(UIPanelType.NoticePanel);
		}
		if (index == 3)
		{
			Singleton<DataManager>.Instance.bNoticePanelType = false;
			Singleton<DataManager>.Instance.iNoticePanelType = 8;
			UI.Instance.OpenPanel(UIPanelType.NoticePanel);
		}
		if (index == 4)
		{
			Singleton<DataManager>.Instance.bNoticePanelType = false;
			Singleton<DataManager>.Instance.iNoticePanelType = 9;
			UI.Instance.OpenPanel(UIPanelType.NoticePanel);
		}
	}

	public void FilebaseLoad()
	{
		//if ((bool)FireBase.Action)
		//{
		//	string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_GoogleID", string.Empty);
		//	if (!(@string == string.Empty) && Application.platform == RuntimePlatform.Android)
		//	{
		//		AndroidJavaObject @static = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//		@static.Call("DownLoadGoogleScore", @string);
		//	}
		//}
	}

	public void PlayInterstitialAd()
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	AndroidJavaObject @static = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//	@static.Call("PlayInterstitialAd");
		//}
	}

	public void SetPush(string on)
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	AndroidJavaObject @static = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//	@static.Call("SettingPush", on);
		//}
	}

	public void FileBaseSave()
	{
		//UnityEngine.Debug.Log("Jy FileBaseSave1");
		//AndroidJavaObject @static = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//@static.Call("firebaseSave");
		if ((bool)FireBase.Action)
		{
			UnityEngine.Debug.Log("Jy FileBaseSave2");
			string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_GoogleID", string.Empty);
			if (@string == string.Empty)
			{
				UnityEngine.Debug.Log("Jy FileBaseSave3");
				return;
			}
			UnityEngine.Debug.Log("Jy FileBaseSave4");
			FireBase.Action.UpdateAllScoreData();
		}
		UI.Instance.ClosePanel();
	}

	public void firebasereturn(int index)
	{
		DDOLSingleton<CoroutineController>.Instance.StartCoroutine(firebasereturnIE(index));
	}

	public void openTest()
	{
		Singleton<DataManager>.Instance.bhanguoTestConfig = true;
	}

	public void openCenterad()
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	AndroidJavaObject @static = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//	@static.Call("PlayCenterBannerHG");
		//}
	}

	public void CloseCenterad()
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	AndroidJavaObject @static = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//	@static.Call("HideCenterBannerHG");
		//}
	}

	public bool bLoginState()
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	AndroidJavaObject @static = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//	string a = @static.Call<string>("CheckLogin", new object[0]);
		//	if (a == "signIn")
		//	{
		//		return true;
		//	}
		//	return false;
		//}
		return false;
	}

	public void ggLogin_in()
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	AndroidJavaObject @static = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//	@static.Call("signIn");
		//}
	}

	public void ggLogin_out()
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	AndroidJavaObject @static = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//	@static.Call("signOut");
		//}
	}
}
