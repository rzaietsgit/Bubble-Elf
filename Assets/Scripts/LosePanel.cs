using DG.Tweening;
using EasyMobile;
using System.Collections;
using ITSoft;
using UnityEngine;

public class LosePanel : LosePanelBase
{
	public static LosePanel panel;

	public GameObject StarObj;

	public GameObject StarObj_Father;

	public Sprite StarObj_crush;

	public override void InitUI()
	{
		AdsManager.ShowBanner();
        Singleton<DataManager>.Instance.bAdRewardPlay = false;
		Singleton<DataManager>.Instance.bAdRewardLose = false;
		Singleton<DataManager>.Instance.bAdRewardHome = false;
		panel = this;
		Singleton<DataManager>.Instance.SaleChangeMapLose = false;

        YunbuCheck();
		int iNowSelectLevelIndex = Singleton<LevelManager>.Instance.iNowSelectLevelIndex;
		BaseUIAnimation.action.SetLanguageFont("LoseLevelText1", detail.LoseLevelText_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("LoseUILevelTitle", detail.LoseUILevelTitle_Text, iNowSelectLevelIndex.ToString());
		BaseUIAnimation.action.SetLanguageFont("LoseUILevelFailedText", detail.LoseUILevelFailedText_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("LoseUILevelRestartText", detail.LoseUILevelRestartText_Text, string.Empty);
		if (Singleton<LevelManager>.Instance.bBossHuang)
		{
			detail.LoseUILevelTitle_Text.text = "�ȳ�ؿ�";
		}
		detail.RestartBtn_Button.gameObject.SetActive(value: false);
		if (InitGame.bChinaVersion)
		{
			detail.LoseLevelText_Text.transform.parent.gameObject.SetActive(value: true);
			int iNowScore = Singleton<LevelManager>.Instance.iNowScore;
			detail.WinScoreText_Text.text = iNowScore.ToString();
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
		detail.RestartBtn_Button.gameObject.SetActive(value: false);
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
		StartCoroutine(IEFinger());
	}

	public void ShowYunbuAd()
	{
	}

	public void ShowYunbuAdVidoe()
	{
	}

	public void SetAdCounts()
	{
	}

	public void HideYunbuAd()
	{
	}

	public override void OnRestartBtn()
	{
        AdsManager.HideBanner();
        bool isReady = AdsManager.InterIsReady();
        // Show it if it's ready
        if (isReady)
        {
            AdsManager.ShowInterstitial();
        }
        Singleton<LevelManager>.Instance.bRstart = true;
		UI.Instance.ClosePanel();
	}

	public override void OnExit()
	{
		if (Singleton<DataManager>.Instance.SaleChangeMapLose && !InitGame.bCloseLBForEnIos)
		{
			UI.Instance.OpenPanel(UIPanelType.SaleAdUI);
		}
		else
		{
			Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
		}
	}

	public virtual void OnHaoping()
	{
		Singleton<DataManager>.Instance.ClickHaoping();
	}

	public void CloseHaoping()
	{
	}

	public void OpenHaoping()
	{
	}

	private IEnumerator IEFinger()
	{
		yield return new WaitForSeconds(1f);
		ShowRestartBtn();
	}

	public void ShowRestartBtn()
	{
		detail.RestartBtn_Button.transform.localScale = new Vector2(0f, 0f);
		detail.RestartBtn_Button.gameObject.SetActive(value: true);
		Sequence s = DOTween.Sequence();
		s.Append(detail.RestartBtn_Button.transform.DOScale(new Vector2(1.2f, 1.1f), 0.2f)).Append(detail.RestartBtn_Button.transform.DOScale(new Vector2(0.9f, 0.9f), 0.06f)).Append(detail.RestartBtn_Button.transform.DOScale(new Vector2(1f, 1f), 0.06f).OnComplete(showAnim));
	}

	public void showAnim()
	{
	}

	private void LoadStarLove()
	{
	}

	private void LoadStar()
	{
		int iNowStar = Singleton<LevelManager>.Instance.iNowStar;
		if (iNowStar < 1)
		{
			detail.star1_Image.gameObject.SetActive(value: false);
		}
		if (iNowStar < 2)
		{
			detail.star2_Image.gameObject.SetActive(value: false);
		}
		if (iNowStar < 3)
		{
			detail.star3_Image.gameObject.SetActive(value: false);
		}
	}

	public void LoseLog()
	{
		int num = Singleton<UserLevelManager>.Instance.GetUserLevel();
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex < 31)
		{
			num = 0;
		}
		//FireBase.Action.UnityWriteLog("LOG_LevelLose", Singleton<LevelManager>.Instance.iNowSelectLevelIndex + "|" + num);
		//GA.FailLevel(Singleton<LevelManager>.Instance.iNowSelectLevelIndex.ToString());
	}

	public void YunbuCheck()
	{
	}

	public bool openSale()
	{
		return false;
	}

	public override void OnCloseButton()
	{
        AdsManager.HideBanner();
        bool isReady = AdsManager.InterIsReady();
        // Show it if it's ready
        if (isReady)
        {
            AdsManager.ShowInterstitial();
        }
        UI.Instance.ClosePanel();
	}
}
