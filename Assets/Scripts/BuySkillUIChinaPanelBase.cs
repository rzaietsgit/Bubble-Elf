using System;
using UnityEngine;
using UnityEngine.UI;

public class BuySkillUIChinaPanelBase : BasePanel
{
	public BuySkillUIChinaPanelDetail detail;

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
		detail.BuySkillTitle_Text = base.transform.Find("Top/BuySkillTitle").gameObject.GetComponent<Text>();
		detail.BuySkillTitle_Shadow = base.transform.Find("Top/BuySkillTitle").gameObject.GetComponent<Shadow>();
		detail.BuySkillTitle_ContentSizeFitter = base.transform.Find("Top/BuySkillTitle").gameObject.GetComponent<ContentSizeFitter>();
		detail.CloseButton_Image = base.transform.Find("Top/CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("Top/CloseButton").gameObject.GetComponent<Button>();
		detail.Image1_Image = base.transform.Find("Top/Image/Image (1)").gameObject.GetComponent<Image>();
		detail.Image1_Image = base.transform.Find("Top/Image/Image1").gameObject.GetComponent<Image>();
		detail.Image_Image = base.transform.Find("Top/Image").gameObject.GetComponent<Image>();
		detail.BuySkillRemark1_Text = base.transform.Find("Top/BuySkillRemark (1)").gameObject.GetComponent<Text>();
		detail.BuySkillRemark1_Shadow = base.transform.Find("Top/BuySkillRemark (1)").gameObject.GetComponent<Shadow>();
		detail.BuySkillRemark1_ContentSizeFitter = base.transform.Find("Top/BuySkillRemark (1)").gameObject.GetComponent<ContentSizeFitter>();
		detail.MoneyText_Text = base.transform.Find("Top/MoneyText").gameObject.GetComponent<Text>();
		detail.MoneyText_Shadow = base.transform.Find("Top/MoneyText").gameObject.GetComponent<Shadow>();
		detail.Btntext_Text = base.transform.Find("Top/Button/Btntext").gameObject.GetComponent<Text>();
		detail.Btntext_Shadow = base.transform.Find("Top/Button/Btntext").gameObject.GetComponent<Shadow>();
		detail.Image_Image = base.transform.Find("Top/Button/Image").gameObject.GetComponent<Image>();
		detail.Button_Image = base.transform.Find("Top/Button").gameObject.GetComponent<Image>();
		detail.Button_Button = base.transform.Find("Top/Button").gameObject.GetComponent<Button>();
		detail.moneytext_Text = base.transform.Find("Top/Image22/moneytext").gameObject.GetComponent<Text>();
		detail.Image22_Image = base.transform.Find("Top/Image22/Image22").gameObject.GetComponent<Image>();
		detail.Image22_Image = base.transform.Find("Top/Image22").gameObject.GetComponent<Image>();
		detail.Top_Image = base.transform.Find("Top").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnCloseButton);
		BtnAnimationBase btnAnimationBase2 = detail.Button_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnButton);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}

	public virtual void OnButton()
	{
	}
}
