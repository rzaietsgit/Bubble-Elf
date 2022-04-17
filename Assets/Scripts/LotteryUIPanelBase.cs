using Spine.Unity;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LotteryUIPanelBase : BasePanel
{
	public LotteryUIPanelDetail detail;

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
		detail.CloseButton_Image = base.transform.Find("bg/CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("bg/CloseButton").gameObject.GetComponent<Button>();
		detail.Image1_Image = base.transform.Find("bg/TitleBg/Image/Image1").gameObject.GetComponent<Image>();
		detail.Image_Image = base.transform.Find("bg/TitleBg/Image").gameObject.GetComponent<Image>();
		detail.TextDemo_Text = base.transform.Find("bg/TitleBg/TextDemo").gameObject.GetComponent<Text>();
		detail.TextDemo_Shadow = base.transform.Find("bg/TitleBg/TextDemo").gameObject.GetComponent<Shadow>();
		detail.TextDemo_ContentSizeFitter = base.transform.Find("bg/TitleBg/TextDemo").gameObject.GetComponent<ContentSizeFitter>();
		detail.TextDemo2_Text = base.transform.Find("bg/TitleBg/TextDemo (2)").gameObject.GetComponent<Text>();
		detail.TextDemo2_Shadow = base.transform.Find("bg/TitleBg/TextDemo (2)").gameObject.GetComponent<Shadow>();
		detail.TextDemo3_Text = base.transform.Find("bg/TitleBg/Time/TextDemo (3)").gameObject.GetComponent<Text>();
		detail.TextDemo3_Shadow = base.transform.Find("bg/TitleBg/Time/TextDemo (3)").gameObject.GetComponent<Shadow>();
		detail.Time_Text = base.transform.Find("bg/TitleBg/Time").gameObject.GetComponent<Text>();
		detail.Time_Shadow = base.transform.Find("bg/TitleBg/Time").gameObject.GetComponent<Shadow>();
		detail.TextDemo4_Text = base.transform.Find("bg/TitleBg/TextDemo (4)").gameObject.GetComponent<Text>();
		detail.TextDemo4_Shadow = base.transform.Find("bg/TitleBg/TextDemo (4)").gameObject.GetComponent<Shadow>();
		detail.TextDemo4_ContentSizeFitter = base.transform.Find("bg/TitleBg/TextDemo (4)").gameObject.GetComponent<ContentSizeFitter>();
		detail.TitleBg_Image = base.transform.Find("bg/TitleBg").gameObject.GetComponent<Image>();
		detail.Text1_Text = base.transform.Find("bg/Image2/Text/Text1").gameObject.GetComponent<Text>();
		detail.Text1_Shadow = base.transform.Find("bg/Image2/Text/Text1").gameObject.GetComponent<Shadow>();
		detail.Text2_Text = base.transform.Find("bg/Image2/Text/Text2").gameObject.GetComponent<Text>();
		detail.Text2_Shadow = base.transform.Find("bg/Image2/Text/Text2").gameObject.GetComponent<Shadow>();
		detail.Text3_Text = base.transform.Find("bg/Image2/Text/Text3").gameObject.GetComponent<Text>();
		detail.Text3_Shadow = base.transform.Find("bg/Image2/Text/Text3").gameObject.GetComponent<Shadow>();
		detail.Text4_Text = base.transform.Find("bg/Image2/Text/Text4").gameObject.GetComponent<Text>();
		detail.Text4_Shadow = base.transform.Find("bg/Image2/Text/Text4").gameObject.GetComponent<Shadow>();
		detail.Text5_Text = base.transform.Find("bg/Image2/Text/Text5").gameObject.GetComponent<Text>();
		detail.Text5_Shadow = base.transform.Find("bg/Image2/Text/Text5").gameObject.GetComponent<Shadow>();
		detail.Text6_Text = base.transform.Find("bg/Image2/Text/Text6").gameObject.GetComponent<Text>();
		detail.Text6_Shadow = base.transform.Find("bg/Image2/Text/Text6").gameObject.GetComponent<Shadow>();
		detail.Text7_Text = base.transform.Find("bg/Image2/Text/Text7").gameObject.GetComponent<Text>();
		detail.Text7_Shadow = base.transform.Find("bg/Image2/Text/Text7").gameObject.GetComponent<Shadow>();
		detail.Text8_Text = base.transform.Find("bg/Image2/Text/Text8").gameObject.GetComponent<Text>();
		detail.Text8_Shadow = base.transform.Find("bg/Image2/Text/Text8").gameObject.GetComponent<Shadow>();
		detail.Text_Image = base.transform.Find("bg/Image2/Text").gameObject.GetComponent<Image>();
		detail.RIcon1_Image = base.transform.Find("bg/Image2/RIcon/RIcon1").gameObject.GetComponent<Image>();
		detail.RIcon2_Image = base.transform.Find("bg/Image2/RIcon/RIcon2").gameObject.GetComponent<Image>();
		detail.RIcon3_Image = base.transform.Find("bg/Image2/RIcon/RIcon3").gameObject.GetComponent<Image>();
		detail.RIcon4_Image = base.transform.Find("bg/Image2/RIcon/RIcon4").gameObject.GetComponent<Image>();
		detail.RIcon5_Image = base.transform.Find("bg/Image2/RIcon/RIcon5").gameObject.GetComponent<Image>();
		detail.RIcon6_Image = base.transform.Find("bg/Image2/RIcon/RIcon6").gameObject.GetComponent<Image>();
		detail.RIcon7_Image = base.transform.Find("bg/Image2/RIcon/RIcon7").gameObject.GetComponent<Image>();
		detail.RIcon8_Image = base.transform.Find("bg/Image2/RIcon/RIcon8").gameObject.GetComponent<Image>();
		detail.RIcon_Image = base.transform.Find("bg/Image2/RIcon").gameObject.GetComponent<Image>();
		detail.R1_Image = base.transform.Find("bg/Image2/R/R1").gameObject.GetComponent<Image>();
		detail.R2_Image = base.transform.Find("bg/Image2/R/R2").gameObject.GetComponent<Image>();
		detail.R3_Image = base.transform.Find("bg/Image2/R/R3").gameObject.GetComponent<Image>();
		detail.R4_Image = base.transform.Find("bg/Image2/R/R4").gameObject.GetComponent<Image>();
		detail.R5_Image = base.transform.Find("bg/Image2/R/R5").gameObject.GetComponent<Image>();
		detail.R6_Image = base.transform.Find("bg/Image2/R/R6").gameObject.GetComponent<Image>();
		detail.R7_Image = base.transform.Find("bg/Image2/R/R7").gameObject.GetComponent<Image>();
		detail.R8_Image = base.transform.Find("bg/Image2/R/R8").gameObject.GetComponent<Image>();
		detail.R_Image = base.transform.Find("bg/Image2/R").gameObject.GetComponent<Image>();
		detail.b1_Image = base.transform.Find("bg/Image2/bg1/b1").gameObject.GetComponent<Image>();
		detail.b2_Image = base.transform.Find("bg/Image2/bg1/b2").gameObject.GetComponent<Image>();
		detail.b3_Image = base.transform.Find("bg/Image2/bg1/b3").gameObject.GetComponent<Image>();
		detail.b4_Image = base.transform.Find("bg/Image2/bg1/b4").gameObject.GetComponent<Image>();
		detail.b5_Image = base.transform.Find("bg/Image2/bg1/b5").gameObject.GetComponent<Image>();
		detail.b6_Image = base.transform.Find("bg/Image2/bg1/b6").gameObject.GetComponent<Image>();
		detail.b7_Image = base.transform.Find("bg/Image2/bg1/b7").gameObject.GetComponent<Image>();
		detail.b8_Image = base.transform.Find("bg/Image2/bg1/b8").gameObject.GetComponent<Image>();
		detail.bg1_Image = base.transform.Find("bg/Image2/bg1").gameObject.GetComponent<Image>();
		detail.Image2_Image = base.transform.Find("bg/Image2").gameObject.GetComponent<Image>();
		detail.Image3_Image = base.transform.Find("bg/Image (3)").gameObject.GetComponent<Image>();
		detail.Image4_Image = base.transform.Find("bg/Image4").gameObject.GetComponent<Image>();
		detail.Image5_Image = base.transform.Find("bg/Image (5)").gameObject.GetComponent<Image>();
		detail.finger_SkeletonAnimation = base.transform.Find("bg/finger").gameObject.GetComponent<SkeletonAnimation>();
		detail.Free_Text = base.transform.Find("bg/S_Button/Count/Free").gameObject.GetComponent<Text>();
		detail.Free_Shadow = base.transform.Find("bg/S_Button/Count/Free").gameObject.GetComponent<Shadow>();
		detail.Count_Text = base.transform.Find("bg/S_Button/Count").gameObject.GetComponent<Text>();
		detail.Count_Shadow = base.transform.Find("bg/S_Button/Count").gameObject.GetComponent<Shadow>();
		detail.TextDemo5_Text = base.transform.Find("bg/S_Button/ClickRemark/TextDemo (5)").gameObject.GetComponent<Text>();
		detail.TextDemo5_Shadow = base.transform.Find("bg/S_Button/ClickRemark/TextDemo (5)").gameObject.GetComponent<Shadow>();
		detail.ClickRemark_Text = base.transform.Find("bg/S_Button/ClickRemark").gameObject.GetComponent<Text>();
		detail.ClickRemark_Shadow = base.transform.Find("bg/S_Button/ClickRemark").gameObject.GetComponent<Shadow>();
		detail.S_Button_Image = base.transform.Find("bg/S_Button").gameObject.GetComponent<Image>();
		detail.S_Button_Button = base.transform.Find("bg/S_Button").gameObject.GetComponent<Button>();
		detail.lvyeCount_Text = base.transform.Find("bg/Image (6)/lvyeCount").gameObject.GetComponent<Text>();
		detail.lvyeCount_Shadow = base.transform.Find("bg/Image (6)/lvyeCount").gameObject.GetComponent<Shadow>();
		detail.Image6_Image = base.transform.Find("bg/Image (6)").gameObject.GetComponent<Image>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		detail.BuyText_Text = base.transform.Find("ResBtn/BuyText").gameObject.GetComponent<Text>();
		detail.BuyText_Shadow = base.transform.Find("ResBtn/BuyText").gameObject.GetComponent<Shadow>();
		detail.Image7_Image = base.transform.Find("ResBtn/Image7").gameObject.GetComponent<Image>();
		detail.BuyText1_Text = base.transform.Find("ResBtn/BuyText (1)").gameObject.GetComponent<Text>();
		detail.BuyText1_Shadow = base.transform.Find("ResBtn/BuyText (1)").gameObject.GetComponent<Shadow>();
		detail.BuyText2_Text = base.transform.Find("ResBtn/BuyText (2)").gameObject.GetComponent<Text>();
		detail.BuyText2_Shadow = base.transform.Find("ResBtn/BuyText (2)").gameObject.GetComponent<Shadow>();
		detail.ResBtn_Image = base.transform.Find("ResBtn").gameObject.GetComponent<Image>();
		detail.ResBtn_Button = base.transform.Find("ResBtn").gameObject.GetComponent<Button>();
		BtnAnimationBase btnAnimationBase = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnCloseButton);
		BtnAnimationBase btnAnimationBase2 = detail.S_Button_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.STATIC);
		btnAnimationBase2.SetAction(OnS_Button);
		BtnAnimationBase btnAnimationBase3 = detail.ResBtn_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase3.SetType(NewBtnType.NONE);
		btnAnimationBase3.SetAction(OnResBtn);
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

	public virtual void OnResBtn()
	{
	}
}
