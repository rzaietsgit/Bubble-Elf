using DG.Tweening;
using System;
using TMPro;
//using Umeng;
using UnityEngine;
using UnityEngine.UI;

public class zaixian : MonoBehaviour
{
	public Sprite[] Icon;

	public TextMeshProUGUI time;

	public GameObject guan;

	private int istarhour;

	private int istarminute;

	private int itime;

	private int istarsecond;

	private bool lingqu1;

	private bool lingqu2;

	private bool lingqu3;

	private bool lingqu4;

	private bool isok;

	private bool isPlay;

	private void Start()
	{
		int month = DateTime.Now.Month;
		int day = DateTime.Now.Day;
		isok = false;
		istarhour = Singleton<DataManager>.Instance.GetUserDataI("DB_zaixian_start_hour" + month + "_" + day);
		istarminute = Singleton<DataManager>.Instance.GetUserDataI("DB_zaixian_start_minute" + month + "_" + day);
		istarsecond = Singleton<DataManager>.Instance.GetUserDataI("DB_zaixian_start_Second" + month + "_" + day);
		itime = Singleton<DataManager>.Instance.GetUserDataI("DB_zaixian_" + month + "_" + day);
		if (Singleton<DataManager>.Instance.GetUserDataI("DB_zaixian_lingqu_4" + month + "_" + day) > 0)
		{
			UnityEngine.Object.DestroyObject(base.gameObject);
		}
		else if (Singleton<DataManager>.Instance.GetUserDataI("DB_zaixian_lingqu_3" + month + "_" + day) > 0)
		{
			lingqu4 = true;
			GetComponent<Image>().sprite = Icon[3];
		}
		else if (Singleton<DataManager>.Instance.GetUserDataI("DB_zaixian_lingqu_2" + month + "_" + day) > 0)
		{
			lingqu3 = true;
			GetComponent<Image>().sprite = Icon[2];
		}
		else if (Singleton<DataManager>.Instance.GetUserDataI("DB_zaixian_lingqu_1" + month + "_" + day) > 0)
		{
			lingqu2 = true;
			GetComponent<Image>().sprite = Icon[1];
		}
		else
		{
			lingqu1 = true;
			GetComponent<Image>().sprite = Icon[0];
		}
	}

	public void Lingqu()
	{
		if (isok)
		{
			//Analytics.Event("LogOnlineAward");
			isok = false;
			int month = DateTime.Now.Month;
			int day = DateTime.Now.Day;
			if (lingqu4)
			{
				Singleton<DataManager>.Instance.SaveUserDate("DB_zaixian_lingqu_4" + month + "_" + day, 1);
				lingqu4 = false;
				UnityEngine.Object.DestroyObject(base.gameObject);
			}
			else if (lingqu3)
			{
				Singleton<DataManager>.Instance.SaveUserDate("DB_zaixian_lingqu_3" + month + "_" + day, 1);
				lingqu3 = false;
				lingqu4 = true;
				GetComponent<Image>().sprite = Icon[3];
			}
			else if (lingqu2)
			{
				Singleton<DataManager>.Instance.SaveUserDate("DB_zaixian_lingqu_2" + month + "_" + day, 1);
				lingqu2 = false;
				lingqu3 = true;
				GetComponent<Image>().sprite = Icon[2];
			}
			else if (lingqu1)
			{
				Singleton<DataManager>.Instance.SaveUserDate("DB_zaixian_lingqu_1" + month + "_" + day, 1);
				lingqu1 = false;
				lingqu2 = true;
				GetComponent<Image>().sprite = Icon[1];
			}
			int hour = DateTime.Now.Hour;
			int minute = DateTime.Now.Minute;
			int userDataI = Singleton<DataManager>.Instance.GetUserDataI("DB_zaixian_start_hour" + month + "_" + day);
			int userDataI2 = Singleton<DataManager>.Instance.GetUserDataI("DB_zaixian_start_minute" + month + "_" + day);
			int userDataI3 = Singleton<DataManager>.Instance.GetUserDataI("DB_zaixian_" + month + "_" + day);
			hour -= userDataI;
			minute -= userDataI2;
			userDataI3 += hour * 60 + minute;
			itime = 0;
			istarhour = DateTime.Now.Hour;
			istarminute = DateTime.Now.Minute;
			Singleton<DataManager>.Instance.SaveUserDate("DB_zaixian_start_hour" + month + "_" + day, istarhour);
			Singleton<DataManager>.Instance.SaveUserDate("DB_zaixian_start_minute" + month + "_" + day, istarminute);
			Singleton<DataManager>.Instance.SaveUserDate("DB_zaixian_" + month + "_" + day, 0);
			guan.SetActive(value: false);
			isPlay = false;
		}
	}

	private void Update()
	{
		int month = DateTime.Now.Month;
		int day = DateTime.Now.Day;
		int hour = DateTime.Now.Hour;
		int minute = DateTime.Now.Minute;
		int second = DateTime.Now.Second;
		hour -= istarhour;
		minute -= istarminute;
		int num = itime + hour * 60 + minute;
		if (isok && !isPlay)
		{
			isPlay = true;
			ButtonPingpong(base.gameObject);
			guan.SetActive(value: true);
		}
		if (lingqu4)
		{
			if (60 - num <= 0)
			{
				isok = true;
				time.text = string.Empty;
			}
			else if (60 - DateTime.Now.Second < 10)
			{
				time.text = 59 - num + ":0" + (60 - DateTime.Now.Second);
			}
			else
			{
				time.text = 59 - num + ":" + (60 - DateTime.Now.Second);
			}
		}
		else if (lingqu3)
		{
			if (30 - num <= 0)
			{
				isok = true;
				time.text = string.Empty;
			}
			else if (60 - DateTime.Now.Second < 10)
			{
				time.text = 29 - num + ":0" + (60 - DateTime.Now.Second);
			}
			else
			{
				time.text = 29 - num + ":" + (60 - DateTime.Now.Second);
			}
		}
		else if (lingqu2)
		{
			if (5 - num <= 0)
			{
				isok = true;
				time.text = string.Empty;
			}
			else if (60 - DateTime.Now.Second < 10)
			{
				time.text = 4 - num + ":0" + (60 - DateTime.Now.Second);
			}
			else
			{
				time.text = 4 - num + ":" + (60 - DateTime.Now.Second);
			}
		}
		else if (lingqu1)
		{
			if (1 - num <= 0)
			{
				isok = true;
				time.text = string.Empty;
			}
			else if (60 - DateTime.Now.Second < 10)
			{
				time.text = "00:0" + (60 - DateTime.Now.Second);
			}
			else
			{
				time.text = 0 + ":" + (60 - DateTime.Now.Second);
			}
		}
	}

	private void ButtonPingpong(GameObject obj)
	{
		if (isPlay)
		{
			obj.transform.DOScale(new Vector3(0.94f, 1.06f, 1f), 1f).SetEase(Ease.InOutSine);
			obj.transform.DOScale(new Vector3(1.06f, 0.94f, 1f), 1.2f).SetEase(Ease.InOutSine).SetDelay(1f)
				.OnComplete(delegate
				{
					ButtonPingpong(obj);
				});
		}
	}
}
