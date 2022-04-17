using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseUIAnimation : MonoBehaviour
{
	public delegate string UserFunctionCB(string res);

	public static BaseUIAnimation action;

	public GameObject caidai;

	public GameObject fireTip;

	public GameObject AwardImgObj;

	public GameObject light;

	public GameObject bgmask;

	public static LanguageType LanguageTp;

	public TMP_FontAsset MyTMP_FontAsset;

	public static string Language = "English";

	public static float btnAnimationTime = 0.03f;

	public static bool bClickButton = true;

	private bool bRightPanel = true;

	private static float iLoverTime;

	public bool bbtnDown;

	public string NowAnimationName;

	private bool bComboObj;

	private int propNum;

	private GameObject bgmaskObj;

	public Texture GetGameStyleColorTexture(int index)
	{
		string path = "Img/fontstyle/" + index;
		return (Texture)Resources.Load(path, typeof(Texture));
	}

	public void LoadLanguage(TMP_FontAsset _TMP_FontAsset)
	{
		MyTMP_FontAsset = _TMP_FontAsset;
		if (LanguageTp == LanguageType.Simplified_Chinese)
		{
			Language = "Simplified_Chinese";
		}
		else if (LanguageTp == LanguageType.English)
		{
			Language = "English";
		}
		else if (LanguageTp == LanguageType.Simplified_French)
		{
			Language = "French";
		}
		else if (LanguageTp == LanguageType.Simplified_German)
		{
			Language = "German";
		}
		else if (LanguageTp == LanguageType.Simplified_Japanese)
		{
			Language = "Japanese";
		}
		else if (LanguageTp == LanguageType.Simplified_Korean)
		{
			Language = "Korean";
		}
		else if (LanguageTp == LanguageType.Simplified_Spanish)
		{
			Language = "Spanish";
		}
		else if (LanguageTp == LanguageType.Simplified_Portuguese)
		{
			Language = "Portuguese";
		}
		else if (LanguageTp == LanguageType.Simplified_Russian)
		{
			Language = "Russian";
		}
		else if (LanguageTp == LanguageType.Traditional_Chinese)
		{
			Language = "Traditional_Chinese";
		}
		Singleton<DataManager>.Instance.SaveUserDate("DB_Language", Language);
	}

	public void VerifyLocalPosition(GameObject obj, float x, float y)
	{
		Transform transform = obj.transform;
		Vector3 localPosition = obj.transform.localPosition;
		float x2 = localPosition.x + x;
		Vector3 localPosition2 = obj.transform.localPosition;
		float y2 = localPosition2.y + y;
		Vector3 localPosition3 = obj.transform.localPosition;
		transform.localPosition = new Vector3(x2, y2, localPosition3.z);
	}

	public string GetLanguage(string key)
	{
		if (key == null)
		{
			return string.Empty;
		}
		return Singleton<DataManager>.Instance.dDataLanguage[key][Language];
	}

	public void SetLanguageFont(string key, Text text, string LEVELID = "", bool isMaxFont = false, bool isLoginData = false)
	{
		try
		{
			if (key != null)
			{
				string empty = string.Empty;
				empty = ((!isLoginData) ? Singleton<DataManager>.Instance.dDataLanguage[key][Language] : Singleton<DataManager>.Instance.dLoginDataLanguage[key][Language]);
				empty = empty.Replace("<BR>", "\n");
				if (empty.LastIndexOf("|") >= 0)
				{
					empty = empty.Replace("|", ",");
				}
				if (LEVELID != string.Empty)
				{
					empty = empty.Replace("LEVELID", LEVELID);
				}
				text.text = empty;
				if (isMaxFont)
				{
					string text2 = Singleton<DataManager>.Instance.dDataLanguageStyle[key]["Color"];
					string text3 = Singleton<DataManager>.Instance.dDataLanguageStyle[key]["outColor"];
					string text4 = Singleton<DataManager>.Instance.dDataLanguageStyle[key]["VerifyLocal"];
					float num = float.Parse(Singleton<DataManager>.Instance.dDataLanguageStyle[key]["outline"]);
					int fontSize = int.Parse(Singleton<DataManager>.Instance.dDataLanguageStyle[key]["FontSize"]);
					float num2 = float.Parse(Singleton<DataManager>.Instance.dDataLanguageStyle[key]["ID_FaceDilate"]);
					float num3 = float.Parse(Singleton<DataManager>.Instance.dDataLanguageStyle[key]["ID_OutlineSoftness"]);
					float num4 = float.Parse(Singleton<DataManager>.Instance.dDataLanguageStyle[key]["characterSpacing"]);
					string text5 = Singleton<DataManager>.Instance.dDataLanguageStyle[key]["FaceTexture2D"];
					string[] array = text2.Split('|');
					text.fontSize = fontSize;
					text.alignment = TextAnchor.MiddleCenter;
					text.color = new Color(0f, 0f, 0f, 0f);
					Gradient component = text.GetComponent<Gradient>();
					component.topColor = new Color(float.Parse(array[1].Split('-')[0]) / 255f, float.Parse(array[1].Split('-')[1]) / 255f, float.Parse(array[1].Split('-')[2]) / 255f, 1f);
					component.bottomColor = new Color(float.Parse(array[2].Split('-')[0]) / 255f, float.Parse(array[2].Split('-')[1]) / 255f, float.Parse(array[2].Split('-')[2]) / 255f, 1f);
					if (text.gameObject.GetComponent<Outline>() == null)
					{
						text.gameObject.AddComponent<Outline>();
					}
					text.GetComponent<Outline>().effectDistance = new Vector2(float.Parse(text4.Split('|')[0]), float.Parse(text4.Split('|')[1]));
					text.GetComponent<Outline>().effectColor = new Color(255f, 255f, 255f, 255f);
					if (text.gameObject.GetComponent<ContentSizeFitter>() == null)
					{
						text.gameObject.AddComponent<ContentSizeFitter>();
					}
					text.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
					text.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
				}
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log("Font" + ex + "key = " + key);
		}
	}

	public void SetLanguageFont(string key, TextMeshProUGUI _TextMeshProUGUI, string LEVELID = "")
	{
		try
		{
			if (key != null)
			{
				_TextMeshProUGUI.font = MyTMP_FontAsset;
				string text = Singleton<DataManager>.Instance.dDataLanguage[key][Language];
				text = text.Replace("<BR>", "\n");
				if (text.LastIndexOf("|") >= 0)
				{
					text = text.Replace("|", ",");
				}
				if (LEVELID != string.Empty)
				{
					text = text.Replace("LEVELID", LEVELID);
				}
				string text2 = Singleton<DataManager>.Instance.dDataLanguageStyle[key]["Color"];
				string text3 = Singleton<DataManager>.Instance.dDataLanguageStyle[key]["outColor"];
				string text4 = Singleton<DataManager>.Instance.dDataLanguageStyle[key]["VerifyLocal"];
				float outlineWidth = float.Parse(Singleton<DataManager>.Instance.dDataLanguageStyle[key]["outline"]);
				float fontSize = float.Parse(Singleton<DataManager>.Instance.dDataLanguageStyle[key]["FontSize"]);
				float value = float.Parse(Singleton<DataManager>.Instance.dDataLanguageStyle[key]["ID_FaceDilate"]);
				float value2 = float.Parse(Singleton<DataManager>.Instance.dDataLanguageStyle[key]["ID_OutlineSoftness"]);
				float characterSpacing = float.Parse(Singleton<DataManager>.Instance.dDataLanguageStyle[key]["characterSpacing"]);
				string text5 = Singleton<DataManager>.Instance.dDataLanguageStyle[key]["FaceTexture2D"];
				if (text5 == string.Empty)
				{
					_TextMeshProUGUI.fontBaseMaterial.SetTexture(ShaderUtilities.ID_FaceTex, null);
					_TextMeshProUGUI.fontBaseMaterial.SetFloat(ShaderUtilities.ID_OutlineSoftness, value2);
					_TextMeshProUGUI.fontBaseMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, value);
					_TextMeshProUGUI.outlineColor = new Color(float.Parse(text3.Split('-')[0]) / 255f, float.Parse(text3.Split('-')[1]) / 255f, float.Parse(text3.Split('-')[2]) / 255f, 1f);
					_TextMeshProUGUI.faceColor = new Color(float.Parse(text2.Split('-')[0]) / 255f, float.Parse(text2.Split('-')[1]) / 255f, float.Parse(text2.Split('-')[2]) / 255f, 1f);
					_TextMeshProUGUI.outlineWidth = outlineWidth;
					_TextMeshProUGUI.fontSize = fontSize;
					_TextMeshProUGUI.characterSpacing = characterSpacing;
					_TextMeshProUGUI.fontBaseMaterial.SetTexture(ShaderUtilities.ID_FaceTex, null);
					_TextMeshProUGUI.SetText(text);
				}
				if (text5 != string.Empty)
				{
					_TextMeshProUGUI.fontBaseMaterial.SetTexture(ShaderUtilities.ID_FaceTex, GetGameStyleColorTexture(int.Parse(text5)));
					_TextMeshProUGUI.fontBaseMaterial.SetFloat(ShaderUtilities.ID_OutlineSoftness, value2);
					_TextMeshProUGUI.fontBaseMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, value);
					_TextMeshProUGUI.outlineWidth = outlineWidth;
					_TextMeshProUGUI.fontSize = fontSize;
					_TextMeshProUGUI.characterSpacing = characterSpacing;
					_TextMeshProUGUI.outlineColor = new Color(1f, 1f, 1f, 1f);
					_TextMeshProUGUI.SetText(text);
				}
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log("Font" + ex + "key = " + key);
		}
	}

	public void Start()
	{
		if (!action)
		{
			action = this;
		}
	}

	private void Awake()
	{
		if (action == null)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			action = this;
		}
		else if (action != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void ClickButton(GameObject obj)
	{
		bClickButton = false;
		obj.transform.DOScale(new Vector3(0.7f, 0.7f, 1f), 0.1f);
		obj.transform.DOScale(new Vector3(0.7f, 0.95f, 1f), 0.1167f);
		obj.transform.DOScale(new Vector3(0.8f, 0.7f, 1f), 0.1167f);
		obj.transform.DOScale(new Vector3(0.75f, 0.85f, 1f), 0.083f);
		obj.transform.DOScale(new Vector3(0.8f, 0.8f, 1f), 0.083f);
		obj.transform.DOScale(new Vector3(1f, 1f, 1f), 0.334f).SetEase(Ease.OutBounce);
		StartCoroutine(SetClickState());
	}

	private IEnumerator SetClickState()
	{
		yield return new WaitForSeconds(0.5f);
		bClickButton = true;
	}

	public void OpenSettingPanelUI(GameObject obj, GameObject bgmask)
	{
		Animator component = obj.GetComponent<Animator>();
		component.SetInteger("stateInt", 1);
		OpenSettingPanelUIMask(bgmask);
		Singleton<DataManager>.Instance.bUiIsOpen = true;
		if ((bool)MapUI.action)
		{
			MapUI.action.ResInvitationObj();
		}
		Camera.main.transform.GetComponent<RapidBlurEffect>().enabled = false;
		if ((bool)SettingPanelUI.action)
		{
			Canvas component2 = SettingPanelUI.action.transform.GetComponent<Canvas>();
			component2.renderMode = RenderMode.ScreenSpaceCamera;
		}
		
	}

	public void OpenSettingPanelUIMask(GameObject bgmask)
	{
		bgmask.SetActive(value: true);
	}

	public void CloseSettingPanelUI(GameObject obj)
	{
		Camera.main.transform.GetComponent<RapidBlurEffect>().enabled = false;
		if ((bool)SettingPanelUI.action)
		{
			Canvas component = SettingPanelUI.action.gameObject.transform.GetComponent<Canvas>();
			component.renderMode = RenderMode.ScreenSpaceCamera;
			component.worldCamera = MapUI.action.MapUISceneCamera.GetComponent<Camera>();
		}
		Animator component2 = obj.GetComponent<Animator>();
		component2.SetInteger("stateInt", 2);
		StartCoroutine(IESetState());
		
	}

	private IEnumerator IESetState()
	{
		yield return new WaitForSeconds(0.2f);
		Singleton<DataManager>.Instance.bUiIsOpen = false;
		if ((bool)MapUI.action)
		{
			MapUI.action.ResInvitationObj();
		}
	}

	public void HideSettingBtnUI(GameObject obj)
	{
		bRightPanel = false;
		Animator component = obj.GetComponent<Animator>();
		component.SetInteger("stateInt", 5);
		Singleton<DataManager>.Instance.SaveUserDate("DB_HideSettingBtnUI", 1);
	}

	public void MoveHideSettingBtnUI(GameObject obj)
	{
		Animator component = obj.GetComponent<Animator>();
		if (bRightPanel)
		{
			component.SetInteger("stateInt", 3);
		}
		else
		{
			component.SetInteger("stateInt", 7);
		}
	}

	public void MoveShowSettingBtnUI(GameObject obj)
	{
		Animator component = obj.GetComponent<Animator>();
		if (bRightPanel)
		{
			component.SetInteger("stateInt", 4);
		}
		else
		{
			component.SetInteger("stateInt", 8);
		}
	}

	public void ShowSettingBtnUI(GameObject obj)
	{
		bRightPanel = true;
		Animator component = obj.GetComponent<Animator>();
		component.SetInteger("stateInt", 6);
		Singleton<DataManager>.Instance.SaveUserDate("DB_HideSettingBtnUI", 0);
	}

	public void MapShowSettingBtnUI(GameObject obj)
	{
		Animator component = obj.GetComponent<Animator>();
		int integer = component.GetInteger("stateInt");
		if (integer == 5)
		{
			component.SetInteger("stateInt", 9);
		}
		else
		{
			component.SetInteger("stateInt", 1);
		}
		OpenSettingPanelUIMask(SettingPanelUI.action.transform.Find("mask").gameObject);
		Singleton<DataManager>.Instance.bUiIsOpen = true;
		if ((bool)MapUI.action)
		{
			MapUI.action.ResInvitationObj();
		}
		Camera.main.transform.GetComponent<RapidBlurEffect>().enabled = false;
		if (!SettingPanelUI.action)
		{
		}
	}

	public void ShowDownUI1(GameObject obj)
	{
		if (Singleton<DataManager>.Instance.bGooglePay)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "GooglePayfreeAD");
			if (@int != 1)
			{
				if ((bool)obj && (bool)obj.GetComponent<RectTransform>())
				{
					obj.GetComponent<RectTransform>().DOLocalMoveY(-464f, 0.3f).SetEase(Ease.OutSine)
						.SetDelay(0.5f);
				}
			}
			else if ((bool)obj && (bool)obj.GetComponent<RectTransform>())
			{
				obj.GetComponent<RectTransform>().DOLocalMoveY(-564f, 0.3f).SetEase(Ease.OutSine)
					.SetDelay(0.5f);
			}
		}
		else if ((bool)obj && (bool)obj.GetComponent<RectTransform>())
		{
			obj.GetComponent<RectTransform>().DOLocalMoveY(-564f, 0.3f).SetEase(Ease.OutSine)
				.SetDelay(0.5f);
		}
	}

	public void ShowDownUI2(GameObject obj)
	{
		obj.GetComponent<RectTransform>().DOLocalMoveY(-564f, 0.2f).SetEase(Ease.OutSine);
	}

	public void ShowTopUI(GameObject obj)
	{
		obj.GetComponent<RectTransform>().DOLocalMoveY(576f, 0.2f).SetEase(Ease.OutSine);
	}

	public void HideDownUI1(GameObject obj)
	{
		obj.GetComponent<RectTransform>().DOLocalMoveY(-718f, 0.3f).SetEase(Ease.InSine);
	}

	public void HideDownUI2(GameObject obj)
	{
		obj.GetComponent<RectTransform>().DOLocalMoveY(-718f, 0.2f).SetEase(Ease.InSine);
	}

	public void HideTopUI(GameObject obj)
	{
		obj.GetComponent<RectTransform>().DOLocalMoveY(700f, 0.2f).SetEase(Ease.OutSine);
	}

	public void ShowLeftUI(GameObject obj)
	{
		Animator component = obj.GetComponent<Animator>();
		component.SetInteger("invState", 2);
	}

	public void HideLeftUI(GameObject obj)
	{
		Animator component = obj.GetComponent<Animator>();
		component.SetInteger("invState", 1);
	}

	public void CreateButton(GameObject obj, bool bAdEvent = true)
	{
		ButtonPingpong(obj);
		AddBtnEventTrigger(obj);
	}

	public void AddBtnEventTrigger(GameObject obj)
	{
		Button component = obj.GetComponent<Button>();
		if (component != null)
		{
		}
		EventTrigger component2 = obj.GetComponent<EventTrigger>();
		if (component2 == null)
		{
			obj.AddComponent<EventTrigger>();
			component2 = obj.GetComponent<EventTrigger>();
		}
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerExit;
		entry.callback.AddListener(delegate
		{
			ButtonUp(obj);
		});
		component2.triggers.Add(entry);
		EventTrigger.Entry entry2 = new EventTrigger.Entry();
		entry2.eventID = EventTriggerType.PointerDown;
		entry2.callback.AddListener(delegate
		{
			ButtonDown(obj);
		});
		component2.triggers.Add(entry2);
	}

	public void HideLoadingSceneUI(GameObject obj)
	{
		obj.SetActive(value: false);
	}

	public void ShowLoadingSceneUI(GameObject obj)
	{
		obj.SetActive(value: true);
	}

	public void StartAnimation(GameObject obj)
	{
		obj.transform.Find("mask").gameObject.SetActive(value: true);
		RectTransform component = obj.transform.Find("Panel").GetComponent<RectTransform>();
		component.localPosition = new Vector2(0f, 1500f);
		component.DOLocalMoveY(-50f, 0.2f).SetEase(Ease.OutSine);
		component.DOLocalMoveY(0f, 0.1f).SetEase(Ease.InSine).SetDelay(0.2f);
	}

	public void ReleaseAnimation(GameObject obj, EnumUIType enumUIType, bool bDouble = false, bool bOpenOther = false, bool bChangeScenes = false)
	{
		Animator component = obj.transform.Find("Panel").GetComponent<Animator>();
		component.SetInteger("stateInt", 2);
		if (enumUIType == EnumUIType.MapRewardUI)
		{
		}
		StartCoroutine(IEEnd(enumUIType, bDouble, bOpenOther, bChangeScenes));
		if (obj.name.LastIndexOf("Guide") >= 0 || obj.name.LastIndexOf("Read") >= 0 || obj.name.LastIndexOf("Win") >= 0)
		{
			obj.transform.Find("mask").gameObject.SetActive(value: false);
			return;
		}
		Camera.main.transform.GetComponent<RapidBlurEffect>().enabled = false;
		StartCoroutine(maskFalse(obj));
	}

	public void HieMohu()
	{
		Camera.main.transform.GetComponent<RapidBlurEffect>().enabled = false;
	}

	private IEnumerator maskFalse(GameObject obj)
	{
		yield return new WaitForSeconds(0.1f);
		try
		{
			obj.transform.Find("mask").gameObject.SetActive(value: false);
		}
		catch (Exception)
		{
		}
	}

	private IEnumerator IEEnd(EnumUIType enumUIType, bool bDouble = false, bool bOpenOther = false, bool bChangeScenes = false)
	{
		float fTime = 0.5f;
		if (enumUIType == EnumUIType.PlayUI || enumUIType == EnumUIType.LoseUI || enumUIType == EnumUIType.WinUI)
		{
			fTime = 0.8f;
		}
		yield return new WaitForSeconds(fTime);
		Singleton<UIManager>.Instance.CloseUI(enumUIType, bDouble);
		if (Singleton<DataManager>.Instance.bBuyLiveSale)
		{
			Singleton<DataManager>.Instance.bBuyLiveSale = false;
			if (Singleton<DataManager>.Instance.EBuyLiveSale != EnumUIType.None)
			{
				Singleton<UIManager>.Instance.OpenUI(Singleton<DataManager>.Instance.EBuyLiveSale);
				Singleton<DataManager>.Instance.EBuyLiveSale = EnumUIType.None;
				yield break;
			}
		}
		if (Singleton<DataManager>.Instance.ChangeSceneType == EnumSceneType.GameScene)
		{
			if (enumUIType == EnumUIType.LoseUI && Singleton<UIManager>.Instance.OtherOpenUI == EnumUIType.BuyLivesChinaUI)
			{
				UI.Instance.OpenPanel(UIPanelType.BuyLivesChinaUI);
				yield break;
			}
			if (enumUIType == EnumUIType.BuyLivesChinaUI)
			{
				if (Singleton<DataManager>.Instance.bexitGameScene)
				{
					Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
				}
				else
				{
					Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.GameScene);
				}
				yield break;
			}
		}
		if (enumUIType == EnumUIType.PlayUI && Singleton<DataManager>.Instance.bplayExitMap && Singleton<DataManager>.Instance.ChangeSceneType == EnumSceneType.GameScene)
		{
			Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
		}
		if (enumUIType == EnumUIType.ChinaShopUI && Singleton<DataManager>.Instance.bAutoPlayGame)
		{
			Singleton<UIManager>.Instance.OpenUI(EnumUIType.PlayUI);
			yield break;
		}
		if (Singleton<DataManager>.Instance.bChinaShopUIopen1 && enumUIType == EnumUIType.BuySkillUI)
		{
			Singleton<DataManager>.Instance.bChinaShopUIopen1 = false;
			Singleton<DataManager>.Instance.bopenBuySkillUI1 = true;
			Singleton<UIManager>.Instance.OpenUI(EnumUIType.ChinaShopUI);
			yield break;
		}
		if (enumUIType == EnumUIType.PlayUI && Singleton<DataManager>.Instance.bopenBuySkillUI)
		{
			Singleton<DataManager>.Instance.bopenBuySkillUI = false;
			UI.Instance.OpenPanel(UIPanelType.BuySkillUI);
			yield break;
		}
		if (enumUIType == EnumUIType.BuySkillUI && Singleton<DataManager>.Instance.bOpenplay1)
		{
			Singleton<DataManager>.Instance.bOpenplay1 = false;
			Singleton<UIManager>.Instance.OpenUI(EnumUIType.PlayUI);
			yield break;
		}
		if (enumUIType == EnumUIType.NewTaskUI && Singleton<DataManager>.Instance.bNewTaskOpenPlay)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
			Singleton<LevelManager>.Instance.iNowSelectLevelIndex = @int + 1;
			Singleton<UIManager>.Instance.OpenUI(EnumUIType.PlayUI);
			yield break;
		}
		if (enumUIType == EnumUIType.NewTask1UI && (bool)OpenScript.actionrenwu1)
		{
			OpenScript.actionrenwu1.Resrenwuhongdian();
		}
		if (enumUIType == EnumUIType.MapRewardUI && Singleton<DataManager>.Instance.bLevel3OpenPlay)
		{
			Singleton<UIManager>.Instance.OpenUI(EnumUIType.PlayUI);
			yield break;
		}
		if (enumUIType == EnumUIType.SignRewardUI)
		{
			if (Singleton<DataManager>.Instance.bSigninUICloseOpen && !InitGame.bCloseLBForEnIos)
			{
				Singleton<DataManager>.Instance.bSigninUICloseOpen = false;
				UI.Instance.OpenPanel(UIPanelType.SaleAdUI);
				yield break;
			}
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Sign7State") == 0 && !InitGame.bEnios && Util.CheckOnline())
			{
				string nowTime_Day = Util.GetNowTime_Day();
				if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SignOK7" + nowTime_Day) == 0 && !InitGame.bHideSign7Task)
				{
					InitAndroid.action.GAEvent("clickbtn:AutoShowSign7");
					UI.Instance.OpenPanel(UIPanelType.SignReward7UI);
					yield break;
				}
			}
		}
		if (Singleton<DataManager>.Instance.bCloseSaleOpenPlayUI)
		{
			switch (enumUIType)
			{
			case EnumUIType.SaleAdUI:
				if (DataManager.sale_adKey == "Pay2" && Singleton<DataManager>.Instance.bSaleAdPay2)
				{
					Singleton<UIManager>.Instance.OpenUI(EnumUIType.ChinaShopUI);
					yield break;
				}
				Singleton<DataManager>.Instance.bCloseSaleOpenPlayUI = false;
				Singleton<UIManager>.Instance.OpenUI(EnumUIType.PlayUI);
				yield break;
			case EnumUIType.PlayUI:
				if (!InitGame.bCloseLBForEnIos)
				{
					UI.Instance.OpenPanel(UIPanelType.SaleAdUI);
					yield break;
				}
				break;
			}
		}
		if (Singleton<DataManager>.Instance.bClosePlayUIOpenSale && !InitGame.bCloseLBForEnIos)
		{
			Singleton<DataManager>.Instance.bClosePlayUIOpenSale = false;
			UI.Instance.OpenPanel(UIPanelType.SaleAdUI);
			yield break;
		}
		if (bOpenOther)
		{
			if (Singleton<UIManager>.Instance.OtherOpenUI == EnumUIType.LoseUI)
			{
				yield return new WaitForSeconds(0.1f);
			}
			Singleton<UIManager>.Instance.OpenUI(Singleton<UIManager>.Instance.OtherOpenUI);
			yield break;
		}
		if (bChangeScenes)
		{
		}
		if (enumUIType == EnumUIType.WinUI)
		{
		}
		if (enumUIType == EnumUIType.RateUsUI)
		{
			yield return new WaitForSeconds(1f);
			Singleton<UIManager>.Instance.OpenUI(EnumUIType.WinUI);
		}
		if (enumUIType == EnumUIType.TipWinUI)
		{
			BubbleSpawner.Instance.WinFallBubble();
		}
		if (enumUIType == EnumUIType.ReadyGoUI)
		{
			PassLevel.action.bGameStart = true;
			GameUI.action.InitUserSkillAni();
			GameGuide.Instance.initGuide();
		}
		if (enumUIType == EnumUIType.SkillTipUI || enumUIType == EnumUIType.FaceBookRankOpenUI)
		{
			Singleton<UIManager>.Instance.OpenUI(EnumUIType.WinUI);
		}
		if (enumUIType == EnumUIType.BuyLivesUI && Singleton<DataManager>.Instance.ChangeSceneType == EnumSceneType.GameScene)
		{
			yield return new WaitForSeconds(0.1f);
			Singleton<UIManager>.Instance.OpenUI(EnumUIType.PlayUI);
		}
		if (enumUIType == EnumUIType.QuitUI && Singleton<DataManager>.Instance.ChangeSceneType == EnumSceneType.GameScene && Singleton<LevelManager>.Instance.bRstart)
		{
			if (InitGame.bChinaVersion)
			{
				Singleton<LevelManager>.Instance.iFailure++;
			}
			Singleton<UIManager>.Instance.OpenUI(EnumUIType.PlayUI);
		}
		if (enumUIType == EnumUIType.RewardBtnUI && Singleton<DataManager>.Instance.NextOpenUI != EnumUIType.None)
		{
			Singleton<UIManager>.Instance.OpenUI(Singleton<DataManager>.Instance.NextOpenUI);
			Singleton<DataManager>.Instance.NextOpenUI = EnumUIType.None;
		}
		if (enumUIType == EnumUIType.cdkeyUI && Singleton<DataManager>.Instance.bcdkeyReward)
		{
			Singleton<DataManager>.Instance.bcdkeyReward = false;
			Singleton<UIManager>.Instance.OpenUI(EnumUIType.cdKeyRewardUI);
			Singleton<DataManager>.Instance.NextOpenUI = EnumUIType.None;
		}
		if (enumUIType == EnumUIType.SaleAdUI && DataManager.sale_adKey == "SaleAdUILoginReward")
		{
			Singleton<UIManager>.Instance.OpenUI(EnumUIType.PlayUI);
		}
		if ((enumUIType != EnumUIType.ChinaShopUI && enumUIType != EnumUIType.SaleAdUI) || bOpenOther || (bool)MapUI.action)
		{
		}
		if (!InitGame.bChinaVersion)
		{
			yield break;
		}
		if (enumUIType == EnumUIType.SaleAdUI)
		{
			if (DataManager.sale_adKey == "Bubble_LB4" && !Singleton<DataManager>.Instance.bBuyLB)
			{
				UI.Instance.OpenPanel(UIPanelType.BuyLivesChinaUI);
			}
			if (DataManager.sale_adKey == "Bubble_LB2" && (bool)GameUI.action)
			{
				Singleton<UIManager>.Instance.OpenUI(EnumUIType.TipFailUI);
			}
		}
		if (enumUIType == EnumUIType.BuyLivesChinaUI && Singleton<DataManager>.Instance.OpenPlayForLive == EnumUIType.PlayUI && !ChinaShopUI.action)
		{
			Singleton<UIManager>.Instance.OpenUI(EnumUIType.PlayUI);
		}
	}

	public void BubbleScoreNumber(GameObject obj, bool isMuTong)
	{
		if (isMuTong)
		{
			obj.transform.GetComponent<SpriteRenderer>().DOFade(0f, 1f).SetEase(Ease.OutSine)
				.SetDelay(0.39f);
		}
		else
		{
			obj.transform.GetComponent<SpriteRenderer>().DOFade(0f, 1f).SetEase(Ease.OutSine)
				.SetDelay(0.69f);
		}
	}

	public void BubbleScore(GameObject obj, bool isMutong)
	{
		if (isMutong)
		{
			Transform transform = obj.transform;
			Vector3 localPosition = obj.transform.localPosition;
			float x = localPosition.x;
			Vector3 localPosition2 = obj.transform.localPosition;
			transform.localPosition = new Vector3(x, -3.75f, localPosition2.z);
			Vector3 vector = obj.transform.localPosition + new Vector3(0f, 4f, 0f);
			obj.transform.localScale = new Vector3(0f, 0f, 0f);
			Sequence s = DOTween.Sequence();
			s.Append(obj.transform.DOScale(new Vector2(0.5f, 2f), 0.08f).SetEase(Ease.InSine).SetDelay(0.05f)).Append(obj.transform.DOScale(new Vector2(1.2f, 0.9f), 0.08f).SetEase(Ease.OutSine)).Append(obj.transform.DOScale(new Vector2(1f, 1f), 0.03f).SetEase(Ease.OutSine))
				.Append(obj.transform.DOLocalMoveY(-3.25f, 1f).SetEase(Ease.InSine).SetDelay(0.15f))
				.OnComplete(delegate
				{
					DescObj(obj);
				});
		}
		else
		{
			Vector3 vector2 = obj.transform.localPosition + new Vector3(0f, 5f, 0f);
			obj.transform.localScale = new Vector3(0f, 0f, 0f);
			Sequence s2 = DOTween.Sequence();
			Sequence s3 = s2.Append(obj.transform.DOScale(new Vector2(0.5f, 2f), 0.08f).SetEase(Ease.InSine).SetDelay(0.35f)).Append(obj.transform.DOScale(new Vector2(1.2f, 0.9f), 0.08f).SetEase(Ease.OutSine)).Append(obj.transform.DOScale(new Vector2(1f, 1f), 0.03f).SetEase(Ease.OutSine));
			Transform transform2 = obj.transform;
			Vector3 localPosition3 = obj.transform.localPosition;
			s3.Append(transform2.DOLocalMoveY(localPosition3.y + 0.5f, 1f).SetEase(Ease.InSine).SetDelay(0.15f)).OnComplete(delegate
			{
				DescObj(obj);
			});
		}
	}

	private void DescObj(GameObject obj)
	{
	}

	public void HidePauseUI(GameObject obj)
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		obj.transform.Find("mask").gameObject.SetActive(value: false);
		Animator component = obj.GetComponent<Animator>();
		component.SetInteger("stateInt", 2);
		StartCoroutine(SetPauseState());
		StartCoroutine(QuitGame());
		Camera.main.transform.GetComponent<RapidBlurEffect>().enabled = false;
	}

	private IEnumerator SetPauseState()
	{
		yield return new WaitForSeconds(0.01f);
		Singleton<DataManager>.Instance.bUiIsOpen = false;
		if ((bool)MapUI.action)
		{
			MapUI.action.ResInvitationObj();
		}
		yield return new WaitForSeconds(0.3f);
		if ((bool)PauseUI.action)
		{
			PauseUI.action.bPause = false;
		}
	}

	private IEnumerator QuitGame()
	{
		UnityEngine.Debug.Log("ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ3333333333");
		if (BubbleSpawner.IsWait)
		{
			yield return new WaitForSeconds(0.1f);
			if ((bool)PauseUI.action && PauseUI.action.bQuit)
			{
				UI.Instance.OpenPanel(UIPanelType.QuitUI);
			}
		}
	}

	public void ShowPauseUI(GameObject obj)
	{
		Singleton<DataManager>.Instance.bUiIsOpen = true;
		if ((bool)MapUI.action)
		{
			MapUI.action.ResInvitationObj();
		}
		obj.transform.Find("mask").gameObject.SetActive(value: true);
		Animator component = obj.GetComponent<Animator>();
		component.SetInteger("stateInt", 1);
		Camera.main.transform.GetComponent<RapidBlurEffect>().enabled = false;
		Canvas component2 = PauseUI.action.transform.parent.GetComponent<Canvas>();
		component2.renderMode = RenderMode.ScreenSpaceOverlay;
	}

	public void MubiaoIconAni(GameObject obj)
	{
		obj.transform.DOScale(new Vector2(1.6f, 1.6f), 0f).SetEase(Ease.InOutSine);
		Sequence s = DOTween.Sequence();
		s.Append(obj.transform.DOScale(new Vector2(2f, 2f), 0.13f).SetEase(Ease.InOutSine)).Append(obj.transform.DOScale(new Vector2(0.5f, 0.5f), 0.18f).SetEase(Ease.OutSine)).Append(obj.transform.DOScale(new Vector2(1f, 1f), 0.18f).SetEase(Ease.OutSine));
	}

	public void MubiaoIconAniFly2(GameObject obj)
	{
		obj.transform.DOScale(new Vector2(1.6f, 1.6f), 0f).SetEase(Ease.InOutSine);
		Sequence s = DOTween.Sequence();
		s.Append(obj.transform.DOScale(new Vector2(2f, 2f), 0.13f).SetEase(Ease.InOutSine)).Append(obj.transform.DOScale(new Vector2(0.5f, 0.5f), 0.18f).SetEase(Ease.OutSine)).Append(obj.transform.DOScale(new Vector2(0.7f, 0.7f), 0.18f).SetEase(Ease.OutSine));
	}

	public void BuyLove(GameObject starObj, bool bover = false)
	{
		if (iLoverTime == 0f)
		{
			iLoverTime = 0.01f;
		}
		else
		{
			iLoverTime += 0.3f;
		}
		StartCoroutine(IEBuyLove(starObj, iLoverTime, bover));
	}

	private IEnumerator IEBuyLove(GameObject starObj, float time, bool bover = false)
	{
		yield return new WaitForSeconds(time);
		if (starObj != null)
		{
			starObj.SetActive(value: true);
			Sequence s = DOTween.Sequence();
			starObj.transform.DOLocalMoveY(10f, 0.2f).SetEase(Ease.InOutSine);
			starObj.transform.DOLocalMoveY(0f, 0.2f).SetEase(Ease.InSine).SetDelay(0.2f);
			s.Append(starObj.transform.DOScale(new Vector2(0f, 0f), 0f).SetEase(Ease.InOutSine)).Append(starObj.transform.DOScale(new Vector2(1.5f, 1.5f), 0.2f).SetEase(Ease.OutSine)).Append(starObj.transform.DOScale(new Vector2(0.7f, 0.7f), 0.18f).SetEase(Ease.InSine))
				.Append(starObj.transform.DOScale(new Vector2(1f, 1f), 0.18f).SetEase(Ease.OutSine));
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("ui_life", NowPlay: true);
			}
		}
		if (bover)
		{
			yield return new WaitForSeconds(0.65f);
			iLoverTime = 0f;
			BuyLivesUI.action.CloseLivesUI();
		}
	}

	public void ButtonDown(GameObject obj)
	{
		bbtnDown = true;
		obj.transform.DOPause();
		obj.transform.localScale = new Vector3(1f, 1f, 1f);
		obj.transform.DOScale(new Vector3(0.814f, 0.814f, 1f), 0.122f).SetEase(Ease.OutSine);
		obj.transform.DOScale(new Vector3(0.9f, 0.9f, 1f), 0.087f).SetEase(Ease.InSine).SetDelay(0.122f);
		obj.transform.DOScale(new Vector3(0.84f, 0.84f, 1f), 0.08f).SetEase(Ease.OutSine).SetDelay(0.209f);
		obj.transform.DOScale(new Vector3(0.884f, 0.884f, 1f), 0.08f).SetEase(Ease.InSine).SetDelay(0.289f);
		obj.transform.DOScale(new Vector3(0.854f, 0.854f, 1f), 0.08f).SetEase(Ease.OutSine).SetDelay(0.369f);
		obj.transform.DOScale(new Vector3(0.884f, 0.884f, 1f), 0.08f).SetEase(Ease.InSine).SetDelay(0.449f);
		obj.transform.DOScale(new Vector3(0.87f, 0.87f, 1f), 0.08f).SetEase(Ease.OutSine).SetDelay(0.529f);
		obj.transform.DOScale(new Vector3(0.884f, 0.884f, 1f), 0.08f).SetEase(Ease.InSine).SetDelay(0.609f)
			.OnComplete(delegate
			{
				ButtonPingpong2(obj);
			});
	}

	public void ButtonUp(GameObject obj)
	{
		if (bbtnDown)
		{
			bbtnDown = false;
			obj.transform.DOPause();
			obj.transform.DOScale(new Vector3(1.13f, 1.13f, 1f), 0.08f).SetEase(Ease.OutSine);
			obj.transform.DOScale(new Vector3(0.9f, 0.9f, 1f), 0.08f).SetEase(Ease.InSine).SetDelay(0.08f);
			obj.transform.DOScale(new Vector3(1f, 1f, 1f), 0.08f).SetEase(Ease.OutSine).SetDelay(0.16f)
				.OnComplete(delegate
				{
					ButtonPingpong(obj);
				});
			ResetButton(obj);
		}
	}

	private void ResetButton(GameObject obj)
	{
		obj.transform.DOScale(new Vector3(1f, 1f, 1f), 0.08f).SetEase(Ease.OutSine).SetDelay(1.3f)
			.OnComplete(delegate
			{
				ButtonPingpong(obj);
			});
	}

	private void ButtonPingpong2(GameObject obj)
	{
		if (!bbtnDown)
		{
			return;
		}
		obj.transform.DOPause();
		if (!bbtnDown)
		{
			return;
		}
		obj.transform.DOScale(new Vector3(0.82f, 0.84f, 1f), 1f).SetEase(Ease.InOutSine);
		if (bbtnDown)
		{
			obj.transform.DOScale(new Vector3(0.84f, 0.82f, 1f), 1f).SetEase(Ease.InOutSine).SetDelay(1f)
				.OnComplete(delegate
				{
					ButtonPingpong2(obj);
				});
			if (bbtnDown)
			{
			}
		}
	}

	private void ButtonPingpong(GameObject obj)
	{
		obj.transform.DOScale(new Vector3(0.98f, 1.02f, 1f), 1f).SetEase(Ease.InOutSine);
		obj.transform.DOScale(new Vector3(1.02f, 0.98f, 1f), 1f).SetEase(Ease.InOutSine).SetDelay(1f)
			.OnComplete(delegate
			{
				ButtonPingpong(obj);
			});
	}

	public void CreateButtonEvent(GameObject obj)
	{
	}

	public void ComboMoveAni(GameObject ComboObj)
	{
		if (!bComboObj)
		{
			bComboObj = true;
			Vector3 localPosition = ComboObj.transform.localPosition;
			float x = localPosition.x;
			ComboObj.SetActive(value: true);
			ComboObj.transform.DOLocalMoveX(x - 252f, 0.2f);
			ComboObj.transform.DOLocalMoveX(x - 242f, 0.1f).SetDelay(0.2f);
			ComboObj.transform.DOLocalMoveX(x, 0.2f).SetDelay(1f).OnComplete(delegate
			{
				HideCombo(ComboObj);
			});
		}
	}

	public void HideCombo(GameObject obj)
	{
		obj.SetActive(value: false);
		bComboObj = false;
	}

	public void ShowTipBubbleObj(GameObject obj, GameObject NumberObj)
	{
		Vector3 localPosition = obj.transform.localPosition;
		float x = localPosition.x;
		obj.SetActive(value: true);
		number10(NumberObj);
		obj.transform.DOLocalMoveX(x + 326f, 0.2f);
		obj.transform.DOLocalMoveX(x + 316f, 0.1f).SetDelay(0.2f);
		obj.transform.DOLocalMoveX(x, 0.2f).SetDelay(2.5f).OnComplete(delegate
		{
			HideTipBubbleObj(obj);
		});
	}

	private void number10(GameObject NumberObj)
	{
		NumberObj.transform.DOScale(new Vector3(1.5f, 1.5f, 1f), 0.3f).SetEase(Ease.InOutSine);
		NumberObj.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f).SetEase(Ease.InOutSine).SetDelay(0.3f)
			.OnComplete(delegate
			{
				number10(NumberObj);
			});
	}

	public void HideTipBubbleObj(GameObject obj)
	{
		obj.SetActive(value: false);
	}

	public void ShowGameText(int iType, GameObject objText)
	{
		objText.SetActive(value: true);
		switch (iType)
		{
		case 3:
		case 4:
		case 5:
		case 6:
		{
			objText.SetActive(value: false);
			objText.transform.localScale = new Vector3(0f, 0f, 0f);
			objText.SetActive(value: true);
			Sequence s5 = DOTween.Sequence();
			Sequence s6 = s5.Append(objText.transform.DOScale(new Vector2(0.5f, 2f), 0.08f).SetEase(Ease.InSine).SetDelay(0.35f)).Append(objText.transform.DOScale(new Vector2(1.2f, 0.9f), 0.08f).SetEase(Ease.OutSine)).Append(objText.transform.DOScale(new Vector2(1f, 1f), 0.03f).SetEase(Ease.OutSine));
			Transform transform2 = objText.transform;
			Vector3 localPosition3 = objText.transform.localPosition;
			s6.Append(transform2.DOLocalMoveY(localPosition3.y + 0.5f, 1.5f).SetEase(Ease.InSine).SetDelay(0.15f));
			StartCoroutine(IEColor(objText.GetComponent<Text>(), 1f, objText, iType));
			break;
		}
		case 7:
		case 8:
		{
			objText.SetActive(value: false);
			objText.transform.localScale = new Vector3(0.5f, 0.8f, 0f);
			objText.SetActive(value: true);
			Sequence s4 = DOTween.Sequence();
			s4.Append(objText.transform.DOScale(new Vector2(0.8f, 1.6f), 0.3f).SetEase(Ease.InOutSine)).Append(objText.transform.DOScale(new Vector2(1.2f, 0.9f), 0.2f).SetEase(Ease.InOutSine)).Append(objText.transform.DOScale(new Vector2(0.7f, 1.2f), 0.1f).SetEase(Ease.InSine))
				.Append(objText.transform.DOScale(new Vector2(1f, 1f), 0.1f).SetEase(Ease.OutSine));
			StartCoroutine(IEColor(objText.GetComponent<Text>(), 1.5f, objText, iType));
			break;
		}
		case 9:
		case 10:
		{
			objText.SetActive(value: false);
			objText.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
			objText.SetActive(value: true);
			objText.transform.DOLocalMoveY(0.3f, 1.5f).SetEase(Ease.InSine).SetDelay(0.5f);
			Sequence s3 = DOTween.Sequence();
			s3.Append(objText.transform.DOScale(new Vector2(0.5f, 2f), 0.1f).SetEase(Ease.InSine)).Append(objText.transform.DOScale(new Vector2(1.2f, 0.9f), 0.15f).SetEase(Ease.OutSine)).Append(objText.transform.DOScale(new Vector2(1f, 1f), 0.1f).SetEase(Ease.OutSine));
			StartCoroutine(IEColor(objText.GetComponent<Text>(), 1.5f, objText, iType));
			break;
		}
		case 1:
		{
			objText.SetActive(value: false);
			objText.transform.localScale = new Vector3(0.5f, 0.8f, 0f);
			objText.SetActive(value: true);
			GameObject gameObject2 = UnityEngine.Object.Instantiate(fireTip);
			gameObject2.transform.SetParent(objText.transform.parent);
			gameObject2.transform.localPosition = objText.transform.localPosition;
			gameObject2.transform.SetParent(GameUI.action.transform.parent);
			UnityEngine.Object.Destroy(gameObject2, 5f);
			Sequence s2 = DOTween.Sequence();
			s2.Append(objText.transform.DOScale(new Vector2(0.8f, 1.6f), 0.3f).SetEase(Ease.InOutSine)).Append(objText.transform.DOScale(new Vector2(1.2f, 0.9f), 0.2f).SetEase(Ease.InOutSine)).Append(objText.transform.DOScale(new Vector2(0.7f, 1.2f), 0.1f).SetEase(Ease.InSine))
				.Append(objText.transform.DOScale(new Vector2(1f, 1f), 0.1f).SetEase(Ease.OutSine));
			StartCoroutine(IEColor(objText.GetComponent<Text>(), 1.5f, objText, iType));
			break;
		}
		case 2:
		{
			objText.SetActive(value: false);
			objText.transform.localScale = new Vector3(0.5f, 0.8f, 0f);
			objText.SetActive(value: true);
			GameObject gameObject = UnityEngine.Object.Instantiate(caidai);
			gameObject.transform.SetParent(GameUI.action.transform);
			Transform transform = gameObject.transform;
			Vector3 localPosition = objText.transform.localPosition;
			float x = localPosition.x;
			Vector3 localPosition2 = objText.transform.localPosition;
			transform.localPosition = new Vector3(x, localPosition2.y, 2600f);
			UnityEngine.Object.Destroy(gameObject, 5f);
			Sequence s = DOTween.Sequence();
			s.Append(objText.transform.DOScale(new Vector2(0.8f, 1.6f), 0.3f).SetEase(Ease.InOutSine)).Append(objText.transform.DOScale(new Vector2(1.2f, 0.9f), 0.2f).SetEase(Ease.InOutSine)).Append(objText.transform.DOScale(new Vector2(0.7f, 1.2f), 0.1f).SetEase(Ease.InSine))
				.Append(objText.transform.DOScale(new Vector2(1f, 1f), 0.1f).SetEase(Ease.OutSine));
			StartCoroutine(IEColor(objText.GetComponent<Text>(), 1.5f, objText, iType));
			break;
		}
		default:
			objText.transform.DOScale(new Vector3(0.8f, 0.8f, 1f), 5f).SetEase(Ease.InOutSine).OnComplete(delegate
			{
				HideShowGameText(objText);
			});
			break;
		}
	}

	private IEnumerator IEColor(Text _Text, float time, GameObject obj, int iTypeID)
	{
		yield return new WaitForSeconds(time);
		float fColor = 1f;
		string _Color = Singleton<DataManager>.Instance.dDataLanguageStyle["KillBubble" + iTypeID]["Color"];
		string[] LColor = _Color.Split('|');
		Gradient g = _Text.GetComponent<Gradient>();
		while (fColor > 0f)
		{
			fColor -= 0.03f;
			g.topColor = new Color(float.Parse(LColor[1].Split('-')[0]) / 255f, float.Parse(LColor[1].Split('-')[1]) / 255f, float.Parse(LColor[1].Split('-')[2]) / 255f, fColor);
			g.bottomColor = new Color(float.Parse(LColor[2].Split('-')[0]) / 255f, float.Parse(LColor[2].Split('-')[1]) / 255f, float.Parse(LColor[2].Split('-')[2]) / 255f, fColor);
			_Text.GetComponent<Outline>().effectColor = new Color(255f, 255f, 255f, fColor);
			yield return new WaitForSeconds(0.01f);
		}
	}

	public void HideShowGameText(GameObject obj)
	{
		DescObj(obj);
	}

	public void FaceBookRankAni(GameObject PanelObj1, GameObject PanelObj2, GameObject okbtnObj, GameObject sharebtnObj)
	{
		Vector3 localPosition = PanelObj1.transform.localPosition;
		float x = localPosition.x;
		Vector3 localPosition2 = PanelObj1.transform.localPosition;
		float y = localPosition2.y;
		Vector3 localPosition3 = PanelObj2.transform.localPosition;
		float x2 = localPosition3.x;
		Vector3 localPosition4 = PanelObj2.transform.localPosition;
		float y2 = localPosition4.y;
		sharebtnObj.SetActive(value: false);
		okbtnObj.SetActive(value: false);
		PanelObj1.transform.Find("jiantou").GetComponent<Image>().DOFade(0f, 1f)
			.SetEase(Ease.OutSine)
			.SetDelay(1.7f);
		PanelObj2.transform.Find("jiantou").GetComponent<Image>().DOFade(0f, 1f)
			.SetEase(Ease.OutSine)
			.SetDelay(1.7f);
		PanelObj1.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f).SetDelay(0.5f);
		PanelObj1.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f).SetDelay(0.5f);
		PanelObj1.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).SetDelay(0.5f);
		PanelObj1.SetActive(value: true);
		Sequence s = DOTween.Sequence();
		s.Append(PanelObj1.transform.DOLocalMove(new Vector3(x - 10f, y + 100f, 0f), 0.5f).SetDelay(0.5f)).Append(PanelObj1.transform.DOLocalMove(new Vector3(x, y - 90f, 0f), 0.8f)).Append(PanelObj1.transform.DOLocalMove(new Vector3((float)Screen.width * 0.02f, y - 90f, 0f), 0.5f));
		PanelObj2.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f).SetDelay(0.5f);
		PanelObj2.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f).SetDelay(0.5f);
		PanelObj2.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).SetDelay(0.5f);
		PanelObj2.SetActive(value: true);
		Sequence s2 = DOTween.Sequence();
		s2.Append(PanelObj2.transform.DOLocalMove(new Vector3(x2 + 10f, y2 - 100f, 0f), 0.5f).SetDelay(0.5f)).Append(PanelObj2.transform.DOLocalMove(new Vector3(x2, y2 + 260f, 0f), 0.8f)).Append(PanelObj2.transform.DOLocalMove(new Vector3((float)Screen.width * 0.02f, y2 + 260f, 0f), 0.5f))
			.OnComplete(delegate
			{
				btnshow(okbtnObj, sharebtnObj);
			});
	}

	private void btnshow(GameObject okbtnObj, GameObject sharebtnObj)
	{
		okbtnObj.transform.localScale = new Vector3(0f, 0f, 0f);
		sharebtnObj.transform.localScale = new Vector3(0f, 0f, 0f);
		sharebtnObj.SetActive(value: true);
		okbtnObj.SetActive(value: true);
		sharebtnObj.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.3f);
		sharebtnObj.transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f).SetDelay(0.3f);
		okbtnObj.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.3f).SetDelay(0.7f);
		okbtnObj.transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f).SetDelay(1f);
	}

	public void ShowProp(int type, int num, GameObject obj)
	{
		if (type >= 4 && type <= 9)
		{
			if (num == 100)
			{
				num = 1;
			}
			if (num == 200)
			{
				num = 1;
			}
		}
		ShowProp(type, num, 0, 0, 0, 0, 0, 0, obj);
	}

	public void ShowProp(int type1, int num1, int type2, int num2, GameObject obj)
	{
		ShowProp(type1, num1, type2, num2, 0, 0, 0, 0, obj);
	}

	public void ShowProp(int type1, int num1, int type2, int num2, int type3, int num3, GameObject obj)
	{
		ShowProp(type1, num1, type2, num2, type3, num3, 0, 0, obj);
	}

	public void ShowProp(int type1, int num1, int type2, int num2, int type3, int num3, int type4, int num4, GameObject obj)
	{
		if (type4 > 0)
		{
			propNum = 4;
		}
		else if (type3 > 0)
		{
			propNum = 3;
		}
		else if (type2 > 0)
		{
			propNum = 2;
		}
		else
		{
			propNum = 1;
		}
		if (propNum == 1)
		{
			CreatAward(obj, type1, num1, new Vector3(0f, 2.5f, 0f), 0.1f, bAddMask: true, bclose: true);
		}
		else if (propNum == 2)
		{
			CreatAward(obj, type1, num1, new Vector3(-0.8f, 2.5f, 0f), 0.1f, bAddMask: true);
			CreatAward(obj, type2, num2, new Vector3(0.8f, 2.5f, 0f), 0.5f, bAddMask: false, bclose: true);
		}
		else if (propNum == 3)
		{
			CreatAward(obj, type1, num1, new Vector3(-2f, 2.5f, 0f), 0.1f, bAddMask: true);
			CreatAward(obj, type2, num2, new Vector3(0f, 2.5f, 0f), 0.5f);
			CreatAward(obj, type3, num3, new Vector3(2f, 2.5f, 0f), 1f, bAddMask: false, bclose: true);
		}
		else if (propNum == 4)
		{
			CreatAward(obj, type1, num1, new Vector3(-2.1f, 2.5f, 0f), 0.1f, bAddMask: true);
			CreatAward(obj, type2, num2, new Vector3(-0.7f, 2.5f, 0f), 0.5f);
			CreatAward(obj, type3, num3, new Vector3(0.7f, 2.5f, 0f), 1f);
			CreatAward(obj, type4, num4, new Vector3(2.1f, 2.5f, 0f), 1.5f, bAddMask: false, bclose: true);
		}
		if (!InitGame.bChinaVersion && (bool)PayManager.action)
		{
			PayManager.action.LoadGold();
		}
	}

	public void ShowProp(List<int> Ltype, List<int> Lnum, GameObject obj)
	{
		propNum = Ltype.Count;
		if (((bool)DareWinUI.action && obj == DareWinUI.action.gameObject) || (bool)HuaGame.action || (bool)GameUI.action)
		{
			if (propNum == 1)
			{
				CreatAward(obj, Ltype[0], Lnum[0], new Vector3(0f, 150f, 0f), 0.1f, bAddMask: true);
			}
			else if (propNum == 2)
			{
				CreatAward(obj, Ltype[0], Lnum[0], new Vector3(-100f, 150f, 0f), 0.1f, bAddMask: true);
				CreatAward(obj, Ltype[1], Lnum[1], new Vector3(100f, 150f, 0f), 0.5f, bAddMask: false, bclose: true);
			}
			else if (propNum == 3)
			{
				CreatAward(obj, Ltype[0], Lnum[0], new Vector3(-200f, 150f, 0f), 0.1f, bAddMask: true);
				CreatAward(obj, Ltype[1], Lnum[1], new Vector3(0f, 150f, 0f), 0.5f);
				CreatAward(obj, Ltype[2], Lnum[2], new Vector3(200f, 150.5f, 0f), 1f, bAddMask: false, bclose: true);
			}
			else if (propNum == 4)
			{
				CreatAward(obj, Ltype[0], Lnum[0], new Vector3(-100f, 200f, 0f), 0.1f, bAddMask: true);
				CreatAward(obj, Ltype[1], Lnum[1], new Vector3(100f, 200f, 0f), 0.5f);
				CreatAward(obj, Ltype[2], Lnum[2], new Vector3(-100f, 0f, 0f), 1f);
				CreatAward(obj, Ltype[3], Lnum[3], new Vector3(100f, 0f, 0f), 1.5f, bAddMask: false, bclose: true);
			}
			else if (propNum == 5)
			{
				CreatAward(obj, Ltype[0], Lnum[0], new Vector3(-200f, 200f, 0f), 0.1f, bAddMask: true);
				CreatAward(obj, Ltype[1], Lnum[1], new Vector3(0f, 200f, 0f), 0.5f);
				CreatAward(obj, Ltype[2], Lnum[2], new Vector3(200f, 200f, 0f), 1f);
				CreatAward(obj, Ltype[3], Lnum[3], new Vector3(-100f, 0f, 0f), 1.5f);
				CreatAward(obj, Ltype[4], Lnum[4], new Vector3(100f, 0f, 0f), 2f, bAddMask: false, bclose: true);
			}
			else if (propNum == 6)
			{
				CreatAward(obj, Ltype[0], Lnum[0], new Vector3(-200f, 200f, 0f), 0.1f, bAddMask: true);
				CreatAward(obj, Ltype[1], Lnum[1], new Vector3(0f, 200f, 0f), 0.5f);
				CreatAward(obj, Ltype[2], Lnum[2], new Vector3(200f, 200f, 0f), 1f);
				CreatAward(obj, Ltype[3], Lnum[3], new Vector3(-200f, 0f, 0f), 1.5f);
				CreatAward(obj, Ltype[4], Lnum[4], new Vector3(0f, 0f, 0f), 2f);
				CreatAward(obj, Ltype[5], Lnum[5], new Vector3(200f, 0f, 0f), 2.5f, bAddMask: false, bclose: true);
			}
		}
		else if (propNum == 1)
		{
			CreatAward(obj, Ltype[0], Lnum[0], new Vector3(0f, 2.5f, 0f), 0.1f, bAddMask: true, bclose: true);
		}
		else if (propNum == 2)
		{
			CreatAward(obj, Ltype[0], Lnum[0], new Vector3(-0.8f, 2.5f, 0f), 0.1f, bAddMask: true);
			CreatAward(obj, Ltype[1], Lnum[1], new Vector3(0.8f, 2.5f, 0f), 0.5f, bAddMask: false, bclose: true);
		}
		else if (propNum == 3)
		{
			CreatAward(obj, Ltype[0], Lnum[0], new Vector3(-2f, 2.5f, 0f), 0.1f, bAddMask: true);
			CreatAward(obj, Ltype[1], Lnum[1], new Vector3(0f, 2.5f, 0f), 0.5f);
			CreatAward(obj, Ltype[2], Lnum[2], new Vector3(2f, 2.5f, 0f), 1f, bAddMask: false, bclose: true);
		}
		else if (propNum == 4)
		{
			CreatAward(obj, Ltype[0], Lnum[0], new Vector3(-2.1f, 2.5f, 0f), 0.1f, bAddMask: true);
			CreatAward(obj, Ltype[1], Lnum[1], new Vector3(-0.7f, 2.5f, 0f), 0.5f);
			CreatAward(obj, Ltype[2], Lnum[2], new Vector3(0.7f, 2.5f, 0f), 1f);
			CreatAward(obj, Ltype[3], Lnum[3], new Vector3(2.1f, 2.5f, 0f), 1.5f, bAddMask: false, bclose: true);
		}
		else if (propNum == 5)
		{
			CreatAward(obj, Ltype[0], Lnum[0], new Vector3(-2f, 2.5f, 0f), 0.1f, bAddMask: true);
			CreatAward(obj, Ltype[1], Lnum[1], new Vector3(0f, 2.5f, 0f), 0.5f);
			CreatAward(obj, Ltype[2], Lnum[2], new Vector3(2f, 2.5f, 0f), 1f);
			CreatAward(obj, Ltype[3], Lnum[3], new Vector3(-0.8f, 1f, 0f), 1.5f);
			CreatAward(obj, Ltype[4], Lnum[4], new Vector3(0.8f, 1f, 0f), 2f, bAddMask: false, bclose: true);
		}
		if (!InitGame.bChinaVersion && (bool)PayManager.action)
		{
			PayManager.action.LoadGold();
		}
	}

	public void CreatAward(GameObject obj, int type, int num, Vector3 pos, float fTime = 0.1f, bool bAddMask = false, bool bclose = false)
	{
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex < 80000 || (bool)GameUI.action)
		{
		}
		if (type >= 4 && type <= 9)
		{
			if (num == 100)
			{
				num = 1;
			}
			if (num == 200)
			{
				num = 1;
			}
		}
		if (!GameUI.action || ((!DareWinUI.action || !(obj != DareWinUI.action.gameObject)) && !BuySkillUI.action && !BuySkillUIChina.action))
		{
			StartCoroutine(IECreatAward(obj, type, num, pos, fTime, bAddMask, bclose));
		}
	}

	public Sprite GetAwardImgIcon(int index)
	{
		string path = "Img/SigninUI/signin_icon_" + index;
		Texture2D texture = (Texture2D)Resources.Load(path, typeof(Texture2D));
		return Sprite.Create(texture, new Rect(0f, 0f, 138f, 114f), new Vector2(0.5f, 0.5f));
	}

	private IEnumerator IECreatAward(GameObject obj, int type, int num, Vector3 pos, float fTime, bool bAddMask = false, bool bclose = false)
	{
		yield return new WaitForSeconds(fTime);
		obj = UI.Instance.CanvasTransform.gameObject;
		if (bAddMask)
		{
			bgmaskObj = UnityEngine.Object.Instantiate(bgmask);
			bgmaskObj.transform.SetParent(obj.transform);
			if ((bool)GameUI.action || (bool)HuaGame.action)
			{
				bgmaskObj.transform.localPosition = new Vector3(0f, 0f, 0f);
			}
			if ((bool)HuaGame.action || (bool)GameUI.action)
			{
				bgmaskObj.AddComponent<Canvas>();
				bgmaskObj.GetComponent<Canvas>().overrideSorting = true;
				bgmaskObj.GetComponent<Canvas>().sortingOrder = 1000;
				bgmaskObj.transform.GetComponent<Image>().DOFade(0f, 0.1f).SetEase(Ease.OutSine)
					.SetDelay(0.1f);
			}
			else
			{
				bgmaskObj.transform.GetComponent<Image>().DOFade(0f, 1.5f).SetEase(Ease.OutSine)
					.SetDelay(0.5f * (float)propNum + 1f);
			}
		}
		GameObject lightObj = UnityEngine.Object.Instantiate(light);
		lightObj.transform.SetParent(bgmaskObj.transform);
		lightObj.transform.position = pos;
		if ((bool)DareWinUI.action && obj == DareWinUI.action.gameObject)
		{
			lightObj.transform.localPosition = pos;
			lightObj.transform.localScale = new Vector3(70f, 70f, 1f);
		}
		GameObject _Obj = UnityEngine.Object.Instantiate(AwardImgObj);
		_Obj.transform.SetParent(bgmaskObj.transform);
		_Obj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		_Obj.transform.position = pos;
		if ((bool)GameUI.action || (bool)HuaGame.action)
		{
			_Obj.transform.localScale = new Vector3(1f, 1f, 1f);
			_Obj.transform.localPosition = pos;
		}
		if ((bool)DareWinUI.action && obj == DareWinUI.action.gameObject)
		{
			_Obj.transform.localScale = new Vector3(1f, 1f, 1f);
			_Obj.transform.localPosition = pos;
		}
		_Obj.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + type, 138, 114);
		_Obj.transform.Find("num").GetComponent<Text>().text = "+" + num.ToString();
		_Obj.AddComponent<Canvas>();
		Canvas _Canvas = _Obj.transform.GetComponent<Canvas>();
		_Canvas.overrideSorting = true;
		_Canvas.sortingOrder = 9002;
		float ff = 100f;
		if ((bool)DareWinUI.action && obj == DareWinUI.action.gameObject)
		{
			ff = 1f;
		}
		if ((bool)GameUI.action || (bool)HuaGame.action)
		{
			ff = 1f;
		}
		DataManager.bbeibaoFlay = true;
		Sequence ts = DOTween.Sequence();
		ts.Append(_Obj.transform.DOScale(new Vector3(1.5f / ff, 1.5f / ff, 1.5f), 0.3f).SetEase(Ease.OutSine)).Append(_Obj.transform.DOScale(new Vector3(0.8f / ff, 0.8f / ff, 0.8f), 0.3f).SetEase(Ease.InSine)).Append(_Obj.transform.DOScale(new Vector3(1f / ff, 1f / ff, 1f), 0.3f).SetEase(Ease.OutSine))
			.OnComplete(delegate
			{
				HideBuy(_Obj, fTime, bgmaskObj, bAddMask, propNum, type, bclose);
			});
	}

	public void DesyOnComplete(GameObject obj, float Ftime, GameObject bgmaskObj, bool bAddMask, int propNum, int type, bool bclose = false)
	{
		UnityEngine.Debug.Log("DesyOnComplete-2bclose = " + bclose);
		if (bAddMask)
		{
			UnityEngine.Object.Destroy(bgmaskObj, 0.5f * (float)propNum + 0.6f);
			UnityEngine.Debug.Log("DesyOnComplete-3bclose = " + bclose);
		}
		obj.transform.Find("num").GetComponent<Text>().gameObject.SetActive(value: true);
		UnityEngine.Object.Destroy(obj, 0.2f);
		if (bclose)
		{
			UnityEngine.Debug.Log("DesyOnComplete-4bclose = " + bclose);
			if ((bool)PayManager.action)
			{
				PayManager.action.LoadGold();
			}
			StartCoroutine(DelCanvase(0.5f));
		}
	}

	public IEnumerator DelCanvase(float dtime)
	{
		yield return new WaitForSeconds(dtime);
		if (!HuaGame.action || (bool)GameUI.action)
		{
			DownObjCanvas(MapUI.action.TopRightObj);
			DownObjCanvas(MapUI.action.TopLeftObj);
			DownObjCanvas(MapUI.action.TopCenterObj);
			DownObjCanvas(MapUI.action.DownLeftOtherObj);
			if ((bool)MapUI.action.DownLeftOtherObj)
			{
			}
			DataManager.bbeibaoFlay = false;
		}
	}

	public void HideBuy(GameObject obj, float Ftime, GameObject bgmaskObj, bool bAddMask, int propNum, int type, bool bclose = false)
	{
		obj.transform.Find("num").GetComponent<Text>().gameObject.SetActive(value: false);
		if ((bool)HuaGame.action || (bool)GameUI.action)
		{
			DesyOnComplete(obj, Ftime, bgmaskObj, bAddMask, propNum, type, bclose);
			DataManager.bbeibaoFlay = false;
			return;
		}
		GameObject gameObject = MapUI.action.DownLeftOtherObj;
		bool flag = false;
		if (type == 10 || type == 11 || type == 12 || type == 1)
		{
			gameObject = MapUI.action.TopLeftObj;
			flag = true;
		}
		else if (type == 3)
		{
			flag = true;
			gameObject = MapUI.action.TopRightObj;
		}
		else if (type == 2)
		{
			flag = true;
			gameObject = MapUI.action.TopCenterObj;
		}
		if (!InitGame.bChinaVersion)
		{
			DesyOnComplete(obj, Ftime, bgmaskObj, bAddMask, propNum, type, bclose);
			DataManager.bbeibaoFlay = false;
			return;
		}
		UnityEngine.Debug.Log("DesyOnComplete-1bclose = " + bclose);
		UnityEngine.Debug.Log("DesyOnComplete-obj = " + obj);
		UnityEngine.Debug.Log("DesyOnComplete-obj_ = " + gameObject);
		if (flag)
		{
			obj.transform.DOMove(gameObject.transform.position, 0.5f).SetEase(Ease.InSine).OnComplete(delegate
			{
				DesyOnComplete(obj, Ftime, bgmaskObj, bAddMask, propNum, type, bclose);
			});
		}
		else
		{
			obj.transform.DOMove(gameObject.transform.position, 0.5f).SetEase(Ease.InSine).OnComplete(delegate
			{
				DesyOnComplete(obj, Ftime, bgmaskObj, bAddMask, propNum, type, bclose);
			});
		}
		obj.transform.DOScale(0.005f, 0.5f).SetEase(Ease.InSine);
		UpObjCanvas(MapUI.action.TopRightObj);
		UpObjCanvas(MapUI.action.TopLeftObj);
		UpObjCanvas(MapUI.action.TopCenterObj);
		UpObjCanvas(MapUI.action.DownLeftOtherObj);
	}

	public void UpObjCanvas(GameObject obj)
	{
		if (!obj.GetComponent<Canvas>())
		{
			Canvas canvas = obj.AddComponent<Canvas>();
			canvas.overrideSorting = true;
			canvas.sortingOrder = 9001;
		}
	}

	public void DownObjCanvas(GameObject obj)
	{
		try
		{
			if ((bool)obj.GetComponent<Canvas>())
			{
				UnityEngine.Object.Destroy(obj.GetComponent<Canvas>());
			}
		}
		catch (Exception)
		{
		}
	}

	public void Bubble_5()
	{
		GameUI.action.BubbleCountText.gameObject.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 0.01f).OnComplete(delegate
		{
			Bubble_anim(GameUI.action.BubbleCountText.gameObject);
		});
	}

	private void Bubble_anim(GameObject obj)
	{
		if (Singleton<LevelManager>.Instance.iBubbleCount >= 6)
		{
			Bubble_10();
			return;
		}
		obj.transform.DOScale(new Vector3(0.016f, 0.016f, 0.016f), 0.3f).SetEase(Ease.InOutSine);
		obj.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 0.4f).SetEase(Ease.InOutSine).SetDelay(0.3f)
			.OnComplete(delegate
			{
				Bubble_anim(obj);
			});
	}

	public void Bubble_10()
	{
		GameUI.action.BubbleCountText.gameObject.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 0.3f);
	}

	public void PlaySkillAni(GameObject obj)
	{
		obj.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.3f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
	}

	public void PlaySkillAniClose(GameObject obj)
	{
		UnityEngine.Debug.Log("PlaySkillAniClose");
		obj.transform.DOKill();
	}

	public bool BuyLiveSale(bool btype = false)
	{
		if (BuyLiveSaleTime())
		{
			return false;
		}
		if (Singleton<UserManager>.Instance.getLoveInfinite() > 0)
		{
			return false;
		}
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
		if (@int == Singleton<DataManager>.Instance.iLoveMaxAll)
		{
			return false;
		}
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
		if (int2 < 20)
		{
			return false;
		}
		if (@int >= Singleton<DataManager>.Instance.iLoveUse)
		{
			return false;
		}
		if (@int * 100 / Singleton<DataManager>.Instance.iLoveMaxAll > 20)
		{
			int num = UnityEngine.Random.Range(1, 100);
			if (num > 30)
			{
				SetShowTime();
				return true;
			}
		}
		else
		{
			if (btype)
			{
				return false;
			}
			int num2 = UnityEngine.Random.Range(1, 100);
			if (num2 > 50)
			{
				SetShowTime();
				return true;
			}
		}
		return false;
	}

	public void SetShowTime(bool b = false)
	{
		if (b)
		{
			int nowTime = Util.GetNowTime();
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_SetShowLoveTime1", nowTime);
		}
		else
		{
			int nowTime2 = Util.GetNowTime();
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_SetShowLoveTime2", nowTime2);
		}
	}

	public bool BuyLiveSaleTime()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SetShowLoveTime1");
		int nowTime = Util.GetNowTime();
		if (@int > 0)
		{
			int num = nowTime - @int;
			if (num < 180)
			{
				return true;
			}
		}
		@int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SetShowLoveTime2");
		if (@int > 0)
		{
			int num2 = nowTime - @int;
			if (num2 < 259200)
			{
				return true;
			}
		}
		return false;
	}
}
