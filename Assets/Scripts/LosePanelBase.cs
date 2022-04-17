using System;
using UnityEngine;
using UnityEngine.UI;

public class LosePanelBase : BasePanel
{
	public LosePanelDetail detail;

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
		detail.LoseUILevelTitle_Text = base.transform.Find("Top/LoseUILevelTitle").gameObject.GetComponent<Text>();
		detail.LoseUILevelTitle_Shadow = base.transform.Find("Top/LoseUILevelTitle").gameObject.GetComponent<Shadow>();
		detail.LoseUILevelTitle_ContentSizeFitter = base.transform.Find("Top/LoseUILevelTitle").gameObject.GetComponent<ContentSizeFitter>();
		detail.Image_Image = base.transform.Find("Top/Image").gameObject.GetComponent<Image>();
		detail.LoseUILevelFailedText_Text = base.transform.Find("Top/CenterImg/LoseUILevelFailedText").gameObject.GetComponent<Text>();
		detail.LoseUILevelFailedText_Shadow = base.transform.Find("Top/CenterImg/LoseUILevelFailedText").gameObject.GetComponent<Shadow>();
		detail.LoseUILevelFailedText_ContentSizeFitter = base.transform.Find("Top/CenterImg/LoseUILevelFailedText").gameObject.GetComponent<ContentSizeFitter>();
		detail.CenterImg_Image = base.transform.Find("Top/CenterImg").gameObject.GetComponent<Image>();
		detail.star1_Image = base.transform.Find("Top/StarPanel/starbg1/star1").gameObject.GetComponent<Image>();
		detail.starbg1_Image = base.transform.Find("Top/StarPanel/starbg1").gameObject.GetComponent<Image>();
		detail.star2_Image = base.transform.Find("Top/StarPanel/starbg2/star2").gameObject.GetComponent<Image>();
		detail.starbg2_Image = base.transform.Find("Top/StarPanel/starbg2").gameObject.GetComponent<Image>();
		detail.star3_Image = base.transform.Find("Top/StarPanel/starbg3/star3").gameObject.GetComponent<Image>();
		detail.starbg3_Image = base.transform.Find("Top/StarPanel/starbg3").gameObject.GetComponent<Image>();
		detail.LoseLevelText_Text = base.transform.Find("Top/LoseStarPanel/StarBg/ScoreChina/LoseLevelText").gameObject.GetComponent<Text>();
		detail.LoseLevelText_Shadow = base.transform.Find("Top/LoseStarPanel/StarBg/ScoreChina/LoseLevelText").gameObject.GetComponent<Shadow>();
		detail.LoseLevelText_ContentSizeFitter = base.transform.Find("Top/LoseStarPanel/StarBg/ScoreChina/LoseLevelText").gameObject.GetComponent<ContentSizeFitter>();
		detail.WinScoreText_Text = base.transform.Find("Top/LoseStarPanel/StarBg/ScoreChina/WinScoreText").gameObject.GetComponent<Text>();
		detail.WinScoreText_Shadow = base.transform.Find("Top/LoseStarPanel/StarBg/ScoreChina/WinScoreText").gameObject.GetComponent<Shadow>();
		detail.StarBg_Image = base.transform.Find("Top/LoseStarPanel/StarBg").gameObject.GetComponent<Image>();
		detail.StarBg_GridLayoutGroup = base.transform.Find("Top/LoseStarPanel/StarBg").gameObject.GetComponent<GridLayoutGroup>();
		detail.LoseUILevelRestartText_Text = base.transform.Find("Top/RestartBtn/LoseUILevelRestartText").gameObject.GetComponent<Text>();
		detail.LoseUILevelRestartText_Shadow = base.transform.Find("Top/RestartBtn/LoseUILevelRestartText").gameObject.GetComponent<Shadow>();
		detail.LoseUILevelRestartText_ContentSizeFitter = base.transform.Find("Top/RestartBtn/LoseUILevelRestartText").gameObject.GetComponent<ContentSizeFitter>();
		detail.RestartBtn_Image = base.transform.Find("Top/RestartBtn").gameObject.GetComponent<Image>();
		detail.RestartBtn_Button = base.transform.Find("Top/RestartBtn").gameObject.GetComponent<Button>();
		detail.CloseButton_Image = base.transform.Find("Top/CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("Top/CloseButton").gameObject.GetComponent<Button>();
		detail.Top_Image = base.transform.Find("Top").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.RestartBtn_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnRestartBtn);
		BtnAnimationBase btnAnimationBase2 = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnCloseButton);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnRestartBtn()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}
}
