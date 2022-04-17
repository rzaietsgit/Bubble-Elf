using UnityEngine;

public class LoadingSceneUI : BaseUI
{
	public static LoadingSceneUI action;

	public GameObject LoginObj;

	private void Awake()
	{
		if (action == null)
		{
			Object.DontDestroyOnLoad(base.gameObject);
			action = this;
		}
		else if (action != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		if (Singleton<DataManager>.Instance.bLogo_)
		{
			Singleton<DataManager>.Instance.bLogo_ = false;
		}
		else
		{
			LoginObj.SetActive(value: false);
		}
	}

	public override EnumUIType GetUIType()
	{
		return EnumUIType.LoadingSceneUI;
	}

	private void Start()
	{
		if (!action)
		{
			action = this;
		}
	}

	public void ShowLoadingSceneUI()
	{
		if ((bool)MapUI.action)
		{
			MapUI.action.gameObject.SetActive(value: false);
		}
		if ((bool)SettingPanelUI.action)
		{
			SettingPanelUI.action.gameObject.SetActive(value: false);
		}
		BaseUIAnimation.action.ShowLoadingSceneUI(base.transform.gameObject);
		if ((bool)MusicController.action)
		{
			MusicController.action.LoadingOFF();
		}
		LoginObj.SetActive(value: true);
	}

	public void HideLoadingSceneUI()
	{
		LoginObj.SetActive(value: false);
	}

	public void ChangeCanvasCamera(Component _Component)
	{
		Canvas component = base.transform.GetComponent<Canvas>();
		component.renderMode = RenderMode.ScreenSpaceCamera;
		component.worldCamera = _Component.GetComponent<Camera>();
	}

	private void Update()
	{
	}
}
