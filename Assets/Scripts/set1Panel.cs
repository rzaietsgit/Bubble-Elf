using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class set1Panel : set1PanelBase
{
	public static set1Panel panel;

	private bool bMusic = true;

	private bool bSound = true;

	private bool bFaceBookConn = true;

	public override void InitUI()
	{
		panel = this;
		ResLanguage();
	}

	public void ResLanguage()
	{
		if (Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_GoogleID", string.Empty) == string.Empty)
		{
			detail.UserIDText_Text.text = string.Empty;
		}
		else
		{
			detail.UserIDText_Text.text = "UID:" + Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_GoogleID", string.Empty);
		}
		CheckPushImg();
		CheckClosesound();
		SetFont();
	}

	private void SetFont()
	{
		BaseUIAnimation.action.SetLanguageFont("SetSetPanelTitle", detail.SetSetPanelTitle_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn1", detail.SaveText_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn2", detail.LoadText_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn3", detail.PlayingText_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn4", detail.Consult_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn5", detail.Language_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn6", detail.zhengce_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn7", detail.fuwu_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn8", detail.PushText_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("MapSetPanelCon", detail.FaceBookText_Text, string.Empty);
		CheckPushImg();
	}

	private void CheckClosesound()
	{
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_MusicSwitch", 1) == 0)
		{
			detail.S_MusicBtn_Image.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/setting/setting_btn_sound", 113, 112);
			bSound = false;
		}
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SoundSwitch", 1) == 0)
		{
			detail.S_SoundBtn_Image.sprite = Util.GetResourcesSprite("Img/setting/setting_btn_effect", 113, 112);
			bMusic = false;
		}
	}

	public override void OnS_MusicBtn()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		if (bSound)
		{
			detail.S_MusicBtn_Image.sprite = Util.GetResourcesSprite("Img/setting/setting_btn_sound", 113, 112);
			bSound = false;
			if ((bool)MusicController.action)
			{
				MusicController.action.MusicOFF();
			}
		}
		else
		{
			detail.S_MusicBtn_Image.sprite = Util.GetResourcesSprite("Img/setting/setting_btn_sound_1", 113, 112);
			bSound = true;
			if ((bool)MusicController.action)
			{
				MusicController.action.MusicON();
			}
		}
	}

	public override void OnS_SoundBtn()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		if (bMusic)
		{
			detail.S_SoundBtn_Image.sprite = Util.GetResourcesSprite("Img/setting/setting_btn_effect", 113, 112);
			bMusic = false;
			if ((bool)SoundController.action)
			{
				SoundController.action.SoundOFF();
			}
		}
		else
		{
			detail.S_SoundBtn_Image.sprite = Util.GetResourcesSprite("Img/setting/setting_btn_effect_1", 113, 112);
			bMusic = true;
			if ((bool)SoundController.action)
			{
				SoundController.action.SoundON();
			}
		}
	}

	public void CheckPushImg()
	{
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DBPushState") == 0)
		{
			detail.PushImage_Image.gameObject.SetActive(value: true);
		}
		else
		{
			detail.PushImage_Image.gameObject.SetActive(value: false);
		}
	}

	public override void OnS_PushBtn()
	{
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DBPushState") == 0)
		{
			detail.PushImage_Image.gameObject.SetActive(value: false);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DBPushState", 1);
			InitAndroid.action.SetPush("0");
		}
		else
		{
			detail.PushImage_Image.gameObject.SetActive(value: true);
			InitAndroid.action.SetPush("1");
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DBPushState", 0);
		}
	}

	public override void OnPauseBase()
	{
	}

	public override void OnResumeBase()
	{
	}

	public override void OnResume()
	{
	}

	public override void OnS_SaveBtn()
	{
		Singleton<DataManager>.Instance.iNoticePanelType = 4;
		UI.Instance.OpenPanel(UIPanelType.NoticePanel);
	}

	public override void OnS_LoadBtn()
	{
		Singleton<DataManager>.Instance.iNoticePanelType = 7;
		UI.Instance.OpenPanel(UIPanelType.NoticePanel);
	}

	public override void OnS_wanfa()
	{
		UI.Instance.OpenPanel(UIPanelType.wanfaPanel);
	}

	private string MyEscapeURL(string url)
	{
		return WWW.EscapeURL(url).Replace("+", "%20");
	}

	public override void OnS_lianxi()
	{
		string text = "Global_apps@enpgames.co.kr";
		string text2 = MyEscapeURL("��bubble bubble pop�� Support Mail");
		string url = "--------------------------------\r\n";
		string text3 = MyEscapeURL(url);
		Application.OpenURL("mailto:" + text + "?subject=" + text2 + "&body=" + text3);
	}

	public override void OnS_yuyan()
	{
		UI.Instance.OpenPanel(UIPanelType.yuyanPanel);
	}

	public override void OnS_FaceBookBtn()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		StartCoroutine(CallClickFaceBook());
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
			BaseUIAnimation.action.SetLanguageFont("MapSetPanelOut", detail.FaceBookText_Text, string.Empty);
		}
		else
		{
			BaseUIAnimation.action.SetLanguageFont("MapSetPanelCon", detail.FaceBookText_Text, string.Empty);
			FaceBookApi.Action.FackBookLogin();
		}
	}

	public void CheckFaceBookLogin()
	{
		if (FaceBookApi.Action.bLoginState())
		{
			BaseUIAnimation.action.SetLanguageFont("MapSetPanelOut", detail.FaceBookText_Text, string.Empty);
		}
		else
		{
			BaseUIAnimation.action.SetLanguageFont("MapSetPanelCon", detail.FaceBookText_Text, string.Empty);
		}
	}

	public override void OnS_zhengce1()
	{
		Singleton<DataManager>.Instance.bzhengce = true;
		UI.Instance.OpenPanel(UIPanelType.zhencePanel);
	}

	public override void OnS_fuwu1()
	{
		Singleton<DataManager>.Instance.bzhengce = false;
		UI.Instance.OpenPanel(UIPanelType.zhencePanel);
	}

	public override void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}

	private void Update()
	{
		CheckFaceBookLogin();
	}
}
