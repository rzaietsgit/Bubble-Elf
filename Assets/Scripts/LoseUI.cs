using DG.Tweening;
using EasyMobile;
using System.Collections;
using ITSoft;
using UnityEngine;
using UnityEngine.UI;

public class LoseUI : BaseUI
{
	public static LoseUI action;

	public Text LoseUILevelTitle;

	public Text LoseUILevelFailedText;

	public Text LoseUILevelRestartText;

	public Text LoseUILevelFaceBookConn;

	public Text LoseUIFaceBookConAwardText;

	public GameObject RestartBtn;

	public GameObject StarObj;

	public GameObject StarObj_Father;

	public Sprite StarObj_crush;

	public GameObject CloseBtn;

	public GameObject FaceBookConnBtn;

	public GameObject Star1;

	public GameObject Star2;

	public GameObject Star3;

	public GameObject ScoreChinaObj;

	public Text ScoreChinaText;

	public GameObject HaopingObj;

	public GameObject FaceBookObj;

	public Text LoseLevelText1;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.LoseUI;
	}

	public override void OnStart()
	{
		action = this;
		//AdManager.action.opadshowcp(DataManager.PAGE_FAIL);
		if (InitGame.bChinaVersion)
		{
			FaceBookObj.SetActive(value: false);
			YunbuCheck();
		}
		else
		{
			HaopingObj.SetActive(value: false);
			FaceBookObj.SetActive(value: true);
		}
		BaseUIAnimation.action.SetLanguageFont("LoseLevelText1", LoseLevelText1, string.Empty);
		RestartBtn.SetActive(value: false);
		if (InitGame.bChinaVersion)
		{
			StarObj_Father.SetActive(value: false);
			ScoreChinaObj.SetActive(value: true);
			int iNowScore = Singleton<LevelManager>.Instance.iNowScore;
			ScoreChinaText.text = iNowScore.ToString();
		}
		Singleton<LevelManager>.Instance.bLoseGame = true;
		if ((bool)MusicController.action)
		{
			MusicController.action.BG_Over();
		}
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_lose");
		}
		LoseLog();
		int iNowSelectLevelIndex = Singleton<LevelManager>.Instance.iNowSelectLevelIndex;
		BaseUIAnimation.action.SetLanguageFont("LoseUILevelTitle", LoseUILevelTitle, iNowSelectLevelIndex.ToString());
		BaseUIAnimation.action.SetLanguageFont("LoseUILevelFailedText", LoseUILevelFailedText, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("LoseUILevelRestartText", LoseUILevelRestartText, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("LoseUILevelFaceBookConn", LoseUILevelFaceBookConn, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("LoseUIFaceBookConAwardText", LoseUIFaceBookConAwardText, string.Empty);
		if (Singleton<LevelManager>.Instance.bBossHuang)
		{
			LoseUILevelTitle.text = "蝗虫关卡";
		}
		BaseUIAnimation.action.CreateButton(FaceBookConnBtn.gameObject);
		if ((bool)FaceBookApi.Action)
		{
			FaceBookApi.Action.CheckLoginIcon(FaceBookConnBtn);
		}
		LoadStarLove();
		LoadStar();
		Singleton<UserLevelManager>.Instance.Gamelose();
		if ((bool)PassLevel.action)
		{
			PassLevel.action.bOver = true;
		}
		if (Singleton<LevelManager>.Instance.iLastFailureLevelID == Singleton<LevelManager>.Instance.iNowSelectLevelIndex)
		{
			Singleton<LevelManager>.Instance.iFailure++;
		}
		else
		{
			Singleton<LevelManager>.Instance.iFailure = 1;
		}
		Singleton<LevelManager>.Instance.iLastFailureLevelID = Singleton<LevelManager>.Instance.iNowSelectLevelIndex;
		if (AdsManager.RewardIsReady())
		{
            AdsManager.ShowRewarded();
		}
		Singleton<UserManager>.Instance.iPassLevelCount = 0;
		failLevel();
		StartCoroutine(IEFinger());
	}

	public void failLevel()
	{
		
	}

	private IEnumerator IEFinger()
	{
		yield return new WaitForSeconds(1f);
		ShowRestartBtn();
		Canvas canvas = base.transform.GetComponent<Canvas>();
		canvas.renderMode = RenderMode.ScreenSpaceCamera;
		canvas.worldCamera = GameUI.action.mainCameraS.GetComponent<Camera>();
	}

	public void ShowRestartBtn()
	{
		RestartBtn.transform.localScale = new Vector2(0f, 0f);
		RestartBtn.SetActive(value: true);
		Sequence s = DOTween.Sequence();
		s.Append(RestartBtn.transform.DOScale(new Vector2(1.2f, 1.1f), 0.2f)).Append(RestartBtn.transform.DOScale(new Vector2(0.9f, 0.9f), 0.06f)).Append(RestartBtn.transform.DOScale(new Vector2(1f, 1f), 0.06f).OnComplete(showAnim));
	}

	public void showAnim()
	{
		BaseUIAnimation.action.CreateButton(RestartBtn.gameObject);
	}

	public void LoseLog()
	{
		int num = Singleton<UserLevelManager>.Instance.GetUserLevel();
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex < 31)
		{
			num = 0;
		}
		FireBase.Action.UnityWriteLog("LOG_LevelLose", Singleton<LevelManager>.Instance.iNowSelectLevelIndex + "|" + num);
		//GA.FailLevel(Singleton<LevelManager>.Instance.iNowSelectLevelIndex.ToString());
	}

	private void LoadStar()
	{
		int iNowStar = Singleton<LevelManager>.Instance.iNowStar;
		if (iNowStar < 1)
		{
			Star1.SetActive(value: false);
		}
		if (iNowStar < 2)
		{
			Star2.SetActive(value: false);
		}
		if (iNowStar < 3)
		{
			Star3.SetActive(value: false);
		}
	}

	private void LoadStarLove()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
		for (int i = 0; i < @int + 1; i++)
		{
			if (i < 5)
			{
				GameObject gameObject = Object.Instantiate(StarObj);
				gameObject.transform.SetParent(StarObj_Father.transform, worldPositionStays: false);
				if (@int == i && @int < 5)
				{
					gameObject.GetComponent<Image>().sprite = StarObj_crush;
				}
				gameObject.SetActive(value: true);
			}
		}
	}

	private void Update()
	{
	}

	public void CloseLoseUI()
	{
		StartCoroutine(CallCloseUI());
	}

	public void _CloseLoseUI()
	{
		if (BaseUIAnimation.bClickButton)
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

	public void RestartGame()
	{
		Singleton<LevelManager>.Instance.bRstart = true;
		Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
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
