using DG.Tweening;
using EasyMobile;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using ITSoft;
using UnityEngine;
using UnityEngine.UI;

public class SignReward7UIPanel : SignReward7UIPanelBase
{
	public static SignReward7UIPanel panel;

	public GameObject[] rewardBox;

	public GameObject[] rewardBox1;

	public Sprite logoimgsp;

	private string sTime = string.Empty;

	private bool b = true;

	private int iTimeClose;

	private int iLizi;

	private bool bshowbox;

	public override void InitUI()
	{
		panel = this;
		if (InitGame.bEnios)
		{
			detail.Image_Image.sprite = logoimgsp;
			detail.down_Text.text = "For 7 consecutive days";
			detail.down1_Text.text = "Receive generous rewards every day";
			detail.text_Text.text = "Tomorrow";
		}
        AdsManager.ShowBanner();
        DataManager.bbeibaoFlay = false;
		if (!Util.CheckOnline())
		{
			InitGame.Action.GetTimeNetTime();
			return;
		}
		Singleton<DataManager>.Instance.bOpenReward7 = true;
		sTime = Util.GetNowTime_Day();
		int iSign7 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Sign7");
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SignOK7" + sTime) == 0)
		{
			if (iSign7 != 1)
			{
				SkeletonAnimation ui_7day_tree_SkeletonAnimation = detail.ui_7day_tree_SkeletonAnimation;
				ui_7day_tree_SkeletonAnimation.state.AddAnimation(0, iSign7 - 1 + "day_static", loop: false, 0f);
				ui_7day_tree_SkeletonAnimation.state.End += delegate
				{
					if (b)
					{
						b = false;
						PlayDay(iSign7);
					}
				};
			}
			else
			{
				PlayDay(iSign7);
			}
		}
		else
		{
			StaticAni7(iSign7 - 1);
			ShowLizi(iSign7);
		}
	}

	public override UIType GetUIType()
	{
		return UIType.DOWN;
	}

	public void PlayDay(int index)
	{
		SkeletonAnimation ui_7day_tree_SkeletonAnimation = detail.ui_7day_tree_SkeletonAnimation;
		ui_7day_tree_SkeletonAnimation.state.AddAnimation(2, index + "day", loop: false, 0f);
		ui_7day_tree_SkeletonAnimation.state.AddAnimation(1, "static", loop: true, 0f);
		detail.CloseButton_Button.gameObject.SetActive(value: true);
		SoundController.action.playNow("ui_flower_open");
		StartCoroutine(boxShow(index));
	}

	private IEnumerator boxShow(int index)
	{
		yield return new WaitForSeconds(1.333f);
		bshowbox = true;
		rewardBox[index - 1].transform.localScale = Vector3.zero;
		rewardBox[index - 1].SetActive(value: true);
		rewardBox1[index - 1].SetActive(value: true);
		if (index <= 4)
		{
			Sequence s = DOTween.Sequence();
			s.Append(rewardBox[index - 1].transform.DOScale(0.0071f, 0.2f)).Append(rewardBox[index - 1].transform.DOScale(0.0055f, 0.2f)).Append(rewardBox[index - 1].transform.DOScale(0.006f, 0.1f));
		}
		if (index == 5)
		{
			Sequence s2 = DOTween.Sequence();
			s2.Append(rewardBox[index - 1].transform.DOScale(0.0081f, 0.2f)).Append(rewardBox[index - 1].transform.DOScale(0.0065f, 0.2f)).Append(rewardBox[index - 1].transform.DOScale(0.007f, 0.1f));
		}
		if (index == 6)
		{
			Sequence s3 = DOTween.Sequence();
			s3.Append(rewardBox[index - 1].transform.DOScale(0.0091f, 0.2f)).Append(rewardBox[index - 1].transform.DOScale(0.0071f, 0.2f)).Append(rewardBox[index - 1].transform.DOScale(0.008f, 0.1f));
		}
		if (index == 7)
		{
			Sequence s4 = DOTween.Sequence();
			s4.Append(rewardBox[index - 1].transform.DOScale(0.0101f, 0.2f)).Append(rewardBox[index - 1].transform.DOScale(0.0085f, 0.2f)).Append(rewardBox[index - 1].transform.DOScale(0.009f, 0.1f));
		}
	}

	public void StaticAni7(int index)
	{
		SkeletonAnimation ui_7day_tree_SkeletonAnimation = detail.ui_7day_tree_SkeletonAnimation;
		ui_7day_tree_SkeletonAnimation.state.AddAnimation(0, index + "day_static", loop: false, 0f);
		StaticAni7_Static();
	}

	public void StaticAni7_Static()
	{
		SkeletonAnimation ui_7day_tree_SkeletonAnimation = detail.ui_7day_tree_SkeletonAnimation;
		ui_7day_tree_SkeletonAnimation.state.AddAnimation(1, "static", loop: true, 0f);
	}

	public void ShowLizi(int index)
	{
		if (index != 1 && index <= 7)
		{
			iLizi = index;
			detail.bg_Image.gameObject.transform.Find("par_7day_tips" + index).gameObject.SetActive(value: true);
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject gameObject = TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject == null || (gameObject.name.LastIndexOf("TopRight") >= 0 && DataManager.bbeibaoFlay))
			{
				return;
			}
			if (gameObject.name.LastIndexOf("Image") >= 0)
			{
				clickpointer(gameObject.name);
			}
		}
		if (Input.GetMouseButtonUp(0) && detail.max_Image.gameObject.active)
		{
			detail.max_Image.gameObject.SetActive(value: false);
		}
	}

	public GameObject TouchChecker(Vector3 mouseposition)
	{
		Vector3 vector = Camera.main.ScreenToWorldPoint(mouseposition);
		Vector2 point = new Vector2(vector.x, vector.y);
		if ((bool)Physics2D.OverlapPoint(point))
		{
			return Physics2D.OverlapPoint(point).gameObject;
		}
		return null;
	}

	public void clickpointer(string sPname)
	{
		if (sPname == "S_Image22")
		{
			ClickSignShowBox(2);
		}
		if (sPname == "S_Image33")
		{
			ClickSignShowBox(3);
		}
		if (sPname == "S_Image44")
		{
			ClickSignShowBox(4);
		}
		if (sPname == "S_Image55")
		{
			ClickSignShowBox(5);
		}
		if (sPname == "S_Image66")
		{
			ClickSignShowBox(6);
		}
		if (sPname == "S_Image77")
		{
			ClickSignShowBox(7);
		}
	}

	public void ClickSignShowBox(int index)
	{
		if (iLizi == index)
		{
			iTimeClose = 3;
			for (int i = 1; i <= 4; i++)
			{
				int num = int.Parse(Singleton<DataManager>.Instance.dDataSignmap7[index.ToString()]["reward" + i]);
				int num2 = int.Parse(Singleton<DataManager>.Instance.dDataSignmap7[index.ToString()]["count" + i]);
				detail.max_Image.gameObject.transform.Find("icon" + i).gameObject.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num, 138, 114);
				detail.max_Image.gameObject.transform.Find("text" + i).gameObject.GetComponent<Text>().text = "x" + num2;
			}
			detail.max_Image.gameObject.SetActive(value: true);
			detail.max_Image.gameObject.transform.position = rewardBox[index - 1].transform.position + new Vector3(0f, 3f, 0f);
			if (index == 7)
			{
				detail.max_Image.gameObject.transform.position = rewardBox[index - 1].transform.position - new Vector3(0f, 4f, 0f);
			}
		}
	}

	public override void OnImage11()
	{
		sign7(1);
	}

	public override void OnS_Image22()
	{
		sign7(2);
	}

	public override void OnS_Image33()
	{
		sign7(3);
	}

	public override void OnS_Image44()
	{
		sign7(4);
	}

	public override void OnS_Image55()
	{
		sign7(5);
	}

	public override void OnS_Image66()
	{
		sign7(6);
	}

	public override void OnS_Image77()
	{
		sign7(7);
	}

	public void sign7(int index)
	{
		if (!bshowbox)
		{
			UnityEngine.Debug.Log("boxreturn " + index);
			return;
		}
		iTimeClose = 3;
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Sign7");
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SignOK7" + sTime);
		if (int2 == 1)
		{
			return;
		}
		if (@int != index)
		{
			index = @int;
		}
		detail.CloseButton_Button.gameObject.SetActive(value: true);
		ClickSignShowBox(index);
		if (!Util.CheckOnline())
		{
			return;
		}
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_SignOK7" + sTime, 1);
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		for (int i = 1; i <= 4; i++)
		{
			int num = int.Parse(Singleton<DataManager>.Instance.dDataSignmap7[index.ToString()]["reward" + i]);
			int num2 = int.Parse(Singleton<DataManager>.Instance.dDataSignmap7[index.ToString()]["count" + i]);
			if (num > 0)
			{
				list.Add(num);
				list2.Add(num2);
				ChinaPay.action.addRewardAll(num, num2, MapUI.action.gameObject, isShow: false);
			}
		}
		BaseUIAnimation.action.ShowProp(list, list2, MapUI.action.gameObject);
		Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_Sign7", @int + 1);
		InitAndroid.action.GAEvent("Reward:Reward7:" + @int);
		rewardBox[@int - 1].SetActive(value: false);
		rewardBox1[@int - 1].SetActive(value: false);
		if (index != 7)
		{
			ShowLizi(index + 1);
		}
		if (@int == 7)
		{
			MapUI.action.SignReward7UIObj.SetActive(value: false);
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_Sign7State", 1);
		}
		//AdManager.action.opadshowcp(DataManager.PAGE_MAIN);
	}

	public override void OnCloseButton()
	{
		if ((bool)OpenScript.actionSevLogin1)
		{
			OpenScript.actionSevLogin1.ResSev7Login();
		}
		StartCoroutine(CLoseSign7());
		UI.Instance.ClosePanel();
		string nowTime_Day = Util.GetNowTime_Day();
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SignRewardDay31" + nowTime_Day) == 0 && !Util.CheckOnline())
		{
		}
	}

	public IEnumerator CLoseSign7()
	{
		yield return new WaitForSeconds(1f);
		Singleton<DataManager>.Instance.bOpenReward7 = false;
	}
}
