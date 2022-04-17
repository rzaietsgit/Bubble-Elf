using EasyMobile;

public class SaleAd2UIPanel : SaleAd2UIPanelBase
{
	public static SaleAd2UIPanel panel;

	public override void InitUI()
	{
		panel = this;
		if (DataManager.sale_adKey == "First_Pay")
		{
			detail.AD4_Image.gameObject.SetActive(value: true);
		}
		if (DataManager.sale_adKey == "Live_Pack")
		{
			detail.AD1_Image.gameObject.SetActive(value: true);
		}
		if (DataManager.sale_adKey == "Any_Way_Pack_1")
		{
			detail.AD3_Image.gameObject.SetActive(value: true);
		}
		if (DataManager.sale_adKey == "Any_Way_Pack_2")
		{
			detail.AD33_Image.gameObject.SetActive(value: true);
		}
		if (DataManager.sale_adKey == "Any_Way_Pack_3")
		{
			detail.AD333_Image.gameObject.SetActive(value: true);
		}
		//InitAndroid.action.GAEvent("showlb" + DataManager.sale_adKey);
		//InitAndroid.action.GAEvent("Newshowlb:" + DataManager.sale_adKey + ":" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
		InitAndroid.action.checkShowAdTip();
	}

	public override void OnPayBtn()
	{
		//InitAndroid.action.doChainePay(DataManager.sale_adKey);
        if (DataManager.sale_adKey == "First_Pay")
        {
            IAPManager.Purchase(EM_IAPConstants.Product_first_pay);
        }
        if (DataManager.sale_adKey == "Live_Pack")
        {
            IAPManager.Purchase(EM_IAPConstants.Product_live_pack);
        }
        if (DataManager.sale_adKey == "Any_Way_Pack_1")
        {
            IAPManager.Purchase(EM_IAPConstants.Product_any_way_pack_1);
        }
        if (DataManager.sale_adKey == "Any_Way_Pack_2")
        {
            IAPManager.Purchase(EM_IAPConstants.Product_any_way_pack_2);
        }
        if (DataManager.sale_adKey == "Any_Way_Pack_3")
        {
            IAPManager.Purchase(EM_IAPConstants.Product_any_way_pack_3);
        }
    }

	public void ShowAdTip()
	{
		detail.adfree_Image.gameObject.SetActive(value: true);
	}
}
