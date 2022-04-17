using UnityEngine;
using UnityEngine.UI;

public class BeibaoBtn : MonoBehaviour
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

	public void ClickOpenBeibao()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Util.GetbForced_guidance())
		{
			UI.Instance.OpenPanel(UIPanelType.PackSkillIconUI);
		}
	}
}
