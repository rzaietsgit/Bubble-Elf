using UnityEngine;
using UnityEngine.UI;

public class InviteFriend : MonoBehaviour
{
	public Text FriendNumber;

	public Text AwardCount;

	public Sprite GoldImg;

	public Sprite BgImg;

	public GameObject GoldObj;

	public GameObject BGObj;

	private int iAwareGoldCount;

	private int _AwardCount;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetID(int index)
	{
		switch (index)
		{
		case 1:
			SetFriendNumber(2);
			SetAwardCount(10);
			break;
		case 2:
			SetFriendNumber(5);
			SetAwardCount(20);
			break;
		case 3:
			SetFriendNumber(10);
			SetAwardCount(30);
			break;
		case 4:
			SetFriendNumber(17);
			SetAwardCount(50);
			break;
		case 5:
			SetFriendNumber(26);
			SetAwardCount(80);
			break;
		}
	}

	public void SetAwardCount(int iCount)
	{
		AwardCount.text = iCount.ToString();
		iAwareGoldCount = iCount;
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_InviteFriend_" + FaceBookApi.Action.UserId + "_" + iAwareGoldCount);
		if (@int == 1)
		{
			CloseText();
		}
	}

	public void SetFriendNumber(int iCount)
	{
		_AwardCount = iCount;
		FriendNumber.text = iCount.ToString();
	}

	public void ClickAward()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ButtonClick");
		}
		if (FaceBookApi.FacebookFriendOnline >= _AwardCount && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_InviteFriend_" + FaceBookApi.Action.UserId + "_" + iAwareGoldCount) == 0)
		{
			Singleton<DataManager>.Instance.SaveUserDate("DB_InviteFriend_" + FaceBookApi.Action.UserId + "_" + iAwareGoldCount, 1);
			PayManager.action.AwardAddGold(iAwareGoldCount, "FACEBOOKFRIEND");
			CloseText();
		}
	}

	public void CloseText()
	{
		BGObj.GetComponent<Image>().sprite = BgImg;
		GoldObj.GetComponent<Image>().sprite = GoldImg;
		AwardCount.color = new Color(26f / 51f, 26f / 51f, 26f / 51f, 1f);
	}
}
