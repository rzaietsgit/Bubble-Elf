using UnityEngine;

public class BuySkillUIChinaPanel : BuySkillUIChinaPanelBase
{
	public static BuySkillUIChinaPanel panel;

	private int iSkillType;

	public Sprite[] LSkillIcon;

	public override void InitUI()
	{
		panel = this;
		iSkillType = DataManager.iSkillOpenType;
		if ((bool)GameUI.action)
		{
			if (iSkillType == 1)
			{
				iSkillType = 4;
			}
			else if (iSkillType == 2)
			{
				iSkillType = 5;
			}
			else if (iSkillType == 3)
			{
				iSkillType = 6;
			}
		}
		float num = float.Parse(Singleton<DataManager>.Instance.dDataChinaPay["GameSkill" + iSkillType]["iMoney"]);
		if (Singleton<DataManager>.Instance.bChinaIos)
		{
			num = float.Parse(Singleton<DataManager>.Instance.dDataChinaPay["GameSkill" + iSkillType]["iMoneyios"]);
			if (InitGame.bEnios)
			{
				num = float.Parse(Singleton<DataManager>.Instance.dDataChinaPay["GameSkill" + iSkillType]["iMoneyiosen"]);
			}
		}
		BaseUIAnimation.action.SetLanguageFont("BuySkillRemark" + iSkillType, detail.BuySkillRemark1_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BuySkillUIChina1", detail.BuySkillTitle_Text, string.Empty);
		detail.Image1_Image.sprite = LSkillIcon[iSkillType - 4];
		int num2 = int.Parse(Singleton<DataManager>.Instance.dDataSystemConfig["GameSkill" + iSkillType]["V"]);
		detail.Btntext_Text.text = num2.ToString();
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		detail.moneytext_Text.text = @int.ToString();
	}

	public override void OnResumeBase()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		detail.moneytext_Text.text = @int.ToString();
	}

	public override void OnPauseBase()
	{
	}

	public override void OnResume()
	{
	}

	public void ShowAdTip()
	{
	}

	public override void OnButton()
	{
		int num = int.Parse(Singleton<DataManager>.Instance.dDataSystemConfig["GameSkill" + iSkillType]["V"]);
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (num > @int)
		{
			Singleton<DataManager>.Instance.ChinaShopOpendaoju = false;
			Singleton<DataManager>.Instance.ChinaShopOpenZuanshi = true;
			UI.Instance.OpenPanel(UIPanelType.ChinaShop);
			return;
		}
		PayManager.action.ExpendGDP(num, GDPType.BUGDAOJU, 1);
		ChinaPay.action.addRewardAll(iSkillType + 3, 1, null, isShow: false);
		BaseUIAnimation.action.ShowProp(iSkillType + 3, 1, null);
		PayManager.action.LoadSkill(100);
		if ((bool)panel)
		{
			panel.OnCloseButton();
		}
	}
}
