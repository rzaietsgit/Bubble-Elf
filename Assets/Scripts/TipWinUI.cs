using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TipWinUI : BaseUI
{
	public static TipWinUI action;

	public GameObject fx_completeObj;

	public Text TipWinUITitle;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.TipWinUI;
	}

	public override void OnStart()
	{
		action = this;
		BaseUIAnimation.action.SetLanguageFont("TipWinUITitle", TipWinUITitle, string.Empty);
		Canvas component = base.transform.GetComponent<Canvas>();
		component.renderMode = RenderMode.ScreenSpaceCamera;
		component.worldCamera = GameUI.action.mainCameraS.GetComponent<Camera>();
		GameObject gameObject = Object.Instantiate(fx_completeObj);
		gameObject.transform.SetParent(base.transform.parent, worldPositionStays: false);
		gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
		UnityEngine.Object.Destroy(gameObject, 8f);
		StartCoroutine(StartCloseUI());
	}

	public void CloseTipWinUI()
	{
		CloseUI();
	}

	private IEnumerator StartCloseUI()
	{
		yield return new WaitForSeconds(1.3f);
		CloseTipWinUI();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject gameObject = Util.TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject == null)
			{
				CloseTipWinUI();
			}
			else if (gameObject.name.LastIndexOf("TipWinUI") < 0)
			{
				CloseTipWinUI();
			}
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
