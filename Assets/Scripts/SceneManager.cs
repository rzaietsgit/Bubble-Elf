using System;
using System.Collections;
using System.Collections.Generic;
using EasyMobile;
using ITSoft;
using UnityEngine;

public class SceneManager : Singleton<SceneManager>
{
	public class SceneInfoData
	{
		public string SceneName
		{
			get;
			private set;
		}

		public object[] Params
		{
			get;
			private set;
		}

		public SceneInfoData(string _sceneName, params object[] _params)
		{
			SceneName = _sceneName;
			Params = _params;
		}
	}

	public EnumSceneType WaitChangeScene;

	private Dictionary<EnumSceneType, SceneInfoData> dicSceneInfos;

	public int GetGameScene()
	{
		if (Singleton<DataManager>.Instance.ChangeSceneType == EnumSceneType.GameScene)
		{
			return 1;
		}
		return 0;
	}

	public void RegisterScene(EnumSceneType _sceneID, string _sceneName, Type _sceneType, params object[] _params)
	{
		if (!dicSceneInfos.ContainsKey(_sceneID))
		{
			SceneInfoData value = new SceneInfoData(_sceneName, _sceneType, _params);
			dicSceneInfos.Add(_sceneID, value);
		}
	}

	public string GetSceneName(EnumSceneType _sceneID)
	{
		if (dicSceneInfos.ContainsKey(_sceneID))
		{
			return dicSceneInfos[_sceneID].SceneName;
		}
		return null;
	}

	public void InitSceneInfo()
	{
		if (dicSceneInfos == null)
		{
			dicSceneInfos = new Dictionary<EnumSceneType, SceneInfoData>();
			Singleton<SceneManager>.Instance.RegisterScene(EnumSceneType.LoginScene, "LoginScene", null);
			Singleton<SceneManager>.Instance.RegisterScene(EnumSceneType.MapScene, "MapScene", null);
			Singleton<SceneManager>.Instance.RegisterScene(EnumSceneType.GameScene, "GameScene", null);
			Singleton<SceneManager>.Instance.RegisterScene(EnumSceneType.InitGame, "InitGame", null);
		}
	}

	public IEnumerator IEChangeScene(EnumSceneType _SceneType)
	{
		yield return new WaitForSeconds(0.1f);
		ChangeScene(_SceneType);
	}

	public void ChangeScene(EnumSceneType _SceneType)
	{
		if (_SceneType == EnumSceneType.MapScene && Singleton<LevelManager>.Instance.iNowSelectLevelIndex >= 80000)
		{
			Singleton<LevelManager>.Instance.iNowSelectLevelIndex = Singleton<UserManager>.Instance.iNowPassLevelID;
			if (Singleton<UserManager>.Instance.iNowPassLevelID <= 0)
			{
				Singleton<LevelManager>.Instance.iNowSelectLevelIndex = 1;
			}
			if (Singleton<DataManager>.Instance.ChangeSceneType == EnumSceneType.GameScene)
			{
				_SceneType = EnumSceneType.HuaScene;
			}
		}
        AdsManager.HideBanner();
        Singleton<DataManager>.Instance.ChangeSceneType = _SceneType;
		Singleton<DataManager>.Instance.bPayState = false;
		if ((bool)LoadingSceneUI.action)
		{
			LoadingSceneUI.action.ShowLoadingSceneUI();
		}
		Singleton<UIManager>.Instance.CloseUIAll();
		Singleton<DataManager>.Instance.bUiIsOpen = false;
		if ((bool)MapUI.action)
		{
			MapUI.action.ResInvitationObj();
		}
		UnityEngine.SceneManagement.SceneManager.LoadScene("LoadingScene");
	}
}
