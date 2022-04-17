public class UserLevelManager : Singleton<UserLevelManager>
{
	public int UserLevelScore = 100;

	private int day;

	public int GetUserLevel()
	{
		return 1;
	}

	public int GetUserLevelScore()
	{
		UserLevelScore = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "UserLevelScore", 100);
		return UserLevelScore;
	}

	public void SetUserLevelScore(int score)
	{
		UserLevelScore = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "UserLevelScore", 100);
		UserLevelScore += score;
		if (UserLevelScore < 0)
		{
			UserLevelScore = 0;
		}
		Singleton<DataManager>.Instance.SaveUserDate("UserLevelScore", UserLevelScore);
	}

	public void GameWin()
	{
		Singleton<DataManager>.Instance.SaveUserDate("UserLevelGamelose" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex, 0);
		int num = int.Parse(Singleton<DataManager>.Instance.dDataUserLevel[GetUserLevel() + string.Empty]["SL"]);
		SetUserLevelScore(-num);
	}

	public void Gamelose()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "UserLevelGamelose" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
		Singleton<DataManager>.Instance.SaveUserDate("UserLevelGamelose" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex, @int + 1);
		FirebaseController.ExitLevel(2);
		@int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "UserLevelGamelose" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
		int num = int.Parse(Singleton<DataManager>.Instance.dDataUserLevel[GetUserLevel() + string.Empty]["S"]);
		if (@int >= num)
		{
			float num2 = float.Parse(Singleton<DataManager>.Instance.dDataUserLevel[GetUserLevel() + string.Empty]["Z"]);
			int num3 = int.Parse(Singleton<DataManager>.Instance.dDataUserLevel[GetUserLevel() + string.Empty]["SL"]);
			int userLevelScore = (int)((float)(@int - num) * num2 * (float)num3);
			SetUserLevelScore(userLevelScore);
		}
	}

	public int CheckPay()
	{
		int result = 0;
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "CheckPay99") > 0)
		{
			return -10;
		}
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "CheckPay49") > 0)
		{
			return -5;
		}
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "CheckPay9") > 0)
		{
			return -2;
		}
		return result;
	}

	public void usePay(int payNum)
	{
		if ((double)payNum == 9.99)
		{
			Singleton<DataManager>.Instance.SaveUserDate("CheckPay9", 1);
		}
		else if ((double)payNum == 49.99)
		{
			Singleton<DataManager>.Instance.SaveUserDate("CheckPay49", 1);
		}
		else if ((double)payNum == 99.99)
		{
			Singleton<DataManager>.Instance.SaveUserDate("CheckPay99", 1);
		}
	}

	public void UseAdded()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "UseAdded");
		Singleton<DataManager>.Instance.SaveUserDate("UseAdded", @int + 1);
	}

	public int GetAdded()
	{
		return Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "UseAdded");
	}

	public void LoginGame()
	{
		int num = Util.GetNowTime() - Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "LoginGameTime", Util.GetNowTime());
		day = num / 86400;
		Singleton<DataManager>.Instance.SaveUserDate("LoginGameTime", Util.GetNowTime());
	}
}
