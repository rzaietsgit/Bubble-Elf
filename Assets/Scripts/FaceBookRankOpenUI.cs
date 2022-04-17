using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FaceBookRankOpenUI : BaseUI
{
	public GameObject OkBtn;

	public GameObject ShareBtn;

	public static FaceBookRankOpenUI action;

	public GameObject Rank1Img;

	public GameObject Rank2Img;

	public TextMeshProUGUI Rank1Name;

	public TextMeshProUGUI Rank2Name;

	public TextMeshProUGUI Rank1Score;

	public TextMeshProUGUI Rank2Score;

	public TextMeshProUGUI Rank1Rank;

	public TextMeshProUGUI Rank2Rank;

	public GameObject TopICon;

	public GameObject DownICon;

	public Text FaceBookRankOpenUIOK;

	public Text FaceBookRankOpenUIShare;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.FaceBookRankOpenUI;
	}

	public void InitFaceBookData()
	{
		LoadImage(FaceBookApi.Action.Rank1FID, Rank1Img);
		LoadImage(FaceBookApi.Action.Rank2FID, Rank2Img);
		string text = FaceBookApi.Action.Rank1Name;
		if (text.Length > 5)
		{
			text = text.Substring(0, 4) + "...";
		}
		Rank1Name.SetText(text);
		text = FaceBookApi.Action.Rank2Name;
		if (text.Length > 5)
		{
			text = text.Substring(0, 4) + "...";
		}
		Rank2Name.SetText(text);
		Rank1Score.SetText(FaceBookApi.Action.Rank1Score);
		Rank2Score.SetText(FaceBookApi.Action.Rank2Score);
		Rank1Rank.SetText((int.Parse(FaceBookApi.Action.Rank2Rank) + 1).ToString());
		Rank2Rank.SetText(FaceBookApi.Action.Rank2Rank);
	}

	private void Update()
	{
	}

	public override void OnStart()
	{
		action = this;
		OkBtn.SetActive(value: false);
		ShareBtn.SetActive(value: false);
		InitFaceBookData();
		BaseUIAnimation.action.FaceBookRankAni(TopICon, DownICon, OkBtn, ShareBtn);
		BaseUIAnimation.action.SetLanguageFont("FaceBookRankOpenUIOK", FaceBookRankOpenUIOK, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("FaceBookRankOpenUIShare", FaceBookRankOpenUIShare, string.Empty);
	}

	protected override void OnAwake()
	{
		base.OnAwake();
	}

	protected override void OnRelease()
	{
		base.OnRelease();
	}

	public void ClickOkBtn()
	{
		StartCoroutine(CallCloseUI());
	}

	private IEnumerator CallCloseUI(bool bDouble = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseUI(bDouble);
	}

	public void ClickShareBtn()
	{
		StartCoroutine(CallShare());
	}

	private IEnumerator CallShare(bool bDouble = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		if (!FaceBookApi.Action.bLoginState())
		{
			FaceBookApi.Action.FackBookLogin();
		}
		else
		{
			FaceBookApi.Action.FBFeedShare();
		}
	}

	public void LoadImage(string FID, GameObject _RankImg)
	{
		if (FID == FaceBookApi.Action.UserId)
		{
			if ((bool)FaceBookApi.Action.MyFacebookImage)
			{
				_RankImg.GetComponent<Image>().sprite = FaceBookApi.Action.MyFacebookImage;
			}
			else
			{
				FaceBookApi.Action.LoadMyImg(_RankImg.GetComponent<Image>());
			}
		}
		else
		{
			try
			{
				_RankImg.GetComponent<Image>().sprite = FaceBookApi.Action.dFacebookImage[FID];
			}
			catch (Exception)
			{
				try
				{
					FaceBookApi.Action.LoadFrImgFB(FID, _RankImg.GetComponent<Image>());
				}
				catch (Exception)
				{
				}
			}
		}
	}
}
