using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MapRewardUI : BaseUI
{
	public static MapRewardUI action;

	public GameObject CloseBtn;

	public Text MapRewardUITitle;

	public Text MapRewardUIRemark;

	public Text MapRewardUIRemark2;

	public GameObject passline;

	public Text StarCount;

	public GameObject groupFather;

	public GameObject RewardSon;

	public Image pingziImg;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.MapRewardUI;
	}

	private void Start()
	{
		DataManager.bbeibaoFlay = false;
		action = this;
		int num = Singleton<DataManager>.Instance.iSelectMapRewardID - 1;
		int num2 = Singleton<DataManager>.Instance.LMapBtnCount[num] * 3;
		int mapStar = Singleton<UserManager>.Instance.GetMapStar(num);
		BaseUIAnimation.action.SetLanguageFont("MapNameRemark" + (num + 1), MapRewardUITitle, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("MapRewardUIRemark1", MapRewardUIRemark, string.Empty);
		for (int i = 1; i <= 3; i++)
		{
			GameObject gameObject = Object.Instantiate(RewardSon);
			gameObject.transform.SetParent(groupFather.transform, worldPositionStays: false);
			MapRewardPanelSon component = gameObject.GetComponent<MapRewardPanelSon>();
			component.SetReward(num + 1, i, mapStar);
			gameObject.SetActive(value: true);
		}
		StarCount.text = mapStar + "/" + num2;
		float num3 = 0.001f;
		num3 = (float)mapStar * 100f / (float)num2 * 100f;
		num3 /= 10000f;
		passline.GetComponent<Image>().fillAmount = num3;
		if (num3 == 0f)
		{
			pingziImg.sprite = Util.GetResourcesSprite("Img/levelmap/level_starcatcher_0", 173, 168);
		}
		else if (num3 >= 1f)
		{
			pingziImg.sprite = Util.GetResourcesSprite("Img/levelmap/level_starcatcher_3", 173, 168);
		}
		else if (num3 >= 0.6f)
		{
			pingziImg.sprite = Util.GetResourcesSprite("Img/levelmap/level_starcatcher_2", 173, 168);
		}
		else
		{
			pingziImg.sprite = Util.GetResourcesSprite("Img/levelmap/level_starcatcher_1", 173, 168);
		}
		string newValue = Singleton<DataManager>.Instance.dDataMapReward[(Singleton<DataManager>.Instance.iSelectMapRewardID * 3).ToString()]["mapnumber"].ToString().Split('!')[0];
		string newValue2 = Singleton<DataManager>.Instance.dDataMapReward[(Singleton<DataManager>.Instance.iSelectMapRewardID * 3).ToString()]["mapnumber"].ToString().Split('!')[1];
		string text = Singleton<DataManager>.Instance.dDataLanguage["MapRewardUI1"][BaseUIAnimation.Language];
		text = text.Replace("A1", newValue);
		text = text.Replace("A2", newValue2);
		MapRewardUIRemark2.text = text;
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

	public void MapRewardUIClose()
	{
		StartCoroutine(IEMapRewardUIClose());
	}

	private IEnumerator IEMapRewardUIClose()
	{
		yield return new WaitForSeconds(3f);
		CloseUI();
	}

	public void _CloseMapRewardUI()
	{
		if (!DataManager.bbeibaoFlay && BaseUIAnimation.bClickButton)
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
