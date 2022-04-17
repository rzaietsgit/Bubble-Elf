using Spine;
using Spine.Unity;
using System.Collections;
using UnityEngine;

public class Raptor : MonoBehaviour
{
	[SpineAnimation("", "")]
	public string walk = "walk";

	[SpineAnimation("", "")]
	public string gungrab = "gungrab";

	[SpineAnimation("", "")]
	public string gunkeep = "gunkeep";

	[SpineEvent("", "")]
	public string footstepEvent = "footstep";

	public AudioSource footstepAudioSource;

	private SkeletonAnimation skeletonAnimation;

	private void Start()
	{
		skeletonAnimation = GetComponent<SkeletonAnimation>();
		skeletonAnimation.state.Event += HandleEvent;
		StartCoroutine(GunGrabRoutine());
	}

	private void HandleEvent(Spine.AnimationState state, int trackIndex, Spine.Event e)
	{
		if (e.Data.Name == footstepEvent)
		{
			footstepAudioSource.pitch = 0.5f + UnityEngine.Random.Range(-0.2f, 0.2f);
			footstepAudioSource.Play();
		}
	}

	private IEnumerator GunGrabRoutine()
	{
		skeletonAnimation.state.SetAnimation(0, walk, loop: true);
		while (true)
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 3f));
			skeletonAnimation.state.SetAnimation(1, gungrab, loop: false);
			yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 3f));
			skeletonAnimation.state.SetAnimation(1, gunkeep, loop: false);
		}
	}
}
