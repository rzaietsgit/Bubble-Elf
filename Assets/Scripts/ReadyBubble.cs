using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyBubble : MonoBehaviour
{
	public float m_speed = 1f;

	public bool move;

	private bool stopBall;

	private Vector3 direction;

	private Vector2 starpos1;

	private Vector2 starpos;

	private Vector2 endpos;

	private Vector3 moveto = new Vector3(-1f, -1f, 0f);

	private Vector3 moveEndPos;

	private Vector3 nowPos;

	private Vector3 oldPos;

	private Vector3 targetDirection;

	private bool isQuick;

	public bool isReady = true;

	public GameObject moveObj;

	private bool isMoveEnd;

	private bool canMove;

	private bool isDownAnim;

	private bool isReadyAnim;

	private int row;

	private int col;

	private int _row;

	private int _col;

	private int mrow;

	private Vector3 oldmovepos;

	private Vector2 oldmovepos2;

	public static bool bjljian = true;

	private void Start()
	{
		isMoveEnd = false;
		canMove = false;
		m_speed = 20f;
		starpos1 = new Vector2(-1f, -1f);
		starpos = new Vector2(-1f, -1f);
		endpos = new Vector2(-1f, -1f);
		moveto = new Vector3(-1f, -1f, 0f);
		bjljian = true;
	}

	private void Update()
	{
		if ((UI.Instance.GetPanelCount() > 0 && UI.Instance.GetTopPanelType() != UIPanelType.GuideMinPanel) || FindPath.bpath2 || FindPath.bpath || ((bool)PauseUI.action && PauseUI.action.bPause))
		{
			return;
		}
		if (Input.GetMouseButtonUp(0) && !move)
		{
			Vector3 mousePosition = UnityEngine.Input.mousePosition;
			Vector3 b = Camera.main.WorldToScreenPoint(base.transform.position);
			direction = mousePosition - b;
			direction = direction.normalized;
			direction.z = 0f;
		}
		if ((UI.Instance.GetPanelCount() > 0 && UI.Instance.GetTopPanelType() != UIPanelType.GuideMinPanel) || GameUI.action.CheckOpenUI() || !PassLevel.action.bGameStart || ((bool)GameUI.action && GameUI.action.bUseSkill) || ((bool)BubbleSpawner.Instance && BubbleSpawner.Instance.gameMove) || RemoveController.Instance.bBoss3Change)
		{
			return;
		}
		if (Input.GetMouseButton(0))
		{
			Vector3 mousePosition2 = UnityEngine.Input.mousePosition;
			Vector3 b2 = Camera.main.WorldToScreenPoint(base.transform.position);
			direction = mousePosition2 - b2;
			direction = direction.normalized;
			direction.z = 0f;
		}
		if ((bool)MapMoveSpawner.Instance && MapMoveSpawner.Instance.isQuick)
		{
			isQuick = true;
		}
		if (!stopBall && Singleton<LevelManager>.Instance.CheckBubble() && Input.GetMouseButtonUp(0) && !move)
		{
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
			if (BubbleSpawner.Instance.isReadyMove || !MapMoveSpawner.Instance.isMoveEnd)
			{
				return;
			}
			if (isQuick)
			{
				if (!MapMoveSpawner.Instance.isMoveEnd)
				{
					return;
				}
				MapMoveSpawner.Instance.isQuick = false;
				isQuick = false;
			}
			GameObject gameObject = TouchChecker(UnityEngine.Input.mousePosition);
			if ((bool)gameObject && (bool)gameObject.GetComponent<ChangeController>())
			{
				BubbleSpawner.Instance.ChangeBubble();
				return;
			}
			if ((bool)GameGuide.Instance && !GameGuide.Instance.isCanShoot)
			{
				return;
			}
			moveObj = GameObject.Find("movepos");
			moveObj.transform.position = base.transform.position;
			Vector3 mousePosition3 = UnityEngine.Input.mousePosition;
			Vector3 vector = Camera.main.WorldToScreenPoint(base.transform.position);
			direction = mousePosition3 - vector;
			direction = direction.normalized;
			direction.z = 0f;
			if (direction.y >= 0.3f && Time.timeScale > 0f && mousePosition3.y / (float)Screen.height <= 0.95f)
			{
				move = true;
				Vector3 a = new Vector3(direction.x, direction.y, 0f);
				moveObj.transform.Translate(a * Time.deltaTime * m_speed, Space.World);
				IEnumerator enumerator2 = BubbleSpawner.Instance.BallParent.transform.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						Transform transform2 = (Transform)enumerator2.Current;
						if ((bool)transform2.GetComponent<BubbleObj>())
						{
							int num = transform2.GetComponent<BubbleObj>().mBubbleData.row;
							if (num > mrow)
							{
								mrow = num;
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
				CheckCollisionRowAndCol();
				BUBBLEDATA mBubbleData = base.gameObject.GetComponent<BubbleObj>().mBubbleData;
				Vector3 vector2 = mousePosition3 - vector;
				float z = Vector3.Angle(vector, mousePosition3);
				if (int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["attributes"]) == 14)
				{
					if ((bool)SoundController.action)
					{
						SoundController.action.playNow("skill_super_3");
					}
					base.gameObject.GetComponent<BubbleObj>().addfx_mofajian_remove(z);
					base.gameObject.GetComponent<BubbleObj>().DelG_fx_mofa_light();
				}
				if (int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["attributes"]) == 15)
				{
					base.gameObject.GetComponent<BubbleObj>().DelG_fx_mofa_light();
				}
				int num2 = 0;
				if (base.gameObject.GetComponent<BubbleObj>().skillbing)
				{
					num2++;
				}
				if (base.gameObject.GetComponent<BubbleObj>().skillmu)
				{
					num2++;
				}
				if (base.gameObject.GetComponent<BubbleObj>().skilldian)
				{
					num2++;
				}
				if (base.gameObject.GetComponent<BubbleObj>().skillhuo)
				{
					num2++;
				}
				if (base.gameObject.GetComponent<BubbleObj>().skillbing && num2 < 3)
				{
					if (BubbleSpawner.Instance.skillBingCount == 3)
					{
						Singleton<LevelManager>.Instance.CutBubble();
					}
				}
				else
				{
					Singleton<LevelManager>.Instance.CutBubble();
				}
				PassLevel.action.BubbleFire();
				GameGuide.Instance.shootBubble();
				if ((bool)SoundController.action)
				{
					SoundController.action.playNow("b_shoot");
				}
			}
		}
		if (move && checkCollision() && !disposeBubble())
		{
		}
	}

	private float GetAngle(Vector3 a, Vector3 b)
	{
		b.x -= a.x;
		b.z -= a.z;
		float num = 0f;
		if (b.x == 0f && b.z == 0f)
		{
			return 0f;
		}
		if (b.x > 0f && b.z > 0f)
		{
			num = 0f;
		}
		else
		{
			if (b.x > 0f && b.z == 0f)
			{
				return 90f;
			}
			if (b.x > 0f && b.z < 0f)
			{
				num = 180f;
			}
			else
			{
				if (b.x == 0f && b.z < 0f)
				{
					return 180f;
				}
				if (b.x < 0f && b.z < 0f)
				{
					num = -180f;
				}
				else
				{
					if (b.x < 0f && b.z == 0f)
					{
						return -90f;
					}
					if (b.x < 0f && b.z > 0f)
					{
						num = 0f;
					}
				}
			}
		}
		return Mathf.Atan(b.x / b.z) * 57.29578f + num;
	}

	public void CheckCollisionRowAndCol()
	{
		Vector3 vector = base.transform.position;
		endpos = vector;
		Vector3 a = new Vector3(direction.x, direction.y, direction.z);
		bool flag = false;
		while (!flag)
		{
			oldmovepos = oldmovepos2;
			vector += a / 10f;
			Vector3 v = BubbleSpawner.Instance.MoveToPos(vector);
			oldmovepos2 = v;
			IEnumerator enumerator = BubbleSpawner.Instance.BallParent.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.Current;
					float num = Vector2.Distance(transform.position, v);
					float num2 = 0.4f;
					if (v.x < -3.2f)
					{
						GameObject gameObject = transform.gameObject;
						if ((bool)gameObject.GetComponent<BubbleObj>())
						{
							BUBBLEDATA mBubbleData = gameObject.GetComponent<BubbleObj>().mBubbleData;
							if (mBubbleData.col == 0 && mBubbleData.row % 2 == 1)
							{
								num2 = 0.6f;
							}
						}
					}
					if (v.x > 3.2f)
					{
						GameObject gameObject2 = transform.gameObject;
						if ((bool)gameObject2.GetComponent<BubbleObj>())
						{
							BUBBLEDATA mBubbleData2 = gameObject2.GetComponent<BubbleObj>().mBubbleData;
							if (mBubbleData2.col == 9 && mBubbleData2.row % 2 == 1)
							{
								num2 = 0.6f;
							}
						}
					}
					if (num < num2)
					{
						GameObject gameObject3 = transform.gameObject;
						if ((bool)gameObject3.GetComponent<BubbleObj>())
						{
							BUBBLEDATA mBubbleData3 = base.gameObject.GetComponent<BubbleObj>().mBubbleData;
							BUBBLEDATA mBubbleData4 = gameObject3.GetComponent<BubbleObj>().mBubbleData;
							if (int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData3.key]["attributes"]) == 14)
							{
								int num3 = mrow - 11;
								if (num3 < 0)
								{
									num3 = 0;
								}
								Vector3 position = BubbleSpawner.Instance.GetSquare(num3, 0).transform.position;
								float y = position.y;
								if (!(v.y < y) || !(vector.x > -3.4f) || !(vector.x < 3.4f))
								{
									_row = mBubbleData4.row;
									_col = mBubbleData4.col;
									flag = true;
									StartCoroutine(MovePos());
									return;
								}
							}
							else
							{
								int num4 = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData4.key]["attributes"]);
								if (num4 != 3)
								{
									_row = transform.GetComponent<BubbleObj>().mBubbleData.row;
									_col = transform.GetComponent<BubbleObj>().mBubbleData.col;
									float num5 = float.MaxValue;
									List<Vector2> around = BubbleSpawner.Instance.GetAround(_row, _col);
									for (int i = 0; i < around.Count; i++)
									{
										GameObject[,] bubbleArray = BubbleSpawner.Instance.BubbleArray;
										Vector2 vector2 = around[i];
										int num6 = (int)vector2.x;
										Vector2 vector3 = around[i];
										GameObject gameObject4 = bubbleArray[num6, (int)vector3.y];
										if (!gameObject4 || ((bool)gameObject4 && (bool)gameObject4.GetComponent<BubbleObj>() && gameObject4.GetComponent<BubbleObj>().mBubbleData.key == "G"))
										{
											BubbleSpawner instance = BubbleSpawner.Instance;
											Vector2 vector4 = around[i];
											int num7 = (int)vector4.x;
											Vector2 vector5 = around[i];
											float num8 = Vector2.Distance(instance.GetSquare(num7, (int)vector5.y).transform.position, vector);
											if (num8 < num5)
											{
												num5 = num8;
												Vector2 vector6 = around[i];
												row = (int)vector6.x;
												Vector2 vector7 = around[i];
												col = (int)vector7.y;
											}
										}
									}
									Vector2 vector8 = (gameObject3.transform.position - oldmovepos).normalized;
									float num9 = Mathf.Atan2(vector8.y, vector8.x) * 57.29578f;
									endpos = BubbleSpawner.Instance.GetPosByRowAndCol(_row, _col);
									if (num9 > -90f && num9 < 30f)
									{
										if (BubbleSpawner.Instance.IsValidPos(_row, _col - 1))
										{
											GameObject gameObject5 = BubbleSpawner.Instance.BubbleArray[_row, _col - 1];
											if (gameObject5 == null)
											{
												row = _row;
												col = _col - 1;
											}
											else
											{
												BUBBLEDATA mBubbleData5 = gameObject5.GetComponent<BubbleObj>().mBubbleData;
												int num10 = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData5.key]["attributes"]);
												if (num10 == 3)
												{
													row = _row;
													col = _col - 1;
												}
											}
										}
									}
									else if (num9 >= 30f && num9 <= 90f)
									{
										int num11 = (_row % 2 != 0) ? _col : (_col - 1);
										if (BubbleSpawner.Instance.IsValidPos(_row + 1, num11))
										{
											GameObject gameObject6 = BubbleSpawner.Instance.BubbleArray[_row + 1, num11];
											if (gameObject6 == null)
											{
												row = _row + 1;
												col = num11;
											}
											else
											{
												BUBBLEDATA mBubbleData6 = gameObject6.GetComponent<BubbleObj>().mBubbleData;
												int num12 = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData6.key]["attributes"]);
												if (num12 == 3)
												{
													row = _row + 1;
													col = num11;
												}
											}
										}
										else if (num11 == -1 && BubbleSpawner.Instance.IsValidPos(_row + 1, 0))
										{
											GameObject gameObject7 = BubbleSpawner.Instance.BubbleArray[_row + 1, 0];
											if (gameObject7 == null)
											{
												row = _row + 1;
												col = 0;
											}
											else
											{
												BUBBLEDATA mBubbleData7 = gameObject7.GetComponent<BubbleObj>().mBubbleData;
												int num13 = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData7.key]["attributes"]);
												if (num13 == 3)
												{
													row = _row + 1;
													col = 0;
												}
											}
										}
									}
									else if (num9 >= 90f && num9 < 150f)
									{
										int num14 = (_row % 2 != 0) ? (_col + 1) : _col;
										if (BubbleSpawner.Instance.IsValidPos(_row + 1, num14))
										{
											GameObject gameObject8 = BubbleSpawner.Instance.BubbleArray[_row + 1, num14];
											if (gameObject8 == null)
											{
												row = _row + 1;
												col = num14;
											}
											else
											{
												BUBBLEDATA mBubbleData8 = gameObject8.GetComponent<BubbleObj>().mBubbleData;
												int num15 = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData8.key]["attributes"]);
												if (num15 == 3)
												{
													row = _row + 1;
													col = num14;
												}
											}
										}
										else if (BubbleSpawner.Instance.IsValidPos(_row + 1, num14 - 1))
										{
											GameObject gameObject9 = BubbleSpawner.Instance.BubbleArray[_row + 1, num14 - 1];
											if (gameObject9 == null)
											{
												row = _row + 1;
												col = num14 - 1;
											}
											else
											{
												BUBBLEDATA mBubbleData9 = gameObject9.GetComponent<BubbleObj>().mBubbleData;
												int num16 = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData9.key]["attributes"]);
												if (num16 == 3)
												{
													row = _row + 1;
													col = num14 - 1;
												}
											}
										}
									}
									else if ((num9 >= 150f || num9 <= -90f) && BubbleSpawner.Instance.IsValidPos(_row, _col + 1))
									{
										GameObject gameObject10 = BubbleSpawner.Instance.BubbleArray[_row, _col + 1];
										if (gameObject10 == null)
										{
											row = _row;
											col = _col + 1;
										}
										else
										{
											BUBBLEDATA mBubbleData10 = gameObject10.GetComponent<BubbleObj>().mBubbleData;
											int num17 = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData10.key]["attributes"]);
											if (num17 == 3)
											{
												row = _row;
												col = _col + 1;
											}
										}
									}
									flag = true;
									StartCoroutine(MovePos());
									return;
								}
							}
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
			float y2 = v.y;
			Vector3 position2 = BubbleSpawner.Instance.GetSquare(0, 0).transform.position;
			if (!(y2 > position2.y))
			{
				continue;
			}
			float num18 = float.MaxValue;
			for (int j = 0; j < BubbleSpawner.cols; j++)
			{
				float num19 = Vector2.Distance(BubbleSpawner.Instance.GetSquare(0, j).transform.position, v);
				if ((bool)BubbleSpawner.Instance.BubbleArray[0, j])
				{
					int num20 = int.Parse(Singleton<DataManager>.Instance.dBubble[BubbleSpawner.Instance.BubbleArray[0, j].GetComponent<BubbleObj>().mBubbleData.key]["attributes"]);
					if (num20 == 3 && num19 < num18)
					{
						num18 = num19;
						row = 0;
						col = j;
						_row = -1;
						_col = -1;
					}
				}
				else if (num19 < num18 && BubbleSpawner.Instance.BubbleArray[0, j] == null)
				{
					num18 = num19;
					row = 0;
					col = j;
					_row = -1;
					_col = -1;
				}
			}
			flag = true;
			StartCoroutine(MovePos());
		}
	}

	public void FixedUpdate()
	{
		if (isMoveEnd)
		{
			bjljian = true;
		}
		if (isMoveEnd || !canMove)
		{
			return;
		}
		oldPos = nowPos;
		nowPos += targetDirection / 2.5f;
		Vector3 v = BubbleSpawner.Instance.MoveToPos(nowPos);
		v.z = -20f;
		starpos = endpos;
		endpos = v;
		if (_row == -1)
		{
			float y = endpos.y;
			Vector3 position = BubbleSpawner.Instance.GetSquare(0, 0).transform.position;
			if (y > position.y)
			{
				isMoveEnd = true;
			}
			else
			{
				base.transform.position = new Vector3(endpos.x, endpos.y, -20f);
			}
		}
		else
		{
			Vector3 position2 = BubbleSpawner.Instance.GetSquare(_row, _col).transform.position;
			float num = Vector2.Distance(position2, endpos);
			if (Vector2.Distance(position2, endpos) <= 0.64f)
			{
				Vector3 vector = starpos;
				bool flag = false;
				while (!flag)
				{
					oldPos += targetDirection / 100f;
					Vector3 v2 = BubbleSpawner.Instance.MoveToPos(oldPos);
					if (Vector2.Distance(position2, v2) <= 0.64f)
					{
						flag = true;
						isMoveEnd = true;
						base.transform.position = new Vector3(v2.x, v2.y, -20f);
						break;
					}
				}
			}
			else
			{
				base.transform.position = new Vector3(endpos.x, endpos.y, -20f);
			}
		}
		BUBBLEDATA mBubbleData = base.gameObject.GetComponent<BubbleObj>().mBubbleData;
		IEnumerator enumerator = BubbleSpawner.Instance.BallParent.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				float num2 = Vector2.Distance(transform.position, v);
				if (num2 < 0.64f)
				{
					GameObject gameObject = transform.gameObject;
					if ((bool)gameObject && (bool)gameObject.GetComponent<BubbleObj>())
					{
						if (int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["attributes"]) == 14)
						{
							BubbleObj component = gameObject.GetComponent<BubbleObj>();
							if (component.mBubbleData.key == "HBoss")
							{
								if (bjljian)
								{
									bjljian = false;
									component.RemoveBubble(isFallBubble: false, 0f, bskill: true);
								}
								else
								{
									component.RemoveBubble();
								}
							}
							else
							{
								float num3 = 1f;
								if (BubbleSpawner.Instance.Combo >= 5 && BubbleSpawner.Instance.Combo < 10)
								{
									num3 = 1.5f;
								}
								else if (BubbleSpawner.Instance.Combo >= 10)
								{
									num3 = 2f;
								}
								int score = (int)(250f * num3);
								component.GetComponent<BubbleObj>().isAddScore = true;
								component.GetComponent<BubbleObj>().AddScore(score);
								component.GetComponent<BubbleObj>().isAddScore = false;
								component.GetComponent<BubbleObj>().isRemoveByJian = true;
								component.RemoveBubble();
							}
						}
						else
						{
							BUBBLEDATA mBubbleData2 = gameObject.GetComponent<BubbleObj>().mBubbleData;
							int num4 = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData2.key]["attributes"]);
							if (num4 == 3)
							{
								gameObject.GetComponent<BubbleObj>().RemoveBubble();
							}
						}
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
	}

	public IEnumerator MovePos()
	{
		nowPos = base.transform.position;
		oldPos = base.transform.position;
		endpos = nowPos;
		targetDirection = new Vector3(direction.x, direction.y, direction.z);
		yield return new WaitForSeconds(0.01f);
		isMoveEnd = false;
		canMove = true;
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

	public bool checkCollision()
	{
		if (isMoveEnd)
		{
			return true;
		}
		float y = moveto.y;
		Vector3 position = BubbleSpawner.Instance.GetSquare(0, 0).transform.position;
		if (y > position.y)
		{
			return true;
		}
		return false;
	}

	public void Ready()
	{
		isReady = true;
	}

	public bool disposeBubble()
	{
		base.transform.parent = BubbleSpawner.Instance.BallParent.transform;
		string text = string.Empty;
		BUBBLEDATA mBubbleData = base.gameObject.GetComponent<BubbleObj>().mBubbleData;
		if (int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["attributes"]) == 14)
		{
			RemoveController.Instance.CheckRemove(base.gameObject, string.Empty);
			base.gameObject.GetComponent<BubbleObj>().RemoveBubble();
			BubbleSpawner.Instance.createReadyBubble();
			return true;
		}
		if (int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["attributes"]) == 15)
		{
			float num = float.MaxValue;
			if (_row == -1 || _col == -1)
			{
				IEnumerator enumerator = BubbleSpawner.Instance.BallParent.transform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						Transform transform = (Transform)enumerator.Current;
						float num2 = Vector2.Distance(transform.position, base.gameObject.transform.position);
						if (num2 < num)
						{
							GameObject gameObject = transform.gameObject;
							if ((bool)gameObject && (bool)gameObject.GetComponent<BubbleObj>() && gameObject != base.gameObject)
							{
								num = num2;
								if (gameObject.GetComponent<BubbleObj>().mBubbleData.row != -1)
								{
									_row = gameObject.GetComponent<BubbleObj>().mBubbleData.row;
									_col = gameObject.GetComponent<BubbleObj>().mBubbleData.col;
								}
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
			}
			GameObject gameObject2 = BubbleSpawner.Instance.BubbleArray[_row, _col];
			if ((bool)gameObject2)
			{
				text = gameObject2.GetComponent<BubbleObj>().mBubbleData.key;
			}
			if (text == string.Empty || (text != string.Empty && int.Parse(Singleton<DataManager>.Instance.dBubble[text]["type"]) > 5))
			{
				text = string.Empty;
				List<Vector2> around = BubbleSpawner.Instance.GetAround(row, col);
				for (int i = 0; i < around.Count; i++)
				{
					GameObject[,] bubbleArray = BubbleSpawner.Instance.BubbleArray;
					Vector2 vector = around[i];
					int num3 = (int)vector.x;
					Vector2 vector2 = around[i];
					GameObject gameObject3 = bubbleArray[num3, (int)vector2.y];
					if ((bool)gameObject3 && (bool)gameObject3.GetComponent<BubbleObj>())
					{
						string key = gameObject3.GetComponent<BubbleObj>().mBubbleData.key;
						if (int.Parse(Singleton<DataManager>.Instance.dBubble[key]["type"]) <= 5)
						{
							text = key;
						}
					}
				}
			}
			if (text == string.Empty)
			{
				IEnumerator enumerator2 = BubbleSpawner.Instance.BallParent.transform.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						Transform transform2 = (Transform)enumerator2.Current;
						GameObject gameObject4 = transform2.gameObject;
						if ((bool)gameObject4.GetComponent<BubbleObj>())
						{
							int num4 = gameObject4.GetComponent<BubbleObj>().mBubbleData.row;
							int num5 = mrow - 11;
							if (num4 > num5)
							{
								string key2 = gameObject4.GetComponent<BubbleObj>().mBubbleData.key;
								if (int.Parse(Singleton<DataManager>.Instance.dBubble[key2]["type"]) <= 5)
								{
									text = key2;
								}
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
		}
		Vector3 a = new Vector3(direction.x, direction.y, direction.z);
		oldPos += a / 3f;
		Vector3 vector3 = BubbleSpawner.Instance.MoveToPos(oldPos);
		base.enabled = false;
		BUBBLEDATA mBubbleData2 = GetComponent<BubbleObj>().mBubbleData;
		mBubbleData2.row = row;
		mBubbleData2.col = col;
		GetComponent<BubbleObj>().SetBubble(row, col);
		StartCoroutine(BubbleSpawner.Instance.HitAnim(base.gameObject, mBubbleData2, vector3, text));
		int num6 = 0;
		if (GetComponent<BubbleObj>().skillbing)
		{
			num6++;
		}
		if (GetComponent<BubbleObj>().skillmu)
		{
			num6++;
		}
		if (GetComponent<BubbleObj>().skilldian)
		{
			num6++;
		}
		if (GetComponent<BubbleObj>().skillhuo)
		{
			num6++;
		}
		if (GetComponent<BubbleObj>().skillbing && num6 < 3)
		{
			if (BubbleSpawner.Instance.skillBingCount > 3 || BubbleSpawner.Instance.skillBingCount < 1)
			{
				BubbleSpawner.Instance.createReadyBubble();
			}
		}
		else
		{
			BubbleSpawner.Instance.createReadyBubble();
		}
		return true;
	}
}
