using System;
using UnityEngine;
using UnityEngine.UI;

public class TipFailPanelBase : BasePanel
{
	public TipFailPanelDetail detail;

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
		detail.TipFailUITitle_Text = base.transform.Find("bg/Center/TipFailUITitle").gameObject.GetComponent<Text>();
		detail.TipFailUITitle_Gradient = base.transform.Find("bg/Center/TipFailUITitle").gameObject.GetComponent<Gradient>();
		detail.TipFailUITitle_Shadow = base.transform.Find("bg/Center/TipFailUITitle").gameObject.GetComponent<Shadow>();
		detail.TipFailUITitle_ContentSizeFitter = base.transform.Find("bg/Center/TipFailUITitle").gameObject.GetComponent<ContentSizeFitter>();
		detail.bg_Image = base.transform.Find("bg").gameObject.GetComponent<Image>();
	}

	public virtual void InitUI()
	{
	}
}
