//using Umeng;
using UnityEngine;
using UnityEngine.UI;

public class TaskObj : MonoBehaviour
{
	public GameObject TaskIcon;

	public Text CountText;

	public Text TaskTitle;

	public Text Remark;

	public Image PassLine;

	public Text ingText;

	public Text btnText;

	private int iSumCount;

	private int iCount;

	private string TaskID = "0";

	private bool bover;

	private void Start()
	{
	}

	public void InitTaskData(int index)
	{
		string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_TaskList" + Util.getInterNetTime(), string.Empty);
		string str = TaskID = @string.Split(',')[index - 1];
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_TaskOver" + TaskID + Util.getInterNetTime());
		if (@int > 0)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		base.gameObject.SetActive(value: true);
		int num = iSumCount = int.Parse(Singleton<DataManager>.Instance.dDataTaskList["task" + str]["taskCount"]);
		string text = Singleton<DataManager>.Instance.dDataTaskList["task" + str]["taskremark"];
		string sType = Singleton<DataManager>.Instance.dDataTaskList["task" + str]["Type"];
		string text2 = Singleton<DataManager>.Instance.dDataTaskList["task" + str]["name"];
		text = text.Replace("%s", string.Empty + num);
		TaskTitle.text = text2;
		Remark.text = text;
		iCount = Singleton<UserManager>.Instance.GetTaskCount(sType);
		ingText.text = iCount + "/" + iSumCount;
		float num2 = 0.001f;
		num2 = (float)iCount * 100f / (float)iSumCount * 100f;
		num2 /= 10000f;
		PassLine.GetComponent<Image>().fillAmount = num2;
		if (iCount >= iSumCount)
		{
			bover = true;
			btnText.text = "领取";
		}
		int num3 = int.Parse(Singleton<DataManager>.Instance.dDataTaskList["task" + TaskID]["Rcount"]);
		int num4 = int.Parse(Singleton<DataManager>.Instance.dDataTaskList["task" + TaskID]["Rid"]);
		TaskIcon.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num4, 138, 114);
		CountText.text = "x" + num3;
	}

	public void ClickLink()
	{
		if (bover)
		{
			int num = int.Parse(Singleton<DataManager>.Instance.dDataTaskList["task" + TaskID]["Rcount"]);
			int index = int.Parse(Singleton<DataManager>.Instance.dDataTaskList["task" + TaskID]["Rid"]);
			ChinaPay.action.addRewardAll(index, num, DayTaskUI.action.gameObject, isShow: true, "free", "taskobj");
			//Analytics.Event("TaskData", "RewardTask_" + TaskID);
			Singleton<DataManager>.Instance.SaveUserDate("DB_TaskOver" + TaskID + Util.getInterNetTime(), 1);
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		string a = Singleton<DataManager>.Instance.dDataTaskList["task" + TaskID]["link"];
		if (a == "OpenUI")
		{
			Singleton<UIManager>.Instance.OtherOpenUI = EnumUIType.PlayUI;
			DayTaskUI.action.CloseUI(bDouble: false, bOpenOther: true);
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
			Singleton<LevelManager>.Instance.iNowSelectLevelIndex = @int + 1;
		}
		if (a == "Shop2")
		{
			Singleton<UIManager>.Instance.OtherOpenUI = EnumUIType.ChinaShopUI;
			DayTaskUI.action.CloseUI(bDouble: false, bOpenOther: true);
			Singleton<DataManager>.Instance.ChinaShopOpendaoju = true;
			Singleton<DataManager>.Instance.ChinaShopOpenZuanshi = false;
			UI.Instance.OpenPanel(UIPanelType.ChinaShop);
		}
	}

	private void Update()
	{
	}
}
