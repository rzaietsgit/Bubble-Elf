using DG.Tweening;
using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using ITSoft;
using UnityEngine;

public class PlayPanel : PlayPanelBase
{
	public static PlayPanel panel;

	public GameObject SelectSkillFather;

	public GameObject SelectSkillObj;

	private List<PlaySkillSelect> skills;

	public int iPlayAdShowMax;

	private bool bAutoStart_;

	public int ClickGuide;

	private int ifinger;

	private int ifingercount = 2;

	public bool bLoveFly;

	private bool isChangeScene;

	public override void InitUI()
	{
		panel = this;
		for (int i = 0; i <= 3; i++)
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_Skill_Select_" + i, 0);
		}
		Singleton<DataManager>.Instance.bAdRewardPlay = false;
		Singleton<DataManager>.Instance.bAdRewardLose = false;
		Singleton<DataManager>.Instance.bAdRewardHome = false;
		Singleton<DataManager>.Instance.PlayGameOpenSale = false;
		skills = new List<PlaySkillSelect>();
		int num = Singleton<LevelManager>.Instance.iNowSelectLevelIndex;
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
		Singleton<UserManager>.Instance.PassLevelAward(@int);
		if (num <= 0)
		{
			num = @int + 1;
		}
		if (num > @int + 1)
		{
			UI.Instance.ClosePanel();
			return;
		}
		if (InitGame.bChinaVersion)
		{
			InitChinaVersion();
		}
		OpenGuide();
		string lEVELID = Singleton<DataManager>.Instance.dDataLevelTypeAndRemark[string.Empty + num]["sRemark"].ToString();
		BaseUIAnimation.action.SetLanguageFont("PlayUIRemark", detail.LevelRemarkText_Text, lEVELID);
		BaseUIAnimation.action.SetLanguageFont("PlayUILevelTitle", detail.PlayUILevelTitle_Text, num.ToString());
		BaseUIAnimation.action.SetLanguageFont("PlayUIStarBtnText", detail.PlayUIStarBtnText_Text, string.Empty);
		LoadStar(Singleton<UserManager>.Instance.GetLevelStar(num));
		LoadSelectSkill();
		InitAndroid.action.CheckPlayVideo(bisplay: true);
	}

	public void ShowYunbuAd()
	{
		detail.ChinaLoveImage_Image.gameObject.SetActive(value: false);
		detail.ChinaLoveImage_Image.gameObject.SetActive(value: false);
		detail.StartBtn_Button.gameObject.SetActive(value: false);
		detail.yunbuAdBtn1_Button.gameObject.SetActive(value: true);
		detail.yunbuAdBtn2_Button.gameObject.SetActive(value: true);
		SetAdCounts();
	}

	public void ShowYunbuAdVidoe()
	{
		ShowYunbuAd();
	}

	public void SetAdCounts()
	{
	}

	public void HideYunbuAd()
	{
		detail.ChinaLoveImage_Image.gameObject.SetActive(value: true);
		detail.ChinaLoveImage_Image.gameObject.SetActive(value: true);
		detail.StartBtn_Button.gameObject.SetActive(value: true);
		detail.yunbuAdBtn1_Button.gameObject.SetActive(value: false);
		detail.yunbuAdBtn2_Button.gameObject.SetActive(value: false);
	}

	public override void OnPauseBase()
	{
	}

	public override void OnResumeBase()
	{
		for (int i = 0; i < skills.Count; i++)
		{
			skills[i].LoadSKillType();
			skills[i].LoadSKillCount();
		}
	}

	public override void OnResume()
	{
		for (int i = 0; i < skills.Count; i++)
		{
			skills[i].LoadSKillType();
			skills[i].LoadSKillCount();
		}
	}

	public override void OnCloseButton()
	{
        AdsManager.HideBanner();
        InitAndroid.action.GAEvent("clickbtn:playclose");
		Singleton<DataManager>.Instance.PlayGameOpenSale = false;
		UI.Instance.ClosePanel();
	}

	private void LoadStar(int iStart)
	{
		if (iStart >= 1)
		{
			detail.star1_Image.gameObject.SetActive(value: true);
		}
		else
		{
			detail.star1_Image.gameObject.SetActive(value: false);
		}
		if (iStart >= 2)
		{
			detail.star2_Image.gameObject.SetActive(value: true);
		}
		else
		{
			detail.star2_Image.gameObject.SetActive(value: false);
		}
		if (iStart >= 3)
		{
			detail.star3_Image.gameObject.SetActive(value: true);
		}
		else
		{
			detail.star3_Image.gameObject.SetActive(value: false);
		}
	}

	private void LoadSelectSkill()
	{
		for (int i = 0; i <= 3; i++)
		{
			GameObject gameObject = Object.Instantiate(SelectSkillObj);
			gameObject.transform.SetParent(SelectSkillFather.transform, worldPositionStays: false);
			gameObject.SetActive(value: true);
			PlaySkillSelect component = gameObject.GetComponent<PlaySkillSelect>();
			component.SetType(i);
			skills.Add(component);
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Skill_Select_" + i);
			if (@int == 1)
			{
				component.LoadSelect();
			}
		}
	}

	public void InitChinaVersion()
	{
		if (!InitGame.bEnios)
		{
			detail.ChinaLoveImage_Image.gameObject.SetActive(value: true);
			detail.PlayUIStarBtnText_Text.fontSize = detail.PlayUIStarBtnText_Text.fontSize - 10;
			detail.PlayUIStarBtnText_Text.transform.localPosition -= new Vector3(40f, 0f, 0f);
			iPlayAdShowMax = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_PlayAdShowMax" + Util.GetNowTime_Day(), 10);
		}
	}

	public void InitVersion()
	{
		detail.ChinaLoveImage_Image.gameObject.SetActive(value: false);
	}

	public void CloseHaoping()
	{
	}

	public void OpenHaoping()
	{
	}

	public void OpenGuide()
	{
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 16)
		{
			ClickGuide = 1;
			SetGuideImagePos(-56);
		}
		else if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 28)
		{
			ClickGuide = 2;
			SetGuideImagePos(74);
		}
		else if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 61)
		{
			ClickGuide = 3;
			SetGuideImagePos(194);
		}
		else if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 81)
		{
			ClickGuide = 4;
			SetGuideImagePos(-180);
		}
		if (ClickGuide > 0)
		{
			detail.finger_SkeletonAnimation.gameObject.SetActive(value: false);
		}
		else if (InitGame.bChinaVersion && !InitGame.bEnios)
		{
			StartCoroutine(IEShowFingerObj());
		}
	}

	public void SetGuideImagePos(int x)
	{
		detail.GuideImg_Image.gameObject.SetActive(value: true);
		detail.GuideImg_Image.transform.localPosition = new Vector3(x, -80f, 0f);
		GuideImagePosAni(detail.GuideImg_Image.gameObject);
	}

	private void GuideImagePosAni(GameObject obj)
	{
		if (ClickGuide == 0)
		{
			detail.GuideImg_Image.gameObject.SetActive(value: false);
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

	private IEnumerator IEShowFingerObj()
	{
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex <= 15)
		{
			yield return new WaitForSeconds(0.1f);
			yield break;
		}
		bool b = true;
		while (b)
		{
			yield return new WaitForSeconds(1f);
			if (ifinger > ifingercount)
			{
				b = false;
			}
			ifinger++;
		}
	}

	public void YunbuCheck()
	{
		if (!InitGame.bEnios && !Singleton<DataManager>.Instance.bChinaIos && Singleton<DataManager>.Instance.bShowHaopingLogin)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_bHaoping");
			if (@int <= 0)
			{
				Singleton<DataManager>.Instance.SendyunbuCheck();
			}
		}
	}

	public override void OnyunbuAdBtn1()
	{
		StartBtn_(b: true);
	}

	public void RewardPlayGame()
	{
		StartBtn_(b: true);
	}

	public void NewOnyunbuAdBtn1()
	{
		StartBtn_(b: true);
	}

	public void NewOnyunbuAdBtn2()
	{
		Singleton<DataManager>.Instance.bAdRewardPlay = true;
		Singleton<DataManager>.Instance.bAdRewardLose = false;
		Singleton<DataManager>.Instance.bAdRewardHome = false;
		InitAndroid.action.PlayVideoAd();
		UnityEngine.Debug.Log("OnyunbuAdBtn2------------");
	}

	public override void OnyunbuAdBtn2()
	{
		Singleton<DataManager>.Instance.bAdRewardPlay = true;
		Singleton<DataManager>.Instance.bAdRewardLose = false;
		Singleton<DataManager>.Instance.bAdRewardHome = false;
		InitAndroid.action.PlayVideoAd();
		UnityEngine.Debug.Log("OnyunbuAdBtn2------------");
	}

	public void StartBtn_(bool b = false)
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
		if (num >= Singleton<DataManager>.Instance.iLoveUse)
		{
			Vector3 vector = new Vector3(0f, 0f, 0f);
			if (!bLoveFly)
			{
				bLoveFly = true;
				if (b)
				{
					bLoveFly = false;
					StartGM(b: false);
				}
				else
				{
					LoveFly(detail.LoveMove_Image.gameObject, detail.ChinaLoveImage_Image.gameObject.transform.localPosition);
				}
			}
		}
		else
		{
			UI.Instance.OpenPanel(UIPanelType.BuyLivesChinaUI);
		}
	}

	public void NewOnStartBtn()
	{
		InitAndroid.action.GAEvent("clickbtn:OnStartBtn");
		StartBtn_();
	}

	public override void OnStartBtn()
	{
		InitAndroid.action.GAEvent("clickbtn:OnStartBtn");
		StartBtn_();
	}

	private void LoveFly(GameObject obj, Vector3 ToPos)
	{
		if ((bool)MapUI.action)
		{
			GameObject gameObject = MapUI.action.TopLeftObj.transform.parent.gameObject;
			MapUI.action.TopLeftObj.gameObject.transform.SetParent(base.gameObject.transform);
			obj.transform.localPosition = MapUI.action.TopLeftObj.transform.localPosition;
			obj.transform.localPosition -= new Vector3(80f, 60f, 0f);
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

	private void StartGM(bool b = true)
	{
		if (b)
		{
			detail.ui_elf_SkeletonAnimation.transform.parent.gameObject.SetActive(value: true);
		}
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
				MapUI.action.LoveText.text = (@int - 1).ToString();
			}
		}
		yield return new WaitForSeconds(0.7f);
		Singleton<DataManager>.Instance.StarGameFlage = false;
		isChangeScene = true;
		UI.Instance.ClosePanel();
	}

	public override void OnExit()
	{
		if (!isChangeScene)
		{
			return;
		}
		if (openSale() && !InitGame.bCloseLBForEnIos)
		{
			UnityEngine.Debug.Log("Playpanel OnExit   PlayGameOpenSale = true");
			Singleton<DataManager>.Instance.PlayGameOpenSale = true;
			UI.Instance.OpenPanel(UIPanelType.SaleAdUI);
			return;
		}
		int num = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
		int loveInfinite = Singleton<UserManager>.Instance.getLoveInfinite();
		if (loveInfinite > 0)
		{
			num = Singleton<DataManager>.Instance.iLoveMaxAll;
		}
		if (num >= Singleton<DataManager>.Instance.iLoveUse)
		{
			if ((bool)MapUI.action)
			{
				MapUI.action.LoadLove();
			}
			Singleton<UserManager>.Instance.EnterLog();
			Singleton<LevelManager>.Instance.bLoadOver = false;
			if (LevelManager.bWwwDataFlag)
			{
				Singleton<LevelManager>.Instance.LoadLevelData();
				DataManager.SelectLevel = 0;
				DDOLSingleton<CoroutineController>.Instance.StartCoroutine(IEStartGame());
			}
			else
			{
				Camera.main.transform.GetComponent<RapidBlurEffect>().enabled = false;
				Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.GameScene);
			}
		}
	}

	private IEnumerator IEStartGame()
	{
		bool b = true;
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
		if (num <= 10)
		{
			DataManager.sale_adKey = "Bubble_LB" + Singleton<UserManager>.Instance.GetLb(50, 85);
			return true;
		}
		if (@int <= 50)
		{
			DataManager.sale_adKey = "Bubble_LB" + Singleton<UserManager>.Instance.GetLb(15, 85);
			return true;
		}
		if (Singleton<UserManager>.Instance.iSKillCount() < 10)
		{
			DataManager.sale_adKey = "Bubble_LB" + Singleton<UserManager>.Instance.GetLb(15, 45);
			return true;
		}
		return false;
	}
}
