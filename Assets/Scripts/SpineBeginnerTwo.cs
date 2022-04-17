using Spine;
using Spine.Unity;
using System.Collections;
using UnityEngine;

public class SpineBeginnerTwo : MonoBehaviour
{
	[SpineAnimation("", "")]
	public string runAnimationName;

	[SpineAnimation("", "")]
	public string idleAnimationName;

	[SpineAnimation("", "")]
	public string walkAnimationName;

	[SpineAnimation("", "")]
	public string shootAnimationName;

	private SkeletonAnimation skeletonAnimation;

	public Spine.AnimationState spineAnimationState;

	public Skeleton skeleton;

	private void Start()
	{
		skeletonAnimation = GetComponent<SkeletonAnimation>();
		spineAnimationState = skeletonAnimation.state;
		skeleton = skeletonAnimation.skeleton;
		StartCoroutine(DoDemoRoutine());
	}

	private IEnumerator DoDemoRoutine()
	{
		while (true)
		{
			spineAnimationState.SetAnimation(0, walkAnimationName, loop: true);
			yield return new WaitForSeconds(1.5f);
			spineAnimationState.SetAnimation(0, runAnimationName, loop: true);
			yield return new WaitForSeconds(1.5f);
			spineAnimationState.SetAnimation(0, idleAnimationName, loop: true);
			yield return new WaitForSeconds(1f);
			skeleton.FlipX = true;
			yield return new WaitForSeconds(0.5f);
			skeleton.FlipX = false;
			yield return new WaitForSeconds(0.5f);
		}
	}
}
