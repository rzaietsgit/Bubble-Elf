using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class BuySkillUI : BaseUI
{
	public static BuySkillUI action;

	public GameObject CloseBtn;

	public GameObject SkillMainIcon;

	public Sprite[] SkillMainSprite;

	public GameObject SkillPanelFather;

	public GameObject SkillPayObj;

	public GameObject GoldIcon;

	public Sprite[] GoldIconSprite;

	public Text GoldText;

	public Text BuySkillTitle;

	public Text BuySkillRemark;

	public GameObject Down;

	private int iSkillType = 1;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.BuySkillUI;
	}

	public override void OnStart()
	{
		action = this;
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
			Down.gameObject.SetActive(value: false);
		}
		//Analytics.Event("ClickSkill" + iSkillType);
		BaseUIAnimation.action.SetLanguageFont("BuySkillRemark" + iSkillType, BuySkillRemark, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BuySkillTitle", BuySkillTitle, string.Empty);
		LoadSkillType();
		LoadSkillList();
		LoadGold();
		if (InitGame.bChinaVersion && !GameUI.action)
		{
		}
	}

	public void LoadGold()
	{
		if (InitGame.bChinaVersion)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GB");
			GoldText.text = @int.ToString();
		}
		else
		{
			int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
			GoldText.text = int2.ToString();
		}
	}

	private void LoadSkillType()
	{
		SkillMainIcon.GetComponent<Image>().sprite = SkillMainSprite[iSkillType - 1];
	}

	private void LoadSkillList()
	{
		for (int i = 1; i <= 3; i++)
		{
			GameObject gameObject = Object.Instantiate(SkillPayObj);
			gameObject.transform.SetParent(SkillPanelFather.transform, worldPositionStays: false);
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
			default:
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

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject gameObject = Util.TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject == null)
			{
				CloseLoseUI();
			}
			else if (gameObject.name.LastIndexOf("BuySkillUI") < 0)
			{
				CloseLoseUI();
			}
		}
	}

	public void CloseLoseUI(bool bDouble = false)
	{
		StartCoroutine(CallCloseUI(bDouble));
	}

	public void _CloseLoseUI()
	{
		if (Singleton<DataManager>.Instance.PlayOpenBuySkill)
		{
			Singleton<DataManager>.Instance.bOpenplay1 = true;
		}
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
			StartCoroutine(CallCloseUI());
		}
	}

	private IEnumerator CallCloseUI(bool bDouble = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseUI(bDouble);
	}

	protected override void OnAwake()
	{
		base.OnAwake();
	}

	protected override void OnRelease()
	{
		base.OnRelease();
	}
}
