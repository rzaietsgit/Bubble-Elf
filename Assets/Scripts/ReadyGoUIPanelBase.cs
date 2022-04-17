using System;
using UnityEngine;
using UnityEngine.UI;

public class ReadyGoUIPanelBase : BasePanel
{
	public ReadyGoUIPanelDetail detail;

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
		detail.goal_img1_Image = base.transform.Find("bg/Center/goal_img1").gameObject.GetComponent<Image>();
		detail.ReadGoUI_Text = base.transform.Find("bg/Center/ReadGoUI").gameObject.GetComponent<Text>();
		detail.ReadGoUI_Gradient = base.transform.Find("bg/Center/ReadGoUI").gameObject.GetComponent<Gradient>();
		detail.ReadGoUI_Shadow = base.transform.Find("bg/Center/ReadGoUI").gameObject.GetComponent<Shadow>();
		detail.ReadGoUI_ContentSizeFitter = base.transform.Find("bg/Center/ReadGoUI").gameObject.GetComponent<ContentSizeFitter>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
	}

	public virtual void InitUI()
	{
	}
}
