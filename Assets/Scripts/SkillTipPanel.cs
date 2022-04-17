using UnityEngine;

public class SkillTipPanel : SkillTipPanelBase
{
	public static SkillTipPanel panel;

	public Sprite[] LSkillSprite;

	public override void InitUI()
	{
		panel = this;
		BaseUIAnimation.action.SetLanguageFont("SkillTipUITitle", detail.SkillTipUITitle_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SkillTipUIOKBtn", detail.SkillTipUIOkBtn_Text, string.Empty);
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 13)
		{
			BaseUIAnimation.action.SetLanguageFont("SkillTipUIRemark1", detail.SkillTipUIRemark_Text, string.Empty);
			detail.Icon_Image.sprite = LSkillSprite[0];
		}
		else if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 17)
		{
			BaseUIAnimation.action.SetLanguageFont("SkillTipUIRemark2", detail.SkillTipUIRemark_Text, string.Empty);
			detail.Icon_Image.sprite = LSkillSprite[1];
		}
		else if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 27)
		{
			BaseUIAnimation.action.SetLanguageFont("SkillTipUIRemark3", detail.SkillTipUIRemark_Text, string.Empty);
			detail.Icon_Image.sprite = LSkillSprite[2];
		}
	}

	public override void OnExit()
	{
		Reward(show: false);
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
