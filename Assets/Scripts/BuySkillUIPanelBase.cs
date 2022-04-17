using System;
using UnityEngine;
using UnityEngine.UI;

public class BuySkillUIPanelBase : BasePanel
{
	public BuySkillUIPanelDetail detail;

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
		detail.bg_Image = base.transform.Find("Top/bg").gameObject.GetComponent<Image>();
		detail.Image_Image = base.transform.Find("Top/Image").gameObject.GetComponent<Image>();
		detail.CenterImg_Image = base.transform.Find("Top/CenterImg").gameObject.GetComponent<Image>();
		detail.BuySkillTitle_Text = base.transform.Find("Top/BuySkillTitle").gameObject.GetComponent<Text>();
		detail.BuySkillTitle_Shadow = base.transform.Find("Top/BuySkillTitle").gameObject.GetComponent<Shadow>();
		detail.BuySkillTitle_ContentSizeFitter = base.transform.Find("Top/BuySkillTitle").gameObject.GetComponent<ContentSizeFitter>();
		detail.BuySkillRemark_Text = base.transform.Find("Top/BuySkillRemark").gameObject.GetComponent<Text>();
		detail.BuySkillRemark_ContentSizeFitter = base.transform.Find("Top/BuySkillRemark").gameObject.GetComponent<ContentSizeFitter>();
		detail.Number_Image = base.transform.Find("Top/SkillPanel/BuySkillSon/Number").gameObject.GetComponent<Image>();
		detail.ViewCount_Text = base.transform.Find("Top/SkillPanel/BuySkillSon/ViewCount").gameObject.GetComponent<Text>();
		detail.ViewCount_Shadow = base.transform.Find("Top/SkillPanel/BuySkillSon/ViewCount").gameObject.GetComponent<Shadow>();
		detail.BuySkillSon_Image = base.transform.Find("Top/SkillPanel/BuySkillSon").gameObject.GetComponent<Image>();
		detail.BuySkillSon_BuySkillSon = base.transform.Find("Top/SkillPanel/BuySkillSon").gameObject.GetComponent<BuySkillSon>();
		detail.SkillPanel_GridLayoutGroup = base.transform.Find("Top/SkillPanel").gameObject.GetComponent<GridLayoutGroup>();
		detail.CloseButton_Image = base.transform.Find("Top/CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("Top/CloseButton").gameObject.GetComponent<Button>();
		detail.GoldText_Text = base.transform.Find("Top/Down/GoldPanel/GoldText").gameObject.GetComponent<Text>();
		detail.GoldText_Shadow = base.transform.Find("Top/Down/GoldPanel/GoldText").gameObject.GetComponent<Shadow>();
		detail.GoldIcon_Image = base.transform.Find("Top/Down/GoldPanel/GoldIcon").gameObject.GetComponent<Image>();
		detail.GoldPanel_Image = base.transform.Find("Top/Down/GoldPanel").gameObject.GetComponent<Image>();
		detail.Down_Image = base.transform.Find("Top/Down").gameObject.GetComponent<Image>();
		detail.moneytext_Text = base.transform.Find("Top/Image22 (2)/moneytext").gameObject.GetComponent<Text>();
		detail.Imagezs_Image = base.transform.Find("Top/Image22 (2)/Imagezs").gameObject.GetComponent<Image>();
		detail.Imagegb_Image = base.transform.Find("Top/Image22 (2)/Imagegb").gameObject.GetComponent<Image>();
		detail.Image222_Image = base.transform.Find("Top/Image22 (2)").gameObject.GetComponent<Image>();
		detail.Top_Image = base.transform.Find("Top").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnCloseButton);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}
}
