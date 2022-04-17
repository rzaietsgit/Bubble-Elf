using Spine;
using Spine.Unity;
using UnityEngine;

public class SpineboyBeginnerView : MonoBehaviour
{
	[Header("Components")]
	public SpineboyBeginnerModel model;

	public SkeletonAnimation skeletonAnimation;

	[SpineAnimation("", "")]
	public string run;

	[SpineAnimation("", "")]
	public string idle;

	[SpineAnimation("", "")]
	public string shoot;

	[SpineAnimation("", "")]
	public string jump;

	[SpineEvent("", "")]
	public string footstepEventName;

	[Header("Audio")]
	public float footstepPitchOffset = 0.2f;

	public float gunsoundPitchOffset = 0.13f;

	public AudioSource footstepSource;

	public AudioSource gunSource;

	public AudioSource jumpSource;

	[Header("Effects")]
	public ParticleSystem gunParticles;

	private SpineBeginnerBodyState previousViewState;

	private void Start()
	{
		if (!(skeletonAnimation == null))
		{
			model.ShootEvent += PlayShoot;
			skeletonAnimation.state.Event += HandleEvent;
		}
	}

	private void HandleEvent(Spine.AnimationState state, int trackIndex, Spine.Event e)
	{
		if (e.Data.Name == footstepEventName)
		{
			PlayFootstepSound();
		}
	}

	private void Update()
	{
		if (!(skeletonAnimation == null) && !(model == null))
		{
			if (skeletonAnimation.skeleton.FlipX != model.facingLeft)
			{
				Turn(model.facingLeft);
			}
			SpineBeginnerBodyState state = model.state;
			if (previousViewState != state)
			{
				PlayNewStableAnimation();
			}
			previousViewState = state;
		}
	}

	private void PlayNewStableAnimation()
	{
		SpineBeginnerBodyState state = model.state;
		if (previousViewState == SpineBeginnerBodyState.Jumping && state != SpineBeginnerBodyState.Jumping)
		{
			PlayFootstepSound();
		}
		string animationName;
		switch (state)
		{
		case SpineBeginnerBodyState.Jumping:
			jumpSource.Play();
			animationName = jump;
			break;
		case SpineBeginnerBodyState.Running:
			animationName = run;
			break;
		default:
			animationName = idle;
			break;
		}
		skeletonAnimation.state.SetAnimation(0, animationName, loop: true);
	}

	private void PlayFootstepSound()
	{
		footstepSource.Play();
		footstepSource.pitch = GetRandomPitch(footstepPitchOffset);
	}

	public void PlayShoot()
	{
		skeletonAnimation.state.SetAnimation(1, shoot, loop: false);
		gunSource.pitch = GetRandomPitch(gunsoundPitchOffset);
		gunSource.Play();
		gunParticles.Play();
	}

	public void Turn(bool facingLeft)
	{
		skeletonAnimation.skeleton.FlipX = facingLeft;
	}

	public float GetRandomPitch(float maxPitchOffset)
	{
		return 1f + UnityEngine.Random.Range(0f - maxPitchOffset, maxPitchOffset);
	}
}
