using System.Collections.Generic;
using UnityEngine;

public class FindPath : MonoBehaviour
{
	private Grid grid;

	public static FindPath action;

	public int xxx;

	public int yyy;

	public int xxxxxx;

	public int yyyyyy;

	public static bool bpath;

	public static bool bpath2;

	private void Start()
	{
		action = this;
		bpath = false;
		bpath2 = false;
		grid = GetComponent<Grid>();
	}

	public void finfpathMoveTopUp()
	{
		grid.finfpathMoveTopUp();
	}

	public void FlyGrid()
	{
		grid.FlyGrid();
	}

	private void Update()
	{
		if (xxx != 0 && yyy != 0 && xxxxxx != 0 && yyyyyy != 0)
		{
		}
	}

	public void MovePeople()
	{
		if (bpath && !PassLevel.bWin && !bpath2)
		{
			grid.UnityMoveFly();
		}
	}

	public bool FindingPath(int x, int y, int xx, int yy, bool btest = false)
	{
		if (!Singleton<LevelManager>.Instance.bflylevel)
		{
			return false;
		}
		Grid.NodeItem itemS = grid.getItemS(x, y);
		Grid.NodeItem itemS2 = grid.getItemS(xx, yy);
		List<Grid.NodeItem> list = new List<Grid.NodeItem>();
		HashSet<Grid.NodeItem> hashSet = new HashSet<Grid.NodeItem>();
		list.Add(itemS);
		while (list.Count > 0)
		{
			Grid.NodeItem nodeItem = list[0];
			int i = 0;
			for (int count = list.Count; i < count; i++)
			{
				if (list[i].fCost <= nodeItem.fCost && list[i].hCost < nodeItem.hCost)
				{
					nodeItem = list[i];
				}
			}
			list.Remove(nodeItem);
			hashSet.Add(nodeItem);
			if (nodeItem == itemS2)
			{
				return generatePath(itemS, itemS2, btest);
			}
			foreach (Grid.NodeItem item in grid.getNeibourhood(nodeItem))
			{
				if (!item.isWall && !hashSet.Contains(item))
				{
					int num = nodeItem.gCost + getDistanceNodes(nodeItem, item);
					if (num < item.gCost || !list.Contains(item))
					{
						item.gCost = num;
						item.hCost = getDistanceNodes(item, itemS2);
						item.parent = nodeItem;
						if (!list.Contains(item))
						{
							list.Add(item);
						}
					}
				}
			}
		}
		return generatePath(itemS, null, btest);
	}

	private bool generatePath(Grid.NodeItem startNode, Grid.NodeItem endNode, bool btest = false)
	{
		List<Grid.NodeItem> list = new List<Grid.NodeItem>();
		if (endNode != null)
		{
			for (Grid.NodeItem nodeItem = endNode; nodeItem != startNode; nodeItem = nodeItem.parent)
			{
				list.Add(nodeItem);
			}
			list.Reverse();
		}
		return grid.updatePath(list, btest);
	}

	private int getDistanceNodes(Grid.NodeItem a, Grid.NodeItem b)
	{
		int num = Mathf.Abs(a.x - b.x);
		int num2 = Mathf.Abs(a.y - b.y);
		if (num > num2)
		{
			return 14 * num2 + 10 * (num - num2);
		}
		return 14 * num + 10 * (num2 - num);
	}
}
