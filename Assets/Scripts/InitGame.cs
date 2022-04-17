using System;
using System.Collections;
using TMPro;

using UnityEngine;

public class InitGame : MonoBehaviour
{
	public static InitGame Action;

	public GameObject InitAndroidObj;

	public static bool bChinaVersion = true;

	public static bool bEnios = true;

	public static bool bVip7;

	public static bool bHideSign7Task;

	public static bool bCloseLBForEnIos = true;

	public string netTime = "0";

	public long resultTime;

	public static bool bStartFlag;

	public static bool IsBackground;

	private string message = string.Empty;

	public string sNetTime = string.Empty;

	private static string Logstrwww = "InitGmae - ";

	private void Awake()
	{
		UnityEngine.Debug.Log("Jy GameInit1");
		if (Action == null)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			Action = this;
		}
		else if (Action != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		FirebaseController.Initialize();
		FirebaseController.LoginOrOutGame(state: true);
	}

	public void Update()
	{
		if (!Singleton<DataManager>.Instance.bChinaIos && bChinaVersion && message != string.Empty)
		{
			UnityEngine.Debug.Log("Update---" + message);
			UpdateErrorLog("Unity Log " + message);
			message = string.Empty;
		}
	}

	private void MyLogCallback(string condition, string stackTrace, LogType type)
	{
		switch (type)
		{
		case LogType.Log:
			break;
		case LogType.Assert:
		{
			string text = message;
			message = text + "      receive an assert log,condition=" + condition + ",stackTrace=" + stackTrace;
			break;
		}
		case LogType.Error:
		{
			string text = message;
			message = text + "      receive an Error log,condition=" + condition + ",stackTrace=" + stackTrace;
			break;
		}
		case LogType.Exception:
		{
			string text = message;
			message = text + "      receive an Exception log,condition=" + condition + ",stackTrace=" + stackTrace;
			break;
		}
		case LogType.Warning:
		{
			string text = message;
			message = text + "      receive an Warning log,condition=" + condition + ",stackTrace=" + stackTrace;
			break;
		}
		}
	}

	public void OpenSkill()
	{
		Singleton<DataManager>.Instance.SaveUserDate("DB_SkillOpen_6", 1);
		Singleton<DataManager>.Instance.SaveUserDate("DB_SkillOpen_2", 1);
		Singleton<DataManager>.Instance.SaveUserDate("DB_SkillOpen_1", 1);
		Singleton<DataManager>.Instance.SaveUserDate("DB_SkillOpen_4", 1);
		Singleton<DataManager>.Instance.SaveUserDate("DB_SkillOpen_0", 1);
		Singleton<DataManager>.Instance.SaveUserDate("DB_SkillOpen_5", 1);
		Singleton<DataManager>.Instance.SaveUserDate("DB_SkillOpen_3", 1);
	}

	public bool CheckInit()
	{
		Logstrwww += "-2:4:9:1-";
		if (!PayManager.action)
		{
			Logstrwww += "-2:4:9:2-";
			return false;
		}
		if (!PayManager.action.InitOver)
		{
			Logstrwww += "-2:4:9:3-";
			return false;
		}
		if (!FireBase.Action)
		{
			Logstrwww += "-2:4:9:4-";
			return false;
		}
		if (!FireBase.Action.InitOver)
		{
			UnityEngine.Debug.Log("Jy IEInit ```~~~~~~~xxxxxxx4");
			Logstrwww += "-2:4:9:5-";
			return false;
		}
		if (!bChinaVersion && !FaceBookApi.Action)
		{
			UnityEngine.Debug.Log("Jy IEInit ```~~~~~~~xxxxxxx5");
			Logstrwww += "-2:4:9:6-";
			return false;
		}
		if (!bChinaVersion && !FaceBookApi.Action.InitOver)
		{
			UnityEngine.Debug.Log("Jy IEInit ```~~~~~~~xxxxxxx6");
			Logstrwww += "-2:4:9:7-";
			return false;
		}
		if (!MusicController.action)
		{
			UnityEngine.Debug.Log("Jy IEInit ```~~~~~~~xxxxxxx7");
			Logstrwww += "-2:4:9:8-";
			return false;
		}
		if (!MusicController.action.InitOver)
		{
			Logstrwww += "-2:4:9:9-";
			UnityEngine.Debug.Log("Jy IEInit ```~~~~~~~xxxxxxx8");
			return false;
		}
		if (!SoundController.action)
		{
			UnityEngine.Debug.Log("Jy IEInit ```~~~~~~~xxxxxxx9");
			Logstrwww += "-2:4:9:10-";
			return false;
		}
		if (!SoundController.action.InitOver)
		{
			UnityEngine.Debug.Log("Jy IEInit ```~~~~~~~xxxxxxx10");
			Logstrwww += "-2:4:9:11-";
			return false;
		}
		//if (!AdManager.action)
		//{
		//	UnityEngine.Debug.Log("Jy IEInit ```~~~~~~~xxxxxxx11");
		//	Logstrwww += "-2:4:9:12-";
		//	return false;
		//}
		//if (!AdManager.action.InitOver)
		//{
		//	UnityEngine.Debug.Log("Jy IEInit ```~~~~~~~xxxxxxx12");
		//	Logstrwww += "-2:4:9:13-";
		//	return false;
		//}
		if (!InitAndroid.action.InitOver)
		{
			UnityEngine.Debug.Log("Jy IEInit ```~~~~~~~xxxxxxx13");
			Logstrwww += "-2:4:9:14-";
			return false;
		}
		if (!GamePush.action)
		{
			UnityEngine.Debug.Log("Jy IEInit ```~~~~~~~xxxxxxx14");
			Logstrwww += "-2:4:9:15-";
			return false;
		}
		if (!GamePush.action.InitOver)
		{
			UnityEngine.Debug.Log("Jy IEInit ```~~~~~~~xxxxxxx15");
			Logstrwww += "-2:4:9:16-";
			return false;
		}
		if (!LoginScene.action.InitOver)
		{
			UnityEngine.Debug.Log("Jy IEInit ```~~~~~~~xxxxxxx16");
			Logstrwww += "-2:4:9:17-";
			return false;
		}
		return true;
	}

	private void InitFistLoginGameDay()
	{
		if (bChinaVersion)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_RDaojuList_Time");
			int num = 28800;
			if (Util.GetNowTime() - @int > num)
			{
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_RDaojuList_Time", Util.GetNowTime());
				Singleton<TestScript>.Instance.SetString(DataManager.SDBNO + "DB_RDaojuList", string.Empty);
			}
			int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_InitFistLoginGameDay");
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_InitFistLoginGameDayOppoAd") == 0)
			{
				int nowTime = Util.GetNowTime();
				Singleton<DataManager>.Instance.SaveUserDate("DB_InitFistLoginGameDayOppoAd", nowTime);
			}
			string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_InitFistLoginGameDayT", string.Empty);
			if (int2 == 0)
			{
				int2 = Util.GetNowTime();
				Singleton<DataManager>.Instance.SaveUserDate("DB_InitFistLoginGameDay", int2);
			}
			if (@string == string.Empty)
			{
				Singleton<DataManager>.Instance.SaveUserDate("DB_InitFistLoginGameDayT", Util.GetNowTime_Day());
			}
			string string2 = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_FistLoginSignRewardDay", string.Empty);
			if (string2 == string.Empty)
			{
				Singleton<TestScript>.Instance.SetString(DataManager.SDBNO + "DB_FistLoginSignRewardDay", DateTime.Now.ToString("yyyy-MM-dd"));
			}
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Sign7") == 0)
			{
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_Sign7", 1);
			}
		}
	}

	private IEnumerator IEInit()
	{
		Logstrwww += "-2:4:1-";
		Singleton<DataManager>.Instance.iLoveMaxAll = 5;
		Singleton<DataManager>.Instance.iLoveUse = 1;
		Singleton<LevelManager>.Instance.ResTime = 1800;
		BaseUIAnimation.Language = "Simplified_Chinese";
		CheckLanguage();
		yield return new WaitForSeconds(0.0001f);
		Logstrwww += "-2:4:6-";
		Singleton<DataManager>.Instance.LocalStaticLoadData();
		Singleton<UserManager>.Instance.bOpenHua();
		Logstrwww += "-2:4:7-";
		yield return new WaitForSeconds(0.0001f);
		if (LevelManager.bWwwDataFlag)
		{
			yield return new WaitForSeconds(3f);
		}
		bool b = true;
		Logstrwww += "-2:4:8-";
		while (b)
		{
			yield return new WaitForSeconds(0.001f);
			Logstrwww += "-2:4:9-";
			if (CheckInit())
			{
				int iMyGold = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
				if (iMyGold < 0)
				{
					Singleton<DataManager>.Instance.SaveUserDate("DB_GOLD", 0);
				}
				Logstrwww += "-2:4:10-";
				yield return new WaitForSeconds(0.001f);
				if (bChinaVersion)
				{
					Logstrwww += "-2:4:11-";
					OpenSkill();
				}
				Logstrwww += "-2:4:12-";
				InitFistLoginGameDay();
				Logstrwww += "-2:4:13-";
				b = false;
				Singleton<SceneManager>.Instance.InitSceneInfo();
				Logstrwww += "-2:4:14-";
				InitData();
				Logstrwww += "-2:4:15-";
				Logstrwww += "-2:4:16-";
				Singleton<UserLevelManager>.Instance.LoginGame();
				Logstrwww += "-2:4:17-";
				Logstrwww += "-2:4:18-";
				Logstrwww += "-2:4:19-";
				DataManager.umengKey = "56fdfe0767e58e052400198e";
				if (Singleton<DataManager>.Instance.bChinaIos)
				{
					DataManager.umengKey = "56fdff7ce0f55aea170024e4";
				}
				if (bEnios)
				{
					DataManager.umengKey = "56fdff4267e58e2a2400034b";
				}
				if (Singleton<DataManager>.Instance.bGooglePay)
				{
					DataManager.umengKey = "58a2d00c65b6d649b0000637";
				}
				//Analytics.StartWithAppKeyAndChannelId(DataManager.umengKey, DataManager.ChannelId);
				Logstrwww += "-2:4:20-";
				if (bChinaVersion)
				{
					wwwGetBubble();
				}
				Logstrwww += "-2:4:21-";
				//Analytics.SetLogEnabled(value: true);
				Logstrwww += "-2:4:22-";
				int index = (int)BaseUIAnimation.LanguageTp;
				TMP_FontAsset _TMP_FontAsset = getLanguage(index);
				Logstrwww += "-2:4:24-";
				BaseUIAnimation.action.LoadLanguage(_TMP_FontAsset);
				Logstrwww += "-2:4:24-";
				NextScene();
				Logstrwww += "-2:4:25-";
				bStartFlag = true;
			}
		}
	}

	public void InitAudioConfig()
	{
	}

	public void initInterNetTime()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			string text = netTime = DateTime.Now.ToString("yyyyMMdd");
		}
	}

	public TMP_FontAsset getLanguage(int index)
	{
		string str = "MFYueYuan_Noncommercial-Regular SDF";
		if (index == 1)
		{
			str = "English_GROBOLD_SDF";
		}
		if (index == 2 || index == 3 || index == 6 || index == 7 || index == 8 || index == 10)
		{
			str = "fadexipue SDF";
		}
		if (index == 4)
		{
			str = "riyu";
		}
		if (index == 4)
		{
			str = "hanyu SDF";
		}
		if (index == 9)
		{
			str = "taiyu SDF";
		}
		return Resources.Load("Font/" + str, typeof(TMP_FontAsset)) as TMP_FontAsset;
	}

	public void SetLang(string sLanguage)
	{
		if (sLanguage == "Simplified_Chinese")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Chinese;
		}
		else if (sLanguage == "English")
		{
			BaseUIAnimation.LanguageTp = LanguageType.English;
		}
		else if (sLanguage == "French")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_French;
		}
		else if (sLanguage == "German")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_German;
		}
		else if (sLanguage == "Japanese")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Japanese;
		}
		else if (sLanguage == "Korean")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Korean;
		}
		else if (sLanguage == "Spanish")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Spanish;
		}
		else if (sLanguage == "Portuguese")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Portuguese;
		}
		else if (sLanguage == "Russian")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Russian;
		}
		else if (sLanguage == "Thai")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Thai;
		}
		else if (sLanguage == "Traditional_Chinese")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Traditional_Chinese;
		}
		else
		{
			BaseUIAnimation.LanguageTp = LanguageType.English;
		}
		BaseUIAnimation.Language = sLanguage;
	}

	public void CheckLanguage()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_FirstLanguage");
		if (@int != 1)
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_FirstLanguage", 1);
			switch (Application.systemLanguage)
			{
			case SystemLanguage.Portuguese:
			case SystemLanguage.Russian:
			case SystemLanguage.Spanish:
			case SystemLanguage.Thai:
				break;
			case SystemLanguage.Afrikaans:
				SetLang("af");
				break;
			case SystemLanguage.Arabic:
				SetLang("ar");
				break;
			case SystemLanguage.Basque:
				SetLang("eu");
				break;
			case SystemLanguage.Belarusian:
				SetLang("be");
				break;
			case SystemLanguage.Bulgarian:
				SetLang("bg");
				break;
			case SystemLanguage.Catalan:
				SetLang("ca");
				break;
			case SystemLanguage.Chinese:
				SetLang("Simplified_Chinese");
				break;
			case SystemLanguage.Czech:
				SetLang("cs");
				break;
			case SystemLanguage.Danish:
				SetLang("da");
				break;
			case SystemLanguage.Dutch:
				SetLang("nl");
				break;
			case SystemLanguage.English:
				SetLang("English");
				break;
			case SystemLanguage.Estonian:
				SetLang("et");
				break;
			case SystemLanguage.Faroese:
				SetLang("fo");
				break;
			case SystemLanguage.Finnish:
				SetLang("fu");
				break;
			case SystemLanguage.French:
				SetLang("French");
				break;
			case SystemLanguage.German:
				SetLang("German");
				break;
			case SystemLanguage.Greek:
				SetLang("el");
				break;
			case SystemLanguage.Hebrew:
				SetLang("he");
				break;
			case SystemLanguage.Icelandic:
				SetLang("is");
				break;
			case SystemLanguage.Indonesian:
				SetLang("id");
				break;
			case SystemLanguage.Italian:
				SetLang("it");
				break;
			case SystemLanguage.Japanese:
				SetLang("Japanese");
				break;
			case SystemLanguage.Korean:
				SetLang("Korean");
				break;
			case SystemLanguage.Latvian:
				SetLang("lv");
				break;
			case SystemLanguage.Lithuanian:
				SetLang("lt");
				break;
			case SystemLanguage.Norwegian:
				SetLang("nn");
				break;
			case SystemLanguage.Polish:
				SetLang("pl");
				break;
			case SystemLanguage.Romanian:
				SetLang("ro");
				break;
			case SystemLanguage.SerboCroatian:
				SetLang("sr");
				break;
			case SystemLanguage.Slovak:
				SetLang("sk");
				break;
			case SystemLanguage.Slovenian:
				SetLang("sl");
				break;
			case SystemLanguage.Swedish:
				SetLang("sv");
				break;
			case SystemLanguage.Turkish:
				SetLang("tr");
				break;
			case SystemLanguage.Ukrainian:
				SetLang("uk");
				break;
			case SystemLanguage.Vietnamese:
				SetLang("vi");
				break;
			case SystemLanguage.ChineseSimplified:
				SetLang("Simplified_Chinese");
				break;
			case SystemLanguage.ChineseTraditional:
				SetLang("Traditional_Chinese");
				break;
			case SystemLanguage.Unknown:
				SetLang("English");
				break;
			case SystemLanguage.Hungarian:
				SetLang("hu");
				break;
			}
		}
	}

	public void LoadLanguage()
	{
		string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_Language", string.Empty);
		if (@string == "Simplified_Chinese")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Chinese;
		}
		else if (@string == "English")
		{
			BaseUIAnimation.LanguageTp = LanguageType.English;
		}
		else if (@string == "French")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_French;
		}
		else if (@string == "German")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_German;
		}
		else if (@string == "Japanese")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Japanese;
		}
		else if (@string == "Korean")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Korean;
		}
		else if (@string == "Spanish")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Spanish;
		}
		else if (@string == "Portuguese")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Portuguese;
		}
		else if (@string == "Russian")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Russian;
		}
		else if (@string == "Thai")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Thai;
		}
		else if (@string == "Traditional_Chinese")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Traditional_Chinese;
		}
	}

	private void LoadTestData()
	{
	}

	public void NextScene()
	{
		FirstLoginGame();
	}

	private void FirstLoginGame()
	{
		if (bChinaVersion)
		{

			if (Singleton<DataManager>.Instance.bLogo)
			{
				
				Singleton<DataManager>.Instance.bLogo = false;
			}
		}
		DataManager.bInGameOk = true;
	}

	public void XiuzhengData()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
		for (int i = 1; i < @int; i++)
		{
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LevelStar_" + i) == 0)
			{
				Singleton<DataManager>.Instance.SaveUserDate("DB_LevelStar_" + i, 3);
				Singleton<DataManager>.Instance.SaveUserDate("DB_LevelScore_" + i, 88888);
			}
		}
	}

	private void InitData()
	{
		Logstrwww += "-2:4:14:1-";
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount", -1);
		if (@int < 0)
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_LoveCount", Singleton<DataManager>.Instance.iLoveMaxAll);
		}
		Singleton<DataManager>.Instance.SaveUserDate("DB_HideSettingBtnUI", 0);
		Singleton<DataManager>.Instance.SaveUserDate("DB_FacebookLogin", 0);
		if (bChinaVersion)
		{
			int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_24hRewardTime");
			if (int2 <= 0)
			{
				int nowTime = Util.GetNowTime();
				Singleton<DataManager>.Instance.SaveUserDate("DB_24hRewardTime", nowTime);
			}
			int int3 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
			XiuzhengData();
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_newUser1218") == 0)
			{
				if (int3 <= 0)
				{
				}
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_newUser1218", 1);
			}
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_Sign7State", 0);
			int int4 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Sign7");
			if (int4 >= 8)
			{
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_Sign7State", 1);
			}
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NewTask1UIObjState", 0);
			int nowTime2 = Util.GetNowTime();
			int int5 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_InitFistLoginGameDay");
			nowTime2 -= int5;
			if (nowTime2 >= 864000)
			{
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NewTask1UIObjState", 1);
			}
		}
		Logstrwww += "-2:4:14:2-";
		SetGameAward();
		Logstrwww += "-2:4:14:3-";
		CheckFacebookLoginlog();
		Logstrwww += "-2:4:14:4-";
		int int6 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
		Singleton<UserManager>.Instance.PassLevelAward(int6);
		Logstrwww += "-2:4:14:5-";
		Singleton<TestScript>.Instance.InitData();
		Logstrwww += "-2:4:14:6-";
	}

	public void CheckFacebookLoginlog()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_FaceBookLoginAward25");
		if (@int == 1)
		{
			Singleton<DataManager>.Instance.FacebookLoginLog = true;
		}
	}

	public void SetGameAward()
	{
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NewUser50") == 0)
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_NewUser50", 1);
			if ((bool)PayManager.action && !bChinaVersion)
			{
				PayManager.action.AwardAddGold(5, "NewUser");
			}
		}
	}

	private void OnApplicationQuit()
	{
		IsBackground = true;
		FirebaseController.ExitLevel(4);
		FirebaseController.LoginOrOutGame(state: false);
		int month = DateTime.Now.Month;
		int day = DateTime.Now.Day;
		int hour = DateTime.Now.Hour;
		int minute = DateTime.Now.Minute;
		int userDataI = Singleton<DataManager>.Instance.GetUserDataI("DB_zaixian_start_hour" + month + "_" + day);
		int userDataI2 = Singleton<DataManager>.Instance.GetUserDataI("DB_zaixian_start_minute" + month + "_" + day);
		int userDataI3 = Singleton<DataManager>.Instance.GetUserDataI("DB_zaixian_" + month + "_" + day);
		hour -= userDataI;
		minute -= userDataI2;
		userDataI3 += hour * 60 + minute;
		Singleton<DataManager>.Instance.SaveUserDate("DB_zaixian_" + month + "_" + day, userDataI3);
		UnityEngine.Debug.Log("OnApplicationQuit   111111111111111111111111");
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_CFTC", 0);
		Singleton<TestScript>.Instance.Save();
	}

	private void OnApplicationPause(bool isPause)
	{
		if (isPause)
		{
			IsBackground = true;
			FirebaseController.LoginOrOutGame(state: false);
			InitAndroid.action.IsHouTai = true;
			int month = DateTime.Now.Month;
			int day = DateTime.Now.Day;
			int hour = DateTime.Now.Hour;
			int minute = DateTime.Now.Minute;
			int userDataI = Singleton<DataManager>.Instance.GetUserDataI("DB_zaixian_start_hour" + month + "_" + day);
			int userDataI2 = Singleton<DataManager>.Instance.GetUserDataI("DB_zaixian_start_minute" + month + "_" + day);
			int userDataI3 = Singleton<DataManager>.Instance.GetUserDataI("DB_zaixian_" + month + "_" + day);
			hour -= userDataI;
			minute -= userDataI2;
			userDataI3 += hour * 60 + minute;
			Singleton<DataManager>.Instance.SaveUserDate("DB_zaixian_" + month + "_" + day, userDataI3);
			UnityEngine.Debug.Log("游戏暂停 一切停止" + userDataI3 + " Hour " + hour + "   starhour " + userDataI);
			UnityEngine.Debug.Log("OnApplicationQuit   33333333333333333333333");
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_CFTC", 0);
		}
		else
		{
			UnityEngine.Debug.Log("OnApplicationQuit   22222222222222222222222");
			int month2 = DateTime.Now.Month;
			int day2 = DateTime.Now.Day;
			int hour2 = DateTime.Now.Hour;
			int minute2 = DateTime.Now.Minute;
			int second = DateTime.Now.Second;
			Singleton<DataManager>.Instance.SaveUserDate("DB_zaixian_start_hour" + month2 + "_" + day2, hour2);
			Singleton<DataManager>.Instance.SaveUserDate("DB_zaixian_start_minute" + month2 + "_" + day2, minute2);
			Singleton<DataManager>.Instance.SaveUserDate("DB_zaixian_start_Second" + month2 + "_" + day2, second);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_CFTC", 0);
		}
	}

	private void init_Game()
	{
		Logstrwww += "-2:1-";
		if (Application.platform == RuntimePlatform.Android)
		{
			InitAndroidObj.SetActive(value: true);
		}
		Application.targetFrameRate = 60;
		string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_Language", string.Empty);
		if (@string != string.Empty)
		{
			BaseUIAnimation.Language = @string;
			LoadLanguage();
			Logstrwww += "-2:3-";
		}
		Logstrwww += "-2:4-";
		StartCoroutine(IEInit());
		Logstrwww += "-2:5-";
		initInterNetTime();
		Logstrwww += "-2:6-";
	}

	public void GetTimeNetTime()
	{
		StartCoroutine(GetTimewww());
	}

	private IEnumerator GetTimewww()
	{
		yield return new WaitForSeconds(0.1f);
		if (Application.platform == RuntimePlatform.Android)
		{
			UnityEngine.Debug.Log("Jy www start 1");
			WWW www = new WWW("http://www.hko.gov.hk/cgi-bin/gts/time5a.pr?a=1");
			yield return www;
			if (www.error != null)
			{
				resultTime = 0L;
				sNetTime = DateTime.Now.ToString("yyyyMMdd");
				netTime = sNetTime;
			}
			else if (www.text != null)
			{
				string text = www.text;
				string timeStamp = text.Substring(2, 10);
				sNetTime = Util.StampToDateTime(timeStamp).ToString("yyyyMMdd");
				netTime = sNetTime;
				sNetTime = netTime;
				Singleton<DataManager>.Instance.iDareCount = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_DareCount" + sNetTime);
				resultTime = 1L;
			}
			else
			{
				sNetTime = DateTime.Now.ToString("yyyyMMdd");
				netTime = sNetTime;
				resultTime = 0L;
			}
		}
		else
		{
			string netDateTime = Util.GetNetDateTime();
			if (netDateTime != string.Empty)
			{
				sNetTime = Convert.ToDateTime(netDateTime).ToString("yyyyMMdd");
				Singleton<DataManager>.Instance.iDareCount = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_DareCount" + sNetTime);
				netTime = sNetTime;
				resultTime = 1L;
			}
			else
			{
				sNetTime = DateTime.Now.ToString("yyyyMMdd");
				netTime = sNetTime;
				resultTime = 0L;
			}
		}
	}

	public void Th_test1()
	{
	}

	private void Start()
	{
		StartTow();
	}

	public void StartTow()
	{
		try
		{
			UnityEngine.Debug.Log(" Jy Initgame2");
			if (!Singleton<DataManager>.Instance.bChinaIos && bChinaVersion)
			{
				Application.RegisterLogCallback(MyLogCallback);
			}
			Logstrwww += "-1-";
			Singleton<DataManager>.Instance.iDareCount = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_DareCount" + Util.GetNowTime_Day());
			GetTimeNetTime();
			Logstrwww += "-2-";
			init_Game();
			Logstrwww += "-3-";
			string text = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_MACCODE", string.Empty);
			if (text == string.Empty)
			{
				text = SystemInfo.deviceUniqueIdentifier;
				Singleton<DataManager>.Instance.SaveUserDate("DB_MACCODE", text);
			}
			DataManager.MACCODE = text;
			Logstrwww += "-4-";
		}
		catch (Exception arg)
		{
			UpdateErrorLog(Logstrwww + " er = " + arg);
		}
		UpdateErrorLog("jy Jy  ok " + Logstrwww, b: true);
		if (!Action)
		{
			Action = this;
			Logstrwww += "-4-";
		}
	}

	public void UpdateErrorLog(string str, bool b = false)
	{
	}

	private void wwwGetBubble()
	{
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_ToDayGetBubble" + Util.GetNowTime_Day()) == 0)
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_ToDayGetBubble" + Util.GetNowTime_Day(), 1);
			SetBubbleCount();
			SetSetpaybtncon();
			SetopadState();
		}
	}

	private void SetBubbleCount()
	{
	}

	private void SetopadState()
	{
	}

	public void InitTimeChina(string str)
	{
		UnityEngine.Debug.Log("jy InitTimeChina str2 = " + str);
		if (str.Length < 10)
		{
			UnityEngine.Debug.Log("jy InitTimeChina str22 = " + str);
			string text = netTime = DateTime.Now.ToString("yyyyMMdd");
			resultTime = 0L;
		}
		else
		{
			resultTime = 1L;
			UnityEngine.Debug.Log("jy InitTimeChina str222 = " + str);
			string text2 = (str + string.Empty).Substring(0, 10);
			netTime = Util.GetTimeChange(text2.ToString()).ToString("yyyyMMdd");
		}
	}

	private void SetOppoAdState(string str)
	{
		if (str == string.Empty)
		{
			return;
		}
		int num = int.Parse(str);
		int num2 = UnityEngine.Random.Range(0, 100);
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_boppoAdOldNumb");
		if (@int == 0 || @int != num)
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_boppoAdOldNumb", num);
			if (num2 <= num)
			{
				Singleton<DataManager>.Instance.SaveUserDate("DB_boppoAd", 1);
			}
		}
	}

	private void WinLog()
	{
		aliyunlog.LevelLog("win");
		int num = Singleton<UserLevelManager>.Instance.GetUserLevel();
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex < 31)
		{
			num = 0;
		}
		if ((bool)FireBase.Action)
		{
			FireBase.Action.UnityWriteLog("LOG_LevelWin", Singleton<LevelManager>.Instance.iNowSelectLevelIndex + "|" + num);
		}
		//GA.FinishLevel(Singleton<LevelManager>.Instance.iNowSelectLevelIndex.ToString());
	}

	public void WInData()
	{
		Singleton<LevelManager>.Instance.iFailureAll = 0;
		Singleton<UserManager>.Instance.SetPassTask("PassLevel");
		Singleton<UserLevelManager>.Instance.GameWin();
		if (Singleton<UserManager>.Instance.getLoveInfinite() <= 0)
		{
			Singleton<LevelManager>.Instance.AddLove();
		}
		WinLog();
		//Analytics.Event("PassWinBubble", "Level_" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex + "_" + Singleton<LevelManager>.Instance.iBubbleCountOver);
		PassLevel.action.SaveUserTask();
		int iNowStar = Singleton<LevelManager>.Instance.iNowStar;
		int iNowScore = Singleton<LevelManager>.Instance.iNowScore;
		Singleton<UserManager>.Instance.SetNowPassLevelNumber(Singleton<LevelManager>.Instance.iNowSelectLevelIndex, iNowStar, iNowScore);
	}

	private void SetSetpaybtncon()
	{
		if (!Singleton<DataManager>.Instance.bGooglePay && !Singleton<DataManager>.Instance.bChinaIos)
		{
		}
	}

	private void SetLevelBubble(string ResStr)
	{
		for (int num = 0; num < ResStr.Split('|').Length; num++)
		{
			string text = ResStr.Split('|')[num];
			if (text.Length >= 3)
			{
				try
				{
					int num2 = int.Parse(text.Split(',')[0]);
					int iDate = int.Parse(text.Split(',')[1]);
					Singleton<DataManager>.Instance.SaveUserDate("DB_Level" + num2, iDate);
				}
				catch (Exception)
				{
				}
			}
		}
	}
}
