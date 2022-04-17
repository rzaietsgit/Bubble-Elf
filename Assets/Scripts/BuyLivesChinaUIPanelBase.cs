using System;
using UnityEngine;
using UnityEngine.UI;

public class BuyLivesChinaUIPanelBase : BasePanel
{
	public BuyLivesChinaUIPanelDetail detail;

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
		detail.Text_Text = base.transform.Find("Top/Text").gameObject.GetComponent<Text>();
		detail.Text_Shadow = base.transform.Find("Top/Text").gameObject.GetComponent<Shadow>();
		detail.Text_ContentSizeFitter = base.transform.Find("Top/Text").gameObject.GetComponent<ContentSizeFitter>();
		detail.CloseButton_Image = base.transform.Find("Top/CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("Top/CloseButton").gameObject.GetComponent<Button>();
		detail.remarkoveText_Text = base.transform.Find("Top/remarkoveText").gameObject.GetComponent<Text>();
		detail.remarkoveText_Shadow = base.transform.Find("Top/remarkoveText").gameObject.GetComponent<Shadow>();
		detail.Remark_Text = base.transform.Find("Top/Image/Remark").gameObject.GetComponent<Text>();
		detail.Remark_Shadow = base.transform.Find("Top/Image/Remark").gameObject.GetComponent<Shadow>();
		detail.Remark_ContentSizeFitter = base.transform.Find("Top/Image/Remark").gameObject.GetComponent<ContentSizeFitter>();
		detail.Remark2_Text = base.transform.Find("Top/Image/Remark2").gameObject.GetComponent<Text>();
		detail.Remark2_Shadow = base.transform.Find("Top/Image/Remark2").gameObject.GetComponent<Shadow>();
		detail.Remark2_ContentSizeFitter = base.transform.Find("Top/Image/Remark2").gameObject.GetComponent<ContentSizeFitter>();
		detail.Time_Text = base.transform.Find("Top/Image/Time").gameObject.GetComponent<Text>();
		detail.Time_Shadow = base.transform.Find("Top/Image/Time").gameObject.GetComponent<Shadow>();
		detail.Time_ContentSizeFitter = base.transform.Find("Top/Image/Time").gameObject.GetComponent<ContentSizeFitter>();
		detail.LoveCount_Text = base.transform.Find("Top/Image/LoveCount").gameObject.GetComponent<Text>();
		detail.LoveCount_Shadow = base.transform.Find("Top/Image/LoveCount").gameObject.GetComponent<Shadow>();
		detail.LoveCount_ContentSizeFitter = base.transform.Find("Top/Image/LoveCount").gameObject.GetComponent<ContentSizeFitter>();
		detail.Image_Image = base.transform.Find("Top/Image").gameObject.GetComponent<Image>();
		detail.Background_RawImage = base.transform.Find("Top/Back/Background").gameObject.GetComponent<RawImage>();
		detail.Image_Image = base.transform.Find("Top/Back/Image2/Image").gameObject.GetComponent<Image>();
		detail.Text2_Text = base.transform.Find("Top/Back/Image2/Text2").gameObject.GetComponent<Text>();
		detail.Text2_Shadow = base.transform.Find("Top/Back/Image2/Text2").gameObject.GetComponent<Shadow>();
		detail.Text2_ContentSizeFitter = base.transform.Find("Top/Back/Image2/Text2").gameObject.GetComponent<ContentSizeFitter>();
		detail.Image2_RawImage = base.transform.Find("Top/Back/Image2").gameObject.GetComponent<RawImage>();
		detail.Image_Image = base.transform.Find("Top/Back/Image3/Image").gameObject.GetComponent<Image>();
		detail.Text3_Text = base.transform.Find("Top/Back/Image3/Text3").gameObject.GetComponent<Text>();
		detail.Text3_Shadow = base.transform.Find("Top/Back/Image3/Text3").gameObject.GetComponent<Shadow>();
		detail.Text3_ContentSizeFitter = base.transform.Find("Top/Back/Image3/Text3").gameObject.GetComponent<ContentSizeFitter>();
		detail.Image3_RawImage = base.transform.Find("Top/Back/Image3").gameObject.GetComponent<RawImage>();
		detail.Image_Image = base.transform.Find("Top/Back/Image4/Image").gameObject.GetComponent<Image>();
		detail.Text4_Text = base.transform.Find("Top/Back/Image4/Text4").gameObject.GetComponent<Text>();
		detail.Text4_Shadow = base.transform.Find("Top/Back/Image4/Text4").gameObject.GetComponent<Shadow>();
		detail.Text4_ContentSizeFitter = base.transform.Find("Top/Back/Image4/Text4").gameObject.GetComponent<ContentSizeFitter>();
		detail.Image4_RawImage = base.transform.Find("Top/Back/Image4").gameObject.GetComponent<RawImage>();
		detail.money_Text = base.transform.Find("Top/S_Button/money").gameObject.GetComponent<Text>();
		detail.money_Shadow = base.transform.Find("Top/S_Button/money").gameObject.GetComponent<Shadow>();
		detail.Image_Image = base.transform.Find("Top/S_Button/Image").gameObject.GetComponent<Image>();
		detail.S_Button_Image = base.transform.Find("Top/S_Button").gameObject.GetComponent<Image>();
		detail.S_Button_Button = base.transform.Find("Top/S_Button").gameObject.GetComponent<Button>();
		detail.money_Text = base.transform.Find("Top/S_ButtonGray/money").gameObject.GetComponent<Text>();
		detail.money_Shadow = base.transform.Find("Top/S_ButtonGray/money").gameObject.GetComponent<Shadow>();
		detail.Image_Image = base.transform.Find("Top/S_ButtonGray/Image").gameObject.GetComponent<Image>();
		detail.S_ButtonGray_Image = base.transform.Find("Top/S_ButtonGray").gameObject.GetComponent<Image>();
		detail.S_ButtonGray_Button = base.transform.Find("Top/S_ButtonGray").gameObject.GetComponent<Button>();
		detail.money099_Text = base.transform.Find("Top/S_Button_099/money099").gameObject.GetComponent<Text>();
		detail.money099_Shadow = base.transform.Find("Top/S_Button_099/money099").gameObject.GetComponent<Shadow>();
		detail.Image_Image = base.transform.Find("Top/S_Button_099/Image").gameObject.GetComponent<Image>();
		detail.S_Button_099_Image = base.transform.Find("Top/S_Button_099").gameObject.GetComponent<Image>();
		detail.S_Button_099_Button = base.transform.Find("Top/S_Button_099").gameObject.GetComponent<Button>();
		detail.money199_Text = base.transform.Find("Top/S_Button_199/money199").gameObject.GetComponent<Text>();
		detail.money199_Shadow = base.transform.Find("Top/S_Button_199/money199").gameObject.GetComponent<Shadow>();
		detail.Image_Image = base.transform.Find("Top/S_Button_199/Image").gameObject.GetComponent<Image>();
		detail.S_Button_199_Image = base.transform.Find("Top/S_Button_199").gameObject.GetComponent<Image>();
		detail.S_Button_199_Button = base.transform.Find("Top/S_Button_199").gameObject.GetComponent<Button>();
		detail.Top_Image = base.transform.Find("Top").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnCloseButton);
		BtnAnimationBase btnAnimationBase2 = detail.S_Button_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.STATIC);
		btnAnimationBase2.SetAction(OnS_Button);
		BtnAnimationBase btnAnimationBase3 = detail.S_ButtonGray_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase3.SetType(NewBtnType.STATIC);
		btnAnimationBase3.SetAction(OnS_ButtonGray);
		BtnAnimationBase btnAnimationBase4 = detail.S_Button_099_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase4.SetType(NewBtnType.STATIC);
		btnAnimationBase4.SetAction(OnS_Button_099);
		BtnAnimationBase btnAnimationBase5 = detail.S_Button_199_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase5.SetType(NewBtnType.STATIC);
		btnAnimationBase5.SetAction(OnS_Button_199);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}

	public virtual void OnS_Button()
	{
	}

	public virtual void OnS_ButtonGray()
	{
	}

	public virtual void OnS_Button_099()
	{
	}

	public virtual void OnS_Button_199()
	{
	}
}
