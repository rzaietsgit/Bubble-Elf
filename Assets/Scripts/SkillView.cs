using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
	public int SkillType;

	public GameObject ViewCountObj;

	public Sprite NullSpriteBg;

	public Sprite SpriteCountBg;

	public Image IconSp;

	public Sprite[] LSkillType;

	public GameObject[] SkillFxObj;

	public TextMeshProUGUI SkillCountText;

	public GameObject bag_icon_timelimit;

	public Sprite SPen;

	public int iSkillCount;

	private int SkillDBID;

	private bool bUse;

	private bool bloadFx = true;

	public int indexyindaoSkillCount;

	public bool bindexyindaoSkillCount = true;

	private void Start()
	{
		if (InitGame.bEnios)
		{
			bag_icon_timelimit.GetComponent<Image>().sprite = SPen;
		}
		if (SkillType == 3)
		{
			IconSp.enabled = false;
		}
	}

	private void InitFxObj(int iSkillID)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(SkillFxObj[iSkillID - 1]);
		gameObject.transform.SetParent(base.transform);
		if (iSkillID == 3 || iSkillID == 2)
		{
			gameObject.transform.localPosition = new Vector3(0f, -9f, 0f);
		}
		else
		{
			gameObject.transform.localPosition = new Vector3(0f, -6f, 0f);
		}
		gameObject.transform.localScale = new Vector3(100f, 100f, 100f);
	}

	public void LoadSkillType(int iType)
	{
		SkillType = iType;
		IconSp.sprite = LSkillType[SkillType - 1];
		if (SkillType == 1)
		{
			SkillDBID = 4;
		}
		else if (SkillType == 2)
		{
			SkillDBID = 5;
		}
		else if (SkillType == 3)
		{
			SkillDBID = 6;
		}
		if (SkillType == 1)
		{
			IconSp.SetNativeSize();
			IconSp.transform.localPosition = new Vector3(0f, -5f, 0f);
			IconSp.transform.localScale = new Vector3(0.9f, 0.9f, 0f);
		}
		if (SkillType == 2)
		{
			IconSp.SetNativeSize();
			IconSp.transform.localPosition = new Vector3(0f, -10f, 0f);
			IconSp.transform.localScale = new Vector3(1f, 1f, 0f);
		}
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SkillOpen_" + SkillDBID) == 0)
		{
			GetComponent<Image>().sprite = LSkillType[3];
			ViewCountObj.SetActive(value: false);
			return;
		}
		if (bloadFx)
		{
			bloadFx = false;
			InitFxObj(iType);
		}
		int skillCount = PayManager.action.GetSkillCount(SkillDBID);
		SetSkillCount(skillCount);
		if (InitGame.bChinaVersion)
		{
			if (PayManager.action.GetSkillTimeCount(SkillDBID) > 0)
			{
				bag_icon_timelimit.SetActive(value: true);
			}
			else
			{
				bag_icon_timelimit.SetActive(value: false);
			}
		}
	}

	public void PlaySkillAni()
	{
	}

	public void CloseSkillAni()
	{
	}

	private void Update()
	{
	}

	private void SetSkillCount(int iCount)
	{
		if (SkillType == 2 && Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 46 && bindexyindaoSkillCount)
		{
			bindexyindaoSkillCount = false;
			indexyindaoSkillCount = 1;
		}
		if (SkillType == 1 && Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 21 && bindexyindaoSkillCount)
		{
			bindexyindaoSkillCount = false;
			indexyindaoSkillCount = 1;
		}
		if (SkillType == 3 && Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 56 && bindexyindaoSkillCount)
		{
			bindexyindaoSkillCount = false;
			indexyindaoSkillCount = 1;
		}
		iCount += indexyindaoSkillCount;
		iSkillCount = iCount;
		if (iCount <= 0)
		{
			ViewCountObj.GetComponent<Image>().sprite = NullSpriteBg;
			SkillCountText.enabled = false;
		}
		else
		{
			ViewCountObj.GetComponent<Image>().sprite = SpriteCountBg;
			SkillCountText.enabled = true;
			SkillCountText.SetText(iCount.ToString());
		}
	}

	public void SkillClick()
	{
		if (PassLevel.bWin)
		{
			return;
		}
		GameGuide.Instance.usedownSkill();
		if (!PassLevel.action.bGameStart || !BubbleSpawner.Instance.isCanUseSkill() || Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SkillOpen_" + SkillDBID) == 0 || Singleton<LevelManager>.Instance.iBubbleCount <= 0 || PassLevel.bWin)
		{
			return;
		}
		if (iSkillCount <= 0 && indexyindaoSkillCount == 0)
		{
			DataManager.iSkillOpenType = SkillType;
			if (InitGame.bChinaVersion)
			{
				UI.Instance.OpenPanel(UIPanelType.BuySkillUIChina);
			}
			else
			{
				UI.Instance.OpenPanel(UIPanelType.BuySkillUI);
			}
		}
		else if (BubbleSpawner.Instance.isCanUseDownSkill())
		{
			if (SkillType == 2)
			{
				BubbleSpawner.Instance.useSkill1_in();
				GameUI.action.StopSkillAniRandmo();
			}
			else if (SkillType == 3)
			{
				BubbleSpawner.Instance.useSkill3_in();
				GameUI.action.StopSkillAniRandmo();
			}
			else if (SkillType == 1)
			{
				BubbleSpawner.Instance.useSkill3();
				GameUI.action.StopSkillAniRandmo();
			}
			if (indexyindaoSkillCount == 1)
			{
				indexyindaoSkillCount = 0;
			}
			else
			{
				PayManager.action.DeleteSkill(SkillDBID);
			}
			Singleton<LevelManager>.Instance.LogSKILL_USE(SkillDBID + 3);
			if ((bool)GameUI.action)
			{
				GameUI.action.ResSkillCount(SkillType);
			}
		}
	}
}
