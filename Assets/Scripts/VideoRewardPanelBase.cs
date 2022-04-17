using System;
using UnityEngine;
using UnityEngine.UI;

public class VideoRewardPanelBase : BasePanel
{
	public VideoRewardPanelDetail detail;

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
		detail.Title2_Text = base.transform.Find("bg/Title2").gameObject.GetComponent<Text>();
		detail.Title2_Shadow = base.transform.Find("bg/Title2").gameObject.GetComponent<Shadow>();
		detail.Title2_ContentSizeFitter = base.transform.Find("bg/Title2").gameObject.GetComponent<ContentSizeFitter>();
		detail.times_Text = base.transform.Find("bg/times").gameObject.GetComponent<Text>();
		detail.times_Shadow = base.transform.Find("bg/times").gameObject.GetComponent<Shadow>();
		detail.times_ContentSizeFitter = base.transform.Find("bg/times").gameObject.GetComponent<ContentSizeFitter>();
		detail.ConfirmText_Text = base.transform.Find("bg/Confirm/ConfirmText").gameObject.GetComponent<Text>();
		detail.ConfirmText_Shadow = base.transform.Find("bg/Confirm/ConfirmText").gameObject.GetComponent<Shadow>();
		detail.ConfirmText_ContentSizeFitter = base.transform.Find("bg/Confirm/ConfirmText").gameObject.GetComponent<ContentSizeFitter>();
		detail.Image_Image = base.transform.Find("bg/Confirm/Image").gameObject.GetComponent<Image>();
		detail.Confirm_Image = base.transform.Find("bg/Confirm").gameObject.GetComponent<Image>();
		detail.Confirm_Button = base.transform.Find("bg/Confirm").gameObject.GetComponent<Button>();
		detail.id_Text = base.transform.Find("bg/Image123/GameObject1232/objbg/id").gameObject.GetComponent<Text>();
		detail.id_Shadow = base.transform.Find("bg/Image123/GameObject1232/objbg/id").gameObject.GetComponent<Shadow>();
		detail.id_ContentSizeFitter = base.transform.Find("bg/Image123/GameObject1232/objbg/id").gameObject.GetComponent<ContentSizeFitter>();
		detail.icon_Image = base.transform.Find("bg/Image123/GameObject1232/objbg/icon").gameObject.GetComponent<Image>();
		detail.count_Text = base.transform.Find("bg/Image123/GameObject1232/objbg/count").gameObject.GetComponent<Text>();
		detail.count_Shadow = base.transform.Find("bg/Image123/GameObject1232/objbg/count").gameObject.GetComponent<Shadow>();
		detail.count_ContentSizeFitter = base.transform.Find("bg/Image123/GameObject1232/objbg/count").gameObject.GetComponent<ContentSizeFitter>();
		detail.objbg_Image = base.transform.Find("bg/Image123/GameObject1232/objbg").gameObject.GetComponent<Image>();
		detail.objbg_VideoSon = base.transform.Find("bg/Image123/GameObject1232/objbg").gameObject.GetComponent<VideoSon>();
		detail.GameObject1232_GridLayoutGroup = base.transform.Find("bg/Image123/GameObject1232").gameObject.GetComponent<GridLayoutGroup>();
		detail.Image123_Image = base.transform.Find("bg/Image123").gameObject.GetComponent<Image>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		detail.CloseButton_Image = base.transform.Find("CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("CloseButton").gameObject.GetComponent<Button>();
		BtnAnimationBase btnAnimationBase = detail.Confirm_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnConfirm);
		BtnAnimationBase btnAnimationBase2 = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnCloseButton);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnConfirm()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}
}
