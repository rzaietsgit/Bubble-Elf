
using UnityEngine;

public class ChinaPay : MonoBehaviour
{
	public static ChinaPay action;

	private void Start()
	{
		action = this;
	}

	public void CallReward24()
	{
		action.addRewardAll(5, 3, MapUI.action.gameObject, isShow: false);
		action.addRewardAll(3, 100, MapUI.action.gameObject, isShow: false);
		action.addRewardAll(4, 3, MapUI.action.gameObject, isShow: false);
		BaseUIAnimation.action.ShowProp(5, 3, 3, 100, 4, 3, MapUI.action.gameObject);
		if ((bool)xinshou.action)
		{
			xinshou.action.ResUIBtn();
		}
		//Analytics.Event("Reward24");
	}

	public void CallNovicepacks()
	{
		if ((bool)MapUI.action)
		{
			MapUI.action.isCanXinshoulibao = false;
		}
		Singleton<DataManager>.Instance.SaveUserDate("DB_XINSHOULIBAO", 1);
		GameObject obj = null;
		if ((bool)MapUI.action)
		{
			obj = ((!SaleAdUI.action) ? MapUI.action.gameObject : MapUI.action.gameObject);
		}
		action.addRewardAll(2, 1000, obj, isShow: false);
		action.addRewardAll(4, 1, obj, isShow: false);
		action.addRewardAll(6, 1, obj, isShow: false);
		action.addRewardAll(5, 1, obj, isShow: false);
		BaseUIAnimation.action.ShowProp(2, 1000, 4, 1, 6, 1, 5, 1, obj);
		if ((bool)yiyuanlibao.action)
		{
			yiyuanlibao.action.ResUIBtn();
		}
	}

	public void shouchonglibao()
	{
		action.addRewardAll(3, 150, FirstPacksUI.action.gameObject, isShow: false);
		action.addRewardAll(2, 5000, FirstPacksUI.action.gameObject, isShow: false);
		action.addRewardAll(6, 2, FirstPacksUI.action.gameObject, isShow: false);
		action.addRewardAll(5, 2, FirstPacksUI.action.gameObject, isShow: false);
		BaseUIAnimation.action.ShowProp(3, 150, 2, 5000, 6, 2, 5, 2, FirstPacksUI.action.gameObject);
	}

	public void ShopUIdaoju1()
	{
		CallShopUIdaoju1();
	}

	public void CallShopUIdaoju1()
	{
		action.addRewardAll(5, 5, ShopUI.action.gameObject);
	}

	public void ShopUIdaoju2()
	{
		CallShopUIdaoju2();
	}

	public void CallShopUIdaoju2()
	{
		action.addRewardAll(6, 3, ShopUI.action.gameObject);
	}

	public void ShopUIdaoju3()
	{
		CallShopUIdaoju3();
	}

	public void CallShopUIdaoju3()
	{
		action.addRewardAll(4, 3, ShopUI.action.gameObject);
	}

	public void ShopUIdaoju4()
	{
		CallShopUIdaoju4();
	}

	public void CallShopUIdaoju4()
	{
		action.addRewardAll(1, 1, ShopUI.action.gameObject);
	}

	public void ShopUIdaoju5()
	{
		CallShopUIdaoju5();
	}

	public void CallShopUIdaoju5()
	{
		action.addRewardAll(1, 3, ShopUI.action.gameObject);
	}

	public void ShopUIdaoju6()
	{
		CallShopUIdaoju6();
	}

	public void CallShopUIdaoju6()
	{
		action.addRewardAll(1, 5, ShopUI.action.gameObject);
	}

	public void cdkey1()
	{
		action.addRewardAll(3, 188, cdkeyUI.action.gameObject, isShow: false);
		action.addRewardAll(4, 1, cdkeyUI.action.gameObject, isShow: false);
		action.addRewardAll(6, 1, cdkeyUI.action.gameObject, isShow: false);
		action.addRewardAll(2, 1888, cdkeyUI.action.gameObject, isShow: false);
		BaseUIAnimation.action.ShowProp(3, 188, 4, 1, 6, 1, 2, 1888, cdkeyUI.action.gameObject);
	}

	public void cdkey2()
	{
		action.addRewardAll(3, 88, cdkeyUI.action.gameObject, isShow: false);
		action.addRewardAll(4, 1, cdkeyUI.action.gameObject, isShow: false);
		action.addRewardAll(6, 1, cdkeyUI.action.gameObject, isShow: false);
		action.addRewardAll(2, 888, cdkeyUI.action.gameObject, isShow: false);
		BaseUIAnimation.action.ShowProp(3, 88, 4, 1, 6, 1, 2, 888, cdkeyUI.action.gameObject);
	}

	public void cdkey3()
	{
		action.addRewardAll(3, 38, cdkeyUI.action.gameObject, isShow: false);
		action.addRewardAll(4, 1, cdkeyUI.action.gameObject, isShow: false);
		action.addRewardAll(6, 1, cdkeyUI.action.gameObject, isShow: false);
		action.addRewardAll(2, 688, cdkeyUI.action.gameObject, isShow: false);
		BaseUIAnimation.action.ShowProp(3, 38, 4, 1, 6, 1, 2, 688, cdkeyUI.action.gameObject);
	}

	public void cdkey4()
	{
		action.addRewardAll(3, 156, cdkeyUI.action.gameObject, isShow: false);
	}

	public void cdkey5()
	{
		action.addRewardAll(3, 318, cdkeyUI.action.gameObject, isShow: false);
	}

	public void cdkey6()
	{
		action.addRewardAll(3, 858, cdkeyUI.action.gameObject, isShow: false);
	}

	public void cdkey7()
	{
		action.addRewardAll(3, 2088, cdkeyUI.action.gameObject, isShow: false);
	}

	public void cdkey8()
	{
		action.addRewardAll(3, 3988, cdkeyUI.action.gameObject, isShow: false);
	}

	public void addRewardAll(int index, int num, GameObject obj, bool isShow = true, string sendtype = "def", string localid = "def", int levelremark = 0)
	{
		InitAndroid.action.GAEvent("RewardAll:" + index + num);
		aliyunlog.rewardSkill(sendtype, localid, index, num);
		switch (index)
		{
		case 1:
			addReward1(num, obj, isShow);
			break;
		case 2:
			addReward2(num, obj, isShow);
			break;
		case 3:
			addReward3(num, obj, isShow);
			break;
		case 4:
			addReward4(num, obj, isShow);
			break;
		case 5:
			addReward5(num, obj, isShow);
			break;
		case 6:
			addReward6(num, obj, isShow);
			break;
		case 7:
			addReward7(num, obj, isShow);
			break;
		case 8:
			addReward8(num, obj, isShow);
			break;
		case 9:
			addReward9(num, obj, isShow);
			break;
		case 10:
			addReward10(num, obj, isShow);
			break;
		case 11:
			addReward11(num, obj, isShow);
			break;
		case 12:
			addReward12(num, obj, isShow);
			break;
		case 13:
			addReward13(num, obj, isShow);
			break;
		case 14:
			addReward14(num, obj, isShow);
			break;
		case 17:
			addReward17(num, obj, isShow);
			break;
		case 15:
			addReward15(num, obj, isShow);
			break;
		case 16:
			addReward16(num, obj, isShow);
			break;
		}
	}

	public void addReward1(int num, GameObject obj, bool isShow = true)
	{
		if (isShow)
		{
			BaseUIAnimation.action.ShowProp(1, num, obj);
		}
		Singleton<LevelManager>.Instance.AddLove(num);
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_reward", NowPlay: true);
		}
	}

	public void addReward2(int num, GameObject obj, bool isShow = true)
	{
		if (isShow)
		{
			BaseUIAnimation.action.ShowProp(2, num, obj);
		}
		PayManager.action.AddGB(0, num);
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_reward", NowPlay: true);
		}
		if ((bool)ChinaShopPanel.panel && (bool)GameUI.action)
		{
			ChinaShopPanel.panel.LoadDataShopUI();
		}
	}

	public void addReward3(int num, GameObject obj, bool isShow = true, bool bAdd = true)
	{
		if ((bool)PayManager.action && bAdd)
		{
			PayManager.action.AwardAddGold(num, "QIANDAO");
		}
		if (isShow)
		{
			BaseUIAnimation.action.ShowProp(3, num, obj);
		}
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_reward", NowPlay: true);
		}
		if ((bool)ChinaShopPanel.panel && (bool)GameUI.action)
		{
			ChinaShopPanel.panel.LoadDataShopUI();
		}
	}

	public void addReward4(int num, GameObject obj, bool isShow = true)
	{
		if (isShow)
		{
			BaseUIAnimation.action.ShowProp(4, num, obj);
		}
		if ((bool)PayManager.action)
		{
			PayManager.action.AddSkill(1, num, "QIANDAO");
		}
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_reward", NowPlay: true);
		}
	}

	public void addReward5(int num, GameObject obj, bool isShow = true)
	{
		if (isShow)
		{
			BaseUIAnimation.action.ShowProp(5, num, obj);
		}
		if ((bool)PayManager.action)
		{
			PayManager.action.AddSkill(2, num, "QIANDAO");
		}
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_reward", NowPlay: true);
		}
	}

	public void addReward6(int num, GameObject obj, bool isShow = true)
	{
		if (isShow)
		{
			BaseUIAnimation.action.ShowProp(6, num, obj);
		}
		if ((bool)PayManager.action)
		{
			PayManager.action.AddSkill(3, num, "QIANDAO");
		}
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_reward", NowPlay: true);
		}
	}

	public void addReward17(int num, GameObject obj, bool isShow = true)
	{
		if (isShow)
		{
			BaseUIAnimation.action.ShowProp(17, num, obj);
		}
		if ((bool)PayManager.action)
		{
			PayManager.action.AddSkill(0, num, "QIANDAO");
		}
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_reward", NowPlay: true);
		}
	}

	public void addReward7(int num, GameObject obj, bool isShow = true)
	{
		if (isShow)
		{
			BaseUIAnimation.action.ShowProp(7, num, obj);
		}
		if ((bool)PayManager.action)
		{
			PayManager.action.AddSkill(4, num, "QIANDAO");
		}
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_reward", NowPlay: true);
		}
	}

	public void addReward8(int num, GameObject obj, bool isShow = true)
	{
		if (isShow)
		{
			BaseUIAnimation.action.ShowProp(8, num, obj);
		}
		if ((bool)PayManager.action)
		{
			PayManager.action.AddSkill(5, num, "QIANDAO");
		}
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_reward", NowPlay: true);
		}
	}

	public void addReward9(int num, GameObject obj, bool isShow = true)
	{
		if (isShow)
		{
			BaseUIAnimation.action.ShowProp(9, num, obj);
		}
		if ((bool)PayManager.action)
		{
			PayManager.action.AddSkill(6, num, "QIANDAO");
		}
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_reward", NowPlay: true);
		}
	}

	public void addReward10(int num, GameObject obj, bool isShow = true)
	{
		if (isShow)
		{
			BaseUIAnimation.action.ShowProp(10, num, obj);
		}
		Singleton<UserManager>.Instance.AddLoveInfinite(num);
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_reward", NowPlay: true);
		}
	}

	public void addReward11(int num, GameObject obj, bool isShow = true)
	{
		if (isShow)
		{
			BaseUIAnimation.action.ShowProp(11, num, obj);
		}
		Singleton<UserManager>.Instance.AddLoveInfinite(0.5f * (float)num);
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_reward", NowPlay: true);
		}
	}

	public void addReward12(int num, GameObject obj, bool isShow = true)
	{
		if (isShow)
		{
			BaseUIAnimation.action.ShowProp(12, num, obj);
		}
		Singleton<UserManager>.Instance.AddLoveInfinite(2 * num);
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_reward", NowPlay: true);
		}
	}

	public void addReward13(int num, GameObject obj, bool isShow = true)
	{
		if (isShow)
		{
			BaseUIAnimation.action.ShowProp(13, num, obj);
		}
		if ((bool)PayManager.action)
		{
			PayManager.action.Addlvye(num);
		}
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_reward", NowPlay: true);
		}
	}

	public void addReward14(int num, GameObject obj, bool isShow = true)
	{
		if (isShow)
		{
			BaseUIAnimation.action.ShowProp(14, num, obj);
		}
		int num2 = int.Parse(Singleton<DataManager>.Instance.dDataHua4["2"]["iConfigData"]);
		HuaGame.action.AddShui(num * num2);
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_reward", NowPlay: true);
		}
	}

	public void addReward15(int num, GameObject obj, bool isShow = true)
	{
		if (isShow)
		{
			BaseUIAnimation.action.ShowProp(15, num, obj);
		}
		int num2 = int.Parse(Singleton<DataManager>.Instance.dDataHua4["3"]["iConfigData"]);
		HuaGame.action.AddFei(num * num2);
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_reward", NowPlay: true);
		}
	}

	public void addReward16(int num, GameObject obj, bool isShow = true)
	{
		if (isShow)
		{
			BaseUIAnimation.action.ShowProp(16, num, obj);
		}
		Singleton<UserManager>.Instance.AddFeiliaoDouble(num);
		if ((bool)HuaGame.action)
		{
			HuaGame.action.InitHua();
		}
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_reward", NowPlay: true);
		}
	}
}
