using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveController : MonoBehaviour
{
	public static RemoveController Instance;

	public bool isFallBubble;

	public bool isFallBubbleCheck;

	private float skillRemoveTime;

	private int shootIndex;

	public static bool bwhileturestart;

	public bool bMoveNextBubble = true;

	private bool binit = true;

	public bool bBoss3Change;

	private void Start()
	{
		Instance = this;
		bwhileturestart = false;
	}

	public void CheckRemove(GameObject bubble, string mkey = "")
	{
		BubbleSpawner.Instance.isCheckMove = true;
		StartCoroutine(BubbleSpawner.Instance.checkMove());
		float num = -1000f;
		skillRemoveTime = 0f;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		BubbleObj component = bubble.GetComponent<BubbleObj>();
		BUBBLEDATA mBubbleData = component.mBubbleData;
		int num2 = 0;
		if (component.skillhuo)
		{
			num2++;
		}
		if (component.skillmu)
		{
			num2++;
		}
		if (component.skilldian)
		{
			num2++;
		}
		if (component.skillbing)
		{
			num2++;
		}
		if (component.skillhuo || component.skillmu || component.skilldian || component.skillbing || component.skillzhadan || component.skillbbb)
		{
			int num3 = 0;
			if (bubble.GetComponent<BubbleObj>().skillbing)
			{
				num3++;
			}
			if (bubble.GetComponent<BubbleObj>().skillmu)
			{
				num3++;
			}
			if (bubble.GetComponent<BubbleObj>().skilldian)
			{
				num3++;
			}
			if (bubble.GetComponent<BubbleObj>().skillhuo)
			{
				num3++;
			}
			if (component.skillbing && num3 < 3)
			{
				if (BubbleSpawner.Instance.skillBingCount < 3)
				{
					BubbleSpawner.Instance.skillBingsquares[BubbleSpawner.Instance.skillBingCount - 1] = bubble;
					BubbleSpawner.Instance.skillBingCount++;
					BubbleSpawner.Instance.createReadyBubbleBing(bubble);
					isFallBubbleCheck = false;
					checkFall();
					return;
				}
				if ((bool)BubbleSpawner.Instance.skillBingsquares[0])
				{
					Vector3 position = BubbleSpawner.Instance.skillBingsquares[0].transform.position;
					num = position.y;
				}
				if ((bool)BubbleSpawner.Instance.skillBingsquares[1])
				{
					Vector3 position2 = BubbleSpawner.Instance.skillBingsquares[1].transform.position;
					if (position2.y > num)
					{
						Vector3 position3 = BubbleSpawner.Instance.skillBingsquares[1].transform.position;
						num = position3.y;
					}
				}
				Vector3 position4 = bubble.transform.position;
				if (position4.y > num)
				{
					Vector3 position5 = bubble.transform.position;
					num = position5.y;
				}
				BubbleSpawner.Instance.skillBingCount = 0;
				float num4 = num;
				Vector3 position6 = Camera.main.transform.position;
				if (num4 > position6.y + 6f)
				{
					flag = true;
					skillRemoveTime = MapMoveSpawner.Instance.GetTime(num);
				}
				if ((bool)BubbleSpawner.Instance.skillBingsquares[0])
				{
					BubbleSpawner.Instance.skillBingsquares[0].GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, skillRemoveTime);
				}
				if ((bool)BubbleSpawner.Instance.skillBingsquares[1])
				{
					BubbleSpawner.Instance.skillBingsquares[1].GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, skillRemoveTime);
				}
				bubble.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, skillRemoveTime);
			}
			else
			{
				Vector3 position7 = bubble.transform.position;
				num = position7.y;
				float num5 = num;
				Vector3 position8 = Camera.main.transform.position;
				if (num5 > position8.y + 6f)
				{
					skillRemoveTime = MapMoveSpawner.Instance.GetTime(num);
					flag = true;
				}
				bubble.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, skillRemoveTime);
			}
			flag2 = true;
		}
		int num6 = 0;
		BUBBLEDATA mBubbleData2 = bubble.GetComponent<BubbleObj>().mBubbleData;
		if (int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData2.key]["attributes"]) == 14)
		{
			flag2 = true;
		}
		if (int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData2.key]["attributes"]) == 15)
		{
			UnityEngine.Debug.Log("mkey=" + mkey);
			if (mkey == null || mkey == string.Empty)
			{
				bubble.GetComponent<BubbleObj>().isRemoveByJingling = true;
				bubble.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, 1f);
			}
			else
			{
				IEnumerator enumerator = BubbleSpawner.Instance.BallParent.transform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						Transform transform = (Transform)enumerator.Current;
						if ((bool)transform.GetComponent<BubbleObj>())
						{
							int row = transform.GetComponent<BubbleObj>().mBubbleData.row;
							if (row > num6)
							{
								num6 = row;
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
				IEnumerator enumerator2 = BubbleSpawner.Instance.BallParent.transform.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						Transform transform2 = (Transform)enumerator2.Current;
						if ((bool)transform2.GetComponent<BubbleObj>() && transform2.GetComponent<BubbleObj>().mType == int.Parse(Singleton<DataManager>.Instance.dBubble[mkey]["type"]))
						{
							int row2 = transform2.GetComponent<BubbleObj>().mBubbleData.row;
							if (num6 - 12 < row2)
							{
								GameObject gameObject = transform2.gameObject;
								if ((bool)gameObject.GetComponent<BubbleObj>().SubMenObj && !gameObject.GetComponent<BubbleObj>().bMenOpen)
								{
									continue;
								}
								gameObject.GetComponent<BubbleObj>().fx_mofa_elf_selectFunc();
								float num7 = 1f;
								if (BubbleSpawner.Instance.Combo >= 5 && BubbleSpawner.Instance.Combo < 10)
								{
									num7 = 2f;
								}
								else if (BubbleSpawner.Instance.Combo >= 10)
								{
									num7 = 3f;
								}
								int score = (int)(250f * num7);
								bubble.GetComponent<BubbleObj>().isAddScore = true;
								bubble.GetComponent<BubbleObj>().AddScore(score);
								bubble.GetComponent<BubbleObj>().isAddScore = false;
								bubble.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, 0.05f);
								gameObject.GetComponent<BubbleObj>().isAddScore = true;
								gameObject.GetComponent<BubbleObj>().AddScore(score);
								gameObject.GetComponent<BubbleObj>().isAddScore = false;
								gameObject.GetComponent<BubbleObj>().isRemoveByJingling = true;
								gameObject.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, 1f);
							}
						}
						if (!bubble.GetComponent<BubbleObj>().SubMenObj || bubble.GetComponent<BubbleObj>().bMenOpen)
						{
							bubble.GetComponent<BubbleObj>().isRemoveByJingling = true;
							bubble.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, 1f);
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
			flag2 = true;
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("skill_super_4");
			}
		}
		List<Vector2> list = findClearBubble(component);
		for (int i = 0; i < list.Count; i++)
		{
			GameObject[,] bubbleArray = BubbleSpawner.Instance.BubbleArray;
			Vector2 vector = list[i];
			int num8 = (int)vector.x;
			Vector2 vector2 = list[i];
			GameObject gameObject2 = bubbleArray[num8, (int)vector2.y];
			if ((bool)gameObject2)
			{
				gameObject2.GetComponent<BubbleObj>().isReadyRemove = true;
			}
		}
		List<Vector2> around = BubbleSpawner.Instance.GetAround(component.mBubbleData.row, component.mBubbleData.col);
		for (int j = 0; j < around.Count; j++)
		{
			GameObject[,] bubbleArray2 = BubbleSpawner.Instance.BubbleArray;
			Vector2 vector3 = around[j];
			int num9 = (int)vector3.x;
			Vector2 vector4 = around[j];
			GameObject gameObject3 = bubbleArray2[num9, (int)vector4.y];
			if (!gameObject3 || gameObject3.GetComponent<BubbleObj>().isCheck)
			{
				continue;
			}
			bool flag4 = false;
			bool flag5 = false;
			BubbleObj component2 = gameObject3.GetComponent<BubbleObj>();
			int num10 = int.Parse(Singleton<DataManager>.Instance.dBubble[component2.mBubbleData.key]["attributes"]);
			int num11 = int.Parse(Singleton<DataManager>.Instance.dBubble[component2.mBubbleData.key]["type"]);
			component2.PlayRemoveCloud();
			component2.PlayRemoveGanran();
			switch (num10)
			{
			case 4:
			{
				List<Vector2> around2 = BubbleSpawner.Instance.GetAround(component.mBubbleData.row, component.mBubbleData.col);
				for (int k = 0; k < around2.Count; k++)
				{
					GameObject[,] bubbleArray3 = BubbleSpawner.Instance.BubbleArray;
					Vector2 vector5 = around2[k];
					int num12 = (int)vector5.x;
					Vector2 vector6 = around2[k];
					GameObject gameObject5 = bubbleArray3[num12, (int)vector6.y];
					if ((bool)gameObject5 && (bool)gameObject5.GetComponent<BubbleObj>() && gameObject5.GetComponent<BubbleObj>().mBubbleData.key == "G")
					{
						gameObject5.GetComponent<BubbleObj>().RemoveBubble();
					}
				}
				GameObject gameObject6 = BubbleSpawner.Instance.BubbleArray[component.mBubbleData.row, component.mBubbleData.col];
				if ((bool)gameObject6 && !gameObject6.GetComponent<BubbleObj>().isRemove)
				{
					gameObject6.GetComponent<BubbleObj>().isRemoveByTiechi = true;
					gameObject6.GetComponent<BubbleObj>().RemoveBubble();
				}
				if ((bool)SoundController.action)
				{
					SoundController.action.playNow("b_spikey");
				}
				break;
			}
			case 3:
				component2.RemoveBubble();
				break;
			case 31:
			{
				component2.RemoveBubble();
				GameObject gameObject7 = BubbleSpawner.Instance.BubbleArray[component.mBubbleData.row, component.mBubbleData.col];
				if ((bool)gameObject7 && !gameObject7.GetComponent<BubbleObj>().isRemove)
				{
					gameObject7.GetComponent<BubbleObj>().ResetPos();
					gameObject7.GetComponent<BubbleObj>().RemoveBubble();
				}
				flag2 = true;
				break;
			}
			case 5:
				component2.RemoveBubble();
				flag2 = true;
				flag3 = true;
				break;
			case 6:
			{
				component2.RemoveBubble();
				GameObject gameObject8 = BubbleSpawner.Instance.BubbleArray[component.mBubbleData.row, component.mBubbleData.col];
				if ((bool)gameObject8 && !gameObject8.GetComponent<BubbleObj>().isRemove)
				{
					gameObject8.GetComponent<BubbleObj>().ResetPos();
					gameObject8.GetComponent<BubbleObj>().RemoveBubble();
				}
				break;
			}
			default:
				if (num11 == 8)
				{
					if ((bool)SoundController.action)
					{
						SoundController.action.playNow("b_stone");
					}
					break;
				}
				switch (num10)
				{
				case 9:
				{
					flag2 = true;
					string text = component.mBubbleData.key;
					switch (text)
					{
					default:
						text = BubbleSpawner.Instance.GetBubbleRandomKey();
						break;
					case "A":
					case "B":
					case "C":
					case "D":
					case "E":
						break;
					}
					component2.Ranse(text);
					if ((bool)SoundController.action)
					{
						SoundController.action.play("b_color_ball");
					}
					GameObject gameObject4 = BubbleSpawner.Instance.BubbleArray[component.mBubbleData.row, component.mBubbleData.col];
					if ((bool)gameObject4 && !gameObject4.GetComponent<BubbleObj>().isRemove)
					{
						gameObject4.GetComponent<BubbleObj>().isRemoveByTiechi = true;
						gameObject4.GetComponent<BubbleObj>().RemoveBubble();
					}
					break;
				}
				case 10:
					flag4 = true;
					flag5 = true;
					flag2 = true;
					break;
				case 32:
					flag4 = true;
					flag5 = true;
					flag2 = true;
					break;
				case 11:
					flag5 = true;
					flag2 = true;
					break;
				case 12:
					flag4 = true;
					flag5 = true;
					flag2 = true;
					break;
				case 13:
					flag4 = true;
					flag5 = true;
					flag2 = true;
					break;
				}
				break;
			}
			if (flag4 && flag5)
			{
				Vector3 position9 = bubble.transform.position;
				if (position9.y > num)
				{
					Vector3 position10 = bubble.transform.position;
					num = position10.y;
				}
				float num13 = num;
				Vector3 position11 = Camera.main.transform.position;
				if (num13 > position11.y + 6f)
				{
					if (MapMoveSpawner.Instance.GetTime(num) > skillRemoveTime)
					{
						skillRemoveTime = MapMoveSpawner.Instance.GetTime(num);
					}
					flag = true;
				}
				GameObject gameObject9 = BubbleSpawner.Instance.BubbleArray[component.mBubbleData.row, component.mBubbleData.col];
				if ((bool)gameObject9 && !gameObject9.GetComponent<BubbleObj>().isRemove)
				{
					gameObject9.GetComponent<BubbleObj>().ResetPos();
					gameObject9.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, skillRemoveTime);
				}
				component2.RemoveBubble(isFallBubble: false, skillRemoveTime);
			}
			else
			{
				if (!flag5)
				{
					continue;
				}
				Vector3 position12 = bubble.transform.position;
				if (position12.y > num)
				{
					Vector3 position13 = bubble.transform.position;
					num = position13.y;
				}
				float num14 = num;
				Vector3 position14 = Camera.main.transform.position;
				if (num14 > position14.y + 6f)
				{
					if (MapMoveSpawner.Instance.GetTime(num) > skillRemoveTime)
					{
						skillRemoveTime = MapMoveSpawner.Instance.GetTime(num);
					}
					flag = true;
				}
				component2.RemoveBubble(isFallBubble: false, skillRemoveTime);
			}
		}
		switch (num2)
		{
		case 4:
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("combo1");
			}
			GameUI.action.ShowGameText(6, new Vector3(0f, 0f, 0f));
			PassLevel.action.GirlAni_fault(b: true);
			break;
		case 3:
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("combo1");
			}
			GameUI.action.ShowGameText(5, new Vector3(0f, 0f, 0f));
			PassLevel.action.GirlAni_fault(b: true);
			break;
		default:
		{
			float num15 = 1f;
			if (BubbleSpawner.Instance.Combo >= 5 && BubbleSpawner.Instance.Combo < 10)
			{
				num15 = 2f;
			}
			else if (BubbleSpawner.Instance.Combo >= 10)
			{
				num15 = 3f;
			}
			int num16 = num16 = (int)(10f * num15);
			if (list.Count >= 17)
			{
				if ((bool)SoundController.action)
				{
					SoundController.action.playNow("combo1");
				}
				for (int l = 0; l < list.Count; l++)
				{
					GameObject[,] bubbleArray4 = BubbleSpawner.Instance.BubbleArray;
					Vector2 vector7 = list[l];
					int num17 = (int)vector7.x;
					Vector2 vector8 = list[l];
					GameObject gameObject10 = bubbleArray4[num17, (int)vector8.y];
					if ((bool)gameObject10 && gameObject10.GetComponent<BubbleObj>().mType <= 5)
					{
						gameObject10.GetComponent<BubbleObj>().isAddScore = false;
					}
				}
				Vector3 position15 = bubble.transform.position;
				if (position15.x < 0f)
				{
					GameUI.action.ShowGameText(8, new Vector3(-176f, -80f, 0f), num16 * list.Count);
				}
				else
				{
					GameUI.action.ShowGameText(8, new Vector3(176f, -80f, 0f), num16 * list.Count);
				}
				PassLevel.action.GirlAni_lively1();
			}
			else if (list.Count >= 7)
			{
				if ((bool)SoundController.action)
				{
					SoundController.action.playNow("combo1");
				}
				for (int m = 0; m < list.Count; m++)
				{
					GameObject[,] bubbleArray5 = BubbleSpawner.Instance.BubbleArray;
					Vector2 vector9 = list[m];
					int num18 = (int)vector9.x;
					Vector2 vector10 = list[m];
					GameObject gameObject11 = bubbleArray5[num18, (int)vector10.y];
					if ((bool)gameObject11 && gameObject11.GetComponent<BubbleObj>().mType <= 5)
					{
						gameObject11.GetComponent<BubbleObj>().isAddScore = false;
					}
				}
				Vector3 position16 = bubble.transform.position;
				if (position16.x < 0f)
				{
					GameUI.action.ShowGameText(7, new Vector3(-176f, -80f, 0f), num16 * list.Count);
				}
				else
				{
					GameUI.action.ShowGameText(7, new Vector3(176f, -80f, 0f), num16 * list.Count);
				}
			}
			if (list.Count < 17)
			{
				PassLevel.action.GirlAni_happy2(b: true);
			}
			if (list.Count == 0 && num2 == 0 && !component.skillzhadan && !flag2)
			{
				PassLevel.action.GirlAni_fault();
				if ((bool)SoundController.action)
				{
					SoundController.action.playNow("b_hit_no_match");
				}
			}
			else
			{
				PassLevel.action.GirlAni_fault(b: true);
			}
			break;
		}
		}
		int num19 = 0;
		for (int n = 0; n < list.Count; n++)
		{
			GameObject[,] bubbleArray6 = BubbleSpawner.Instance.BubbleArray;
			Vector2 vector11 = list[n];
			int num20 = (int)vector11.x;
			Vector2 vector12 = list[n];
			GameObject gameObject12 = bubbleArray6[num20, (int)vector12.y];
			if ((bool)gameObject12 && gameObject12.GetComponent<BubbleObj>().mType <= 5)
			{
				int num21 = int.Parse(Singleton<DataManager>.Instance.dBubble[gameObject12.GetComponent<BubbleObj>().mBubbleData.key]["attributes"]);
				if (num21 == 100 || num21 == 101)
				{
					num19++;
				}
			}
		}
		if (num19 >= 6)
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("combo1");
			}
			GameUI.action.ShowGameText(4, new Vector3(0f, 0f, 0f));
		}
		else if (num19 >= 3)
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("combo1");
			}
			GameUI.action.ShowGameText(3, new Vector3(0f, 0f, 0f));
		}
		if (list.Count > 0)
		{
			if (!flag3)
			{
				BubbleSpawner.Instance.Combo++;
				BubbleSpawner.Instance.NoKill = 0;
			}
		}
		else if (!flag2)
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("b_hit_no_match");
			}
			if (!flag3)
			{
				BubbleSpawner.Instance.Combo = 0;
				BubbleSpawner.Instance.NoKill++;
			}
		}
		if (BubbleSpawner.Instance.NoKill > 3)
		{
		}
		GameUI.action.BubbleCombo(BubbleSpawner.Instance.Combo);
		BubbleSpawner.Instance.SetBubbleCheck();
		for (int num22 = 0; num22 < list.Count; num22++)
		{
			GameObject[,] bubbleArray7 = BubbleSpawner.Instance.BubbleArray;
			Vector2 vector13 = list[num22];
			int num23 = (int)vector13.x;
			Vector2 vector14 = list[num22];
			GameObject gameObject13 = bubbleArray7[num23, (int)vector14.y];
			if (!gameObject13 || gameObject13.GetComponent<BubbleObj>().mType > 5)
			{
				continue;
			}
			BubbleObj component3 = gameObject13.GetComponent<BubbleObj>();
			List<Vector2> around3 = BubbleSpawner.Instance.GetAround(component3.mBubbleData.row, component3.mBubbleData.col);
			for (int num24 = 0; num24 < around3.Count; num24++)
			{
				GameObject[,] bubbleArray8 = BubbleSpawner.Instance.BubbleArray;
				Vector2 vector15 = around3[num24];
				int num25 = (int)vector15.x;
				Vector2 vector16 = around3[num24];
				GameObject gameObject14 = bubbleArray8[num25, (int)vector16.y];
				if (!gameObject14)
				{
					continue;
				}
				BubbleObj component4 = gameObject14.GetComponent<BubbleObj>();
				int num26 = int.Parse(Singleton<DataManager>.Instance.dBubble[component4.mBubbleData.key]["attributes"]);
				if (num26 == 2 && !component4.isCheck)
				{
					string key = string.Empty;
					if (gameObject13.GetComponent<BubbleObj>().mType == 1)
					{
						key = "A";
					}
					else if (gameObject13.GetComponent<BubbleObj>().mType == 2)
					{
						key = "B";
					}
					else if (gameObject13.GetComponent<BubbleObj>().mType == 3)
					{
						key = "C";
					}
					else if (gameObject13.GetComponent<BubbleObj>().mType == 4)
					{
						key = "D";
					}
					else if (gameObject13.GetComponent<BubbleObj>().mType == 5)
					{
						key = "E";
					}
					component4.ChangeTo(key);
				}
			}
		}
		int num27 = 0;
		for (int num28 = 0; num28 < list.Count; num28++)
		{
			GameObject[,] bubbleArray9 = BubbleSpawner.Instance.BubbleArray;
			Vector2 vector17 = list[num28];
			int num29 = (int)vector17.x;
			Vector2 vector18 = list[num28];
			GameObject gameObject15 = bubbleArray9[num29, (int)vector18.y];
			if ((bool)gameObject15)
			{
				gameObject15.GetComponent<BubbleObj>().RemoveBubble();
			}
		}
		if (flag)
		{
			MapMoveSpawner.Instance.MoveCamera(num);
		}
		else
		{
			isFallBubbleCheck = true;
		}
		shootIndex++;
		Singleton<DataManager>.Instance.bPlayzhizhuOne = true;
		UnityEngine.Debug.Log("shootIndex++");
		StartCoroutine(FindSuo());
		if (Singleton<LevelManager>.Instance.bBossHuang)
		{
			Singleton<LevelManager>.Instance.bBossHuang3Change++;
			if (Singleton<LevelManager>.Instance.bBossHuang3Change % 3 == 0 && !PassLevel.bWin)
			{
				bBoss3Change = true;
				UnityEngine.Debug.Log("bBoss3Change=true");
				StartCoroutine(IEBossHuangChange3());
			}
		}
	}

	public void MoveNextBubble()
	{
		if (bMoveNextBubble)
		{
			bMoveNextBubble = false;
			StartCoroutine(IEMoveNextBubble());
		}
	}

	public IEnumerator IEMoveNextBubble()
	{
		yield return new WaitForSeconds(0.1f);
		while (true)
		{
			MoveNextBubbleSSS();
			if (FindPath.bpath)
			{
				FindPath.action.MovePeople();
			}
			yield return new WaitForSeconds(0.5f);
		}
	}

	public void MoveNextBubbleSSS()
	{
		if (!PassLevel.bWin && bwhileturestart && Singleton<LevelManager>.Instance.bflylevel && !FindPath.bpath)
		{
			if (binit)
			{
				binit = false;
				Grid.action.IEUpdate();
			}
			else if (Grid.action.IEUpdate())
			{
				return;
			}
			Vector2 vector = FindOverPos(b: true);
			if (!(vector.x < 0f))
			{
				BubbleObj component = BubbleSpawner.Instance.BubbleFlyObj.GetComponent<BubbleObj>();
				int num = int.Parse(vector.x + string.Empty);
				int row = component.mBubbleData.row;
				GameObject gameObject = BubbleSpawner.Instance.BubbleArray[component.mBubbleData.row, component.mBubbleData.col];
				FindPath.action.FindingPath(component.mBubbleData.row, component.mBubbleData.col, int.Parse(vector.x + string.Empty), int.Parse(vector.y + string.Empty));
			}
		}
	}

	public Vector2 FindOverPos(bool b)
	{
		if (!BubbleSpawner.Instance.BubbleFlyObj)
		{
			return new Vector2(-1f, -1f);
		}
		BubbleObj component = BubbleSpawner.Instance.BubbleFlyObj.GetComponent<BubbleObj>();
		int num = component.mBubbleData.row;
		int num2 = component.mBubbleData.col;
		for (int num3 = component.mBubbleData.row + 1; num3 >= 0; num3--)
		{
			bool flag = true;
			for (int i = 0; i < BubbleSpawner.cols; i++)
			{
				int num4 = 0;
				switch (i)
				{
				case 0:
					num4 = 5;
					break;
				case 1:
					num4 = 4;
					break;
				case 2:
					num4 = 6;
					break;
				case 3:
					num4 = 3;
					break;
				case 4:
					num4 = 7;
					break;
				case 5:
					num4 = 2;
					break;
				case 6:
					num4 = 8;
					break;
				case 7:
					num4 = 1;
					break;
				case 8:
					num4 = 9;
					break;
				case 9:
					num4 = 10;
					break;
				case 10:
					num4 = 0;
					break;
				}
				if (num3 % 2 == 0 || num4 != 10)
				{
					GameObject exists = BubbleSpawner.Instance.BubbleArray[num3, num4];
					if (!exists && FindPath.action.FindingPath(num, num2, num3, num4, btest: true))
					{
						num = num3;
						num2 = num4;
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				break;
			}
		}
		if (component.mBubbleData.row == num && component.mBubbleData.col == num2)
		{
			return new Vector2(-1f, -1f);
		}
		return new Vector2(num, num2);
	}

	public void SwitchAniMax(GameObject obj, string AniName, bool bboss = false)
	{
		SkeletonAnimation component = obj.GetComponent<SkeletonAnimation>();
		component.Initialize(overwrite: true);
		component.loop = false;
		if (AniName == "idle")
		{
			component.state.SetAnimation(0, AniName, loop: true);
			return;
		}
		component.state.SetAnimation(0, AniName, loop: false);
		component.state.End += delegate
		{
			SwitchAniMax(obj, "idle", bboss: true);
		};
	}

	private IEnumerator IEBossHuangChange3()
	{
		bool b = true;
		bool bskill = true;
		yield return new WaitForSeconds(0.001f);
		while (b)
		{
			if (BubbleSpawner.Instance.isCheckMove)
			{
				while (b)
				{
					yield return new WaitForSeconds(0.001f);
					if (BubbleSpawner.Instance.isCheckMove)
					{
						continue;
					}
					yield return new WaitForSeconds(0.001f);
					bBoss3Change = true;
					SwitchAniMax(BubbleSpawner.Instance.Bossobj, "skill");
					GameUI.action.GameBG.GetComponent<SpriteRenderer>().color = new Color(20f / 51f, 20f / 51f, 20f / 51f, 1f);
					yield return new WaitForSeconds(1.5f);
					GameUI.action.GameBG.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
					if ((bool)SoundController.action)
					{
						SoundController.action.playNow("sfx_snails_shield");
					}
					for (int i = 0; i < Singleton<LevelManager>.Instance.LTbubbleBoss.Count; i++)
					{
						BUBBLEDATA data = Singleton<LevelManager>.Instance.LTbubbleBoss[i];
						GameObject gameObject = BubbleSpawner.Instance.BubbleArray[data.row, data.col];
						if ((bool)gameObject)
						{
							gameObject.GetComponent<BubbleObj>().Boss3Change();
							continue;
						}
						int num = int.Parse(Singleton<DataManager>.Instance.dBubble[data.key]["type"]);
						if (num >= 1 && num <= 5)
						{
							string text = "A_B_C_D_E";
							string[] array = text.Split('_');
							int num2 = UnityEngine.Random.Range(0, 5);
							data.key = array[num2];
						}
						else if (data.key == "F" || data.key == "G" || data.key == "H" || data.key == "I" || data.key == "J" || data.key == "K")
						{
							string text2 = "F_G_H_I_J_K";
							string[] array2 = text2.Split('_');
							int num3 = UnityEngine.Random.Range(0, 6);
							string text3 = data.key = array2[num3];
						}
						else if (data.key == "O" || data.key == "L" || data.key == "M" || data.key == "N")
						{
							string text4 = "O_L_M_N";
							string[] array3 = text4.Split('_');
							int num4 = UnityEngine.Random.Range(0, 4);
							string text5 = data.key = array3[num4];
						}
						BubbleSpawner.Instance.SpawnerInitData(data, bskill);
						bskill = false;
					}
					bool bb = true;
					while (bb)
					{
						int icount = 0;
						IEnumerator enumerator = BubbleSpawner.Instance.BallParent.transform.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								Transform transform = (Transform)enumerator.Current;
								BubbleObj component = transform.GetComponent<BubbleObj>();
								if ((bool)component && !component.BbossInit)
								{
									icount++;
									component.RemoveBubble(isFallBubble: false, 0f, bskill: false, bboss3Kill: false);
								}
							}
						}
						finally
						{
							IDisposable disposable;
							IDisposable disposable2 = disposable = (enumerator as IDisposable);
							if (disposable != null)
							{
								disposable2.Dispose();
							}
						}
						if (icount == 0)
						{
							bb = false;
						}
						yield return new WaitForSeconds(0.1f);
					}
					b = false;
					bBoss3Change = false;
				}
			}
			yield return new WaitForSeconds(0.1f);
		}
	}

	public void ClearBossOther()
	{
		IEnumerator enumerator = BubbleSpawner.Instance.BallParent.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				BubbleObj component = transform.GetComponent<BubbleObj>();
				if ((bool)component && !component.BbossInit)
				{
					component.RemoveBubble();
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

	public IEnumerator FindSuo()
	{
		yield return new WaitForSeconds(skillRemoveTime + 0.1f);
		List<Vector2> SuoList = new List<Vector2>();
		for (int i = 0; i < BubbleSpawner.cols; i++)
		{
			for (int j = 0; j < BubbleSpawner.rows; j++)
			{
				GameObject gameObject = BubbleSpawner.Instance.BubbleArray[j, i];
				if ((bool)gameObject && (bool)gameObject.GetComponent<BubbleObj>().fx_suo_obj && !gameObject.GetComponent<BubbleObj>().isRemove)
				{
					SuoList.Add(new Vector2(gameObject.GetComponent<BubbleObj>().mBubbleData.row, gameObject.GetComponent<BubbleObj>().mBubbleData.col));
				}
			}
		}
		for (int k = 0; k < SuoList.Count; k++)
		{
			BubbleSpawner instance = BubbleSpawner.Instance;
			Vector2 vector = SuoList[k];
			int nRow = (int)vector.x;
			Vector2 vector2 = SuoList[k];
			List<Vector2> around = instance.GetAround(nRow, (int)vector2.y);
			for (int l = 0; l < around.Count; l++)
			{
				GameObject[,] bubbleArray = BubbleSpawner.Instance.BubbleArray;
				Vector2 vector3 = around[l];
				int num = (int)vector3.x;
				Vector2 vector4 = around[l];
				GameObject gameObject2 = bubbleArray[num, (int)vector4.y];
				if (!gameObject2)
				{
					continue;
				}
				BubbleObj component = gameObject2.GetComponent<BubbleObj>();
				if (component.isRemove || !component.fx_lian_obj)
				{
					continue;
				}
				bool flag = false;
				for (int m = 0; m < SuoList.Count; m++)
				{
					Vector2 vector5 = SuoList[m];
					if ((int)vector5.x == component.mBubbleData.row)
					{
						Vector2 vector6 = SuoList[m];
						if ((int)vector6.y == component.mBubbleData.col)
						{
							flag = true;
						}
					}
				}
				if (!flag)
				{
					SuoList.Add(new Vector2(component.mBubbleData.row, component.mBubbleData.col));
				}
			}
		}
		List<Vector2> LianList = new List<Vector2>();
		for (int n = 0; n < BubbleSpawner.cols; n++)
		{
			for (int num2 = 0; num2 < BubbleSpawner.rows; num2++)
			{
				GameObject gameObject3 = BubbleSpawner.Instance.BubbleArray[num2, n];
				if (!gameObject3 || !gameObject3.GetComponent<BubbleObj>().fx_lian_obj)
				{
					continue;
				}
				BubbleObj component2 = gameObject3.GetComponent<BubbleObj>();
				bool flag2 = false;
				for (int num3 = 0; num3 < SuoList.Count; num3++)
				{
					Vector2 vector7 = SuoList[num3];
					if ((int)vector7.x == component2.mBubbleData.row)
					{
						Vector2 vector8 = SuoList[num3];
						if ((int)vector8.y == component2.mBubbleData.col)
						{
							flag2 = true;
						}
					}
				}
				if (!flag2)
				{
					LianList.Add(new Vector2(component2.mBubbleData.row, component2.mBubbleData.col));
				}
			}
		}
		for (int num4 = 0; num4 < LianList.Count; num4++)
		{
			GameObject[,] bubbleArray2 = BubbleSpawner.Instance.BubbleArray;
			Vector2 vector9 = LianList[num4];
			int num5 = (int)vector9.x;
			Vector2 vector10 = LianList[num4];
			GameObject gameObject4 = bubbleArray2[num5, (int)vector10.y];
			if ((bool)gameObject4)
			{
				gameObject4.GetComponent<BubbleObj>().RemoveBubble();
			}
		}
	}

	public void checkFall(bool fallBubble = true)
	{
		List<Vector2> noLinkBubbleList = checkFallBubble();
		List<Vector2> list = checkFallBubbleGuadian(noLinkBubbleList);
		for (int i = 0; i < list.Count; i++)
		{
			GameObject[,] bubbleArray = BubbleSpawner.Instance.BubbleArray;
			Vector2 vector = list[i];
			int num = (int)vector.x;
			Vector2 vector2 = list[i];
			GameObject gameObject = bubbleArray[num, (int)vector2.y];
			if ((bool)gameObject)
			{
				BubbleObj component = gameObject.GetComponent<BubbleObj>();
				int num2 = int.Parse(Singleton<DataManager>.Instance.dBubble[component.mBubbleData.key]["down"]);
				if (num2 == 1)
				{
					gameObject.GetComponent<BubbleObj>().RemoveBubble();
				}
			}
		}
		List<Vector2> noLinkBubbleList2 = checkFallBubble();
		List<Vector2> list2 = checkFallBubbleGuadian(noLinkBubbleList2);
		for (int j = 0; j < list2.Count; j++)
		{
			GameObject[,] bubbleArray2 = BubbleSpawner.Instance.BubbleArray;
			Vector2 vector3 = list2[j];
			int num3 = (int)vector3.x;
			Vector2 vector4 = list2[j];
			GameObject gameObject2 = bubbleArray2[num3, (int)vector4.y];
			if ((bool)gameObject2)
			{
				gameObject2.GetComponent<BubbleObj>().isFall = true;
			}
		}
		if (list2.Count > 10)
		{
		}
		isFallBubble = fallBubble;
	}

	public void FallBubble(bool opendoor = true)
	{
		List<Vector2> noLinkBubbleList = checkFallBubble();
		List<Vector2> list = checkFallBubbleGuadian(noLinkBubbleList);
		if (list.Count >= 16)
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("combo2");
			}
			GameUI.action.ShowGameText(10, new Vector3(0f, -176f, 0f));
			PassLevel.action.GirlAni_happy2();
		}
		else if (list.Count > 6)
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("combo2");
			}
			GameUI.action.ShowGameText(9, new Vector3(0f, -176f, 0f));
		}
		if (list.Count < 16)
		{
			PassLevel.action.GirlAni_happy2(b: true);
		}
		for (int i = 0; i < list.Count; i++)
		{
			GameObject[,] bubbleArray = BubbleSpawner.Instance.BubbleArray;
			Vector2 vector = list[i];
			int num = (int)vector.x;
			Vector2 vector2 = list[i];
			GameObject gameObject = bubbleArray[num, (int)vector2.y];
			if ((bool)gameObject)
			{
				BubbleObj component = gameObject.GetComponent<BubbleObj>();
				int num2 = int.Parse(Singleton<DataManager>.Instance.dBubble[component.mBubbleData.key]["down"]);
				if (num2 == 1)
				{
					gameObject.GetComponent<BubbleObj>().RemoveBubble();
				}
				else
				{
					gameObject.GetComponent<BubbleObj>().FallBubble();
				}
			}
		}
		checkFall(fallBubble: false);
		BubbleSpawner.Instance.ChangeRandom(opendoor);
		MapMoveSpawner.Instance.MoveLevelUp();
	}

	public void Playzhizhu()
	{
		int num = 0;
		if (!Singleton<DataManager>.Instance.bPlayzhizhuOne || shootIndex % 3 != 0 || shootIndex == 0)
		{
			return;
		}
		Singleton<DataManager>.Instance.bPlayzhizhuOne = false;
		IEnumerator enumerator = BubbleSpawner.Instance.BallParent.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				if ((bool)transform.GetComponent<BubbleObj>())
				{
					int row = transform.GetComponent<BubbleObj>().mBubbleData.row;
					if (row > num)
					{
						num = row;
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
		List<BubbleObj> list = new List<BubbleObj>();
		List<BubbleObj> list2 = new List<BubbleObj>();
		IEnumerator enumerator2 = BubbleSpawner.Instance.BallParent.transform.GetEnumerator();
		try
		{
			while (enumerator2.MoveNext())
			{
				Transform transform2 = (Transform)enumerator2.Current;
				if ((bool)transform2.GetComponent<BubbleObj>())
				{
					BubbleObj component = transform2.GetComponent<BubbleObj>();
					int num2 = int.Parse(Singleton<DataManager>.Instance.dBubble[component.mBubbleData.key]["attributes"]);
					if (!component.isRemove)
					{
						if (component.mType <= 5)
						{
							if (!component.SubMenObj && !(component.mBubbleData.key == "H") && !component.fx_suo_obj && !component.fx_lian_obj)
							{
								int row2 = transform2.GetComponent<BubbleObj>().mBubbleData.row;
								if (num - 11 < row2 && !component.fx_ganran_obj && (num2 == 0 || num2 == 7 || num2 == 8))
								{
									list.Add(component);
								}
							}
						}
						else
						{
							int row3 = component.mBubbleData.row;
							if (num - 11 < row3 && component.mBubbleData.key == "GR")
							{
								list2.Add(component);
							}
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
		for (int i = 0; i < list2.Count; i++)
		{
			int num3 = 3;
			if (list.Count < 3)
			{
				num3 = list.Count;
			}
			for (int j = 0; j < num3; j++)
			{
				int index = UnityEngine.Random.Range(0, list.Count - 1);
				list2[i].PlayZhiZhuSi(list[index]);
				list.RemoveAt(index);
			}
			list2[i].SwitchZhiZhu(list2[i].fx_zhizhu_obj, "attack");
		}
	}

	private List<Vector2> findClearBubble(BubbleObj pReadyBubble)
	{
		List<Vector2> list = findSameBubble(pReadyBubble);
		if (list.Count < 3)
		{
			list.Clear();
		}
		return list;
	}

	private List<Vector2> findSameBubble(BubbleObj pReadyBubble)
	{
		List<Vector2> list = new List<Vector2>();
		int row = pReadyBubble.mBubbleData.row;
		int col = pReadyBubble.mBubbleData.col;
		list.Add(new Vector2(row, col));
		int num = 0;
		bool flag = false;
		while (!flag)
		{
			num++;
			List<Vector2> sameBubble = GetSameBubble(list, pReadyBubble.mType);
			if (sameBubble.Count > 0)
			{
				for (int i = 0; i < sameBubble.Count; i++)
				{
					GameObject[,] bubbleArray = BubbleSpawner.Instance.BubbleArray;
					Vector2 vector = sameBubble[i];
					int num2 = (int)vector.x;
					Vector2 vector2 = sameBubble[i];
					GameObject gameObject = bubbleArray[num2, (int)vector2.y];
					if ((bool)gameObject)
					{
						gameObject.GetComponent<BubbleObj>().iRemoveIndex = num;
					}
					list.Add(sameBubble[i]);
				}
			}
			else
			{
				flag = true;
			}
		}
		return list;
	}

	public List<Vector2> GetSameBubble(List<Vector2> _list, int mType)
	{
		List<Vector2> list = new List<Vector2>();
		for (int i = 0; i < _list.Count; i++)
		{
			BubbleSpawner instance = BubbleSpawner.Instance;
			Vector2 vector = _list[i];
			int nRow = (int)vector.x;
			Vector2 vector2 = _list[i];
			List<Vector2> around = instance.GetAround(nRow, (int)vector2.y);
			for (int j = 0; j < around.Count; j++)
			{
				Vector2 vector3 = around[j];
				float x = (int)vector3.x;
				Vector2 vector4 = around[j];
				if (_list.Contains(new Vector2(x, (int)vector4.y)))
				{
					continue;
				}
				List<Vector2> list2 = list;
				Vector2 vector5 = around[j];
				float x2 = (int)vector5.x;
				Vector2 vector6 = around[j];
				if (list2.Contains(new Vector2(x2, (int)vector6.y)))
				{
					continue;
				}
				GameObject[,] bubbleArray = BubbleSpawner.Instance.BubbleArray;
				Vector2 vector7 = around[j];
				int num = (int)vector7.x;
				Vector2 vector8 = around[j];
				GameObject gameObject = bubbleArray[num, (int)vector8.y];
				if ((bool)gameObject)
				{
					BubbleObj component = gameObject.GetComponent<BubbleObj>();
					if (component.mType == mType && !component.fx_lian_obj && !component.fx_ganran_obj && (!component.SubMenObj || component.bMenOpen))
					{
						list.Add(new Vector2(component.mBubbleData.row, component.mBubbleData.col));
					}
				}
			}
		}
		return list;
	}

	private List<Vector2> checkFallBubbleGuadian(List<Vector2> NoLinkBubbleList)
	{
		if (Instance.isFallBubbleCheck)
		{
			return NoLinkBubbleList;
		}
		List<Vector2> list = new List<Vector2>();
		List<Vector2> list2 = new List<Vector2>();
		for (int i = 0; i < NoLinkBubbleList.Count; i++)
		{
			GameObject[,] bubbleArray = BubbleSpawner.Instance.BubbleArray;
			Vector2 vector = NoLinkBubbleList[i];
			int num = (int)vector.x;
			Vector2 vector2 = NoLinkBubbleList[i];
			GameObject gameObject = bubbleArray[num, (int)vector2.y];
			if ((bool)gameObject)
			{
				BubbleObj component = gameObject.GetComponent<BubbleObj>();
				int num2 = int.Parse(Singleton<DataManager>.Instance.dBubble[component.mBubbleData.key]["attributes"]);
				if (num2 == 5)
				{
					list2.Add(NoLinkBubbleList[i]);
				}
			}
		}
		for (int j = 0; j < list2.Count; j++)
		{
			BubbleSpawner instance = BubbleSpawner.Instance;
			Vector2 vector3 = list2[j];
			int nRow = (int)vector3.x;
			Vector2 vector4 = list2[j];
			List<Vector2> around = instance.GetAround(nRow, (int)vector4.y);
			for (int k = 0; k < around.Count; k++)
			{
				GameObject[,] bubbleArray2 = BubbleSpawner.Instance.BubbleArray;
				Vector2 vector5 = around[k];
				int num3 = (int)vector5.x;
				Vector2 vector6 = around[k];
				GameObject gameObject2 = bubbleArray2[num3, (int)vector6.y];
				if (!gameObject2)
				{
					continue;
				}
				BubbleObj component2 = gameObject2.GetComponent<BubbleObj>();
				if (component2.isRemove)
				{
					continue;
				}
				bool flag = false;
				for (int l = 0; l < list2.Count; l++)
				{
					Vector2 vector7 = list2[l];
					if ((int)vector7.x == component2.mBubbleData.row)
					{
						Vector2 vector8 = list2[l];
						if ((int)vector8.y == component2.mBubbleData.col)
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					list2.Add(new Vector2(component2.mBubbleData.row, component2.mBubbleData.col));
				}
			}
		}
		for (int m = 0; m < NoLinkBubbleList.Count; m++)
		{
			GameObject[,] bubbleArray3 = BubbleSpawner.Instance.BubbleArray;
			Vector2 vector9 = NoLinkBubbleList[m];
			int num4 = (int)vector9.x;
			Vector2 vector10 = NoLinkBubbleList[m];
			GameObject gameObject3 = bubbleArray3[num4, (int)vector10.y];
			if (!gameObject3)
			{
				continue;
			}
			BubbleObj component3 = gameObject3.GetComponent<BubbleObj>();
			if (component3.isRemove)
			{
				continue;
			}
			bool flag2 = false;
			for (int n = 0; n < list2.Count; n++)
			{
				Vector2 vector11 = list2[n];
				if ((int)vector11.x == component3.mBubbleData.row)
				{
					Vector2 vector12 = list2[n];
					if ((int)vector12.y == component3.mBubbleData.col)
					{
						flag2 = true;
						break;
					}
				}
			}
			if (!flag2)
			{
				list.Add(new Vector2(component3.mBubbleData.row, component3.mBubbleData.col));
			}
		}
		return list;
	}

	private List<Vector2> checkFallBubble()
	{
		List<Vector2> list = new List<Vector2>();
		for (int i = 0; i < BubbleSpawner.cols; i++)
		{
			GameObject gameObject = BubbleSpawner.Instance.BubbleArray[0, i];
			if ((bool)gameObject)
			{
				BubbleObj component = gameObject.GetComponent<BubbleObj>();
				if (!component.isRemove)
				{
					list.Add(new Vector2(0f, i));
				}
			}
		}
		List<Vector2> list2 = new List<Vector2>();
		if (list.Count == 0)
		{
			for (int j = 0; j < BubbleSpawner.rows; j++)
			{
				for (int k = 0; k < BubbleSpawner.cols - j % 2; k++)
				{
					if ((bool)BubbleSpawner.Instance.BubbleArray[j, k])
					{
						list2.Add(new Vector2(j, k));
					}
				}
			}
			return list2;
		}
		for (int l = 0; l < list.Count; l++)
		{
			BubbleSpawner instance = BubbleSpawner.Instance;
			Vector2 vector = list[l];
			int nRow = (int)vector.x;
			Vector2 vector2 = list[l];
			List<Vector2> around = instance.GetAround(nRow, (int)vector2.y);
			for (int m = 0; m < around.Count; m++)
			{
				GameObject[,] bubbleArray = BubbleSpawner.Instance.BubbleArray;
				Vector2 vector3 = around[m];
				int num = (int)vector3.x;
				Vector2 vector4 = around[m];
				GameObject gameObject2 = bubbleArray[num, (int)vector4.y];
				if (!gameObject2)
				{
					continue;
				}
				BubbleObj component2 = gameObject2.GetComponent<BubbleObj>();
				if (component2.isRemove)
				{
					continue;
				}
				bool flag = false;
				for (int n = 0; n < list.Count; n++)
				{
					Vector2 vector5 = list[n];
					if ((int)vector5.x == component2.mBubbleData.row)
					{
						Vector2 vector6 = list[n];
						if ((int)vector6.y == component2.mBubbleData.col)
						{
							flag = true;
						}
					}
				}
				if (!flag)
				{
					list.Add(new Vector2(component2.mBubbleData.row, component2.mBubbleData.col));
				}
			}
		}
		for (int num2 = 0; num2 < BubbleSpawner.rows; num2++)
		{
			for (int num3 = 0; num3 < BubbleSpawner.cols - num2 % 2; num3++)
			{
				GameObject gameObject3 = BubbleSpawner.Instance.BubbleArray[num2, num3];
				if (!gameObject3)
				{
					continue;
				}
				BubbleObj component3 = gameObject3.GetComponent<BubbleObj>();
				if (component3.isRemove)
				{
					continue;
				}
				bool flag2 = false;
				for (int num4 = 0; num4 < list.Count; num4++)
				{
					Vector2 vector7 = list[num4];
					if ((int)vector7.x == component3.mBubbleData.row)
					{
						Vector2 vector8 = list[num4];
						if ((int)vector8.y == component3.mBubbleData.col)
						{
							flag2 = true;
						}
					}
				}
				if (!flag2)
				{
					list2.Add(new Vector2(component3.mBubbleData.row, component3.mBubbleData.col));
				}
			}
		}
		return list2;
	}

	private void Update()
	{
		if (isFallBubbleCheck && (bool)BubbleSpawner.Instance)
		{
			int num = 0;
			IEnumerator enumerator = BubbleSpawner.Instance.RemoveParent.transform.GetEnumerator();
			try
			{
				if (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.Current;
					num++;
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
			if (num == 0)
			{
				isFallBubbleCheck = false;
				checkFall();
			}
		}
		if (isFallBubble)
		{
			int num2 = 0;
			IEnumerator enumerator2 = BubbleSpawner.Instance.RemoveParent.transform.GetEnumerator();
			try
			{
				if (enumerator2.MoveNext())
				{
					Transform transform2 = (Transform)enumerator2.Current;
					num2++;
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
			if (num2 == 0)
			{
				isFallBubble = false;
				FallBubble();
			}
		}
	}
}
