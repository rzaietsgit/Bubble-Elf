using Spine;
using Spine.Unity;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SkeletonRenderer))]
public class SpineGauge : MonoBehaviour
{
	[Range(0f, 1f)]
	public float fillPercent;

	[SpineAnimation("", "")]
	public string fillAnimationName;

	private SkeletonRenderer skeletonRenderer;

	private Spine.Animation fillAnimation;

	private void Awake()
	{
		skeletonRenderer = GetComponent<SkeletonRenderer>();
	}

	private void Update()
	{
		SetGaugePercent(fillPercent);
	}

	public void SetGaugePercent(float x)
	{
		if (skeletonRenderer == null)
		{
			return;
		}
		Skeleton skeleton = skeletonRenderer.skeleton;
		if (skeleton == null)
		{
			return;
		}
		if (fillAnimation == null)
		{
			fillAnimation = skeleton.Data.FindAnimation(fillAnimationName);
			if (fillAnimation == null)
			{
				return;
			}
		}
		fillAnimation.Apply(skeleton, 0f, x, loop: false, null);
		skeleton.Update(Time.deltaTime);
		skeleton.UpdateWorldTransform();
	}
}
