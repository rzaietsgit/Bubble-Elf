using System.Collections;
using EasyMobile;
using ITSoft;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanelUI : MonoBehaviour
{
	public static SettingPanelUI action;

	public GameObject SettingPanelBtnBg;

	public GameObject MapBtnObj;

	public GameObject SettingBtnObj;

	public GameObject CloseBtn;

	public TextMeshProUGUI MailCount;

	public GameObject MapDianObj;

	public GameObject MapObj_;

	public GameObject MapSet1_;

	public GameObject MapSet2_;

	public GameObject QQqunObj;

	public static bool bSettingPanelUIOpen;

	public static bool bSettingPanelUIReadyOpen;

	private bool bNowMovePanel;

	private bool bShowPanelOpen;

	private bool bPanelFlag = true;

	private Vector3 startFingerPos;

	private Vector3 nowFingerPos;

	private float xMoveDistance;

	private float yMoveDistance;

	private bool OldClickPanel;

	private void Start()
	{
		action = this;
		CheckHideSettingBtnUI();
		InitAndroid.action.CheckQQqun();
	}

	public void ShowQQQun()
	{
		QQqunObj.SetActive(value: true);
	}

	public void HideQQQun()
	{
		QQqunObj.SetActive(value: false);
	}

	public void HideMailCount()
	{
	}

	public void ShowMailCount(int iCount)
	{
		MailCount.SetText(iCount.ToString());
	}

	private void CheckHideSettingBtnUI()
	{
		if (InitGame.bChinaVersion)
		{
			HideSettingBtnUI(bplaymp3: false);
			return;
		}
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_HideSettingBtnUI");
		if (@int == 1)
		{
			HideSettingBtnUI(bplaymp3: false);
		}
	}

	public void CheckClose()
	{
		if (!bSettingPanelUIOpen && !Singleton<DataManager>.Instance.bUiIsOpen)
		{
			return;
		}
		GameObject gameObject = TouchChecker(UnityEngine.Input.mousePosition);
		if (gameObject == null)
		{
			CloseSettingBtnUI();
			return;
		}
		bool flag = true;
		if (gameObject.name == "SettingPanelBtnBg")
		{
			flag = false;
		}
		if (gameObject.name == "MapBtn")
		{
			flag = false;
		}
		if (gameObject.name == "SettingBtn")
		{
			flag = false;
		}
		if (gameObject.name == "MailBtn")
		{
			flag = false;
		}
		if (flag)
		{
			CloseSettingBtnUI();
		}
	}

	public static GameObject TouchChecker(Vector3 mouseposition)
	{
		Canvas component = action.transform.GetComponent<Canvas>();
		if (component.renderMode == RenderMode.ScreenSpaceOverlay)
		{
			if ((bool)Physics2D.OverlapPoint(mouseposition))
			{
				return Physics2D.OverlapPoint(mouseposition).gameObject;
			}
			return null;
		}
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
		if (Singleton<DataManager>.Instance.bGrilMoveing || Util.GetbForced_guidance() || Util.GetNowOpenUI() || UI.Instance.GetPanelCount() > 0)
		{
			return;
		}
		if (Input.GetMouseButtonDown(0))
		{
			UnityEngine.Debug.Log("JySettingPan1");
			CheckClose();
			UnityEngine.Debug.Log("JySettingPan2");
			GameObject gameObject = TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject != null && (gameObject.name == "ClickPanel" || gameObject.name == "MapBtn" || gameObject.name == "MailBtn" || gameObject.name == "SettingBtn"))
			{
				UnityEngine.Debug.Log("JySettingPan3 Pointer.name" + gameObject.name);
				if ((bool)SoundController.action)
				{
					SoundController.action.playNow("ButtonClick");
				}
				MapUI.action.bClickUI = true;
				nowFingerPos = UnityEngine.Input.mousePosition;
				bNowMovePanel = true;
				OldClickPanel = true;
				if (Singleton<DataManager>.Instance.bUiIsOpen)
				{
					OldClickPanel = false;
				}
				bSettingPanelUIReadyOpen = true;
				if (gameObject.name == "MapBtn")
				{
					MapBtnObj.transform.Find("Select").GetComponent<Image>().enabled = true;
					MapBtnObj.GetComponent<Toggle>().isOn = true;
				}
				else if (!(gameObject.name == "MailBtn") && gameObject.name == "SettingBtn")
				{
					SettingBtnObj.transform.Find("Select").GetComponent<Image>().enabled = true;
					SettingBtnObj.GetComponent<Toggle>().isOn = true;
					SetPanelUI.action.hideall();
				}
			}
			if (gameObject != null && gameObject.name == "SettingPanelBtnBg")
			{
				MapUI.action.bClickUI = true;
				nowFingerPos = UnityEngine.Input.mousePosition;
				bNowMovePanel = true;
			}
			CheckClickPanel();
		}
		if (!Input.GetMouseButtonUp(0))
		{
			return;
		}
		MapUI.action.bClickUI = false;
		bSettingPanelUIReadyOpen = false;
		if (OldClickPanel)
		{
			OldClickPanel = false;
			GameObject gameObject2 = TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject2 != null && (gameObject2.name == "MapBtn" || gameObject2.name == "MailBtn" || gameObject2.name == "SettingBtn"))
			{
				if (bSettingPanelUIOpen || Singleton<DataManager>.Instance.bUiIsOpen)
				{
					bNowMovePanel = false;
					return;
				}
				OpenSettingBtnUI();
			}
			else if (!bSettingPanelUIOpen)
			{
				SettingBtnObj.transform.Find("Select").GetComponent<Image>().enabled = false;
				MapBtnObj.transform.Find("Select").GetComponent<Image>().enabled = false;
			}
		}
		bNowMovePanel = false;
		bShowPanelOpen = false;
	}

	public void ClickAddQQun()
	{
		UnityEngine.Debug.Log("jy ClickAddQQun");
		InitAndroid.action.ClickAddQQun();
	}

	public void CheckClickPanel()
	{
		UnityEngine.Debug.Log("jy2222 CheckClickPanel 1");
		if (Singleton<DataManager>.Instance.bGrilMoveing || Util.GetbForced_guidance() || !bNowMovePanel || bSettingPanelUIOpen)
		{
			return;
		}
		startFingerPos = UnityEngine.Input.mousePosition;
		GameObject gameObject = TouchChecker(UnityEngine.Input.mousePosition);
		if (gameObject != null && gameObject.name == "ClickPanel")
		{
			if (bPanelFlag)
			{
				HideSettingBtnUI();
			}
			else
			{
				ShowSettingBtnUI();
			}
		}
	}

	public void CheckPanelMove2()
	{
		if (Singleton<DataManager>.Instance.bGrilMoveing || Util.GetbForced_guidance() || !bNowMovePanel || !bSettingPanelUIOpen)
		{
			return;
		}
		startFingerPos = UnityEngine.Input.mousePosition;
		GameObject gameObject = TouchChecker(UnityEngine.Input.mousePosition);
		if (gameObject != null && gameObject.name == "SettingPanelBtnBg")
		{
			xMoveDistance = Mathf.Abs(nowFingerPos.x - startFingerPos.x);
			yMoveDistance = Mathf.Abs(nowFingerPos.y - startFingerPos.y);
			if (xMoveDistance > yMoveDistance && !(nowFingerPos.x - startFingerPos.x > 10f) && nowFingerPos.x - startFingerPos.x < -50f)
			{
				CloseSettingBtnUI();
				bPanelFlag = true;
			}
		}
	}

	public void CheckPanelMove()
	{
		if (Singleton<DataManager>.Instance.bGrilMoveing || Util.GetbForced_guidance() || !bNowMovePanel || bSettingPanelUIOpen)
		{
			return;
		}
		startFingerPos = UnityEngine.Input.mousePosition;
		GameObject gameObject = TouchChecker(UnityEngine.Input.mousePosition);
		if (!(gameObject != null) || (!(gameObject.name == "MapBtn") && !(gameObject.name == "MailBtn") && !(gameObject.name == "SettingBtn")))
		{
			return;
		}
		xMoveDistance = Mathf.Abs(nowFingerPos.x - startFingerPos.x);
		yMoveDistance = Mathf.Abs(nowFingerPos.y - startFingerPos.y);
		if (xMoveDistance > yMoveDistance)
		{
			if (nowFingerPos.x - startFingerPos.x > 10f)
			{
				ShowSettingBtnUI();
			}
			else if (nowFingerPos.x - startFingerPos.x < -10f)
			{
				HideSettingBtnUI(bplaymp3: false);
			}
		}
	}

	public void HideSettingBtnUI(bool bplaymp3 = true)
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance())
		{
			bPanelFlag = false;
			if (bplaymp3 && (bool)SoundController.action)
			{
				SoundController.action.playNow("ui_Recover_swoop");
			}
			BaseUIAnimation.action.HideSettingBtnUI(SettingPanelBtnBg.gameObject);
		}
	}

	public void MoveHideSettingBtnUI()
	{
		if (!Util.GetbForced_guidance())
		{
			BaseUIAnimation.action.MoveHideSettingBtnUI(SettingPanelBtnBg.gameObject);
		}
	}

	public void MoveShowSettingBtnUI()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance())
		{
			BaseUIAnimation.action.MoveShowSettingBtnUI(SettingPanelBtnBg.gameObject);
		}
	}

	public void ShowSettingBtnUI()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance())
		{
			bPanelFlag = true;
			bShowPanelOpen = true;
			BaseUIAnimation.action.ShowSettingBtnUI(SettingPanelBtnBg.gameObject);
		}
	}

	public void MapShowSettingBtnUI()
	{
		bPanelFlag = true;
		bShowPanelOpen = true;
		bNowMovePanel = false;
		bSettingPanelUIOpen = true;
		bSettingPanelUIReadyOpen = false;
		Singleton<DataManager>.Instance.bUiIsOpen = true;
		BaseUIAnimation.action.MapShowSettingBtnUI(SettingPanelBtnBg.gameObject);
		if ((bool)MapUI.action)
		{
			MapUI.action.ResInvitationObj();
		}
		MapUI.action.HideDownUI1();
		action.OpenMapReward();
	}

	public void OpenSettingBtnUI()
	{
		if (Singleton<DataManager>.Instance.bGrilMoveing || Util.GetbForced_guidance() || (bool)LanguageUI.action)
		{
			return;
		}
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		if (bShowPanelOpen)
		{
			return;
		}
		bNowMovePanel = false;
		if (bPanelFlag)
		{
			BaseUIAnimation.action.OpenSettingPanelUI(SettingPanelBtnBg.gameObject, base.transform.Find("mask").gameObject);
			bSettingPanelUIOpen = true;
			bSettingPanelUIReadyOpen = false;
			Singleton<DataManager>.Instance.bUiIsOpen = true;
			if ((bool)MapUI.action)
			{
				MapUI.action.ResInvitationObj();
			}
			MapUI.action.HideDownUI1();
            AdsManager.ShowBanner();
        }
	}

	public void MapOpenSettingBtn()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance())
		{
			bNowMovePanel = false;
			BaseUIAnimation.action.OpenSettingPanelUI(SettingPanelBtnBg.gameObject, base.transform.Find("mask").gameObject);
			bSettingPanelUIOpen = true;
			bSettingPanelUIReadyOpen = false;
			Singleton<DataManager>.Instance.bUiIsOpen = true;
			if ((bool)MapUI.action)
			{
				MapUI.action.ResInvitationObj();
			}
			MapUI.action.HideDownUI1();
		}
	}

	private IEnumerator CallCloseSettingBtnUI()
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		bSettingPanelUIOpen = false;
		base.transform.Find("mask").gameObject.SetActive(value: false);
		SettingBtnObj.transform.Find("Select").GetComponent<Image>().enabled = false;
		MapBtnObj.transform.Find("Select").GetComponent<Image>().enabled = false;
		BaseUIAnimation.action.CloseSettingPanelUI(SettingPanelBtnBg.gameObject);
		MapUI.action.ShowDownUI1();
        AdsManager.HideBanner();
    }

	public void CloseSettingBtnUI()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance())
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("ButtonClick");
			}
			bNowMovePanel = false;
			if (bPanelFlag && bSettingPanelUIOpen)
			{
				StartCoroutine(CallCloseSettingBtnUI());
			}
		}
	}

	public void _CloseSettingBtnUI()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance())
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("ButtonClick");
			}
			bNowMovePanel = false;
			if (bPanelFlag && bSettingPanelUIOpen && BaseUIAnimation.bClickButton)
			{
				BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
				StartCoroutine(CallCloseSettingBtnUI());
				SettingBtnObj.transform.Find("Select").GetComponent<Image>().enabled = false;
				MapBtnObj.transform.Find("Select").GetComponent<Image>().enabled = false;
			}
		}
	}

	public void ChangeMapCloseSetting()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance())
		{
			bNowMovePanel = false;
			if (bPanelFlag && bSettingPanelUIOpen)
			{
				bSettingPanelUIOpen = false;
				base.transform.Find("mask").gameObject.SetActive(value: false);
				SettingBtnObj.transform.Find("Select").GetComponent<Image>().enabled = false;
				MapBtnObj.transform.Find("Select").GetComponent<Image>().enabled = false;
				BaseUIAnimation.action.CloseSettingPanelUI(SettingPanelBtnBg.gameObject);
				MapUI.action.ShowDownUI1();
			}
		}
	}

	public void OpenMapReward()
	{
		MapObj_.SetActive(value: true);
		MapSet1_.SetActive(value: false);
		MapSet2_.SetActive(value: false);
		SettingBtnObj.transform.Find("Select").GetComponent<Image>().enabled = false;
		MapBtnObj.transform.Find("Select").GetComponent<Image>().enabled = true;
	}
}
