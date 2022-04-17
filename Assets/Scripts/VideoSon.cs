using UnityEngine;
using UnityEngine.UI;

public class VideoSon : MonoBehaviour
{
	public Image icon;

	public Text iNo;

	public Text iCount;

	public Sprite ysbg;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Init(int index)
	{
		int num = int.Parse(Singleton<DataManager>.Instance.dDatavideoReward[index.ToString()]["sReward"].Split('|')[0]);
		icon.sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num, 138, 114);
		iNo.text = index.ToString();
		iCount.text = Singleton<DataManager>.Instance.dDatavideoReward[index.ToString()]["sReward"].Split('|')[1];
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_VideoIndex", 1);
		if (@int == index)
		{
			GetComponent<Image>().sprite = ysbg;
		}
	}
}
