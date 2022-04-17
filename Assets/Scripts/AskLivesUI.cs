using System.Collections.Generic;
using UnityEngine;

public class AskLivesUI : BaseUI
{
	public GameObject AskBtn;

	public GameObject CloseBtn;

	public static AskLivesUI action;

	public GameObject LLive;

	public GameObject LiveParent;

	private List<GameObject> LiveListObj;

	public GameObject SelectImg;

	private int iCount;

	private int iNowCount;

	private bool bSelect = true;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.AskLivesUI;
	}

	private void Start()
	{
		action = this;
		LiveListObj = new List<GameObject>();
		LoadFriends();
	}

	public void CloseAskLivesUI()
	{
		CloseUI();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject gameObject = Util.TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject == null)
			{
				CloseAskLivesUI();
			}
			else if (!(gameObject.name == "MapUIDoubleClick") && !(gameObject.name == "MapBtn") && !(gameObject.name == "MailBtn") && !(gameObject.name == "SettingBtn") && gameObject.name.LastIndexOf("LLives") < 0 && gameObject.name.LastIndexOf("AskLivesUI") < 0)
			{
				CloseAskLivesUI();
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

	private void LoadFriends()
	{
		iCount = FaceBookApi.Action.LFacebookFriend.Count;
		if (iCount > 4)
		{
			int num = 140;
			int num2 = iCount - 4;
			RectTransform component = LiveParent.transform.GetComponent<RectTransform>();
			RectTransform rectTransform = component;
			Vector3 localPosition = component.localPosition;
			float x = localPosition.x;
			Vector3 localPosition2 = component.localPosition;
			float y = localPosition2.y - (float)(num * num2 / 2);
			Vector3 localPosition3 = component.localPosition;
			rectTransform.localPosition = new Vector3(x, y, localPosition3.z);
			RectTransform rectTransform2 = component;
			Vector2 sizeDelta = component.sizeDelta;
			float x2 = sizeDelta.x;
			Vector2 sizeDelta2 = component.sizeDelta;
			rectTransform2.sizeDelta = new Vector2(x2, sizeDelta2.y + (float)(num * num2));
		}
		iNowCount = iCount;
	}

	public void LoadAskMsg(string name, string id)
	{
		iCount++;
		GameObject gameObject = Object.Instantiate(LLive);
		gameObject.transform.SetParent(LiveParent.transform, worldPositionStays: false);
		LiveListObj.Add(gameObject);
		LLives component = gameObject.GetComponent<LLives>();
		component.SetFaceBookID(id);
	}

	public void ClickAskSend()
	{
		if (LiveListObj == null || LiveListObj.Count <= 0)
		{
			return;
		}
		string str = string.Empty;
		int num = 0;
		for (int i = 0; i < LiveListObj.Count; i++)
		{
			GameObject gameObject = LiveListObj[i];
			LLives component = gameObject.GetComponent<LLives>();
			if (component.getSelect())
			{
				str = ((num != 0) ? (str + "," + component.getFriendID()) : (str + component.getFriendID()));
				num++;
			}
		}
		if (num > 0)
		{
			FaceBookApi.Action.FBFacebookAskString();
		}
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

	public void SelectAll()
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
