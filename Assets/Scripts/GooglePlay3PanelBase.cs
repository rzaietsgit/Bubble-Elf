using System;
using UnityEngine;
using UnityEngine.UI;

public class GooglePlay3PanelBase : BasePanel
{
	public GooglePlay3PanelDetail detail;

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
		detail.Title_Text = base.transform.Find("bg/Title").gameObject.GetComponent<Text>();
		detail.Title_Shadow = base.transform.Find("bg/Title").gameObject.GetComponent<Shadow>();
		detail.Title_ContentSizeFitter = base.transform.Find("bg/Title").gameObject.GetComponent<ContentSizeFitter>();
		detail.LoginText_Text = base.transform.Find("bg/Login/LoginText").gameObject.GetComponent<Text>();
		detail.LoginText_Shadow = base.transform.Find("bg/Login/LoginText").gameObject.GetComponent<Shadow>();
		detail.LoginText_ContentSizeFitter = base.transform.Find("bg/Login/LoginText").gameObject.GetComponent<ContentSizeFitter>();
		detail.Login_Image = base.transform.Find("bg/Login").gameObject.GetComponent<Image>();
		detail.Login_Button = base.transform.Find("bg/Login").gameObject.GetComponent<Button>();
		detail.RankingListText_Text = base.transform.Find("bg/RankingList/RankingListText").gameObject.GetComponent<Text>();
		detail.RankingListText_Shadow = base.transform.Find("bg/RankingList/RankingListText").gameObject.GetComponent<Shadow>();
		detail.RankingListText_ContentSizeFitter = base.transform.Find("bg/RankingList/RankingListText").gameObject.GetComponent<ContentSizeFitter>();
		detail.RankingList_Image = base.transform.Find("bg/RankingList").gameObject.GetComponent<Image>();
		detail.RankingList_Button = base.transform.Find("bg/RankingList").gameObject.GetComponent<Button>();
		detail.AchievementText_Text = base.transform.Find("bg/Achievement/AchievementText").gameObject.GetComponent<Text>();
		detail.AchievementText_Shadow = base.transform.Find("bg/Achievement/AchievementText").gameObject.GetComponent<Shadow>();
		detail.AchievementText_ContentSizeFitter = base.transform.Find("bg/Achievement/AchievementText").gameObject.GetComponent<ContentSizeFitter>();
		detail.Achievement_Image = base.transform.Find("bg/Achievement").gameObject.GetComponent<Image>();
		detail.Achievement_Button = base.transform.Find("bg/Achievement").gameObject.GetComponent<Button>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		detail.CloseButton_Image = base.transform.Find("CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("CloseButton").gameObject.GetComponent<Button>();
		BtnAnimationBase btnAnimationBase = detail.Login_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnLogin);
		BtnAnimationBase btnAnimationBase2 = detail.RankingList_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnRankingList);
		BtnAnimationBase btnAnimationBase3 = detail.Achievement_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase3.SetType(NewBtnType.NONE);
		btnAnimationBase3.SetAction(OnAchievement);
		BtnAnimationBase btnAnimationBase4 = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase4.SetType(NewBtnType.NONE);
		btnAnimationBase4.SetAction(OnCloseButton);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnLogin()
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
