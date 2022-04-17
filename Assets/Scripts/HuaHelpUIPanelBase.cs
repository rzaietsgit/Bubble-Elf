using System;
using UnityEngine;
using UnityEngine.UI;

public class HuaHelpUIPanelBase : BasePanel
{
	public HuaHelpUIPanelDetail detail;

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
		detail.tiaozhan1_Text = base.transform.Find("Top/PlayLevelTitle/tiaozhan1").gameObject.GetComponent<Text>();
		detail.tiaozhan1_Shadow = base.transform.Find("Top/PlayLevelTitle/tiaozhan1").gameObject.GetComponent<Shadow>();
		detail.tiaozhan1_ContentSizeFitter = base.transform.Find("Top/PlayLevelTitle/tiaozhan1").gameObject.GetComponent<ContentSizeFitter>();
		detail.PlayLevelTitle_Image = base.transform.Find("Top/PlayLevelTitle").gameObject.GetComponent<Image>();
		detail.CloseButton_Image = base.transform.Find("Top/CloseButton").gameObject.GetComponent<Image>();
		detail.CloseButton_Button = base.transform.Find("Top/CloseButton").gameObject.GetComponent<Button>();
		detail.Top_Image = base.transform.Find("Top").gameObject.GetComponent<Image>();
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
