using System;
using UnityEngine;
using UnityEngine.UI;

public class PayMaskPanelBase : BasePanel
{
	public PayMaskPanelDetail detail;

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
		detail.Image1_Image = base.transform.Find("Image/Image (1)").gameObject.GetComponent<Image>();
		detail.Image_Image = base.transform.Find("Image").gameObject.GetComponent<Image>();
	}

	public virtual void InitUI()
	{
	}
}
