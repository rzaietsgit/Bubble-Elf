using System.Collections;
using UnityEngine;

public class EnConnectFacebook : BaseUI
{
	public static EnConnectFacebook action;

	public GameObject CloseBtn;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.EnConnectFacebook;
	}

	private void Awake()
	{
	}

	public void FackBookLogin()
	{
		FaceBookApi.Action.FackBookLogin();
	}

	public void DoubleEnConnectFacebook(bool bClickClose = true)
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

	public void CloseEnConnectFacebook(bool bClickClose = true)
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
				CloseEnConnectFacebook(bClickClose: false);
			}
			else if (gameObject.name.LastIndexOf("ConnectFacebook") < 0)
			{
				CloseEnConnectFacebook(bClickClose: false);
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
