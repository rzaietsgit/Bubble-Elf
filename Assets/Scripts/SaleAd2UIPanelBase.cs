using System;
using UnityEngine;
using UnityEngine.UI;

public class SaleAd2UIPanelBase : BasePanel
{
	public SaleAd2UIPanelDetail detail;

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
		detail.AD1_Image = base.transform.Find("Top/AD1").gameObject.GetComponent<Image>();
		detail.AD2_Image = base.transform.Find("Top/AD2").gameObject.GetComponent<Image>();
		detail.AD3_Image = base.transform.Find("Top/AD3").gameObject.GetComponent<Image>();
		detail.AD4_Image = base.transform.Find("Top/AD4").gameObject.GetComponent<Image>();
		detail.AD5_Image = base.transform.Find("Top/AD5").gameObject.GetComponent<Image>();
		detail.Title_Image = base.transform.Find("Top/Title").gameObject.GetComponent<Image>();
		detail.AD33_Image = base.transform.Find("Top/AD33").gameObject.GetComponent<Image>();
		detail.AD333_Image = base.transform.Find("Top/AD333").gameObject.GetComponent<Image>();
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
