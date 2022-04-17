using System;
using EasyMobile;
using ITSoft;
using UnityEngine;
using UnityEngine.UI;

public class OpenScript : MonoBehaviour
{
	public static OpenScript action;

	public static OpenScript action1;

	public static OpenScript actionrenwu1;

	public static OpenScript actionSevLogin1;

	public GameObject hongdian;

	public GameObject NewTaskObj;

	public int isHaohua;

	public int isrenwu;

	public int SevLogin1;

	public int cdkey;

	public int zhuanpan;

	public Sprite[] LIcon;

	public GameObject OtherImage1;

	public GameObject OtherImage2;

	public bool bother;

	public static OpenScript otheraction;

	private bool botheropen = true;

	private bool renwuhongdian;

	private int iNowDay;

	private void Start()
	{
		if (bother)
		{
			otheraction = this;
		}
		if ((bool)hongdian && (bool)NewTaskObj)
		{
			action = this;
			Reshongdian();
		}
		if (isHaohua == 1)
		{
			action1 = this;
		}
		if (isrenwu == 1)
		{
			if (InitGame.bEnios)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			actionrenwu1 = this;
			Resrenwuhongdian();
		}
		if (SevLogin1 == 1)
		{
			actionSevLogin1 = this;
			if (InitGame.bEnios)
			{
				try
				{
					base.gameObject.transform.Find("Image").GetComponent<Image>().sprite = LIcon[0];
				}
				catch (Exception)
				{
				}
			}
			ResSev7Login();
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Sign7State");
			if (@int == 1)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		if (cdkey == 1)
		{
			if (!Singleton<DataManager>.Instance.bGooglePay)
			{
				if (Singleton<DataManager>.Instance.bChinaIos)
				{
					base.gameObject.GetComponent<Image>().enabled = false;
					base.gameObject.transform.Find("Image").GetComponent<Image>().enabled = false;
				}
				else
				{
					base.gameObject.GetComponent<Image>().enabled = false;
					base.gameObject.transform.Find("Image").GetComponent<Image>().enabled = false;
				}
			}
			if (Singleton<DataManager>.Instance.bGooglePay)
			{
				base.gameObject.transform.Find("Image").GetComponent<Image>().sprite = LIcon[0];
			}
			else if (InitGame.bEnios)
			{
				base.gameObject.GetComponent<Image>().enabled = false;
				base.gameObject.transform.Find("Image").gameObject.GetComponent<Image>().enabled = false;
			}
			else if (Singleton<DataManager>.Instance.bChinaIos)
			{
				base.gameObject.GetComponent<Image>().enabled = false;
				base.gameObject.transform.Find("Image").GetComponent<Image>().enabled = false;
			}
			else
			{
				base.gameObject.GetComponent<Image>().enabled = true;
				base.gameObject.transform.Find("Image").GetComponent<Image>().enabled = true;
			}
		}
		if (zhuanpan == 1)
		{
			base.gameObject.GetComponent<Image>().enabled = false;
		}
		ResHaohuaBtnUI();
	}

	public void Reshongdian()
	{
	}
    private int indexads;
    public void ClickBuyBubble10()
	{
		if (PassLevel.bWin || PauseUI.action.bPause || FindPath.bpath2 || FindPath.bpath || Singleton<LevelManager>.Instance.iBubbleCount == 0)
		{
			return;
		}
		Singleton<DataManager>.Instance.bAutoOpen = false;
		if (Application.platform == RuntimePlatform.WindowsEditor)
		{
			GameUI.action.isZhiJieBoFang = true;
			GameUI.action.buyBuyBuShu = 1;
			Singleton<LevelManager>.Instance.iBubbleCount = Singleton<LevelManager>.Instance.iBubbleCount + 5;
			Singleton<LevelManager>.Instance.iBubbleUseCount = 0;
			PassLevel.action.KillLiuhan();
			GameUI.action.LoadBubbleCount();
			BubbleSpawner.Instance.initReadyBubble(isusekey: false);
			GameUI.action.KillNowBuyBubble();
		}
		else
		{
			if (AdsManager.RewardIsReady())
			{
                indexads = 1;
                AdsManager.ShowRewarded();
      
			}
		}
	}

    void OnEnable()
    {
	    AdsManager.OnCompleteRewardVideo += RewardedAdCompletedHandler;
    }
    // The event handler
    void RewardedAdCompletedHandler()
    {
        if (indexads == 1)
        {
            indexads = 0;
            //GameUI.action.isZhiJieBoFang = true;
            //DataManager.isShopAd = false;
            //Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "NowBuyBubbleUIPanel" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex, 1);
            //UnityEngine.Debug.Log("================================2");
            //InitAndroid.action.PlayVideoHG(_bShop: true);
            GameUI.action.isZhiJieBoFang = true;
            GameUI.action.buyBuyBuShu = 1;
            Singleton<LevelManager>.Instance.iBubbleCount = Singleton<LevelManager>.Instance.iBubbleCount + 5;
            Singleton<LevelManager>.Instance.iBubbleUseCount = 0;
            PassLevel.action.KillLiuhan();
            GameUI.action.LoadBubbleCount();
            BubbleSpawner.Instance.initReadyBubble(false);
            GameUI.action.KillNowBuyBubble();
        }
    }
    // Unsubscribe
    void OnDisable()
    {
	    AdsManager.OnCompleteRewardVideo -= RewardedAdCompletedHandler;
    }

    public void ResHaohuaBtnUI()
	{
		if (isHaohua == 1 && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "PAYBubble_LB3") > 0)
		{
			base.gameObject.SetActive(value: false);
		}
	}

	public void BuyHaohuaLIbao()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance() && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "PAYBubble_LB3") <= 0)
		{
			DataManager.sale_adKey = "Bubble_LB3";
			UI.Instance.OpenPanel(UIPanelType.SaleAdUI);
		}
	}

	public void ClickXinshourenwu()
	{
		if (!InitGame.bHideSign7Task && !Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance())
		{
			UI.Instance.OpenPanel(UIPanelType.NewTask1UI);
			InitAndroid.action.GAEvent("clickbtn:ClickNewTask1");
			Resrenwuhongdian();
		}
	}

	public void ClickBeibaoBackPack()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance())
		{
			UI.Instance.OpenPanel(UIPanelType.PackSkillIconUI);
		}
	}

	public void ClickSign31()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance() && Util.CheckOnline())
		{
			UI.Instance.OpenPanel(UIPanelType.SignRewardUI);
			InitAndroid.action.GAEvent("clickbtn:SignRewardUI");
		}
	}

	public void ClickSign7()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance() && !InitGame.bHideSign7Task && Util.CheckOnline())
		{
			UI.Instance.OpenPanel(UIPanelType.SignReward7UI);
			InitAndroid.action.GAEvent("clickbtn:ClickShowSign7");
		}
	}

	public void ClickShouchonglibao()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance())
		{
			UI.Instance.OpenPanel(UIPanelType.ChinaShop);
		}
	}

	public void ClickZhuanpan()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance())
		{
			UI.Instance.OpenPanel(UIPanelType.LotteryUI);
		}
	}

	public void Clickkey()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance())
		{
			UI.Instance.OpenPanel(UIPanelType.cdkeyUI);
		}
	}

	public void ClickOther()
	{
		InitAndroid.action.GAEvent("clickbtn:other");
		if (OtherImage1.active)
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("ui_button_pinwheel");
			}
			OtherImage1.SetActive(value: false);
			OtherImage2.SetActive(value: false);
			Animator component = MapUI.action.FengcheObj.GetComponent<Animator>();
			component.SetTrigger("hide");
		}
		else
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("ui_button_pinwheel");
			}
			OtherImage1.SetActive(value: true);
			OtherImage2.SetActive(value: true);
			Animator component2 = MapUI.action.FengcheObj.GetComponent<Animator>();
			component2.SetTrigger("show");
		}
		botheropen = true;
	}

	public void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			botheropen = false;
		}
		if (Input.GetMouseButtonUp(0) && !botheropen && bother && OtherImage1.active)
		{
			OtherImage1.SetActive(value: false);
			OtherImage2.SetActive(value: false);
			Animator component = MapUI.action.FengcheObj.GetComponent<Animator>();
			component.SetTrigger("hide");
		}
	}

	public void ClickShopUI()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance())
		{
			Singleton<DataManager>.Instance.ChinaShopOpendaoju = true;
			Singleton<DataManager>.Instance.ChinaShopOpenZuanshi = false;
			UI.Instance.OpenPanel(UIPanelType.ChinaShop);
			InitAndroid.action.GAEvent("clickbtn:Shangdian");
		}
	}

	public void ResSev7Login()
	{
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Sign7State") == 0)
		{
			string nowTime_Day = Util.GetNowTime_Day();
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SignOK7" + nowTime_Day) == 0)
			{
				if ((bool)hongdian)
				{
					hongdian.SetActive(value: true);
				}
			}
			else if ((bool)hongdian)
			{
				hongdian.SetActive(value: false);
			}
		}
		else if ((bool)hongdian)
		{
			hongdian.SetActive(value: false);
		}
	}

	public void Resrenwuhongdian()
	{
		int nowTime = Util.GetNowTime();
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_InitFistLoginGameDay");
		nowTime -= @int;
		if (nowTime <= 864000)
		{
			nowTime = 864000 - nowTime;
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
		}
		else
		{
			iNowDay = 0;
		}
		renwuhongdian = false;
		for (int i = 1; i < 8; i++)
		{
			if (!renwuhongdian)
			{
				GoTaskFor(i);
			}
		}
		if (renwuhongdian)
		{
			hongdian.SetActive(value: true);
		}
		else
		{
			hongdian.SetActive(value: false);
		}
	}

	public void GoTaskFor(int index)
	{
		if (index <= iNowDay)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int num = int.Parse(Singleton<DataManager>.Instance.dDataTaskList1["task" + index]["Rid1"]);
			int num2 = int.Parse(Singleton<DataManager>.Instance.dDataTaskList1["task" + index]["Rid2"]);
			int num3 = int.Parse(Singleton<DataManager>.Instance.dDataTaskList1["task" + index]["Rcount1"]);
			int num4 = int.Parse(Singleton<DataManager>.Instance.dDataTaskList1["task" + index]["Rcount2"]);
			string text = Singleton<DataManager>.Instance.dDataTaskList1["task" + index]["taskremark"];
			string s = Singleton<DataManager>.Instance.dDataTaskList1["task" + index]["taskCount"];
			string text2 = Singleton<DataManager>.Instance.dDataTaskList1["task" + index]["Type"];
			int num5 = 0;
			float num6 = 0f;
			if (text2 == "PassLevel")
			{
				int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
				num5 = @int;
			}
			else if (text2 == "UseGang" || text2 == "jingling" || text2 == "KillBubble" || text2 == "Skill4" || text2 == "Use_gbdj" || text2 == "Use_zsdj")
			{
				num5 = Singleton<UserManager>.Instance.GetTaskCount1(text2);
			}
			num6 = (float)num5 * 100f / ((float)int.Parse(s) * 100f);
			flag2 = ((num6 >= 1f) ? true : false);
			int num7 = int.Parse(Singleton<DataManager>.Instance.dDataTaskList1["taskadd" + index]["Rid1"]);
			int num8 = int.Parse(Singleton<DataManager>.Instance.dDataTaskList1["taskadd" + index]["Rid2"]);
			int num9 = int.Parse(Singleton<DataManager>.Instance.dDataTaskList1["taskadd" + index]["Rcount1"]);
			int num10 = int.Parse(Singleton<DataManager>.Instance.dDataTaskList1["taskadd" + index]["Rcount2"]);
			string text3 = Singleton<DataManager>.Instance.dDataTaskList1["taskadd" + index]["taskremark"];
			string s2 = Singleton<DataManager>.Instance.dDataTaskList1["taskadd" + index]["taskCount"];
			num6 = 0f;
			num6 = (float)num5 * 100f / ((float)int.Parse(s2) * 100f);
			if (num5 > int.Parse(s2))
			{
				num5 = int.Parse(s2);
			}
			int nowTime = Util.GetNowTime();
			int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_InitFistLoginGameDay");
			nowTime -= int2;
			if (nowTime <= index * 60 * 60 * 24)
			{
				flag3 = false;
			}
			else
			{
				flag3 = true;
			}
			num6 = (float)num5 * 100f / ((float)int.Parse(s2) * 100f);
			flag = ((num6 >= 1f) ? true : false);
			int int3 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NewTask1_1" + index);
			if (int3 != 1 && flag2)
			{
				renwuhongdian = true;
			}
			int int4 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NewTask1_2" + index);
			if (int4 != 1 && flag)
			{
				renwuhongdian = true;
			}
		}
	}
}
