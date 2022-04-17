using System.Collections;
using TMPro;
using UnityEngine;

public class BuyGoldUIByDiamonds : BaseUI
{
	public static BuyGoldUIByDiamonds action;

	public TextMeshProUGUI BuyGoldUITitle;

	public GameObject BuyGoldSonObjByDiamonds;

	public GameObject BuyGoldSonFatherObj;

	public GameObject CloseBtn;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.BuyGoldUIByDiamonds;
	}

	private void InitBuyGoldSonObj()
	{
		BaseUIAnimation.action.SetLanguageFont("BuyGoldUIByDiamondsTitle", BuyGoldUITitle, string.Empty);
		for (int i = 1; i <= 5; i++)
		{
			GameObject gameObject = Object.Instantiate(BuyGoldSonObjByDiamonds);
			gameObject.transform.SetParent(BuyGoldSonFatherObj.transform, worldPositionStays: false);
			BuyGoldSonByDiamonds component = gameObject.GetComponent<BuyGoldSonByDiamonds>();
			component.InitForPayID(i.ToString());
		}
	}

	public void CloseBuyGoldUI(bool bClickClose = true)
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

	public void DoubleCloseBuyGoldUI(bool bClickClose = true)
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
				CloseBuyGoldUI(bClickClose: false);
			}
			else if (gameObject.name.LastIndexOf("BuyGoldUIByDiamonds") < 0)
			{
				CloseBuyGoldUI(bClickClose: false);
			}
		}
	}

	public override void OnStart()
	{
		action = this;
		InitBuyGoldSonObj();
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
