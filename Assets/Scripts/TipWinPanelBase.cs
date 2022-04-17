using System;
using UnityEngine;
using UnityEngine.UI;

public class TipWinPanelBase : BasePanel
{
	public TipWinPanelDetail detail;

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
		detail.goal_img1_Image = base.transform.Find("bg/goal_img1").gameObject.GetComponent<Image>();
		detail.TipWinUITitle_Text = base.transform.Find("bg/TipWinUITitle").gameObject.GetComponent<Text>();
		detail.TipWinUITitle_Gradient = base.transform.Find("bg/TipWinUITitle").gameObject.GetComponent<Gradient>();
		detail.TipWinUITitle_Shadow = base.transform.Find("bg/TipWinUITitle").gameObject.GetComponent<Shadow>();
		detail.TipWinUITitle_ContentSizeFitter = base.transform.Find("bg/TipWinUITitle").gameObject.GetComponent<ContentSizeFitter>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
	}

	public virtual void InitUI()
	{
	}
}
