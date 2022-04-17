using DG.Tweening;
using UnityEngine;

public class rightdown : MonoBehaviour
{
	public static rightdown action;

	public GameObject googleRight;

	public GameObject btnImg;

	private bool open;

	private float ftime = 0.2f;

	private void Start()
	{
		action = this;
	}

	public void Clickopen()
	{
		if (open)
		{
			closeui();
		}
		else
		{
			openui();
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			GameObject gameObject = TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject == null)
			{
				closeui();
			}
			else if (gameObject.name != "rightdown")
			{
				closeui();
			}
		}
	}

	public GameObject TouchChecker(Vector3 mouseposition)
	{
		Vector3 vector = Camera.main.ScreenToWorldPoint(mouseposition);
		Vector2 point = new Vector2(vector.x, vector.y);
		if ((bool)Physics2D.OverlapPoint(point))
		{
			return Physics2D.OverlapPoint(point).gameObject;
		}
		return null;
	}

	public void closeui()
	{
		open = false;
		googleRight.transform.DOLocalMove(new Vector3(-18f, 0f, 0f), ftime).SetEase(Ease.InSine);
		Csale_(b: false);
		btnImg.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
	}

	public void openui()
	{
		open = true;
		googleRight.transform.DOLocalMove(new Vector3(-18f, 150f, 0f), ftime).SetEase(Ease.InSine);
		Csale_();
		btnImg.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
	}

	private void Csale_(bool b = true)
	{
		Csale(googleRight, b);
	}

	private void Csale(GameObject obj, bool b = true)
	{
		if (b)
		{
			obj.transform.DOScale(new Vector3(1f, 1f, 1f), ftime).SetEase(Ease.InSine);
		}
		else
		{
			obj.transform.DOScale(new Vector3(0f, 0f, 0f), ftime).SetEase(Ease.InSine);
		}
	}
}
