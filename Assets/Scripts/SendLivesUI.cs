using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendLivesUI : BaseUI
{
	public GameObject SendBtn;

	public GameObject CloseBtn;

	public static SendLivesUI action;

	public GameObject LLive;

	public GameObject LiveParent;

	private List<GameObject> LiveListObj;

	public GameObject SelectImg;

	public Text SendLivesUITitle;

	public Text SendLivesUISendBtn;

	public Text SendLivesUISelectall;

	private int iNowCount;

	private bool bSelect = true;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.SendLivesUI;
	}

	private void Start()
	{
		action = this;
		LiveListObj = new List<GameObject>();
		LoadAskMsg();
		BaseUIAnimation.action.SetLanguageFont("SendLivesUITitle", SendLivesUITitle, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SendLivesUISendBtn", SendLivesUISendBtn, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SendLivesUISelectall", SendLivesUISelectall, string.Empty);
	}

	public void _CloseSendLivesUI()
	{
		StartCoroutine(CallCloseUI());
	}

	public void CloseSendLivesUI()
	{
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
			StartCoroutine(CallCloseUI());
		}
	}

	private IEnumerator CallCloseUI(bool bDouble = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseUI(bDouble);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject gameObject = Util.TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject == null)
			{
				CloseSendLivesUI();
			}
			else if (!(gameObject.name == "MapUIDoubleClick") && !(gameObject.name == "MapBtn") && !(gameObject.name == "MailBtn") && !(gameObject.name == "SettingBtn") && gameObject.name.LastIndexOf("LLives") < 0 && gameObject.name.LastIndexOf("SendLivesUI") < 0)
			{
				CloseSendLivesUI();
			}
		}
	}

	public override void OnStart()
	{
	}

	protected override void OnAwake()
	{
		base.OnAwake();
	}

	protected override void OnRelease()
	{
		base.OnRelease();
	}

	private void LoadAskMsg()
	{
		int num = 0;
		for (int i = 0; i < FaceBookApi.Action.LFacebookFriend.Count; i++)
		{
			CreateAskMsg(FaceBookApi.Action.dFacebookName[FaceBookApi.Action.LFacebookFriend[i]], FaceBookApi.Action.LFacebookFriend[i]);
		}
	}

	private void DelMsg(string sMsgID)
	{
		StartCoroutine(IEDelMsg(sMsgID));
	}

	private IEnumerator IEDelMsg(string sMsgID)
	{
		yield return new WaitForSeconds(0.1f);
		FaceBookApi.Action.Delmsg(sMsgID);
	}

	public void CreateAskMsg(string name, string id)
	{
		GameObject gameObject = Object.Instantiate(LLive);
		gameObject.transform.SetParent(LiveParent.transform, worldPositionStays: false);
		LiveListObj.Add(gameObject);
		LLives component = gameObject.GetComponent<LLives>();
		component.SetFaceBookID(id);
	}

	public void ClickSend()
	{
		bool flag = true;
		flag = false;
		List<string> list = new List<string>();
		if (LiveListObj == null || LiveListObj.Count <= 0)
		{
			return;
		}
		string empty = string.Empty;
		int num = 0;
		for (int i = 0; i < LiveListObj.Count; i++)
		{
			GameObject gameObject = LiveListObj[i];
			LLives component = gameObject.GetComponent<LLives>();
			if (!component.getSelect())
			{
				continue;
			}
			bool flag2 = true;
			for (int j = 0; j < list.Count; j++)
			{
				if (component.getFriendID() == list[j])
				{
					flag2 = false;
					break;
				}
			}
			if (flag2)
			{
				list.Add(component.getFriendID());
				num++;
			}
		}
		if (num > 0)
		{
			FaceBookApi.Action.SendLiveFB(list);
		}
	}

	private void DeleteForLFaceBookID(string[] LsFaceBookID)
	{
		for (int i = 0; i < LsFaceBookID.Length; i++)
		{
		}
	}

	private void DeleteHttp(string sMessID)
	{
		FaceBookApi.Action.Delmsg(sMessID);
	}

	public void UpdatePosition(string FriendID)
	{
		int num = 140;
		iNowCount--;
		if (iNowCount > 4)
		{
			RectTransform component = LiveParent.transform.GetComponent<RectTransform>();
			RectTransform rectTransform = component;
			Vector2 sizeDelta = component.sizeDelta;
			float x = sizeDelta.x;
			Vector2 sizeDelta2 = component.sizeDelta;
			rectTransform.sizeDelta = new Vector2(x, sizeDelta2.y - (float)num);
		}
		int num2 = 0;
		while (true)
		{
			if (num2 < LiveListObj.Count)
			{
				GameObject gameObject = LiveListObj[num2];
				LLives component2 = gameObject.GetComponent<LLives>();
				if (component2.getFriendID() == FriendID)
				{
					break;
				}
				num2++;
				continue;
			}
			return;
		}
		LiveListObj.RemoveAt(num2);
	}

	public void RefreshSelect()
	{
		for (int i = 0; i < LiveListObj.Count; i++)
		{
			GameObject gameObject = LiveListObj[i];
			LLives component = gameObject.GetComponent<LLives>();
			if (!component.getSelect())
			{
				bSelect = false;
				SelectImg.SetActive(value: false);
				return;
			}
		}
		bSelect = true;
		SelectImg.SetActive(value: true);
	}

	public void ClickSelectAll()
	{
		for (int i = 0; i < LiveListObj.Count; i++)
		{
			GameObject gameObject = LiveListObj[i];
			LLives component = gameObject.GetComponent<LLives>();
			if (bSelect)
			{
				component.OffSelect();
			}
			else
			{
				component.OnSelect();
			}
		}
		if (bSelect)
		{
			bSelect = false;
			SelectImg.SetActive(value: false);
		}
		else
		{
			bSelect = true;
			SelectImg.SetActive(value: true);
		}
	}
}
