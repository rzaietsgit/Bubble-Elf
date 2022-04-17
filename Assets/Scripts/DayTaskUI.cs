using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTaskUI : BaseUI
{
	public static DayTaskUI action;

	public GameObject TaskObj;

	public GameObject TaskFather;

	public GameObject CloseBtn;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.DayTaskUI;
	}

	public void _CloseDayTaskUI()
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

	private void Awake()
	{
		Canvas component = base.gameObject.transform.GetComponent<Canvas>();
		component.renderMode = RenderMode.ScreenSpaceCamera;
		component.worldCamera = MapUI.action.MapUISceneCamera.GetComponent<Camera>();
	}

	private void Start()
	{
		action = this;
		if ((bool)ClickDayTask.action)
		{
			ClickDayTask.action.CheckOnline();
		}
		CreateDayTask();
		for (int i = 1; i <= 5; i++)
		{
			GameObject gameObject = Object.Instantiate(TaskObj);
			gameObject.transform.SetParent(TaskFather.transform, worldPositionStays: false);
			TaskObj component = gameObject.GetComponent<TaskObj>();
			component.InitTaskData(i);
		}
	}

	public void CreateDayTask()
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		List<int> list3 = new List<int>();
		string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_TaskList" + Util.getInterNetTime(), string.Empty);
		if (!(@string == string.Empty) && @string.Length >= 2)
		{
			return;
		}
		for (int i = 1; i <= Singleton<DataManager>.Instance.dDataTaskList.Count; i++)
		{
			int num = int.Parse(Singleton<DataManager>.Instance.dDataTaskList["task" + i]["level"]);
			if (num == 10)
			{
				list.Add(i);
			}
			if (num == 1)
			{
				list2.Add(i);
			}
		}
		int index = Random.Range(0, list.Count);
		int index2 = Random.Range(0, list2.Count);
		list3.Add(list[index]);
		list3.Add(list2[index2]);
		for (int j = 1; j <= 100000; j++)
		{
			if (list3.Count >= 5)
			{
				break;
			}
			int num2 = Random.Range(0, Singleton<DataManager>.Instance.dDataTaskList.Count + 1);
			bool flag = true;
			for (int k = 0; k < list3.Count; k++)
			{
				if (num2 == list3[k])
				{
					flag = false;
					break;
				}
				int num3 = int.Parse(Singleton<DataManager>.Instance.dDataTaskList["task" + list3[k]]["ID"]);
				int num4 = int.Parse(Singleton<DataManager>.Instance.dDataTaskList["task" + num2]["ID"]);
				if (num3 == num4)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				list3.Add(num2);
			}
		}
		string text = string.Empty;
		for (int l = 0; l < list3.Count; l++)
		{
			text = text + list3[l] + ",";
		}
		Singleton<DataManager>.Instance.SaveUserDate("DB_TaskList" + Util.getInterNetTime(), text);
	}

	private void Update()
	{
	}
}
