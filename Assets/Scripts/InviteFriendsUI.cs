using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InviteFriendsUI : BaseUI
{
	public static InviteFriendsUI action;

	public GameObject InviteBtn;

	public GameObject CloseBtn;

	public Text InviteFriendsUITitle;

	public Text InviteFriendsUIRemark;

	public Text InviteFriendsUINumber;

	public Text InviteFriendsUIBtn;

	public Text FriendSum;

	public GameObject groupFather;

	public GameObject InviteFriend;

	public GameObject ImageLine;

	public override EnumUIType GetUIType()
	{
		return EnumUIType.InviteFriendsUI;
	}

	public void CloseInviteFriendsUI(bool bDouble = false, bool bExit = true)
	{
		StartCoroutine(CallCloseUI(bDouble, bExit));
	}

	public void _CloseInviteFriendsUI()
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
		if ((bool)MapUI.action)
		{
			MapUI.action.LoadGold();
		}
		CloseUI(bDouble);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject gameObject = Util.TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject == null)
			{
				CloseLoseUI();
			}
			else if (gameObject.name.LastIndexOf("InviteFriendsUI") < 0)
			{
				CloseLoseUI();
			}
		}
	}

	public void CloseLoseUI(bool bDouble = false)
	{
		StartCoroutine(CallCloseUI(bDouble));
	}

	public override void OnStart()
	{
		action = this;
		BaseUIAnimation.action.CreateButton(InviteBtn.gameObject);
		BaseUIAnimation.action.SetLanguageFont("InviteFriendsUITitle", InviteFriendsUITitle, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("InviteFriendsUIRemark", InviteFriendsUIRemark, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("InviteFriendsUINumber", InviteFriendsUINumber, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("InviteFriendsUIBtn", InviteFriendsUIBtn, string.Empty);
		FriendSum.text = FaceBookApi.FacebookFriendOnline.ToString();
		for (int i = 0; i < 5; i++)
		{
			GameObject gameObject = Object.Instantiate(InviteFriend);
			gameObject.transform.SetParent(groupFather.transform, worldPositionStays: false);
			InviteFriend component = gameObject.GetComponent<InviteFriend>();
			component.SetID(i + 1);
		}
		InitLine();
	}

	public void InitLine()
	{
		int facebookFriendOnline = FaceBookApi.FacebookFriendOnline;
		float num = 0f;
		num = ((facebookFriendOnline >= 26) ? (0.92f + (float)(facebookFriendOnline - 26) * 0.03f) : ((facebookFriendOnline >= 17) ? (0.71f + (float)(facebookFriendOnline - 17) * 0.02333f) : ((facebookFriendOnline >= 10) ? (0.51f + (float)(facebookFriendOnline - 10) * 0.028f) : ((facebookFriendOnline >= 5) ? (0.32f + (float)(facebookFriendOnline - 5) * 0.038f) : ((facebookFriendOnline < 2) ? ((float)facebookFriendOnline * 0.07f) : (0.12f + (float)(facebookFriendOnline - 2) * 0.066f))))));
		ImageLine.GetComponent<Image>().fillAmount = num;
	}

	protected override void OnAwake()
	{
		base.OnAwake();
	}

	protected override void OnRelease()
	{
		base.OnRelease();
	}

	public void ClickInviteBtn()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		if (FaceBookApi.Action.bLoginState())
		{
			if (FaceBookApi.bWeb)
			{
				Application.ExternalCall("FaceBookInvitation");
			}
			else
			{
				FaceBookApi.Action.Invite();
			}
		}
		else
		{
			FaceBookApi.Action.FackBookLogin();
		}
	}
}
