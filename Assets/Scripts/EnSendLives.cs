using System.Collections;
using UnityEngine;

public class EnSendLives : BaseUI
{
	public static EnSendLives action;

	public GameObject CloseBtn;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.EnSendLives;
	}

	private void Awake()
	{
	}

	public void DoubleEnSendLives(bool bClickClose = true)
	{
		if (bClickClose)
		{
			if (!BaseUIAnimation.bClickButton)
			{
				return;
			}
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
		}
		StartCoroutine(CallCloseUI(bClickClose));
	}

	public void CloseEnSendLives(bool bClickClose = true)
	{
		if (bClickClose)
		{
			if (!BaseUIAnimation.bClickButton)
			{
				return;
			}
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
		}
		StartCoroutine(CallCloseUI());
	}

	private IEnumerator CallCloseUI(bool b = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseUI(b);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject gameObject = Util.TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject == null)
			{
				CloseEnSendLives(bClickClose: false);
			}
			else if (gameObject.name.LastIndexOf("SendLives") < 0)
			{
				CloseEnSendLives(bClickClose: false);
			}
		}
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
}
