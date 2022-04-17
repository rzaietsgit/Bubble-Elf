using EasyMobile;
using UnityEngine;

public class Vip7UI : BaseUI
{
	public static Vip7UI action;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.Vip7UI;
	}

	private void Start()
	{
		action = this;
	}

	private void Awake()
	{
		Canvas component = base.gameObject.transform.GetComponent<Canvas>();
		component.renderMode = RenderMode.ScreenSpaceCamera;
		component.worldCamera = MapUI.action.MapUISceneCamera.GetComponent<Camera>();
	}

	private void Update()
	{
	}

	public void ClickPayVip()
	{
		if (!InitGame.bVip7)
		{
			//InitAndroid.action.doChainePay("VipPlayer");
            IAPManager.Purchase(EM_IAPConstants.Product_vipplayer);
        }
	}

	public void CloseVip7()
	{
		CloseUI();
	}
}
