using DG.Tweening;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HuaGame : MonoBehaviour
{
	public static HuaGame action;

	public Text ShuiText1;

	public Text FeiText1;

	public Text JiankangText1;

	public Image ShuiImg1;

	public Image FeiImg1;

	public Image JiankangImg1;

	public Image ShuiImg1_Double;

	public Camera HuaCamera;

	public GameObject HuaObj;

	public GameObject HuaState;

	public GameObject guideObj;

	public Text guideText1;

	public Text guideText2;

	public GameObject ChouQianBtn;

	public GameObject[] flowerArr;

	public GameObject flowerObj;

	public GameObject[] flowerUpAniArr;

	public GameObject flowerFather;

	public GameObject huangchongObj;

	public GameObject huangchongBtn;

	public GameObject BtnDay;

	public Text HuaNameText;

	public Text TextLevelNumber;

	public Text NextLevel1;

	public Text NextLevel2;

	public Text NextLevelNow1;

	public Text NextLevelNow2;

	public Text Next0;

	public Text sTimeText;

	public Image NextImg;

	public GameObject ShowHuaRemarkObj;

	public GameObject showHuaObjF;

	public GameObject tippaopao;

	private GameObject flowerObj_;

	private bool binithua = true;

	private int indexGrude;

	private bool bguide8;

	public Image ImageShuimiaoMove;

	private int iNextLevelf;

	private int iNextLevels;

	private int iNowLevelf;

	private int iNowLevels;

	private bool bdouble;

	private int iCutTIme = 600;

	public int ichengzhang = 1;

	private int _ChangeHuaState_;

	public void ShowHuaRemark()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuaID");
		if (@int != 0 && _ChangeHuaState_ + 1 != 4)
		{
			string text = Singleton<DataManager>.Instance.dDataHua2[@int.ToString()]["name"];
			HuaNameText.text = text.ToString();
			TextLevelNumber.text = _ChangeHuaState_ + 1 + string.Empty;
			NextLevel1.text = iNextLevelf.ToString();
			NextLevel2.text = iNextLevels.ToString();
			NextLevelNow1.text = iNowLevelf.ToString();
			NextLevelNow2.text = iNowLevels.ToString();
			Next0.text = ichengzhang.ToString();
			if (bdouble)
			{
				Next0.text = (ichengzhang * 2).ToString();
			}
			int num = iNextLevelf;
			if (iNextLevelf < iNextLevels)
			{
				num = iNextLevels;
			}
			num = num * iCutTIme / ichengzhang;
			if (bdouble)
			{
				num /= 2;
			}
			int num2 = new TimeSpan(0, 0, num).Minutes;
			if (num2 < 20)
			{
				num2 = 20;
			}
			string text2 = num2 + string.Empty;
			if (num2 < 10)
			{
				text2 = "0" + text2;
			}
			sTimeText.text = text2.ToString() + "分钟";
			ShowHuaRemarkObj.SetActive(value: true);
			if (binithua)
			{
				showHuaObjF.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/hua/" + @int + "_" + (_ChangeHuaState_ + 2), 180, 170);
			}
		}
	}

	private void Start()
	{
		if (!Util.CheckOnline())
		{
			Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
			return;
		}
		tippaopao.SetActive(value: false);
		ShowHuaRemarkObj.gameObject.SetActive(value: false);
		ImageShuimiaoMove.gameObject.SetActive(value: false);
		huangchongObj.SetActive(value: false);
		huangchongBtn.SetActive(value: false);
		action = this;
		iCutTIme = int.Parse(Singleton<DataManager>.Instance.dDataHua4["1"]["iConfigData"]) * 60;
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuaID") == 0)
		{
			NullHua();
		}
		else
		{
			InitHuangChong();
			calcchengzhang();
			RefreshState();
			InitHua();
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "Huajieduan");
			ChangeHuaState(@int);
			Checkjieduanjiangli();
		}
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iFirstZhanBuDay" + Util.GetNowTime_Day()) == 0)
		{
			BtnDay.gameObject.SetActive(value: true);
		}
		else
		{
			BtnDay.gameObject.SetActive(value: false);
		}
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuangChong");
		UnityEngine.Debug.Log("iNowHuangChong=" + int2);
		StartCoroutine(IEUpdate());
		if ((bool)MusicController.action)
		{
			MusicController.action.BG_menu();
		}
		UnityEngine.Debug.Log("ShowGuide 1=");
		UnityEngine.Debug.Log("ShowGuide 2=");
		int int3 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "HuangchongbossFirst");
		if (int3 == 1)
		{
			UnityEngine.Debug.Log("ShowGuide 3=");
			Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "HuangchongbossFirst", 2);
			ShowGuide(8);
		}
	}

	public void InitHuangChong()
	{
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuangChong") == 0)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuangChongCount" + Util.GetNowTime_Day());
			if (@int <= 4)
			{
				int num = int.Parse(DateTime.Now.ToString("HH"));
				bool flag = false;
				if (num >= 0 && num <= 6)
				{
					if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuangChongCount" + Util.GetNowTime_Day() + "0") == 0)
					{
						Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NowHuangChongCount" + Util.GetNowTime_Day() + "0", 1);
						flag = true;
					}
				}
				else if (num >= 7 && num <= 12)
				{
					if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuangChongCount" + Util.GetNowTime_Day() + "1") == 0)
					{
						Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NowHuangChongCount" + Util.GetNowTime_Day() + "1", 1);
						flag = true;
					}
				}
				else if (num >= 13 && num <= 18)
				{
					if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuangChongCount" + Util.GetNowTime_Day() + "2") == 0)
					{
						Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NowHuangChongCount" + Util.GetNowTime_Day() + "2", 1);
						flag = true;
					}
				}
				else if (num >= 19 && num <= 24 && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuangChongCount" + Util.GetNowTime_Day() + "3") == 0)
				{
					Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NowHuangChongCount" + Util.GetNowTime_Day() + "3", 1);
					flag = true;
				}
				if (flag)
				{
					@int++;
					Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NowHuangChongCount" + Util.GetNowTime_Day(), @int);
					int num2 = UnityEngine.Random.Range(1, 101);
					int num3 = 0;
					int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHua_PaiID");
					if (int2 == 1)
					{
						num3 = 15;
					}
					UnityEngine.Debug.Log("iRand=" + num2);
					UnityEngine.Debug.Log("iShuai=" + num3);
					if (num2 < 50 + num3)
					{
						Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NowHuangChong", 1);
						AddJianKangDu(-10);
					}
					else
					{
						UnityEngine.Debug.Log("AddJianKangDu--------no------");
					}
				}
			}
		}
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuangChong") == 1 && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuaID") != 0)
		{
			huangchongObj.SetActive(value: true);
			huangchongBtn.SetActive(value: true);
		}
	}

	public void AddJianKangDu(int iJiankang)
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "iinitjiankang");
		@int += iJiankang;
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "iinitjiankang", @int);
		if (iJiankang >= 50)
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NowHuangChong", 0);
		}
		if (@int >= 100)
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "iinitjiankang", 100);
		}
		UnityEngine.Debug.Log("AddJianKangDu--------ok------" + iJiankang);
	}

	public void calcchengzhang()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuaID");
		if (@int != 0)
		{
			ichengzhang = int.Parse(Singleton<DataManager>.Instance.dDataHua2[@int.ToString()]["chengzhang"]);
			int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHua_PaiID", 1);
			int2 = int.Parse(Singleton<DataManager>.Instance.dDataHua1[int2.ToString()]["ihuastate1"]);
			ichengzhang += int2;
			UnityEngine.Debug.Log("成长=" + ichengzhang);
			string str = Singleton<DataManager>.Instance.dDataHua2[@int.ToString()]["name"];
			UnityEngine.Debug.Log("名字  =" + str);
		}
	}

	private IEnumerator IEUpdate()
	{
		yield return new WaitForSeconds(30f);
		while (true)
		{
			if (Singleton<UserManager>.Instance.bOpenHua() > 0)
			{
				if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuaID") == 0)
				{
					yield return new WaitForSeconds(30f);
					continue;
				}
				UnityEngine.Debug.Log("----------IEUpdate----------------");
				calcchengzhang();
				RefreshState();
				InitHua();
				int iJieduan = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "Huajieduan");
				ChangeHuaState(iJieduan);
				Checkjieduanjiangli();
			}
			else
			{
				Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
			}
			yield return new WaitForSeconds(60f);
		}
	}

	public void NullHua()
	{
		HuaObj.SetActive(value: false);
		HuaState.SetActive(value: false);
		guideObj.SetActive(value: true);
		ChouQianBtn.SetActive(value: true);
		Singleton<UserManager>.Instance.KillHua();
		huangchongObj.SetActive(value: false);
		huangchongBtn.SetActive(value: false);
		if ((bool)flowerObj)
		{
			UnityEngine.Object.Destroy(flowerObj);
		}
		binithua = true;
		CheckShowTipPaopao();
	}

	public static GameObject TouchChecker(Vector3 mouseposition)
	{
		Canvas component = action.GetComponent<Canvas>();
		if ((bool)Physics2D.OverlapPoint(mouseposition))
		{
			return Physics2D.OverlapPoint(mouseposition).gameObject;
		}
		Vector3 vector = Camera.main.ScreenToWorldPoint(mouseposition);
		Vector2 point = new Vector2(vector.x, vector.y);
		if ((bool)Physics2D.OverlapPoint(point))
		{
			return Physics2D.OverlapPoint(point).gameObject;
		}
		vector = Camera.main.WorldToScreenPoint(mouseposition);
		point = new Vector2(vector.x, vector.y);
		if ((bool)Physics2D.OverlapPoint(point))
		{
			return Physics2D.OverlapPoint(point).gameObject;
		}
		return null;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			guideObj.SetActive(value: false);
			GameObject gameObject = TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject == null)
			{
				return;
			}
			UnityEngine.Debug.Log("Pointer.name===" + gameObject.name);
			if (gameObject.name.LastIndexOf("HuaCanvas") >= 0)
			{
				if (Util.GetNowOpenUI() || Checkjieduanjiangli())
				{
					return;
				}
				ShowHuaRemark();
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			ShowHuaRemarkObj.gameObject.SetActive(value: false);
			if (indexGrude == 3)
			{
				ShowGuide(4);
			}
			if (indexGrude == 4)
			{
				ShowGuide(5);
			}
			if (indexGrude == 6)
			{
				ShowGuide(7);
			}
		}
	}

	public void ShowGuide(int index = 1)
	{
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_ShowGuideChong" + index) == 0)
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_ShowGuideChong" + index, 1);
			if (index > 3 && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_ShowGuideChong" + (index - 1)) == 0)
			{
				return;
			}
			UnityEngine.Debug.Log("ShowGuide  index =" + index);
			indexGrude = index;
			Transform transform = guideObj.transform;
			Vector3 localPosition = guideObj.transform.localPosition;
			transform.localPosition = new Vector3(localPosition.x, -109f, 0f);
			if (index == 3)
			{
				guideText1.text = "哇，得了一颗铃兰种子，现在来培养它吧！";
				Transform transform2 = guideObj.transform;
				Vector3 localPosition2 = guideObj.transform.localPosition;
				transform2.localPosition = new Vector3(localPosition2.x, -438f, 0f);
			}
			if (index == 4)
			{
				guideText1.text = "种子成长是需要水份和养份一起的哦！";
			}
			if (index == 5)
			{
				guideText1.text = " 打开鲜花商店，可以购买水份和养份！";
				Transform transform3 = guideObj.transform;
				Vector3 localPosition3 = guideObj.transform.localPosition;
				transform3.localPosition = new Vector3(localPosition3.x, 239f, 0f);
			}
			if (index == 6)
			{
				guideText1.text = " 真棒，鲜花现在开始成长了！";
			}
			switch (index)
			{
			case 7:
				if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuaID") != 0)
				{
					Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NowHuangChong", 1);
					huangchongObj.SetActive(value: true);
					huangchongBtn.SetActive(value: true);
					guideText1.text = "哎呀，糟糕，可恶的坏蜗牛来破坏鲜花了，点击击败他，让鲜花健康成长吧！";
					Transform transform5 = guideObj.transform;
					Vector3 localPosition5 = guideObj.transform.localPosition;
					transform5.localPosition = new Vector3(localPosition5.x, -219f, 0f);
				}
				break;
			case 8:
			{
				guideText1.text = "哇哦，太好了，鲜花成熟了，点击收获它吧！";
				Transform transform4 = guideObj.transform;
				Vector3 localPosition4 = guideObj.transform.localPosition;
				transform4.localPosition = new Vector3(localPosition4.x, 31f, 0f);
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "iHuaShuiCount1", 100);
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "iHuaFeiCount1", 100);
				bguide8 = true;
				InitHua();
				break;
			}
			}
			guideObj.SetActive(value: true);
		}
		else
		{
			indexGrude = 0;
		}
	}

	public void ZhanbuSetNull()
	{
		flowerObj.gameObject.SetActive(value: false);
		ImageShuimiaoMove.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
		StartCoroutine(IEShowHua());
	}

	public IEnumerator IEShowHua()
	{
		yield return new WaitForSeconds(0.5f);
		int iNowHuaID = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuaID");
		ImageShuimiaoMove.gameObject.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/hua/" + iNowHuaID, 61, 61);
		ImageShuimiaoMove.gameObject.SetActive(value: true);
		ImageShuimiaoMove.gameObject.transform.DOLocalMoveY(-320f, 1f).OnComplete(delegate
		{
			OpenFx();
		});
	}

	public void OpenFx()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("flower_growing");
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(flowerUpAniArr[0]);
		gameObject.transform.SetParent(flowerObj.transform, worldPositionStays: false);
		flowerObj.gameObject.SetActive(value: true);
		ImageShuimiaoMove.gameObject.SetActive(value: false);
	}

	public void InitHua(bool bres = false)
	{
		CheckShowTipPaopao();
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuaID");
		if (@int != 0)
		{
			if (flowerObj == null)
			{
				flowerObj = UnityEngine.Object.Instantiate(flowerArr[@int - 1]);
				flowerObj.transform.SetParent(flowerFather.transform, worldPositionStays: false);
				flowerObj.transform.localPosition = new Vector3(0f, -331f, 0f);
			}
			calcchengzhang();
			HuaObj.SetActive(value: true);
			HuaState.SetActive(value: true);
			guideObj.SetActive(value: false);
			ChouQianBtn.SetActive(value: false);
			int num = int.Parse(Singleton<DataManager>.Instance.dDataHua2[@int.ToString()]["ishui"]);
			int num2 = int.Parse(Singleton<DataManager>.Instance.dDataHua2[@int.ToString()]["ufei"]);
			int num3 = int.Parse(Singleton<DataManager>.Instance.dDataHua2[@int.ToString()]["ijiankang"]);
			int num4 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "iinitjiankang");
			if (num4 == 0)
			{
				num4 = int.Parse(Singleton<DataManager>.Instance.dDataHua2[@int.ToString()]["iinitjiankang"]);
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "iinitjiankang", num4);
			}
			int num5 = int.Parse(Singleton<DataManager>.Instance.dDataHua2[@int.ToString()]["iinitshui"]);
			int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "iHuaShuiCount1");
			num5 += int2;
			int int3 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_HuaShuiCount");
			int num6 = int.Parse(Singleton<DataManager>.Instance.dDataHua2[@int.ToString()]["iinitfei"]);
			int int4 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "iHuaFeiCount1");
			num6 += int4;
			int int5 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_HuaFeiCount");
			ShuiText1.text = int3.ToString();
			FeiText1.text = int5.ToString();
			UnityEngine.Debug.Log("肥  " + num6 + "/" + num2 + "/ Myfei:" + int5);
			UnityEngine.Debug.Log("水分 " + num5 + "/" + num + "/ Myshui:" + int3);
			UnityEngine.Debug.Log("成长=" + ichengzhang);
			UnityEngine.Debug.Log("健康=" + num4);
			JiankangText1.text = num4.ToString();
			float fillAmount = (float)num5 * 1f / ((float)num * 1f);
			float fillAmount2 = (float)num6 * 1f / ((float)num2 * 1f);
			float fillAmount3 = (float)num4 * 1f / ((float)num3 * 1f);
			ShuiImg1.fillAmount = fillAmount;
			FeiImg1.fillAmount = fillAmount2;
			iNextLevelf = num2 - num6;
			int num7 = num2 / 3 + 1;
			while (iNextLevelf > num7 - 1)
			{
				iNextLevelf -= num7;
			}
			iNextLevels = num - num5;
			num7 = num / 3 + 1;
			while (iNextLevels > num7 - 1)
			{
				iNextLevels -= num7;
			}
			iNowLevelf = num6;
			iNowLevels = num5;
			JiankangImg1.fillAmount = fillAmount3;
			if (bres)
			{
				AddFei(0);
				AddShui(0);
			}
			if (Singleton<UserManager>.Instance.ResFeiliaoDouble())
			{
				ShuiImg1_Double.gameObject.SetActive(value: true);
				bdouble = true;
			}
			else
			{
				ShuiImg1_Double.gameObject.SetActive(value: false);
				bdouble = false;
			}
		}
	}

	public void TestData()
	{
		ShuiText1.text = "88/100'";
		FeiText1.text = "88/100'";
		JiankangText1.text = "88/100'";
		ShuiImg1.fillAmount = 0.8f;
		FeiImg1.fillAmount = 0.8f;
		JiankangImg1.fillAmount = 0.8f;
	}

	public void ClickHelp()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("button");
		}
		UI.Instance.OpenPanel(UIPanelType.HuaHelp);
	}

	public void ClickHusShopUI()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("button");
		}
		Singleton<UIManager>.Instance.OpenUI(EnumUIType.HuaShopUI);
	}

	public void RefreshState()
	{
		RefreshStateShui();
		RefreshStateFei();
		Singleton<UserManager>.Instance.ResFeiliaoDouble();
	}

	public void RefreshStateShui()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuaID");
		if (@int == 0)
		{
			return;
		}
		int num = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_HuaShuiCount");
		if (num == 0)
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_HuaShuiStarTime", 0);
			return;
		}
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_HuaShuiStarTime");
		if (int2 == 0)
		{
			return;
		}
		int nowTime = Util.GetNowTime();
		int num2 = nowTime - int2;
		if (num2 >= iCutTIme)
		{
			int num3 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "iHuaShuiCount1");
			int num4 = int.Parse(Singleton<DataManager>.Instance.dDataHua2[@int.ToString()]["ishui"]);
			int num5 = int.Parse(Singleton<DataManager>.Instance.dDataHua2[@int.ToString()]["iinitshui"]);
			num4 -= num5;
			num2 /= iCutTIme;
			int num6 = num2 * ichengzhang;
			int num7 = 0;
			while (num6 >= 1 && num >= 1 && num3 < num4)
			{
				num7++;
				num3++;
				num6--;
				num--;
				if (num >= 1 && num3 < num4)
				{
					int int3 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "iFeiliaoDoubleiTimeShui");
					if (int3 > 0)
					{
						num7++;
						num3++;
						num--;
						Singleton<UserManager>.Instance.CutDouble();
					}
				}
			}
			if (num7 > 0)
			{
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "iHuaShuiCount1", num3);
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_HuaShuiCount", num);
				int2 += num7 * iCutTIme;
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_HuaShuiStarTime", Util.GetNowTime());
			}
			if (num4 == num3)
			{
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_HuaShuiStarTime", 0);
			}
		}
		if (num == 0)
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_HuaShuiStarTime", 0);
		}
	}

	public void RefreshStateFei()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuaID");
		if (@int == 0)
		{
			return;
		}
		int num = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_HuaFeiCount");
		if (num == 0)
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_HuaFeiStarTime", 0);
			return;
		}
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_HuaFeiStarTime");
		if (int2 == 0)
		{
			return;
		}
		int nowTime = Util.GetNowTime();
		int num2 = nowTime - int2;
		if (num2 >= iCutTIme)
		{
			int num3 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "iHuaFeiCount1");
			int num4 = int.Parse(Singleton<DataManager>.Instance.dDataHua2[@int.ToString()]["ufei"]);
			int num5 = int.Parse(Singleton<DataManager>.Instance.dDataHua2[@int.ToString()]["iinitfei"]);
			num4 -= num5;
			num2 /= iCutTIme;
			int num6 = num2 * ichengzhang;
			int num7 = 0;
			while (num6 >= 1 && num >= 1 && num3 < num4)
			{
				num7++;
				num3++;
				num6--;
				num--;
				if (num >= 1 && num3 < num4)
				{
					int int3 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "iFeiliaoDoubleiTimeFei");
					if (int3 > 0)
					{
						num7++;
						num3++;
						num--;
						Singleton<UserManager>.Instance.CutDouble(1);
					}
				}
			}
			if (num7 > 0)
			{
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "iHuaFeiCount1", num3);
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_HuaFeiCount", num);
				int2 += num7 * iCutTIme;
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_HuaFeiStarTime", Util.GetNowTime());
			}
			if (num4 == num3)
			{
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_HuaFeiStarTime", 0);
			}
		}
		if (num == 0)
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_HuaFeiStarTime", 0);
		}
	}

	public void AddShui(int iShui)
	{
		RefreshStateShui();
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_HuaShuiCount");
		@int += iShui;
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_HuaShuiCount", @int);
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_HuaShuiStarTime") == 0 && @int > 0)
		{
			int nowTime = Util.GetNowTime();
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_HuaShuiStarTime", nowTime);
		}
		InitHua();
	}

	public void AddFei(int ifei)
	{
		RefreshStateFei();
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_HuaFeiCount");
		@int += ifei;
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_HuaFeiCount", @int);
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_HuaFeiStarTime") == 0 && @int > 0)
		{
			int nowTime = Util.GetNowTime();
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_HuaFeiStarTime", nowTime);
		}
		InitHua();
	}

	public bool Checkjieduanjiangli(bool bup = true)
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuaID");
		if (@int == 0)
		{
			return false;
		}
		int num = int.Parse(Singleton<DataManager>.Instance.dDataHua2[@int.ToString()]["ishui"]);
		int num2 = int.Parse(Singleton<DataManager>.Instance.dDataHua2[@int.ToString()]["ufei"]);
		int num3 = int.Parse(Singleton<DataManager>.Instance.dDataHua2[@int.ToString()]["ijiankang"]);
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "iinitjiankang") == 0)
		{
			int value = int.Parse(Singleton<DataManager>.Instance.dDataHua2[@int.ToString()]["iinitjiankang"]);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "iinitjiankang", value);
		}
		int num4 = int.Parse(Singleton<DataManager>.Instance.dDataHua2[@int.ToString()]["iinitshui"]);
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "iHuaShuiCount1");
		num4 += int2;
		int num5 = int.Parse(Singleton<DataManager>.Instance.dDataHua2[@int.ToString()]["iinitfei"]);
		int int3 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "iHuaFeiCount1");
		num5 += int3;
		float num6 = (float)num4 * 1f / ((float)num * 1f);
		float num7 = (float)num5 * 1f / ((float)num2 * 1f);
		ShuiImg1.fillAmount = num6;
		FeiImg1.fillAmount = num7;
		int int4 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "Huajieduan");
		int num8 = int4;
		float num9 = (num6 + num7) / 2f;
		if (num9 < 0.3333f)
		{
			int4 = 0;
		}
		else if (num9 < 0.66666f)
		{
			int4 = 1;
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "Huajieduan", 1);
		}
		else if (num9 >= 1f)
		{
			int4 = 3;
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "Huajieduan", 3);
		}
		else
		{
			int4 = 2;
		}
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "Huajieduan", int4);
		int int5 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "HuajieduanOk");
		if (num8 != int4)
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("flower_growing");
			}
			try
			{
				UnityEngine.Debug.Log("iJieduan=" + int4);
				GameObject gameObject = UnityEngine.Object.Instantiate(flowerUpAniArr[int4]);
				gameObject.transform.SetParent(flowerObj.transform, worldPositionStays: false);
			}
			catch (Exception arg)
			{
				UnityEngine.Debug.Log("iJieduan=" + int4);
				UnityEngine.Debug.Log("ex=" + arg);
			}
		}
		UnityEngine.Debug.Log("   Update  iJieduan = " + int4);
		if (bup)
		{
			ChangeHuaState(int4);
		}
		if (int4 > int5)
		{
			return true;
		}
		return false;
	}

	public void ChangeHuaState(int index)
	{
		if (!(flowerObj == null) && _ChangeHuaState_ != index)
		{
			_ChangeHuaState_ = index;
			UnityEngine.Debug.Log("ChangeHuaState index = " + (index + 1));
			SkeletonAnimation component = flowerObj.GetComponent<SkeletonAnimation>();
			component.state.SetAnimation(0, "level" + (index + 1), loop: true);
		}
	}

	public void ClickChouPai()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("button");
		}
		BtnDay.SetActive(value: false);
		ChouQianBtn.SetActive(value: false);
		Singleton<UIManager>.Instance.OpenUI(EnumUIType.HuaZhanbu);
	}

	public int GetRandom(int _ihuastate2)
	{
		List<int> list = new List<int>();
		for (int i = 1; i <= 6; i++)
		{
			int num = int.Parse(Singleton<DataManager>.Instance.dDataHua2[i.ToString()]["iRand"]);
			if (i == 6)
			{
				num += _ihuastate2;
			}
			list.Add(num);
		}
		int num2 = 0;
		for (int j = 0; j < list.Count; j++)
		{
			num2 += list[j] + 1;
		}
		int num3 = UnityEngine.Random.Range(0, num2);
		num2 = 0;
		for (int k = 0; k < list.Count; k++)
		{
			num2 += list[k] + 1;
			if (num3 <= num2)
			{
				return k + 1;
			}
		}
		return 1;
	}

	public void Clickback()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("button");
		}
		Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
	}

	public void CheckShowTipPaopao()
	{
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuaID") == 0)
		{
			tippaopao.SetActive(value: false);
		}
		else if (Checkjieduanjiangli())
		{
			tippaopao.SetActive(value: true);
		}
		else
		{
			tippaopao.SetActive(value: false);
		}
	}

	public void ClickReward()
	{
		UnityEngine.Debug.Log("---------ClickReward");
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("button");
		}
		if (bguide8)
		{
			bguide8 = false;
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuaID");
			string text = Singleton<DataManager>.Instance.dDataHua2[@int.ToString()]["fayujiangli3"];
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NowHuaID", 0);
			NullHua();
			for (int num = 0; num < text.Split('F').Length; num++)
			{
				string text2 = text.Split('F')[num];
				if (!(text2 == string.Empty))
				{
					int num2 = int.Parse(text2.Split('|')[0]);
					int num3 = int.Parse(text2.Split('|')[1]);
					int num4 = int.Parse(text2.Split('|')[2]);
					int num5 = UnityEngine.Random.Range(1, 101);
					if (num5 <= num4)
					{
						UnityEngine.Debug.Log("Rewardid =" + num2);
						UnityEngine.Debug.Log("RewardCount =" + num3);
						int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "iinitjiankang");
						if (num2 == 2 || num2 == 3)
						{
							num3 = num3 * int2 / 100;
						}
						if (num3 <= 1)
						{
							num3 = 1;
						}
						list.Add(num2);
						list2.Add(num3);
						ChinaPay.action.addRewardAll(num2, num3, base.gameObject, isShow: false);
					}
				}
			}
			if (list2.Count >= 1)
			{
				BaseUIAnimation.action.ShowProp(list, list2, base.gameObject);
			}
		}
		else
		{
			ClickRewardjeiduanjiangli();
		}
	}

	public void ClickHuangChongBoss()
	{
		UnityEngine.Debug.Log("---------ClickHuangChongBoss");
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuangChong");
		if (@int == 1)
		{
			GoHuangchongBoss();
		}
	}

	public void GoHuangchongBoss()
	{
		int num = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LoveCount");
		int loveInfinite = Singleton<UserManager>.Instance.getLoveInfinite();
		if (loveInfinite > 0)
		{
			num = Singleton<DataManager>.Instance.iLoveMaxAll;
		}
		if (num > Singleton<DataManager>.Instance.iLoveUse)
		{
			Camera.main.transform.GetComponent<RapidBlurEffect>().enabled = false;
			Singleton<LevelManager>.Instance.iNowSelectLevelIndex = 80000;
			Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.GameScene);
		}
		else
		{
			Singleton<UIManager>.Instance.OpenUI(EnumUIType.BuyLivesChinaUI);
		}
	}

	public void ClickRewardjeiduanjiangli()
	{
		bool flag = false;
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_NowHuaID");
		if (@int == 0)
		{
			return;
		}
		flag = true;
		if (!Checkjieduanjiangli())
		{
			return;
		}
		tippaopao.SetActive(value: false);
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("button");
		}
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "HuajieduanOk");
		int2++;
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "HuajieduanOk", int2);
		if (Checkjieduanjiangli())
		{
			int2++;
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "HuajieduanOk", int2);
		}
		if (Checkjieduanjiangli())
		{
			int2++;
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "HuajieduanOk", int2);
		}
		if (int2 == 0 || int2 >= 4)
		{
			return;
		}
		string text = Singleton<DataManager>.Instance.dDataHua2[@int.ToString()]["fayujiangli" + int2];
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		if (int2 == 3)
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_NowHuaID", 0);
			NullHua();
		}
		for (int num = 0; num < text.Split('F').Length; num++)
		{
			string text2 = text.Split('F')[num];
			if (!(text2 == string.Empty))
			{
				int num2 = int.Parse(text2.Split('|')[0]);
				int num3 = int.Parse(text2.Split('|')[1]);
				int num4 = int.Parse(text2.Split('|')[2]);
				int num5 = UnityEngine.Random.Range(1, 101);
				if (num5 <= num4)
				{
					UnityEngine.Debug.Log("Rewardid =" + num2);
					UnityEngine.Debug.Log("RewardCount =" + num3);
					int int3 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "iinitjiankang");
					if (num2 == 2 || num2 == 3)
					{
						num3 = num3 * int3 / 100;
					}
					if (num3 <= 1)
					{
						num3 = 1;
					}
					list.Add(num2);
					list2.Add(num3);
					ChinaPay.action.addRewardAll(num2, num3, base.gameObject, isShow: false);
				}
			}
		}
		if (list2.Count >= 1)
		{
			UnityEngine.Debug.Log("Reward iJieduanOk = " + int2);
			BaseUIAnimation.action.ShowProp(list, list2, base.gameObject);
			flag = false;
		}
	}
}
