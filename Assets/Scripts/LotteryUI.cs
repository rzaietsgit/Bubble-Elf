using DG.Tweening;
using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using ITSoft;
using UnityEngine;
using UnityEngine.UI;

public class LotteryUI : BaseUI
{
	public static LotteryUI action;

	public GameObject CloseBtn;

	public GameObject ClickBtn;

	public GameObject ZhuanpanObj;

	public GameObject ZhizhengObj;

	public GameObject LotteryUISonObj;

	public GameObject LotteryUIFatherObj;

	public Text TimeUI;

	public GameObject LotteryUIIconObjFather;

	public GameObject LotteryUITextObjFather;

	public GameObject LotteryUIBgObjFather;

	public GameObject LotteryUIRIconObjFather;

	public Text Zhuanpanwenben;

	public Text ClickRemark;

	public Text FreeCount;

	public Text dianwotext;

	public Text dianwotext2;

	public Text TextLvyeCount;

	public Text zhuanpantext1;

	public Text zhuanpantext2;

	public GameObject GoogleResBtnObj;

	public Text GoogleResBtnText;

	public GameObject LvyeObj;

	public GameObject fingerObj;

	private string sTime = string.Empty;

	private int ifinger;

	private int ifingercount = 3;

	private List<int> quanzhong;

	private int fTime = 43200;

	private int iZhuanpanFreeCount;

	private int iZhuanpanFreeCountTime;

	private int iZhuanpanClickCount;

	private int iFreeTime;

	private int Resutl;

	private int old;

	private bool bStart = true;

	private int iRewardID;

	private int iRewardInumber;

	private bool bruning;

	private int TempResult;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.LotteryUI;
	}

	private void Start()
	{
		DataManager.bbeibaoFlay = false;
		BaseUIAnimation.action.SetLanguageFont("Zhuanpanwenben", Zhuanpanwenben, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("zhuanpantext5", ClickRemark, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("zhuanpantext3", dianwotext, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("zhuanpantext3", dianwotext2, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("zhuanpantext1", zhuanpantext1, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("Zhuanpanwenben", zhuanpantext2, string.Empty);
        AdsManager.ShowBanner();
        sTime = Util.GetNowTime_Day();
		if (Singleton<DataManager>.Instance.bGooglePay)
		{
			sTime = string.Empty;
			LvyeObj.SetActive(value: true);
			GoogleResBtnObj.SetActive(value: true);
			iZhuanpanFreeCount = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LvyeZhuanpan");
			TextLvyeCount.text = "X" + iZhuanpanFreeCount.ToString();
			BaseUIAnimation.action.SetLanguageFont("zhuanpantext6", GoogleResBtnText, string.Empty);
		}
		else
		{
			GoogleResBtnObj.SetActive(value: false);
			zhuanpantext2.gameObject.SetActive(value: false);
			LvyeObj.SetActive(value: false);
		}
		RandomList();
		action = this;
		if ((bool)zhuanpan.action)
		{
			zhuanpan.action.CheckGuang();
		}
		if (!Util.CheckOnline())
		{
			CloseUI();
		}
		if ((bool)zhuanpan.action)
		{
			zhuanpan.action.CheckOnline();
		}
		old = 0;
		ResLotteryUI();
		iZhuanpanClickCount = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_ZhuanpanClickCount" + sTime);
		ResFreeCount();
		LoadTestData();
		LoadOld();
		StartCoroutine(IEShowFingerObj());
	}

	public void ClickGoogleResBtn()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (20 > @int)
		{
			EnumUIType[] uiTypes = new EnumUIType[2]
			{
				EnumUIType.BuyGoldUI,
				EnumUIType.LotteryUI
			};
			Singleton<UIManager>.Instance.OpenUI(uiTypes);
			CloseUI();
		}
		else
		{
			PayManager.action.PayZhuanpan(20, 0, 1);
			ClearOld();
			Singleton<DataManager>.Instance.SaveUserDate("DB_ZhuanpanOldData" + sTime, string.Empty);
		}
	}

	public void ResLotteryUI()
	{
		for (int i = 1; i <= 8; i++)
		{
			int num = int.Parse(Singleton<DataManager>.Instance.dDatazhuanpan[GetZPIndex(i).ToString()]["icon"]);
			string text = Singleton<DataManager>.Instance.dDatazhuanpan[GetZPIndex(i).ToString()]["name"];
			if (InitGame.bEnios)
			{
				text = Singleton<DataManager>.Instance.dDatazhuanpan[GetZPIndex(i).ToString()]["nameen"];
			}
			string str = Singleton<DataManager>.Instance.dDatazhuanpan[GetZPIndex(i).ToString()]["inumber"];
			LotteryUIIconObjFather.transform.Find("R" + i).GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num, 138, 114);
			if (Singleton<DataManager>.Instance.bGooglePay)
			{
				LotteryUITextObjFather.transform.Find("Text" + i).GetComponent<Text>().text = "x" + str;
			}
			else
			{
				LotteryUITextObjFather.transform.Find("Text" + i).GetComponent<Text>().text = text;
			}
			if (num <= 9 && num >= 4)
			{
				int num2 = int.Parse(Singleton<DataManager>.Instance.dDatazhuanpan[GetZPIndex(i).ToString()]["inumber"]);
				if (num2 == 100)
				{
					LotteryUIRIconObjFather.transform.Find("R" + i).gameObject.SetActive(value: true);
				}
				else
				{
					LotteryUIRIconObjFather.transform.Find("R" + i).gameObject.SetActive(value: false);
				}
			}
		}
	}

	public void RandomList(bool bRes = false)
	{
		string text = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_ZhuanpansResultList" + sTime, string.Empty);
		List<int> list = new List<int>();
		if (text == string.Empty || bRes)
		{
			if (bRes)
			{
				text = string.Empty;
			}
			do
			{
				int num = Random.Range(1, 11);
				bool flag = true;
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] == num)
					{
						flag = false;
						i = 100;
					}
				}
				if (flag)
				{
					text = text + num + ",";
					list.Add(num);
				}
			}
			while (list.Count != 8);
		}
		Singleton<TestScript>.Instance.SetString(DataManager.SDBNO + "DB_ZhuanpansResultList" + sTime, text);
	}

	public int GetZPIndex(int i)
	{
		string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_ZhuanpansResultList" + sTime, string.Empty);
		if (@string == string.Empty)
		{
			RandomList();
			@string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_ZhuanpansResultList" + sTime, string.Empty);
		}
		return int.Parse(@string.Split(',')[i - 1]);
	}

	private IEnumerator IEShowFingerObj()
	{
		yield return new WaitForSeconds(0.1f);
		while (true)
		{
			yield return new WaitForSeconds(1f);
			if (ifinger > ifingercount)
			{
				fingerObj.SetActive(value: true);
			}
			ifinger++;
		}
	}

	private void ClearOld()
	{
		for (int i = 1; i <= 8; i++)
		{
			LotteryUIBgObjFather.transform.Find("b" + i).gameObject.SetActive(value: false);
		}
		RandomList(bRes: true);
		ResLotteryUI();
	}

	public void LoadOld()
	{
		int num = 0;
		string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_ZhuanpanOldData" + sTime, string.Empty);
		if (@string != string.Empty)
		{
			if (@string.LastIndexOf(',') >= 0)
			{
				for (int num2 = 0; num2 < @string.Split(',').Length; num2++)
				{
					if (@string.Split(',')[num2] != string.Empty)
					{
						LotteryUIBgObjFather.transform.Find("b" + (int.Parse(@string.Split(',')[num2]) + 1)).gameObject.SetActive(value: true);
						num++;
					}
				}
			}
			else
			{
				LotteryUIBgObjFather.transform.Find("b" + (int.Parse(@string) + 1)).gameObject.SetActive(value: true);
			}
		}
		if (num == 8)
		{
			ClearOld();
			Singleton<DataManager>.Instance.SaveUserDate("DB_ZhuanpanOldData" + sTime, string.Empty);
		}
	}

	private void LoadTestData()
	{
	}

	private IEnumerator AutoMove()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.1f);
			Transform transform = LotteryUIFatherObj.transform;
			Vector3 localPosition = LotteryUIFatherObj.transform.localPosition;
			float x = localPosition.x;
			Vector3 localPosition2 = LotteryUIFatherObj.transform.localPosition;
			float y = localPosition2.y + 2f;
			Vector3 localPosition3 = LotteryUIFatherObj.transform.localPosition;
			transform.localPosition = new Vector3(x, y, localPosition3.z);
			Vector3 localPosition4 = LotteryUIFatherObj.transform.localPosition;
			if (localPosition4.y >= 1048f)
			{
				Transform transform2 = LotteryUIFatherObj.transform;
				Vector3 localPosition5 = LotteryUIFatherObj.transform.localPosition;
				float x2 = localPosition5.x;
				Vector3 localPosition6 = LotteryUIFatherObj.transform.localPosition;
				transform2.localPosition = new Vector3(x2, 3f, localPosition6.z);
			}
		}
	}

	private int RandomQuanzhong()
	{
		List<int> list = new List<int>();
		string text = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_ZhuanpanOldData" + sTime, string.Empty);
		if (text != string.Empty)
		{
			if (text.LastIndexOf(',') >= 0)
			{
				for (int num = 0; num < text.Split(',').Length; num++)
				{
					if (text.Split(',')[num] != string.Empty)
					{
						list.Add(int.Parse(text.Split(',')[num]));
					}
				}
			}
			else
			{
				list.Add(int.Parse(text));
			}
		}
		if (list.Count == 8)
		{
			list.Clear();
			Singleton<DataManager>.Instance.SaveUserDate("DB_ZhuanpanOldData" + sTime, string.Empty);
			text = string.Empty;
		}
		if (iZhuanpanClickCount > 4)
		{
			iZhuanpanClickCount = 4;
		}
		List<int> list2 = new List<int>();
		string key = "imax" + iZhuanpanClickCount;
		for (int i = 1; i <= 8; i++)
		{
			int item = int.Parse(Singleton<DataManager>.Instance.dDatazhuanpan[GetZPIndex(i).ToString()][key]);
			for (int j = 0; j < list.Count; j++)
			{
				if (i == list[j] + 1)
				{
					item = 1;
					break;
				}
			}
			list2.Add(item);
		}
		int num2 = 0;
		for (int k = 0; k < list2.Count; k++)
		{
			num2 += list2[k] + 1;
		}
		int num3 = Random.Range(0, num2);
		num2 = 0;
		for (int l = 0; l < list2.Count; l++)
		{
			num2 += list2[l] + 1;
			if (num3 > num2)
			{
				continue;
			}
			if (list.Count == 7)
			{
				for (int m = 0; m < 8; m++)
				{
					bool flag = true;
					for (int n = 0; n < list.Count; n++)
					{
						if (m == list[n])
						{
							flag = false;
						}
					}
					if (flag)
					{
						l = m;
					}
				}
			}
			else
			{
				for (int num4 = 0; num4 < list.Count; num4++)
				{
					if (l == list[num4])
					{
						return -1;
					}
				}
			}
			text = ((!(text == string.Empty)) ? (text + "," + l) : l.ToString());
			Singleton<DataManager>.Instance.SaveUserDate("DB_ZhuanpanOldData" + sTime, text);
			return l;
		}
		return -1;
	}

	public void ResFreeCount()
	{
		ifinger = 0;
		if (iZhuanpanFreeCount > 0)
		{
			ClickRemark.gameObject.SetActive(value: false);
			FreeCount.gameObject.SetActive(value: true);
			FreeCount.text = "扣除1次";
			BaseUIAnimation.action.SetLanguageFont("zhuanpantext4", FreeCount, string.Empty);
			return;
		}
		ClickRemark.gameObject.SetActive(value: true);
		FreeCount.gameObject.SetActive(value: false);
		if (iZhuanpanClickCount > 4)
		{
			iZhuanpanClickCount = 4;
		}
		int num = int.Parse(Singleton<DataManager>.Instance.dDatazhuanpanMoney[iZhuanpanClickCount.ToString()]["Type"]);
		string text = Singleton<DataManager>.Instance.dDatazhuanpanMoney[iZhuanpanClickCount.ToString()]["UpText"];
		int num2 = int.Parse(Singleton<DataManager>.Instance.dDatazhuanpanMoney[iZhuanpanClickCount.ToString()]["money"]);
		string empty = string.Empty;
		if (Singleton<DataManager>.Instance.bGooglePay)
		{
			empty = "钻石 " + num2;
			ClickRemark.text = empty;
			BaseUIAnimation.action.SetLanguageFont("zhuanpantext5", ClickRemark, string.Empty);
		}
		else if (num == 1)
		{
			empty = "金币 " + num2;
			ClickRemark.text = empty;
		}
		else
		{
			empty = "钻石 " + num2;
			ClickRemark.text = empty;
			BaseUIAnimation.action.SetLanguageFont(empty, ClickRemark, string.Empty);
		}
	}

	private void Awake()
	{
		Canvas component = base.gameObject.transform.GetComponent<Canvas>();
		component.renderMode = RenderMode.ScreenSpaceCamera;
		component.worldCamera = MapUI.action.MapUISceneCamera.GetComponent<Camera>();
	}

	public void _CloseLotteryUI()
	{
		if (!DataManager.bbeibaoFlay && !bruning && BaseUIAnimation.bClickButton)
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

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (ifinger >= 0)
			{
				ifinger = 0;
			}
			fingerObj.SetActive(value: false);
		}
	}

	public bool CheckZhuan()
	{
		iZhuanpanClickCount = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_ZhuanpanClickCount" + sTime);
		if (iZhuanpanClickCount > 4)
		{
			iZhuanpanClickCount = 4;
		}
		int num = int.Parse(Singleton<DataManager>.Instance.dDatazhuanpanMoney[iZhuanpanClickCount.ToString()]["Type"]);
		int num2 = int.Parse(Singleton<DataManager>.Instance.dDatazhuanpanMoney[iZhuanpanClickCount.ToString()]["money"]);
		if (Singleton<DataManager>.Instance.bGooglePay && iZhuanpanFreeCount > 0)
		{
			iZhuanpanFreeCount--;
			TextLvyeCount.text = "X" + iZhuanpanFreeCount.ToString();
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_LvyeZhuanpan", iZhuanpanFreeCount);
			if ((bool)zhuanpan.action)
			{
				zhuanpan.action.CheckGuang();
			}
		}
		else if (num == 1)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GB");
			if (num2 > @int)
			{
				EnumUIType[] array = new EnumUIType[2];
				Singleton<DataManager>.Instance.ChinaShopOpendaoju = true;
				Singleton<DataManager>.Instance.ChinaShopOpenZuanshi = false;
				array[0] = EnumUIType.BuyGoldUI;
				array[1] = EnumUIType.LotteryUI;
				Singleton<UIManager>.Instance.OpenUI(array);
				CloseUI();
				return false;
			}
			PayManager.action.PayZhuanpan(0, num2, 1);
		}
		else
		{
			int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
			if (num2 > int2)
			{
				EnumUIType[] uiTypes = new EnumUIType[2]
				{
					EnumUIType.BuyGoldUI,
					EnumUIType.LotteryUI
				};
				Singleton<UIManager>.Instance.OpenUI(uiTypes);
				CloseUI();
				return false;
			}
			PayManager.action.PayZhuanpan(num2, 0, 1);
		}
		iZhuanpanClickCount++;
		Singleton<DataManager>.Instance.SaveUserDate("DB_ZhuanpanClickCount" + sTime, iZhuanpanClickCount);
		//Analytics.Event("zhuanpanData", "Shoufei" + iZhuanpanClickCount);
		return true;
	}

	public void ClickZhuan()
	{
		if (bStart && CheckZhuan())
		{
			bruning = true;
			SoundController.action.playNow("ui_wheel_button_down", NowPlay: true);
			SoundController.action.playNow("ui_wheel_rot_start", NowPlay: true);
			StartCoroutine(Playmp3());
			//Analytics.Event("PayZhuan" + iZhuanpanClickCount);
			bStart = false;
			for (Resutl = RandomQuanzhong(); Resutl == -1; Resutl = RandomQuanzhong())
			{
			}
			iRewardID = Resutl + 1;
			//Analytics.Event("zhuanpanData", "Reward_" + Resutl);
			iRewardInumber = int.Parse(Singleton<DataManager>.Instance.dDatazhuanpan[GetZPIndex(iRewardID).ToString()]["inumber"]);
			iRewardID = int.Parse(Singleton<DataManager>.Instance.dDatazhuanpan[GetZPIndex(iRewardID).ToString()]["icon"]);
			Resutl = 8 - Resutl;
			Sequence s = DOTween.Sequence();
			ifinger = -100;
			s.Append(ZhuanpanObj.transform.DOLocalRotate(new Vector3(0f, 0f, -2520f), 2.5f).SetEase(Ease.InQuad)).Append(ZhuanpanObj.transform.DOLocalRotate(new Vector3(0f, 0f, -745 - 45 * Resutl), 1.2f).SetEase(Ease.OutQuad)).Append(ZhuanpanObj.transform.DOLocalRotate(new Vector3(0f, 0f, -45 * Resutl - 1), 0.8f).SetEase(Ease.InOutSine))
				.OnComplete(delegate
				{
					Over();
				});
			ZhizhengObj.transform.DOLocalRotate(new Vector3(0f, 0f, -5f), 0.1f);
			ZhizhengObj.transform.DOLocalRotate(new Vector3(0f, 0f, 8f), 0.15f).SetDelay(0.1f);
			ZhizhengObj.transform.DOLocalRotate(new Vector3(0f, 0f, -10f), 0.15f).SetDelay(0.25f);
			for (int i = 0; i < 19; i++)
			{
				ZhizhengObj.transform.DOLocalRotate(new Vector3(0f, 0f, 10f), 0.1f).SetDelay(0.4f + (float)i * 0.2f);
				ZhizhengObj.transform.DOLocalRotate(new Vector3(0f, 0f, -10f), 0.1f).SetDelay(0.5f + (float)i * 0.2f);
			}
			old = 45 * Resutl;
			ZhizhengObj.transform.DOLocalRotate(new Vector3(0f, 0f, -10f), 0.1f).SetDelay(3.5f);
			ZhizhengObj.transform.DOLocalRotate(new Vector3(0f, 0f, 9f), 0.1f).SetDelay(3.6f);
			ZhizhengObj.transform.DOLocalRotate(new Vector3(0f, 0f, -7f), 0.1f).SetDelay(3.7f);
			ZhizhengObj.transform.DOLocalRotate(new Vector3(0f, 0f, 5f), 0.1f).SetDelay(3.8f);
			ZhizhengObj.transform.DOLocalRotate(new Vector3(0f, 0f, -3f), 0.1f).SetDelay(3.9f);
			ZhizhengObj.transform.DOLocalRotate(new Vector3(0f, 0f, 1f), 0.1f).SetDelay(4f);
			ZhizhengObj.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.1f).SetDelay(4.1f);
		}
	}

	private IEnumerator Playmp3()
	{
		yield return new WaitForSeconds(3f);
		SoundController.action.playNow("ui_wheel_rot_end_1", NowPlay: true);
	}

	private void Over()
	{
		ifinger = 0;
		bStart = true;
		ResFreeCount();
		ChinaPay.action.addRewardAll(iRewardID, iRewardInumber, base.gameObject, isShow: true, "buy", "lottery");
		LoadOld();
		//AdManager.action.opadshowcp(DataManager.PAGE_MAIN);
		bruning = false;
		
	}

	private IEnumerator IEZhuanQuanQuan()
	{
		yield return new WaitForSeconds(0.1f);
		int iZCount = 0;
		int iIndex = 0;
		bool b = true;
		while (b)
		{
			yield return new WaitForSeconds(0.01f);
			iIndex -= 30 - iZCount * 10;
			if (iIndex < -360)
			{
				iIndex = 0;
				iZCount++;
			}
			ZhuanpanObj.transform.localRotation = Quaternion.Euler(0f, 0f, iIndex);
			if (iZCount >= 3)
			{
				b = false;
			}
		}
	}

	public void aa()
	{
	}
}
