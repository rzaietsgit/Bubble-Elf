using System.Collections;
using GooglePlayGames;
using EasyMobile;
using UnityEngine;

public class GooglePlay3Panel : GooglePlay3PanelBase
{
	public static GooglePlay3Panel panel;

	public override void InitUI()
	{
		panel = this;
		BaseUIAnimation.action.SetLanguageFont("ggplaybtntext3", detail.RankingListText_Text, string.Empty);
		//BaseUIAnimation.action.SetLanguageFont("ggplaybtntext4", detail.AchievementText_Text, string.Empty);
		//BaseUIAnimation.action.SetLanguageFont("ggplaybtntext5", detail.Title_Text, string.Empty);
		//CheckLogin();
	}

	public void ResGg()
	{
		//CheckLogin();
		StartCoroutine(resggg());
	}

	public IEnumerator resggg()
	{
		while (true)
		{
			//CheckLogin();
			yield return new WaitForSeconds(0.2f);
		}
	}

	//public void CheckLogin()
	//{
		//if (InitAndroid.action.bLoginState())
		//{
			//UnityEngine.Debug.Log("jy Unity CheckLogin1");
			//BaseUIAnimation.action.SetLanguageFont("ggplaybtntext2", detail.LoginText_Text, string.Empty);
		//}
		//else
		//{
			//UnityEngine.Debug.Log("jy Unity CheckLogin2");
			//BaseUIAnimation.action.SetLanguageFont("ggplaybtntext1", detail.LoginText_Text, string.Empty);
		//}
	//}

	//public override void OnLogin()
	//{
		//if (InitAndroid.action.bLoginState())
		//{
		//	UnityEngine.Debug.Log("jy Unity OnLogin1");
		//	InitAndroid.action.ggLogin_out();
		//	BaseUIAnimation.action.SetLanguageFont("ggplaybtntext1", detail.LoginText_Text, string.Empty);
		//}
		//else
		//{
		//	UnityEngine.Debug.Log("jy Unity OnLogin2");
		//	BaseUIAnimation.action.SetLanguageFont("ggplaybtntext2", detail.LoginText_Text, string.Empty);
		//	InitAndroid.action.ggLogin_in();
		//}
	//}

	public override void OnRankingList()
	{

		if (GameServiceManager.LocalUser.authenticated)
		{
			GameServiceManager.ShowLeaderboardUI();
		}
	}

	//public override void OnAchievement()
	//{
		
	//}
}
