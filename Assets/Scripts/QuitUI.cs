using System.Collections;
//using Umeng;
using UnityEngine;
using UnityEngine.UI;

public class QuitUI : BaseUI
{
	public static QuitUI action;

	public GameObject ContinueBtn;

	public GameObject QuitBtn;

	public GameObject CloseBtn;

	public Text QuitUIContinuebtn;

	public Text QuitUITitle;

	public Text QuitUIQuitbtn;

	public Text QuitUIRemark;

	private bool bcontinue = true;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.QuitUI;
	}

	public override void OnStart()
	{
		action = this;
		BaseUIAnimation.action.SetLanguageFont("QuitUIContinuebtn", QuitUIContinuebtn, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("QuitUITitle", QuitUITitle, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("QuitUIQuitbtn", QuitUIQuitbtn, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("QuitUIRemark", QuitUIRemark, string.Empty);
		if (Singleton<LevelManager>.Instance.bRstart)
		{
			QuitUITitle.text = "重 玩";
			QuitUIQuitbtn.text = "重 玩";
		}
		if (Singleton<DataManager>.Instance.ChangeSceneType != EnumSceneType.GameScene)
		{
			QuitUIRemark.text = " 确定要退出游戏？";
		}
		if (InitGame.bEnios)
		{
			QuitUIRemark.resizeTextForBestFit = false;
		}
	}

	public void CloseQuitUI(bool bDouble = false)
	{
		Singleton<LevelManager>.Instance.bRstart = false;
		StartCoroutine(CallCloseUI(bDouble));
	}

	public void _CloseQuitUI()
	{
		Singleton<LevelManager>.Instance.bRstart = false;
		CloseUI();
	}

	private IEnumerator CallCloseUI(bool bDouble = false)
	{
		Singleton<LevelManager>.Instance.bRstart = false;
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseUI();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject gameObject = Util.TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject == null)
			{
				Singleton<LevelManager>.Instance.bRstart = false;
				CloseUI();
			}
			else if (gameObject.name.LastIndexOf("QuitUI") < 0)
			{
				Singleton<LevelManager>.Instance.bRstart = false;
				CloseUI();
			}
		}
	}

	protected override void OnAwake()
	{
		base.OnAwake();
	}

	protected override void OnRelease()
	{
		base.OnRelease();
	}

	public void ClickContinueBtn()
	{
		Singleton<LevelManager>.Instance.bRstart = false;
		if (bcontinue)
		{
			bcontinue = false;
			CloseUI();
		}
	}

	public void _ClickContinueBtn()
	{
		Singleton<LevelManager>.Instance.bRstart = false;
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(ContinueBtn.gameObject);
			StartCoroutine(CallCloseUI());
		}
	}

	public void ClickQuitBtn()
	{
		UnityEngine.Debug.Log("DataManager.Instance.ChangeSceneType=" + Singleton<DataManager>.Instance.ChangeSceneType);
		if (Singleton<DataManager>.Instance.ChangeSceneType != EnumSceneType.GameScene)
		{
			Application.Quit();
		}
		else if (Singleton<LevelManager>.Instance.bRstart)
		{
			LoseLog();
			Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
		}
		else if (!Singleton<LevelManager>.Instance.bRstart)
		{
			Singleton<LevelManager>.Instance.bExit = true;
			LoseLog();
			Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
		}
		else
		{
			CloseUI();
		}
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
