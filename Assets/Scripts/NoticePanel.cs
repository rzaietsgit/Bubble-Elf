using UnityEngine;

public class NoticePanel : NoticePanelBase
{
	public static NoticePanel panel;

	public override void InitUI()
	{
		panel = this;
		LoadSetText();
	}

	private void LoadSetText()
	{
		BaseUIAnimation.action.SetLanguageFont("NoticeText1", detail.Title_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("NoticeText2", detail.OkText_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("NoticeText" + Singleton<DataManager>.Instance.iNoticePanelType, detail.RemarkText_Text, string.Empty);
	}

	public override void OnOkBtn()
	{
		Singleton<DataManager>.Instance.bNoticePanelType = true;
		UI.Instance.ClosePanel();
		UnityEngine.Debug.Log(" NoticePanel OnOkBtn  bNoticePanelType =  " + Singleton<DataManager>.Instance.bNoticePanelType);
	}

	public override void OnExit()
	{
		UnityEngine.Debug.Log(" NoticePanel OnExit DataManager.Instance.bNoticePanelType = " + Singleton<DataManager>.Instance.bNoticePanelType);
		UnityEngine.Debug.Log(" NoticePanel OnExit DataManager.Instance.iNoticePanelType = " + Singleton<DataManager>.Instance.iNoticePanelType);
		if (!Singleton<DataManager>.Instance.bNoticePanelType)
		{
			return;
		}
		Singleton<DataManager>.Instance.bNoticePanelType = false;
		if (Singleton<DataManager>.Instance.iNoticePanelType == 4)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				InitAndroid.action.FileBaseSave();
			}
			if (Application.platform == RuntimePlatform.WindowsEditor)
			{
				UnityEngine.Debug.Log("OnExit firebasereturn iNoticePanelType= " + Singleton<DataManager>.Instance.iNoticePanelType);
				InitAndroid.action.firebasereturn(1);
			}
		}
		if (Singleton<DataManager>.Instance.iNoticePanelType == 7)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				InitAndroid.action.FilebaseLoad();
			}
			if (Application.platform == RuntimePlatform.WindowsEditor)
			{
				InitGame.Action.StartTow();
				Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.InitGame);
			}
		}
	}
}
