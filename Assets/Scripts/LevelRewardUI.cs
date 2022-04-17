using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelRewardUI : BaseUI
{
	public static LevelRewardUI action;

	public GameObject CloseBtn;

	public Image SkillIcon;

	public Text CountText;

	public GameObject H2Icon;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.LevelRewardUI;
	}

	private void Start()
	{
		action = this;
		SkillIcon.sprite = Util.GetResourcesSprite("Img/SigninUI/Max_signin_icon_" + Singleton<DataManager>.Instance.iLevelRewardID, 208, 184);
		CountText.text = "X" + Singleton<DataManager>.Instance.iLevelRewardCount.ToString();
		H2Icon.SetActive(value: false);
	}

	public void _CloseLevelRewardUI()
	{
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
			StartCoroutine(CallCloseUI());
		}
	}

	private IEnumerator CallCloseUI(bool bDouble = false, bool bExit = true)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseReward();
		CloseUI(bDouble);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject gameObject = Util.TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject == null)
			{
				CloseReward();
				CloseUI();
			}
			else if (gameObject.name.LastIndexOf("LevelRewardUI") < 0)
			{
				CloseReward();
				CloseUI();
			}
		}
	}

	public void ClickRewardBtn()
	{
		CloseReward();
		CloseUI();
	}

	private void CloseReward()
	{
		if ((bool)BtnManager.action && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LevelScore_" + (Singleton<DataManager>.Instance.iLevelRewardLevelID - 1)) != 0)
		{
			ChinaPay.action.addRewardAll(Singleton<DataManager>.Instance.iLevelRewardID, Singleton<DataManager>.Instance.iLevelRewardCount, MapUI.action.gameObject);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_LevelReward_" + Singleton<DataManager>.Instance.iLevelRewardLevelID, 1);
			BtnManager.action.ResNowBtnReward();
		}
	}
}
