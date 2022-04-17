using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuyDiamondUI : BaseUI
{
	public static BuyDiamondUI action;

	public GameObject CloseBtn;

	public GameObject GemsButton;

	public GameObject PackageButton;

	public GameObject GemsBG;

	public GameObject PackageBG;

	public Sprite SelectSprite;

	public Sprite UnSelectSprite;

	public Text BuyDiamondTitle;

	public Text BuyDiamondTitle1;

	public Text BuyDiamondTitle2;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.BuyDiamondUI;
	}

	private void InitBuyGoldSonObj()
	{
		if (!InitGame.bChinaVersion)
		{
		}
	}

	public void GemsButtonCallback()
	{
		GemsBG.SetActive(value: true);
		PackageBG.SetActive(value: false);
		GemsButton.GetComponent<Image>().sprite = SelectSprite;
		PackageButton.GetComponent<Image>().sprite = UnSelectSprite;
	}

	public void PackageButtonCallback()
	{
		GemsBG.SetActive(value: false);
		PackageBG.SetActive(value: true);
		GemsButton.GetComponent<Image>().sprite = UnSelectSprite;
		PackageButton.GetComponent<Image>().sprite = SelectSprite;
	}

	public void CloseBuyDiamondUI(bool bClickClose = true)
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

	private IEnumerator CallCloseUI()
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseUI();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject gameObject = Util.TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject == null)
			{
				CloseBuyDiamondUI(bClickClose: false);
			}
			else if (gameObject.name.LastIndexOf("BuyDiamondUI") < 0)
			{
				CloseBuyDiamondUI(bClickClose: false);
			}
		}
	}

	public override void OnStart()
	{
		action = this;
		InitBuyGoldSonObj();
		BaseUIAnimation.action.SetLanguageFont("BuyGoldTitle", BuyDiamondTitle, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BuyGoldTitle1", BuyDiamondTitle1, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BuyGoldTitle2", BuyDiamondTitle2, string.Empty);
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
