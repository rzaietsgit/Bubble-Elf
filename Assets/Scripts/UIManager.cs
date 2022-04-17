using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
	private class UIInfoData
	{
		public EnumUIType UIType
		{
			get;
			private set;
		}

		public Type ScriptType
		{
			get;
			private set;
		}

		public string Path
		{
			get;
			private set;
		}

		public object[] UIParams
		{
			get;
			private set;
		}

		public UIInfoData(EnumUIType _uiType, string _path, params object[] _uiParams)
		{
			UIType = _uiType;
			Path = _path;
			UIParams = _uiParams;
			ScriptType = UIPathDefines.GetUIScriptByType(UIType);
		}
	}

	private Dictionary<EnumUIType, GameObject> dicOpenUIs;

	private Stack<UIInfoData> stackOpenUIs;

	public static EnumUIType[] oldUI = new EnumUIType[10];

	public static EnumUIType MemoryUI = EnumUIType.None;

	public EnumUIType OtherOpenUI;

	public override void Init()
	{
		dicOpenUIs = new Dictionary<EnumUIType, GameObject>();
		stackOpenUIs = new Stack<UIInfoData>();
	}

	public T GetUI<T>(EnumUIType _uiType) where T : BaseUI
	{
		GameObject uIObject = GetUIObject(_uiType);
		if (uIObject != null)
		{
			return uIObject.GetComponent<T>();
		}
		return (T)null;
	}

	public GameObject GetUIObject(EnumUIType _uiType)
	{
		GameObject value = null;
		if (!dicOpenUIs.TryGetValue(_uiType, out value))
		{
			throw new Exception("dicOpenUIs TryGetValue Failure! _uiType :" + _uiType.ToString());
		}
		return value;
	}

	public void PreloadUI(EnumUIType[] _uiTypes)
	{
		for (int i = 0; i < _uiTypes.Length; i++)
		{
			PreloadUI(_uiTypes[i]);
		}
	}

	public void PreloadUI(EnumUIType _uiType)
	{
		string prefabPathByType = UIPathDefines.GetPrefabPathByType(_uiType);
		Resources.Load(prefabPathByType);
	}

	public void OpenUI(EnumUIType[] uiTypes)
	{
		if (uiTypes[0] == EnumUIType.BuyGoldUI && InitGame.bChinaVersion)
		{
			uiTypes[0] = EnumUIType.ChinaShopUI;
		}
		OpenUI(_isCloseOthers: false, uiTypes, null);
	}

	public void OpenUI(EnumUIType uiType, params object[] uiObjParams)
	{
		if (Singleton<LevelManager>.Instance.bBossHuang && uiType == EnumUIType.WinUI)
		{
			if (Singleton<LevelManager>.Instance.bBossHuang && Singleton<LevelManager>.Instance.bBossHuangReward)
			{
				Singleton<LevelManager>.Instance.bBossHuangReward = false;
				HuaGame.action.AddJianKangDu(50);
				GameUI.action.BossReward();
				if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "HuangchongbossFirst") == 0)
				{
					Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "HuangchongbossFirst", 1);
				}
			}
		}
		else
		{
			if (Singleton<DataManager>.Instance.bGrilMoveing)
			{
				return;
			}
			if (Singleton<DataManager>.Instance.bGooglePay && uiType == EnumUIType.WinUI && !Singleton<DataManager>.Instance.bRateUsUIOpen && Singleton<DataManager>.Instance.GetUserDataI("DB_Google_Score") == 0 && Singleton<LevelManager>.Instance.iNowStar == 3)
			{
				int iNowSelectLevelIndex = Singleton<LevelManager>.Instance.iNowSelectLevelIndex;
				if ((iNowSelectLevelIndex == 7 || iNowSelectLevelIndex % 20 == 0) && !Singleton<DataManager>.Instance.bChinaIos)
				{
					uiType = EnumUIType.RateUsUI;
				}
			}
			if ((bool)BaseUIAnimation.action)
			{
				BaseUIAnimation.action.bbtnDown = false;
			}
			switch (uiType)
			{
			case EnumUIType.PlayUI:
				if ((bool)SoundController.action)
				{
					SoundController.action.playNow("ui_popup_start_level_appear");
				}
				break;
			default:
				if ((bool)SoundController.action)
				{
					SoundController.action.playNow("ui_info_swoop");
				}
				break;
			case EnumUIType.GuideMaxUI:
			case EnumUIType.GuideMinUI:
			case EnumUIType.ChinaShopUI:
				break;
			}
			switch (uiType)
			{
			}
			if ((!SettingPanelUI.action || !SettingPanelUI.bSettingPanelUIOpen || uiType == EnumUIType.LanguageUI || uiType == EnumUIType.MapRewardUI || uiType == EnumUIType.cdkeyUI || uiType == EnumUIType.aboutChinaUI || uiType == EnumUIType.BuyDaojuUI) && (!Singleton<DataManager>.Instance.bUiIsOpen || uiType == EnumUIType.LanguageUI || uiType == EnumUIType.cdkeyUI || uiType == EnumUIType.MapRewardUI || uiType == EnumUIType.BuyDaojuUI || uiType == EnumUIType.aboutChinaUI))
			{
				Singleton<DataManager>.Instance.bUiIsOpen = true;
				if ((bool)MapUI.action)
				{
					MapUI.action.ResInvitationObj();
				}
				OpenUI(_isCloseOthers: false, new EnumUIType[1]
				{
					uiType
				}, uiObjParams);
			}
		}
	}

	public void OpenUICloseOthers(EnumUIType[] uiTypes)
	{
		OpenUI(_isCloseOthers: true, uiTypes, null);
	}

	public void OpenUICloseOthers(EnumUIType uiType, params object[] uiObjParams)
	{
		OpenUI(_isCloseOthers: true, new EnumUIType[1]
		{
			uiType
		}, uiObjParams);
	}

	private void OpenUI(bool _isCloseOthers, EnumUIType[] _uiTypes, params object[] _uiParams)
	{
		if (_isCloseOthers)
		{
			CloseUIAll();
		}
		for (int i = 0; i < _uiTypes.Length; i++)
		{
			EnumUIType enumUIType = _uiTypes[i];
			if (i == 1)
			{
				for (int j = 0; j < 5; j++)
				{
					if (oldUI[j] == EnumUIType.TestOne || oldUI[j] == EnumUIType.None)
					{
						oldUI[j] = _uiTypes[i];
						break;
					}
				}
			}
			if (!dicOpenUIs.ContainsKey(enumUIType))
			{
				string prefabPathByType = UIPathDefines.GetPrefabPathByType(enumUIType);
				stackOpenUIs.Push(new UIInfoData(enumUIType, prefabPathByType, _uiParams));
			}
		}
		if (stackOpenUIs.Count > 0)
		{
			Singleton<DataManager>.Instance.bUiIsOpen = true;
			if ((bool)MapUI.action)
			{
				MapUI.action.ResInvitationObj();
			}
			if ((bool)MapManagerUI.action)
			{
				MapManagerUI.action.ResDoubleClick();
			}
			DDOLSingleton<CoroutineController>.Instance.StartCoroutine(AsyncLoadData());
		}
	}

	private IEnumerator<int> AsyncLoadData()
	{
		if (stackOpenUIs != null && stackOpenUIs.Count > 0)
		{
			do
			{
				UIInfoData _uiInfoData;
				UnityEngine.Object _prefabObj;
				try
				{
					_uiInfoData = stackOpenUIs.Pop();
					_prefabObj = Resources.Load(_uiInfoData.Path);
				}
				catch (Exception arg)
				{
					UnityEngine.Debug.Log("AsyncLoadData error +" + arg);
					goto IL_0152;
				}
				if (_prefabObj != null)
				{
					GameObject _uiObject = UnityEngine.Object.Instantiate(_prefabObj) as GameObject;
					BaseUI baseUI = _uiObject.GetComponent<BaseUI>();
					if (null == baseUI)
					{
						baseUI = (_uiObject.AddComponent(_uiInfoData.ScriptType) as BaseUI);
					}
					if (null != baseUI)
					{
						baseUI.SetUIWhenOpening(_uiInfoData.UIParams);
					}
					dicOpenUIs.Add(_uiInfoData.UIType, _uiObject);
				}
			}
			while (stackOpenUIs.Count > 0);
		}
		goto IL_0152;
		IL_0152:
		yield return 0;
	}

	public void CloseUI(EnumUIType _uiType, bool bDouble = false)
	{
		if (!bDouble && _uiType != EnumUIType.GuideMinUI)
		{
			Singleton<DataManager>.Instance.bUiIsOpen = false;
			if ((bool)MapUI.action)
			{
				MapUI.action.ResInvitationObj();
			}
		}
		GameObject value = null;
		if (dicOpenUIs.TryGetValue(_uiType, out value))
		{
			CloseUI(_uiType, value);
		}
	}

	public void CloseUI(EnumUIType[] _uiTypes)
	{
		for (int i = 0; i < _uiTypes.Length; i++)
		{
			CloseUI(_uiTypes[i]);
		}
	}

	public void CloseUIAll()
	{
		List<EnumUIType> list = new List<EnumUIType>(dicOpenUIs.Keys);
		for (int i = 0; i < list.Count; i++)
		{
			EnumUIType enumUIType = list[i];
			if (enumUIType != EnumUIType.LoadingSceneUI)
			{
				GameObject uiObj = dicOpenUIs[enumUIType];
				CloseUI(enumUIType, uiObj);
			}
		}
		dicOpenUIs.Clear();
	}

	private void CloseUI(EnumUIType _uiType, GameObject _uiObj)
	{
		if (_uiObj == null)
		{
			dicOpenUIs.Remove(_uiType);
		}
		else
		{
			BaseUI component = _uiObj.GetComponent<BaseUI>();
			if (component != null)
			{
				component.StateChanged += CloseUIHandler;
				component.Release();
			}
			else
			{
				UnityEngine.Object.Destroy(_uiObj);
				dicOpenUIs.Remove(_uiType);
			}
		}
		int num = 4;
		while (true)
		{
			if (num >= 0)
			{
				if (oldUI[num] != 0 && oldUI[num] != EnumUIType.None)
				{
					break;
				}
				num--;
				continue;
			}
			return;
		}
		if (_uiType != oldUI[num] && _uiType != EnumUIType.LoadingbyOut)
		{
			EnumUIType uiType = oldUI[num];
			oldUI[num] = EnumUIType.None;
			OpenUI(uiType);
		}
	}

	private void CloseUIHandler(object _sender, EnumObjectState _newState, EnumObjectState _oldState)
	{
		if (_newState == EnumObjectState.Closing)
		{
			BaseUI baseUI = _sender as BaseUI;
			dicOpenUIs.Remove(baseUI.GetUIType());
			baseUI.StateChanged -= CloseUIHandler;
		}
	}
}
