using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class cdKeyRewardUI : BaseUI
{
	public GameObject CloseBtn;

	public Text OkBtn;

	public Sprite[] LSpriteIcon;

	public GameObject showiconobj;

	public GameObject[] LIcon;

	public Text[] LNumber;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.cdKeyRewardUI;
	}

	private void Start()
	{
		BaseUIAnimation.action.SetLanguageFont("CdkeyRewardOKbtn", OkBtn, string.Empty);
		if (Singleton<DataManager>.Instance.cdkeys_key == 1)
		{
			SetIconAndNumber(LIcon[0].gameObject, LNumber[0].gameObject, 3, 188);
			SetIconAndNumber(LIcon[1].gameObject, LNumber[1].gameObject, 4, 1);
			SetIconAndNumber(LIcon[2].gameObject, LNumber[2].gameObject, 6, 1);
			SetIconAndNumber(LIcon[3].gameObject, LNumber[3].gameObject, 2, 1888);
		}
		else if (Singleton<DataManager>.Instance.cdkeys_key == 2)
		{
			SetIconAndNumber(LIcon[0].gameObject, LNumber[0].gameObject, 3, 88);
			SetIconAndNumber(LIcon[1].gameObject, LNumber[1].gameObject, 4, 1);
			SetIconAndNumber(LIcon[2].gameObject, LNumber[2].gameObject, 6, 1);
			SetIconAndNumber(LIcon[3].gameObject, LNumber[3].gameObject, 2, 888);
		}
		else if (Singleton<DataManager>.Instance.cdkeys_key == 3)
		{
			SetIconAndNumber(LIcon[0].gameObject, LNumber[0].gameObject, 3, 38);
			SetIconAndNumber(LIcon[1].gameObject, LNumber[1].gameObject, 4, 1);
			SetIconAndNumber(LIcon[2].gameObject, LNumber[2].gameObject, 6, 1);
			SetIconAndNumber(LIcon[3].gameObject, LNumber[3].gameObject, 2, 688);
		}
		else if (Singleton<DataManager>.Instance.cdkeys_key == 4)
		{
			SetIconAndNumber(LIcon[0].gameObject, LNumber[0].gameObject, 3, 156);
			SetIconAndNumber(LIcon[1].gameObject, LNumber[1].gameObject, 3, 0);
			SetIconAndNumber(LIcon[2].gameObject, LNumber[2].gameObject, 8, 0);
			SetIconAndNumber(LIcon[3].gameObject, LNumber[3].gameObject, 2, 0);
		}
		else if (Singleton<DataManager>.Instance.cdkeys_key == 5)
		{
			SetIconAndNumber(LIcon[0].gameObject, LNumber[0].gameObject, 3, 318);
			SetIconAndNumber(LIcon[1].gameObject, LNumber[1].gameObject, 3, 0);
			SetIconAndNumber(LIcon[2].gameObject, LNumber[2].gameObject, 8, 0);
			SetIconAndNumber(LIcon[3].gameObject, LNumber[3].gameObject, 2, 0);
		}
		else if (Singleton<DataManager>.Instance.cdkeys_key == 6)
		{
			SetIconAndNumber(LIcon[0].gameObject, LNumber[0].gameObject, 3, 858);
			SetIconAndNumber(LIcon[1].gameObject, LNumber[1].gameObject, 3, 0);
			SetIconAndNumber(LIcon[2].gameObject, LNumber[2].gameObject, 8, 0);
			SetIconAndNumber(LIcon[3].gameObject, LNumber[3].gameObject, 2, 0);
		}
		if (Singleton<DataManager>.Instance.cdkeys_key == 7)
		{
			SetIconAndNumber(LIcon[0].gameObject, LNumber[0].gameObject, 3, 2088);
			SetIconAndNumber(LIcon[1].gameObject, LNumber[1].gameObject, 3, 0);
			SetIconAndNumber(LIcon[2].gameObject, LNumber[2].gameObject, 8, 0);
			SetIconAndNumber(LIcon[3].gameObject, LNumber[3].gameObject, 2, 0);
		}
		if (Singleton<DataManager>.Instance.cdkeys_key == 8)
		{
			SetIconAndNumber(LIcon[0].gameObject, LNumber[0].gameObject, 3, 3988);
			SetIconAndNumber(LIcon[1].gameObject, LNumber[1].gameObject, 3, 0);
			SetIconAndNumber(LIcon[2].gameObject, LNumber[2].gameObject, 8, 0);
			SetIconAndNumber(LIcon[3].gameObject, LNumber[3].gameObject, 2, 0);
		}
		if (Singleton<DataManager>.Instance.cdkeys_key >= 4)
		{
			Transform transform = showiconobj.gameObject.transform;
			Vector3 localPosition = showiconobj.gameObject.transform.localPosition;
			float y = localPosition.y;
			Vector3 localPosition2 = showiconobj.gameObject.transform.localPosition;
			transform.localPosition = new Vector3(0f, y, localPosition2.z);
		}
		if ((bool)PayManager.action)
		{
			PayManager.action.LoadGold();
		}
	}

	private void SetIconAndNumber(GameObject obj1, GameObject obj2, int itype, int number)
	{
		if (number == 0)
		{
			obj1.gameObject.transform.parent.gameObject.SetActive(value: false);
			obj2.gameObject.transform.parent.gameObject.SetActive(value: false);
		}
		else
		{
			ChinaPay.action.addRewardAll(itype, number, base.gameObject, isShow: false);
			obj1.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + itype, 138, 114);
			obj2.GetComponent<Text>().text = "x" + number;
		}
	}

	public void _ClosecdKeyRewardUI()
	{
		if (BaseUIAnimation.bClickButton)
		{
			BaseUIAnimation.action.ClickButton(CloseBtn.gameObject);
			StartCoroutine(CallCloseUI());
		}
	}

	private IEnumerator CallCloseUI(bool bDouble = false)
	{
		yield return new WaitForSeconds(BaseUIAnimation.btnAnimationTime);
		CloseUI(bDouble);
	}

	private void Update()
	{
	}

	public void ClickOK()
	{
		StartCoroutine(CallCloseUI());
	}
}
