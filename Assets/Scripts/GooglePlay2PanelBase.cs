using System;
using UnityEngine;
using UnityEngine.UI;

public class GooglePlay2PanelBase : BasePanel
{
	public GooglePlay2PanelDetail detail;

	private void Start()
	{
		try
		{
			SetAllMemberValue();
			InitUI();
		}
		catch (Exception arg)
		{
			UnityEngine.Debug.LogError("UI error :" + arg);
		}
	}

	public void SetAllMemberValue()
	{
		detail.TextDemo2_Text = base.transform.Find("bg/TextDemo (2)").gameObject.GetComponent<Text>();
		detail.TextDemo2_Shadow = base.transform.Find("bg/TextDemo (2)").gameObject.GetComponent<Shadow>();
		detail.TextDemo2_ContentSizeFitter = base.transform.Find("bg/TextDemo (2)").gameObject.GetComponent<ContentSizeFitter>();
		detail.TextDemo5_Text = base.transform.Find("bg/RankingList/TextDemo (5)").gameObject.GetComponent<Text>();
		detail.TextDemo5_Shadow = base.transform.Find("bg/RankingList/TextDemo (5)").gameObject.GetComponent<Shadow>();
		detail.TextDemo5_ContentSizeFitter = base.transform.Find("bg/RankingList/TextDemo (5)").gameObject.GetComponent<ContentSizeFitter>();
		detail.RankingList_Image = base.transform.Find("bg/RankingList").gameObject.GetComponent<Image>();
		detail.RankingList_Button = base.transform.Find("bg/RankingList").gameObject.GetComponent<Button>();
		detail.TextDemo5_Text = base.transform.Find("bg/Achievement/TextDemo (5)").gameObject.GetComponent<Text>();
		detail.TextDemo5_Shadow = base.transform.Find("bg/Achievement/TextDemo (5)").gameObject.GetComponent<Shadow>();
		detail.TextDemo5_ContentSizeFitter = base.transform.Find("bg/Achievement/TextDemo (5)").gameObject.GetComponent<ContentSizeFitter>();
		detail.Achievement_Image = base.transform.Find("bg/Achievement").gameObject.GetComponent<Image>();
		detail.Achievement_Button = base.transform.Find("bg/Achievement").gameObject.GetComponent<Button>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		detail.CloseButton_Image = base.transform.Find("CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("CloseButton").gameObject.GetComponent<Button>();
		BtnAnimationBase btnAnimationBase = detail.RankingList_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnRankingList);
		BtnAnimationBase btnAnimationBase2 = detail.Achievement_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnAchievement);
		BtnAnimationBase btnAnimationBase3 = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase3.SetType(NewBtnType.NONE);
		btnAnimationBase3.SetAction(OnCloseButton);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnRankingList()
	{
	}

	public virtual void OnAchievement()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}
}
