using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HuaShowUI : BaseUI
{
	public static HuaShowUI action;

	public GameObject EnterBtn;

	public Text TimeText;

	public GameObject CloseBtn;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.HuaShowUI;
	}

	public void CloseDareUI(bool bDouble = false)
	{
		StartCoroutine(CallCloseUI(bDouble));
	}

	public void _CloseDareUI()
	{
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
			StartCoroutine(CallCloseUI());
		}
	}

	public IEnumerator CallCloseUI(bool bDouble = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseUI(bDouble);
	}

	private void Update()
	{
	}

	public override void OnStart()
	{
		action = this;
	}

	protected override void OnAwake()
	{
		base.OnAwake();
	}

	protected override void OnRelease()
	{
		base.OnRelease();
	}

	public void ClickClose22222()
	{
		CloseUI();
	}

	public void ClickGoHua()
	{
		if (Util.CheckOnline() && Singleton<UserManager>.Instance.bOpenHua() > 0)
		{
			Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.HuaScene);
		}
	}
}
