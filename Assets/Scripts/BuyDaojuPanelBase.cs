using System;
using UnityEngine;
using UnityEngine.UI;

public class BuyDaojuPanelBase : BasePanel
{
	public BuyDaojuPanelDetail detail;

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
		detail.Mask_Image = base.transform.Find("Mask").gameObject.GetComponent<Image>();
		detail.CloseButton_Image = base.transform.Find("bg/CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("bg/CloseButton").gameObject.GetComponent<Button>();
		detail.BuyBubbleTtitle_Text = base.transform.Find("bg/BuyBubbleTtitle").gameObject.GetComponent<Text>();
		detail.BuyBubbleTtitle_Shadow = base.transform.Find("bg/BuyBubbleTtitle").gameObject.GetComponent<Shadow>();
		detail.BuyBubbleTtitle_ContentSizeFitter = base.transform.Find("bg/BuyBubbleTtitle").gameObject.GetComponent<ContentSizeFitter>();
		detail.SkillCount_Text = base.transform.Find("bg/number/Icon/SkillCount").gameObject.GetComponent<Text>();
		detail.SkillCount_Shadow = base.transform.Find("bg/number/Icon/SkillCount").gameObject.GetComponent<Shadow>();
		detail.SkillCount_ContentSizeFitter = base.transform.Find("bg/number/Icon/SkillCount").gameObject.GetComponent<ContentSizeFitter>();
		detail.Icon_Image = base.transform.Find("bg/number/Icon").gameObject.GetComponent<Image>();
		detail.GoldNumber_Text = base.transform.Find("bg/number/PayBubble/GoldNumber").gameObject.GetComponent<Text>();
		detail.GoldNumber_Shadow = base.transform.Find("bg/number/PayBubble/GoldNumber").gameObject.GetComponent<Shadow>();
		detail.typeicon1_Image = base.transform.Find("bg/number/PayBubble/typeicon1").gameObject.GetComponent<Image>();
		detail.PayBubble_Image = base.transform.Find("bg/number/PayBubble").gameObject.GetComponent<Image>();
		detail.PayBubble_Button = base.transform.Find("bg/number/PayBubble").gameObject.GetComponent<Button>();
		detail.bag_icon_timelimit_Image = base.transform.Find("bg/number/bag_icon_timelimit").gameObject.GetComponent<Image>();
		detail.bag_icon_timelimitRemark_Text = base.transform.Find("bg/number/bag_icon_timelimitRemark").gameObject.GetComponent<Text>();
		detail.bag_icon_timelimitRemark_Shadow = base.transform.Find("bg/number/bag_icon_timelimitRemark").gameObject.GetComponent<Shadow>();
		detail.DaojuRemark_Text = base.transform.Find("bg/DaojuRemark").gameObject.GetComponent<Text>();
		detail.DaojuRemark_Shadow = base.transform.Find("bg/DaojuRemark").gameObject.GetComponent<Shadow>();
		detail.DaojuRemark_ContentSizeFitter = base.transform.Find("bg/DaojuRemark").gameObject.GetComponent<ContentSizeFitter>();
		detail.typeicon_Image = base.transform.Find("bg/down/typeicon").gameObject.GetComponent<Image>();
		detail.GoldText_Text = base.transform.Find("bg/down/GoldText").gameObject.GetComponent<Text>();
		detail.GoldText_Shadow = base.transform.Find("bg/down/GoldText").gameObject.GetComponent<Shadow>();
		detail.down_Image = base.transform.Find("bg/down").gameObject.GetComponent<Image>();
		detail.Image1_Image = base.transform.Find("bg/Image (1)").gameObject.GetComponent<Image>();
		detail.Daojutitle_Text = base.transform.Find("bg/Image/Daojutitle").gameObject.GetComponent<Text>();
		detail.Daojutitle_Shadow = base.transform.Find("bg/Image/Daojutitle").gameObject.GetComponent<Shadow>();
		detail.Daojutitle_ContentSizeFitter = base.transform.Find("bg/Image/Daojutitle").gameObject.GetComponent<ContentSizeFitter>();
		detail.Image_Image = base.transform.Find("bg/Image").gameObject.GetComponent<Image>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnCloseButton);
		BtnAnimationBase btnAnimationBase2 = detail.PayBubble_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnPayBubble);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}

	public virtual void OnPayBubble()
	{
	}
}
