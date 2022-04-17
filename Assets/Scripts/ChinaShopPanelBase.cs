using System;
using UnityEngine;
using UnityEngine.UI;

public class ChinaShopPanelBase : BasePanel
{
	public ChinaShopPanelDetail detail;

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
		detail.CloseButton_Image = base.transform.Find("bg/BG2/CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("bg/BG2/CloseButton").gameObject.GetComponent<Button>();
		detail.BG2_Image = base.transform.Find("bg/BG2").gameObject.GetComponent<Image>();
		detail.UITitle_Text = base.transform.Find("bg/UITitle").gameObject.GetComponent<Text>();
		detail.UITitle_Shadow = base.transform.Find("bg/UITitle").gameObject.GetComponent<Shadow>();
		detail.UITitle_ContentSizeFitter = base.transform.Find("bg/UITitle").gameObject.GetComponent<ContentSizeFitter>();
		detail.GameObjectFather_GridLayoutGroup = base.transform.Find("bg/libaoobj/GameObjectFather").gameObject.GetComponent<GridLayoutGroup>();
		detail.GameObject2_Image = base.transform.Find("bg/libaoobj/GameObject2").gameObject.GetComponent<Image>();
		detail.GameObject2_Mask = base.transform.Find("bg/libaoobj/GameObject2").gameObject.GetComponent<Mask>();
		detail.GameObject2_ScrollRect = base.transform.Find("bg/libaoobj/GameObject2").gameObject.GetComponent<ScrollRect>();
		detail.GameObject_GridLayoutGroup = base.transform.Find("bg/zuanshiobj/GameObject").gameObject.GetComponent<GridLayoutGroup>();
		detail.zuanshiobj_ScrollRect = base.transform.Find("bg/zuanshiobj").gameObject.GetComponent<ScrollRect>();
		detail.zuanshiobj_Image = base.transform.Find("bg/zuanshiobj").gameObject.GetComponent<Image>();
		detail.zuanshiobj_Mask = base.transform.Find("bg/zuanshiobj").gameObject.GetComponent<Mask>();
		detail.daojishitime_Text = base.transform.Find("bg/daojuobj/bg/daojishitime").gameObject.GetComponent<Text>();
		detail.daojishitime_Shadow = base.transform.Find("bg/daojuobj/bg/daojishitime").gameObject.GetComponent<Shadow>();
		detail.daojusTime_Text = base.transform.Find("bg/daojuobj/bg/daojusTime").gameObject.GetComponent<Text>();
		detail.daojusTime_Shadow = base.transform.Find("bg/daojuobj/bg/daojusTime").gameObject.GetComponent<Shadow>();
		detail.daojuImage_Image = base.transform.Find("bg/daojuobj/bg/daojuResBtn/daojuImage").gameObject.GetComponent<Image>();
		detail.daojishitext1_Text = base.transform.Find("bg/daojuobj/bg/daojuResBtn/daojishitext1").gameObject.GetComponent<Text>();
		detail.daojishitext1_Shadow = base.transform.Find("bg/daojuobj/bg/daojuResBtn/daojishitext1").gameObject.GetComponent<Shadow>();
		detail.daojishitext2_Text = base.transform.Find("bg/daojuobj/bg/daojuResBtn/daojishitext2").gameObject.GetComponent<Text>();
		detail.daojishitext2_Shadow = base.transform.Find("bg/daojuobj/bg/daojuResBtn/daojishitext2").gameObject.GetComponent<Shadow>();
		detail.daojuResBtn_Image = base.transform.Find("bg/daojuobj/bg/daojuResBtn").gameObject.GetComponent<Image>();
		detail.daojuResBtn_Button = base.transform.Find("bg/daojuobj/bg/daojuResBtn").gameObject.GetComponent<Button>();
		detail.bg_Image = base.transform.Find("bg/daojuobj/bg").gameObject.GetComponent<Image>();
		detail.father_GridLayoutGroup = base.transform.Find("bg/daojuobj/daojuobj___/father").gameObject.GetComponent<GridLayoutGroup>();
		detail.daojuobj____Image = base.transform.Find("bg/daojuobj/daojuobj___").gameObject.GetComponent<Image>();
		detail.daojuobj____ScrollRect = base.transform.Find("bg/daojuobj/daojuobj___").gameObject.GetComponent<ScrollRect>();
		detail.daojuobj____Mask = base.transform.Find("bg/daojuobj/daojuobj___").gameObject.GetComponent<Mask>();
		detail.liebiaoTitle_Text = base.transform.Find("bg/S_libaobtn/liebiaoTitle").gameObject.GetComponent<Text>();
		detail.liebiaoTitle_Shadow = base.transform.Find("bg/S_libaobtn/liebiaoTitle").gameObject.GetComponent<Shadow>();
		detail.S_libaobtn_Image = base.transform.Find("bg/S_libaobtn").gameObject.GetComponent<Image>();
		detail.S_libaobtn_Button = base.transform.Find("bg/S_libaobtn").gameObject.GetComponent<Button>();
		detail.daojuTitle_Text = base.transform.Find("bg/S_daojubtn/daojuTitle").gameObject.GetComponent<Text>();
		detail.daojuTitle_Shadow = base.transform.Find("bg/S_daojubtn/daojuTitle").gameObject.GetComponent<Shadow>();
		detail.S_daojubtn_Image = base.transform.Find("bg/S_daojubtn").gameObject.GetComponent<Image>();
		detail.S_daojubtn_Button = base.transform.Find("bg/S_daojubtn").gameObject.GetComponent<Button>();
		detail.zuanshiTitle_Text = base.transform.Find("bg/S_zuanshibtn/zuanshiTitle").gameObject.GetComponent<Text>();
		detail.zuanshiTitle_Shadow = base.transform.Find("bg/S_zuanshibtn/zuanshiTitle").gameObject.GetComponent<Shadow>();
		detail.S_zuanshibtn_Image = base.transform.Find("bg/S_zuanshibtn").gameObject.GetComponent<Image>();
		detail.S_zuanshibtn_Button = base.transform.Find("bg/S_zuanshibtn").gameObject.GetComponent<Button>();
		detail.Imagegoldgbtext_Text = base.transform.Find("bg/Imagegold/ImagegoldImage/Imagegoldgbtext").gameObject.GetComponent<Text>();
		detail.Imagegoldgbtext_Shadow = base.transform.Find("bg/Imagegold/ImagegoldImage/Imagegoldgbtext").gameObject.GetComponent<Shadow>();
		detail.ImagegoldImage_Image = base.transform.Find("bg/Imagegold/ImagegoldImage").gameObject.GetComponent<Image>();
		detail.Imagegold_Image = base.transform.Find("bg/Imagegold").gameObject.GetComponent<Image>();
		detail.Imagegemzstext_Text = base.transform.Find("bg/Imagegem/ImagegemImage/Imagegemzstext").gameObject.GetComponent<Text>();
		detail.Imagegemzstext_Shadow = base.transform.Find("bg/Imagegem/ImagegemImage/Imagegemzstext").gameObject.GetComponent<Shadow>();
		detail.ImagegemImage_Image = base.transform.Find("bg/Imagegem/ImagegemImage").gameObject.GetComponent<Image>();
		detail.Imagegem_Image = base.transform.Find("bg/Imagegem").gameObject.GetComponent<Image>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnCloseButton);
		BtnAnimationBase btnAnimationBase2 = detail.daojuResBtn_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OndaojuResBtn);
		BtnAnimationBase btnAnimationBase3 = detail.S_libaobtn_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase3.SetType(NewBtnType.STATIC);
		btnAnimationBase3.SetAction(OnS_libaobtn);
		BtnAnimationBase btnAnimationBase4 = detail.S_daojubtn_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase4.SetType(NewBtnType.STATIC);
		btnAnimationBase4.SetAction(OnS_daojubtn);
		BtnAnimationBase btnAnimationBase5 = detail.S_zuanshibtn_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase5.SetType(NewBtnType.STATIC);
		btnAnimationBase5.SetAction(OnS_zuanshibtn);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}

	public virtual void OndaojuResBtn()
	{
	}

	public virtual void OnS_libaobtn()
	{
	}

	public virtual void OnS_daojubtn()
	{
	}

	public virtual void OnS_zuanshibtn()
	{
	}
}
