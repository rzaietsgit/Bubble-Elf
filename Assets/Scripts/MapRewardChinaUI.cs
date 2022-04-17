using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MapRewardChinaUI : BaseUI
{
	public static MapRewardChinaUI action;

	public GameObject CloseBtn;

	public Text MapRewardUITitle;

	public Text MapRewardUIRemark;

	public Text MapRewardUIRemark2;

	public GameObject passline;

	public Text StarCount;

	public GameObject groupFather;

	public GameObject RewardSon;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.MapRewardUI;
	}

	private void Start()
	{
		action = this;
		int iSelectMapRewardID = Singleton<DataManager>.Instance.iSelectMapRewardID;
		int num = Singleton<DataManager>.Instance.LMapBtnCount[iSelectMapRewardID] * 3;
		int mapStar = Singleton<UserManager>.Instance.GetMapStar(iSelectMapRewardID);
		BaseUIAnimation.action.SetLanguageFont("MapNameRemark" + (iSelectMapRewardID + 1), MapRewardUITitle, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("MapRewardUIRemark1", MapRewardUIRemark, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("MapRewardUIRemark2", MapRewardUIRemark2, string.Empty);
		for (int i = 1; i <= 3; i++)
		{
			GameObject gameObject = Object.Instantiate(RewardSon);
			gameObject.transform.SetParent(groupFather.transform, worldPositionStays: false);
			MapRewardPanelSon component = gameObject.GetComponent<MapRewardPanelSon>();
			component.SetReward(iSelectMapRewardID + 1, i, mapStar);
			gameObject.SetActive(value: true);
		}
		StarCount.text = mapStar + "/" + num;
		float num2 = 0.001f;
		num2 = (float)mapStar * 100f / (float)num * 100f;
		num2 /= 10000f;
		passline.GetComponent<Image>().fillAmount = num2;
	}

	private void Update()
	{
	}

	private void Awake()
	{
		Canvas component = base.gameObject.transform.GetComponent<Canvas>();
		component.renderMode = RenderMode.ScreenSpaceCamera;
		component.worldCamera = MapUI.action.MapUISceneCamera.GetComponent<Camera>();
	}

	public void _CloseMapRewardUI()
	{
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
			StartCoroutine(CallCloseUI());
		}
	}

	private IEnumerator CallCloseUI(bool bDouble = false, bool bExit = true)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseUI(bDouble);
	}
}
