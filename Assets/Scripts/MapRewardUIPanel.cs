using UnityEngine;

public class MapRewardUIPanel : MapRewardUIPanelBase
{
	public static MapRewardUIPanel panel;

	public override void InitUI()
	{
		panel = this;
		DataManager.bbeibaoFlay = false;
		int num = Singleton<DataManager>.Instance.iSelectMapRewardID - 1;
		int num2 = Singleton<DataManager>.Instance.LMapBtnCount[num] * 3;
		int mapStar = Singleton<UserManager>.Instance.GetMapStar(num);
		BaseUIAnimation.action.SetLanguageFont("MapNameRemark" + (num + 1), detail.MapRewardUITitle_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("MapRewardUIRemark1", detail.MapRewardUIRemark_Text, string.Empty);
		for (int i = 1; i <= 3; i++)
		{
			GameObject gameObject = Object.Instantiate(detail.p_reward_Image.gameObject);
			gameObject.transform.SetParent(detail.group_GridLayoutGroup.gameObject.transform, worldPositionStays: false);
			MapRewardPanelSon component = gameObject.GetComponent<MapRewardPanelSon>();
			component.SetReward(num + 1, i, mapStar);
			gameObject.SetActive(value: true);
		}
		detail.StarCount_Text.text = mapStar + "/" + num2;
		float num3 = 0.001f;
		num3 = (float)mapStar * 100f / (float)num2 * 100f;
		num3 /= 10000f;
		detail.passline_Image.fillAmount = num3;
		if (num3 == 0f)
		{
			detail.Image_Image.sprite = Util.GetResourcesSprite("Img/levelmap/level_starcatcher_0", 173, 168);
		}
		else if (num3 >= 1f)
		{
			detail.Image_Image.sprite = Util.GetResourcesSprite("Img/levelmap/level_starcatcher_3", 173, 168);
		}
		else if (num3 >= 0.6f)
		{
			detail.Image_Image.sprite = Util.GetResourcesSprite("Img/levelmap/level_starcatcher_2", 173, 168);
		}
		else
		{
			detail.Image_Image.sprite = Util.GetResourcesSprite("Img/levelmap/level_starcatcher_1", 173, 168);
		}
		string newValue = Singleton<DataManager>.Instance.dDataMapReward[(Singleton<DataManager>.Instance.iSelectMapRewardID * 3).ToString()]["mapnumber"].ToString().Split('!')[0];
		string newValue2 = Singleton<DataManager>.Instance.dDataMapReward[(Singleton<DataManager>.Instance.iSelectMapRewardID * 3).ToString()]["mapnumber"].ToString().Split('!')[1];
		string text = Singleton<DataManager>.Instance.dDataLanguage["MapRewardUI1"][BaseUIAnimation.Language];
		text = text.Replace("A1", newValue);
		text = text.Replace("A2", newValue2);
		detail.MapRewardUIRemark_Text.text = text;
	}
}
