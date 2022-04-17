using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReadyGoUI : BaseUI
{
	public Text ReadGoUI;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.ReadyGoUI;
	}

	public void CloseReadyGoUI()
	{
		CloseUI();
	}

	private IEnumerator StartCloseUI()
	{
		yield return new WaitForSeconds(1.5f);
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_game_start");
		}
		CloseReadyGoUI();
	}

	private void Update()
	{
	}

	public override void OnStart()
	{
		BaseUIAnimation.action.SetLanguageFont("ReadGoUI", ReadGoUI, string.Empty);
		StartCoroutine(StartCloseUI());
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
