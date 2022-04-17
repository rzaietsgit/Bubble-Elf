using System.Collections;
using TMPro;
//using Umeng;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : BaseUI
{
	public static ShopUI action;

	public GameObject CloseBtn;

	public GameObject libaoObj;

	public GameObject daojuObj;

	public GameObject libaobg;

	public GameObject daojubg;

	public Sprite select;

	public Sprite unselect;

	public TextMeshProUGUI ChinaShopUITitle;

	public TextMeshProUGUI ChinaShopUIlibao;

	public TextMeshProUGUI ChinaShopUIdaoju;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.ShopUI;
	}

	private void Awake()
	{
		Canvas component = base.gameObject.transform.GetComponent<Canvas>();
		component.renderMode = RenderMode.ScreenSpaceCamera;
		component.worldCamera = MapUI.action.MapUISceneCamera.GetComponent<Camera>();
	}

	private void InitShopUI()
	{
		selectlibao();
	}

	public void selectlibao()
	{
		libaoObj.GetComponent<Image>().sprite = select;
		daojuObj.GetComponent<Image>().sprite = unselect;
		libaobg.SetActive(value: true);
		daojubg.SetActive(value: false);
	}

	public void selctdaoju()
	{
		libaoObj.GetComponent<Image>().sprite = unselect;
		daojuObj.GetComponent<Image>().sprite = select;
		libaobg.SetActive(value: false);
		daojubg.SetActive(value: true);
	}

	public void daoju1()
	{
		//Analytics.Event("LogClick4");
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (@int - 180 >= 0)
		{
			//Analytics.Event("Logpay4");
			ChinaPay.action.ShopUIdaoju1();
			PayManager.action.BuyDaoju(180);
			return;
		}
		EnumUIType[] uiTypes = new EnumUIType[2]
		{
			EnumUIType.BuyGoldUI,
			EnumUIType.ShopUI
		};
		Singleton<UIManager>.Instance.OpenUI(uiTypes);
		if ((bool)FirstPacksUI.action)
		{
			FirstPacksUI.action.DoubleFirstPacksUI();
		}
	}

	public void daoju2()
	{
		//Analytics.Event("LogClick5");
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (@int - 219 >= 0)
		{
			//Analytics.Event("Logpay5");
			ChinaPay.action.ShopUIdaoju2();
			PayManager.action.BuyDaoju(219);
			return;
		}
		EnumUIType[] uiTypes = new EnumUIType[2]
		{
			EnumUIType.BuyGoldUI,
			EnumUIType.ShopUI
		};
		Singleton<UIManager>.Instance.OpenUI(uiTypes);
		if ((bool)FirstPacksUI.action)
		{
			FirstPacksUI.action.DoubleFirstPacksUI();
		}
	}

	public void daoju3()
	{
		//Analytics.Event("LogClick6");
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (@int - 150 >= 0)
		{
			//Analytics.Event("Logpay6");
			ChinaPay.action.ShopUIdaoju3();
			PayManager.action.BuyDaoju(150);
			return;
		}
		EnumUIType[] uiTypes = new EnumUIType[2]
		{
			EnumUIType.BuyGoldUI,
			EnumUIType.ShopUI
		};
		Singleton<UIManager>.Instance.OpenUI(uiTypes);
		if ((bool)action)
		{
			action.DoubleShopUI();
		}
	}

	public void daoju4()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		//Analytics.Event("LogClick7");
		if (@int - 25 >= 0)
		{
			//Analytics.Event("Logpay7");
			ChinaPay.action.ShopUIdaoju4();
			PayManager.action.BuyDaoju(25);
			return;
		}
		EnumUIType[] uiTypes = new EnumUIType[2]
		{
			EnumUIType.BuyGoldUI,
			EnumUIType.ShopUI
		};
		Singleton<UIManager>.Instance.OpenUI(uiTypes);
		if ((bool)action)
		{
			action.DoubleShopUI();
		}
	}

	public void daoju5()
	{
		//Analytics.Event("LogClick8");
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (@int - 68 >= 0)
		{
			//Analytics.Event("Logpay8");
			ChinaPay.action.ShopUIdaoju5();
			PayManager.action.BuyDaoju(68);
			return;
		}
		EnumUIType[] uiTypes = new EnumUIType[2]
		{
			EnumUIType.BuyGoldUI,
			EnumUIType.ShopUI
		};
		Singleton<UIManager>.Instance.OpenUI(uiTypes);
		if ((bool)action)
		{
			action.DoubleShopUI();
		}
	}

	public void daoju6()
	{
		//Analytics.Event("LogClick9");
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (@int - 100 >= 0)
		{
			//Analytics.Event("Logpay9");
			ChinaPay.action.ShopUIdaoju6();
			PayManager.action.BuyDaoju(100);
			return;
		}
		EnumUIType[] uiTypes = new EnumUIType[2]
		{
			EnumUIType.BuyGoldUI,
			EnumUIType.ShopUI
		};
		Singleton<UIManager>.Instance.OpenUI(uiTypes);
		if ((bool)action)
		{
			action.DoubleShopUI();
		}
	}

	public void DoubleShopUI(bool bClickClose = true)
	{
		if (bClickClose)
		{
			if (!BaseUIAnimation.bClickButton)
			{
				return;
			}
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
		}
		StartCoroutine(CallCloseUI(bClickClose));
	}

	public void CloseShopUI(bool bClickClose = true)
	{
		if (bClickClose)
		{
			if (!BaseUIAnimation.bClickButton)
			{
				return;
			}
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
		}
		StartCoroutine(CallCloseUI());
	}

	private IEnumerator CallCloseUI(bool b = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseUI(b);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject gameObject = Util.TouchChecker(UnityEngine.Input.mousePosition);
			if (!(gameObject == null) && gameObject.name.LastIndexOf("ShopUI") >= 0)
			{
			}
		}
	}

	public override void OnStart()
	{
		action = this;
		BaseUIAnimation.action.SetLanguageFont("ChinaShopUITitle", ChinaShopUITitle, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("ChinaShopUIlibao", ChinaShopUIlibao, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("ChinaShopUIdaoju", ChinaShopUIdaoju, string.Empty);
		InitShopUI();
		//Analytics.Event("LogClickShop");
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
