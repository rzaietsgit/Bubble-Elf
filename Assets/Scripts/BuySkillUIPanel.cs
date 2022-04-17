
using UnityEngine;

public class BuySkillUIPanel : BuySkillUIPanelBase
{
	public static BuySkillUIPanel panel;

	private int iSkillType = 1;

	public Sprite[] GoldIconSprite;

	public Sprite[] SkillMainSprite;

	public override void InitUI()
	{
		panel = this;
		iSkillType = DataManager.iSkillOpenType;
		if ((bool)GameUI.action && !InitGame.bChinaVersion)
		{
			if (iSkillType == 1)
			{
				iSkillType = 4;
			}
			else if (iSkillType == 2)
			{
				iSkillType = 5;
			}
			else if (iSkillType == 3)
			{
				iSkillType = 6;
			}
		}
		if (InitGame.bChinaVersion)
		{
			detail.Down_Image.gameObject.SetActive(value: false);
		}
		//Analytics.Event("ClickSkill" + iSkillType);
		UnityEngine.Debug.Log("iSkillType1111 = " + iSkillType);
		InitAndroid.action.GAEvent("ClickSkill" + iSkillType);
		InitAndroid.action.GAEvent("NewClickSkill:" + iSkillType + ":" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
		int num = int.Parse(Singleton<DataManager>.Instance.dDataSkillPriceChina[iSkillType.ToString()]["buyType"]);
		detail.Imagezs_Image.enabled = false;
		detail.Imagegb_Image.enabled = false;
		if (num == 1)
		{
			detail.Imagezs_Image.enabled = true;
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
			detail.moneytext_Text.text = @int.ToString();
		}
		else
		{
			int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GB");
			detail.moneytext_Text.text = int2.ToString();
			detail.Imagegb_Image.enabled = true;
		}
		BaseUIAnimation.action.SetLanguageFont("BuySkillRemark" + iSkillType, detail.BuySkillRemark_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BuySkillTitle", detail.BuySkillTitle_Text, string.Empty);
		LoadSkillType();
		LoadSkillList();
		LoadGold();
		if (InitGame.bChinaVersion && !GameUI.action)
		{
			if (num == 2)
			{
				detail.GoldIcon_Image.sprite = GoldIconSprite[0];
			}
			else
			{
				detail.GoldIcon_Image.sprite = GoldIconSprite[1];
			}
		}
	}

	private void LoadSkillType()
	{
		detail.CenterImg_Image.sprite = SkillMainSprite[iSkillType];
	}

	private void LoadSkillList()
	{
		for (int i = 1; i <= 3; i++)
		{
			GameObject gameObject = Object.Instantiate(detail.BuySkillSon_Image.gameObject);
			gameObject.transform.SetParent(detail.SkillPanel_GridLayoutGroup.gameObject.transform, worldPositionStays: false);
			gameObject.SetActive(value: true);
			BuySkillSon component = gameObject.GetComponent<BuySkillSon>();
			component.SetType(iSkillType);
			switch (i)
			{
			case 1:
			{
				component.SetNumber(1);
				int money2 = int.Parse(Singleton<DataManager>.Instance.dDataSkillPrice[iSkillType.ToString()]["numb1"]);
				if (InitGame.bChinaVersion)
				{
					money2 = int.Parse(Singleton<DataManager>.Instance.dDataSkillPriceChina[iSkillType.ToString()]["numb1"]);
				}
				component.SetMoney(money2);
				break;
			}
			case 2:
			{
				component.SetNumber(3);
				int money3 = int.Parse(Singleton<DataManager>.Instance.dDataSkillPrice[iSkillType.ToString()]["numb3"]);
				if (InitGame.bChinaVersion)
				{
					money3 = int.Parse(Singleton<DataManager>.Instance.dDataSkillPriceChina[iSkillType.ToString()]["numb3"]);
				}
				component.SetMoney(money3);
				break;
			}
			case 3:
			{
				component.SetNumber(9);
				int money = int.Parse(Singleton<DataManager>.Instance.dDataSkillPrice[iSkillType.ToString()]["numb9"]);
				if (InitGame.bChinaVersion)
				{
					money = int.Parse(Singleton<DataManager>.Instance.dDataSkillPriceChina[iSkillType.ToString()]["numb9"]);
				}
				component.SetMoney(money);
				break;
			}
			}
		}
	}

	public void LoadGold()
	{
		if (InitGame.bChinaVersion)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GB");
			detail.GoldText_Text.text = @int.ToString();
		}
		else
		{
			int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
			detail.GoldText_Text.text = int2.ToString();
		}
	}

	public override void OnPauseBase()
	{
	}

	public override void OnResume()
	{
	}

	public override void OnExit()
	{
	}
}
