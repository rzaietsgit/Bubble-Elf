using System;
using System.Collections;
using EasyMobile;
using ITSoft;
using UnityEngine;

public class Reward24UIPanel : Reward24UIPanelBase
{
	public static Reward24UIPanel panel;

	public Sprite[] btnIcon;

	private bool bGet;

	private bool isClicklingquBtn;

	public override void InitUI()
	{
		detail.UI_RawImage.transform.localPosition = new Vector3(0f, 0f, 0f);
		panel = this;
		detail.lingquButton_Button.enabled = false;
		detail.lingquButton_Image.sprite = btnIcon[0];
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_24hReward") == 0)
		{
			AutoTIme();
		}
        AdsManager.ShowBanner();
        BaseUIAnimation.action.SetLanguageFont("Reward24UIBtn", detail.TextDemo4_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("Reward24UITitle", detail.Reward24UITitle_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("Reward24UI1", detail.TextDemo5_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("Reward24UI2", detail.TextDemo2_Text, string.Empty);
	}

	private void AutoTIme()
	{
		int num = 86400;
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_24hRewardTime");
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_24hReward") == 0)
		{
			int nowTime = Util.GetNowTime();
			if (nowTime - @int > num)
			{
				detail.TextDemo2_Text.gameObject.SetActive(value: false);
				bGet = true;
				detail.lingquButton_Button.enabled = true;
				detail.lingquButton_Image.sprite = btnIcon[1];
				detail.UI_RawImage.transform.localPosition = new Vector3(0f, 50f, 0f);
			}
			else
			{
				int iTime = num - (nowTime - @int);
				_SetTime(iTime);
				StartCoroutine(IESetTime(iTime));
			}
		}
		else
		{
			bGet = false;
			detail.lingquButton_Button.enabled = false;
			detail.lingquButton_Image.sprite = btnIcon[0];
		}
	}

	private IEnumerator IESetTime(int iTime)
	{
		yield return new WaitForSeconds(1f);
		bool b = true;
		while (b)
		{
			if (iTime < 0)
			{
				b = false;
				AutoTIme();
			}
			else
			{
				iTime--;
			}
			_SetTime(iTime);
			yield return new WaitForSeconds(1f);
		}
	}

	private void _SetTime(int iTime)
	{
		if (iTime < 0)
		{
			detail.TextDemo2_Text.gameObject.SetActive(value: false);
			return;
		}
		if (iTime < 1)
		{
			iTime = 1;
		}
		TimeSpan timeSpan = new TimeSpan(0, 0, iTime);
		int minutes = timeSpan.Minutes;
		int hours = timeSpan.Hours;
		int seconds = timeSpan.Seconds;
		string text = minutes + string.Empty;
		string text2 = hours + string.Empty;
		string text3 = seconds + string.Empty;
		if (minutes < 10)
		{
			text = "0" + text;
		}
		if (hours < 10)
		{
			text2 = "0" + text2;
		}
		if (seconds < 10)
		{
			text3 = "0" + text3;
		}
		detail.time_Text.text = text2 + ":" + text + ":" + text3;
	}

	public override void OnlingquButton()
	{
		isClicklingquBtn = true;
		if (bGet && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_24hReward") == 0)
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_24hReward", 1);
			Reward();
			UI.Instance.ClosePanel();
		}
	}

	private void Reward()
	{
		ChinaPay.action.CallReward24();
		ClickBtnFun._24Obj.res24();
	}

	public override void OnExit()
	{
	}
}
