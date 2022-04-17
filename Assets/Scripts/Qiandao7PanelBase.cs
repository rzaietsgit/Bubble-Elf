using System;
using UnityEngine;
using UnityEngine.UI;

public class Qiandao7PanelBase : BasePanel
{
	public Qiandao7PanelDetail detail;

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
		detail.CloseButton_Image = base.transform.Find("bg/CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("bg/CloseButton").gameObject.GetComponent<Button>();
		detail.nooktxt_Text = base.transform.Find("bg/Image/linepat/lineobj/nooktxt").gameObject.GetComponent<Text>();
		detail.nooktxt_Shadow = base.transform.Find("bg/Image/linepat/lineobj/nooktxt").gameObject.GetComponent<Shadow>();
		detail.nooktxt_ContentSizeFitter = base.transform.Find("bg/Image/linepat/lineobj/nooktxt").gameObject.GetComponent<ContentSizeFitter>();
		detail.Number_Text = base.transform.Find("bg/Image/linepat/lineobj/iconlinepat/Iconline/Number").gameObject.GetComponent<Text>();
		detail.Number_Shadow = base.transform.Find("bg/Image/linepat/lineobj/iconlinepat/Iconline/Number").gameObject.GetComponent<Shadow>();
		detail.Number_ContentSizeFitter = base.transform.Find("bg/Image/linepat/lineobj/iconlinepat/Iconline/Number").gameObject.GetComponent<ContentSizeFitter>();
		detail.Iconline_Image = base.transform.Find("bg/Image/linepat/lineobj/iconlinepat/Iconline").gameObject.GetComponent<Image>();
		detail.iconlinepat_GridLayoutGroup = base.transform.Find("bg/Image/linepat/lineobj/iconlinepat").gameObject.GetComponent<GridLayoutGroup>();
		detail.yesimg_Image = base.transform.Find("bg/Image/linepat/lineobj/yesimg").gameObject.GetComponent<Image>();
		detail.dayok_Text = base.transform.Find("bg/Image/linepat/lineobj/okimg/dayok").gameObject.GetComponent<Text>();
		detail.dayok_Shadow = base.transform.Find("bg/Image/linepat/lineobj/okimg/dayok").gameObject.GetComponent<Shadow>();
		detail.dayok_ContentSizeFitter = base.transform.Find("bg/Image/linepat/lineobj/okimg/dayok").gameObject.GetComponent<ContentSizeFitter>();
		detail.okimg_Image = base.transform.Find("bg/Image/linepat/lineobj/okimg").gameObject.GetComponent<Image>();
		detail.lineobj_Image = base.transform.Find("bg/Image/linepat/lineobj").gameObject.GetComponent<Image>();
		detail.lineobj_qiandaobj = base.transform.Find("bg/Image/linepat/lineobj").gameObject.GetComponent<qiandaobj>();
		detail.linepat_GridLayoutGroup = base.transform.Find("bg/Image/linepat").gameObject.GetComponent<GridLayoutGroup>();
		detail.Image_Image = base.transform.Find("bg/Image").gameObject.GetComponent<Image>();
		detail.btnText_Text = base.transform.Find("bg/Btn/Btn1/btnText").gameObject.GetComponent<Text>();
		detail.btnText_Shadow = base.transform.Find("bg/Btn/Btn1/btnText").gameObject.GetComponent<Shadow>();
		detail.Btn1_Image = base.transform.Find("bg/Btn/Btn1").gameObject.GetComponent<Image>();
		detail.Btn1_Button = base.transform.Find("bg/Btn/Btn1").gameObject.GetComponent<Button>();
		detail.remarktext_Text = base.transform.Find("bg/remarktext").gameObject.GetComponent<Text>();
		detail.remarktext_Shadow = base.transform.Find("bg/remarktext").gameObject.GetComponent<Shadow>();
		detail.remarktext_ContentSizeFitter = base.transform.Find("bg/remarktext").gameObject.GetComponent<ContentSizeFitter>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnCloseButton);
		BtnAnimationBase btnAnimationBase2 = detail.Btn1_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnBtn1);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}

	public virtual void OnBtn1()
	{
	}
}
