using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillTipUI : BaseUI
{
	public static SkillTipUI action;

	public GameObject EnterBtn;

	public GameObject CloseBtn;

	public GameObject IconObj;

	public Sprite[] LSkillSprite;

	public Text SkillTipUITitle;

	public Text SkillTipUIRemark;

	public Text SkillTipUIOKBtn;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.SkillTipUI;
	}

	public void CloseSkillTipUI(bool bDouble = false)
	{
		StartCoroutine(CallCloseUI(bDouble));
	}

	public void _CloseSkillTipUI()
	{
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
			StartCoroutine(CallCloseUI());
		}
	}

	private IEnumerator CallCloseUI(bool bDouble = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		Reward(show: false);
		CloseUI(bDouble);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject gameObject = Util.TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject == null)
			{
				Reward(show: false);
				CloseUI();
			}
			else if (gameObject.name.LastIndexOf("SkillTipUI") < 0)
			{
				Reward(show: false);
				CloseUI();
			}
		}
	}

	public override void OnStart()
	{
		action = this;
		BaseUIAnimation.action.SetLanguageFont("SkillTipUITitle", SkillTipUITitle, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SkillTipUIOKBtn", SkillTipUIOKBtn, string.Empty);
		BaseUIAnimation.action.CreateButton(EnterBtn.gameObject);
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 13)
		{
			BaseUIAnimation.action.SetLanguageFont("SkillTipUIRemark1", SkillTipUIRemark, string.Empty);
			IconObj.GetComponent<Image>().sprite = LSkillSprite[0];
		}
		else if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 17)
		{
			BaseUIAnimation.action.SetLanguageFont("SkillTipUIRemark2", SkillTipUIRemark, string.Empty);
			IconObj.GetComponent<Image>().sprite = LSkillSprite[1];
		}
		else if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 27)
		{
			BaseUIAnimation.action.SetLanguageFont("SkillTipUIRemark3", SkillTipUIRemark, string.Empty);
			IconObj.GetComponent<Image>().sprite = LSkillSprite[2];
		}
	}

	protected override void OnAwake()
	{
		base.OnAwake();
	}

	protected override void OnRelease()
	{
		base.OnRelease();
	}

	public void ClickEnterBtn()
	{
		Reward(show: true);
		CloseUI();
	}

	private void CloseReward()
	{
		Reward(show: false);
		CloseUI();
	}

	private void Reward(bool show)
	{
		int num = 0;
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 13)
		{
			num = 4;
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 17)
		{
			num = 5;
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 27)
		{
			num = 6;
		}
		if (num > 0)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iMaxGuideReward_" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_iMaxGuideReward_" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex, 1);
			if (@int == 0)
			{
				ChinaPay.action.addRewardAll(num, 1, GameUI.action.gameObject, isShow: false, "free", "skillfree");
			}
		}
	}
}
