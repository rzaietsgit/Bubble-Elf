using UnityEngine;
using UnityEngine.UI;

public class LanguagepanScript : MonoBehaviour
{
	public static LanguagepanScript action;

	public GameObject[] LangArr;

	public Text LangText;

	private void Start()
	{
		action = this;
		ResLanguage();
	}

	public void ResLanguage()
	{
		for (int i = 0; i < 7; i++)
		//for (int i = 0; i < 11; i++)
		{
			LangArr[i].SetActive(value: false);
		}
		BaseUIAnimation.action.SetLanguageFont("SettingsBtn5", LangText, string.Empty);
		string language = BaseUIAnimation.Language;

		//if (language == "English")
		//{
			//LangArr[0].SetActive(value: true);
		//}
		//else if (language == "French")
		//{
			//LangArr[1].SetActive(value: true);
		//}
		//else if (language == "German")
		//{
			//LangArr[2].SetActive(value: true);
		//}
		//else if (language == "Spanish")
		//{
			//LangArr[3].SetActive(value: true);
		//}
		//else if (language == "Portuguese")
		//{
			//LangArr[4].SetActive(value: true);
		//}
		//else if (language == "Russian")
		//{
			//LangArr[5].SetActive(value: true);
		//}
		//else if (language == "Chinese")
		//{
			//LangArr[6].SetActive(value: true);
		//}
		//else if (language == "Traditional_Chinese")
		//{
			//LangArr[7].SetActive(value: true);
		//}
		//else if (language == "Thai")
		//{
			//LangArr[8].SetActive(value: true);
		//}
		//else if (language == "Japanese")
		//{
			//LangArr[9].SetActive(value: true);
		//}
		//else if (language == "Korean")
		//{
			//LangArr[10].SetActive(value: true);
		//}


		if (language == "Korean")
		{
			LangArr[0].SetActive(value: true);
		}
		else if (language == "Simplified_Chinese")
		{
			LangArr[1].SetActive(value: true);
		}
		else if (language == "Traditional_Chinese")
		{
			LangArr[2].SetActive(value: true);
		}
		else if (language == "English")
		{
			LangArr[3].SetActive(value: true);
		}
		else if (language == "German")
		{
			LangArr[4].SetActive(value: true);
		}
		else if (language == "Japanese")
		{
			LangArr[5].SetActive(value: true);
		}
		else if (language == "French")
		{
			LangArr[6].SetActive(value: true);
		}
	}

	private void Update()
	{
	}
}
