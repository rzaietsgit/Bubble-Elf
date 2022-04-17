public class BuyLivesPanel : BuyLivesPanelBase
{
	public static BuyLivesPanel panel;

	public override void InitUI()
	{
		panel = this;
	}

	public override void OnPlayButton()
	{
		UI.Instance.OpenPanel(UIPanelType.Play);
	}

	public override void OnResumeBase()
	{
		UI.Instance.ClosePanel(isShowExit: false);
	}
}
