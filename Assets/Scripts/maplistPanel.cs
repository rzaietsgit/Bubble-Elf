public class maplistPanel : maplistPanelBase
{
	public static maplistPanel panel;

	public override void InitUI()
	{
		panel = this;
		BaseUIAnimation.action.SetLanguageFont("MapTitle1", detail.SetSetPanelTitle_Text, string.Empty);
	}

	public override void OnResume()
	{
		base.OnResume();
		detail.MapPanel_MapPanelUI.UpdateAllMapObjChina_HongDian();
	}
}
