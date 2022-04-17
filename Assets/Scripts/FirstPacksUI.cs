using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirstPacksUI : BaseUI
{
	public static FirstPacksUI action;

	public GameObject CloseBtn;

	public GameObject lingquObj;

	public TextMeshProUGUI lingquText;

	public TextMeshProUGUI FirstPacksTitle;

	public TextMeshProUGUI FirstPacksBtn;

	public bool isLingqu;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.FirstPacksUI;
	}

	private void Awake()
	{
		Canvas component = base.gameObject.transform.GetComponent<Canvas>();
		component.renderMode = RenderMode.ScreenSpaceCamera;
		component.worldCamera = MapUI.action.MapUISceneCamera.GetComponent<Camera>();
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_CHONGZHI") > 0)
		{
			isLingqu = true;
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SHOUCHONGLIBAO") == 0)
			{
				lingquText.text = "领取";
				return;
			}
			lingquText.text = "已领取";
			lingquObj.GetComponent<Button>().enabled = false;
		}
	}

	private void InitFirstPacksUI()
	{
		BaseUIAnimation.action.SetLanguageFont("FirstPacksTitle", FirstPacksTitle, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("FirstPacksBtn", FirstPacksBtn, string.Empty);
	}

	public void lingqu()
	{
	}

	public void DoubleFirstPacksUI(bool bClickClose = true)
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

	public void CloseFirstPacksUI(bool bClickClose = true)
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
			if (!(gameObject == null) && gameObject.name.LastIndexOf("FirstPacksUI") >= 0)
			{
			}
		}
	}

	public override void OnStart()
	{
		action = this;
		InitFirstPacksUI();
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
