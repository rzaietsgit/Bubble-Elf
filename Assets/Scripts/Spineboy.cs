using Spine;
using Spine.Unity;
using UnityEngine;

public class Spineboy : MonoBehaviour
{
	private SkeletonAnimation skeletonAnimation;

	public void Start()
	{
		skeletonAnimation = GetComponent<SkeletonAnimation>();
		skeletonAnimation.state.Event += Event;
		skeletonAnimation.state.End += delegate(Spine.AnimationState state, int trackIndex)
		{
			UnityEngine.Debug.Log("start: " + state.GetCurrent(trackIndex));
		};
		skeletonAnimation.state.AddAnimation(0, "jump", loop: false, 2f);
		skeletonAnimation.state.AddAnimation(0, "run", loop: true, 0f);
	}

	public void Event(Spine.AnimationState state, int trackIndex, Spine.Event e)
	{
		UnityEngine.Debug.Log(trackIndex + " " + state.GetCurrent(trackIndex) + ": event " + e + ", " + e.Int);
	}

	public void OnMouseDown()
	{
		skeletonAnimation.state.SetAnimation(0, "jump", loop: false);
		skeletonAnimation.state.AddAnimation(0, "run", loop: true, 0f);
	}
}
