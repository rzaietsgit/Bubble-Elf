using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DareWinUI : BaseUI
{
	public static DareWinUI action;

	public GameObject EnterBtn;

	public GameObject CloseBtn;

	public Text TipWinUITitle;

	public Text tiaozhan4;

	public Text tiaozhan4Count;

	public Text tiaozhan3;

	public Text WinUILevelScoreText;

	public Text LoseUILevelRestartText;

	public Text CountText;

	public Text ScoreText;

	public GameObject BuyImg;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.DareWinUI;
	}

	public void CloseDareUI(bool bDouble = false)
	{
		StartCoroutine(CallCloseUI(bDouble));
	}

	public void _CloseDareUI()
	{
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
			StartCoroutine(CallCloseUI());
		}
	}

	public IEnumerator CallCloseUI(bool bDouble = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		Singleton<LevelManager>.Instance.iNowSelectLevelIndex = Singleton<UserManager>.Instance.iNowPassLevelID;
		Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
	}

	private void Update()
	{
	}

	public override void OnStart()
	{
		action = this;
		BaseUIAnimation.action.SetLanguageFont("TipWinUITitle", TipWinUITitle, string.Empty);
		BaseUIAnimation.action.SetLanguageFont(" tiaozhan4", tiaozhan4, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("tiaozhan3", tiaozhan3, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("WinUILevelScoreText", WinUILevelScoreText, string.Empty);
		if (Singleton<DataManager>.Instance.iDareCount >= 2)
		{
			BaseUIAnimation.action.SetLanguageFont("QuitUIQuitbtn", LoseUILevelRestartText, string.Empty);
		}
		else
		{
			BaseUIAnimation.action.SetLanguageFont("LoseUILevelRestartText", LoseUILevelRestartText, string.Empty);
			LoseUILevelRestartText.transform.localPosition = new Vector3(-69f, 1f, 0f);
			BuyImg.SetActive(value: true);
		}
		BaseUIAnimation.action.CreateButton(EnterBtn.gameObject);
		ScoreText.text = Singleton<DataManager>.Instance.iDareScore.ToString();
		CountText.text = 2 - Singleton<DataManager>.Instance.iDareCount + "/2";
		tiaozhan4Count.text = Singleton<LevelManager>.Instance.dareIndex.ToString();
		Reward();
	}

	private void Reward()
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		if (Singleton<LevelManager>.Instance.dareIndex >= 1)
		{
			list.Add(3);
			list2.Add(15);
			ChinaPay.action.addRewardAll(3, 15, action.gameObject, isShow: false, "free", "darewin");
		}
		if (Singleton<LevelManager>.Instance.dareIndex >= 2)
		{
			list.Add(11);
			list2.Add(1);
			ChinaPay.action.addRewardAll(11, 1, action.gameObject, isShow: false, "free", "darewin");
		}
		if (Singleton<LevelManager>.Instance.dareIndex >= 3)
		{
			int num = Random.Range(5, 8);
			list.Add(num);
			list2.Add(1);
			ChinaPay.action.addRewardAll(num, 1, action.gameObject, isShow: false, "free", "darewin");
		}
		if (Singleton<LevelManager>.Instance.dareIndex >= 4)
		{
			int num2 = Random.Range(7, 10);
			list.Add(num2);
			list2.Add(1);
			ChinaPay.action.addRewardAll(num2, 1, action.gameObject, isShow: false, "free", "darewin");
		}
		BaseUIAnimation.action.ShowProp(list, list2, action.gameObject);
	}

	protected override void OnAwake()
	{
		if ((bool)GameUI.action)
		{
			Canvas component = base.gameObject.transform.GetComponent<Canvas>();
			component.renderMode = RenderMode.ScreenSpaceCamera;
			component.worldCamera = GameUI.action.mainCameraS.GetComponent<Camera>();
		}
		else
		{
			Canvas component2 = base.gameObject.transform.GetComponent<Canvas>();
			component2.renderMode = RenderMode.ScreenSpaceCamera;
			component2.worldCamera = MapUI.action.MapUISceneCamera.GetComponent<Camera>();
		}
		base.OnAwake();
	}

	protected override void OnRelease()
	{
		base.OnRelease();
	}

	public void ClickStart()
	{
		if (Singleton<DataManager>.Instance.iDareCount >= 2)
		{
			Singleton<LevelManager>.Instance.iNowSelectLevelIndex = Singleton<UserManager>.Instance.iNowPassLevelID;
			Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
			return;
		}
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (50 > @int)
		{
			EnumUIType[] uiTypes = new EnumUIType[2]
			{
				EnumUIType.BuyGoldUI,
				EnumUIType.DareUI
			};
			Singleton<UIManager>.Instance.OpenUI(uiTypes);
			CloseUI();
			return;
		}
		PayManager.action.DarePay(50);
		Singleton<LevelManager>.Instance.dareLevels = new List<int>();
		Singleton<LevelManager>.Instance.dareIndex = 0;
		bool flag = false;
		while (!flag)
		{
			int item = Random.Range(1, 15);
			if (!Singleton<LevelManager>.Instance.dareLevels.Contains(item))
			{
				Singleton<LevelManager>.Instance.dareLevels.Add(item);
			}
			if (Singleton<LevelManager>.Instance.dareLevels.Count == 4)
			{
				flag = true;
			}
		}
		Singleton<DataManager>.Instance.iDareCount++;
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_DareCount" + Util.GetNowTime_Day(), Singleton<DataManager>.Instance.iDareCount);
		Singleton<LevelManager>.Instance.iNowSelectLevelIndex = 10000 + Singleton<LevelManager>.Instance.dareLevels[0];
		if (LevelManager.bWwwDataFlag)
		{
			Singleton<LevelManager>.Instance.bLoadOver = false;
			DataManager.SelectLevel = 0;
			Singleton<LevelManager>.Instance.LoadLevelData();
			StartCoroutine(IEStarGame());
		}
		else
		{
			Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.GameScene);
		}
	}

	private IEnumerator IEStarGame()
	{
		bool b = true;
		if (!LevelManager.bWwwDataFlag)
		{
			yield break;
		}
		while (b)
		{
			yield return new WaitForSeconds(0.1f);
			if (Singleton<LevelManager>.Instance.bLoadOver)
			{
				b = false;
				Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.GameScene);
			}
		}
	}
}
