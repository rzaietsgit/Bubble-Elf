//using Umeng;
using EasyMobile;
using ITSoft;
using UnityEngine;
using UnityEngine.UI;

public class QuitUIPanel : QuitUIPanelBase
{
	public static QuitUIPanel panel;

	public bool bcontinue = true;

	private bool bMusic = true;

	private bool bSound = true;

	public bool bexitExit;

	public override void InitUI()
	{
		panel = this;
		bexitExit = false;
		//aliyunlog.LevelLog("quit");
		BaseUIAnimation.action.SetLanguageFont("QuitUIContinuebtn", detail.QuitUIContinuebtn_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("QuitUITitle", detail.QuitUITitle_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("QuitUIQuitbtn", detail.QuitUIQuitbtn_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("QuitUIRemark", detail.QuitUIRemark_Text, string.Empty);
		if (Singleton<LevelManager>.Instance.bRstart)
		{
			BaseUIAnimation.action.SetLanguageFont("QuitUIRestart1", detail.QuitUITitle_Text, string.Empty);
			BaseUIAnimation.action.SetLanguageFont("QuitUIRestart2", detail.QuitUIQuitbtn_Text, string.Empty);
		}
		if (Singleton<DataManager>.Instance.ChangeSceneType != EnumSceneType.GameScene)
		{
			BaseUIAnimation.action.SetLanguageFont("QuitGame1", detail.QuitUIRemark_Text, string.Empty);
		}
		if (Singleton<DataManager>.Instance.ChangeSceneType == EnumSceneType.GameScene)
		{
			//InitAndroid.action.GAEvent("LevelLog:Exit:" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
		}
		if (InitGame.bEnios)
		{
			detail.QuitUIRemark_Text.resizeTextForBestFit = false;
		}
		CheckClosesound();
		InitAndroid.action.openCenterad();
	}

	public override void OnButton2()
	{
        AdsManager.HideBanner();
        UnityEngine.Debug.Log("OnContinueBtn111111111");
		Singleton<LevelManager>.Instance.bRstart = false;
		if (bcontinue)
		{
			UnityEngine.Debug.Log("OnContinueBtn111111122222221");
			bcontinue = false;
			UI.Instance.ClosePanel();
		}
		FirebaseController.ResumeLevel(state: true);
	}

	public override void OnS_sound()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		if (bSound)
		{
			detail.S_sound_Image.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/setting/setting_btn_sound", 113, 112);
			bSound = false;
			if ((bool)MusicController.action)
			{
				MusicController.action.MusicOFF();
			}
		}
		else
		{
			detail.S_sound_Image.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/setting/setting_btn_sound_1", 113, 112);
			bSound = true;
			if ((bool)MusicController.action)
			{
				MusicController.action.MusicON();
			}
		}
	}

	private void CheckClosesound()
	{
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_MusicSwitch", 1) == 0)
		{
			detail.S_sound_Image.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/setting/setting_btn_sound", 113, 112);
			bSound = false;
		}
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SoundSwitch", 1) == 0)
		{
			detail.S_music_Image.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/setting/setting_btn_effect", 113, 112);
			bMusic = false;
		}
	}

	public override void OnS_music()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		if (bMusic)
		{
			detail.S_music_Image.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/setting/setting_btn_effect", 113, 112);
			bMusic = false;
			if ((bool)SoundController.action)
			{
				SoundController.action.SoundOFF();
			}
		}
		else
		{
			detail.S_music_Image.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/setting/setting_btn_effect_1", 113, 112);
			bMusic = true;
			if ((bool)SoundController.action)
			{
				SoundController.action.SoundON();
			}
		}
	}

	public override void OnButton1()
	{
        AdsManager.HideBanner();
        if (Singleton<DataManager>.Instance.ChangeSceneType != EnumSceneType.GameScene)
		{
			UnityEngine.Debug.Log("OnQuitBtn1jyexit");
			InitAndroid.action.ExityJyTest(1);
			Application.Quit();
			InitAndroid.action.ExityJyTest(2);
			return;
		}
		FirebaseController.RestartLevel(state: true);
		if (Singleton<LevelManager>.Instance.bRstart)
		{
			aliyunlog.LevelLog("rstart");
			Singleton<LevelManager>.Instance.bRstart2 = true;
			Singleton<LevelManager>.Instance.bRstart3 = true;
			Singleton<LevelManager>.Instance.bRstart4 = true;
			Singleton<LevelManager>.Instance.bRstart5 = true;
			Singleton<LevelManager>.Instance.bRstart6 = true;
			Singleton<LevelManager>.Instance.bRstart7 = true;
			UnityEngine.Debug.Log("OnQuitBtn2");
			LoseLog();
			UnityEngine.Debug.Log("OnQuitBtn3");
			bexitExit = true;
			UnityEngine.Debug.Log("OnQuitBtn4");
			UI.Instance.ClosePanel();
		}
		else if (!Singleton<LevelManager>.Instance.bRstart)
		{
			Singleton<LevelManager>.Instance.bRstart7 = true;
			Singleton<LevelManager>.Instance.bRstart6 = true;
			Singleton<LevelManager>.Instance.bRstart5 = false;
			Singleton<LevelManager>.Instance.bRstart2 = false;
			Singleton<LevelManager>.Instance.bRstart3 = true;
			Singleton<LevelManager>.Instance.bRstart4 = false;
			Singleton<LevelManager>.Instance.bExit = true;
			LoseLog();
			bexitExit = true;
			UI.Instance.ClosePanel();
		}
		else
		{
			UI.Instance.ClosePanel();
		}
	}

	public override void OnExit()
	{
		if (bexitExit)
		{
			//if (Singleton<DataManager>.Instance.ChangeSceneType == EnumSceneType.GameScene)
			//{
			//	InitAndroid.action.GAEvent("LevelLog:Exit:" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
			//}
			//InitAndroid.action.GAEvent("clickbtn:exitgame");
			Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
		}
		InitAndroid.action.CloseCenterad();
	}

	public void LoseLog()
	{
		int num = Singleton<UserLevelManager>.Instance.GetUserLevel();
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex < 31)
		{
			num = 0;
		}
		//FireBase.Action.UnityWriteLog("LOG_LevelLose", Singleton<LevelManager>.Instance.iNowSelectLevelIndex + "|" + num);
		//GA.FailLevel(Singleton<LevelManager>.Instance.iNowSelectLevelIndex.ToString());
	}
}
