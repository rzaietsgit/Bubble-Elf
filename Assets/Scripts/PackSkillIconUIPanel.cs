using System.Collections.Generic;
using UnityEngine;

public class PackSkillIconUIPanel : PackSkillIconUIPanelBase
{
	public static PackSkillIconUIPanel panel;

	private List<BackPackSKILL> LBackPackSKILL;

	private List<PackSkillIcon> LPackSkillIcon;

	public override void InitUI()
	{
		panel = this;
		InitAndroid.action.GAEvent("Clickbeibao");
		LBackPackSKILL = new List<BackPackSKILL>();
		LPackSkillIcon = new List<PackSkillIcon>();
		LoadSkillData();
		ShowSkill();
		BaseUIAnimation.action.SetLanguageFont("BeibaoTitleUi", detail.TextDemo_Text, string.Empty);
	}

	private void LoadSkillData()
	{
		for (int i = 0; i <= 6; i++)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Skill_Count_" + i);
			if (@int > 0)
			{
				BackPackSKILL item = default(BackPackSKILL);
				item.bisNull = false;
				item.skillID = i + 3;
				item.iCount = @int;
				item.btime = false;
				string key = Singleton<DataManager>.Instance.dDataBuyDaojuRemark[item.skillID.ToString()]["remark"].ToString();
				item.remark = BaseUIAnimation.action.GetLanguage(key);
				key = Singleton<DataManager>.Instance.dDataBuyDaojuRemark[item.skillID.ToString()]["name"].ToString();
				item.remarkTitle = BaseUIAnimation.action.GetLanguage(key.ToString());
				LBackPackSKILL.Add(item);
			}
		}
	}

	public void ShowSkill()
	{
		int num = 0;
		for (int i = 0; i < LBackPackSKILL.Count; i++)
		{
			num++;
			ShowCreate(LBackPackSKILL[i]);
		}
		int num2 = num / 3;
		int num3 = num % 3;
		if (num == 1)
		{
			num3 = 1;
		}
		if (num == 2)
		{
			num3 = 2;
		}
		if (num3 != 0)
		{
			num3 = 3 - num3;
			num2++;
			BackPackSKILL backPackSKILL = default(BackPackSKILL);
			for (int j = 0; j < num3; j++)
			{
				backPackSKILL.bisNull = true;
				ShowCreate(backPackSKILL);
			}
		}
		if (num2 > 4)
		{
			RectTransform component = detail.group_GridLayoutGroup.gameObject.transform.GetComponent<RectTransform>();
			component.sizeDelta = new Vector2(469f, num2 * 165);
			RectTransform rectTransform = component;
			Vector3 localPosition = component.localPosition;
			float x = localPosition.x;
			Vector3 localPosition2 = component.localPosition;
			rectTransform.localPosition = new Vector3(x, -1000f, localPosition2.z);
		}
	}

	private void ShowCreate(BackPackSKILL _BackPackSKILL)
	{
		GameObject gameObject = Object.Instantiate(detail.PackSkillIcon_Image.gameObject);
		gameObject.transform.SetParent(detail.group_GridLayoutGroup.gameObject.transform, worldPositionStays: false);
		gameObject.SetActive(value: true);
		PackSkillIcon component = gameObject.GetComponent<PackSkillIcon>();
		component.InitSkill(_BackPackSKILL);
		LPackSkillIcon.Add(component);
	}

	public void ClearOtherRemark()
	{
		for (int i = 0; i < LPackSkillIcon.Count; i++)
		{
			LPackSkillIcon[i].HideRemark();
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ClearOtherRemark();
		}
	}
}
