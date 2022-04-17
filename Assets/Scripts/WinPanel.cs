using DG.Tweening;
using EasyMobile;
using System.Collections;
using UnityEngine;

public class WinPanel : WinPanelBase
{
	public static WinPanel panel;

	public GameObject fx_winui_starObj;

	public override void InitUI()
	{
		panel = this;
		//AdManager.action.bubbleenAdchaping(1);
		Singleton<DataManager>.Instance.SaleChangeMapWin = false;
		Singleton<LevelManager>.Instance.bLastWin = true;
		InitAndroid.action.GAEvent("winbubble1:ClassLevel" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
		InitAndroid.action.GAEvent("winbubble2:ClassLevel" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex + "_" + Singleton<LevelManager>.Instance.iBubbleCountOver);
		InitAndroid.action.GAEvent("winbubble3:ClassLevel" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex + ":Bubble" + Singleton<LevelManager>.Instance.iBubbleCountOver);
		InitAndroid.action.GAEvent("Newwinbubble3:ClassLevel" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex + ":Bubble" + Singleton<LevelManager>.Instance.iBubbleCountOver);
		Singleton<DataManager>.Instance.isUpdateWinData = true;
        YunbuCheck();
		detail.NextBtn_Button.transform.localScale = new Vector2(0f, 0f);
		detail.NextBtn_Button.gameObject.SetActive(value: false);
		if ((bool)MusicController.action)
		{
			MusicController.action.BG_Over();
		}
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_win");
		}
		BaseUIAnimation.action.SetLanguageFont("WinUITitle", detail.WinUITitle_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("WinUILevelScoreText", detail.ScoreBgWinUILevelScoreText_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("WinUINext", detail.NextText_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("WinUILevelScoreTextChina", detail.ScoreBgChinaWinUILevelScoreText_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("WinUILevelLevelTitle", detail.LevelText_Text, Singleton<LevelManager>.Instance.iNowSelectLevelIndex.ToString());
		if (Singleton<LevelManager>.Instance.bBossHuang)
		{
			detail.LevelText_Text.text = "蝗虫关卡";
		}
		LoadStarAndScore();
		if (InitGame.bChinaVersion)
		{
			if (Singleton<UserManager>.Instance.bOpenHua() > 0)
			{
				detail.ScoreBgChinaHua_Image.gameObject.SetActive(value: true);
				detail.ScoreBgChina_Image.gameObject.SetActive(value: false);
				int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
				if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex > @int)
				{
					int num = int.Parse(Singleton<DataManager>.Instance.dDataHua4["5"]["iConfigData"]);
					detail.ScoreBgChinaHuaAddHua_Text.text = "+" + num;
				}
				else
				{
					int num2 = int.Parse(Singleton<DataManager>.Instance.dDataHua4["6"]["iConfigData"]);
					detail.ScoreBgChinaHuaAddHua_Text.text = "+" + num2;
				}
			}
			else
			{
				detail.ScoreBgChina_Image.gameObject.SetActive(value: true);
			}
		}
		InitGame.Action.WInData();
	}

	public override void OnExit()
	{
		if (Singleton<DataManager>.Instance.SaleChangeMapWin && !InitGame.bCloseLBForEnIos)
		{
			UI.Instance.OpenPanel(UIPanelType.SaleAdUI);
		}
		else
		{
			Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
		}
	}

	public override void OnNextBtn()
	{
		if (openSale())
		{
			Singleton<DataManager>.Instance.SaleChangeMapWin = true;
		}
		UI.Instance.ClosePanel();
	}

	private void AddGB()
	{
	}

	private void LoadStarAndScore()
	{
		int iNowScore = Singleton<LevelManager>.Instance.iNowScore;
		detail.star1_Image.gameObject.SetActive(value: false);
		detail.star2_Image.gameObject.SetActive(value: false);
		detail.star3_Image.gameObject.SetActive(value: false);
		int iNowStar = Singleton<LevelManager>.Instance.iNowStar;
		StartCoroutine(PalyStar(iNowStar));
		if (InitGame.bChinaVersion)
		{
			detail.ScoreBgChinaWinScoreText_Text.text = iNowScore.ToString();
			detail.ScoreBgChinaHuaWinScoreText_Text.text = iNowScore.ToString();
		}
		else
		{
			detail.ScoreBgWinScoreText_Text.text = iNowScore.ToString();
		}
	}

	public void nextShow()
	{
		detail.NextBtn_Button.transform.localScale = new Vector2(0f, 0f);
		detail.NextBtn_Button.gameObject.SetActive(value: true);
		Sequence s = DOTween.Sequence();
		s.Append(detail.NextBtn_Button.transform.DOScale(new Vector2(1.2f, 1.1f), 0.2f)).Append(detail.NextBtn_Button.transform.DOScale(new Vector2(0.9f, 0.9f), 0.06f)).Append(detail.NextBtn_Button.transform.DOScale(new Vector2(1f, 1f), 0.06f).OnComplete(showAnim));
	}

	public void showAnim()
	{
	}

	public override void Onhaoping()
	{
		Singleton<DataManager>.Instance.ClickHaoping();
	}

	public void CloseHaoping()
	{
		detail.haoping_Button.gameObject.SetActive(value: false);
	}

	public void OpenHaoping()
	{
		detail.haoping_Button.gameObject.SetActive(value: true);
	}

	private IEnumerator PalyStar(int iStar)
	{
		yield return new WaitForSeconds(0.6f);
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
			detail.star1_Image.gameObject.SetActive(value: true);
			GameObject _fx_winui_starObj3 = Object.Instantiate(fx_winui_starObj, base.transform.position, base.transform.rotation);
			_fx_winui_starObj3.transform.parent = detail.star1_Image.gameObject.transform.transform;
			_fx_winui_starObj3.transform.localPosition = new Vector3(0f, 0f, 0f);
			detail.star1_Image.gameObject.transform.localScale = new Vector3(5f, 5f, 5f);
			Sequence ts3 = DOTween.Sequence();
			ts3.Append(detail.star1_Image.gameObject.transform.DOScale(new Vector2(0.9f, 0.9f), 0.2f).SetEase(Ease.InOutSine)).Append(detail.star1_Image.gameObject.transform.DOScale(new Vector2(1.1f, 1.1f), 0.06f).SetEase(Ease.OutSine)).Append(detail.star1_Image.gameObject.transform.DOScale(new Vector2(1f, 1f), 0.05f).SetEase(Ease.OutSine));
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
			detail.star2_Image.gameObject.SetActive(value: true);
			GameObject _fx_winui_starObj2 = Object.Instantiate(fx_winui_starObj, base.transform.position, base.transform.rotation);
			_fx_winui_starObj2.transform.parent = detail.star2_Image.gameObject.transform.transform;
			_fx_winui_starObj2.transform.localPosition = new Vector3(0f, 0f, 0f);
			detail.star2_Image.gameObject.transform.localScale = new Vector3(5f, 5f, 5f);
			Sequence ts2 = DOTween.Sequence();
			ts2.Append(detail.star2_Image.gameObject.transform.DOScale(new Vector2(0.9f, 0.9f), 0.2f).SetEase(Ease.InOutSine)).Append(detail.star2_Image.gameObject.transform.DOScale(new Vector2(1.1f, 1.1f), 0.06f).SetEase(Ease.OutSine)).Append(detail.star2_Image.gameObject.transform.DOScale(new Vector2(1f, 1f), 0.05f).SetEase(Ease.OutSine));
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
			detail.star3_Image.gameObject.SetActive(value: true);
			GameObject _fx_winui_starObj = Object.Instantiate(fx_winui_starObj, base.transform.position, base.transform.rotation);
			_fx_winui_starObj.transform.parent = detail.star3_Image.gameObject.transform.transform;
			_fx_winui_starObj.transform.localPosition = new Vector3(0f, 0f, 0f);
			detail.star3_Image.gameObject.transform.localScale = new Vector3(5f, 5f, 5f);
			Sequence ts = DOTween.Sequence();
			ts.Append(detail.star3_Image.gameObject.transform.DOScale(new Vector2(0.9f, 0.9f), 0.2f).SetEase(Ease.InOutSine)).Append(detail.star3_Image.gameObject.transform.DOScale(new Vector2(1.1f, 1.1f), 0.06f).SetEase(Ease.OutSine)).Append(detail.star3_Image.gameObject.transform.DOScale(new Vector2(1f, 1f), 0.05f).SetEase(Ease.OutSine));
			if (iStar == 3)
			{
				yield return new WaitForSeconds(0.3f);
				nextShow();
			}
		}
	}

	public void YunbuCheck()
	{
		if (Singleton<DataManager>.Instance.bChinaIos)
		{
			detail.haoping_Image.gameObject.SetActive(value: false);
		}
		if (Singleton<DataManager>.Instance.bShowHaopingLogin)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_bHaoping");
			if (@int > 0)
			{
				detail.haoping_Image.gameObject.SetActive(value: false);
			}
			else
			{
				Singleton<DataManager>.Instance.SendyunbuCheck();
			}
		}
		else
		{
			detail.haoping_Image.gameObject.SetActive(value: false);
		}
	}

	public bool openSale()
	{
		DataManager.sale_adKey = string.Empty;
		if (!InitGame.bChinaVersion)
		{
			return false;
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex <= 30)
		{
			return false;
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex <= Singleton<UserManager>.Instance.iNowPassLevelID)
		{
			return false;
		}
		int num = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
		int loveInfinite = Singleton<UserManager>.Instance.getLoveInfinite();
		if (loveInfinite > 0)
		{
			num = 100;
		}
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		UnityEngine.Debug.Log("UserManager.Instance.iSKillCount()=" + Singleton<UserManager>.Instance.iSKillCount());
		if (num <= 10)
		{
			DataManager.sale_adKey = "Bubble_LB" + Singleton<UserManager>.Instance.GetLb(90, 95);
			return true;
		}
		if (@int <= 50)
		{
			DataManager.sale_adKey = "Bubble_LB" + Singleton<UserManager>.Instance.GetLb(5, 85);
			return true;
		}
		if (Singleton<UserManager>.Instance.iSKillCount() < 10)
		{
			DataManager.sale_adKey = "Bubble_LB" + Singleton<UserManager>.Instance.GetLb(5, 45);
			return true;
		}
		return false;
	}
}
