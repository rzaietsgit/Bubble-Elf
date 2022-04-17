using UnityEngine;

public class LocalNotificationAndroid
{
	public enum NotificationExecuteMode
	{
		Inexact,
		Exact,
		ExactAndAllowWhileIdle
	}

	public static void SendNotification(long delay, string title, string message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "", NotificationExecuteMode executeMode = NotificationExecuteMode.Inexact)
	{
	}

	public static void CancelNotification(int id)
	{
	}
}
