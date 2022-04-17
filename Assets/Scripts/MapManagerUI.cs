using Spine.Unity;
using System.Collections;
using UnityEngine;

public class MapManagerUI : MonoBehaviour
{
	public static MapManagerUI action;

	public int iMapIndex;

	private float[] MapSale;

	private float[] MapSaleMin;

	private float[] MapSaleMax;

	private float[] MapBorder;

	public GameObject Map_cloudObj;

	public GameObject Map_cloudFather;

	private float iMoveSetNumber = 5f;

	public float NowMapSaleMin = 0.4f;

	public float NowMapSale = 0.4f;

	public float NowMapSaleMax = 1f;

	public float NowMapBorder = 50f;

	public bool bClickBtn = true;

	private GameObject _Map_cloudObj;

	private Vector3 OldPos;

	private Vector2 MapNowPos = new Vector2(0f, 0f);

	private Vector3 DoubleCliclOldPos = new Vector3(-100f, -100f);

	private int DoubleCliclOldTime = 1000;

	private void Start()
	{
		action = this;
		if (!PayManager.action.OpenPay && Map_cloudFather != null)
		{
			Map_cloudFather.gameObject.SetActive(value: false);
		}
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
			if (num > LevelManager.iMaxLevelID)
			{
				num = LevelManager.iMaxLevelID;
			}
			int mapForLevelID = Singleton<UserManager>.Instance.GetMapForLevelID(num);
			iMapIndex = mapForLevelID - 1;
			if (Singleton<LevelManager>.Instance.bRstart5)
			{
				Singleton<LevelManager>.Instance.bRstart5 = true;
				if (num > 4)
				{
					mapForLevelID = Singleton<UserManager>.Instance.GetMapForLevelID(num - 1);
					iMapIndex = mapForLevelID - 1;
				}
			}
		}
		InitMapSale();
		GoMap(iMapIndex, binit: true);
		if (Singleton<LevelManager>.Instance.bGoNextMap)
		{
			StartCoroutine(IEResetDate());
		}
		if (DataManager.iFirstLoginGameLoadCloud == 0)
		{
			StartCoroutine(IEResetDate2());
		}
		if (Singleton<LevelManager>.Instance.bOpenNewLevel)
		{
			return;
		}
		if (!Singleton<LevelManager>.Instance.bExit && !Singleton<LevelManager>.Instance.bFirstInMap && !Singleton<LevelManager>.Instance.bGoNextMap && Singleton<LevelManager>.Instance.bLastWin)
		{
			Singleton<LevelManager>.Instance.bLastWin = false;
			if ((bool)FireBase.Action)
			{
				if (!FireBase.Action.bDownloadFacebookData)
				{
					StartCoroutine(IEOpenNextLevel2());
				}
				else
				{
					FireBase.Action.bDownloadFacebookData = false;
				}
			}
		}
		if (Singleton<LevelManager>.Instance.bRstart)
		{
			Singleton<LevelManager>.Instance.bRstart = false;
			StartCoroutine(IEOpenNextLevel1());
		}
	}

	private IEnumerator IEOpenNextLevel2()
	{
		yield return new WaitForSeconds(0.3f);
		Singleton<LevelManager>.Instance.iNowSelectLevelIndex = Singleton<LevelManager>.Instance.iNowSelectLevelIndex + 1;
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex <= LevelManager.iMaxLevelID)
		{
			if ((bool)PayManager.action)
			{
				PayManager.action.OpenSignOrPlay();
			}
			else if (UI.Instance.GetPanelCount() <= 0)
			{
				UI.Instance.OpenPanel(UIPanelType.Play);
			}
		}
	}

	private IEnumerator IEOpenNextLevel1()
	{
		yield return new WaitForSeconds(0.3f);
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex <= LevelManager.iMaxLevelID)
		{
			if (BaseUIAnimation.action.BuyLiveSale())
			{
				Singleton<DataManager>.Instance.EBuyLiveSale = EnumUIType.PlayUI;
				UI.Instance.OpenPanel(UIPanelType.BuyLivesChinaUI);
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
	}

	private IEnumerator IEResetDate()
	{
		yield return new WaitForSeconds(8f);
		Singleton<LevelManager>.Instance.bGoNextMap = false;
	}

	private IEnumerator IEResetDate2()
	{
		yield return new WaitForSeconds(1f);
		DataManager.iFirstLoginGameLoadCloud = 1;
		Singleton<DataManager>.Instance.SaveUserDate("DB_FirstLoginGameLoadCloud", 1);
	}

	public void LoadCloud(int iMapID)
	{
		if (LevelManager.bWwwDataFlag || !PayManager.action.OpenPay)
		{
			return;
		}
		if (Singleton<LevelManager>.Instance.bRstart3)
		{
			Singleton<LevelManager>.Instance.bRstart3 = false;
			return;
		}
		int iNowMapID = Singleton<UserManager>.Instance.iNowMapID;
		if (iMapIndex > iNowMapID)
		{
			iMapID++;
			if (_Map_cloudObj != null)
			{
				UnityEngine.Object.Destroy(_Map_cloudObj);
			}
			_Map_cloudObj = UnityEngine.Object.Instantiate(Map_cloudObj);
			_Map_cloudObj.transform.SetParent(Map_cloudFather.transform, worldPositionStays: false);
			SkeletonAnimation component = _Map_cloudObj.GetComponent<SkeletonAnimation>();
			component.Initialize(overwrite: true);
			component.loop = false;
			int num = iMapID;
			if (num > 20)
			{
				num = 20;
			}
			if (num == 2)
			{
				num = 3;
			}
			component.state.SetAnimation(0, "map" + num + "_static", loop: false);
			UnityEngine.Debug.Log("_iMapID" + num);
			UnityEngine.Debug.Log("iMapIndex" + iMapIndex);
			if (iMapIndex == 1)
			{
				Map_cloudFather.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
			}
			else if (iMapIndex > 0)
			{
				Map_cloudFather.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
			}
			else
			{
				Map_cloudFather.transform.localScale = new Vector3(1f, 1f, 1f);
			}
		}
		else if (_Map_cloudObj != null)
		{
			UnityEngine.Object.Destroy(_Map_cloudObj);
		}
		if (DataManager.iFirstLoginGameLoadCloud == 0)
		{
			UnityEngine.Debug.Log("iFirstLoginGameLoadCloud=" + DataManager.iFirstLoginGameLoadCloud);
			PlayMapCloud(iMapID);
		}
	}

	public void PlayMapCloud(int iMapID)
	{
		iMapID++;
		if (_Map_cloudObj != null)
		{
			UnityEngine.Object.Destroy(_Map_cloudObj);
		}
		_Map_cloudObj = UnityEngine.Object.Instantiate(Map_cloudObj);
		_Map_cloudObj.transform.SetParent(Map_cloudFather.transform, worldPositionStays: false);
		SkeletonAnimation component = _Map_cloudObj.GetComponent<SkeletonAnimation>();
		component.Initialize(overwrite: true);
		component.loop = false;
		int num = iMapID;
		if (num > 20)
		{
			num = 20;
		}
		component.state.SetAnimation(0, "map" + num + "_unlock", loop: false);
		component.state.End += delegate
		{
			SetMapMaxAni();
		};
	}

	private IEnumerator IEOpenLevel()
	{
		yield return new WaitForSeconds(0.5f);
		int iNowSelectLevelIndex = Singleton<LevelManager>.Instance.iNowSelectLevelIndex;
		int iMaxLevelID = LevelManager.iMaxLevelID;
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex <= LevelManager.iMaxLevelID)
		{
			if ((bool)PayManager.action)
			{
				PayManager.action.OpenSignOrPlay();
			}
			else if (UI.Instance.GetPanelCount() <= 0)
			{
				UI.Instance.OpenPanel(UIPanelType.Play);
			}
		}
	}

	public void GoMap(int iIndex, bool binit = false)
	{
		if (!binit)
		{
			MapUI.action.ShowLodingUI();
		}
		iMapIndex = iIndex;
		base.transform.localScale = new Vector3(MapSale[iMapIndex], MapSale[iMapIndex], MapSale[iMapIndex]);
		base.transform.localPosition = new Vector3(0f, 0f, 0f);
		NowMapSale = MapSale[iMapIndex];
		NowMapSaleMin = MapSaleMin[iMapIndex];
		NowMapSaleMax = MapSaleMax[iMapIndex];
		NowMapBorder = MapBorder[iMapIndex];
		InitMapFxAni();
		LoadCloud(iIndex);
	}

	private void InitMapFxAni()
	{
	}

	public void SetMapMax()
	{
		base.transform.localScale = new Vector3(NowMapSaleMax, NowMapSaleMax, NowMapSaleMax);
	}

	public void SetMapMaxAni()
	{
		StartCoroutine(IESetMapMaxAni());
	}

	private IEnumerator IESetMapMaxAni()
	{
		yield return new WaitForSeconds(0.1f);
		int iNowPassLevelID = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
		if (iNowPassLevelID == 0)
		{
			Vector3 vector = BtnManager.action.LBtnObj[0].transform.localPosition + new Vector3(BtnManager.action.LBtnMainPos[0].x, BtnManager.action.LBtnMainPos[0].y, 0f);
			if (iMapIndex > 1)
			{
				Vector3 localPosition = BtnManager.action.LBtnObj[0].transform.localPosition;
				float x = localPosition.x * 0.35f;
				Vector3 localPosition2 = BtnManager.action.LBtnObj[0].transform.localPosition;
				vector = new Vector3(x, localPosition2.y * 0.35f, 0f) + new Vector3(BtnManager.action.LBtnMainPos[0].x, BtnManager.action.LBtnMainPos[0].y, 0f);
			}
			Vector3 vector2 = OldPos = new Vector3(vector.x * -1f * NowMapSaleMax, vector.y * -1f * NowMapSaleMax, vector.z);
		}
		else
		{
			Vector3 vector3 = BtnManager.action.LBtnObj[0].transform.localPosition + new Vector3(BtnManager.action.LBtnMainPos[iMapIndex].x, BtnManager.action.LBtnMainPos[iMapIndex].y, 0f);
			if (iMapIndex > 1)
			{
				Vector3 localPosition3 = BtnManager.action.LBtnObj[0].transform.localPosition;
				float x2 = localPosition3.x * 0.35f;
				Vector3 localPosition4 = BtnManager.action.LBtnObj[0].transform.localPosition;
				vector3 = new Vector3(x2, localPosition4.y * 0.35f, 0f) + new Vector3(BtnManager.action.LBtnMainPos[iMapIndex].x, BtnManager.action.LBtnMainPos[iMapIndex].y, 0f);
			}
			Vector3 vector4 = OldPos = new Vector3(vector3.x * -1f * NowMapSaleMax, vector3.y * -1f * NowMapSaleMax, vector3.z);
		}
		float iCount = 30f;
		int i = 0;
		while ((float)i < iCount)
		{
			yield return new WaitForSeconds(0.01f);
			float moveSize9 = 0f;
			float moveSize8 = 0f;
			float moveSize7 = 0f;
			float moveSize6 = 0f;
			float moveSize5 = 0f;
			Vector3 localPosition5 = base.transform.localPosition;
			if (localPosition5.x == OldPos.x)
			{
				Vector3 localPosition6 = base.transform.localPosition;
				if (localPosition6.y == OldPos.y)
				{
					goto IL_08b7;
				}
			}
			Vector3 localPosition7 = base.transform.localPosition;
			if (localPosition7.x < OldPos.x)
			{
				if (moveSize9 == 0f)
				{
					float x3 = OldPos.x;
					Vector3 localPosition8 = base.transform.localPosition;
					moveSize9 = (x3 - localPosition8.x) / iCount;
				}
				Vector3 localPosition9 = base.transform.localPosition;
				if (localPosition9.x + moveSize9 < OldPos.x)
				{
					Transform transform = base.transform;
					Vector3 localPosition10 = base.transform.localPosition;
					float x4 = localPosition10.x + moveSize9;
					Vector3 localPosition11 = base.transform.localPosition;
					transform.localPosition = new Vector3(x4, localPosition11.y, 0f);
				}
				else
				{
					Transform transform2 = base.transform;
					float x5 = OldPos.x;
					Vector3 localPosition12 = base.transform.localPosition;
					transform2.localPosition = new Vector3(x5, localPosition12.y, 0f);
				}
			}
			else
			{
				if (moveSize8 == 0f)
				{
					Vector3 localPosition13 = base.transform.localPosition;
					moveSize8 = (localPosition13.x - OldPos.x) / iCount;
				}
				Vector3 localPosition14 = base.transform.localPosition;
				if (localPosition14.x - moveSize8 > OldPos.x)
				{
					Transform transform3 = base.transform;
					Vector3 localPosition15 = base.transform.localPosition;
					float x6 = localPosition15.x - moveSize8;
					Vector3 localPosition16 = base.transform.localPosition;
					transform3.localPosition = new Vector3(x6, localPosition16.y, 0f);
				}
				else
				{
					Transform transform4 = base.transform;
					float x7 = OldPos.x;
					Vector3 localPosition17 = base.transform.localPosition;
					transform4.localPosition = new Vector3(x7, localPosition17.y, 0f);
				}
			}
			Vector3 localPosition18 = base.transform.localPosition;
			if (localPosition18.y < OldPos.y)
			{
				if (moveSize7 == 0f)
				{
					float y = OldPos.y;
					Vector3 localPosition19 = base.transform.localPosition;
					moveSize7 = (y - localPosition19.y) / iCount;
				}
				Vector3 localPosition20 = base.transform.localPosition;
				if (localPosition20.y + moveSize7 < OldPos.y)
				{
					Transform transform5 = base.transform;
					Vector3 localPosition21 = base.transform.localPosition;
					float x8 = localPosition21.x;
					Vector3 localPosition22 = base.transform.localPosition;
					transform5.localPosition = new Vector3(x8, localPosition22.y + moveSize7, 0f);
				}
				else
				{
					Transform transform6 = base.transform;
					Vector3 localPosition23 = base.transform.localPosition;
					transform6.localPosition = new Vector3(localPosition23.x, OldPos.y, 0f);
				}
			}
			else
			{
				if (moveSize6 == 0f)
				{
					Vector3 localPosition24 = base.transform.localPosition;
					moveSize6 = (localPosition24.y - OldPos.y) / iCount;
				}
				Vector3 localPosition25 = base.transform.localPosition;
				if (localPosition25.y - moveSize6 > OldPos.y)
				{
					Transform transform7 = base.transform;
					Vector3 localPosition26 = base.transform.localPosition;
					float x9 = localPosition26.x;
					Vector3 localPosition27 = base.transform.localPosition;
					transform7.localPosition = new Vector3(x9, localPosition27.y - moveSize6, 0f);
				}
				else
				{
					Transform transform8 = base.transform;
					Vector3 localPosition28 = base.transform.localPosition;
					transform8.localPosition = new Vector3(localPosition28.x, OldPos.y, 0f);
				}
			}
			goto IL_08b7;
			IL_08b7:
			if (moveSize5 == 0f)
			{
				float nowMapSaleMax = NowMapSaleMax;
				Vector3 localScale = base.transform.localScale;
				moveSize5 = (nowMapSaleMax - localScale.x) / iCount;
			}
			Vector3 localScale2 = base.transform.localScale;
			if (localScale2.x < NowMapSaleMax)
			{
				Transform transform9 = base.transform;
				Vector3 localScale3 = base.transform.localScale;
				float x10 = localScale3.x + moveSize5;
				Vector3 localScale4 = base.transform.localScale;
				float y2 = localScale4.x + moveSize5;
				Vector3 localScale5 = base.transform.localScale;
				transform9.localScale = new Vector3(x10, y2, localScale5.x + moveSize5);
			}
			i++;
		}
		bool bFlag = true;
		if (DataManager.iFirstLoginGameLoadCloud == 0 && iNowPassLevelID == 1)
		{
			BtnManager.action.GirlMove(bFlag: false);
			bFlag = false;
		}
		if (iNowPassLevelID != 0 && bFlag)
		{
			StartCoroutine(IEOpenLevel());
		}
	}

	public void SetMapStock()
	{
		base.transform.localScale = new Vector3(NowMapSale, NowMapSale, NowMapSale);
	}

	public void SetMapGirlPos(Vector3 vPos)
	{
		if (!Singleton<LevelManager>.Instance.bGoNextMap)
		{
			if (DataManager.iFirstLoginGameLoadCloud != 0)
			{
				float num = vPos.x * -1f;
				Vector3 localScale = base.transform.localScale;
				float x = num * localScale.x;
				float num2 = vPos.y * -1f;
				Vector3 localScale2 = base.transform.localScale;
				Vector3 localPosition = new Vector3(x, num2 * localScale2.x, vPos.z);
				base.transform.localPosition = localPosition;
			}
		}
		else
		{
			float num3 = vPos.x * -1f;
			Vector3 localScale3 = base.transform.localScale;
			float x2 = num3 * localScale3.x;
			float num4 = vPos.y * -1f;
			Vector3 localScale4 = base.transform.localScale;
			Vector3 vector = OldPos = new Vector3(x2, num4 * localScale4.x, vPos.z);
		}
	}

	public void InitMapSale()
	{
		MapSaleMin = new float[UserManager.iMapCount];
		MapSaleMax = new float[UserManager.iMapCount];
		MapSale = new float[UserManager.iMapCount];
		MapBorder = new float[UserManager.iMapCount];
		MapSale[0] = (float)Screen.width / 1500f;
		MapSale[1] = (float)Screen.width / 1200f;
		MapSale[2] = (float)Screen.width / 500f;
		MapSale[3] = (float)Screen.width / 500f;
		MapSale[4] = (float)Screen.width / 500f;
		MapSale[5] = (float)Screen.width / 500f;
		MapSale[6] = (float)Screen.width / 500f;
		MapSale[7] = (float)Screen.width / 500f;
		for (int i = 0; i < UserManager.iMapCount; i++)
		{
			if (i >= 8)
			{
				MapSale[i] = (float)Screen.width / 700f;
			}
			if (i == 17)
			{
				MapSale[i] = (float)Screen.width / 500f;
			}
			MapSaleMin[i] = MapSale[i] * 0.5f;
			MapSaleMax[i] = MapSale[i] * 2f;
			MapBorder[i] = 800f;
			if (i > 1)
			{
				MapSale[i] *= 0.6f;
			}
		}
	}

	public void ReSetPos()
	{
		bClickBtn = true;
		ref Vector2 mapNowPos = ref MapNowPos;
		Vector3 localPosition = base.transform.localPosition;
		mapNowPos.x = localPosition.x;
		ref Vector2 mapNowPos2 = ref MapNowPos;
		Vector3 localPosition2 = base.transform.localPosition;
		mapNowPos2.y = localPosition2.y;
	}

	public void DoubleClickMap()
	{
		if (UI.Instance.GetPanelCount() <= 0)
		{
			ResDoubleClick();
			Vector3 localScale = base.transform.localScale;
			if (localScale.x > NowMapSale)
			{
				SetMapStock();
				base.transform.localPosition = new Vector3(0f, 0f, 0f);
			}
		}
	}

	public void ResDoubleClick()
	{
		DoubleCliclOldPos = new Vector3(-100f, -100f);
	}

	public static GameObject TouchChecker(Vector3 mouseposition)
	{
		Vector3 vector = Camera.main.ScreenToWorldPoint(mouseposition);
		Vector2 point = new Vector2(vector.x, vector.y);
		if ((bool)Physics2D.OverlapPoint(point))
		{
			return Physics2D.OverlapPoint(point).gameObject;
		}
		return null;
	}

	private void Update()
	{
		if (bClickBtn)
		{
			Vector3 localPosition = base.transform.localPosition;
			float num = localPosition.x - MapNowPos.x;
			if (num < 0f)
			{
				num *= -1f;
			}
			Vector3 localPosition2 = base.transform.localPosition;
			float num2 = localPosition2.y - MapNowPos.y;
			if (num2 < 0f)
			{
				num2 *= -1f;
			}
			float num3 = num + num2;
			if (num3 <= iMoveSetNumber)
			{
				bClickBtn = true;
			}
			else
			{
				bClickBtn = false;
			}
		}
		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}
		GameObject gameObject = TouchChecker(UnityEngine.Input.mousePosition);
		if (gameObject != null && gameObject.name == "MapUIDoubleClick")
		{
			bool flag = true;
			float num4 = 50f;
			float x = DoubleCliclOldPos.x;
			Vector3 mousePosition = UnityEngine.Input.mousePosition;
			float num5 = x - mousePosition.x;
			float y = DoubleCliclOldPos.y;
			Vector3 mousePosition2 = UnityEngine.Input.mousePosition;
			float num6 = y - mousePosition2.y;
			if (flag && num5 > num4)
			{
				flag = false;
			}
			if (flag && num5 <= 0f && num5 * -1f > num4)
			{
				flag = false;
			}
			if (flag && num6 > num4)
			{
				flag = false;
			}
			if (flag && num6 <= 0f && num6 * -1f > num4)
			{
				flag = false;
			}
			if (flag)
			{
				int nowTime_FF = Util.GetNowTime_FF();
				int num7 = nowTime_FF - DoubleCliclOldTime;
				if (num7 < 250)
				{
					DoubleClickMap();
				}
			}
			else
			{
				DoubleCliclOldPos = UnityEngine.Input.mousePosition;
			}
		}
		DoubleCliclOldTime = Util.GetNowTime_FF();
	}
}
