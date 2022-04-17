using DG.Tweening;
using UnityEngine;

public class fx_mofajianRotation : MonoBehaviour
{
	public int towards = 1;

	private float x;

	private void Start()
	{
		base.transform.DOLocalMoveY(20f, 0.9f).SetEase(Ease.OutSine);
	}

	private void Update()
	{
		x += Time.deltaTime * 360f * 5f * (float)towards;
		base.transform.localEulerAngles = new Vector3(0f, x, 0f);
	}
}
