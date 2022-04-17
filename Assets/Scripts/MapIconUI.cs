using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapIconUI : MonoBehaviour
{
	public GameObject MapIconObj;

	public GameObject LevelMaplineObj;

	public Text MapIconUIMapName;

	public Text MapStarNumber;

	public GameObject MyMapGirlIcon;

	public GameObject lockobj;

	public GameObject yunObj;

	public GameObject FaceBookHeadImage;

	public GameObject facebookicon;

	public TextMeshProUGUI MapIconUIMapLevelNum;

	public Text YunRemark;

	public Sprite[] NullMapIconSprite;

	public Sprite[] LRobot_head;

	private int iMapindex;

	public GameObject BtnImage;

	public List<string> LFriendID;

	public static int HeadCount;

	private void Start()
	{
		BaseUIAnimation.action.SetLanguageFont("YunRemark", YunRemark, string.Empty);
	}

	private void Update()
	{
	}

	public void SetOrientation(int iMapIndex)
	{
		bool flag = false;
		if (iMapIndex % 2 == 0)
		{
			flag = true;
		}
		if (flag)
		{
			Transform transform = MapIconObj.transform;
			Vector3 localPosition = MapIconObj.transform.localPosition;
			float y = localPosition.y;
			Vector3 localPosition2 = MapIconObj.transform.localPosition;
			transform.localPosition = new Vector3(-131f, y, localPosition2.z);
			LevelMaplineObj.transform.localPosition = new Vector3(-20f, 0f, 0f);
			LevelMaplineObj.transform.localRotation = Quaternion.Euler(0f, 0f, -180f);
		}
		else
		{
			Transform transform2 = MapIconObj.transform;
			Vector3 localPosition3 = MapIconObj.transform.localPosition;
			float y2 = localPosition3.y;
			Vector3 localPosition4 = MapIconObj.transform.localPosition;
			transform2.localPosition = new Vector3(87f, y2, localPosition4.z);
			LevelMaplineObj.transform.localPosition = new Vector3(-14.5f, 0f, 0f);
		}
	}

	public void LoadFacebookFriendIcon()
	{
		LFriendID = new List<string>();
		if ((bool)FireBase.Action)
		{
			int num = 3;
			if (FireBase.Action.dDataFBFriendlevel != null)
			{
				foreach (string key in FireBase.Action.dDataFBFriendlevel.Keys)
				{
					int indexLevel = FireBase.Action.dDataFBFriendlevel[key];
					int mapForLevelID = Singleton<UserManager>.Instance.GetMapForLevelID(indexLevel);
					if (mapForLevelID - 1 == iMapindex)
					{
						if (num == 0)
						{
							break;
						}
						LFriendID.Add(key);
						num--;
					}
				}
			}
		}
	}

	public void SetIcon(int iMapIndex)
	{
		iMapindex = iMapIndex;
		LoadFacebookFriendIcon();
		int iNowMapID = Singleton<UserManager>.Instance.iNowMapID;
		if (iMapIndex > iNowMapID)
		{
			lockobj.SetActive(value: true);
			MapIconObj.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/mapEn/level_map" + (iMapIndex + 1), 298, 288);
		}
		else
		{
			MapIconObj.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/mapEn/level_map_" + (iMapIndex + 1), 298, 288);
		}
		SetOrientation(iMapIndex);
		BaseUIAnimation.action.SetLanguageFont("MapIconUIMapName" + (iMapIndex + 1), MapIconUIMapName, string.Empty);
		int num = Singleton<DataManager>.Instance.LMapBtnCount[iMapIndex] * 3;
		int mapStar = Singleton<UserManager>.Instance.GetMapStar(iMapIndex);
		MapStarNumber.text = mapStar + "/" + num;
		if (iMapIndex == 0)
		{
			BtnImage.SetActive(value: true);
		}
		if (iMapIndex == UserManager.iMapCount - 1)
		{
			yunObj.gameObject.SetActive(value: true);
			if (iMapIndex % 2 == 0)
			{
				yunObj.transform.localPosition = new Vector3(-19f, 182f, 0f);
			}
		}
		if (iMapIndex == iNowMapID)
		{
			MyMapGirlIcon.SetActive(value: true);
			SetGirlHead();
			if (iMapIndex % 2 == 0)
			{
				MyMapGirlIcon.transform.localPosition = new Vector3(160f, 0f, 0f);
				MyMapGirlIcon.transform.Find("bg").transform.localPosition = new Vector3(-20f, 0f, 0f);
				MyMapGirlIcon.transform.Find("bg").transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			}
			else
			{
				MyMapGirlIcon.transform.localPosition = new Vector3(-170f, 0f, 0f);
			}
			BaseUIAnimation.action.SetLanguageFont("MapIconUIMapLevelNum", MapIconUIMapLevelNum, Singleton<UserManager>.Instance.iNowPassLevelID.ToString());
			SetFacebookIcon(bShow: false, iMapIndex);
		}
		else
		{
			SetFacebookIcon(bShow: true, iMapIndex);
		}
	}

	public void SetGirlHead()
	{
		if (FaceBookApi.Action.bLoginState())
		{
			if ((bool)FaceBookApi.Action.MyFacebookImage)
			{
				FaceBookHeadImage.GetComponent<Image>().sprite = FaceBookApi.Action.MyFacebookImage;
			}
			else
			{
				FaceBookApi.Action.LoadMyImg(FaceBookHeadImage.GetComponent<Image>());
			}
		}
	}

	public void SetFacebookIcon(bool bShow, int iMapIndex)
	{
		int num = 20;
		int num2 = UnityEngine.Random.Range(-num, num);
		int num3 = UnityEngine.Random.Range(-num, num);
		Vector3 b = new Vector3(num2, num3, 0f);
		num2 = UnityEngine.Random.Range(-num, num);
		num3 = UnityEngine.Random.Range(-num, num);
		Vector3 b2 = new Vector3(num2, num3, 0f);
		num2 = UnityEngine.Random.Range(-num, num);
		num3 = UnityEngine.Random.Range(-num, num);
		Vector3 b3 = new Vector3(num2, num3, 0f);
		if (iMapIndex == UserManager.iMapCount - 1)
		{
			facebookicon.gameObject.SetActive(value: false);
			return;
		}
		if (!bShow)
		{
			facebookicon.gameObject.SetActive(value: false);
			return;
		}
		if (iMapIndex % 2 == 0)
		{
			if (FaceBookApi.Action.bLoginState())
			{
				for (int i = 1; i <= 3; i++)
				{
					if (LFriendID.Count >= i)
					{
						if (i == 1)
						{
							facebookicon.transform.Find("icon1").transform.localPosition = new Vector3(100f, 58f, 0f) + b;
						}
						if (i == 2)
						{
							facebookicon.transform.Find("icon2").transform.localPosition = new Vector3(90f, -67f, 0f) + b2;
						}
						if (i == 3)
						{
							facebookicon.transform.Find("icon3").transform.localPosition = new Vector3(80f, -1f, 0f) + b3;
						}
						facebookicon.transform.Find("icon" + i).Find("bg").transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
						facebookicon.transform.Find("icon" + i).Find("icon").transform.localPosition = new Vector3(20f, 0f, 0f);
						try
						{
							facebookicon.transform.Find("icon" + i).Find("icon").Find("head")
								.GetComponent<Image>()
								.sprite = FaceBookApi.Action.dFacebookImage[LFriendID[i - 1]];
							}
							catch (Exception)
							{
								try
								{
									Image component = facebookicon.transform.Find("icon" + i).Find("icon").Find("head")
										.GetComponent<Image>();
									FaceBookApi.Action.LoadFrImgFB(LFriendID[i - 1], component);
								}
								catch (Exception)
								{
								}
							}
						}
						else
						{
							facebookicon.transform.Find("icon" + i).gameObject.SetActive(value: false);
						}
					}
				}
				else
				{
					facebookicon.transform.Find("icon1").transform.localPosition = new Vector3(100f, 58f, 0f) + b;
					facebookicon.transform.Find("icon1").Find("bg").transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
					facebookicon.transform.Find("icon1").Find("icon").transform.localPosition = new Vector3(20f, 0f, 0f);
					facebookicon.transform.Find("icon2").transform.localPosition = new Vector3(90f, -67f, 0f) + b2;
					facebookicon.transform.Find("icon2").Find("bg").transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
					facebookicon.transform.Find("icon2").Find("icon").transform.localPosition = new Vector3(20f, 0f, 0f);
					facebookicon.transform.Find("icon3").transform.localPosition = new Vector3(80f, -1f, 0f) + b3;
					facebookicon.transform.Find("icon3").Find("bg").transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
					facebookicon.transform.Find("icon3").Find("icon").transform.localPosition = new Vector3(20f, 0f, 0f);
				}
			}
			else if (FaceBookApi.Action.bLoginState())
			{
				for (int j = 1; j <= 3; j++)
				{
					if (LFriendID.Count >= j)
					{
						if (j == 1)
						{
							facebookicon.transform.Find("icon" + j).transform.localPosition = new Vector3(-133f, 59f, 0f) + b;
						}
						if (j == 2)
						{
							facebookicon.transform.Find("icon2").transform.localPosition = new Vector3(-143f, -8f, 0f) + b2;
						}
						if (j == 3)
						{
							facebookicon.transform.Find("icon3").transform.localPosition = new Vector3(-143f, -72f, 0f) + b3;
						}
						try
						{
							facebookicon.transform.Find("icon" + j).Find("icon").Find("head")
								.GetComponent<Image>()
								.sprite = FaceBookApi.Action.dFacebookImage[LFriendID[j - 1]];
							}
							catch (Exception)
							{
								try
								{
									GameObject gameObject = facebookicon.transform.Find("icon" + j).gameObject;
									gameObject = gameObject.transform.Find("icon").gameObject;
									gameObject = gameObject.transform.Find("head").gameObject;
									FaceBookApi.Action.LoadFrImgFB(LFriendID[j - 1], gameObject.GetComponent<Image>());
								}
								catch (Exception)
								{
								}
							}
						}
						else
						{
							facebookicon.transform.Find("icon" + j).gameObject.SetActive(value: false);
						}
					}
				}
				else
				{
					facebookicon.transform.Find("icon1").transform.localPosition = new Vector3(-133f, 59f, 0f) + b;
					facebookicon.transform.Find("icon2").transform.localPosition = new Vector3(-143f, -8f, 0f) + b2;
					facebookicon.transform.Find("icon3").transform.localPosition = new Vector3(-143f, -72f, 0f) + b3;
				}
				if (!FaceBookApi.Action.bLoginState())
				{
					SetHead();
				}
			}

			public void SetHead()
			{
				if (HeadCount > 4)
				{
					HeadCount = 0;
				}
				facebookicon.transform.Find("icon1").Find("icon").Find("head")
					.GetComponent<Image>()
					.sprite = LRobot_head[HeadCount];
					HeadCount++;
					if (HeadCount > 4)
					{
						HeadCount = 0;
					}
					facebookicon.transform.Find("icon2").Find("icon").Find("head")
						.GetComponent<Image>()
						.sprite = LRobot_head[HeadCount];
						HeadCount++;
						if (HeadCount > 4)
						{
							HeadCount = 0;
						}
					}

					public void SelectMap()
					{
						if ((bool)SoundController.action)
						{
							SoundController.action.playNow("ButtonClick");
						}
						if (iMapindex != MapManagerUI.action.iMapIndex && iMapindex <= UserManager.iMapCount && BaseUIAnimation.bClickButton)
						{
							BaseUIAnimation.action.ClickButton(MapIconObj.gameObject);
							StartCoroutine(CallSelectMap());
						}
					}

					private IEnumerator CallSelectMap(bool bDouble = false)
					{
						yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
						MapUI.action.GoMap(iMapindex);
						SettingPanelUI.action.ChangeMapCloseSetting();
					}
				}
