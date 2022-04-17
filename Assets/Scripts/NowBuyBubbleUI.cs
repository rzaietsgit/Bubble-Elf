using System.Collections;
using EasyMobile;
using UnityEngine;
using UnityEngine.UI;

public class NowBuyBubbleUI : BaseUI
{
	public static NowBuyBubbleUI action;

	public GameObject PayBtn;

	public GameObject CloseBtn;

	public Text GoldText;

	public Text BuyBubbleTtitle;

	public Text BuyBubbleRemark;

	public Text GoldNumber;

	public Text ChinaMoneyText;

	public Text YunbuText;

	private int iBubblePrice;

	public void ChangeText()
	{
		YunbuText.text = "立 即 领 取";
	}

	public override EnumUIType GetUIType()
	{
		return EnumUIType.NowBuyBubbleUI;
	}

	public void CloseBuyBubbleUI(bool bDouble = false, bool bExit = true)
	{
		StartCoroutine(CallCloseUI(bDouble, bExit));
	}

	public void LoadGold()
	{
	}

	public void _CloseBuyBubbleUI()
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
		if (Singleton<LevelManager>.Instance.iBubbleCount > 0)
		{
			CloseUI();
		}
		else
		{
			CloseUI(bDouble);
		}
	}

	private void Update()
	{
	}

	public override void OnStart()
	{
		action = this;
		LoadGold();
		BaseUIAnimation.action.CreateButton(PayBtn.gameObject);
		BaseUIAnimation.action.SetLanguageFont("BuyBubbleTtitle", BuyBubbleTtitle, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BuyBubbleRemark", BuyBubbleRemark, string.Empty);
		iBubblePrice = int.Parse(Singleton<DataManager>.Instance.dDataSystemConfig["BuyBubble1"]["V"]);
		//Analytics.Event("ClickBuyBubble1");
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
			string text = Singleton<DataManager>.Instance.dDataLanguage["NowBuyBubbleUIPay"][BaseUIAnimation.Language];
			text = text.Replace("A1", num.ToString());
			ChinaMoneyText.text = text;
			ChinaMoneyText.gameObject.SetActive(value: true);
		}
		InitAndroid.action.checkshowBuyButton();
		if (InitGame.bEnios)
		{
			YunbuText.text = "Pay";
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

	public void PayBubble()
	{
		//Analytics.Event("ClickBuyBubble11");
		if (InitGame.bChinaVersion)
		{
            IAPManager.Purchase(EM_IAPConstants.Product_buybubble1);
            //InitAndroid.action.doChainePay("BuyBubble1");
			return;
		}
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (iBubblePrice > @int)
		{
			EnumUIType[] uiTypes = new EnumUIType[2]
			{
				EnumUIType.BuyGoldUI,
				EnumUIType.NowBuyBubbleUI
			};
			Singleton<UIManager>.Instance.OpenUI(uiTypes);
			CloseBuyBubbleUI(bDouble: true, bExit: false);
			return;
		}
		//Analytics.Event("PayBuyBubble1");
		PayManager.action.BuyBubble(iBubblePrice, 10);
		Singleton<LevelManager>.Instance.iBubbleCount = Singleton<LevelManager>.Instance.iBubbleCount + 10;
		Singleton<LevelManager>.Instance.iBubbleUseCount = 0;
		PassLevel.action.KillLiuhan();
		GameUI.action.LoadBubbleCount();
		BubbleSpawner.Instance.initReadyBubble(isusekey: false);
		GameUI.action.KillNowBuyBubble();
		CloseBuyBubbleUI(bDouble: false, bExit: false);
		GameUI.action.StopSkillAniRandmo();
	}
}
