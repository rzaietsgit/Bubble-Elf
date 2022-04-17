using System;
using UnityEngine;
using UnityEngine.UI;

public class NoticePanelBase : BasePanel
{
	public NoticePanelDetail detail;

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
		detail.Title_Text = base.transform.Find("bg/Title").gameObject.GetComponent<Text>();
		detail.Title_Shadow = base.transform.Find("bg/Title").gameObject.GetComponent<Shadow>();
		detail.Title_ContentSizeFitter = base.transform.Find("bg/Title").gameObject.GetComponent<ContentSizeFitter>();
		detail.OkText_Text = base.transform.Find("bg/OkBtn/OkText").gameObject.GetComponent<Text>();
		detail.OkText_Shadow = base.transform.Find("bg/OkBtn/OkText").gameObject.GetComponent<Shadow>();
		detail.OkText_ContentSizeFitter = base.transform.Find("bg/OkBtn/OkText").gameObject.GetComponent<ContentSizeFitter>();
		detail.OkBtn_Image = base.transform.Find("bg/OkBtn").gameObject.GetComponent<Image>();
		detail.OkBtn_Button = base.transform.Find("bg/OkBtn").gameObject.GetComponent<Button>();
		detail.RemarkText_Text = base.transform.Find("bg/Image/RemarkText").gameObject.GetComponent<Text>();
		detail.RemarkText_Shadow = base.transform.Find("bg/Image/RemarkText").gameObject.GetComponent<Shadow>();
		detail.RemarkText_ContentSizeFitter = base.transform.Find("bg/Image/RemarkText").gameObject.GetComponent<ContentSizeFitter>();
		detail.Image_Image = base.transform.Find("bg/Image").gameObject.GetComponent<Image>();
		detail.Image1_Image = base.transform.Find("bg/Image (1)").gameObject.GetComponent<Image>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		detail.CloseButton_Image = base.transform.Find("CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("CloseButton").gameObject.GetComponent<Button>();
		BtnAnimationBase btnAnimationBase = detail.OkBtn_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnOkBtn);
		BtnAnimationBase btnAnimationBase2 = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnCloseButton);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnOkBtn()
	{
	}

	public virtual void OnCloseButton()
	{
		Singleton<DataManager>.Instance.bNoticePanelType = false;
		UI.Instance.ClosePanel();
	}
}
