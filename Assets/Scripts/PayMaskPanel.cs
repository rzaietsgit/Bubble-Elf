using UnityEngine;

public class PayMaskPanel : PayMaskPanelBase
{
	public static PayMaskPanel panel;

	public float speed = 150f;

	public override UIType GetUIType()
	{
		return UIType.STATIC;
	}

	public override void InitUI()
	{
		panel = this;
	}

	private void Update()
	{
		detail.Image1_Image.rectTransform.Rotate(new Vector3(0f, 0f, -1f) * speed * Time.deltaTime);
	}

	public override void OnPauseBase()
	{
	}

	public void CloseMask()
	{
		UI.Instance.ClosePanel();
	}
}
