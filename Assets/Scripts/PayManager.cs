using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
//using Umeng;
using UnityEngine;

public class PayManager : MonoBehaviour
{
	public static PayManager action;

	public bool OpenPay = true;

	public bool InitOver;

	public float Static_fPayGOLDCount;

	private bool Paying;

	private void Start()
	{
		action = this;
		//if (OpenPay)
		//{
		//	try
		//	{
		//		if (Application.platform == RuntimePlatform.IPhonePlayer)
		//		{
		//			InitIAPManager();
		//		}
		//	}
		//	catch (Exception)
		//	{
		//	}
		//}
		InitOver = true;
		//if (Application.platform == RuntimePlatform.WindowsPlayer)
		//{
		//	string str = "com.enp.bubble.princess.pop.diamond156#US$0.99|com.enp.bubble.princess.pop.diamond318#US$1.99|com.enp.bubble.princess.pop.diamond858#US$4.99|com.enp.bubble.princess.pop.diamond2088#US$9.99|com.enp.bubble.princess.pop.diamond3988#US$19.99|com.enp.bubble.princess.pop.diamond10388#US$49.99|com.enp.bubble.princess.pop.package1#US$0.99|com.enp.bubble.princess.pop.package2#US$0.99|com.enp.bubble.princess.pop.package3#US$2.99|com.enp.bubble.princess.pop.package4#US$4.99|com.enp.bubble.princess.pop.prop1#US$0.99|com.enp.bubble.princess.pop.prop2#US$0.99|com.enp.bubble.princess.pop.prop3#US$0.99|com.enp.bubble.princess.pop.skill1#US$0.99|com.enp.bubble.princess.pop.skill2#US$0.99|com.enp.bubble.princess.pop.skill3#US$0.99|com.enp.bubble.princess.pop.skill4#US$0.99|com.enp.bubble.princess.pop.move#US$0.99|com.enp.bubble.princess.pop.diamond300#US$0.99|com.enp.bubble.princess.pop.diamond600#US$1.99|com.enp.bubble.princess.pop.diamond1500#US$4.99|com.enp.bubble.princess.pop.diamond3400#US$9.99|com.enp.bubble.princess.pop.diamond6400#US$19.99|com.enp.bubble.princess.pop.diamond16400#US$49.99|com.enp.bubble.princess.pop.adsfree#US$1.99";
		//	CommodityPricesSave(str);
		//}
		Static_fPayGOLDCount = Singleton<TestScript>.Instance.GetFloat(DataManager.SDBNO + "DB_PayGOLDCount");
	}

	public bool CheckUserisBigMoney()
	{
		if (Static_fPayGOLDCount > 49.99f)
		{
			return true;
		}
		return false;
	}

	private void Update()
	{
	}

	public void ResultPay(string key)
	{
		InitAndroid.action.onPaySuccessChina(key, string.Empty);
	}

	public void AddPayCountMoney(float fmoney)
	{
		float @float = Singleton<TestScript>.Instance.GetFloat(DataManager.SDBNO + "DB_PayGOLDCount");
		@float += fmoney;
		FirebaseController.Buy((int)(fmoney * 100f));
		Singleton<DataManager>.Instance.SaveUserDate("DB_PayGOLDCount", @float);
		Static_fPayGOLDCount = @float;
	}

	public void Pay(string key)
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_PayLastTime");
		int nowTime = Util.GetNowTime();
		if (@int > 0 && nowTime - @int < 5)
		{
			return;
		}
		float num = float.Parse(Singleton<DataManager>.Instance.dDataPayGold[key]["iMoney"]);
		int numItems = int.Parse(Singleton<DataManager>.Instance.dDataPayGold[key]["iGold"]);
		FaceBookApi.Action.LogInitiatedCheckoutEvent(key, key, numItems, paymentInfoAvailable: true, key, num);
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_PayLastTime", nowTime);
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
		{
			return;
		}
		if (InitGame.bChinaVersion && Singleton<DataManager>.Instance.bChinaIos)
		{
			string s = Singleton<DataManager>.Instance.dDataPayGold[key]["ioskey"];
			if (InitGame.bEnios)
			{
				s = Singleton<DataManager>.Instance.dDataPayGold[key]["ioskeyus"];
			}
			//BuyProduct(s);
		}
		else
		{
			//BuyProduct(key);
		}
	}

	public void BuyDaoju(int iGodl)
	{
		ExpendGDP(iGodl, GDPType.BUGDAOJU, 1);
	}

	public void AddGB(int iGodl, int iGB)
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GB");
		@int += iGB;
		Singleton<DataManager>.Instance.SaveUserDate("DB_GB", @int);
		ExpendGDP(iGodl, GDPType.BUYGB, iGB);
	}

	public void UseGB(int GB)
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GB");
		@int -= GB;
		Singleton<UserManager>.Instance.SetPassTask("USE_GB", GB);
		Singleton<DataManager>.Instance.SaveUserDate("DB_GB", @int);
	}

	public void ExpendGDP(int iMoney, GDPType _GDPType, int iCount, int iSkillType = 0, bool playmap3 = true)
	{
		if (iMoney > 0)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
			int oldGold = @int;
			@int -= iMoney;
			Singleton<DataManager>.Instance.SaveUserDate("DB_GOLD", @int);
			FirebaseController.AddOrSubStone(-iMoney);
			LOG_USEGOLD(_GDPType, iSkillType, iMoney, oldGold, @int, iCount);
			Singleton<UserManager>.Instance.SetPassTask("Use_diamond", iMoney);
			if (_GDPType != GDPType.BUYLOVE && iMoney > 0 && playmap3)
			{
				SoundController.action.playNow("ui_coin");
			}
		}
	}

	public void AwardAddGold(int iGold, string sRemark)
	{
		UnityEngine.Debug.Log("AwardAddGold=" + iGold);
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		int num = @int;
		@int += iGold;
		Singleton<DataManager>.Instance.SaveUserDate("DB_GOLD", @int);
		FirebaseController.AddOrSubStone(iGold);
		if (sRemark == "FaceBookShareAward")
		{
			//GA.Bonus(iGold, GA.BonusSource.Source2);
		}
		else if (sRemark == "FaceBookLoginAward")
		{
			//GA.Bonus(iGold, GA.BonusSource.Source3);
		}
		else if (sRemark == "NewUser")
		{
			//GA.Bonus(iGold, GA.BonusSource.Source4);
		}
		else if (sRemark == "QIANDAO")
		{
			//GA.Bonus(iGold, GA.BonusSource.Source6);
		}
		else if (sRemark == "FACEBOOKFRIEND")
		{
			//GA.Bonus(iGold, GA.BonusSource.Source7);
		}
		else if (sRemark == "cdkey")
		{
			//GA.Bonus(iGold, GA.BonusSource.Source8);
		}
		else
		{
			//GA.Bonus(iGold, GA.BonusSource.Source5);
		}
		if ((bool)FireBase.Action)
		{
			//FireBase.Action.UnityWriteLog("LOG_AWARDADDGOLD", iGold + "|" + sRemark + "|" + num + "|" + @int);
		}
	}

	public void LOG_USEGOLD(GDPType _GDPType, int Type2, int iMoney, int OldGold, int iMyGold, int iCount)
	{
		int gameScene = Singleton<SceneManager>.Instance.GetGameScene();
		int userLevel = Singleton<UserLevelManager>.Instance.GetUserLevel();
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
		if (gameScene == 1)
		{
			@int = Singleton<LevelManager>.Instance.iNowSelectLevelIndex;
			if (@int < 31)
			{
				userLevel = 0;
			}
		}
		else if (@int < 31)
		{
			userLevel = 0;
		}
		//GA.Buy(_GDPType + "_" + Type2, iCount, iMoney);
	}

	public void BuySkill(int iSkillType, int iMoney, int GB, int iNunb)
	{
		//Analytics.Event("PaySkill" + iSkillType);
		//InitAndroid.action.GAEvent("Buyskill" + iSkillType);
		//InitAndroid.action.GAEvent("NewBuySkill:" + iSkillType + ":" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
		if (GB > 0)
		{
			UseGB(GB);
			if ((bool)action)
			{
				action.AddSkill(iSkillType, iNunb, "GBBuy");
			}
			LoadGold(bType: false);
			LoadSkill(iSkillType);
		}
		else
		{
			ExpendGDP(iMoney, GDPType.BUYSKILL, iNunb, iSkillType);
			if ((bool)action)
			{
				action.AddSkill(iSkillType, iNunb, "Buy");
			}
			LoadGold(bType: false);
			LoadSkill(iSkillType);
		}
	}

	public void PayZhuanpan(int iMoney, int GB, int iNunb)
	{
		if (GB > 0)
		{
			UseGB(GB);
			LoadGold(bType: false);
		}
		else
		{
			ExpendGDP(iMoney, GDPType.ZHUANPAN, iNunb, 0, playmap3: false);
			LoadGold(bType: false);
		}
	}

	public void BuyGang(int iGangType, int iMoney, int iNunb)
	{
		ExpendGDP(iMoney, GDPType.BUYGANG, iNunb, iGangType);
		BubbleSpawner.Instance.buyskillGang.GetComponent<MuTong>().AddSkill();
	}

	public void BuyBuqian(int iMoney)
	{
		ExpendGDP(iMoney, GDPType.BUYBUQIANDAO, 1, 1);
		LoadGold(bType: false);
	}

	public void LoadSkill(int iSkillType)
	{
		if ((bool)GameUI.action)
		{
			GameUI.action.ResSkillCount(iSkillType);
		}
	}

	public void BuyLove(int iMoney, int iLove)
	{
		Singleton<UserLevelManager>.Instance.UseAdded();
		ExpendGDP(iMoney, GDPType.BUYLOVE, iLove);
		if (InitGame.bChinaVersion)
		{
			Singleton<LevelManager>.Instance.AddLove(Singleton<DataManager>.Instance.iLoveMaxAll);
		}
		else
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_LoveCount", Singleton<DataManager>.Instance.iLoveMaxAll);
			Singleton<DataManager>.Instance.SaveUserDate("DB_FullLoveTime", 0);
		}
		LoadLove();
	}

	public void BuyBubble(int iMoney, int iBubble)
	{
		Singleton<UserLevelManager>.Instance.UseAdded();
		ExpendGDP(iMoney, GDPType.BUYBUBBLE, iBubble);
		if (!PassLevel.action)
		{
		}
	}

	public void DarePay(int iMoney)
	{
		ExpendGDP(iMoney, GDPType.ZHUANPAN, 1);
	}

	public void AddSkill(int iType, int iCount, string remark)
	{
		if (InitGame.bChinaVersion && (iCount == 100 || iCount == 200))
		{
			string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_Skill_Count_Time_" + iType, string.Empty);
			int num = Util.GetNowTime() + 86400;
			if (iCount == 200)
			{
				num = Util.GetNowTime() + 7200;
			}
			if (@string == string.Empty)
			{
				Singleton<TestScript>.Instance.SetString(DataManager.SDBNO + "DB_Skill_Count_Time_" + iType, num.ToString());
				return;
			}
			@string = @string + "," + num.ToString();
			Singleton<TestScript>.Instance.SetString(DataManager.SDBNO + "DB_Skill_Count_Time_" + iType, @string);
		}
		else
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Skill_Count_" + iType);
			@int += iCount;
			Singleton<DataManager>.Instance.SaveUserDate("DB_Skill_Count_" + iType, @int);
			FirebaseController.AddOrSubProp(iType, iCount);
		}
	}

	public void Addlvye(int iCount)
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LvyeZhuanpan");
		@int += iCount;
		Singleton<DataManager>.Instance.SaveUserDate("DB_LvyeZhuanpan", @int);
		if ((bool)zhuanpan.action)
		{
			zhuanpan.action.CheckGuang();
		}
	}

	public int GetSkillCount(int iType)
	{
		int num = 0;
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Skill_Count_" + iType);
		num += @int;
		if (InitGame.bChinaVersion)
		{
			num += GetSkillTimeCount(iType);
		}
		return num;
	}

	public int GetSkillTimeCount(int iType)
	{
		if (!InitGame.bChinaVersion)
		{
			return 0;
		}
		int num = 0;
		string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_Skill_Count_Time_" + iType, string.Empty);
		if (@string != string.Empty)
		{
			for (int num2 = 0; num2 < @string.Split(',').Length; num2++)
			{
				string text = @string.Split(',')[num2];
				if (text.Length > 3)
				{
					int num3 = int.Parse(text);
					num3 -= Util.GetNowTime();
					if (num3 > 60)
					{
						num++;
					}
				}
			}
		}
		return num;
	}

	public void DeleteSkill(int iType)
	{
		if (iType <= 3)
		{
			Singleton<UserManager>.Instance.SetPassTask1("Use_gbdj");
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "FirstUse_gbdj" + iType) == 0)
			{
				InitAndroid.action.GAEvent("Use:First:GB:iType:" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "FirstUse_gbdj" + iType, 1);
			}
			else
			{
				InitAndroid.action.GAEvent("Use:GB:" + iType + ":" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
			}
		}
		else
		{
			Singleton<UserManager>.Instance.SetPassTask1("Use_zsdj");
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "FirstUse_zsdj" + iType) == 0)
			{
				InitAndroid.action.GAEvent("Use:First:ZS:iType:" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "FirstUse_zsdj" + iType, 1);
			}
			else
			{
				InitAndroid.action.GAEvent("Use:ZS:" + iType + ":" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
			}
		}
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_UseDaoju" + iType);
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_UseDaoju" + iType, @int + 1);
		if (InitGame.bChinaVersion)
		{
			int num = 0;
			string oldValue = string.Empty;
			string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_Skill_Count_Time_" + iType, string.Empty);
			if (@string != string.Empty)
			{
				for (int num2 = 0; num2 < @string.Split(',').Length; num2++)
				{
					string text = @string.Split(',')[num2];
					if (text.Length > 3)
					{
						int num3 = int.Parse(text);
						num3 -= Util.GetNowTime();
						if (num3 > 60)
						{
							num++;
							oldValue = text;
							break;
						}
					}
				}
			}
			if (num > 0)
			{
				@string = @string.Replace(oldValue, string.Empty);
				Singleton<TestScript>.Instance.SetString(DataManager.SDBNO + "DB_Skill_Count_Time_" + iType, @string);
				return;
			}
		}
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Skill_Count_" + iType);
		int2--;
		if (int2 >= 0)
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_Skill_Count_" + iType, int2);
			FirebaseController.AddOrSubProp(iType, -1);
		}
	}

	private void LoadLove()
	{
		if ((bool)MapUI.action)
		{
			MapUI.action.LoadLove();
		}
	}

	public void LoadGold(bool bType = true)
	{
		if ((bool)MapUI.action)
		{
			if ((bool)MapUI.action)
			{
				MapUI.action.LoadGold(bUpdate: true, bType);
			}
			if ((bool)BuySkillUI.action)
			{
				BuySkillUI.action.LoadGold();
			}
			if ((bool)BuyBubbleUI.action)
			{
				BuyBubbleUI.action.LoadGold();
			}
			if ((bool)NowBuyBubbleUI.action)
			{
				NowBuyBubbleUI.action.LoadGold();
			}
			if ((bool)MapUI.action)
			{
				MapUI.action.LoadGB(bUpdate: true);
			}
			if (Singleton<UserManager>.Instance.getLoveInfinite() <= 0)
			{
				MapUI.action.LoadLove();
				return;
			}
			MapUI.action.LoveInfiniteTimeObj.SetActive(value: true);
			MapUI.action.LoveTimeObj.SetActive(value: false);
			MapUI.action.LoadLoveInfiniteTime();
			MapUI.action._IEUpdateLoveInfinite();
		}
	}

	public void CommodityPricesSave(string str)
	{
		UnityEngine.Debug.Log(" 2132133333       " + str);
		Singleton<DataManager>.Instance.CommodityPricesDic = new Dictionary<string, string>();
		string[] array = str.Split('|');
		for (int i = 0; i < array.Length; i++)
		{
			UnityEngine.Debug.Log(" str1[i]=" + array[i]);
			string[] array2 = array[i].Split('#');
			UnityEngine.Debug.Log(" str2=" + array2);
			Singleton<DataManager>.Instance.CommodityPricesDic.Add(array2[0], array2[1]);
		}
		foreach (KeyValuePair<string, string> item in Singleton<DataManager>.Instance.CommodityPricesDic)
		{
			UnityEngine.Debug.Log("Jy  CommodityPricesSave=" + item.Key + "|" + item.Value + "\n");
		}
	}

	private void Awake()
	{
		if (action == null)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			action = this;
		}
		else if (action != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		Test();
	}

	public void SetHG_User_ltvData_props(int index, int num)
	{
		string text = "1/1/1/11/1/1/1";
		string[] array = text.Split('/');
		text = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			int num2 = int.Parse(array[i]);
			if (i + 1 == index)
			{
				num2 = num;
			}
			text += num2.ToString();
			if (i < array.Length - 1)
			{
				text += "/";
			}
		}
		UnityEngine.Debug.Log(text);
	}

	private void Test()
	{
	}



	public void _UBuyProduct(string s)
	{
		InitAndroid.action.ShowPayMask();
		//BuyProduct(s);
	}

	public void SaveGoogleID(string uid)
	{
		Singleton<TestScript>.Instance.SetString(DataManager.SDBNO + "DB_GoogleID", uid);
	}

	public void sign(string _sign)
	{
	}

	public void GoogleCallBack(string callback)
	{
		string[] array = callback.Split('#');
		string text = array[0];
		string text2 = array[1];
		string text3 = array[2];
		string text4 = array[3];
		StartCoroutine(DoPay(text, text3, text2, text4));
		UnityEngine.Debug.Log("PayKey = " + text + " =====  productId  ===  " + text3 + " packageName =  " + text2 + "  token =  " + text4);
	}

	private IEnumerator DoPay(string PayKey, string productId, string packageName, string token)
	{
		UnityEngine.Debug.Log("   ====  DoPay ======");
		yield return null;
		ResultPay(PayKey);
	}

	public void ResultVideo(string index)
	{
		UnityEngine.Debug.Log("ResultVideo   ====  " + index);
		if (index == "999999")
		{
			UnityEngine.Debug.Log("ResultVideo   2====  " + index);
			VideoCallBack();
		}
		else
		{
			UnityEngine.Debug.Log("ResultVideo");
		}
	}

	public void VideoCallBack()
	{
		UnityEngine.Debug.Log("ResultVideo  3 ====  ");
	}

	public bool CheckOpenSign()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
		if (@int < 11)
		{
			return false;
		}
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iqiandao7Count", 1);
		if (int2 <= 7)
		{
			string nowTime_Day = Util.GetNowTime_Day();
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SignOK7" + nowTime_Day) == 0 && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SignOKDay1" + nowTime_Day) == 0)
			{
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_SignOKDay1" + nowTime_Day, 1);
				UI.Instance.OpenPanel(UIPanelType.Qiandao7Panel);
				return true;
			}
		}
		if (int2 > 7)
		{
			int int3 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "ToDayAutoSingnin");
			if (int3 == 1 && Util.CheckOnline())
			{
				string nowTime_Day2 = Util.GetNowTime_Day();
				if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SignRewardDay31" + nowTime_Day2) == 0 && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SignOK32Day1" + nowTime_Day2) == 0)
				{
					Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_SignOK32Day1" + nowTime_Day2, 1);
					Singleton<DataManager>.Instance.bAutoOpenSigninUI = true;
					UI.Instance.OpenPanel(UIPanelType.SignRewardUI);
					return true;
				}
			}
		}
		return false;
	}

	public void OpenSignOrPlay()
	{
		if (CheckOpenSign())
		{
			Singleton<DataManager>.Instance.bOpenPlayforSign = true;
		}
		else if (UI.Instance.GetPanelCount() <= 0)
		{
			UI.Instance.OpenPanel(UIPanelType.Play);
		}
	}
}
