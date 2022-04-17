using DG.Tweening;
using Spine.Unity;
using UnityEngine;

public class BallFlyObj : MonoBehaviour
{
	public GameObject fx_bigElfObj;

	public GameObject fx_elfinObj;

	public GameObject ElfObj;

	private int iTypes = 100;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetTop()
	{
	}

	public void SwitchAnimation(string sAniName)
	{
		SkeletonAnimation component = ElfObj.GetComponent<SkeletonAnimation>();
		component.Initialize(overwrite: true);
		component.state.SetAnimation(0, sAniName, loop: true);
	}

	public void SetType(int iType)
	{
		iTypes = iType;
		if (iType == 100)
		{
			ElfObj = UnityEngine.Object.Instantiate(fx_elfinObj, base.transform.position, base.transform.rotation);
			ElfObj.transform.parent = base.transform;
			Sequence s = DOTween.Sequence();
			s.Append(ElfObj.transform.DOScale(new Vector2(0.5f, 0.5f), 0f)).Append(ElfObj.transform.DOScale(new Vector2(2f, 2f), 0.4f).SetEase(Ease.OutSine)).Append(ElfObj.transform.DOScale(new Vector2(1f, 1f), 0.3f).SetEase(Ease.InSine));
			return;
		}
		ElfObj = UnityEngine.Object.Instantiate(fx_bigElfObj, base.transform.position, base.transform.rotation);
		ElfObj.transform.parent = base.transform;
		Transform transform = ElfObj.transform;
		Vector3 localScale = ElfObj.transform.localScale;
		float x = localScale.x * 2f;
		Vector3 localScale2 = ElfObj.transform.localScale;
		float y = localScale2.y * 2f;
		Vector3 localScale3 = ElfObj.transform.localScale;
		transform.localScale = new Vector3(x, y, localScale3.z * 2f);
		Transform transform2 = ElfObj.transform;
		Vector3 localScale4 = ElfObj.transform.localScale;
		float x2 = localScale4.x / 2f;
		Vector3 localScale5 = ElfObj.transform.localScale;
		float y2 = localScale5.y / 2f;
		Vector3 localScale6 = ElfObj.transform.localScale;
		transform2.DOScale(new Vector3(x2, y2, localScale6.z / 2f), 1.5f);
	}
}
