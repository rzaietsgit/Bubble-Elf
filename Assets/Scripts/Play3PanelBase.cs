using System;
using UnityEngine;
using UnityEngine.UI;

public class Play3PanelBase : BasePanel
{
	public Play3PanelDetail detail;

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
		detail.AdAwardTitle_Text = base.transform.Find("bg/BuyBubbleTitleBg/AdAwardTitle").gameObject.GetComponent<Text>();
		detail.AdAwardTitle_Shadow = base.transform.Find("bg/BuyBubbleTitleBg/AdAwardTitle").gameObject.GetComponent<Shadow>();
		detail.AdAwardTitle_ContentSizeFitter = base.transform.Find("bg/BuyBubbleTitleBg/AdAwardTitle").gameObject.GetComponent<ContentSizeFitter>();
		detail.BuyBubbleTitleBg_Image = base.transform.Find("bg/BuyBubbleTitleBg").gameObject.GetComponent<Image>();
		detail.Count_Text = base.transform.Find("bg/Count").gameObject.GetComponent<Text>();
		detail.Count_Shadow = base.transform.Find("bg/Count").gameObject.GetComponent<Shadow>();
		detail.AdAwardRemark_Text = base.transform.Find("bg/AdAwardRemark").gameObject.GetComponent<Text>();
		detail.AdAwardRemark_Shadow = base.transform.Find("bg/AdAwardRemark").gameObject.GetComponent<Shadow>();
		detail.AdAwardRemark_ContentSizeFitter = base.transform.Find("bg/AdAwardRemark").gameObject.GetComponent<ContentSizeFitter>();
		detail.Text_Text = base.transform.Find("bg/Text").gameObject.GetComponent<Text>();
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
