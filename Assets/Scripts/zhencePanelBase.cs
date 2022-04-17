using System;
using UnityEngine;
using UnityEngine.UI;

public class zhencePanelBase : BasePanel
{
	public zhencePanelDetail detail;

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
		detail.Image_Image = base.transform.Find("bg/xinxi1/Image").gameObject.GetComponent<Image>();
		detail.Content_Text = base.transform.Find("bg/xinxi1/Select1ScrollView1/Viewport/Content").gameObject.GetComponent<Text>();
		detail.Content_ContentSizeFitter = base.transform.Find("bg/xinxi1/Select1ScrollView1/Viewport/Content").gameObject.GetComponent<ContentSizeFitter>();
		detail.Viewport_Mask = base.transform.Find("bg/xinxi1/Select1ScrollView1/Viewport").gameObject.GetComponent<Mask>();
		detail.Viewport_Image = base.transform.Find("bg/xinxi1/Select1ScrollView1/Viewport").gameObject.GetComponent<Image>();
		detail.Select1ScrollView1_ScrollRect = base.transform.Find("bg/xinxi1/Select1ScrollView1").gameObject.GetComponent<ScrollRect>();
		detail.Select1ScrollView1_Image = base.transform.Find("bg/xinxi1/Select1ScrollView1").gameObject.GetComponent<Image>();
		detail.title_Text = base.transform.Find("bg/xinxi1/title").gameObject.GetComponent<Text>();
		detail.title_Gradient = base.transform.Find("bg/xinxi1/title").gameObject.GetComponent<Gradient>();
		detail.title_Shadow = base.transform.Find("bg/xinxi1/title").gameObject.GetComponent<Shadow>();
		detail.title_ContentSizeFitter = base.transform.Find("bg/xinxi1/title").gameObject.GetComponent<ContentSizeFitter>();
		detail.Text1_Text = base.transform.Find("bg/xinxi1/Language (2)/Text1").gameObject.GetComponent<Text>();
		detail.Text1_Shadow = base.transform.Find("bg/xinxi1/Language (2)/Text1").gameObject.GetComponent<Shadow>();
		detail.Text1_ContentSizeFitter = base.transform.Find("bg/xinxi1/Language (2)/Text1").gameObject.GetComponent<ContentSizeFitter>();
		detail.Language2_Image = base.transform.Find("bg/xinxi1/Language (2)").gameObject.GetComponent<Image>();
		detail.Language2_Button = base.transform.Find("bg/xinxi1/Language (2)").gameObject.GetComponent<Button>();
		detail.Content_Text = base.transform.Find("bg/xinxi1/Select1ScrollView2/Viewport/Content").gameObject.GetComponent<Text>();
		detail.Content_ContentSizeFitter = base.transform.Find("bg/xinxi1/Select1ScrollView2/Viewport/Content").gameObject.GetComponent<ContentSizeFitter>();
		detail.Viewport_Mask = base.transform.Find("bg/xinxi1/Select1ScrollView2/Viewport").gameObject.GetComponent<Mask>();
		detail.Viewport_Image = base.transform.Find("bg/xinxi1/Select1ScrollView2/Viewport").gameObject.GetComponent<Image>();
		detail.Select1ScrollView2_ScrollRect = base.transform.Find("bg/xinxi1/Select1ScrollView2").gameObject.GetComponent<ScrollRect>();
		detail.Select1ScrollView2_Image = base.transform.Find("bg/xinxi1/Select1ScrollView2").gameObject.GetComponent<Image>();
		detail.xinxi1_Image = base.transform.Find("bg/xinxi1").gameObject.GetComponent<Image>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		detail.CloseButton_Image = base.transform.Find("CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("CloseButton").gameObject.GetComponent<Button>();
		BtnAnimationBase btnAnimationBase = detail.Language2_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnLanguage2);
		BtnAnimationBase btnAnimationBase2 = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnCloseButton);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnLanguage2()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}
}
