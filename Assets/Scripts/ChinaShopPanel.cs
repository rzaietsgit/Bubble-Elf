using System;
using System.Collections;
using System.Collections.Generic;
using EasyMobile;
using ITSoft;
using UnityEngine;
using UnityEngine.UI;

public class ChinaShopPanel : ChinaShopPanelBase
{
	public static ChinaShopPanel panel;

	public Image adicon;

	public Text adtimes;

	public Text adrewardcounttext;

	public GameObject N_ClearAd;

	public Text freeAds_adtimes;

	public Text freeAds_AdRewardText2;

	public GameObject libaoobj;

	public GameObject daojuobj;

	public GameObject zuanshiobj;

	public Sprite[] propshop_btn;

	public GameObject DaojuObjIcon;

	public GameObject DaojuObjFatch;

	public GameObject zuanshiFather;

	public GameObject zuanshiSon;

	public GameObject LBFather;

	public GameObject LBSon;

	public Text AdRewardText1;

	public Text AdRewardText2;

	public ChinaPaySon defaultChinaPaySonLeft;

	public ChinaPaySon defaultChinaPaySonRight;

	private int clickIndex;

	public Image AdButton;

	public Sprite[] AdButtonsp;

	private bool bfree;

	private bool bInitZs;

	private GameObject[] _zuanshiSon;

	private bool bInitLB;

	private bool bLoadDaojuIcon = true;

	private GameObject[] L_DaojuObjIcon;

	public override UIType GetUIType()
	{
		return UIType.STATIC;
	}

	public void ResAdReward()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_VideoIndex", 1);
		int num = int.Parse(Singleton<DataManager>.Instance.dDatavideoReward[@int.ToString()]["sReward"].Split('|')[0]);
		adrewardcounttext.text = "x" + Singleton<DataManager>.Instance.dDatavideoReward[@int.ToString()]["sReward"].Split('|')[1];
		adicon.sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num, 138, 114);
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_VideoNextTime");
		int nowTime = Util.GetNowTime();
		adtimes.gameObject.SetActive(value: false);
		if (nowTime >= int2)
		{
			bfree = true;
			AdButton.sprite = AdButtonsp[0];
		}
		else
		{
			AdButton.sprite = AdButtonsp[1];
			StartCoroutine(UpdateTime(int2 - nowTime));
		}
	}

	public IEnumerator UpdateTime(int itime)
	{
		while (itime > 1)
		{
			adtimes.text = itime + "s";
			itime--;
			adtimes.gameObject.SetActive(value: true);
			yield return new WaitForSeconds(1f);
		}
		AdButton.sprite = AdButtonsp[0];
		adtimes.gameObject.SetActive(value: false);
		bfree = true;
	}

	public override void InitUI()
	{
        AdsManager.HideBanner();
        panel = this;
		N_ClearAd.SetActive(value: false);
		BaseUIAnimation.action.SetLanguageFont("AdRewardText1", AdRewardText1, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("AdRewardText2", AdRewardText2, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("ChinaShopUITitle1", detail.UITitle_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("ChinaShopUI1", detail.liebiaoTitle_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("ChinaShopUI2", detail.daojuTitle_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("ChinaShopUI3", detail.zuanshiTitle_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("ChinaShopUI7", detail.daojishitext2_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("ChinaShopUI8", detail.daojishitime_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("ADS_FREE", freeAds_adtimes, string.Empty);
		if (InitGame.bEnios)
		{
			detail.liebiaoTitle_Text.fontSize = 35;
			detail.zuanshiTitle_Text.fontSize = 30;
			detail.daojishitime_Text.transform.localPosition += new Vector3(25f, 0f, 0f);
			detail.daojusTime_Text.transform.localPosition += new Vector3(25f, 0f, 0f);
			detail.daojishitext2_Text.fontSize = 15;
			detail.daojuResBtn_Button.transform.localPosition += new Vector3(30f, 0f, 0f);
		}
		LoadDataShopUI();
		if (Singleton<DataManager>.Instance.ChinaShopOpendaoju)
		{
			Clicklidaojuobj();
		}
		else if (Singleton<DataManager>.Instance.ChinaShopOpenZuanshi)
		{
			Clickzuanshiobj();
		}
		else
		{
			Clicklibaoobj();
		}
		Singleton<DataManager>.Instance.ChinaShopOpendaoju = false;
		Singleton<DataManager>.Instance.ChinaShopOpenZuanshi = false;
		InitAndroid.action.checkShowAdTip();
		ResAdReward();
		string text = Singleton<DataManager>.Instance.dDataLanguage["ChinaShopUI4"][BaseUIAnimation.Language];
		text = text.Replace("A1", Singleton<DataManager>.Instance.dDataChinaPay["Adsfree"]["iMoneyiosen"].ToString());
		freeAds_AdRewardText2.text = text;
		if (Singleton<DataManager>.Instance.CommodityPricesDic != null)
		{
			string key = Singleton<DataManager>.Instance.dDataChinaPay["Adsfree"]["googlekey"];
			string text2 = Singleton<DataManager>.Instance.CommodityPricesDic[key];
			freeAds_AdRewardText2.text = text2;
		}
		UpdateClearAdBtn();
	}

	public void UpdateClearAdBtn()
	{
		if (Singleton<TestScript>.Instance.GetInt("IsClearAd") == 0)
		{
			N_ClearAd.SetActive(value: true);
		}
		else
		{
			N_ClearAd.SetActive(value: false);
		}
	}

	public void Clicklibaoobj()
	{
		clickIndex = 1;
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		libaoobj.SetActive(value: true);
		daojuobj.SetActive(value: false);
		zuanshiobj.SetActive(value: false);
		detail.S_libaobtn_Button.GetComponent<Image>().sprite = propshop_btn[0];
		detail.S_daojubtn_Button.GetComponent<Image>().sprite = propshop_btn[1];
		detail.S_zuanshibtn_Button.GetComponent<Image>().sprite = propshop_btn[1];
		ResLibao();
		InitLB();
	}

	public void ResLibao()
	{
		if (!InitGame.bEnios)
		{
		}
	}

	public void Clicklidaojuobj()
	{
		clickIndex = 2;
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		libaoobj.SetActive(value: false);
		daojuobj.SetActive(value: true);
		zuanshiobj.SetActive(value: false);
		detail.S_libaobtn_Button.GetComponent<Image>().sprite = propshop_btn[1];
		detail.S_daojubtn_Button.GetComponent<Image>().sprite = propshop_btn[0];
		detail.S_zuanshibtn_Button.GetComponent<Image>().sprite = propshop_btn[1];
		InitDaoju();
	}

	public void Clickzuanshiobj()
	{
		clickIndex = 3;
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		libaoobj.SetActive(value: false);
		daojuobj.SetActive(value: false);
		zuanshiobj.SetActive(value: true);
		detail.S_libaobtn_Button.GetComponent<Image>().sprite = propshop_btn[1];
		detail.S_daojubtn_Button.GetComponent<Image>().sprite = propshop_btn[1];
		detail.S_zuanshibtn_Button.GetComponent<Image>().sprite = propshop_btn[0];
		InitZs();
	}

	public override void OnPauseBase()
	{
	}

	public void ShowAdTip()
	{
	}

	public void InitZs()
	{
		if (!bInitZs)
		{
			_zuanshiSon = new GameObject[5];
			bInitZs = true;
			for (int i = 1; i <= 3; i++)
			{
				_zuanshiSon[i - 1] = UnityEngine.Object.Instantiate(zuanshiSon);
				_zuanshiSon[i - 1].transform.SetParent(zuanshiFather.transform, worldPositionStays: false);
				_zuanshiSon[i - 1].SetActive(value: true);
				ChinaPaySon component = _zuanshiSon[i - 1].GetComponent<ChinaPaySon>();
				component.InitPay(i);
			}
			defaultChinaPaySonLeft.InitPay(4);
			defaultChinaPaySonRight.InitPay(5);
		}
	}

	public void ResZs()
	{
		for (int i = 1; i <= 3; i++)
		{
			ChinaPaySon component = _zuanshiSon[i - 1].GetComponent<ChinaPaySon>();
			component.InitPay(i);
		}
		defaultChinaPaySonLeft.InitPay(4);
		defaultChinaPaySonRight.InitPay(5);
	}

	public void InitLB()
	{
		if (!bInitLB)
		{
			bInitLB = true;
			for (int i = 1; i <= 3; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(LBSon);
				gameObject.transform.SetParent(LBFather.transform, worldPositionStays: false);
				gameObject.SetActive(value: true);
				ShopLibao component = gameObject.GetComponent<ShopLibao>();
				component.SetIndex(i);
			}
		}
	}

	public override void OndaojuResBtn()
	{
		UI.Instance.OpenPanel(UIPanelType.okbuyPanel);
	}

	public void InitDaoju()
	{
		if (bLoadDaojuIcon)
		{
			bLoadDaojuIcon = false;
			Create9Shop(bmoney: false, binit: true);
			sUpdateTime();
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
			detail.daojusTime_Text.text = sH + ":" + sF + ":" + sM;
			yield return new WaitForSeconds(1f);
			if (iTime <= 1)
			{
				Create9Shop(bmoney: true);
			}
		}
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
				if (num < 3)
				{
					L_DaojuObjIcon[num].gameObject.SetActive(value: false);
				}
			}
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

	public override void OnS_libaobtn()
	{
		Clicklibaoobj();
	}

	public override void OnS_daojubtn()
	{
		Clicklidaojuobj();
	}

	public override void OnS_zuanshibtn()
	{
		Clickzuanshiobj();
	}

	public void LoadDataShopUI()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GB");
		detail.Imagegoldgbtext_Text.text = int2.ToString();
		detail.Imagegemzstext_Text.text = @int.ToString();
	}

	public override void OnResume()
	{
		if (clickIndex == 2)
		{
			LoadDaoju_1(binit: false);
		}
	}
    private int indexads;
	public void ClickAdReward()
	{
#if UNITY_EDITOR
        DataManager.isShopAd = true;
        InitAndroid.action.PlayVideoHG(true);
#endif
        if (AdsManager.RewardIsReady())
        {
            indexads = 1;
            AdsManager.ShowRewarded();
        }
        else
        {
            Singleton<DataManager>.Instance.iNoticePanelType = 3;
            UI.Instance.OpenPanel(UIPanelType.NoticePanel);
        }
    }

    void OnEnable()
    {
        AdsManager.OnCompleteRewardVideo += RewardedAdCompletedHandler;
    }
    // The event handler
    void RewardedAdCompletedHandler()
    {
        if(indexads == 1)
        {
            indexads = 0;
            InitAndroid.action.PlayVideoHG(true);
            DataManager.isShopAd = true;

            //int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_VideoNextTime");
            //int nowTime = Util.GetNowTime();
            //if (nowTime > @int)
            //{
            //    if (AdManager.IsRewardedAdReady())
            //    {
            //        UnityEngine.Debug.Log("================================3");
            //        InitAndroid.action.PlayVideoHG(true);
            //    }
            //    else
            //    {
            //        Singleton<DataManager>.Instance.iNoticePanelType = 3;
            //        UI.Instance.OpenPanel(UIPanelType.NoticePanel);
            //    }
            //}
        }
    }
    // Unsubscribe
    void OnDisable()
    {
        AdsManager.OnCompleteRewardVideo -= RewardedAdCompletedHandler;
    }

    public void OnBtn_ClearAd()
	{
		//InitAndroid.action.doChainePay("Adsfree");
        IAPManager.Purchase(EM_IAPConstants.Product_adsfree);
    }
}
