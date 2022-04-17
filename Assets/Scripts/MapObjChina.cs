using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MapObjChina : MonoBehaviour
{
	public Image IconImage;

	public Text NowStarCount;

	public Text MapNameText;

	public Text[] StarCount;

	public Image RewardBgImage;

	public Image RewardIconImage;

	public Sprite[] LRewardIconImage;

	public Sprite OverSprite;

	public Text LevelCountText;

	public Image Lineobj;

	public GameObject DianwoObj;

	public GameObject fingerObj;

	private int ioverIndex;

	private int indexmap;

	private int iNowRewardIndex;

	public GameObject Tip;

	public Text TipText;

	public int iMapid;

	public GameObject RenwuHongDian;

	private float iStarCountTip;

	public void ClickGoMap()
	{
		InitAndroid.action.GAEvent("ClickGoMap");
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		if (indexmap - 1 != MapManagerUI.action.iMapIndex && indexmap - 1 <= UserManager.iMapCount && BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(IconImage.gameObject);
			StartCoroutine(CallSelectMap());
		}
	}

	public void ClearTip()
	{
		Tip.SetActive(value: false);
	}

	private IEnumerator CallSelectMap(bool bDouble = false)
	{
		maplistPanel.panel.OnCloseButton();
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		MapUI.action.GoMap(indexmap - 1);
	}

	private IEnumerator IEShowTip()
	{
		yield return new WaitForSeconds(0.2f);
		if (iStarCountTip > 0f)
		{
			Tip.SetActive(value: true);
			TipText.text = " 还 剩 " + iStarCountTip + " 颗 星 星 就 可 以 获 取 奖 励 ！";
			UnityEngine.Debug.Log("ShowTip");
		}
	}

	public void ClickReward()
	{
		Singleton<DataManager>.Instance.iSelectMapRewardID = iMapid;
		UI.Instance.OpenPanel(UIPanelType.MapRewardUI);
		InitAndroid.action.GAEvent("clickbtn:ClickMapRewardUIPanel:0");
	}

	public void LoadOver()
	{
		DianwoObj.SetActive(value: false);
		int num = 0;
		for (int i = 1; i <= ioverIndex; i++)
		{
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_StarMapReward" + indexmap + "_" + i) == 0)
			{
				iNowRewardIndex = i;
				RewardIconImage.sprite = LRewardIconImage[i - 1];
				DianwoObj.SetActive(value: false);
				break;
			}
			RewardIconImage.sprite = LRewardIconImage[i - 1];
			num++;
		}
		if (num >= 3)
		{
			iNowRewardIndex = 4;
			DianwoObj.SetActive(value: false);
			RewardIconImage.gameObject.SetActive(value: false);
			RewardBgImage.sprite = OverSprite;
		}
	}

	public void Load(int index)
	{
		iMapid = index + 1;
		UpdateHongDian();
		Tip.SetActive(value: false);
		RewardIconImage.sprite = LRewardIconImage[0];
		Lineobj.fillAmount = 0f;
		index++;
		if (index == 1 && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_bForced_guidance1") == 0 && Singleton<DataManager>.Instance.bForced_guidance == 1)
		{
			fingerObj.SetActive(value: true);
		}
		indexmap = index;
		int num = 0;
		int num2 = 0;
		IconImage.sprite = Util.GetResourcesSprite("Img/mapChina/" + index, 137, 115);
		BaseUIAnimation.action.SetLanguageFont("MapIconUIMapName" + index, MapNameText, string.Empty);
		int num3 = 0;
		for (int i = 1; i < index; i++)
		{
			num3 = i;
			num += Singleton<DataManager>.Instance.LMapBtnCount[i - 1];
		}
		num2 = num + Singleton<DataManager>.Instance.LMapBtnCount[num3];
		string text = Singleton<DataManager>.Instance.dDataLanguage["MapIcounUI1"][BaseUIAnimation.Language];
		text = text.Replace("A1", (num + 1).ToString());
		text = text.Replace("A2", num2.ToString());
		LevelCountText.text = text;
		int mapStar = Singleton<UserManager>.Instance.GetMapStar(index - 1);
		NowStarCount.text = mapStar.ToString();
		int[] array = new int[3];
		for (int j = 1; j <= Singleton<DataManager>.Instance.dDataMapReward.Count; j++)
		{
			try
			{
				int num4 = int.Parse(Singleton<DataManager>.Instance.dDataMapReward[j.ToString()]["Mapid"]);
				if (num4 == index)
				{
					int num5 = int.Parse(Singleton<DataManager>.Instance.dDataMapReward[j.ToString()]["inumber"]);
					int num6 = int.Parse(Singleton<DataManager>.Instance.dDataMapReward[j.ToString()]["iStar"]);
					StarCount[num5 - 1].text = num6.ToString();
					array[num5 - 1] = num6;
				}
			}
			catch (Exception)
			{
				UnityEngine.Debug.LogError(j);
				throw;
			}
		}
		ioverIndex = 0;
		float num7 = 1E-05f;
		if (mapStar >= array[2])
		{
			num7 = 1f;
			ioverIndex = 3;
			mapStar = 0;
		}
		else if (mapStar >= array[1])
		{
			float num8 = mapStar - array[1];
			float num9 = array[2] - array[1];
			float num10 = num8 / num9;
			num10 *= 0.3f;
			num7 = num10 + 0.7f;
			ioverIndex = 2;
			iStarCountTip = num9 - num8;
		}
		else if (mapStar >= array[0])
		{
			float num11 = mapStar - array[0];
			float num12 = array[1] - array[0];
			float num13 = num11 / num12;
			num13 *= 0.3f;
			num7 = num13 + 0.4f;
			ioverIndex = 1;
			iStarCountTip = num12 - num11;
		}
		else
		{
			float num14 = mapStar;
			float num15 = array[0];
			float num16 = num14 / num15;
			num16 *= 0.4f;
			num7 = num16;
			ioverIndex = 0;
			iStarCountTip = num15 - num14;
			if (num14 == 0f)
			{
				iStarCountTip = 0f;
			}
		}
		Lineobj.fillAmount = num7;
		LoadOver();
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void UpdateHongDian()
	{
		RenwuHongDian.SetActive(value: false);
		if (MapRewardHondianRes(iMapid - 1))
		{
			RenwuHongDian.SetActive(value: true);
		}
	}

	public bool MapRewardHondianRes(int _index)
	{
		int num = Singleton<DataManager>.Instance.LMapBtnCount[_index] * 3;
		int mapStar = Singleton<UserManager>.Instance.GetMapStar(_index);
		bool flag = false;
		for (int i = 1; i <= 3; i++)
		{
			if (GetRewardFlag(_index + 1, i, mapStar))
			{
				return true;
			}
		}
		return false;
	}

	public bool GetRewardFlag(int iMapID, int iIndex, int iMapStart)
	{
		int mapStar = Singleton<UserManager>.Instance.GetMapStar(iMapID - 1);
		for (int i = 1; i <= Singleton<DataManager>.Instance.dDataMapReward.Count; i++)
		{
			int num = int.Parse(Singleton<DataManager>.Instance.dDataMapReward[i.ToString()]["iStar"]);
			int num2 = int.Parse(Singleton<DataManager>.Instance.dDataMapReward[i.ToString()]["Mapid"]);
			if (num2 != iMapID)
			{
				continue;
			}
			int num3 = int.Parse(Singleton<DataManager>.Instance.dDataMapReward[i.ToString()]["inumber"]);
			if (num3 == iIndex)
			{
				int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_MapReward" + iMapID + "_" + iIndex);
				if (@int != 1 && iMapStart >= num)
				{
					return true;
				}
				return false;
			}
		}
		return false;
	}
}
