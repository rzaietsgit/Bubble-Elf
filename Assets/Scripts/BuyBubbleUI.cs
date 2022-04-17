using System.Collections;
using EasyMobile;
using UnityEngine;
using UnityEngine.UI;

public class BuyBubbleUI : BaseUI
{
	public static BuyBubbleUI action;

	public GameObject PayBtn;

	public GameObject CloseBtn;

	public Text GoldText;

	public Text BuyBubbleTtitle;

	public Text BuyBubbleRemark;

	public Text GoldNumber;

	public GameObject VideoBuy1;

	public GameObject VideoBuy2;

	public Text ChinaMoneyText;

	public Text YunbuText;

	private int iBubblePrice;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.BuyBubbleUI;
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
		if (bExit)
		{
			UI.Instance.OpenPanel(UIPanelType.TipFail);
			CloseUI(bDouble, bOpenOther: true);
		}
		else
		{
			CloseUI(bDouble);
		}
	}

	private void Update()
	{
	}

	public void ChangeText()
	{
		YunbuText.text = "立 即 领 取";
	}

	public override void OnStart()
	{
		UnityEngine.Debug.Log("BuyBubbleUI>>>");
		action = this;
		LoadGold();
		BaseUIAnimation.action.CreateButton(PayBtn.gameObject);
		BaseUIAnimation.action.SetLanguageFont("BuyBubbleTtitle", BuyBubbleTtitle, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BuyBubbleRemark", BuyBubbleRemark, string.Empty);
		iBubblePrice = int.Parse(Singleton<DataManager>.Instance.dDataSystemConfig["BuyBubble2"]["V"]);
		//Analytics.Event("ClickBuyBubble2");
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
		//Analytics.Event("ClickBuyBubble22");
		if (InitGame.bChinaVersion)
		{
			//InitAndroid.action.doChainePay("BuyBubble2");
            IAPManager.Purchase(EM_IAPConstants.Product_buybubble2);
            return;
		}
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (iBubblePrice > @int)
		{
			EnumUIType[] uiTypes = new EnumUIType[2]
			{
				EnumUIType.BuyGoldUI,
				EnumUIType.BuyBubbleUI
			};
			Singleton<UIManager>.Instance.OpenUI(uiTypes);
			CloseBuyBubbleUI(bDouble: true, bExit: false);
			return;
		}
		//Analytics.Event("PayBuyBubble2");
		if (InitGame.bChinaVersion)
		{
			iBubblePrice = 0;
		}
		PayManager.action.BuyBubble(iBubblePrice, 10);
		Singleton<LevelManager>.Instance.iBubbleCount = 10;
		Singleton<LevelManager>.Instance.iBubbleUseCount = 0;
		PassLevel.action.KillLiuhan();
		GameUI.action.LoadBubbleCount();
		BubbleSpawner.Instance.ready_1 = null;
		BubbleSpawner.Instance.ready_2 = null;
		BubbleSpawner.Instance.initReadyBubble();
		CloseBuyBubbleUI(bDouble: false, bExit: false);
	}

	public void PayBubbleChina()
	{
		//Analytics.Event("PayBuyBubble2");
		if (InitGame.bChinaVersion)
		{
			iBubblePrice = 0;
		}
		PayManager.action.BuyBubble(iBubblePrice, 10);
		Singleton<LevelManager>.Instance.iBubbleCount = 10;
		Singleton<LevelManager>.Instance.iBubbleUseCount = 0;
		PassLevel.action.KillLiuhan();
		GameUI.action.LoadBubbleCount();
		BubbleSpawner.Instance.ready_1 = null;
		BubbleSpawner.Instance.ready_2 = null;
		BubbleSpawner.Instance.initReadyBubble();
		CloseBuyBubbleUI(bDouble: false, bExit: false);
	}
}
