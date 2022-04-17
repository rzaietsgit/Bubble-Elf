using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailUI : MonoBehaviour
{
	public static MailUI action;

	public GameObject MsgObj;

	public GameObject MsgObjFather;

	public GameObject FaceBookLoginObj;

	public GameObject FaceBookLoginRemark;

	public GameObject MailNullRemark;

	private List<GameObject> LTempObj;

	public Text MailPanelCon;

	public Text MailPanelConRemark;

	public Text MailNullRemarkText;

	public Text SetMailPanelTitle;

	public Text MapMailFaceBookConAwardText;

	private int iObjIndex;

	private bool btest;

	public int iNowCount;

	private void Start()
	{
		BaseUIAnimation.action.SetLanguageFont("MailPanelCon", MailPanelCon, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("MailPanelConRemark", MailPanelConRemark, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("MailNullRemark", MailNullRemarkText, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SetMailPanelTitle", SetMailPanelTitle, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("MapMailFaceBookConAwardText", MapMailFaceBookConAwardText, string.Empty);
		action = this;
		if (InitGame.bEnios)
		{
			MailPanelConRemark.gameObject.SetActive(value: true);
		}
		else
		{
			MailPanelConRemark.gameObject.SetActive(value: false);
		}
		if (btest)
		{
			InitMails();
			FaceBookLoginObj.SetActive(value: false);
			FaceBookLoginRemark.SetActive(value: false);
			return;
		}
		MailNullRemark.SetActive(value: false);
		ResFaceBookLoginState();
		BaseUIAnimation.action.CreateButton(FaceBookLoginObj.gameObject);
		if ((bool)FaceBookApi.Action)
		{
			FaceBookApi.Action.CheckLoginIcon(FaceBookLoginObj);
		}
		if (InitGame.bChinaVersion && !InitGame.bEnios)
		{
			FaceBookLoginObj.SetActive(value: false);
		}
	}

	public void ResFaceBookLoginState()
	{
		if (!FaceBookApi.Action.bLoginState())
		{
			FaceBookLoginObj.SetActive(value: true);
			if (InitGame.bEnios)
			{
				FaceBookLoginRemark.SetActive(value: true);
			}
			MailNullRemark.SetActive(value: false);
		}
		else
		{
			InitMails();
			FaceBookLoginObj.SetActive(value: false);
			FaceBookLoginRemark.SetActive(value: false);
		}
	}

	private void InitMails()
	{
		iObjIndex = 0;
		int num = 0;
		if (LTempObj == null)
		{
			LTempObj = new List<GameObject>();
		}
		for (int i = 0; i < LTempObj.Count; i++)
		{
			UnityEngine.Object.Destroy(LTempObj[i].gameObject);
		}
		if (btest)
		{
			num = 5;
			for (int j = 0; j < num; j++)
			{
				LoadMail(1, "Jy" + j, "jy" + j, "jy" + j);
			}
		}
		else
		{
			FaceBookApi.Action.MailUILoad();
		}
	}

	public void RemoveMailOne()
	{
		iNowCount--;
		if (iNowCount <= 0)
		{
			MailNullRemark.SetActive(value: true);
			return;
		}
		MailNullRemark.SetActive(value: false);
		if ((bool)SettingPanelUI.action)
		{
			SettingPanelUI.action.ShowMailCount(iNowCount);
		}
	}

	public void LoadMail(int iType, string Name, string sID, string sMessID)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(MsgObj);
		gameObject.transform.SetParent(MsgObjFather.transform, worldPositionStays: false);
		MessageObj component = gameObject.GetComponent<MessageObj>();
		component.SetType(iType, Name, sID, sMessID);
		LTempObj.Add(gameObject);
		iObjIndex++;
	}

	private void Update()
	{
	}

	public void ClickFaceBookLogin()
	{
		if ((bool)FaceBookApi.Action)
		{
			FaceBookApi.Action.FackBookLogin();
		}
	}

	public void ClickSettingBtn(bool bClick)
	{
		if ((bool)MapPanelUI.action)
		{
			MapPanelUI.action.gameObject.SetActive(value: false);
		}
		if ((bool)SetPanelUI.action)
		{
			SetPanelUI.action.gameObject.SetActive(value: false);
		}
	}
}
