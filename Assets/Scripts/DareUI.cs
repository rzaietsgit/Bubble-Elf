using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DareUI : BaseUI
{
	public static DareUI action;

	public GameObject EnterBtn;

	public GameObject CloseBtn;

	public Text LevelRemarkText;

	public GameObject SelectSkillFather;

	public GameObject SelectSkillObj;

	public Text tiaozhan1;

	public Text tiaozhan5;

	public Text tiaozhan3;

	public Text Login_Play;

	public GameObject FreeObj;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.DareUI;
	}

	private void LoadSelectSkill()
	{
		for (int i = 1; i <= 3; i++)
		{
			GameObject gameObject = Object.Instantiate(SelectSkillObj);
			gameObject.transform.SetParent(SelectSkillFather.transform, worldPositionStays: false);
			gameObject.SetActive(value: true);
			PlaySkillSelect component = gameObject.GetComponent<PlaySkillSelect>();
			component.SetType(i);
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Skill_Select_" + i);
			if (@int == 1)
			{
				component.LoadSelect();
			}
		}
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
		CloseUI(bDouble);
	}

	private void Update()
	{
	}

	public override void OnStart()
	{
		action = this;
		BaseUIAnimation.action.SetLanguageFont("tiaozhan1", tiaozhan1, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("tiaozhan5", tiaozhan5, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("tiaozhan3", tiaozhan3, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("Login_Play", Login_Play, string.Empty);
		LevelRemarkText.text = 2 - Singleton<DataManager>.Instance.iDareCount + "/2";
		LoadSelectSkill();
		BaseUIAnimation.action.CreateButton(EnterBtn.gameObject);
		string nowTime_Day = Util.GetNowTime_Day();
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_DareCount" + nowTime_Day) <= 0)
		{
			FreeObj.SetActive(value: false);
			Login_Play.transform.localPosition = new Vector3(0f, 0f, 0f);
		}
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
		string nowTime_Day = Util.GetNowTime_Day();
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_DareCount" + nowTime_Day) > 0)
		{
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
		}
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
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_DareCount" + nowTime_Day, Singleton<DataManager>.Instance.iDareCount);
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
