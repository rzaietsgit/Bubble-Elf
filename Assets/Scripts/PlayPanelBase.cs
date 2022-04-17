using Spine.Unity;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayPanelBase : BasePanel
{
	public PlayPanelDetail detail;

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
		detail.PlayUILevelTitle_Text = base.transform.Find("Top/PlayUILevelTitle").gameObject.GetComponent<Text>();
		detail.PlayUILevelTitle_Shadow = base.transform.Find("Top/PlayUILevelTitle").gameObject.GetComponent<Shadow>();
		detail.PlayUILevelTitle_ContentSizeFitter = base.transform.Find("Top/PlayUILevelTitle").gameObject.GetComponent<ContentSizeFitter>();
		detail.CenterImag_Image = base.transform.Find("Top/CenterImag").gameObject.GetComponent<Image>();
		detail.star1_Image = base.transform.Find("Top/StarPanel/starbg1/star1").gameObject.GetComponent<Image>();
		detail.starbg1_Image = base.transform.Find("Top/StarPanel/starbg1").gameObject.GetComponent<Image>();
		detail.star2_Image = base.transform.Find("Top/StarPanel/starbg2/star2").gameObject.GetComponent<Image>();
		detail.starbg2_Image = base.transform.Find("Top/StarPanel/starbg2").gameObject.GetComponent<Image>();
		detail.star3_Image = base.transform.Find("Top/StarPanel/starbg3/star3").gameObject.GetComponent<Image>();
		detail.starbg3_Image = base.transform.Find("Top/StarPanel/starbg3").gameObject.GetComponent<Image>();
		detail.StarPanel_Image = base.transform.Find("Top/StarPanel").gameObject.GetComponent<Image>();
		detail.LevelRemarkText_Text = base.transform.Find("Top/LevelRemarkText").gameObject.GetComponent<Text>();
		detail.LevelRemarkText_Shadow = base.transform.Find("Top/LevelRemarkText").gameObject.GetComponent<Shadow>();
		detail.LevelRemarkText_ContentSizeFitter = base.transform.Find("Top/LevelRemarkText").gameObject.GetComponent<ContentSizeFitter>();
		detail.PlaySkillSelectFather_GridLayoutGroup = base.transform.Find("Top/SkillSelect/PlaySkillSelectFather").gameObject.GetComponent<GridLayoutGroup>();
		detail.SkillSelect_Image = base.transform.Find("Top/SkillSelect").gameObject.GetComponent<Image>();
		detail.PlayUIStarBtnText_Text = base.transform.Find("Top/StartBtn/PlayUIStarBtnText").gameObject.GetComponent<Text>();
		detail.PlayUIStarBtnText_Shadow = base.transform.Find("Top/StartBtn/PlayUIStarBtnText").gameObject.GetComponent<Shadow>();
		detail.PlayUIStarBtnText_ContentSizeFitter = base.transform.Find("Top/StartBtn/PlayUIStarBtnText").gameObject.GetComponent<ContentSizeFitter>();
		detail.finger_SkeletonAnimation = base.transform.Find("Top/StartBtn/finger").gameObject.GetComponent<SkeletonAnimation>();
		detail.StartBtn_Image = base.transform.Find("Top/StartBtn").gameObject.GetComponent<Image>();
		detail.StartBtn_Button = base.transform.Find("Top/StartBtn").gameObject.GetComponent<Button>();
		detail.PlayUIStarBtnText1_Text = base.transform.Find("Top/yunbuAdBtn1/PlayUIStarBtnText (1)").gameObject.GetComponent<Text>();
		detail.PlayUIStarBtnText1_Shadow = base.transform.Find("Top/yunbuAdBtn1/PlayUIStarBtnText (1)").gameObject.GetComponent<Shadow>();
		detail.PlayUIStarBtnText1_ContentSizeFitter = base.transform.Find("Top/yunbuAdBtn1/PlayUIStarBtnText (1)").gameObject.GetComponent<ContentSizeFitter>();
		detail.yunbuAdBtn1_Image = base.transform.Find("Top/yunbuAdBtn1").gameObject.GetComponent<Image>();
		detail.yunbuAdBtn1_Button = base.transform.Find("Top/yunbuAdBtn1").gameObject.GetComponent<Button>();
		detail.yunbuAdBtn2_Image = base.transform.Find("Top/yunbuAdBtn2").gameObject.GetComponent<Image>();
		detail.yunbuAdBtn2_Button = base.transform.Find("Top/yunbuAdBtn2").gameObject.GetComponent<Button>();
		detail.CloseButton_Image = base.transform.Find("Top/CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("Top/CloseButton").gameObject.GetComponent<Button>();
		detail.GuideImg_Image = base.transform.Find("Top/GuideImg").gameObject.GetComponent<Image>();
		detail.ChinaLoveImage_Image = base.transform.Find("Top/ChinaLoveImage").gameObject.GetComponent<Image>();
		detail.ui_elf_SkeletonAnimation = base.transform.Find("Top/LoveMove/fx_life/ui_elf").gameObject.GetComponent<SkeletonAnimation>();
		detail.LoveMove_Image = base.transform.Find("Top/LoveMove").gameObject.GetComponent<Image>();
		detail.Top_Image = base.transform.Find("Top").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.StartBtn_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnStartBtn);
		BtnAnimationBase btnAnimationBase2 = detail.yunbuAdBtn1_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnyunbuAdBtn1);
		BtnAnimationBase btnAnimationBase3 = detail.yunbuAdBtn2_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase3.SetType(NewBtnType.NONE);
		btnAnimationBase3.SetAction(OnyunbuAdBtn2);
		BtnAnimationBase btnAnimationBase4 = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase4.SetType(NewBtnType.NONE);
		btnAnimationBase4.SetAction(OnCloseButton);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnStartBtn()
	{
	}

	public virtual void OnyunbuAdBtn1()
	{
	}

	public virtual void OnyunbuAdBtn2()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}
}
