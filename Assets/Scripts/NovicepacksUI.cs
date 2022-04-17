using System.Collections;
using EasyMobile;
using UnityEngine;
using UnityEngine.UI;

public class NovicepacksUI : BaseUI
{
	public static NovicepacksUI action;

	public GameObject CloseBtn;

	public GameObject ShopBtn;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.NovicepacksUI;
	}

	private void Awake()
	{
		Canvas component = base.gameObject.transform.GetComponent<Canvas>();
		component.renderMode = RenderMode.ScreenSpaceCamera;
		component.worldCamera = MapUI.action.MapUISceneCamera.GetComponent<Camera>();
	}

	private void InitXinshouUI()
	{
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_XINSHOULIBAO") > 0)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void Shop()
	{
		//InitAndroid.action.doChainePay("yiyuantehuilibao");
        IAPManager.Purchase(EM_IAPConstants.Product_yiyuantehuilibao);
        ShopBtn.GetComponent<Button>().enabled = false;
	}

	public void CloseXinshouUI(bool bClickClose = true)
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
			if (!(gameObject == null) && gameObject.name.LastIndexOf("NovicepacksUI") >= 0)
			{
			}
		}
	}

	public override void OnStart()
	{
		action = this;
		InitXinshouUI();
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
