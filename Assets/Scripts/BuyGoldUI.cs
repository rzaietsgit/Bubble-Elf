using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuyGoldUI : BaseUI
{
	public static BuyGoldUI action;

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
		return EnumUIType.BuyGoldUI;
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
		if ((bool)GemsBG)
		{
			GemsBG.SetActive(value: false);
		}
		if ((bool)PackageBG)
		{
			PackageBG.SetActive(value: true);
		}
		GemsButton.GetComponent<Image>().sprite = UnSelectSprite;
		PackageButton.GetComponent<Image>().sprite = SelectSprite;
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

	private IEnumerator CallCloseUI()
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseUI();
	}

	private void Update()
	{
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
		if ((bool)GameUI.action)
		{
			Canvas component = base.gameObject.transform.GetComponent<Canvas>();
			component.renderMode = RenderMode.ScreenSpaceCamera;
			component.worldCamera = GameUI.action.mainCameraS.GetComponent<Camera>();
		}
		else
		{
			Canvas component2 = base.gameObject.transform.GetComponent<Canvas>();
			component2.renderMode = RenderMode.ScreenSpaceCamera;
			component2.worldCamera = MapUI.action.MapUISceneCamera.GetComponent<Camera>();
		}
		base.OnAwake();
	}

	protected override void OnRelease()
	{
		base.OnRelease();
	}
}
