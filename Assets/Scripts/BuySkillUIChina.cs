using System.Collections;
using EasyMobile;
using UnityEngine;
using UnityEngine.UI;

public class BuySkillUIChina : BaseUI
{
	public static BuySkillUIChina action;

	public GameObject CloseBtn;

	public GameObject SkillIcon;

	public Text MoneyText;

	public Text Btntext;

	public Text BuySkillRemark;

	public Sprite[] LSkillIcon;

	public Text BuySkillUIChina1;

	private int iSkillType;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.BuySkillUIChina;
	}

	private void Start()
	{
		action = this;
		iSkillType = DataManager.iSkillOpenType;
		if ((bool)GameUI.action)
		{
			if (iSkillType == 1)
			{
				iSkillType = 4;
			}
			else if (iSkillType == 2)
			{
				iSkillType = 5;
			}
			else if (iSkillType == 3)
			{
				iSkillType = 6;
			}
		}
		float num = float.Parse(Singleton<DataManager>.Instance.dDataChinaPay["GameSkill" + iSkillType]["iMoney"]);
		if (Singleton<DataManager>.Instance.bChinaIos)
		{
			num = float.Parse(Singleton<DataManager>.Instance.dDataChinaPay["GameSkill" + iSkillType]["iMoneyios"]);
			if (InitGame.bEnios)
			{
				num = float.Parse(Singleton<DataManager>.Instance.dDataChinaPay["GameSkill" + iSkillType]["iMoneyiosen"]);
			}
		}
		BaseUIAnimation.action.SetLanguageFont("BuySkillRemark" + iSkillType, BuySkillRemark, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("BuySkillUIChina1", BuySkillUIChina1, string.Empty);
		SkillIcon.GetComponent<Image>().sprite = LSkillIcon[iSkillType - 4];
		string text = Singleton<DataManager>.Instance.dDataLanguage["BuySkillUIChina2"][BaseUIAnimation.Language];
		text = text.Replace("A1", num.ToString());
		MoneyText.text = text;
		Btntext.text = "立 即 购 买";
		InitAndroid.action.checkshowBuyButton();
		if (InitGame.bEnios)
		{
			BaseUIAnimation.action.SetLanguageFont("BuySkillUIChina3", Btntext, string.Empty);
		}
	}

	public void ChangeText()
	{
		Btntext.text = "立 即 使 用";
	}

	public void _CloseBuySkillUIChina()
	{
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
			StartCoroutine(CallCloseUI());
		}
	}

	private IEnumerator CallCloseUI(bool bDouble = false, bool bExit = true)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseUI(bDouble);
	}

	private void Update()
	{
	}

	public void ClickPaySkillBtn()
	{
		//InitAndroid.action.doChainePay("GameSkill" + iSkillType);
        if (iSkillType == 1)
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
    }
}
