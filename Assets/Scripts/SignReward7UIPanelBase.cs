using Spine.Unity;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SignReward7UIPanelBase : BasePanel
{
	public SignReward7UIPanelDetail detail;

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
		detail.Image_Image = base.transform.Find("bg/ui_7day_tree/Root/box1/Image1/Image").gameObject.GetComponent<Image>();
		detail.finger1_SkeletonAnimation = base.transform.Find("bg/ui_7day_tree/Root/box1/Image1/finger1").gameObject.GetComponent<SkeletonAnimation>();
		detail.Image1_Image = base.transform.Find("bg/ui_7day_tree/Root/box1/Image1").gameObject.GetComponent<Image>();
		detail.Image11_Image = base.transform.Find("bg/ui_7day_tree/Root/box1/Image1 (1)").gameObject.GetComponent<Image>();
		detail.Image11_Button = base.transform.Find("bg/ui_7day_tree/Root/box1/Image1 (1)").gameObject.GetComponent<Button>();
		detail.box1_SkeletonUtilityBone = base.transform.Find("bg/ui_7day_tree/Root/box1").gameObject.GetComponent<SkeletonUtilityBone>();
		detail.box1_BoneFollower = base.transform.Find("bg/ui_7day_tree/Root/box1").gameObject.GetComponent<BoneFollower>();
		detail.Image_Image = base.transform.Find("bg/ui_7day_tree/Root/box2/Image2/Image").gameObject.GetComponent<Image>();
		detail.Image2_Image = base.transform.Find("bg/ui_7day_tree/Root/box2/Image2").gameObject.GetComponent<Image>();
		detail.Image_Image = base.transform.Find("bg/ui_7day_tree/Root/box2/Image2 (1)/Image").gameObject.GetComponent<Image>();
		detail.Image21_Image = base.transform.Find("bg/ui_7day_tree/Root/box2/Image2 (1)").gameObject.GetComponent<Image>();
		detail.Image21_Button = base.transform.Find("bg/ui_7day_tree/Root/box2/Image2 (1)").gameObject.GetComponent<Button>();
		detail.S_Image22_Image = base.transform.Find("bg/ui_7day_tree/Root/box2/S_Image22").gameObject.GetComponent<Image>();
		detail.S_Image22_Button = base.transform.Find("bg/ui_7day_tree/Root/box2/S_Image22").gameObject.GetComponent<Button>();
		detail.box2_SkeletonUtilityBone = base.transform.Find("bg/ui_7day_tree/Root/box2").gameObject.GetComponent<SkeletonUtilityBone>();
		detail.box2_BoneFollower = base.transform.Find("bg/ui_7day_tree/Root/box2").gameObject.GetComponent<BoneFollower>();
		detail.Image_Image = base.transform.Find("bg/ui_7day_tree/Root/box3/Image3/Image").gameObject.GetComponent<Image>();
		detail.Image3_Image = base.transform.Find("bg/ui_7day_tree/Root/box3/Image3").gameObject.GetComponent<Image>();
		detail.Image_Image = base.transform.Find("bg/ui_7day_tree/Root/box3/Image3 (1)/Image").gameObject.GetComponent<Image>();
		detail.Image31_Image = base.transform.Find("bg/ui_7day_tree/Root/box3/Image3 (1)").gameObject.GetComponent<Image>();
		detail.Image31_Button = base.transform.Find("bg/ui_7day_tree/Root/box3/Image3 (1)").gameObject.GetComponent<Button>();
		detail.S_Image33_Image = base.transform.Find("bg/ui_7day_tree/Root/box3/S_Image33").gameObject.GetComponent<Image>();
		detail.S_Image33_Button = base.transform.Find("bg/ui_7day_tree/Root/box3/S_Image33").gameObject.GetComponent<Button>();
		detail.box3_SkeletonUtilityBone = base.transform.Find("bg/ui_7day_tree/Root/box3").gameObject.GetComponent<SkeletonUtilityBone>();
		detail.box3_BoneFollower = base.transform.Find("bg/ui_7day_tree/Root/box3").gameObject.GetComponent<BoneFollower>();
		detail.Image_Image = base.transform.Find("bg/ui_7day_tree/Root/box4/Image4/Image").gameObject.GetComponent<Image>();
		detail.Image4_Image = base.transform.Find("bg/ui_7day_tree/Root/box4/Image4").gameObject.GetComponent<Image>();
		detail.Image_Image = base.transform.Find("bg/ui_7day_tree/Root/box4/Image4 (1)/Image").gameObject.GetComponent<Image>();
		detail.Image41_Image = base.transform.Find("bg/ui_7day_tree/Root/box4/Image4 (1)").gameObject.GetComponent<Image>();
		detail.Image41_Button = base.transform.Find("bg/ui_7day_tree/Root/box4/Image4 (1)").gameObject.GetComponent<Button>();
		detail.S_Image44_Image = base.transform.Find("bg/ui_7day_tree/Root/box4/S_Image44").gameObject.GetComponent<Image>();
		detail.S_Image44_Button = base.transform.Find("bg/ui_7day_tree/Root/box4/S_Image44").gameObject.GetComponent<Button>();
		detail.box4_SkeletonUtilityBone = base.transform.Find("bg/ui_7day_tree/Root/box4").gameObject.GetComponent<SkeletonUtilityBone>();
		detail.box4_BoneFollower = base.transform.Find("bg/ui_7day_tree/Root/box4").gameObject.GetComponent<BoneFollower>();
		detail.Image_Image = base.transform.Find("bg/ui_7day_tree/Root/box5/Image5/Image").gameObject.GetComponent<Image>();
		detail.Image5_Image = base.transform.Find("bg/ui_7day_tree/Root/box5/Image5").gameObject.GetComponent<Image>();
		detail.Image_Image = base.transform.Find("bg/ui_7day_tree/Root/box5/Image5 (1)/Image").gameObject.GetComponent<Image>();
		detail.Image51_Image = base.transform.Find("bg/ui_7day_tree/Root/box5/Image5 (1)").gameObject.GetComponent<Image>();
		detail.Image51_Button = base.transform.Find("bg/ui_7day_tree/Root/box5/Image5 (1)").gameObject.GetComponent<Button>();
		detail.S_Image55_Image = base.transform.Find("bg/ui_7day_tree/Root/box5/S_Image55").gameObject.GetComponent<Image>();
		detail.S_Image55_Button = base.transform.Find("bg/ui_7day_tree/Root/box5/S_Image55").gameObject.GetComponent<Button>();
		detail.box5_SkeletonUtilityBone = base.transform.Find("bg/ui_7day_tree/Root/box5").gameObject.GetComponent<SkeletonUtilityBone>();
		detail.box5_BoneFollower = base.transform.Find("bg/ui_7day_tree/Root/box5").gameObject.GetComponent<BoneFollower>();
		detail.Image_Image = base.transform.Find("bg/ui_7day_tree/Root/box6/Image6/Image").gameObject.GetComponent<Image>();
		detail.Image6_Image = base.transform.Find("bg/ui_7day_tree/Root/box6/Image6").gameObject.GetComponent<Image>();
		detail.Image_Image = base.transform.Find("bg/ui_7day_tree/Root/box6/Image6 (1)/Image").gameObject.GetComponent<Image>();
		detail.Image61_Image = base.transform.Find("bg/ui_7day_tree/Root/box6/Image6 (1)").gameObject.GetComponent<Image>();
		detail.Image61_Button = base.transform.Find("bg/ui_7day_tree/Root/box6/Image6 (1)").gameObject.GetComponent<Button>();
		detail.S_Image66_Image = base.transform.Find("bg/ui_7day_tree/Root/box6/S_Image66").gameObject.GetComponent<Image>();
		detail.S_Image66_Button = base.transform.Find("bg/ui_7day_tree/Root/box6/S_Image66").gameObject.GetComponent<Button>();
		detail.box6_SkeletonUtilityBone = base.transform.Find("bg/ui_7day_tree/Root/box6").gameObject.GetComponent<SkeletonUtilityBone>();
		detail.box6_BoneFollower = base.transform.Find("bg/ui_7day_tree/Root/box6").gameObject.GetComponent<BoneFollower>();
		detail.Image_Image = base.transform.Find("bg/ui_7day_tree/Root/box7/Image7/Image").gameObject.GetComponent<Image>();
		detail.Image7_Image = base.transform.Find("bg/ui_7day_tree/Root/box7/Image7").gameObject.GetComponent<Image>();
		detail.Image_Image = base.transform.Find("bg/ui_7day_tree/Root/box7/Image7 (1)/Image").gameObject.GetComponent<Image>();
		detail.Image71_Image = base.transform.Find("bg/ui_7day_tree/Root/box7/Image7 (1)").gameObject.GetComponent<Image>();
		detail.Image71_Button = base.transform.Find("bg/ui_7day_tree/Root/box7/Image7 (1)").gameObject.GetComponent<Button>();
		detail.S_Image77_Image = base.transform.Find("bg/ui_7day_tree/Root/box7/S_Image77").gameObject.GetComponent<Image>();
		detail.S_Image77_Button = base.transform.Find("bg/ui_7day_tree/Root/box7/S_Image77").gameObject.GetComponent<Button>();
		detail.box7_SkeletonUtilityBone = base.transform.Find("bg/ui_7day_tree/Root/box7").gameObject.GetComponent<SkeletonUtilityBone>();
		detail.box7_BoneFollower = base.transform.Find("bg/ui_7day_tree/Root/box7").gameObject.GetComponent<BoneFollower>();
		detail.ui_7day_tree_SkeletonAnimation = base.transform.Find("bg/ui_7day_tree").gameObject.GetComponent<SkeletonAnimation>();
		detail.TextDemo3_Text = base.transform.Find("bg/GameObject/TextDemo (3)").gameObject.GetComponent<Text>();
		detail.TextDemo3_Shadow = base.transform.Find("bg/GameObject/TextDemo (3)").gameObject.GetComponent<Shadow>();
		detail.TextDemo4_Text = base.transform.Find("bg/GameObject/TextDemo (4)").gameObject.GetComponent<Text>();
		detail.TextDemo4_Shadow = base.transform.Find("bg/GameObject/TextDemo (4)").gameObject.GetComponent<Shadow>();
		detail.TextDemo5_Text = base.transform.Find("bg/GameObject/TextDemo (5)").gameObject.GetComponent<Text>();
		detail.TextDemo5_Shadow = base.transform.Find("bg/GameObject/TextDemo (5)").gameObject.GetComponent<Shadow>();
		detail.TextDemo6_Text = base.transform.Find("bg/GameObject/TextDemo (6)").gameObject.GetComponent<Text>();
		detail.TextDemo6_Shadow = base.transform.Find("bg/GameObject/TextDemo (6)").gameObject.GetComponent<Shadow>();
		detail.TextDemo7_Text = base.transform.Find("bg/GameObject/TextDemo (7)").gameObject.GetComponent<Text>();
		detail.TextDemo7_Shadow = base.transform.Find("bg/GameObject/TextDemo (7)").gameObject.GetComponent<Shadow>();
		detail.TextDemo8_Text = base.transform.Find("bg/GameObject/TextDemo (8)").gameObject.GetComponent<Text>();
		detail.TextDemo8_Shadow = base.transform.Find("bg/GameObject/TextDemo (8)").gameObject.GetComponent<Shadow>();
		detail.TextDemo9_Text = base.transform.Find("bg/GameObject/TextDemo (9)").gameObject.GetComponent<Text>();
		detail.TextDemo9_Shadow = base.transform.Find("bg/GameObject/TextDemo (9)").gameObject.GetComponent<Shadow>();
		detail.down_Text = base.transform.Find("bg/GameObject/down").gameObject.GetComponent<Text>();
		detail.down_Shadow = base.transform.Find("bg/GameObject/down").gameObject.GetComponent<Shadow>();
		detail.down1_Text = base.transform.Find("bg/GameObject/down (1)").gameObject.GetComponent<Text>();
		detail.down1_Shadow = base.transform.Find("bg/GameObject/down (1)").gameObject.GetComponent<Shadow>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		detail.CloseButton_Image = base.transform.Find("CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("CloseButton").gameObject.GetComponent<Button>();
		detail.Image_Image = base.transform.Find("Image").gameObject.GetComponent<Image>();
		detail.icon1_Image = base.transform.Find("max/icon1").gameObject.GetComponent<Image>();
		detail.icon2_Image = base.transform.Find("max/icon2").gameObject.GetComponent<Image>();
		detail.icon3_Image = base.transform.Find("max/icon3").gameObject.GetComponent<Image>();
		detail.icon4_Image = base.transform.Find("max/icon4").gameObject.GetComponent<Image>();
		detail.text1_Text = base.transform.Find("max/text1").gameObject.GetComponent<Text>();
		detail.text1_Shadow = base.transform.Find("max/text1").gameObject.GetComponent<Shadow>();
		detail.text2_Text = base.transform.Find("max/text2").gameObject.GetComponent<Text>();
		detail.text2_Shadow = base.transform.Find("max/text2").gameObject.GetComponent<Shadow>();
		detail.text3_Text = base.transform.Find("max/text3").gameObject.GetComponent<Text>();
		detail.text3_Shadow = base.transform.Find("max/text3").gameObject.GetComponent<Shadow>();
		detail.text4_Text = base.transform.Find("max/text4").gameObject.GetComponent<Text>();
		detail.text4_Shadow = base.transform.Find("max/text4").gameObject.GetComponent<Shadow>();
		detail.text_Text = base.transform.Find("max/text").gameObject.GetComponent<Text>();
		detail.text_Shadow = base.transform.Find("max/text").gameObject.GetComponent<Shadow>();
		detail.max_Image = base.transform.Find("max").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.Image11_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnImage11);
		BtnAnimationBase btnAnimationBase2 = detail.Image21_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnImage21);
		BtnAnimationBase btnAnimationBase3 = detail.S_Image22_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase3.SetType(NewBtnType.STATIC);
		btnAnimationBase3.SetAction(OnS_Image22);
		BtnAnimationBase btnAnimationBase4 = detail.Image31_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase4.SetType(NewBtnType.NONE);
		btnAnimationBase4.SetAction(OnImage31);
		BtnAnimationBase btnAnimationBase5 = detail.S_Image33_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase5.SetType(NewBtnType.STATIC);
		btnAnimationBase5.SetAction(OnS_Image33);
		BtnAnimationBase btnAnimationBase6 = detail.Image41_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase6.SetType(NewBtnType.NONE);
		btnAnimationBase6.SetAction(OnImage41);
		BtnAnimationBase btnAnimationBase7 = detail.S_Image44_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase7.SetType(NewBtnType.STATIC);
		btnAnimationBase7.SetAction(OnS_Image44);
		BtnAnimationBase btnAnimationBase8 = detail.Image51_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase8.SetType(NewBtnType.NONE);
		btnAnimationBase8.SetAction(OnImage51);
		BtnAnimationBase btnAnimationBase9 = detail.S_Image55_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase9.SetType(NewBtnType.STATIC);
		btnAnimationBase9.SetAction(OnS_Image55);
		BtnAnimationBase btnAnimationBase10 = detail.Image61_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase10.SetType(NewBtnType.NONE);
		btnAnimationBase10.SetAction(OnImage61);
		BtnAnimationBase btnAnimationBase11 = detail.S_Image66_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase11.SetType(NewBtnType.STATIC);
		btnAnimationBase11.SetAction(OnS_Image66);
		BtnAnimationBase btnAnimationBase12 = detail.Image71_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase12.SetType(NewBtnType.NONE);
		btnAnimationBase12.SetAction(OnImage71);
		BtnAnimationBase btnAnimationBase13 = detail.S_Image77_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase13.SetType(NewBtnType.STATIC);
		btnAnimationBase13.SetAction(OnS_Image77);
		BtnAnimationBase btnAnimationBase14 = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase14.SetType(NewBtnType.NONE);
		btnAnimationBase14.SetAction(OnCloseButton);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnImage11()
	{
	}

	public virtual void OnImage21()
	{
	}

	public virtual void OnS_Image22()
	{
	}

	public virtual void OnImage31()
	{
	}

	public virtual void OnS_Image33()
	{
	}

	public virtual void OnImage41()
	{
	}

	public virtual void OnS_Image44()
	{
	}

	public virtual void OnImage51()
	{
	}

	public virtual void OnS_Image55()
	{
	}

	public virtual void OnImage61()
	{
	}

	public virtual void OnS_Image66()
	{
	}

	public virtual void OnImage71()
	{
	}

	public virtual void OnS_Image77()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}
}
