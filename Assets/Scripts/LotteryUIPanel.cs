using DG.Tweening;
using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using ITSoft;
using UnityEngine;
using UnityEngine.UI;

public class LotteryUIPanel : LotteryUIPanelBase
{
	public static LotteryUIPanel panel;

	private string sTime = string.Empty;

	private int iZhuanpanFreeCount;

	private int iZhuanpanFreeCountTime;

	private int iZhuanpanClickCount;

	private int iFreeTime;

	private int old;

	private int ifinger;

	private int ifingercount = 3;

	private bool bStart = true;

	private bool bruning;

	private int Resutl;

	private int iRewardID;

	private int iRewardInumber;

	public override void InitUI()
	{
		panel = this;
		DataManager.bbeibaoFlay = false;
		BaseUIAnimation.action.SetLanguageFont("Zhuanpanwenben", detail.TextDemo4_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("zhuanpantext5", detail.ClickRemark_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("zhuanpantext3", detail.TextDemo5_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("zhuanpantext3", detail.Free_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("zhuanpantext1", detail.TextDemo_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("Zhuanpanwenben", detail.TextDemo2_Text, string.Empty);
        AdsManager.ShowBanner();
        sTime = Util.GetNowTime_Day();
		if (Singleton<DataManager>.Instance.bGooglePay)
		{
			sTime = string.Empty;
			detail.Image6_Image.gameObject.SetActive(value: true);
			detail.ResBtn_Button.gameObject.SetActive(value: true);
			iZhuanpanFreeCount = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LvyeZhuanpan");
			detail.lvyeCount_Text.text = "X" + iZhuanpanFreeCount.ToString();
			BaseUIAnimation.action.SetLanguageFont("zhuanpantext6", detail.BuyText_Text, string.Empty);
		}
		else
		{
			detail.ResBtn_Button.gameObject.SetActive(value: false);
			detail.TextDemo2_Text.gameObject.SetActive(value: false);
			detail.Image6_Image.gameObject.SetActive(value: false);
		}
		RandomList();
		if ((bool)zhuanpan.action)
		{
			zhuanpan.action.CheckGuang();
		}
		if (!Util.CheckOnline())
		{
			UI.Instance.ClosePanel(isShowExit: false);
		}
		if ((bool)zhuanpan.action)
		{
			zhuanpan.action.CheckOnline();
		}
		old = 0;
		ResLotteryUI();
		iZhuanpanClickCount = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_ZhuanpanClickCount" + sTime);
		ResFreeCount();
		LoadOld();
		StartCoroutine(IEShowFingerObj());
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
			detail.R_Image.gameObject.transform.Find("R" + i).GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num, 138, 114);
			if (Singleton<DataManager>.Instance.bGooglePay)
			{
				detail.Text_Image.gameObject.transform.Find("Text" + i).GetComponent<Text>().text = "x" + str;
			}
			else
			{
				detail.Text_Image.gameObject.transform.Find("Text" + i).GetComponent<Text>().text = text;
			}
			if (num <= 9 && num >= 4)
			{
				int num2 = int.Parse(Singleton<DataManager>.Instance.dDatazhuanpan[GetZPIndex(i).ToString()]["inumber"]);
				if (num2 == 100)
				{
					detail.RIcon_Image.gameObject.transform.Find("RIcon" + i).gameObject.SetActive(value: true);
				}
				else
				{
					detail.RIcon_Image.gameObject.transform.Find("RIcon" + i).gameObject.SetActive(value: false);
				}
			}
		}
	}

	public void ResFreeCount()
	{
		ifinger = 0;
		if (iZhuanpanFreeCount > 0)
		{
			detail.ClickRemark_Text.gameObject.SetActive(value: false);
			detail.Count_Text.gameObject.SetActive(value: true);
			detail.Count_Text.text = "扣除1次";
			BaseUIAnimation.action.SetLanguageFont("zhuanpantext4", detail.Count_Text, string.Empty);
			return;
		}
		detail.ClickRemark_Text.gameObject.SetActive(value: true);
		detail.Count_Text.gameObject.SetActive(value: false);
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
			detail.ClickRemark_Text.text = empty;
			BaseUIAnimation.action.SetLanguageFont("zhuanpantext5", detail.ClickRemark_Text, string.Empty);
		}
		else if (num == 1)
		{
			empty = "金币 " + num2;
			detail.ClickRemark_Text.text = empty;
		}
		else
		{
			empty = "钻石 " + num2;
			detail.ClickRemark_Text.text = empty;
			BaseUIAnimation.action.SetLanguageFont(empty, detail.ClickRemark_Text, string.Empty);
		}
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
						detail.bg1_Image.gameObject.transform.Find("b" + (int.Parse(@string.Split(',')[num2]) + 1)).gameObject.SetActive(value: true);
						num++;
					}
				}
			}
			else
			{
				detail.bg1_Image.gameObject.transform.Find("b" + (int.Parse(@string) + 1)).gameObject.SetActive(value: true);
			}
		}
		if (num == 8)
		{
			ClearOld();
			Singleton<DataManager>.Instance.SaveUserDate("DB_ZhuanpanOldData" + sTime, string.Empty);
		}
	}

	private IEnumerator IEShowFingerObj()
	{
		yield return new WaitForSeconds(0.1f);
		while (true)
		{
			yield return new WaitForSeconds(1f);
			if (ifinger > ifingercount)
			{
				detail.finger_SkeletonAnimation.gameObject.SetActive(value: true);
			}
			ifinger++;
		}
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

	private void ClearOld()
	{
		for (int i = 1; i <= 8; i++)
		{
			detail.bg1_Image.gameObject.transform.Find("b" + i).gameObject.SetActive(value: false);
		}
		RandomList(bRes: true);
		ResLotteryUI();
	}

	public override void OnS_Button()
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
			s.Append(detail.Image2_Image.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, -2520f), 2.5f).SetEase(Ease.InQuad)).Append(detail.Image2_Image.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, -745 - 45 * Resutl), 1.2f).SetEase(Ease.OutQuad)).Append(detail.Image2_Image.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, -45 * Resutl - 1), 0.8f).SetEase(Ease.InOutSine))
				.OnComplete(delegate
				{
					Over();
				});
			detail.Image4_Image.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, -5f), 0.1f);
			detail.Image4_Image.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, 8f), 0.15f).SetDelay(0.1f);
			detail.Image4_Image.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, -10f), 0.15f).SetDelay(0.25f);
			for (int i = 0; i < 19; i++)
			{
				detail.Image4_Image.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, 10f), 0.1f).SetDelay(0.4f + (float)i * 0.2f);
				detail.Image4_Image.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, -10f), 0.1f).SetDelay(0.5f + (float)i * 0.2f);
			}
			old = 45 * Resutl;
			detail.Image4_Image.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, -10f), 0.1f).SetDelay(3.5f);
			detail.Image4_Image.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, 9f), 0.1f).SetDelay(3.6f);
			detail.Image4_Image.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, -7f), 0.1f).SetDelay(3.7f);
			detail.Image4_Image.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, 5f), 0.1f).SetDelay(3.8f);
			detail.Image4_Image.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, -3f), 0.1f).SetDelay(3.9f);
			detail.Image4_Image.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, 1f), 0.1f).SetDelay(4f);
			detail.Image4_Image.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.1f).SetDelay(4.1f);
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
			detail.lvyeCount_Text.text = "X" + iZhuanpanFreeCount.ToString();
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
				UI.Instance.OpenPanel(UIPanelType.ChinaShop);
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
				return false;
			}
			PayManager.action.PayZhuanpan(num2, 0, 1);
		}
		iZhuanpanClickCount++;
		Singleton<DataManager>.Instance.SaveUserDate("DB_ZhuanpanClickCount" + sTime, iZhuanpanClickCount);
		//Analytics.Event("zhuanpanData", "Shoufei" + iZhuanpanClickCount);
		return true;
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
		ChinaPay.action.addRewardAll(iRewardID, iRewardInumber, base.gameObject);
		LoadOld();
		//AdManager.action.opadshowcp(DataManager.PAGE_MAIN);
		bruning = false;
		//if (Application.platform == RuntimePlatform.Android && Singleton<DataManager>.Instance.bGooglePay)
		//{
		//	AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		//	androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		//	AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		//	@static.Call("showVideo", "3");
		//}
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

	public override void OnResBtn()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (20 > @int)
		{
			UI.Instance.OpenPanel(UIPanelType.ChinaShop);
			return;
		}
		PayManager.action.PayZhuanpan(20, 0, 1);
		ClearOld();
		Singleton<DataManager>.Instance.SaveUserDate("DB_ZhuanpanOldData" + sTime, string.Empty);
	}
}
