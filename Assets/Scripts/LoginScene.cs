using System;
using System.Collections;
using EasyMobile;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScene : MonoBehaviour
{
	public static LoginScene action;

	public GameObject StartBtn;

	public Component LoginSceneCamera;

	public GameObject LogImage;

	public Text T_Login_Play;

	public GameObject ChinaAndroidText;

	public GameObject SayObj;

	public Image LogObj;

	public Sprite SpLogEn;

	public Sprite SpLogEngoogle;

	public Text loadingText;

	public Image loadingimg;

	public Text loadingNum;

	public bool InitOver;

	private static string Logstrwww = "LoginScene - ";

	private AsyncOperation operation;

	private int displayProgress;

	private int toProgress;

	public static bool bweb = true;

	private int iTestCount;

	private static int ICdkey;

	private bool bu = true;

	public static int iii;

	public static int icounts;

	private int i = 1;

	private bool isLoadingEnd;

	public void OnClickDown(GameObject go)
	{
	}

	private void TempStart()
	{
	}

	public void twoStart()
	{
		UnityEngine.Debug.Log("Jy LoginScene1");
		StartCoroutine(UPLoadingText());
		if (Application.platform == RuntimePlatform.WindowsEditor)
		{
			Singleton<DataManager>.Instance.bhanguoTestConfig = true;
		}
		UnityEngine.Debug.Log("Jy LoginScene2");
		CheckLanguage();
		InitAndroid.action.GAEvent("StartUIGame");
		UnityEngine.Debug.Log("Jy LoginScene3");
		Singleton<DataManager>.Instance.LocalStaticLoadDataForLogin();
		iTestCount = 1;
		UnityEngine.Debug.Log("Jy LoginScene4");
		BaseUIAnimation.action.SetLanguageFont("Login_Play", T_Login_Play, string.Empty, isMaxFont: false, isLoginData: true);
		BaseUIAnimation.action.CreateButton(StartBtn.gameObject);
		UnityEngine.Debug.Log("Jy LoginScene5");
		if ((bool)LoadingSceneUI.action)
		{
			LoadingSceneUI.action.ChangeCanvasCamera(LoginSceneCamera);
		}
		UnityEngine.Debug.Log("Jy LoginScene6");
		StartCoroutine(AsyncLoading());
		UnityEngine.Debug.Log("Jy LoginScene7");
	}

	public void Start()
	{
		try
		{
			action = this;
			InitOver = true;
			twoStart();
		}
		catch (Exception)
		{
		}
	}

	private IEnumerator AsyncLoading()
	{
		DataManager.iFirstLoginGameLoadCloud = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_FirstLoginGameLoadCloud");
		string sceneName;
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_FirstLoginGame") == 0)
		{
			Singleton<LevelManager>.Instance.iNowSelectLevelIndex = 1;
			Singleton<UserManager>.Instance.EnterLog();
			Singleton<LevelManager>.Instance.bLoadOver = false;
			Singleton<DataManager>.Instance.SaveUserDate("DB_FirstLoginGame", 1);
			Singleton<DataManager>.Instance.SaveUserDate("DB_ThisLoginEnterGame", 0);
			sceneName = "GameScene";
			Singleton<DataManager>.Instance.ChangeSceneType = EnumSceneType.GameScene;
		}
		else
		{
			Singleton<DataManager>.Instance.bLoginGame = true;
			if (InitGame.bChinaVersion)
			{
				OneDayOneSigninUI();
			}
			Singleton<DataManager>.Instance.SaveUserDate("DB_FirstLoginMap", 1);
			sceneName = "MapScene";
			Singleton<DataManager>.Instance.ChangeSceneType = EnumSceneType.MapScene;
		}
		operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
		operation.allowSceneActivation = false;
		while (operation.progress < 0.9f)
		{
			toProgress = (int)operation.progress * 100;
			while (displayProgress < toProgress)
			{
				displayProgress++;
				SetLoadingPercentage(displayProgress);
				yield return new WaitForEndOfFrame();
			}
			yield return new WaitForSeconds(0.1f);
		}
		toProgress = 100;
		while (displayProgress < toProgress)
		{
			displayProgress++;
			SetLoadingPercentage(displayProgress);
			yield return new WaitForEndOfFrame();
		}
		operation.allowSceneActivation = true;
	}

	private void SetLoadingPercentage(int Percentage)
	{
		loadingimg.fillAmount = (float)Percentage / 100f;
		loadingNum.text = Percentage + "%";
	}

	private IEnumerator IEopenUI()
	{
		yield return new WaitForSeconds(0.5f);
	}

	public void LoadLanguage()
	{
		string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_Language", string.Empty);
		if (@string == "Simplified_Chinese")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Chinese;
		}
		else if (@string == "English")
		{
			BaseUIAnimation.LanguageTp = LanguageType.English;
		}
		else if (@string == "French")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_French;
		}
		else if (@string == "German")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_German;
		}
		else if (@string == "Japanese")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Japanese;
		}
		else if (@string == "Korean")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Korean;
		}
		else if (@string == "Spanish")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Spanish;
		}
		else if (@string == "Portuguese")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Portuguese;
		}
		else if (@string == "Russian")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Russian;
		}
		else if (@string == "Thai")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Thai;
		}
		else if (@string == "Traditional_Chinese")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Traditional_Chinese;
		}
	}

	public void CheckLanguage()
	{
		string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_Language", string.Empty);
		if (@string != string.Empty)
		{
			BaseUIAnimation.Language = @string;
			LoadLanguage();
		}
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_FirstLanguage2");
		if (@int != 1)
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_FirstLanguage2", 1);
			switch (Application.systemLanguage)
			{
			case SystemLanguage.Portuguese:
			case SystemLanguage.Russian:
			case SystemLanguage.Spanish:
			case SystemLanguage.Thai:
				break;
			case SystemLanguage.Afrikaans:
				SetLang("af");
				break;
			case SystemLanguage.Arabic:
				SetLang("ar");
				break;
			case SystemLanguage.Basque:
				SetLang("eu");
				break;
			case SystemLanguage.Belarusian:
				SetLang("be");
				break;
			case SystemLanguage.Bulgarian:
				SetLang("bg");
				break;
			case SystemLanguage.Catalan:
				SetLang("ca");
				break;
			case SystemLanguage.Chinese:
				SetLang("Simplified_Chinese");
				break;
			case SystemLanguage.Czech:
				SetLang("cs");
				break;
			case SystemLanguage.Danish:
				SetLang("da");
				break;
			case SystemLanguage.Dutch:
				SetLang("nl");
				break;
			case SystemLanguage.English:
				SetLang("English");
				break;
			case SystemLanguage.Estonian:
				SetLang("et");
				break;
			case SystemLanguage.Faroese:
				SetLang("fo");
				break;
			case SystemLanguage.Finnish:
				SetLang("fu");
				break;
			case SystemLanguage.French:
				SetLang("French");
				break;
			case SystemLanguage.German:
				SetLang("German");
				break;
			case SystemLanguage.Greek:
				SetLang("el");
				break;
			case SystemLanguage.Hebrew:
				SetLang("he");
				break;
			case SystemLanguage.Icelandic:
				SetLang("is");
				break;
			case SystemLanguage.Indonesian:
				SetLang("id");
				break;
			case SystemLanguage.Italian:
				SetLang("it");
				break;
			case SystemLanguage.Japanese:
				SetLang("Japanese");
				break;
			case SystemLanguage.Korean:
				SetLang("Korean");
				break;
			case SystemLanguage.Latvian:
				SetLang("lv");
				break;
			case SystemLanguage.Lithuanian:
				SetLang("lt");
				break;
			case SystemLanguage.Norwegian:
				SetLang("nn");
				break;
			case SystemLanguage.Polish:
				SetLang("pl");
				break;
			case SystemLanguage.Romanian:
				SetLang("ro");
				break;
			case SystemLanguage.SerboCroatian:
				SetLang("sr");
				break;
			case SystemLanguage.Slovak:
				SetLang("sk");
				break;
			case SystemLanguage.Slovenian:
				SetLang("sl");
				break;
			case SystemLanguage.Swedish:
				SetLang("sv");
				break;
			case SystemLanguage.Turkish:
				SetLang("tr");
				break;
			case SystemLanguage.Ukrainian:
				SetLang("uk");
				break;
			case SystemLanguage.Vietnamese:
				SetLang("vi");
				break;
			case SystemLanguage.ChineseSimplified:
				SetLang("Simplified_Chinese");
				break;
			case SystemLanguage.ChineseTraditional:
				SetLang("Traditional_Chinese");
				break;
			case SystemLanguage.Unknown:
				SetLang("English");
				break;
			case SystemLanguage.Hungarian:
				SetLang("hu");
				break;
			}
		}
	}

	public void SetLang(string sLanguage)
	{
		if (sLanguage == "Simplified_Chinese")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Chinese;
		}
		else if (sLanguage == "English")
		{
			BaseUIAnimation.LanguageTp = LanguageType.English;
		}
		else if (sLanguage == "French")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_French;
		}
		else if (sLanguage == "German")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_German;
		}
		else if (sLanguage == "Japanese")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Japanese;
		}
		else if (sLanguage == "Korean")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Korean;
		}
		else if (sLanguage == "Spanish")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Spanish;
		}
		else if (sLanguage == "Portuguese")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Portuguese;
		}
		else if (sLanguage == "Russian")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Russian;
		}
		else if (sLanguage == "Thai")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Simplified_Thai;
		}
		else if (sLanguage == "Traditional_Chinese")
		{
			BaseUIAnimation.LanguageTp = LanguageType.Traditional_Chinese;
		}
		else
		{
			BaseUIAnimation.LanguageTp = LanguageType.English;
		}
		BaseUIAnimation.Language = sLanguage;
	}

	private void WebAutoLogin()
	{
		bweb = false;
	}

	private void InitData()
	{
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ICdkey++;
			if (ICdkey > 150)
			{
				ICdkey = 0;
				UI.Instance.OpenPanel(UIPanelType.cdkeyUI);
				return;
			}
			if (Singleton<DataManager>.Instance.bInitUnityConfig2 && ICdkey > 10)
			{
				ICdkey = 0;
				UI.Instance.OpenPanel(UIPanelType.cdkeyUI);
				return;
			}
		}
		if (Application.platform == RuntimePlatform.Android && UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			UI.Instance.OpenPanel(UIPanelType.ExitUI);
		}
		else if (bu && UnityEngine.Input.GetKey("u"))
		{
			bu = false;
			UI.Instance.OpenPanel(UIPanelType.ExitUI);
		}
		else if (Input.GetMouseButtonDown(0))
		{
			iTestCount++;
			if (iTestCount > 3)
			{
				iTestCount = 0;
			}
		}
	}

	public static GameObject TouchChecker(Vector3 mouseposition)
	{
		if ((bool)Physics2D.OverlapPoint(mouseposition))
		{
			return Physics2D.OverlapPoint(mouseposition).gameObject;
		}
		return null;
	}

	public void ClickButton(int iSwitch)
	{
	}

	public void PlayGame()
	{
		if (!InitGame.Action || !InitGame.bStartFlag)
		{
			return;
		}
		//Analytics.Event("Log_Star");
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(StartBtn.gameObject);
			StartCoroutine(CallPlayGame());
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_InitFistLoginAd1") == 0)
			{
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_InitFistLoginAd1", 1);
			}
			else
			{
                //bool isReady = AdManager.IsInterstitialAdReady();
                //// Show it if it's ready
                //if (isReady)
                //{
                //    AdManager.ShowInterstitialAd();
                //}
            }
		}
	}

	private IEnumerator CallPlayGame(bool bDouble = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		if (!LevelManager.bWwwDataFlag)
		{
			GoGame();
			yield break;
		}
		Singleton<DataManager>.Instance.LoadLanguageStyle();
		yield return new WaitForSeconds(2f);
		InitAndroid.action.GAEvent("clickStartUIGame");
		Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
	}

	public void GoGame()
	{
		DataManager.iFirstLoginGameLoadCloud = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_FirstLoginGameLoadCloud");
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_FirstLoginGame");
		Singleton<DataManager>.Instance.SaveUserDate("DB_FirstLoginMap", 0);
		if (@int == 0)
		{
			Singleton<LevelManager>.Instance.iNowSelectLevelIndex = 1;
			Singleton<UserManager>.Instance.EnterLog();
			Singleton<LevelManager>.Instance.bLoadOver = false;
			Singleton<DataManager>.Instance.SaveUserDate("DB_FirstLoginGame", 1);
			Singleton<DataManager>.Instance.SaveUserDate("DB_ThisLoginEnterGame", 0);
			Singleton<DataManager>.Instance.ChangeSceneType = EnumSceneType.GameScene;
			UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
		}
		else
		{
			Singleton<DataManager>.Instance.bLoginGame = true;
			if (InitGame.bChinaVersion)
			{
				OneDayOneSigninUI();
			}
			Singleton<DataManager>.Instance.SaveUserDate("DB_FirstLoginMap", 1);
			Singleton<DataManager>.Instance.ChangeSceneType = EnumSceneType.MapScene;
			UnityEngine.SceneManagement.SceneManager.LoadScene("MapScene");
		}
	}

	private void OneDayOneSigninUI()
	{
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_ToDayFirstLogin" + Util.GetNowTime_Day()) == 0)
		{
			Singleton<DataManager>.Instance.SaveUserDate("ToDayAutoSingnin", 1);
		}
		Singleton<DataManager>.Instance.SaveUserDate("DB_ToDayFirstLogin" + Util.GetNowTime_Day(), 1);
	}

	private IEnumerator UPLoadingText()
	{
		int index = 0;
		while (!isLoadingEnd)
		{
			yield return new WaitForSeconds(0.5f);
			switch (index)
			{
			case 0:
				loadingText.text = "Loading";
				index = 1;
				break;
			case 1:
				loadingText.text = "Loading.";
				index = 2;
				break;
			case 2:
				loadingText.text = "Loading..";
				index = 3;
				break;
			case 3:
				loadingText.text = "Loading...";
				index = 0;
				break;
			}
		}
	}
}
