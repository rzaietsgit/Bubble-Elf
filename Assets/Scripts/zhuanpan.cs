using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class zhuanpan : MonoBehaviour
{
	public GameObject fx_bglight;

	public GameObject Dian;

	public Text zhuanpantext2;

	public static zhuanpan action;

	private void Start()
	{
		action = this;
		CheckOnline();
		CheckGuang();
		StartCoroutine(UpdateCheckGuang());
		BaseUIAnimation.action.SetLanguageFont("zhuanpantext2", zhuanpantext2, string.Empty);
	}

	public void CheckOnline()
	{
		if (!Util.CheckOnline())
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private IEnumerator UpdateCheckGuang()
	{
		while (true)
		{
			yield return new WaitForSeconds(10f);
			CheckGuang();
		}
	}

	public void CheckGuang()
	{
		if (Singleton<DataManager>.Instance.bGooglePay)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LvyeZhuanpan");
			if (@int > 0)
			{
				fx_bglight.SetActive(value: true);
				Dian.SetActive(value: true);
			}
			else
			{
				fx_bglight.SetActive(value: false);
				Dian.SetActive(value: false);
			}
		}
		else
		{
			fx_bglight.SetActive(value: false);
		}
	}

	private void Update()
	{
	}

	public void ClickOpenZhuanpan()
	{
		if (!Util.GetbForced_guidance() && !Singleton<DataManager>.Instance.bGrilMoveing)
		{
			UI.Instance.OpenPanel(UIPanelType.LotteryUI);
		}
	}
}
