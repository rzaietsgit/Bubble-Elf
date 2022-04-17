using UnityEngine;
using UnityEngine.UI;

public class playingScript : MonoBehaviour
{
	public Sprite[] imgL;

	public Image skillimg;

	public Text PlayingText1;

	public Text playingNextText;

	public Text playremarkText;

	public Text NumberText;

	public static playingScript action;

	private int index = 1;

	private void Start()
	{
		action = this;
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn3", PlayingText1, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn9", playingNextText, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn9", playremarkText, string.Empty);
		InitData();
	}

	public void InitData()
	{
		index = 1;
		NumberText.text = index + "/11";
		BaseUIAnimation.action.SetLanguageFont("SettingsPlaying" + index, playremarkText, string.Empty);
	}

	public void ClickNextBtn()
	{
		if (index >= 11)
		{
			wanfaPanel.panel.OnCloseButton();
			return;
		}
		index++;
		BaseUIAnimation.action.SetLanguageFont("SettingsPlaying" + index, playremarkText, string.Empty);
		NumberText.text = index + "/11";
		skillimg.sprite = imgL[index - 1];
		skillimg.SetNativeSize();
	}
}
