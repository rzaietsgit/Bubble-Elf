using Spine.Unity;
using UnityEngine;

public class Chimera : MonoBehaviour
{
	public SkeletonDataAsset skeletonDataSource;

	[SpineAttachment(false, true, false, "", "skeletonDataSource")]
	public string attachmentPath;

	[SpineSlot("", "", false)]
	public string targetSlot;

	private void Start()
	{
		GetComponent<SkeletonRenderer>().skeleton.FindSlot(targetSlot).Attachment = SpineAttachment.GetAttachment(attachmentPath, skeletonDataSource);
	}
}
