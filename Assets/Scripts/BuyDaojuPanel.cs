using UnityEngine;
using UnityEngine.UI;

public class BuyDaojuPanel : BuyDaojuPanelBase
{
	public static BuyDaojuPanel panel;

	public Sprite GbIconImg;

	private string _iType = string.Empty;

	private int iconID;

	private int money;

	private int number;

	private string umeng = string.Empty;

	private int iMyGold;

	private int iNumber = 1;

	private bool ChinaShopbGBBuy;

	private int BuyMoney;

	private bool bPay;

	public override void InitUI()
	{
		panel = this;
		LoadGold();
		LoadDaoju();
		BaseUIAnimation.action.SetLanguageFont("BuyDaojuUI1", detail.BuyBubbleTtitle_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BuyDaojuUI2", detail.bag_icon_timelimitRemark_Text, string.Empty);
	}

	public void LoadDaoju()
	{
		_iType = Singleton<DataManager>.Instance.BuyDaojuID;
		ChinaShopbGBBuy = Singleton<DataManager>.Instance.ChinaShopbGBBuy;
		iconID = int.Parse(Singleton<DataManager>.Instance.dDataBuyDaojuList[_iType]["iconID"]);
		int num = int.Parse(Singleton<DataManager>.Instance.dDataBuyDaojuList[_iType]["itype"]);
		money = int.Parse(Singleton<DataManager>.Instance.dDataBuyDaojuList[_iType]["money"]);
		number = int.Parse(Singleton<DataManager>.Instance.dDataBuyDaojuList[_iType]["number"]);
		umeng = Singleton<DataManager>.Instance.dDataBuyDaojuList[_iType]["umeng"];
		iMyGold = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (ChinaShopbGBBuy)
		{
			money = int.Parse(Singleton<DataManager>.Instance.dDataBuyDaojuList[_iType.ToString()]["gold"]);
			detail.typeicon_Image.sprite = GbIconImg;
			detail.typeicon1_Image.sprite = GbIconImg;
			iMyGold = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GB");
		}
		BaseUIAnimation.action.SetLanguageFont("ShopDaojuInfo" + num, detail.Daojutitle_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("ShopDaojuInfo" + num + "_1", detail.DaojuRemark_Text, string.Empty);
		detail.Icon_Image.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + iconID, 138, 114);
		detail.GoldNumber_Text.text = money.ToString();
		iNumber = 1;
		detail.SkillCount_Text.text = "x" + number;
		BuyMoney = money;
	}

	public void LoadGold()
	{
		ChinaShopbGBBuy = Singleton<DataManager>.Instance.ChinaShopbGBBuy;
		if (ChinaShopbGBBuy)
		{
			iMyGold = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GB");
		}
		else
		{
			iMyGold = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		}
		detail.GoldText_Text.text = iMyGold.ToString();
	}

	public override void OnPayBubble()
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
				UI.Instance.ClosePanel();
				if ((bool)ChinaShopPanel.panel)
				{
					ChinaShopPanel.panel.Clickzuanshiobj();
				}
			}
			return;
		}
		bPay = true;
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_BuyDaojuRecording" + _iType, 1);
		if ((bool)ChinaShopPanel.panel)
		{
			ChinaShopPanel.panel.buyResUI();
		}
		money = BuyMoney;
		number *= iNumber;
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_coin");
		}
		if (ChinaShopbGBBuy)
		{
			PayManager.action.UseGB(money);
		}
		else
		{
			PayManager.action.BuyDaoju(money);
		}
		if (ChinaShopbGBBuy)
		{
			aliyunlog.GameUseLog("gold", BuyMoney, "shopicon" + iconID, number);
		}
		else
		{
			aliyunlog.GameUseLog("diamond", BuyMoney, "shopicon" + iconID, number);
		}
		GameObject gameObject = null;
		gameObject = ((!MapUI.action) ? GameUI.action.gameObject : MapUI.action.gameObject);
		ChinaPay.action.addRewardAll(iconID, number, gameObject, isShow: true, "buy", "daoju");
		UI.Instance.ClosePanel();
	}
}
