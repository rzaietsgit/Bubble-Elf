using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignRewardUIPanel : SignRewardUIPanelBase
{
	public static SignRewardUIPanel panel;

	public Sprite[] SigImgL;

	private string sTime = string.Empty;

	public Sprite SinOkImg;

	private List<Vector3> btnPos;

	private List<int> LRewardOk;

	public int sid;

	public bool bopenPlay;

	private int iAllDay = 1;

	private int iNowDay;

	private int i31;

	private bool b4;

	private int iLastId;

	private bool bshowWindows;

	private int iTimeClose;

	public override void OnExit()
	{
	}

	public override void InitUI()
	{
		panel = this;
		if (!Util.CheckOnline())
		{
			InitGame.Action.GetTimeNetTime();
			UI.Instance.ClosePanel();
			return;
		}
		InitBuyGoldSonObj();
		string interNetTime = Util.getInterNetTime();
		if (interNetTime != Util.GetNowTime_Day())
		{
		}
		sTime = Util.GetNowTime_Day();
		SignOver();
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SignRewardDay31" + sTime);
		BaseUIAnimation.action.SetLanguageFont("SignRewardUI0", detail.Title_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("SignRewardUI1", detail.TextDemo2_Text, string.Empty);
		detail.Title_Text.fontSize = 40;
		detail.TextDemo2_Text.fontSize = 30;
	}

	private void InitBuyGoldSonObj()
	{
		btnPos = new List<Vector3>();
		LRewardOk = new List<int>();
		iNowDay = getnowDay();
		iNowDay++;
		for (iAllDay = iNowDay; iNowDay > 31; iNowDay -= 31)
		{
			i31++;
		}
		while (iAllDay > 93)
		{
			i31 = 0;
			iAllDay -= 93;
		}
		for (int i = 1; i <= 31; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(detail.sigimgButton_Button.gameObject);
			gameObject.transform.SetParent(detail.jbf_Image.gameObject.transform, worldPositionStays: false);
			int num = int.Parse(Singleton<DataManager>.Instance.dDataSignmap31[i.ToString()]["x"]);
			int num2 = int.Parse(Singleton<DataManager>.Instance.dDataSignmap31[i.ToString()]["y"]);
			gameObject.transform.localPosition = new Vector3(num, 1024 - num2, 0f);
			if (i == 6)
			{
				gameObject.GetComponent<Image>().sprite = SigImgL[0];
				gameObject.GetComponent<Image>().SetNativeSize();
			}
			if (i == 14)
			{
				gameObject.GetComponent<Image>().sprite = SigImgL[1];
				gameObject.GetComponent<Image>().SetNativeSize();
			}
			if (i == 27)
			{
				gameObject.GetComponent<Image>().sprite = SigImgL[2];
				gameObject.GetComponent<Image>().SetNativeSize();
			}
			gameObject.SetActive(value: true);
			if (i == iNowDay)
			{
				detail.HeadIcon_Image.gameObject.transform.SetParent(gameObject.transform, worldPositionStays: false);
				detail.HeadIcon_Image.gameObject.transform.localPosition = new Vector3(0f, 88f, 0f);
			}
			sigimg component = gameObject.GetComponent<sigimg>();
			component.SetId(i);
			if (i > iNowDay)
			{
				component.SetNull();
			}
			else if (i < iNowDay)
			{
				string str = DateTime.Now.AddDays(-iNowDay + i).ToString("yyyyMMdd");
				string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_Sign31" + str, string.Empty);
				if (@string == string.Empty)
				{
					component.SetNull();
				}
				else
				{
					LRewardOk.Add(i);
				}
			}
			btnPos.Add(gameObject.transform.localPosition);
		}
	}

	public void SignOver()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SignRewardDay31" + sTime);
		if (@int == 1)
		{
			detail.SigninButton_Image.sprite = SinOkImg;
		}
	}

	public int getnowDay()
	{
		string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_FistLoginSignRewardDay", string.Empty);
		DateTime now = DateTime.Now;
		DateTime d = DateTime.Parse(@string + " 00:00:00");
		return (now - d).Days;
	}

	public override void OnSigninButton()
	{
		if (!Util.CheckOnline())
		{
			InitGame.Action.GetTimeNetTime();
			return;
		}
		//AdManager.action.opadshowcp(DataManager.PAGE_MAIN);
		Singleton<DataManager>.Instance.SaveUserDate("ToDayAutoSingnin", 0);
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SignRewardDay31" + sTime);
		if (@int == 1)
		{
			return;
		}
		SignOver();
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_SignRewardDay31" + sTime, 1);
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		if (iAllDay == 6 || iAllDay == 14 || iAllDay == 27 || iAllDay == 37 || iAllDay == 45 || iAllDay == 58 || iAllDay == 68 || iAllDay == 76 || iAllDay == 89)
		{
			b4 = true;
			for (int i = 1; i <= 4; i++)
			{
				int num = int.Parse(Singleton<DataManager>.Instance.dDataSignmap31Reward[iAllDay.ToString()]["reward" + i]);
				int num2 = int.Parse(Singleton<DataManager>.Instance.dDataSignmap31Reward[iAllDay.ToString()]["count" + i]);
				list.Add(num);
				list2.Add(num2);
				ChinaPay.action.addRewardAll(num, num2, MapUI.action.gameObject, isShow: false, "free", "signreward");
			}
		}
		else
		{
			int num3 = int.Parse(Singleton<DataManager>.Instance.dDataSignmap31Reward[iAllDay.ToString()]["reward1"]);
			int num4 = int.Parse(Singleton<DataManager>.Instance.dDataSignmap31Reward[iAllDay.ToString()]["count1"]);
			list.Add(num3);
			list2.Add(num4);
			ChinaPay.action.addRewardAll(num3, num4, MapUI.action.gameObject, isShow: false, "free", "signreward");
		}
		InitAndroid.action.GAEvent("Reward:Reward:" + iAllDay);
		BaseUIAnimation.action.ShowProp(list, list2, MapUI.action.gameObject);
		string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_Sign31" + sTime, string.Empty);
		if (@string == string.Empty)
		{
		}
		Singleton<TestScript>.Instance.SetString(DataManager.SDBNO + "DB_Sign31" + sTime, "1");
		StartCoroutine(IESIGClose());
	}

	public IEnumerator IESIGClose()
	{
		yield return new WaitForSeconds(1.8f);
		if (b4)
		{
			yield return new WaitForSeconds(1f);
		}
		UI.Instance.ClosePanel();
	}

	public void ClickSignShowBox(int index)
	{
		bshowWindows = true;
		if (iLastId != 0)
		{
			if (iLastId == index)
			{
				iLastId = 0;
				bshowWindows = false;
			}
			else
			{
				iLastId = index;
			}
		}
		else
		{
			iLastId = index;
		}
		UnityEngine.Debug.Log("ClickSignShowBox   =  3");
		iTimeClose = 3;
		if (index == 6 || index == 14 || index == 27)
		{
			for (int i = 1; i <= 4; i++)
			{
				int num = int.Parse(Singleton<DataManager>.Instance.dDataSignmap31Reward[(index + i31 * 31).ToString()]["reward" + i]);
				int num2 = int.Parse(Singleton<DataManager>.Instance.dDataSignmap31Reward[(index + i31 * 31).ToString()]["count" + i]);
				detail.max_Image.gameObject.transform.Find("icon" + i).gameObject.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num, 138, 114);
				detail.max_Image.gameObject.transform.Find("text" + i).gameObject.GetComponent<Text>().text = "x" + num2;
				detail.max_Image.gameObject.SetActive(value: true);
				detail.min_Image.gameObject.SetActive(value: false);
			}
			detail.max_Image.gameObject.transform.localPosition = btnPos[index - 1];
			if (index >= 27)
			{
				detail.max_Image.gameObject.transform.localPosition -= new Vector3(0f, 220f, 0f);
			}
			else
			{
				detail.max_Image.gameObject.transform.localPosition += new Vector3(0f, 210f, 0f);
			}
		}
		else
		{
			int num3 = int.Parse(Singleton<DataManager>.Instance.dDataSignmap31Reward[(index + i31 * 31).ToString()]["reward1"]);
			int num4 = int.Parse(Singleton<DataManager>.Instance.dDataSignmap31Reward[(index + i31 * 31).ToString()]["count1"]);
			detail.min_Image.gameObject.transform.Find("icon5").gameObject.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num3, 138, 114);
			detail.min_Image.gameObject.transform.Find("text5").gameObject.GetComponent<Text>().text = "x" + num4;
			detail.min_Image.gameObject.SetActive(value: true);
			detail.max_Image.gameObject.SetActive(value: false);
			detail.min_Image.gameObject.transform.localPosition = btnPos[index - 1];
			if (index >= 27)
			{
				detail.min_Image.gameObject.transform.localPosition -= new Vector3(0f, 100f, 0f);
			}
			else
			{
				detail.min_Image.gameObject.transform.localPosition += new Vector3(0f, 100f, 0f);
			}
		}
		SetShowText(index);
	}

	public void SetShowText(int iClickDay)
	{
		int num = 3;
		string text = "����ȡ��";
		if (iClickDay == iNowDay)
		{
			text = "���տ���";
			num = 4;
		}
		else if (iClickDay - 1 == iNowDay)
		{
			text = "�������";
			num = 2;
		}
		else if (iClickDay - 2 == iNowDay)
		{
			text = "�������";
			num = 5;
		}
		else
		{
			if (iClickDay > iNowDay)
			{
				text = iClickDay - iNowDay + "������";
				text = Singleton<DataManager>.Instance.dDataLanguage["SignRewardUI7"][BaseUIAnimation.Language];
				text = text.Replace("A1", (iClickDay - iNowDay).ToString());
				detail.text6_Text.text = text;
				detail.text_Text.text = text;
				return;
			}
			text = "�ѹ�����";
			num = 5;
			for (int i = 0; i < LRewardOk.Count; i++)
			{
				if (iClickDay == LRewardOk[i])
				{
					text = "����ȡ��";
					num = 3;
				}
			}
		}
		text = Singleton<DataManager>.Instance.dDataLanguage["SignRewardUI" + num][BaseUIAnimation.Language];
		detail.text6_Text.text = text;
		detail.text_Text.text = text;
	}
}
