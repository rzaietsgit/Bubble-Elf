using System.Collections;
using UnityEngine;

public class EnFirstInvite : BaseUI
{
	public static EnFirstInvite action;

	public GameObject CloseBtn;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.EnFirstInvite;
	}

	private void Awake()
	{
	}

	public void Invite()
	{
		CloseEnFirstInvite();
		FaceBookApi.Action.Invite();
	}

	public void DoubleEnFirstInvite(bool bClickClose = true)
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

	public void CloseEnFirstInvite(bool bClickClose = true)
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
				CloseEnFirstInvite(bClickClose: false);
			}
			else if (gameObject.name.LastIndexOf("FirstInvite") < 0)
			{
				CloseEnFirstInvite(bClickClose: false);
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
