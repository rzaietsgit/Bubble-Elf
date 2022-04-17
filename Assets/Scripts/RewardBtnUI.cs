using System.Collections;
using EasyMobile;
using ITSoft;
using UnityEngine;

public class RewardBtnUI : BaseUI
{
	public GameObject CloseBtn;

	public GameObject dian;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.RewardBtnUI;
	}

	private void Start()
	{
        AdsManager.ShowBanner();
        if (MapUI.action.isCanQiandao)
		{
			dian.SetActive(value: true);
		}
		else
		{
			dian.SetActive(value: false);
		}
	}

	public void _CloseRewardBtnUI()
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

	private void Update()
	{
	}

	public void ClickBtn1()
	{
		StartCoroutine(CallCloseUI());
		Singleton<DataManager>.Instance.NextOpenUI = EnumUIType.DayTaskUI;
	}

	public void ClickBtn2()
	{
		StartCoroutine(CallCloseUI());
		Singleton<DataManager>.Instance.NextOpenUI = EnumUIType.SignRewardUI;
	}

	public void ClickBtn3()
	{
		StartCoroutine(CallCloseUI());
		Singleton<DataManager>.Instance.NextOpenUI = EnumUIType.cdkeyUI;
	}
}
