using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FacebookRankIcon : MonoBehaviour
{
	public TextMeshProUGUI LevelRankNumberText;

	public TextMeshProUGUI FaceBookName;

	public TextMeshProUGUI FaceBookScore;

	public GameObject HeadIcon;

	public GameObject LevelRankNumberBg;

	public GameObject lineObj;

	public Sprite RandNoOne;

	public Sprite[] LPeopleIcon;

	private void Start()
	{
		if (!FaceBookApi.Action.bLoginState())
		{
			int iNowSelectLevelIndex = Singleton<LevelManager>.Instance.iNowSelectLevelIndex;
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LevelScore_" + iNowSelectLevelIndex);
			FaceBookScore.SetText(@int.ToString());
		}
	}

	public void SetRankID()
	{
	}

	public void HideLine()
	{
		lineObj.gameObject.SetActive(value: false);
	}

	private void Update()
	{
	}

	public void SetNoOne()
	{
		LevelRankNumberBg.GetComponent<Image>().sprite = RandNoOne;
	}

	public void SetRankNb(int index)
	{
		LevelRankNumberText.SetText(index.ToString());
		if (index != 1)
		{
			LevelRankNumberText.faceColor = new Color(1f, 1f, 1f, 1f);
			LevelRankNumberText.outlineWidth = 0.5f;
		}
	}

	public void SetHide()
	{
		base.gameObject.SetActive(value: false);
	}

	public void SetShow()
	{
		base.gameObject.SetActive(value: true);
	}

	public void SetHaedIcon(string Fid = "")
	{
		if (Fid == string.Empty)
		{
			int num = UnityEngine.Random.Range(0, 6);
			HeadIcon.GetComponent<Image>().sprite = LPeopleIcon[num];
		}
		else if (Fid == FaceBookApi.Action.UserId)
		{
			if ((bool)FaceBookApi.Action.MyFacebookImage)
			{
				HeadIcon.GetComponent<Image>().sprite = FaceBookApi.Action.MyFacebookImage;
			}
		}
		else
		{
			try
			{
				HeadIcon.GetComponent<Image>().sprite = FaceBookApi.Action.dFacebookImage[Fid];
			}
			catch (Exception)
			{
				FaceBookApi.Action.SetImage(Fid, HeadIcon.GetComponent<Image>());
			}
		}
	}

	public void SetScore(int iScore = 0)
	{
		if (iScore == 0)
		{
			int num = UnityEngine.Random.Range(10000, 50000);
			FaceBookScore.SetText(num.ToString());
		}
		else
		{
			FaceBookScore.SetText(iScore.ToString());
		}
	}

	public void SetName(string Name = "")
	{
		if (Name == string.Empty)
		{
			string[] array = new string[3]
			{
				"JY",
				"Love",
				"Ko"
			};
			int num = UnityEngine.Random.Range(0, array.Length);
			string text = array[num];
			FaceBookName.SetText(text);
		}
		else
		{
			FaceBookName.SetText(Name);
		}
	}
}
