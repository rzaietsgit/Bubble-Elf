using Spine.Unity;
using System.Collections;
using UnityEngine;

public class SpineBlinkPlayer : MonoBehaviour
{
	private const int BlinkTrack = 1;

	[SpineAnimation("", "")]
	public string blinkAnimation;

	public float minimumDelay = 0.15f;

	public float maximumDelay = 3f;

	private IEnumerator Start()
	{
		SkeletonAnimation skeletonAnimation = GetComponent<SkeletonAnimation>();
		if (skeletonAnimation == null)
		{
			yield break;
		}
		while (true)
		{
			skeletonAnimation.state.SetAnimation(1, blinkAnimation, loop: false);
			yield return new WaitForSeconds(UnityEngine.Random.Range(minimumDelay, maximumDelay));
		}
	}
}
