using System;
using UnityEngine;
using UnityEngine.UI;

public class GuideMinUIPanelBase : BasePanel
{
	public GuideMinUIPanelDetail detail;

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
		detail.GuideMinRemark_Text = base.transform.Find("Panel/Top/GuideMinRemark").gameObject.GetComponent<Text>();
		detail.GuideMinRemark_Shadow = base.transform.Find("Panel/Top/GuideMinRemark").gameObject.GetComponent<Shadow>();
		detail.GuideMinRemark_ContentSizeFitter = base.transform.Find("Panel/Top/GuideMinRemark").gameObject.GetComponent<ContentSizeFitter>();
		detail.Image_Image = base.transform.Find("Panel/Top/Image").gameObject.GetComponent<Image>();
		detail.Top_Image = base.transform.Find("Panel/Top").gameObject.GetComponent<Image>();
	}

	public virtual void InitUI()
	{
	}
}
