using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GGqiandaoUI : BaseUI
{
	public static GGqiandaoUI action;

	public GameObject CloseBtn;

	public GameObject DayObj;

	public Text titleText;

	public Sprite[] bgSprite;

	private string nowTime = string.Empty;

	private int iType = 1;

	private int iNowQiandao;

	private int iNowQiandaoCount;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.GGqiandaoUI;
	}

	public override void OnStart()
	{
		action = this;
		BaseUIAnimation.action.SetLanguageFont("signintext1", titleText, string.Empty);
		nowTime = Util.GetNowTime_Day();
		InitData();
		InitDay();
		if (!InitGame.bChinaVersion)
		{
			InitGame.Action.GetTimeNetTime();
		}
	}

	private IEnumerator Test()
	{
		yield return new WaitForSeconds(3f);
		InitData();
		InitDay();
	}

	private void InitData()
	{
		iType = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GGQianDao");
		if (iType == 0)
		{
			iType = 1;
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_GGQianDao", 1);
		}
		iNowQiandao = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GGQianDaoBooL" + nowTime);
		iNowQiandaoCount = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GGQianDaoCount");
		if (iNowQiandaoCount == 0 && iNowQiandao == 1)
		{
			iType--;
			if (iType == 0)
			{
				iType = 3;
			}
			iNowQiandaoCount = 6;
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_GGQianDaoBooL" + nowTime, 0);
		}
	}

	private void InitDay()
	{
		UnityEngine.Debug.Log("iType" + iType);
		int num = int.Parse(Singleton<DataManager>.Instance.dDataSigninGG[(iType * 7).ToString()]["icon"]);
		int num2 = int.Parse(Singleton<DataManager>.Instance.dDataSigninGG[(iType * 7).ToString()]["reward"]);
		int num3 = int.Parse(Singleton<DataManager>.Instance.dDataSigninGG[(iType * 7).ToString()]["inumber1"]);
		int num4 = int.Parse(Singleton<DataManager>.Instance.dDataSigninGG[(iType * 7).ToString()]["inumber2"]);
		DayObj.transform.Find("BtnDay7").Find("imageday1").GetComponent<Image>()
			.sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num, 138, 114);
			DayObj.transform.Find("BtnDay7").Find("imageday2").GetComponent<Image>()
				.sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num2, 138, 114);
				DayObj.transform.Find("BtnDay7").Find("number1").GetComponent<Text>()
					.text = "x" + num3;
					DayObj.transform.Find("BtnDay7").Find("number2").GetComponent<Text>()
						.text = "x" + num4;
						if (iNowQiandaoCount + 1 == 7)
						{
							if (iNowQiandao == 0)
							{
								DayObj.transform.Find("BtnDay7").GetComponent<Image>().sprite = bgSprite[1];
							}
							else
							{
								DayObj.transform.Find("BtnDay7").Find("mask").gameObject.SetActive(value: true);
							}
						}
						for (int i = 1; i <= 6; i++)
						{
							if (i <= iNowQiandaoCount)
							{
								DayObj.transform.Find("BtnDay" + i).GetComponent<Image>().sprite = bgSprite[0];
								DayObj.transform.Find("BtnDay" + i).Find("mask").gameObject.SetActive(value: true);
							}
							else if (i == iNowQiandaoCount + 1 && iNowQiandao == 0)
							{
								DayObj.transform.Find("BtnDay" + i).GetComponent<Image>().sprite = bgSprite[0];
							}
							num = int.Parse(Singleton<DataManager>.Instance.dDataSigninGG[(iType * 7 - 7 + i).ToString()]["icon"]);
							num3 = int.Parse(Singleton<DataManager>.Instance.dDataSigninGG[(iType * 7 - 7 + i).ToString()]["inumber1"]);
							DayObj.transform.Find("BtnDay" + i).Find("number").GetComponent<Text>()
								.text = "x" + num3;
								DayObj.transform.Find("BtnDay" + i).Find("imageday").GetComponent<Image>()
									.sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num, 138, 114);
								}
							}

							public void ClickQiandaoGG(int index)
							{
								if (InitGame.Action.sNetTime == string.Empty)
								{
									return;
								}
								int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GGQianDaoBooL" + nowTime);
								if (@int == 1 || iNowQiandaoCount + 1 != index)
								{
									return;
								}
								Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_GGQianDaoBooL" + nowTime, 1);
								DayObj.transform.Find("BtnDay" + index).Find("mask").gameObject.SetActive(value: true);
								Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_GGQianDaoCount", iNowQiandaoCount + 1);
								if (index == 7)
								{
									RewardID(index, iType);
									if (iType == 3)
									{
										Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_GGQianDao", 1);
									}
									else
									{
										Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_GGQianDao", iType + 1);
									}
									Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_GGQianDaoCount", 0);
								}
								else
								{
									RewardID(index, iType);
								}
								MapUI.action.isCanQiandao = false;
							}

							private void RewardID(int _index, int _iType)
							{
								List<int> list = new List<int>();
								List<int> list2 = new List<int>();
								int num = int.Parse(Singleton<DataManager>.Instance.dDataSigninGG[(iType * 7 - (7 - _index)).ToString()]["icon"]);
								int num2 = int.Parse(Singleton<DataManager>.Instance.dDataSigninGG[(iType * 7 - (7 - _index)).ToString()]["inumber1"]);
								list.Add(num);
								list2.Add(num2);
								ChinaPay.action.addRewardAll(num, num2, MapUI.action.gameObject, isShow: false);
								if (_index == 7)
								{
									int num3 = int.Parse(Singleton<DataManager>.Instance.dDataSigninGG[(iType * 7 - (7 - _index)).ToString()]["reward"]);
									int num4 = int.Parse(Singleton<DataManager>.Instance.dDataSigninGG[(iType * 7 - (7 - _index)).ToString()]["inumber2"]);
									list.Add(num3);
									list2.Add(num4);
									ChinaPay.action.addRewardAll(num3, num4, MapUI.action.gameObject, isShow: false);
								}
								BaseUIAnimation.action.ShowProp(list, list2, MapUI.action.gameObject);
							}

							private void Awake()
							{
								Canvas component = base.gameObject.transform.GetComponent<Canvas>();
								component.renderMode = RenderMode.ScreenSpaceCamera;
								component.worldCamera = MapUI.action.MapUISceneCamera.GetComponent<Camera>();
							}

							private void Update()
							{
							}

							public void _CloseGGUI()
							{
								if (BaseUIAnimation.bClickButton)
								{
									BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
									StartCoroutine(CallCloseUI());
								}
							}

							private IEnumerator CallCloseUI()
							{
								yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
								CloseUI();
							}

							protected override void OnAwake()
							{
								base.OnAwake();
							}

							protected override void OnRelease()
							{
								base.OnRelease();
							}

							public void RestartGame()
							{
							}
						}
