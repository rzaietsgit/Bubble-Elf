using System.Collections;
using UnityEngine;

public class TipWinPanel : TipWinPanelBase
{
	public static TipWinPanel panel;

	public GameObject fx_completeObj;

	public override void InitUI()
	{
		panel = this;
		BaseUIAnimation.action.SetLanguageFont("TipWinUITitle", detail.TipWinUITitle_Text, string.Empty);
		GameObject gameObject = Object.Instantiate(fx_completeObj);
		gameObject.transform.SetParent(base.transform.parent, worldPositionStays: false);
		gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
		UnityEngine.Object.Destroy(gameObject, 8f);
		StartCoroutine(StartCloseUI());
	}

	private IEnumerator StartCloseUI()
	{
		yield return new WaitForSeconds(1.3f);
		UI.Instance.ClosePanel();
	}

	public override void OnExit()
	{
		BubbleSpawner.Instance.WinFallBubble();
	}
}
