using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
	public static PauseUI action;

	public GameObject SoundBtn;

	public GameObject MusicBtn;

	public GameObject CloseBtn;

	public GameObject QuitBtn;

	public Text FaceBookConn;

	public Text PauseUIQuitLevel;

	public Text PauseUITitle;

	public Text PauseUIResStartText;

	public GameObject QQ;

	public GameObject ExitBtn;

	private bool bMusic = true;

	private bool bSound = true;

	private bool bFaceBookConn = true;

	public bool bPause;

	public bool bQuit;

	public void TestBtn()
	{
		ChinaPay.action.addRewardAll(8, 1, null);
	}

	private void Start()
	{
		action = this;
		CheckClosesound();
		bQuit = false;
		base.transform.Find("mask").gameObject.SetActive(value: false);
		BaseUIAnimation.action.ClickButton(ExitBtn.gameObject);
		BaseUIAnimation.action.SetLanguageFont("PauseUIQuitLevel", PauseUIQuitLevel, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("PauseUITitle", PauseUITitle, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("PauseUIResStartText", PauseUIResStartText, string.Empty);
	}

	public GameObject TouchChecker(Vector3 mouseposition)
	{
		if ((bool)Physics2D.OverlapPoint(mouseposition))
		{
			return Physics2D.OverlapPoint(mouseposition).gameObject;
		}
		return null;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && Singleton<DataManager>.Instance.bUiIsOpen)
		{
			GameObject gameObject = TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject == null)
			{
				bQuit = false;
				ClosePauseUI();
			}
			else if (gameObject.name.LastIndexOf("Close") >= 0)
			{
				_ClosePauseUI();
			}
			else if (gameObject.name.LastIndexOf("Sound") >= 0)
			{
				ClickSound();
			}
			else if (gameObject.name.LastIndexOf("Music") >= 0)
			{
				ClickMusic();
			}
			else if (gameObject.name.LastIndexOf("FaceBook") >= 0)
			{
				ClickFaceBook();
			}
			else if (gameObject.name.LastIndexOf("Quit") >= 0)
			{
				Singleton<LevelManager>.Instance.bRstart = false;
				InitAndroid.action.GAEvent("clickbtn:clickexitgame");
				ClickQuit();
			}
			else if (gameObject.name.LastIndexOf("QQ") >= 0)
			{
				InitAndroid.action.GAEvent("clickbtn:gameRestart");
				Singleton<LevelManager>.Instance.bRstart = true;
				ClickQuit();
			}
			else if (gameObject.name.LastIndexOf("PauseUI") < 0)
			{
				Singleton<LevelManager>.Instance.bRstart = false;
				bQuit = false;
				ClosePauseUI();
			}
		}
	}

	public void ClosePauseUI()
	{
		BaseUIAnimation.action.HidePauseUI(base.gameObject);
	}

	public void _ClosePauseUI()
	{
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
			BaseUIAnimation.action.HidePauseUI(base.gameObject);
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

	public void ClickFaceBook()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		if (BaseUIAnimation.bClickButton)
		{
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
			BaseUIAnimation.action.SetLanguageFont("PauseUICon", FaceBookConn, string.Empty);
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
			BaseUIAnimation.action.SetLanguageFont("PauseUIOut", FaceBookConn, string.Empty);
			FaceBookConn.fontSize = 45;
		}
		else
		{
			BaseUIAnimation.action.SetLanguageFont("PauseUICon", FaceBookConn, string.Empty);
			FaceBookConn.fontSize = 50;
		}
	}

	public void ClickQuit()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		if (Singleton<LevelManager>.Instance.bRstart)
		{
			if (!BaseUIAnimation.bClickButton)
			{
				return;
			}
			BaseUIAnimation.action.ClickButton(QQ.gameObject);
		}
		else
		{
			if (!BaseUIAnimation.bClickButton)
			{
				return;
			}
			BaseUIAnimation.action.ClickButton(QuitBtn.gameObject);
		}
		StartCoroutine(CallClickQuit());
	}

	public void clickquit2()
	{
		StartCoroutine(CallClickQuit());
	}

	private IEnumerator CallClickQuit()
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		bQuit = true;
		Singleton<DataManager>.Instance.bUiIsOpen = false;
		ClosePauseUI();
	}
}
