using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class BuyGoldSonByDiamonds : MonoBehaviour
{
	public GameObject BuyIconObj;

	public GameObject BuyBtnType;

	public TextMeshProUGUI BuyPrice;

	public TextMeshProUGUI MoneyViewText;

	public GameObject PayBtn;

	private string PayKey;

	public Sprite[] LBuyIconObj;

	public void InitForPayID(string ID)
	{
		PayKey = ID;
		BuyPrice.SetText(GetNumByID(int.Parse(ID)).ToString());
		MoneyViewText.SetText(GetGoldByID(int.Parse(ID)).ToString());
		BuyIconObj.GetComponent<Image>().sprite = LBuyIconObj[int.Parse(ID) - 1];
	}

	public int GetNumByID(int ID)
	{
		int result = 0;
		switch (ID)
		{
		case 1:
			result = 50;
			break;
		case 2:
			result = 475;
			break;
		case 3:
			result = 1380;
			break;
		case 4:
			result = 3600;
			break;
		case 5:
			result = 4350;
			break;
		}
		return result;
	}

	public int GetGoldByID(int ID)
	{
		int result = 0;
		switch (ID)
		{
		case 1:
			result = 1000;
			break;
		case 2:
			result = 10000;
			break;
		case 3:
			result = 30000;
			break;
		case 4:
			result = 80000;
			break;
		case 5:
			result = 100000;
			break;
		}
		return result;
	}

	public void UmengGBLog(int iByGB, bool bClick = true)
	{
		//switch (iByGB)
		//{
		//case 1000:
		//	if (bClick)
		//	{
		//		Analytics.Event("LogClick15");
		//	}
		//	else
		//	{
		//		Analytics.Event("Logpay15");
		//	}
		//	break;
		//case 10000:
		//	if (bClick)
		//	{
		//		Analytics.Event("LogClick16");
		//	}
		//	else
		//	{
		//		Analytics.Event("Logpay16");
		//	}
		//	break;
		//case 30000:
		//	if (bClick)
		//	{
		//		Analytics.Event("LogClick17");
		//	}
		//	else
		//	{
		//		Analytics.Event("Logpay17");
		//	}
		//	break;
		//case 80000:
		//	if (bClick)
		//	{
		//		Analytics.Event("LogClick18");
		//	}
		//	else
		//	{
		//		Analytics.Event("Logpay18");
		//	}
		//	break;
		//case 100000:
		//	if (bClick)
		//	{
		//		Analytics.Event("LogClick19");
		//	}
		//	else
		//	{
		//		Analytics.Event("Logpay19");
		//	}
		//	break;
		//}
	}

	public void ClickPay()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		int goldByID = GetGoldByID(int.Parse(PayKey));
		UmengGBLog(goldByID);
		if (@int >= GetNumByID(int.Parse(PayKey)))
		{
			UmengGBLog(goldByID, bClick: false);
			PayManager.action.AddGB(GetNumByID(int.Parse(PayKey)), GetGoldByID(int.Parse(PayKey)));
			if ((bool)BuyGoldUIByDiamonds.action)
			{
				BuyGoldUIByDiamonds.action.CloseBuyGoldUI();
			}
		}
		else
		{
			EnumUIType[] uiTypes = new EnumUIType[2]
			{
				EnumUIType.BuyGoldUI,
				EnumUIType.BuyGoldUIByDiamonds
			};
			Singleton<UIManager>.Instance.OpenUI(uiTypes);
			if ((bool)BuyGoldUIByDiamonds.action)
			{
				BuyGoldUIByDiamonds.action.DoubleCloseBuyGoldUI();
			}
		}
	}

	private void Start()
	{
		BaseUIAnimation.action.CreateButton(PayBtn.gameObject);
	}

	private void Update()
	{
	}
}
