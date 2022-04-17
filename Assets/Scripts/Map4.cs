using UnityEngine;

public class Map4 : MonoBehaviour
{
	public static Map4 _map4;

	private GameObject[] Map_4;

	public GameObject MapImgObj;

	private int iMapIndex;

	private void Start()
	{
		_map4 = this;
		Map_4 = new GameObject[4];
		Singleton<UserManager>.Instance.LoadNowPassLevelNumber();
		if (Singleton<LevelManager>.Instance.bFirstInMap)
		{
			iMapIndex = Singleton<UserManager>.Instance.iNowMapID;
		}
		else
		{
			int num = Singleton<LevelManager>.Instance.iNowSelectLevelIndex;
			if (!Singleton<LevelManager>.Instance.bExit && !Singleton<LevelManager>.Instance.bLoseGame)
			{
				num++;
			}
			int mapForLevelID = Singleton<UserManager>.Instance.GetMapForLevelID(num);
			iMapIndex = mapForLevelID - 1;
		}
		GoMap(iMapIndex);
	}

	private void Update()
	{
	}

	public void GoMap(int iIndex)
	{
	}

	public void UpdateImagePos(GameObject ImageObj, int index)
	{
		Vector2 sizeDelta = ImageObj.GetComponent<RectTransform>().sizeDelta;
		float x = sizeDelta.x;
		Vector2 sizeDelta2 = ImageObj.GetComponent<RectTransform>().sizeDelta;
		float y = sizeDelta2.y;
		switch (index)
		{
		case 0:
			ImageObj.transform.localPosition = new Vector2(x / 2f, y / 2f * -1f);
			break;
		case 1:
			ImageObj.transform.localPosition = new Vector2(x / 2f * 3f, y / 2f * -1f);
			break;
		case 2:
			ImageObj.transform.localPosition = new Vector2(x / 2f, y / 2f * -1f * 3f);
			break;
		case 3:
			ImageObj.transform.localPosition = new Vector2(x / 2f * 3f, y / 2f * -1f * 3f);
			break;
		}
	}
}
