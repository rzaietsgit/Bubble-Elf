using System.Collections;
using UnityEngine;

public class GuideMinUIPanel : GuideMinUIPanelBase
{
	public static GuideMinUIPanel panel;

	public override void InitUI()
	{
		panel = this;
		UnityEngine.Debug.Log("GuideMinUIPanel      InitUI");
		aliyunlog.OpenAndClickBtn("GuideMinUI", Singleton<LevelManager>.Instance.iNowSelectLevelIndex.ToString(), string.Empty);
		base.gameObject.transform.Find("mask").gameObject.SetActive(value: false);
		Singleton<DataManager>.Instance.bUiIsOpen = false;
		if ((bool)MapUI.action)
		{
			MapUI.action.ResInvitationObj();
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 5 && GameGuide.Instance.iGuideStep == 1)
		{
			BaseUIAnimation.action.SetLanguageFont("GuideMin1Remark" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex, detail.GuideMinRemark_Text, string.Empty);
		}
		else if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 5)
		{
			BaseUIAnimation.action.SetLanguageFont("GuideMin2Remark" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex, detail.GuideMinRemark_Text, string.Empty);
		}
		else if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex >= 80000)
		{
			detail.GuideMinRemark_Text.text = "����ţ�Ա������������ü��\u073f��Ի�������������ÿ3���غϻᵷ��һ��Ŷ�����\u07b6������ڣ����м��\u0738������ɣ�";
		}
		else
		{
			BaseUIAnimation.action.SetLanguageFont("GuideMin1Remark" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex, detail.GuideMinRemark_Text, string.Empty);
		}
		InitAndroid.action.GAEvent("NewGuideMin:0:" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
		StartCoroutine(Time5());
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

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && Singleton<LevelManager>.Instance.iNowSelectLevelIndex >= 80000)
		{
			GameGuide.Instance.isCanShoot = true;
			UI.Instance.ClosePanel();
		}
	}
}
