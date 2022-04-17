using Spine;
using Spine.Unity;
using UnityEngine;

public class Goblins : MonoBehaviour
{
	private bool girlSkin;

	private SkeletonAnimation skeletonAnimation;

	private Bone headBone;

	public void Start()
	{
		skeletonAnimation = GetComponent<SkeletonAnimation>();
		headBone = skeletonAnimation.skeleton.FindBone("head");
		skeletonAnimation.UpdateLocal += UpdateLocal;
	}

	public void UpdateLocal(ISkeletonAnimation skeletonRenderer)
	{
		headBone.Rotation += 15f;
	}

	public void OnMouseDown()
	{
		skeletonAnimation.skeleton.SetSkin((!girlSkin) ? "goblingirl" : "goblin");
		skeletonAnimation.skeleton.SetSlotsToSetupPose();
		girlSkin = !girlSkin;
		if (girlSkin)
		{
			skeletonAnimation.skeleton.SetAttachment("right hand item", null);
			skeletonAnimation.skeleton.SetAttachment("left hand item", "spear");
		}
		else
		{
			skeletonAnimation.skeleton.SetAttachment("left hand item", "dagger");
		}
	}
}
