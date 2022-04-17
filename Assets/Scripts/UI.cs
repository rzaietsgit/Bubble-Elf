using System;
using System.Collections.Generic;
using UnityEngine;

public class UI
{
	[Serializable]
	private class UIPanelTypeJson
	{
		public List<UIPanelInfo> infoList;
	}

	private List<UIPanelType> listType;

	private static UI _instance;

	private Transform canvasTransform;

	private GameObject maskTransform;

	private Dictionary<UIPanelType, string> panelPathDict;

	private Dictionary<UIPanelType, BasePanel> panelDict;

	private Stack<BasePanel> panelStack;

	public static UI Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new UI();
			}
			return _instance;
		}
	}

	public Transform CanvasTransform
	{
		get
		{
			if (canvasTransform == null)
			{
				canvasTransform = GameObject.Find("Canvas").transform;
			}
			return canvasTransform;
		}
	}

	public GameObject MaskTransform
	{
		get
		{
			if (maskTransform == null)
			{
				maskTransform = CanvasTransform.Find("Mask").gameObject;
			}
			return maskTransform;
		}
	}

	private UI()
	{
		listType = new List<UIPanelType>();
		ParseUIPanelTypeJson();
	}

	public int GetPanelCount()
	{
		if (panelStack == null)
		{
			return 0;
		}
		return panelStack.Count;
	}

	public void ClearUI()
	{
		if (panelStack != null)
		{
			panelStack.Clear();
		}
		if (listType != null)
		{
			listType.Clear();
		}
	}

	public List<UIPanelType> GetListUIType()
	{
		return listType;
	}

	public UIPanelType GetTopPanelType()
	{
		if (panelStack == null)
		{
			return UIPanelType.NONE;
		}
		if (panelStack.Count > 0)
		{
			BasePanel basePanel = panelStack.Peek();
			return basePanel.uiType;
		}
		return UIPanelType.NONE;
	}

	public void OpenPanel(UIPanelType panelType, List<UIPanelType> list = null)
	{
		try
		{
			if ((bool)Leftdown.action)
			{
				Leftdown.action.closeui();
			}
			if ((bool)rightdown.action)
			{
				rightdown.action.closeui();
			}
			if (panelStack == null)
			{
				panelStack = new Stack<BasePanel>();
			}
			if (MaskTransform != null)
			{
				MaskTransform.SetActive(value: true);
			}
			if (panelType == UIPanelType.GuideMaxPanel || panelType == UIPanelType.GuideMinPanel)
			{
				MaskTransform.SetActive(value: false);
			}
			if (panelStack.Count <= 0)
			{
				goto IL_00b5;
			}
			BasePanel basePanel = panelStack.Peek();
			if (basePanel.GetUIPanelType() != panelType)
			{
				basePanel.OnPauseBase();
				goto IL_00b5;
			}
			goto end_IL_0000;
			IL_00b5:
			BasePanel panel = GetPanel(panelType);
			if (!panelStack.Equals(panel))
			{
				panel.OnEnterBase(panelType);
				if (list != null)
				{
					for (int i = 0; i < list.Count; i++)
					{
						listType.Add(list[i]);
					}
				}
				panelStack.Push(panel);
				aliyunlog.OpenAndClickBtn(panelType.ToString(), string.Empty, string.Empty);
			}
			end_IL_0000:;
		}
		catch (Exception ex)
		{
			ClosePanel();
			MaskTransform.SetActive(value: false);
			UnityEngine.Debug.LogError("  panelType == " + panelType + "OpenPanel error :" + ex);
		}
	}

	public void ClosePanel(bool isShowExit = true, bool clearAllUI = false)
	{
		try
		{
			if (panelStack == null)
			{
				panelStack = new Stack<BasePanel>();
			}
			if (panelStack.Count > 0)
			{
				bool closeMsk = false;
				BasePanel basePanel = panelStack.Pop();
				if (panelStack.Count <= 0)
				{
					List<UIPanelType> listUIType = GetListUIType();
					if (listUIType.Count == 0)
					{
						closeMsk = true;
					}
				}
				basePanel.OnExitBase(isShowExit, closeMsk);
				if (!clearAllUI)
				{
					if (panelStack.Count <= 0)
					{
						List<UIPanelType> listUIType2 = GetListUIType();
						if (listUIType2 != null && listUIType2.Count != 0)
						{
							Instance.OpenPanel(listUIType2[0]);
							listUIType2.Remove(listUIType2[0]);
						}
					}
					else
					{
						BasePanel basePanel2 = panelStack.Peek();
						basePanel2.OnResumeBase();
					}
				}
			}
		}
		catch (Exception arg)
		{
			ClosePanel();
			MaskTransform.SetActive(value: false);
			UnityEngine.Debug.LogError("ClosePanel error :" + arg);
		}
	}

	private BasePanel GetPanel(UIPanelType panelType)
	{
		if (panelDict == null)
		{
			panelDict = new Dictionary<UIPanelType, BasePanel>();
		}
		BasePanel basePanel = panelDict.TryGet(panelType);
		if (basePanel == null)
		{
			panelDict.Remove(panelType);
			string path = panelPathDict.TryGet(panelType);
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load(path)) as GameObject;
			gameObject.transform.SetParent(CanvasTransform, worldPositionStays: false);
			panelDict.Add(panelType, gameObject.GetComponent<BasePanel>());
			return gameObject.GetComponent<BasePanel>();
		}
		return basePanel;
	}

	private void ParseUIPanelTypeJson()
	{
		panelPathDict = new Dictionary<UIPanelType, string>();
		TextAsset textAsset = Resources.Load<TextAsset>("UIPanelType");
		UIPanelTypeJson uIPanelTypeJson = JsonUtility.FromJson<UIPanelTypeJson>(textAsset.text);
		foreach (UIPanelInfo info in uIPanelTypeJson.infoList)
		{
			panelPathDict.Add(info.panelType, info.path);
		}
	}

	public void Test()
	{
	}
}
