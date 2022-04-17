using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class xinshou : MonoBehaviour
{
	public GameObject guan;

	public GameObject dian;

	public static xinshou action;

	public Sprite levelsales_icon;

	public Sprite levelsales_icon1;

	private void Start()
	{
		action = this;
		ResUIBtn();
	}

	public void ClickNovicepacksUI()
	{
		if (!Util.GetbForced_guidance() && !Singleton<DataManager>.Instance.bUiIsOpen && BaseUIAnimation.bClickButton)
		{
			StartCoroutine(CallNovicepacksUI());
		}
	}

	private IEnumerator CallNovicepacksUI()
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		int i24hReward = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_24hReward");
		if (i24hReward == 1)
		{
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "PAYBubble_LB6") > 0)
			{
				DataManager.sale_adKey = "Bubble_LB3";
				UI.Instance.OpenPanel(UIPanelType.SaleAdUI);
			}
			else
			{
				DataManager.sale_adKey = "Bubble_LB6";
				UI.Instance.OpenPanel(UIPanelType.SaleAdUI);
			}
		}
		else
		{
			UI.Instance.OpenPanel(UIPanelType.Reward24UI);
		}
	}

	public void ResUIBtn()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_24hReward");
		if (@int == 1)
		{
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "PAYBubble_LB6") > 0)
			{
				GetComponent<Image>().sprite = levelsales_icon1;
				return;
			}
			GetComponent<Image>().sprite = levelsales_icon;
			if (DataManager.ChannelId == "dianxin" || DataManager.ChannelId == "xiaowo")
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		else if ((bool)guan)
		{
			guan.SetActive(value: true);
			if (!InitGame.bEnios)
			{
				StartCoroutine(UpdateDIan());
			}
		}
	}

	private IEnumerator UpdateDIan()
	{
		bool b = true;
		while (b)
		{
			yield return new WaitForSeconds(60f);
			int i24hTimeDB = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_24hRewardTime");
			int i24hTime = 86400;
			int iNowTime = Util.GetNowTime();
			if (iNowTime - i24hTimeDB > i24hTime && (bool)dian)
			{
				dian.SetActive(value: true);
				b = false;
			}
		}
	}

	private void Update()
	{
	}
}
