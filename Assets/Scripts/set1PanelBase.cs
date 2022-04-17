using System;
using UnityEngine;
using UnityEngine.UI;

public class set1PanelBase : BasePanel
{
	public set1PanelDetail detail;

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
		detail.SetSetPanelTitle_Text = base.transform.Find("bg/SetSetPanelTitle").gameObject.GetComponent<Text>();
		detail.SetSetPanelTitle_Gradient = base.transform.Find("bg/SetSetPanelTitle").gameObject.GetComponent<Gradient>();
		detail.SetSetPanelTitle_Shadow = base.transform.Find("bg/SetSetPanelTitle").gameObject.GetComponent<Shadow>();
		detail.SetSetPanelTitle_ContentSizeFitter = base.transform.Find("bg/SetSetPanelTitle").gameObject.GetComponent<ContentSizeFitter>();
		detail.S_MusicBtn_Image = base.transform.Find("bg/Image/S_MusicBtn").gameObject.GetComponent<Image>();
		detail.S_MusicBtn_Button = base.transform.Find("bg/Image/S_MusicBtn").gameObject.GetComponent<Button>();
		detail.S_SoundBtn_Image = base.transform.Find("bg/Image/S_SoundBtn").gameObject.GetComponent<Image>();
		detail.S_SoundBtn_Button = base.transform.Find("bg/Image/S_SoundBtn").gameObject.GetComponent<Button>();
		detail.PushText_Text = base.transform.Find("bg/Image/PushText").gameObject.GetComponent<Text>();
		detail.PushImage_Image = base.transform.Find("bg/Image/S_PushBtn/PushImage").gameObject.GetComponent<Image>();
		detail.S_PushBtn_Image = base.transform.Find("bg/Image/S_PushBtn").gameObject.GetComponent<Image>();
		detail.S_PushBtn_Button = base.transform.Find("bg/Image/S_PushBtn").gameObject.GetComponent<Button>();
		detail.SaveText_Text = base.transform.Find("bg/Image/S_SaveBtn/SaveText").gameObject.GetComponent<Text>();
		detail.SaveText_Shadow = base.transform.Find("bg/Image/S_SaveBtn/SaveText").gameObject.GetComponent<Shadow>();
		detail.SaveText_ContentSizeFitter = base.transform.Find("bg/Image/S_SaveBtn/SaveText").gameObject.GetComponent<ContentSizeFitter>();
		detail.S_SaveBtn_Image = base.transform.Find("bg/Image/S_SaveBtn").gameObject.GetComponent<Image>();
		detail.S_SaveBtn_Button = base.transform.Find("bg/Image/S_SaveBtn").gameObject.GetComponent<Button>();
		detail.LoadText_Text = base.transform.Find("bg/Image/S_LoadBtn/LoadText").gameObject.GetComponent<Text>();
		detail.LoadText_Shadow = base.transform.Find("bg/Image/S_LoadBtn/LoadText").gameObject.GetComponent<Shadow>();
		detail.LoadText_ContentSizeFitter = base.transform.Find("bg/Image/S_LoadBtn/LoadText").gameObject.GetComponent<ContentSizeFitter>();
		detail.S_LoadBtn_Image = base.transform.Find("bg/Image/S_LoadBtn").gameObject.GetComponent<Image>();
		detail.S_LoadBtn_Button = base.transform.Find("bg/Image/S_LoadBtn").gameObject.GetComponent<Button>();
		detail.Image_Image = base.transform.Find("bg/Image").gameObject.GetComponent<Image>();
		detail.PlayingText_Text = base.transform.Find("bg/S_wanfa/PlayingText").gameObject.GetComponent<Text>();
		detail.PlayingText_Shadow = base.transform.Find("bg/S_wanfa/PlayingText").gameObject.GetComponent<Shadow>();
		detail.PlayingText_ContentSizeFitter = base.transform.Find("bg/S_wanfa/PlayingText").gameObject.GetComponent<ContentSizeFitter>();
		detail.S_wanfa_Image = base.transform.Find("bg/S_wanfa").gameObject.GetComponent<Image>();
		detail.S_wanfa_Button = base.transform.Find("bg/S_wanfa").gameObject.GetComponent<Button>();
		detail.Consult_Text = base.transform.Find("bg/S_lianxi/Consult").gameObject.GetComponent<Text>();
		detail.Consult_Shadow = base.transform.Find("bg/S_lianxi/Consult").gameObject.GetComponent<Shadow>();
		detail.Consult_ContentSizeFitter = base.transform.Find("bg/S_lianxi/Consult").gameObject.GetComponent<ContentSizeFitter>();
		detail.S_lianxi_Image = base.transform.Find("bg/S_lianxi").gameObject.GetComponent<Image>();
		detail.S_lianxi_Button = base.transform.Find("bg/S_lianxi").gameObject.GetComponent<Button>();
		detail.Language_Text = base.transform.Find("bg/S_yuyan/Language").gameObject.GetComponent<Text>();
		detail.Language_Shadow = base.transform.Find("bg/S_yuyan/Language").gameObject.GetComponent<Shadow>();
		detail.Language_ContentSizeFitter = base.transform.Find("bg/S_yuyan/Language").gameObject.GetComponent<ContentSizeFitter>();
		detail.S_yuyan_Image = base.transform.Find("bg/S_yuyan").gameObject.GetComponent<Image>();
		detail.S_yuyan_Button = base.transform.Find("bg/S_yuyan").gameObject.GetComponent<Button>();
		detail.FaceBookText_Text = base.transform.Find("bg/S_FaceBookBtn/FaceBookText").gameObject.GetComponent<Text>();
		detail.FaceBookText_Shadow = base.transform.Find("bg/S_FaceBookBtn/FaceBookText").gameObject.GetComponent<Shadow>();
		detail.FaceBookText_ContentSizeFitter = base.transform.Find("bg/S_FaceBookBtn/FaceBookText").gameObject.GetComponent<ContentSizeFitter>();
		detail.Image_Image = base.transform.Find("bg/S_FaceBookBtn/Image").gameObject.GetComponent<Image>();
		detail.S_FaceBookBtn_Image = base.transform.Find("bg/S_FaceBookBtn").gameObject.GetComponent<Image>();
		detail.S_FaceBookBtn_Button = base.transform.Find("bg/S_FaceBookBtn").gameObject.GetComponent<Button>();
		detail.zhengce_Text = base.transform.Find("bg/S_zhengce1/zhengce").gameObject.GetComponent<Text>();
		detail.zhengce_Shadow = base.transform.Find("bg/S_zhengce1/zhengce").gameObject.GetComponent<Shadow>();
		detail.zhengce_ContentSizeFitter = base.transform.Find("bg/S_zhengce1/zhengce").gameObject.GetComponent<ContentSizeFitter>();
		detail.S_zhengce1_Image = base.transform.Find("bg/S_zhengce1").gameObject.GetComponent<Image>();
		detail.S_zhengce1_Button = base.transform.Find("bg/S_zhengce1").gameObject.GetComponent<Button>();
		detail.fuwu_Text = base.transform.Find("bg/S_fuwu1/fuwu").gameObject.GetComponent<Text>();
		detail.fuwu_Shadow = base.transform.Find("bg/S_fuwu1/fuwu").gameObject.GetComponent<Shadow>();
		detail.fuwu_ContentSizeFitter = base.transform.Find("bg/S_fuwu1/fuwu").gameObject.GetComponent<ContentSizeFitter>();
		detail.S_fuwu1_Image = base.transform.Find("bg/S_fuwu1").gameObject.GetComponent<Image>();
		detail.S_fuwu1_Button = base.transform.Find("bg/S_fuwu1").gameObject.GetComponent<Button>();
		detail.UserIDText_Text = base.transform.Find("bg/UserIDText").gameObject.GetComponent<Text>();
		detail.UserIDText_Gradient = base.transform.Find("bg/UserIDText").gameObject.GetComponent<Gradient>();
		detail.UserIDText_Shadow = base.transform.Find("bg/UserIDText").gameObject.GetComponent<Shadow>();
		detail.UserIDText_ContentSizeFitter = base.transform.Find("bg/UserIDText").gameObject.GetComponent<ContentSizeFitter>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		detail.CloseButton_Image = base.transform.Find("CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("CloseButton").gameObject.GetComponent<Button>();
		BtnAnimationBase btnAnimationBase = detail.S_MusicBtn_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.STATIC);
		btnAnimationBase.SetAction(OnS_MusicBtn);
		BtnAnimationBase btnAnimationBase2 = detail.S_SoundBtn_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.STATIC);
		btnAnimationBase2.SetAction(OnS_SoundBtn);
		BtnAnimationBase btnAnimationBase3 = detail.S_PushBtn_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase3.SetType(NewBtnType.STATIC);
		btnAnimationBase3.SetAction(OnS_PushBtn);
		BtnAnimationBase btnAnimationBase4 = detail.S_SaveBtn_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase4.SetType(NewBtnType.STATIC);
		btnAnimationBase4.SetAction(OnS_SaveBtn);
		BtnAnimationBase btnAnimationBase5 = detail.S_LoadBtn_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase5.SetType(NewBtnType.STATIC);
		btnAnimationBase5.SetAction(OnS_LoadBtn);
		BtnAnimationBase btnAnimationBase6 = detail.S_wanfa_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase6.SetType(NewBtnType.STATIC);
		btnAnimationBase6.SetAction(OnS_wanfa);
		BtnAnimationBase btnAnimationBase7 = detail.S_lianxi_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase7.SetType(NewBtnType.STATIC);
		btnAnimationBase7.SetAction(OnS_lianxi);
		BtnAnimationBase btnAnimationBase8 = detail.S_yuyan_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase8.SetType(NewBtnType.STATIC);
		btnAnimationBase8.SetAction(OnS_yuyan);
		BtnAnimationBase btnAnimationBase9 = detail.S_FaceBookBtn_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase9.SetType(NewBtnType.STATIC);
		btnAnimationBase9.SetAction(OnS_FaceBookBtn);
		BtnAnimationBase btnAnimationBase10 = detail.S_zhengce1_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase10.SetType(NewBtnType.STATIC);
		btnAnimationBase10.SetAction(OnS_zhengce1);
		BtnAnimationBase btnAnimationBase11 = detail.S_fuwu1_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase11.SetType(NewBtnType.STATIC);
		btnAnimationBase11.SetAction(OnS_fuwu1);
		BtnAnimationBase btnAnimationBase12 = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase12.SetType(NewBtnType.NONE);
		btnAnimationBase12.SetAction(OnCloseButton);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnS_MusicBtn()
	{
	}

	public virtual void OnS_SoundBtn()
	{
	}

	public virtual void OnS_PushBtn()
	{
	}

	public virtual void OnS_SaveBtn()
	{
	}

	public virtual void OnS_LoadBtn()
	{
	}

	public virtual void OnS_wanfa()
	{
	}

	public virtual void OnS_lianxi()
	{
	}

	public virtual void OnS_yuyan()
	{
	}

	public virtual void OnS_FaceBookBtn()
	{
	}

	public virtual void OnS_zhengce1()
	{
	}

	public virtual void OnS_fuwu1()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}
}
