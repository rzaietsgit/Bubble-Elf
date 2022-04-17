using System;
using System.Collections;
using System.Collections.Generic;
using EasyMobile;
using ITSoft;
using UnityEngine;
using UnityEngine.UI;

public class ChinaShopUI : BaseUI
{
	public GameObject CloseBtn;

	public static ChinaShopUI action;

	public GameObject libaoobj;

	public GameObject daojuobj;

	public GameObject zuanshiobj;

	public GameObject[] yuan30Hide;

	public GameObject zuanshiobDouble;

	public GameObject libaoBtn;

	public GameObject daojuBtn;

	public GameObject zuanshiBtn;

	public GameObject zuanshiFather;

	public GameObject zuanshiSon;

	public GameObject DaojuObjIcon;

	public GameObject DaojuObjFatch;

	public Sprite[] propshop_btn;

	public bool bgodaoju;

	public Sprite BuyLeftimg;

	public Text sTimeObj;

	public Text ChinaShopUITitle;

	public Text ChinaShopUI1;

	public Text ChinaShopUI2;

	public Text ChinaShopUI3;

	public Text ChinaShopUI7;

	public Text ChinaShopUI8;

	public GameObject ChinaAdFree;

	private bool bShowAd;

	public Text gbText;

	public Text zsText;

	private bool bLoadDaojuIcon = true;

	private GameObject[] L_DaojuObjIcon;

	private bool bInitZs;

	private GameObject[] _zuanshiSon;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.ChinaShopUI;
	}

	public void ShowAdTip()
	{
		bShowAd = true;
		ChinaAdFree.SetActive(value: true);
	}

	public void LoadDataShopUI()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GB");
		gbText.text = int2.ToString();
		zsText.text = @int.ToString();
	}

	public void _CloseChinaShopUI()
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

	private void Start()
	{
        AdsManager.HideBanner();
        ChinaAdFree.SetActive(value: false);
		if (Singleton<DataManager>.Instance.EBuyLiveSale != EnumUIType.None)
		{
			Singleton<DataManager>.Instance.bBuyLiveSale = true;
		}
		BaseUIAnimation.action.SetLanguageFont("ChinaShopUITitle1", ChinaShopUITitle, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("ChinaShopUI1", ChinaShopUI1, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("ChinaShopUI2", ChinaShopUI2, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("ChinaShopUI3", ChinaShopUI3, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("ChinaShopUI7", ChinaShopUI7, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("ChinaShopUI8", ChinaShopUI8, string.Empty);
		DataManager.bbeibaoFlay = false;
		Singleton<DataManager>.Instance.bOpenReward7 = true;
		action = this;
		bgodaoju = false;
		LoadDataShopUI();
		if (InitGame.bChinaVersion && (DataManager.ChannelId == "andgame" || DataManager.ChannelId == "xiaowo" || DataManager.ChannelId == "dianxin"))
		{
			for (int i = 0; i < yuan30Hide.Length; i++)
			{
				yuan30Hide[i].SetActive(value: false);
			}
		}
		if (Singleton<DataManager>.Instance.ChinaShopOpen)
		{
			Singleton<DataManager>.Instance.ChinaShopOpen = false;
			Clicklibaoobj();
		}
		else if (Singleton<DataManager>.Instance.ChinaShopOpendaoju)
		{
			Singleton<DataManager>.Instance.ChinaShopOpendaoju = false;
			Clicklidaojuobj();
		}
		else
		{
			Clickzuanshiobj();
		}
		InitAndroid.action.checkShowAdTip();
	}

	private void Awake()
	{
		if ((bool)GameUI.action)
		{
			Canvas component = base.gameObject.transform.GetComponent<Canvas>();
			component.renderMode = RenderMode.ScreenSpaceCamera;
			component.worldCamera = GameUI.action.mainCameraS.GetComponent<Camera>();
		}
		else if ((bool)MapUI.action)
		{
			Canvas component2 = base.gameObject.transform.GetComponent<Canvas>();
			component2.renderMode = RenderMode.ScreenSpaceCamera;
			component2.worldCamera = MapUI.action.MapUISceneCamera.GetComponent<Camera>();
		}
		if ((bool)HuaGame.action)
		{
			Canvas component3 = base.gameObject.transform.GetComponent<Canvas>();
			component3.renderMode = RenderMode.ScreenSpaceCamera;
			component3.worldCamera = HuaGame.action.HuaCamera.GetComponent<Camera>();
		}
	}

	private void LoadDaojuIcon()
	{
		if (bLoadDaojuIcon)
		{
			bLoadDaojuIcon = false;
			Create9Shop(bmoney: false, binit: true);
			sUpdateTime();
		}
	}

	public int GetRandom(int NoR1, int NoR2)
	{
		if (NoR1 != 0)
		{
			NoR1 -= 3;
		}
		if (NoR2 != 0)
		{
			NoR2 -= 3;
		}
		List<int> list = new List<int>();
		for (int i = 1; i <= 8; i++)
		{
		}
		for (int j = 1; j <= 6; j++)
		{
			if (j == NoR1)
			{
				list.Add(1);
				continue;
			}
			if (j == NoR2)
			{
				list.Add(1);
				continue;
			}
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_UseDaoju" + j, 1);
			list.Add(@int);
		}
		int num = 0;
		for (int k = 0; k < list.Count; k++)
		{
			num += list[k] + 1;
		}
		int num2 = UnityEngine.Random.Range(0, num);
		num = 0;
		for (int l = 0; l < list.Count; l++)
		{
			num += list[l] + 1;
			if (num2 <= num)
			{
				return l + 1;
			}
		}
		return 1;
	}

	public void Create9Shop(bool bmoney = false, bool binit = false)
	{
		int num = UnityEngine.Random.Range(1, 6);
		string a = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_RDaojuList", string.Empty);
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_RDaojuList_Time");
		int num2 = 28800;
		bool flag = false;
		if (Util.GetNowTime() - @int > num2)
		{
			flag = true;
			a = string.Empty;
		}
		if (a == string.Empty || bmoney)
		{
			int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_ResDaoju" + Util.GetNowTime_Day());
			int num3 = GetRandom(num, 0) + 3;
			int num4 = 0;
			while (num3 == num)
			{
				num3 = GetRandom(num, 0) + 3;
			}
			if (int2 >= 4)
			{
				num4 = GetRandom(num, num3) + 3;
				while (num4 == num || num4 == num3)
				{
					num4 = GetRandom(num, num3) + 3;
				}
			}
			string text = string.Empty;
			for (int i = 1; i <= 39; i++)
			{
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_BuyDaojuRecording" + i, 0);
			}
			int value = 0;
			for (int j = 1; j <= 11; j++)
			{
				int num5 = UnityEngine.Random.Range(1, 100);
				int num6 = j * 3 - 2;
				float num7 = (0.6f - float.Parse(int2 + string.Empty) * 0.04f) * 100f;
				float num8 = (0.3f + float.Parse(int2 + string.Empty) * 0.03f) * 100f;
				float num9 = (0.1f + float.Parse(int2 + string.Empty) * 0.01f) * 100f;
				if ((float)num5 > 100f - num9)
				{
					num6 += 2;
				}
				else if ((float)num5 > 100f - num9 - num8)
				{
					num6++;
				}
				if (j == num)
				{
					value = num6;
				}
				if (num3 == j)
				{
					num6 = j + 30;
				}
				if (num4 == j)
				{
					num6 = j + 30;
				}
				text = text + num6 + ",";
			}
			Singleton<TestScript>.Instance.SetString(DataManager.SDBNO + "DB_RDaojuList", text);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_RDaojuGB", value);
			if (flag)
			{
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_RDaojuList_Time", Util.GetNowTime());
			}
		}
		LoadDaoju_1(binit);
	}

	public void buyResUI()
	{
		int num = 0;
		for (int i = 1; i <= 39; i++)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_BuyDaojuRecording" + i);
			if (@int == 1)
			{
				num++;
			}
		}
		if (num >= 11)
		{
			Create9Shop(bmoney: true);
		}
		else
		{
			LoadDaoju_1(binit: false);
		}
	}

	private void LoadDaoju_1(bool binit = true)
	{
		string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_RDaojuList", string.Empty);
		if (binit)
		{
			L_DaojuObjIcon = new GameObject[11];
		}
		else
		{
			for (int i = 0; i < 11; i++)
			{
				UnityEngine.Object.Destroy(L_DaojuObjIcon[i]);
			}
		}
		for (int num = 0; num < @string.Split(',').Length; num++)
		{
			string text = @string.Split(',')[num];
			if (!(text == string.Empty))
			{
				L_DaojuObjIcon[num] = UnityEngine.Object.Instantiate(DaojuObjIcon);
				L_DaojuObjIcon[num].transform.SetParent(DaojuObjFatch.transform, worldPositionStays: false);
				ShopDaoju component = L_DaojuObjIcon[num].GetComponent<ShopDaoju>();
				component.SetType(int.Parse(text));
			}
		}
	}

	private void sUpdateTime()
	{
		StartCoroutine(IEUpdateTime());
	}

	private IEnumerator IEUpdateTime()
	{
		while (true)
		{
			int iRDaojuList_Time = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_RDaojuList_Time");
			int h_8 = 28800;
			int iTime = h_8 - (Util.GetNowTime() - iRDaojuList_Time);
			if (iTime < 1)
			{
				iTime = 1;
			}
			TimeSpan ts = new TimeSpan(0, 0, iTime);
			int iF_ = ts.Minutes;
			int iH_ = ts.Hours;
			int iM_ = ts.Seconds;
			string sF = iF_ + string.Empty;
			string sH = iH_ + string.Empty;
			string sM = iM_ + string.Empty;
			if (iF_ < 10)
			{
				sF = "0" + sF;
			}
			if (iH_ < 10)
			{
				sH = "0" + sH;
			}
			if (iM_ < 10)
			{
				sM = "0" + sM;
			}
			sTimeObj.text = sH + ":" + sF + ":" + sM;
			yield return new WaitForSeconds(1f);
			if (iTime <= 1)
			{
				Create9Shop(bmoney: true);
			}
		}
	}

	public void ResDaojuList()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (25 > @int)
		{
			action.Clickzuanshiobj();
			return;
		}
		PayManager.action.BuyDaoju(25);
		LoadDataShopUI();
		Create9Shop(bmoney: true);
		//Analytics.Event("ResDaoju");
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_ResDaoju" + Util.GetNowTime_Day());
		int2++;
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_ResDaoju" + Util.GetNowTime_Day(), int2);
		if ((bool)MapUI.action)
		{
			MapUI.action.LoadGold(bUpdate: false);
		}
	}

	private void Update()
	{
	}

	public void Buylibao1()
	{
		//InitAndroid.action.doChainePay("Bubble_LB1");
        IAPManager.Purchase(EM_IAPConstants.Product_bubble_lb1);
    }

	public void Buylibao2()
	{
		//InitAndroid.action.doChainePay("Bubble_LB2");
        IAPManager.Purchase(EM_IAPConstants.Product_bubble_lb2);
    }

	public void Buylibao3()
	{
		//InitAndroid.action.doChainePay("Bubble_LB3");
        IAPManager.Purchase(EM_IAPConstants.Product_bubble_lb3);
    }

	public void Buylibao4()
	{
		//InitAndroid.action.doChainePay("Bubble_LB4");
        IAPManager.Purchase(EM_IAPConstants.Product_bubble_lb4);
    }

	public void BuyGold1()
	{
		//InitAndroid.action.doChainePay("Bubble_GOLD1");
        IAPManager.Purchase(EM_IAPConstants.Product_bubble_gold1);
    }

	public void BuyGold2()
	{
		//InitAndroid.action.doChainePay("Bubble_GOLD2");
        IAPManager.Purchase(EM_IAPConstants.Product_bubble_gold2);
    }

	public void BuyGold3()
	{
		//InitAndroid.action.doChainePay("Bubble_GOLD3");
        IAPManager.Purchase(EM_IAPConstants.Product_bubble_gold3);
    }

	public void BuyGold4()
	{
		//InitAndroid.action.doChainePay("Bubble_GOLD4");
        IAPManager.Purchase(EM_IAPConstants.Product_bubble_gold4);
    }

	public void BuyGold5()
	{
		//InitAndroid.action.doChainePay("Bubble_GOLD5");
        IAPManager.Purchase(EM_IAPConstants.Product_bubble_gold5);
    }

	public void Clicklibaoobj()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		if (!libaoobj.active)
		{
			libaoobj.SetActive(value: true);
		}
		if (daojuobj.active)
		{
			daojuobj.SetActive(value: false);
		}
		if (zuanshiobj.active)
		{
			zuanshiobj.SetActive(value: false);
		}
		libaoBtn.GetComponent<Image>().sprite = propshop_btn[0];
		daojuBtn.GetComponent<Image>().sprite = propshop_btn[1];
		zuanshiBtn.GetComponent<Image>().sprite = propshop_btn[1];
	}

	public void Clicklidaojuobj()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		bgodaoju = false;
		if (libaoobj.active)
		{
			libaoobj.SetActive(value: false);
		}
		if (!daojuobj.active)
		{
			daojuobj.SetActive(value: true);
		}
		if (zuanshiobj.active)
		{
			zuanshiobj.SetActive(value: false);
		}
		libaoBtn.GetComponent<Image>().sprite = propshop_btn[1];
		daojuBtn.GetComponent<Image>().sprite = propshop_btn[0];
		zuanshiBtn.GetComponent<Image>().sprite = propshop_btn[1];
		LoadDaojuIcon();
	}

	public void Clickzuanshiobj()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		if (libaoobj.active)
		{
			libaoobj.SetActive(value: false);
		}
		if (daojuobj.active)
		{
			daojuobj.SetActive(value: false);
		}
		if (!zuanshiobj.active)
		{
			zuanshiobj.SetActive(value: true);
		}
		libaoBtn.GetComponent<Image>().sprite = propshop_btn[1];
		daojuBtn.GetComponent<Image>().sprite = propshop_btn[1];
		zuanshiBtn.GetComponent<Image>().sprite = propshop_btn[0];
		InitZs();
	}

	public void ResZs()
	{
		if (!bInitZs)
		{
			return;
		}
		for (int i = 1; i <= 6; i++)
		{
			if ((bool)_zuanshiSon[i - 1])
			{
				ChinaPaySon component = _zuanshiSon[i - 1].GetComponent<ChinaPaySon>();
				component.InitPay(i);
			}
		}
	}

	public void InitZs()
	{
		if (!bInitZs)
		{
			_zuanshiSon = new GameObject[6];
			bInitZs = true;
			for (int i = 1; i <= 6; i++)
			{
				_zuanshiSon[i - 1] = UnityEngine.Object.Instantiate(zuanshiSon);
				_zuanshiSon[i - 1].transform.SetParent(zuanshiFather.transform, worldPositionStays: false);
				_zuanshiSon[i - 1].SetActive(value: true);
				ChinaPaySon component = _zuanshiSon[i - 1].GetComponent<ChinaPaySon>();
				component.InitPay(i);
			}
		}
	}
}
