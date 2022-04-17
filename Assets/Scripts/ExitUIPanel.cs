using UnityEngine;

public class ExitUIPanel : ExitUIPanelBase
{
	public static ExitUIPanel panel;

	private bool bexitExit;

	public override void InitUI()
	{
		panel = this;
		bexitExit = false;
		BaseUIAnimation.action.SetLanguageFont("QuitGame1", detail.QuitUIRemark_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("QuitUIQuitbtn", detail.QuitUITitle_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("QuitUIQuitbtn", detail.QuitUIQuitbtn_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("QuitUIContinuebtn", detail.QuitUIContinuebtn_Text, string.Empty);
		InitAndroid.action.openCenterad();
	}

	public override void OnButton1()
	{
		bexitExit = true;
		Application.Quit();
		UI.Instance.ClosePanel();
	}

	public override void OnButton2()
	{
		UI.Instance.ClosePanel();
	}

	public override void OnExit()
	{
		InitAndroid.action.CloseCenterad();
		if (bexitExit)
		{
			Application.Quit();
		}
	}
}
