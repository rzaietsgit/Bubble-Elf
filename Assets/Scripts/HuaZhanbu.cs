using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HuaZhanbu : BaseUI
{
	public GameObject Zhanbu_fx;

	public GameObject Zhanbu_award;

	public GameObject Zhanbu_level;

	public GameObject zhanbu_ball;

	public GameObject elf;

	public Sprite[] ArrSp1;

	public Sprite[] ArrSp2;

	private List<int> Ltype = new List<int>();

	private List<int> LNum = new List<int>();

	private int iResult = 4;

	private int izhongzi;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.HuaZhanbu;
	}

	public override void OnStart()
	{
		if (!Util.CheckOnline())
		{
			Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
			return;
		}
		Choujiang();
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("magic_ball");
		}
		Zhanbu_shake();
	}

	private void Zhanbu_shake()
	{
		Animator component = zhanbu_ball.GetComponent<Animator>();
		component.SetTrigger("shake");
		StartCoroutine(zhanbu_fx_Show());
	}

	public void Choujiang()
	{
		int num = Random.Range(1, 101);
		if (num < 10)
		{
			iResult = 1;
		}
		else if (num < 20)
		{
			iResult = 2;
		}
		else if (num < 70)
		{
			iResult = 3;
		}
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NowHua_PaiID", iResult);
		string text = Singleton<DataManager>.Instance.dDataHua1[iResult.ToString()]["sname"];
		int ihuastate = int.Parse(Singleton<DataManager>.Instance.dDataHua1[iResult.ToString()]["ihuastate2"]);
		float value = float.Parse(Singleton<DataManager>.Instance.dDataHua1[iResult.ToString()]["ihuastate1"]);
		Singleton<TestScript>.Instance.SetFloat(DataManager.SDBNO + "DB_ihuastate1", value);
		for (int i = 1; i <= 5; i++)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iFirstZhanBuDay" + Util.GetNowTime_Day());
			if (@int != 1)
			{
				int num2 = int.Parse(Singleton<DataManager>.Instance.dDataHua1[iResult.ToString()]["igailv" + i]);
				int num3 = Random.Range(1, 101);
				if (num3 <= num2)
				{
					string text2 = Singleton<DataManager>.Instance.dDataHua1[iResult.ToString()]["iReward" + i];
					int num4 = int.Parse(text2.Split('|')[0]);
					int num5 = int.Parse(text2.Split('|')[1]);
					Ltype.Add(num4);
					LNum.Add(num5);
					ChinaPay.action.addRewardAll(num4, num5, base.gameObject, isShow: false);
				}
			}
		}
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_iFirstZhanBuDay" + Util.GetNowTime_Day(), 1);
		int value2 = HuaGame.action.GetRandom(ihuastate);
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iFirstZhanBu") == 0)
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_iFirstZhanBu", 1);
			value2 = 1;
		}
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuaID") == 0)
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NowHuaID", value2);
			izhongzi = value2;
			int num6 = Random.Range(10000, 90000);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NowHuaRandID", num6);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NowHuaRandID" + num6, 1);
			HuaGame.action.AddShui(10);
			HuaGame.action.AddFei(10);
			HuaGame.action.InitHua();
		}
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_get_seed");
		}
	}

	private IEnumerator zhanbu_fx_Show()
	{
		yield return new WaitForSeconds(1.5f);
		GameObject _zhanbu_fx = Object.Instantiate(Zhanbu_fx);
		_zhanbu_fx.transform.SetParent(elf.transform, worldPositionStays: false);
		UnityEngine.Object.Destroy(_zhanbu_fx, 2f);
		Animator mAnim3 = zhanbu_ball.GetComponent<Animator>();
		mAnim3.SetTrigger("static");
		yield return new WaitForSeconds(0.5f);
		GameObject _zhanbu_award = Object.Instantiate(Zhanbu_award);
		_zhanbu_award.transform.SetParent(elf.transform, worldPositionStays: false);
		UnityEngine.Object.Destroy(_zhanbu_award, 4f);
		StartCoroutine(Mp3());
		yield return new WaitForSeconds(1f);
		GameObject _zhanbu_level = Object.Instantiate(Zhanbu_level);
		_zhanbu_level.transform.SetParent(elf.transform, worldPositionStays: false);
		UnityEngine.Debug.Log("Ltype.iResult =" + iResult);
		ChangezhanbuType(_zhanbu_level, iResult);
		Animator mAnim2 = _zhanbu_level.transform.GetComponent<Animator>();
		UnityEngine.Debug.Log("Ltype.Count =" + Ltype.Count);
		UnityEngine.Debug.Log("izhongzi =" + izhongzi);
		if (izhongzi > 0)
		{
			mAnim2.SetTrigger(string.Empty + (Ltype.Count + 1));
			UnityEngine.Debug.Log("mAnim2222222222 =");
		}
		else if (Ltype.Count == 0)
		{
			mAnim2.SetTrigger("0");
			StartCoroutine(IECLose(2));
		}
		else
		{
			mAnim2.SetTrigger(string.Empty + Ltype.Count);
		}
		StartCoroutine(Mp31());
		StartCoroutine(IECLose(4));
	}

	public IEnumerator Mp3()
	{
		yield return new WaitForSeconds(1.28f);
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_get_fortune_" + iResult);
		}
	}

	public IEnumerator Mp31()
	{
		yield return new WaitForSeconds(2f);
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_reward");
		}
	}

	public IEnumerator IECLose(int itime)
	{
		yield return new WaitForSeconds(itime);
		base.gameObject.SetActive(value: false);
		HuaGame.action.ShowGuide(3);
		if (izhongzi > 0)
		{
			HuaGame.action.ZhanbuSetNull();
		}
		CloseUI();
	}

	public void ChangezhanbuType(GameObject obj, int index)
	{
		if (izhongzi > 0)
		{
			obj.transform.Find("zhanbu_1").GetComponent<Image>().sprite = ArrSp1[index - 1];
			obj.transform.Find("zhanbu_1").Find("Image").GetComponent<Image>()
				.sprite = Util.GetResourcesSprite("Img/hua/" + izhongzi, 61, 61);
				obj.transform.Find("zhanbu_1").Find("Image").GetComponent<Image>()
					.SetNativeSize();
				obj.transform.Find("zhanbu_1").Find("Image").Find("count")
					.gameObject.SetActive(value: false);
					for (int i = 1; i <= Ltype.Count; i++)
					{
						obj.transform.Find("zhanbu_" + (i + 1)).GetComponent<Image>().sprite = ArrSp1[index - 1];
						obj.transform.Find("zhanbu_" + (i + 1)).Find("Image").GetComponent<Image>()
							.sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + Ltype[i - 1], 138, 114);
							obj.transform.Find("zhanbu_" + (i + 1)).Find("Image").GetComponent<Image>()
								.SetNativeSize();
							obj.transform.Find("zhanbu_" + (i + 1)).Find("Image").Find("count")
								.GetComponent<Text>()
								.text = "+" + LNum[i - 1];
							}
						}
						else
						{
							for (int j = 1; j <= Ltype.Count; j++)
							{
								obj.transform.Find("zhanbu_" + j).GetComponent<Image>().sprite = ArrSp1[index - 1];
								obj.transform.Find("zhanbu_" + j).Find("Image").GetComponent<Image>()
									.sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + Ltype[j - 1], 138, 114);
									obj.transform.Find("zhanbu_" + j).Find("Image").GetComponent<Image>()
										.SetNativeSize();
									obj.transform.Find("zhanbu_" + j).Find("Image").Find("count")
										.GetComponent<Text>()
										.text = "+" + LNum[j - 1];
									}
								}
								obj.transform.Find("zhanbu_name").GetComponent<Image>().sprite = ArrSp2[index - 1];
							}

							private void Update()
							{
							}

							protected override void OnAwake()
							{
								Canvas component = base.gameObject.transform.GetComponent<Canvas>();
								component.renderMode = RenderMode.ScreenSpaceCamera;
								component.worldCamera = HuaGame.action.HuaCamera.GetComponent<Camera>();
								base.OnAwake();
							}

							protected override void OnRelease()
							{
								base.OnRelease();
							}
						}
