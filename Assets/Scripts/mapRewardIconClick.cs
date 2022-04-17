using UnityEngine;

public class mapRewardIconClick : MonoBehaviour
{
	public static mapRewardIconClick action;

	public Sprite[] StateObj;

	private void Start()
	{
		action = this;
	}

	private void Update()
	{
	}

	public void ClickmapRewardIconClick()
	{
		Singleton<DataManager>.Instance.iSelectMapRewardID = MapUI.action.iMapIndex + 1;
		UI.Instance.OpenPanel(UIPanelType.MapRewardUI);
		InitAndroid.action.GAEvent("clickbtn:ClickMapRewardUIPanel:1");
	}

	public void ResUI(int index)
	{
	}
}
