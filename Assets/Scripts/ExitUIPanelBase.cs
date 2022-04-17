using System;
using UnityEngine;
using UnityEngine.UI;

public class ExitUIPanelBase : BasePanel
{
	public ExitUIPanelDetail detail;

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
		detail.QuitUIRemark_Text = base.transform.Find("bg/QuitUIRemark").gameObject.GetComponent<Text>();
		detail.QuitUIRemark_Shadow = base.transform.Find("bg/QuitUIRemark").gameObject.GetComponent<Shadow>();
		detail.QuitUIRemark_ContentSizeFitter = base.transform.Find("bg/QuitUIRemark").gameObject.GetComponent<ContentSizeFitter>();
		detail.QuitUITitle_Text = base.transform.Find("bg/QuitUITitle").gameObject.GetComponent<Text>();
		detail.QuitUITitle_Shadow = base.transform.Find("bg/QuitUITitle").gameObject.GetComponent<Shadow>();
		detail.QuitUITitle_ContentSizeFitter = base.transform.Find("bg/QuitUITitle").gameObject.GetComponent<ContentSizeFitter>();
		detail.QuitUIQuitbtn_Text = base.transform.Find("bg/Button1/QuitUIQuitbtn").gameObject.GetComponent<Text>();
		detail.QuitUIQuitbtn_Shadow = base.transform.Find("bg/Button1/QuitUIQuitbtn").gameObject.GetComponent<Shadow>();
		detail.QuitUIQuitbtn_ContentSizeFitter = base.transform.Find("bg/Button1/QuitUIQuitbtn").gameObject.GetComponent<ContentSizeFitter>();
		detail.Button1_Image = base.transform.Find("bg/Button1").gameObject.GetComponent<Image>();
		detail.Button1_Button = base.transform.Find("bg/Button1").gameObject.GetComponent<Button>();
		detail.QuitUIContinuebtn_Text = base.transform.Find("bg/Button2/QuitUIContinuebtn").gameObject.GetComponent<Text>();
		detail.QuitUIContinuebtn_Shadow = base.transform.Find("bg/Button2/QuitUIContinuebtn").gameObject.GetComponent<Shadow>();
		detail.QuitUIContinuebtn_ContentSizeFitter = base.transform.Find("bg/Button2/QuitUIContinuebtn").gameObject.GetComponent<ContentSizeFitter>();
		detail.Button2_Image = base.transform.Find("bg/Button2").gameObject.GetComponent<Image>();
		detail.Button2_Button = base.transform.Find("bg/Button2").gameObject.GetComponent<Button>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.Button1_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnButton1);
		BtnAnimationBase btnAnimationBase2 = detail.Button2_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnButton2);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnButton1()
	{
	}

	public virtual void OnButton2()
	{
	}
}
