using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageObj : MonoBehaviour
{
	public GameObject messageBtn;

	public Sprite[] BtnType;

	public TextMeshProUGUI MessageName;

	public TextMeshProUGUI MailRemark;

	public TextMeshProUGUI MailBtn;

	private int iMailType;

	private string sToMailID = string.Empty;

	private string sMessID = string.Empty;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetType(int iType, string sName, string sID, string _sMessID)
	{
		sMessID = _sMessID;
		iMailType = iType;
		sToMailID = sID;
		messageBtn.GetComponent<Image>().sprite = BtnType[iMailType];
		if (iMailType == 0)
		{
			BaseUIAnimation.action.SetLanguageFont("MailSendBtn", MailBtn, string.Empty);
			BaseUIAnimation.action.SetLanguageFont("MailSendRemark", MailRemark, string.Empty);
		}
		else
		{
			BaseUIAnimation.action.SetLanguageFont("MailReceiveBtn", MailBtn, string.Empty);
			BaseUIAnimation.action.SetLanguageFont("MailReceiveRemark", MailRemark, string.Empty);
		}
		if (sName.Length >= 4)
		{
			sName = sName.Substring(0, 4) + ".";
		}
		MessageName.SetText(sName);
	}

	public void ClickBtn()
	{
		if (iMailType == 0)
		{
			SendLove();
		}
		else
		{
			ReceiveLove();
		}
	}

	public void ReceiveLove()
	{
		UnityEngine.Object.Destroy(base.gameObject);
		FaceBookApi.Action.ReceiveLoveFB(sMessID);
	}

	public void SendLove()
	{
		bool flag = true;
		flag = false;
		FaceBookApi.Action.SendMessageFB(base.gameObject, sToMailID);
	}
}
