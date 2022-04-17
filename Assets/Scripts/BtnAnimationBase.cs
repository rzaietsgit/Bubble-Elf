using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BtnAnimationBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler, IEventSystemHandler
{
	public float interval = 0.1f;

	[SerializeField]
	private UnityEvent m_OnLongpress = new UnityEvent();

	private bool isPointDown;

	private bool isPointDownCheck;

	private float lastInvokeTime;

	private bool isPointExit;

	private float UpBtnTime;

	private TweenCallback action;

	private NewBtnType btnType;

	private void Start()
	{
	}

	public void SetType(NewBtnType type)
	{
		btnType = type;
		if (btnType == NewBtnType.NONE)
		{
			return;
			UpBtnTime = 0.2f;
			base.gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.05f).OnComplete(delegate
			{
				ButtonPingpong();
			});
		}
		else
		{
			UpBtnTime = 0.01f;
		}
	}

	public void SetAction(TweenCallback _action)
	{
		action = _action;
	}

	private void ButtonPingpong()
	{
		return;
		base.gameObject.transform.DOScale(new Vector3(1.04f, 0.95f, 1f), 1f).SetEase(Ease.InOutSine);
		base.gameObject.transform.DOScale(new Vector3(0.95f, 1.04f, 1f), 1.2f).SetEase(Ease.InOutSine).SetDelay(1f)
			.OnComplete(delegate
			{
				ButtonPingpong();
			});
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		return;
		UnityEngine.Debug.Log("OnPointerDown");
		if (!isPointDown && !isPointDownCheck)
		{
			isPointDownCheck = true;
			m_OnLongpress.Invoke();
			isPointDown = true;
			isPointExit = false;
			lastInvokeTime = Time.time;
			if (btnType == NewBtnType.NONE)
			{
				base.gameObject.transform.DOKill();
				base.gameObject.transform.DOScale(new Vector3(0.7f, 0.7f, 1f), 0.1f);
				base.gameObject.transform.DOScale(new Vector3(0.7f, 0.95f, 1f), 0.1167f);
				base.gameObject.transform.DOScale(new Vector3(0.8f, 0.7f, 1f), 0.1167f);
				base.gameObject.transform.DOScale(new Vector3(0.75f, 0.85f, 1f), 0.083f);
				base.gameObject.transform.DOScale(new Vector3(0.8f, 0.8f, 1f), 0.083f);
			}
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		return;
		isPointDown = false;
		if (btnType == NewBtnType.NONE)
		{
			base.gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), UpBtnTime).OnComplete(delegate
			{
				ButtonPingpong();
			});
		}
		else
		{
			base.gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), UpBtnTime);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		return;
		isPointDownCheck = false;
		isPointDown = false;
		isPointExit = true;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		action?.Invoke();
		return;
		SoundController.action.playNow("ButtonClick");
		UnityEngine.Debug.Log("OnPointerClick1           " + action);
		if (action == null)
		{
			UnityEngine.Debug.Log("OnPointerClick4");
		}
		base.gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), UpBtnTime).OnComplete(action);
		UnityEngine.Debug.Log("OnPointerClick2      " + UpBtnTime);
		base.gameObject.transform.DORotate(Vector3.zero, UpBtnTime).OnComplete(delegate
		{
			isPointDown = false;
			isPointDownCheck = false;
		});
		UnityEngine.Debug.Log("OnPointerClick3");
	}
}
