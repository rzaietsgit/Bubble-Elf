using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackSkillIconUI : BaseUI
{
	public static PackSkillIconUI action;

	public GameObject CloseBtn;

	public GameObject SkillObj;

	public GameObject GroupObj;

	public Text BeibaoTitleUi;

	private List<BackPackSKILL> LBackPackSKILL;

	private List<PackSkillIcon> LPackSkillIcon;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.PackSkillIconUI;
	}

	private void Start()
	{
		action = this;
		LBackPackSKILL = new List<BackPackSKILL>();
		LPackSkillIcon = new List<PackSkillIcon>();
		LoadSkillData();
		ShowSkill();
		BaseUIAnimation.action.SetLanguageFont("BeibaoTitleUi", BeibaoTitleUi, string.Empty);
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
			RectTransform component = GroupObj.transform.GetComponent<RectTransform>();
			component.sizeDelta = new Vector2(469f, num2 * 165);
			RectTransform rectTransform = component;
			Vector3 localPosition = component.localPosition;
			float x = localPosition.x;
			Vector3 localPosition2 = component.localPosition;
			rectTransform.localPosition = new Vector3(x, -1000f, localPosition2.z);
		}
	}

	public void ClearOtherRemark()
	{
		for (int i = 0; i < LPackSkillIcon.Count; i++)
		{
			LPackSkillIcon[i].HideRemark();
		}
	}

	private void ShowCreate(BackPackSKILL _BackPackSKILL)
	{
		GameObject gameObject = Object.Instantiate(SkillObj);
		gameObject.transform.SetParent(GroupObj.transform, worldPositionStays: false);
		gameObject.SetActive(value: true);
		PackSkillIcon component = gameObject.GetComponent<PackSkillIcon>();
		component.InitSkill(_BackPackSKILL);
		LPackSkillIcon.Add(component);
	}

	private void LoadSkillData()
	{
		for (int i = 1; i <= 6; i++)
		{
			string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_Skill_Count_Time_" + i, string.Empty);
			if (!(@string != string.Empty))
			{
				continue;
			}
			for (int num = 0; num < @string.Split(',').Length; num++)
			{
				string text = @string.Split(',')[num];
				if (text.Length > 3)
				{
					int num2 = int.Parse(text);
					num2 -= Util.GetNowTime();
					if (num2 > 60)
					{
						BackPackSKILL item = default(BackPackSKILL);
						item.bisNull = false;
						item.skillID = i + 3;
						item.btime = true;
						item.iTime = num2;
						item.remark = Singleton<DataManager>.Instance.dDataBuyDaojuRemark[item.skillID.ToString()]["remark"].ToString();
						item.remarkTitle = Singleton<DataManager>.Instance.dDataBuyDaojuRemark[item.skillID.ToString()]["name"].ToString();
						if (InitGame.bEnios)
						{
							item.remark = Singleton<DataManager>.Instance.dDataBuyDaojuRemark[item.skillID.ToString()]["remarken"].ToString();
							item.remarkTitle = Singleton<DataManager>.Instance.dDataBuyDaojuRemark[item.skillID.ToString()]["nameen"].ToString();
						}
						LBackPackSKILL.Add(item);
					}
				}
			}
		}
		for (int j = 1; j <= 6; j++)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Skill_Count_" + j);
			if (@int > 0)
			{
				BackPackSKILL item2 = default(BackPackSKILL);
				item2.bisNull = false;
				item2.skillID = j + 3;
				item2.iCount = @int;
				item2.btime = false;
				item2.remark = Singleton<DataManager>.Instance.dDataBuyDaojuRemark[item2.skillID.ToString()]["remark"].ToString();
				item2.remarkTitle = Singleton<DataManager>.Instance.dDataBuyDaojuRemark[item2.skillID.ToString()]["name"].ToString();
				if (InitGame.bEnios)
				{
					item2.remark = Singleton<DataManager>.Instance.dDataBuyDaojuRemark[item2.skillID.ToString()]["remarken"].ToString();
					item2.remarkTitle = Singleton<DataManager>.Instance.dDataBuyDaojuRemark[item2.skillID.ToString()]["nameen"].ToString();
				}
				LBackPackSKILL.Add(item2);
			}
		}
	}

	private void Awake()
	{
		Canvas component = base.gameObject.transform.GetComponent<Canvas>();
		component.renderMode = RenderMode.ScreenSpaceCamera;
		component.worldCamera = MapUI.action.MapUISceneCamera.GetComponent<Camera>();
	}

	public void _ClosePackSkillIconUI()
	{
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
			StartCoroutine(CallCloseUI());
		}
	}

	private IEnumerator CallCloseUI(bool bDouble = false, bool bExit = true)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseUI(bDouble);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ClearOtherRemark();
		}
	}
}
