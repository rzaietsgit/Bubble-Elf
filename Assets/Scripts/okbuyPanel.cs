public class okbuyPanel : okbuyPanelBase
{
	public static okbuyPanel panel;

	public bool pay;

	public bool buy;

	public override void InitUI()
	{
		panel = this;
		BaseUIAnimation.action.SetLanguageFont("shoprefresh1", detail.BuyBubbleTtitle_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("shoprefresh2", detail.DaojuRemark_Text, string.Empty);
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		detail.GoldText_Text.text = @int.ToString();
	}

	public override void OnExit()
	{
		if (pay)
		{
			ChinaShopPanel.panel.Clickzuanshiobj();
		}
		if (buy)
		{
			aliyunlog.GameUseLog("diamond", 25, "refresh", 1);
			PayManager.action.BuyDaoju(25);
			ChinaShopPanel.panel.LoadDataShopUI();
			ChinaShopPanel.panel.Create9Shop(bmoney: true);
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_ResDaoju" + Util.GetNowTime_Day());
			@int++;
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_ResDaoju" + Util.GetNowTime_Day(), @int);
			if ((bool)MapUI.action)
			{
				MapUI.action.LoadGold(bUpdate: false);
			}
		}
	}

	public override void OnPayBubble()
	{
		pay = false;
		buy = false;
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (25 > @int)
		{
			pay = true;
		}
		else
		{
			buy = true;
		}
		OnCloseButton();
	}
}
