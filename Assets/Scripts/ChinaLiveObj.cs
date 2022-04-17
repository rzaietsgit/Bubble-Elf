
using UnityEngine;
using UnityEngine.UI;

public class ChinaLiveObj : MonoBehaviour
{
	public Text NowMoneyText;

	public Text OldMoneyText;

	public GameObject SaleImg;

	public Sprite[] LSaleImg;

	public GameObject IconObj;

	public Sprite[] LIconObj;

	public Text zhekouText;

	private int iLove;

	private int iMoney;

	private void Start()
	{
	}

	public void InitData(int index)
	{
		base.gameObject.SetActive(value: true);
		int num = int.Parse(Singleton<DataManager>.Instance.dDataChinaBuyLive[index.ToString()]["Icon"]);
		iLove = int.Parse(Singleton<DataManager>.Instance.dDataChinaBuyLive[index.ToString()]["ilove"]);
		IconObj.GetComponent<Image>().sprite = LIconObj[num];
		iMoney = int.Parse(Singleton<DataManager>.Instance.dDataChinaBuyLive[index.ToString()]["iGold"]);
		int num2 = int.Parse(Singleton<DataManager>.Instance.dDataChinaBuyLive[index.ToString()]["iOldMoney"]);
		string text = Singleton<DataManager>.Instance.dDataLanguage["BuyLoveMoney1"][BaseUIAnimation.Language];
		text = text.Replace("A1", num2.ToString());
		OldMoneyText.text = text;
		NowMoneyText.text = iMoney.ToString();
		int num3 = int.Parse(Singleton<DataManager>.Instance.dDataChinaBuyLive[index.ToString()]["saleIcon"]);
		if (num3 == -1)
		{
			SaleImg.gameObject.SetActive(value: false);
			OldMoneyText.gameObject.SetActive(value: false);
		}
		else if (InitGame.bEnios)
		{
			SaleImg.GetComponent<Image>().sprite = LSaleImg[num3 + 2];
		}
		else
		{
			SaleImg.GetComponent<Image>().sprite = LSaleImg[num3];
		}
		if (index == 2)
		{
			BaseUIAnimation.action.SetLanguageFont("LovesaleText1", zhekouText, string.Empty);
		}
		if (index == 3)
		{
			BaseUIAnimation.action.SetLanguageFont("LovesaleText2", zhekouText, string.Empty);
		}
	}

	public void ClickBuyLive()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (iMoney > @int)
		{
			Singleton<DataManager>.Instance.ChinaShopOpendaoju = false;
			Singleton<DataManager>.Instance.ChinaShopOpenZuanshi = true;

			UI.Instance.OpenPanel(UIPanelType.ChinaShop);
			return;
		}

		aliyunlog.GameUseLog("diamond", iMoney, "live", iLove);
		ChinaPay.action.addRewardAll(1, iLove, BuyLivesChinaUIPanel.panel.gameObject, isShow: true, "buy", "love");
		PayManager.action.ExpendGDP(iMoney, GDPType.BUYLOVE, iLove);
		if ((bool)GameUI.action)
		{
			BuyLivesChinaUIPanel.panel.OnCloseButton();
		}
	}

	private void Update()
	{
	}
}
