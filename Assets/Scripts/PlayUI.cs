using DG.Tweening;
using EasyMobile;
using System.Collections;
using ITSoft;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : BaseUI
{
	public static PlayUI action;

	public Text LevelRemark;

	public Text LevelTitle;

	public GameObject StartBtn;

	public GameObject Star1;

	public GameObject Star2;

	public GameObject Star3;

	public GameObject SelectSkillFather;

	public GameObject SelectSkillObj;

	public GameObject CloseBtn;

	public GameObject AdBtn;

	public Text AdBtnText;

	public Text PlayUIStarBtnText;

	public Text PlayUILevelFaceBookConn;

	public Text PlayUIFaceBookConAwardText;

	public Sprite StartBtnSprite;

	public Sprite PlayStartBtnSprite;

	public GameObject ConnectObj;

	public GameObject ImageGuideObj;

	public GameObject fingerObj;

	public GameObject LoveMoveObj;

	public GameObject fx_life;

	public GameObject ChinaLoveImage;

	public GameObject TopObj;

	public GameObject HaopingObj;

	public GameObject FaceBookObj;

	private bool bAutoStart_;

	public int ClickGuide;

	private int ifinger;

	private int ifingercount = 2;

	public int iPlayAdShowMax;

	public bool bLoveFly;

	private int iAdType;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.PlayUI;
	}

	public void ClickHaoping()
	{
		if (!bLoveFly)
		{
			Singleton<DataManager>.Instance.ClickHaoping();
		}
	}

	public void CloseHaoping()
	{
		HaopingObj.SetActive(value: false);
	}

	public void YunbuCheck()
	{
		if (InitGame.bEnios)
		{
			HaopingObj.SetActive(value: false);
		}
		else if (Singleton<DataManager>.Instance.bChinaIos)
		{
			HaopingObj.SetActive(value: false);
		}
		else if (Singleton<DataManager>.Instance.bShowHaopingLogin)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_bHaoping");
			if (@int > 0)
			{
				HaopingObj.SetActive(value: false);
			}
			else
			{
				Singleton<DataManager>.Instance.SendyunbuCheck();
			}
		}
		else
		{
			HaopingObj.SetActive(value: false);
		}
	}

	public override void OnStart()
	{
		Singleton<LevelManager>.Instance.bRstart3 = false;
		Singleton<LevelManager>.Instance.bRstart2 = false;
		Singleton<LevelManager>.Instance.bRstart4 = false;
		Singleton<LevelManager>.Instance.bRstart5 = false;
        AdsManager.ShowBanner();
        Singleton<DataManager>.Instance.bplayExitMap = false;
		Singleton<DataManager>.Instance.bGrilMoveing = false;
		OpenGuide();
		Singleton<DataManager>.Instance.bLevel3OpenPlay = false;
		if (InitGame.bChinaVersion && !InitGame.bEnios)
		{
			FaceBookObj.SetActive(value: false);
			HaopingObj.SetActive(value: true);
			YunbuCheck();
		}
		else
		{
			HaopingObj.SetActive(value: false);
			FaceBookObj.SetActive(value: true);
			fingerObj.SetActive(value: false);
		}
		if (InitGame.bChinaVersion && !InitGame.bEnios)
		{
			ChinaLoveImage.SetActive(value: true);
			PlayUIStarBtnText.fontSize -= 10;
			PlayUIStarBtnText.transform.localPosition -= new Vector3(40f, 0f, 0f);
			iPlayAdShowMax = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_PlayAdShowMax" + Util.GetNowTime_Day(), 10);
			if (Singleton<DataManager>.Instance.bAutoPlayGame && ClickGuide == 0)
			{
				Singleton<DataManager>.Instance.bAutoPlayGame = false;
				bAutoStart_ = true;
				int num = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
				int loveInfinite = Singleton<UserManager>.Instance.getLoveInfinite();
				if (loveInfinite > 0)
				{
					num = 5;
				}
				if (num >= Singleton<DataManager>.Instance.iLoveUse)
				{
					StartGame(bstart: true);
					return;
				}
			}
		}
		Singleton<LevelManager>.Instance.bRstart = false;
		if ((bool)ConnectObj)
		{
			BaseUIAnimation.action.CreateButton(ConnectObj.gameObject);
			if ((bool)FaceBookApi.Action)
			{
				FaceBookApi.Action.CheckLoginIcon(ConnectObj);
			}
		}
		if (Singleton<LevelManager>.Instance.bGoNextMap)
		{
			Singleton<LevelManager>.Instance.bGoNextMap = false;
		}
		action = this;
		BaseUIAnimation.action.CreateButton(StartBtn.gameObject);
		int iNowSelectLevelIndex = Singleton<LevelManager>.Instance.iNowSelectLevelIndex;
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
		if (iNowSelectLevelIndex > @int + 1)
		{
			CloseUI();
			return;
		}
		string lEVELID = Singleton<DataManager>.Instance.dDataLevelTypeAndRemark[string.Empty + iNowSelectLevelIndex]["sRemark"].ToString();
		BaseUIAnimation.action.SetLanguageFont("PlayUIRemark", LevelRemark, lEVELID);
		BaseUIAnimation.action.SetLanguageFont("PlayUILevelTitle", LevelTitle, iNowSelectLevelIndex.ToString());
		BaseUIAnimation.action.SetLanguageFont("PlayUIStarBtnText", PlayUIStarBtnText, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("PlayUIFaceBookConAwardText", PlayUIFaceBookConAwardText, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("PlayUILevelFaceBookConn", PlayUILevelFaceBookConn, string.Empty);
		LoadStar(Singleton<UserManager>.Instance.GetLevelStar(iNowSelectLevelIndex));
		LoadSelectSkill();
		Singleton<LevelManager>.Instance.bGoNextMap = false;
		if (AdsManager.RewardIsReady())
		{
			ShwAdIcon(3);
		}
		if ((bool)MusicController.action)
		{
			MusicController.action.AdReturnOpenMp3();
		}
	}

	private void LoadSelectSkill()
	{
		for (int i = 1; i <= 3; i++)
		{
			GameObject gameObject = Object.Instantiate(SelectSkillObj);
			gameObject.transform.SetParent(SelectSkillFather.transform, worldPositionStays: false);
			gameObject.SetActive(value: true);
			PlaySkillSelect component = gameObject.GetComponent<PlaySkillSelect>();
			component.SetType(i);
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Skill_Select_" + i);
			if (@int == 1)
			{
				component.LoadSelect();
			}
		}
	}

	public void LoadStar(int iStart)
	{
		if (iStart >= 1)
		{
			Star1.SetActive(value: true);
		}
		else
		{
			Star1.SetActive(value: false);
		}
		if (iStart >= 2)
		{
			Star2.SetActive(value: true);
		}
		else
		{
			Star2.SetActive(value: false);
		}
		if (iStart >= 3)
		{
			Star3.SetActive(value: true);
		}
		else
		{
			Star3.SetActive(value: false);
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ifinger = 0;
		}
	}

	public void ClosePlayUI(bool bDouble = false, bool bClickClose = true)
	{
		if (ClickGuide <= 0)
		{
			StartCoroutine(CallCloseUI(bDouble));
		}
	}

	public void _ClosePlayUI()
	{
		if (!bLoveFly && ClickGuide <= 0)
		{
			SaleAdClose();
			if (BaseUIAnimation.bClickButton)
			{
				BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
				StartCoroutine(CallCloseUI());
			}
		}
	}

	private IEnumerator CallCloseUI(bool bDouble = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		Singleton<DataManager>.Instance.bplayExitMap = true;
		CloseUI(bDouble);
		MapUI.action.ShowDownUI1();
	}

	public void OpenGuide()
	{
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 16)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_ClickGuideBy_16");
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_ClickGuideBy_16", 1);
			if (@int == 0)
			{
				ClickGuide = 1;
				SetGuideImagePos(-188);
			}
		}
		else if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 28)
		{
			int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_ClickGuideBy_28");
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_ClickGuideBy_28", 1);
			if (int2 == 0)
			{
				ClickGuide = 2;
				SetGuideImagePos(0);
			}
		}
		else if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 56)
		{
			int int3 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_ClickGuideBy_56");
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_ClickGuideBy_56", 1);
			if (int3 == 0)
			{
				ClickGuide = 3;
				SetGuideImagePos(188);
			}
		}
		if (ClickGuide > 0)
		{
			fingerObj.SetActive(value: false);
		}
		else if (InitGame.bChinaVersion && !InitGame.bEnios)
		{
			StartCoroutine(IEShowFingerObj());
		}
	}

	private IEnumerator IEShowFingerObj()
	{
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex <= 15)
		{
			yield return new WaitForSeconds(0.1f);
			fingerObj.SetActive(value: true);
			yield break;
		}
		bool b = true;
		while (b)
		{
			yield return new WaitForSeconds(1f);
			if (ifinger > ifingercount)
			{
				fingerObj.SetActive(value: true);
				b = false;
			}
			ifinger++;
		}
	}

	public void SetGuideImagePos(int x)
	{
		ImageGuideObj.gameObject.SetActive(value: true);
		ImageGuideObj.transform.localPosition = new Vector3(x, -80f, 0f);
		GuideImagePosAni(ImageGuideObj);
	}

	private void GuideImagePosAni(GameObject obj)
	{
		if (ClickGuide == 0)
		{
			ImageGuideObj.gameObject.SetActive(value: false);
			return;
		}
		Vector3 localPosition = obj.transform.localPosition;
		float y = localPosition.y;
		Sequence s = DOTween.Sequence();
		s.Append(obj.transform.DOLocalMoveY(y + 50f, 0.6f).SetEase(Ease.InOutSine)).Append(obj.transform.DOLocalMoveY(y, 0.6f).SetEase(Ease.InOutSine).OnComplete(delegate
		{
			GuideImagePosAni(obj);
		}));
	}

	protected override void OnAwake()
	{
		if ((bool)GameUI.action)
		{
			Canvas component = base.gameObject.transform.GetComponent<Canvas>();
			component.renderMode = RenderMode.ScreenSpaceCamera;
			component.worldCamera = GameUI.action.mainCameraS.GetComponent<Camera>();
		}
		else
		{
			Canvas component2 = base.gameObject.transform.GetComponent<Canvas>();
			component2.renderMode = RenderMode.ScreenSpaceCamera;
			component2.worldCamera = MapUI.action.MapUISceneCamera.GetComponent<Camera>();
		}
		base.OnAwake();
	}

	protected override void OnRelease()
	{
		base.OnRelease();
	}

	private IEnumerator IEStarGame()
	{
		if (ClickGuide != 0)
		{
			yield break;
		}
		int iLove = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
		int _iLoveTime = Singleton<UserManager>.Instance.getLoveInfinite();
		if (_iLoveTime > 0)
		{
			iLove = Singleton<DataManager>.Instance.iLoveMaxAll;
		}
		if (iLove > Singleton<DataManager>.Instance.iLoveUse)
		{
			if ((bool)MapUI.action)
			{
				MapUI.action.LoadLove();
			}
			Singleton<UserManager>.Instance.EnterLog();
			Singleton<LevelManager>.Instance.bLoadOver = false;
			bool b = true;
			if (LevelManager.bWwwDataFlag)
			{
				while (b)
				{
					yield return new WaitForSeconds(0.1f);
					if (Singleton<LevelManager>.Instance.bLoadOver)
					{
						b = false;
						Camera.main.transform.GetComponent<RapidBlurEffect>().enabled = false;
						Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.GameScene);
					}
				}
			}
			else
			{
				Camera.main.transform.GetComponent<RapidBlurEffect>().enabled = false;
				Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.GameScene);
			}
		}
		else if (InitGame.bChinaVersion)
		{
			if (InitGame.bCloseLBForEnIos)
			{
				Singleton<DataManager>.Instance.bAutoPlayGame = true;
				EnumUIType[] uiTypes = new EnumUIType[2]
				{
					EnumUIType.BuyLivesChinaUI,
					EnumUIType.PlayUI
				};
				DataManager.SelectLevel = 0;
				Singleton<UIManager>.Instance.OpenUI(uiTypes);
				ClosePlayUI(bDouble: true);
			}
			else
			{
				Singleton<DataManager>.Instance.bAutoPlayGame = true;
				DataManager.sale_adKey = "Bubble_LB4";
				DataManager.SelectLevel = 0;
				Singleton<UIManager>.Instance.OtherOpenUI = EnumUIType.SaleAdUI;
				Singleton<DataManager>.Instance.OpenPlayForLive = EnumUIType.PlayUI;
				CloseUI(bDouble: false, bOpenOther: true);
			}
		}
		else
		{
			Singleton<DataManager>.Instance.bAutoPlayGame = true;
			EnumUIType[] uiTypes2 = new EnumUIType[2]
			{
				EnumUIType.BuyLivesUI,
				EnumUIType.PlayUI
			};
			DataManager.SelectLevel = 0;
			Singleton<UIManager>.Instance.OpenUI(uiTypes2);
			ClosePlayUI(bDouble: true);
		}
	}

	public bool CheckDay()
	{
		string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_InitFistLoginGameDayT", string.Empty);
		string nowTime_Day = Util.GetNowTime_Day();
		if (@string == nowTime_Day)
		{
			return true;
		}
		return false;
	}

	public bool SaleAd()
	{
		if (!InitGame.bChinaVersion)
		{
			return false;
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex < 20)
		{
			return false;
		}
		if (bAutoStart_)
		{
			return false;
		}
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_PlayAdShow" + Util.GetNowTime_Day());
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_PlayAdShow" + Util.GetNowTime_Day(), @int + 1);
		if (@int == 0)
		{
			return false;
		}
		if (@int > 14 || @int > iPlayAdShowMax)
		{
			int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_PlayAdShow6" + Util.GetNowTime_Day());
			if (int2 == 0)
			{
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_PlayAdShow6" + Util.GetNowTime_Day(), Util.GetNowTime());
				return false;
			}
			if (Util.GetNowTime() - int2 > 21600)
			{
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_PlayAdShow6" + Util.GetNowTime_Day(), 0);
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_PlayAdShow" + Util.GetNowTime_Day(), 1);
			}
			return false;
		}
		return PlayAdShow(@int);
	}

	public bool SaleAdClose()
	{
		if (!InitGame.bChinaVersion)
		{
			return false;
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex < 20)
		{
			return false;
		}
		if (bAutoStart_)
		{
			return false;
		}
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_CloseAdShow" + Util.GetNowTime_Day());
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_CloseAdShow" + Util.GetNowTime_Day(), @int + 1);
		if (@int == 0)
		{
			return false;
		}
		if (@int > 14 || @int > iPlayAdShowMax)
		{
			int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_CloseAdShow6" + Util.GetNowTime_Day());
			if (int2 == 0)
			{
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_CloseAdShow6" + Util.GetNowTime_Day(), Util.GetNowTime());
				return false;
			}
			int num = Util.GetNowTime() - int2;
			UnityEngine.Debug.Log("itime=" + num);
			if (num > 21600)
			{
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_CloseAdShow6" + Util.GetNowTime_Day(), 0);
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_CloseAdShow" + Util.GetNowTime_Day(), 1);
			}
			return false;
		}
		return PlayAdShow(@int, playorcolse: false);
	}

	private bool PlayAdShow(int iPlayAdShow, bool playorcolse = true)
	{
		string str = "DB_PlayAdShow";
		if (playorcolse)
		{
			str = "DB_CloseAdShow";
		}
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "PAYBubble_LB2" + Util.GetNowTime_Day());
		if (@int == 1)
		{
			return false;
		}
		@int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "PAYBubble_LB3" + Util.GetNowTime_Day());
		if (@int == 1)
		{
			return false;
		}
		if (playorcolse)
		{
			Singleton<DataManager>.Instance.bAutoPlayGame = true;
		}
		DataManager.sale_adKey = string.Empty;
		if (iPlayAdShow == 1 || iPlayAdShow == 2 || DataManager.sale_adKey == string.Empty)
		{
			if (iPlayAdShow == 2)
			{
				iPlayAdShow++;
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_PlayAdShowMax" + Util.GetNowTime_Day(), iPlayAdShowMax + 1);
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + str + Util.GetNowTime_Day(), iPlayAdShow);
			}
			else
			{
				iPlayAdShow += 2;
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_PlayAdShowMax" + Util.GetNowTime_Day(), iPlayAdShowMax + 2);
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + str + Util.GetNowTime_Day(), iPlayAdShow);
			}
		}
		if (iPlayAdShow == 3 || iPlayAdShow == 4 || DataManager.sale_adKey == string.Empty)
		{
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_DoublePay") == 0)
			{
				DataManager.sale_adKey = "Pay2";
			}
			else if (iPlayAdShow == 4)
			{
				iPlayAdShow++;
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_PlayAdShowMax" + Util.GetNowTime_Day(), iPlayAdShowMax + 1);
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + str + Util.GetNowTime_Day(), iPlayAdShow);
			}
			else
			{
				iPlayAdShow += 2;
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_PlayAdShowMax" + Util.GetNowTime_Day(), iPlayAdShowMax + 2);
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + str + Util.GetNowTime_Day(), iPlayAdShow);
			}
		}
		if (iPlayAdShow == 5 || iPlayAdShow == 6 || DataManager.sale_adKey == string.Empty)
		{
			DataManager.sale_adKey = "Bubble_LB3";
		}
		if (iPlayAdShow == 7 || iPlayAdShow == 8 || DataManager.sale_adKey == string.Empty)
		{
			DataManager.sale_adKey = "Bubble_LB2";
		}
		if (DataManager.ChannelId != "dianxin" && DataManager.ChannelId != "xiaowo")
		{
			if (iPlayAdShow == 9 || iPlayAdShow == 10 || DataManager.sale_adKey == string.Empty)
			{
				DataManager.sale_adKey = "Bubble_LB8";
			}
			if (iPlayAdShow == 11 || iPlayAdShow == 12 || DataManager.sale_adKey == string.Empty)
			{
				DataManager.sale_adKey = "Bubble_LB6";
			}
			if (iPlayAdShow == 13 || iPlayAdShow == 14 || DataManager.sale_adKey == string.Empty)
			{
				DataManager.sale_adKey = "Bubble_LB7";
			}
		}
		if (DataManager.sale_adKey == string.Empty)
		{
			if (iPlayAdShow % 2 == 0)
			{
				DataManager.sale_adKey = "Bubble_LB3";
			}
			else
			{
				DataManager.sale_adKey = "Bubble_LB2";
			}
		}
		if (playorcolse)
		{
			if (!InitGame.bCloseLBForEnIos)
			{
				Singleton<DataManager>.Instance.bCloseSaleOpenPlayUI = true;
				CloseUI();
			}
		}
		else
		{
			Singleton<DataManager>.Instance.bClosePlayUIOpenSale = true;
		}
		return true;
	}

	public bool SaleAd1234567()
	{
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 20)
		{
			return SaleAd2();
		}
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_ThisLoginEnterGame");
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex > 21 && @int % 8 == 0 && @int > 5)
		{
			return SaleAd2();
		}
		return false;
	}

	private bool SaleAd2()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_ThisLoginPlay");
		if (@int == 1)
		{
			return false;
		}
		Singleton<DataManager>.Instance.SaveUserDate("DB_ThisLoginPlay", 1);
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "PAYBubble_LB3" + Util.GetNowTime_Day()) == 0 && !InitGame.bCloseLBForEnIos)
		{
			Singleton<DataManager>.Instance.bAutoPlayGame = true;
			DataManager.sale_adKey = "Bubble_LB3";
			EnumUIType[] uiTypes = new EnumUIType[2]
			{
				EnumUIType.SaleAdUI,
				EnumUIType.PlayUI
			};
			Singleton<UIManager>.Instance.OpenUI(uiTypes);
			ClosePlayUI(bDouble: true);
			return true;
		}
		return false;
	}

	public Vector3 GetCenterPost(Vector3 Start, Vector3 End, float r = 0f, bool _bOrientation = false)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = (Start.x + End.x) / 2f;
		float num4 = (Start.y + End.y) / 2f;
		num = num3 - Start.x;
		num2 = num4 - Start.y;
		float num5 = 0f;
		num5 = num;
		if (_bOrientation)
		{
			num = num2 / r;
			num2 = (0f - num5) / r;
		}
		else
		{
			num = (0f - num2) / r;
			num2 = num5 / r;
		}
		num = num3 - num;
		num2 = num4 - num2;
		return new Vector3(num, num2, Start.z);
	}

	private void LoveFly(GameObject obj, Vector3 ToPos)
	{
		if ((bool)MapUI.action)
		{
			GameObject gameObject = MapUI.action.TopLeftObj.transform.parent.gameObject;
			MapUI.action.TopLeftObj.gameObject.transform.SetParent(TopObj.transform);
			obj.transform.localPosition = MapUI.action.TopLeftObj.transform.localPosition;
			obj.transform.localPosition -= new Vector3(90f, 0f, 0f);
			MapUI.action.TopLeftObj.gameObject.transform.SetParent(gameObject.transform);
			obj.SetActive(value: true);
			Vector3 centerPost = GetCenterPost(obj.transform.localPosition, ToPos, 9f);
			Vector3[] path = new Vector3[3]
			{
				obj.transform.localPosition,
				centerPost,
				ToPos
			};
			obj.transform.DOLocalPath(path, 0.7f, PathType.CatmullRom, PathMode.TopDown2D).SetEase(Ease.InSine).OnComplete(delegate
			{
				StartGM();
			});
		}
		else
		{
			StartGM();
		}
	}

	private void StartGM()
	{
		fx_life.SetActive(value: true);
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_life_fly");
		}
		StartCoroutine(LoveStart());
	}

	private IEnumerator LoveStart()
	{
		if ((bool)MapUI.action)
		{
			Singleton<DataManager>.Instance.StarGameFlage = true;
			int loveInfinite = Singleton<UserManager>.Instance.getLoveInfinite();
			if (loveInfinite <= 0)
			{
				int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
				MapUI.action.LoveText.text = (@int - 5).ToString();
			}
		}
		yield return new WaitForSeconds(0.7f);
		Singleton<DataManager>.Instance.StarGameFlage = false;
		StartGame(bstart: true);
	}

	public void _ClickStart()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		StartGame();
	}

	public void StartGame(bool bstart = false)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
		}
		if (InitGame.bChinaVersion)
		{
			if (ClickGuide > 0)
			{
				return;
			}
			int num = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
			int loveInfinite = Singleton<UserManager>.Instance.getLoveInfinite();
			if (loveInfinite > 0)
			{
				num = Singleton<DataManager>.Instance.iLoveMaxAll;
			}
			if (num >= Singleton<DataManager>.Instance.iLoveUse && !bstart)
			{
				LoveMoveObj.SetActive(value: true);
				Vector3 vector = new Vector3(0f, 0f, 0f);
				if (!bLoveFly)
				{
					bLoveFly = true;
					LoveFly(LoveMoveObj, ChinaLoveImage.transform.localPosition);
				}
				return;
			}
		}
		if (LevelManager.bWwwDataFlag)
		{
			Singleton<LevelManager>.Instance.LoadLevelData();
			DataManager.SelectLevel = 0;
			StartCoroutine(IEStarGame());
			return;
		}
		int num2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
		int loveInfinite2 = Singleton<UserManager>.Instance.getLoveInfinite();
		if (loveInfinite2 > 0)
		{
			num2 = Singleton<DataManager>.Instance.iLoveMaxAll;
		}
		if (num2 >= Singleton<DataManager>.Instance.iLoveUse)
		{
			if (!SaleAd())
			{
				if ((bool)MapUI.action)
				{
					MapUI.action.LoadLove();
				}
				Singleton<UserManager>.Instance.EnterLog();
				Singleton<LevelManager>.Instance.bLoadOver = false;
				Camera.main.transform.GetComponent<RapidBlurEffect>().enabled = false;
				Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.GameScene);
			}
		}
		else if (InitGame.bChinaVersion && !InitGame.bCloseLBForEnIos)
		{
			Singleton<DataManager>.Instance.bAutoPlayGame = true;
			DataManager.sale_adKey = "Bubble_LB4";
			DataManager.SelectLevel = 0;
			Singleton<UIManager>.Instance.OtherOpenUI = EnumUIType.SaleAdUI;
			Singleton<DataManager>.Instance.OpenPlayForLive = EnumUIType.PlayUI;
			CloseUI(bDouble: false, bOpenOther: true);
		}
		else
		{
			Singleton<DataManager>.Instance.bAutoPlayGame = true;
			EnumUIType[] uiTypes = new EnumUIType[2]
			{
				EnumUIType.BuyLivesChinaUI,
				EnumUIType.PlayUI
			};
			DataManager.SelectLevel = 0;
			Singleton<UIManager>.Instance.OpenUI(uiTypes);
			ClosePlayUI(bDouble: true);
		}
	}

	public void ClickAddSkillbtn1()
	{
	}

	public void ClickAddSkillbtn2()
	{
	}

	public void ClickAddSkillbtn3()
	{
	}

	public void ShwAdIcon(int _iAdType)
	{
		iAdType = _iAdType;
		Image component = StartBtn.GetComponent<Image>();
		component.sprite = StartBtnSprite;
		component.SetNativeSize();
		AdBtn.SetActive(value: true);
		if (_iAdType == 1)
		{
			BaseUIAnimation.action.SetLanguageFont("PlayUIAdGift", AdBtnText, string.Empty);
		}
		else
		{
			BaseUIAnimation.action.SetLanguageFont("PlayUIAdLive", AdBtnText, string.Empty);
		}
		StartBtn.transform.localPosition -= new Vector3(150f, 0f, 0f);
		BaseUIAnimation.action.CreateButton(AdBtn.gameObject);
	}

	public void ClickAdBtn()
	{
		if (AdsManager.RewardIsReady())
		{
            AdsManager.ShowRewarded();
		}
		Image component = StartBtn.GetComponent<Image>();
		component.sprite = PlayStartBtnSprite;
		component.SetNativeSize();
		AdBtn.SetActive(value: false);
		StartBtn.transform.localPosition -= new Vector3(-150f, 0f, 0f);
		CloseUI();
		if ((bool)MapUI.action)
		{
			MapUI.action.ShowAdMask();
		}
	}
}
