using DG.Tweening;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnManager : MonoBehaviour
{
	public static BtnManager action;

	public int iNowLevel = 1;

	public GameObject GirlObj;

	public GameObject GirlImageObj;

	private GameObject Girl;

	private GameObject GirlImage;

	private int iMapIndex;

	private Vector2[] BtnPos;

	private float[] Btnsale;

	private bool bGril;

	public GameObject _LevelBtn;

	public GameObject _LevelBtnMin;

	public GameObject[] LBtnObj;

	public GameObject[,] LBtnObjMin;

	public Vector2[] LBtnMainPos;

	public static bool bClickBtn = true;

	public static bool bClickTimeDown;

	public bool isShowFinger;

	public int[] LMapCount;

	private bool bOpenMap;

	private int btnMinCount;

	private int iGirlNextID;

	public void HideFinger()
	{
		if ((bool)Girl)
		{
			isShowFinger = false;
			Girl.transform.Find("finger").gameObject.SetActive(value: false);
		}
	}

	public void Reslanguage()
	{
		for (int i = 0; i < LBtnObj.Length; i++)
		{
			LevelBtnScript component = LBtnObj[i].GetComponent<LevelBtnScript>();
			component.Reslanguage();
		}
	}

	public void ShowFinger()
	{
		if ((bool)Girl)
		{
			isShowFinger = true;
			Girl.transform.Find("finger").gameObject.SetActive(value: true);
		}
	}

	private void Start()
	{
		Singleton<DataManager>.Instance.bGrilMoveing = false;
		action = this;
		Singleton<UserManager>.Instance.LoadNowPassLevelNumber();
		if (Singleton<LevelManager>.Instance.bFirstInMap)
		{
			iMapIndex = Singleton<UserManager>.Instance.iNowMapID;
		}
		else
		{
			int num = Singleton<LevelManager>.Instance.iNowSelectLevelIndex;
			if (!Singleton<LevelManager>.Instance.bExit && !Singleton<LevelManager>.Instance.bLoseGame)
			{
				num++;
			}
			int mapForLevelID = Singleton<UserManager>.Instance.GetMapForLevelID(num);
			if (Singleton<LevelManager>.Instance.bRstart4)
			{
				Singleton<LevelManager>.Instance.bRstart4 = false;
				if (num > 4)
				{
					mapForLevelID = Singleton<UserManager>.Instance.GetMapForLevelID(num - 1);
				}
			}
			iMapIndex = mapForLevelID - 1;
		}
		LoadLMapCount();
		InitBtnSale();
		InitMainPos();
		GoMap(iMapIndex);
		StartCoroutine(UpdateMove());
		if (FaceBookApi.Action.bLoginState())
		{
			LoadGirlImage();
		}
	}

	private void LoadLMapCount()
	{
		LMapCount = new int[UserManager.iMapCount];
		for (int i = 0; i < UserManager.iMapCount; i++)
		{
			int num = 0;
			try
			{
				for (int j = 0; j < 5000; j++)
				{
					string a = Singleton<DataManager>.Instance.dDataMapBtnConfig[(j + 1).ToString()]["map" + (i + 1)].ToString();
					if (a == "0" || a == string.Empty)
					{
						break;
					}
					num++;
				}
			}
			catch (Exception)
			{
			}
			LMapCount[i] = num;
		}
	}

	private IEnumerator TestCamera()
	{
		yield return new WaitForSeconds(3f);
	}

	private IEnumerator UpdateMove()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.1f);
			if (!bClickTimeDown)
			{
				bClickBtn = true;
			}
		}
	}

	public void TestGirlMove()
	{
	}

	private void InitMainPos()
	{
		LBtnMainPos = new Vector2[UserManager.iMapCount];
		LBtnMainPos[0] = new Vector2(-65f, 138f);
		LBtnMainPos[1] = new Vector2(-276f, 572f);
		LBtnMainPos[2] = new Vector2(-127f, 210f);
		LBtnMainPos[3] = new Vector2(-132f, 193f);
		LBtnMainPos[4] = new Vector2(-134.3f, 266.2f);
		LBtnMainPos[5] = new Vector2(-127f, 248f);
		LBtnMainPos[6] = new Vector2(-127f, 248f);
		LBtnMainPos[7] = new Vector2(-130f, 260f);
		for (int i = 8; i < UserManager.iMapCount; i++)
		{
			LBtnMainPos[i] = new Vector2(-276f, 100f);
		}
		LBtnMainPos[17] = new Vector2(-125f, 270f);
	}

	private void InitBtnSale()
	{
		Btnsale = new float[UserManager.iMapCount];
		for (int i = 0; i < UserManager.iMapCount; i++)
		{
			Btnsale[i] = 1f;
			if (i >= 8)
			{
				Btnsale[i] = 1.4f;
			}
			if (i == 17)
			{
				Btnsale[i] = 1f;
			}
		}
		Btnsale[1] = 1f;
	}

	public void GoMap(int iIndex)
	{
		if (iIndex == 1)
		{
			base.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
		}
		else if (iIndex > 0)
		{
			base.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
		}
		else
		{
			base.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		bOpenMap = false;
		iMapIndex = iIndex;
		InitMapPosByDate(iIndex);
		CreateMapBtn();
		Transform transform = base.transform;
		float x = LBtnMainPos[iMapIndex].x;
		float y = LBtnMainPos[iMapIndex].y;
		Vector3 localPosition = base.transform.localPosition;
		transform.localPosition = new Vector3(x, y, localPosition.z);
		TestGirlMove();
	}

	public void ResNowBtnReward()
	{
		int num = Singleton<DataManager>.Instance.LMapStarBtnID[iMapIndex];
		for (int i = 0; i < BtnPos.Length; i++)
		{
			LevelBtnScript component = LBtnObj[i].GetComponent<LevelBtnScript>();
			if (Singleton<DataManager>.Instance.iLevelRewardLevelID == i + 1 + num)
			{
				component.SetRewardState(Singleton<DataManager>.Instance.iLevelRewardLevelID);
			}
		}
	}

	public void ClearRewardRemark()
	{
		int num = Singleton<DataManager>.Instance.LMapStarBtnID[iMapIndex];
		for (int i = 0; i < BtnPos.Length; i++)
		{
			try
			{
				if (LBtnObj[i] != null)
				{
					LevelBtnScript component = LBtnObj[i].GetComponent<LevelBtnScript>();
					if ((bool)component)
					{
						component.ClearRemark();
					}
				}
			}
			catch (Exception)
			{
			}
		}
	}

	public void CreateMapBtn()
	{
		if (LBtnObj != null)
		{
			for (int i = 0; i < LBtnObj.Length; i++)
			{
				UnityEngine.Object.Destroy(LBtnObj[i].gameObject);
			}
		}
		if (LBtnObjMin != null)
		{
			for (int j = 0; j < LBtnObjMin.Length / 3; j++)
			{
				for (int k = 0; k < 3; k++)
				{
					if (LBtnObjMin[j, k] != null)
					{
						UnityEngine.Object.Destroy(LBtnObjMin[j, k].gameObject);
					}
				}
			}
			btnMinCount = 0;
		}
		if ((bool)Girl)
		{
			UnityEngine.Object.Destroy(Girl);
		}
		int iNowMapID = Singleton<UserManager>.Instance.iNowMapID;
		if (iMapIndex > iNowMapID && PayManager.action.OpenPay)
		{
			return;
		}
		LBtnObjMin = new GameObject[BtnPos.Length, 3];
		LBtnObj = new GameObject[BtnPos.Length];
		int num = 0;
		num = Singleton<DataManager>.Instance.LMapStarBtnID[iMapIndex];
		for (int l = 0; l < BtnPos.Length; l++)
		{
			LBtnObj[l] = UnityEngine.Object.Instantiate(_LevelBtn);
			LBtnObj[l].transform.SetParent(base.transform, worldPositionStays: false);
			LBtnObj[l].transform.localPosition = new Vector2(BtnPos[l].x - 696f, 1182f - BtnPos[l].y);
			int num2 = l + 1 + num;
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
			if (@int == LevelManager.iMaxLevelID)
			{
				if (iMapIndex == UserManager.iMapCount - 2 && Singleton<UserManager>.Instance.iNowPassLevelID == num2)
				{
					CheckCreateGirl(l);
				}
			}
			else if (Singleton<UserManager>.Instance.iNowPassLevelID + 1 == num2)
			{
				CheckCreateGirl(l);
			}
			LevelBtnScript component = LBtnObj[l].GetComponent<LevelBtnScript>();
			float num3 = Btnsale[iMapIndex];
			component.UpdateBtnScale(num3);
			component.SelectLevelIndex(l + 1 + num);
			int levelStar = Singleton<UserManager>.Instance.GetLevelStar(l + 1 + num);
			component.SetBtnState(levelStar);
			CreateBtnMin(l, l + 1 + num, num3, levelStar);
			if (Singleton<LevelManager>.Instance.bOpenNewLevel && num2 == Singleton<UserManager>.Instance.iNowPassLevelID)
			{
				component.HideStar();
				HideBtnMin(l);
			}
			if (num2 <= Singleton<UserManager>.Instance.iNowPassLevelID + 1)
			{
				if (Singleton<UserManager>.Instance.iNowPassLevelID + 1 == num2)
				{
					if (Singleton<LevelManager>.Instance.bOpenNewLevel)
					{
						component.HideNumber();
						HideBtnMin(l);
					}
					else
					{
						component.SetPassLevel();
					}
				}
				else
				{
					component.SetPassLevel();
				}
			}
			else
			{
				component.HideNumber();
				HideBtnMin(l);
			}
		}
	}

	public void HideBtnMin(int iIndex)
	{
		for (int i = 0; i < 3 && !(LBtnObjMin[iIndex, i] == null); i++)
		{
			LevelBtnScriptMin component = LBtnObjMin[iIndex, i].GetComponent<LevelBtnScriptMin>();
			component.SetNoPassLevel();
		}
	}

	public void CreateBtnMin(int iLocalIndex, int btnIndex, float f, int iStarState)
	{
		Vector2 vector = default(Vector2);
		for (int i = 0; i < 3; i++)
		{
			string text = Singleton<DataManager>.Instance.dDataMapBtnConfig_min[btnIndex + string.Empty]["Pos" + (i + 1)].ToString();
			if (text == "0" || text == string.Empty)
			{
				break;
			}
			vector = new Vector2(int.Parse(text.Split('-')[0]), int.Parse(text.Split('-')[1]));
			if (iMapIndex >= 8)
			{
				vector = new Vector2((float)int.Parse(text.Split('-')[0]) * 2.9f, (float)int.Parse(text.Split('-')[1]) * 2.9f);
			}
			if (iMapIndex == 17)
			{
				vector = new Vector2(int.Parse(text.Split('-')[0]), int.Parse(text.Split('-')[1]));
			}
			LBtnObjMin[iLocalIndex, i] = UnityEngine.Object.Instantiate(_LevelBtnMin);
			LBtnObjMin[iLocalIndex, i].transform.SetParent(base.transform, worldPositionStays: false);
			LBtnObjMin[iLocalIndex, i].transform.localPosition = new Vector2(vector.x - 696f, 1182f - vector.y);
			LevelBtnScriptMin component = LBtnObjMin[iLocalIndex, i].GetComponent<LevelBtnScriptMin>();
			component.UpdateBtnScale(f);
			if (iStarState > 0)
			{
				component.SetPassLevel();
			}
		}
	}

	public void LoadGirlImage()
	{
		try
		{
			if (FaceBookApi.Action.bLoginState() && (bool)GirlImage)
			{
				if ((bool)FaceBookApi.Action.MyFacebookImage)
				{
					GirlImage.transform.Find("head1").Find("head").GetComponent<Image>()
						.sprite = FaceBookApi.Action.MyFacebookImage;
					}
					else
					{
						FaceBookApi.Action.LoadImageFB(GirlImage.transform.Find("head1").Find("head").GetComponent<Image>());
					}
				}
			}
			catch (Exception)
			{
			}
		}

		public void CheckCreateGirl(int index)
		{
			if (Singleton<LevelManager>.Instance.bOpenNewLevel)
			{
				if (!Singleton<LevelManager>.Instance.bGoNextMap)
				{
					iGirlNextID = index;
					index--;
				}
				else
				{
					LevelBtnScript component = LBtnObj[index].GetComponent<LevelBtnScript>();
					component.Createfx_ui_mapPin();
				}
			}
			else
			{
				LevelBtnScript component2 = LBtnObj[index].GetComponent<LevelBtnScript>();
				component2.Createfx_ui_mapPin();
			}
			Girl = UnityEngine.Object.Instantiate(GirlObj);
			Girl.transform.SetParent(base.transform);
			Girl.transform.localPosition = LBtnObj[index].transform.localPosition + new Vector3(0f, 200f, 0f);
			Girl.transform.localScale = new Vector2(100f, 100f);
			SkeletonAnimation component3 = Girl.GetComponent<SkeletonAnimation>();
			GirlImage = UnityEngine.Object.Instantiate(GirlImageObj);
			GirlImage.transform.SetParent(Girl.transform);
			GirlImage.transform.localScale = new Vector2(0.01f, 0.01f);
			GirlImage.transform.localPosition = new Vector3(0f, 1f, 0f);
			GirlImage.GetComponent<Canvas>().sortingOrder = 101;
			bGril = true;
			SetMapMax();
			Vector3 mapGirlPos = LBtnObj[index].transform.localPosition + new Vector3(LBtnMainPos[iMapIndex].x, LBtnMainPos[iMapIndex].y, 0f);
			if (iMapIndex == 1)
			{
				Vector3 localPosition = LBtnObj[index].transform.localPosition;
				float x = localPosition.x * 0.7f;
				Vector3 localPosition2 = LBtnObj[index].transform.localPosition;
				mapGirlPos = new Vector3(x, localPosition2.y * 0.7f, 0f) + new Vector3(LBtnMainPos[iMapIndex].x, LBtnMainPos[iMapIndex].y, 0f);
			}
			else if (iMapIndex > 0)
			{
				Vector3 localPosition3 = LBtnObj[index].transform.localPosition;
				float x2 = localPosition3.x * 0.35f;
				Vector3 localPosition4 = LBtnObj[index].transform.localPosition;
				mapGirlPos = new Vector3(x2, localPosition4.y * 0.35f, 0f) + new Vector3(LBtnMainPos[iMapIndex].x, LBtnMainPos[iMapIndex].y, 0f);
			}
			MapManagerUI.action.SetMapGirlPos(mapGirlPos);
			Singleton<UserManager>.Instance.SetNowMapID(iMapIndex);
			StartCoroutine(IEOpenNextLevel(iMapIndex));
			LoadGirlImage();
		}

		private IEnumerator IEOpenNextLevel(int iMapIndex)
		{
			yield return new WaitForSeconds(0.2f);
			if (Singleton<LevelManager>.Instance.bOpenNewLevel)
			{
				if (!Singleton<LevelManager>.Instance.bGoNextMap)
				{
					GirlMove();
					yield break;
				}
				MapManagerUI.action.PlayMapCloud(iMapIndex);
				Unlocked(b: false);
			}
		}

		public void GirlMove(bool bFlag = true)
		{
			if (DataManager.iFirstLoginGameLoadCloud == 0)
			{
				int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
				if (bFlag && @int == 1)
				{
					return;
				}
			}
			Vector3 vector = LBtnObj[iGirlNextID].transform.localPosition + new Vector3(0f, 200f, 0f);
			List<Vector3> list = new List<Vector3>();
			int num = 0;
			if (iGirlNextID == 0)
			{
				return;
			}
			for (int i = 0; i < 3 && !(LBtnObjMin[iGirlNextID - 1, i] == null); i++)
			{
				list.Add(LBtnObjMin[iGirlNextID - 1, i].transform.localPosition + new Vector3(0f, 200f, 0f));
				LevelBtnScriptMin component = LBtnObjMin[iGirlNextID - 1, i].GetComponent<LevelBtnScriptMin>();
				component.WaitPass((float)(i + 1) * 0.6f);
				num++;
			}
			Vector3[] array = new Vector3[num + 1];
			for (int j = 0; j < array.Length; j++)
			{
				if (j == array.Length - 1)
				{
					array[j] = vector;
				}
				else
				{
					array[j] = list[j];
				}
			}
			Singleton<DataManager>.Instance.bGrilMoveing = true;
			Girl.transform.DOLocalPath(array, 1.8f, PathType.CatmullRom, PathMode.TopDown2D).SetEase(Ease.InSine).OnComplete(delegate
			{
				Unlocked();
			});
			Vector3 vector2 = LBtnObj[iGirlNextID].transform.localPosition + new Vector3(LBtnMainPos[iMapIndex].x, LBtnMainPos[iMapIndex].y, 0f);
			if (iMapIndex > 0)
			{
				Vector3 localPosition = LBtnObj[iGirlNextID].transform.localPosition;
				float x = localPosition.x * 0.35f + LBtnMainPos[iMapIndex].x;
				Vector3 localPosition2 = LBtnObj[iGirlNextID].transform.localPosition;
				vector2 = new Vector3(x, localPosition2.y * 0.35f + LBtnMainPos[iMapIndex].y, 0f);
			}
			float num2 = vector2.x * -1f;
			Vector3 localScale = MapManagerUI.action.transform.localScale;
			float x2 = num2 * localScale.x;
			float num3 = vector2.y * -1f;
			Vector3 localScale2 = MapManagerUI.action.transform.localScale;
			Vector3 endValue = new Vector3(x2, num3 * localScale2.x, vector2.z);
			MapManagerUI.action.transform.DOLocalMove(endValue, 2.5f).SetEase(Ease.InOutSine);
		}

		private void Unlocked(bool b = true)
		{
			Singleton<DataManager>.Instance.bGrilMoveing = false;
			if (!Singleton<LevelManager>.Instance.bGoNextMap)
			{
				StartCoroutine(IEOpenLevel());
			}
			Singleton<LevelManager>.Instance.bOpenNewLevel = false;
			Singleton<LevelManager>.Instance.iNowSelectLevelIndex = Singleton<LevelManager>.Instance.iNowSelectLevelIndex + 1;
			LevelBtnScript component = LBtnObj[iGirlNextID].GetComponent<LevelBtnScript>();
			component.ShowNumber();
			component.SetPassLevel();
			component.Unlocked(b);
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("ui_level_unlocked");
			}
		}

		private IEnumerator IEOpenLevel()
		{
			yield return new WaitForSeconds(0.3f);
			if (BaseUIAnimation.action.BuyLiveSale(btype: true))
			{
				Singleton<DataManager>.Instance.EBuyLiveSale = EnumUIType.PlayUI;
				UI.Instance.OpenPanel(UIPanelType.BuyLivesChinaUI);
				yield break;
			}
			if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 2 && InitGame.bChinaVersion && !InitGame.bEnios && !InitGame.bHideSign7Task && Util.CheckOnline() && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_FirstSign7") == 0)
			{
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_FirstSign7", 1);
				if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Sign7State") == 0)
				{
					Singleton<DataManager>.Instance.Openplay1 = true;
					UI.Instance.OpenPanel(UIPanelType.SignReward7UI);
					yield break;
				}
			}
			if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex > LevelManager.iMaxLevelID)
			{
				yield break;
			}
			if (InitGame.bChinaVersion)
			{
			}
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "SaleAdUILoginReward") == 0)
			{
				UnityEngine.Debug.Log("y 3");
				if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 5)
				{
					UnityEngine.Debug.Log("y5");
					ShowMapRewardForced_guidance();
					yield break;
				}
				if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 6)
				{
					DataManager.sale_adKey = "SaleAdUILoginReward";
					if (InitGame.bChinaVersion && !InitGame.bCloseLBForEnIos && !Singleton<DataManager>.Instance.bGooglePay)
					{
						UI.Instance.OpenPanel(UIPanelType.SaleAdUI);
					}
					else if ((bool)PayManager.action)
					{
						PayManager.action.OpenSignOrPlay();
					}
					else if (UI.Instance.GetPanelCount() <= 0)
					{
						UI.Instance.OpenPanel(UIPanelType.Play);
					}
					yield break;
				}
				if (Singleton<DataManager>.Instance.bGooglePay && Singleton<DataManager>.Instance.GetUserDataI("DB_Google_Score") == 0 && Singleton<LevelManager>.Instance.iNowStar == 3)
				{
					int iNowSelectLevelIndex = Singleton<LevelManager>.Instance.iNowSelectLevelIndex;
					if (iNowSelectLevelIndex > 19 && iNowSelectLevelIndex % 10 != 0)
					{
					}
				}
				if ((bool)PayManager.action)
				{
					PayManager.action.OpenSignOrPlay();
				}
				else if (UI.Instance.GetPanelCount() <= 0)
				{
					UI.Instance.OpenPanel(UIPanelType.Play);
				}
			}
			else if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 5)
			{
				ShowMapRewardForced_guidance();
			}
			else if ((bool)PayManager.action)
			{
				PayManager.action.OpenSignOrPlay();
			}
			else if (UI.Instance.GetPanelCount() <= 0)
			{
				UI.Instance.OpenPanel(UIPanelType.Play);
			}
		}

		public void ShowMapRewardForced_guidance()
		{
			if (InitGame.bChinaVersion && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_bForced_guidance1") == 0 && (bool)MapUI.action)
			{
				Singleton<DataManager>.Instance.bForced_guidance = 0;
				MapUI.action.ShowMapRewardForced_guidance();
			}
		}

		private void SetMapMax()
		{
			if (!Singleton<LevelManager>.Instance.bGoNextMap && DataManager.iFirstLoginGameLoadCloud != 0)
			{
				MapManagerUI.action.SetMapMax();
			}
		}

		public void InitMapPosByDate(int index)
		{
			BtnPos = new Vector2[LMapCount[index]];
			Vector2 vector = default(Vector2);
			for (int i = 0; i < 10000; i++)
			{
				try
				{
					string text = Singleton<DataManager>.Instance.dDataMapBtnConfig[i + 1 + string.Empty]["map" + (index + 1)].ToString();
					if (text == "0")
					{
						return;
					}
					vector = new Vector2(int.Parse(text.Split('-')[0]), int.Parse(text.Split('-')[1]));
					if (index >= 8)
					{
						vector = new Vector2((float)int.Parse(text.Split('-')[0]) * 2.9f, (float)int.Parse(text.Split('-')[1]) * 2.9f);
					}
					if (index == 17)
					{
						vector = new Vector2(int.Parse(text.Split('-')[0]), int.Parse(text.Split('-')[1]));
					}
					vector += new Vector2(0f, 10f);
					BtnPos[i] = vector;
				}
				catch (Exception)
				{
					return;
				}
			}
		}

		public void InitmapPosMin()
		{
		}

		public void ClickBtnUpdate()
		{
			GameObject gameObject = Util.TouchChecker(UnityEngine.Input.mousePosition);
			if (!(gameObject == null))
			{
				if (gameObject.name == "Btn")
				{
					MapManagerUI.action.ReSetPos();
				}
				else if (gameObject.name == "MapUIDoubleClick")
				{
					MapManagerUI.action.bClickBtn = true;
				}
			}
		}

		public GameObject TouchChecker(Vector3 mouseposition)
		{
			Vector3 vector = Camera.main.ScreenToViewportPoint(mouseposition);
			Vector2 point = new Vector2(vector.x, vector.y);
			if ((bool)Physics2D.OverlapPoint(point))
			{
				return Physics2D.OverlapPoint(point).gameObject;
			}
			return null;
		}

		private void Update()
		{
			if (InitGame.bChinaVersion && Input.GetMouseButtonDown(0))
			{
				ClearRewardRemark();
			}
			if (Input.GetMouseButtonDown(0))
			{
				bClickTimeDown = true;
				ClickBtnUpdate();
			}
			if (Input.GetMouseButtonUp(0))
			{
				bClickTimeDown = false;
			}
			if (Girl != null)
			{
				SkeletonAnimation component = Girl.GetComponent<SkeletonAnimation>();
				if (GirlImage != null)
				{
					GirlImage.transform.localPosition = new Vector3(component.skeleton.FindBone("play_icon").worldX, component.skeleton.FindBone("play_icon").worldY, 0f);
				}
			}
		}
	}
