using UnityEngine;
using UnityEngine.UI;

public class BuyHuaObj : MonoBehaviour
{
	public GameObject quehuo;

	public Text MoneyText;

	public Image IconImg;

	public Text iNumberText;

	public Text NameText;

	private int iNumber = 1;

	private int iprice = 50;

	public Text xiangou;

	public Text CountText;

	public Sprite ZsSp;

	public Image ImgBuy;

	private int iLimit;

	private string huaID = string.Empty;

	private int iSkillID;

	private int iNum = 1;

	public int ReturnNumber()
	{
		if (iLimit > 0)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "HuaID_" + iSkillID + "_" + huaID);
			return iLimit - @int;
		}
		return 200;
	}

	public void ClickBuy()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_HuaPayLastTime");
		int nowTime = Util.GetNowTime();
		if (@int > 0)
		{
			int num = nowTime - @int;
			if (num < 3)
			{
				return;
			}
		}
		if (Singleton<UserManager>.Instance.bOpenHua() <= 0)
		{
			Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
		}
		else
		{
			if (ReturnNumber() < iNumber)
			{
				return;
			}
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_HuaPayLastTime", nowTime);
			int num2 = iprice * iNumber;
			if (iSkillID == 16)
			{
				int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_GOLD");
				if (num2 > int2)
				{
					return;
				}
				int2 -= num2;
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_GOLD", int2);
				FirebaseController.AddOrSubStone(-num2);
			}
			else
			{
				if (num2 > Singleton<UserManager>.Instance.GetHuaBi())
				{
					return;
				}
				Singleton<UserManager>.Instance.AddHuaBi(num2 * -1);
			}
			UnityEngine.Debug.Log("iNumber=" + iNumber);
			Reward(iNumber * iNum);
			int int3 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "HuaID_" + iSkillID + "_" + huaID);
			int3 += iNumber;
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "HuaID_" + iSkillID + "_" + huaID, int3);
			if (ReturnNumber() <= 0)
			{
				quehuo.SetActive(value: true);
				xiangou.gameObject.SetActive(value: false);
			}
			xiangou.text = "限购" + ReturnNumber() + "次";
		}
	}

	private void Reward(int number)
	{
		GameObject gameObject = HuaShopUI.action.gameObject;
		ChinaPay.action.addRewardAll(iSkillID, number, gameObject);
		UnityEngine.Debug.Log("iSkillID=" + iSkillID);
	}

	public void AddNumber()
	{
		if (iNumber < 100 && ReturnNumber() >= iNumber + 1)
		{
			iNumber++;
			iNumberText.text = iNumber + string.Empty;
			MoneyText.text = iprice * iNumber + string.Empty;
		}
	}

	public void CutNumber()
	{
		if (iNumber > 1)
		{
			iNumber--;
			iNumberText.text = iNumber + string.Empty;
			MoneyText.text = iprice * iNumber + string.Empty;
		}
	}

	private void Start()
	{
		iNumberText.text = "1";
		huaID = Singleton<UserManager>.Instance.getHuaBuyID();
	}

	public void InitData(int index)
	{
		if (huaID == string.Empty)
		{
			huaID = Singleton<UserManager>.Instance.getHuaBuyID();
		}
		NameText.text = Singleton<DataManager>.Instance.dDataHua3[index.ToString()]["name"];
		iprice = int.Parse(Singleton<DataManager>.Instance.dDataHua3[index.ToString()]["money"]);
		iNum = int.Parse(Singleton<DataManager>.Instance.dDataHua3[index.ToString()]["iNum"]);
		iLimit = int.Parse(Singleton<DataManager>.Instance.dDataHua3[index.ToString()]["ilimited"]);
		CountText.text = "+" + iNum;
		MoneyText.text = iprice + string.Empty;
		iSkillID = int.Parse(Singleton<DataManager>.Instance.dDataHua3[index.ToString()]["img"]);
		IconImg.sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + iSkillID, 138, 114);
		quehuo.SetActive(value: false);
		if (iSkillID == 16)
		{
			ImgBuy.sprite = ZsSp;
		}
		if (iLimit > 0)
		{
			xiangou.text = "限购" + ReturnNumber() + "次";
			xiangou.gameObject.SetActive(value: true);
		}
		else
		{
			xiangou.gameObject.SetActive(value: false);
		}
		if (ReturnNumber() <= 0)
		{
			quehuo.SetActive(value: true);
		}
	}

	public void ShowQuehuo()
	{
		quehuo.SetActive(value: true);
	}

	private void Update()
	{
	}
}
