using System;
using System.Globalization;
using System.Net;
using UnityEngine;

public class Util : MonoBehaviour
{
	public static GameObject TouchChecker(Vector3 mouseposition)
	{
		if ((bool)Physics2D.OverlapPoint(mouseposition))
		{
			GameObject gameObject = Physics2D.OverlapPoint(mouseposition).gameObject;
			DebugClick(gameObject);
			return gameObject;
		}
		return null;
	}

	public static bool GetNowOpenUI()
	{
		if ((bool)BuyBubbleUI.action)
		{
			return true;
		}
		if ((bool)NowBuyBubbleUI.action)
		{
			return true;
		}
		if ((bool)BuyGangUI.action)
		{
			return true;
		}
		if ((bool)BuySkillUI.action)
		{
			return true;
		}
		if ((bool)BuyGoldUIByDiamonds.action)
		{
			return true;
		}
		if ((bool)BuyGoldUI.action)
		{
			return true;
		}
		if ((bool)Reward24UI.action)
		{
			return true;
		}
		if ((bool)ChinaShopUI.action)
		{
			return true;
		}
		if ((bool)ChinaShopUI.action)
		{
			return true;
		}
		if ((bool)MapRewardUI.action)
		{
			return true;
		}
		if ((bool)LotteryUI.action)
		{
			return true;
		}
		if ((bool)DayTaskUI.action)
		{
			return true;
		}
		if ((bool)TipWinUI.action)
		{
			return true;
		}
		if ((bool)QuitUI.action)
		{
			return true;
		}
		if ((bool)BuyLivesUI.action)
		{
			return true;
		}
		if ((bool)WinUI.action)
		{
			return true;
		}
		if ((bool)PlayUI.action)
		{
			return true;
		}
		if ((bool)LoseUI.action)
		{
			return true;
		}
		if ((bool)LanguageUI.action)
		{
			return true;
		}
		if ((bool)SigninUI.action)
		{
			return true;
		}
		if ((bool)BuySkillUIChina.action)
		{
			return true;
		}
		if ((bool)BuyLivesChinaUI.action)
		{
			return true;
		}
		if ((bool)SaleAdUI.action)
		{
			return true;
		}
		if ((bool)aboutChinaUI.action)
		{
			return true;
		}
		if ((bool)SkillTipUI.action)
		{
			return true;
		}
		if ((bool)EnBuyLives.action)
		{
			return true;
		}
		if ((bool)EnConnectFacebook.action)
		{
			return true;
		}
		if ((bool)EnFirstInvite.action)
		{
			return true;
		}
		if ((bool)EnSendLives.action)
		{
			return true;
		}
		if ((bool)EnSuperSale.action)
		{
			return true;
		}
		if ((bool)cdkeyUI.action)
		{
			return true;
		}
		if ((bool)PackSkillIconUI.action)
		{
			return true;
		}
		if ((bool)NewTaskUI.action)
		{
			return true;
		}
		if ((bool)HuaShopUI.action)
		{
			return true;
		}
		if ((bool)HuaHelpUIPanel.panel)
		{
			return true;
		}
		return false;
	}

	public static bool GetbForced_guidance()
	{
		if (Singleton<DataManager>.Instance.bForced_guidance >= 1)
		{
			return true;
		}
		return false;
	}

	public static Sprite GetResourcesSprite(string srcPath, int iSize1, int iSize2)
	{
		Texture2D texture = (Texture2D)Resources.Load(srcPath, typeof(Texture2D));
		return Sprite.Create(texture, new Rect(0f, 0f, iSize1, iSize2), new Vector2(0.5f, 0.5f));
	}

	public static bool CheckMapImage(string sImageName)
	{
		if (sImageName == "map_01_03_03")
		{
			return true;
		}
		if (sImageName == "map_01_01_02")
		{
			return true;
		}
		if (sImageName == "map_01_01_01")
		{
			return true;
		}
		if (sImageName == "map_01_02_01")
		{
			return true;
		}
		if (sImageName == "map_01_02_02")
		{
			return true;
		}
		if (sImageName == "map_01_04_03")
		{
			return true;
		}
		if (sImageName == "map_01_04_04")
		{
			return true;
		}
		if (sImageName == "map_02_01_01")
		{
			return true;
		}
		if (sImageName == "map_02_02_01")
		{
			return true;
		}
		if (sImageName == "map_02_02_02")
		{
			return true;
		}
		if (sImageName == "map_02_03_03")
		{
			return true;
		}
		if (sImageName == "map_02_03_04")
		{
			return true;
		}
		if (sImageName == "map_02_04_03")
		{
			return true;
		}
		if (sImageName == "map_03_01_01")
		{
			return true;
		}
		if (sImageName == "map_03_02_02")
		{
			return true;
		}
		if (sImageName == "map_03_04_04")
		{
			return true;
		}
		if (sImageName == "map_04_01_01")
		{
			return true;
		}
		if (sImageName == "map_05_01_01")
		{
			return true;
		}
		if (sImageName == "map_05_03_03")
		{
			return true;
		}
		if (sImageName == "map_05_04_04")
		{
			return true;
		}
		if (sImageName == "map_06_02_02")
		{
			return true;
		}
		if (sImageName == "map_07_01_01")
		{
			return true;
		}
		if (sImageName == "map_02_01_02")
		{
			return true;
		}
		if (sImageName == "map_02_04_04")
		{
			return true;
		}
		if (sImageName == "map_03_03_03")
		{
			return true;
		}
		if (sImageName == "map_04_01_02")
		{
			return true;
		}
		if (sImageName == "map_04_02_02")
		{
			return true;
		}
		if (sImageName == "map_04_02_04")
		{
			return true;
		}
		if (sImageName == "map_04_03_01")
		{
			return true;
		}
		if (sImageName == "map_04_03_03")
		{
			return true;
		}
		if (sImageName == "map_04_04_04")
		{
			return true;
		}
		if (sImageName == "map_05_01_02")
		{
			return true;
		}
		if (sImageName == "map_05_03_04")
		{
			return true;
		}
		if (sImageName == "map_05_04_03")
		{
			return true;
		}
		return false;
	}

	public static void DebugClick(GameObject Pointer)
	{
		if (!(Pointer == null))
		{
		}
	}

	public static DateTime StampToDateTime(string timeStamp)
	{
		DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
		long ticks = long.Parse(timeStamp + "0000000");
		TimeSpan value = new TimeSpan(ticks);
		return dateTime.Add(value);
	}

	public static int GetNowTime()
	{
		return int.Parse(GetTimeStamp());
	}

	public static string GetTimeTest()
	{
		return DateTime.Now.ToString("ss fff");
	}

	public static int GetNowTime_FF()
	{
		return int.Parse(DateTime.Now.ToString("mmssfff").ToString());
	}

	public static string GetNowTime_Day()
	{
		string result = DateTime.Now.ToString("yyyyMMdd");
		if (Application.platform == RuntimePlatform.WindowsEditor)
		{
			return result;
		}
		if (Singleton<DataManager>.Instance.bInitUnityConfig1)
		{
			return result;
		}
		if ((bool)InitGame.Action && InitGame.Action.sNetTime != string.Empty)
		{
			return InitGame.Action.sNetTime;
		}
		return result;
	}

	public static string GetLast_Day()
	{
		return DateTime.Now.AddDays(-1.0).ToString("yyyyMMdd");
	}

	public static string GetAdd1_Day()
	{
		return DateTime.Now.AddDays(1.0).ToString("yyyyMMdd");
	}

	public static int getAdd1Time()
	{
		int num = StringToIntTime(DateTime.Now.AddDays(1.0).ToString("yyyyMMdd"));
		int nowTime = GetNowTime();
		return num - nowTime;
	}

	public static string GetTimeStamp()
	{
		return Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds).ToString();
	}

	public static string getInterNetTime()
	{
		return InitGame.Action.netTime;
	}

	public static bool CheckOnline()
	{
		if (DataManager.bisTestPack)
		{
			return true;
		}
		if (Singleton<DataManager>.Instance.bInitUnityConfig1)
		{
			return true;
		}
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			if (InitGame.Action.resultTime == 0)
			{
				return false;
			}
			if (InitGame.Action.sNetTime != DateTime.Now.ToString("yyyyMMdd"))
			{
				return false;
			}
			return true;
		}
		return true;
	}

	public static DateTime GetTimeChange(string timeStamp)
	{
		DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
		long ticks = long.Parse(timeStamp + "0000000");
		TimeSpan value = new TimeSpan(ticks);
		return dateTime.Add(value);
	}

	public static string GetNetDateTime()
	{
		WebRequest webRequest = null;
		WebResponse webResponse = null;
		WebHeaderCollection webHeaderCollection = null;
		string result = string.Empty;
		try
		{
			webRequest = WebRequest.Create("http://www.baidu.com");
			webRequest.Timeout = 3000;
			webRequest.Credentials = CredentialCache.DefaultCredentials;
			webResponse = webRequest.GetResponse();
			webHeaderCollection = webResponse.Headers;
			string[] allKeys = webHeaderCollection.AllKeys;
			foreach (string text in allKeys)
			{
				if (text == "Date")
				{
					result = webHeaderCollection[text];
				}
			}
			return result;
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log("Exception=" + ex.ToString());
			return string.Empty;
		}
		finally
		{
			webRequest?.Abort();
			webResponse?.Close();
			webHeaderCollection?.Clear();
		}
	}

	public static int StringToIntTime(string stime)
	{
		DateTime d = DateTime.ParseExact(stime, "yyyyMMdd", CultureInfo.CurrentCulture);
		DateTime d2 = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
		long num = (long)(d - d2).TotalSeconds;
		return (int)num;
	}
}
