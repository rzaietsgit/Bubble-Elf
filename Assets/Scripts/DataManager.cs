using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
	public enum dDataType
	{
		dDataGameObj
	}

	public bool isrewardad;

	public bool bdownloadgoogle;

	public bool bopenMaxGuide;

	public static bool bisTestPack = false;

	public static bool isShopAd = false;

	public static string umengKey = string.Empty;

	public static string ChannelId = "App Store";

	public static string IosAppLink = "https://itunes.apple.com/us/app/bubble-elf2/id1172659057";

	public static string AndroidAppLink = "https://play.google.com/store/apps/details?id=com.bubbleshooter.shooting.balls.free&hl=en";

	public static string IosFacebookLink = "https://fb.me/383775315290998";

	public static string IosAppIcon = "https://lh3.googleusercontent.com/_xM0roKI8rPQT0-5dPNehAJ6-5iGnnER_0PWqH3ENejekND-ZdoZis4cU204zYfheA=w300-rw";

	public static string MACCODE = string.Empty;

	public static string errorhttp = "http://jyerrorpaopao.unitygame8.com/uperror.php?Jyerror=";

	public bool bPayState = true;

	public bool bGooglePay = true;

	public bool bgooglejy = true;

	public string sversionCode = "0";

	public int iWinNum;

	public bool bChinaIos = true;

	public int iDareCount;

	public int iDareScore;

	public static bool bInGameOk = false;

	public int iDareBushu;

	public bool bLiuhai;

	public bool SaleChangeMapWin;

	public bool SaleChangeMapLose;

	public int ivo_role_shoot = 20;

	public int ivo_role_save = 60;

	public int ivo_role_cheers = 90;

	public bool bAdRewardPlay;

	public bool bAdRewardLose;

	public bool bAdRewardHome;

	public bool bShowAdisVideo;

	public bool bFirstLose = true;

	public bool bPlayzhizhuOne = true;

	public bool bAutoOpen;

	public bool bhanguoTestConfig = true;

	public bool bOpenReward7;

	public bool isLaunch = true;

	public bool bzhengce;

	public bool bOpenPlayforSign;

	public static bool bPc = false;

	public static bool bbeibaoFlay = true;

	public bool bShowSkill;

	public int bForced_guidance;

	public static int iSkillOpenType = 1;

	public static string SDBNO = "168";

	public bool bShowHaopingLogin = true;

	public bool StarGameFlage;

	public bool bOpenNewPayMode = true;

	public int iLoveMaxAll = 5;

	public bool bChinaShopUIopen1;

	public bool bGrilMoveing;

	public bool bBuyLiveSale;

	public EnumUIType EBuyLiveSale = EnumUIType.None;

	public bool bshowLoginButton;

	public bool bInitUnityConfig1;

	public bool bInitUnityConfig2;

	public bool bInitUnityConfig3;

	public bool byaping;

	public bool bhaoping;

	public int iLoveUse = 1;

	public int iSelectMapRewardID = 1;

	public bool bAutoOpenSigninUI;

	public bool bCloseSaleOpenPlayUI;

	public bool bClosePlayUIOpenSale;

	public bool bSaleAdPay2;

	public bool bNewTaskOpenPlay;

	public bool bLevel3OpenPlay;

	public int iLevelRewardID;

	public int iLevelRewardCount;

	public int iLevelRewardLevelID;

	public bool bRateUsUIOpen;

	public bool bLogo = true;

	public bool bLogo_ = true;

	public bool bplayExitMap;

	public bool bAutoPlayGame;

	public bool bSigninUICloseOpen;

	public Hashtable LevelRewardDataCache;

	public bool PlayOpenBuySkill;

	public bool bOpenplay1;

	public static string sale_adKey = string.Empty;

	public bool bopenBuySkillUI;

	public bool bopenBuySkillUI1;

	public bool bBuyLi1;

	public bool bBuyLi2;

	public bool bBuyLB;

	public static int SelectLevel = 0;

	private const string path = "Assets/Resources/Data/";

	public Dictionary<string, Dictionary<string, string>> dBubble;

	public Dictionary<string, Dictionary<string, string>> dDataLevelTypeAndRemark;

	public bool FacebookLoginLog;

	public static int iFirstLoginGameLoadCloud = 0;

	public bool ChinaShopOpenZuanshi;

	public bool ChinaShopOpen;

	public bool ChinaShopOpendaoju;

	public bool bLoginGame;

	public string BuyDaojuID = string.Empty;

	public bool ChinaShopbGBBuy;

	public EnumUIType NextOpenUI = EnumUIType.None;

	public EnumUIType OpenPlayForLive = EnumUIType.None;

	public static string PAGE_SWITCHIN = "switchin";

	public static string PAGE_PAY = "pay";

	public static string PAGE_SPLASH = "splash";

	public static string PAGE_HOME = "home";

	public static string PAGE_MAIN = "main";

	public static string PAGE_PAUSE = "pause";

	public static string PAGE_EXIT = "exit";

	public static string PAGE_SUCCESS = "success";

	public static string PAGE_FAIL = "failed";

	public static string PAGE_GIFT = "gift";

	public bool bexitGameScene;

	public bool bcdkeyReward;

	public int cdkeys_key;

	public Dictionary<string, Dictionary<string, string>> dDataPayGold;

	public Dictionary<string, Dictionary<string, string>> dDataMapBtnConfig;

	public int iNoticePanelType;

	public bool bNoticePanelType;

	public Dictionary<string, Dictionary<string, string>> dDataplayvideo;

	public Dictionary<string, Dictionary<string, string>> dDataMapBtnConfig_min;

	public Dictionary<string, string> CommodityPricesDic;

	public Dictionary<string, Dictionary<string, string>> dDataAudio;

	public bool bclicktow;

	public bool bclickkuaisudianji;

	public Dictionary<string, Dictionary<string, string>> dDataUserLevel;

	public Dictionary<string, Dictionary<string, string>> dDatashoplb;

	public Dictionary<string, Dictionary<string, string>> dDataLanguage;

	public Dictionary<string, Dictionary<string, string>> dLoginDataLanguage;

	public Dictionary<string, Dictionary<string, string>> dDataLanguageStyle;

	public Dictionary<string, Dictionary<string, string>> dDataSignin;

	public Dictionary<string, Dictionary<string, string>> dDataSigninGG;

	public Dictionary<string, Dictionary<string, string>> dDataSigninCount;

	public Dictionary<string, Dictionary<string, string>> dDataBuyDaojuList;

	public Dictionary<string, Dictionary<string, string>> dDataBuyDaojuRemark;

	public Dictionary<string, Dictionary<string, string>> dDatazhuanpan;

	public Dictionary<string, Dictionary<string, string>> dDatazhuanpanMoney;

	public Dictionary<string, Dictionary<string, string>> dDataLotteryNameTest;

	public Dictionary<string, Dictionary<string, string>> dDataTaskList;

	public Dictionary<string, Dictionary<string, string>> dDataNewTaskList;

	public Dictionary<string, Dictionary<string, string>> dDataTaskList1;

	public Dictionary<string, Dictionary<string, string>> dDataMapReward;

	public Dictionary<string, Dictionary<string, string>> dDataChinaPay;

	public Dictionary<string, Dictionary<string, string>> dDataSkillPriceChina;

	public Dictionary<string, Dictionary<string, string>> dDataSkillPrice;

	public Dictionary<string, Dictionary<string, string>> dDataLevelReward;

	public Dictionary<string, Dictionary<string, string>> dDataMapicon;

	public Dictionary<string, Dictionary<string, string>> dDataChinaBuyLive;

	public Dictionary<string, Dictionary<string, string>> dDataSignmap31;

	public Dictionary<string, Dictionary<string, string>> dDataSignmap31Reward;

	public Dictionary<string, Dictionary<string, string>> dDataSignmap7;

	public Dictionary<string, Dictionary<string, string>> dDataSystemConfig;

	public Dictionary<string, Dictionary<string, string>> dDataHua1;

	public Dictionary<string, Dictionary<string, string>> dDataHua2;

	public Dictionary<string, Dictionary<string, string>> dDataHua3;

	public Dictionary<string, Dictionary<string, string>> dDataHua4;

	public Dictionary<string, Dictionary<string, string>> dDataHua5;

	public Dictionary<string, Dictionary<string, string>> dDataHua6;

	public Dictionary<string, Dictionary<string, string>> dDatavideoReward;

	public static string Net_Address = "file://" + Application.dataPath + string.Empty;

	public bool bUiIsOpen;

	private Hashtable htDataCache;

	public EnumSceneType ChangeSceneType;

	public int[] LMapEndBtnID;

	public int[] LMapStarBtnID;

	public bool isUpdateWinData;

	public bool Openplay1;

	public bool PlayGameOpenSale;

	public int[] LMapBtnCount;

	public bool bIELoadLanguageStyle;

	public Dictionary<int, int> dDataLevel_Map;

	public void LocalStaticLoadDataForLogin()
	{
		dLoginDataLanguage = LoadData("Assets/Resources/Data/", "LanguageLogin", bWWWload: false, string.Empty);
	}

	public void LocalStaticLoadDataAudio()
	{
		dDataAudio = LoadData("Assets/Resources/Data/", "Audio", bWWWload: false, string.Empty);
	}

	public void LocalStaticLoadData()
	{
		InitMapAndBtnData();
		if (!LevelManager.bWwwDataFlag)
		{
			dDataLanguage = LoadData("Assets/Resources/Data/", "Language", bWWWload: false, string.Empty);
			dDataLanguageStyle = LoadData("Assets/Resources/Data/", "LanguageStyle_Simplified_Chinese", bWWWload: false, string.Empty);
			dDataHua1 = LoadData("Assets/Resources/Data/", "hua1", bWWWload: false, string.Empty);
			dDataHua2 = LoadData("Assets/Resources/Data/", "hua2", bWWWload: false, string.Empty);
			dDataHua3 = LoadData("Assets/Resources/Data/", "hua3", bWWWload: false, string.Empty);
			dDataHua4 = LoadData("Assets/Resources/Data/", "hua4", bWWWload: false, string.Empty);
		}
		else
		{
			DDOLSingleton<CoroutineController>.Instance.StartCoroutine(IELoadData());
			DDOLSingleton<CoroutineController>.Instance.StartCoroutine(IELoadData1());
			DDOLSingleton<CoroutineController>.Instance.StartCoroutine(IELoadData2());
			DDOLSingleton<CoroutineController>.Instance.StartCoroutine(IELoadDataHua());
		}
		dDataHua5 = LoadData("Assets/Resources/Data/", "hua5", bWWWload: false, string.Empty);
		dDataHua6 = LoadData("Assets/Resources/Data/", "hua6", bWWWload: false, string.Empty);
		dDatavideoReward = LoadData("Assets/Resources/Data/", "videoReward", bWWWload: false, string.Empty);
		dDataPayGold = LoadData("Assets/Resources/Data/", "PayGold", bWWWload: false, string.Empty);
		dBubble = LoadData("Assets/Resources/Data/", "Bubble", bWWWload: false, string.Empty);
		dDataLevelTypeAndRemark = LoadData("Assets/Resources/Data/", "levelTypeAndRemark", bWWWload: false, string.Empty);
		dDataUserLevel = LoadData("Assets/Resources/Data/", "UserLevelManager", bWWWload: false, string.Empty);
		dDataMapBtnConfig = LoadData("Assets/Resources/Data/", "MapBtnConfig", bWWWload: false, string.Empty);
		dDataMapBtnConfig_min = LoadData("Assets/Resources/Data/", "MapBtnConfig_min", bWWWload: false, string.Empty);
		dDataplayvideo = LoadData("Assets/Resources/Data/", "playvideo", bWWWload: false, string.Empty);
		int month = DateTime.Now.Month;
		dDataSignin = LoadData("Assets/Resources/Data/", "Signin_" + month, bWWWload: false, string.Empty);
		dDataSigninCount = LoadData("Assets/Resources/Data/", "Signin", bWWWload: false, string.Empty);
		dDatashoplb = LoadData("Assets/Resources/Data/", "shoplb", bWWWload: false, string.Empty);
		dDataBuyDaojuList = LoadData("Assets/Resources/Data/", "BuyDaojuList", bWWWload: false, string.Empty);
		dDataSigninGG = LoadData("Assets/Resources/Data/", "signinGG", bWWWload: false, string.Empty);
		dDataBuyDaojuRemark = LoadData("Assets/Resources/Data/", "BuyDaojuRemark", bWWWload: false, string.Empty);
		if (bGooglePay)
		{
			dDatazhuanpan = LoadData("Assets/Resources/Data/", "zhuanpanGG", bWWWload: false, string.Empty);
			dDatazhuanpanMoney = LoadData("Assets/Resources/Data/", "zhuanpanMoneyGG", bWWWload: false, string.Empty);
		}
		else
		{
			dDatazhuanpan = LoadData("Assets/Resources/Data/", "zhuanpan", bWWWload: false, string.Empty);
			dDatazhuanpanMoney = LoadData("Assets/Resources/Data/", "zhuanpanMoney", bWWWload: false, string.Empty);
		}
		dDataLotteryNameTest = LoadData("Assets/Resources/Data/", "LotteryNameTest", bWWWload: false, string.Empty);
		dDataMapReward = LoadData("Assets/Resources/Data/", "MapReward", bWWWload: false, string.Empty);
		dDataTaskList = LoadData("Assets/Resources/Data/", "TaskList", bWWWload: false, string.Empty);
		dDataTaskList1 = LoadData("Assets/Resources/Data/", "TaskList1", bWWWload: false, string.Empty);
		dDataNewTaskList = LoadData("Assets/Resources/Data/", "NewTaskList", bWWWload: false, string.Empty);
		dDataChinaPay = LoadData("Assets/Resources/Data/", "ChinaPay", bWWWload: false, string.Empty);
		dDataSkillPriceChina = LoadData("Assets/Resources/Data/", "SkillPriceChina", bWWWload: false, string.Empty);
		dDataSkillPrice = LoadData("Assets/Resources/Data/", "SkillPrice", bWWWload: false, string.Empty);
		dDataLevelReward = LoadData("Assets/Resources/Data/", "LevelReward", bWWWload: false, string.Empty);
		dDataMapicon = LoadData("Assets/Resources/Data/", "Mapicon", bWWWload: false, string.Empty);
		dDataChinaBuyLive = LoadData("Assets/Resources/Data/", "ChinaBuyLive", bWWWload: false, string.Empty);
		dDataSystemConfig = LoadData("Assets/Resources/Data/", "SystemConfig", bWWWload: false, string.Empty);
		dDataSignmap31Reward = LoadData("Assets/Resources/Data/", "Signmap31Reward", bWWWload: false, string.Empty);
		dDataSignmap31 = LoadData("Assets/Resources/Data/", "Signmap31", bWWWload: false, string.Empty);
		dDataSignmap7 = LoadData("Assets/Resources/Data/", "Signmap7", bWWWload: false, string.Empty);
		initKeyCache();
	}

	public void LoadLanguageStyle()
	{
	}

	private IEnumerator IELoadLanguageStyle(string fileName)
	{
		string url = Net_Address + "/Data/" + fileName + ".txt";
		UnityEngine.Debug.Log("url>>" + url);
		WWW w = new WWW(url);
		yield return new WaitForSeconds(2f);
		string mapStringdata = w.text;
		UnityEngine.Debug.Log("mapStringdata>>" + mapStringdata);
		dDataLanguageStyle = LoadData("Assets/Resources/Data/", string.Empty, bWWWload: true, mapStringdata);
		UnityEngine.Debug.Log("url>>over");
		bIELoadLanguageStyle = true;
	}

	private IEnumerator IELoadData()
	{
		string url = Net_Address + "/Data/GameGuide.txt";
		WWW w = new WWW(url);
		yield return new WaitForSeconds(2f);
		string text = w.text;
	}

	private IEnumerator IELoadData1()
	{
		string empty = string.Empty;
		string url = Net_Address + "/Data/LanguageStyle" + BaseUIAnimation.Language;
		WWW w = new WWW(url);
		yield return new WaitForSeconds(2f);
		dDataLanguageStyle = LoadData(sWWWData: w.text, sPath: "Assets/Resources/Data/", sName: "LanguageStyle_" + BaseUIAnimation.Language, bWWWload: true);
	}

	private IEnumerator IELoadData2()
	{
		string url = Net_Address + "/Data/Language.txt";
		WWW w = new WWW(url);
		yield return new WaitForSeconds(2f);
		string mapStringdata = w.text;
		dDataLanguage = LoadData("Assets/Resources/Data/", "Language", bWWWload: true, mapStringdata);
		UnityEngine.Debug.Log("dDataLanguage" + dDataLanguage);
	}

	private IEnumerator IELoadDataHua()
	{
		string url4 = Net_Address + "/Data/hua1.txt";
		WWW w4 = new WWW(url4);
		yield return new WaitForSeconds(2f);
		string mapStringdata4 = w4.text;
		dDataHua1 = LoadData("Assets/Resources/Data/", "hua1", bWWWload: true, mapStringdata4);
		url4 = Net_Address + "/Data/hua2.txt";
		w4 = new WWW(url4);
		yield return new WaitForSeconds(2f);
		mapStringdata4 = w4.text;
		dDataHua2 = LoadData("Assets/Resources/Data/", "hua2", bWWWload: true, mapStringdata4);
		url4 = Net_Address + "/Data/hua3.txt";
		w4 = new WWW(url4);
		yield return new WaitForSeconds(2f);
		mapStringdata4 = w4.text;
		dDataHua3 = LoadData("Assets/Resources/Data/", "hua3", bWWWload: true, mapStringdata4);
		url4 = Net_Address + "/Data/hua4.txt";
		w4 = new WWW(url4);
		yield return new WaitForSeconds(2f);
		mapStringdata4 = w4.text;
		dDataHua4 = LoadData("Assets/Resources/Data/", "hua4", bWWWload: true, mapStringdata4);
	}

	public void InitMapAndBtnData()
	{
		InitLMapBtnCount();
		InitLMapStarBtnID();
		InitLMapEndBtnID();
		InitLevel_Map();
	}

	public void InitLevel_Map()
	{
		dDataLevel_Map = new Dictionary<int, int>();
		int num = 0;
		for (int i = 1; i <= UserManager.iMapCount; i++)
		{
			for (int j = 1; j <= LMapBtnCount[i - 1]; j++)
			{
				num++;
				dDataLevel_Map.Add(num, i);
			}
		}
		try
		{
			int @int = Singleton<TestScript>.Instance.GetInt(SDBNO + "DB_iNowPassLevelID");
			if (@int != 0)
			{
				@int++;
				if (@int < LevelManager.iMaxLevelID)
				{
					int mapForLevelID = Singleton<UserManager>.Instance.GetMapForLevelID(@int);
					Singleton<DataManager>.Instance.SaveUserDate("DB_iNowMapID", mapForLevelID - 1);
				}
			}
		}
		catch (Exception arg)
		{
			UnityEngine.Debug.Log("InitLevel_Map error = " + arg);
		}
	}

	public void InitLMapBtnCount()
	{
		LMapBtnCount = new int[UserManager.iMapCount];
		LMapBtnCount[0] = 10;
		LMapBtnCount[1] = 10;
		LMapBtnCount[2] = 15;
		LMapBtnCount[3] = 15;
		for (int i = 4; i < UserManager.iMapCount; i++)
		{
			LMapBtnCount[i] = 25;
		}
	}

	public void InitLMapEndBtnID()
	{
		LMapEndBtnID = new int[UserManager.iMapCount];
		LMapEndBtnID[0] = 0;
		int num = 0;
		for (int i = 0; i < UserManager.iMapCount; i++)
		{
			LMapEndBtnID[i] = num;
			num += LMapBtnCount[i];
		}
	}

	public void InitLMapStarBtnID()
	{
		LMapStarBtnID = new int[UserManager.iMapCount];
		LMapStarBtnID[0] = 0;
		int num = 0;
		for (int i = 0; i < UserManager.iMapCount; i++)
		{
			LMapStarBtnID[i] = num;
			num += LMapBtnCount[i];
		}
	}

	public void initKeyCache()
	{
		htDataCache = new Hashtable();
		foreach (string key in Singleton<DataManager>.Instance.dBubble.Keys)
		{
			if (!htDataCache.Contains(key) && !(key == string.Empty))
			{
				htDataCache.Add(key, dDataType.dDataGameObj);
			}
		}
		LevelRewardDataCache = new Hashtable();
		foreach (string key2 in Singleton<DataManager>.Instance.dDataMapicon.Keys)
		{
			if (!LevelRewardDataCache.Contains(key2) && !(key2 == string.Empty))
			{
				LevelRewardDataCache.Add(key2, 0);
			}
		}
	}

	public Dictionary<string, Dictionary<string, string>> LoadData(string sPath, string sName, bool bWWWload = false, string sWWWData = "")
	{
		UnityEngine.Debug.Log("sName=" + sName);
		Dictionary<string, Dictionary<string, string>> dictionary = new Dictionary<string, Dictionary<string, string>>();
		string text = string.Empty;
		if (!bWWWload)
		{
			TextAsset textAsset = (TextAsset)Resources.Load("Data/" + sName, typeof(TextAsset));
			text = textAsset.ToString();
		}
		string[] array = text.Split('\n');
		if (bWWWload)
		{
			array = sWWWData.Split('\n');
		}
		if (sName == "LanguageLogin")
		{
			sName = "Language";
		}
		string[] array2 = null;
		Dictionary<string, string> dictionary2 = null;
		for (int i = 0; i < array.Length; i++)
		{
			string text2 = array[i];
			if (i == 1)
			{
				int num = text2.Split(',').Length;
				if (sName == "Language")
				{
					num = text2.Split('|').Length;
				}
				array2 = new string[num];
				for (int j = 0; j < num - 1; j++)
				{
					if (sName == "Language")
					{
						array2[j] = text2.Split('|')[j];
					}
					else
					{
						array2[j] = text2.Split(',')[j];
					}
				}
			}
			else if (i > 1)
			{
				int num2 = text2.Split(',').Length;
				if (sName == "Language")
				{
					num2 = text2.Split('|').Length;
				}
				dictionary2 = new Dictionary<string, string>();
				for (int k = 1; k < num2 - 1; k++)
				{
					try
					{
						if (sName == "Language")
						{
							dictionary2.Add(array2[k], text2.Split('|')[k]);
						}
						else
						{
							dictionary2.Add(array2[k], text2.Split(',')[k]);
						}
					}
					catch (Exception)
					{
					}
				}
				if (sName == "Language")
				{
					try
					{
						dictionary.Add(text2.Split('|')[0], dictionary2);
					}
					catch (Exception ex2)
					{
						UnityEngine.Debug.LogError(ex2.Message + "    " + text2);
					}
				}
				else
				{
					dictionary.Add(text2.Split(',')[0], dictionary2);
				}
			}
		}
		return dictionary;
	}

	public void SaveUserDate(string sKey, string sDate)
	{
		Singleton<TestScript>.Instance.SetString(SDBNO + sKey, sDate);
	}

	public void SaveUserDate(string sKey, int iDate)
	{
		Singleton<TestScript>.Instance.SetInt(SDBNO + sKey, iDate);
	}

	public void SaveUserDate(string sKey, float fDate)
	{
		Singleton<TestScript>.Instance.SetFloat(SDBNO + sKey, fDate);
	}

	public int GetUserDataI(string sKey)
	{
		return Singleton<TestScript>.Instance.GetInt(SDBNO + sKey);
	}

	public float GetUserDataF(string sKey)
	{
		return Singleton<TestScript>.Instance.GetFloat(SDBNO + sKey);
	}

	public string GetUserDataS(string sKey)
	{
		return Singleton<TestScript>.Instance.GetString(SDBNO + sKey, string.Empty);
	}

	public void ClickHaoping()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(SDBNO + "DB_bHaoping");
		if (@int != 2)
		{
			Singleton<TestScript>.Instance.SetInt(SDBNO + "DB_bHaoping", 1);
		}
		yunbuHaoping();
	}

	public void SendyunbuCheck()
	{
		if (Singleton<DataManager>.Instance.bShowHaopingLogin)
		{
			
		}
		else
		{
			InitAndroid.action.ReturnshowMarket("Close");
		}
	}

	public void yunbuHaoping()
	{
		
	}

	public void CheckVip7Reward()
	{
		if (!Util.CheckOnline())
		{
			InitGame.bVip7 = false;
			InitGame.Action.GetTimeNetTime();
			Singleton<TestScript>.Instance.SetInt(SDBNO + "DB_NowDayCheckVip7" + Util.GetNowTime_Day(), 0);
			return;
		}
		int @int = Singleton<TestScript>.Instance.GetInt(SDBNO + "DB_RewardCheckVip7" + Util.GetNowTime_Day());
		if (@int != 1 && InitGame.bVip7)
		{
			RewardVip7();
		}
	}

	public void RewardVip7(bool b = false)
	{
		if (!b)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(SDBNO + "DB_RewardCheckVip7" + Util.GetNowTime_Day());
			if (@int == 1)
			{
				return;
			}
		}
		GameObject gameObject = MapUI.action.gameObject;
		if ((bool)Vip7UI.action)
		{
			gameObject = Vip7UI.action.gameObject;
		}
		if ((bool)ChinaShopUI.action)
		{
			gameObject = ChinaShopUI.action.gameObject;
		}
		if ((bool)PlayUI.action)
		{
			gameObject = PlayUI.action.gameObject;
		}
		if ((bool)SignRewardUI.action)
		{
			gameObject = SignRewardUI.action.gameObject;
		}
		ChinaPay.action.addRewardAll(10, 4, gameObject, isShow: false);
		ChinaPay.action.addRewardAll(3, 500, gameObject, isShow: false);
		ChinaPay.action.addRewardAll(2, 3000, gameObject, isShow: false);
		BaseUIAnimation.action.ShowProp(10, 4, 3, 500, 2, 3000, gameObject);
		InitGame.bVip7 = true;
		Singleton<TestScript>.Instance.SetInt(SDBNO + "DB_RewardCheckVip7" + Util.GetNowTime_Day(), 1);
	}

	public void CheckVip7()
	{
		if (!Util.CheckOnline())
		{
			InitGame.bVip7 = false;
			InitGame.Action.GetTimeNetTime();
			Singleton<TestScript>.Instance.SetInt(SDBNO + "DB_NowDayCheckVip7" + Util.GetNowTime_Day(), 0);
			return;
		}
		if (InitGame.bVip7)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(SDBNO + "DB_NowDayCheckVip7" + Util.GetNowTime_Day());
			if (@int == 1)
			{
				return;
			}
		}
		FireBase.Action.UnityCheckVip7();
		Singleton<TestScript>.Instance.SetInt(SDBNO + "DB_NowDayCheckVip7" + Util.GetNowTime_Day(), 1);
		string userDataS = Singleton<DataManager>.Instance.GetUserDataS("DB_Vip7");
		if (userDataS == string.Empty)
		{
			InitGame.bVip7 = false;
			return;
		}
		string s = DateTime.Now.ToString("yyyyMMdd");
		DateTime d = DateTime.ParseExact(s, "yyyyMMdd", CultureInfo.CurrentCulture);
		DateTime d2 = DateTime.ParseExact(userDataS, "yyyyMMdd", CultureInfo.CurrentCulture);
		string text = (d2 - d).ToString();
		UnityEngine.Debug.Log("stimes=" + text);
		text = text.ToString().Replace("00:00:00", string.Empty);
		text = text.ToString().Replace(".", string.Empty);
		bool flag = false;
		if (text == string.Empty)
		{
			flag = true;
		}
		else
		{
			int num = int.Parse(text);
			flag = ((num >= 1) ? true : false);
		}
		if (flag)
		{
			InitGame.bVip7 = true;
			FireBase.Action.UnityUpFaceBookVip7();
		}
		else
		{
			InitGame.bVip7 = false;
		}
	}

	public void FacdBookCheckVip7(string Day)
	{
		string s = DateTime.Now.ToString("yyyyMMdd");
		DateTime d = DateTime.ParseExact(s, "yyyyMMdd", CultureInfo.CurrentCulture);
		DateTime d2 = DateTime.ParseExact(Day, "yyyyMMdd", CultureInfo.CurrentCulture);
		string text = (d2 - d).ToString().Replace(".00:00:00", string.Empty);
		text = text.ToString().Replace("00:00:00", string.Empty);
		text = text.ToString().Replace(".", string.Empty);
		bool flag = false;
		if (text == string.Empty)
		{
			flag = true;
		}
		else
		{
			int num = int.Parse(text);
			if (num >= 1)
			{
				flag = true;
				if (num >= 7)
				{
					string sDate = DateTime.Now.AddDays(6.0).ToString("yyyyMMdd");
					Singleton<DataManager>.Instance.SaveUserDate("DB_Vip7", sDate);
					FireBase.Action.UnityUpFaceBookVip7();
				}
			}
			else
			{
				flag = false;
			}
		}
		if (flag)
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_Vip7", Day);
			InitGame.bVip7 = true;
			CheckVip7();
		}
		else
		{
			InitGame.bVip7 = false;
			Singleton<DataManager>.Instance.SaveUserDate("DB_Vip7", string.Empty);
		}
	}
}
