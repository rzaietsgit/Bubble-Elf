using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DareLoseUI : BaseUI
{
	public static DareLoseUI action;

	public GameObject EnterBtn;

	public GameObject CloseBtn;

	public Text LoseUILevelFailedText;

	public Text LoseUILevelRestartText;

	public Text tiaozhan2;

	public Text tiaozhan3;

	public Text tiaozhan3Count;

	public GameObject BuyImg;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.DareLoseUI;
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
		BaseUIAnimation.action.SetLanguageFont("LoseUILevelFailedText", LoseUILevelFailedText, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("tiaozhan2", tiaozhan2, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("tiaozhan3", tiaozhan3, string.Empty);
		if (Singleton<DataManager>.Instance.iDareCount >= 2)
		{
			BaseUIAnimation.action.SetLanguageFont("QuitUIQuitbtn", LoseUILevelRestartText, string.Empty);
		}
		else
		{
			LoseUILevelRestartText.text = "50";
			LoseUILevelRestartText.transform.localPosition = new Vector3(40f, 4f, 0f);
			BuyImg.SetActive(value: true);
		}
		tiaozhan3Count.text = 2 - Singleton<DataManager>.Instance.iDareCount + "/2";
		BaseUIAnimation.action.CreateButton(EnterBtn.gameObject);
		Reward();
	}

	private void Reward()
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		if (Singleton<LevelManager>.Instance.dareIndex >= 2)
		{
			list.Add(3);
			list2.Add(15);
			ChinaPay.action.addRewardAll(3, 15, DareWinUI.action.gameObject, isShow: false);
		}
		if (Singleton<LevelManager>.Instance.dareIndex >= 3)
		{
			list.Add(11);
			list2.Add(1);
			ChinaPay.action.addRewardAll(11, 1, DareWinUI.action.gameObject, isShow: false);
		}
		if (Singleton<LevelManager>.Instance.dareIndex >= 4)
		{
			int num = Random.Range(5, 8);
			list.Add(num);
			list2.Add(1);
			ChinaPay.action.addRewardAll(num, 1, DareWinUI.action.gameObject, isShow: false);
		}
		BaseUIAnimation.action.ShowProp(list, list2, DareWinUI.action.gameObject);
	}

	protected override void OnAwake()
	{
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
