using System;
using UnityEngine;
using UnityEngine.UI;

public class Reward24UIPanelBase : BasePanel
{
	public Reward24UIPanelDetail detail;

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
		detail.bggezi_Image = base.transform.Find("bg/UI/bggezi").gameObject.GetComponent<Image>();
		detail.Image_Image = base.transform.Find("bg/UI/Image").gameObject.GetComponent<Image>();
		detail.Image1_Image = base.transform.Find("bg/UI/Image (1)").gameObject.GetComponent<Image>();
		detail.Image2_Image = base.transform.Find("bg/UI/Image (2)").gameObject.GetComponent<Image>();
		detail.TextDemo1_Text = base.transform.Find("bg/UI/TextDemo (1)").gameObject.GetComponent<Text>();
		detail.TextDemo1_Shadow = base.transform.Find("bg/UI/TextDemo (1)").gameObject.GetComponent<Shadow>();
		detail.TextDemo3_Text = base.transform.Find("bg/UI/TextDemo (3)").gameObject.GetComponent<Text>();
		detail.TextDemo3_Shadow = base.transform.Find("bg/UI/TextDemo (3)").gameObject.GetComponent<Shadow>();
		detail.TextDemo_Text = base.transform.Find("bg/UI/TextDemo").gameObject.GetComponent<Text>();
		detail.TextDemo_Shadow = base.transform.Find("bg/UI/TextDemo").gameObject.GetComponent<Shadow>();
		detail.TextDemo4_Text = base.transform.Find("bg/UI/lingquButton/TextDemo (4)").gameObject.GetComponent<Text>();
		detail.TextDemo4_Shadow = base.transform.Find("bg/UI/lingquButton/TextDemo (4)").gameObject.GetComponent<Shadow>();
		detail.lingquButton_Image = base.transform.Find("bg/UI/lingquButton").gameObject.GetComponent<Image>();
		detail.lingquButton_Button = base.transform.Find("bg/UI/lingquButton").gameObject.GetComponent<Button>();
		detail.UI_RawImage = base.transform.Find("bg/UI").gameObject.GetComponent<RawImage>();
		detail.CloseButton_Image = base.transform.Find("bg/CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("bg/CloseButton").gameObject.GetComponent<Button>();
		detail.Reward24UITitle_Text = base.transform.Find("bg/Reward24UITitle").gameObject.GetComponent<Text>();
		detail.Reward24UITitle_Shadow = base.transform.Find("bg/Reward24UITitle").gameObject.GetComponent<Shadow>();
		detail.Reward24UITitle_ContentSizeFitter = base.transform.Find("bg/Reward24UITitle").gameObject.GetComponent<ContentSizeFitter>();
		detail.TextDemo5_Text = base.transform.Find("bg/TextDemo (5)").gameObject.GetComponent<Text>();
		detail.TextDemo5_Shadow = base.transform.Find("bg/TextDemo (5)").gameObject.GetComponent<Shadow>();
		detail.TextDemo2_Text = base.transform.Find("bg/TextDemo (2)").gameObject.GetComponent<Text>();
		detail.TextDemo2_Shadow = base.transform.Find("bg/TextDemo (2)").gameObject.GetComponent<Shadow>();
		detail.time_Text = base.transform.Find("bg/time").gameObject.GetComponent<Text>();
		detail.time_Shadow = base.transform.Find("bg/time").gameObject.GetComponent<Shadow>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.lingquButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnlingquButton);
		BtnAnimationBase btnAnimationBase2 = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnCloseButton);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnlingquButton()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}
}
