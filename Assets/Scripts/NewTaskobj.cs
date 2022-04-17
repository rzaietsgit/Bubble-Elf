using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewTaskobj : MonoBehaviour
{
	public Sprite[] LIconSp;

	public Sprite[] LBtnSp;

	public GameObject IconImage;

	public GameObject BtnImage;

	public Text RemarkText;

	public Text BtnText;

	public Image overImg;

	private int iMyTaskID;

	private int iMyLevelID;

	private bool bReward;

	private bool bOpen;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void InitData(int TaskID, string remark, int levelID)
	{
		overImg.gameObject.SetActive(value: false);
		iMyTaskID = TaskID;
		iMyLevelID = levelID;
		RemarkText.text = remark;
		if (levelID == 60)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_InitFistLoginGameDay");
			int nowTime = Util.GetNowTime();
			nowTime -= @int;
			if (nowTime <= 259200)
			{
				IconImage.GetComponent<Image>().sprite = LIconSp[1];
			}
		}
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
		if (int2 >= levelID)
		{
			int int3 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NewTaskReward" + TaskID);
			if (int3 == 1)
			{
				BtnImage.SetActive(value: false);
				overImg.gameObject.SetActive(value: true);
				return;
			}
			BtnImage.GetComponent<Image>().sprite = LBtnSp[1];
			bReward = true;
			if (NewTaskUI.action.iTaskCenter == 0)
			{
				NewTaskUI.action.iTaskCenter = TaskID;
			}
			return;
		}
		BtnText.text = "前 往";
		if (NewTaskUI.action.bbtnGo)
		{
			NewTaskUI.action.bbtnGo = false;
			bOpen = true;
			if (NewTaskUI.action.iTaskCenter == 0)
			{
				NewTaskUI.action.iTaskCenter = TaskID;
			}
		}
		else
		{
			BtnImage.GetComponent<Image>().sprite = LBtnSp[2];
		}
	}

	public void ClickGo()
	{
		if (bReward)
		{
			BtnImage.SetActive(value: false);
			overImg.gameObject.SetActive(value: true);
			bReward = false;
			RewardTask();
			OpenScript.action.Reshongdian();
		}
		if (bOpen)
		{
			Singleton<DataManager>.Instance.bNewTaskOpenPlay = true;
			NewTaskUI.action._CloseNewTaskUI();
		}
	}

	public void RewardTask()
	{
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NewTaskReward" + iMyTaskID, 1);
		string text = Singleton<DataManager>.Instance.dDataNewTaskList[iMyTaskID.ToString()]["Reward1"];
		string text2 = Singleton<DataManager>.Instance.dDataNewTaskList[iMyTaskID.ToString()]["Reward2"];
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		if (text2 != string.Empty && iMyLevelID == 60)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_InitFistLoginGameDay");
			int nowTime = Util.GetNowTime();
			nowTime -= @int;
			if (nowTime <= 259200)
			{
				for (int num = 0; num < text2.Split('|').Length; num++)
				{
					int num2 = int.Parse(text2.Split('|')[num].Split('-')[0]);
					int num3 = int.Parse(text2.Split('|')[num].Split('-')[1]);
					list.Add(num2);
					list2.Add(num3);
					ChinaPay.action.addRewardAll(num2, num3, NewTaskUI.action.gameObject, isShow: false, "free", "newtaskobj", iMyTaskID);
				}
			}
		}
		if (text != string.Empty)
		{
			for (int num4 = 0; num4 < text.Split('|').Length; num4++)
			{
				int num5 = int.Parse(text.Split('|')[num4].Split('-')[0]);
				int num6 = int.Parse(text.Split('|')[num4].Split('-')[1]);
				list.Add(num5);
				list2.Add(num6);
				ChinaPay.action.addRewardAll(num5, num6, NewTaskUI.action.gameObject, isShow: false, "free", "newtaskobj", iMyTaskID);
			}
		}
		BaseUIAnimation.action.ShowProp(list, list2, NewTaskUI.action.gameObject);
	}
}
