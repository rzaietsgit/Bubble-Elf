public static class FirebaseController
{
	public struct HG_TenTime
	{
		public string UID;

		public string Zipcode;

		public string Version;

		public string CreateTime;

		public int LevelID;

		public string NowTime;

		public int IntervalDays;

		public int Stagenum;

		public string Stage;

		public int LevelType;

		public int Life;

		public int Gold;

		public int Move;

		public int LevelNumber;
	}

	public struct HG_User_ltvData
	{
		public string UID;

		public string Zipcode;

		public string Version;

		public string CreateTime;

		public int IsNew;

		public int IntervalDays;

		public string LogInTime;

		public string LogOutTime;

		public int InadSnum;

		public int Adsnum;

		public int Buy;

		public int Maxlevel;

		public int GetCion;

		public int Costcoin;

		public int Gold;

		public string Props;

		public string GetpPops;

		public string CostProps;

		public int Levelnum;

		public int Failnum;

		public int Succnum;
	}

	public struct HG_User_LevelData
	{
		public string UID;

		public int LevelID;

		public int Stagenum;

		public string StarTime;

		public string EndTime;

		public string Success;

		public int Move;
	}

	private static HG_TenTime hg_TenTime;

	private static HG_User_ltvData hg_User_LtvData;

	private static HG_User_LevelData hg_User_LevelData;

	private static bool isInitialized;

	private static bool isStartLevel;

	public static void Initialize()
	{
	}

	private static void Initialize_HG_TenTime()
	{
	}

	private static void Initialize_HG_User_ltvData()
	{
	}

	private static void Initialize_HG_User_LevelData()
	{
	}

	private static void Update_HG_TenTime()
	{
	}

	private static void Update_HG_User_ltvData()
	{
	}

	private static void Update_HG_User_LevelData()
	{
	}

	public static void LoginOrOutGame(bool state)
	{
	}

	public static void Buy(int count)
	{
	}

	public static void AddOrSubStone(int count)
	{
	}

	public static void AddOrSubProp(int type, int count)
	{
	}

	public static void StartLevel()
	{
	}

	public static void ExitLevel(int state)
	{
	}

	public static void PauseLevel(bool state)
	{
	}

	public static void ResumeLevel(bool state)
	{
	}

	public static void RestartLevel(bool state)
	{
	}

	public static void PopFailureBuyForm()
	{
	}

	public static void VideoAddFiveStep()
	{
	}

	public static void StoneAddFiveStep(int num)
	{
	}

	public static void PlayScreenAds()
	{
	}

	public static void PlayVideoAds()
	{
	}
}
