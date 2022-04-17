using DG.Tweening;
using EasyMobile;
using System.Collections;
using ITSoft;
//using Umeng;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : BaseUI
{
	public static WinUI action;

	public Text LevelTitle;

	public GameObject NextBtn;

	public GameObject FaceBookShareBtn;

	public GameObject FaceBookConnbtn;

	public GameObject CloseBtn;

	public GameObject Star1;

	public GameObject Star2;

	public GameObject Star3;

	public GameObject fx_winui_starObj;

	public Text WinScoreText;

	public Text WinScoreTextChina;

	public Text WinUILevelFaceBookConn;

	public Text WinUITitle;

	public Text WinUILevelScoreText;

	public Text WinUILevelScoreTextChina;

	public Text WinUINext;

	public Text WinUIShare;

	public Text WinUIFaceBookConAwardText;

	public GameObject ScoreObj;

	public GameObject ScoreObj_ChinaAddGold;

	public Text AddGBText;

	public GameObject fingerObj;

	public GameObject HaopingObj;

	public GameObject FaceBookObj;

	public GameObject ScoreBgChinaHua;

	public Text WinScoreTextChinaHua;

	public Text AddGBTextHua;

	public Text AddHuaBiText;

	private bool bLoad;

	private int ifinger;

	private int ifingercount = 2;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.WinUI;
	}

	public override void OnStart()
	{
		action = this;
		//AdManager.action.opadshowcp(DataManager.PAGE_SUCCESS);
		if (InitGame.bChinaVersion && !InitGame.bEnios)
		{
			FaceBookObj.SetActive(value: false);
			HaopingObj.SetActive(value: true);
		}
		else
		{
			HaopingObj.SetActive(value: false);
			FaceBookObj.SetActive(value: true);
		}
		if (InitGame.bChinaVersion && !InitGame.bEnios)
		{
			YunbuCheck();
		}
		NextBtn.SetActive(value: false);
		if ((bool)MusicController.action)
		{
			MusicController.action.BG_Over();
		}
		BaseUIAnimation.action.SetLanguageFont("WinUITitle", WinUITitle, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("WinUILevelScoreText", WinUILevelScoreText, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("WinUINext", WinUINext, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("WinUILevelScoreTextChina", WinUILevelScoreTextChina, string.Empty);
		Singleton<LevelManager>.Instance.bLastWin = true;
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_win");
		}
		BaseUIAnimation.action.SetLanguageFont("WinUILevelLevelTitle", LevelTitle, Singleton<LevelManager>.Instance.iNowSelectLevelIndex.ToString());
		if (Singleton<LevelManager>.Instance.bBossHuang)
		{
			LevelTitle.text = "蝗虫关卡";
		}
		LoadStarAndScore();
		if (InitGame.bChinaVersion)
		{
			if (Singleton<UserManager>.Instance.bOpenHua() > 0)
			{
				ScoreBgChinaHua.SetActive(value: true);
				ScoreObj_ChinaAddGold.gameObject.SetActive(value: false);
				int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
				if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex > @int)
				{
					int num = int.Parse(Singleton<DataManager>.Instance.dDataHua4["5"]["iConfigData"]);
					AddHuaBiText.text = "+" + num;
				}
				else
				{
					int num2 = int.Parse(Singleton<DataManager>.Instance.dDataHua4["6"]["iConfigData"]);
					AddHuaBiText.text = "+" + num2;
				}
			}
			else
			{
				ScoreObj_ChinaAddGold.gameObject.SetActive(value: true);
			}
			NextBtn.gameObject.transform.localPosition = new Vector3(0f, -321f, 0f);
			AddGB();
		}
		else
		{
			ScoreObj.gameObject.SetActive(value: true);
			BaseUIAnimation.action.SetLanguageFont("WinUIFaceBookConAwardText", WinUIFaceBookConAwardText, string.Empty);
			BaseUIAnimation.action.SetLanguageFont("WinUIShare", WinUIShare, string.Empty);
			BaseUIAnimation.action.SetLanguageFont("WinUILevelFaceBookConn", WinUILevelFaceBookConn, string.Empty);
			BaseUIAnimation.action.CreateButton(FaceBookConnbtn.gameObject);
			if ((bool)FaceBookApi.Action)
			{
				FaceBookApi.Action.CheckLoginIcon(FaceBookConnbtn);
			}
			FaceBookShareBtn.SetActive(value: true);
			BaseUIAnimation.action.CreateButton(FaceBookShareBtn.gameObject);
			if (AdsManager.RewardIsReady())
			{
                AdsManager.ShowRewarded();
			}
		}
		Singleton<DataManager>.Instance.isUpdateWinData = true;
		bLoad = true;
		FaceBookApi.Action.LogAchievedLevelEvent(Singleton<LevelManager>.Instance.iNowSelectLevelIndex.ToString());
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex >= 3 && Singleton<DataManager>.Instance.GetUserDataI("dbLogCompletedTutorialEvent") == 0)
		{
			FaceBookApi.Action.LogCompletedTutorialEvent(Singleton<LevelManager>.Instance.iNowSelectLevelIndex.ToString(), success: true);
			Singleton<DataManager>.Instance.SaveUserDate("dbLogCompletedTutorialEvent", 1);
		}
		StartCoroutine(IEShowFingerObj());
	}

	private void UpdateWinData()
	{
		Singleton<UserManager>.Instance.SetPassTask("PassLevel");
		Singleton<UserLevelManager>.Instance.GameWin();
		//Analytics.Event("PassWinBubble", "Level_" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex + "_" + Singleton<LevelManager>.Instance.iBubbleCountOver);
		PassLevel.action.SaveUserTask();
		int iNowStar = Singleton<LevelManager>.Instance.iNowStar;
		int iNowScore = Singleton<LevelManager>.Instance.iNowScore;
		Singleton<UserManager>.Instance.SetNowPassLevelNumber(Singleton<LevelManager>.Instance.iNowSelectLevelIndex, iNowStar, iNowScore);
		bLoad = true;
	}

	private void AddGB()
	{
	}

	private void LoadStarAndScore()
	{
		int iNowScore = Singleton<LevelManager>.Instance.iNowScore;
		Star1.SetActive(value: false);
		Star2.SetActive(value: false);
		Star3.SetActive(value: false);
		int iNowStar = Singleton<LevelManager>.Instance.iNowStar;
		StartCoroutine(PalyStar(iNowStar));
		if (InitGame.bChinaVersion)
		{
			WinScoreTextChina.text = iNowScore.ToString();
			WinScoreTextChinaHua.text = iNowScore.ToString();
		}
		else
		{
			WinScoreText.text = iNowScore.ToString();
		}
	}

	public void nextShow()
	{
		NextBtn.transform.localScale = new Vector2(0f, 0f);
		NextBtn.SetActive(value: true);
		Sequence s = DOTween.Sequence();
		s.Append(NextBtn.transform.DOScale(new Vector2(1.2f, 1.1f), 0.2f)).Append(NextBtn.transform.DOScale(new Vector2(0.9f, 0.9f), 0.06f)).Append(NextBtn.transform.DOScale(new Vector2(1f, 1f), 0.06f).OnComplete(showAnim));
	}

	public void showAnim()
	{
		if (Singleton<DataManager>.Instance.bGooglePay)
		{
		}
		BaseUIAnimation.action.CreateButton(NextBtn.gameObject);
	}

	private IEnumerator PalyStar(int iStar)
	{
		yield return new WaitForSeconds(0.6f);
		if ((bool)GameUI.action)
		{
			Canvas component = base.transform.GetComponent<Canvas>();
			component.renderMode = RenderMode.ScreenSpaceCamera;
			component.worldCamera = GameUI.action.mainCameraS.GetComponent<Camera>();
		}
		if (iStar == 0)
		{
			nextShow();
		}
		if (iStar >= 1)
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("ui_winstar1");
			}
			Star1.SetActive(value: true);
			GameObject _fx_winui_starObj3 = Object.Instantiate(fx_winui_starObj, base.transform.position, base.transform.rotation);
			_fx_winui_starObj3.transform.parent = Star1.transform.transform;
			_fx_winui_starObj3.transform.localPosition = new Vector3(0f, 0f, 0f);
			Star1.transform.localScale = new Vector3(5f, 5f, 5f);
			Sequence ts3 = DOTween.Sequence();
			ts3.Append(Star1.transform.DOScale(new Vector2(0.9f, 0.9f), 0.2f).SetEase(Ease.InOutSine)).Append(Star1.transform.DOScale(new Vector2(1.1f, 1.1f), 0.06f).SetEase(Ease.OutSine)).Append(Star1.transform.DOScale(new Vector2(1f, 1f), 0.05f).SetEase(Ease.OutSine));
			if (iStar == 1)
			{
				yield return new WaitForSeconds(0.3f);
				nextShow();
			}
			yield return new WaitForSeconds(0.5f);
		}
		if (iStar >= 2)
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("ui_winstar2");
			}
			Star2.SetActive(value: true);
			GameObject _fx_winui_starObj2 = Object.Instantiate(fx_winui_starObj, base.transform.position, base.transform.rotation);
			_fx_winui_starObj2.transform.parent = Star2.transform.transform;
			_fx_winui_starObj2.transform.localPosition = new Vector3(0f, 0f, 0f);
			Star2.transform.localScale = new Vector3(5f, 5f, 5f);
			Sequence ts2 = DOTween.Sequence();
			ts2.Append(Star2.transform.DOScale(new Vector2(0.9f, 0.9f), 0.2f).SetEase(Ease.InOutSine)).Append(Star2.transform.DOScale(new Vector2(1.1f, 1.1f), 0.06f).SetEase(Ease.OutSine)).Append(Star2.transform.DOScale(new Vector2(1f, 1f), 0.05f).SetEase(Ease.OutSine));
			if (iStar == 2)
			{
				yield return new WaitForSeconds(0.3f);
				nextShow();
			}
			yield return new WaitForSeconds(0.5f);
		}
		if (iStar >= 3)
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("ui_winstar3");
			}
			Star3.SetActive(value: true);
			GameObject _fx_winui_starObj = Object.Instantiate(fx_winui_starObj, base.transform.position, base.transform.rotation);
			_fx_winui_starObj.transform.parent = Star3.transform.transform;
			_fx_winui_starObj.transform.localPosition = new Vector3(0f, 0f, 0f);
			Star3.transform.localScale = new Vector3(5f, 5f, 5f);
			Sequence ts = DOTween.Sequence();
			ts.Append(Star3.transform.DOScale(new Vector2(0.9f, 0.9f), 0.2f).SetEase(Ease.InOutSine)).Append(Star3.transform.DOScale(new Vector2(1.1f, 1.1f), 0.06f).SetEase(Ease.OutSine)).Append(Star3.transform.DOScale(new Vector2(1f, 1f), 0.05f).SetEase(Ease.OutSine));
			if (iStar == 3)
			{
				yield return new WaitForSeconds(0.3f);
				nextShow();
			}
		}
	}

	public void showInterstitial()
	{
		finishLevel();
	}

	public void finishLevel()
	{

	}

	private IEnumerator IEShowFingerObj()
	{
		yield return new WaitForSeconds(0.1f);
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex <= 15)
		{
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

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ifinger = 0;
		}
	}

	public void _CloseWinUI()
	{
		StartCoroutine(CallCloseUI());
	}

	public void CloseWinUI()
	{
		if (bLoad && BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
			StartCoroutine(CallCloseUI());
		}
	}

	private IEnumerator CallCloseUI()
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
	}

	protected override void OnAwake()
	{
		base.OnAwake();
	}

	protected override void OnRelease()
	{
		base.OnRelease();
	}

	public void NextGame()
	{
		if (bLoad)
		{
			Singleton<LevelManager>.Instance.bOpenPlay = true;
			Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
		}
	}

	public void ClickShare()
	{
		if (!FaceBookApi.Action.bLoginState())
		{
			FaceBookApi.Action.FackBookLogin();
		}
		else
		{
			FaceBookApi.Action.FBFeedSharewin1();
		}
	}

	public void ClickHaoping()
	{
		Singleton<DataManager>.Instance.ClickHaoping();
	}

	public void CloseHaoping()
	{
		HaopingObj.SetActive(value: false);
	}

	public void YunbuCheck()
	{
		if (Singleton<DataManager>.Instance.bChinaIos)
		{
			HaopingObj.SetActive(value: false);
		}
		if (Singleton<DataManager>.Instance.bShowHaopingLogin)
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
}
