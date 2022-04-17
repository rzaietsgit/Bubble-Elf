
using UnityEngine;

public class BuyGangUIPanel : BuyGangUIPanelBase
{
	public static BuyGangUIPanel panel;

	public int iGangType = 1;

	private int iGangprice = 25;

	public Sprite[] LGangSprite;

	public Sprite[] LGangSprite2;

	public override void InitUI()
	{
		panel = this;
		iGangType = GameUI.action.iGangType;
		if (iGangType == 3)
		{
			iGangType = 4;
		}
		else if (iGangType == 4)
		{
			iGangType = 3;
		}
		else if (iGangType == 5)
		{
			iGangType = 4;
		}

		LoadGold();
		LoadGangType();
		BaseUIAnimation.action.SetLanguageFont("BuyGangRemark" + iGangType, detail.BuyGangRemark_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BuyGangTitle", detail.BuyGangTitle_Text, string.Empty);
		int num = iGangprice = int.Parse(Singleton<DataManager>.Instance.dDataSystemConfig["Gang" + iGangType]["V"]);
		InitAndroid.action.checkshowBuyButton();
		detail.Btntext_Text.text = num.ToString();
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		detail.moneytext_Text.text = @int.ToString();
	}

	public override void OnResumeBase()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		detail.moneytext_Text.text = @int.ToString();
	}

	public void ShowAdTip()
	{
	}

	public override void OnPauseBase()
	{
	}

	public override void OnResume()
	{
	}

	public void LoadGold()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		detail.GoldText_Text.text = @int.ToString();
	}

	private void LoadGangType()
	{
		detail.GamgIcon1_Image.sprite = LGangSprite[iGangType - 1];
		detail.GamgIcon2_Image.sprite = LGangSprite2[iGangType - 1];
	}

	public override void OnPayGangBtn()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (iGangprice > @int)
		{
			Singleton<DataManager>.Instance.ChinaShopOpendaoju = false;
			Singleton<DataManager>.Instance.ChinaShopOpenZuanshi = true;
			UI.Instance.OpenPanel(UIPanelType.ChinaShop);
			return;
		}
		PayManager.action.ExpendGDP(iGangprice, GDPType.BUGDAOJU, 1);
		BubbleSpawner.Instance.buyskillGang.GetComponent<MuTong>().AddSkill();
		if ((bool)panel)
		{
			panel.OnCloseButton();
		}
	}
}
