using UnityEngine;
using UnityEngine.UI;

public class qiandaoBtn : MonoBehaviour
{
	public Image ImgDown;

	public Sprite Spen;

	private void Start()
	{
		if (InitGame.bEnios)
		{
			ImgDown.GetComponent<Image>().sprite = Spen;
		}
	}

	private void Update()
	{
	}

	public void ClickSign31()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance() && Util.CheckOnline())
		{
			UI.Instance.OpenPanel(UIPanelType.SignRewardUI);
			InitAndroid.action.GAEvent("clickbtn:SignRewardUI");
		}
	}
}
