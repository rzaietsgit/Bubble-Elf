using DG.Tweening;
using UnityEngine;

public class finger_slide : MonoBehaviour
{
	private void Start()
	{
		base.transform.DOLocalRotate(new Vector3(0f, 0f, -50f), 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
	}

	private void Update()
	{
	}
}
