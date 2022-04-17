using System;
using UnityEngine;
using UnityEngine.UI;

public class wanfaPanelBase : BasePanel
{
	public wanfaPanelDetail detail;

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
		detail.PlayingText2_Text = base.transform.Find("bg/playingpan/PlayingText2").gameObject.GetComponent<Text>();
		detail.PlayingText2_Gradient = base.transform.Find("bg/playingpan/PlayingText2").gameObject.GetComponent<Gradient>();
		detail.PlayingText2_Shadow = base.transform.Find("bg/playingpan/PlayingText2").gameObject.GetComponent<Shadow>();
		detail.PlayingText2_ContentSizeFitter = base.transform.Find("bg/playingpan/PlayingText2").gameObject.GetComponent<ContentSizeFitter>();
		detail.Image_Image = base.transform.Find("bg/playingpan/Image/Image").gameObject.GetComponent<Image>();
		detail.Image_Image = base.transform.Find("bg/playingpan/Image").gameObject.GetComponent<Image>();
		detail.playingtitleText_Text = base.transform.Find("bg/playingpan/playingtitleText").gameObject.GetComponent<Text>();
		detail.playingtitleText_Shadow = base.transform.Find("bg/playingpan/playingtitleText").gameObject.GetComponent<Shadow>();
		detail.playingtitleText_ContentSizeFitter = base.transform.Find("bg/playingpan/playingtitleText").gameObject.GetComponent<ContentSizeFitter>();
		detail.SetUILanguage_Text = base.transform.Find("bg/playingpan/Language (2)/SetUILanguage").gameObject.GetComponent<Text>();
		detail.SetUILanguage_Shadow = base.transform.Find("bg/playingpan/Language (2)/SetUILanguage").gameObject.GetComponent<Shadow>();
		detail.SetUILanguage_ContentSizeFitter = base.transform.Find("bg/playingpan/Language (2)/SetUILanguage").gameObject.GetComponent<ContentSizeFitter>();
		detail.Language2_Image = base.transform.Find("bg/playingpan/Language (2)").gameObject.GetComponent<Image>();
		detail.Language2_Button = base.transform.Find("bg/playingpan/Language (2)").gameObject.GetComponent<Button>();
		detail.SetUILanguage2_Text = base.transform.Find("bg/playingpan/SetUILanguage (2)").gameObject.GetComponent<Text>();
		detail.SetUILanguage2_Shadow = base.transform.Find("bg/playingpan/SetUILanguage (2)").gameObject.GetComponent<Shadow>();
		detail.SetUILanguage2_ContentSizeFitter = base.transform.Find("bg/playingpan/SetUILanguage (2)").gameObject.GetComponent<ContentSizeFitter>();
		detail.playingpan_Image = base.transform.Find("bg/playingpan").gameObject.GetComponent<Image>();
		detail.playingpan_playingScript = base.transform.Find("bg/playingpan").gameObject.GetComponent<playingScript>();
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
