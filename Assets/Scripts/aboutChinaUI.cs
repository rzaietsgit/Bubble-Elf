using System.Collections;
using UnityEngine;

public class aboutChinaUI : BaseUI
{
	public GameObject CloseBtn;

	public GameObject yunbuObj;

	public GameObject dianxinObj;

	public GameObject qq;

	public static aboutChinaUI action;

	private string key = string.Empty;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.aboutChinaUI;
	}

	private void Start()
	{
		action = this;
		yunbuObj.SetActive(value: true);
		InitAndroid.action.CheckQQqun();
	}

	public void Showqq()
	{
		qq.gameObject.SetActive(value: true);
	}

	public void _CloseaboutChinaUI()
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
}
