using System;
using UnityEngine;
using UnityEngine.UI;

public class YunbuAdShowPanelBase : BasePanel
{
	public YunbuAdShowPanelDetail detail;

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
		detail.GoldNumber123_Text = base.transform.Find("bg/Down/PayBubble/GoldNumber123").gameObject.GetComponent<Text>();
		detail.GoldNumber123_Shadow = base.transform.Find("bg/Down/PayBubble/GoldNumber123").gameObject.GetComponent<Shadow>();
		detail.PayBubble_Image = base.transform.Find("bg/Down/PayBubble").gameObject.GetComponent<Image>();
		detail.PayBubble_Button = base.transform.Find("bg/Down/PayBubble").gameObject.GetComponent<Button>();
		detail.Down_Image = base.transform.Find("bg/Down").gameObject.GetComponent<Image>();
		detail.BuyBubbleRemark_Text = base.transform.Find("bg/BuyBubbleRemark").gameObject.GetComponent<Text>();
		detail.BuyBubbleRemark_Shadow = base.transform.Find("bg/BuyBubbleRemark").gameObject.GetComponent<Shadow>();
		detail.BuyBubbleRemark_ContentSizeFitter = base.transform.Find("bg/BuyBubbleRemark").gameObject.GetComponent<ContentSizeFitter>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.PayBubble_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnPayBubble);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnPayBubble()
	{
	}
}
