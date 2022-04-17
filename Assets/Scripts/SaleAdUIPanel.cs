using System.Collections;
using EasyMobile;
//using Umeng;
using UnityEngine;
using UnityEngine.UI;

public class SaleAdUIPanel : SaleAdUIPanelBase
{
	public static SaleAdUIPanel panel;

	private string key = " ";

	private bool isClickPayBtn;

	public override void InitUI()
	{
		UnityEngine.Debug.Log("InitUI```````````````````````````");
		panel = this;
		key = DataManager.sale_adKey;
		//Analytics.Event("ShowSale" + key);
		detail.Top_Image.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/sale_ad/" + key, 684, 836);
		detail.adfree_Image.gameObject.SetActive(value: false);
		if (key == "SaleAdUILoginReward")
		{
			UnityEngine.Debug.Log("免费领取");
			detail.PayText_Text.text = "免费领取";
		}
		else
		{
			InitAndroid.action.checkshowBuyButton();
			InitAndroid.action.checkShowAdTip();
		}
		InitAndroid.action.GAEvent("showlb" + key);
		InitAndroid.action.GAEvent("Newshowlb:" + key + ":" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
		panel.gameObject.SetActive(value: true);
	}

	public override void OnPayBtn()
	{
		isClickPayBtn = true;
		//Analytics.Event("ClickSale" + key);
		if (key == "SaleAdUILoginReward")
		{
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "SaleAdUILoginReward") == 0)
			{
				Singleton<DataManager>.Instance.SaveUserDate("SaleAdUILoginReward", 1);
				ChinaPay.action.addRewardAll(7, 1, MapUI.action.gameObject, isShow: false);
				ChinaPay.action.addRewardAll(3, 18, MapUI.action.gameObject, isShow: false);
				ChinaPay.action.addRewardAll(8, 1, MapUI.action.gameObject, isShow: false);
				BaseUIAnimation.action.ShowProp(7, 1, 3, 18, 8, 1, MapUI.action.gameObject);
				UI.Instance.ClosePanel();
			}
		}
		else if (key == "Pay2")
		{
			UI.Instance.OpenPanel(UIPanelType.ChinaShop);
		}
		else
		{
			//InitAndroid.action.doChainePay(key);
            IAPManager.Purchase(key.ToLower());
        }
	}

	private IEnumerator IEStartGame()
	{
		bool b = true;
		while (b)
		{
			yield return new WaitForSeconds(0.1f);
			if (Singleton<LevelManager>.Instance.bLoadOver)
			{
				b = false;
				Camera.main.transform.GetComponent<RapidBlurEffect>().enabled = false;
				Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.GameScene);
			}
		}
	}

	public override void OnExit()
	{
		UnityEngine.Debug.Log("OnExit1111111111111111111111```````````````" + Singleton<DataManager>.Instance.PlayGameOpenSale);
		if (Singleton<DataManager>.Instance.PlayGameOpenSale)
		{
			Singleton<LevelManager>.Instance.bLoadOver = false;
			if (LevelManager.bWwwDataFlag)
			{
				Singleton<LevelManager>.Instance.LoadLevelData();
				DataManager.SelectLevel = 0;
				DDOLSingleton<CoroutineController>.Instance.StartCoroutine(IEStartGame());
			}
			else
			{
				UnityEngine.Debug.Log("OnExit1111111111111111111111```````````1````");
				Singleton<UserManager>.Instance.EnterLog();
				Camera.main.transform.GetComponent<RapidBlurEffect>().enabled = false;
				Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.GameScene);
			}
		}
		if (Singleton<DataManager>.Instance.SaleChangeMapWin || Singleton<DataManager>.Instance.SaleChangeMapLose)
		{
			UnityEngine.Debug.Log("OnExit1111111111111111111111```````````2````");
			Singleton<DataManager>.Instance.SaleChangeMapLose = false;
			Singleton<DataManager>.Instance.SaleChangeMapWin = false;
			Singleton<LevelManager>.Instance.bLoadOver = false;
			Camera.main.transform.GetComponent<RapidBlurEffect>().enabled = false;
			Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
		}
	}

	public void ShowAdTip()
	{
		if (!(key == "SaleAdUILoginReward"))
		{
			detail.adfree_Image.gameObject.SetActive(value: true);
		}
	}
}
