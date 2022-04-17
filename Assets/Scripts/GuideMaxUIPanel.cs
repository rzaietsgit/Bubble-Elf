using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GuideMaxUIPanel : GuideMaxUIPanelBase
{
	public static GuideMaxUIPanel panel;

	public override void InitUI()
	{
		panel = this;
		UnityEngine.Debug.Log("GuideMaxUIPanel      InitUI");
		aliyunlog.OpenAndClickBtn("GuideMaxUI", Singleton<LevelManager>.Instance.iNowSelectLevelIndex.ToString(), string.Empty);
		GameGuide.Instance.isCanShoot = false;
		BaseUIAnimation.action.SetLanguageFont("GuideMaxRemark" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex, detail.GuideMaxRemark_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("GuideMaxTitle", detail.GuideMaxTitle_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("GuideMaxNext", detail.NextText_Text, string.Empty);
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 2 || Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 61)
		{
			detail.GuideImage_Image.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/guide/guide_bubble_level" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex, 592, 311);
		}
		else
		{
			detail.GuideImage_Image.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/guide/guide_bubble_level" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex, 592, 283);
		}
		InitAndroid.action.GAEvent("NewGuideMax:0:" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
		StartCoroutine(Time5());
	}

	private IEnumerator Time5()
	{
		yield return new WaitForSeconds(5f);
		InitAndroid.action.GAEvent("NewGuideMax:5:" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
		yield return new WaitForSeconds(5f);
		InitAndroid.action.GAEvent("NewGuideMax:10:" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
		yield return new WaitForSeconds(5f);
		InitAndroid.action.GAEvent("NewGuideMax:15:" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
	}

	public void CloseLoseUI(bool bDouble = false)
	{
		StartCoroutine(CallCloseUI(bDouble));
	}

	public override void OnClose()
	{
		base.OnClose();
		UnityEngine.Debug.Log("OnClose       1111111111111111111");
		if (BaseUIAnimation.bClickButton)
		{
			UnityEngine.Debug.Log("OnClose       222222222222222222");
			BaseUIAnimation.action.ClickButton(detail.Close_Button.gameObject);
			UnityEngine.Debug.Log("OnClose       33333333333333333");
			StartCoroutine(CallCloseUI());
		}
	}

	public override void OnNextBtn2()
	{
		UnityEngine.Debug.Log("OnNextBtn       0000000000000000000");
		base.OnNextBtn2();
		UnityEngine.Debug.Log("OnNextBtn       1111111111111111111");
		if (BaseUIAnimation.bClickButton)
		{
			UnityEngine.Debug.Log("OnNextBtn       222222222222222222222");
			BaseUIAnimation.action.ClickButton(detail.NextBtn2_Button.gameObject);
			UnityEngine.Debug.Log("OnNextBtn       33333333333333333");
			StartCoroutine(CallClickNext());
		}
	}

	private IEnumerator CallClickNext(bool bDouble = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		UnityEngine.Debug.Log("OnNextBtn       444444444444444444444");
		CloseLoseUI();
	}

	private IEnumerator CallCloseUI(bool bDouble = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		GameGuide.Instance.isCanShoot = true;
		if (Singleton<DataManager>.Instance.bopenMaxGuide)
		{
			GameGuide.Instance.nextGuide();
			Singleton<DataManager>.Instance.bopenMaxGuide = false;
		}
		UI.Instance.ClosePanel();
	}
}
