
using UnityEngine;
using UnityEngine.UI;

public class MapRewardPanelSon : MonoBehaviour
{
	public Text StarCount;

	public GameObject RewardIcon1;

	public Text RewardCount1;

	public GameObject RewardBtn;

	public Text RewardBtnText;

	public GameObject RewardPass;

	public Sprite[] LRewardIcon;

	public Sprite RewardBtnSprite;

	public Sprite RewardPassSpEn;

	private bool bReward;

	private int _TempiMap;

	private int _TempiIndex;

	private int RType1;

	private int RCount1;

	private void Start()
	{
		base.gameObject.SetActive(value: true);
		BaseUIAnimation.action.SetLanguageFont("MapRewardUIBtn", RewardBtnText, string.Empty);
		if (!InitGame.bEnios)
		{
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			Singleton<DataManager>.Instance.bclicktow = false;
		}
	}

	public void SetData()
	{
	}

	public void SetReward(int iMapID, int iIndex, int iMapStart)
	{
		_TempiMap = iMapID;
		_TempiIndex = iIndex;
		int mapStar = Singleton<UserManager>.Instance.GetMapStar(iMapID - 1);
		int num = 1;
		while (true)
		{
			if (num > Singleton<DataManager>.Instance.dDataMapReward.Count)
			{
				return;
			}
			int num2 = int.Parse(Singleton<DataManager>.Instance.dDataMapReward[num.ToString()]["Mapid"]);
			if (num2 == iMapID)
			{
				int num3 = int.Parse(Singleton<DataManager>.Instance.dDataMapReward[num.ToString()]["inumber"]);
				if (num3 == iIndex)
				{
					break;
				}
			}
			num++;
		}
		int num4 = int.Parse(Singleton<DataManager>.Instance.dDataMapReward[num.ToString()]["iStar"]);
		int num5 = int.Parse(Singleton<DataManager>.Instance.dDataMapReward[num.ToString()]["RewardCount"]);
		int num6 = int.Parse(Singleton<DataManager>.Instance.dDataMapReward[num.ToString()]["Reward"]);
		StarCount.text = string.Empty + mapStar + "/" + num4;
		RewardCount1.text = "x" + num5;
		RewardIcon1.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num6, 138, 114);
		RewardIcon1.GetComponent<Image>().SetNativeSize();
		RType1 = num6;
		RCount1 = num5;
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_MapReward" + _TempiMap + "_" + _TempiIndex);
		if (@int == 1)
		{
			RewardPass.SetActive(value: true);
			RewardBtn.SetActive(value: false);
			return;
		}
		RewardPass.SetActive(value: false);
		RewardBtn.SetActive(value: true);
		if (iMapStart >= num4)
		{
			bReward = true;
			return;
		}
		RewardBtn.GetComponent<Image>().sprite = RewardBtnSprite;
		RewardBtn.GetComponent<Button>().enabled = false;
	}

	public void ClickReward()
	{
		if (Singleton<DataManager>.Instance.bclicktow)
		{
			return;
		}
		Singleton<DataManager>.Instance.bclicktow = true;
		if (bReward)
		{
			bReward = false;
			Singleton<DataManager>.Instance.SaveUserDate("DB_MapReward" + _TempiMap + "_" + _TempiIndex, 1);
			//Analytics.Event("MapStarReward", "Reward_" + _TempiMap + "_" + _TempiIndex);
			ChinaPay.action.addRewardAll(RType1, RCount1, MapRewardUIPanel.panel.gameObject, isShow: false);
			BaseUIAnimation.action.ShowProp(RType1, RCount1, MapRewardUIPanel.panel.gameObject);
			RewardPass.SetActive(value: true);
			RewardBtn.SetActive(value: false);
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_First_Reward") == 0)
			{
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_First_Reward", 1);
			}
			MapUI.action._MapRewardHondianRes(_TempiMap - 1);
			InitAndroid.action.GAEvent("RewardMapRewardUIPanel");
		}
	}
}
