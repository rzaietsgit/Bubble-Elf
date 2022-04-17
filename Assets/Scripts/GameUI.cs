using DG.Tweening;
using EasyMobile;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using ITSoft;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	public bool adbuy = true;

	public static GameUI action;

	public Component mainCameraS;

	public GameObject mainCamera;

	public TextMeshProUGUI[] LSkillCountText;

	public GameObject ScoreImg;

	public GameObject ScoreImgBG;

	public Text BubbleCountText;

	public GameObject QiuPos;

	public GameObject GameBG;

	public GameObject ComboViewObj;

	public GameObject fx_combleObj;

	public GameObject bgImage;

	public GameObject fx_completeObj;

	public GameObject fx_combleObj_Father;

	public GameObject ComboOneNumberObj;

	public GameObject ComboTwoNumberObj1;

	public GameObject ComboTwoNumberObj2;

	public GameObject ScoreObj;

	public GameObject ScoreNumberObj;

	private int iSkillCountText;

	public GameObject Star1;

	public GameObject Star2;

	public GameObject Star3;

	public GameObject SkillFather;

	public GameObject SkillObj;

	public GameObject PauseUIObj;

	public GameObject PauseBtnObj;

	public GameObject fx_prop1_MaxLine;

	public GameObject SkillImgObj;

	public GameObject fx_prop2_Bubble3;

	public GameObject fx_prop3_addBubble;

	public GameObject fx_prop4_bomb;

	public GameObject fx_ui_goalStarObj;

	public GameObject fx_goalstar_lightObj;

	public GameObject TipBubbleObj;

	public GameObject TipBubbleObjNumber;

	public GameObject BuyBubbleBtnObj;

	private GameObject _BuyBubbleBtnObj;

	public bool bAddBubble;

	public bool bMaxLine;

	public bool bGangSkill;

	public bool bBubble3;

	private float iStar1;

	private float iStar2;

	private float iStar3;

	public bool bUseSkill;

	public Text ShowGameTextObj;

	public Text ComboText;

	public Text BubbleBuyTipText;

	public GameObject DownBgObj;

	private int iTimeAd;

	private int iTimeAdMax;

	public GameObject TopBg;

	public GameObject TopBgBoss;

	public Image LevelGameBossHp;

	public GameObject FlyBg;

	public GameObject mubiaoiCon1;

	public GameObject mubiaoiCon2;

	public Text LEVELTEXT;

	public Text LEVELNBTEXT;

	public int buyBuyBuShu;

	public bool isZhiJieBoFang;

	public int buyBuShuCount;

	public int iBossHp = 100;

	private int iNoComboCount;

	private GameObject _fx_combleObj;

	private GameObject[] LSelectSkillObj;

	public int skilliRand;

	private float devHeight = 12.8f;

	private float devWidth = 7.2f;

	private int iLastCombo;

	private bool bLoadBubbleCont;

	private int iNowStar;

	private bool bui_winstar1 = true;

	private bool bui_winstar2 = true;

	private bool bui_winstar3 = true;

	private bool b = true;

	private bool bl = true;

	public int iGangType;

	private bool bBubbleBuyTipText = true;

	public void CheckBossLevel()
	{
		TopBg.SetActive(value: false);
		TopBgBoss.SetActive(value: false);
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex >= 80000)
		{
			TopBgBoss.SetActive(value: true);
			bossHpInit();
			if (Singleton<DataManager>.Instance.bLiuhai)
			{
				TopBgBoss.transform.localPosition -= new Vector3(0f, 65f, 0f);
			}
		}
		else
		{
			TopBg.SetActive(value: true);
		}
		if (Singleton<DataManager>.Instance.bLiuhai)
		{
			TopBg.transform.localPosition -= new Vector3(0f, 65f, 0f);
		}
	}

	public void CutHp(int iHp)
	{
		iBossHp -= iHp;
		LevelGameBossHp.fillAmount = (float)iBossHp * 1f / 100f;
		if (iBossHp <= 0)
		{
			PassLevel.bWin = true;
			PassLevel.action.FlyOver();
		}
		else if ((bool)SoundController.action)
		{
			SoundController.action.playNow("sfx_snails_hit");
		}
	}

	public void ShowGameText(int iType, Vector3 pos, int score = 0)
	{
		Text text = UnityEngine.Object.Instantiate(ShowGameTextObj);
		text.transform.SetParent(ShowGameTextObj.transform.parent.transform, worldPositionStays: false);
		text.gameObject.SetActive(value: true);
		if (score == 0)
		{
			BaseUIAnimation.action.SetLanguageFont("KillBubble" + iType, text, string.Empty, isMaxFont: true);
		}
		else
		{
			BaseUIAnimation.action.SetLanguageFont("KillBubble" + iType, text, score.ToString(), isMaxFont: true);
		}
		AddScore(score);
		text.transform.localPosition = pos;
		BaseUIAnimation.action.ShowGameText(iType, text.gameObject);
	}

	public void AutoShipei()
	{
		if (Singleton<DataManager>.Instance.bGooglePay || !Singleton<DataManager>.Instance.bChinaIos)
		{
			CanvasScaler component = GetComponent<CanvasScaler>();
			if (Screen.height > 1280)
			{
				float num = (float)Screen.width * 100f / ((float)Screen.height * 100f);
				num = 0.5628f - num;
				num *= 2232.14282f;
				num += 1280f;
				component.referenceResolution = new Vector2(720f, num);
				float num2 = (float)Screen.width * 100f / ((float)Screen.height * 100f);
				num2 = 0.5628f - num2;
				num2 *= 11.1607f;
				num2 += 6.4f;
				mainCamera.GetComponent<Camera>().orthographicSize = num2;
				float num3 = (float)Screen.width * 100f / ((float)Screen.height * 100f);
				num3 = 0.5628f - num3;
				num3 *= 11.1607f;
				num3 *= -1f;
				QiuPos.transform.localPosition -= new Vector3(0f, num3, 0f);
			}
		}
	}

	public void bossHpInit()
	{
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_FirstInHuangchong1") == 0)
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_FirstInHuangchong1", 1);
			iBossHp = 10;
		}
	}

	private void Start()
	{
		action = this;
		UI.Instance.ClosePanel();
		if (Singleton<LevelManager>.Instance.bflylevel)
		{
			mubiaoiCon1.SetActive(value: false);
			mubiaoiCon2.SetActive(value: true);
		}
		else
		{
			mubiaoiCon1.SetActive(value: true);
			mubiaoiCon2.SetActive(value: false);
		}
		CheckBossLevel();
		float orthographicSize = mainCamera.GetComponent<Camera>().orthographicSize;
		float num = (float)Screen.width * 1f / (float)Screen.height;
		float num2 = orthographicSize * 2f * num;
		if (num2 < devWidth)
		{
			orthographicSize = devWidth / (2f * num);
			action = this;
			mainCamera.GetComponent<Camera>().orthographicSize = orthographicSize;
		}
		AutoShipei();
		Singleton<DataManager>.Instance.StarGameFlage = false;
		Singleton<DataManager>.Instance.isUpdateWinData = false;
		ScoreImg.GetComponent<Image>().fillAmount = 0f;
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex > 10000 && Singleton<LevelManager>.Instance.iNowSelectLevelIndex < 20000)
		{
			ScoreImgBG.SetActive(value: false);
		}
		iSkillCountText = 0;
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex > 10000 && Singleton<LevelManager>.Instance.iNowSelectLevelIndex < 20000)
		{
			iSkillCountText = Singleton<DataManager>.Instance.iDareScore;
		}
		InitStarData();
		Star1.SetActive(value: false);
		Star2.SetActive(value: false);
		Star3.SetActive(value: false);
		SetScoreText(iSkillCountText);
		MusicController.action.bPlay = false;
		StarMusic();
		LoadBubbleCount(binit: false);
		LoadSkill();
		ComboViewObj.SetActive(value: false);
		if ((bool)LoadingSceneUI.action)
		{
			LoadingSceneUI.action.ChangeCanvasCamera(mainCameraS);
		}
		ChangeGameBg();
		BaseUIAnimation.action.SetLanguageFont("ComboText", ComboText, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BubbleBuyTipText", BubbleBuyTipText, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("WinLevelText1", LEVELTEXT, string.Empty);
		LEVELNBTEXT.text = Singleton<LevelManager>.Instance.iNowSelectLevelIndex.ToString();
		GameInCount();
		iTimeAdMax = 30;
		iTimeAd = iTimeAdMax;
		StartCoroutine(UpdateTimeShowOppoAd());
		HideAD();
		GoogleUMengStartLevel();
		Singleton<LevelManager>.Instance.RBubbleSum = 0;
        AdsManager.HideBanner();
        buyBuShuCount = 0;
		buyBuyBuShu = 0;
		isZhiJieBoFang = false;
	}

	public void HideAD()
	{

	}

	private IEnumerator UpdateTimeShowOppoAd()
	{
		while (true)
		{
			yield return new WaitForSeconds(1f);
			if (iTimeAd > 30)
			{
				iTimeAd = 30;
			}
			iTimeAd--;
			if (iTimeAd <= 0)
			{
				iTimeAd = 32;
                //bool isReady = AdManager.IsInterstitialAdReady();
                //// Show it if it's ready
                //if (isReady)
                //{
                //    AdManager.ShowInterstitialAd();
                //}
            }
		}
	}

	public void GoogleUMengStartLevel()
	{
		
	}

	private void GameInCount()
	{
		if (InitGame.bChinaVersion && Singleton<LevelManager>.Instance.iNowSelectLevelIndex > 20)
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_ThisLoginPlay", 0);
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_ThisLoginEnterGame");
			@int++;
			Singleton<DataManager>.Instance.SaveUserDate("DB_ThisLoginEnterGame", @int);
		}
	}

	public void ChangeGameBg()
	{
		int mapForLevelID = Singleton<UserManager>.Instance.GetMapForLevelID(Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
		mapForLevelID = 1;
		GameBG.GetComponent<SpriteRenderer>().sprite = Util.GetResourcesSprite("Img/bg/game_img_" + mapForLevelID, 1000, 1280);
	}

	public bool CheckOpenUI()
	{
		if (Util.GetNowOpenUI())
		{
			return true;
		}
		return false;
	}

	public bool CheckOpenPauseUI()
	{
		if (Util.GetNowOpenUI())
		{
			return true;
		}
		return false;
	}

	public void InitStarData()
	{
		iStar1 = Singleton<LevelManager>.Instance.star1;
		iStar2 = Singleton<LevelManager>.Instance.star2;
		iStar3 = Singleton<LevelManager>.Instance.star3;
	}

	private IEnumerator IEShowScore(int iScore, GameObject _obj, bool isMuTong = false, float dtime = 0.1f, bool bPos = false)
	{
		if (PassLevel.bWin)
		{
			yield return new WaitForSeconds(0f);
		}
		else
		{
			yield return new WaitForSeconds(dtime);
		}
		int iNowTime = Util.GetNowTime_FF();
		int iPayLastTime = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_ShowScoreLastTime");
		if (iPayLastTime == 0)
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_ShowScoreLastTime", iNowTime);
			iPayLastTime = iNowTime;
		}
		if (iNowTime - iPayLastTime < 300 && iNowTime - iPayLastTime > -1)
		{
			ShowScore(iScore, _obj, isMuTong, bShow: true, 0.3f, bPos: true);
			yield break;
		}
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_ShowScoreLastTime", iNowTime);
		BubbleObj objScript = _obj.GetComponent<BubbleObj>();
		List<Vector2> vecRowCol = BubbleSpawner.Instance.GetAround(objScript.mBubbleData.row, objScript.mBubbleData.col);
		for (int i = 0; i < vecRowCol.Count; i++)
		{
			GameObject[,] bubbleArray = BubbleSpawner.Instance.BubbleArray;
			Vector2 vector = vecRowCol[i];
			int num = (int)vector.x;
			Vector2 vector2 = vecRowCol[i];
			GameObject gameObject = bubbleArray[num, (int)vector2.y];
			if ((bool)gameObject && gameObject.GetComponent<BubbleObj>().mBubbleData.key == "HBoss" && gameObject.GetComponent<BubbleObj>().mBubbleData.i == 0)
			{
				ShowScore(iScore, gameObject, isMuTong, bShow: false, 0.1f, bPos);
			}
		}
	}

	public void ShowScore(int iScore, GameObject obj, bool isMuTong = false, bool bShow = true, float dtime = 0.1f, bool bPos = false)
	{
		if (iScore >= 10000)
		{
			iScore = 9999;
		}
		if (Singleton<LevelManager>.Instance.bBossHuang && bShow)
		{
			StartCoroutine(IEShowScore(iScore, obj, isMuTong, 0.1f, bPos));
			return;
		}
		AddScore(iScore);
		GameObject gameObject = UnityEngine.Object.Instantiate(ScoreObj);
		UnityEngine.Object.Destroy(gameObject, 3f);
		gameObject.transform.SetParent(mainCamera.transform, worldPositionStays: false);
		if ((bool)obj.GetComponent<BubbleObj>())
		{
			Vector2 posByRowAndCol = BubbleSpawner.Instance.GetPosByRowAndCol(obj.GetComponent<BubbleObj>().mBubbleData.row, obj.GetComponent<BubbleObj>().mBubbleData.col);
			gameObject.transform.position = new Vector3(posByRowAndCol.x, posByRowAndCol.y, -20f);
		}
		else
		{
			Transform transform = gameObject.transform;
			Vector3 localPosition = obj.transform.localPosition;
			float x = localPosition.x;
			Vector3 localPosition2 = obj.transform.localPosition;
			transform.position = new Vector3(x, localPosition2.y, -20f);
		}
		Transform transform2 = gameObject.transform;
		Vector3 localPosition3 = obj.transform.localPosition;
		float x2 = localPosition3.x;
		Vector3 localPosition4 = obj.transform.localPosition;
		transform2.position = new Vector3(x2, localPosition4.y, -11f);
		if (bPos)
		{
			int num = UnityEngine.Random.Range(20, 50);
			int num2 = UnityEngine.Random.Range(1, 100);
			if (num2 > 50)
			{
				gameObject.transform.position += new Vector3((float)num * 0.01f, (float)num * 0.01f, 0f);
			}
			else
			{
				gameObject.transform.position -= new Vector3((float)num * 0.01f, (float)num * 0.01f, 0f);
			}
		}
		for (int i = 0; i < iScore.ToString().Length; i++)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate(ScoreNumberObj);
			gameObject2.transform.SetParent(gameObject.transform, worldPositionStays: false);
			int num3 = int.Parse(iScore.ToString().Substring(i, 1));
			int iSize = 28;
			int iSize2 = 36;
			gameObject2.GetComponent<SpriteRenderer>().sprite = Util.GetResourcesSprite("Img/combo/game_score_" + num3, iSize, iSize2);
			gameObject2.SetActive(value: true);
			BaseUIAnimation.action.BubbleScoreNumber(gameObject2, isMuTong);
			if (iScore.ToString().Length == 2)
			{
				if (i == 0)
				{
					gameObject2.transform.localPosition = new Vector3(-0.079f, 0f, 0f);
				}
				if (i == 1)
				{
					gameObject2.transform.localPosition = new Vector3(0.15f, 0f, 0f);
				}
			}
			else if (iScore.ToString().Length == 3)
			{
				if (iScore >= 500)
				{
					if (i == 0)
					{
						gameObject2.transform.localPosition = new Vector3(-0.136f, 0f, 0f);
					}
					if (i == 2)
					{
						gameObject2.transform.localPosition = new Vector3(0.114f, 0f, 0f);
					}
					gameObject2.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
				}
				else
				{
					if (i == 0)
					{
						gameObject2.transform.localPosition = new Vector3(-0.266f, 0f, 0f);
					}
					if (i == 2)
					{
						gameObject2.transform.localPosition = new Vector3(0.244f, 0f, 0f);
					}
				}
			}
			else if (iScore.ToString().Length == 4)
			{
				if (i == 0)
				{
					gameObject2.transform.localPosition = new Vector3(-0.428f, 0f, 0f);
				}
				if (i == 1)
				{
					gameObject2.transform.localPosition = new Vector3(-0.145f, 0f, 0f);
				}
				if (i == 2)
				{
					gameObject2.transform.localPosition = new Vector3(0.15f, 0f, 0f);
				}
				if (i == 3)
				{
					gameObject2.transform.localPosition = new Vector3(0.452f, 0f, 0f);
				}
			}
		}
		BaseUIAnimation.action.BubbleScore(gameObject, isMuTong);
	}

	public void BubbleCombo(int iCombo)
	{
		if (iCombo == 0)
		{
			iNoComboCount++;
		}
		else
		{
			iNoComboCount = 0;
		}
		if (iNoComboCount >= 5)
		{
		}
		if (iCombo < 5)
		{
			if (iLastCombo >= 5)
			{
			}
			if ((bool)MusicController.action)
			{
				MusicController.action.BG_play();
			}
			if (_fx_combleObj != null)
			{
				UnityEngine.Object.Destroy(_fx_combleObj);
				bgImage.transform.GetComponent<SpriteRenderer>().DOColor(new Color(1f, 1f, 1f, 1f), 0.05f);
			}
			return;
		}
		iLastCombo = iCombo;
		if (iCombo >= 5 && (bool)MusicController.action)
		{
			MusicController.action.BG_Combo();
		}
		if (iCombo == 5)
		{
		}
		if (iCombo == 5)
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("combo3");
			}
			ShowGameText(2, new Vector3(0f, 0f, 0f));
		}
		if (iCombo >= 5)
		{
		}
		BaseUIAnimation.action.ComboMoveAni(ComboViewObj);
		if (_fx_combleObj == null)
		{
			_fx_combleObj = UnityEngine.Object.Instantiate(fx_combleObj);
			_fx_combleObj.transform.SetParent(fx_combleObj_Father.transform, worldPositionStays: false);
			bgImage.transform.GetComponent<SpriteRenderer>().DOColor(new Color(0.5f, 0.5f, 0.5f, 1f), 0.1f);
		}
		if (iCombo < 10)
		{
			ComboOneNumberObj.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/combo/game_num_combo_" + iCombo, 40, 52);
			ComboOneNumberObj.SetActive(value: true);
			ComboTwoNumberObj1.SetActive(value: false);
			ComboTwoNumberObj2.SetActive(value: false);
			ComboOneNumberObj.GetComponent<Image>().SetNativeSize();
		}
		else
		{
			ComboTwoNumberObj1.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/combo/game_num_combo_" + iCombo / 10, 40, 52);
			ComboTwoNumberObj2.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/combo/game_num_combo_" + iCombo % 10, 40, 52);
			ComboOneNumberObj.SetActive(value: false);
			ComboTwoNumberObj1.SetActive(value: true);
			ComboTwoNumberObj2.SetActive(value: true);
			ComboTwoNumberObj1.GetComponent<Image>().SetNativeSize();
			ComboTwoNumberObj2.GetComponent<Image>().SetNativeSize();
		}
	}

	public void WinCombo()
	{
		if (_fx_combleObj == null)
		{
			_fx_combleObj = UnityEngine.Object.Instantiate(fx_combleObj);
			_fx_combleObj.transform.SetParent(fx_combleObj_Father.transform, worldPositionStays: false);
			bgImage.transform.GetComponent<SpriteRenderer>().DOColor(new Color(0.5f, 0.5f, 0.5f, 1f), 0.1f);
		}
	}

	private void LoadSkill()
	{
		LSelectSkillObj = new GameObject[3];
		for (int i = 1; i <= 3; i++)
		{
			LSelectSkillObj[i - 1] = UnityEngine.Object.Instantiate(SkillObj);
			LSelectSkillObj[i - 1].transform.SetParent(SkillFather.transform, worldPositionStays: false);
			LSelectSkillObj[i - 1].SetActive(value: true);
			SkillView component = LSelectSkillObj[i - 1].GetComponent<SkillView>();
			component.LoadSkillType(i);
		}
	}

	public void PlaySkillAniRandmo()
	{
		for (int i = 0; i < 3; i++)
		{
			SkillView component = LSelectSkillObj[i].GetComponent<SkillView>();
			component.PlaySkillAni();
		}
	}

	public void StopSkillAniRandmo()
	{
	}

	public void ResSkillCount(int i)
	{
		if (i == 100)
		{
			for (int j = 1; j <= 3; j++)
			{
				if ((bool)LSelectSkillObj[j - 1])
				{
					SkillView component = LSelectSkillObj[j - 1].GetComponent<SkillView>();
					component.LoadSkillType(j);
				}
			}
		}
		else
		{
			if (i > 3)
			{
				i -= 3;
			}
			if ((bool)LSelectSkillObj[i - 1])
			{
				SkillView component2 = LSelectSkillObj[i - 1].GetComponent<SkillView>();
				component2.LoadSkillType(i);
			}
		}
	}

	public void InitUserSkillAni()
	{
		GameFailureUseSkill();
		GameOutUseSkill();
		if (bMaxLine)
		{
			Skill1();
		}
		else if (bBubble3)
		{
			Skill2();
		}
		else if (bAddBubble)
		{
			Skill3();
		}
		else if (bGangSkill)
		{
			Skill4();
		}
	}

	public void Skill1()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_target_item");
		}
		Singleton<LevelManager>.Instance.LogSKILL_USE(1, 0);
		bUseSkill = true;
		GameObject gameObject = UnityEngine.Object.Instantiate(fx_prop1_MaxLine);
		gameObject.transform.SetParent(DownBgObj.transform.parent.transform, worldPositionStays: false);
		gameObject.transform.localPosition = new Vector3(0f, 700f, 0f);
		GameObject gameObject2 = UnityEngine.Object.Instantiate(SkillImgObj);
		gameObject2.transform.localScale = Vector3.zero;
		gameObject2.transform.GetComponent<Image>().DOFade(0f, 0.5f).SetEase(Ease.OutSine)
			.SetDelay(1.3f);
		Sequence s = DOTween.Sequence();
		s.Append(gameObject2.transform.DOScale(new Vector3(0f, 0f, 0f), 0f)).Append(gameObject2.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.17f).SetEase(Ease.OutSine)).Append(gameObject2.transform.DOScale(new Vector3(1f, 1f, 1f), 0.17f).SetEase(Ease.InSine))
			.Append(gameObject2.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.17f).SetEase(Ease.OutSine).SetDelay(0.83f))
			.Append(gameObject2.transform.DOScale(new Vector3(2f, 2f, 2f), 0.17f).SetEase(Ease.InSine));
		gameObject2.transform.SetParent(DownBgObj.transform.parent.transform, worldPositionStays: false);
		gameObject2.transform.localPosition = new Vector3(0f, 700f, 0f);
		gameObject2.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/skillicon/play_icon_prop_1", 112, 110);
		gameObject2.SetActive(value: true);
		UnityEngine.Object.Destroy(gameObject2, 5f);
		BubbleSpawner.Instance.useSkill1();
		StartCoroutine(IESkill1(gameObject));
	}

	private IEnumerator IESkill1(GameObject obj)
	{
		yield return new WaitForSeconds(3f);
		UnityEngine.Object.Destroy(obj);
		bUseSkill = false;
		if (bBubble3)
		{
			Skill2();
		}
		else if (bAddBubble)
		{
			Skill3();
		}
		else if (bGangSkill)
		{
			Skill4();
		}
	}

	public void Skill2()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_target_item");
		}
		Singleton<LevelManager>.Instance.LogSKILL_USE(2, 0);
		bUseSkill = true;
		GameObject gameObject = UnityEngine.Object.Instantiate(SkillImgObj);
		gameObject.transform.localScale = Vector3.zero;
		gameObject.transform.GetComponent<Image>().DOFade(0f, 0.5f).SetEase(Ease.OutSine)
			.SetDelay(1.3f);
		Sequence s = DOTween.Sequence();
		s.Append(gameObject.transform.DOScale(new Vector3(0f, 0f, 0f), 0f)).Append(gameObject.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.17f).SetEase(Ease.OutSine)).Append(gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.17f).SetEase(Ease.InSine))
			.Append(gameObject.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.17f).SetEase(Ease.OutSine).SetDelay(0.83f))
			.Append(gameObject.transform.DOScale(new Vector3(2f, 2f, 2f), 0.17f).SetEase(Ease.InSine));
		gameObject.transform.SetParent(DownBgObj.transform.parent.transform, worldPositionStays: false);
		gameObject.transform.localPosition = new Vector3(0f, 700f, 0f);
		gameObject.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/skillicon/play_icon_prop_2", 112, 110);
		gameObject.SetActive(value: true);
		UnityEngine.Object.Destroy(gameObject, 5f);
		GameObject _fx_prop2_Bubble3 = UnityEngine.Object.Instantiate(fx_prop2_Bubble3);
		_fx_prop2_Bubble3.transform.SetParent(DownBgObj.transform.parent.transform, worldPositionStays: false);
		_fx_prop2_Bubble3.transform.localPosition = new Vector3(0f, 700f, 0f);
		_fx_prop2_Bubble3.transform.DOLocalMove(DownBgObj.transform.localPosition + new Vector3(66f, -20f, 0f), 0.8f).SetDelay(1.17f).OnComplete(delegate
		{
			ChangeAniBubble3(_fx_prop2_Bubble3);
		});
	}

	public void Skill4()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_target_item");
		}
		bUseSkill = true;
		Singleton<LevelManager>.Instance.LogSKILL_USE(0, 0);
		GameObject gameObject = UnityEngine.Object.Instantiate(fx_prop1_MaxLine);
		gameObject.transform.SetParent(DownBgObj.transform.parent.transform, worldPositionStays: false);
		gameObject.transform.localPosition = new Vector3(0f, 700f, 0f);
		GameObject gameObject2 = UnityEngine.Object.Instantiate(SkillImgObj);
		gameObject2.transform.localScale = Vector3.zero;
		gameObject2.transform.GetComponent<Image>().DOFade(0f, 0.5f).SetEase(Ease.OutSine)
			.SetDelay(1.3f);
		Sequence s = DOTween.Sequence();
		s.Append(gameObject2.transform.DOScale(new Vector3(0f, 0f, 0f), 0f)).Append(gameObject2.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.17f).SetEase(Ease.OutSine)).Append(gameObject2.transform.DOScale(new Vector3(1f, 1f, 1f), 0.17f).SetEase(Ease.InSine))
			.Append(gameObject2.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.17f).SetEase(Ease.OutSine).SetDelay(0.83f))
			.Append(gameObject2.transform.DOScale(new Vector3(2f, 2f, 2f), 0.17f).SetEase(Ease.InSine));
		gameObject2.transform.SetParent(DownBgObj.transform.parent.transform, worldPositionStays: false);
		gameObject2.transform.localPosition = new Vector3(0f, 700f, 0f);
		gameObject2.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/skillicon/play_icon_prop_4", 112, 110);
		gameObject2.SetActive(value: true);
		UnityEngine.Object.Destroy(gameObject2, 5f);
		StartCoroutine(FullGang(gameObject));
	}

	private IEnumerator FullGang(GameObject obj)
	{
		yield return new WaitForSeconds(3f);
		UnityEngine.Object.Destroy(obj);
		bool bAdd = false;
		if (skilliRand < 15)
		{
			if (GameGuide.Instance.MT1.GetComponent<MuTong>().isSkill)
			{
				GameGuide.Instance.MT1.GetComponent<MuTong>().AddSkill();
				bAdd = true;
			}
		}
		else if (skilliRand < 55)
		{
			if (GameGuide.Instance.MT2.GetComponent<MuTong>().isSkill)
			{
				GameGuide.Instance.MT2.GetComponent<MuTong>().AddSkill();
				bAdd = true;
			}
		}
		else if (skilliRand < 85)
		{
			if (GameGuide.Instance.MT4.GetComponent<MuTong>().isSkill)
			{
				GameGuide.Instance.MT4.GetComponent<MuTong>().AddSkill();
				bAdd = true;
			}
		}
		else if (GameGuide.Instance.MT5.GetComponent<MuTong>().isSkill)
		{
			GameGuide.Instance.MT5.GetComponent<MuTong>().AddSkill();
			bAdd = true;
		}
		if (!bAdd)
		{
			if (GameGuide.Instance.MT2.GetComponent<MuTong>().isSkill)
			{
				GameGuide.Instance.MT2.GetComponent<MuTong>().AddSkill();
			}
			else if (GameGuide.Instance.MT4.GetComponent<MuTong>().isSkill)
			{
				GameGuide.Instance.MT4.GetComponent<MuTong>().AddSkill();
			}
			else if (GameGuide.Instance.MT1.GetComponent<MuTong>().isSkill)
			{
				GameGuide.Instance.MT1.GetComponent<MuTong>().AddSkill();
			}
			else if (GameGuide.Instance.MT5.GetComponent<MuTong>().isSkill)
			{
				GameGuide.Instance.MT5.GetComponent<MuTong>().AddSkill();
			}
		}
		bUseSkill = false;
	}

	public void Skill3()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_target_item");
		}
		bUseSkill = true;
		Singleton<LevelManager>.Instance.LogSKILL_USE(3, 0);
		GameObject gameObject = UnityEngine.Object.Instantiate(SkillImgObj);
		gameObject.transform.localScale = Vector3.zero;
		gameObject.transform.GetComponent<Image>().DOFade(0f, 0.5f).SetEase(Ease.OutSine)
			.SetDelay(1.3f);
		Sequence s = DOTween.Sequence();
		s.Append(gameObject.transform.DOScale(new Vector3(0f, 0f, 0f), 0f)).Append(gameObject.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.17f).SetEase(Ease.OutSine)).Append(gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.17f).SetEase(Ease.InSine))
			.Append(gameObject.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.17f).SetEase(Ease.OutSine).SetDelay(0.83f))
			.Append(gameObject.transform.DOScale(new Vector3(2f, 2f, 2f), 0.17f).SetEase(Ease.InSine));
		gameObject.transform.SetParent(DownBgObj.transform.parent.transform, worldPositionStays: false);
		gameObject.transform.localPosition = new Vector3(0f, 700f, 0f);
		gameObject.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/skillicon/play_icon_prop_3", 112, 110);
		gameObject.SetActive(value: true);
		UnityEngine.Object.Destroy(gameObject, 5f);
		GameObject _fx_prop3_addBubble = UnityEngine.Object.Instantiate(fx_prop3_addBubble);
		_fx_prop3_addBubble.transform.SetParent(DownBgObj.transform.parent.transform, worldPositionStays: false);
		_fx_prop3_addBubble.transform.localPosition = new Vector3(0f, 700f, 0f);
		_fx_prop3_addBubble.transform.DOLocalMove(DownBgObj.transform.localPosition, 0.8f).SetDelay(1.17f).OnComplete(delegate
		{
			ChangeAnibAddBubble(_fx_prop3_addBubble);
		});
	}

	private void ChangeAniBubble3(GameObject obj)
	{
		SkeletonAnimation component = obj.GetComponent<SkeletonAnimation>();
		component.Initialize(overwrite: true);
		component.loop = false;
		component.state.SetAnimation(0, "hit", loop: false);
		BubbleSpawner.Instance.useSkill2();
		component.state.End += delegate
		{
			UnityEngine.Object.Destroy(obj, 5f);
			bUseSkill = false;
			if (bAddBubble)
			{
				Skill3();
			}
			else if (bGangSkill)
			{
				Skill4();
			}
		};
	}

	private void ChangeAnibAddBubble(GameObject obj)
	{
		SkeletonAnimation component = obj.GetComponent<SkeletonAnimation>();
		component.Initialize(overwrite: true);
		component.loop = false;
		component.state.SetAnimation(0, "hit", loop: false);
		component.state.End += delegate
		{
			bUseSkill = false;
			Singleton<LevelManager>.Instance.iBubbleCount = Singleton<LevelManager>.Instance.iBubbleCount + 10;
			Singleton<LevelManager>.Instance.iBubbleUseCount = 0;
			BubbleCountText.text = Singleton<LevelManager>.Instance.iBubbleCount.ToString();
			UnityEngine.Object.Destroy(obj, 5f);
			if (bGangSkill)
			{
				Skill4();
			}
		};
	}

	public void LoadBubbleCount(bool binit = true)
	{
		if (Singleton<LevelManager>.Instance.iBubbleCount < 0)
		{
			Singleton<LevelManager>.Instance.iBubbleCount = 0;
		}
		BubbleCountText.text = Singleton<LevelManager>.Instance.iBubbleCount.ToString();
		if (Singleton<LevelManager>.Instance.iBubbleCount <= 5)
		{
			bLoadBubbleCont = true;
			try
			{
			}
			catch (Exception)
			{
			}
		}
		else
		{
			try
			{
			}
			catch (Exception)
			{
			}
		}
		if ((bool)PassLevel.action && PassLevel.bWin && Singleton<LevelManager>.Instance.iBubbleCount <= 0)
		{
			BubbleCountText.gameObject.SetActive(value: false);
		}
	}

	public void ShowBubbleCountText()
	{
		BubbleCountText.gameObject.SetActive(value: true);
		BubbleCountText.text = Singleton<LevelManager>.Instance.iBubbleCount.ToString();
	}

	public IEnumerator OpenBuyBubbleUI()
	{
		if (Singleton<LevelManager>.Instance.bflylevel)
		{
			yield return new WaitForSeconds(1.1f);
		}
		else
		{
			yield return new WaitForSeconds(0.6f);
		}
		if (!PassLevel.bWin)
		{
			InitAndroid.action.GAEvent("clickAdd10Auto");
			InitAndroid.action.GAEvent("NewclickAdd10Auto:" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
			Singleton<DataManager>.Instance.bAutoOpen = true;
			action.KillNowBuyBubble();
			action.isZhiJieBoFang = false;
			UI.Instance.OpenPanel(UIPanelType.NowBuyBubbleUI);
			FirebaseController.PopFailureBuyForm();
		}
	}

	private IEnumerator OpenReadyGoUI()
	{
		yield return new WaitForSeconds(0.5f);
		UI.Instance.OpenPanel(UIPanelType.ReadyGoUI);
	}

	public void StarMusic()
	{
		if ((bool)MusicController.action)
		{
			MusicController.action.BG_play();
		}
	}

	public void AddScore(int iScore)
	{
		iSkillCountText += iScore;
		Singleton<LevelManager>.Instance.iNowScore = iSkillCountText;
		SetScoreText(iSkillCountText);
		UpdateProgressBar();
	}

	public void UpdateProgressBar()
	{
		if (ScoreImgBG.active)
		{
			float num = 0f;
			int num2 = 0;
			if ((float)iSkillCountText > iStar3)
			{
				num2 = 3;
				num = 1f;
				ScoreImg.GetComponent<Image>().fillAmount = 1f;
			}
			else if ((float)iSkillCountText > iStar2)
			{
				num2 = 2;
				num = ((float)iSkillCountText - iStar2) / (iStar3 - iStar2);
				num = (80f + 20f * num) / 100f;
				ScoreImg.GetComponent<Image>().fillAmount = num;
			}
			else if ((float)iSkillCountText > iStar1)
			{
				num2 = 1;
				num = ((float)iSkillCountText - iStar1) / (iStar2 - iStar1);
				num = (48f + 32f * num) / 100f;
				ScoreImg.GetComponent<Image>().fillAmount = num;
			}
			else
			{
				num2 = 0;
				num = (float)iSkillCountText / iStar1;
				num = 48f * num / 100f;
				ScoreImg.GetComponent<Image>().fillAmount = num;
			}
			if (num2 > Singleton<LevelManager>.Instance.iNowStar)
			{
				Singleton<LevelManager>.Instance.iNowStar = num2;
			}
			if (num >= 0.48f)
			{
				FlyStar(1, Star1);
			}
			if (num >= 0.8f)
			{
				FlyStar(2, Star2);
			}
			if (num >= 1f)
			{
				FlyStar(3, Star3);
				Singleton<LevelManager>.Instance.iNowStar = 3;
			}
		}
	}

	public void FlyStar(int iStar, GameObject _StarObj)
	{
		if (iStar > iNowStar)
		{
			iNowStar = iStar;
			GameObject _fx_ui_goalStarObj = UnityEngine.Object.Instantiate(fx_ui_goalStarObj);
			_fx_ui_goalStarObj.transform.SetParent(_StarObj.transform.parent.transform, worldPositionStays: false);
			_fx_ui_goalStarObj.transform.localPosition = new Vector3(0f, -500f, 0f);
			_fx_ui_goalStarObj.transform.localScale = new Vector2(150f, 150f);
			_fx_ui_goalStarObj.transform.DOScale(new Vector2(200f, 200f), 0.4f).SetEase(Ease.OutSine);
			_fx_ui_goalStarObj.transform.DOScale(new Vector2(150f, 150f), 0.3f).SetEase(Ease.OutSine).SetDelay(0.4f);
			_fx_ui_goalStarObj.transform.DOScale(new Vector2(200f, 200f), 0.2f).SetEase(Ease.OutSine).SetDelay(0.3f);
			Vector3 vector = _StarObj.transform.localPosition;
			float x = vector.x;
			float y = vector.y;
			Vector3 localPosition = _StarObj.transform.localPosition;
			vector = new Vector3(x, y, localPosition.z);
			Vector3 localPosition2 = _fx_ui_goalStarObj.transform.localPosition;
			float num = localPosition2.y - 20f;
			Vector3 localPosition3 = _fx_ui_goalStarObj.transform.localPosition;
			float num2 = localPosition3.x + 20f;
			float duration = 0.8f;
			float x2 = num2;
			float y2 = num;
			Vector3 localPosition4 = _fx_ui_goalStarObj.transform.localPosition;
			Vector3 vector2 = new Vector3(x2, y2, localPosition4.z);
			Vector3 centerPost = PassLevel.action.GetCenterPost(vector2, vector, 9f);
			Vector3[] path = new Vector3[1]
			{
				vector2
			};
			Vector3[] waypoints = new Vector3[2]
			{
				centerPost,
				vector
			};
			_fx_ui_goalStarObj.transform.DOLocalPath(path, duration, PathType.CatmullRom, PathMode.TopDown2D).SetEase(Ease.OutSine).OnComplete(delegate
			{
				Fly2(waypoints, _StarObj, _fx_ui_goalStarObj);
			});
		}
	}

	public void Fly2(Vector3[] waypoints1, GameObject obj1, GameObject obj2)
	{
		obj2.transform.DOLocalPath(waypoints1, 0.7f, PathType.CatmullRom, PathMode.TopDown2D).SetEase(Ease.InSine).OnComplete(delegate
		{
			OpenStar(obj1, obj2);
		});
	}

	private void OpenStar(GameObject _StarObj, GameObject fx_ui_goalStarObj)
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_star");
		}
		UnityEngine.Object.Destroy(fx_ui_goalStarObj);
		GameObject gameObject = UnityEngine.Object.Instantiate(fx_goalstar_lightObj);
		gameObject.transform.SetParent(_StarObj.transform.parent.transform, worldPositionStays: false);
		gameObject.transform.localPosition = _StarObj.transform.localPosition;
		_StarObj.SetActive(value: true);
	}

	private void SetScoreText(int iScore)
	{
		for (int i = 0; i < 6; i++)
		{
			LSkillCountText[i].SetText("0");
			if (iScore <= 0)
			{
				LSkillCountText[i].gameObject.SetActive(value: true);
				LSkillCountText[i].SetText("0");
				continue;
			}
			LSkillCountText[i].gameObject.SetActive(value: true);
			int num = iScore % 10;
			iScore /= 10;
			LSkillCountText[i].SetText(num.ToString());
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			iTimeAd = iTimeAdMax;
			if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 1 && !GuideMaxUI.action)
			{
				GameGuide.Instance.isCanShoot = true;
			}
		}
		ReturnClick();
	}

	private void ReturnClick()
	{
		if (UnityEngine.Input.GetKey("l") && bl)
		{
			bl = false;
			PassLevel.bWin = true;
			UI.Instance.OpenPanel(UIPanelType.TipWin);
		}
		if (Application.platform == RuntimePlatform.Android && UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			if (!action)
			{
				return;
			}
			if ((bool)GuideMaxUI.action)
			{
				GuideMaxUI.action.CloseUI();
			}
			if ((bool)GuideMaxUIPanel.panel && UI.Instance.GetTopPanelType() == UIPanelType.GuideMaxPanel)
			{
				GameGuide.Instance.isCanShoot = true;
				UI.Instance.ClosePanel();
				return;
			}
			if ((bool)GuideMinUI.action)
			{
				GuideMinUI.action.CloseUI();
			}
			if ((bool)GuideMinUIPanel.panel && UI.Instance.GetTopPanelType() == UIPanelType.GuideMinPanel)
			{
				GameGuide.Instance.isCanShoot = true;
				UI.Instance.ClosePanel();
			}
			else if (UI.Instance.GetPanelCount() > 0)
			{
				UI.Instance.ClosePanel();
			}
			else
			{
				PauseUI.action.clickquit2();
			}
		}
		else
		{
			if (!Input.GetKey("u") || !b)
			{
				return;
			}
			b = false;
			if (!action)
			{
				return;
			}
			if ((bool)GuideMaxUI.action)
			{
				GameGuide.Instance.isCanShoot = true;
				GuideMaxUI.action.CloseUI();
				if ((bool)GuideMaxUIPanel.panel && UI.Instance.GetTopPanelType() == UIPanelType.GuideMaxPanel)
				{
					UI.Instance.ClosePanel();
				}
			}
			else if ((bool)GuideMinUI.action)
			{
				GameGuide.Instance.isCanShoot = true;
				GuideMinUI.action.CloseUI();
				if ((bool)GuideMinUIPanel.panel && UI.Instance.GetTopPanelType() == UIPanelType.GuideMinPanel)
				{
					UI.Instance.ClosePanel();
				}
			}
			else if (UI.Instance.GetPanelCount() > 0)
			{
				UI.Instance.ClosePanel();
			}
			else
			{
				PauseUI.action.clickquit2();
			}
		}
	}

	public void PauseBtn()
	{
		if (!BubbleSpawner.IsWait)
		{
			return;
		}
        //if ((bool)AdManager.action)
        //{
        //	AdManager.action.OpenShowVideoManager(1);
        //	AdManager.action.opadshowcp(DataManager.PAGE_PAUSE);
        //	AdManager.action.bubbleenAdchaping(3);
        //}
        if (UI.Instance.GetPanelCount() <= 0 && !PassLevel.bWin && !Singleton<DataManager>.Instance.bUiIsOpen)
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("ButtonClick");
			}
			if (BaseUIAnimation.bClickButton)
			{
				BaseUIAnimation.action.ClickButton(PauseBtnObj.gameObject);
				Singleton<LevelManager>.Instance.bRstart = true;
				isZhiJieBoFang = false;
				UI.Instance.OpenPanel(UIPanelType.QuitUI);
                AdsManager.ShowBanner();
            }
		}
	}

	private IEnumerator CallClosePauseUI()
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		PauseUI.action.bQuit = false;
		BaseUIAnimation.action.ShowPauseUI(PauseUIObj.gameObject);
	}

	public void BuyGang(int iType)
	{
		if (!Util.GetNowOpenUI() && !PassLevel.bWin)
		{
			iGangType = iType;
			UI.Instance.OpenPanel(UIPanelType.BuyGangUI);
		}
	}

	public void ShowTipBubbleObj()
	{
		BaseUIAnimation.action.ShowTipBubbleObj(TipBubbleObj, TipBubbleObjNumber);
		if (bBubbleBuyTipText)
		{
			BaseUIAnimation.action.SetLanguageFont("BubbleBuyTipText", BubbleBuyTipText, string.Empty);
			bBubbleBuyTipText = false;
		}
	}

	public void CreateNowBuyBubble()
	{
		_BuyBubbleBtnObj = UnityEngine.Object.Instantiate(BuyBubbleBtnObj);
		_BuyBubbleBtnObj.transform.SetParent(base.transform, worldPositionStays: false);
		BaseUIAnimation.action.CreateButton(_BuyBubbleBtnObj, bAdEvent: false);
	}

	public void KillNowBuyBubble()
	{
		isZhiJieBoFang = false;
		UnityEngine.Object.Destroy(_BuyBubbleBtnObj);
	}

	public void BossReward()
	{
		StartCoroutine(IEShowReward());
	}

	private IEnumerator IEShowReward()
	{
		yield return new WaitForSeconds(2f);
		List<int> Ltype = new List<int>();
		List<int> LNum = new List<int>();
		for (int i = 1; i <= 5; i++)
		{
			int num = UnityEngine.Random.Range(1, 101);
			int num2 = int.Parse(Singleton<DataManager>.Instance.dDataHua6[i.ToString()]["inumber"]);
			if (num > num2)
			{
				num = UnityEngine.Random.Range(0, 4);
				string text = Singleton<DataManager>.Instance.dDataHua6[i.ToString()]["sReward"];
				string text2 = text.Split('F')[num];
				int num3 = int.Parse(text2.Split('|')[0]);
				int num4 = int.Parse(text2.Split('|')[1]);
				Ltype.Add(num3);
				LNum.Add(num4);
				ChinaPay.action.addRewardAll(num3, num4, base.gameObject, isShow: false);
			}
		}
		if (Ltype.Count > 0)
		{
			BaseUIAnimation.action.ShowProp(Ltype, LNum, base.gameObject);
		}
		yield return new WaitForSeconds(2.5f);
		Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.HuaScene);
	}

	public void GameOutUseSkill()
	{
		for (int i = 0; i <= 3; i++)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Skill_Select_" + i);
			if (@int == 1)
			{
				if (i == 0)
				{
				}
				Singleton<DataManager>.Instance.SaveUserDate("DB_Skill_Select_" + i, 0);
				Singleton<UserLevelManager>.Instance.UseAdded();
				bool flag = false;
				if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 16 && i == 1)
				{
					flag = true;
				}
				if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 28 && i == 2)
				{
					flag = true;
				}
				if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 61 && i == 3)
				{
					flag = true;
				}
				if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 81 && i == 0)
				{
					flag = true;
				}
				if (!flag)
				{
					PayManager.action.DeleteSkill(i);
				}
				if (i == 0)
				{
					bGangSkill = true;
					aliyunlog.UseSkill("free", 7);
				}
				switch (i)
				{
				case 1:
					bMaxLine = true;
					aliyunlog.UseSkill("free", 1);
					break;
				case 2:
					bBubble3 = true;
					aliyunlog.UseSkill("free", 2);
					break;
				case 3:
					aliyunlog.UseSkill("free", 3);
					bAddBubble = true;
					break;
				}
			}
		}
	}

	public void GameFailureUseSkill()
	{
	}
}
