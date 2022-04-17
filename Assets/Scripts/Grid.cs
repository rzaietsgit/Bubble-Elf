using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
	public class NodeItem
	{
		public bool isWall;

		public Vector3 pos;

		public int x;

		public int y;

		public int gCost;

		public int hCost;

		public NodeItem parent;

		public int fCost => gCost + hCost;

		public NodeItem(bool isWall, Vector3 pos, int x, int y)
		{
			this.isWall = isWall;
			this.pos = pos;
			this.x = x;
			this.y = y;
		}
	}

	public static Grid action;

	private NodeItem[,] grid;

	private int w;

	private int h;

	private bool bmove;

	public static int Tx = -1;

	public static int Ty = -1;

	public static int TCount;

	public static Vector3[] Twaypoints;

	private static int iPosIndex;

	private GameObject objS;

	private GameObject obj;

	private void Awake()
	{
		action = this;
		Vector3 localScale = base.transform.localScale;
		w = Mathf.RoundToInt(localScale.x * 2f);
		Vector3 localScale2 = base.transform.localScale;
		h = Mathf.RoundToInt(localScale2.y * 2f);
		w = 150;
		h = 11;
		grid = new NodeItem[w, h];
	}

	public bool IEUpdate()
	{
		bool flag = true;
		if (BubbleSpawner.Instance.BubbleArray == null)
		{
			return flag;
		}
		for (int i = 0; i < w; i++)
		{
			for (int j = 0; j < h; j++)
			{
				bool flag2 = true;
				Vector3 pos = new Vector3(i, j, -0.25f);
				if (i % 2 != 0 && j == h - 1)
				{
					flag2 = true;
					if (flag && grid[i, j] != null && grid[i, j].isWall != flag2)
					{
						flag = false;
					}
					grid[i, j] = new NodeItem(flag2, pos, i, j);
				}
				else
				{
					flag2 = ((!(BubbleSpawner.Instance.BubbleArray[i, j] == null)) ? true : false);
					if (flag && grid[i, j] != null && grid[i, j].isWall != flag2)
					{
						flag = false;
					}
					grid[i, j] = new NodeItem(flag2, pos, i, j);
				}
			}
		}
		return flag;
	}

	public NodeItem getItemS(int x, int y)
	{
		return grid[x, y];
	}

	public List<NodeItem> getNeibourhood(NodeItem node)
	{
		List<NodeItem> list = new List<NodeItem>();
		if (node.x % 2 == 0)
		{
			for (int i = -1; i <= 1; i++)
			{
				if (i == 0)
				{
					for (int j = -1; j <= 1; j++)
					{
						if (i != 0 || j != 0)
						{
							int num = node.x + i;
							int num2 = node.y + j;
							if (num < w && num >= 0 && num2 < h && num2 >= 0)
							{
								list.Add(grid[num, num2]);
							}
						}
					}
					continue;
				}
				for (int k = -1; k <= 0; k++)
				{
					if (i != 0 || k != 0)
					{
						int num3 = node.x + i;
						int num4 = node.y + k;
						if (num3 < w && num3 >= 0 && num4 < h && num4 >= 0)
						{
							list.Add(grid[num3, num4]);
						}
					}
				}
			}
		}
		else
		{
			for (int l = -1; l <= 1; l++)
			{
				if (l == 0)
				{
					for (int m = -1; m <= 1; m++)
					{
						if (l != 0 || m != 0)
						{
							int num5 = node.x + l;
							int num6 = node.y + m;
							if (num5 < w && num5 >= 0 && num6 < h && num6 >= 0)
							{
								list.Add(grid[num5, num6]);
							}
						}
					}
					continue;
				}
				for (int n = 0; n <= 1; n++)
				{
					if (l != 0 || n != 0)
					{
						int num7 = node.x + l;
						int num8 = node.y + n;
						if (num7 < w && num7 >= 0 && num8 < h && num8 >= 0)
						{
							list.Add(grid[num7, num8]);
						}
					}
				}
			}
		}
		return list;
	}

	public bool updatePath(List<NodeItem> lines, bool btest = false)
	{
		Vector3 localPosition = GameUI.action.QiuPos.transform.localPosition;
		float num = localPosition.y;
		if (Singleton<DataManager>.Instance.bLiuhai)
		{
			num -= 0.65f;
		}
		List<Vector2> list = new List<Vector2>();
		int i = 0;
		for (int count = lines.Count; i < count; i++)
		{
			if (btest)
			{
				return true;
			}
			list.Add(new Vector2(int.Parse(lines[i].pos.x + string.Empty), int.Parse(lines[i].pos.y + string.Empty)));
		}
		int num2 = 0;
		int num3 = 0;
		if (list.Count > 0)
		{
			Vector3[] array = new Vector3[list.Count];
			for (int j = 0; j < list.Count; j++)
			{
				Vector2 vector = list[j];
				num2 = int.Parse(vector.x + string.Empty);
				Vector2 vector2 = list[j];
				num3 = int.Parse(vector2.y + string.Empty);
				array[j] = BubbleSpawner.Instance.BubbleFlyArray[num2, num3].transform.localPosition + new Vector3(0f, num, 0f);
			}
			if (!btest)
			{
				FindPath.bpath = true;
				Twaypoints = array;
				Tx = num2;
				Ty = num3;
				TCount = list.Count;
			}
		}
		return false;
	}

	public void UnityMoveFly()
	{
		iPosIndex = 0;
		if (!FindPath.bpath2)
		{
			FindPath.bpath2 = true;
			IEUnityMoveFly();
		}
	}

	private void Update()
	{
		if (FindPath.bpath2)
		{
			if ((bool)objS)
			{
				Vector3 position = objS.transform.position;
				Vector3 position2 = objS.transform.position;
				Vector3 up = position2 - obj.transform.position;
				up.z = 0f;
				up = up.normalized;
				obj.transform.up = up;
				MapMoveSpawner.Instance.MoveLevelUp();
			}
		}
		else if ((bool)obj)
		{
			obj.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		}
	}

	private void IEUnityMoveFly()
	{
		BubbleSpawner.Instance.BubbleFlyObj.GetComponent<BubbleObj>().FlyReady();
	}

	public void FlyGrid()
	{
		if (!obj)
		{
			obj = BubbleSpawner.Instance.BubbleFlyObj.GetComponent<BubbleObj>().fx_flyobj_;
		}
		if (!objS)
		{
			objS = UnityEngine.Object.Instantiate(obj, obj.transform.position, obj.transform.rotation);
			objS.transform.parent = obj.transform.parent;
			objS.SetActive(value: false);
		}
		objS.transform.DOPath(Twaypoints, 0.1099f * (float)TCount, PathType.CatmullRom, PathMode.Full3D, 20).SetEase(Ease.Linear);
		obj.transform.DOPath(Twaypoints, 0.11f * (float)TCount, PathType.CatmullRom, PathMode.Full3D, 20).SetEase(Ease.Linear).OnComplete(delegate
		{
			ResUpdate();
		});
		if (Tx == 0)
		{
			PassLevel.bWin = true;
		}
		StartCoroutine(IEupMove(0.1099f * (float)TCount));
	}

	private IEnumerator IEupMove(float ftime)
	{
		UnityEngine.Debug.Log("IEupMove ftime=" + ftime);
		yield return new WaitForSeconds(0.5f);
		MapMoveSpawner.Instance.BubbleFlyObjrow = Tx;
		MapMoveSpawner.Instance.BubbleFlyObjcol = Ty;
		MapMoveSpawner.Instance.MoveLevelUp();
	}

	public void finfpathMoveTopUp()
	{
	}

	public void ResUpdate()
	{
		BubbleSpawner.Instance.BubbleFlyObj.GetComponent<BubbleObj>().BubbleFlyAnimationFlyStop();
		BubbleObj component = BubbleSpawner.Instance.BubbleFlyObj.GetComponent<BubbleObj>();
		component.mBubbleData.row = Tx;
		component.mBubbleData.col = Ty;
		MapMoveSpawner.Instance.BubbleFlyObjrow = Tx;
		MapMoveSpawner.Instance.BubbleFlyObjcol = Ty;
		MapMoveSpawner.Instance.MoveLevelUp();
		int num = UnityEngine.Random.Range(1, 4);
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("vo_elf_save" + num);
		}
		if (Tx == 0)
		{
			PassLevel.bWin = true;
			PassLevel.action.FlyOver();
			PassLevel.action.CreateFlyBubbleObj2();
			GameUI.action.FlyBg.SetActive(value: false);
		}
		FindPath.bpath2 = false;
		if (!PassLevel.bWin)
		{
			FindPath.bpath = false;
		}
	}
}
