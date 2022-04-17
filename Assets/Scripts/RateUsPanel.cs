using UnityEngine;

public class RateUsPanel : RateUsPanelBase
{
	public static RateUsPanel panel;

	public override void InitUI()
	{
		panel = this;
	}

	public override void OnCloseButton()
	{
		UI.Instance.ClosePanel();
		UI.Instance.OpenPanel(UIPanelType.Play);
	}

	public override void OnContinueBtn()
	{
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.bubbleshooter.shooting.balls.free&hl=en");
		Singleton<DataManager>.Instance.SaveUserDate("DB_Google_Score", 1);
		FaceBookApi.Action.LogRatedEvent(string.Empty, string.Empty, 5, 5.0);
		UI.Instance.ClosePanel();
		UI.Instance.OpenPanel(UIPanelType.Play);
	}

	public override void OnResumeBase()
	{
	}
}
