using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSelectUI : MonoBehaviour
{
	public GameObject ImgSelect;

	public Sprite[] LImgSelect;

	public Sprite[] LImgSelectUse;

	public Sprite UseBg;

	private int iLanguageIndex;

	private void Start()
	{
		base.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	public void SetType(int iIndex)
	{
		iLanguageIndex = iIndex;
		ImgSelect.GetComponent<Image>().sprite = LImgSelect[iIndex];
		int num = 0;
		string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_Language", string.Empty);

		//if (@string == "English")
		//{
			//num = 0;
		//}
		//else if (@string == "French")
		//{
			//num = 1;
		//}
		//else if (@string == "German")
		//{
			//num = 2;
		//}
		//else if (@string == "Spanish")
		//{
			//num = 3;
		//}
		//else if (@string == "Portuguese")
		//{
			//num = 4;
		//}
		//else if (@string == "Russian")
		//{
			//num = 5;
		//}
		//else if (@string == "Simplified_Chinese")
		//{
			//num = 6;
		//}
		//else if (@string == "Traditional_Chinese")
		//{
			//num = 7;
		//}
		//else if (@string == "Thai")
		//{
			//num = 8;
		//}
		//else if (@string == "Japanese")
		//{
			//num = 9;
		//}
		//else if (@string == "Korean")
		//{
			//num = 10;
		//}


		if (@string == "Simplified_Chinese")
		{
			num = 0;
		}
		else if (@string == "English")
		{
			num = 1;
		}
		else if (@string == "French")
		{
			num = 2;
		}
		else if (@string == "German")
		{
			num = 4;
		}
		else if (@string == "Japanese")
		{
			num = 5;
		}
		else if (@string == "Korean")
		{
			num = 3;
		}
		else if (@string == "Spanish")
		{
			num = 6;
		}
		else if (@string == "Portuguese")
		{
			num = 7;
		}
		else if (@string == "Russian")
		{
			num = 8;
		}
		else if (@string == "Thai")
		{
			num = 9;
		}
		if (iIndex == num)
		{
			SetUse();
		}
		if (iIndex == 9 && LanguageUI.action.bDown)
		//if (iIndex == 10 && LanguageUI.action.bDown)
		{
			UnityEngine.Debug.Log("this.transform.parent.name:" + base.transform.parent.name);
			base.transform.parent.transform.localPosition = new Vector3(16f, 95f, 0f);
		}
	}

	public void SetUse()
	{
		GetComponent<Image>().sprite = UseBg;
		ImgSelect.GetComponent<Image>().sprite = LImgSelectUse[iLanguageIndex];
		if (iLanguageIndex > 6)
		//if (iLanguageIndex > 10)
		{
			LanguageUI.action.bDown = true;
		}
		else
		{
			LanguageUI.action.bDown = false;
		}
	}

	private void Update()
	{
	}

	public void ClickSelectLanguage()
	{
		string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_Language", string.Empty);

		//if (iLanguageIndex == 0)
		//{
			//BaseUIAnimation.LanguageTp = LanguageType.English;
		//}
		//if (iLanguageIndex == 1)
		//{
			//BaseUIAnimation.LanguageTp = LanguageType.Simplified_French;
		//}
		//if (iLanguageIndex == 2)
		//{
			//BaseUIAnimation.LanguageTp = LanguageType.Simplified_German;
		//}
		//else if (iLanguageIndex == 3)
		//{
			//BaseUIAnimation.LanguageTp = LanguageType.Simplified_Spanish;
		//}
		//else if (iLanguageIndex == 4)
		//{
			//BaseUIAnimation.LanguageTp = LanguageType.Simplified_Portuguese;
		//}
		//else if (iLanguageIndex == 5)
		//{
			//BaseUIAnimation.LanguageTp = LanguageType.Simplified_Russian;
		//}
		//else if (iLanguageIndex == 6)
		//{
			//BaseUIAnimation.LanguageTp = LanguageType.Simplified_Chinese;
		//}
		//else if (iLanguageIndex == 7)
		//{
			//BaseUIAnimation.LanguageTp = LanguageType.Traditional_Chinese;
		//}
		//else if (iLanguageIndex == 8)
		//{
			//BaseUIAnimation.LanguageTp = LanguageType.Simplified_Thai;
		//}
		//else if (iLanguageIndex == 9)
		//{
			//BaseUIAnimation.LanguageTp = LanguageType.Simplified_Japanese;
		//}
		//else if (iLanguageIndex == 10)
		//{
			//BaseUIAnimation.LanguageTp = LanguageType.Simplified_Korean;
		//}

		if (iLanguageIndex == 0)
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Chinese;
		}
		if (iLanguageIndex == 1)
		{
			BaseUIAnimation.LanguageTp = LanguageType.English;
		}
		if (iLanguageIndex == 2)
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_French;
		}
		else if (iLanguageIndex == 3)
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Korean;
		}
		else if (iLanguageIndex == 4)
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_German;
		}
		else if (iLanguageIndex == 5)
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Japanese;
		}
		else if (iLanguageIndex == 6)
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Spanish;
		}
		else if (iLanguageIndex == 7)
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Portuguese;
		}
		else if (iLanguageIndex == 8)
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Russian;
		}
		else if (iLanguageIndex == 9)
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Thai;
		}
		else if (iLanguageIndex == 10)
		{
			BaseUIAnimation.LanguageTp = LanguageType.Traditional_Chinese;
		}
		if (InitGame.bEnios)
		{
			BaseUIAnimation.LanguageTp = LanguageType.English;
		}
	}

	private IEnumerator IEChangeMap()
	{
		yield return new WaitForSeconds(1f);
		bool b = true;
		while (b)
		{
			yield return new WaitForSeconds(0.1f);
			if (Singleton<DataManager>.Instance.bIELoadLanguageStyle)
			{
				Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
				b = false;
			}
		}
	}
}
