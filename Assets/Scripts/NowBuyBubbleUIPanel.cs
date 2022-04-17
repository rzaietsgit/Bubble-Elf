using EasyMobile;
using ITSoft;
using UnityEngine;

public class NowBuyBubbleUIPanel : NowBuyBubbleUIPanelBase
{
	public static NowBuyBubbleUIPanel panel;

	public int iBubblePrice;

	public bool isBuy;

	public void LoadGold()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		detail.mygold_Text.text = @int.ToString();
	}

	private void Update()
	{
		if (detail != null && detail.mygold_Text != null)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
			detail.mygold_Text.text = @int.ToString();
		}
	}

	public override void InitUI()
	{
		panel = this;
		aliyunlog.LevelLog("nomove");
		LoadGold();
		BaseUIAnimation.action.SetLanguageFont("BuyBubbleTtitle", detail.BuyBubbleTtitle_Text, string.Empty);
		iBubblePrice = int.Parse(Singleton<DataManager>.Instance.dDataSystemConfig["BuyBubble1"]["V"]);

		//InitAndroid.action.GAEvent("NewclickAdd10Lv:" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
		if (InitGame.bChinaVersion)
		{
			float num = float.Parse(Singleton<DataManager>.Instance.dDataChinaPay["BuyBubble2"]["iMoney"]);
			if (Singleton<DataManager>.Instance.bChinaIos)
			{
				num = float.Parse(Singleton<DataManager>.Instance.dDataChinaPay["BuyBubble2"]["iMoneyios"]);
				if (InitGame.bEnios)
				{
					num = float.Parse(Singleton<DataManager>.Instance.dDataChinaPay["BuyBubble2"]["iMoneyiosen"]);
				}
			}
			detail.GoldNumberm_Text.text = iBubblePrice.ToString();
			detail.GoldNumberm_Text.gameObject.SetActive(value: true);
		}
		InitAndroid.action.checkshowBuyButton();
		BaseUIAnimation.action.SetLanguageFont("BuybubbleText1", detail.BuyBubbleRemark123_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BuybubbleText2", detail.GoldNumber1233_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BuyBubbleRemark5", detail.BuyBubbleRemark_Text, string.Empty);
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "NowBuyBubbleUIPanel" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
		if (GameUI.action.adbuy)
		{
			detail.PayBubblead_Button.gameObject.SetActive(value: true);
			detail.PayBubble_Button.gameObject.SetActive(value: true);
		}
		else
		{
			detail.PayBubblead_Button.gameObject.SetActive(value: false);
			detail.PayBubble_Button.gameObject.SetActive(value: true);
		}
		if (GameUI.action.adbuy && Application.platform == RuntimePlatform.WindowsEditor)
		{
			detail.PayBubblead_Button.gameObject.SetActive(value: true);
			detail.PayBubble_Button.gameObject.SetActive(value: true);
		}
	}

	public void ShowYunbuAdVidoe()
	{
	}

	public void ShowAdTip()
	{
	}

	public void BuyOk()
	{
		isBuy = true;
	}

	public override void OnPayBubble()
	{
		PayBubbleClick();
	}

	public void PayBubbleClick()
	{
		//Analytics.Event("ClickBuyBubble11");
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (iBubblePrice > @int)
		{
			isBuy = true;
			Singleton<DataManager>.Instance.ChinaShopOpendaoju = false;
			Singleton<DataManager>.Instance.ChinaShopOpen = false;
			Singleton<DataManager>.Instance.ChinaShopOpenZuanshi = true;
			UI.Instance.OpenPanel(UIPanelType.ChinaShop);
			return;
		}
		isBuy = true;
		//Analytics.Event("PayBuyBubble1");
		PayManager.action.BuyBubble(iBubblePrice, 5);
		if (Singleton<LevelManager>.Instance.iBubbleCount == 0)
		{
			//Analytics.Event("PayBuyBubble2");
			Singleton<LevelManager>.Instance.iBubbleCount = 5;
			Singleton<LevelManager>.Instance.iBubbleUseCount = 0;
			PassLevel.action.KillLiuhan();
			GameUI.action.LoadBubbleCount();
			BubbleSpawner.Instance.ready_1 = null;
			BubbleSpawner.Instance.ready_2 = null;
			BubbleSpawner.Instance.initReadyBubble();
			panel.BuyOk();
			panel.OnCloseButton();
			GameUI.action.buyBuShuCount++;
			FirebaseController.StoneAddFiveStep(GameUI.action.buyBuShuCount);
		}
		else
		{
			//Analytics.Event("PayBuyBubble1");
			Singleton<LevelManager>.Instance.iBubbleCount = Singleton<LevelManager>.Instance.iBubbleCount + 5;
			Singleton<LevelManager>.Instance.iBubbleUseCount = 0;
			PassLevel.action.KillLiuhan();
			GameUI.action.LoadBubbleCount();
			BubbleSpawner.Instance.initReadyBubble(isusekey: false);
			GameUI.action.KillNowBuyBubble();
			panel.BuyOk();
			panel.OnCloseButton();
			GameUI.action.buyBuShuCount++;
			FirebaseController.StoneAddFiveStep(GameUI.action.buyBuShuCount);
		}
	}

	public void AdReward()
	{
		UnityEngine.Debug.Log("AdReward");
		isBuy = true;
		GameUI.action.adbuy = false;
		if (Singleton<LevelManager>.Instance.iBubbleCount == 0)
		{
			FirebaseController.VideoAddFiveStep();
			//Analytics.Event("PayBuyBubble2");
			Singleton<LevelManager>.Instance.iBubbleCount = 5;
			Singleton<LevelManager>.Instance.iBubbleUseCount = 0;
			PassLevel.action.KillLiuhan();
			GameUI.action.LoadBubbleCount();
			BubbleSpawner.Instance.ready_1 = null;
			BubbleSpawner.Instance.ready_2 = null;
			BubbleSpawner.Instance.initReadyBubble();
			panel.BuyOk();
			panel.OnCloseButton();
		}
		else
		{
			//Analytics.Event("PayBuyBubble1");
			FirebaseController.VideoAddFiveStep();
			Singleton<LevelManager>.Instance.iBubbleCount = Singleton<LevelManager>.Instance.iBubbleCount + 5;
			Singleton<LevelManager>.Instance.iBubbleUseCount = 0;
			PassLevel.action.KillLiuhan();
			GameUI.action.LoadBubbleCount();
			BubbleSpawner.Instance.initReadyBubble(isusekey: false);
			GameUI.action.KillNowBuyBubble();
			panel.BuyOk();
			panel.OnCloseButton();
		}
	}

	public override void OnPauseBase()
	{
	}

	public override void OnResumeBase()
	{
		isBuy = false;
	}

	public override void OnResume()
	{
		isBuy = false;
	}

	public override void OnExit()
	{
		if (Singleton<LevelManager>.Instance.iBubbleCount <= 0 && !isBuy)
		{
			UI.Instance.OpenPanel(UIPanelType.TipFail);
		}
	}
    private int indexads;
	public override void OnPayBubblead()
	{
        //if (Application.platform == RuntimePlatform.WindowsEditor)
        //{
            //GameUI.action.buyBuyBuShu = 2;
            //AdReward();
        //}
        //else
        //{
            if (AdsManager.RewardIsReady())
            {
                indexads = 1;
                AdsManager.ShowRewarded();
            }
        //}
	}

    void OnEnable()
    {
        AdsManager.OnCompleteRewardVideo += RewardedAdCompletedHandler;
    }
    // The event handler
    void RewardedAdCompletedHandler()
    {
        if(indexads == 1)
        {
            indexads = 0;
            GameUI.action.buyBuyBuShu = 2;
            AdReward();
        }
    }
    // Unsubscribe
    void OnDisable()
    {
	    AdsManager.OnCompleteRewardVideo -= RewardedAdCompletedHandler;
    }
}
