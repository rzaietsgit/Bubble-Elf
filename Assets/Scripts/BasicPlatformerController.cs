using Spine;
using Spine.Unity;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BasicPlatformerController : MonoBehaviour
{
	public string XAxis = "Horizontal";

	public string YAxis = "Vertical";

	public string JumpButton = "Jump";

	public float walkSpeed = 4f;

	public float runSpeed = 10f;

	public float gravity = 65f;

	public float jumpSpeed = 25f;

	public float jumpDuration = 0.5f;

	public float jumpInterruptFactor = 100f;

	public float forceCrouchVelocity = 25f;

	public float forceCrouchDuration = 0.5f;

	public Transform graphicsRoot;

	public SkeletonAnimation skeletonAnimation;

	[SpineAnimation("", "skeletonAnimation")]
	public string walkName = "Walk";

	[SpineAnimation("", "skeletonAnimation")]
	public string runName = "Run";

	[SpineAnimation("", "skeletonAnimation")]
	public string idleName = "Idle";

	[SpineAnimation("", "skeletonAnimation")]
	public string jumpName = "Jump";

	[SpineAnimation("", "skeletonAnimation")]
	public string fallName = "Fall";

	[SpineAnimation("", "skeletonAnimation")]
	public string crouchName = "Crouch";

	public AudioSource jumpAudioSource;

	public AudioSource hardfallAudioSource;

	public AudioSource footstepAudioSource;

	[SpineEvent("", "")]
	public string footstepEventName = "Footstep";

	private CharacterController controller;

	private Vector2 velocity = Vector2.zero;

	private Vector2 lastVelocity = Vector2.zero;

	private bool lastGrounded;

	private float jumpEndTime;

	private bool jumpInterrupt;

	private float forceCrouchEndTime;

	private Quaternion flippedRotation = Quaternion.Euler(0f, 180f, 0f);

	private void Awake()
	{
		controller = GetComponent<CharacterController>();
	}

	private void Start()
	{
		skeletonAnimation.state.Event += HandleEvent;
	}

	private void HandleEvent(Spine.AnimationState state, int trackIndex, Spine.Event e)
	{
		if (e.Data.Name == footstepEventName)
		{
			footstepAudioSource.Stop();
			footstepAudioSource.pitch = GetRandomPitch(0.2f);
			footstepAudioSource.Play();
		}
	}

	private void Update()
	{
		float axis = UnityEngine.Input.GetAxis(XAxis);
		float axis2 = UnityEngine.Input.GetAxis(YAxis);
		bool flag = (controller.isGrounded && axis2 < -0.5f) || forceCrouchEndTime > Time.time;
		velocity.x = 0f;
		if (!flag)
		{
			if (Input.GetButtonDown(JumpButton) && controller.isGrounded)
			{
				jumpAudioSource.Stop();
				jumpAudioSource.Play();
				velocity.y = jumpSpeed;
				jumpEndTime = Time.time + jumpDuration;
			}
			else if (Time.time < jumpEndTime && Input.GetButtonUp(JumpButton))
			{
				jumpInterrupt = true;
			}
			if (axis != 0f)
			{
				velocity.x = ((!(Mathf.Abs(axis) > 0.6f)) ? walkSpeed : runSpeed);
				velocity.x *= Mathf.Sign(axis);
			}
			if (jumpInterrupt)
			{
				if (velocity.y > 0f)
				{
					velocity.y = Mathf.MoveTowards(velocity.y, 0f, Time.deltaTime * 100f);
				}
				else
				{
					jumpInterrupt = false;
				}
			}
		}
		velocity.y -= gravity * Time.deltaTime;
		controller.Move(new Vector3(velocity.x, velocity.y, 0f) * Time.deltaTime);
		if (controller.isGrounded)
		{
			velocity.y = (0f - gravity) * Time.deltaTime;
			jumpInterrupt = false;
		}
		Vector2 vector = lastVelocity - velocity;
		if (!lastGrounded && controller.isGrounded)
		{
			if (gravity * Time.deltaTime - vector.y > forceCrouchVelocity)
			{
				forceCrouchEndTime = Time.time + forceCrouchDuration;
				hardfallAudioSource.Play();
			}
			else
			{
				footstepAudioSource.Play();
			}
		}
		if (controller.isGrounded)
		{
			if (flag)
			{
				skeletonAnimation.AnimationName = crouchName;
			}
			else if (axis == 0f)
			{
				skeletonAnimation.AnimationName = idleName;
			}
			else
			{
				skeletonAnimation.AnimationName = ((!(Mathf.Abs(axis) > 0.6f)) ? walkName : runName);
			}
		}
		else if (velocity.y > 0f)
		{
			skeletonAnimation.AnimationName = jumpName;
		}
		else
		{
			skeletonAnimation.AnimationName = fallName;
		}
		if (axis > 0f)
		{
			graphicsRoot.localRotation = Quaternion.identity;
		}
		else if (axis < 0f)
		{
			graphicsRoot.localRotation = flippedRotation;
		}
		lastVelocity = velocity;
		lastGrounded = controller.isGrounded;
	}

	private static float GetRandomPitch(float maxOffset)
	{
		return 1f + UnityEngine.Random.Range(0f - maxOffset, maxOffset);
	}
}
