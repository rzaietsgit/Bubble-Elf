//using Facebook.Unity;
using LitJson;
using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class FaceBookApi : MonoBehaviour
{
	public static FaceBookApi Action;

	public string UserId = string.Empty;

	public Dictionary<string, string> dFacebookName;

	public Dictionary<string, Sprite> dFacebookImage;

	public List<string> LFacebookFriend;

	public string MyFaceBookName = string.Empty;

	public Sprite MyFacebookImage;

	public static bool bWeb = true;

	public static string FacebookAskString = "i'm out of lives,can you please send me one?";

	public static string FacebookSendLiftString = "help your friends by sending them lives.";

	public bool InitOver;

	public static int FacebookFriendOnline;

	private bool bLoginSuccess;

	public string Rank1FID = string.Empty;

	public string Rank2FID = string.Empty;

	public string Rank1Name = string.Empty;

	public string Rank2Name = string.Empty;

	public string Rank1Score = string.Empty;

	public string Rank2Score = string.Empty;

	public string Rank2Rank = string.Empty;

	private void Start()
	{
		Action = this;
		dFacebookName = new Dictionary<string, string>();
		dFacebookImage = new Dictionary<string, Sprite>();
		LFacebookFriend = new List<string>();
		CheckWeb();
		if (UserId == string.Empty)
		{
			UserId = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_FaceBookID", string.Empty);
		}
		InitOver = true;
		LoginSuccess();
	}

	public void FBFeedShare()
	{
		//FB.FeedShare(string.Empty, new Uri(DataManager.IosAppLink), "ScoreRank", "ScoreRank", "Hey,everyone,I get the " + Action.Rank2Rank + " in Level " + Singleton<LevelManager>.Instance.iNowSelectLevelIndex + ",come and exceed me!", new Uri(DataManager.IosAppIcon), string.Empty, ShareCallback);
	}

	public void FBFeedSharewin1()
	{
		//if (Singleton<DataManager>.Instance.bGooglePay)
		//{
		//	FB.FeedShare(string.Empty, new Uri(DataManager.AndroidAppLink), "Level " + Singleton<LevelManager>.Instance.iNowSelectLevelIndex, "Level  " + Singleton<LevelManager>.Instance.iNowSelectLevelIndex, "I Earn " + Singleton<LevelManager>.Instance.iNowStar + "Stars in " + Singleton<LevelManager>.Instance.iNowSelectLevelIndex + ",Come and play with me!", new Uri(DataManager.IosAppIcon), string.Empty, ShareCallbackwin1);
		//}
		//else
		//{
		//	FB.FeedShare(string.Empty, new Uri(DataManager.IosAppLink), "Level " + Singleton<LevelManager>.Instance.iNowSelectLevelIndex, "Level  " + Singleton<LevelManager>.Instance.iNowSelectLevelIndex, "I Earn " + Singleton<LevelManager>.Instance.iNowStar + "Stars in " + Singleton<LevelManager>.Instance.iNowSelectLevelIndex + ",Come and play with me!", new Uri(DataManager.IosAppIcon), string.Empty, ShareCallbackwin1);
		//}
	}

	//public void ShareCallbackwin1(IShareResult result)
	//{
		//if (!result.Cancelled && string.IsNullOrEmpty(result.Error) && string.IsNullOrEmpty(result.PostId))
		//{
		//}
	//}

	//public void ShareCallback(IShareResult result)
	//{
	//	if (result.Cancelled || !string.IsNullOrEmpty(result.Error))
	//	{
	//		if ((bool)FaceBookRankOpenUI.action)
	//		{
	//			FaceBookRankOpenUI.action.CloseUI();
	//		}
	//	}
	//	else if (!string.IsNullOrEmpty(result.PostId))
	//	{
	//		if ((bool)FaceBookRankOpenUI.action)
	//		{
	//			FaceBookRankOpenUI.action.CloseUI();
	//		}
	//	}
	//	else if ((bool)FaceBookRankOpenUI.action)
	//	{
	//		FaceBookRankOpenUI.action.CloseUI();
	//	}
	//}

	public void Delmsg(string msgid)
	{
		//FB.API(msgid, HttpMethod.DELETE, delegate
		//{
		//});
	}

	public void FBFacebookAskString()
	{
		//List<object> list = new List<object>();
		//list.Add("app_users");
		//List<object> list2 = list;
		//FB.AppRequest(FacebookAskString, null, new List<object>
		//{
		//	"app_users"
		//}, null, null, null, "Ask Loves Title", delegate
		//{
		//});
	}

	//public void AppRequestCallBack(IAppRequestResult result)
	//{
		//if (result.Error == null && (bool)AskLivesUI.action)
		//{
		//	AskLivesUI.action.CloseUI();
		//}
	//}

	private void CheckWeb()
	{
		//bool flag = true;
		//if (false)
		//{
		//	bWeb = true;
		//}
		//else
		//{
		//	bWeb = false;
		//}
	}

	public void LoadF()
	{
		//FB.API("/me/apprequests", HttpMethod.GET, delegate(IGraphResult result)
		//{
		//	JsonData jsonData = JsonMapper.ToObject(result.RawResult);
		//	FaceBookMessage faceBookMessage = default(FaceBookMessage);
		//	for (int i = 0; i < jsonData["data"].Count; i++)
		//	{
		//		faceBookMessage.id = jsonData["data"][i]["id"].ToString();
		//		faceBookMessage.from_id = jsonData["data"][i]["from"]["id"].ToString();
		//		faceBookMessage.from_name = jsonData["data"][i]["from"]["name"].ToString();
		//		faceBookMessage.message = jsonData["data"][i]["message"].ToString();
		//		faceBookMessage.created_time = jsonData["data"][i]["created_time"].ToString();
		//		if (faceBookMessage.message == FacebookAskString && (bool)AskLivesUI.action)
		//		{
		//			AskLivesUI.action.LoadAskMsg(faceBookMessage.from_name, faceBookMessage.from_id);
		//		}
		//	}
		//});
	}

	public void SendLiveFB(List<string> LSendFriendList)
	{
		//FB.AppRequest(FacebookSendLiftString, LSendFriendList, null, null, null, null, "Send Loves Title", delegate(IAppRequestResult result)
		//{
		//	JsonData jsonData = JsonMapper.ToObject(result.RawResult.ToString());
		//	string text = string.Empty;
		//	for (int i = 0; i < jsonData["to"].Count; i++)
		//	{
		//		text = text + "," + jsonData["to"][i].ToString();
		//	}
		//	text = text.Replace("\"", string.Empty);
		//	if (text.Length >= 3)
		//	{
		//		if (text.LastIndexOf(",") >= 0)
		//		{
		//			for (int num = 0; num < text.Split(',').Length; num++)
		//			{
		//				Action.DeleteMsg(text.Split(',')[num]);
		//			}
		//		}
		//		else
		//		{
		//			Action.DeleteMsg(text);
		//		}
		//		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_iDaySendLives" + Util.GetNowTime_Day(), 1);
		//		if ((bool)SendLivesUI.action)
		//		{
		//			SendLivesUI.action.CloseSendLivesUI();
		//		}
		//	}
		//});
	}

	public void LoadImageFB(Image img)
	{
		//FB.API("/me/picture?width=100&height=100", HttpMethod.GET, delegate(IGraphResult result)
		//{
		//	if (string.IsNullOrEmpty(result.Error) && result.Texture != null)
		//	{
		//		Sprite sprite = Sprite.Create(result.Texture, new Rect(0f, 0f, 100f, 100f), new Vector2(0.5f, 0.5f));
		//		img.sprite = sprite;
		//	}
		//});
	}

	public void ResEmail()
	{
		//int iCount = 0;
		//List<string> LSendFriendList = new List<string>();
		//FB.API("/me/apprequests", HttpMethod.GET, delegate(IGraphResult result)
		//{
		//	JsonData jsonData = JsonMapper.ToObject(result.RawResult);
		//	FaceBookMessage faceBookMessage = default(FaceBookMessage);
		//	for (int i = 0; i < jsonData["data"].Count; i++)
		//	{
		//		faceBookMessage.id = jsonData["data"][i]["id"].ToString();
		//		faceBookMessage.from_id = jsonData["data"][i]["from"]["id"].ToString();
		//		faceBookMessage.from_name = jsonData["data"][i]["from"]["name"].ToString();
		//		faceBookMessage.message = jsonData["data"][i]["message"].ToString();
		//		faceBookMessage.created_time = jsonData["data"][i]["created_time"].ToString();
		//		if (faceBookMessage.message == FacebookAskString)
		//		{
		//			if (Action.SendLoveList(faceBookMessage.from_id))
		//			{
		//				FB.API(faceBookMessage.id, HttpMethod.DELETE, delegate
		//				{
		//				});
		//			}
		//			else
		//			{
		//				bool flag = true;
		//				for (int j = 0; j < LSendFriendList.Count; j++)
		//				{
		//					if (faceBookMessage.from_id == LSendFriendList[j])
		//					{
		//						flag = false;
		//						FB.API(faceBookMessage.id, HttpMethod.DELETE, delegate
		//						{
		//						});
		//						break;
		//					}
		//				}
		//				if (flag)
		//				{
		//					LSendFriendList.Add(faceBookMessage.from_id);
		//					iCount++;
		//				}
		//			}
		//		}
		//		else if (faceBookMessage.message == FacebookSendLiftString)
		//		{
		//			iCount++;
		//		}
		//	}
		//	if (iCount > 0)
		//	{
		//		if ((bool)SettingPanelUI.action)
		//		{
		//			SettingPanelUI.action.ShowMailCount(iCount);
		//		}
		//	}
		//	else if ((bool)SettingPanelUI.action)
		//	{
		//		SettingPanelUI.action.HideMailCount();
		//	}
		//});
	}

	public void AskFriendsFB()
	{
		//FB.AppRequest(FacebookAskString, null, new List<object>
		//{
		//	"app_users"
		//}, null, null, null, "Ask Loves Title", delegate
		//{
		//	if ((bool)BuyLivesUI.action)
		//	{
		//		BuyLivesUI.action.CloseLivesUI();
		//	}
		//});
	}

	public void MailUILoad()
	{
		//int iCount = 0;
		//List<string> LSendFriendList = new List<string>();
		//FB.API("/me/apprequests", HttpMethod.GET, delegate(IGraphResult result)
		//{
		//	JsonData jsonData = JsonMapper.ToObject(result.RawResult);
		//	FaceBookMessage faceBookMessage = default(FaceBookMessage);
		//	for (int i = 0; i < jsonData["data"].Count; i++)
		//	{
		//		faceBookMessage.id = jsonData["data"][i]["id"].ToString();
		//		faceBookMessage.from_id = jsonData["data"][i]["from"]["id"].ToString();
		//		faceBookMessage.from_name = jsonData["data"][i]["from"]["name"].ToString();
		//		faceBookMessage.message = jsonData["data"][i]["message"].ToString();
		//		faceBookMessage.created_time = jsonData["data"][i]["created_time"].ToString();
		//		if (faceBookMessage.message == FacebookAskString)
		//		{
		//			if (Action.SendLoveList(faceBookMessage.from_id))
		//			{
		//				FB.API(faceBookMessage.id, HttpMethod.DELETE, delegate
		//				{
		//				});
		//			}
		//			else
		//			{
		//				bool flag = true;
		//				for (int j = 0; j < LSendFriendList.Count; j++)
		//				{
		//					if (faceBookMessage.from_id == LSendFriendList[j])
		//					{
		//						flag = false;
		//						FB.API(faceBookMessage.id, HttpMethod.DELETE, delegate
		//						{
		//						});
		//						break;
		//					}
		//				}
		//				if (flag)
		//				{
		//					LSendFriendList.Add(faceBookMessage.from_id);
		//					MailUI.action.LoadMail(0, faceBookMessage.from_name, faceBookMessage.from_id, faceBookMessage.id);
		//					iCount++;
		//				}
		//			}
		//		}
		//		else if (faceBookMessage.message == FacebookSendLiftString)
		//		{
		//			MailUI.action.LoadMail(1, faceBookMessage.from_name, faceBookMessage.from_id, faceBookMessage.id);
		//			iCount++;
		//		}
		//	}
		//	if (iCount > 0)
		//	{
		//		if ((bool)SettingPanelUI.action)
		//		{
		//			SettingPanelUI.action.ShowMailCount(iCount);
		//			MailUI.action.MailNullRemark.SetActive(value: false);
		//		}
		//	}
		//	else if ((bool)SettingPanelUI.action)
		//	{
		//		SettingPanelUI.action.HideMailCount();
		//		MailUI.action.MailNullRemark.SetActive(value: true);
		//	}
		//	if (iCount > 8)
		//	{
		//		int num = 140;
		//		int num2 = iCount - 8;
		//		RectTransform component = MailUI.action.MsgObjFather.transform.GetComponent<RectTransform>();
		//		RectTransform rectTransform = component;
		//		Vector3 localPosition = component.localPosition;
		//		float x = localPosition.x;
		//		float y = num * num2 / 2;
		//		Vector3 localPosition2 = component.localPosition;
		//		rectTransform.localPosition = new Vector3(x, y, localPosition2.z);
		//		RectTransform rectTransform2 = component;
		//		Vector2 sizeDelta = component.sizeDelta;
		//		float x2 = sizeDelta.x;
		//		Vector2 sizeDelta2 = component.sizeDelta;
		//		rectTransform2.sizeDelta = new Vector2(x2, sizeDelta2.y + (float)(num * num2));
		//	}
		//	MailUI.action.iNowCount = iCount;
		//});
	}

	public void LoadMyImg(Image img)
	{
		//FB.API("/me/picture?width=100&height=100", HttpMethod.GET, delegate(IGraphResult result)
		//{
		//	if (string.IsNullOrEmpty(result.Error) && result.Texture != null)
		//	{
		//		Sprite sprite = Sprite.Create(result.Texture, new Rect(0f, 0f, 100f, 100f), new Vector2(0.5f, 0.5f));
		//		img.sprite = sprite;
		//	}
		//});
	}

	public void ReceiveLoveFB(string sMessID)
	{
		//FB.API(sMessID, HttpMethod.DELETE, delegate
		//{
		//	Singleton<LevelManager>.Instance.AddLove();
		//	if ((bool)MapUI.action)
		//	{
		//		MapUI.action.LoadLove();
		//	}
		//	if ((bool)MapUI.action)
		//	{
		//		MapUI.action.CheckFacebookMailCountIcon();
		//		MailUI.action.RemoveMailOne();
		//	}
		//});
	}

	public void LoadFrImgFB(string FID, Image _RankImg)
	{
		//FB.API(FID + "/picture?width=100&height=100", HttpMethod.GET, delegate(IGraphResult result)
		//{
		//	Sprite sprite = Sprite.Create(result.Texture, new Rect(0f, 0f, 100f, 100f), new Vector2(0.5f, 0.5f));
		//	_RankImg.sprite = sprite;
		//	dFacebookImage[FID] = sprite;
		//});
	}

	public void ReadMailFB()
	{
		//FB.API("/me/apprequests", HttpMethod.GET, delegate(IGraphResult result)
		//{
		//	JsonData jsonData = JsonMapper.ToObject(result.RawResult);
		//	int num = 0;
		//	FaceBookMessage faceBookMessage = default(FaceBookMessage);
		//	while (true)
		//	{
		//		if (num >= jsonData["data"].Count)
		//		{
		//			return;
		//		}
		//		faceBookMessage.id = jsonData["data"][num]["id"].ToString();
		//		faceBookMessage.from_id = jsonData["data"][num]["from"]["id"].ToString();
		//		faceBookMessage.from_name = jsonData["data"][num]["from"]["name"].ToString();
		//		faceBookMessage.message = jsonData["data"][num]["message"].ToString();
		//		faceBookMessage.created_time = jsonData["data"][num]["created_time"].ToString();
		//		if (faceBookMessage.message == FacebookAskString)
		//		{
		//			if (!Action.SendLoveList(faceBookMessage.from_id))
		//			{
		//				break;
		//			}
		//			FB.API(faceBookMessage.id, HttpMethod.DELETE, delegate
		//			{
		//			});
		//		}
		//		num++;
		//	}
		//	Singleton<DataManager>.Instance.SaveUserDate("DB_FacebookLogin", 1);
		//	Singleton<UIManager>.Instance.OpenUI(EnumUIType.SendLivesUI);
		//});
	}

	public bool bLoginState()
	{
		//if (FB.IsLoggedIn)
		//{
		//	return true;
		//}
		return false;
	}

	public void SendMessageFB(GameObject obj, string sToMailID)
	{
		//FB.AppRequest(to: new string[1]
		//{
		//	sToMailID
		//}, message: FacebookSendLiftString, filters: null, excludeIds: null, maxRecipients: null, data: null, title: "Send Loves Title", callback: delegate(IAppRequestResult result)
		//{
		//	JsonData jsonData = JsonMapper.ToObject(result.RawResult.ToString());
		//	string empty = string.Empty;
		//	empty = jsonData["to"][0].ToString();
		//	if (empty.Length >= 3)
		//	{
		//		empty = empty.Replace("\"", string.Empty);
		//		Action.DeleteMsg(empty);
		//		UnityEngine.Object.Destroy(obj);
		//	}
		//});
	}

	public void SetImage(string Fid, Image headIconImg)
	{
		//FB.API(Fid + "/picture?width=100&height=100", HttpMethod.GET, delegate(IGraphResult result)
		//{
		//	Sprite sprite = Sprite.Create(result.Texture, new Rect(0f, 0f, 100f, 100f), new Vector2(0.5f, 0.5f));
		//	headIconImg.sprite = sprite;
		//	dFacebookImage[Fid] = sprite;
		//});
	}

	private void Awake()
	{
		//UnityEngine.Debug.Log(" Jy Awake facebook init 1");
		//if (!FB.IsInitialized)
		//{
		//	UnityEngine.Debug.Log(" Jy Awake facebook init 2");
		//	try
		//	{
		//		UnityEngine.Debug.Log(" Jy Awake facebook init 3");
		//		FB.Init(InitCallback, OnHideUnity);
		//	}
		//	catch (Exception arg)
		//	{
		//		UnityEngine.Debug.Log("facebook init error :" + arg);
		//	}
		//}
		//else
		//{
		//	FB.ActivateApp();
		//	FB.LogOut();
		//	Action.LoginOutExit();
		//	Singleton<DataManager>.Instance.SaveUserDate("DB_FaceBookID", string.Empty);
		//}
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

	public void LoginOutExit()
	{
		//UserId = string.Empty;
		//bLoginSuccess = false;
		//dFacebookName = null;
		//dFacebookImage.Clear();
		//LFacebookFriend.Clear();
		//MyFaceBookName = string.Empty;
		//MyFacebookImage = null;
		//FacebookFriendOnline = 0;
		//FireBase.Action.bUnitySearchFriendSum = false;
		//if ((bool)SettingPanelUI.action)
		//{
		//	SettingPanelUI.action.HideMailCount();
		//}
		//ResUI();
	}

	public void InitCallback()
	{
		//UnityEngine.Debug.Log(" Jy Awake facebook InitCallback 1");
		//if (FB.IsInitialized)
		//{
		//	UnityEngine.Debug.Log(" Jy Awake facebook InitCallback 2");
		//	FB.ActivateApp();
		//}
		//else
		//{
		//	UnityEngine.Debug.Log(" Jy Awake facebook InitCallback 3");
		//	UnityEngine.Debug.Log("Failed to Initialize the Facebook SDK");
		//}
	}

	public void OnHideUnity(bool isGameShown)
	{
		if (!isGameShown)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1f;
		}
	}

	public void FaceBookLoginAward()
	{
	}

	public void FaceBookShareAward()
	{
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_FaceBookShareAward25") == 0)
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_FaceBookShareAward25", 1);
			if ((bool)PayManager.action)
			{
				PayManager.action.AwardAddGold(25, "FaceBookShareAward");
			}
		}
	}

	public void LoginSuccess()
	{
		//if (!bLoginSuccess && FB.IsLoggedIn)
		//{
		//	if ((bool)EnConnectFacebook.action)
		//	{
		//		EnConnectFacebook.action.CloseEnConnectFacebook();
		//	}
		//	bLoginSuccess = true;
		//	FB.API("/me/friends", HttpMethod.GET, FriendsCallback);
		//	try
		//	{
		//		FB.API("/me/picture?width=100&height=100", HttpMethod.GET, delegate(IGraphResult result)
		//		{
		//			if (string.IsNullOrEmpty(result.Error) && result.Texture != null)
		//			{
		//				Sprite sprite = MyFacebookImage = Sprite.Create(result.Texture, new Rect(0f, 0f, 100f, 100f), new Vector2(0.5f, 0.5f));
		//			}
		//		});
		//	}
		//	catch (Exception)
		//	{
		//	}
		//	FB.API("/me?fields=id,name", HttpMethod.GET, delegate(IGraphResult result)
		//	{
		//		JsonData jsonData = JsonMapper.ToObject(result.RawResult);
		//		UserId = jsonData["id"].ToString();
		//		MyFaceBookName = jsonData["name"].ToString();
		//		if (MyFaceBookName.Length > 5)
		//		{
		//			MyFaceBookName = MyFaceBookName.Substring(0, 5) + "...";
		//		}
		//		Singleton<DataManager>.Instance.SaveUserDate("DB_FaceBookID", UserId);
		//		LoadFacebookData();
		//	});
		//	if (Singleton<DataManager>.Instance.GetUserDataI("DB_LogCompletedRegistrationEvent") == 0)
		//	{
		//		LogCompletedRegistrationEvent(UserId);
		//		Singleton<DataManager>.Instance.SaveUserDate("DB_LogCompletedRegistrationEvent", 1);
		//	}
		//	ResUI();
		//}
	}

	public void LoadFacebookData()
	{
	}

	//private void FriendsCallback(IGraphResult result)
	//{
	//	dFacebookName = new Dictionary<string, string>();
	//	dFacebookImage = new Dictionary<string, Sprite>();
	//	IDictionary<string, object> resultDictionary = result.ResultDictionary;
	//	List<object> list = (List<object>)resultDictionary["data"];
	//	for (int i = 0; i < list.Count; i++)
	//	{
	//		object obj = list[i];
	//		Dictionary<string, object> dictionary = (Dictionary<string, object>)obj;
	//		dFacebookName.Add(dictionary["id"].ToString(), dictionary["name"].ToString());
	//		LFacebookFriend.Add(dictionary["id"].ToString());
	//		InitImage(dictionary["id"].ToString());
	//	}
	//	if ((bool)FireBase.Action)
	//	{
	//		FireBase.Action.LoadFriendLevel();
	//		FireBase.Action.UnitySearchFriendSum(LFacebookFriend);
	//	}
	//}

	public void InitImage(string Fid)
	{
		//FB.API(Fid + "/picture?width=100&height=100", HttpMethod.GET, delegate(IGraphResult result)
		//{
		//	Sprite value = Sprite.Create(result.Texture, new Rect(0f, 0f, 100f, 100f), new Vector2(0.5f, 0.5f));
		//	dFacebookImage.Add(Fid.ToString(), value);
		//});
	}

	public void Invite()
	{
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_DYCYQ", 1);
		//FB.Mobile.AppInvite(new Uri(DataManager.IosFacebookLink), new Uri(DataManager.IosAppIcon));
	}

	public void FackBookLogin()
	{
		UnityEngine.Debug.Log("FackBookLogin01");
		List<string> list = new List<string>();
		list.Add("public_profile");
		list.Add("email");
		List<string> permissions = list;
		//FB.LogInWithReadPermissions(permissions, AuthCallback);
	}

	public void FackBookLoginOut()
	{
		//FB.LogOut();
		Action.LoginOutExit();
		Singleton<DataManager>.Instance.SaveUserDate("DB_FaceBookID", string.Empty);
	}

	//private void AuthCallback(ILoginResult result = null)
	//{
		//if (FB.IsLoggedIn)
		//{
		//	if (result.Error != null)
		//	{
		//		UnityEngine.Debug.Log("User  login result.Error " + result.Error);
		//		return;
		//	}
		//	JsonData jsonData = JsonMapper.ToObject(result.RawResult);
		//	UnityEngine.Debug.Log("dJsonData userid ToString()--------" + jsonData["user_id"].ToString());
		//	UserId = jsonData["user_id"].ToString();
		//	GA.ProfileSignIn(UserId, "FaceBook");
		//	Singleton<DataManager>.Instance.SaveUserDate("DB_FaceBookID", UserId);
		//	ResUI();
		//	LoginSuccess();
		//}
		//else
		//{
		//	UnityEngine.Debug.Log("User cancelled login fault" + result);
		//}
	//}

	public void ResUI()
	{
		if ((bool)SetPanelUI.action)
		{
			SetPanelUI.action.CheckFaceBookLogin();
		}
		if ((bool)MailUI.action)
		{
			MailUI.action.ResFaceBookLoginState();
		}
		if ((bool)FacebookRankUI.action)
		{
			FacebookRankUI.action.CheckOnline();
		}
		if ((bool)MapUI.action)
		{
			MapUI.action.ResInvitation();
			MapUI.action.CheckFacebookMailCountIcon();
		}
		if ((bool)BtnManager.action)
		{
			BtnManager.action.LoadGirlImage();
		}
	}

	public void DeleteMsg(string sFaceBookID)
	{
		SaveSendLove(sFaceBookID);
		//FB.API("/me/apprequests", HttpMethod.GET, delegate(IGraphResult result)
		//{
		//	JsonData jsonData = JsonMapper.ToObject(result.RawResult);
		//	FaceBookMessage faceBookMessage = default(FaceBookMessage);
		//	for (int i = 0; i < jsonData["data"].Count; i++)
		//	{
		//		faceBookMessage.id = jsonData["data"][i]["id"].ToString();
		//		faceBookMessage.from_id = jsonData["data"][i]["from"]["id"].ToString();
		//		faceBookMessage.from_name = jsonData["data"][i]["from"]["name"].ToString();
		//		faceBookMessage.message = jsonData["data"][i]["message"].ToString();
		//		faceBookMessage.created_time = jsonData["data"][i]["created_time"].ToString();
		//		if (faceBookMessage.message == FacebookAskString && faceBookMessage.from_id == sFaceBookID)
		//		{
		//			FB.API(faceBookMessage.id, HttpMethod.DELETE, delegate
		//			{
		//				if ((bool)MapUI.action)
		//				{
		//					MapUI.action.CheckFacebookMailCountIcon();
		//					MailUI.action.RemoveMailOne();
		//				}
		//			});
		//		}
		//	}
		//});
	}

	private void SaveSendLove(string Fid)
	{
		string text = DateTime.Now.ToString("yyyy-MM-dd");
		string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_LoveList_" + Action.UserId + "_" + text, string.Empty);
		@string = @string + "," + Fid;
		Singleton<DataManager>.Instance.SaveUserDate("DB_LoveList_" + Action.UserId + "_" + text, @string);
	}

	public bool SendLoveList(string Fid)
	{
		string text = DateTime.Now.ToString("yyyy-MM-dd");
		string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_LoveList_" + Action.UserId + "_" + text, string.Empty);
		if (@string == string.Empty)
		{
			return false;
		}
		for (int num = 0; num < @string.Split(',').Length; num++)
		{
			if (@string.Split(',')[num] == Fid)
			{
				return true;
			}
		}
		return false;
	}

	//private void LoginCallback(ILoginResult result)
	//{
	//	try
	//	{
	//		if (result.Error == null && !FB.IsLoggedIn)
	//		{
	//		}
	//	}
	//	catch (Exception)
	//	{
	//	}
	//}

	public void CheckLoginIcon(GameObject obj)
	{
	}

	public void TestRank()
	{
		Rank1FID = "1111111";
		Rank2FID = "22222";
		Rank1Name = "name1";
		Rank2Name = "name2";
		Rank1Score = "100000";
		Rank2Score = "20000";
		Rank2Rank = "1";
	}

	public void FbEvPay(float priceAmount, string priceCurrency, string packageName)
	{
		try
		{
			// UnityEngine.Debug.Log("priceAmount=" + priceAmount);
			UnityEngine.Debug.Log("priceCurrency=" + priceCurrency);
			UnityEngine.Debug.Log("packageName=" + packageName);
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["mygame_packagename"] = packageName;
			//FB.LogPurchase(priceAmount, priceCurrency, dictionary);
		}
		catch (Exception arg)
		{
			UnityEngine.Debug.Log("FbEvPay=" + arg);
		}
	}

	public void LogAddedToWishlistEvent(string contentId, string contentType, string currency, double price)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["fb_content_id"] = contentId;
		dictionary["fb_content_type"] = contentType;
		dictionary["fb_currency"] = currency;
		//FB.LogAppEvent("fb_mobile_add_to_wishlist", (float)price, dictionary);
	}

	public void LogAchievedLevelEvent(string level)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["fb_level"] = level;
		//FB.LogAppEvent("fb_mobile_level_achieved", null, dictionary);
	}

	public void LogAddedPaymentInfoEvent(bool success)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["fb_success"] = (success ? 1 : 0);
		//FB.LogAppEvent("fb_mobile_add_payment_info", null, dictionary);
	}

	public void LogCompletedRegistrationEvent(string registrationMethod)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["fb_registration_method"] = registrationMethod;
		//FB.LogAppEvent("fb_mobile_complete_registration", null, dictionary);
	}

	public void LogCompletedTutorialEvent(string contentId, bool success)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["fb_content_id"] = contentId;
		dictionary["fb_success"] = (success ? 1 : 0);
		//FB.LogAppEvent("fb_mobile_tutorial_completion", null, dictionary);
	}

	public void LogInitiatedCheckoutEvent(string contentId, string contentType, int numItems, bool paymentInfoAvailable, string currency, double totalPrice)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["fb_content_id"] = contentId;
		dictionary["fb_content_type"] = contentType;
		dictionary["fb_num_items"] = numItems;
		dictionary["fb_payment_info_available"] = (paymentInfoAvailable ? 1 : 0);
		dictionary["fb_currency"] = currency;
		//FB.LogAppEvent("fb_mobile_initiated_checkout", (float)totalPrice, dictionary);
	}

	public void LogRatedEvent(string contentType, string contentId, int maxRatingValue, double ratingGiven)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["fb_content_type"] = contentType;
		dictionary["fb_content_id"] = contentId;
		dictionary["fb_max_rating_value"] = maxRatingValue;
		//FB.LogAppEvent("fb_mobile_rate", (float)ratingGiven, dictionary);
	}

	public void LogSpentCreditsEvent(string contentId, string contentType, double totalValue)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["fb_content_id"] = contentId;
		dictionary["fb_content_type"] = contentType;
		//FB.LogAppEvent("fb_mobile_spent_credits", (float)totalValue, dictionary);
	}
}
