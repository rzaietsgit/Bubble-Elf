using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class FireBase : MonoBehaviour
{
	public static FireBase Action;

	public static bool bWeb = true;

	private bool bLogSwitch = true;

	public bool bload;

	public Dictionary<string, int> dDataFBFriendlevel;

	public bool InitOver;

	public bool bDownloadFacebookData;

	private int iFriendListRankCount;

	private int iFriendListRankReturnCount;

	public bool bUnitySearchFriendSum;

	private Dictionary<string, int> FriendListRank;

	public Dictionary<string, int> _TempFriendListRank;

	public bool bSearchNoShow;

	private void Start()
	{
		CheckWeb();
		InitializeFirebaseStart();
		InitOver = true;
		//if (Application.platform == RuntimePlatform.IPhonePlayer)
		//{
		//	InitXcoeConfig();
		//	if (InitGame.bEnios)
		//	{
		//		InitGameAnalyticsJyenios();
		//	}
		//	else
		//	{
		//		InitGameAnalyticsJy();
		//	}
		//}
	}

	public void InitUnityConfig1(string n)
	{
		UnityEngine.Debug.Log("InitUnityConfig1=");
		Singleton<DataManager>.Instance.bInitUnityConfig1 = true;
	}

	public void InitUnityConfig2(string n)
	{
		UnityEngine.Debug.Log("InitUnityConfig2=");
		Singleton<DataManager>.Instance.bInitUnityConfig2 = true;
		Singleton<DataManager>.Instance.bInitUnityConfig3 = true;
	}

	private void InitializeFirebaseStart()
	{
	}

	private void InitializeFirebase()
	{
	}

	public void TestGG()
	{

	}

	public void TestGoogle(string key)
	{
		UnityEngine.Debug.Log("Unity Print key=" + key);
	}

	public void TestdDataFBFriendlevel()
	{
		dDataFBFriendlevel = new Dictionary<string, int>();
		dDataFBFriendlevel.Add("117226922083945", 15);
	}

	private void CheckWeb()
	{
		bool flag = true;
		if (false)
		{
			bWeb = true;
		}
		else
		{
			bWeb = false;
		}
	}

	private void Awake()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			if (Singleton<DataManager>.Instance.bChinaIos && !InitGame.bEnios)
			{
				if (InitGame.bEnios)
				{
					UnityEngine.Debug.Log("JyLogInitFirebase>>>1");
					//InitFirebase();
					UnityEngine.Debug.Log("JyLogInitFirebase>>>2");
				}
			}
			else
			{
				UnityEngine.Debug.Log("JyLogInitFirebase>>>1");
				//InitFirebase();
				UnityEngine.Debug.Log("JyLogInitFirebase>>>2");
			}
		}
		if (Action == null)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			Action = this;
		}
		else if (Action != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void ErrorCdKey(string strerr)
	{
		if ((bool)cdkeyUI.action)
		{
			cdkeyUI.action.errorcdkey();
		}
	}

	public void ResultCdKey(string sResultCdKey)
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		for (int num = 0; num < sResultCdKey.Split(',').Length; num++)
		{
			string text = sResultCdKey.Split(',')[num];
			if (text != "0")
			{
				list.Add(num + 1);
				list2.Add(int.Parse(text));
				ChinaPay.action.addRewardAll(num + 1, int.Parse(text), MapUI.action.gameObject, isShow: false, "free", "ResultCdKey");
			}
		}
		BaseUIAnimation.action.ShowProp(list, list2, MapUI.action.gameObject);
	}

	public void UCheckCdKey(string key)
	{
		//if (Application.platform == RuntimePlatform.IPhonePlayer)
		//{
		//	CheckCdKey(key);
		//}
	}

	public void UnityUpFaceBookVip7()
	{
		if (!InitGame.bEnios || !FaceBookApi.Action.bLoginState() || FaceBookApi.Action.UserId == string.Empty)
		{
			return;
		}
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_UnityUpFaceBookVip7" + Util.GetNowTime_Day());
		if (@int != 1)
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_UnityUpFaceBookVip7" + Util.GetNowTime_Day(), 1);
			string userId = FaceBookApi.Action.UserId;
			string defaultValue = DateTime.Now.AddDays(6.0).ToString("yyyyMMdd");
			string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_Vip7", defaultValue);
			InitGame.bVip7 = true;
			UnityEngine.Debug.Log("UnityUpFaceBookVip7   ID" + userId);
			UnityEngine.Debug.Log("UnityUpFaceBookVip7   sTime" + @string);
			//if (Application.platform == RuntimePlatform.IPhonePlayer)
			//{
			//	UpFaceBookVip7(userId, @string);
			//}
		}
	}

	public void UnityCheckVip7()
	{
		if (InitGame.bEnios && FaceBookApi.Action.bLoginState() && !(FaceBookApi.Action.UserId == string.Empty))
		{
			string userId = FaceBookApi.Action.UserId;
			UnityEngine.Debug.Log("UnityCheckVip7   ID" + userId);
			//if (Application.platform == RuntimePlatform.IPhonePlayer)
			//{
			//	CheckVip7(userId);
			//}
		}
	}

	public void ReturnVip7(string sDay)
	{
		UnityEngine.Debug.Log(" jy ReturnVip7   sDay" + sDay);
		Singleton<DataManager>.Instance.FacdBookCheckVip7(sDay);
	}

	public void DownFaceBookData()
	{
		UnityEngine.Debug.Log("Jy DownFaceBookData1  ");
		if (FaceBookApi.Action == null)
		{
			return;
		}
		UnityEngine.Debug.Log("Jy DownFaceBookData2  ");
		string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_GoogleID", string.Empty);
		if (@string == string.Empty || bload)
		{
			return;
		}
		bload = true;
		//if (Application.platform == RuntimePlatform.IPhonePlayer)
		//{
		//	if (!Singleton<DataManager>.Instance.bChinaIos || InitGame.bEnios)
		//	{
		//		UnityEngine.Debug.Log("Jy DownFaceBookData5  ");
		//		ReadFBNowLevel(@string);
		//	}
		//}
		
	}

	public void ReturnReadFBNowLevel(string sReadFBNowLevelDate)
	{
		UnityEngine.Debug.Log("jy 1 unityReturnReadFBNowLevel=" + sReadFBNowLevelDate);
		int num = int.Parse(sReadFBNowLevelDate);
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
		if (num > @int)
		{
			string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_GoogleID", string.Empty);
			if (@string == string.Empty)
			{
				return;
			}
			UnityEngine.Debug.Log("  jy 2 unity DownLoadFaceBookScore=" + @string);
			//if (Application.platform == RuntimePlatform.IPhonePlayer)
			//{
			//	if (Singleton<DataManager>.Instance.bChinaIos && !InitGame.bEnios)
			//	{
			//		return;
			//	}
			//	DownLoadFaceBookScore(@string);
			//}
			
			int mapForLevelID = Singleton<UserManager>.Instance.GetMapForLevelID(num);
			Singleton<UserManager>.Instance.SetNowMapID(mapForLevelID - 1);
		}
		if (num < @int)
		{
			UpdateAllScoreData();
		}
	}

	public void LoadFriendLevel()
	{
	}

	public void ReturnFBFriendFBNowLevel(string sData)
	{
		string key = sData.Split(',')[0].ToString();
		int value = int.Parse(sData.Split(',')[1]);
		dDataFBFriendlevel.Add(key, value);
	}

	public void ReturnReadUserDate(string nsData)
	{
	}

	public void ReturnDownLoadGoogleScore(string ScoreData)
	{
		UnityEngine.Debug.Log("Unity  ReturnDownLoadFaceBookScore  ScoreData= " + ScoreData);
		if (ScoreData == null || ScoreData == string.Empty)
		{
			return;
		}
		if (ScoreData == "ReturnOver")
		{
			Singleton<LevelManager>.Instance.bFirstInMap = true;
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowMapID");
			UnityEngine.Debug.Log("jy 8989   001  iNowMapID=" + @int);
			Singleton<DataManager>.Instance.bdownloadgoogle = true;
			bDownloadFacebookData = true;
			SettingPanelUI.bSettingPanelUIOpen = false;
			int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
			Singleton<DataManager>.Instance.SaveUserDate("DB_iNowPassLevelID", int2);
			for (int i = 0; i < Singleton<DataManager>.Instance.LMapEndBtnID.Length; i++)
			{
				if (int2 == Singleton<DataManager>.Instance.LMapEndBtnID[i])
				{
					Singleton<UserManager>.Instance.GoNextMap();
				}
			}
			@int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowMapID");
			UnityEngine.Debug.Log("jy 8989   002  iNowMapID=" + @int);
			InitGame.Action.StartTow();
			Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.InitGame);
			return;
		}
		string text = ScoreData.Split(',')[0];
		text = text.Replace("lv", string.Empty);
		string s = ScoreData.Split(',')[1];
		string s2 = ScoreData.Split(',')[2];
		Singleton<DataManager>.Instance.SaveUserDate("DB_LevelStar_" + text, int.Parse(s));
		Singleton<DataManager>.Instance.SaveUserDate("DB_LevelScore_" + text, int.Parse(s2));
		int int3 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
		if (int.Parse(text) > int3)
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_iNowPassLevelID", int.Parse(text));
			for (int j = 0; j < Singleton<DataManager>.Instance.LMapEndBtnID.Length; j++)
			{
				if (int.Parse(text) == Singleton<DataManager>.Instance.LMapEndBtnID[j])
				{
					Singleton<UserManager>.Instance.GoNextMap();
				}
			}
		}
		Singleton<UserManager>.Instance.PassLevelAward(int.Parse(text));
	}

	public void UpdateAllScoreData()
	{
		for (int i = 1; i < 100000 && Singleton<UserManager>.Instance.UpdateScore(i); i++)
		{
		}
	}

	public void UnityWriteLog(string sType, string _sContent)
	{
	}

	public void UnitySearchRankScore(List<string> FriendList, bool bSearchFirend = false)
	{
		if (FriendListRank == null)
		{
			FriendListRank = new Dictionary<string, int>();
		}
		iFriendListRankCount = FriendList.Count;
		iFriendListRankReturnCount = 0;
		string text = string.Empty;
		for (int i = 0; i < FriendList.Count && i <= 50; i++)
		{
			text = ((Singleton<LevelManager>.Instance.iNowSelectLevelIndex <= 10000) ? (text + FriendList[i] + "-" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex + ",") : (text + FriendList[i] + "-" + 10000 + ","));
		}
		if (text == string.Empty)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LevelScore_" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
			if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex > 10000)
			{
				@int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LevelScore_" + 10000);
			}
			if (@int > 0)
			{
				FriendListRank.Add(FaceBookApi.Action.UserId, @int);
				Dictionary<string, int> friendListRank = DictionarySort(FriendListRank);
				ShowRank(friendListRank);
			}
		}
		else
		{
			text = text.Substring(0, text.Length - 1);
		}
	}

	public void UnitySearchFriendSum(List<string> FriendList, bool bSearchFirend = false)
	{
		if (!bUnitySearchFriendSum)
		{
			bUnitySearchFriendSum = true;
			string text = string.Empty;
			for (int i = 0; i < FriendList.Count; i++)
			{
				text = text + FriendList[i] + "-1,";
			}
			if (text == string.Empty || text.Length == 0)
			{
				FaceBookApi.FacebookFriendOnline = 0;
				UnityEngine.Debug.Log("UnitySearchFriendSum   FaceBookApi.FacebookFriendOnline>000");
			}
			else
			{
				text = text.Substring(0, text.Length - 1);
			}
		}
	}

	public void ReturnSearchFriendSum(string sReturnScore)
	{
		FaceBookApi.FacebookFriendOnline++;
	}

	public void ReturnRankScore(string sReturnScore)
	{
		if (FriendListRank == null)
		{
			FriendListRank = new Dictionary<string, int>();
		}
		if (sReturnScore != "Null")
		{
			string key = sReturnScore.Split(',')[0].ToString();
			string s = sReturnScore.Split(',')[1];
			FriendListRank.Add(key, int.Parse(s));
		}
		iFriendListRankReturnCount++;
		if (iFriendListRankReturnCount != iFriendListRankCount)
		{
			return;
		}
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LevelScore_" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
		if (@int > 0)
		{
			FriendListRank.Add(FaceBookApi.Action.UserId, @int);
		}
		if (FriendListRank.Count > 0)
		{
			Dictionary<string, int> dictionary = DictionarySort(FriendListRank);
			if (bSearchNoShow)
			{
				_TempFriendListRank = DictionarySort(FriendListRank);
				if (_TempFriendListRank.Count <= 0)
				{
					_TempFriendListRank = dictionary;
				}
				return;
			}
			ShowRank(dictionary);
		}
		FriendListRank = null;
	}

	public void ShowRank(Dictionary<string, int> _FriendListRank)
	{
		if ((bool)FacebookRankUI.action)
		{
			FacebookRankUI.action.OnlineFacebook(_FriendListRank);
		}
	}

	public Dictionary<string, int> DictionarySort(Dictionary<string, int> dic)
	{
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>(dic);
		list.Sort((KeyValuePair<string, int> s1, KeyValuePair<string, int> s2) => s2.Value.CompareTo(s1.Value));
		dic.Clear();
		for (int i = 0; i < list.Count; i++)
		{
			KeyValuePair<string, int> keyValuePair = list[i];
			if (keyValuePair.Value > 0)
			{
				dictionary[keyValuePair.Key] = keyValuePair.Value;
			}
		}
		return dictionary;
	}

	public void UnityOnlyWriteDate(string sKey, string sData)
	{
	}

	public void UnityUpdateScore(int iLevel, int iStar, int iScore)
	{

		
	}

	public void GAUnityGAEvenIos(string SKEY)
	{
		
	}

	public void GAUnityGAEvenIosCount(string SKEY, int iCount)
	{
		
	}

	public void GAUnityGAEventByGameStatus1(string Level)
	{
		 
	}

	public void GAUnityGAEventByGameStatus2(string Level)
	{
		
	}

	public void GAUnityGAEventByGameStatus3(string Level)
	{
		
	}

	public void GAUnityGAEventPay(string SKEY, string amount)
	{
		
	}
}
