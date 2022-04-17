public class YunbuAdShowPanel : YunbuAdShowPanelBase
{
	public static YunbuAdShowPanel panel;

	public override void InitUI()
	{
		panel = this;
	}

	public override void OnPayBubble()
	{
		UI.Instance.ClosePanel();
		InitAndroid.action.PlayVideoAdOK();
	}
}
