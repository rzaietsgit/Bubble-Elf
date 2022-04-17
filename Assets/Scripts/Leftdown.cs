using DG.Tweening;
using UnityEngine;

public class Leftdown : MonoBehaviour
{
	public static Leftdown action;

	public GameObject google;

	public GameObject bag;

	public GameObject set;

	public GameObject exit;

	private bool open;

	private float ftime = 0.2f;

	private void Start()
	{
		action = this;
	}

	public void closeui()
	{
		open = false;
		google.transform.DOLocalMove(new Vector3(0f, 0f, 0f), ftime).SetEase(Ease.InSine);
		bag.transform.DOLocalMove(new Vector3(0f, 0f, 0f), ftime).SetEase(Ease.InSine);
		set.transform.DOLocalMove(new Vector3(0f, 0f, 0f), ftime).SetEase(Ease.InSine);
		exit.transform.DOLocalMove(new Vector3(0f, 0f, 0f), ftime).SetEase(Ease.InSine);
		Csale_(b: false);
	}

	public void openui()
	{
		open = true;
		google.transform.DOLocalMove(new Vector3(-18f, 150f, 0f), ftime).SetEase(Ease.InSine);
		bag.transform.DOLocalMove(new Vector3(70f, 120f, 0f), ftime).SetEase(Ease.InSine);
		set.transform.DOLocalMove(new Vector3(142f, 60f, 0f), ftime).SetEase(Ease.InSine);
		exit.transform.DOLocalMove(new Vector3(154f, -29f, 0f), ftime).SetEase(Ease.InSine);
		Csale_();
	}

	private void Csale_(bool b = true)
	{
		Csale(google, b);
		Csale(bag, b);
		Csale(set, b);
		Csale(exit, b);
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

	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			GameObject gameObject = TouchChecker(UnityEngine.Input.mousePosition);
			if (gameObject == null)
			{
				closeui();
			}
			else if (gameObject.name != "leftdown")
			{
				closeui();
			}
		}
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

	public void ClickGoogle()
	{
		if (open)
		{
			UI.Instance.OpenPanel(UIPanelType.GooglePlay3Panel);
		}

		if (!open)
		{
			UI.Instance.OpenPanel(UIPanelType.GooglePlay3Panel);
		}
	}

	public void Clickbag()
	{
		if (open)
		{
			UI.Instance.OpenPanel(UIPanelType.PackSkillIconUI);
		}
	}

	public void Clickset()
	{
		if (open)
		{
			UI.Instance.OpenPanel(UIPanelType.set1Panel);
		}
	}

	public void ClickExit()
	{
		if (open)
		{
			UI.Instance.OpenPanel(UIPanelType.ExitUI);
		}
	}

	public void ClickMap()
	{
		UI.Instance.OpenPanel(UIPanelType.maplistPanel);
	}
}
