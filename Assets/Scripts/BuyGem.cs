using UnityEngine;

public class BuyGem : MonoBehaviour
{
	public string key;

	private void Start()
	{
	}

	public void BuyGems()
	{
		PayManager.action.Pay(key);
	}

	private void Update()
	{
	}
}
