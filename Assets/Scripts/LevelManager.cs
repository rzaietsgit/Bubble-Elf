using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
	public struct LevelObject
	{
		public string key;

		public int value;
	}

	public bool bExit;

	public static int iMaxLevelID = 600;

	public bool bFirstInMap = true;

	public bool bRstart;

	public bool bRstart2;

	public bool bRstart3;

	public bool bRstart4;

	public bool bRstart5;

	public bool bRstart6;

	public bool bRstart7;

	public bool bLastWin;

	public int ijiesuanScore = 1000;

	public int iFailure;

	public bool bLoseGame;

	public int iLastFailureLevelID;

	public bool bstartbubble;

	public static bool bWwwDataFlag = false;

	public static string Net_Address = "file://" + Application.dataPath + string.Empty;

	public JsonData dJsonData;

	public List<LevelObject> gemSpawnChance;

	public List<BUBBLEDATA> LTbubble;

	public List<BUBBLEDATA> LTsub;

	public List<BUBBLEDATA> Ljysub;

	public bool bflylevel;

	public List<BUBBLEDATA> LTDown;

	public int iBubbleCount;

	public int iBubbleCountOver;

	public int iBubbleUseCount;

	public int iLevelType;

	public int iLevelCount;

	public int star1;

	public int star2;

	public int star3;

	public int G1;

	public int G2;

	public int G3;

	public int G4;

	public int G5;

	public int RBubbleSum;

	public int R1;

	public int R2;

	public int R3;

	public int R4;

	public int iNowStar;

	public int iNowScore;

	public int iNowSelectLevelIndex = 1;

	public bool bOpenPlay;

	public int iFailureAll;

	public bool bOpenNewLevel;

	public bool bGoNextMap;

	public int dareIndex;

	public List<int> dareLevels;

	public bool bLoadOver;

	public bool bBossHuang;

	public bool bBossHuangReward;

	public int bBossHuang3Change;

	public List<BUBBLEDATA> LTbubbleBoss;

	public bool Skill_Select_0_Gamelose;

	public bool Skill_Select_1_Gamelose;

	public bool Skill_Select_2_Gamelose;

	public bool Skill_Select_3_Gamelose;

	private bool isNew;

	public int ResTime = 1800;

	public override void Init()
	{
	}

	public void LoadNetData(int iLev)
	{
		DDOLSingleton<CoroutineController>.Instance.StartCoroutine(LoadAsset(iLev));
	}

	private IEnumerator LoadAsset(int iLev)
	{
		string empty = string.Empty;
		Singleton<UserLevelManager>.Instance.GetUserLevel();
		if (iLev >= 10000 && iLev <= 20000)
		{
			string text = Net_Address + "/Data/level/Level_" + iLev + ".txt";
		}
		string url = (iLev > 10000 && iLev < 20000) ? (Net_Address + "/Data/level/Level_" + iLev + ".txt") : ((iLev <= 101) ? (Net_Address + "/Data/level/Level_" + iLev + ".txt") : (Net_Address + "/Data/level/Level_" + iLev + "_1.txt"));
		if (iLev >= 80000)
		{
			bBossHuang = true;
		}
		WWW w = new WWW(url);
		yield return new WaitForSeconds(2f);
		string mapStringdata2 = w.text;
		mapStringdata2 = mapStringdata2.Replace("\r\n", string.Empty);
		dJsonData = JsonMapper.ToObject(mapStringdata2);
		UnityEngine.Debug.Log("iLev=" + iLev);
		UnityEngine.Debug.Log("url=" + url);
		UnityEngine.Debug.Log("dJsonData=" + dJsonData);
		LoadLevelData(iLev, bwww: true, dJsonData);
	}

	public void LoadLevelData(int iLevelID = -1, bool bwww = false, JsonData www_dJsonData = null)
	{
		Singleton<DataManager>.Instance.bRateUsUIOpen = false;
		bBossHuang3Change = 0;
		bBossHuang = false;
		bBossHuangReward = true;
		bLoadOver = false;
		int num = iLevelID;
		if (iLevelID == -1)
		{
			num = iNowSelectLevelIndex;
		}
		if (num >= 80000)
		{
			int num2 = UnityEngine.Random.Range(1, 11);
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_FirstInHuangchong") == 0)
			{
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_FirstInHuangchong", 1);
				num2 = 1;
			}
			iNowSelectLevelIndex += num2;
			num = iNowSelectLevelIndex;
			bBossHuang = true;
			LTbubbleBoss = new List<BUBBLEDATA>();
			if ((bool)GameUI.action)
			{
				GameUI.action.CheckBossLevel();
			}
		}
		int userLevel = Singleton<UserLevelManager>.Instance.GetUserLevel();
		bBossHuang = false;
		string str = (num > 10000 && num < 20000) ? ("Level_" + num) : ((num <= 101) ? ("Level_" + num) : ("Level_" + num + "_1"));
		if (num >= 80000)
		{
			str = "Level_" + num;
			bBossHuang = true;
		}
		iNowStar = 0;
		star1 = 0;
		star2 = 0;
		star3 = 0;
		G1 = 0;
		G2 = 0;
		G3 = 0;
		G4 = 0;
		ijiesuanScore = 1000;
		G5 = 0;
		bstartbubble = false;
		R1 = 0;
		R2 = 0;
		R3 = 0;
		R4 = 0;
		bflylevel = false;
		iBubbleCount = 0;
		iBubbleCountOver = 0;
		iBubbleUseCount = 0;
		iLevelType = 0;
		iLevelCount = 0;
		RBubbleSum = 0;
		aliyunlog.ClearUseSkill();
		Singleton<DataManager>.Instance.bLevel3OpenPlay = false;
		if (!bwww && bWwwDataFlag)
		{
			LoadNetData(num);
			return;
		}
		string empty = string.Empty;
		if (!bWwwDataFlag)
		{
			TextAsset textAsset = (TextAsset)Resources.Load("Data/level/" + str, typeof(TextAsset));
			empty = textAsset.ToString();
			empty = empty.Replace("\r\n", string.Empty);
			dJsonData = JsonMapper.ToObject(empty);
		}
		else
		{
			dJsonData = www_dJsonData;
		}
		iBubbleCount = (int)dJsonData["BallNumber"];
		if (Singleton<LevelManager>.Instance.dareIndex == 0)
		{
			Singleton<DataManager>.Instance.iDareBushu = iBubbleCount;
		}
		if (num > 10000 && num < 20000 && Singleton<LevelManager>.Instance.dareIndex != 0)
		{
			iBubbleCount = Singleton<DataManager>.Instance.iDareBushu;
		}
		Singleton<LevelManager>.Instance.iFailureAll++;
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Level" + num);
		if (@int > 0)
		{
			iBubbleCount = @int;
		}
		if (iBubbleCount < 3)
		{
			iBubbleCount = 3;
		}
		star1 = (int)dJsonData["Star"]["star1"];
		star2 = (int)dJsonData["Star"]["star2"];
		star3 = (int)dJsonData["Star"]["star3"];
		try
		{
			R1 = (int)dJsonData["ReadyBubble"]["R1"];
			R2 = (int)dJsonData["ReadyBubble"]["R2"];
			R3 = (int)dJsonData["ReadyBubble"]["R3"];
			R4 = (int)dJsonData["ReadyBubble"]["R4"];
		}
		catch (Exception)
		{
		}
		try
		{
			G1 = (int)dJsonData["Gang"]["Gang1"];
			G2 = (int)dJsonData["Gang"]["Gang2"];
			G3 = (int)dJsonData["Gang"]["Gang3"];
			G4 = (int)dJsonData["Gang"]["Gang4"];
			G5 = (int)dJsonData["Gang"]["Gang5"];
			if (num > 10000 && num < 20000)
			{
				int num3 = UnityEngine.Random.Range(0, 1000);
				if (num3 < 150)
				{
					G1 = 1;
				}
				num3 = UnityEngine.Random.Range(0, 1000);
				if (num3 < 350)
				{
					G2 = 1;
				}
				num3 = UnityEngine.Random.Range(0, 1000);
				if (num3 < 350)
				{
					G4 = 1;
				}
				num3 = UnityEngine.Random.Range(0, 1000);
				if (num3 < 350)
				{
					G5 = 1;
				}
				if (G1 + G2 + G4 + G5 == 0)
				{
					num3 = UnityEngine.Random.Range(0, 1000);
					if (num3 < 50)
					{
						G1 = 1;
					}
					else if (num3 < 350)
					{
						G2 = 1;
					}
					else if (num3 < 650)
					{
						G4 = 1;
					}
					else
					{
						G5 = 1;
					}
				}
			}
		}
		catch (Exception)
		{
		}
		iLevelType = (int)dJsonData["LevelType"]["Type"];
		iLevelCount = (int)dJsonData["LevelType"]["Number"];
		bRstart7 = false;
		gemSpawnChance = new List<LevelObject>();
		LevelObject item = default(LevelObject);
		for (int i = 0; i < dJsonData["ObjChance"].Count; i++)
		{
			item.key = dJsonData["ObjChance"][i]["Key"].ToString();
			item.value = int.Parse(dJsonData["ObjChance"][i]["P"].ToString());
			gemSpawnChance.Add(item);
		}
		LTbubble = new List<BUBBLEDATA>();
		LTsub = new List<BUBBLEDATA>();
		Ljysub = new List<BUBBLEDATA>();
		LTDown = new List<BUBBLEDATA>();
		BUBBLEDATA item2 = default(BUBBLEDATA);
		for (int j = 0; j < dJsonData["bubble"].Count; j++)
		{
			string key = dJsonData["bubble"][j]["key"].ToString();
			string s = dJsonData["bubble"][j]["x"].ToString();
			string s2 = dJsonData["bubble"][j]["y"].ToString();
			string s3 = dJsonData["bubble"][j]["s"].ToString();
			string s4 = dJsonData["bubble"][j]["i"].ToString();
			item2.key = key;
			item2.row = int.Parse(s);
			item2.col = int.Parse(s2);
			item2.s = int.Parse(s3);
			item2.i = int.Parse(s4);
			LTbubble.Add(item2);
			if (bBossHuang)
			{
				LTbubbleBoss.Add(item2);
			}
		}
		BUBBLEDATA item3 = default(BUBBLEDATA);
		for (int k = 0; k < dJsonData["sub"].Count; k++)
		{
			string key2 = dJsonData["sub"][k]["key"].ToString();
			string s5 = dJsonData["sub"][k]["x"].ToString();
			string s6 = dJsonData["sub"][k]["y"].ToString();
			string s7 = dJsonData["sub"][k]["s"].ToString();
			string s8 = dJsonData["sub"][k]["i"].ToString();
			item3.key = key2;
			item3.row = int.Parse(s5);
			item3.col = int.Parse(s6);
			item3.s = int.Parse(s7);
			item3.i = int.Parse(s8);
			LTsub.Add(item3);
		}
		try
		{
			BUBBLEDATA item4 = default(BUBBLEDATA);
			for (int l = 0; l < dJsonData["jysub"].Count; l++)
			{
				string key3 = dJsonData["jysub"][l]["key"].ToString();
				string s9 = dJsonData["jysub"][l]["x"].ToString();
				string s10 = dJsonData["jysub"][l]["y"].ToString();
				string s11 = dJsonData["jysub"][l]["s"].ToString();
				string s12 = dJsonData["jysub"][l]["i"].ToString();
				item4.key = key3;
				item4.row = int.Parse(s9);
				item4.col = int.Parse(s10);
				item4.s = int.Parse(s11);
				item4.i = int.Parse(s12);
				Ljysub.Add(item4);
				bflylevel = true;
			}
		}
		catch (Exception)
		{
		}
		if (bflylevel)
		{
			if ((bool)GameUI.action)
			{
				GameUI.action.mubiaoiCon1.SetActive(value: false);
				GameUI.action.mubiaoiCon2.SetActive(value: true);
			}
		}
		else if ((bool)GameUI.action)
		{
			GameUI.action.mubiaoiCon1.SetActive(value: true);
			GameUI.action.mubiaoiCon2.SetActive(value: false);
		}
		try
		{
			BUBBLEDATA item5 = default(BUBBLEDATA);
			for (int m = 0; m < dJsonData["down"].Count; m++)
			{
				string key4 = dJsonData["down"][m]["key"].ToString();
				string s13 = dJsonData["down"][m]["x"].ToString();
				string s14 = dJsonData["down"][m]["y"].ToString();
				string s15 = dJsonData["down"][m]["s"].ToString();
				string s16 = dJsonData["down"][m]["i"].ToString();
				item5.key = key4;
				item5.row = int.Parse(s13);
				item5.col = int.Parse(s14);
				item5.s = int.Parse(s15);
				item5.i = int.Parse(s16);
				LTDown.Add(item5);
			}
		}
		catch (Exception)
		{
		}
		if ((bool)PassLevel.action)
		{
			PassLevel.action.InitMubiao();
		}
		aliyunlog.LevelLog("enter");
		bLoadOver = true;
		Singleton<LevelManager>.Instance.bGoNextMap = false;
		bExit = false;
		bFirstInMap = false;
		bRstart = false;
		bLastWin = false;
		bLoseGame = false;
		Singleton<DataManager>.Instance.bFirstLose = true;
		if ((bool)FireBase.Action && FaceBookApi.Action.bLoginState())
		{
			FireBase.Action.bSearchNoShow = true;
			FireBase.Action.UnitySearchRankScore(FaceBookApi.Action.LFacebookFriend);
		}
		//GA.StartLevel(Singleton<LevelManager>.Instance.iNowSelectLevelIndex.ToString());
		if ((bool)SoundController.action)
		{
			SoundController.action.ClearPlay();
		}
		if ((bool)GameUI.action)
		{
			GameUI.action.InitStarData();
		}
		isNew = true;
		FailedGoGame();
	}

	public void FailedGoGame()
	{
		Skill_Select_0_Gamelose = false;
		Skill_Select_1_Gamelose = false;
		Skill_Select_2_Gamelose = false;
		Skill_Select_3_Gamelose = false;
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "UserLevelGamelose" + iNowSelectLevelIndex);
		if (iNowSelectLevelIndex <= 50)
		{
			if (@int <= 1 && @int > 0)
			{
				iBubbleCount += 5;
				Singleton<DataManager>.Instance.SaveUserDate("DB_Skill_SelectNew_" + 1, 1);
				Skill_Select_1_Gamelose = true;
			}
			else if (@int > 1)
			{
				iBubbleCount += 8;
				Singleton<DataManager>.Instance.SaveUserDate("DB_Skill_SelectNew_" + 1, 1);
				Skill_Select_1_Gamelose = true;
				Singleton<DataManager>.Instance.SaveUserDate("DB_Skill_SelectNew_" + 2, 1);
				Skill_Select_2_Gamelose = true;
			}
		}
		else if (iNowSelectLevelIndex <= 150)
		{
			if (@int <= 1 && @int > 0)
			{
				iBubbleCount += 3;
			}
			else if (@int <= 2 && @int > 0)
			{
				iBubbleCount += 3;
				Singleton<DataManager>.Instance.SaveUserDate("DB_Skill_SelectNew_" + 1, 1);
				Skill_Select_1_Gamelose = true;
			}
			else
			{
				if (@int <= 2)
				{
					return;
				}
				iBubbleCount += 5;
				Singleton<DataManager>.Instance.SaveUserDate("DB_Skill_SelectNew_" + 1, 1);
				Skill_Select_1_Gamelose = true;
				List<int> list = new List<int>();
				if (G1 == 0)
				{
					list.Add(1);
				}
				if (G2 == 0)
				{
					list.Add(2);
				}
				if (G4 == 0)
				{
					list.Add(3);
				}
				if (G5 == 0)
				{
					list.Add(4);
				}
				if (list.Count > 0)
				{
					int num = UnityEngine.Random.Range(0, list.Count * 100);
					int num2 = -1;
					if (num < 100)
					{
						num2 = 0;
					}
					else if (num < 200)
					{
						num2 = 1;
					}
					else if (num < 300)
					{
						num2 = 2;
					}
					else if (num < 400)
					{
						num2 = 3;
					}
					if (num2 != -1 && list.Count > num2)
					{
						if (list[num2] == 1)
						{
							G1 = 1;
						}
						if (list[num2] == 2)
						{
							G2 = 1;
						}
						if (list[num2] == 3)
						{
							G4 = 1;
						}
						if (list[num2] == 4)
						{
							G5 = 1;
						}
					}
				}
				Singleton<DataManager>.Instance.SaveUserDate("DB_Skill_SelectNew_" + 0, 1);
				Skill_Select_0_Gamelose = true;
			}
		}
		else if (iNowSelectLevelIndex <= 200)
		{
			if (@int <= 1 && @int > 0)
			{
				iBubbleCount += 3;
			}
			else if (@int > 1 && @int <= 2)
			{
				iBubbleCount += 3;
				Singleton<DataManager>.Instance.SaveUserDate("DB_Skill_SelectNew_" + 1, 1);
				Skill_Select_1_Gamelose = true;
			}
			else if (@int > 2)
			{
				iBubbleCount += 3;
				Singleton<DataManager>.Instance.SaveUserDate("DB_Skill_SelectNew_" + 1, 1);
				Skill_Select_1_Gamelose = true;
				Singleton<DataManager>.Instance.SaveUserDate("DB_Skill_SelectNew_" + 2, 1);
				Skill_Select_2_Gamelose = true;
			}
		}
		else if (iNowSelectLevelIndex > 200 && @int > 0)
		{
			iBubbleCount += 3;
		}
	}

	public void CutBubble()
	{
		if (!bstartbubble)
		{
			bstartbubble = true;
			InitAndroid.action.GAEvent("LevelLog:StartLevel1Bubble:" + iNowSelectLevelIndex);
		}
		iBubbleCount--;
		if (!GameUI.action)
		{
			return;
		}
		GameUI.action.LoadBubbleCount();
		if (PassLevel.bWin)
		{
			return;
		}
		RBubbleSum++;
		if (iBubbleCount == 10 && isNew)
		{
			isNew = false;
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("b_remain_10_bubbles", NowPlay: true);
			}
			GameUI.action.ShowTipBubbleObj();
			GameUI.action.CreateNowBuyBubble();
			GameUI.action.PlaySkillAniRandmo();
		}
		if (iBubbleCount == 5)
		{
			PassLevel.action.Liuhan();
		}
	}

	public bool CheckBubble()
	{
		if ((bool)PassLevel.action && PassLevel.bWin)
		{
			return false;
		}
		if (iBubbleCount > 0)
		{
			return true;
		}
		return false;
	}

	public void CutLove()
	{
		int loveInfinite = Singleton<UserManager>.Instance.getLoveInfinite();
		if (loveInfinite <= 0)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
			@int--;
			Singleton<DataManager>.Instance.SaveUserDate("DB_LoveCount", @int);
			if (@int < Singleton<DataManager>.Instance.iLoveMaxAll)
			{
				int num = 0;
				int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_FullLoveTime");
				num = ((Util.GetNowTime() >= int2) ? (Util.GetNowTime() + ResTime) : (int2 + ResTime));
				Singleton<DataManager>.Instance.SaveUserDate("DB_FullLoveTime", num);
			}
		}
	}

	public bool GetGangSkill(int iGangID)
	{
		switch (iGangID)
		{
		case 1:
			if (G1 == 1)
			{
				return true;
			}
			break;
		case 2:
			if (G2 == 1)
			{
				return true;
			}
			break;
		case 3:
			if (G3 == 1)
			{
				return true;
			}
			break;
		case 4:
			if (G4 == 1)
			{
				return true;
			}
			break;
		case 5:
			if (G5 == 1)
			{
				return true;
			}
			break;
		}
		return false;
	}

	public void AddLove(int iCount = 1)
	{
		if (iCount >= 1)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
			int num = 0;
			if (iCount > 5 - @int)
			{
				num = 5 - @int;
			}
			else if (iCount < 5 - @int)
			{
				num = iCount;
			}
			if (@int < Singleton<DataManager>.Instance.iLoveMaxAll)
			{
				int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_FullLoveTime");
				int2 -= Singleton<LevelManager>.Instance.ResTime * num;
				Singleton<DataManager>.Instance.SaveUserDate("DB_FullLoveTime", int2);
			}
			@int += iCount;
			Singleton<DataManager>.Instance.SaveUserDate("DB_LoveCount", @int);
		}
	}

	public void LogBubbleKill(int iKillBubbleCount)
	{
		int num = Singleton<UserLevelManager>.Instance.GetUserLevel();
		int num2 = Singleton<LevelManager>.Instance.iNowSelectLevelIndex;
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex < 31)
		{
			num = 0;
		}

	}

	public void LogBubbleDown(int GangID, int iDownGangCount)
	{
		int num = Singleton<UserLevelManager>.Instance.GetUserLevel();
		int num2 = Singleton<LevelManager>.Instance.iNowSelectLevelIndex;
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex < 31)
		{
			num = 0;
		}

	}

	public void LogGangUSE(int GangID)
	{
		int num = Singleton<UserLevelManager>.Instance.GetUserLevel();
		int num2 = Singleton<LevelManager>.Instance.iNowSelectLevelIndex;
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex < 31)
		{
			num = 0;
		}
		FireBase.Action.UnityWriteLog("LOG_GangUSE", num2 + "|" + num + "|" + GangID);
		//GA.Use("Gang" + GangID, 1, 25.0);
	}

	public void LogSKILL_USE(int SkillID, int iGameScene = 1)
	{
		Singleton<UserManager>.Instance.SetPassTask("SKILL" + SkillID);
		int num = Singleton<UserLevelManager>.Instance.GetUserLevel();
		int num2 = Singleton<LevelManager>.Instance.iNowSelectLevelIndex;
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex < 31)
		{
			num = 0;
		}
		int num3 = 18;
		switch (SkillID)
		{
		case 3:
			num3 = 20;
			break;
		case 4:
			num3 = 12;
			break;
		}
		//GA.Use("Skill" + SkillID, 1, num3);
	}
}
