using System.Collections;
using UnityEngine;

public class TipFailPanel : TipFailPanelBase
{
	public static TipFailPanel panel;

	public override void InitUI()
	{
		panel = this;
		BaseUIAnimation.action.SetLanguageFont("TipFailUITitle", detail.TipFailUITitle_Text, string.Empty);
		StartCoroutine(StartCloseUI());
		if ((bool)PassLevel.action)
		{
			PassLevel.action.SwitchoverElfAni("cry", bLoop: false);
		}
	}

	private IEnumerator StartCloseUI()
	{
		yield return new WaitForSeconds(1f);
		UI.Instance.ClosePanel();
	}

	public override void OnExit()
	{
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex > 10000 && Singleton<LevelManager>.Instance.iNowSelectLevelIndex < 20000)
		{
			UI.Instance.OpenPanel(UIPanelType.Lose);
		}
		else
		{
			UI.Instance.OpenPanel(UIPanelType.Lose);
		}
	}
}
