using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PayLoading : MonoBehaviour
{
	public static PayLoading action;

	public GameObject obj;

	public Image loading;

	public float speed = 150f;

	private void Start()
	{
		action = this;
	}

	private void Update()
	{
		loading.rectTransform.Rotate(new Vector3(0f, 0f, -1f) * speed * Time.deltaTime);
	}

	public IEnumerator IEHide()
	{
		yield return new WaitForSeconds(60f);
		obj.SetActive(value: false);
	}

	public void ShowObj()
	{
		obj.SetActive(value: true);
		StartCoroutine(IEHide());
	}

	public void HideObj()
	{
		obj.SetActive(value: false);
	}
}
