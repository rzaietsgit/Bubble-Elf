using UnityEngine;

public class DeadTime : MonoBehaviour
{
	public float Time = 1f;

	private void Start()
	{
		UnityEngine.Object.Destroy(base.transform.gameObject, Time);
	}

	private void Update()
	{
	}

	public void OnDestroy()
	{
		if ((bool)GameUI.action)
		{
			GameUI.action.GameBG.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
		}
	}
}
