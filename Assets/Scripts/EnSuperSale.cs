using System.Collections;
using UnityEngine;

public class EnSuperSale : BaseUI
{
	public static EnSuperSale action;

	public GameObject CloseBtn;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.EnSuperSale;
	}

	private void Awake()
	{
		if ((bool)GameUI.action)
		{
			Canvas component = base.gameObject.transform.GetComponent<Canvas>();
			component.renderMode = RenderMode.ScreenSpaceCamera;
			component.worldCamera = GameUI.action.mainCameraS.GetComponent<Camera>();
		}
		else
		{
			Canvas component2 = base.gameObject.transform.GetComponent<Canvas>();
			component2.renderMode = RenderMode.ScreenSpaceCamera;
			component2.worldCamera = MapUI.action.MapUISceneCamera.GetComponent<Camera>();
		}
	}

	public void SuperSale()
	{
		PayManager.action.Pay("PACKAGE00099");
	}

	public void DoubleEnSuperSale(bool bClickClose = true)
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

	public void CloseEnSuperSale(bool bClickClose = true)
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
	}

	public override void OnStart()
	{
		action = this;
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
