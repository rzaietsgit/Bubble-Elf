using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
	private Transform _CachedTransform;

	private GameObject _CachedGameObject;

	protected EnumObjectState state;

	public Transform cachedTransform
	{
		get
		{
			if (!_CachedTransform)
			{
				_CachedTransform = base.transform;
			}
			return _CachedTransform;
		}
	}

	public GameObject cachedGameObject
	{
		get
		{
			if (!_CachedGameObject)
			{
				_CachedGameObject = base.gameObject;
			}
			return _CachedGameObject;
		}
	}

	public EnumObjectState State
	{
		get
		{
			return state;
		}
		protected set
		{
			if (value != state)
			{
				EnumObjectState oldState = state;
				state = value;
				if (this.StateChanged != null)
				{
					this.StateChanged(this, state, oldState);
				}
			}
		}
	}

	public event StateChangedEvent StateChanged;

	public abstract EnumUIType GetUIType();

	protected virtual void SetDepthToTop()
	{
	}

	private void Start()
	{
		if (GetUIType() != EnumUIType.SignReward7UI && GetUIType() != EnumUIType.ChinaShopUI)
		{
			BaseUIAnimation.action.StartAnimation(base.gameObject);
		}
		OnStart();
	}

	private void Awake()
	{
		State = EnumObjectState.Initial;
		OnAwake();
	}

	private void Update()
	{
		if (state == EnumObjectState.Ready)
		{
			OnUpdate(Time.deltaTime);
		}
	}

	public void Release()
	{
		State = EnumObjectState.Closing;
		UnityEngine.Object.Destroy(cachedGameObject);
		OnRelease();
	}

	public virtual void OnStart()
	{
	}

	protected virtual void OnAwake()
	{
		State = EnumObjectState.Loading;
		OnPlayOpenUIAudio();
	}

	protected virtual void OnUpdate(float deltaTime)
	{
	}

	protected virtual void OnRelease()
	{
		OnPlayCloseUIAudio();
		BaseUIAnimation.action.HieMohu();
	}

	protected virtual void OnPlayOpenUIAudio()
	{
	}

	protected virtual void OnPlayCloseUIAudio()
	{
	}

	protected virtual void SetUI(params object[] uiParams)
	{
		State = EnumObjectState.Loading;
	}

	public virtual void SetUIparam(params object[] uiParams)
	{
	}

	protected virtual void OnLoadData()
	{
	}

	public void SetUIWhenOpening(params object[] uiParams)
	{
		SetUI(uiParams);
	}

	private IEnumerator AsyncOnLoadData()
	{
		yield return new WaitForSeconds(0f);
		if (State == EnumObjectState.Loading)
		{
			OnLoadData();
			State = EnumObjectState.Ready;
		}
	}

	public void CloseUI(bool bDouble = false, bool bOpenOther = false, bool bChangeScenes = false)
	{
		if (GetUIType() == EnumUIType.HuaShopUI)
		{
			HuaGame.action.ShowGuide(6);
		}
		Singleton<DataManager>.Instance.bBuyLi1 = false;
		Singleton<DataManager>.Instance.bBuyLi2 = false;
		if (GetUIType() == EnumUIType.SaleAdUI)
		{
			UnityEngine.Debug.Log("111111111111");
		}
		if (GetUIType() == EnumUIType.GuideMinUI || GetUIType() == EnumUIType.GuideMaxUI)
		{
			if (Singleton<DataManager>.Instance.bopenMaxGuide)
			{
				GameGuide.Instance.nextGuide();
				Singleton<DataManager>.Instance.bopenMaxGuide = false;
			}
		}
		else if (GetUIType() == EnumUIType.PlayUI)
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("ui_popup_start_level_disappear");
			}
		}
		else if (GetUIType() != EnumUIType.ChinaShopUI && (bool)SoundController.action)
		{
			SoundController.action.playNow("ui_Recover_swoop");
		}
		if (GetUIType() == EnumUIType.ReadyGoUI || GetUIType() == EnumUIType.ReadyWinUI || GetUIType() == EnumUIType.TipFailUI || GetUIType() == EnumUIType.GuideMinUI || GetUIType() == EnumUIType.GuideMaxUI)
		{
		}
		if (GetUIType() == EnumUIType.SignReward7UI)
		{
			if ((bool)OpenScript.actionSevLogin1)
			{
				OpenScript.actionSevLogin1.ResSev7Login();
			}
			StartCoroutine(CLoseSign7());
			Singleton<UIManager>.Instance.CloseUI(EnumUIType.SignReward7UI);
			if (Singleton<DataManager>.Instance.Openplay1)
			{
				Singleton<DataManager>.Instance.Openplay1 = false;
				Singleton<UIManager>.Instance.OpenUI(EnumUIType.PlayUI);
			}
			if (Singleton<UserManager>.Instance.bOpenHuaUI())
			{
				Singleton<UIManager>.Instance.OpenUI(EnumUIType.HuaShowUI);
			}
		}
		else if (GetUIType() == EnumUIType.ChinaShopUI)
		{
			StartCoroutine(CLoseSign7());
			Singleton<UIManager>.Instance.CloseUI(EnumUIType.ChinaShopUI);
			if (Singleton<DataManager>.Instance.bAutoPlayGame)
			{
				Singleton<UIManager>.Instance.OpenUI(EnumUIType.PlayUI);
				return;
			}
			if (bOpenOther || (bool)MapUI.action)
			{
			}
			if (Singleton<DataManager>.Instance.bopenBuySkillUI1)
			{
				Singleton<DataManager>.Instance.bopenBuySkillUI1 = false;
				UI.Instance.OpenPanel(UIPanelType.BuySkillUI);
			}
			if (Singleton<DataManager>.Instance.bBuyLiveSale)
			{
				Singleton<DataManager>.Instance.bBuyLiveSale = false;
				if (Singleton<DataManager>.Instance.EBuyLiveSale != EnumUIType.None)
				{
					Singleton<UIManager>.Instance.OpenUI(Singleton<DataManager>.Instance.EBuyLiveSale);
					Singleton<DataManager>.Instance.EBuyLiveSale = EnumUIType.None;
				}
			}
		}
		else
		{
			BaseUIAnimation.action.ReleaseAnimation(base.gameObject, GetUIType(), bDouble, bOpenOther, bChangeScenes);
		}
	}

	public IEnumerator CLoseSign7()
	{
		yield return new WaitForSeconds(1f);
		Singleton<DataManager>.Instance.bOpenReward7 = false;
	}
}
