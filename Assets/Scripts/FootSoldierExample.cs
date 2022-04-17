using Spine.Unity;
using System;
using System.Collections;
using UnityEngine;

public class FootSoldierExample : MonoBehaviour
{
	[SpineAnimation("Idle", "")]
	public string idleAnimation;

	[SpineAnimation("", "")]
	public string attackAnimation;

	[SpineAnimation("", "")]
	public string moveAnimation;

	[SpineSlot("", "", false)]
	public string eyesSlot;

	[SpineAttachment(true, false, false, "eyesSlot", "")]
	public string eyesOpenAttachment;

	[SpineAttachment(true, false, false, "eyesSlot", "")]
	public string blinkAttachment;

	[Range(0f, 0.2f)]
	public float blinkDuration = 0.05f;

	public KeyCode attackKey = KeyCode.Mouse0;

	public KeyCode rightKey = KeyCode.D;

	public KeyCode leftKey = KeyCode.A;

	public float moveSpeed = 3f;

	private SkeletonAnimation skeletonAnimation;

	private void Awake()
	{
		skeletonAnimation = GetComponent<SkeletonAnimation>();
		SkeletonAnimation obj = skeletonAnimation;
		obj.OnRebuild = (SkeletonRenderer.SkeletonRendererDelegate)Delegate.Combine(obj.OnRebuild, new SkeletonRenderer.SkeletonRendererDelegate(Apply));
	}

	private void Apply(SkeletonRenderer skeletonRenderer)
	{
		StartCoroutine("Blink");
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKey(attackKey))
		{
			skeletonAnimation.AnimationName = attackAnimation;
		}
		else if (UnityEngine.Input.GetKey(rightKey))
		{
			skeletonAnimation.AnimationName = moveAnimation;
			skeletonAnimation.skeleton.FlipX = false;
			base.transform.Translate(moveSpeed * Time.deltaTime, 0f, 0f);
		}
		else if (UnityEngine.Input.GetKey(leftKey))
		{
			skeletonAnimation.AnimationName = moveAnimation;
			skeletonAnimation.skeleton.FlipX = true;
			base.transform.Translate((0f - moveSpeed) * Time.deltaTime, 0f, 0f);
		}
		else
		{
			skeletonAnimation.AnimationName = idleAnimation;
		}
	}

	private IEnumerator Blink()
	{
		while (true)
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(0.25f, 3f));
			skeletonAnimation.skeleton.SetAttachment(eyesSlot, blinkAttachment);
			yield return new WaitForSeconds(blinkDuration);
			skeletonAnimation.skeleton.SetAttachment(eyesSlot, eyesOpenAttachment);
		}
	}
}
