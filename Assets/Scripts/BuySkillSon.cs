using EasyMobile;
using UnityEngine;
using UnityEngine.UI;

public class BuySkillSon : MonoBehaviour
{
	private int iSkillType = 1;

	public Sprite[] LSkillSprite;

	public GameObject IconObj;

	public Text ViewCountText;

	public Text ViewPayMoneyText;

	public GameObject PayBtn;

	public GameObject GoldIcon;

	public Sprite GoldIconSprite;

	private int iNunb;

	private int iMon;

	public void SetNumber(int iNumber)
	{
		iNunb = iNumber;
		ViewCountText.text = "+" + iNumber;
	}

	public void SetMoney(int iMoney)
	{
		iMon = iMoney;
		ViewPayMoneyText.text = iMoney.ToString();
	}

	public void SetType(int iType)
	{
		iSkillType = iType;
		IconObj.GetComponent<Image>().sprite = LSkillSprite[iSkillType];
	}

	public void ClickPaySkill()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		int num = int.Parse(Singleton<DataManager>.Instance.dDataSkillPrice[iSkillType.ToString()]["buyType"]);
		if (InitGame.bChinaVersion)
		{
			num = int.Parse(Singleton<DataManager>.Instance.dDataSkillPriceChina[iSkillType.ToString()]["buyType"]);
		}
		switch (num)
		{
		case 1:
		{
			int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
			if (iMon > int2)
			{
				Singleton<DataManager>.Instance.ChinaShopOpenZuanshi = true;
				Singleton<DataManager>.Instance.ChinaShopOpendaoju = false;
				UI.Instance.OpenPanel(UIPanelType.ChinaShop);
			}
			else
			{
				aliyunlog.GameUseLog("diamond", iMon, "buyskilltype" + iSkillType, iNunb);
				PayManager.action.BuySkill(iSkillType, iMon, 0, iNunb);
				BuySkillUIPanel.panel.OnCloseButton();
			}
			break;
		}
		case 2:
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GB");
			if (iMon > @int)
			{
				EnumUIType[] array = new EnumUIType[2];
				Singleton<DataManager>.Instance.ChinaShopOpendaoju = true;
				Singleton<DataManager>.Instance.ChinaShopOpenZuanshi = false;
				Singleton<DataManager>.Instance.bChinaShopUIopen1 = true;
				UI.Instance.OpenPanel(UIPanelType.ChinaShop);
			}
			else
			{
				aliyunlog.GameUseLog("gold", iMon, "buyskilltype" + iSkillType, iNunb);
				Singleton<DataManager>.Instance.bOpenplay1 = true;
				PayManager.action.BuySkill(iSkillType, 0, iMon, iNunb);
				BuySkillUIPanel.panel.OnCloseButton();
			}
			break;
		}
		default:
			//InitAndroid.action.doChainePay("GameSkill" + iSkillType);
                if(iSkillType == 1)
                {
                    IAPManager.Purchase(EM_IAPConstants.Product_gameskill1);
                }
                else if (iSkillType == 2)
                {
                    IAPManager.Purchase(EM_IAPConstants.Product_gameskill2);
                }
                else if (iSkillType == 3)
                {
                    IAPManager.Purchase(EM_IAPConstants.Product_gameskill3);
                }
                else if (iSkillType == 4)
                {
                    IAPManager.Purchase(EM_IAPConstants.Product_gameskill4);
                }
                else if (iSkillType == 5)
                {
                    IAPManager.Purchase(EM_IAPConstants.Product_gameskill5);
                }
                else if (iSkillType == 6)
                {
                    IAPManager.Purchase(EM_IAPConstants.Product_gameskill6);
                }
                break;
		}
	}

	private void Start()
	{
		BaseUIAnimation.action.CreateButton(PayBtn.gameObject);
		if (InitGame.bChinaVersion)
		{
			int num = int.Parse(Singleton<DataManager>.Instance.dDataSkillPriceChina[iSkillType.ToString()]["buyType"]);
			if (num == 2)
			{
				GoldIcon.GetComponent<Image>().sprite = GoldIconSprite;
			}
		}
	}

	private void Update()
	{
	}
}
