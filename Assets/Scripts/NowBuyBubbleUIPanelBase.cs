using System;
using UnityEngine;
using UnityEngine.UI;

public class NowBuyBubbleUIPanelBase : BasePanel
{
	public NowBuyBubbleUIPanelDetail detail;

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
		detail.mygold1_Text = base.transform.Find("bg/Icon (1)/mygold (1)").gameObject.GetComponent<Text>();
		detail.mygold1_Shadow = base.transform.Find("bg/Icon (1)/mygold (1)").gameObject.GetComponent<Shadow>();
		detail.Icon1_Image = base.transform.Find("bg/Icon (1)").gameObject.GetComponent<Image>();
		detail.CloseButton_Image = base.transform.Find("bg/CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("bg/CloseButton").gameObject.GetComponent<Button>();
		detail.BuyBubbleTtitle_Text = base.transform.Find("bg/BuyBubbleTtitle").gameObject.GetComponent<Text>();
		detail.BuyBubbleTtitle_Shadow = base.transform.Find("bg/BuyBubbleTtitle").gameObject.GetComponent<Shadow>();
		detail.BuyBubbleTtitle_ContentSizeFitter = base.transform.Find("bg/BuyBubbleTtitle").gameObject.GetComponent<ContentSizeFitter>();
		detail.Icon_Image = base.transform.Find("bg/Down/Icon").gameObject.GetComponent<Image>();
		detail.GoldNumber1233_Text = base.transform.Find("bg/Down/Grid/PayBubble/GoldNumber1233").gameObject.GetComponent<Text>();
		detail.GoldNumber1233_Shadow = base.transform.Find("bg/Down/Grid/PayBubble/GoldNumber1233").gameObject.GetComponent<Shadow>();
		detail.GoldNumberm_Text = base.transform.Find("bg/Down/Grid/PayBubble/GoldNumberm").gameObject.GetComponent<Text>();
		detail.GoldNumberm_Shadow = base.transform.Find("bg/Down/Grid/PayBubble/GoldNumberm").gameObject.GetComponent<Shadow>();
		detail.Icon1_Image = base.transform.Find("bg/Down/Grid/PayBubble/Icon (1)").gameObject.GetComponent<Image>();
		detail.PayBubble_Image = base.transform.Find("bg/Down/Grid/PayBubble").gameObject.GetComponent<Image>();
		detail.PayBubble_Button = base.transform.Find("bg/Down/Grid/PayBubble").gameObject.GetComponent<Button>();
		detail.Icon1_Image = base.transform.Find("bg/Down/Grid/PayBubblead/Icon (1)").gameObject.GetComponent<Image>();
		detail.PayBubblead_Image = base.transform.Find("bg/Down/Grid/PayBubblead").gameObject.GetComponent<Image>();
		detail.PayBubblead_Button = base.transform.Find("bg/Down/Grid/PayBubblead").gameObject.GetComponent<Button>();
		detail.Grid_GridLayoutGroup = base.transform.Find("bg/Down/Grid").gameObject.GetComponent<GridLayoutGroup>();
		detail.Down_Image = base.transform.Find("bg/Down").gameObject.GetComponent<Image>();
		detail.BuyBubbleRemark_Text = base.transform.Find("bg/BuyBubbleRemark").gameObject.GetComponent<Text>();
		detail.BuyBubbleRemark_Shadow = base.transform.Find("bg/BuyBubbleRemark").gameObject.GetComponent<Shadow>();
		detail.BuyBubbleRemark_ContentSizeFitter = base.transform.Find("bg/BuyBubbleRemark").gameObject.GetComponent<ContentSizeFitter>();
		detail.BuyBubbleRemark123_Text = base.transform.Find("bg/Image/BuyBubbleRemark123").gameObject.GetComponent<Text>();
		detail.BuyBubbleRemark123_Shadow = base.transform.Find("bg/Image/BuyBubbleRemark123").gameObject.GetComponent<Shadow>();
		detail.Image_Image = base.transform.Find("bg/Image").gameObject.GetComponent<Image>();
		detail.mygold_Text = base.transform.Find("bg/Image (1)/mygold").gameObject.GetComponent<Text>();
		detail.mygold_Shadow = base.transform.Find("bg/Image (1)/mygold").gameObject.GetComponent<Shadow>();
		detail.Image1_Image = base.transform.Find("bg/Image (1)").gameObject.GetComponent<Image>();
		detail.Image2_Image = base.transform.Find("bg/Image (2)").gameObject.GetComponent<Image>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnCloseButton);
		BtnAnimationBase btnAnimationBase2 = detail.PayBubble_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnPayBubble);
		BtnAnimationBase btnAnimationBase3 = detail.PayBubblead_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase3.SetType(NewBtnType.NONE);
		btnAnimationBase3.SetAction(OnPayBubblead);
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

	public virtual void OnPayBubblead()
	{
	}
}
