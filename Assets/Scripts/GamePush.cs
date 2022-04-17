using System;
using System.Collections;
using UnityEngine;

public class GamePush : MonoBehaviour
{
	public static GamePush action;

	public bool InitOver;

	public static int iPowerFen = 3;

	public static int iPowerMiao = iPowerFen * 60;

	public bool bPowerPush;

	public static int iPushIDCount = 1;

	private void Start()
	{
		action = this;
		InitOver = true;
	}

	private void Update()
	{
	}

	private void Awake()
	{
		if (!action)
		{
			CleanNotification();
			action = this;
			UnityEngine.Object.DontDestroyOnLoad(this);
		}
	}

	public void NotificationMessage(string message, int hour, bool isRepeatDay, int p_id = 0)
	{
	}

	public void NotificationMessage(string message, int year, int month, int day, int hour, int minute, int second, bool isRepeatDay, int p_id = 0)
	{
	}

	public void NotificationMessage(string message, DateTime newDate, bool isRepeatDay, int p_id = 0)
	{
	}

	private void LocalNotificationPushAndroid(string message, int itime)
	{
		LocalNotificationAndroid.SendNotification(itime, "泡泡精灵传奇", message, new Color32(byte.MaxValue, 68, 68, byte.MaxValue), sound: true, vibrate: true, lights: true, string.Empty);
	}

	public void PushManager()
	{
	}

	private void PlayTip()
	{
	}

	private DateTime GetTime(string timeStamp)
	{
		DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
		long ticks = long.Parse(timeStamp + "0000000");
		TimeSpan value = new TimeSpan(ticks);
		return dateTime.Add(value);
	}

	public void PushPowerTip()
	{
		int num = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
		int loveInfinite = Singleton<UserManager>.Instance.getLoveInfinite();
		if (loveInfinite >= Singleton<DataManager>.Instance.iLoveUse)
		{
			num = Singleton<DataManager>.Instance.iLoveMaxAll;
		}
		if (num < Singleton<DataManager>.Instance.iLoveMaxAll)
		{
			bPowerPush = true;
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_FullLoveTime");
			int num2 = @int - Util.GetNowTime();
			UnityEngine.Debug.Log("jypush jyjy你的体力充满了，来玩吧 iPushTime=" + num2);
			LocalNotificationPushAndroid("你的体力充满了，来玩吧！", num2);
		}
	}

	public void CheckHaopingReward()
	{
		if (InitGame.bChinaVersion && (bool)MapUI.action)
		{
			MapUI.action.ChinaHaopingCheck();
		}
	}

	private void OnApplicationPause(bool paused)
	{
		if (paused)
		{
			int nowTime = Util.GetNowTime();
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_showBannerHGTimeHouTai", nowTime);
			return;
		}
		if ((bool)GooglePlay3Panel.panel)
		{
			GooglePlay3Panel.panel.ResGg();
		}
		if ((bool)InitAndroid.action)
		{
			InitAndroid.action.showBannerHGHouTai();
		}
		CleanNotification();
		if (!Util.CheckOnline())
		{
			InitGame.Action.GetTimeNetTime();
		}
		CheckHaopingReward();
		StartCoroutine(ieSetAdState());
	}

	private IEnumerator ieSetAdState()
	{
		yield return new WaitForSeconds(2f);
		Singleton<DataManager>.Instance.isrewardad = false;
	}

	public void CleanNotification()
	{
		ClearAndoridPush();
	}

	public void ClearAndoridPush()
	{
		if ((bool)action)
		{
			int num = iPushIDCount;
			iPushIDCount = 1;
			for (int i = 1; i <= num; i++)
			{
				UnityEngine.Debug.Log("jy GamePush ClearAndoridPush= " + i);
				LocalNotificationAndroid.CancelNotification(i);
			}
		}
	}
}
