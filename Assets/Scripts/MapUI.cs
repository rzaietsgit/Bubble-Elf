using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
	public int iMapIndex;

	public static MapUI action;

	public Component MapUISceneCamera;

	public GameObject LeftIcon;

	public GameObject RightIcon;

	public GameObject LeftIconAddBtn;

	public GameObject RightIconAddBtn;

	public GameObject CenterIconAddBtn;

	public GameObject DownLeftBtn;

	public GameObject DownRightBtn;

	public Text MapNameRemark;

	public Text CNMapNameRemark;

	public GameObject TopLeftObj;

	public GameObject TopRightObj;

	public GameObject TopCenterObj;

	public GameObject DownLeftOtherObj;

	public Sprite DownLeftOtherbeibaoSp;

	public Sprite DownLeftOtherbeibaoSpMe;

	public GameObject LodingUI;

	public bool bClickUI;

	private Vector3 oldPos = new Vector3(0f, 0f, 0f);

	private bool bClickTopLeft;

	private bool bClickTopRight;

	private bool bClickTopCenter;

	public GameObject DownChinaUIChinaObj;

	public GameObject GoogleObject;

	public Text GoldText;

	public Text GBText;

	public Text LoveText;

	private int iRtime;

	public Text TimeFull;

	public Text TimeCalcText;

	public GameObject LoveInfiniteTimeObj;

	public GameObject LoveTimeObj;

	public GameObject AdMask;

	public bool isCanQiandao;

	public bool isCanXinshoulibao;

	public bool isCanShouchong;

	public GameObject MapRewardBtn;

	public GameObject MapRewardBtnfinger;

	public GameObject mapDianObj;

	public Image MapRewardBtnImg;

	public Text RewardStarCount;

	public GameObject RenwuHongDian;

	public Text MapLevelText;

	public GameObject FengcheObj;

	public GameObject SignReward7UIObj;

	public GameObject mapBtnHongdian;

	public GameObject mapQianDaoHongdian;

	public GameObject mapVideoRewardHongdain;

	public Text daoJiShiTime;

	private int iTimefinger;

	private int iTimeAd;

	private int iTimeAdMax;

	private int iTimefingerMax;

	public GameObject haohuabtn;

	public GameObject testButtton;

	private static int OldGold;

	private bool b = true;

	private bool bTestAdPlay = true;

	private bool bShowAd;

	private static int OldGB;

	private bool bfree;

	public void InitHuabiChangeGB()
	{
	}

	public void resLanguage()
	{
		BaseUIAnimation.action.SetLanguageFont("LoveFull", TimeFull, string.Empty);
		ResRewardMap(iMapIndex);
		SetMapName(iMapIndex);
	}

	public void ResetSelectSkill()
	{
		for (int i = 0; i <= 3; i++)
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_Skill_Select_" + i, 0);
		}
	}

	private IEnumerator Time10()
	{
		int iCount = 20;
		while (true)
		{
			yield return new WaitForSeconds(iCount);
			InitAndroid.action.GAEvent("ShowMapUI:" + iCount);
			iCount += 20;
		}
	}

	private void Start()
	{
		aliyunlog.UserBag();
		InitAndroid.action.GAEvent("ShowMapUI:0");
		action = this;
		ResetSelectSkill();
		Singleton<DataManager>.Instance.PlayGameOpenSale = false;
		InitHuabiChangeGB();
		iTimefingerMax = 10;
		iTimefinger = iTimefingerMax;
		iTimeAdMax = 30;
		iTimeAd = iTimeAdMax;
		Singleton<DataManager>.Instance.isUpdateWinData = false;
		if (InitGame.bChinaVersion)
		{
			LoadGB();
			int nowTime = Util.GetNowTime();
			if ((bool)PayManager.action)
			{
				PayManager.action.LoadGold();
			}
			LoadGold(bUpdate: false);
			LoadGB();
		}
		BaseUIAnimation.action.SetLanguageFont("LoveFull", TimeFull, string.Empty);
		if ((bool)SoundFireController.action)
		{
			SoundFireController.action.stop();
		}
		Singleton<UserManager>.Instance.LoadNowPassLevelNumber();
		if (Singleton<LevelManager>.Instance.bFirstInMap)
		{
			iMapIndex = Singleton<UserManager>.Instance.iNowMapID;
		}
		else
		{
			int num = Singleton<LevelManager>.Instance.iNowSelectLevelIndex;
			if (!Singleton<LevelManager>.Instance.bExit && !Singleton<LevelManager>.Instance.bLoseGame)
			{
				num++;
			}
			int mapForLevelID = Singleton<UserManager>.Instance.GetMapForLevelID(num);
			iMapIndex = mapForLevelID - 1;
			if (Singleton<LevelManager>.Instance.bRstart6)
			{
				Singleton<LevelManager>.Instance.bRstart6 = false;
				if (num > 4)
				{
					mapForLevelID = Singleton<UserManager>.Instance.GetMapForLevelID(num - 1);
					iMapIndex = mapForLevelID - 1;
				}
			}
		}
		BaseUIAnimation.action.CreateButton(LeftIcon.gameObject);
		BaseUIAnimation.action.CreateButton(RightIcon.gameObject);
		if ((bool)MusicController.action)
		{
			MusicController.action.BG_menu();
		}
		LoadGold(bUpdate: false);
		if (Singleton<UserManager>.Instance.getLoveInfinite() <= 0)
		{
			LoveInfiniteTimeObj.SetActive(value: false);
			LoveTimeObj.SetActive(value: true);
			LoadLove();
			CalcTime();
			LoadTime();
			StartCoroutine(UpdateViewLove());
		}
		else
		{
			LoveInfiniteTimeObj.SetActive(value: true);
			LoveTimeObj.SetActive(value: false);
			LoadLoveInfiniteTime();
			StartCoroutine(UpdateLoveInfinite());
		}
		CheckFaceBookAskList();
		SetMapName(iMapIndex);
		if ((bool)LoadingSceneUI.action)
		{
			LoadingSceneUI.action.ChangeCanvasCamera(MapUISceneCamera);
		}
		if ((bool)FaceBookApi.Action)
		{
			FaceBookApi.Action.LoginSuccess();
		}
		//if ((bool)AdManager.action)
		//{
		//	AdManager.action.AdManagerShowButton();
		//}
		CheckFacebookMailCountIcon();
		InitShowGuan();
		PayManager.action.CheckOpenSign();
		if (InitGame.bChinaVersion)
		{
			MapRewardBtn.SetActive(value: true);
			ResCheckMapRewarDian();
			Singleton<UserManager>.Instance.ClearBackSkill();
		}
		else
		{
			if ((bool)MapRewardBtn)
			{
				MapRewardBtn.SetActive(value: false);
			}
			if ((bool)ttdaypk.action)
			{
				ttdaypk.action.CheckGuang();
			}
		}
		MapShowAd();
		ResChinaBtn();
		ChinaHaopingCheck();
		if (Singleton<DataManager>.Instance.bLiuhai)
		{
			TopLeftObj.transform.localPosition -= new Vector3(0f, 50f, 0f);
			TopRightObj.transform.localPosition -= new Vector3(0f, 50f, 0f);
			TopCenterObj.transform.localPosition -= new Vector3(0f, 50f, 0f);
		}
		AutoTIme();
		SetQiandaoRedDot();
		SetVideoRewardRedDot();
	}

	public void ResChinaBtn()
	{
		if (InitGame.bChinaVersion && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "PAYBubble_LB3") <= 0)
		{
		}
	}

	public void UpdateMyData()
	{
	}

	private IEnumerator IEUpdateUserLog(string str)
	{
		yield return new WWW(str);
	}

	public IEnumerator IEonPaySuccessChinaRewardID(string id)
	{
		yield return new WaitForSeconds(1f);
		InitAndroid.action.onPaySuccessChinaRewardID(id);
	}

	public void ChinaHaopingCheck()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_bHaoping");
		if (@int == 1)
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_bHaoping", 2);
			RewardHaoping();
		}
	}

	public void RewardHaoping()
	{
		Singleton<DataManager>.Instance.bhaoping = false;
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		list.Add(8);
		list2.Add(2);
		list.Add(1);
		list2.Add(30);
		list.Add(2);
		list2.Add(2500);
		ChinaPay.action.addRewardAll(8, 2, base.gameObject, isShow: false, "free", "haoping");
		ChinaPay.action.addRewardAll(1, 30, base.gameObject, isShow: false, "free", "haoping");
		ChinaPay.action.addRewardAll(2, 2500, base.gameObject, isShow: false, "free", "haoping");
		BaseUIAnimation.action.ShowProp(list, list2, base.gameObject);
	}

	public void MapShowAd()
	{
	}

	public void _IEUpdateLoveInfinite()
	{
		StartCoroutine(UpdateLoveInfinite());
	}

	public void ShowMapRewardForced_guidance()
	{
		if (InitGame.bChinaVersion)
		{
			Singleton<DataManager>.Instance.bLevel3OpenPlay = true;
			UI.Instance.OpenPanel(UIPanelType.MapRewardUI);
			InitAndroid.action.GAEvent("clickbtn:ClickMapRewardUIPanel:1");
		}
	}

	public void HideMapRewardForced_guidance()
	{
		if (!InitGame.bChinaVersion)
		{
		}
	}

	public void ResCheckMapRewarDian()
	{
		bool flag = false;
	}

	private void _iFirstLoginMap()
	{
		if (InitGame.bChinaVersion)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_FirstLoginMap");
			if (@int == 1)
			{
				Singleton<DataManager>.Instance.SaveUserDate("DB_FirstLoginMap", 0);
			}
			return;
		}
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_FirstLoginMap");
		if (int2 != 1)
		{
			return;
		}
		Singleton<DataManager>.Instance.SaveUserDate("DB_FirstLoginMap", 0);
		int int3 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_CFTC");
		if (int3 == 1)
		{
			return;
		}
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_CFTC", 1);
		if (FaceBookApi.Action.bLoginState())
		{
			int num = UnityEngine.Random.Range(0, 100);
			if (num < 80)
			{
				if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_DYCYQ") == 0)
				{
					Singleton<UIManager>.Instance.OpenUI(EnumUIType.EnFirstInvite);
				}
				else if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iDaySendLives" + Util.GetNowTime_Day()) == 0)
				{
					Singleton<UIManager>.Instance.OpenUI(EnumUIType.SendLivesUI);
				}
			}
			else if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_TLHDYH") == 0)
			{
				num = UnityEngine.Random.Range(0, 100);
				if (num < 40)
				{
					Singleton<UIManager>.Instance.OpenUI(EnumUIType.EnSuperSale);
				}
				else
				{
					UI.Instance.OpenPanel(UIPanelType.BuyLivesChinaUI);
				}
			}
			else
			{
				UI.Instance.OpenPanel(UIPanelType.BuyLivesChinaUI);
			}
			return;
		}
		int num2 = UnityEngine.Random.Range(0, 100);
		if (num2 < 80)
		{
			Singleton<UIManager>.Instance.OpenUI(EnumUIType.EnConnectFacebook);
		}
		else if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_TLHDYH") == 0)
		{
			num2 = UnityEngine.Random.Range(0, 100);
			if (num2 < 40)
			{
				Singleton<UIManager>.Instance.OpenUI(EnumUIType.EnSuperSale);
			}
			else
			{
				UI.Instance.OpenPanel(UIPanelType.BuyLivesChinaUI);
			}
		}
		else
		{
			UI.Instance.OpenPanel(UIPanelType.BuyLivesChinaUI);
		}
	}

	public void SaleAd1()
	{
		OneDayOneSigninUI();
	}

	public void OneDayOneSigninUI()
	{
	}

	public void clickTestBtn()
	{
		string nowTime_Day = Util.GetNowTime_Day();
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_iNowDaySaleAd3" + nowTime_Day, 0);
		UI.Instance.OpenPanel(UIPanelType.SaleAd3UI);
	}

	public void InitShowGuan()
	{
		if (Singleton<DataManager>.Instance.bGooglePay)
		{
			string nowTime_Day = Util.GetNowTime_Day();
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GGQianDaoBooL" + nowTime_Day);
			if (@int == 1)
			{
				isCanQiandao = false;
			}
			else
			{
				isCanQiandao = true;
			}
			return;
		}
		int month = DateTime.Now.Month;
		int day = DateTime.Now.Day;
		if (Singleton<DataManager>.Instance.GetUserDataI("DB_YJQD_" + month + "_" + day) == 0)
		{
			isCanQiandao = true;
		}
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_XINSHOULIBAO") == 0)
		{
			isCanXinshoulibao = true;
		}
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_CHONGZHI") != 0)
		{
			isCanShouchong = true;
		}
	}

	public void ResInvitationObj()
	{
	}

	public void ResInvitation()
	{
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
			TimeCalcText.gameObject.SetActive(value: false);
			return;
		}
		TimeFull.gameObject.SetActive(value: false);
		TimeCalcText.gameObject.SetActive(value: true);
		int num = iRtime;
		int seconds = num;
		TimeSpan timeSpan = new TimeSpan(0, 0, seconds);
		int minutes = timeSpan.Minutes;
		int num2 = timeSpan.Hours;
		int seconds2 = timeSpan.Seconds;
		int days = timeSpan.Days;
		if (days > 0)
		{
			num2 = days * 24 + num2;
		}
		string text = minutes + string.Empty;
		string str = num2 + string.Empty;
		string text2 = seconds2 + string.Empty;
		if (minutes < 10)
		{
			text = "0" + text;
		}
		if (num2 < 10)
		{
			str = "0" + str;
		}
		if (seconds2 < 10)
		{
			text2 = "0" + text2;
		}
		TimeCalcText.text = text + ":" + text2;
	}

	public void LoadLove()
	{
		if (!Singleton<DataManager>.Instance.StarGameFlage)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
			LoveText.text = @int.ToString();
		}
	}

	public void LoadGold(bool bUpdate = true, bool bType = true)
	{
		if (bUpdate)
		{
			StartCoroutine(IELoadGold(bType));
		}
		else
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
			GoldText.text = @int.ToString();
			OldGold = @int;
		}
		if ((bool)ChinaShopPanel.panel)
		{
			ChinaShopPanel.panel.LoadDataShopUI();
		}
	}

	private IEnumerator IELoadGold(bool bType)
	{
		yield return new WaitForSeconds(0.1f);
		bool b = true;
		int iMyGold = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		int _AddCount = (iMyGold - OldGold) / 8;
		if (!bType)
		{
			_AddCount = (OldGold - iMyGold) / 8;
		}
		if (_AddCount == 0)
		{
			_AddCount = 1;
		}
		if (!bType)
		{
		}
		while (b)
		{
			yield return new WaitForSeconds(0.1f);
			if (bType)
			{
				if (OldGold < iMyGold)
				{
					OldGold += _AddCount;
					GoldText.text = OldGold.ToString();
				}
				else
				{
					GoldText.text = iMyGold.ToString();
					OldGold = iMyGold;
					b = false;
				}
			}
			else if (OldGold > iMyGold)
			{
				OldGold -= _AddCount;
				GoldText.text = OldGold.ToString();
			}
			else
			{
				GoldText.text = iMyGold.ToString();
				OldGold = iMyGold;
				b = false;
			}
		}
	}

	public void ShowLodingUI()
	{
		LodingUI.SetActive(value: true);
		StartCoroutine(IEShowLodingUI());
	}

	public IEnumerator IEShowLodingUI()
	{
		yield return new WaitForSeconds(0.5f);
		LodingUI.SetActive(value: false);
	}

	public void HideDownUI1()
	{
	}

	public void ShowDownUI1()
	{
	}

	public void HideUI()
	{
	}

	public void ShowUI()
	{
	}

	public static GameObject TouchChecker(Vector3 mouseposition)
	{
		Vector3 vector = Camera.main.ScreenToWorldPoint(mouseposition);
		Vector2 point = new Vector2(vector.x, vector.y);
		if ((bool)Physics2D.OverlapPoint(point))
		{
			return Physics2D.OverlapPoint(point).gameObject;
		}
		return null;
	}

	private void ReturnClick()
	{
		if (Application.platform == RuntimePlatform.Android && UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			if ((bool)ExitUIPanel.panel)
			{
				UI.Instance.ClosePanel();
			}
			else if (!ExitUIPanel.panel && UI.Instance.GetPanelCount() > 0)
			{
				UI.Instance.ClosePanel();
			}
			else
			{
				UI.Instance.OpenPanel(UIPanelType.ExitUI);
			}
		}
		else if (UnityEngine.Input.GetKey("u") && b)
		{
			b = false;
			UnityEngine.Debug.Log("aaaa");
			if ((bool)ExitUIPanel.panel)
			{
				UI.Instance.ClosePanel();
			}
			else if (!ExitUIPanel.panel && UI.Instance.GetPanelCount() > 0)
			{
				UI.Instance.ClosePanel();
			}
			else
			{
				UI.Instance.OpenPanel(UIPanelType.ExitUI);
			}
		}
	}

	private IEnumerator UpdateTimeShowFinger()
	{
		while (true)
		{
			yield return new WaitForSeconds(1f);
			if (!BtnManager.action.isShowFinger && !Util.GetNowOpenUI())
			{
				iTimefinger--;
				if (iTimefinger <= 0)
				{
					iTimefinger = iTimefingerMax;
					BtnManager.action.ShowFinger();
				}
			}
		}
	}

	private IEnumerator UpdateTimeShowOppoAd()
	{
		while (true)
		{
			yield return new WaitForSeconds(1f);
			iTimeAd--;
			if (iTimeAd <= 0)
			{
				iTimeAd = iTimeAdMax;
				//AdManager.action.opadshowcp(DataManager.PAGE_MAIN);
			}
		}
	}

	private void Update()
	{
		SetVideoRewardRedDot();
		ReturnClick();
		if (UI.Instance.GetPanelCount() > 0 || Util.GetbForced_guidance() || Singleton<DataManager>.Instance.bGrilMoveing)
		{
			return;
		}
		if (Input.GetMouseButtonDown(0))
		{
			iTimefinger = iTimefingerMax;
			iTimeAd = iTimeAdMax;
			if (BtnManager.action.isShowFinger)
			{
				BtnManager.action.HideFinger();
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			oldPos = UnityEngine.Input.mousePosition;
		}
		if (Input.GetMouseButtonDown(0))
		{
			GameObject gameObject = TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject == null)
			{
				return;
			}
			if (gameObject.name == "DownUI")
			{
				bClickUI = true;
			}
			if (gameObject.name == "TopLeft")
			{
				bClickUI = true;
				bClickTopLeft = true;
			}
			else if (gameObject.name == "TopRight")
			{
				if (Singleton<DataManager>.Instance.bOpenReward7)
				{
					Singleton<DataManager>.Instance.bOpenReward7 = false;
				}
				else
				{
					bClickUI = true;
					bClickTopRight = true;
				}
			}
			else if (gameObject.name == "TopCenter")
			{
				bClickUI = true;
				bClickTopCenter = true;
			}
			if (Singleton<DataManager>.Instance.bUiIsOpen)
			{
				return;
			}
		}
		if (!Input.GetMouseButtonUp(0))
		{
			return;
		}
		GameObject gameObject2 = TouchChecker(UnityEngine.Input.mousePosition);
		if (gameObject2 == null)
		{
			bClickUI = false;
			bClickTopLeft = false;
			bClickTopRight = false;
			bClickTopCenter = false;
			return;
		}
		if (gameObject2.name == "TopLeft")
		{
			if (bClickTopLeft)
			{
				TopLeft();
			}
		}
		else if (gameObject2.name == "TopRight")
		{
			if (bClickTopRight)
			{
				TopRight();
			}
		}
		else if (gameObject2.name == "TopCenter" && bClickTopCenter)
		{
			TopCenter();
		}
		bClickTopLeft = false;
		bClickTopRight = false;
		bClickTopCenter = false;
		bClickUI = false;
	}

	public void TestAdPlay()
	{
		if (bTestAdPlay)
		{
			bTestAdPlay = false;
			if ((bool)MusicController.action)
			{
				MusicController.action.PlayAdCloseMp3();
			}
			if ((bool)SoundController.action)
			{
				MusicController.action.PlayAdCloseMp3();
			}
		}
		else if ((bool)MusicController.action)
		{
			MusicController.action.AdReturnOpenMp3();
		}
	}

	public void TestFirebase()
	{
	}

	public void GoLeftMap()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance())
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("ButtonClick");
			}
			if (BaseUIAnimation.bClickButton)
			{
				BaseUIAnimation.action.ClickButton(DownLeftBtn.gameObject);
				StartCoroutine(CallGoLeftMap());
			}
		}
	}

	private IEnumerator CallGoLeftMap()
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		if (iMapIndex != 0)
		{
			iMapIndex--;
			GoMap(iMapIndex);
		}
	}

	public void GoRightMap()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance())
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("ButtonClick");
			}
			if (BaseUIAnimation.bClickButton)
			{
				BaseUIAnimation.action.ClickButton(DownRightBtn.gameObject);
				StartCoroutine(CallGoRightMap());
			}
		}
	}

	private IEnumerator CallGoRightMap()
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		if (iMapIndex < UserManager.iMapCount - 1)
		{
			iMapIndex++;
			GoMap(iMapIndex);
		}
	}

	public void GoMap(int index)
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance())
		{
			iMapIndex = index;
			MapManagerUI.action.GoMap(index);
			Map1._map1.GoMap(index);
			BtnManager.action.GoMap(index);
			SetMapName(index);
			ResRewardMap(index);
		}
	}

	public void ResRewardMap(int _index)
	{
		int num = Singleton<DataManager>.Instance.LMapBtnCount[_index] * 3;
		int mapStar = Singleton<UserManager>.Instance.GetMapStar(_index);
		RewardStarCount.text = mapStar + "/" + num;
		string newValue = Singleton<DataManager>.Instance.dDataMapReward[((_index + 1) * 3).ToString()]["mapnumber"].ToString().Split('!')[0];
		string newValue2 = Singleton<DataManager>.Instance.dDataMapReward[((_index + 1) * 3).ToString()]["mapnumber"].ToString().Split('!')[1];
		string text = Singleton<DataManager>.Instance.dDataLanguage["MapLevelText1"][BaseUIAnimation.Language];
		text = text.Replace("A1", newValue);
		text = text.Replace("A2", newValue2);
		MapLevelText.text = text;
		float num2 = 0.001f;
		num2 = (float)mapStar * 100f / (float)num * 100f;
		num2 /= 10000f;
		if (num2 == 0f)
		{
			MapRewardBtnImg.sprite = Util.GetResourcesSprite("Img/levelmap/level_starcatcher_0", 173, 168);
		}
		else if (num2 >= 1f)
		{
			MapRewardBtnImg.sprite = Util.GetResourcesSprite("Img/levelmap/level_starcatcher_3", 173, 168);
		}
		else if (num2 >= 0.6f)
		{
			MapRewardBtnImg.sprite = Util.GetResourcesSprite("Img/levelmap/level_starcatcher_2", 173, 168);
		}
		else
		{
			MapRewardBtnImg.sprite = Util.GetResourcesSprite("Img/levelmap/level_starcatcher_1", 173, 168);
		}
		_MapRewardHondianRes(_index);
	}

	public void _MapRewardHondianRes(int _index)
	{
		RenwuHongDian.SetActive(value: false);
		if (MapRewardHondianRes(_index))
		{
			RenwuHongDian.SetActive(value: true);
		}
		MapBtnHongdianRes();
	}

	public void MapBtnHongdianRes()
	{
		mapBtnHongdian.SetActive(value: false);
		int num = 0;
		while (true)
		{
			if (num < UserManager.iMapCount)
			{
				if (MapRewardHondianRes(num))
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		mapBtnHongdian.SetActive(value: true);
	}

	public bool MapRewardHondianRes(int _index)
	{
		int num = Singleton<DataManager>.Instance.LMapBtnCount[_index] * 3;
		int mapStar = Singleton<UserManager>.Instance.GetMapStar(_index);
		bool flag = false;
		for (int i = 1; i <= 3; i++)
		{
			if (GetRewardFlag(_index + 1, i, mapStar))
			{
				return true;
			}
		}
		return false;
	}

	public bool GetRewardFlag(int iMapID, int iIndex, int iMapStart)
	{
		int mapStar = Singleton<UserManager>.Instance.GetMapStar(iMapID - 1);
		for (int i = 1; i <= Singleton<DataManager>.Instance.dDataMapReward.Count; i++)
		{
			int num = int.Parse(Singleton<DataManager>.Instance.dDataMapReward[i.ToString()]["iStar"]);
			int num2 = int.Parse(Singleton<DataManager>.Instance.dDataMapReward[i.ToString()]["Mapid"]);
			if (num2 != iMapID)
			{
				continue;
			}
			int num3 = int.Parse(Singleton<DataManager>.Instance.dDataMapReward[i.ToString()]["inumber"]);
			if (num3 == iIndex)
			{
				int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_MapReward" + iMapID + "_" + iIndex);
				if (@int != 1 && iMapStart >= num)
				{
					return true;
				}
				return false;
			}
		}
		return false;
	}

	public void SetMapName(int index)
	{
		if (InitGame.bChinaVersion)
		{
			BaseUIAnimation.action.SetLanguageFont("MapNameRemark" + (index + 1), CNMapNameRemark, string.Empty);
		}
		else
		{
			BaseUIAnimation.action.SetLanguageFont("MapNameRemark" + (index + 1), MapNameRemark, string.Empty);
		}
	}

	public void TopCenter()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance() && !Singleton<DataManager>.Instance.bUiIsOpen && BaseUIAnimation.bClickButton)
		{
			StartCoroutine(CallTopCenter());
		}
	}

	private IEnumerator CallTopCenter()
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		Singleton<DataManager>.Instance.ChinaShopOpendaoju = true;
		Singleton<DataManager>.Instance.ChinaShopOpenZuanshi = false;
		UI.Instance.OpenPanel(UIPanelType.ChinaShop);
		InitAndroid.action.GAEvent("clickbtn:BuyGB");
	}

	public void TopRight()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance() && !Singleton<DataManager>.Instance.bUiIsOpen && BaseUIAnimation.bClickButton)
		{
			StartCoroutine(CallTopRight());
			InitAndroid.action.GAEvent("clickbtn:BuyZs");
		}
	}

	private IEnumerator CallTopRight()
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		Singleton<DataManager>.Instance.ChinaShopOpenZuanshi = true;
		Singleton<DataManager>.Instance.ChinaShopOpendaoju = false;
		if (InitGame.bChinaVersion)
		{
			UI.Instance.OpenPanel(UIPanelType.ChinaShop);
		}
		else
		{
			UI.Instance.OpenPanel(UIPanelType.ChinaShop);
		}
	}

	public void ClickShopUI()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance() && !Singleton<DataManager>.Instance.bUiIsOpen && BaseUIAnimation.bClickButton)
		{
			StartCoroutine(CallShopUI());
		}
	}

	public void ClickGoogleSigninUI()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance() && !Singleton<DataManager>.Instance.bUiIsOpen && BaseUIAnimation.bClickButton)
		{
			StartCoroutine(CallGoogleSigninUI());
		}
	}

	private IEnumerator CallGoogleSigninUI()
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		Singleton<UIManager>.Instance.OpenUI(EnumUIType.GGqiandaoUI);
	}

	private IEnumerator CallShopUI()
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_DoublePay") != 0)
		{
			Singleton<DataManager>.Instance.ChinaShopOpen = true;
		}
		UI.Instance.OpenPanel(UIPanelType.ChinaShop);
	}

	public void ClickFirstPacksUI()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance() && !Singleton<DataManager>.Instance.bUiIsOpen && BaseUIAnimation.bClickButton)
		{
			StartCoroutine(CallFirstPacksUI());
		}
	}

	private IEnumerator CallFirstPacksUI()
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		Singleton<UIManager>.Instance.OpenUI(EnumUIType.FirstPacksUI);
	}

	public void TopLeft()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance() && !Singleton<DataManager>.Instance.bUiIsOpen && BaseUIAnimation.bClickButton)
		{
			StartCoroutine(CallTopLeft());
		}
	}

	private IEnumerator CallTopLeft()
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		InitAndroid.action.GAEvent("clickbtn:BuyLivesChinaUI");
		if (InitGame.bChinaVersion)
		{
			UI.Instance.OpenPanel(UIPanelType.BuyLivesChinaUI);
		}
		else
		{
			Singleton<UIManager>.Instance.OpenUI(EnumUIType.BuyLivesUI);
		}
	}

	public void ClickSigninUI()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance() && !Singleton<DataManager>.Instance.bUiIsOpen && BaseUIAnimation.bClickButton)
		{
			StartCoroutine(CallSigninUI());
		}
	}

	private IEnumerator CallSigninUI()
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		Singleton<UIManager>.Instance.OpenUI(EnumUIType.RewardBtnUI);
	}

	public void ClickInvitation()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance())
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("ButtonClick");
			}
			if (!PayManager.action.OpenPay)
			{
				Singleton<UIManager>.Instance.OpenUI(EnumUIType.InviteFriendsUI);
			}
			else if (FaceBookApi.Action.bLoginState())
			{
				Singleton<UIManager>.Instance.OpenUI(EnumUIType.InviteFriendsUI);
			}
			else
			{
				FaceBookApi.Action.FackBookLogin();
			}
		}
	}

	public void ShowAdMask()
	{
		bShowAd = true;
		AdMask.SetActive(value: true);
		StartCoroutine(_ShowAdMask());
	}

	private IEnumerator _ShowAdMask()
	{
		yield return new WaitForSeconds(1f);
		Singleton<DataManager>.Instance.bUiIsOpen = true;
		yield return new WaitForSeconds(5f);
		Singleton<DataManager>.Instance.bUiIsOpen = false;
		AdMask.SetActive(value: false);
	}

	public void CheckFacebookMailCountIcon()
	{
		if (FaceBookApi.Action.bLoginState())
		{
			FaceBookApi.Action.ResEmail();
		}
	}

	public void LoadGB(bool bUpdate = false, bool bType = true)
	{
		if (bUpdate)
		{
			StartCoroutine(IELoadGB(bType));
		}
		else
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GB");
			GBText.text = @int.ToString();
			OldGB = @int;
		}
		if ((bool)ChinaShopPanel.panel)
		{
			ChinaShopPanel.panel.LoadDataShopUI();
		}
	}

	private IEnumerator IELoadGB(bool bType = true)
	{
		yield return new WaitForSeconds(0.1f);
		bool b = true;
		int iMyGB = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GB");
		int _AddCount = (iMyGB - OldGB) / 8;
		if (!bType)
		{
			_AddCount = (OldGB - iMyGB) / 8;
		}
		if (!bType)
		{
		}
		while (b)
		{
			yield return new WaitForSeconds(0.1f);
			if (bType)
			{
				if (OldGB < iMyGB)
				{
					OldGB += _AddCount;
					GBText.text = OldGB.ToString();
				}
				else
				{
					GBText.text = iMyGB.ToString();
					OldGB = iMyGB;
					b = false;
				}
			}
			else if (OldGB > iMyGB)
			{
				OldGB -= _AddCount;
				GBText.text = OldGB.ToString();
			}
			else
			{
				GBText.text = iMyGB.ToString();
				OldGB = iMyGB;
				b = false;
			}
		}
	}

	public void ChangeScenceGame()
	{
		StartCoroutine(IEChangeScenceGame());
	}

	public IEnumerator IEChangeScenceGame()
	{
		yield return new WaitForSeconds(2f);
		Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.GameScene);
	}

	public void CheckFaceBookAskList()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_FacebookLogin");
		bool flag = false;
		if (@int == 0 && FaceBookApi.Action.bLoginState())
		{
			FaceBookApi.Action.ReadMailFB();
		}
	}

	private void AutoTIme()
	{
		int num = 86400;
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_24hRewardTime");
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_24hReward") == 0)
		{
			int nowTime = Util.GetNowTime();
			if (nowTime - @int > num)
			{
				daoJiShiTime.gameObject.SetActive(value: false);
				return;
			}
			int num2 = num - (nowTime - @int);
			SetTime(num2);
			StartCoroutine(IESetTime(num2));
		}
	}

	private IEnumerator IESetTime(int iTime)
	{
		yield return new WaitForSeconds(1f);
		bool b = true;
		while (b)
		{
			if (iTime < 0)
			{
				b = false;
				AutoTIme();
			}
			else
			{
				iTime--;
			}
			SetTime(iTime);
			yield return new WaitForSeconds(1f);
		}
	}

	private void SetTime(int iTime)
	{
		if (iTime < 0)
		{
			daoJiShiTime.gameObject.SetActive(value: false);
			return;
		}
		if (iTime < 1)
		{
			iTime = 1;
		}
		TimeSpan timeSpan = new TimeSpan(0, 0, iTime);
		int minutes = timeSpan.Minutes;
		int hours = timeSpan.Hours;
		int seconds = timeSpan.Seconds;
		string text = minutes + string.Empty;
		string text2 = hours + string.Empty;
		string text3 = seconds + string.Empty;
		if (minutes < 10)
		{
			text = "0" + text;
		}
		if (hours < 10)
		{
			text2 = "0" + text2;
		}
		if (seconds < 10)
		{
			text3 = "0" + text3;
		}
		daoJiShiTime.text = text2 + ":" + text + ":" + text3;
	}

	public void SetQiandaoRedDot()
	{
		string nowTime_Day = Util.GetNowTime_Day();
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iqiandao7Count", 1);
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SignOK7" + nowTime_Day);
		if (@int > 7 || int2 == 1)
		{
			if (mapQianDaoHongdian != null)
			{
				mapQianDaoHongdian.gameObject.SetActive(value: false);
			}
		}
		else if (mapQianDaoHongdian != null)
		{
			mapQianDaoHongdian.gameObject.SetActive(value: true);
		}
	}

	public void SetVideoRewardRedDot()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_VideoNextTime");
		int nowTime = Util.GetNowTime();
		if (nowTime >= @int)
		{
			if (mapVideoRewardHongdain != null)
			{
				mapVideoRewardHongdain.SetActive(value: true);
			}
		}
		else if (mapVideoRewardHongdain != null)
		{
			mapVideoRewardHongdain.SetActive(value: false);
		}
	}
}
