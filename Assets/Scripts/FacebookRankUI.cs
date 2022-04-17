using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FacebookRankUI : MonoBehaviour
{
	public GameObject FacebookRankIconObj;

	public GameObject FacebookRankIconFatherObj;

	public static FacebookRankUI action;

	public GameObject OnlineObj;

	public GameObject OfficeObj;

	public GameObject FBOBJ;

	public GameObject FacebookRankIconMeObj;

	private bool bTestRank;

	public FacebookRankIcon HideMyIconScript;

	private void Start()
	{
		action = this;
		if (InitGame.bChinaVersion)
		{
			FBOBJ.SetActive(value: false);
		}
		else
		{
			FBOBJ.SetActive(value: true);
		}
		if (InitGame.bEnios)
		{
			FBOBJ.SetActive(value: true);
		}
		if (bTestRank)
		{
			OnlineObj.SetActive(value: true);
			OfficeObj.SetActive(value: false);
			base.gameObject.SetActive(value: false);
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			FaceBookApi.Action.UserId = "123456";
			for (int i = 0; i < 10; i++)
			{
				if (i == 5)
				{
					dictionary.Add("123456", 1235);
				}
				dictionary.Add("11111116" + i, 1235);
			}
			OnlineFacebook(dictionary);
			GameObject gameObject = UnityEngine.Object.Instantiate(FacebookRankIconMeObj);
			FacebookRankIcon component = gameObject.GetComponent<FacebookRankIcon>();
			component.SetRankNb(3);
			component.SetName("You");
			component.SetScore();
			component.SetHaedIcon(string.Empty);
			gameObject.transform.SetParent(FacebookRankIconFatherObj.transform.parent.transform.parent, worldPositionStays: false);
			gameObject.SetActive(value: false);
			return;
		}
		CheckOnline();
		if ((bool)FireBase.Action)
		{
			FireBase.Action.bSearchNoShow = false;
			if (FaceBookApi.Action.bLoginState())
			{
				FireBase.Action.UnitySearchRankScore(FaceBookApi.Action.LFacebookFriend);
			}
		}
	}

	private IEnumerator IEMoveRank(GameObject obj)
	{
		yield return new WaitForSeconds(2f);
		obj.transform.DOLocalMoveX(-174f, 3f, snapping: true).SetEase(Ease.OutSine).OnComplete(delegate
		{
			ShowFaceBookIcon(obj);
		});
		obj.SetActive(value: true);
	}

	public void ShowFaceBookIcon(GameObject obj)
	{
		HideMyIconScript.SetShow();
		obj.SetActive(value: false);
	}

	public void FaceBookLogin()
	{
		FaceBookApi.Action.FackBookLogin();
	}

	public void CheckOnline()
	{
		if (FaceBookApi.Action.bLoginState())
		{
			base.gameObject.SetActive(value: false);
			OnlineObj.SetActive(value: true);
			OfficeObj.SetActive(value: false);
		}
		else
		{
			Offline();
			Animator component = base.transform.parent.GetComponent<Animator>();
			component.SetInteger("facebookint", 3);
		}
	}

	private void Offline()
	{
		OnlineObj.SetActive(value: false);
		OfficeObj.SetActive(value: true);
	}

	public void OnlineFacebook(Dictionary<string, int> _FriendListRank)
	{
		int num = 0;
		float num2 = 0f;
		int num3 = 0;
		FacebookRankIcon facebookRankIcon = null;
		foreach (string key in _FriendListRank.Keys)
		{
			string text = string.Empty;
			try
			{
				if (key == FaceBookApi.Action.UserId)
				{
					if (FaceBookApi.Action.MyFaceBookName != null)
					{
						text = FaceBookApi.Action.MyFaceBookName;
					}
					num2 = num + 1;
				}
				else
				{
					text = FaceBookApi.Action.dFacebookName[key];
				}
			}
			catch (Exception)
			{
			}
			if (text.Length > 5)
			{
				text = text.Substring(0, 5) + "...";
			}
			GameObject gameObject = UnityEngine.Object.Instantiate(FacebookRankIconObj);
			facebookRankIcon = gameObject.GetComponent<FacebookRankIcon>();
			facebookRankIcon.SetRankNb(num3 + 1);
			if (num3 == 0)
			{
				facebookRankIcon.SetNoOne();
			}
			facebookRankIcon.SetName(text);
			facebookRankIcon.SetScore(_FriendListRank[key]);
			facebookRankIcon.SetHaedIcon(key);
			gameObject.transform.SetParent(FacebookRankIconFatherObj.transform, worldPositionStays: false);
			gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
			num++;
			num3++;
		}
		if (num > 0 || num != 1)
		{
			facebookRankIcon.HideLine();
		}
		if (num == 2)
		{
			FacebookRankIconFatherObj.GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.UpperRight;
		}
		float num4 = num / 2;
		int num5 = 70;
		if (num % 2 == 1)
		{
			num5 = 0;
			num2 -= 1f;
		}
		if (num > 3)
		{
			int num6 = 200;
			int num7 = num - 4;
			RectTransform component = FacebookRankIconFatherObj.transform.GetComponent<RectTransform>();
			if (num == 4)
			{
				RectTransform rectTransform = component;
				Vector2 sizeDelta = component.sizeDelta;
				rectTransform.sizeDelta = new Vector2(800f, sizeDelta.y);
			}
			else
			{
				RectTransform rectTransform2 = component;
				float x = 800 + num6 * num7;
				Vector2 sizeDelta2 = component.sizeDelta;
				rectTransform2.sizeDelta = new Vector2(x, sizeDelta2.y);
			}
			if (num2 <= num4)
			{
				num2 = (float)num5 + (num4 - num2) * (float)num6;
			}
			else
			{
				num2 = (num2 - num4) * (float)num6 - (float)num5;
				num2 *= -1f;
			}
			RectTransform rectTransform3 = component;
			float x2 = num2;
			Vector3 localPosition = component.localPosition;
			float y = localPosition.y;
			Vector3 localPosition2 = component.localPosition;
			rectTransform3.localPosition = new Vector3(x2, y, localPosition2.z);
		}
		base.gameObject.SetActive(value: true);
		Animator component2 = base.transform.parent.GetComponent<Animator>();
		component2.SetInteger("facebookint", 3);
	}

	private void Update()
	{
	}
}
