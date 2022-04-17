using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ttdaypk : MonoBehaviour
{
	public GameObject fx_bglight;

	public GameObject Dian;

	public Text zhuanpantext2;

	public static ttdaypk action;

	private void Start()
	{
		if (InitGame.bChinaVersion)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		action = this;
		CheckOnline();
		CheckGuang();
		if ((bool)action && action.isActiveAndEnabled)
		{
			StartCoroutine(UpdateCheckGuang());
		}
		BaseUIAnimation.action.SetLanguageFont("tiaozhan1", zhuanpantext2, string.Empty);
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
		while ((bool)action && action.isActiveAndEnabled)
		{
			yield return new WaitForSeconds(0.1f);
			CheckGuang();
			yield return new WaitForSeconds(10f);
		}
	}

	public void CheckGuang()
	{
		if (Singleton<DataManager>.Instance.bGooglePay || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			if (Singleton<DataManager>.Instance.iDareCount >= 2)
			{
				if ((bool)action && action.isActiveAndEnabled)
				{
					fx_bglight.SetActive(value: false);
					Dian.SetActive(value: false);
					UnityEngine.Object.Destroy(this);
					base.gameObject.SetActive(value: false);
				}
			}
			else if ((bool)action && action.isActiveAndEnabled)
			{
				fx_bglight.SetActive(value: true);
				Dian.SetActive(value: true);
			}
		}
		else if ((bool)action && action.isActiveAndEnabled)
		{
			fx_bglight.SetActive(value: false);
			UnityEngine.Object.Destroy(this);
			base.gameObject.SetActive(value: false);
		}
	}

	private void Update()
	{
	}

	public void ClickOpenttdaypk()
	{
		if (!Util.GetbForced_guidance())
		{
			if (Singleton<DataManager>.Instance.iDareCount >= 2)
			{
				base.gameObject.SetActive(value: false);
			}
			else
			{
				Singleton<UIManager>.Instance.OpenUI(EnumUIType.DareUI);
			}
		}
	}
}
