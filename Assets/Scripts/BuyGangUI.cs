using System.Collections;
using EasyMobile;
using UnityEngine;
using UnityEngine.UI;

public class BuyGangUI : BaseUI
{
	public static BuyGangUI action;

	public GameObject CloseBtn;

	public GameObject GangIcon;

	public GameObject GangIcon2;

	public Sprite[] LGangSprite;

	public Sprite[] LGangSprite2;

	public GameObject GangPayObj;

	public Text GoldText;

	public Text GoldNumber;

	public Text BuyGangRemark;

	public Text BuyGangTitle;

	public GameObject CenterPrice;

	public GameObject Down;

	public Text ChinaMoneyText;

	private int iGangType = 1;

	private int iGangprice = 25;

	public GameObject _sanyuan;

	public GameObject PayBtn;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.BuyGangUI;
	}

	public override void OnStart()
	{
		action = this;
		BaseUIAnimation.action.CreateButton(PayBtn.gameObject);
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
		//Analytics.Event("ShowGangPay" + iGangType);
		LoadGold();
		LoadGangType();
		BaseUIAnimation.action.SetLanguageFont("BuyGangRemark" + iGangType, BuyGangRemark, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BuyGangTitle", BuyGangTitle, string.Empty);
		int num = iGangprice = int.Parse(Singleton<DataManager>.Instance.dDataSystemConfig["Gang" + iGangType]["V"]);
		GoldNumber.text = iGangprice + string.Empty;
		_sanyuan.SetActive(value: false);
		if (InitGame.bChinaVersion)
		{
			_sanyuan.SetActive(value: true);
			Down.SetActive(value: false);
			CenterPrice.SetActive(value: false);
			ChinaMoneyText.text = "立即购买";
			if (InitGame.bEnios)
			{
				BaseUIAnimation.action.SetLanguageFont("BuyGangUI2", ChinaMoneyText, string.Empty);
			}
			ChinaMoneyText.gameObject.SetActive(value: true);
			InitAndroid.action.checkshowBuyButton();
		}
		float num2 = float.Parse(Singleton<DataManager>.Instance.dDataChinaPay["BuyGang" + iGangType]["iMoney"]);
		if (Singleton<DataManager>.Instance.bChinaIos)
		{
			num2 = float.Parse(Singleton<DataManager>.Instance.dDataChinaPay["BuyGang" + iGangType]["iMoneyios"]);
			if (InitGame.bEnios)
			{
				num2 = float.Parse(Singleton<DataManager>.Instance.dDataChinaPay["BuyGang" + iGangType]["iMoneyiosen"]);
			}
		}
		string text = Singleton<DataManager>.Instance.dDataLanguage["BuyGangUI1"][BaseUIAnimation.Language];
		text = text.Replace("A1", num2.ToString());
		_sanyuan.GetComponent<Text>().text = text;
	}

	public void ChangeText()
	{
		ChinaMoneyText.text = "立即领取";
	}

	public void LoadGold()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		GoldText.text = @int.ToString();
	}

	private void LoadGangType()
	{
		GangIcon.GetComponent<Image>().sprite = LGangSprite[iGangType - 1];
		GangIcon2.GetComponent<Image>().sprite = LGangSprite2[iGangType - 1];
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject gameObject = Util.TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject == null)
			{
				CloseLoseUI();
			}
			else if (gameObject.name.LastIndexOf("BuyGangUI") < 0)
			{
				CloseLoseUI();
			}
		}
	}

	public void CloseLoseUI(bool bDouble = false)
	{
		StartCoroutine(CallCloseUI(bDouble));
	}

	public void _CloseLoseUI()
	{
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
			StartCoroutine(CallCloseUI());
		}
	}

	private IEnumerator CallCloseUI(bool bDouble = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseUI(bDouble);
	}

	public void PayGang()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(GangPayObj.gameObject);
			StartCoroutine(CallPayGang());
		}
	}

	private IEnumerator CallPayGang(bool bDouble = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		if (InitGame.bChinaVersion)
		{
			//InitAndroid.action.doChainePay("BuyGang" + iGangType);
            switch (iGangType)
            {
                case 1:
                    IAPManager.Purchase(EM_IAPConstants.Product_buygang1);
                    break;
                case 2:
                    IAPManager.Purchase(EM_IAPConstants.Product_buygang2);
                    break;
                case 3:
                    IAPManager.Purchase(EM_IAPConstants.Product_buygang3);
                    break;
                case 4:
                    IAPManager.Purchase(EM_IAPConstants.Product_buygang4);
                    break;
            }
            yield break;
		}
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (iGangprice > @int)
		{
			EnumUIType[] uiTypes = new EnumUIType[2]
			{
				EnumUIType.BuyGoldUI,
				EnumUIType.BuyGangUI
			};
			CloseLoseUI();
			Singleton<UIManager>.Instance.OpenUI(uiTypes);
		}
		else
		{
			PayManager.action.BuyGang(iGangType, iGangprice, 1);
			//Analytics.Event("GangPay" + iGangType);
			CloseLoseUI();
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
}
