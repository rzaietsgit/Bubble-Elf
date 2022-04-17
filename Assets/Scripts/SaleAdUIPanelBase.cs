using System;
using UnityEngine;
using UnityEngine.UI;

public class SaleAdUIPanelBase : BasePanel
{
	public SaleAdUIPanelDetail detail;

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
