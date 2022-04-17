using UnityEngine;
using UnityEngine.UI;

public class PlaySkillSelect : MonoBehaviour
{
	private int iSkillType = 1;

	public Sprite[] LSkillSprite;

	public Text SkillCountText;

	public Text UnSkillRemark;

	public GameObject SkillSelect;

	public GameObject SkillNumberBg;

	public GameObject bag_icon_timelimit;

	public Sprite Sp_bag_icon_timelimit;

	public bool bSelect;

	private void Start()
	{
		if (InitGame.bEnios)
		{
			bag_icon_timelimit.GetComponent<Image>().sprite = Sp_bag_icon_timelimit;
		}
	}

	public void LoadSKillType()
	{
		int num = iSkillType;
		switch (num)
		{
		case 0:
			num = 0;
			break;
		case 1:
			num = 1;
			break;
		case 2:
			num = 2;
			break;
		case 3:
			num = 3;
			break;
		}
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SkillOpen_" + num);
		if (@int == 1)
		{
			GetComponent<Image>().sprite = LSkillSprite[iSkillType];
			UnSkillRemark.gameObject.SetActive(value: false);
			return;
		}
		if (InitGame.bChinaVersion)
		{
			UnSkillRemark.gameObject.SetActive(value: true);
			BaseUIAnimation.action.SetLanguageFont("UnSkillRemark" + iSkillType, UnSkillRemark, string.Empty);
		}
		else
		{
			UnSkillRemark.gameObject.SetActive(value: false);
		}
		GetComponent<Image>().sprite = LSkillSprite[4];
		SkillNumberBg.SetActive(value: false);
	}

	public void LoadSKillCount()
	{
		int num = PayManager.action.GetSkillCount(iSkillType);
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 16 && iSkillType == 1)
		{
			num++;
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 28 && iSkillType == 2)
		{
			num++;
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 81 && iSkillType == 0)
		{
			num++;
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 61 && iSkillType == 3)
		{
			num++;
		}
		if (num <= 0)
		{
			num = 0;
		}
		if (num == 0)
		{
			SkillCountText.text = "+";
		}
		else
		{
			SkillCountText.text = num.ToString();
			SkillCountText.fontSize = 25;
		}
		if (InitGame.bChinaVersion && PayManager.action.GetSkillTimeCount(iSkillType) > 0)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SkillOpen_" + iSkillType);
			if (@int == 1)
			{
				bag_icon_timelimit.SetActive(value: true);
			}
		}
	}

	public void SetType(int iType)
	{
		iSkillType = iType;
		LoadSKillType();
		LoadSKillCount();
	}

	public void ClickSelectSkill()
	{
		if (PlayPanel.panel.bLoveFly)
		{
			return;
		}
		InitAndroid.action.GAEvent("clickbtn:ClickSelectSkill:" + iSkillType);
		if (iSkillType == PlayPanel.panel.ClickGuide)
		{
			PlayPanel.panel.ClickGuide = 0;
		}
		if (PlayPanel.panel.ClickGuide == 4 && iSkillType == 0)
		{
			PlayPanel.panel.ClickGuide = 0;
		}
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SkillOpen_" + iSkillType) == 0)
		{
			return;
		}
		DataManager.iSkillOpenType = iSkillType;
		int num = PayManager.action.GetSkillCount(iSkillType);
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 16 && iSkillType == 1)
		{
			num++;
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 28 && iSkillType == 2)
		{
			num++;
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 61 && iSkillType == 3)
		{
			num++;
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 81 && iSkillType == 0)
		{
			num++;
		}
		if (num == 0)
		{
			if (InitGame.bChinaVersion)
			{
				UI.Instance.OpenPanel(UIPanelType.BuySkillUI);
			}
			else
			{
				UI.Instance.OpenPanel(UIPanelType.BuySkillUI);
			}
		}
		else
		{
			LoadSelect();
		}
	}

	public void LoadSelect()
	{
		if (bSelect)
		{
			bSelect = false;
			SkillCountText.gameObject.SetActive(value: true);
			SkillSelect.SetActive(value: false);
			Singleton<DataManager>.Instance.SaveUserDate("DB_Skill_Select_" + iSkillType, 0);
		}
		else
		{
			bSelect = true;
			SkillCountText.gameObject.SetActive(value: false);
			SkillSelect.SetActive(value: true);
			Singleton<DataManager>.Instance.SaveUserDate("DB_Skill_Select_" + iSkillType, 1);
		}
	}

	private void Update()
	{
	}
}
