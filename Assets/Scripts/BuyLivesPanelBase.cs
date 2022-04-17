using System;
using UnityEngine;
using UnityEngine.UI;

public class BuyLivesPanelBase : BasePanel
{
	public BuyLivesPanelDetail detail;

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
		detail.CloseButton_Image = base.transform.Find("bg/CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("bg/CloseButton").gameObject.GetComponent<Button>();
		detail.Text_Text = base.transform.Find("bg/GoldTitleBg/Text").gameObject.GetComponent<Text>();
		detail.Text_Shadow = base.transform.Find("bg/GoldTitleBg/Text").gameObject.GetComponent<Shadow>();
		detail.GoldTitleBg_Image = base.transform.Find("bg/GoldTitleBg").gameObject.GetComponent<Image>();
		detail.Image_Image = base.transform.Find("bg/ImgBg/Image").gameObject.GetComponent<Image>();
		detail.Image1_Image = base.transform.Find("bg/ImgBg/Image (1)").gameObject.GetComponent<Image>();
		detail.ImgBg_Image = base.transform.Find("bg/ImgBg").gameObject.GetComponent<Image>();
		detail.Text_Text = base.transform.Find("bg/PlayButton/Text").gameObject.GetComponent<Text>();
		detail.Text_Shadow = base.transform.Find("bg/PlayButton/Text").gameObject.GetComponent<Shadow>();
		detail.PlayButton_Image = base.transform.Find("bg/PlayButton").gameObject.GetComponent<Image>();
		detail.PlayButton_Button = base.transform.Find("bg/PlayButton").gameObject.GetComponent<Button>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnCloseButton);
		BtnAnimationBase btnAnimationBase2 = detail.PlayButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnPlayButton);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}

	public virtual void OnPlayButton()
	{
	}
}
