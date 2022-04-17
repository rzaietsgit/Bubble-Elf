using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelBtnScript : MonoBehaviour
{
	public GameObject BtnObj;

	public TextMeshProUGUI LevelBtnNumber;

	public GameObject StarBg1;

	public GameObject StarBg2;

	public GameObject StarBg3;

	public Sprite StarSprite;

	public GameObject levle_starObj;

	public GameObject level_btnObj;

	public GameObject fx_ui_mapPinObj;

	public GameObject LevelRewardObj;

	public GameObject TipObj;

	public Text TipText;

	public Text MapUnBtnText;

	public Sprite PassLevelBtn;

	private int iNowBtnSelectLevelIndex = 1;

	private int iLevelIndexID;

	private int istarNb;

	private int iiiiiii = 2;

	public void ClearRemark()
	{
		TipObj.SetActive(value: false);
	}

	public void SetPassLevel()
	{
		BtnObj.GetComponent<Image>().sprite = PassLevelBtn;
		BtnObj.GetComponent<Button>().enabled = true;
	}

	public void Unlocked(bool b = true)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(level_btnObj, base.transform.position, base.transform.rotation);
		gameObject.transform.parent = base.transform.transform;
		if (b)
		{
			Createfx_ui_mapPin();
		}
		UnityEngine.Object.Destroy(gameObject, 3f);
	}

	public void Createfx_ui_mapPin()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(fx_ui_mapPinObj);
		gameObject.transform.parent = base.transform.transform;
		gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
		gameObject.transform.localScale = new Vector3(100f, 100f, 0f);
	}

	public void HideNumber()
	{
		LevelBtnNumber.gameObject.SetActive(value: false);
		if (PayManager.action.OpenPay)
		{
			BtnObj.GetComponent<Button>().enabled = false;
			return;
		}
		BtnObj.GetComponent<Button>().enabled = true;
		LevelBtnNumber.gameObject.SetActive(value: true);
	}

	public void ShowNumber()
	{
		LevelBtnNumber.gameObject.SetActive(value: true);
		BtnObj.GetComponent<Button>().enabled = true;
	}

	public void SetLevelBtnNumber(int index)
	{
		if (index >= 100)
		{
			LevelBtnNumber.fontSize = 26f;
		}
		iLevelIndexID = index;
		LevelBtnNumber.SetText(index.ToString());
		SetRewardState(index);
	}

	public void HideStar()
	{
		StarBg1.SetActive(value: false);
		StarBg2.SetActive(value: false);
		StarBg3.SetActive(value: false);
		StartCoroutine(IEStarAni());
	}

	private void OpenFx_StarAni(GameObject obj)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(levle_starObj, base.transform.position, base.transform.rotation);
		gameObject.transform.parent = obj.transform.transform;
		gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
		UnityEngine.Object.Destroy(gameObject, 3f);
	}

	private IEnumerator IEStarAni()
	{
		yield return new WaitForSeconds(0.1f);
		if (istarNb >= 1)
		{
			StarBg1.GetComponent<Image>().sprite = StarSprite;
			StarBg1.transform.localPosition = new Vector3(0f, 0f, 0f);
			StarBg1.transform.localScale = new Vector2(0f, 0f);
			StarBg1.SetActive(value: true);
			Vector3 endValue = new Vector3(-50f, 66f, 0f);
			Sequence s = DOTween.Sequence();
			s.Append(StarBg1.transform.DOScale(new Vector2(1f, 1.2f), 0.35f)).Append(StarBg1.transform.DOScale(new Vector2(0.8f, 0.8f), 0.15f));
			StarBg1.transform.DOLocalMove(endValue, 0.35f).OnComplete(delegate
			{
				OpenFx_StarAni(StarBg1);
			});
		}
		yield return new WaitForSeconds(0.2f);
		if (istarNb >= 2)
		{
			StarBg2.GetComponent<Image>().sprite = StarSprite;
			StarBg2.transform.localPosition = new Vector3(0f, 0f, 0f);
			StarBg2.transform.localScale = new Vector2(0f, 0f);
			StarBg2.SetActive(value: true);
			Sequence s2 = DOTween.Sequence();
			s2.Append(StarBg2.transform.DOScale(new Vector2(1f, 1.2f), 0.35f)).Append(StarBg2.transform.DOScale(new Vector2(0.8f, 0.8f), 0.15f));
			ShortcutExtensions.DOLocalMove(endValue: new Vector3(0f, 79.3f, 0f), target: StarBg2.transform, duration: 0.35f).OnComplete(delegate
			{
				OpenFx_StarAni(StarBg2);
			});
		}
		yield return new WaitForSeconds(0.2f);
		if (istarNb >= 3)
		{
			StarBg3.GetComponent<Image>().sprite = StarSprite;
			StarBg3.transform.localPosition = new Vector3(0f, 0f, 0f);
			StarBg3.transform.localScale = new Vector2(0f, 0f);
			StarBg3.SetActive(value: true);
			Vector3 endValue3 = new Vector3(50f, 66f, 0f);
			Sequence s3 = DOTween.Sequence();
			s3.Append(StarBg3.transform.DOScale(new Vector2(1f, 1.2f), 0.35f)).Append(StarBg3.transform.DOScale(new Vector2(0.8f, 0.8f), 0.15f));
			StarBg3.transform.DOLocalMove(endValue3, 0.35f).OnComplete(delegate
			{
				OpenFx_StarAni(StarBg3);
			});
		}
	}

	public void SetBtnState(int iStarNumber)
	{
		istarNb = iStarNumber;
		if (iStarNumber == 0)
		{
			StarBg1.SetActive(value: false);
			StarBg2.SetActive(value: false);
			StarBg3.SetActive(value: false);
			return;
		}
		if (iStarNumber >= 1)
		{
			StarBg1.GetComponent<Image>().sprite = StarSprite;
			StarBg1.AddComponent<Canvas>();
			StarBg1.GetComponent<Canvas>().overrideSorting = true;
			StarBg1.GetComponent<Canvas>().sortingOrder = 10;
		}
		if (iStarNumber >= 2)
		{
			StarBg2.GetComponent<Image>().sprite = StarSprite;
			StarBg2.AddComponent<Canvas>();
			StarBg2.GetComponent<Canvas>().overrideSorting = true;
			StarBg2.GetComponent<Canvas>().sortingOrder = 10;
		}
		if (iStarNumber >= 3)
		{
			StarBg3.GetComponent<Image>().sprite = StarSprite;
			StarBg3.AddComponent<Canvas>();
			StarBg3.GetComponent<Canvas>().overrideSorting = true;
			StarBg3.GetComponent<Canvas>().sortingOrder = 10;
		}
	}

	private void Start()
	{
	}

	public void SelectLevelIndex(int index)
	{
		iNowBtnSelectLevelIndex = index;
		SetLevelBtnNumber(index);
	}

	private void Update()
	{
	}

	public GameObject TouchChecker(Vector3 mouseposition)
	{
		if ((bool)Physics2D.OverlapPoint(mouseposition))
		{
			return Physics2D.OverlapPoint(mouseposition).gameObject;
		}
		return null;
	}

	public void UpdateBtnScale(float fScale)
	{
		base.transform.localScale = new Vector2(fScale, fScale);
	}

	private IEnumerator IETipRemark()
	{
		yield return new WaitForSeconds(0.2f);
		TipObj.SetActive(value: true);
		string sRemark = Singleton<DataManager>.Instance.dDataLevelReward[iLevelIndexID.ToString()]["Remark"];
		TipText.text = sRemark;
	}

	public void ClickLevelReward()
	{
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LevelScore_" + (iLevelIndexID - 1)) == 0)
		{
			StartCoroutine(IETipRemark());
		}
		else if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_LevelReward_" + iLevelIndexID) == 0)
		{
			Singleton<DataManager>.Instance.iLevelRewardLevelID = iLevelIndexID;
			int iLevelRewardID = int.Parse(Singleton<DataManager>.Instance.dDataLevelReward[iLevelIndexID.ToString()]["RewardID"]);
			int iLevelRewardCount = int.Parse(Singleton<DataManager>.Instance.dDataLevelReward[iLevelIndexID.ToString()]["RewardCount"]);
			Singleton<DataManager>.Instance.iLevelRewardID = iLevelRewardID;
			Singleton<DataManager>.Instance.iLevelRewardCount = iLevelRewardCount;
			Singleton<UIManager>.Instance.OpenUI(EnumUIType.LevelRewardUI);
		}
	}

	public void Reslanguage()
	{
		BaseUIAnimation.action.SetLanguageFont("MapUnBtnText", MapUnBtnText, string.Empty);
	}

	public void SetRewardState(int indexLevel)
	{
		LevelRewardObj.SetActive(value: false);
		if (InitGame.bChinaVersion && Singleton<DataManager>.Instance.LevelRewardDataCache.Contains(indexLevel.ToString()))
		{
			string empty = string.Empty;
			if (indexLevel - 1 > Singleton<UserManager>.Instance.iNowPassLevelID)
			{
				empty = Singleton<DataManager>.Instance.dDataMapicon[indexLevel.ToString()]["unicon"];
			}
			else
			{
				empty = Singleton<DataManager>.Instance.dDataMapicon[indexLevel.ToString()]["icon"];
				LevelRewardObj.transform.localPosition = new Vector3(0f, 188f, 0f);
				LevelRewardObj.transform.Find("Text").gameObject.SetActive(value: false);
			}
			if (indexLevel == 251)
			{
				UnityEngine.Debug.Log("215  sname=sname=" + empty);
				UnityEngine.Debug.Log("215  indexLevel=indexLevel=" + indexLevel);
				LevelRewardObj.GetComponent<Image>().enabled = false;
				LevelRewardObj.transform.Find("iconmax").GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/mapicon/" + empty, 268, 292);
				LevelRewardObj.transform.Find("iconmax").gameObject.SetActive(value: false);
				LevelRewardObj.transform.Find("iconmax").gameObject.SetActive(value: true);
			}
			else
			{
				UnityEngine.Debug.Log("sname=sname=" + empty);
				UnityEngine.Debug.Log("indexLevel=indexLevel=" + indexLevel);
				LevelRewardObj.transform.Find("icon").GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/mapicon/" + empty, 134, 146);
				LevelRewardObj.transform.Find("icon").gameObject.SetActive(value: false);
				LevelRewardObj.transform.Find("icon").gameObject.SetActive(value: true);
			}
			LevelRewardObj.name = "RewardObj_" + indexLevel;
			BaseUIAnimation.action.SetLanguageFont("MapUnBtnText", MapUnBtnText, string.Empty);
			LevelRewardObj.SetActive(value: false);
			LevelRewardObj.SetActive(value: true);
			if (indexLevel == 251)
			{
				LevelRewardObj.transform.Find("iconmax").gameObject.SetActive(value: false);
				LevelRewardObj.transform.Find("iconmax").gameObject.SetActive(value: true);
			}
			else
			{
				LevelRewardObj.transform.Find("icon").gameObject.SetActive(value: false);
				LevelRewardObj.transform.Find("icon").gameObject.SetActive(value: true);
			}
			Canvas component = LevelRewardObj.GetComponent<Canvas>();
			component.overrideSorting = true;
		}
	}

	public void OpenPlayUI()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		if (Util.GetbForced_guidance() || Singleton<DataManager>.Instance.bGrilMoveing || !BtnManager.bClickBtn)
		{
			return;
		}
		Singleton<LevelManager>.Instance.iNowSelectLevelIndex = iNowBtnSelectLevelIndex;
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex > 1)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
			if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex > @int + 1)
			{
				return;
			}
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex <= LevelManager.iMaxLevelID)
		{
			if (BaseUIAnimation.action.BuyLiveSale())
			{
				Singleton<DataManager>.Instance.EBuyLiveSale = EnumUIType.PlayUI;
				UI.Instance.OpenPanel(UIPanelType.BuyLivesChinaUI);
			}
			else if (UI.Instance.GetPanelCount() <= 0)
			{
				UI.Instance.OpenPanel(UIPanelType.Play);
			}
		}
	}
}
