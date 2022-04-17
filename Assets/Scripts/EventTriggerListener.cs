using UnityEngine;
using UnityEngine.EventSystems;

public class EventTriggerListener : EventTrigger
{
	public delegate void VoidDelegate(GameObject go);

	public delegate void BoolDelegate(GameObject go, bool state);

	public delegate void FloatDelegate(GameObject go, float delta);

	public delegate void VectorDelegate(GameObject go, Vector2 delta);

	public delegate void ObjectDelegate(GameObject go, GameObject obj);

	public delegate void KeyCodeDelegate(GameObject go, KeyCode key);

	public VoidDelegate onClick;

	public VoidDelegate onDown;

	public VoidDelegate onEnter;

	public VoidDelegate onExit;

	public VoidDelegate onUp;

	public VoidDelegate onSelect;

	public VoidDelegate onUpdateSelect;

	public static EventTriggerListener Get(GameObject go)
	{
		EventTriggerListener eventTriggerListener = go.GetComponent<EventTriggerListener>();
		if (eventTriggerListener == null)
		{
			eventTriggerListener = go.AddComponent<EventTriggerListener>();
		}
		return eventTriggerListener;
	}

	public static EventTriggerListener Get(Transform transform)
	{
		EventTriggerListener eventTriggerListener = transform.GetComponent<EventTriggerListener>();
		if (eventTriggerListener == null)
		{
			eventTriggerListener = transform.gameObject.AddComponent<EventTriggerListener>();
		}
		return eventTriggerListener;
	}

	public override void OnPointerClick(PointerEventData eventData)
	{
		if (onClick != null)
		{
			onClick(base.gameObject);
		}
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
		if (onDown != null)
		{
			onDown(base.gameObject);
		}
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		if (onEnter != null)
		{
			onEnter(base.gameObject);
		}
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		if (onExit != null)
		{
			onExit(base.gameObject);
		}
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		if (onUp != null)
		{
			onUp(base.gameObject);
		}
	}

	public override void OnSelect(BaseEventData eventData)
	{
		if (onSelect != null)
		{
			onSelect(base.gameObject);
		}
	}

	public override void OnUpdateSelected(BaseEventData eventData)
	{
		if (onUpdateSelect != null)
		{
			onUpdateSelect(base.gameObject);
		}
	}
}
