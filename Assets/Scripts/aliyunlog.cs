using UnityEngine;

public class aliyunlog : MonoBehaviour
{
	private static int iEnterGameLogTime;

	private static int iLastTime;

	public static string GetBaseLog()
	{
		int nowTime = Util.GetNowTime();
		string text = "init_time";
		string nowTime_Day = Util.GetNowTime_Day();
		bool flag = false;
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "init_value") != 0 || Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "init_vTemp1") == 1)
		{
			text = "init_time," + Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "init_value") + "|";
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "init_value") == 201811)
			{
				flag = false;
			}
			else
			{
				flag = true;
			}
		}
		else if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "init_vTemp1") == 0 && GetNowLevelID() == 0)
		{
			text = "init_time,201811|";
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "init_vTemp1", 1);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "init_value", 201811);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "init_time", nowTime);
			flag = false;
		}
		else
		{
			flag = true;
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "init_vTemp1", 1);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "init_value", 201810);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "init_time", nowTime);
			text = "init_time,201810|";
		}
		bool flag2 = false;
		if (GetNowLevelID() == 0 && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "init_NewUser") == 0)
		{
			flag2 = true;
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "init_NewUser", 1);
		}
		text = ((!flag2) ? (text + "newuser,0|") : (text + "newuser," + nowTime + "|"));
		int num = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "ogt");
		if (num == 0)
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "ogt", nowTime);
			num = nowTime;
		}
		text = text + "ogt," + num + "|";
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "firstlogin" + nowTime_Day) == 0)
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "firstlogin" + nowTime_Day, 1);
			text = text + "firstlogin," + nowTime + "|";
		}
		else
		{
			text += "firstlogin,0|";
		}
		text = text + "basetime," + nowTime + "|";
		text = text + "nowdaystr," + nowTime_Day + "|";
		string text2 = text;
		text = text2 + "levelid," + GetNowLevelID() + "|";
		text2 = text;
		text = text2 + "useallmoney," + Singleton<TestScript>.Instance.GetFloat(DataManager.SDBNO + "DB_aliLogAllMoney") + "|";
		text2 = text;
		text = text2 + "useallgold," + Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_aliLogAllGold") + "|";
		text2 = text;
		text = text2 + "usealldiamond," + Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_aliLogAllDiamond") + "|";
		text2 = text;
		text = text2 + "nowlgold," + Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GB") + "|";
		text2 = text;
		text = text2 + "nowdiamond," + Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD") + "|";
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount", -1);
		if (@int < 0)
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_LoveCount", Singleton<DataManager>.Instance.iLoveMaxAll);
		}
		text2 = text;
		text = text2 + "lovecount," + @int + "|";
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_FullLoveTime");
		if (nowTime < int2)
		{
			int num2 = int2 - nowTime;
			text2 = text;
			text = text2 + "unlimitedlove," + num2 + "|";
		}
		return text;
	}

	public static int GetNowLevelID()
	{
		return Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
	}

	public static void UpdateAliYun(string logtype, string str, bool bBase = true)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			string text = GetBaseLog() + "logtypes," + logtype + "|" + str;
			if (!bBase)
			{
				int nowTime = Util.GetNowTime();
				text = "basetime," + nowTime + "|logtypes," + logtype + "|" + str;
			}
		}
		else if (Application.platform == RuntimePlatform.WindowsEditor)
		{
			string text2 = GetBaseLog() + "logtypes," + logtype + "|" + str;
			if (!bBase)
			{
				int nowTime2 = Util.GetNowTime();
				text2 = "basetime," + nowTime2 + "|logtypes," + logtype + "|" + str;
			}
		}
	}

	public static void SaveUserAllPayMoney(float fmoney)
	{
		float @float = Singleton<TestScript>.Instance.GetFloat(DataManager.SDBNO + "DB_aliLogAllMoney");
		@float += fmoney;
		Singleton<TestScript>.Instance.SetFloat(DataManager.SDBNO + "DB_aliLogAllMoney", @float);
	}

	public static void SaveUserAllUseGold(int gold)
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_aliLogAllGold");
		@int += gold;
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_aliLogAllGold", @int);
	}

	public static void SaveUserAllUseDiamond(int Diamond)
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_aliLogAllDiamond");
		@int += Diamond;
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_aliLogAllDiamond", @int);
	}

	public static void GameUseLog(string type, int numb, string skillname, int icount)
	{
		if (numb > 0)
		{
			if (type == "gold")
			{
				SaveUserAllUseGold(numb);
			}
			else
			{
				SaveUserAllUseDiamond(numb);
			}
			UpdateAliYun("gameuselog", "buytypess," + type + "|usecount," + numb + "|buytypes," + skillname + "|skillcount," + icount);
		}
	}

	public static void rewardSkill(string sendtype, string localid, int skillid, int skillcount)
	{
		UpdateAliYun("rewardskill", "sendtype," + sendtype + "|localid," + localid + "|skillid," + skillid + "|skillcount," + skillcount);
	}

	public static void UseGang(int index)
	{
		switch (index)
		{
		case 1:
			UseSkill("free", 10);
			break;
		case 2:
			UseSkill("free", 11);
			break;
		case 4:
			UseSkill("free", 12);
			break;
		case 5:
			UseSkill("free", 13);
			break;
		}
	}

	public static void PayLog(string key, string paytype)
	{
		if (key == "GameSkill4")
		{
			UseSkill("pay", 6);
		}
		if (key == "GameSkill5")
		{
			UseSkill("pay", 4);
		}
		if (key == "GameSkill5")
		{
			UseSkill("pay", 5);
		}
		if (key == "BuyGang1")
		{
			UseSkill("pay", 10);
		}
		if (key == "BuyGang2")
		{
			UseSkill("pay", 11);
		}
		if (key == "BuyGang3")
		{
			UseSkill("pay", 11);
		}
		if (key == "BuyGang4")
		{
			UseSkill("pay", 12);
		}
		if (key == "BuyBubble1")
		{
			UseSkill("pay", 8);
		}
		if (key == "BuyBubble2")
		{
			UseSkill("pay", 9);
		}
		string str = "paykey," + key + "|";
		str = str + "paytype," + paytype + "|";
		float num = float.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["iMoney"]);
		string str2 = Singleton<DataManager>.Instance.dDataChinaPay[key]["desc"];
		string str3 = Singleton<DataManager>.Instance.dDataChinaPay[key]["payid"];
		string str4 = Singleton<DataManager>.Instance.dDataChinaPay[key]["igold"];
		string str5 = Singleton<DataManager>.Instance.dDataChinaPay[key]["igb"];
		string str6 = Singleton<DataManager>.Instance.dDataChinaPay[key]["skill1"];
		string str7 = Singleton<DataManager>.Instance.dDataChinaPay[key]["skill2"];
		string str8 = Singleton<DataManager>.Instance.dDataChinaPay[key]["skill3"];
		string str9 = Singleton<DataManager>.Instance.dDataChinaPay[key]["skill4"];
		string str10 = Singleton<DataManager>.Instance.dDataChinaPay[key]["skill5"];
		string str11 = Singleton<DataManager>.Instance.dDataChinaPay[key]["skill6"];
		string str12 = Singleton<DataManager>.Instance.dDataChinaPay[key]["skill7"];
		string str13 = Singleton<DataManager>.Instance.dDataChinaPay[key]["iLoveInfinite"];
		string str14 = Singleton<DataManager>.Instance.dDataChinaPay[key]["iLoveInfinite05"];
		if (key == "fzgz24")
		{
			str4 = "1000";
		}
		SaveUserAllPayMoney(num);
		str = str + "paymoney," + num + "|";
		str = str + "paydesc," + str2 + "|";
		str = str + "payid," + str3 + "|";
		str = str + "payigold," + str4 + "|";
		str = str + "payigb," + str5 + "|";
		str = str + "payskill1," + str6 + "|";
		str = str + "payskill2," + str7 + "|";
		str = str + "payskill3," + str8 + "|";
		str = str + "payskill4," + str9 + "|";
		str = str + "payskill5," + str10 + "|";
		str = str + "payskill6," + str11 + "|";
		str = str + "losecount," + Singleton<LevelManager>.Instance.iFailureAll + "|";
		str = str + "payskill7," + str12 + "|";
		str = str + "thislevelid," + Singleton<LevelManager>.Instance.iNowSelectLevelIndex + "|";
		str = str + "payiLoveInfinite," + str13 + "|";
		str = str + "payiLoveInfinite05," + str14 + "|";
		UpdateAliYun("paylog", str);
	}

	public static void ClearUseSkill()
	{
		for (int i = 1; i <= 13; i++)
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_freeUseSkill" + i, 0);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_payUseSkill" + i, 0);
		}
	}

	public static string GetNowLevelUseSkill()
	{
		string text = string.Empty;
		for (int i = 1; i <= 13; i++)
		{
			if (i == 13)
			{
				string text2 = text;
				text = text2 + "levelusefree" + i + "," + Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_freeUseSkill" + i) + "|";
				text2 = text;
				text = text2 + "levelusepay" + i + "," + Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_payUseSkill" + i);
			}
			else
			{
				string text2 = text;
				text = text2 + "levelusefree" + i + "," + Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_freeUseSkill" + i) + "|";
				text2 = text;
				text = text2 + "levelusepay" + i + "," + Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_payUseSkill" + i) + "|";
			}
		}
		return text;
	}

	public static void UseSkill(string stype, int skill)
	{
		UseSkillLog(stype + skill);
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_" + stype + "UseSkill" + skill);
		@int++;
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_" + stype + "UseSkill" + skill, @int);
	}

	public static void UseSkillLog(string sidstr)
	{
		string text = "skillid," + sidstr + "|";
		text = text + "thislevelid," + Singleton<LevelManager>.Instance.iNowSelectLevelIndex + "|";
		text = text + "score," + Singleton<LevelManager>.Instance.iNowScore + "|";
		text = text + "star1," + Singleton<LevelManager>.Instance.star1 + "|";
		text = text + "star2," + Singleton<LevelManager>.Instance.star2 + "|";
		text = text + "star3," + Singleton<LevelManager>.Instance.star3 + "|";
		text = text + "starlv," + Singleton<LevelManager>.Instance.iNowStar + "|";
		text = text + "game_ball," + Singleton<LevelManager>.Instance.iBubbleCount + string.Empty;
		UpdateAliYun("useskilllog", text);
	}

	public static void UpdateDaR(string str)
	{
		UpdateAliYun("dardata", str);
	}

	public static void UpdateSuipian(string str)
	{
		UpdateAliYun("suipian", str);
	}

	public static void LevelLog(string levelstate)
	{
		string empty = string.Empty;
		string text = "levelstate," + levelstate + "|";
		text = text + "thislevelid," + Singleton<LevelManager>.Instance.iNowSelectLevelIndex + "|";
		text = text + "losecount," + Singleton<LevelManager>.Instance.iFailureAll + "|";
		text = text + "levelnowid," + Singleton<LevelManager>.Instance.iNowSelectLevelIndex + "|";
		if (levelstate != "enter")
		{
			text = text + "runtime," + (Util.GetNowTime() - Singleton<LevelManager>.Instance.iNowSelectLevelIndex) + "|";
			text = ((!(levelstate == "win")) ? (text + "game_ball," + Singleton<LevelManager>.Instance.iBubbleCount + "|") : (text + "game_ball," + Singleton<LevelManager>.Instance.iBubbleCountOver + "|"));
		}
		if (levelstate == "quit" || levelstate == "win" || levelstate == "lose")
		{
			text = text + "score," + Singleton<LevelManager>.Instance.iNowScore + "|";
			text = text + "star1," + Singleton<LevelManager>.Instance.star1 + "|";
			text = text + "star2," + Singleton<LevelManager>.Instance.star2 + "|";
			text = text + "star3," + Singleton<LevelManager>.Instance.star3 + "|";
			text = text + "starlv," + Singleton<LevelManager>.Instance.iNowStar + "|";
			text += GetNowLevelUseSkill();
		}
		UpdateAliYun("levellog", text);
	}

	public static void GameInLogic(string log)
	{
		string str = "levelnowid," + Singleton<LevelManager>.Instance.iNowSelectLevelIndex + "|";
		str += log;
		UpdateAliYun("gamelogic", str);
	}

	public static void GamebaseInfo(string log)
	{
		UpdateAliYun("gamebaseinfo", log);
	}

	public static void EnterGameLog(string log, int index = 1, string serror = "")
	{
	}

	public static string GetError(string err)
	{
		string text = err.Replace("+", "%2B");
		text = text.Replace("/", "%2F");
		text = text.Replace("?", "%3F ");
		text = text.Replace("%", "%25");
		text = text.Replace("# ", "%23");
		text = text.Replace("&", "%26");
		text = text.Replace("=", "%3D");
		text = text.Replace("\"", string.Empty);
		text = text.Replace("<", string.Empty);
		text = text.Replace(">", string.Empty);
		text = text.Replace("'", string.Empty);
		text = text.Replace("\n", string.Empty);
		text = text.Replace("\n\r", string.Empty);
		text = text.Replace("\r", string.Empty);
		text = text.Replace(",", string.Empty);
		text = text.Replace("|", string.Empty);
		text = text.Replace("\\,", string.Empty);
		return text.Replace("\\|", string.Empty);
	}

	public static void OpenAndClickBtn(string pname, string btnname, string serror = "")
	{
		string str = "pname," + pname + "|";
		if (serror != string.Empty)
		{
			str = str + "bname," + btnname + "|";
			str = str + "panel_errors," + GetError(serror);
		}
		else
		{
			str = str + "bname," + btnname + string.Empty;
		}
		UpdateAliYun("panelandbtn", str);
	}

	public static void Cutbubble(int ballIndex)
	{
	}

	public void AdLog(string adress, string stype)
	{
		string str = "adress," + adress + "|";
		str = str + "stype," + stype + "|";
		UpdateAliYun("adlog", str);
	}

	public static void UserBag()
	{
		string text = string.Empty;
		for (int i = 0; i <= 6; i++)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Skill_Count_" + i);
			if (i < 6)
			{
				string text2 = text;
				text = text2 + "levelusefree" + (i + 1) + "," + @int + "|";
			}
			else
			{
				string text2 = text;
				text = text2 + "levelusefree" + (i + 1) + "," + @int + string.Empty;
			}
		}
		UpdateAliYun("userbag", text);
	}
}
