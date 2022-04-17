using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SigninReceive : MonoBehaviour
{
	public Sprite receiveBtnenabled;

	public Sprite receiveBtnOK;

	public GameObject light;

	private GameObject lightObj;

	private int _index;

	private int _month;

	public void Activation(int index, int month)
	{
		_month = month;
		_index = index;
		if (Singleton<DataManager>.Instance.GetUserDataI("SigninReceive" + index + "_" + month) != 0)
		{
			base.gameObject.GetComponent<Button>().enabled = false;
			base.gameObject.GetComponent<Image>().sprite = receiveBtnOK;
			base.gameObject.transform.DOKill(complete: true);
			base.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			if ((bool)lightObj)
			{
				Object.DestroyObject(lightObj.gameObject);
			}
		}
		else
		{
			if (!lightObj)
			{
				lightObj = UnityEngine.Object.Instantiate(light);
				lightObj.transform.parent = base.gameObject.transform.parent;
				lightObj.transform.localPosition = base.gameObject.transform.localPosition;
			}
			base.gameObject.GetComponent<Button>().enabled = true;
			base.gameObject.transform.DOScale(1.2f, 0.8f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
		}
	}

	public void Unavailable(int index, int month)
	{
		base.gameObject.transform.DOKill(complete: true);
		base.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		if ((bool)lightObj)
		{
			Object.DestroyObject(lightObj.gameObject);
		}
		_month = month;
		_index = index;
		base.gameObject.GetComponent<Button>().enabled = false;
		base.gameObject.GetComponent<Image>().sprite = receiveBtnenabled;
	}

	public void Receive()
	{
		Singleton<DataManager>.Instance.SaveUserDate("SigninReceive" + _index + "_" + _month, 1);
		base.gameObject.GetComponent<Button>().enabled = false;
		base.gameObject.GetComponent<Image>().sprite = receiveBtnOK;
		SigninUI.action.RefreshUI();
		string key = string.Empty;
		string key2 = string.Empty;
		if (_index == 1)
		{
			key = "Type3";
			key2 = "Num3";
		}
		else if (_index == 2)
		{
			key = "Type7";
			key2 = "Num7";
		}
		else if (_index == 3)
		{
			key = "Type14";
			key2 = "Num14";
		}
		else if (_index == 4)
		{
			key = "Type23";
			key2 = "Num23";
		}
		string text = Singleton<DataManager>.Instance.dDataSigninCount[_month.ToString()][key];
		string text2 = Singleton<DataManager>.Instance.dDataSigninCount[_month.ToString()][key2];
		string[] array = text.Split('|');
		string[] array2 = text2.Split('|');
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

	private void Start()
	{
	}

	private void Update()
	{
	}
}
