using System;
using UnityEngine;
using UnityEngine.UI;

public class NewTask1UIPanelBase : BasePanel
{
	public NewTask1UIPanelDetail detail;

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
		detail.TextDemo4_Text = base.transform.Find("Top/TextDemo (4)").gameObject.GetComponent<Text>();
		detail.TextDemo4_Shadow = base.transform.Find("Top/TextDemo (4)").gameObject.GetComponent<Shadow>();
		detail.time_Text = base.transform.Find("Top/time").gameObject.GetComponent<Text>();
		detail.time_Shadow = base.transform.Find("Top/time").gameObject.GetComponent<Shadow>();
		detail.day1_Text = base.transform.Find("Top/GameObject/Image1/day1").gameObject.GetComponent<Text>();
		detail.day1_Shadow = base.transform.Find("Top/GameObject/Image1/day1").gameObject.GetComponent<Shadow>();
		detail.mask1_Image = base.transform.Find("Top/GameObject/Image1/mask1").gameObject.GetComponent<Image>();
		detail.Image1_Image = base.transform.Find("Top/GameObject/Image1").gameObject.GetComponent<Image>();
		detail.Image1_Button = base.transform.Find("Top/GameObject/Image1").gameObject.GetComponent<Button>();
		detail.day2_Text = base.transform.Find("Top/GameObject/Image2/day2").gameObject.GetComponent<Text>();
		detail.mask2_Image = base.transform.Find("Top/GameObject/Image2/mask2").gameObject.GetComponent<Image>();
		detail.Image2_Image = base.transform.Find("Top/GameObject/Image2").gameObject.GetComponent<Image>();
		detail.Image2_Button = base.transform.Find("Top/GameObject/Image2").gameObject.GetComponent<Button>();
		detail.day3_Text = base.transform.Find("Top/GameObject/Image3/day3").gameObject.GetComponent<Text>();
		detail.day3_Shadow = base.transform.Find("Top/GameObject/Image3/day3").gameObject.GetComponent<Shadow>();
		detail.mask3_Image = base.transform.Find("Top/GameObject/Image3/mask3").gameObject.GetComponent<Image>();
		detail.Image3_Image = base.transform.Find("Top/GameObject/Image3").gameObject.GetComponent<Image>();
		detail.Image3_Button = base.transform.Find("Top/GameObject/Image3").gameObject.GetComponent<Button>();
		detail.day4_Text = base.transform.Find("Top/GameObject/Image4/day4").gameObject.GetComponent<Text>();
		detail.day4_Shadow = base.transform.Find("Top/GameObject/Image4/day4").gameObject.GetComponent<Shadow>();
		detail.mask4_Image = base.transform.Find("Top/GameObject/Image4/mask4").gameObject.GetComponent<Image>();
		detail.Image4_Image = base.transform.Find("Top/GameObject/Image4").gameObject.GetComponent<Image>();
		detail.Image4_Button = base.transform.Find("Top/GameObject/Image4").gameObject.GetComponent<Button>();
		detail.day5_Text = base.transform.Find("Top/GameObject/Image5/day5").gameObject.GetComponent<Text>();
		detail.day5_Shadow = base.transform.Find("Top/GameObject/Image5/day5").gameObject.GetComponent<Shadow>();
		detail.mask5_Image = base.transform.Find("Top/GameObject/Image5/mask5").gameObject.GetComponent<Image>();
		detail.Image5_Image = base.transform.Find("Top/GameObject/Image5").gameObject.GetComponent<Image>();
		detail.Image5_Button = base.transform.Find("Top/GameObject/Image5").gameObject.GetComponent<Button>();
		detail.day6_Text = base.transform.Find("Top/GameObject/Image6/day6").gameObject.GetComponent<Text>();
		detail.day6_Shadow = base.transform.Find("Top/GameObject/Image6/day6").gameObject.GetComponent<Shadow>();
		detail.mask6_Image = base.transform.Find("Top/GameObject/Image6/mask6").gameObject.GetComponent<Image>();
		detail.Image6_Image = base.transform.Find("Top/GameObject/Image6").gameObject.GetComponent<Image>();
		detail.Image6_Button = base.transform.Find("Top/GameObject/Image6").gameObject.GetComponent<Button>();
		detail.day7_Text = base.transform.Find("Top/GameObject/Image7/day7").gameObject.GetComponent<Text>();
		detail.day7_Shadow = base.transform.Find("Top/GameObject/Image7/day7").gameObject.GetComponent<Shadow>();
		detail.mask7_Image = base.transform.Find("Top/GameObject/Image7/mask7").gameObject.GetComponent<Image>();
		detail.Image7_Image = base.transform.Find("Top/GameObject/Image7").gameObject.GetComponent<Image>();
		detail.Image7_Button = base.transform.Find("Top/GameObject/Image7").gameObject.GetComponent<Button>();
		detail.GameObject_Image = base.transform.Find("Top/GameObject").gameObject.GetComponent<Image>();
		detail.TextDemo2_Text = base.transform.Find("Top/Image/TextDemo (2)").gameObject.GetComponent<Text>();
		detail.TextDemo2_Shadow = base.transform.Find("Top/Image/TextDemo (2)").gameObject.GetComponent<Shadow>();
		detail.TextDemo2_ContentSizeFitter = base.transform.Find("Top/Image/TextDemo (2)").gameObject.GetComponent<ContentSizeFitter>();
		detail.Image_Image = base.transform.Find("Top/Image").gameObject.GetComponent<Image>();
		detail.Top_Image = base.transform.Find("Top").gameObject.GetComponent<Image>();
		detail.Image8_Image = base.transform.Find("Image8").gameObject.GetComponent<Image>();
		detail.TextDemo3_Text = base.transform.Find("down/TextDemo (3)").gameObject.GetComponent<Text>();
		detail.TextDemo3_Shadow = base.transform.Find("down/TextDemo (3)").gameObject.GetComponent<Shadow>();
		detail.CloseButton_Image = base.transform.Find("down/CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("down/CloseButton").gameObject.GetComponent<Button>();
		detail.DayText_Text = base.transform.Find("down/R1Obj/Image9/Image10/DayText").gameObject.GetComponent<Text>();
		detail.DayText_Shadow = base.transform.Find("down/R1Obj/Image9/Image10/DayText").gameObject.GetComponent<Shadow>();
		detail.Image10_Image = base.transform.Find("down/R1Obj/Image9/Image10").gameObject.GetComponent<Image>();
		detail.passText_Text = base.transform.Find("down/R1Obj/Image9/passText").gameObject.GetComponent<Text>();
		detail.passText_Shadow = base.transform.Find("down/R1Obj/Image9/passText").gameObject.GetComponent<Shadow>();
		detail.Image12_Image = base.transform.Find("down/R1Obj/Image9/Image (11)/Image12").gameObject.GetComponent<Image>();
		detail.text1_Text = base.transform.Find("down/R1Obj/Image9/Image (11)/text1").gameObject.GetComponent<Text>();
		detail.text1_Shadow = base.transform.Find("down/R1Obj/Image9/Image (11)/text1").gameObject.GetComponent<Shadow>();
		detail.Image11_Image = base.transform.Find("down/R1Obj/Image9/Image (11)").gameObject.GetComponent<Image>();
		detail.TextDemo5_Text = base.transform.Find("down/R1Obj/Image9/Reward1/Image (26)/Image14/TextDemo (5)").gameObject.GetComponent<Text>();
		detail.TextDemo5_Shadow = base.transform.Find("down/R1Obj/Image9/Reward1/Image (26)/Image14/TextDemo (5)").gameObject.GetComponent<Shadow>();
		detail.Image14_Image = base.transform.Find("down/R1Obj/Image9/Reward1/Image (26)/Image14").gameObject.GetComponent<Image>();
		detail.Image26_Image = base.transform.Find("down/R1Obj/Image9/Reward1/Image (26)").gameObject.GetComponent<Image>();
		detail.Image26_Button = base.transform.Find("down/R1Obj/Image9/Reward1/Image (26)").gameObject.GetComponent<Button>();
		detail.TextDemo6_Text = base.transform.Find("down/R1Obj/Image9/Reward1/Image (13)/Image15/TextDemo (6)").gameObject.GetComponent<Text>();
		detail.TextDemo6_Shadow = base.transform.Find("down/R1Obj/Image9/Reward1/Image (13)/Image15/TextDemo (6)").gameObject.GetComponent<Shadow>();
		detail.Image15_Image = base.transform.Find("down/R1Obj/Image9/Reward1/Image (13)/Image15").gameObject.GetComponent<Image>();
		detail.Image13_Image = base.transform.Find("down/R1Obj/Image9/Reward1/Image (13)").gameObject.GetComponent<Image>();
		detail.Image13_Button = base.transform.Find("down/R1Obj/Image9/Reward1/Image (13)").gameObject.GetComponent<Button>();
		detail.Reward1Text_Text = base.transform.Find("down/R1Obj/Image9/Reward1/Reward1Text").gameObject.GetComponent<Text>();
		detail.Reward1Text_Shadow = base.transform.Find("down/R1Obj/Image9/Reward1/Reward1Text").gameObject.GetComponent<Shadow>();
		detail.Reward1_Image = base.transform.Find("down/R1Obj/Image9/Reward1").gameObject.GetComponent<Image>();
		detail.passimg_Image = base.transform.Find("down/R1Obj/Image9/passimg").gameObject.GetComponent<Image>();
		detail.Image9_Image = base.transform.Find("down/R1Obj/Image9").gameObject.GetComponent<Image>();
		detail.Reward1Ok_Image = base.transform.Find("down/R1Obj/Reward1Ok").gameObject.GetComponent<Image>();
		detail.Button_Image = base.transform.Find("down/R1Obj/Button").gameObject.GetComponent<Image>();
		detail.Button_Button = base.transform.Find("down/R1Obj/Button").gameObject.GetComponent<Button>();
		detail.passText2_Text = base.transform.Find("down/R2Obj/Image16/Image17/passText (2)").gameObject.GetComponent<Text>();
		detail.passText2_Shadow = base.transform.Find("down/R2Obj/Image16/Image17/passText (2)").gameObject.GetComponent<Shadow>();
		detail.Image17_Image = base.transform.Find("down/R2Obj/Image16/Image17").gameObject.GetComponent<Image>();
		detail.Image16_Image = base.transform.Find("down/R2Obj/Image16").gameObject.GetComponent<Image>();
		detail.TextDemo7_Text = base.transform.Find("down/R2Obj/TextDemo (7)").gameObject.GetComponent<Text>();
		detail.TextDemo8_Text = base.transform.Find("down/R2Obj/Reward2/Image (18)/Image19/TextDemo (8)").gameObject.GetComponent<Text>();
		detail.TextDemo8_Shadow = base.transform.Find("down/R2Obj/Reward2/Image (18)/Image19/TextDemo (8)").gameObject.GetComponent<Shadow>();
		detail.Image19_Image = base.transform.Find("down/R2Obj/Reward2/Image (18)/Image19").gameObject.GetComponent<Image>();
		detail.Image18_Image = base.transform.Find("down/R2Obj/Reward2/Image (18)").gameObject.GetComponent<Image>();
		detail.Image18_Button = base.transform.Find("down/R2Obj/Reward2/Image (18)").gameObject.GetComponent<Button>();
		detail.TextDemo9_Text = base.transform.Find("down/R2Obj/Reward2/Image (20)/Image21/TextDemo (9)").gameObject.GetComponent<Text>();
		detail.TextDemo9_Shadow = base.transform.Find("down/R2Obj/Reward2/Image (20)/Image21/TextDemo (9)").gameObject.GetComponent<Shadow>();
		detail.Image21_Image = base.transform.Find("down/R2Obj/Reward2/Image (20)/Image21").gameObject.GetComponent<Image>();
		detail.Image20_Image = base.transform.Find("down/R2Obj/Reward2/Image (20)").gameObject.GetComponent<Image>();
		detail.Image20_Button = base.transform.Find("down/R2Obj/Reward2/Image (20)").gameObject.GetComponent<Button>();
		detail.Reward2Text_Text = base.transform.Find("down/R2Obj/Reward2/Reward2Text").gameObject.GetComponent<Text>();
		detail.Reward2Text_Shadow = base.transform.Find("down/R2Obj/Reward2/Reward2Text").gameObject.GetComponent<Shadow>();
		detail.Reward2_Image = base.transform.Find("down/R2Obj/Reward2").gameObject.GetComponent<Image>();
		detail.Reward2Ok_Image = base.transform.Find("down/R2Obj/Reward2Ok").gameObject.GetComponent<Image>();
		detail.passimg1_Image = base.transform.Find("down/R2Obj/passimg (1)").gameObject.GetComponent<Image>();
		detail.passText1_Text = base.transform.Find("down/R2Obj/passText (1)").gameObject.GetComponent<Text>();
		detail.passText1_Shadow = base.transform.Find("down/R2Obj/passText (1)").gameObject.GetComponent<Shadow>();
		detail.Button1_Image = base.transform.Find("down/R2Obj/Button (1)").gameObject.GetComponent<Image>();
		detail.Button1_Button = base.transform.Find("down/R2Obj/Button (1)").gameObject.GetComponent<Button>();
		detail.down_Image = base.transform.Find("down").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.Image1_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnImage1);
		BtnAnimationBase btnAnimationBase2 = detail.Image2_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnImage2);
		BtnAnimationBase btnAnimationBase3 = detail.Image3_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase3.SetType(NewBtnType.NONE);
		btnAnimationBase3.SetAction(OnImage3);
		BtnAnimationBase btnAnimationBase4 = detail.Image4_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase4.SetType(NewBtnType.NONE);
		btnAnimationBase4.SetAction(OnImage4);
		BtnAnimationBase btnAnimationBase5 = detail.Image5_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase5.SetType(NewBtnType.NONE);
		btnAnimationBase5.SetAction(OnImage5);
		BtnAnimationBase btnAnimationBase6 = detail.Image6_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase6.SetType(NewBtnType.NONE);
		btnAnimationBase6.SetAction(OnImage6);
		BtnAnimationBase btnAnimationBase7 = detail.Image7_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase7.SetType(NewBtnType.NONE);
		btnAnimationBase7.SetAction(OnImage7);
		BtnAnimationBase btnAnimationBase8 = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase8.SetType(NewBtnType.NONE);
		btnAnimationBase8.SetAction(OnCloseButton);
		BtnAnimationBase btnAnimationBase9 = detail.Image26_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase9.SetType(NewBtnType.NONE);
		btnAnimationBase9.SetAction(OnImage26);
		BtnAnimationBase btnAnimationBase10 = detail.Image13_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase10.SetType(NewBtnType.NONE);
		btnAnimationBase10.SetAction(OnImage13);
		BtnAnimationBase btnAnimationBase11 = detail.Button_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase11.SetType(NewBtnType.NONE);
		btnAnimationBase11.SetAction(OnButton);
		BtnAnimationBase btnAnimationBase12 = detail.Image18_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase12.SetType(NewBtnType.NONE);
		btnAnimationBase12.SetAction(OnImage18);
		BtnAnimationBase btnAnimationBase13 = detail.Image20_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase13.SetType(NewBtnType.NONE);
		btnAnimationBase13.SetAction(OnImage20);
		BtnAnimationBase btnAnimationBase14 = detail.Button1_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase14.SetType(NewBtnType.NONE);
		btnAnimationBase14.SetAction(OnButton1);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnImage1()
	{
	}

	public virtual void OnImage2()
	{
	}

	public virtual void OnImage3()
	{
	}

	public virtual void OnImage4()
	{
	}

	public virtual void OnImage5()
	{
	}

	public virtual void OnImage6()
	{
	}

	public virtual void OnImage7()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}

	public virtual void OnImage26()
	{
	}

	public virtual void OnImage13()
	{
	}

	public virtual void OnButton()
	{
	}

	public virtual void OnImage18()
	{
	}

	public virtual void OnImage20()
	{
	}

	public virtual void OnButton1()
	{
	}
}
