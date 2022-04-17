using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine2 : MonoBehaviour
{
	public GameObject readyBubbleParent;

	public GameObject pointer;

	private GameObject[] pointers = new GameObject[30];

	private Vector3 direction;

	private List<GameObject> gameobj;

	private bool isDownAnim;

	private bool isReadyAnim;

	private int colorindex;

	private int index;

	private float time;

	private void Start()
	{
		gameobj = new List<GameObject>();
		GeneratePoints();
		HidePoints();
		index = 0;
	}

	private void GeneratePoints()
	{
		for (int i = 0; i < pointers.Length; i++)
		{
			pointers[i] = UnityEngine.Object.Instantiate(pointer, base.transform.position, base.transform.rotation);
			pointers[i].transform.parent = base.transform;
		}
	}

	private void HidePoints()
	{
		GameObject[] array = pointers;
		foreach (GameObject gameObject in array)
		{
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	private void Update()
	{
		if (Util.GetNowOpenUI() || !Singleton<LevelManager>.Instance.CheckBubble() || Singleton<DataManager>.Instance.bUiIsOpen)
		{
			return;
		}
		if (Input.GetMouseButton(0) && Time.time - time > 0.01f)
		{
			time = Time.time;
			IEnumerator enumerator = BubbleSpawner.Instance.RemoveParent.transform.GetEnumerator();
			try
			{
				if (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.Current;
					return;
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
			if (!MapMoveSpawner.Instance.isMoveEnd || !BubbleSpawner.Instance.initReady)
			{
				return;
			}
			if ((bool)BubbleSpawner.Instance.ready_1)
			{
				string key = BubbleSpawner.Instance.ready_1.GetComponent<BubbleObj>().mBubbleData.key;
				int num = int.Parse(Singleton<DataManager>.Instance.dBubble[key]["type"]);
				if (colorindex != num)
				{
					colorindex = num;
					if (num <= 5)
					{
						for (int i = 0; i < pointers.Length; i++)
						{
							pointers[i].GetComponent<SpriteRenderer>().sprite = Util.GetResourcesSprite("Img/pointer/pointer" + num, 26, 26);
						}
					}
					else
					{
						for (int j = 0; j < pointers.Length; j++)
						{
							pointers[j].GetComponent<SpriteRenderer>().sprite = Util.GetResourcesSprite("Img/pointer/pointer0", 18, 17);
						}
					}
					gameobj.Clear();
				}
				if (gameobj.Count == 0)
				{
					for (int num2 = BubbleSpawner.rows - 1; num2 >= 0; num2--)
					{
						for (int k = 0; k < BubbleSpawner.cols - num2 % 2; k++)
						{
							if (num2 < MapMoveSpawner.Instance.lowrow - 13)
							{
								return;
							}
							GameObject gameObject = BubbleSpawner.Instance.BubbleArray[num2, k];
							if ((!gameObject || !gameObject.GetComponent<BubbleObj>() || !gameObject.GetComponent<BubbleObj>().isFall) && (bool)gameObject)
							{
								gameobj.Add(gameObject);
							}
						}
					}
				}
			}
			Vector3 mousePosition = UnityEngine.Input.mousePosition;
			Vector3 b = Camera.main.WorldToScreenPoint(readyBubbleParent.transform.position);
			direction = mousePosition - b;
			direction = direction.normalized;
			direction.z = 0f;
			if (direction.y >= 0.3f && Time.timeScale > 0f && mousePosition.y / (float)Screen.height <= 0.95f)
			{
				if (!isReadyAnim)
				{
					isReadyAnim = true;
					isDownAnim = false;
					PassLevel.action.BubbleReady();
				}
				Vector3 vector = readyBubbleParent.transform.position;
				Vector3 a = new Vector3(direction.x, direction.y, direction.z);
				for (int l = 0; l < pointers.Length; l++)
				{
					vector += a / 2f;
					Vector3 v = BubbleSpawner.Instance.MoveToPos(vector);
					bool flag = false;
					for (int m = 0; m < gameobj.Count; m++)
					{
						if (!gameobj[m])
						{
							continue;
						}
						Transform transform2 = gameobj[m].transform;
						float num3 = Vector2.Distance(transform2.position, v);
						if (!(num3 < 0.38f))
						{
							continue;
						}
						GameObject gameObject2 = transform2.gameObject;
						if ((bool)gameObject2 && (bool)gameObject2.GetComponent<BubbleObj>())
						{
							BUBBLEDATA mBubbleData = gameObject2.GetComponent<BubbleObj>().mBubbleData;
							int num4 = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["attributes"]);
							if (num4 != 3)
							{
								flag = true;
							}
						}
					}
					if (!BubbleSpawner.Instance.useyanchangxian)
					{
						float num5 = 3.6f - BubbleSpawner.Instance.offsetStep;
						if ((double)vector.x > 3.6 - (double)BubbleSpawner.Instance.offsetStep)
						{
							if (vector.x > num5 + 0.6f)
							{
								flag = true;
							}
						}
						else if ((double)vector.x < -3.6 + (double)BubbleSpawner.Instance.offsetStep && vector.x < 0f - num5 - 0.6f)
						{
							flag = true;
						}
					}
					if (!flag)
					{
						GameObject gameObject3 = pointers[l];
						v = BubbleSpawner.Instance.MoveToPos(vector + a / 100f * index);
						gameObject3.transform.position = v;
						gameObject3.GetComponent<SpriteRenderer>().enabled = true;
						continue;
					}
					for (int n = l - 1; n < pointers.Length; n++)
					{
						GameObject gameObject4 = pointers[n];
						gameObject4.GetComponent<SpriteRenderer>().enabled = false;
					}
					break;
				}
				index++;
				if (index >= 50)
				{
					index = 0;
				}
			}
			else
			{
				HidePoints();
				if (!isDownAnim)
				{
					isReadyAnim = false;
					isDownAnim = true;
					PassLevel.action.CancelFireBubble();
				}
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			HidePoints();
			gameobj.Clear();
			if (!isDownAnim)
			{
				isReadyAnim = false;
				isDownAnim = true;
				PassLevel.action.CancelFireBubble();
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
}
