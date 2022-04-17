using UnityEngine;

public class SetDelay : MonoBehaviour
{
	public float ShowTime;

	public Transform obj;

	private void Start()
	{
		obj.gameObject.SetActive(value: false);
		Invoke("showObj", ShowTime);
	}

	private void showObj()
	{
		obj.gameObject.SetActive(value: true);
	}

	private void Update()
	{
	}
}
