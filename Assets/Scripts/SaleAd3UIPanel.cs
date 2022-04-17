using System;
using System.Collections;
using EasyMobile;
using UnityEngine;
using UnityEngine.UI;

public class SaleAd3UIPanel : SaleAd3UIPanelBase
{
	public static SaleAd3UIPanel panel;

	public Sprite[] ListLove;

	public int iType = 1;

	public string KeyStr = string.Empty;

	public int iKey = 1;

	public string smoney = "6";

	private int iCount = 3;

	public override void InitUI()
	{
		panel = this;
		InitSalad();
	}

	public void InitSalad()
	{
		string nowTime_Day = Util.GetNowTime_Day();
		int num = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowDaySaleAd3" + nowTime_Day);
		UnityEngine.Debug.Log("iNowDaySaleAd3=" + num);
		if (num == 0 || num == 101)
		{
			int num2 = UnityEngine.Random.Range(1, 7);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_iNowDaySaleAd3" + nowTime_Day, num2);
			num = num2;
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_iNowDaySaleAd3Res" + nowTime_Day, 0);
		}
		//InitAndroid.action.GAEvent("showlbchuangguan" + num);
		//InitAndroid.action.GAEvent("Newshowlbchuangguan:" + num + ":" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
		SetPayKey(num);
		setSkillKey();
		InitSkillShow();
		int add1Time = Util.getAdd1Time();
		detail.ADTime_Text.text = "00:00:00";
		TimeSpan timeSpan = new TimeSpan(0, 0, add1Time);
		int minutes = timeSpan.Minutes;
		int num3 = timeSpan.Hours;
		int seconds = timeSpan.Seconds;
		int days = timeSpan.Days;
		if (days > 0)
		{
			num3 = days * 24 + num3;
		}
		string text = minutes + string.Empty;
		string text2 = num3 + string.Empty;
		string text3 = seconds + string.Empty;
		if (minutes < 10)
		{
			text = "0" + text;
		}
		if (num3 < 10)
		{
			text2 = "0" + text2;
		}
		if (seconds < 10)
		{
			text3 = "0" + text3;
		}
		detail.ADTime_Text.text = text2 + ":" + text + ":" + text3;
		StartCoroutine(IEtime());
	}

	private IEnumerator IEtime()
	{
		yield return new WaitForSeconds(0.1f);
		while (true)
		{
			yield return new WaitForSeconds(1f);
			int _iLoveTime = Util.getAdd1Time();
			detail.ADTime_Text.text = "00:00:00";
			TimeSpan ts = new TimeSpan(0, 0, _iLoveTime);
			int iF_ = ts.Minutes;
			int iH_ = ts.Hours;
			int iM_ = ts.Seconds;
			int iD_ = ts.Days;
			if (iD_ > 0)
			{
				iH_ = iD_ * 24 + iH_;
			}
			string sF = iF_ + string.Empty;
			string sH = iH_ + string.Empty;
			string sM = iM_ + string.Empty;
			if (iF_ < 10)
			{
				sF = "0" + sF;
			}
			if (iH_ < 10)
			{
				sH = "0" + sH;
			}
			if (iM_ < 10)
			{
				sM = "0" + sM;
			}
			detail.ADTime_Text.text = sH + ":" + sF + ":" + sM;
		}
	}

	public void InitSkillShow()
	{
		UnityEngine.Debug.Log("KeyStr=" + KeyStr);
		detail.ADMoney_Text.text = smoney.ToString() + " 元";
		int num = int.Parse(Singleton<DataManager>.Instance.dDataChinaPay[KeyStr]["igold"]);
		detail.AD3T1_Text.text = "x" + num;
		for (int i = 1; i <= iCount; i++)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SaleAd3_ID" + i);
			int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SaleAd3_Count" + i);
			if (@int != 0)
			{
				if (i == 1)
				{
					detail.AD3T2_Text.text = "x" + int2;
					SetImage(@int, detail.AD3I2_Image);
				}
				if (i == 2)
				{
					detail.AD3T3_Text.text = "x" + int2;
					SetImage(@int, detail.AD3I3_Image);
				}
				else
				{
					detail.AD3T4_Text.text = "x" + int2;
					SetImage(@int, detail.AD3I4_Image);
				}
			}
		}
		if (iCount < 2)
		{
			detail.AD3T3_Text.gameObject.SetActive(value: false);
			detail.AD3I3_Image.gameObject.SetActive(value: false);
			detail.Image3_Image.gameObject.SetActive(value: false);
		}
		if (iCount < 3)
		{
			detail.AD3T4_Text.gameObject.SetActive(value: false);
			detail.AD3I4_Image.gameObject.SetActive(value: false);
			detail.Image4_Image.gameObject.SetActive(value: false);
		}
		if (iCount == 1)
		{
			detail.Image1_Image.gameObject.transform.localPosition = new Vector3(-107f, -9f, 0f);
			detail.Image2_Image.gameObject.transform.localPosition = new Vector3(135f, -9f, 0f);
			detail.AD3T1_Text.gameObject.transform.localPosition = new Vector3(-100f, -142f, 0f);
			detail.AD3T2_Text.gameObject.transform.localPosition = new Vector3(142f, -142f, 0f);
		}
		if (iCount == 2)
		{
			detail.Image1_Image.gameObject.transform.localPosition = new Vector3(-180f, -9f, 0f);
			detail.Image2_Image.gameObject.transform.localPosition = new Vector3(0f, -9f, 0f);
			detail.Image3_Image.gameObject.transform.localPosition = new Vector3(180f, -9f, 0f);
			detail.AD3T1_Text.gameObject.transform.localPosition = new Vector3(-180f, -142f, 0f);
			detail.AD3T2_Text.gameObject.transform.localPosition = new Vector3(0f, -142f, 0f);
			detail.AD3T3_Text.gameObject.transform.localPosition = new Vector3(180f, -142f, 0f);
		}
	}

	public void SetImage(int index, Image Img)
	{
		Img.sprite = ListLove[index - 1];
	}

	public void setSkillKey()
	{
		iType = 3;
		if (iKey == 1)
		{
			iType = 1;
			iCount = 1;
		}
		if (iKey == 2)
		{
			iType = 2;
			iCount = 2;
		}
		string nowTime_Day = Util.GetNowTime_Day();
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowDaySaleAd3Res" + nowTime_Day) == 0)
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_iNowDaySaleAd3Res" + nowTime_Day, 1);
			RSkill(iCount);
		}
	}

	public void RSkill(int index)
	{
		for (int i = 1; i <= 3; i++)
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_SaleAd3_ID" + i, 0);
		}
		for (int j = 1; j <= index; j++)
		{
			int num = UnityEngine.Random.Range(1, 10);
			while (FindOld(index, num))
			{
				num = UnityEngine.Random.Range(1, 10);
			}
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_SaleAd3_ID" + j, num);
			int value = 1;
			if (iKey == 2)
			{
				value = 2;
			}
			if (iKey == 3)
			{
				value = 3;
			}
			if (iKey == 4)
			{
				value = 2;
			}
			if (iKey == 5)
			{
				value = 4;
			}
			if (iKey == 6)
			{
				value = 10;
			}
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_SaleAd3_Count" + j, value);
		}
	}

	public bool FindOld(int index, int iv)
	{
		for (int i = 1; i <= index; i++)
		{
			if (iv == Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SaleAd3_ID" + i))
			{
				return true;
			}
		}
		return false;
	}

	public void SetPayKey(int index)
	{
		bool flag = true;
		UnityEngine.Debug.Log("SetPayKey index=" + index);
		iKey = index;
		KeyStr = "Pack_Pass_";
		string str = "6";
		if (index == 2)
		{
			str = "12";
		}
		if (index == 3)
		{
			str = "30";
		}
		if (index == 4)
		{
			str = "68";
		}
		if (index == 5)
		{
			str = "128";
		}
		if (index == 6)
		{
			str = "328";
		}
		string empty = string.Empty;
		if (Singleton<DataManager>.Instance.GetUserDataI("PAYPack_Pass_Double_" + str) == 0)
		{
			flag = false;
			UnityEngine.Debug.Log("_Pass_Double_");
			KeyStr = "Pack_Pass_Double_";
		}
		if (index == 1)
		{
			smoney = "6";
			KeyStr += "6";
		}
		if (index == 2)
		{
			smoney = "1 2";
			KeyStr += "12";
		}
		if (index == 3)
		{
			smoney = "3 0";
			KeyStr += "30";
		}
		if (index == 4)
		{
			smoney = "6 8";
			KeyStr += "68";
		}
		if (index == 5)
		{
			smoney = "1 2 8";
			KeyStr += "128";
		}
		if (index == 6)
		{
			smoney = "3 2 8";
			KeyStr += "328";
		}
	}

	public override void OnPayBtn()
	{
		//InitAndroid.action.doChainePay(KeyStr);
        if (KeyStr == "Pack_Pass_12")
        {
            IAPManager.Purchase(EM_IAPConstants.Product_pack_pass_12);
        }
        if (KeyStr == "Pack_Pass_30")
        {
            IAPManager.Purchase(EM_IAPConstants.Product_pack_pass_30);
        }
        if (KeyStr == "Pack_Pass_68")
        {
            IAPManager.Purchase(EM_IAPConstants.Product_pack_pass_68);
        }
        if (KeyStr == "Pack_Pass_128")
        {
            IAPManager.Purchase(EM_IAPConstants.Product_pack_pass_128);
        }
        if (KeyStr == "Pack_Pass_328")
        {
            IAPManager.Purchase(EM_IAPConstants.Product_pack_pass_328);
        }
        if (KeyStr == "Pack_Pass_Double_6")
        {
            IAPManager.Purchase(EM_IAPConstants.Product_pack_pass_328);
        }
        if (KeyStr == "Pack_Pass_Double_12")
        {
            IAPManager.Purchase(EM_IAPConstants.Product_pack_pass_328);
        }
        if (KeyStr == "Pack_Pass_Double_30")
        {
            IAPManager.Purchase(EM_IAPConstants.Product_pack_pass_328);
        }
        if (KeyStr == "Pack_Pass_Double_68")
        {
            IAPManager.Purchase(EM_IAPConstants.Product_pack_pass_328);
        }
        if (KeyStr == "Pack_Pass_Double_128")
        {
            IAPManager.Purchase(EM_IAPConstants.Product_pack_pass_328);
        }
        if (KeyStr == "Pack_Pass_Double_328")
        {
            IAPManager.Purchase(EM_IAPConstants.Product_pack_pass_328);
        }
    }
}
