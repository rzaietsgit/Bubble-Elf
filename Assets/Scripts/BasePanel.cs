using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
	public CanvasGroup canvasGroup;

	public UIPanelType uiType;

	public virtual UIType GetUIType()
	{
		return UIType.POP;
	}

	public virtual UIPanelType GetUIPanelType()
	{
		return uiType;
	}

	public virtual void OnEnterBase(UIPanelType type)
	{
		if (canvasGroup == null)
		{
			canvasGroup = GetComponent<CanvasGroup>();
		}
		canvasGroup.alpha = 1f;
		canvasGroup.blocksRaycasts = true;
		base.gameObject.SetActive(value: false);
		if (GetUIType() == UIType.POP)
		{
			Vector3 localPosition = base.transform.localPosition;
			localPosition.y = Screen.height;
			base.transform.localPosition = localPosition;
			uiType = type;
			base.gameObject.SetActive(value: true);
			StartCoroutine(OnEnter());
		}
		else if (GetUIType() == UIType.DOWN)
		{
			uiType = type;
			base.gameObject.SetActive(value: true);
		}
		else
		{
			uiType = type;
			base.transform.localPosition = new Vector3(0f, 0f, 0f);
			base.gameObject.SetActive(value: true);
		}
	}

	private IEnumerator OnEnter()
	{
		SoundController.action.playNow("ButtonClick");
		base.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(0.1f);
		base.transform.DOLocalMoveY(0f, 0.2f).SetEase(Ease.InOutSine);
	}

	public virtual void OnPauseBase()
	{
		if (canvasGroup != null)
		{
			canvasGroup.blocksRaycasts = false;
		}
		base.transform.DOLocalMoveY(-Screen.height, 0.2f).SetEase(Ease.InSine).OnComplete(OnPause)
			.OnComplete(delegate
			{
				canvasGroup.alpha = 0f;
			});
	}

	public virtual void OnPause()
	{
	}

	public virtual void OnResumeBase()
	{
		canvasGroup.blocksRaycasts = true;
		canvasGroup.alpha = 1f;
		if (GetUIType() == UIType.POP)
		{
			Vector3 localPosition = base.transform.localPosition;
			localPosition.y = Screen.height;
			base.transform.localPosition = localPosition;
			base.transform.DOLocalMoveY(0f, 0.2f).SetEase(Ease.InOutSine).OnComplete(OnResume);
		}
		else if (GetUIType() == UIType.DOWN)
		{
			OnResume();
		}
		else
		{
			base.transform.localPosition = new Vector3(0f, 0f, 0f);
			OnResume();
		}
	}

	public virtual void OnResume()
	{
	}

	public void OnExitBase(bool isShowExit, bool closeMsk)
	{
		if (isShowExit && GetUIType() == UIType.POP)
		{
			base.transform.DOLocalMoveY(-Screen.height, 0.2f).SetEase(Ease.InSine).OnComplete(delegate
			{
				OnExitBase1(closeMsk);
			});
		}
		else
		{
			OnExitBase1(closeMsk);
		}
	}

	public void OnExitBase1(bool closeMsk)
	{
		if (closeMsk && UI.Instance.MaskTransform != null)
		{
			UI.Instance.MaskTransform.SetActive(value: false);
		}
		canvasGroup.blocksRaycasts = false;
		OnExit();
		UnityEngine.Object.DestroyObject(base.gameObject);
	}

	public virtual void OnExit()
	{
	}

	private void Update()
	{
		try
		{
			OnUpdate();
		}
		catch (Exception arg)
		{
			UnityEngine.Debug.LogError("UI Update error :" + arg);
		}
	}

	public virtual void OnUpdate()
	{
	}
}
