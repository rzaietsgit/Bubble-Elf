using System.Collections;
using EasyMobile;
//using Umeng;
using UnityEngine;
using UnityEngine.UI;

public class SaleAdUI : BaseUI
{
	public static SaleAdUI action;

	public GameObject CloseBtn;

	public GameObject PayBtn;

	public GameObject CenterIcon;

	public Text PayText;

	public GameObject finger;

	public GameObject adfree;

	private string key = string.Empty;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.SaleAdUI;
	}

	private void Start()
	{
		action = this;
		key = DataManager.sale_adKey;
		//Analytics.Event("ShowSale" + key);
		if (key == "SaleAdUILoginReward")
		{
			PayText.text = "免费领取";
		}
		else
		{
			InitAndroid.action.checkshowBuyButton();
		}
		CenterIcon.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/sale_ad/" + key, 684, 836);
		showAdfreebtn();
		adfree.SetActive(value: false);
		InitAndroid.action.checkShowAdTip();
	}

	public void ChangeText()
	{
		PayText.text = "立即领取";
	}

	private void showAdfreebtn()
	{
	}

	public void ShowAdTip()
	{
		if (!(key == "SaleAdUILoginReward"))
		{
			adfree.SetActive(value: true);
		}
	}

	public void _CloseSaleAdUI()
	{
		Singleton<DataManager>.Instance.bSaleAdPay2 = false;
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
			StartCoroutine(CallCloseUI());
		}
	}

	public void ClickNext()
	{
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(PayBtn.gameObject);
			StartCoroutine(CallClickNext());
		}
	}

	private IEnumerator CallClickNext(bool bDouble = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		ClickPay();
	}

	public void ClickPay()
	{
		if ((bool)finger)
		{
			UnityEngine.Object.Destroy(finger.gameObject);
		}
		//Analytics.Event("ClickSale" + key);
		if (key == "SaleAdUILoginReward")
		{
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "SaleAdUILoginReward") == 0)
			{
				Singleton<DataManager>.Instance.SaveUserDate("SaleAdUILoginReward", 1);
				ChinaPay.action.addRewardAll(7, 1, MapUI.action.gameObject, isShow: false);
				ChinaPay.action.addRewardAll(3, 18, MapUI.action.gameObject, isShow: false);
				ChinaPay.action.addRewardAll(8, 1, MapUI.action.gameObject, isShow: false);
				BaseUIAnimation.action.ShowProp(7, 1, 3, 18, 8, 1, MapUI.action.gameObject);
				StartCoroutine(CloseIE());
			}
		}
		else if (key == "Pay2")
		{
			Singleton<UIManager>.Instance.OtherOpenUI = EnumUIType.ChinaShopUI;
			Singleton<DataManager>.Instance.bSaleAdPay2 = true;
			CloseUI(bDouble: false, bOpenOther: true);
		}
		else
		{
			//InitAndroid.action.doChainePay(key);
            IAPManager.Purchase(key.ToLower());
            if (key == "yiyuantehuilibao")
			{
				PayBtn.GetComponent<Button>().enabled = false;
			}
			Singleton<DataManager>.Instance.bSaleAdPay2 = false;
		}
	}

	public void PayError()
	{
		if (key == "yiyuantehuilibao")
		{
			PayBtn.GetComponent<Button>().enabled = true;
		}
	}

	private IEnumerator CloseIE()
	{
		yield return new WaitForSeconds(2f);
		CloseUI();
	}

	private IEnumerator CallCloseUI(bool bDouble = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseUI(bDouble);
	}

	private void Awake()
	{
		if (!GameUI.action)
		{
			Canvas component = base.gameObject.transform.GetComponent<Canvas>();
			component.renderMode = RenderMode.ScreenSpaceCamera;
			component.worldCamera = MapUI.action.MapUISceneCamera.GetComponent<Camera>();
		}
	}
}
