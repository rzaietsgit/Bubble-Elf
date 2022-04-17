using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SetPanelUI : MonoBehaviour
{
	public static SetPanelUI action;

	public GameObject ChinaAbout;

	public Text FaceBookConn;

	public Text SetSetPanelTitle;

	public Text SetUILanguage;

	public Text cdkeyUITtitle;

	private bool bMusic = true;

	private bool bSound = true;

	private bool bFaceBookConn = true;

	public GameObject SoundBtn;

	public GameObject MusicBtn;

	public GameObject FacebookBtn;

	public GameObject SetUILanguageObj;

	public GameObject IconObj;

	public GameObject CdKeyObj;

	public Sprite SpLogEn;

	public Sprite SpLogEnGoogle;

	public GameObject SetBtn;

	public GameObject playingpan;

	public GameObject Languagepan;

	public GameObject infopan;

	public GameObject infopan1;

	public Text SaveText;

	public Text LoadText;

	public Text PlayingText;

	public Text ConsultText;

	public Text LanguageText;

	public Text zhengceText;

	public Text fuwuText;

	public Text PushText;

	public GameObject PushImg;

	public Text USERIDText;

	public Text infoText;

	public Text infoTextBtnText;

	public Text fuwuText1;

	public Text fuwuTextBtnText;

	private void Start()
	{
		action = this;
		USERIDText.text = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_GoogleID", string.Empty);
		CheckFaceBookLogin();
		CheckClosesound();
		if ((bool)FaceBookApi.Action)
		{
			FaceBookApi.Action.CheckLoginIcon(FacebookBtn);
		}
		BaseUIAnimation.action.SetLanguageFont("SetSetPanelTitle", SetSetPanelTitle, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn1", SaveText, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn2", LoadText, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn3", PlayingText, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn4", ConsultText, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn5", LanguageText, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn6", zhengceText, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn7", fuwuText, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn8", PushText, string.Empty);
		CheckPushImg();
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn6", infoText, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn7", fuwuText1, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn10", infoTextBtnText, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn10", fuwuTextBtnText, string.Empty);
	}

	public void ClickPushImg()
	{
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DBPushState") == 0)
		{
			PushImg.SetActive(value: false);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DBPushState", 1);
			InitAndroid.action.SetPush("0");
		}
		else
		{
			PushImg.SetActive(value: true);
			InitAndroid.action.SetPush("1");
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DBPushState", 0);
		}
	}

	public void CheckPushImg()
	{
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DBPushState") == 0)
		{
			PushImg.SetActive(value: true);
		}
		else
		{
			PushImg.SetActive(value: false);
		}
	}

	public void ResBtn()
	{
		if (!InitGame.bChinaVersion)
		{
			BaseUIAnimation.action.CreateButton(SetUILanguageObj.gameObject);
			BaseUIAnimation.action.CreateButton(FacebookBtn.gameObject);
			BaseUIAnimation.action.CreateButton(CdKeyObj.gameObject);
		}
	}

	private void changePos()
	{
		BaseUIAnimation.action.CreateButton(SetUILanguageObj.gameObject);
		FacebookBtn.SetActive(value: true);
		BaseUIAnimation.action.SetLanguageFont("SetUILanguage", SetUILanguage, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("cdkeyUITtitle", cdkeyUITtitle, string.Empty);
		BaseUIAnimation.action.CreateButton(FacebookBtn.gameObject);
		IconObj.transform.localPosition += new Vector3(0f, 70f, 0f);
		SoundBtn.transform.localPosition += new Vector3(0f, 80f, 0f);
		MusicBtn.transform.localPosition += new Vector3(0f, 80f, 0f);
		FacebookBtn.transform.localPosition += new Vector3(0f, 120f, 0f);
		SetUILanguageObj.transform.localPosition += new Vector3(0f, 120f, 0f);
		CdKeyObj.transform.localPosition -= new Vector3(0f, 45f, 0f);
		CdKeyObj.SetActive(value: true);
		BaseUIAnimation.action.CreateButton(CdKeyObj.gameObject);
	}

	public void ClickGengduo()
	{
		
	}

	public void OpenLanguageUI()
	{
		Singleton<UIManager>.Instance.OpenUI(EnumUIType.LanguageUI);
		if ((bool)SettingPanelUI.action)
		{
			SettingPanelUI.action.CloseSettingBtnUI();
		}
	}

	public void OpenCdKey()
	{
		UI.Instance.OpenPanel(UIPanelType.cdkeyUI);
		if ((bool)SettingPanelUI.action)
		{
			SettingPanelUI.action.CloseSettingBtnUI();
		}
	}

	private void CheckClosesound()
	{
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_MusicSwitch", 1) == 0)
		{
			SoundBtn.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/setting/setting_btn_sound", 113, 112);
			bSound = false;
		}
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SoundSwitch", 1) == 0)
		{
			MusicBtn.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/setting/setting_btn_effect", 113, 112);
			bMusic = false;
		}
	}

	public void ResFaceBookLoginState()
	{
		if (FaceBookApi.Action.bLoginState())
		{
			bFaceBookConn = true;
		}
		else
		{
			bFaceBookConn = false;
		}
	}

	private void Update()
	{
	}

	public void ClickMusic()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		if (!BaseUIAnimation.bClickButton)
		{
			return;
		}
		BaseUIAnimation.action.ClickButton(SoundBtn.gameObject);
		if (bSound)
		{
			SoundBtn.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/setting/setting_btn_sound", 113, 112);
			bSound = false;
			if ((bool)MusicController.action)
			{
				MusicController.action.MusicOFF();
			}
		}
		else
		{
			SoundBtn.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/setting/setting_btn_sound_1", 113, 112);
			bSound = true;
			if ((bool)MusicController.action)
			{
				MusicController.action.MusicON();
			}
		}
	}

	public void ClickSound()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		if (!BaseUIAnimation.bClickButton)
		{
			return;
		}
		BaseUIAnimation.action.ClickButton(MusicBtn.gameObject);
		if (bMusic)
		{
			MusicBtn.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/setting/setting_btn_effect", 113, 112);
			bMusic = false;
			if ((bool)SoundController.action)
			{
				SoundController.action.SoundOFF();
			}
		}
		else
		{
			MusicBtn.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/setting/setting_btn_effect_1", 113, 112);
			bMusic = true;
			if ((bool)SoundController.action)
			{
				SoundController.action.SoundON();
			}
		}
	}

	public void ClickAbout()
	{
		Singleton<UIManager>.Instance.OpenUI(EnumUIType.aboutChinaUI);
		if ((bool)SettingPanelUI.action)
		{
			SettingPanelUI.action.CloseSettingBtnUI();
		}
	}

	public void ClickFaceBook()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(FacebookBtn.gameObject);
			StartCoroutine(CallClickFaceBook());
		}
	}

	private IEnumerator CallClickFaceBook()
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		if (FaceBookApi.Action.bLoginState())
		{
			FaceBookApi.Action.FackBookLoginOut();
			BaseUIAnimation.action.SetLanguageFont("MapSetPanelCon", FaceBookConn, string.Empty);
			FaceBookConn.fontSize = 50;
		}
		else
		{
			FaceBookApi.Action.FackBookLogin();
		}
	}

	public void CheckFaceBookLogin()
	{
		if (FaceBookApi.Action.bLoginState())
		{
			BaseUIAnimation.action.SetLanguageFont("MapSetPanelOut", FaceBookConn, string.Empty);
			FaceBookConn.fontSize = 45;
		}
		else
		{
			BaseUIAnimation.action.SetLanguageFont("MapSetPanelCon", FaceBookConn, string.Empty);
			FaceBookConn.fontSize = 50;
		}
	}

	public void ClickSettingBtn(bool bClick)
	{
		if ((bool)MailUI.action)
		{
			MailUI.action.gameObject.SetActive(value: false);
		}
		if ((bool)MapPanelUI.action)
		{
			MapPanelUI.action.gameObject.SetActive(value: false);
		}
	}

	public void clickSwitchLanguage(int index)
	{
		hideother();
		Languagepan.SetActive(value: true);
	}

	public void hideall()
	{
		playingpan.SetActive(value: false);
		Languagepan.SetActive(value: false);
		infopan.SetActive(value: false);
		infopan1.SetActive(value: false);
		SetBtn.SetActive(value: true);
	}

	public void hideother()
	{
		SetBtn.SetActive(value: false);
		playingpan.SetActive(value: false);
		Languagepan.SetActive(value: false);
		infopan.SetActive(value: false);
		infopan1.SetActive(value: false);
	}

	public void clickzhengche()
	{
		hideother();
		infopan.SetActive(value: true);
	}

	public void clickfuwu()
	{
		hideother();
		infopan1.SetActive(value: true);
	}

	public void clickplayingbtn(int index)
	{
		hideother();
		playingpan.SetActive(value: true);
		playingpan.GetComponent<playingScript>().InitData();
	}

	public void Clicklianxi()
	{
		SendEmail();
	}

	public void ClickSaveBtn()
	{
		Singleton<DataManager>.Instance.iNoticePanelType = 4;
		UI.Instance.OpenPanel(UIPanelType.NoticePanel);
	}

	public void ClickLoadBtn()
	{
		Singleton<DataManager>.Instance.iNoticePanelType = 7;
		UI.Instance.OpenPanel(UIPanelType.NoticePanel);
	}

	public void SendEmail()
	{
		string text = "Global_apps@enpgames.co.kr";
		string text2 = MyEscapeURL("★Bubble Bubble Pop★ Support Mail");
		string url = "--------------------------------\r\n";
		string text3 = MyEscapeURL(url);
		Application.OpenURL("mailto:" + text + "?subject=" + text2 + "&body=" + text3);
	}

	private string MyEscapeURL(string url)
	{
		return WWW.EscapeURL(url).Replace("+", "%20");
	}

	public void clickxinxireturn()
	{
		hideall();
	}

	public void clickfuwureturn()
	{
		hideall();
	}
}
