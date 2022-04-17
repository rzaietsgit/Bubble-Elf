using UnityEngine;
using UnityEngine.UI;

public class Map1 : MonoBehaviour
{
	public static Map1 _map1;

	private GameObject[] Map_4;

	private int iMapIndex;

	public GameObject MapImgObj;

	private void Start()
	{
		_map1 = this;
		Map_4 = new GameObject[4];
		Singleton<UserManager>.Instance.LoadNowPassLevelNumber();
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iNowMapID");
		UnityEngine.Debug.Log("jy 8989 iNowMapID=" + @int);
		UnityEngine.Debug.Log("jy 8989 UserManager.Instance.iNowMapID=" + Singleton<UserManager>.Instance.iNowMapID);
		UnityEngine.Debug.Log("jy 8989  LevelManager.Instance.bFirstInMap=" + Singleton<LevelManager>.Instance.bFirstInMap);
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
			if (Singleton<LevelManager>.Instance.bRstart2)
			{
				Singleton<LevelManager>.Instance.bRstart2 = false;
				if (num > 3)
				{
					mapForLevelID = Singleton<UserManager>.Instance.GetMapForLevelID(num - 1);
				}
			}
			iMapIndex = mapForLevelID - 1;
		}
		UnityEngine.Debug.Log("jy 8989  iMapIndex" + iMapIndex);
		GoMap(iMapIndex);
		if ((bool)MapUI.action)
		{
			MapUI.action.ResRewardMap(iMapIndex);
		}
	}

	private void Update()
	{
	}

	public void GoMap(int iIndex)
	{
		UnityEngine.Debug.Log("JyGoMap123  = " + iIndex);
		iMapIndex = iIndex;
		int num = 0;
		Image component;
		string text;
		int num2;
		while (true)
		{
			if (num < 4)
			{
				if (Map_4[num] == null)
				{
					Map_4[num] = UnityEngine.Object.Instantiate(MapImgObj);
					Map_4[num].transform.SetParent(base.transform.parent, worldPositionStays: false);
					Map_4[num].AddComponent<Image>();
					Map_4[num].AddComponent<Canvas>();
					Map_4[num].GetComponent<Canvas>().overrideSorting = true;
					Map_4[num].GetComponent<Canvas>().sortingOrder = 1;
				}
				component = Map_4[num].GetComponent<Image>();
				num2 = 512;
				if (iMapIndex >= 2)
				{
					num2 = 256;
				}
				text = "Map/map" + (iMapIndex + 1) + "/map_0" + (iMapIndex + 1) + "_01_0" + (num + 1);
				string sImageName = "map_0" + (iMapIndex + 1) + "_01_0" + (num + 1);
				if (iMapIndex >= 9)
				{
					text = "Map/map" + (iMapIndex + 1) + "/map_" + (iMapIndex + 1) + "_01_0" + (num + 1);
					sImageName = "map_" + (iMapIndex + 1) + "_01_0" + (num + 1);
				}
				bool flag = false;
				if (Util.CheckMapImage(sImageName))
				{
					text = "Map/map" + num2;
					flag = true;
				}
				if (flag)
				{
					num2 = 32;
				}
				if (num == 0)
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		text = "Map/mapmax/map_0" + (iMapIndex + 1);
		if (iMapIndex >= 9)
		{
			text = "Map/mapmax/map_" + (iMapIndex + 1);
		}
		num2 = 1024;
		Texture2D texture = (Texture2D)Resources.Load(text, typeof(Texture2D));
		Sprite sprite2 = component.sprite = Sprite.Create(texture, new Rect(0f, 0f, num2, num2), new Vector2(0.5f, 0.5f));
		component.SetNativeSize();
		Map_4[num].transform.localPosition = new Vector3(0f, 0f, 0f);
		if (iMapIndex <= 1)
		{
			Map_4[num].transform.localScale = new Vector3(2f, 2f, 2f);
		}
		else
		{
			Map_4[num].transform.localScale = new Vector3(1f, 1f, 1f);
		}
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
			ImageObj.transform.localPosition = new Vector2(x * -1f * 1.5f, y * 1.5f);
			break;
		case 1:
			ImageObj.transform.localPosition = new Vector2(x / 2f * -1f, y / 2f * 3f);
			break;
		case 2:
			ImageObj.transform.localPosition = new Vector2(x / 2f * -3f, y / 2f);
			break;
		case 3:
			ImageObj.transform.localPosition = new Vector2(x / 2f * -1f, y / 2f);
			break;
		}
	}
}
