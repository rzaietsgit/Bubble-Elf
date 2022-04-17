using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillTipPanelBase : BasePanel
{
	public SkillTipPanelDetail detail;

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
		detail.CloseButton_Image = base.transform.Find("bg/CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("bg/CloseButton").gameObject.GetComponent<Button>();
		detail.SkillTipUITitle_Text = base.transform.Find("bg/BuyBubbleTitleBg/SkillTipUITitle").gameObject.GetComponent<Text>();
		detail.SkillTipUITitle_Shadow = base.transform.Find("bg/BuyBubbleTitleBg/SkillTipUITitle").gameObject.GetComponent<Shadow>();
		detail.SkillTipUITitle_ContentSizeFitter = base.transform.Find("bg/BuyBubbleTitleBg/SkillTipUITitle").gameObject.GetComponent<ContentSizeFitter>();
		detail.BuyBubbleTitleBg_Image = base.transform.Find("bg/BuyBubbleTitleBg").gameObject.GetComponent<Image>();
		detail.bg_Image = base.transform.Find("bg/Down/bg").gameObject.GetComponent<Image>();
		detail.Icon_Image = base.transform.Find("bg/Down/Icon").gameObject.GetComponent<Image>();
		detail.SkillTipUIOkBtn_Text = base.transform.Find("bg/Down/EnterBtn/SkillTipUIOkBtn").gameObject.GetComponent<Text>();
		detail.SkillTipUIOkBtn_Shadow = base.transform.Find("bg/Down/EnterBtn/SkillTipUIOkBtn").gameObject.GetComponent<Shadow>();
		detail.SkillTipUIOkBtn_ContentSizeFitter = base.transform.Find("bg/Down/EnterBtn/SkillTipUIOkBtn").gameObject.GetComponent<ContentSizeFitter>();
		detail.EnterBtn_Image = base.transform.Find("bg/Down/EnterBtn").gameObject.GetComponent<Image>();
		detail.EnterBtn_Button = base.transform.Find("bg/Down/EnterBtn").gameObject.GetComponent<Button>();
		detail.SkillTipUIRemark_Text = base.transform.Find("bg/SkillTipUIRemark").gameObject.GetComponent<Text>();
		detail.SkillTipUIRemark_Shadow = base.transform.Find("bg/SkillTipUIRemark").gameObject.GetComponent<Shadow>();
		detail.SkillTipUIRemark_ContentSizeFitter = base.transform.Find("bg/SkillTipUIRemark").gameObject.GetComponent<ContentSizeFitter>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnCloseButton);
		BtnAnimationBase btnAnimationBase2 = detail.EnterBtn_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnEnterBtn);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}

	public virtual void OnEnterBtn()
	{
	}
}
