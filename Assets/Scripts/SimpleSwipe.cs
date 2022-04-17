using Lean;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleSwipe : MonoBehaviour
{
	public LayerMask LayerMask = -5;

	public float ForceMultiplier = 1f;

	protected virtual void OnEnable()
	{
		LeanTouch.OnFingerSwipe = (Action<LeanFinger>)Delegate.Combine(LeanTouch.OnFingerSwipe, new Action<LeanFinger>(OnFingerSwipe));
	}

	protected virtual void OnDisable()
	{
		LeanTouch.OnFingerSwipe = (Action<LeanFinger>)Delegate.Remove(LeanTouch.OnFingerSwipe, new Action<LeanFinger>(OnFingerSwipe));
	}

	public void OnFingerSwipe(LeanFinger finger)
	{
		Ray startRay = finger.GetStartRay();
		RaycastHit hitInfo = default(RaycastHit);
		if (Physics.Raycast(startRay, out hitInfo, float.PositiveInfinity, LayerMask) && hitInfo.collider.gameObject == base.gameObject)
		{
			Rigidbody component = GetComponent<Rigidbody>();
			component.AddForce(finger.ScaledSwipeDelta * ForceMultiplier);
		}
	}
}
