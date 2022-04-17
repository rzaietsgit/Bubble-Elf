using System;
using UnityEngine;
using UnityEngine.UI;

public class GuideMaxUIPanelBase : BasePanel
{
	public GuideMaxUIPanelDetail detail;

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
		detail.mask_Image = base.transform.Find("mask").gameObject.GetComponent<Image>();
		detail.Image1_Image = base.transform.Find("Panel/Top/Image1").gameObject.GetComponent<Image>();
		detail.GuideMaxTitle_Text = base.transform.Find("Panel/Top/GuideMaxTitle").gameObject.GetComponent<Text>();
		detail.GuideMaxTitle_Shadow = base.transform.Find("Panel/Top/GuideMaxTitle").gameObject.GetComponent<Shadow>();
		detail.GuideMaxTitle_ContentSizeFitter = base.transform.Find("Panel/Top/GuideMaxTitle").gameObject.GetComponent<ContentSizeFitter>();
		detail.Close_Image = base.transform.Find("Panel/Top/Close").gameObject.GetComponent<Image>();
		detail.Close_Button = base.transform.Find("Panel/Top/Close").gameObject.GetComponent<Button>();
		detail.NextText_Text = base.transform.Find("Panel/Top/NextBtn2/NextText").gameObject.GetComponent<Text>();
		detail.NextText_Shadow = base.transform.Find("Panel/Top/NextBtn2/NextText").gameObject.GetComponent<Shadow>();
		detail.NextText_ContentSizeFitter = base.transform.Find("Panel/Top/NextBtn2/NextText").gameObject.GetComponent<ContentSizeFitter>();
		detail.NextBtn2_Image = base.transform.Find("Panel/Top/NextBtn2").gameObject.GetComponent<Image>();
		detail.NextBtn2_Button = base.transform.Find("Panel/Top/NextBtn2").gameObject.GetComponent<Button>();
		detail.GuideImage_Image = base.transform.Find("Panel/Top/GuideImage").gameObject.GetComponent<Image>();
		detail.GuideMaxRemark_Text = base.transform.Find("Panel/Top/Image/GuideMaxRemark").gameObject.GetComponent<Text>();
		detail.GuideMaxRemark_Shadow = base.transform.Find("Panel/Top/Image/GuideMaxRemark").gameObject.GetComponent<Shadow>();
		detail.GuideMaxRemark_ContentSizeFitter = base.transform.Find("Panel/Top/Image/GuideMaxRemark").gameObject.GetComponent<ContentSizeFitter>();
		detail.Image_Image = base.transform.Find("Panel/Top/Image").gameObject.GetComponent<Image>();
		detail.Top_Image = base.transform.Find("Panel/Top").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.Close_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnClose);
		BtnAnimationBase btnAnimationBase2 = detail.NextBtn2_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnNextBtn2);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnClose()
	{
	}

	public virtual void OnNextBtn2()
	{
	}
}
