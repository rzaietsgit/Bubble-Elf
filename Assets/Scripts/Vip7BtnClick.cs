using UnityEngine;

public class Vip7BtnClick : MonoBehaviour
{
	public static Vip7BtnClick action;

	private void Start()
	{
		action = this;
	}

	private void Update()
	{
		if (InitGame.bVip7)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		if (!InitGame.bEnios)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void ClickOpenVip()
	{
		Singleton<UIManager>.Instance.OpenUI(EnumUIType.Vip7UI);
	}
}
