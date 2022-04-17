using System;
using UnityEngine;
using UnityEngine.UI;

public class SaleAd3UIPanelBase : BasePanel
{
	public SaleAd3UIPanelDetail detail;

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
		detail.Title_Image = base.transform.Find("Top/Title").gameObject.GetComponent<Image>();
		detail.Image1_Image = base.transform.Find("Top/PayText/Image (1)").gameObject.GetComponent<Image>();
		detail.PayText_Text = base.transform.Find("Top/PayText").gameObject.GetComponent<Text>();
		detail.PayText_Shadow = base.transform.Find("Top/PayText").gameObject.GetComponent<Shadow>();
		detail.ADMoney_Text = base.transform.Find("Top/ADMoney").gameObject.GetComponent<Text>();
		detail.ADMoney_Shadow = base.transform.Find("Top/ADMoney").gameObject.GetComponent<Shadow>();
		detail.AD3I1_Image = base.transform.Find("Top/AD3/obj3/Image/Image1/AD3I1").gameObject.GetComponent<Image>();
		detail.Image1_Image = base.transform.Find("Top/AD3/obj3/Image/Image1").gameObject.GetComponent<Image>();
		detail.AD3I2_Image = base.transform.Find("Top/AD3/obj3/Image/Image2/AD3I2").gameObject.GetComponent<Image>();
		detail.Image2_Image = base.transform.Find("Top/AD3/obj3/Image/Image2").gameObject.GetComponent<Image>();
		detail.AD3I3_Image = base.transform.Find("Top/AD3/obj3/Image/Image3/AD3I3").gameObject.GetComponent<Image>();
		detail.Image3_Image = base.transform.Find("Top/AD3/obj3/Image/Image3").gameObject.GetComponent<Image>();
		detail.AD3I4_Image = base.transform.Find("Top/AD3/obj3/Image/Image4/AD3I4").gameObject.GetComponent<Image>();
		detail.Image4_Image = base.transform.Find("Top/AD3/obj3/Image/Image4").gameObject.GetComponent<Image>();
		detail.AD3T1_Text = base.transform.Find("Top/AD3/obj3/Image/AD3T1").gameObject.GetComponent<Text>();
		detail.AD3T1_Shadow = base.transform.Find("Top/AD3/obj3/Image/AD3T1").gameObject.GetComponent<Shadow>();
		detail.AD3T2_Text = base.transform.Find("Top/AD3/obj3/Image/AD3T2").gameObject.GetComponent<Text>();
		detail.AD3T2_Shadow = base.transform.Find("Top/AD3/obj3/Image/AD3T2").gameObject.GetComponent<Shadow>();
		detail.AD3T3_Text = base.transform.Find("Top/AD3/obj3/Image/AD3T3").gameObject.GetComponent<Text>();
		detail.AD3T3_Shadow = base.transform.Find("Top/AD3/obj3/Image/AD3T3").gameObject.GetComponent<Shadow>();
		detail.AD3T4_Text = base.transform.Find("Top/AD3/obj3/Image/AD3T4").gameObject.GetComponent<Text>();
		detail.AD3T4_Shadow = base.transform.Find("Top/AD3/obj3/Image/AD3T4").gameObject.GetComponent<Shadow>();
		detail.Image_Image = base.transform.Find("Top/AD3/obj3/Image").gameObject.GetComponent<Image>();
		detail.AD3_Image = base.transform.Find("Top/AD3").gameObject.GetComponent<Image>();
		detail.PayText123123_Text = base.transform.Find("Top/Image123123/PayText123123").gameObject.GetComponent<Text>();
		detail.PayText123123_Shadow = base.transform.Find("Top/Image123123/PayText123123").gameObject.GetComponent<Shadow>();
		detail.ADTime_Text = base.transform.Find("Top/Image123123/ADTime").gameObject.GetComponent<Text>();
		detail.ADTime_Shadow = base.transform.Find("Top/Image123123/ADTime").gameObject.GetComponent<Shadow>();
		detail.Image123123_Image = base.transform.Find("Top/Image123123").gameObject.GetComponent<Image>();
		detail.Top_Image = base.transform.Find("Top").gameObject.GetComponent<Image>();
		detail.CloseButton_Image = base.transform.Find("CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("CloseButton").gameObject.GetComponent<Button>();
		detail.PayText_Text = base.transform.Find("PayBtn/PayText").gameObject.GetComponent<Text>();
		detail.PayText_Shadow = base.transform.Find("PayBtn/PayText").gameObject.GetComponent<Shadow>();
		detail.adfree_Image = base.transform.Find("PayBtn/adfree").gameObject.GetComponent<Image>();
		detail.PayBtn_Image = base.transform.Find("PayBtn").gameObject.GetComponent<Image>();
		detail.PayBtn_Button = base.transform.Find("PayBtn").gameObject.GetComponent<Button>();
		BtnAnimationBase btnAnimationBase = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnCloseButton);
		BtnAnimationBase btnAnimationBase2 = detail.PayBtn_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnPayBtn);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}

	public virtual void OnPayBtn()
	{
	}
}
