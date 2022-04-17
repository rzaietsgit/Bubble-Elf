using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TipFailUI : BaseUI
{
	public Text TipFailUITitle;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.TipFailUI;
	}

	public override void OnStart()
	{
		BaseUIAnimation.action.SetLanguageFont("TipFailUITitle", TipFailUITitle, string.Empty);
		StartCoroutine(StartCloseUI());
		if ((bool)PassLevel.action)
		{
			PassLevel.action.SwitchoverElfAni("cry", bLoop: false);
		}
	}

	public void CloseTipFailUI()
	{
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex > 10000 && Singleton<LevelManager>.Instance.iNowSelectLevelIndex < 20000)
		{
			Singleton<UIManager>.Instance.OtherOpenUI = EnumUIType.DareLoseUI;
		}
		else
		{
			UI.Instance.OpenPanel(UIPanelType.Lose);
		}
		CloseUI(bDouble: false, bOpenOther: true);
	}

	private IEnumerator StartCloseUI()
	{
		yield return new WaitForSeconds(1f);
		CloseTipFailUI();
	}

	private void Update()
	{
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
