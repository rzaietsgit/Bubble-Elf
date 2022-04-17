using System.Collections;
using System.Collections.Generic;
using EasyMobile;
using ITSoft;
using UnityEngine;

public class ReadyGoUIPanel : ReadyGoUIPanelBase
{
	public static ReadyGoUIPanel panel;

	public override void InitUI()
	{
		panel = this;
		BaseUIAnimation.action.SetLanguageFont("ReadGoUI", detail.ReadGoUI_Text, string.Empty);
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Skill_Select_" + 0) == 1)
		{
			GameUI.action.skilliRand = Random.Range(1, 101);
		}
		int skilliRand = GameUI.action.skilliRand;
		for (int i = 0; i <= 3; i++)
		{
			int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Skill_SelectNew_" + i);
			UnityEngine.Debug.Log("11111111111111111      " + Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Skill_Select_" + i));
			if (@int != 1)
			{
				continue;
			}
			Singleton<DataManager>.Instance.SaveUserDate("DB_Skill_SelectNew_" + i, 0);
			if (i == 0)
			{
				List<int> list = new List<int>();
				if ((skilliRand <= 0 || skilliRand >= 15) && GameGuide.Instance.MT1.GetComponent<MuTong>().isSkill)
				{
					list.Add(1);
				}
				if ((skilliRand < 15 || skilliRand >= 55) && GameGuide.Instance.MT2.GetComponent<MuTong>().isSkill)
				{
					list.Add(2);
				}
				if ((skilliRand < 55 || skilliRand >= 85) && GameGuide.Instance.MT4.GetComponent<MuTong>().isSkill)
				{
					list.Add(3);
				}
				if (skilliRand < 85 && GameGuide.Instance.MT5.GetComponent<MuTong>().isSkill)
				{
					list.Add(4);
				}
				if (list.Count > 0)
				{
					int index = Random.Range(0, list.Count);
					if (list[index] == 1)
					{
						if (GameGuide.Instance.MT1.GetComponent<MuTong>().isSkill)
						{
							GameGuide.Instance.MT1.GetComponent<MuTong>().AddSkill();
						}
					}
					else if (list[index] == 2)
					{
						if (GameGuide.Instance.MT2.GetComponent<MuTong>().isSkill)
						{
							GameGuide.Instance.MT2.GetComponent<MuTong>().AddSkill();
						}
					}
					else if (list[index] == 3)
					{
						if (GameGuide.Instance.MT4.GetComponent<MuTong>().isSkill)
						{
							GameGuide.Instance.MT4.GetComponent<MuTong>().AddSkill();
						}
					}
					else if (list[index] == 4 && GameGuide.Instance.MT5.GetComponent<MuTong>().isSkill)
					{
						GameGuide.Instance.MT5.GetComponent<MuTong>().AddSkill();
					}
				}
			}
			if (i == 1)
			{
				BubbleSpawner.Instance.useSkill1();
			}
			if (i == 2 && Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_Skill_Select_" + i) == 0)
			{
				BubbleSpawner.Instance.useSkill2();
			}
			if (i != 3)
			{
			}
		}
		StartCoroutine(StartCloseUI());
	}

	private IEnumerator StartCloseUI()
	{
		yield return new WaitForSeconds(2f);
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_game_start");
		}
		UI.Instance.ClosePanel();
	}

	public override void OnExit()
	{
		PassLevel.action.bGameStart = true;
		GameUI.action.InitUserSkillAni();
		GameGuide.Instance.initGuide();
	}
    private int indexads;
    public void WatchAds()
    {
        bool isReady = AdsManager.RewardIsReady();
        // Show it if it's ready
        if (isReady)
        {
            indexads = 1;
            AdsManager.ShowRewarded();
        }
    }

    void OnEnable()
    {
	    AdsManager.OnCompleteRewardVideo += RewardedAdCompletedHandler;
    }
    // The event handler
    void RewardedAdCompletedHandler()
    {
     if(indexads == 1)
        {
            indexads = 0;
            GameUI.action.isZhiJieBoFang = true;
            GameUI.action.buyBuyBuShu = 1;
            Singleton<LevelManager>.Instance.iBubbleCount = Singleton<LevelManager>.Instance.iBubbleCount + 3;
            Singleton<LevelManager>.Instance.iBubbleUseCount = 0;
            PassLevel.action.KillLiuhan();
            GameUI.action.LoadBubbleCount();
            BubbleSpawner.Instance.initReadyBubble(isusekey: false);
            GameUI.action.KillNowBuyBubble();
        }
    }
    // Unsubscribe
    void OnDisable()
    {
	    AdsManager.OnCompleteRewardVideo -= RewardedAdCompletedHandler;
    }
}
