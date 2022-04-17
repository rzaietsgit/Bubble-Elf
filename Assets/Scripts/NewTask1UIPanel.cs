using System;
using System.Collections;
using System.Collections.Generic;
using EasyMobile;
using ITSoft;
using UnityEngine;
using UnityEngine.UI;

public class NewTask1UIPanel : NewTask1UIPanelBase
{
	public static NewTask1UIPanel panel;

	public GameObject Par_task_lightObj;

	private GameObject[] _Par_task_lightObjArr;

	public Sprite ImgNoSelect;

	public Sprite ImgSelect;

	private int iNowIndex_;

	private int iNowDay;

	private bool bReward1;

	private bool bReward2;

	private bool bnowOver;

	public override void InitUI()
	{
		panel = this;
		Singleton<DataManager>.Instance.bclickkuaisudianji = false;
        AdsManager.ShowBanner();
        DataManager.bbeibaoFlay = false;
		detail.Reward1Text_Text.gameObject.SetActive(value: false);
		detail.Reward2Text_Text.gameObject.SetActive(value: false);
		if (!Util.CheckOnline())
		{
			InitGame.Action.GetTimeNetTime();
			OnCloseButton();
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
				GameObject gameObject = detail.GameObject_Image.gameObject.transform.Find("Image" + i).gameObject.transform.Find("mask" + i).gameObject;
				gameObject.SetActive(value: false);
			}
			for (int j = num + 1; j <= 7; j++)
			{
				detail.GameObject_Image.gameObject.transform.Find("Image" + j).gameObject.GetComponent<Button>().enabled = false;
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
		detail.time_Text.text = text5;
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
		detail.time_Text.text = string.Empty;
	}

	public void GoTaskFor(int index)
	{
		if (index > iNowDay)
		{
			return;
		}
		if (iNowIndex_ != 0)
		{
			GameObject gameObject = detail.GameObject_Image.gameObject.transform.Find("Image" + iNowIndex_).gameObject;
			Text component = gameObject.transform.Find("day" + iNowIndex_).GetComponent<Text>();
			gameObject.GetComponent<Image>().sprite = ImgNoSelect;
			component.color = new Color(203f / 255f, 86f / 255f, 0.2f, 1f);
			component.GetComponent<Outline>().effectColor = new Color(1f, 1f, 1f, 1f);
		}
		iNowIndex_ = index;
		GameObject gameObject2 = detail.GameObject_Image.gameObject.transform.Find("Image" + iNowIndex_).gameObject;
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
		detail.Image14_Image.sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num, 138, 114);
		detail.Image15_Image.sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num2, 138, 114);
		detail.TextDemo5_Text.text = "x" + num3;
		detail.TextDemo6_Text.text = "x" + num4;
		detail.passText_Text.text = text.Replace("%s", string.Empty + text2);
		if (num5 > int.Parse(text2))
		{
			detail.passText_Text.text = text.Replace("%s", string.Empty + text2) + "(" + text2 + "/" + text2 + ")";
		}
		else
		{
			detail.passText_Text.text = text.Replace("%s", string.Empty + text2) + "(" + num5 + "/" + text2 + ")";
		}
		detail.passimg_Image.gameObject.SetActive(value: false);
		if (num6 >= 1f)
		{
			detail.passimg_Image.gameObject.SetActive(value: true);
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
		detail.Image21_Image.sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num7, 138, 114);
		detail.Image19_Image.sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num8, 138, 114);
		num6 = 0f;
		num6 = (float)num5 * 100f / ((float)int.Parse(text5) * 100f);
		detail.passText1_Text.text = text4.Replace("%s", string.Empty + text5);
		if (num5 > int.Parse(text5))
		{
			num5 = int.Parse(text5);
			detail.passText1_Text.text = text4.Replace("%s", string.Empty + text5) + "(" + text5 + "/" + text5 + ")";
		}
		else
		{
			detail.passText1_Text.text = text4.Replace("%s", string.Empty + text5) + "(" + num5 + "/" + text5 + ")";
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
			detail.TextDemo7_Text.text = string.Empty;
			bnowOver = true;
		}
		num6 = (float)num5 * 100f / ((float)int.Parse(text5) * 100f);
		if (num6 >= 1f)
		{
			bReward2 = true;
			detail.TextDemo7_Text.gameObject.SetActive(value: false);
			detail.passimg1_Image.gameObject.SetActive(value: true);
		}
		else
		{
			detail.passimg1_Image.gameObject.SetActive(value: false);
			bReward2 = false;
		}
		if (bnowOver)
		{
			detail.TextDemo9_Text.text = "x" + 1;
			detail.TextDemo8_Text.text = "x" + 1;
		}
		else
		{
			detail.TextDemo9_Text.text = "x" + num9;
			detail.TextDemo8_Text.text = "x" + num10;
		}
		detail.Reward1Text_Text.gameObject.SetActive(value: false);
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
			detail.Reward1_Image.gameObject.SetActive(value: false);
			detail.Reward1Ok_Image.gameObject.SetActive(value: true);
			detail.passimg_Image.gameObject.SetActive(value: false);
			UnityEngine.Debug.Log("hide 0 1");
			detail.Reward1Text_Text.gameObject.SetActive(value: false);
		}
		else
		{
			detail.Reward1_Image.gameObject.SetActive(value: true);
			detail.Reward1Ok_Image.gameObject.SetActive(value: false);
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
				_Par_task_lightObjArr[0].transform.SetParent(detail.Image14_Image.gameObject.transform.parent, worldPositionStays: false);
				_Par_task_lightObjArr[1] = UnityEngine.Object.Instantiate(Par_task_lightObj);
				_Par_task_lightObjArr[1].transform.SetParent(detail.Image15_Image.gameObject.transform.parent, worldPositionStays: false);
				detail.passimg_Image.gameObject.SetActive(value: true);
				detail.Reward1Text_Text.gameObject.SetActive(value: true);
			}
			else
			{
				detail.passimg_Image.gameObject.SetActive(value: false);
			}
		}
		int int4 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NewTask1_2" + iNowIndex_);
		if (int4 == 1)
		{
			detail.Reward2_Image.gameObject.SetActive(value: false);
			detail.Reward2Ok_Image.gameObject.SetActive(value: true);
			detail.passimg1_Image.gameObject.SetActive(value: false);
			detail.TextDemo7_Text.gameObject.SetActive(value: false);
			if ((bool)_Par_task_lightObjArr[2])
			{
				UnityEngine.Object.Destroy(_Par_task_lightObjArr[2]);
			}
			if ((bool)_Par_task_lightObjArr[3])
			{
				UnityEngine.Object.Destroy(_Par_task_lightObjArr[3]);
			}
			detail.Reward2Text_Text.gameObject.SetActive(value: false);
			return;
		}
		detail.Reward2_Image.gameObject.SetActive(value: true);
		detail.Reward2Ok_Image.gameObject.SetActive(value: false);
		detail.Reward2Text_Text.gameObject.SetActive(value: false);
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
			_Par_task_lightObjArr[2].transform.SetParent(detail.Image21_Image.transform.parent, worldPositionStays: false);
			_Par_task_lightObjArr[3] = UnityEngine.Object.Instantiate(Par_task_lightObj);
			_Par_task_lightObjArr[3].transform.SetParent(detail.Image19_Image.transform.parent, worldPositionStays: false);
			detail.Reward2Text_Text.gameObject.SetActive(value: true);
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
		detail.Reward2Text_Text.gameObject.SetActive(value: false);
	}

	private void SetDayText(int index)
	{
		switch (index)
		{
		case 1:
			detail.DayText_Text.text = "第一天";
			break;
		case 2:
			detail.DayText_Text.text = "第二天";
			break;
		case 3:
			detail.DayText_Text.text = "第三天";
			break;
		case 4:
			detail.DayText_Text.text = "第四天";
			break;
		case 5:
			detail.DayText_Text.text = "第五天";
			break;
		case 6:
			detail.DayText_Text.text = "第六天";
			break;
		case 7:
			detail.DayText_Text.text = "第七天";
			break;
		}
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
		detail.TextDemo7_Text.text = text4;
	}

	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			Singleton<DataManager>.Instance.bclicktow = false;
		}
	}

	public override void OnButton()
	{
		if (!Singleton<DataManager>.Instance.bclicktow)
		{
			Singleton<DataManager>.Instance.bclicktow = true;
			if (!Singleton<DataManager>.Instance.bclickkuaisudianji)
			{
				Singleton<DataManager>.Instance.bclickkuaisudianji = true;
				StartCoroutine(returnIEBtnClick());
				Reward1();
			}
		}
	}

	public override void OnButton1()
	{
		if (!Singleton<DataManager>.Instance.bclickkuaisudianji && !Singleton<DataManager>.Instance.bclicktow)
		{
			Singleton<DataManager>.Instance.bclicktow = true;
			Singleton<DataManager>.Instance.bclickkuaisudianji = true;
			Reward2();
			StartCoroutine(returnIEBtnClick());
		}
	}

	public IEnumerator returnIEBtnClick()
	{
		yield return new WaitForSeconds(2f);
		Singleton<DataManager>.Instance.bclickkuaisudianji = false;
	}

	public void Reward1()
	{
		if (bReward1 && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NewTask1_1" + iNowIndex_) == 0)
		{
			detail.Reward1_Image.gameObject.SetActive(value: false);
			detail.Reward1Ok_Image.gameObject.SetActive(value: true);
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
			InitAndroid.action.GAEvent("Reward:NewTask");
			detail.passimg_Image.gameObject.SetActive(value: false);
			detail.Reward1Text_Text.gameObject.SetActive(value: false);
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

	public override void OnImage26()
	{
		Reward1();
	}

	public override void OnImage13()
	{
		Reward1();
	}

	public override void OnImage18()
	{
		Reward2();
	}

	public override void OnImage20()
	{
		Reward2();
	}

	public void Reward2()
	{
		if (!bReward2 || Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NewTask1_2" + iNowIndex_) != 0)
		{
			return;
		}
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NewTask1_2" + iNowIndex_, 1);
		detail.Reward2_Image.gameObject.SetActive(value: false);
		detail.Reward2Ok_Image.gameObject.SetActive(value: true);
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
		//InitAndroid.action.GAEvent("RewardNewTask");
		detail.passimg1_Image.gameObject.SetActive(value: false);
		detail.TextDemo7_Text.gameObject.SetActive(value: false);
		detail.Reward2Text_Text.gameObject.SetActive(value: false);
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

	public override void OnImage1()
	{
		GoTaskFor(1);
	}

	public override void OnImage2()
	{
		GoTaskFor(2);
	}

	public override void OnImage3()
	{
		GoTaskFor(3);
	}

	public override void OnImage4()
	{
		GoTaskFor(4);
	}

	public override void OnImage5()
	{
		GoTaskFor(5);
	}

	public override void OnImage6()
	{
		GoTaskFor(6);
	}

	public override void OnImage7()
	{
		GoTaskFor(7);
	}
}
