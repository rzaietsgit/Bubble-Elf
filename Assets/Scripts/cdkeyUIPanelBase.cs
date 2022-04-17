using System;
using UnityEngine;
using UnityEngine.UI;

public class cdkeyUIPanelBase : BasePanel
{
	public cdkeyUIPanelDetail detail;

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
		detail.cdkeyUITitle_Text = base.transform.Find("bg/TitleBg/cdkeyUITitle").gameObject.GetComponent<Text>();
		detail.cdkeyUITitle_Shadow = base.transform.Find("bg/TitleBg/cdkeyUITitle").gameObject.GetComponent<Shadow>();
		detail.cdkeyUITitle_ContentSizeFitter = base.transform.Find("bg/TitleBg/cdkeyUITitle").gameObject.GetComponent<ContentSizeFitter>();
		detail.TitleBg_Image = base.transform.Find("bg/TitleBg").gameObject.GetComponent<Image>();
		detail.btnText_Text = base.transform.Find("bg/bg1/Button/btnText").gameObject.GetComponent<Text>();
		detail.btnText_Shadow = base.transform.Find("bg/bg1/Button/btnText").gameObject.GetComponent<Shadow>();
		detail.Button_Image = base.transform.Find("bg/bg1/Button").gameObject.GetComponent<Image>();
		detail.Button_Button = base.transform.Find("bg/bg1/Button").gameObject.GetComponent<Button>();
		detail.err_Text = base.transform.Find("bg/bg1/err").gameObject.GetComponent<Text>();
		detail.err_Shadow = base.transform.Find("bg/bg1/err").gameObject.GetComponent<Shadow>();
		detail.err_ContentSizeFitter = base.transform.Find("bg/bg1/err").gameObject.GetComponent<ContentSizeFitter>();
		detail.Placeholder_Text = base.transform.Find("bg/bg1/InputField (1)/Placeholder").gameObject.GetComponent<Text>();
		detail.Text_Text = base.transform.Find("bg/bg1/InputField (1)/Text").gameObject.GetComponent<Text>();
		detail.InputField1_Image = base.transform.Find("bg/bg1/InputField (1)").gameObject.GetComponent<Image>();
		detail.InputField1_InputField = base.transform.Find("bg/bg1/InputField (1)").gameObject.GetComponent<InputField>();
		detail.bg1_Image = base.transform.Find("bg/bg1").gameObject.GetComponent<Image>();
		detail.cdkeyUIRemarkText_Text = base.transform.Find("bg/cdkeyUIRemarkText").gameObject.GetComponent<Text>();
		detail.cdkeyUIRemarkText_Shadow = base.transform.Find("bg/cdkeyUIRemarkText").gameObject.GetComponent<Shadow>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnCloseButton);
		BtnAnimationBase btnAnimationBase2 = detail.Button_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnButton);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}

	public virtual void OnButton()
	{
	}
}
