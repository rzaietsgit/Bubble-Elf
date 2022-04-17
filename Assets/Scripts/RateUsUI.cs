using System.Collections;
using UnityEngine;

public class RateUsUI : BaseUI
{
	public static RateUsUI action;

	public GameObject EnterBtn;

	public GameObject CloseBtn;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.RateUsUI;
	}

	public void CloseRateUsUI(bool bDouble = false)
	{
		StartCoroutine(CallCloseUI(bDouble));
	}

	public void _CloseRateUsUI()
	{
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
			StartCoroutine(CallCloseUI());
		}
	}

	public IEnumerator CallCloseUI(bool bDouble = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseUI(bDouble);
	}

	private void Update()
	{
	}

	public override void OnStart()
	{
		action = this;
		Singleton<DataManager>.Instance.bRateUsUIOpen = true;
		BaseUIAnimation.action.CreateButton(EnterBtn.gameObject);
	}

	protected override void OnAwake()
	{
		base.OnAwake();
	}

	protected override void OnRelease()
	{
		base.OnRelease();
	}

	public void GoURL()
	{
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.bubbleshooter.shooting.balls.free");
		Singleton<DataManager>.Instance.SaveUserDate("DB_Google_Score", 1);
		FaceBookApi.Action.LogRatedEvent(string.Empty, string.Empty, 5, 5.0);
		CloseUI();
	}
}
