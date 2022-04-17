using EasyMobile;
using UnityEngine;
using UnityEngine.UI;

public class ShopLibao : MonoBehaviour
{
	public int iLibaoIndex = 1;

	public Sprite[] SpArr;

	public Sprite[] LbSp;

	public Image Icon;

	public Text MoneyText;

	public GameObject father;

	private void Start()
	{
	}

	public void SetIndex(int iIndex)
	{
		iLibaoIndex = iIndex;
		base.transform.Find("icon").GetComponent<Image>().sprite = SpArr[iIndex - 1];
		base.transform.Find("left").GetComponent<Image>().sprite = LbSp[iIndex - 1];
		Debug.LogError(Singleton<DataManager>.Instance.dDataChinaPay["Bubble_LB" + iIndex]["iMoneyiosen"]);
		string num = Singleton<DataManager>.Instance.dDataChinaPay["Bubble_LB" + iIndex]["iMoneyiosen"];
		string text = Singleton<DataManager>.Instance.dDataLanguage["ChinaShopUI4"][BaseUIAnimation.Language];
		text = text.Replace("A1", num.ToString());
		MoneyText.text = text;
		if (Singleton<DataManager>.Instance.CommodityPricesDic != null)
		{
			string key = Singleton<DataManager>.Instance.dDataChinaPay["Bubble_LB" + iIndex]["googlekey"];
			string text2 = Singleton<DataManager>.Instance.CommodityPricesDic[key];
			MoneyText.text = text2;
		}
		string text3 = Singleton<DataManager>.Instance.dDatashoplb[iIndex.ToString()]["sdata"];
		if (text3.Split('F').Length == 3)
		{
			GridLayoutGroup component = father.gameObject.GetComponent<GridLayoutGroup>();
			component.spacing = new Vector2(50f, 0f);
			for (int i = 0; i <= 2; i++)
			{
				string text4 = text3.Split('F')[i];
				int num2 = int.Parse(text4.Split('|')[0]);
				int num3 = int.Parse(text4.Split('|')[1]);
				GameObject gameObject = UnityEngine.Object.Instantiate(Icon.gameObject);
				gameObject.transform.SetParent(father.transform, worldPositionStays: false);
				gameObject.SetActive(value: true);
				gameObject.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num2, 138, 114);
				gameObject.transform.Find("Text").gameObject.GetComponent<Text>().text = "X" + num3;
			}
		}
		else
		{
			GridLayoutGroup component2 = father.gameObject.GetComponent<GridLayoutGroup>();
			component2.spacing = new Vector2(0f, 0f);
			for (int j = 0; j <= 3; j++)
			{
				string text5 = text3.Split('F')[j];
				int num4 = int.Parse(text5.Split('|')[0]);
				int num5 = int.Parse(text5.Split('|')[1]);
				GameObject gameObject2 = UnityEngine.Object.Instantiate(Icon.gameObject);
				gameObject2.transform.SetParent(father.transform, worldPositionStays: false);
				gameObject2.SetActive(value: true);
				gameObject2.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num4, 138, 114);
				gameObject2.transform.Find("Text").gameObject.GetComponent<Text>().text = "X" + num5;
			}
		}
	}

	private void Update()
	{
	}

	public void ClickLb()
	{
		//InitAndroid.action.doChainePay("Bubble_LB" + iLibaoIndex);
       
        switch (iLibaoIndex)
        {
            case 1:
                IAPManager.Purchase(EM_IAPConstants.Product_bubble_lb1);
                break;
            case 2:
                IAPManager.Purchase(EM_IAPConstants.Product_bubble_lb2);
                break;
            case 3:
                IAPManager.Purchase(EM_IAPConstants.Product_bubble_lb3);
                break;
        }
    }
}
