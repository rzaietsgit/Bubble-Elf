using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PackSkillIcon : MonoBehaviour
{
	private bool btime;

	public Image Icon;

	public Text iCountText;

	public Text sTimeText;

	public GameObject TimePackImgObj;

	public GameObject RemarkObj;

	public Text RemarkTextTitle;

	public Text RemarkText;

	private BackPackSKILL ThisBackPackSKILL;

	public Sprite SpEn;

	private void Start()
	{
		RemarkObj.SetActive(value: false);
		if (InitGame.bEnios)
		{
			TimePackImgObj.GetComponent<Image>().sprite = SpEn;
		}
	}

	private void Update()
	{
	}

	public void ClickSkillShowRemark()
	{
		if (!ThisBackPackSKILL.bisNull)
		{
			StartCoroutine(IEShow());
		}
	}

	private IEnumerator IEShow()
	{
		yield return new WaitForSeconds(0.2f);
		RemarkObj.SetActive(value: true);
		ThisBackPackSKILL.remark = ThisBackPackSKILL.remark.Replace("<BR>", "\n");
		RemarkText.text = ThisBackPackSKILL.remark;
		RemarkTextTitle.text = ThisBackPackSKILL.remarkTitle;
	}

	public void HideRemark()
	{
		RemarkObj.SetActive(value: false);
	}

	public void InitSkill(BackPackSKILL _BackPackSKILL)
	{
		ThisBackPackSKILL = _BackPackSKILL;
		if (_BackPackSKILL.skillID == 3)
		{
			Icon.sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_17", 138, 114);
		}
		else
		{
			Icon.sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + _BackPackSKILL.skillID, 138, 114);
		}
		btime = _BackPackSKILL.btime;
		if (btime)
		{
			TimePackImgObj.SetActive(value: true);
			ShowTextTime(_BackPackSKILL.iTime);
			iCountText.gameObject.SetActive(value: false);
			StartCoroutine(UpdateSkillTime(_BackPackSKILL.iTime - 60));
		}
		else
		{
			TimePackImgObj.SetActive(value: false);
			iCountText.text = "x" + _BackPackSKILL.iCount;
			sTimeText.gameObject.SetActive(value: false);
		}
		if (_BackPackSKILL.bisNull)
		{
			sTimeText.gameObject.SetActive(value: false);
			TimePackImgObj.SetActive(value: false);
			iCountText.gameObject.SetActive(value: false);
			TimePackImgObj.SetActive(value: false);
			Icon.gameObject.SetActive(value: false);
		}
	}

	private IEnumerator UpdateSkillTime(int itime)
	{
		yield return new WaitForSeconds(60f);
		while (true)
		{
			itime -= 60;
			ShowTextTime(itime);
			yield return new WaitForSeconds(60f);
		}
	}

	public void ShowTextTime(int iTime)
	{
		if (iTime > 0)
		{
			TimeSpan timeSpan = new TimeSpan(0, 0, iTime);
			int minutes = timeSpan.Minutes;
			int hours = timeSpan.Hours;
			int seconds = timeSpan.Seconds;
			string text = minutes + string.Empty;
			string text2 = hours + string.Empty;
			string str = seconds + string.Empty;
			if (minutes < 10)
			{
				text = "0" + text;
			}
			if (hours < 10)
			{
				text2 = "0" + text2;
			}
			if (seconds < 10)
			{
				str = "0" + str;
			}
			sTimeText.text = text2 + ":" + text;
		}
	}
}
