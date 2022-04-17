using System;
using System.Collections;
using System.Collections.Generic;
using EasyMobile;
using ITSoft;
using UnityEngine;
using UnityEngine.UI;

public class NewTask1UI : BaseUI
{
	public static NewTask1UI action;

	public GameObject TaskDayF;

	public Sprite ImgNoSelect;

	public Sprite ImgSelect;

	public GameObject CloseBtn;

	public Text TimeText;

	public Image Task1Image1;

	public Image Task1Image2;

	public Text Task1Count1;

	public Text Task1Count2;

	public Text Task1Remark1;

	public GameObject Task1Over;

	public Image Task2Image1;

	public Image Task2Image2;

	public Text Task2Count1;

	public Text Task2Count2;

	public Text Task2Remark1;

	public Text Task2Time;

	public GameObject Reward1Obj;

	public GameObject Reward2Obj;

	public GameObject Reward1Ok;

	public GameObject Reward2Ok;

	public GameObject Reward1lqText;

	public GameObject Reward2lqText;

	public GameObject Task2Over;

	public Text DayText;

	public GameObject Par_task_lightObj;

	private int iNowDay;

	private int iNowIndex_;

	private GameObject[] _Par_task_lightObjArr;

	private bool bnowOver;

	private bool bReward1;

	private bool bReward2;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.NewTask1UI;
	}

	private void Start()
	{
		action = this;
        AdsManager.ShowBanner();
        DataManager.bbeibaoFlay = false;
		Reward1lqText.SetActive(value: false);
		Reward2lqText.SetActive(value: false);
		if (!Util.CheckOnline())
		{
			InitGame.Action.GetTimeNetTime();
			CloseUI();
			return;
		}
		_Par_task_lightObjArr = new GameObject[4];
		int nowTime = Util.GetNowTime();
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_InitFistLoginGameDay");
		nowTime -= @int;
		if (nowTime <= 864000)
		{
			nowTime = 864000 - nowTime;
			Settime(nowTime);
			StartCoroutine(IESetTIme(nowTime));
			int days = new TimeSpan(0, 0, nowTime).Days;
			int num = 10 - days;
			if (num >= 7)
			{
				num = 7;
			}
			if (num <= 1)
			{
				num = 1;
			}
			iNowDay = num;
			GoTaskFor(num);
			for (int i = 1; i <= num; i++)
			{
				GameObject gameObject = TaskDayF.transform.Find("Image" + i).gameObject.transform.Find("mask" + i).gameObject;
				gameObject.SetActive(value: false);
			}
			for (int j = num + 1; j <= 7; j++)
			{
				TaskDayF.transform.Find("Image" + j).gameObject.GetComponent<Button>().enabled = false;
			}
		}
	}

	public void Settime(int iTime)
	{
		TimeSpan timeSpan = new TimeSpan(0, 0, iTime);
		int minutes = timeSpan.Minutes;
		int hours = timeSpan.Hours;
		int seconds = timeSpan.Seconds;
		int days = timeSpan.Days;
		string text = minutes + string.Empty;
		string text2 = hours + string.Empty;
		string text3 = seconds + string.Empty;
		if (minutes < 10)
		{
			text = "0" + text;
		}
		if (hours < 10)
		{
			text2 = "0" + text2;
		}
		if (seconds < 10)
		{
			text3 = "0" + text3;
		}
		string text4 = days + "天" + text2 + "小时" + text + "分" + text3 + "秒";
		string text5 = string.Empty;
		for (int i = 0; i < text4.Length; i++)
		{
			text5 = text5 + text4.Substring(i, 1) + " ";
		}
		TimeText.text = text5;
	}

	private IEnumerator IESetTIme(int iTime)
	{
		yield return new WaitForSeconds(1f);
		while (iTime > 1)
		{
			iTime--;
			Settime(iTime);
			yield return new WaitForSeconds(1f);
		}
		TimeText.text = string.Empty;
	}

	private void Awake()
	{
		Canvas component = base.gameObject.transform.GetComponent<Canvas>();
		component.renderMode = RenderMode.ScreenSpaceCamera;
		component.worldCamera = MapUI.action.MapUISceneCamera.GetComponent<Camera>();
	}

	public void _CloseNewTaskUI()
	{
		if (!DataManager.bbeibaoFlay && BaseUIAnimation.bClickButton)
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
	}

	private void SetDayText(int index)
	{
		switch (index)
		{
		case 1:
			DayText.text = "第一天";
			break;
		case 2:
			DayText.text = "第二天";
			break;
		case 3:
			DayText.text = "第三天";
			break;
		case 4:
			DayText.text = "第四天";
			break;
		case 5:
			DayText.text = "第五天";
			break;
		case 6:
			DayText.text = "第六天";
			break;
		case 7:
			DayText.text = "第七天";
			break;
		}
	}

	public void GoTaskFor(int index)
	{
		if (index > iNowDay)
		{
			return;
		}
		if (iNowIndex_ != 0)
		{
			GameObject gameObject = TaskDayF.transform.Find("Image" + iNowIndex_).gameObject;
			Text component = gameObject.transform.Find("day" + iNowIndex_).GetComponent<Text>();
			gameObject.GetComponent<Image>().sprite = ImgNoSelect;
			component.color = new Color(203f / 255f, 86f / 255f, 0.2f, 1f);
			component.GetComponent<Outline>().effectColor = new Color(1f, 1f, 1f, 1f);
		}
		iNowIndex_ = index;
		GameObject gameObject2 = TaskDayF.transform.Find("Image" + iNowIndex_).gameObject;
		Text component2 = gameObject2.transform.Find("day" + iNowIndex_).GetComponent<Text>();
		SetDayText(index);
		gameObject2.GetComponent<Image>().sprite = ImgSelect;
		component2.color = new Color(1f, 1f, 1f, 1f);
		component2.GetComponent<Outline>().effectColor = new Color(0.647058845f, 6f / 85f, 41f / 85f, 1f);
		int num = int.Parse(Singleton<DataManager>.Instance.dDataTaskList1["task" + index]["Rid1"]);
		int num2 = int.Parse(Singleton<DataManager>.Instance.dDataTaskList1["task" + index]["Rid2"]);
		int num3 = int.Parse(Singleton<DataManager>.Instance.dDataTaskList1["task" + index]["Rcount1"]);
		int num4 = int.Parse(Singleton<DataManager>.Instance.dDataTaskList1["task" + index]["Rcount2"]);
		string text = Singleton<DataManager>.Instance.dDataTaskList1["task" + index]["taskremark"];
		string text2 = Singleton<DataManager>.Instance.dDataTaskList1["task" + index]["taskCount"];
		string text3 = Singleton<DataManager>.Instance.dDataTaskList1["task" + index]["Type"];
		int num5 = 0;
		float num6 = 0f;
		if (text3 == "PassLevel")
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
			num5 = @int;
		}
		else if (text3 == "UseGang" || text3 == "jingling" || text3 == "KillBubble" || text3 == "Skill4" || text3 == "Use_gbdj" || text3 == "Use_zsdj")
		{
			num5 = Singleton<UserManager>.Instance.GetTaskCount1(text3);
		}
		num6 = (float)num5 * 100f / ((float)int.Parse(text2) * 100f);
		Task1Image1.sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num, 138, 114);
		Task1Image2.sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num2, 138, 114);
		Task1Count1.text = "x" + num3;
		Task1Count2.text = "x" + num4;
		Task1Remark1.text = text.Replace("%s", string.Empty + text2);
		if (num5 > int.Parse(text2))
		{
			Task1Remark1.text = text.Replace("%s", string.Empty + text2) + "(" + text2 + "/" + text2 + ")";
		}
		else
		{
			Task1Remark1.text = text.Replace("%s", string.Empty + text2) + "(" + num5 + "/" + text2 + ")";
		}
		Task1Over.gameObject.SetActive(value: false);
		if (num6 >= 1f)
		{
			Task1Over.gameObject.SetActive(value: true);
			bReward1 = true;
		}
		else
		{
			bReward1 = false;
		}
		int num7 = int.Parse(Singleton<DataManager>.Instance.dDataTaskList1["taskadd" + index]["Rid1"]);
		int num8 = int.Parse(Singleton<DataManager>.Instance.dDataTaskList1["taskadd" + index]["Rid2"]);
		int num9 = int.Parse(Singleton<DataManager>.Instance.dDataTaskList1["taskadd" + index]["Rcount1"]);
		int num10 = int.Parse(Singleton<DataManager>.Instance.dDataTaskList1["taskadd" + index]["Rcount2"]);
		string text4 = Singleton<DataManager>.Instance.dDataTaskList1["taskadd" + index]["taskremark"];
		string text5 = Singleton<DataManager>.Instance.dDataTaskList1["taskadd" + index]["taskCount"];
		Task2Image1.sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num7, 138, 114);
		Task2Image2.sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num8, 138, 114);
		num6 = 0f;
		num6 = (float)num5 * 100f / ((float)int.Parse(text5) * 100f);
		Task2Remark1.text = text4.Replace("%s", string.Empty + text5);
		if (num5 > int.Parse(text5))
		{
			num5 = int.Parse(text5);
			Task2Remark1.text = text4.Replace("%s", string.Empty + text5) + "(" + text5 + "/" + text5 + ")";
		}
		else
		{
			Task2Remark1.text = text4.Replace("%s", string.Empty + text5) + "(" + num5 + "/" + text5 + ")";
		}
		int nowTime = Util.GetNowTime();
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_InitFistLoginGameDay");
		nowTime -= int2;
		if (nowTime <= index * 60 * 60 * 24)
		{
			nowTime = index * 60 * 60 * 24 - nowTime;
			Settime1(nowTime);
			bnowOver = false;
		}
		else
		{
			Task2Time.text = string.Empty;
			bnowOver = true;
		}
		num6 = (float)num5 * 100f / ((float)int.Parse(text5) * 100f);
		if (num6 >= 1f)
		{
			bReward2 = true;
			Task2Time.gameObject.SetActive(value: false);
			Task2Over.gameObject.SetActive(value: true);
		}
		else
		{
			Task2Over.gameObject.SetActive(value: false);
			bReward2 = false;
		}
		if (bnowOver)
		{
			Task2Count1.text = "x" + 1;
			Task2Count2.text = "x" + 1;
		}
		else
		{
			Task2Count1.text = "x" + num9;
			Task2Count2.text = "x" + num10;
		}
		Reward1lqText.SetActive(value: false);
		int int3 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NewTask1_1" + iNowIndex_);
		if ((bool)_Par_task_lightObjArr[0])
		{
			UnityEngine.Object.Destroy(_Par_task_lightObjArr[0]);
		}
		if ((bool)_Par_task_lightObjArr[1])
		{
			UnityEngine.Object.Destroy(_Par_task_lightObjArr[1]);
		}
		if (int3 == 1)
		{
			Reward1Obj.gameObject.SetActive(value: false);
			Reward1Ok.gameObject.SetActive(value: true);
			Task1Over.gameObject.SetActive(value: false);
			UnityEngine.Debug.Log("hide 0 1");
			Reward1lqText.SetActive(value: false);
		}
		else
		{
			Reward1Obj.gameObject.SetActive(value: true);
			Reward1Ok.gameObject.SetActive(value: false);
			if ((bool)_Par_task_lightObjArr[0])
			{
				UnityEngine.Object.Destroy(_Par_task_lightObjArr[0]);
			}
			if ((bool)_Par_task_lightObjArr[1])
			{
				UnityEngine.Object.Destroy(_Par_task_lightObjArr[1]);
			}
			if (bReward1)
			{
				_Par_task_lightObjArr[0] = UnityEngine.Object.Instantiate(Par_task_lightObj);
				_Par_task_lightObjArr[0].transform.SetParent(Task1Image1.transform.parent, worldPositionStays: false);
				_Par_task_lightObjArr[1] = UnityEngine.Object.Instantiate(Par_task_lightObj);
				_Par_task_lightObjArr[1].transform.SetParent(Task1Image2.transform.parent, worldPositionStays: false);
				Reward1lqText.SetActive(value: true);
			}
			else
			{
				UnityEngine.Debug.Log("hide 0 1");
				Reward1lqText.SetActive(value: false);
			}
		}
		int int4 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NewTask1_2" + iNowIndex_);
		if (int4 == 1)
		{
			Reward2Obj.gameObject.SetActive(value: false);
			Reward2Ok.gameObject.SetActive(value: true);
			Task2Over.gameObject.SetActive(value: false);
			Task2Time.gameObject.SetActive(value: false);
			UnityEngine.Debug.Log("hide 2 3");
			if ((bool)_Par_task_lightObjArr[2])
			{
				UnityEngine.Object.Destroy(_Par_task_lightObjArr[2]);
			}
			if ((bool)_Par_task_lightObjArr[3])
			{
				UnityEngine.Object.Destroy(_Par_task_lightObjArr[3]);
			}
			Reward2lqText.SetActive(value: false);
			return;
		}
		Reward2Obj.gameObject.SetActive(value: true);
		Reward2Ok.gameObject.SetActive(value: false);
		Reward2lqText.SetActive(value: false);
		if ((bool)_Par_task_lightObjArr[2])
		{
			UnityEngine.Object.Destroy(_Par_task_lightObjArr[2]);
		}
		if ((bool)_Par_task_lightObjArr[3])
		{
			UnityEngine.Object.Destroy(_Par_task_lightObjArr[3]);
		}
		if (bReward2)
		{
			_Par_task_lightObjArr[2] = UnityEngine.Object.Instantiate(Par_task_lightObj);
			_Par_task_lightObjArr[2].transform.SetParent(Task2Image1.transform.parent, worldPositionStays: false);
			_Par_task_lightObjArr[3] = UnityEngine.Object.Instantiate(Par_task_lightObj);
			_Par_task_lightObjArr[3].transform.SetParent(Task2Image2.transform.parent, worldPositionStays: false);
			Reward2lqText.SetActive(value: true);
			return;
		}
		if ((bool)_Par_task_lightObjArr[2])
		{
			UnityEngine.Object.Destroy(_Par_task_lightObjArr[2]);
		}
		if ((bool)_Par_task_lightObjArr[3])
		{
			UnityEngine.Object.Destroy(_Par_task_lightObjArr[3]);
		}
		Reward2lqText.SetActive(value: false);
	}

	public void Settime1(int iTime)
	{
		TimeSpan timeSpan = new TimeSpan(0, 0, iTime);
		int minutes = timeSpan.Minutes;
		int num = timeSpan.Hours;
		int seconds = timeSpan.Seconds;
		int days = timeSpan.Days;
		if (days > 0)
		{
			num = days * 24 + num;
		}
		string text = minutes + string.Empty;
		string text2 = num + string.Empty;
		string str = seconds + string.Empty;
		if (minutes < 10)
		{
			text = "0" + text;
		}
		if (num < 10)
		{
			text2 = "0" + text2;
		}
		if (seconds < 10)
		{
			str = "0" + str;
		}
		string text3 = text2 + "小时" + text + "分";
		string text4 = string.Empty;
		for (int i = 0; i < text3.Length; i++)
		{
			text4 = text4 + text3.Substring(i, 1) + " ";
		}
		Task2Time.text = text4;
	}

	public void Reward1()
	{
		if (bReward1 && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NewTask1_1" + iNowIndex_) == 0)
		{
			Reward1Obj.gameObject.SetActive(value: false);
			Reward1Ok.gameObject.SetActive(value: true);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NewTask1_1" + iNowIndex_, 1);
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			for (int i = 1; i <= 2; i++)
			{
				int num = int.Parse(Singleton<DataManager>.Instance.dDataTaskList1["task" + iNowIndex_]["Rid" + i]);
				int num2 = int.Parse(Singleton<DataManager>.Instance.dDataTaskList1["task" + iNowIndex_]["Rcount" + i]);
				list.Add(num);
				list2.Add(num2);
				ChinaPay.action.addRewardAll(num, num2, base.gameObject, isShow: false, "free", "newtask1");
			}
			BaseUIAnimation.action.ShowProp(list, list2, base.gameObject);
			Task1Over.gameObject.SetActive(value: false);
			Reward1lqText.SetActive(value: false);
			if ((bool)_Par_task_lightObjArr[0])
			{
				UnityEngine.Object.Destroy(_Par_task_lightObjArr[0]);
			}
			if ((bool)_Par_task_lightObjArr[1])
			{
				UnityEngine.Object.Destroy(_Par_task_lightObjArr[1]);
			}
			//AdManager.action.opadshowcp(DataManager.PAGE_MAIN);
		}
	}

	public void Reward2()
	{
		if (!bReward2 || Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NewTask1_2" + iNowIndex_) != 0)
		{
			return;
		}
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NewTask1_2" + iNowIndex_, 1);
		Reward2Obj.gameObject.SetActive(value: false);
		Reward2Ok.gameObject.SetActive(value: true);
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		for (int i = 1; i <= 2; i++)
		{
			int num = int.Parse(Singleton<DataManager>.Instance.dDataTaskList1["taskadd" + iNowIndex_]["Rid" + i]);
			int num2 = int.Parse(Singleton<DataManager>.Instance.dDataTaskList1["taskadd" + iNowIndex_]["Rcount" + i]);
			list.Add(num);
			if (bnowOver)
			{
				num2 = 1;
			}
			list2.Add(num2);
			ChinaPay.action.addRewardAll(num, num2, base.gameObject, isShow: false, "free", "newtask1");
		}
		BaseUIAnimation.action.ShowProp(list, list2, base.gameObject);
		Task2Over.gameObject.SetActive(value: false);
		Task2Time.gameObject.SetActive(value: false);
		Reward2lqText.SetActive(value: false);
		if ((bool)_Par_task_lightObjArr[2])
		{
			UnityEngine.Object.Destroy(_Par_task_lightObjArr[2]);
		}
		if ((bool)_Par_task_lightObjArr[3])
		{
			UnityEngine.Object.Destroy(_Par_task_lightObjArr[3]);
		}
		//AdManager.action.opadshowcp(DataManager.PAGE_MAIN);
	}
}
