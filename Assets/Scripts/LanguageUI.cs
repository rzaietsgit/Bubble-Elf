using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LanguageUI : BaseUI
{
	public static LanguageUI action;

	public GameObject CloseBtn;

	public GameObject bg2Father;

	public GameObject LanguageObj;

	public Text SetUILanguage;

	public bool bDown;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.LanguageUI;
	}

	private void Start()
	{
		action = this;
		BaseUIAnimation.action.SetLanguageFont("SetUILanguage", SetUILanguage, string.Empty);
		LoadBtn();
		if ((bool)SetPanelUI.action)
		{
			SetPanelUI.action.ResBtn();
		}
	}

	private void LoadBtn()
	{
		for (int i = 0; i < 10; i++)
		{
			GameObject gameObject = Object.Instantiate(LanguageObj);
			gameObject.transform.parent = bg2Father.transform;
			LanguageSelectUI component = gameObject.GetComponent<LanguageSelectUI>();
			component.SetType(i);
		}
	}

	public void _CloseLoseUI()
	{
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
			StartCoroutine(CallCloseUI());
		}
	}

	private IEnumerator CallCloseUI(bool bDouble = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseUI(bDouble);
	}

	public void CloseLoseUI(bool bDouble = false)
	{
		StartCoroutine(CallCloseUI(bDouble));
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject gameObject = Util.TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject == null)
			{
				CloseLoseUI();
			}
			else if (gameObject.name.LastIndexOf("LanguageUI") < 0)
			{
				CloseLoseUI();
			}
		}
	}
}
