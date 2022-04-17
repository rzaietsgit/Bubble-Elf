using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class BuyLivesUI : BaseUI
{
	public static BuyLivesUI action;

	public GameObject AskBtn;

	public GameObject BuyLivestBtn;

	public GameObject CloseBtn;

	public GameObject StarObj;

	public GameObject StarObj_Father;

	public Sprite StarObj_crush;

	public Text Time1;

	public Text Time2;

	public Text Time3;

	public Text Time4;

	public GameObject TimeCalcText2;

	public Sprite FullImg;

	public Sprite NullBuyBtn;

	public GameObject CenterImg;

	public Text BuyLivesTitle;

	public Text BuyLivesRemark;

	public Text BuyLivesAskFriends;

	public Text BuyLivesbtn;

	public Text BuyLivesTimeCalcRemark;

	public Text GoldNumber;

	public GameObject inaGoldNumberChina;

	private int iLovePrice = 25;

	private int iRtime;

	private int iNowLove;

	private bool bwhile = true;

	private GameObject[] tmpStar;

	private static int oldlove;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.BuyLivesUI;
	}

	public override void OnStart()
	{
		action = this;
		iRtime = 80;
		BaseUIAnimation.action.CreateButton(AskBtn.gameObject);
		BaseUIAnimation.action.CreateButton(BuyLivestBtn.gameObject);
		LoadStar();
		CalcTime();
		LoadTime();
		StartCoroutine(IERunTime());
		if (InitGame.bChinaVersion)
		{
			AskBtn.SetActive(value: false);
		}
		else
		{
			AskBtn.SetActive(value: true);
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
			if (@int >= 5)
			{
				BuyLivestBtn.GetComponent<Image>().sprite = NullBuyBtn;
			}
		}
		//Analytics.Event("LogClick20");
		BaseUIAnimation.action.SetLanguageFont("BuyLivesTitle", BuyLivesTitle, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BuyLivesRemark", BuyLivesRemark, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BuyLivesAskFriends", BuyLivesAskFriends, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BuyLivesbtn", BuyLivesbtn, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BuyLivesTimeCalcRemark", BuyLivesTimeCalcRemark, string.Empty);
		if (InitGame.bChinaVersion)
		{
			iLovePrice = 100;
			inaGoldNumberChina.gameObject.SetActive(value: true);
			GoldNumber.transform.localPosition = GoldNumber.transform.localPosition + new Vector3(39f, 0f, 0f);
		}
		GoldNumber.text = iLovePrice.ToString();
	}

	public void FullLove()
	{
		CenterImg.GetComponent<Image>().sprite = FullImg;
	}

	private void CalcTime()
	{
		if (!bwhile)
		{
			return;
		}
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
		if (iNowLove != @int)
		{
			LoadStar();
		}
		if (@int >= 5)
		{
			iRtime = 0;
			BuyLivesRemark.gameObject.SetActive(value: true);
			BuyLivesTimeCalcRemark.gameObject.SetActive(value: false);
			TimeCalcText2.SetActive(value: true);
			FullLove();
			return;
		}
		TimeCalcText2.SetActive(value: false);
		BuyLivesTimeCalcRemark.gameObject.SetActive(value: true);
		BuyLivesRemark.gameObject.SetActive(value: false);
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_FullLoveTime");
		int nowTime = Util.GetNowTime();
		if (nowTime > int2)
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_LoveCount", Singleton<DataManager>.Instance.iLoveMaxAll);
			Singleton<DataManager>.Instance.SaveUserDate("DB_FullLoveTime", 0);
		}
		iRtime = int2 - nowTime;
		int num = 0;
		while (iRtime > Singleton<LevelManager>.Instance.ResTime)
		{
			num++;
			iRtime -= Singleton<LevelManager>.Instance.ResTime;
		}
		Singleton<DataManager>.Instance.SaveUserDate("DB_LoveCount", 5 - num - 1);
	}

	private IEnumerator IERunTime()
	{
		while (true)
		{
			if (!bwhile)
			{
				yield break;
			}
			iRtime--;
			yield return new WaitForSeconds(1f);
			if (!bwhile)
			{
				continue;
			}
			CalcTime();
			LoadTime();
			if (iRtime == 0)
			{
				int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
				if (@int >= 5)
				{
					break;
				}
			}
		}
		if (bwhile)
		{
			LoadStar();
			BuyLivesTimeCalcRemark.gameObject.SetActive(value: false);
		}
	}

	public void LoadTime()
	{
		int num = iRtime / 60;
		if (num < 10)
		{
			Time1.text = "0";
			Time2.text = num.ToString();
		}
		else
		{
			Time1.text = (num / 10).ToString();
			Time2.text = (num % 10).ToString();
		}
		int num2 = iRtime % 60;
		if (num2 < 10)
		{
			Time3.text = "0";
			Time4.text = num2.ToString();
		}
		else
		{
			Time3.text = (num2 / 10).ToString();
			Time4.text = (num2 % 10).ToString();
		}
	}

	private void LoadStar(bool bani = false)
	{
		if (!bani && !bwhile)
		{
			return;
		}
		if (tmpStar != null)
		{
			for (int i = 0; i < 5; i++)
			{
				UnityEngine.Object.Destroy(tmpStar[i].gameObject);
			}
		}
		tmpStar = new GameObject[5];
		int num = iNowLove = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
		for (int j = 0; j < 5; j++)
		{
			tmpStar[j] = Object.Instantiate(StarObj);
			tmpStar[j].transform.SetParent(StarObj_Father.transform, worldPositionStays: false);
			if (num <= j)
			{
				tmpStar[j].GetComponent<Image>().sprite = StarObj_crush;
			}
			else if (bani)
			{
				if (j >= oldlove)
				{
					bool bover = false;
					if (j == 4)
					{
						bover = true;
					}
					BaseUIAnimation.action.BuyLove(tmpStar[j], bover);
				}
				else
				{
					tmpStar[j].SetActive(value: true);
				}
			}
			else
			{
				oldlove = j + 1;
			}
			if (!bani)
			{
				tmpStar[j].SetActive(value: true);
			}
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject gameObject = Util.TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject == null)
			{
				CloseLivesUI();
			}
			else if (gameObject.name.LastIndexOf("BuyLivesUI") < 0)
			{
				CloseLivesUI();
			}
		}
	}

	public void _CloseLivesUI()
	{
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
			StartCoroutine(CallCloseLivesUI());
		}
	}

	public void CloseLivesUI(bool bDouble = false)
	{
		StartCoroutine(CallCloseLivesUI(bDouble));
	}

	private IEnumerator CallCloseLivesUI(bool bDouble = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseUI(bDouble);
	}

	protected override void OnAwake()
	{
		base.OnAwake();
	}

	protected override void OnRelease()
	{
		base.OnRelease();
	}

	public void ClickAskFriends()
	{
		if (FaceBookApi.Action.bLoginState())
		{
			FaceBookApi.Action.AskFriendsFB();
		}
		else
		{
			FaceBookApi.Action.FackBookLogin();
		}
	}

	public void ClickBuyLife()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
		if (@int >= 5)
		{
			return;
		}
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (int2 < iLovePrice)
		{
			EnumUIType[] uiTypes = new EnumUIType[2]
			{
				EnumUIType.BuyGoldUI,
				EnumUIType.BuyLivesUI
			};
			Singleton<UIManager>.Instance.OpenUI(uiTypes);
			CloseLivesUI(bDouble: true);
			return;
		}
		//Analytics.Event("Logpay20");
		FullLove();
		if (InitGame.bChinaVersion)
		{
			PayManager.action.BuyLove(iLovePrice, 5);
			return;
		}
		PayManager.action.BuyLove(iLovePrice, 5 - @int);
		bwhile = false;
		LoadLove(bani: true);
		iRtime = 0;
		BuyLivesRemark.gameObject.SetActive(value: true);
		BuyLivesTimeCalcRemark.gameObject.SetActive(value: false);
		TimeCalcText2.SetActive(value: true);
		if (!InitGame.bChinaVersion && (bool)PayManager.action)
		{
			PayManager.action.LoadGold();
		}
	}

	private void LoadLove(bool bani = false)
	{
		LoadStar(bani);
	}
}
