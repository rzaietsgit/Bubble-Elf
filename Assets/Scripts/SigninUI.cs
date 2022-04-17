using System;
using System.Collections;
using System.Linq;
using EasyMobile;
using ITSoft;
using UnityEngine;
using UnityEngine.UI;

public class SigninUI : BaseUI
{
	public static SigninUI action;

	public GameObject CloseBtn;

	public GameObject SinginObj;

	public GameObject SinginParent;

	public GameObject Content;

	public GameObject SinginBtn;

	public GameObject replenishSigninBtn;

	public Sprite SinginBtnenabled;

	public GameObject receiveBtn1;

	public GameObject receiveBtn2;

	public GameObject receiveBtn3;

	public GameObject receiveBtn4;

	private GameObject nowdayObj;

	public int iReceiveNum;

	public Text ReceiveNum;

	public Text ReceiveMonth;

	private int month = 12;

	private int nowday = 21;

	public GameObject NoNetTip;

	private bool b = true;

	private bool bSin = true;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.SigninUI;
	}

	private void Awake()
	{
		Canvas component = base.gameObject.transform.GetComponent<Canvas>();
		component.renderMode = RenderMode.ScreenSpaceCamera;
		component.worldCamera = MapUI.action.MapUISceneCamera.GetComponent<Camera>();
		Singleton<DataManager>.Instance.SaveUserDate("DB_ToDayFirstLogin" + Util.GetNowTime_Day(), 1);
	}

	public override void OnStart()
	{
		action = this;
		if (!Util.CheckOnline())
		{
			CloseUI();
			return;
		}
        AdsManager.ShowBanner();
        InitBuyGoldSonObj();
		if (!Singleton<DataManager>.Instance.bAutoOpenSigninUI)
		{
			string interNetTime = Util.getInterNetTime();
			if (interNetTime != Util.GetNowTime_Day())
			{
				bSin = false;
				SinginBtn.GetComponent<Button>().enabled = false;
				SinginBtn.GetComponent<Image>().sprite = SinginBtnenabled;
			}
		}
	}

	private void InitBuyGoldSonObj()
	{
		month = DateTime.Now.Month;
		nowday = DateTime.Now.Day;
		iReceiveNum = 0;
		ReceiveMonth.text = month + "月签到";
		for (int i = 0; i < Singleton<DataManager>.Instance.dDataSignin.Keys.Count; i++)
		{
			string text = Singleton<DataManager>.Instance.dDataSignin.Keys.ToArray()[i];
			if (text.Length >= 1)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(SinginObj);
				gameObject.transform.SetParent(SinginParent.transform, worldPositionStays: false);
				gameObject.name = "_SigninObj" + text;
				SigninObj component = gameObject.GetComponent<SigninObj>();
				component.InitData(int.Parse(text), month, nowday);
				if (component.receive)
				{
					iReceiveNum++;
				}
			}
		}
		if (iReceiveNum > 20)
		{
			Content.transform.localPosition = new Vector3(0f, 666f, 0f);
		}
		else if (iReceiveNum > 12)
		{
			Content.transform.localPosition = new Vector3(0f, 420f, 0f);
		}
		RefreshUI();
	}

	private IEnumerator HideNoNetTip()
	{
		yield return new WaitForSeconds(0.01f);
		NoNetTip.SetActive(value: true);
		if (b)
		{
			b = false;
			yield return new WaitForSeconds(5f);
			NoNetTip.SetActive(value: false);
			b = true;
		}
	}

	public void RefreshUI()
	{
		ReceiveNum.text = iReceiveNum.ToString();
		ReceiveMonth.text = month + "月签到";
		if (iReceiveNum >= 23)
		{
			receiveBtn1.GetComponent<SigninReceive>().Activation(1, month);
			receiveBtn2.GetComponent<SigninReceive>().Activation(2, month);
			receiveBtn3.GetComponent<SigninReceive>().Activation(3, month);
			receiveBtn4.GetComponent<SigninReceive>().Activation(4, month);
		}
		else if (iReceiveNum >= 14)
		{
			receiveBtn1.GetComponent<SigninReceive>().Activation(1, month);
			receiveBtn2.GetComponent<SigninReceive>().Activation(2, month);
			receiveBtn3.GetComponent<SigninReceive>().Activation(3, month);
			receiveBtn4.GetComponent<SigninReceive>().Unavailable(4, month);
		}
		else if (iReceiveNum >= 7)
		{
			receiveBtn1.GetComponent<SigninReceive>().Activation(1, month);
			receiveBtn2.GetComponent<SigninReceive>().Activation(2, month);
			receiveBtn3.GetComponent<SigninReceive>().Unavailable(3, month);
			receiveBtn4.GetComponent<SigninReceive>().Unavailable(4, month);
		}
		else if (iReceiveNum >= 3)
		{
			receiveBtn1.GetComponent<SigninReceive>().Activation(1, month);
			receiveBtn2.GetComponent<SigninReceive>().Unavailable(2, month);
			receiveBtn3.GetComponent<SigninReceive>().Unavailable(3, month);
			receiveBtn4.GetComponent<SigninReceive>().Unavailable(4, month);
		}
		else
		{
			receiveBtn1.GetComponent<SigninReceive>().Unavailable(1, month);
			receiveBtn2.GetComponent<SigninReceive>().Unavailable(2, month);
			receiveBtn3.GetComponent<SigninReceive>().Unavailable(3, month);
			receiveBtn4.GetComponent<SigninReceive>().Unavailable(4, month);
		}
		if (!GetRefreshReplenish())
		{
			bSin = false;
			replenishSigninBtn.GetComponent<Button>().enabled = false;
			replenishSigninBtn.GetComponent<Image>().sprite = SinginBtnenabled;
			SinginBtn.GetComponent<Button>().enabled = false;
			SinginBtn.GetComponent<Image>().sprite = SinginBtnenabled;
		}
		if (Singleton<DataManager>.Instance.GetUserDataI("DB_YJQD_" + month + "_" + nowday) == 1)
		{
			bSin = false;
			SinginBtn.GetComponent<Button>().enabled = false;
			SinginBtn.GetComponent<Image>().sprite = SinginBtnenabled;
		}
	}

	public void SigninBtn()
	{
		if (!bSin)
		{
			return;
		}
		string interNetTime = Util.getInterNetTime();
		if (interNetTime.Length > 3)
		{
			if (interNetTime != Util.GetNowTime_Day())
			{
				StartCoroutine(HideNoNetTip());
				return;
			}
			Singleton<DataManager>.Instance.SaveUserDate("DB_YJQD_" + month + "_" + nowday, 1);
			Qiandao();
			bSin = false;
			MapUI.action.isCanQiandao = false;
			OpenSadeUI();
		}
	}

	public void OpenSadeUI()
	{
		if (!CheckDay())
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "PayDay" + Util.GetLast_Day());
			if (@int != 1)
			{
				Singleton<DataManager>.Instance.bSigninUICloseOpen = true;
				SigninUICloseOpenSale();
			}
		}
	}

	public void SigninUICloseOpenSale()
	{
		DataManager.sale_adKey = string.Empty;
		int num = 0;
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SigninUIClose");
		if ((@int == 3 || @int == 4 || DataManager.sale_adKey == string.Empty) && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_DoublePay") == 0)
		{
			DataManager.sale_adKey = "Pay2";
		}
		if (@int == 5 || @int == 6 || DataManager.sale_adKey == string.Empty)
		{
			DataManager.sale_adKey = "Bubble_LB3";
		}
		if (@int == 7 || @int == 8 || DataManager.sale_adKey == string.Empty)
		{
			DataManager.sale_adKey = "Bubble_LB2";
		}
		if (DataManager.ChannelId != "dianxin" && DataManager.ChannelId != "xiaowo")
		{
			if (@int == 9 || @int == 10 || DataManager.sale_adKey == string.Empty)
			{
				DataManager.sale_adKey = "Bubble_LB8";
			}
			if (@int == 11 || @int == 12 || DataManager.sale_adKey == string.Empty)
			{
				DataManager.sale_adKey = "Bubble_LB6";
			}
			if (@int == 13 || @int == 14 || DataManager.sale_adKey == string.Empty)
			{
				DataManager.sale_adKey = "Bubble_LB7";
			}
		}
		if (DataManager.sale_adKey == string.Empty)
		{
			if (@int % 2 == 0)
			{
				DataManager.sale_adKey = "Bubble_LB3";
			}
			else
			{
				DataManager.sale_adKey = "Bubble_LB2";
			}
		}
		@int++;
		if (@int > 14)
		{
			@int = 1;
		}
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_SigninUIClose", @int);
		StartCoroutine(IECloseOpenSale());
	}

	private IEnumerator IECloseOpenSale()
	{
		yield return new WaitForSeconds(2f);
		CloseUI();
	}

	public bool CheckDay()
	{
		string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_InitFistLoginGameDayT", string.Empty);
		string nowTime_Day = Util.GetNowTime_Day();
		if (@string == nowTime_Day)
		{
			return true;
		}
		return false;
	}

	public void ReplenishSigninBtn()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (20 > @int)
		{
			EnumUIType[] uiTypes = new EnumUIType[2]
			{
				EnumUIType.BuyGoldUI,
				EnumUIType.SigninUI
			};
			Singleton<UIManager>.Instance.OpenUI(uiTypes);
			CloseUI();
		}
		else
		{
			PayManager.action.BuyBuqian(20);
			Qiandao();
		}
	}

	public void Qiandao()
	{
		int num = 0;
		for (int i = 0; i < Singleton<DataManager>.Instance.dDataSignin.Keys.Count; i++)
		{
			string text = Singleton<DataManager>.Instance.dDataSignin.Keys.ToArray()[i];
			if (text.Length >= 1)
			{
				num = int.Parse(text);
				if (num <= nowday && Singleton<DataManager>.Instance.GetUserDataI("DB_LQJL_" + month + "_" + num) == 0)
				{
					Transform transform = SinginParent.transform.Find("_SigninObj" + text);
					iReceiveNum++;
					transform.GetComponent<SigninObj>().Signin(num);
					break;
				}
			}
		}
		if (num > 20)
		{
			Content.transform.localPosition = new Vector3(0f, 666f, 0f);
		}
		else if (num > 12)
		{
			Content.transform.localPosition = new Vector3(0f, 420f, 0f);
		}
		else
		{
			Content.transform.localPosition = new Vector3(0f, 0f, 0f);
		}
		RefreshUI();
	}

	public bool GetRefreshReplenish()
	{
		for (int i = 0; i < Singleton<DataManager>.Instance.dDataSignin.Keys.Count; i++)
		{
			string text = Singleton<DataManager>.Instance.dDataSignin.Keys.ToArray()[i];
			if (text.Length >= 1)
			{
				int num = int.Parse(text);
				if (num <= nowday && Singleton<DataManager>.Instance.GetUserDataI("DB_LQJL_" + month + "_" + num) == 0)
				{
					return true;
				}
			}
		}
		return false;
	}

	public void CloseSigninUI(bool bClickClose = true)
	{
		if (bClickClose)
		{
			if (!BaseUIAnimation.bClickButton)
			{
				return;
			}
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
		}
		StartCoroutine(CallCloseUI());
	}

	private IEnumerator CallCloseUI(bool b = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseUI(b);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject gameObject = Util.TouchChecker(UnityEngine.Input.mousePosition);
			if (!(gameObject == null) && gameObject.name.LastIndexOf("SigninUI") >= 0)
			{
			}
		}
	}

	protected override void OnAwake()
	{
		base.OnAwake();
	}

	protected override void OnRelease()
	{
		base.OnRelease();
	}
}
