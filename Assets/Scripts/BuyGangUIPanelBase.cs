using System;
using UnityEngine;
using UnityEngine.UI;

public class BuyGangUIPanelBase : BasePanel
{
	public BuyGangUIPanelDetail detail;

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
		detail.BuyGangTitle_Text = base.transform.Find("Top/BuyGangTitle").gameObject.GetComponent<Text>();
		detail.BuyGangTitle_Shadow = base.transform.Find("Top/BuyGangTitle").gameObject.GetComponent<Shadow>();
		detail.BuyGangTitle_ContentSizeFitter = base.transform.Find("Top/BuyGangTitle").gameObject.GetComponent<ContentSizeFitter>();
		detail.GamgIcon1_Image = base.transform.Find("Top/GangIcon/GamgIcon1").gameObject.GetComponent<Image>();
		detail.GamgIcon2_Image = base.transform.Find("Top/GangIcon/GamgIcon2").gameObject.GetComponent<Image>();
		detail._3yuan_Text = base.transform.Find("Top/GangIcon/_3yuan").gameObject.GetComponent<Text>();
		detail._3yuan_Shadow = base.transform.Find("Top/GangIcon/_3yuan").gameObject.GetComponent<Shadow>();
		detail.Image_Image = base.transform.Find("Top/GangIcon/Image").gameObject.GetComponent<Image>();
		detail.GangIcon_Image = base.transform.Find("Top/GangIcon").gameObject.GetComponent<Image>();
		detail.BuyGangRemark_Text = base.transform.Find("Top/BuyGangRemark").gameObject.GetComponent<Text>();
		detail.BuyGangRemark_Shadow = base.transform.Find("Top/BuyGangRemark").gameObject.GetComponent<Shadow>();
		detail.BuyGangRemark_ContentSizeFitter = base.transform.Find("Top/BuyGangRemark").gameObject.GetComponent<ContentSizeFitter>();
		detail.CloseButton_Image = base.transform.Find("Top/CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("Top/CloseButton").gameObject.GetComponent<Button>();
		detail.GoldText_Text = base.transform.Find("Top/Down/GoldPanel/GoldText").gameObject.GetComponent<Text>();
		detail.GoldText_Shadow = base.transform.Find("Top/Down/GoldPanel/GoldText").gameObject.GetComponent<Shadow>();
		detail.GoldIcon_Image = base.transform.Find("Top/Down/GoldPanel/GoldIcon").gameObject.GetComponent<Image>();
		detail.GoldPanel_Image = base.transform.Find("Top/Down/GoldPanel").gameObject.GetComponent<Image>();
		detail.Down_Image = base.transform.Find("Top/Down").gameObject.GetComponent<Image>();
		detail.Btntext_Text = base.transform.Find("Top/PayGangBtn/Btntext").gameObject.GetComponent<Text>();
		detail.Btntext_Shadow = base.transform.Find("Top/PayGangBtn/Btntext").gameObject.GetComponent<Shadow>();
		detail.Image_Image = base.transform.Find("Top/PayGangBtn/Image").gameObject.GetComponent<Image>();
		detail.PayGangBtn_Image = base.transform.Find("Top/PayGangBtn").gameObject.GetComponent<Image>();
		detail.PayGangBtn_Button = base.transform.Find("Top/PayGangBtn").gameObject.GetComponent<Button>();
		detail.moneytext_Text = base.transform.Find("Top/Image22 (1)/moneytext").gameObject.GetComponent<Text>();
		detail.Image22_Image = base.transform.Find("Top/Image22 (1)/Image22").gameObject.GetComponent<Image>();
		detail.Image221_Image = base.transform.Find("Top/Image22 (1)").gameObject.GetComponent<Image>();
		detail.Top_Image = base.transform.Find("Top").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnCloseButton);
		BtnAnimationBase btnAnimationBase2 = detail.PayGangBtn_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnPayGangBtn);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}

	public virtual void OnPayGangBtn()
	{
	}
}
