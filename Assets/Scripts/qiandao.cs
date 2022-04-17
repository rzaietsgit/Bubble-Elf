using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class qiandao : MonoBehaviour
{
	public GameObject guan;

	public GameObject Dian;

	public Text signintext2;

	private bool isShow;

	private string nowTime = string.Empty;

	private void Start()
	{
		nowTime = Util.GetNowTime_Day();
		BaseUIAnimation.action.SetLanguageFont("signintext2", signintext2, string.Empty);
		Dian.SetActive(value: false);
	}

	private void Update()
	{
		if (Singleton<DataManager>.Instance.bGooglePay)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GGQianDaoBooL" + nowTime);
			if ((bool)MapUI.action)
			{
				if (!isShow && @int == 0)
				{
					guan.SetActive(value: true);
					Dian.SetActive(value: true);
					isShow = true;
					ButtonPingpong(base.gameObject);
				}
				else if (isShow && @int == 1)
				{
					guan.SetActive(value: false);
					Dian.SetActive(value: false);
					isShow = false;
				}
			}
		}
		else if ((bool)MapUI.action)
		{
			if (!isShow && MapUI.action.isCanQiandao)
			{
				guan.SetActive(value: true);
				Dian.SetActive(value: true);
				isShow = true;
				ButtonPingpong(base.gameObject);
			}
			else if (isShow && !MapUI.action.isCanQiandao)
			{
				guan.SetActive(value: false);
				Dian.SetActive(value: false);
				isShow = false;
			}
		}
	}

	private void ButtonPingpong(GameObject obj)
	{
		if (isShow)
		{
			obj.transform.DOScale(new Vector3(0.94f, 1.06f, 1f), 1f).SetEase(Ease.InOutSine);
			obj.transform.DOScale(new Vector3(1.06f, 0.94f, 1f), 1.2f).SetEase(Ease.InOutSine).SetDelay(1f)
				.OnComplete(delegate
				{
					ButtonPingpong(obj);
				});
		}
	}
}
