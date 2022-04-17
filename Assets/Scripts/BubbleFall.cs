using UnityEngine;

public class BubbleFall : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.layer == 12 || coll.gameObject.layer == 13 || coll.gameObject.layer == 14 || coll.gameObject.layer == 15 || coll.gameObject.layer == 16)
		{
			MuTong component = coll.gameObject.GetComponent<MuTong>();
			component.RuDai(base.gameObject.transform.position);
			GetComponent<BubbleObj>().RemoveBubble(true);
		}
		else if (coll.gameObject.layer == 17 && (bool)SoundController.action)
		{
			SoundController.action.playNow("b_bounce_bucket_hit");
		}
	}
}
