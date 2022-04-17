using UnityEngine;

public class ClickDayTask : MonoBehaviour
{
	public static ClickDayTask action;

	private void Start()
	{
		action = this;
		CheckOnline();
	}

	public void CheckOnline()
	{
		if (!Util.CheckOnline())
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Update()
	{
	}

	public void ClickOpenZhuanpan()
	{
		Singleton<UIManager>.Instance.OpenUI(EnumUIType.DayTaskUI);
	}
}
