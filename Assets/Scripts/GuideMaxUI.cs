using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GuideMaxUI : BaseUI
{
	public static GuideMaxUI action;

	public GameObject CloseBtn;

	public GameObject NextBtn;

	public GameObject GuideImageObj;

	public Text GuideMaxRemark;

	public Text GuideMaxTitle;

	public Text GuideMaxNext;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.GuideMaxUI;
	}

	public override void OnStart()
	{
		action = this;
	}

	private void Update()
	{
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

	public void InitUI()
	{
	}

	public void LoadType(int iIndex)
	{
		GuideImageObj.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/guide/guide_bubble_level" + iIndex, 608, 308);
	}

	public void CloseLoseUI(bool bDouble = false)
	{
		StartCoroutine(CallCloseUI(bDouble));
	}

	public void _CloseLoseUI()
	{
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
			StartCoroutine(CallCloseUI());
		}
	}

	public void ClickNext()
	{
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(NextBtn.gameObject);
			StartCoroutine(CallClickNext());
		}
	}

	private IEnumerator CallClickNext(bool bDouble = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseLoseUI();
	}

	private IEnumerator CallCloseUI(bool bDouble = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		GameGuide.Instance.isCanShoot = true;
		CloseUI(bDouble);
	}

	protected override void OnAwake()
	{
		base.OnAwake();
	}

	protected override void OnRelease()
	{
		base.OnRelease();
	}
}
