using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GuideMinUI : BaseUI
{
	public static GuideMinUI action;

	public Text GuideMinRemark;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.GuideMinUI;
	}

	public override void OnStart()
	{
		action = this;
	}

	public IEnumerator Time5()
	{
		yield return new WaitForSeconds(5f);
		InitAndroid.action.GAEvent("NewGuideMin:5:" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
		yield return new WaitForSeconds(5f);
		InitAndroid.action.GAEvent("NewGuideMin:10:" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
		yield return new WaitForSeconds(5f);
		InitAndroid.action.GAEvent("NewGuideMin:15:" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
	}

	public void InitUI()
	{
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && Singleton<LevelManager>.Instance.iNowSelectLevelIndex >= 80000)
		{
			GameGuide.Instance.isCanShoot = true;
			CloseUI();
		}
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
