using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qiandao7Panel : Qiandao7PanelBase
{
	public static Qiandao7Panel panel;

	public Sprite btnnull;

	private qiandaobj qiandaobjScriptTemp;

	private bool bclick;

	public bool bopenPlay;

	public override void InitUI()
	{
		UnityEngine.Debug.Log("jyqiandao 2  BtnType.qiandao");
		panel = this;
		BaseUIAnimation.action.SetLanguageFont("qiandaoText1", detail.Title_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("qiandaoText2", detail.btnText_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("qiandaoText3", detail.remarktext_Text, string.Empty);
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iqiandao7Count", 1);
		for (int i = 0; i < 7; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(detail.lineobj_Image.gameObject);
			gameObject.transform.SetParent(detail.linepat_GridLayoutGroup.gameObject.transform, worldPositionStays: false);
			gameObject.SetActive(value: true);
			qiandaobj component = gameObject.GetComponent<qiandaobj>();
			component.Initqiandao(i + 1);
			if (@int == 8 - (i + 1))
			{
				qiandaobjScriptTemp = gameObject.GetComponent<qiandaobj>();
			}
		}
		string nowTime_Day = Util.GetNowTime_Day();
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SignOK7" + nowTime_Day);
		if (@int > 7 || int2 == 1)
		{
			detail.Btn1_Image.sprite = btnnull;
		}
		UnityEngine.Debug.Log("jyqiandao 3  BtnType.qiandao");
	}

	public override void OnResumeBase()
	{
	}

	public override void OnPauseBase()
	{
	}

	public override void OnExit()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_qiandaoone");
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowPassLevelID");
		if (int2 == 11 && @int == 0)
		{
			Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_qiandaoone", 1);
			Singleton<LevelManager>.Instance.iNowSelectLevelIndex = int2 + 1;
			if (UI.Instance.GetPanelCount() <= 0)
			{
				UI.Instance.OpenPanel(UIPanelType.Play);
			}
		}
	}

	public override void OnBtn1()
	{
		if (!bclick)
		{
			bclick = true;
			string nowTime_Day = Util.GetNowTime_Day();
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iqiandao7Count", 1);
			if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SignOK7" + nowTime_Day) == 0 && @int <= 7)
			{
				Reward();
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_iqiandao7" + @int, 1);
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_iqiandao7Count", @int + 1);
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_SignOK7" + nowTime_Day, 1);
				MapUI.action.SetQiandaoRedDot();
			}
		}
	}

	public void Reward()
	{
		string text = DateTime.Now.ToString("yyyyMMdd");
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iqiandao7Count", 1);
		string text2 = "1|30F1|15F1|20F1|5";
		text2 = Singleton<DataManager>.Instance.dDataSignmap7[@int.ToString()]["reward"];
		for (int i = 0; i < 3; i++)
		{
			string text3 = text2.Split('F')[i];
			int num = int.Parse(text3.Split('|')[0]);
			int num2 = int.Parse(text3.Split('|')[1]);
			list.Add(num);
			list2.Add(num2);
			ChinaPay.action.addRewardAll(num, num2, MapUI.action.gameObject, isShow: false);
		}
		BaseUIAnimation.action.ShowProp(list, list2, MapUI.action.gameObject);
		qiandaobjScriptTemp.IsOk();
		StartCoroutine(IECloseUI());
	}

	private IEnumerator IECloseUI()
	{
		yield return new WaitForSeconds(3f);
		UI.Instance.ClosePanel();
	}
}
