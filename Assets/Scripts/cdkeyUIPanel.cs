using System.Collections;
using UnityEngine;

public class cdkeyUIPanel : cdkeyUIPanelBase
{
	public static cdkeyUIPanel panel;

	private bool bSetFont = true;

	private bool bshow;

	public override void InitUI()
	{
		panel = this;
		Singleton<DataManager>.Instance.bcdkeyReward = false;
		detail.err_Text.gameObject.SetActive(value: false);
		BaseUIAnimation.action.SetLanguageFont("cdkeyUITtitle", detail.cdkeyUITitle_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("cdkeyUIButtonText", detail.btnText_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("cdkeyUIRemarkText", detail.cdkeyUIRemarkText_Text, string.Empty);
		string text = Singleton<DataManager>.Instance.dDataLanguage["cdkeyEnterRemark"][BaseUIAnimation.Language];
		detail.Placeholder_Text.text = text;
		if ((bool)SetPanelUI.action)
		{
			SetPanelUI.action.ResBtn();
		}
	}

	public override void OnButton()
	{
		if (InitGame.bChinaVersion)
		{
			GMDebug(detail.Text_Text.text);
	        if (detail.Text_Text.text.Length == 0 || detail.Text_Text.text.Length != 22)
			{
				if (bSetFont)
				{
					bSetFont = false;
					BaseUIAnimation.action.SetLanguageFont("CdkeyErrorText1", detail.err_Text, string.Empty);
				}
				else
				{
					string text = Singleton<DataManager>.Instance.dDataLanguage["CdkeyErrorText1"][BaseUIAnimation.Language];
					detail.err_Text.text = text;
				}
				detail.err_Text.gameObject.SetActive(value: true);
				if (!bshow)
				{
					bshow = true;
					StartCoroutine(hideCdkeyErrorText());
				}
			}
			else
			{
				StartCoroutine(UseCDKey(detail.Text_Text.text.ToString()));
			}
		}
		else
		{
			if (!FireBase.Action)
			{
				return;
			}
			GMDebug(detail.Text_Text.text);
			if (detail.Text_Text.text.Length == 0 || detail.Text_Text.text.Length != 32)
			{
				if (bSetFont)
				{
					bSetFont = false;
					BaseUIAnimation.action.SetLanguageFont("CdkeyErrorText1", detail.err_Text, string.Empty);
				}
				else
				{
					string text2 = Singleton<DataManager>.Instance.dDataLanguage["CdkeyErrorText1"][BaseUIAnimation.Language];
					detail.err_Text.text = text2;
				}
				detail.err_Text.gameObject.SetActive(value: true);
				if (!bshow)
				{
					bshow = true;
				}
			}
			else
			{
				FireBase.Action.UCheckCdKey(detail.Text_Text.text.ToString());
			}
		}
	}

	public void GMDebug(string GMCode)
	{
		if (Application.platform == RuntimePlatform.WindowsEditor)
		{
			TestFunc(GMCode);
		}
		if (Singleton<DataManager>.Instance.bInitUnityConfig2)
		{
			TestFunc(GMCode);
		}
		StartCoroutine(IEGMDebug(GMCode));
	}

	private void TestFunc(string code)
	{
		if (code == "clear")
		{
			PlayerPrefs.DeleteAll();
			Singleton<TestScript>.Instance.Clear();
		}
		if (code == "clearjyvip")
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_UnityUpFaceBookVip7" + Util.GetNowTime_Day(), 0);
			Singleton<DataManager>.Instance.SaveUserDate("DB_Vip7", string.Empty);
			InitGame.bVip7 = false;
			FireBase.Action.UnityUpFaceBookVip7();
		}
		if (code.LastIndexOf("pay") >= 0)
		{
			int iGold = int.Parse(code.Split(' ')[1]);
			PayManager.action.AwardAddGold(iGold, "QIANDAO");
			if ((bool)PayManager.action)
			{
				PayManager.action.LoadGold();
			}
		}
		if (code.LastIndexOf("payhua") >= 0)
		{
			int ibi = int.Parse(code.Split(' ')[1]);
			Singleton<UserManager>.Instance.AddHuaBi(ibi);
		}
		if (code.LastIndexOf("love") >= 0)
		{
			int num = int.Parse(code.Split(' ')[1]);
			for (int i = 0; i < num; i++)
			{
				Singleton<LevelManager>.Instance.CutLove();
			}
		}
		if (code.LastIndexOf("open") >= 0)
		{
			int num2 = int.Parse(code.Split(' ')[1]);
			for (int j = 1; j <= num2; j++)
			{
				int num3 = j;
				string s = "3";
				string s2 = "88888";
				Singleton<DataManager>.Instance.SaveUserDate("DB_LevelStar_" + num3, int.Parse(s));
				Singleton<DataManager>.Instance.SaveUserDate("DB_LevelScore_" + num3, int.Parse(s2));
				int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
				if (num3 <= @int)
				{
					continue;
				}
				Singleton<DataManager>.Instance.SaveUserDate("DB_iNowPassLevelID", num3);
				for (int k = 0; k < Singleton<DataManager>.Instance.LMapEndBtnID.Length; k++)
				{
					if (num3 == Singleton<DataManager>.Instance.LMapEndBtnID[k])
					{
						Singleton<UserManager>.Instance.GoNextMap();
					}
				}
			}
			Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.InitGame);
		}
		else
		{
			if (code.LastIndexOf("skill") < 0)
			{
				return;
			}
			int num4 = int.Parse(code.Split(' ')[1]);
			if (num4 == 100)
			{
				for (int l = 1; l <= 6; l++)
				{
					Singleton<DataManager>.Instance.SaveUserDate("DB_SkillOpen_" + l, 1);
				}
			}
			else
			{
				Singleton<DataManager>.Instance.SaveUserDate("DB_SkillOpen_" + num4, 1);
			}
		}
	}

	private IEnumerator IEGMDebug(string GMCode)
	{
		WWW www = new WWW("http://jyerrorpaopao.unitygame8.com/cdeky.html");
		yield return www;
		if (www.text.Equals("open"))
		{
			TestFunc(GMCode);
		}
	}

	private IEnumerator hideCdkeyErrorText()
	{
		yield return new WaitForSeconds(3f);
		bshow = false;
		detail.err_Text.gameObject.SetActive(value: false);
	}

	private IEnumerator UseCDKey(string str)
	{
		WWW www = new WWW("http://op.yunbu.me/bubbleelf/cdkey.jsp?key=" + str);
		yield return www;
		if (www.text.Equals("true"))
		{
			string a = str.Substring(0, 2);
			if (a == "01")
			{
				Singleton<DataManager>.Instance.cdkeys_key = 1;
			}
			else if (a == "02")
			{
				Singleton<DataManager>.Instance.cdkeys_key = 2;
			}
			else if (a == "03")
			{
				Singleton<DataManager>.Instance.cdkeys_key = 3;
			}
			else if (a == "04")
			{
				Singleton<DataManager>.Instance.cdkeys_key = 4;
			}
			else if (a == "05")
			{
				Singleton<DataManager>.Instance.cdkeys_key = 5;
			}
			else if (a == "06")
			{
				Singleton<DataManager>.Instance.cdkeys_key = 6;
			}
			else if (a == "07")
			{
				Singleton<DataManager>.Instance.cdkeys_key = 7;
			}
			else if (a == "08")
			{
				Singleton<DataManager>.Instance.cdkeys_key = 8;
			}
			Singleton<DataManager>.Instance.bcdkeyReward = true;
			UI.Instance.ClosePanel();
		}
	}
}
