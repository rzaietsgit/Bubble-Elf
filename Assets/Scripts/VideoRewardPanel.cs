using System.Collections;
using EasyMobile;
using ITSoft;
using UnityEngine;

public class VideoRewardPanel : VideoRewardPanelBase
{
	public static VideoRewardPanel panel;

	public Sprite[] btnsp;

	private bool bfree;

	public override void InitUI()
	{
		panel = this;
		BaseUIAnimation.action.SetLanguageFont("videoText1", detail.Title_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("videoText2", detail.Title2_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("videoText3", detail.ConfirmText_Text, string.Empty);
		for (int i = 1; i < 10; i++)
		{
			GameObject gameObject = Object.Instantiate(base.transform.Find("bg/Image123/GameObject1232/objbg").gameObject, base.transform.position, base.transform.rotation);
			gameObject.transform.parent = base.transform.Find("bg/Image123/GameObject1232").gameObject.transform;
			VideoSon component = gameObject.GetComponent<VideoSon>();
			component.Init(i);
			gameObject.SetActive(value: true);
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_VideoNextTime");
		int nowTime = Util.GetNowTime();
		detail.times_Text.gameObject.SetActive(value: false);
		if (nowTime >= @int)
		{
			bfree = true;
			detail.Confirm_Image.sprite = btnsp[0];
		}
		else
		{
			StartCoroutine(UpdateTime(@int - nowTime));
		}
	}

	public IEnumerator UpdateTime(int itime)
	{
		detail.Confirm_Image.sprite = btnsp[1];
		while (itime > 1)
		{
			detail.times_Text.text = itime + "s";
			itime--;
			detail.times_Text.gameObject.SetActive(value: true);
			yield return new WaitForSeconds(1f);
		}
		detail.Confirm_Image.sprite = btnsp[0];
		detail.times_Text.gameObject.SetActive(value: false);
		bfree = true;
	}

	public override void OnPauseBase()
	{
	}

	public override void OnResumeBase()
	{
	}
    private int indexads;
	public override void OnConfirm()
	{
#if UNITY_EDITOR
        DataManager.isShopAd = true;
        InitAndroid.action.PlayVideoHG();
#endif
        if (bfree)
		{
			//if (nowTime <= @int)
			//{
			//	Singleton<DataManager>.Instance.iNoticePanelType = 3;
			//	UI.Instance.OpenPanel(UIPanelType.NoticePanel);
			//}
			//else 
            if (AdsManager.RewardIsReady())
			{
                indexads = 1;
                AdsManager.ShowRewarded();
                int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_VideoNextTime");
                int nowTime = Util.GetNowTime();
            }
			else
			{
				Singleton<DataManager>.Instance.iNoticePanelType = 3;
				UI.Instance.OpenPanel(UIPanelType.NoticePanel);
			}
		}
	}

    void OnEnable()
    {
	    AdsManager.OnCompleteRewardVideo += RewardedAdCompletedHandler;
    }
    // The event handler
    void RewardedAdCompletedHandler()
    {
        if (indexads == 1)
        {
            indexads = 0;
            DataManager.isShopAd = true;
            InitAndroid.action.PlayVideoHG();
        }
    }
    // Unsubscribe
    void OnDisable()
    {
	    AdsManager.OnCompleteRewardVideo -= RewardedAdCompletedHandler;
    }
}
