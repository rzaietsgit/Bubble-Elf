using System;
using UnityEngine;
using UnityEngine.UI;

public class yuyanPanelBase : BasePanel
{
	public yuyanPanelDetail detail;

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
		detail.SetSetPanelTitle1_Text = base.transform.Find("bg/Languagepan/SetSetPanelTitle (1)").gameObject.GetComponent<Text>();
		detail.SetSetPanelTitle1_Gradient = base.transform.Find("bg/Languagepan/SetSetPanelTitle (1)").gameObject.GetComponent<Gradient>();
		detail.SetSetPanelTitle1_Shadow = base.transform.Find("bg/Languagepan/SetSetPanelTitle (1)").gameObject.GetComponent<Shadow>();
		detail.SetSetPanelTitle1_ContentSizeFitter = base.transform.Find("bg/Languagepan/SetSetPanelTitle (1)").gameObject.GetComponent<ContentSizeFitter>();
		detail.SetUILanguage_Text = base.transform.Find("bg/Languagepan/GameObject/Language (2)/SetUILanguage").gameObject.GetComponent<Text>();
		detail.SetUILanguage_Shadow = base.transform.Find("bg/Languagepan/GameObject/Language (2)/SetUILanguage").gameObject.GetComponent<Shadow>();
		detail.SetUILanguage_ContentSizeFitter = base.transform.Find("bg/Languagepan/GameObject/Language (2)/SetUILanguage").gameObject.GetComponent<ContentSizeFitter>();
		detail.Image_Image = base.transform.Find("bg/Languagepan/GameObject/Language (2)/Image/Image").gameObject.GetComponent<Image>();
		detail.Image_Image = base.transform.Find("bg/Languagepan/GameObject/Language (2)/Image").gameObject.GetComponent<Image>();
		detail.Language2_Image = base.transform.Find("bg/Languagepan/GameObject/Language (2)").gameObject.GetComponent<Image>();
		detail.Language2_Button = base.transform.Find("bg/Languagepan/GameObject/Language (2)").gameObject.GetComponent<Button>();
		detail.Language2_languageLoad = base.transform.Find("bg/Languagepan/GameObject/Language (2)").gameObject.GetComponent<languageLoad>();
		detail.SetUILanguage_Text = base.transform.Find("bg/Languagepan/GameObject/Language (3)/SetUILanguage").gameObject.GetComponent<Text>();
		detail.SetUILanguage_Shadow = base.transform.Find("bg/Languagepan/GameObject/Language (3)/SetUILanguage").gameObject.GetComponent<Shadow>();
		detail.SetUILanguage_ContentSizeFitter = base.transform.Find("bg/Languagepan/GameObject/Language (3)/SetUILanguage").gameObject.GetComponent<ContentSizeFitter>();
		detail.Image_Image = base.transform.Find("bg/Languagepan/GameObject/Language (3)/Image/Image").gameObject.GetComponent<Image>();
		detail.Image_Image = base.transform.Find("bg/Languagepan/GameObject/Language (3)/Image").gameObject.GetComponent<Image>();
		detail.Language3_Image = base.transform.Find("bg/Languagepan/GameObject/Language (3)").gameObject.GetComponent<Image>();
		detail.Language3_Button = base.transform.Find("bg/Languagepan/GameObject/Language (3)").gameObject.GetComponent<Button>();
		detail.Language3_languageLoad = base.transform.Find("bg/Languagepan/GameObject/Language (3)").gameObject.GetComponent<languageLoad>();
		detail.SetUILanguage_Text = base.transform.Find("bg/Languagepan/GameObject/Language (4)/SetUILanguage").gameObject.GetComponent<Text>();
		detail.SetUILanguage_Shadow = base.transform.Find("bg/Languagepan/GameObject/Language (4)/SetUILanguage").gameObject.GetComponent<Shadow>();
		detail.SetUILanguage_ContentSizeFitter = base.transform.Find("bg/Languagepan/GameObject/Language (4)/SetUILanguage").gameObject.GetComponent<ContentSizeFitter>();
		detail.Image_Image = base.transform.Find("bg/Languagepan/GameObject/Language (4)/Image/Image").gameObject.GetComponent<Image>();
		detail.Image_Image = base.transform.Find("bg/Languagepan/GameObject/Language (4)/Image").gameObject.GetComponent<Image>();
		detail.Language4_Image = base.transform.Find("bg/Languagepan/GameObject/Language (4)").gameObject.GetComponent<Image>();
		detail.Language4_Button = base.transform.Find("bg/Languagepan/GameObject/Language (4)").gameObject.GetComponent<Button>();
		detail.Language4_languageLoad = base.transform.Find("bg/Languagepan/GameObject/Language (4)").gameObject.GetComponent<languageLoad>();
		detail.SetUILanguage_Text = base.transform.Find("bg/Languagepan/GameObject/Language (5)/SetUILanguage").gameObject.GetComponent<Text>();
		detail.SetUILanguage_Shadow = base.transform.Find("bg/Languagepan/GameObject/Language (5)/SetUILanguage").gameObject.GetComponent<Shadow>();
		detail.SetUILanguage_ContentSizeFitter = base.transform.Find("bg/Languagepan/GameObject/Language (5)/SetUILanguage").gameObject.GetComponent<ContentSizeFitter>();
		detail.Image_Image = base.transform.Find("bg/Languagepan/GameObject/Language (5)/Image/Image").gameObject.GetComponent<Image>();
		detail.Image_Image = base.transform.Find("bg/Languagepan/GameObject/Language (5)/Image").gameObject.GetComponent<Image>();
		detail.Language5_Image = base.transform.Find("bg/Languagepan/GameObject/Language (5)").gameObject.GetComponent<Image>();
		detail.Language5_Button = base.transform.Find("bg/Languagepan/GameObject/Language (5)").gameObject.GetComponent<Button>();
		detail.Language5_languageLoad = base.transform.Find("bg/Languagepan/GameObject/Language (5)").gameObject.GetComponent<languageLoad>();
		detail.SetUILanguage_Text = base.transform.Find("bg/Languagepan/GameObject/Language (6)/SetUILanguage").gameObject.GetComponent<Text>();
		detail.SetUILanguage_Shadow = base.transform.Find("bg/Languagepan/GameObject/Language (6)/SetUILanguage").gameObject.GetComponent<Shadow>();
		detail.SetUILanguage_ContentSizeFitter = base.transform.Find("bg/Languagepan/GameObject/Language (6)/SetUILanguage").gameObject.GetComponent<ContentSizeFitter>();
		detail.Image_Image = base.transform.Find("bg/Languagepan/GameObject/Language (6)/Image/Image").gameObject.GetComponent<Image>();
		detail.Image_Image = base.transform.Find("bg/Languagepan/GameObject/Language (6)/Image").gameObject.GetComponent<Image>();
		detail.Language6_Image = base.transform.Find("bg/Languagepan/GameObject/Language (6)").gameObject.GetComponent<Image>();
		detail.Language6_Button = base.transform.Find("bg/Languagepan/GameObject/Language (6)").gameObject.GetComponent<Button>();
		detail.Language6_languageLoad = base.transform.Find("bg/Languagepan/GameObject/Language (6)").gameObject.GetComponent<languageLoad>();
		detail.SetUILanguage_Text = base.transform.Find("bg/Languagepan/GameObject/Language (7)/SetUILanguage").gameObject.GetComponent<Text>();
		detail.SetUILanguage_Shadow = base.transform.Find("bg/Languagepan/GameObject/Language (7)/SetUILanguage").gameObject.GetComponent<Shadow>();
		detail.SetUILanguage_ContentSizeFitter = base.transform.Find("bg/Languagepan/GameObject/Language (7)/SetUILanguage").gameObject.GetComponent<ContentSizeFitter>();
		detail.Image_Image = base.transform.Find("bg/Languagepan/GameObject/Language (7)/Image/Image").gameObject.GetComponent<Image>();
		detail.Image_Image = base.transform.Find("bg/Languagepan/GameObject/Language (7)/Image").gameObject.GetComponent<Image>();
		detail.Language7_Image = base.transform.Find("bg/Languagepan/GameObject/Language (7)").gameObject.GetComponent<Image>();
		detail.Language7_Button = base.transform.Find("bg/Languagepan/GameObject/Language (7)").gameObject.GetComponent<Button>();
		detail.Language7_languageLoad = base.transform.Find("bg/Languagepan/GameObject/Language (7)").gameObject.GetComponent<languageLoad>();
		detail.SetUILanguage_Text = base.transform.Find("bg/Languagepan/GameObject/Language (8)/SetUILanguage").gameObject.GetComponent<Text>();
		detail.SetUILanguage_Shadow = base.transform.Find("bg/Languagepan/GameObject/Language (8)/SetUILanguage").gameObject.GetComponent<Shadow>();
		detail.SetUILanguage_ContentSizeFitter = base.transform.Find("bg/Languagepan/GameObject/Language (8)/SetUILanguage").gameObject.GetComponent<ContentSizeFitter>();
		detail.Image_Image = base.transform.Find("bg/Languagepan/GameObject/Language (8)/Image/Image").gameObject.GetComponent<Image>();
		detail.Image_Image = base.transform.Find("bg/Languagepan/GameObject/Language (8)/Image").gameObject.GetComponent<Image>();
		detail.Language8_Image = base.transform.Find("bg/Languagepan/GameObject/Language (8)").gameObject.GetComponent<Image>();
		detail.Language8_Button = base.transform.Find("bg/Languagepan/GameObject/Language (8)").gameObject.GetComponent<Button>();
		detail.Language8_languageLoad = base.transform.Find("bg/Languagepan/GameObject/Language (8)").gameObject.GetComponent<languageLoad>();
		detail.GameObject_GridLayoutGroup = base.transform.Find("bg/Languagepan/GameObject").gameObject.GetComponent<GridLayoutGroup>();
		detail.Languagepan_Image = base.transform.Find("bg/Languagepan").gameObject.GetComponent<Image>();
		detail.Languagepan_LanguagepanScript = base.transform.Find("bg/Languagepan").gameObject.GetComponent<LanguagepanScript>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		detail.CloseButton_Image = base.transform.Find("CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("CloseButton").gameObject.GetComponent<Button>();
		BtnAnimationBase btnAnimationBase = detail.Language2_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnLanguage2);
		BtnAnimationBase btnAnimationBase2 = detail.Language3_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnLanguage3);
		BtnAnimationBase btnAnimationBase3 = detail.Language4_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase3.SetType(NewBtnType.NONE);
		btnAnimationBase3.SetAction(OnLanguage4);
		BtnAnimationBase btnAnimationBase4 = detail.Language5_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase4.SetType(NewBtnType.NONE);
		btnAnimationBase4.SetAction(OnLanguage5);
		BtnAnimationBase btnAnimationBase5 = detail.Language6_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase5.SetType(NewBtnType.NONE);
		btnAnimationBase5.SetAction(OnLanguage6);
		BtnAnimationBase btnAnimationBase6 = detail.Language7_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase6.SetType(NewBtnType.NONE);
		btnAnimationBase6.SetAction(OnLanguage7);
		BtnAnimationBase btnAnimationBase7 = detail.Language8_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase7.SetType(NewBtnType.NONE);
		btnAnimationBase7.SetAction(OnLanguage8);
		BtnAnimationBase btnAnimationBase8 = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase8.SetType(NewBtnType.NONE);
		btnAnimationBase8.SetAction(OnCloseButton);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnLanguage2()
	{
	}

	public virtual void OnLanguage3()
	{
	}

	public virtual void OnLanguage4()
	{
	}

	public virtual void OnLanguage5()
	{
	}

	public virtual void OnLanguage6()
	{
	}

	public virtual void OnLanguage7()
	{
	}

	public virtual void OnLanguage8()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}
}
