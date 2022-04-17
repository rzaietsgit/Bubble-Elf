using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
	public GameObject readyBubbleParent;

	public GameObject pointer;

	public GameObject tipPoint;

	private GameObject[] pointers = new GameObject[30];

	private Vector3 direction;

	private List<GameObject> gameobj;

	private Vector3 endpos;

	private bool isDownAnim;

	private bool isReadyAnim;

	private int colorindex;

	private int index;

	private int oldrow;

	private int oldcol;

	private Vector3 oldmovepos;

	private Vector2 oldmovepos2;

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
		tipPoint.SetActive(value: false);
	}

	private void Update()
	{
		if (Util.GetNowOpenUI() || FindPath.bpath2 || FindPath.bpath || !Singleton<LevelManager>.Instance.CheckBubble() || (UI.Instance.GetPanelCount() > 0 && UI.Instance.GetTopPanelType() != UIPanelType.GuideMinPanel))
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
				GameObject gameObject2 = null;
				for (int l = 0; l < pointers.Length * 10; l++)
				{
					oldmovepos = oldmovepos2;
					vector += a / 10f;
					Vector3 v = BubbleSpawner.Instance.MoveToPos(vector);
					oldmovepos2 = v;
					bool flag = false;
					float num3 = 0.4f;
					for (int m = 0; m < gameobj.Count; m++)
					{
						if (!gameobj[m])
						{
							continue;
						}
						Transform transform2 = gameobj[m].transform;
						float num4 = Vector2.Distance(transform2.position, v);
						if (v.x < -3.2f)
						{
							GameObject gameObject3 = transform2.gameObject;
							if ((bool)gameObject3.GetComponent<BubbleObj>())
							{
								BUBBLEDATA mBubbleData = gameObject3.GetComponent<BubbleObj>().mBubbleData;
								if (mBubbleData.col == 0 && mBubbleData.row % 2 == 1)
								{
									num3 = 0.6f;
								}
							}
						}
						if (v.x > 3.2f)
						{
							GameObject gameObject4 = transform2.gameObject;
							if ((bool)gameObject4.GetComponent<BubbleObj>())
							{
								BUBBLEDATA mBubbleData2 = gameObject4.GetComponent<BubbleObj>().mBubbleData;
								if (mBubbleData2.col == 9 && mBubbleData2.row % 2 == 1)
								{
									num3 = 0.6f;
								}
							}
						}
						if (!(num4 < num3))
						{
							continue;
						}
						gameObject2 = transform2.gameObject;
						if ((bool)gameObject2 && (bool)gameObject2.GetComponent<BubbleObj>())
						{
							BUBBLEDATA mBubbleData3 = gameObject2.GetComponent<BubbleObj>().mBubbleData;
							int num5 = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData3.key]["attributes"]);
							if (num5 != 3)
							{
								flag = true;
							}
						}
					}
					if (!BubbleSpawner.Instance.useyanchangxian)
					{
						float num6 = 4.5f - BubbleSpawner.Instance.offsetStep;
						if (vector.x > 4.5f - BubbleSpawner.Instance.offsetStep)
						{
							if (vector.x > num6 + 0.6f)
							{
								flag = true;
							}
						}
						else if (vector.x < -4.5f + BubbleSpawner.Instance.offsetStep && vector.x < 0f - num6 - 0.6f)
						{
							flag = true;
						}
					}
					if (!flag)
					{
						if (l % 5 == 0 && l / 5 < pointers.Length)
						{
							GameObject gameObject5 = pointers[l / 5];
							v = BubbleSpawner.Instance.MoveToPos(vector + a / 100f * index);
							gameObject5.transform.position = v;
							gameObject5.GetComponent<SpriteRenderer>().enabled = true;
						}
						tipPoint.SetActive(value: false);
						continue;
					}
					if (gameObject2 != null)
					{
						int row = gameObject2.GetComponent<BubbleObj>().mBubbleData.row;
						int col = gameObject2.GetComponent<BubbleObj>().mBubbleData.col;
						Vector2 vector2 = (gameObject2.transform.position - oldmovepos).normalized;
						float num7 = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
						endpos = BubbleSpawner.Instance.GetSquare(row, col).transform.position;
						bool flag2 = false;
						if (num7 > -90f && num7 < 30f)
						{
							if (BubbleSpawner.Instance.IsValidPos(row, col - 1))
							{
								GameObject gameObject6 = BubbleSpawner.Instance.BubbleArray[row, col - 1];
								if (gameObject6 == null)
								{
									endpos = BubbleSpawner.Instance.GetSquare(row, col - 1).transform.position;
									flag2 = true;
								}
								else
								{
									BUBBLEDATA mBubbleData4 = gameObject6.GetComponent<BubbleObj>().mBubbleData;
									int num8 = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData4.key]["attributes"]);
									if (num8 == 3)
									{
										endpos = BubbleSpawner.Instance.GetSquare(row, col - 1).transform.position;
										flag2 = true;
									}
								}
							}
						}
						else if (num7 >= 30f && num7 <= 90f)
						{
							int num9 = (row % 2 != 0) ? col : (col - 1);
							if (BubbleSpawner.Instance.IsValidPos(row + 1, num9))
							{
								GameObject gameObject7 = BubbleSpawner.Instance.BubbleArray[row + 1, num9];
								if (gameObject7 == null)
								{
									endpos = BubbleSpawner.Instance.GetSquare(row + 1, num9).transform.position;
									flag2 = true;
								}
								else
								{
									BUBBLEDATA mBubbleData5 = gameObject7.GetComponent<BubbleObj>().mBubbleData;
									int num10 = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData5.key]["attributes"]);
									if (num10 == 3)
									{
										endpos = BubbleSpawner.Instance.GetSquare(row + 1, num9).transform.position;
										flag2 = true;
									}
								}
							}
							else if (num9 == -1 && BubbleSpawner.Instance.IsValidPos(row + 1, 0))
							{
								GameObject gameObject8 = BubbleSpawner.Instance.BubbleArray[row + 1, 0];
								if (gameObject8 == null)
								{
									endpos = BubbleSpawner.Instance.GetSquare(row + 1, 0).transform.position;
									flag2 = true;
								}
								else
								{
									BUBBLEDATA mBubbleData6 = gameObject8.GetComponent<BubbleObj>().mBubbleData;
									int num11 = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData6.key]["attributes"]);
									if (num11 == 3)
									{
										endpos = BubbleSpawner.Instance.GetSquare(row + 1, 0).transform.position;
										flag2 = true;
									}
								}
							}
						}
						else if (num7 >= 90f && num7 < 150f)
						{
							int num12 = (row % 2 != 0) ? (col + 1) : col;
							if (BubbleSpawner.Instance.IsValidPos(row + 1, num12))
							{
								GameObject gameObject9 = BubbleSpawner.Instance.BubbleArray[row + 1, num12];
								if (gameObject9 == null)
								{
									endpos = BubbleSpawner.Instance.GetSquare(row + 1, num12).transform.position;
									flag2 = true;
								}
								else
								{
									BUBBLEDATA mBubbleData7 = gameObject9.GetComponent<BubbleObj>().mBubbleData;
									int num13 = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData7.key]["attributes"]);
									if (num13 == 3)
									{
										endpos = BubbleSpawner.Instance.GetSquare(row + 1, num12).transform.position;
										flag2 = true;
									}
								}
							}
							else if (BubbleSpawner.Instance.IsValidPos(row + 1, num12 - 1))
							{
								GameObject gameObject10 = BubbleSpawner.Instance.BubbleArray[row + 1, num12 - 1];
								if (gameObject10 == null)
								{
									endpos = BubbleSpawner.Instance.GetSquare(row + 1, num12 - 1).transform.position;
									flag2 = true;
								}
								else
								{
									BUBBLEDATA mBubbleData8 = gameObject10.GetComponent<BubbleObj>().mBubbleData;
									int num14 = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData8.key]["attributes"]);
									if (num14 == 3)
									{
										endpos = BubbleSpawner.Instance.GetSquare(row + 1, num12 - 1).transform.position;
										flag2 = true;
									}
								}
							}
						}
						else if ((num7 >= 150f || num7 <= -90f) && BubbleSpawner.Instance.IsValidPos(row, col + 1))
						{
							GameObject gameObject11 = BubbleSpawner.Instance.BubbleArray[row, col + 1];
							if (gameObject11 == null)
							{
								endpos = BubbleSpawner.Instance.GetSquare(row, col + 1).transform.position;
								flag2 = true;
							}
							else
							{
								BUBBLEDATA mBubbleData9 = gameObject11.GetComponent<BubbleObj>().mBubbleData;
								int num15 = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData9.key]["attributes"]);
								if (num15 == 3)
								{
									endpos = BubbleSpawner.Instance.GetSquare(row, col + 1).transform.position;
									flag2 = true;
								}
							}
						}
						if (flag2)
						{
							tipPoint.SetActive(value: true);
							tipPoint.transform.position = endpos;
						}
						else
						{
							tipPoint.SetActive(value: false);
						}
					}
					else
					{
						tipPoint.SetActive(value: false);
					}
					for (int n = l - 1; n < pointers.Length * 10; n++)
					{
						int num16 = n / 5;
						if (num16 < pointers.Length - 1)
						{
							gameObject2 = pointers[num16];
							gameObject2.GetComponent<SpriteRenderer>().enabled = false;
						}
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
