public class zhencePanel : zhencePanelBase
{
	public static zhencePanel panel;

	public override void InitUI()
	{
		panel = this;
		if (Singleton<DataManager>.Instance.bzhengce)
		{
			base.transform.Find("bg/xinxi1/Select1ScrollView2").gameObject.SetActive(value: false);
			BaseUIAnimation.action.SetLanguageFont("SettingsBtn6", detail.title_Text, string.Empty);
		}
		else
		{
			base.transform.Find("bg/xinxi1/Select1ScrollView1").gameObject.SetActive(value: false);
			BaseUIAnimation.action.SetLanguageFont("SettingsBtn7", detail.title_Text, string.Empty);
		}
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn10", detail.Text1_Text, string.Empty);
	}

	public override void OnLanguage2()
	{
		UI.Instance.ClosePanel();
	}
}
