using System;
using UnityEngine;
using UnityEngine.UI;

public class StartTipPanelBase : BasePanel
{
	public StartTipPanelDetail detail;

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
		detail.ltite_Text = base.transform.Find("bg/ltite").gameObject.GetComponent<Text>();
		detail.ltite_Shadow = base.transform.Find("bg/ltite").gameObject.GetComponent<Shadow>();
		detail.ltite_ContentSizeFitter = base.transform.Find("bg/ltite").gameObject.GetComponent<ContentSizeFitter>();
		detail.Image_Image = base.transform.Find("bg/GameObject/Image").gameObject.GetComponent<Image>();
		detail.Content_Text = base.transform.Find("bg/GameObject/Select1ScrollView (1)/Viewport/Content").gameObject.GetComponent<Text>();
		detail.Content_ContentSizeFitter = base.transform.Find("bg/GameObject/Select1ScrollView (1)/Viewport/Content").gameObject.GetComponent<ContentSizeFitter>();
		detail.Viewport_Mask = base.transform.Find("bg/GameObject/Select1ScrollView (1)/Viewport").gameObject.GetComponent<Mask>();
		detail.Viewport_Image = base.transform.Find("bg/GameObject/Select1ScrollView (1)/Viewport").gameObject.GetComponent<Image>();
		detail.Select1ScrollView1_ScrollRect = base.transform.Find("bg/GameObject/Select1ScrollView (1)").gameObject.GetComponent<ScrollRect>();
		detail.Select1ScrollView1_Image = base.transform.Find("bg/GameObject/Select1ScrollView (1)").gameObject.GetComponent<Image>();
		detail.imok1_Text = base.transform.Find("bg/imok1").gameObject.GetComponent<Text>();
		detail.imok1_Shadow = base.transform.Find("bg/imok1").gameObject.GetComponent<Shadow>();
		detail.imok1_ContentSizeFitter = base.transform.Find("bg/imok1").gameObject.GetComponent<ContentSizeFitter>();
		detail.btn1img_Image = base.transform.Find("bg/S_btn1/btn1img").gameObject.GetComponent<Image>();
		detail.S_btn1_Image = base.transform.Find("bg/S_btn1").gameObject.GetComponent<Image>();
		detail.S_btn1_Button = base.transform.Find("bg/S_btn1").gameObject.GetComponent<Button>();
		detail.Image_Image = base.transform.Find("bg/GameObject (1)/Image").gameObject.GetComponent<Image>();
		detail.Content_Text = base.transform.Find("bg/GameObject (1)/Select1ScrollView/Viewport/Content").gameObject.GetComponent<Text>();
		detail.Content_ContentSizeFitter = base.transform.Find("bg/GameObject (1)/Select1ScrollView/Viewport/Content").gameObject.GetComponent<ContentSizeFitter>();
		detail.Viewport_Mask = base.transform.Find("bg/GameObject (1)/Select1ScrollView/Viewport").gameObject.GetComponent<Mask>();
		detail.Viewport_Image = base.transform.Find("bg/GameObject (1)/Select1ScrollView/Viewport").gameObject.GetComponent<Image>();
		detail.Select1ScrollView_ScrollRect = base.transform.Find("bg/GameObject (1)/Select1ScrollView").gameObject.GetComponent<ScrollRect>();
		detail.Select1ScrollView_Image = base.transform.Find("bg/GameObject (1)/Select1ScrollView").gameObject.GetComponent<Image>();
		detail.btn2img_Image = base.transform.Find("bg/S_btn2/btn2img").gameObject.GetComponent<Image>();
		detail.S_btn2_Image = base.transform.Find("bg/S_btn2").gameObject.GetComponent<Image>();
		detail.S_btn2_Button = base.transform.Find("bg/S_btn2").gameObject.GetComponent<Button>();
		detail.imok2_Text = base.transform.Find("bg/imok2").gameObject.GetComponent<Text>();
		detail.imok2_Shadow = base.transform.Find("bg/imok2").gameObject.GetComponent<Shadow>();
		detail.imok2_ContentSizeFitter = base.transform.Find("bg/imok2").gameObject.GetComponent<ContentSizeFitter>();
		detail.okbtn_Text = base.transform.Find("bg/okokbtn/okbtn").gameObject.GetComponent<Text>();
		detail.okbtn_Shadow = base.transform.Find("bg/okokbtn/okbtn").gameObject.GetComponent<Shadow>();
		detail.okbtn_ContentSizeFitter = base.transform.Find("bg/okokbtn/okbtn").gameObject.GetComponent<ContentSizeFitter>();
		detail.okokbtn_Image = base.transform.Find("bg/okokbtn").gameObject.GetComponent<Image>();
		detail.okokbtn_Button = base.transform.Find("bg/okokbtn").gameObject.GetComponent<Button>();
		detail.S_btn001_Image = base.transform.Find("bg/S_btn001").gameObject.GetComponent<Image>();
		detail.S_btn001_Button = base.transform.Find("bg/S_btn001").gameObject.GetComponent<Button>();
		detail.S_btn002_Image = base.transform.Find("bg/S_btn002").gameObject.GetComponent<Image>();
		detail.S_btn002_Button = base.transform.Find("bg/S_btn002").gameObject.GetComponent<Button>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.S_btn1_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.STATIC);
		btnAnimationBase.SetAction(OnS_btn1);
		BtnAnimationBase btnAnimationBase2 = detail.S_btn2_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.STATIC);
		btnAnimationBase2.SetAction(OnS_btn2);
		BtnAnimationBase btnAnimationBase3 = detail.okokbtn_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase3.SetType(NewBtnType.NONE);
		btnAnimationBase3.SetAction(Onokokbtn);
		BtnAnimationBase btnAnimationBase4 = detail.S_btn001_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase4.SetType(NewBtnType.STATIC);
		btnAnimationBase4.SetAction(OnS_btn001);
		BtnAnimationBase btnAnimationBase5 = detail.S_btn002_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase5.SetType(NewBtnType.STATIC);
		btnAnimationBase5.SetAction(OnS_btn002);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnS_btn1()
	{
	}

	public virtual void OnS_btn2()
	{
	}

	public virtual void Onokokbtn()
	{
	}

	public virtual void OnS_btn001()
	{
	}

	public virtual void OnS_btn002()
	{
	}
}
