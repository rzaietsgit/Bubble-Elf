using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class BuyDaojuUI : BaseUI
{
	public static BuyDaojuUI action;

	public GameObject PayBtn;

	public GameObject CloseBtn;

	public Text GoldText;

	public Text Ttitle;

	public Text Remark;

	public Text BuyNumberText;

	public Text BuyNumberPriceText;

	public GameObject IconObj;

	public Text IconObjNumber;

	public Image typeicon1;

	public Image typeicon2;

	public Sprite GbIconImg;

	public GameObject bag_icon_timelimit;

	public GameObject bag_icon_timelimitRemark;

	private bool bPay;

	public Text BuyDaojuUI1;

	public Text BuyDaojuUI2;

	public Image xianshiImg;

	public Sprite EnSp;

	private int iBubblePrice;

	private string _iType = string.Empty;

	private int iconID;

	private int money;

	private int number;

	private string umeng = string.Empty;

	private int iMyGold;

	private int iNumber = 1;

	private bool ChinaShopbGBBuy;

	private int BuyMoney;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.BuyDaojuUI;
	}

	public void CloseBuyDaojuUI(bool bDouble = false, bool bExit = true)
	{
		StartCoroutine(CallCloseUI(bDouble, bExit));
	}

	public void LoadGold()
	{
		if (ChinaShopbGBBuy)
		{
			iMyGold = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GB");
		}
		else
		{
			iMyGold = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		}
		GoldText.text = iMyGold.ToString();
	}

	public void _CloseBuyDaojuUI()
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
		Singleton<UIManager>.Instance.OtherOpenUI = EnumUIType.ChinaShopUI;
		Singleton<DataManager>.Instance.ChinaShopOpendaoju = true;
		Singleton<DataManager>.Instance.ChinaShopOpenZuanshi = false;
		CloseUI(bDouble: false, bOpenOther: true);
	}

	private void Update()
	{
	}

	public override void OnStart()
	{
		action = this;
		LoadDaoju();
		LoadGold();
		BaseUIAnimation.action.SetLanguageFont("BuyDaojuUI1", BuyDaojuUI1, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BuyDaojuUI2", BuyDaojuUI2, string.Empty);
		if (InitGame.bEnios)
		{
			xianshiImg.sprite = EnSp;
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

	public void PayDaoju()
	{
		if (bPay)
		{
			return;
		}
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_BuyDaojuRecording" + _iType);
		if (@int == 1)
		{
			return;
		}
		if (BuyMoney > iMyGold)
		{
			if (!ChinaShopbGBBuy)
			{
				CloseUI();
				ChinaShopUI.action.Clickzuanshiobj();
			}
			return;
		}
		bPay = true;
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_BuyDaojuRecording" + _iType, 1);
		if ((bool)ChinaShopUI.action)
		{
			ChinaShopUI.action.buyResUI();
		}
		money = BuyMoney;
		number *= iNumber;
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_coin");
		}
		//Analytics.Event(umeng);
		if (ChinaShopbGBBuy)
		{
			PayManager.action.UseGB(money);
		}
		else
		{
			PayManager.action.BuyDaoju(money);
		}
		GameObject gameObject = null;
		gameObject = ((!MapUI.action) ? GameUI.action.gameObject : MapUI.action.gameObject);
		ChinaPay.action.addRewardAll(iconID, number, gameObject, isShow: true, "buy", "daoju");
		Singleton<UIManager>.Instance.OtherOpenUI = EnumUIType.ChinaShopUI;
		Singleton<DataManager>.Instance.ChinaShopOpendaoju = true;
		Singleton<DataManager>.Instance.ChinaShopOpenZuanshi = false;
		Singleton<DataManager>.Instance.ChinaShopOpen = false;
		CloseUI(bDouble: false, bOpenOther: true);
	}

	public void LoadDaoju()
	{
		_iType = Singleton<DataManager>.Instance.BuyDaojuID;
		ChinaShopbGBBuy = Singleton<DataManager>.Instance.ChinaShopbGBBuy;
		iconID = int.Parse(Singleton<DataManager>.Instance.dDataBuyDaojuList[_iType]["iconID"]);
		money = int.Parse(Singleton<DataManager>.Instance.dDataBuyDaojuList[_iType]["money"]);
		number = int.Parse(Singleton<DataManager>.Instance.dDataBuyDaojuList[_iType]["number"]);
		umeng = Singleton<DataManager>.Instance.dDataBuyDaojuList[_iType]["umeng"];
		iMyGold = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (ChinaShopbGBBuy)
		{
			money = int.Parse(Singleton<DataManager>.Instance.dDataBuyDaojuList[_iType.ToString()]["gold"]);
			typeicon1.sprite = GbIconImg;
			typeicon2.sprite = GbIconImg;
			iMyGold = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GB");
		}
		string text = Singleton<DataManager>.Instance.dDataBuyDaojuList[_iType]["title"];
		string text2 = Singleton<DataManager>.Instance.dDataBuyDaojuList[_iType]["remark"];
		if (InitGame.bEnios)
		{
			text = Singleton<DataManager>.Instance.dDataBuyDaojuList[_iType]["titleen"];
			text2 = Singleton<DataManager>.Instance.dDataBuyDaojuList[_iType]["remarken"];
		}
		Ttitle.text = text;
		Remark.text = text2;
		IconObj.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + iconID, 138, 114);
		BuyNumberPriceText.text = money.ToString();
		iNumber = 1;
		BuyNumberText.text = iNumber.ToString();
		IconObjNumber.text = "x" + number;
		BuyMoney = money;
		if (iconID >= 4 && iconID <= 9 && number == 100)
		{
			IconObjNumber.gameObject.SetActive(value: false);
			bag_icon_timelimit.SetActive(value: true);
			bag_icon_timelimitRemark.SetActive(value: true);
		}
	}

	public void ClickAdd()
	{
		if (BuyMoney + money <= iMyGold)
		{
			BuyMoney += money;
			BuyNumberPriceText.text = BuyMoney.ToString();
			iNumber++;
			BuyNumberText.text = iNumber.ToString();
		}
	}

	public void ClickCut()
	{
		if (iNumber >= 2)
		{
			iNumber--;
			BuyMoney -= money;
			BuyNumberText.text = iNumber.ToString();
			BuyNumberPriceText.text = BuyMoney.ToString();
		}
	}
}
