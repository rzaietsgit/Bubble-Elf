public class yuyanPanel : yuyanPanelBase
{
	public static yuyanPanel panel;

	public override void InitUI()
	{
		panel = this;
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn5", detail.SetSetPanelTitle1_Text, string.Empty);
	}

	public void ResLanguage()
	{
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn5", detail.SetSetPanelTitle1_Text, string.Empty);
	}
}
