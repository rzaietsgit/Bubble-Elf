using System;
using System.Text;
//using Umeng;
using UnityEngine;

public class UserManager : Singleton<UserManager>
{
	public int iNowPassLevelID;

	public int iNowMapID;

	public static int iMapCount = 26;

	public int iPassLevelCount;

	private bool busemoney12;

	public string sUserID = string.Empty;

	private static char[] constant = new char[62]
	{
		'0',
		'1',
		'2',
		'3',
		'4',
		'5',
		'6',
		'7',
		'8',
		'9',
		'a',
		'b',
		'c',
		'd',
		'e',
		'f',
		'g',
		'h',
		'i',
		'j',
		'k',
		'l',
		'm',
		'n',
		'o',
		'p',
		'q',
		'r',
		's',
		't',
		'u',
		'v',
		'w',
		'x',
		'y',
		'z',
		'A',
		'B',
		'C',
		'D',
		'E',
		'F',
		'G',
		'H',
		'I',
		'J',
		'K',
		'L',
		'M',
		'N',
		'O',
		'P',
		'Q',
		'R',
		'S',
		'T',
		'U',
		'V',
		'W',
		'X',
		'Y',
		'Z'
	};

	public int GetMapStar(int indexMap)
	{
		int num = 1;
		int num2 = 10;
		if (indexMap > 0)
		{
			for (int i = 0; i < indexMap; i++)
			{
				num += Singleton<DataManager>.Instance.LMapBtnCount[i];
			}
			num2 = num + Singleton<DataManager>.Instance.LMapBtnCount[indexMap] - 1;
		}
		int num3 = 0;
		for (int j = num; j <= num2; j++)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LevelStar_" + j);
			num3 += @int;
		}
		return num3;
	}

	public int getLoveInfinite()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveInfinite");
		if (@int <= 0)
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_LoveInfinite", 0);
			return 0;
		}
		int nowTime = Util.GetNowTime();
		@int -= nowTime;
		if (@int > 0)
		{
			return @int;
		}
		Singleton<DataManager>.Instance.SaveUserDate("DB_LoveInfinite", 0);
		return 0;
	}

	public void AddLoveInfinite(float iH)
	{
		if (!(iH < 0.5f))
		{
			int num = 0;
			int num2 = 0;
			num2 = ((!(iH < 1f)) ? ((int)iH * 60 * 60) : 1800);
			num = ((getLoveInfinite() <= 0) ? (Util.GetNowTime() + num2) : (Util.GetNowTime() + getLoveInfinite() + num2));
			Singleton<DataManager>.Instance.SaveUserDate("DB_LoveInfinite", num);
		}
	}

	public int GetMapForLevelID(int indexLevel)
	{
		if (indexLevel > LevelManager.iMaxLevelID)
		{
			indexLevel = LevelManager.iMaxLevelID;
		}
		return Singleton<DataManager>.Instance.dDataLevel_Map[indexLevel];
	}

	public void SetNowPassLevelNumber(int iPassLevelID, int iStar, int iScore)
	{
		SetLevelStarAndScore(iPassLevelID, iStar, iScore);
		FirebaseController.ExitLevel(1);
	}

	public void GoNextMap()
	{
		if (iNowMapID <= iMapCount)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
			if (@int != LevelManager.iMaxLevelID)
			{
				SetNowMapID(iNowMapID + 1);
				Singleton<LevelManager>.Instance.bGoNextMap = true;
			}
		}
	}

	public void SetNowMapID(int iNowMapid)
	{
		if (iNowMapid < iMapCount)
		{
			iNowMapID = iNowMapid;
			Singleton<DataManager>.Instance.SaveUserDate("DB_iNowMapID", iNowMapID);
		}
	}

	public void SetLevelStarAndScore(int iPassLevelID, int iStar, int iScore)
	{
		if (iStar >= 3)
		{
			Singleton<UserManager>.Instance.SetPassTask("PassStar3");
		}
		Singleton<UserManager>.Instance.SetPassTask("starCount" + iStar);
		if (iPassLevelID > LevelManager.iMaxLevelID)
		{
			iPassLevelCount++;
			SetPassTask("PassLevelCount", iPassLevelCount);
			return;
		}
		bool flag = false;
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LevelScore_" + iPassLevelID);
		if (iScore > @int)
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_LevelStar_" + iPassLevelID, iStar);
			Singleton<DataManager>.Instance.SaveUserDate("DB_LevelScore_" + iPassLevelID, iScore);
			UpdateScore(iPassLevelID);
		}
		if (iPassLevelID > iNowPassLevelID)
		{
			flag = true;
			iPassLevelCount++;
			SetPassTask("PassLevelCount", iPassLevelCount);
			if (iPassLevelID != LevelManager.iMaxLevelID)
			{
				Singleton<LevelManager>.Instance.bOpenNewLevel = true;
			}
			iNowPassLevelID = iPassLevelID;

			if (iPassLevelID == 1 || iPassLevelID == 2)
			{
				DataManager.iFirstLoginGameLoadCloud = 1;
				Singleton<DataManager>.Instance.SaveUserDate("DB_FirstLoginGameLoadCloud", 1);
			}
			Singleton<DataManager>.Instance.SaveUserDate("DB_iNowPassLevelID", iNowPassLevelID);
			for (int i = 0; i < Singleton<DataManager>.Instance.LMapEndBtnID.Length; i++)
			{
				if (iNowPassLevelID == Singleton<DataManager>.Instance.LMapEndBtnID[i])
				{
					Singleton<UserManager>.Instance.GoNextMap();
				}
			}
		}
		else
		{
			iPassLevelCount = 0;
		}
		PassLevelAward(iNowPassLevelID);
		int int2 = PlayerPrefs.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
		int num = 0;
		for (int j = 0; j < int2; j++)
		{
			num += Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LevelStar_" + j);
		}
		
	}

	public void PassLevelAward(int iLevel)
	{
	}

	public bool UpdateScore(int iPassLevelID)
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LevelStar_" + iPassLevelID);
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LevelScore_" + iPassLevelID);
		if (int2 > 100 && @int > 0)
		{
			if ((bool)FireBase.Action)
			{
				FireBase.Action.UnityUpdateScore(iPassLevelID, @int, int2);
			}
			return true;
		}
		return false;
	}

	public void LoadNowPassLevelNumber()
	{
		iNowPassLevelID = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
		iNowMapID = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowMapID");
	}

	public int GetLevelStar(int iNumber)
	{
		if (iNumber > iNowPassLevelID)
		{
			return 0;
		}
		return Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LevelStar_" + iNumber);
	}

	public void EnterLog()
	{
		int num = Singleton<UserLevelManager>.Instance.GetUserLevel();
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex < 31)
		{
			num = 0;
		}
		FireBase.Action.UnityWriteLog("LOG_LevelEnter", Singleton<LevelManager>.Instance.iNowSelectLevelIndex + "|" + num);
	}

	public int GetTaskCount(string sType)
	{
		return Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_TaskCount" + sType + Util.getInterNetTime());
	}

	public int GetTaskCount1(string sType)
	{
		return Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_TaskCount1" + sType);
	}

	public void SetPassTask(string sType, int iCount = 1)
	{
		if (sType == "PassLevelCount")
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_TaskCount" + sType + Util.getInterNetTime());
			if (iCount > @int)
			{
				Singleton<DataManager>.Instance.SaveUserDate("DB_TaskCount" + sType + Util.getInterNetTime(), @int);
			}
		}
		else
		{
			int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_TaskCount" + sType + Util.getInterNetTime());
			int2 += iCount;
			Singleton<DataManager>.Instance.SaveUserDate("DB_TaskCount" + sType + Util.getInterNetTime(), int2);
		}
	}

	public void SetPassTask1(string sType, int iCount = 1)
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_TaskCount1" + sType);
		@int += iCount;
		Singleton<DataManager>.Instance.SaveUserDate("DB_TaskCount1" + sType, @int);
	}

	public float useMoneyCount()
	{
		return Singleton<TestScript>.Instance.GetFloat(DataManager.SDBNO + "DB_PayGOLDCount");
	}

	public bool UseMoney12()
	{
		if (busemoney12)
		{
			return true;
		}
		if (Application.platform == RuntimePlatform.Android && !Singleton<DataManager>.Instance.bGooglePay)
		{
			
		}
		return false;
	}

	public void ClearBackSkill()
	{
		for (int i = 1; i <= 6; i++)
		{
			int num = 0;
			string oldValue = string.Empty;
			string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_Skill_Count_Time_" + i, string.Empty);
			if (@string != string.Empty)
			{
				for (int num2 = 0; num2 < @string.Split(',').Length; num2++)
				{
					string text = @string.Split(',')[num2];
					if (text.Length > 3)
					{
						int num3 = int.Parse(text);
						num3 -= Util.GetNowTime();
						if (num3 <= 0)
						{
							oldValue = text;
							num++;
							break;
						}
					}
				}
			}
			if (num > 0)
			{
				@string = @string.Replace(oldValue, string.Empty);
				@string = @string.Replace(",,", string.Empty);
				Singleton<TestScript>.Instance.SetString(DataManager.SDBNO + "DB_Skill_Count_Time_" + i, @string);
			}
		}
	}

	public void AddHuaBi(int ibi)
	{
		UnityEngine.Debug.Log("iBi = " + ibi);
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_HuaBiCount");
		@int += ibi;
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_HuaBiCount", @int);
		if ((bool)HuaShopUI.action)
		{
			HuaShopUI.action.InitHuaBi();
		}
	}

	public int GetHuaBi()
	{
		return Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_HuaBiCount");
	}

	public void NextLb2()
	{
		int num = 0;
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NextLb2");
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NextLb1");
		num = @int;
		@int = UnityEngine.Random.Range(2, 14);
		bool flag = false;
		if (@int == 9 && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "First_Pay") == 1)
		{
			flag = true;
		}
		while (int2 == @int || @int == 0 || @int == 3 || @int == num || flag)
		{
			flag = false;
			@int = UnityEngine.Random.Range(2, 14);
			if (@int == 9 && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "First_Pay") == 1)
			{
				flag = true;
			}
		}
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NextLb2", @int);
		if ((bool)ClickBtnFun._24Obj)
		{
			ClickBtnFun._24Obj.ResLbUI();
		}
	}

	public void NextLb1()
	{
		int num = 0;
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NextLb1");
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NextLb2");
		num = @int;
		@int = UnityEngine.Random.Range(2, 14);
		bool flag = false;
		if (@int == 9 && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "First_Pay") == 1)
		{
			flag = true;
		}
		while (@int == int2 || @int == 0 || @int == num || flag)
		{
			flag = false;
			@int = UnityEngine.Random.Range(2, 14);
			if (@int == 9 && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "First_Pay") == 1)
			{
				flag = true;
			}
		}
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NextLb1", @int);
		if ((bool)ClickBtnFun._HaohuaObj)
		{
			ClickBtnFun._HaohuaObj.ResLbUI();
		}
	}

	public void AddFeiliaoDouble(int iCount)
	{
		int nowTime = Util.GetNowTime();
		int num = int.Parse(Singleton<DataManager>.Instance.dDataHua4["4"]["iConfigData"]);
		int num2 = iCount * num * 60 * 60;
		int num3 = iCount * num * 60 * 60 / 600 * HuaGame.action.ichengzhang;
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "iFeiliaoDoubleiTimeShui");
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "iFeiliaoDoubleiTimeFei");
		int int3 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "iFeiliaoDoubleiTime");
		if (int3 < nowTime)
		{
			int3 = nowTime + num2;
			@int = num3;
			int2 = num3;
		}
		else
		{
			int3 += num2;
			@int += num3;
			int2 += num3;
		}
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "iFeiliaoDoubleiTimeShui", @int);
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "iFeiliaoDoubleiTimeFei", int2);
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "iFeiliaoDoubleiTime", int3);
	}

	public void CutDouble(int iType = 0)
	{
		if (iType == 0)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "iFeiliaoDoubleiTimeShui");
			@int--;
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "iFeiliaoDoubleiTimeShui", @int);
		}
		else
		{
			int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "iFeiliaoDoubleiTimeFei");
			int2--;
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "iFeiliaoDoubleiTimeFei", int2);
		}
	}

	public bool ResFeiliaoDouble()
	{
		int nowTime = Util.GetNowTime();
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "iFeiliaoDoubleiTime");
		if (@int < nowTime)
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "iFeiliaoDoubleiTimeShui", 0);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "iFeiliaoDoubleiTimeFei", 0);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "iFeiliaoDoubleiTime", 0);
			return false;
		}
		return true;
	}

	public int bOpenHua()
	{
		if (InitGame.bEnios)
		{
			return 0;
		}
		if (Singleton<UserManager>.Instance.iNowPassLevelID < 30)
		{
			return 0;
		}
		foreach (string key in Singleton<DataManager>.Instance.dDataHua5.Keys)
		{
			string stime = Singleton<DataManager>.Instance.dDataHua5[key]["start"];
			string stime2 = Singleton<DataManager>.Instance.dDataHua5[key]["end"];
			int num = Util.StringToIntTime(stime);
			int num2 = Util.StringToIntTime(stime2);
			int nowTime = Util.GetNowTime();
			if (nowTime >= num && nowTime <= num2)
			{
				return num2 - nowTime;
			}
		}
		return 0;
	}

	public bool bOpenHuaUI()
	{
		foreach (string key in Singleton<DataManager>.Instance.dDataHua5.Keys)
		{
			string text = Singleton<DataManager>.Instance.dDataHua5[key]["start"];
			string stime = Singleton<DataManager>.Instance.dDataHua5[key]["end"];
			int num = Util.StringToIntTime(text);
			int num2 = Util.StringToIntTime(stime);
			int nowTime = Util.GetNowTime();
			if (nowTime >= num && nowTime <= num2 && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "iFlagbOpenHuaUI" + text) == 0)
			{
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "iFlagbOpenHuaUI" + text, 1);
				return true;
			}
		}
		return false;
	}

	public string getHuaBuyID()
	{
		foreach (string key in Singleton<DataManager>.Instance.dDataHua5.Keys)
		{
			string text = Singleton<DataManager>.Instance.dDataHua5[key]["start"];
			string stime = Singleton<DataManager>.Instance.dDataHua5[key]["end"];
			int num = Util.StringToIntTime(text);
			int num2 = Util.StringToIntTime(stime);
			int nowTime = Util.GetNowTime();
			if (nowTime >= num && nowTime <= num2)
			{
				return text;
			}
		}
		return string.Empty;
	}

	public void KillHua()
	{
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "iHuaShuiCount1", 0);
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "iHuaFeiCount1", 0);
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_HuaShuiStarTime", 0);
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_HuaFeiStarTime", 0);
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "Huajieduan", 0);
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "HuajieduanOk", 0);
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NowHuaID", 0);
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuaRandID");
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NowHuaRandID", 0);
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NowHuaRandID" + @int, 0);
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NowHuangChong", 0);
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "iinitjiankang", 0);
	}

	public int iSKillCount()
	{
		int num = 0;
		for (int i = 1; i <= 6; i++)
		{
			string @string = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "DB_Skill_Count_Time_" + i, string.Empty);
			if (!(@string != string.Empty))
			{
				continue;
			}
			for (int num2 = 0; num2 < @string.Split(',').Length; num2++)
			{
				string text = @string.Split(',')[num2];
				if (text.Length > 3)
				{
					int num3 = int.Parse(text);
					num3 -= Util.GetNowTime();
					if (num3 > 60)
					{
						num++;
					}
				}
			}
		}
		for (int j = 1; j <= 6; j++)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Skill_Count_" + j);
			if (@int > 0)
			{
				num += @int;
			}
		}
		return num;
	}

	public int GetLb(int iR1 = 30, int iR2 = 60, int iR3 = 100)
	{
		int num = UnityEngine.Random.Range(1, 101);
		if (num < iR1)
		{
			return 4;
		}
		if (num < iR2)
		{
			return UnityEngine.Random.Range(5, 8);
		}
		int num2 = UnityEngine.Random.Range(1, 100);
		if (num2 < 33)
		{
			return 2;
		}
		if (num2 < 66)
		{
			return 3;
		}
		return 8;
	}

	public string initUserID()
	{
		if (sUserID != string.Empty)
		{
			return sUserID;
		}
		sUserID = Singleton<TestScript>.Instance.GetString(DataManager.SDBNO + "sRandomUserID", string.Empty);
		if (sUserID != string.Empty)
		{
			return sUserID;
		}
		sUserID = GenerateRandomNumber(32);
		Singleton<TestScript>.Instance.SetString(DataManager.SDBNO + "sRandomUserID", sUserID);
		return sUserID;
	}

	public static string GenerateRandomNumber(int Length)
	{
		StringBuilder stringBuilder = new StringBuilder(62);
		for (int i = 0; i < Length; i++)
		{
			stringBuilder.Append(constant[UnityEngine.Random.Range(0, 62)]);
		}
		return stringBuilder.ToString();
	}
}
