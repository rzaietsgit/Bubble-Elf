using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HuaShopUI : BaseUI
{
	public static HuaShopUI action;

	public GameObject EnterBtn;

	public GameObject CloseBtn;

	public GameObject ShopObj;

	public GameObject ShopObjFather;

	public Text HuaBiText;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.HuaShopUI;
	}

	public void InitHuaBi()
	{
		HuaBiText.text = Singleton<UserManager>.Instance.GetHuaBi().ToString();
		if ((bool)MapHuaTopUI.action)
		{
			MapHuaTopUI.action.InitHuaBi();
		}
	}

	public void CloseDareUI(bool bDouble = false)
	{
		StartCoroutine(CallCloseUI(bDouble));
	}

	public void _CloseDareUI()
	{
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
			StartCoroutine(CallCloseUI());
		}
	}

	public IEnumerator CallCloseUI(bool bDouble = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		base.gameObject.SetActive(value: false);
		CloseUI(bDouble);
	}

	private void Update()
	{
	}

	public override void OnStart()
	{
		action = this;
		if (!Util.CheckOnline())
		{
			Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
			return;
		}
		createUI();
		if (Singleton<UserManager>.Instance.bOpenHua() <= 0)
		{
			Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
		}
	}

	public void createUI()
	{
		ShopObj.SetActive(value: false);
		for (int i = 1; i <= 6; i++)
		{
			GameObject gameObject = Object.Instantiate(ShopObj);
			gameObject.transform.SetParent(ShopObjFather.transform, worldPositionStays: false);
			BuyHuaObj component = gameObject.GetComponent<BuyHuaObj>();
			component.InitData(i);
			gameObject.SetActive(value: true);
		}
		InitHuaBi();
	}

	protected override void OnAwake()
	{
		Canvas component = base.gameObject.transform.GetComponent<Canvas>();
		component.renderMode = RenderMode.ScreenSpaceCamera;
		component.worldCamera = HuaGame.action.HuaCamera.GetComponent<Camera>();
		base.OnAwake();
	}

	protected override void OnRelease()
	{
		base.OnRelease();
	}

	public void ClickClose22222()
	{
		CloseUI();
	}
}
