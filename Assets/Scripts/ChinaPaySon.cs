using EasyMobile;
using UnityEngine;
using UnityEngine.UI;

public class ChinaPaySon : MonoBehaviour
{
	public RawImage PayIcon;

	public Image LeftImg;

	public Text Money1;

	public Text Money2;

	public Text Money3;

	public Text Money4;

	public GameObject zs;

	private string key = string.Empty;

	public Text BuyDoubleText;

	public void ShowAdFree()
	{
	}
    public int indexiap;
	public void InitPay(int index)
	{
		key = "Bubble_GOLD" + index;
		if (Singleton<DataManager>.Instance.GetUserDataI("PAY" + key) == 1)
		{
			key = "Bubble_GOLD" + index;
			LeftImg.gameObject.SetActive(value: false);
		}
        indexiap = index;

        int num = int.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["zhuanshi1"]);
		int num2 = int.Parse(Singleton<DataManager>.Instance.dDataChinaPay[key]["zhuanshi2"]);
		string num3 = Singleton<DataManager>.Instance.dDataChinaPay[key]["iMoney"];
		string text = Singleton<DataManager>.Instance.dDataChinaPay[key]["iLoveInfinite"];
		if (num2 > 0)
		{
			BuyDoubleText.text = "+" + num2 + "%";
			LeftImg.gameObject.SetActive(value: true);
		}
		else
		{
			LeftImg.gameObject.SetActive(value: false);
		}
		if (Singleton<DataManager>.Instance.bChinaIos)
		{
			num3 = Singleton<DataManager>.Instance.dDataChinaPay[key]["iMoneyios"];
			if (InitGame.bEnios)
			{
				num3 = Singleton<DataManager>.Instance.dDataChinaPay[key]["iMoneyiosen"];
			}
		}
		Money1.text = num.ToString();
		Money2.text = num2.ToString();
		string text2 = Singleton<DataManager>.Instance.dDataLanguage["ChinaShopUI4"][BaseUIAnimation.Language];
		text2 = text2.Replace("A1", num3.ToString());
		Money3.text = text2;
		UnityEngine.Debug.Log("jy  DataManager.Instance.CommodityPricesDic=" + Singleton<DataManager>.Instance.CommodityPricesDic);
		if (Singleton<DataManager>.Instance.CommodityPricesDic != null)
		{
			string text3 = Singleton<DataManager>.Instance.dDataChinaPay[key]["googlekey"];
			string text4 = Singleton<DataManager>.Instance.CommodityPricesDic[text3];
			Money3.text = text4;
			UnityEngine.Debug.Log("jy mText=" + text4);
		}
		if (text == "0")
		{
			Money4.gameObject.SetActive(value: false);
			zs.transform.localPosition = new Vector3(-38f, 0f, 0f);
		}
		else
		{
			Money4.text = text;
		}
		if(PayIcon != null)
		{
			PayIcon.texture = (Texture2D) Resources.Load("Img/payiconcn/buygem_gem_" + index, typeof(Texture2D));
			PayIcon.SetNativeSize();
		}
	}

	private void Start()
	{
		if (!InitGame.bEnios)
		{
		}
	}

	private void Update()
	{
	}

	public void ClickPay()
	{
		//InitAndroid.action.doChainePay(key);
        switch (indexiap)
        {
            case 1:
                IAPManager.Purchase(EM_IAPConstants.Product_bubble_gold1);
                break;
            case 2:
                IAPManager.Purchase(EM_IAPConstants.Product_bubble_gold2);
                break;
            case 3:
                IAPManager.Purchase(EM_IAPConstants.Product_bubble_gold3);
                break;
            case 4:
                IAPManager.Purchase(EM_IAPConstants.Product_bubble_gold4);
                break;
            case 5:
                IAPManager.Purchase(EM_IAPConstants.Product_bubble_gold5);
                break;
        }
    }
}
