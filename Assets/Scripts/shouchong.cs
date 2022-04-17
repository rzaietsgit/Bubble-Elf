using DG.Tweening;
using UnityEngine;

public class shouchong : MonoBehaviour
{
	public GameObject guan;

	private bool isShow;

	private void Start()
	{
	}

	private void Update()
	{
		if ((bool)MapUI.action)
		{
			if (!isShow && MapUI.action.isCanShouchong)
			{
				guan.SetActive(value: true);
				isShow = true;
				ButtonPingpong(base.gameObject);
			}
			else if (isShow && !MapUI.action.isCanShouchong)
			{
				guan.SetActive(value: false);
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
