using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LLives : MonoBehaviour
{
	private bool bShowImg = true;

	public GameObject SelectBtnImg;

	public GameObject PeopleIcon;

	public GameObject AskByOne_btn;

	private List<GameObject> LiveListObj;

	public Text LivesRemark;

	private string friendID = string.Empty;

	private string TypeStr = string.Empty;

	private bool SelectNow = true;

	public void SetTypeStr(string str)
	{
		TypeStr = str;
	}

	public void SetSelect(bool b)
	{
		SelectNow = b;
	}

	public bool getSelect()
	{
		return SelectNow;
	}

	private void Start()
	{
		BaseUIAnimation.action.SetLanguageFont("LivesRemark", LivesRemark, string.Empty);
	}

	public string getFriendID()
	{
		return friendID;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject gameObject = Util.TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject != null && "LLives" == gameObject.name)
			{
				ClickSelect();
			}
		}
	}

	public void SetPeopleImage(string _friendID)
	{
		friendID = _friendID;
	}

	public void SetFaceBookID(string sID)
	{
		friendID = sID;
	}

	public void AskByOne()
	{
	}

	public void OffSelect()
	{
		bShowImg = false;
		SelectNow = false;
		SelectBtnImg.SetActive(value: false);
	}

	public void OnSelect()
	{
		bShowImg = true;
		SelectNow = true;
		SelectBtnImg.SetActive(value: true);
	}

	public void ClickSelect()
	{
		if (bShowImg)
		{
			bShowImg = false;
			SelectNow = false;
			SelectBtnImg.SetActive(value: false);
		}
		else
		{
			bShowImg = true;
			SelectNow = true;
			SelectBtnImg.SetActive(value: true);
		}
		SendLivesUI.action.RefreshSelect();
	}
}
