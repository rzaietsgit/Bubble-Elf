using System;
using UnityEngine;
using UnityEngine.UI;

public class PackSkillIconUIPanelBase : BasePanel
{
	public PackSkillIconUIPanelDetail detail;

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
		detail.TextDemo_Text = base.transform.Find("bg/TextDemo").gameObject.GetComponent<Text>();
		detail.TextDemo_Shadow = base.transform.Find("bg/TextDemo").gameObject.GetComponent<Shadow>();
		detail.TextDemo_ContentSizeFitter = base.transform.Find("bg/TextDemo").gameObject.GetComponent<ContentSizeFitter>();
		detail.TimeTip_Image = base.transform.Find("bg/bg1/bg (2)/group/PackSkillIcon/TimeTip").gameObject.GetComponent<Image>();
		detail.Title_Text = base.transform.Find("bg/bg1/bg (2)/group/PackSkillIcon/Remark/Title").gameObject.GetComponent<Text>();
		detail.Title_Shadow = base.transform.Find("bg/bg1/bg (2)/group/PackSkillIcon/Remark/Title").gameObject.GetComponent<Shadow>();
		detail.Title_ContentSizeFitter = base.transform.Find("bg/bg1/bg (2)/group/PackSkillIcon/Remark/Title").gameObject.GetComponent<ContentSizeFitter>();
		detail.Text_Text = base.transform.Find("bg/bg1/bg (2)/group/PackSkillIcon/Remark/Text").gameObject.GetComponent<Text>();
		detail.Text_Shadow = base.transform.Find("bg/bg1/bg (2)/group/PackSkillIcon/Remark/Text").gameObject.GetComponent<Shadow>();
		detail.Text_ContentSizeFitter = base.transform.Find("bg/bg1/bg (2)/group/PackSkillIcon/Remark/Text").gameObject.GetComponent<ContentSizeFitter>();
		detail.Remark_Image = base.transform.Find("bg/bg1/bg (2)/group/PackSkillIcon/Remark").gameObject.GetComponent<Image>();
		detail.PackSkillIcon_Image = base.transform.Find("bg/bg1/bg (2)/group/PackSkillIcon").gameObject.GetComponent<Image>();
		detail.PackSkillIcon_PackSkillIcon = base.transform.Find("bg/bg1/bg (2)/group/PackSkillIcon").gameObject.GetComponent<PackSkillIcon>();
		detail.group_GridLayoutGroup = base.transform.Find("bg/bg1/bg (2)/group").gameObject.GetComponent<GridLayoutGroup>();
		detail.bg2_Image = base.transform.Find("bg/bg1/bg (2)").gameObject.GetComponent<Image>();
		detail.bg2_ScrollRect = base.transform.Find("bg/bg1/bg (2)").gameObject.GetComponent<ScrollRect>();
		detail.bg2_Mask = base.transform.Find("bg/bg1/bg (2)").gameObject.GetComponent<Mask>();
		detail.bg1_Image = base.transform.Find("bg/bg1").gameObject.GetComponent<Image>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnCloseButton);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}
}
