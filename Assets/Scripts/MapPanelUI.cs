using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPanelUI : MonoBehaviour
{
	public GameObject ChinaMapIconUIObj;

	public GameObject MapObjList;

	public static MapPanelUI action;

	public Text SetMapUITitle;

	public List<MapObjChina> LMapObjChina;

	public void ClearRemark()
	{
		for (int i = 0; i < LMapObjChina.Count; i++)
		{
			LMapObjChina[i].ClearTip();
		}
	}

	private void Start()
	{
		action = this;
		BaseUIAnimation.action.SetLanguageFont("SetMapUITitle", SetMapUITitle, string.Empty);
		if (InitGame.bChinaVersion)
		{
			LMapObjChina = new List<MapObjChina>();
		}
		for (int i = 0; i < UserManager.iMapCount; i++)
		{
			if (InitGame.bChinaVersion)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(ChinaMapIconUIObj);
				gameObject.transform.SetParent(MapObjList.transform, worldPositionStays: false);
				MapObjChina component = gameObject.GetComponent<MapObjChina>();
				component.Load(i);
				LMapObjChina.Add(component);
				gameObject.SetActive(value: true);
			}
		}
		RectTransform component2 = MapObjList.transform.GetComponent<RectTransform>();
		component2.sizeDelta = new Vector2(596f, UserManager.iMapCount * 276 + 276);
		int num = Singleton<UserManager>.Instance.iNowMapID + 1;
		float num2 = (UserManager.iMapCount * 276 + 276) / 2 - 276 - 310 - num * 276 + 550;
		if (num <= 2)
		{
			num2 = (UserManager.iMapCount * 276 + 276) / 2 - 529;
		}
		if (num >= UserManager.iMapCount - 1)
		{
			num2 = ((UserManager.iMapCount * 276 + 276) / 2 - 529) * -1;
		}
		if (InitGame.bChinaVersion)
		{
			GridLayoutGroup component3 = MapObjList.GetComponent<GridLayoutGroup>();
			component3.cellSize = new Vector2(516f, 162f);
			component2.sizeDelta = new Vector2(516f, UserManager.iMapCount * 162);
		}
	}

	public void ClickSettingBtn(bool bClick)
	{
		if ((bool)MailUI.action)
		{
			MailUI.action.gameObject.SetActive(value: false);
		}
		if ((bool)SetPanelUI.action)
		{
			SetPanelUI.action.gameObject.SetActive(value: false);
		}
	}

	private void Update()
	{
		if (InitGame.bChinaVersion && Input.GetMouseButtonDown(0))
		{
			ClearRemark();
		}
	}

	public void UpdateAllMapObjChina_HongDian()
	{
		for (int i = 0; i < LMapObjChina.Count; i++)
		{
			LMapObjChina[i].UpdateHongDian();
		}
	}
}
