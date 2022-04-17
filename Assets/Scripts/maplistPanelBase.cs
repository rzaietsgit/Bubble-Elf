using Spine.Unity;
using System;
using UnityEngine;
using UnityEngine.UI;

public class maplistPanelBase : BasePanel
{
	public maplistPanelDetail detail;

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
		detail.Image_Image = base.transform.Find("bg/SetSetPanelTitle/Image").gameObject.GetComponent<Image>();
		detail.SetSetPanelTitle_Text = base.transform.Find("bg/SetSetPanelTitle").gameObject.GetComponent<Text>();
		detail.SetSetPanelTitle_Gradient = base.transform.Find("bg/SetSetPanelTitle").gameObject.GetComponent<Gradient>();
		detail.SetSetPanelTitle_Shadow = base.transform.Find("bg/SetSetPanelTitle").gameObject.GetComponent<Shadow>();
		detail.SetSetPanelTitle_ContentSizeFitter = base.transform.Find("bg/SetSetPanelTitle").gameObject.GetComponent<ContentSizeFitter>();
		detail.LevelRemark_Text = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/LevelRemark").gameObject.GetComponent<Text>();
		detail.LevelRemark_Shadow = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/LevelRemark").gameObject.GetComponent<Shadow>();
		detail.MapNameText_Text = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/mapIcon/MapNameText").gameObject.GetComponent<Text>();
		detail.MapNameText_Shadow = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/mapIcon/MapNameText").gameObject.GetComponent<Shadow>();
		detail.mapIcon_Image = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/mapIcon").gameObject.GetComponent<Image>();
		detail.mapIcon_Button = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/mapIcon").gameObject.GetComponent<Button>();
		detail.RewardIcon_Image = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/RewardBgBtn/RewardIcon").gameObject.GetComponent<Image>();
		detail.discoun_icon_ui_SkeletonGraphic = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/RewardBgBtn/discoun_icon_ui").gameObject.GetComponent<SkeletonGraphic>();
		detail.RewardBgBtn_Image = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/RewardBgBtn").gameObject.GetComponent<Image>();
		detail.RewardBgBtn_Button = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/RewardBgBtn").gameObject.GetComponent<Button>();
		detail.line_Image = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/linebg/line").gameObject.GetComponent<Image>();
		detail.StarCount_Text = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/linebg/Star/StarCount").gameObject.GetComponent<Text>();
		detail.StarCount_Shadow = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/linebg/Star/StarCount").gameObject.GetComponent<Shadow>();
		detail.Star_Image = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/linebg/Star").gameObject.GetComponent<Image>();
		detail.Image_Image = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/linebg/Image").gameObject.GetComponent<Image>();
		detail.Star1_Text = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/linebg/Star1").gameObject.GetComponent<Text>();
		detail.Star1_Shadow = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/linebg/Star1").gameObject.GetComponent<Shadow>();
		detail.Star2_Text = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/linebg/Star2").gameObject.GetComponent<Text>();
		detail.Star2_Shadow = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/linebg/Star2").gameObject.GetComponent<Shadow>();
		detail.Star3_Text = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/linebg/Star3").gameObject.GetComponent<Text>();
		detail.Star3_Shadow = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/linebg/Star3").gameObject.GetComponent<Shadow>();
		detail.linebg_Image = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/linebg").gameObject.GetComponent<Image>();
		detail.TipText_Text = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/Tip/TipText").gameObject.GetComponent<Text>();
		detail.TipText_Shadow = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/Tip/TipText").gameObject.GetComponent<Shadow>();
		detail.Tip_Image = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina/Tip").gameObject.GetComponent<Image>();
		detail.MapObjChina_Image = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina").gameObject.GetComponent<Image>();
		detail.MapObjChina_MapObjChina = base.transform.Find("bg/MapPanel/Map/MapObjList/MapObjChina").gameObject.GetComponent<MapObjChina>();
		detail.MapObjList_GridLayoutGroup = base.transform.Find("bg/MapPanel/Map/MapObjList").gameObject.GetComponent<GridLayoutGroup>();
		detail.Map_Image = base.transform.Find("bg/MapPanel/Map").gameObject.GetComponent<Image>();
		detail.Map_ScrollRect = base.transform.Find("bg/MapPanel/Map").gameObject.GetComponent<ScrollRect>();
		detail.Map_Mask = base.transform.Find("bg/MapPanel/Map").gameObject.GetComponent<Mask>();
		detail.MapPanel_MapPanelUI = base.transform.Find("bg/MapPanel").gameObject.GetComponent<MapPanelUI>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
		detail.CloseButton_Image = base.transform.Find("CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("CloseButton").gameObject.GetComponent<Button>();
		BtnAnimationBase btnAnimationBase = detail.mapIcon_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnmapIcon);
		BtnAnimationBase btnAnimationBase2 = detail.RewardBgBtn_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnRewardBgBtn);
		BtnAnimationBase btnAnimationBase3 = detail.CloseButton_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase3.SetType(NewBtnType.NONE);
		btnAnimationBase3.SetAction(OnCloseButton);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnmapIcon()
	{
	}

	public virtual void OnRewardBgBtn()
	{
	}

	public virtual void OnCloseButton()
	{
		UI.Instance.ClosePanel();
	}
}
