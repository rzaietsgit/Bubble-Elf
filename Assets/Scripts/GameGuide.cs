using Spine.Unity;
using System;
using System.Collections;
using UnityEngine;

public class GameGuide : MonoBehaviour
{
	public static GameGuide Instance;

	public int iGuideStep = 1;

	public GameObject shouzhi;

	public GameObject jiantou;

	public GameObject MT1;

	public GameObject MT2;

	public GameObject MT4;

	public GameObject MT5;

	public bool isCanShoot = true;

	private void Start()
	{
		Instance = this;
	}

	public void initGuide()
	{
		iGuideStep = 1;
		nextGuide();
	}

	public void nextGuide()
	{
		switch (Singleton<LevelManager>.Instance.iNowSelectLevelIndex)
		{
		case 1:
			Level1();
			break;
		case 2:
			Level2();
			break;
		case 3:
			Level3();
			break;
		case 4:
			Level4();
			break;
		case 5:
			Level5();
			break;
		case 6:
			Level6();
			break;
		case 8:
			Level8();
			break;
		case 81:
			Level81();
			break;
		case 11:
			Level11();
			break;
		case 12:
			Level12();
			break;
		case 21:
			Level21();
			break;
		case 16:
			Level16();
			break;
		case 18:
			Level18();
			break;
		case 14:
			Level14();
			break;
		case 28:
			Level28();
			break;
		case 46:
			Level46();
			break;
		case 56:
			Level56();
			break;
		case 25:
			Level25();
			break;
		case 24:
			Level24();
			break;
		case 27:
			Level27();
			break;
		case 41:
			Level41();
			break;
		case 44:
			Level44();
			break;
		case 51:
			Level51();
			break;
		case 36:
			Level36();
			break;
		case 76:
			Level76();
			break;
		case 101:
			Level101();
			break;
		case 126:
			Level126();
			break;
		case 151:
			Level151();
			break;
		case 301:
			Level301();
			break;
		case 401:
			Level451();
			break;
		case 501:
			Level501();
			break;
		case 601:
			Level551();
			break;
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex >= 80000 && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_BossGuide") == 0)
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_BossGuide", 1);
			Level80000();
		}
	}

	private void Level25()
	{
		if (iGuideStep == 1)
		{
			jiantou.SetActive(value: true);
			jiantou.transform.localPosition = new Vector3(-0.26f, 1.64f, 0f);
		}
		else if (iGuideStep == 2)
		{
			jiantou.SetActive(value: false);
		}
	}

	public void usedownSkill()
	{
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 56 && iGuideStep == 2)
		{
			iGuideStep++;
			nextGuide();
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 46 && iGuideStep == 2)
		{
			iGuideStep++;
			nextGuide();
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 21 && iGuideStep == 2)
		{
			iGuideStep++;
			nextGuide();
		}
	}

	public void useSkill()
	{
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 5)
		{
			iGuideStep++;
			nextGuide();
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 14 && iGuideStep == 2)
		{
			iGuideStep++;
			nextGuide();
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 14 && iGuideStep == 1)
		{
			iGuideStep++;
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 27)
		{
			iGuideStep++;
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 27 && iGuideStep == 4)
		{
			nextGuide();
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 23 && iGuideStep == 2)
		{
			iGuideStep++;
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 44 && iGuideStep == 4)
		{
			iGuideStep++;
			nextGuide();
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 44 && iGuideStep == 3)
		{
			iGuideStep++;
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 44 && iGuideStep == 2)
		{
			iGuideStep++;
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 44 && iGuideStep == 1)
		{
			iGuideStep++;
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 24 && iGuideStep == 3)
		{
			iGuideStep++;
			nextGuide();
		}
	}

	public void SkillOk()
	{
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 5 && iGuideStep == 1)
		{
			iGuideStep++;
			nextGuide();
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 11 && iGuideStep == 1)
		{
			iGuideStep++;
			nextGuide();
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 24 && iGuideStep == 1)
		{
			iGuideStep++;
			nextGuide();
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 24 && iGuideStep == 2)
		{
			iGuideStep++;
			nextGuide();
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 24 && iGuideStep == 4)
		{
			iGuideStep++;
			nextGuide();
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 41 && iGuideStep == 1)
		{
			iGuideStep++;
			nextGuide();
		}
	}

	public void shootBubble()
	{
		if (GuideMinUIPanel.panel != null)
		{
			UI.Instance.ClosePanel();
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 6 && iGuideStep == 1)
		{
			iGuideStep++;
			nextGuide();
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 12 && iGuideStep == 1)
		{
			iGuideStep++;
			nextGuide();
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 1 && iGuideStep == 1)
		{
			iGuideStep++;
			nextGuide();
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 25 && iGuideStep == 1)
		{
			iGuideStep++;
			nextGuide();
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 4 && iGuideStep == 1)
		{
			iGuideStep++;
			nextGuide();
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 5 && iGuideStep == 1)
		{
			jiantou.SetActive(value: false);
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 7 && iGuideStep == 1)
		{
			jiantou.SetActive(value: false);
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 8 && iGuideStep == 1)
		{
			iGuideStep++;
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 11 && iGuideStep == 1)
		{
			jiantou.SetActive(value: false);
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 24 && iGuideStep == 1)
		{
			jiantou.SetActive(value: false);
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 24 && iGuideStep == 4)
		{
			jiantou.SetActive(value: false);
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 41 && iGuideStep == 1)
		{
			jiantou.SetActive(value: false);
		}
	}

	public void OpenSkillUI()
	{
		if ((bool)GuideMinUI.action)
		{
			GuideMinUI.action.CloseUI();
		}
		if ((bool)GuideMinUIPanel.panel && UI.Instance.GetTopPanelType() == UIPanelType.GuideMinPanel)
		{
			UI.Instance.ClosePanel();
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 8 && iGuideStep == 1)
		{
			iGuideStep++;
			MT2.GetComponent<MuTong>().shouzhi.SetActive(value: false);
			nextGuide();
		}
	}

	public void changeBubble()
	{
		if ((bool)GuideMinUI.action)
		{
			GuideMinUI.action.CloseUI();
		}
		if ((bool)GuideMinUIPanel.panel && UI.Instance.GetTopPanelType() == UIPanelType.GuideMinPanel)
		{
			UI.Instance.ClosePanel();
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 3 && iGuideStep == 1)
		{
			iGuideStep++;
			nextGuide();
		}
	}

	private void Level80000()
	{
		if (iGuideStep == 1)
		{
			isCanShoot = false;
			UI.Instance.OpenPanel(UIPanelType.GuideMinPanel);
		}
	}

	private void Level1()
	{
		if (iGuideStep == 1)
		{
			jiantou.SetActive(value: false);
			isCanShoot = true;
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
		}
	}

	private void Level2()
	{
		if (iGuideStep == 1)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
		}
	}

	private void Level3()
	{
		if (iGuideStep == 1)
		{
			shouzhi.SetActive(value: true);
			isCanShoot = false;
			UI.Instance.OpenPanel(UIPanelType.GuideMinPanel);
		}
		if (iGuideStep == 2)
		{
			isCanShoot = true;
			shouzhi.SetActive(value: false);
		}
	}

	private void Level4()
	{
		if (iGuideStep == 1)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMinPanel);
			jiantou.SetActive(value: true);
			jiantou.transform.localPosition = new Vector3(-2f, 1f, 0f);
			IEnumerator enumerator = jiantou.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.Current;
					SkeletonAnimation component = transform.GetComponent<SkeletonAnimation>();
					component.Initialize(overwrite: true);
					component.state.SetAnimation(0, "add3", loop: true);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
		if (iGuideStep == 2)
		{
			jiantou.SetActive(value: false);
		}
	}

	private void Level5()
	{
		if (iGuideStep == 1)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMinPanel);
			jiantou.SetActive(value: true);
			jiantou.transform.localPosition = new Vector3(0f, -1f, 0f);
		}
		if (iGuideStep == 2)
		{
			isCanShoot = false;
			UI.Instance.OpenPanel(UIPanelType.GuideMinPanel);
			MT2.GetComponent<MuTong>().shouzhi.SetActive(value: true);
		}
		if (iGuideStep == 3)
		{
			if ((bool)GuideMinUI.action)
			{
				GuideMinUI.action.CloseUI();
			}
			if ((bool)GuideMinUIPanel.panel && UI.Instance.GetTopPanelType() == UIPanelType.GuideMinPanel)
			{
				UI.Instance.ClosePanel();
			}
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
			MT2.GetComponent<MuTong>().shouzhi.SetActive(value: true);
		}
	}

	private void Level7()
	{
		if (iGuideStep == 1)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMinPanel);
			if ((bool)GuideMinUI.action)
			{
				GuideMinUI.action.InitUI();
			}
			jiantou.SetActive(value: true);
			jiantou.transform.localPosition = new Vector3(-2.2f, 0.3f, 0f);
		}
		if (iGuideStep != 2)
		{
		}
	}

	private void Level6()
	{
		if (iGuideStep == 1)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMinPanel);
		}
		if (iGuideStep == 2)
		{
			if ((bool)GuideMinUI.action)
			{
				GuideMinUI.action.CloseUI();
			}
			if ((bool)GuideMinUIPanel.panel && UI.Instance.GetTopPanelType() == UIPanelType.GuideMinPanel)
			{
				UI.Instance.ClosePanel();
			}
		}
	}

	private void Level12()
	{
		if (iGuideStep == 1)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMinPanel);
		}
		if (iGuideStep == 2)
		{
			if ((bool)GuideMinUI.action)
			{
				GuideMinUI.action.CloseUI();
			}
			if ((bool)GuideMinUIPanel.panel && UI.Instance.GetTopPanelType() == UIPanelType.GuideMinPanel)
			{
				UI.Instance.ClosePanel();
			}
		}
	}

	private void Level8()
	{
	}

	private void Level11()
	{
		if (iGuideStep == 1)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMinPanel);
			if ((bool)GuideMinUI.action)
			{
				GuideMinUI.action.InitUI();
			}
			jiantou.SetActive(value: true);
			jiantou.transform.localPosition = new Vector3(0f, -1f, 0f);
		}
		if (iGuideStep == 2)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
			if ((bool)GuideMaxUI.action)
			{
				GuideMaxUI.action.InitUI();
			}
			MT4.GetComponent<MuTong>().shouzhi.SetActive(value: true);
		}
	}

	private void Level21()
	{
		if (iGuideStep == 1)
		{
			Singleton<DataManager>.Instance.bopenMaxGuide = true;
			iGuideStep++;
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
		}
		else if (iGuideStep == 2)
		{
			jiantou.SetActive(value: true);
			jiantou.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
			jiantou.transform.localPosition = new Vector3(-1.89f, -4.22f, 0f);
		}
		else if (iGuideStep == 3)
		{
			jiantou.SetActive(value: false);
		}
	}

	private void Level14()
	{
		if (iGuideStep == 1)
		{
			isCanShoot = false;
			UI.Instance.OpenPanel(UIPanelType.GuideMinPanel);
			if ((bool)GuideMinUI.action)
			{
				GuideMinUI.action.InitUI();
			}
			MT2.GetComponent<MuTong>().shouzhi.SetActive(value: true);
			MT2.GetComponent<MuTong>().AddSkill();
			MT4.GetComponent<MuTong>().shouzhi.SetActive(value: true);
			MT4.GetComponent<MuTong>().AddSkill();
		}
		if (iGuideStep == 3)
		{
			MT2.GetComponent<MuTong>().shouzhi.SetActive(value: false);
			MT4.GetComponent<MuTong>().shouzhi.SetActive(value: false);
			isCanShoot = true;
			if ((bool)GuideMinUI.action)
			{
				GuideMinUI.action.CloseUI();
			}
			if ((bool)GuideMinUIPanel.panel && UI.Instance.GetTopPanelType() == UIPanelType.GuideMinPanel)
			{
				UI.Instance.ClosePanel();
			}
		}
	}

	private void Level18()
	{
		if (iGuideStep == 1)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
			if ((bool)GuideMaxUI.action)
			{
				GuideMaxUI.action.InitUI();
			}
		}
	}

	private void Level16()
	{
		if (iGuideStep == 1)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
			if ((bool)GuideMaxUI.action)
			{
				GuideMaxUI.action.InitUI();
			}
		}
	}

	private void Level28()
	{
		if (iGuideStep == 1)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
		}
	}

	private void Level56()
	{
		if (iGuideStep == 1)
		{
			Singleton<DataManager>.Instance.bopenMaxGuide = true;
			iGuideStep++;
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
		}
		else if (iGuideStep == 2)
		{
			jiantou.SetActive(value: true);
			jiantou.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
			jiantou.transform.localPosition = new Vector3(1.38f, -4.22f, 0f);
		}
		else if (iGuideStep == 3)
		{
			jiantou.SetActive(value: false);
		}
	}

	private void Level24()
	{
		if (iGuideStep == 1)
		{
			jiantou.SetActive(value: true);
			jiantou.transform.localPosition = new Vector3(0f, -0.8f, 0f);
			UI.Instance.OpenPanel(UIPanelType.GuideMinPanel);
		}
		if (iGuideStep == 2)
		{
			jiantou.SetActive(value: false);
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
			Singleton<DataManager>.Instance.bopenMaxGuide = true;
		}
		if (iGuideStep == 3)
		{
			MT1.GetComponent<MuTong>().shouzhi.SetActive(value: true);
		}
		if (iGuideStep == 4)
		{
			jiantou.SetActive(value: true);
			jiantou.transform.localPosition = new Vector3(0.4f, 3.36f, 0f);
		}
		if (iGuideStep == 5)
		{
			jiantou.SetActive(value: false);
		}
	}

	private void Level46()
	{
		if (iGuideStep == 1)
		{
			Singleton<DataManager>.Instance.bopenMaxGuide = true;
			iGuideStep++;
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
		}
		else if (iGuideStep == 2)
		{
			jiantou.SetActive(value: true);
			jiantou.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
			jiantou.transform.localPosition = new Vector3(-0.23f, -4.22f, 0f);
		}
		else if (iGuideStep == 3)
		{
			jiantou.SetActive(value: false);
		}
	}

	private void Level27()
	{
		if (iGuideStep == 1)
		{
			isCanShoot = false;
			UI.Instance.OpenPanel(UIPanelType.GuideMinPanel);
			if ((bool)GuideMinUI.action)
			{
				GuideMinUI.action.InitUI();
			}
			MT1.GetComponent<MuTong>().shouzhi.SetActive(value: true);
			MT1.GetComponent<MuTong>().AddSkill();
			MT2.GetComponent<MuTong>().shouzhi.SetActive(value: true);
			MT2.GetComponent<MuTong>().AddSkill();
			MT4.GetComponent<MuTong>().shouzhi.SetActive(value: true);
			MT4.GetComponent<MuTong>().AddSkill();
		}
		if (iGuideStep == 4)
		{
			if ((bool)GuideMinUI.action)
			{
				GuideMinUI.action.CloseUI();
			}
			if ((bool)GuideMinUIPanel.panel && UI.Instance.GetTopPanelType() == UIPanelType.GuideMinPanel)
			{
				UI.Instance.ClosePanel();
			}
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
			if ((bool)GuideMaxUI.action)
			{
				GuideMaxUI.action.InitUI();
			}
			MT1.GetComponent<MuTong>().shouzhi.SetActive(value: false);
			MT2.GetComponent<MuTong>().shouzhi.SetActive(value: false);
			MT4.GetComponent<MuTong>().shouzhi.SetActive(value: false);
			isCanShoot = true;
		}
	}

	private void Level41()
	{
		if (iGuideStep == 1)
		{
			jiantou.SetActive(value: true);
			jiantou.transform.localPosition = new Vector3(0f, -0.8f, 0f);
			UI.Instance.OpenPanel(UIPanelType.GuideMinPanel);
		}
		if (iGuideStep == 2)
		{
			jiantou.SetActive(value: false);
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
		}
	}

	private void Level44()
	{
		if (iGuideStep == 1)
		{
			isCanShoot = false;
			UI.Instance.OpenPanel(UIPanelType.GuideMinPanel);
			if ((bool)GuideMinUI.action)
			{
				GuideMinUI.action.InitUI();
			}
			MT1.GetComponent<MuTong>().shouzhi.SetActive(value: true);
			MT1.GetComponent<MuTong>().AddSkill();
			MT2.GetComponent<MuTong>().shouzhi.SetActive(value: true);
			MT2.GetComponent<MuTong>().AddSkill();
			MT4.GetComponent<MuTong>().shouzhi.SetActive(value: true);
			MT4.GetComponent<MuTong>().AddSkill();
			MT5.GetComponent<MuTong>().shouzhi.SetActive(value: true);
			MT5.GetComponent<MuTong>().AddSkill();
		}
		if (iGuideStep == 5)
		{
			if ((bool)GuideMinUI.action)
			{
				GuideMinUI.action.CloseUI();
			}
			if ((bool)GuideMinUIPanel.panel && UI.Instance.GetTopPanelType() == UIPanelType.GuideMinPanel)
			{
				UI.Instance.ClosePanel();
			}
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
			if ((bool)GuideMaxUI.action)
			{
				GuideMaxUI.action.InitUI();
			}
			MT1.GetComponent<MuTong>().shouzhi.SetActive(value: false);
			MT2.GetComponent<MuTong>().shouzhi.SetActive(value: false);
			MT4.GetComponent<MuTong>().shouzhi.SetActive(value: false);
			MT5.GetComponent<MuTong>().shouzhi.SetActive(value: false);
		}
	}

	private void Level51()
	{
		if (iGuideStep == 1)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
			if ((bool)GuideMaxUI.action)
			{
				GuideMaxUI.action.InitUI();
			}
		}
	}

	private void Level36()
	{
		if (iGuideStep == 1)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
			if ((bool)GuideMaxUI.action)
			{
				GuideMaxUI.action.InitUI();
			}
		}
	}

	private void Level76()
	{
		if (iGuideStep == 1)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
			if ((bool)GuideMaxUI.action)
			{
				GuideMaxUI.action.InitUI();
			}
		}
	}

	private void Level81()
	{
		if (iGuideStep == 1)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
			if ((bool)GuideMaxUI.action)
			{
				GuideMaxUI.action.InitUI();
			}
		}
	}

	private void Level101()
	{
		if (iGuideStep == 1)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
			if ((bool)GuideMaxUI.action)
			{
				GuideMaxUI.action.InitUI();
			}
		}
	}

	private void Level126()
	{
		if (iGuideStep == 1)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
			if ((bool)GuideMaxUI.action)
			{
				GuideMaxUI.action.InitUI();
			}
		}
	}

	private void Level151()
	{
		if (iGuideStep == 1)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
			if ((bool)GuideMaxUI.action)
			{
				GuideMaxUI.action.InitUI();
			}
		}
	}

	private void Level201()
	{
		if (iGuideStep == 1)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
			if ((bool)GuideMaxUI.action)
			{
				GuideMaxUI.action.InitUI();
			}
		}
	}

	private void Level301()
	{
		if (iGuideStep == 1)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
			if ((bool)GuideMaxUI.action)
			{
				GuideMaxUI.action.InitUI();
			}
		}
	}

	private void Level451()
	{
		if (iGuideStep == 1)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
			if ((bool)GuideMaxUI.action)
			{
				GuideMaxUI.action.InitUI();
			}
		}
	}

	private void Level501()
	{
		if (iGuideStep == 1)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
			if ((bool)GuideMaxUI.action)
			{
				GuideMaxUI.action.InitUI();
			}
		}
	}

	private void Level551()
	{
		if (iGuideStep == 1)
		{
			UI.Instance.OpenPanel(UIPanelType.GuideMaxPanel);
			if ((bool)GuideMaxUI.action)
			{
				GuideMaxUI.action.InitUI();
			}
		}
	}
}
