using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AdAwardUI : BaseUI
{
	public static AdAwardUI action;

	public GameObject EnterBtn;

	public GameObject CloseBtn;

	public GameObject IconObj;

	public Sprite[] LSkillSprite;

	public Text AdAwardRemark;

	public Text TCountText;

	public Text AdAwardTitle;

	public Text AdAwardUIOK;

	private int AwardID = 1;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.AdAwardUI;
	}

	public override void OnStart()
	{
		action = this;
		BaseUIAnimation.action.CreateButton(EnterBtn.gameObject);
		BaseUIAnimation.action.SetLanguageFont("AdAwardTitle", AdAwardTitle, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("AdAwardUIOK", AdAwardUIOK, string.Empty);
	}

	public void SetType(int _iAwardID, int iCount = 1)
	{
		switch (_iAwardID)
		{
		case 5:
			AdAwardRemark.gameObject.SetActive(value: false);
			TCountText.gameObject.SetActive(value: false);
			TCountText.text = "x" + iCount;
			break;
		case 6:
			AdAwardRemark.gameObject.SetActive(value: false);
			break;
		default:
			BaseUIAnimation.action.SetLanguageFont("AdAwardRemark" + _iAwardID, AdAwardRemark, string.Empty);
			break;
		}
		IconObj.GetComponent<Image>().sprite = LSkillSprite[_iAwardID - 1];
	}

	public void CloseAdAwardUI(bool bDouble = false)
	{
		StartCoroutine(CallCloseUI(bDouble));
	}

	public void _CloseAdAwardUI()
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
		if (Input.GetMouseButtonDown(0))
		{
			GameObject gameObject = Util.TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject == null)
			{
				CloseUI();
			}
			else if (gameObject.name.LastIndexOf("AdAwardUI") < 0)
			{
				CloseUI();
			}
		}
	}

	protected override void OnAwake()
	{
		base.OnAwake();
	}

	protected override void OnRelease()
	{
		base.OnRelease();
	}

	public void ClickEnterBtn()
	{
		CloseUI();
	}
}
