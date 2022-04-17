using UnityEngine;
using UnityEngine.UI;

public class yiyuanlibao : MonoBehaviour
{
	public static yiyuanlibao action;

	public Sprite levelsales_icon;

	public Sprite levelsales_icon1;

	private int iBuyCount;

	private void Start()
	{
		action = this;
		ResUIBtn();
	}

	public void ResUIBtn()
	{
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_XINSHOULIBAO") > 0)
		{
			if (DataManager.ChannelId == "dianxin" || DataManager.ChannelId == "xiaowo")
			{
				base.gameObject.SetActive(value: false);
				UnityEngine.Object.Destroy(this);
			}
			else if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "PAYBubble_LB5") > 0)
			{
				GetComponent<Image>().sprite = levelsales_icon1;
			}
			else
			{
				GetComponent<Image>().sprite = levelsales_icon;
			}
		}
	}

	private void Update()
	{
	}

	public void ClickYiyuanlibao()
	{
		if (Util.GetbForced_guidance())
		{
			return;
		}
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_XINSHOULIBAO") > 0)
		{
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "PAYBubble_LB5") > 0)
			{
				DataManager.sale_adKey = "Bubble_LB8";
				UI.Instance.OpenPanel(UIPanelType.SaleAdUI);
			}
			else
			{
				DataManager.sale_adKey = "Bubble_LB5";
				UI.Instance.OpenPanel(UIPanelType.SaleAdUI);
			}
		}
		else
		{
			DataManager.sale_adKey = "Bubble_LB8";
			UI.Instance.OpenPanel(UIPanelType.SaleAdUI);
		}
	}
}
