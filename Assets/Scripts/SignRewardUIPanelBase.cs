using System;
using UnityEngine;
using UnityEngine.UI;

public class SignRewardUIPanelBase : BasePanel
{
	public SignRewardUIPanelDetail detail;

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

	public override UIType GetUIType()
	{
		return UIType.STATIC;
	}

	public void SetAllMemberValue()
	{
		detail.CloseButton_Image = base.transform.Find("bg/CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("bg/CloseButton").gameObject.GetComponent<Button>();
		detail.Title_Text = base.transform.Find("bg/Title").gameObject.GetComponent<Text>();
		detail.Title_Shadow = base.transform.Find("bg/Title").gameObject.GetComponent<Shadow>();
		detail.TextDemo2_Text = base.transform.Find("bg/SigninButton/TextDemo (2)").gameObject.GetComponent<Text>();
		detail.TextDemo2_Shadow = base.transform.Find("bg/SigninButton/TextDemo (2)").gameObject.GetComponent<Shadow>();
		detail.SigninButton_Image = base.transform.Find("bg/SigninButton").gameObject.GetComponent<Image>();
		detail.SigninButton_Button = base.transform.Find("bg/SigninButton").gameObject.GetComponent<Button>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		detail.sigimgButton_Image = base.transform.Find("jbf/sigimgButton").gameObject.GetComponent<Image>();
		detail.sigimgButton_Button = base.transform.Find("jbf/sigimgButton").gameObject.GetComponent<Button>();
		detail.sigimgButton_sigimg = base.transform.Find("jbf/sigimgButton").gameObject.GetComponent<sigimg>();
		detail.HeadIcon_Image = base.transform.Find("jbf/HeadIcon").gameObject.GetComponent<Image>();
		detail.text_Text = base.transform.Find("jbf/max/text").gameObject.GetComponent<Text>();
		detail.text_Shadow = base.transform.Find("jbf/max/text").gameObject.GetComponent<Shadow>();
		detail.icon1_Image = base.transform.Find("jbf/max/icon1").gameObject.GetComponent<Image>();
		detail.text1_Text = base.transform.Find("jbf/max/text1").gameObject.GetComponent<Text>();
		detail.text1_Shadow = base.transform.Find("jbf/max/text1").gameObject.GetComponent<Shadow>();
		detail.text2_Text = base.transform.Find("jbf/max/text2").gameObject.GetComponent<Text>();
		detail.text2_Shadow = base.transform.Find("jbf/max/text2").gameObject.GetComponent<Shadow>();
		detail.icon2_Image = base.transform.Find("jbf/max/icon2").gameObject.GetComponent<Image>();
		detail.icon3_Image = base.transform.Find("jbf/max/icon3").gameObject.GetComponent<Image>();
		detail.text3_Text = base.transform.Find("jbf/max/text3").gameObject.GetComponent<Text>();
		detail.text3_Shadow = base.transform.Find("jbf/max/text3").gameObject.GetComponent<Shadow>();
		detail.text4_Text = base.transform.Find("jbf/max/text4").gameObject.GetComponent<Text>();
		detail.text4_Shadow = base.transform.Find("jbf/max/text4").gameObject.GetComponent<Shadow>();
		detail.icon4_Image = base.transform.Find("jbf/max/icon4").gameObject.GetComponent<Image>();
		detail.max_Image = base.transform.Find("jbf/max").gameObject.GetComponent<Image>();
		detail.icon5_Image = base.transform.Find("jbf/min/icon5").gameObject.GetComponent<Image>();
		detail.text5_Text = base.transform.Find("jbf/min/text5").gameObject.GetComponent<Text>();
		detail.text5_Shadow = base.transform.Find("jbf/min/text5").gameObject.GetComponent<Shadow>();
		detail.text6_Text = base.transform.Find("jbf/min/text6").gameObject.GetComponent<Text>();
		detail.text6_Shadow = base.transform.Find("jbf/min/text6").gameObject.GetComponent<Shadow>();
		detail.min_Image = base.transform.Find("jbf/min").gameObject.GetComponent<Image>();
		detail.jbf_Image = base.transform.Find("jbf").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnCloseButton);
		BtnAnimationBase btnAnimationBase2 = detail.SigninButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnSigninButton);
		BtnAnimationBase btnAnimationBase3 = detail.sigimgButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase3.SetType(NewBtnType.NONE);
		btnAnimationBase3.SetAction(OnsigimgButton);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}

	public virtual void OnSigninButton()
	{
	}

	public virtual void OnsigimgButton()
	{
	}
}
