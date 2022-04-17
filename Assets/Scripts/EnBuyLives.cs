using System.Collections;
using UnityEngine;

public class EnBuyLives : BaseUI
{
	public static EnBuyLives action;

	public GameObject CloseBtn;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.EnBuyLives;
	}

	private void Awake()
	{
	}

	public void BuyLives()
	{
		Singleton<UIManager>.Instance.OtherOpenUI = EnumUIType.ChinaShopUI;
		Singleton<DataManager>.Instance.bBuyLiveSale = false;
		Singleton<DataManager>.Instance.ChinaShopOpendaoju = false;
		Singleton<DataManager>.Instance.ChinaShopOpen = false;
		CloseUI(bDouble: false, bOpenOther: true);
	}

	public void DoubleEnBuyLives(bool bClickClose = true)
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

	public void CloseEnBuyLives(bool bClickClose = true)
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
		Singleton<DataManager>.Instance.bBuyLiveSale = true;
		CloseUI(b);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject gameObject = Util.TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject == null)
			{
				CloseEnBuyLives(bClickClose: false);
			}
			else if (gameObject.name.LastIndexOf("BuyLives") < 0)
			{
				CloseEnBuyLives(bClickClose: false);
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
