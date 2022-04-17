using DG.Tweening;
using EasyMobile;
using System.Collections;
using ITSoft;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class WinPanmove : MonoBehaviour
{
	public static WinPanmove action;

	public GameObject Movebg1;

	public GameObject Movebg2;

	public GameObject Movebg3;

	public GameObject Movepanel1;

	public GameObject Movepanel2;

	public GameObject MoveObj;

	public GameObject Hide1;

	public GameObject Start1;

	public GameObject Start2;

	public GameObject Start3;

	public GameObject ButtonNext;

	public Text ButtonNextText;

	public GameObject yanhua1;

	public GameObject yanhua2;

	public GameObject yanhua3;

	public GameObject fx_end_star;

	public Text scoreTexten;

	public Text LevelTexten;

	public Text LevelNumberen;

	public Text addgold;

	public GameObject renwu;

	private int iNowScores;

	public void Uprenwu()
	{
	}

	private void Start()
	{
		action = this;
		ButtonNext.SetActive(value: false);
		yanhua1.SetActive(value: false);
		yanhua2.SetActive(value: false);
		yanhua3.SetActive(value: false);
		BaseUIAnimation.action.SetLanguageFont("WinUINext", ButtonNextText, string.Empty);
	}

	private IEnumerator IEaddScore()
	{
		yield return new WaitForSeconds(0.1f);
		while (true)
		{
			int iScore = Singleton<LevelManager>.Instance.iNowScore;
			int iaddcount = (iScore - iNowScores) / 10;
			while (iNowScores < iScore)
			{
				if (iNowScores + iaddcount < iScore)
				{
					iNowScores += iaddcount;
				}
				else
				{
					iNowScores = iScore;
				}
				if (scoreTexten != null)
				{
					scoreTexten.text = iNowScores + string.Empty;
					
				}
				yield return new WaitForSeconds(0.1f);
			}
			yield return new WaitForSeconds(0.2f);
			
		}

	}

	public void winUIload()
	{
		AdsManager.HideBanner();
		int iNowScore = Singleton<LevelManager>.Instance.iNowScore;
		scoreTexten.text = Singleton<LevelManager>.Instance.iNowScore + string.Empty;
		InitGame.Action.StartCoroutine(IEaddScore());
		AddGB();
		iNowScores = iNowScore;
		BaseUIAnimation.action.SetLanguageFont("WinLevelText", LevelTexten, string.Empty);
		LevelNumberen.text = Singleton<LevelManager>.Instance.iNowSelectLevelIndex + string.Empty;
		AnalyticsManager.Log("level_completed_level" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex + string.Empty);
	}

	public void movepanel()
	{
		winUIload();
		if ((bool)GameUI.action)
		{
			GameUI.action.gameObject.SetActive(value: false);
		}
		Hide1.SetActive(value: false);
		GameUI.action.GameBG.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
		move2();
	}

	public void move2()
	{
		Movebg1.transform.DOMove(new Vector3(0f, -13.96f, 10f), 1f).SetEase(Ease.InSine);
		Movebg2.transform.DOMove(new Vector3(0f, 0.12f, 10f), 1f).SetEase(Ease.InSine);
		Movebg3.transform.DOMove(new Vector3(0f, 8.92f, 10f), 1f).SetEase(Ease.InSine);
		Movepanel1.transform.DOLocalMove(new Vector3(0f, 150f, 0f), 1f).SetEase(Ease.InSine).OnComplete(delegate
		{
			Startani();
		});
		yanhua1.SetActive(value: true);
		yanhua2.SetActive(value: true);
		yanhua3.SetActive(value: true);
	}

	public void Startani()
	{
		StartCoroutine(IEStartani());
	}

	public IEnumerator IEStartani()
	{
		int istart = Singleton<LevelManager>.Instance.iNowStar;
		yield return new WaitForSeconds(0.5f);
		GameUI.action.GameBG.GetComponent<SpriteRenderer>().DOColor(new Color(42f / 85f, 128f / 255f, 178f / 255f, 1f), 3f);
		Movebg1.GetComponent<SpriteRenderer>().DOColor(new Color(42f / 85f, 128f / 255f, 178f / 255f, 1f), 3f);
		Movebg2.GetComponent<SpriteRenderer>().DOColor(new Color(42f / 85f, 128f / 255f, 178f / 255f, 1f), 3f);
		Movebg3.GetComponent<SpriteRenderer>().DOColor(new Color(42f / 85f, 128f / 255f, 178f / 255f, 1f), 3f);
		float delay = 0f;
		for (int i = 0; i < istart; i++)
		{
			GameObject gameObject = Start1;
			switch (i)
			{
			case 1:
				gameObject = Start2;
				break;
			case 2:
				gameObject = Start3;
				break;
			}
			gameObject.SetActive(value: true);
			gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
			if (i == 0)
			{
				gameObject.transform.DOScale(new Vector3(7f, 7f), 0f).SetEase(Ease.InOutSine).SetDelay(delay);
			}
			if (i == 1)
			{
				gameObject.transform.DOScale(new Vector3(6f, 6f), 0f).SetEase(Ease.InOutSine).SetDelay(delay);
			}
			if (i == 2)
			{
				gameObject.transform.DOScale(new Vector3(5f, 5f), 0f).SetEase(Ease.InOutSine).SetDelay(delay);
			}
			gameObject.transform.DOScale(new Vector3(0.7f, 0.7f), 0.3f).SetEase(Ease.InOutSine).SetDelay(delay);
			gameObject.transform.DOScale(new Vector2(1.3f, 1.3f), 0.1f).SetEase(Ease.OutSine).SetDelay(delay + 0.3f);
			Color color = gameObject.GetComponent<Image>().color;
			gameObject.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0.4f);
			gameObject.GetComponent<Image>().DOColor(new Color(color.r, color.g, color.b, 1f), 0.2f).SetDelay(delay);
			float num = 0.075f;
			Movepanel2.transform.DOScale(new Vector2(1.1f, 1.1f), num).SetEase(Ease.InSine).SetDelay(delay + 0.3f);
			Movepanel2.transform.DOScale(new Vector2(0.9f, 0.9f), num).SetEase(Ease.InSine).SetDelay(delay + 0.3f + num);
			Movepanel2.transform.DOScale(new Vector2(1f, 1f), num).SetEase(Ease.InSine).SetDelay(delay + 0.3f + num + num);
			gameObject.transform.DOScale(new Vector2(1f, 1f), 0.12f).SetEase(Ease.OutSine).SetDelay(delay + 0.4f);
			StartCoroutine(Playfx_end_star(i, delay, 0.4f));
			delay += 0.4f;
		}

		ButtonNext.SetActive(value: true);
		BaseUIAnimation.action.CreateButton(ButtonNext.gameObject);
		winstart();
	}

	private IEnumerator Playfx_end_star(int j, float stime, float stime2)
	{
		yield return new WaitForSeconds(stime);
		switch (j)
		{
		case 1:
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("ui_winstar2", NowPlay: true);
			}
			break;
		case 2:
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("ui_winstar3", NowPlay: true);
			}
			break;
		case 0:
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("ui_winstar1", NowPlay: true);
			}
			break;
		}
		yield return new WaitForSeconds(stime2);
		if (Singleton<DataManager>.Instance.bGooglePay || InitGame.bEnios)
		{
			scoreTexten.gameObject.SetActive(value: false);
			LevelTexten.gameObject.SetActive(value: false);
			LevelNumberen.gameObject.SetActive(value: false);
			scoreTexten.gameObject.SetActive(value: true);
			LevelTexten.gameObject.SetActive(value: true);
			LevelNumberen.gameObject.SetActive(value: true);
		}
		GameObject s = Start1;
		switch (j)
		{
		case 1:
			s = Start2;
			break;
		case 2:
			s = Start3;
			break;
		}
		GameObject obj = UnityEngine.Object.Instantiate(fx_end_star, base.transform.position, base.transform.rotation);
		obj.transform.parent = s.gameObject.transform;
		obj.transform.localPosition = new Vector3(0f, 0f, 0f);
	}

	private void winstart()
	{
		//AdManager.action.bubbleenAdchaping(1);
		Singleton<DataManager>.Instance.SaleChangeMapWin = false;
		Singleton<LevelManager>.Instance.bLastWin = true;
		//InitAndroid.action.GAEvent("winbubble1:ClassLevel" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
		//InitAndroid.action.GAEvent("winbubble2:ClassLevel" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex + "_" + Singleton<LevelManager>.Instance.iBubbleCountOver);
		//InitAndroid.action.GAEvent("winbubble3:ClassLevel" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex + ":Bubble" + Singleton<LevelManager>.Instance.iBubbleCountOver);
		//InitAndroid.action.GAEvent("Newwinbubble3:ClassLevel" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex + ":Bubble" + Singleton<LevelManager>.Instance.iBubbleCountOver);
		Singleton<DataManager>.Instance.isUpdateWinData = true;
        AdsManager.ShowBanner();
        if ((bool)MusicController.action)
		{
			MusicController.action.BG_Over();
		}
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_win");
		}
		if (!InitGame.bChinaVersion || Singleton<UserManager>.Instance.bOpenHua() > 0)
		{
		}
		
		AdsManager.ShowInterstitial(() =>
		{
			
			InitGame.Action.WInData();
		});
		
        
	}

	private void AddGB()
	{
		if (InitGame.bChinaVersion)
		{
			int iNowStar = Singleton<LevelManager>.Instance.iNowStar;
			int num = 0;
			if (iNowStar == 1)
			{
				num = 50;
			}
			if (iNowStar == 2)
			{
				num = 100;
			}
			if (iNowStar == 3)
			{
				num = 200;
			}
			addgold.text = "+" + num;
			PayManager.action.AddGB(0, num);
		}
	}

	public void Nextbtn()
	{
        AdsManager.HideBanner();
        // Show it if it's ready
        if (Social.localUser.authenticated)
        {
	        Social.ReportScore(Singleton<LevelManager>.Instance.iNowSelectLevelIndex, EM_GPGSIds.leaderboard_level_complete, (bool success) =>
	        {
		        if (success)
		        {
			        Debug.Log("Update Score Success");
		        }
		        else
		        {
			        Debug.Log("Update Score Fail");
		        }
	        });
        }
        Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
		// Leaderboard publish score
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

	public void moveready()
	{
		MoveObj.transform.DOMove(new Vector3(0f, -8.8f, 0f), 1f).SetEase(Ease.InSine);
	}

	private void Update()
	{
	}
}
