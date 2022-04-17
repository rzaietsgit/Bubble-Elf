using System;
using UnityEngine;
using UnityEngine.UI;

public class RateUsPanelBase : BasePanel
{
	public RateUsPanelDetail detail;

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
		detail.Icon_Image = base.transform.Find("bg/Icon").gameObject.GetComponent<Image>();
		detail.CloseButton_Image = base.transform.Find("bg/CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("bg/CloseButton").gameObject.GetComponent<Button>();
		detail.QuitUIRemark_Text = base.transform.Find("bg/QuitUIRemark").gameObject.GetComponent<Text>();
		detail.QuitUIRemark_Shadow = base.transform.Find("bg/QuitUIRemark").gameObject.GetComponent<Shadow>();
		detail.QuitUIRemark_ContentSizeFitter = base.transform.Find("bg/QuitUIRemark").gameObject.GetComponent<ContentSizeFitter>();
		detail.QuitUIContinuebtn_Text = base.transform.Find("bg/ContinueBtn/QuitUIContinuebtn").gameObject.GetComponent<Text>();
		detail.QuitUIContinuebtn_Shadow = base.transform.Find("bg/ContinueBtn/QuitUIContinuebtn").gameObject.GetComponent<Shadow>();
		detail.QuitUIContinuebtn_ContentSizeFitter = base.transform.Find("bg/ContinueBtn/QuitUIContinuebtn").gameObject.GetComponent<ContentSizeFitter>();
		detail.ContinueBtn_Image = base.transform.Find("bg/ContinueBtn").gameObject.GetComponent<Image>();
		detail.ContinueBtn_Button = base.transform.Find("bg/ContinueBtn").gameObject.GetComponent<Button>();
		detail.QuitUITitle_Text = base.transform.Find("bg/BuyBubbleTitleBg/QuitUITitle").gameObject.GetComponent<Text>();
		detail.QuitUITitle_Shadow = base.transform.Find("bg/BuyBubbleTitleBg/QuitUITitle").gameObject.GetComponent<Shadow>();
		detail.QuitUITitle_ContentSizeFitter = base.transform.Find("bg/BuyBubbleTitleBg/QuitUITitle").gameObject.GetComponent<ContentSizeFitter>();
		detail.BuyBubbleTitleBg_Image = base.transform.Find("bg/BuyBubbleTitleBg").gameObject.GetComponent<Image>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnCloseButton);
		BtnAnimationBase btnAnimationBase2 = detail.ContinueBtn_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnContinueBtn);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}

	public virtual void OnContinueBtn()
	{
	}
}
