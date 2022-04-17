using System;
using System.Collections;
using UnityEngine;

public class MuTong2 : MonoBehaviour
{
	public int mutongindex;

	public MuTong mutong;

	public GameObject lizifly;

	private void Update()
	{
		if (!Input.GetMouseButtonUp(0))
		{
			return;
		}
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
		GameObject gameObject = TouchChecker(UnityEngine.Input.mousePosition);
		if (((bool)BubbleSpawner.Instance && BubbleSpawner.Instance.ready_1 == null) || !PassLevel.action.bGameStart || !gameObject || !gameObject.GetComponent<MuTong2>() || gameObject.GetComponent<MuTong2>().mutongindex != mutongindex || !mutong.isSkill)
		{
			return;
		}
		if (mutong.mtScore < mutong.iScore)
		{
			if (BubbleSpawner.Instance.isCanUseSkill() && !Util.GetNowOpenUI())
			{
				BubbleSpawner.Instance.buyskillGang = mutong.gameObject;
				GameUI.action.iGangType = mutong.mtIndex;
				if (!PassLevel.bWin)
				{
					GameGuide.Instance.OpenSkillUI();
					UI.Instance.OpenPanel(UIPanelType.BuyGangUI);
				}
			}
		}
		else if (BubbleSpawner.Instance.isCanUseGanSkill(mutong.mtIndex))
		{
			mutong.UseSkill();
			InitAndroid.action.GAEvent("usegang:" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex + ":" + mutong.mtIndex);
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("skill_use");
			}
			BubbleSpawner.Instance.ChangeToSkill(mutong.mtIndex, mutong.juneng);
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
