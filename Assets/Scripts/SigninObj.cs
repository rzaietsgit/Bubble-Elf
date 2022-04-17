//using Umeng;
using UnityEngine;
using UnityEngine.UI;

public class SigninObj : MonoBehaviour
{
	public Sprite[] SigninStateObj;

	public Sprite[] SigninIconObj;

	public GameObject iconObj;

	public GameObject stateObj;

	public GameObject okObj;

	public GameObject maskObj;

	public Text daynum;

	public Text num;

	public bool receive;

	private int month;

	private int nowday;

	public void InitData(int day, int _month, int _nowday)
	{
		month = _month;
		nowday = _nowday;
		int num = int.Parse(Singleton<DataManager>.Instance.dDataSignin[day.ToString()]["Type"]);
		iconObj.GetComponent<Image>().sprite = SigninIconObj[num - 1];
		int num2 = int.Parse(Singleton<DataManager>.Instance.dDataSignin[day.ToString()]["Num"]);
		this.num.text = "x" + num2.ToString();
		daynum.text = "第" + day.ToString() + "天";
		if (day < nowday)
		{
			if (Singleton<DataManager>.Instance.GetUserDataI("DB_LQJL_" + month + "_" + day) == 0)
			{
				state3();
			}
			else
			{
				state1();
			}
		}
		else if (day == nowday)
		{
			if (Singleton<DataManager>.Instance.GetUserDataI("DB_LQJL_" + month + "_" + day) == 0)
			{
				state2();
			}
			else
			{
				state1();
			}
		}
		else
		{
			state4();
		}
	}

	public void Signin(int day = 0)
	{
		state1();
		int num = int.Parse(Singleton<DataManager>.Instance.dDataSignin[nowday.ToString()]["Type"]);
		int num2 = int.Parse(Singleton<DataManager>.Instance.dDataSignin[nowday.ToString()]["Num"]);
		if (day == 0)
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_LQJL_" + month + "_" + nowday, 1);
		}
		else
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_LQJL_" + month + "_" + day, 1);
			num = int.Parse(Singleton<DataManager>.Instance.dDataSignin[day.ToString()]["Type"]);
			num2 = int.Parse(Singleton<DataManager>.Instance.dDataSignin[day.ToString()]["Num"]);
		}
		//Analytics.Event("LogQiandao");
		SigninUI.action.RefreshUI();
	}

	public void addReward1(int num)
	{
	}

	public void addReward2(int num)
	{
	}

	public void addReward3(int num)
	{
	}

	public void addReward4(int num)
	{
	}

	public void addReward5(int num)
	{
	}

	public void addReward6(int num)
	{
	}

	public void addReward7(int num)
	{
	}

	public void state1()
	{
		receive = true;
		maskObj.SetActive(value: false);
		okObj.SetActive(value: true);
		stateObj.GetComponent<Image>().sprite = SigninStateObj[1];
	}

	private void state2()
	{
		maskObj.SetActive(value: false);
		stateObj.GetComponent<Image>().sprite = SigninStateObj[2];
		okObj.SetActive(value: false);
	}

	private void state3()
	{
		stateObj.GetComponent<Image>().sprite = SigninStateObj[0];
		okObj.SetActive(value: false);
	}

	private void state4()
	{
		maskObj.SetActive(value: false);
		stateObj.GetComponent<Image>().sprite = SigninStateObj[0];
		okObj.SetActive(value: false);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
