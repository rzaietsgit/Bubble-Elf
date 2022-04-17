using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class MapMoveSpawner : MonoBehaviour
{
	public static MapMoveSpawner Instance;

	public bool isMoveEnd = true;

	private float lineY = float.MaxValue;

	public bool isQuick;

	public float delayTime;

	public bool isHaveElf;

	public int BubbleFlyObjrow = -1;

	public int BubbleFlyObjcol = -1;

	public int lowrow;

	private void Start()
	{
		Instance = this;
		isQuick = false;
	}

	private void Update()
	{
	}

	public void MoveLevelUp()
	{
		MoveMap();
	}

	private void MoveMap(bool isFallBubble = false)
	{
		int num = 0;
		int num2 = 0;
		lowrow = 0;
		lineY = float.MaxValue;
		IEnumerator enumerator = BubbleSpawner.Instance.BallParent.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				Vector3 position = transform.position;
				if (position.y < lineY && (bool)transform.GetComponent<BubbleObj>())
				{
					num = transform.GetComponent<BubbleObj>().mBubbleData.row;
					num2 = transform.GetComponent<BubbleObj>().mBubbleData.col;
					Vector3 position2 = BubbleSpawner.Instance.GetSquare(num, num2).transform.position;
					lineY = position2.y;
					if (num > lowrow)
					{
						lowrow = num;
					}
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		if ((bool)BubbleSpawner.Instance.BubbleFlyObj && RemoveController.bwhileturestart && BubbleFlyObjrow != -1 && lowrow > BubbleFlyObjrow + 6)
		{
			int row = BubbleFlyObjrow + 6;
			Vector3 position3 = BubbleSpawner.Instance.GetSquare(row, BubbleFlyObjcol).transform.position;
			lineY = position3.y;
		}
		if ((bool)BubbleSpawner.Instance.BubbleFlyObj && RemoveController.bwhileturestart)
		{
			if (BubbleFlyObjrow != -1 && BubbleFlyObjrow <= 6)
			{
				lineY = 0.64f;
			}
		}
		else if (num <= 10)
		{
			lineY = 0.64f;
		}
		Vector3 position4 = Camera.main.transform.position;
		if (position4.y == lineY)
		{
			RemoveController.Instance.Playzhizhu();
			return;
		}
		float num3 = lineY;
		Vector3 position5 = Camera.main.transform.position;
		if (num3 == position5.y)
		{
			RemoveController.Instance.Playzhizhu();
			return;
		}
		if (lineY != 0.64f)
		{
			lineY -= 0.227f;
		}
		Camera.main.transform.DOKill();
		Vector2 a = Camera.main.transform.position;
		Vector3 position6 = Camera.main.transform.position;
		float x = position6.x;
		float y = lineY;
		Vector3 position7 = Camera.main.transform.position;
		float num4 = Vector2.Distance(a, new Vector3(x, y, position7.z));
		float num5 = 0f;
		num5 = ((!((double)num4 > 0.69)) ? (num4 / 2f) : (num4 / 3f));
		isMoveEnd = false;
		if (isFallBubble)
		{
			num5 = 0.5f;
			Transform transform2 = Camera.main.transform;
			Vector3 position8 = Camera.main.transform.position;
			float x2 = position8.x;
			float y2 = lineY;
			Vector3 position9 = Camera.main.transform.position;
			transform2.DOMove(new Vector3(x2, y2, position9.z), num5).OnComplete(MoveEnd2);
		}
		else
		{
			Transform transform3 = Camera.main.transform;
			Vector3 position10 = Camera.main.transform.position;
			float x3 = position10.x;
			float y3 = lineY;
			Vector3 position11 = Camera.main.transform.position;
			transform3.DOMove(new Vector3(x3, y3, position11.z), num5).OnComplete(MoveEnd);
		}
		isHaveElf = false;
		IEnumerator enumerator2 = BubbleSpawner.Instance.BallParent.transform.GetEnumerator();
		try
		{
			while (enumerator2.MoveNext())
			{
				Transform transform4 = (Transform)enumerator2.Current;
				Vector3 position12 = transform4.position;
				if ((double)position12.y < (double)lineY + 5.5 && (bool)transform4.GetComponent<BubbleObj>())
				{
					BUBBLEDATA mBubbleData = transform4.GetComponent<BubbleObj>().mBubbleData;
					int num6 = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["attributes"]);
					if (num6 == 100 || num6 == 101)
					{
						isHaveElf = true;
						break;
					}
				}
			}
		}
		finally
		{
			IDisposable disposable2;
			if ((disposable2 = (enumerator2 as IDisposable)) != null)
			{
				disposable2.Dispose();
			}
		}
	}

	private void MoveEnd()
	{
		isMoveEnd = true;
		RemoveController.Instance.Playzhizhu();
	}

	private void MoveEnd2()
	{
		isMoveEnd = true;
		RemoveController.Instance.Playzhizhu();
		RemoveController.Instance.isFallBubbleCheck = true;
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

	public void quickMove()
	{
		GameObject gameObject = TouchChecker(UnityEngine.Input.mousePosition);
		if ((!(gameObject != null) || !(gameObject.name == "PauseBtn")) && !isQuick)
		{
			isQuick = true;
			Camera.main.transform.DOKill();
			Vector2 a = Camera.main.transform.position;
			Vector3 position = Camera.main.transform.position;
			float x = position.x;
			float y = lineY;
			Vector3 position2 = Camera.main.transform.position;
			float num = Vector2.Distance(a, new Vector3(x, y, position2.z));
			float num2 = 0f;
			num2 = num / 10f;
			Transform transform = Camera.main.transform;
			Vector3 position3 = Camera.main.transform.position;
			float x2 = position3.x;
			float y2 = lineY;
			Vector3 position4 = Camera.main.transform.position;
			transform.DOMove(new Vector3(x2, y2, position4.z), num2).OnComplete(MoveEnd);
		}
	}

	public void MoveCamera(float posY)
	{
		posY -= 4f;
		isMoveEnd = false;
		if (posY >= 0.64f)
		{
			MoveEnd2();
			return;
		}
		Camera.main.transform.DOKill();
		Transform transform = Camera.main.transform;
		Vector3 position = Camera.main.transform.position;
		float x = position.x;
		float y = posY;
		Vector3 position2 = Camera.main.transform.position;
		transform.DOMove(new Vector3(x, y, position2.z), GetTime(posY)).OnComplete(MoveCameraEnd);
	}

	public float GetTime(float y)
	{
		Vector2 a = Camera.main.transform.position;
		Vector3 position = Camera.main.transform.position;
		float x = position.x;
		Vector3 position2 = Camera.main.transform.position;
		float num = Vector2.Distance(a, new Vector3(x, y, position2.z));
		float num2 = 0f;
		return num / 10f;
	}

	private void MoveCameraEnd()
	{
		StartCoroutine(MoveCameraEnd2());
	}

	private IEnumerator MoveCameraEnd2()
	{
		yield return new WaitForSeconds(0.5f);
		yield return new WaitForSeconds(delayTime);
		MoveMap(isFallBubble: true);
	}
}
