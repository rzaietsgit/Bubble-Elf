using System;
using UnityEngine;
using UnityEngine.UI;

public class MapRewardUIPanelBase : BasePanel
{
	public MapRewardUIPanelDetail detail;

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
		detail.MapRewardUITitle_Text = base.transform.Find("bg/MapRewardUITitle").gameObject.GetComponent<Text>();
		detail.MapRewardUITitle_Shadow = base.transform.Find("bg/MapRewardUITitle").gameObject.GetComponent<Shadow>();
		detail.MapRewardUITitle_ContentSizeFitter = base.transform.Find("bg/MapRewardUITitle").gameObject.GetComponent<ContentSizeFitter>();
		detail.MapRewardUIRemark_Text = base.transform.Find("bg/Strarline/MapRewardUIRemark").gameObject.GetComponent<Text>();
		detail.MapRewardUIRemark_Shadow = base.transform.Find("bg/Strarline/MapRewardUIRemark").gameObject.GetComponent<Shadow>();
		detail.passline_Image = base.transform.Find("bg/Strarline/line/passline").gameObject.GetComponent<Image>();
		detail.Image_Image = base.transform.Find("bg/Strarline/line/Image").gameObject.GetComponent<Image>();
		detail.StarCount_Text = base.transform.Find("bg/Strarline/line/StarCount").gameObject.GetComponent<Text>();
		detail.StarCount_Shadow = base.transform.Find("bg/Strarline/line/StarCount").gameObject.GetComponent<Shadow>();
		detail.line_Image = base.transform.Find("bg/Strarline/line").gameObject.GetComponent<Image>();
		detail.p_reward_Image = base.transform.Find("bg/bgpanel/group/p_reward").gameObject.GetComponent<Image>();
		detail.p_reward_MapRewardPanelSon = base.transform.Find("bg/bgpanel/group/p_reward").gameObject.GetComponent<MapRewardPanelSon>();
		detail.group_GridLayoutGroup = base.transform.Find("bg/bgpanel/group").gameObject.GetComponent<GridLayoutGroup>();
		detail.bgpanel_Image = base.transform.Find("bg/bgpanel").gameObject.GetComponent<Image>();
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
