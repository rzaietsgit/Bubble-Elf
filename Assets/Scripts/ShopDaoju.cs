using UnityEngine;
using UnityEngine.UI;

public class ShopDaoju : MonoBehaviour
{
	public GameObject IconObj;

	public GameObject sale2Obj;

	public GameObject MaskObj;

	public Text oldmoneyText;

	public Text moneyText;

	public Text NumberText;

	public Image TypeImage;

	public Sprite GbImage;

	public Text ChinaShopUI11;

	private bool bGBBuy;

	private int _iType;

	private void Start()
	{
		BaseUIAnimation.action.SetLanguageFont("ChinaShopUI11", ChinaShopUI11, string.Empty);
	}

	public void ClickBuy()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_BuyDaojuRecording" + _iType);
		if (@int != 1)
		{
			Singleton<DataManager>.Instance.BuyDaojuID = _iType.ToString();
			Singleton<DataManager>.Instance.ChinaShopbGBBuy = bGBBuy;
			UI.Instance.OpenPanel(UIPanelType.BuyDaoju);
		}
	}

	public void SetType(int iType)
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_BuyDaojuRecording" + iType);
		if (@int == 1)
		{
			MaskObj.SetActive(value: true);
		}
		_iType = iType;
		string text = Singleton<DataManager>.Instance.dDataBuyDaojuList[iType.ToString()]["iconID"];
		string text2 = Singleton<DataManager>.Instance.dDataBuyDaojuList[iType.ToString()]["sale"];
		string text3 = Singleton<DataManager>.Instance.dDataBuyDaojuList[iType.ToString()]["oldmoney"];
		string text4 = Singleton<DataManager>.Instance.dDataBuyDaojuList[iType.ToString()]["money"];
		string text5 = Singleton<DataManager>.Instance.dDataBuyDaojuList[iType.ToString()]["number"];
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_RDaojuGB");
		if (_iType == int2 || int.Parse(text5) == 100)
		{
			bGBBuy = true;
			text3 = Singleton<DataManager>.Instance.dDataBuyDaojuList[iType.ToString()]["oldgold"];
			text4 = Singleton<DataManager>.Instance.dDataBuyDaojuList[iType.ToString()]["gold"];
			TypeImage.sprite = GbImage;
		}
		else
		{
			bGBBuy = false;
		}
		base.gameObject.SetActive(value: true);
		NumberText.text = "x" + text5;
		int num = int.Parse(text);
		if (num >= 4 && num <= 9 && int.Parse(text5) == 100)
		{
			BaseUIAnimation.action.SetLanguageFont("ChinaShopUI10", NumberText, string.Empty);
			NumberText.gameObject.SetActive(value: false);
		}
		moneyText.text = text4;
		if (text2 == "0")
		{
			sale2Obj.SetActive(value: false);
			moneyText.transform.localPosition -= new Vector3(35f, 0f, 0f);
		}
		else
		{
			string text6 = Singleton<DataManager>.Instance.dDataLanguage["ChinaShopUI9"][BaseUIAnimation.Language];
			text6 = text6.Replace("A1", text2.ToString());
			oldmoneyText.text = text3;
		}
		IconObj.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + text, 138, 114);
	}

	private void Update()
	{
	}
}
