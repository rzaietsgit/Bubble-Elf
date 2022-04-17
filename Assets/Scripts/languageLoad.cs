using TMPro;
using UnityEngine;

public class languageLoad : MonoBehaviour
{
	public GameObject SelectImg;

	public int LangType;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void ClickChange()
	{
		//if (LangType == 1)
		//{
			//BaseUIAnimation.LanguageTp = LanguageType.English;
			//BaseUIAnimation.Language = "English";
		//}
		//if (LangType == 2)
		//{
			//BaseUIAnimation.LanguageTp = LanguageType.Simplified_French;
			//BaseUIAnimation.Language = "French";
		//}
		//if (LangType == 3)
		//{
			//BaseUIAnimation.LanguageTp = LanguageType.Simplified_German;
			//BaseUIAnimation.Language = "German";
		//}
		//if (LangType == 4)
		//{
			//BaseUIAnimation.LanguageTp = LanguageType.Simplified_Spanish;
			//BaseUIAnimation.Language = "Spanish";
		//}
		//if (LangType == 5)
		//{
			//BaseUIAnimation.LanguageTp = LanguageType.Simplified_Portuguese;
			//BaseUIAnimation.Language = "Portuguese";
		//}
		//if (LangType == 6)
		//{
			//BaseUIAnimation.LanguageTp = LanguageType.Simplified_Russian;
			//BaseUIAnimation.Language = "Russian";
		//}
		//if (LangType == 7) 
		//{
			//BaseUIAnimation.LanguageTp = LanguageType.Simplified_Chinese;
			//BaseUIAnimation.Language = "Chinese";
		//}
		//if (LangType == 8)
		//{
			//BaseUIAnimation.LanguageTp = LanguageType.Traditional_Chinese;
			//BaseUIAnimation.Language = "Traditional_Chinese";
		//}
		//if (LangType == 9)
		//{
			//BaseUIAnimation.LanguageTp = LanguageType.Simplified_Thai;
			//BaseUIAnimation.Language = "Thai";
		//}
		//if (LangType == 10)
		//{
			//BaseUIAnimation.LanguageTp = LanguageType.Simplified_Japanese;
			//BaseUIAnimation.Language = "Japanese";
		//}
		//if (LangType == 11)
		//{
			//BaseUIAnimation.LanguageTp = LanguageType.Simplified_Korean;
			//BaseUIAnimation.Language = "Korean";
		//}



		if (LangType == 1)
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Korean;
			BaseUIAnimation.Language = "Korean";
		}
		if (LangType == 2)
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Chinese;
			BaseUIAnimation.Language = "Chinese";
		}
		if (LangType == 3)
		{
			BaseUIAnimation.LanguageTp = LanguageType.Traditional_Chinese;
			BaseUIAnimation.Language = "Traditional_Chinese";
		}
		if (LangType == 4)
		{
			BaseUIAnimation.LanguageTp = LanguageType.English;
			BaseUIAnimation.Language = "English";
		}
		if (LangType == 5)
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_German;
			BaseUIAnimation.Language = "German";
		}
		if (LangType == 6)
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Japanese;
			BaseUIAnimation.Language = "Japanese";
		}
		if (LangType == 7)
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_French;
			BaseUIAnimation.Language = "French";
		}
		int languageTp = (int)BaseUIAnimation.LanguageTp;
		TMP_FontAsset language = InitGame.Action.getLanguage(languageTp);
		BaseUIAnimation.action.LoadLanguage(language);
		Singleton<DataManager>.Instance.LoadLanguageStyle();
		SettingPanelUI.bSettingPanelUIOpen = false;
		if ((bool)yuyanPanel.panel)
		{
			yuyanPanel.panel.ResLanguage();
			MapUI.action.resLanguage();
			set1Panel.panel.ResLanguage();
			LanguagepanScript.action.ResLanguage();
			BtnManager.action.Reslanguage();
		}
	}
}
