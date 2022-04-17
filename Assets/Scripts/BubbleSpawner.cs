using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using ITSoft;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
	public static bool IsWait;

	public static BubbleSpawner Instance;

	public GameObject ballPrefab;

	public GameObject boxPrefab;

	public GameObject BallParent;

	public GameObject LineParent;

	public GameObject TopBallFlyParent;

	private GameObject BoxParent;

	public GameObject FallParent;

	public GameObject FXParent;

	public GameObject RemoveParent;

	public GameObject ReadyBubbleParent;

	public GameObject ReadyBubbleParent1;

	public GameObject ReadyBubbleParent2;

	public GameObject skill4BubbleParent;

	public Sprite[] BubbleSprite;

	public GameObject[,] BubbleArray;

	public GameObject[,] BubbleFlyArray;

	public GameObject BubbleFlyObj;

	public GameObject[,] squares;

	public PhysicsMaterial2D fallPhysicsMaterial2D;

	public GameObject topobj;

	public GameObject buyskillGang;

	public GameObject Par_emitter_light;

	public GameObject Bossobj;

	public int Combo;

	public int NoKill;

	public int iReadyNum = 2;

	public bool isCheckMove;

	public static int cols = 11;

	public static int rows = 150;

	public float offsetStep = 0.32f;

	private int lastRow;

	public GameObject ready_1;

	public GameObject ready_2;

	public GameObject ready_3;

	public int skillBingCount;

	public GameObject[] skillBingsquares;

	public float shootBubbleTime;

	private bool initMoveLevelUp;

	private bool initEnd;

	public bool initReady;

	public bool isReadyMove;

	private float movetime = 0.2f;

	private List<BubbleShow> gameBubbleShow;

	private List<string> gameBubbleShow1;

	public bool useyanchangxian;

	public GameObject fxGameEndFire;

	public GameObject moveObj;

	public bool gameMove;

	public string readykey = string.Empty;

	public int readyindex;

	private int randomindex = 1;

	private int iOverShootBubbleCount;

	private static int iiOverShootBubbleCountIndex = -5;

	public bool bmoveaniState = true;

	private bool biiOverShootBubbleCountIndexstate = true;

	private void OnEnable()
	{
		IsWait = false;
	}

	private void Start()
	{
		Combo = 0;
		NoKill = 0;
		initEnd = false;
		Instance = this;
		initReady = false;
		isReadyMove = false;
		isCheckMove = false;
		gameMove = true;
		useyanchangxian = false;
		if (InitGame.bVip7)
		{
			useyanchangxian = true;
		}
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex <= 12)
		{
			useyanchangxian = true;
		}
		iReadyNum = 2;
		gameBubbleShow = new List<BubbleShow>();
		if (!LevelManager.bWwwDataFlag)
		{
			Singleton<LevelManager>.Instance.LoadLevelData();
		}
		if ((bool)GameUI.action)
		{
			GameUI.action.LoadBubbleCount();
		}
		Singleton<LevelManager>.Instance.CutLove();
		BallParent = GameObject.Find("-Ball");
		TopBallFlyParent = GameObject.Find("-BallFly");
		BoxParent = GameObject.Find("-Box");
		FallParent = GameObject.Find("-Fall");
		RemoveParent = GameObject.Find("-Remove");
		ReadyBubbleParent = GameObject.Find("-ReadyBubble");
		BubbleArray = new GameObject[rows, cols];
		BubbleFlyArray = new GameObject[rows, cols];
		squares = new GameObject[rows, cols];
		skillBingCount = 0;
		skillBingsquares = new GameObject[3];
		createBox();
		createBubble();
		StartCoroutine(GameInitEnd());
		if (Singleton<DataManager>.Instance.bLiuhai)
		{
			LineParent.transform.localPosition = new Vector3(0f, 5.77f, 0f);
			BallParent.transform.localPosition = new Vector3(0f, -0.65f, 0f);
			BoxParent.transform.localPosition = new Vector3(0f, -0.65f, 0f);
			FallParent.transform.localPosition = new Vector3(0f, -0.65f, 0f);
			RemoveParent.transform.localPosition = new Vector3(0f, -0.65f, 0f);
			TopBallFlyParent.transform.localPosition = new Vector3(0f, -0.65f, 0f);
		}
	}

	private IEnumerator GameInitEnd()
	{
		yield return new WaitForSeconds(0f);
		initEnd = true;
	}

	private void Update()
	{
		if (initEnd && !initMoveLevelUp && (bool)MapMoveSpawner.Instance)
		{
			initMoveLevelUp = true;
			MapMoveSpawner.Instance.MoveLevelUp();
		}
		if (Input.GetMouseButton(0) && initEnd && initMoveLevelUp && !MapMoveSpawner.Instance.isMoveEnd)
		{
			MapMoveSpawner.Instance.quickMove();
		}
		if (initEnd && initMoveLevelUp && !initReady && MapMoveSpawner.Instance.isMoveEnd)
		{
			initReady = true;
			UI.Instance.OpenPanel(UIPanelType.ReadyGoUI);
			initReadyBubble();
			float orthographicSize = Camera.main.GetComponent<Camera>().orthographicSize;
			float num = (float)Screen.width * 1f / (float)Screen.height;
			float num2 = orthographicSize * 2f * num;
			if (num2 < 7.2f)
			{
				float num3 = 6.4f - Camera.main.GetComponent<Camera>().orthographicSize;
				moveObj.transform.DOLocalMove(new Vector3(0f, num3 - 0.4f + 0.75f, 0f), 0.3f).OnComplete(MoveEnd);
			}
			else
			{
				moveObj.transform.DOLocalMove(new Vector3(0f, -0.4f + 0.75f, 0f), 0.3f).OnComplete(MoveEnd);
			}
			if (Singleton<LevelManager>.Instance.bflylevel && RemoveController.Instance.bMoveNextBubble)
			{
				RemoveController.bwhileturestart = true;
				GameUI.action.FlyBg.SetActive(value: true);
				RemoveController.Instance.MoveNextBubble();
			}
			IsWait = true;
		}
	}

	private void OnDisable()
	{
		IsWait = true;
	}

	private void MoveEnd()
	{
		gameMove = false;
		Par_emitter_light.SetActive(value: true);
		AdsManager.ShowBanner();
	}

	public void createBox()
	{
		float num = 0f;
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				BubbleArray[i, j] = null;
				BubbleFlyArray[i, j] = null;
				if (i % 2 == 0)
				{
					num = 0f;
				}
				else
				{
					num = offsetStep;
				}
				GameObject gameObject = UnityEngine.Object.Instantiate(boxPrefab, base.transform.position, base.transform.rotation);
				gameObject.transform.parent = BoxParent.transform;
				gameObject.transform.localPosition = GetPosByRowAndCol(i, j);
				squares[i, j] = gameObject;
				lastRow = i;
				BubbleFlyArray[i, j] = gameObject;
			}
		}
	}

	public void createBubble()
	{
		randomindex = UnityEngine.Random.Range(0, 5);
		for (int i = 0; i < Singleton<LevelManager>.Instance.gemSpawnChance.Count; i++)
		{
			LevelManager.LevelObject levelObject = Singleton<LevelManager>.Instance.gemSpawnChance[i];
			string key = levelObject.key;
			int num = int.Parse(Singleton<DataManager>.Instance.dBubble[key]["type"]);
			if (num <= 5)
			{
				string key2 = key;
				int num2 = num + randomindex;
				if (num2 > 5)
				{
					num2 -= 5;
				}
				switch (num2)
				{
				case 1:
					key2 = "A";
					break;
				case 2:
					key2 = "B";
					break;
				case 3:
					key2 = "C";
					break;
				case 4:
					key2 = "D";
					break;
				case 5:
					key2 = "E";
					break;
				}
				LevelManager.LevelObject value = Singleton<LevelManager>.Instance.gemSpawnChance[i];
				value.key = key2;
				Singleton<LevelManager>.Instance.gemSpawnChance[i] = value;
			}
		}
		for (int j = 0; j < Singleton<LevelManager>.Instance.LTbubble.Count; j++)
		{
			BUBBLEDATA data = Singleton<LevelManager>.Instance.LTbubble[j];
			SpawnerInitData(data);
		}
		for (int k = 0; k < Singleton<LevelManager>.Instance.LTsub.Count; k++)
		{
			BUBBLEDATA data2 = Singleton<LevelManager>.Instance.LTsub[k];
			BubbleObj component = BubbleArray[data2.row, data2.col].GetComponent<BubbleObj>();
			component.InitTop(data2);
		}
		for (int l = 0; l < Singleton<LevelManager>.Instance.Ljysub.Count; l++)
		{
			BUBBLEDATA data3 = Singleton<LevelManager>.Instance.Ljysub[l];
			SpawnerInitDataFly(data3);
		}
		for (int m = 0; m < Singleton<LevelManager>.Instance.LTDown.Count; m++)
		{
			BUBBLEDATA data4 = Singleton<LevelManager>.Instance.LTDown[m];
			BubbleObj component2 = BubbleArray[data4.row, data4.col].GetComponent<BubbleObj>();
			component2.InitDown(data4);
		}
	}

	public void SpawnerInitDataFly(BUBBLEDATA data, bool bBoss = false)
	{
		BubbleFlyObj = createBallFly(GetSquare(data.row, data.col).transform.position, readyBubble: false, data.row);
		BubbleObj component = BubbleFlyObj.GetComponent<BubbleObj>();
		data.key = "JY";
		component.InitDataFly(data);
	}

	public void SpawnerInitData(BUBBLEDATA data, bool bBoss = false)
	{
		BubbleArray[data.row, data.col] = createBall(GetSquare(data.row, data.col).transform.position, readyBubble: false, data.row);
		BubbleObj component = BubbleArray[data.row, data.col].GetComponent<BubbleObj>();
		if (data.key == "@")
		{
			int num = UnityEngine.Random.Range(0, Singleton<LevelManager>.Instance.gemSpawnChance.Count);
			if (num == Singleton<LevelManager>.Instance.gemSpawnChance.Count)
			{
				num = Singleton<LevelManager>.Instance.gemSpawnChance.Count - 1;
			}
			LevelManager.LevelObject levelObject = Singleton<LevelManager>.Instance.gemSpawnChance[num];
			data.key = levelObject.key;
		}
		else if (int.Parse(Singleton<DataManager>.Instance.dBubble[data.key]["type"]) <= 5 && int.Parse(Singleton<DataManager>.Instance.dBubble[data.key]["img"]) <= 5)
		{
			int num2 = int.Parse(Singleton<DataManager>.Instance.dBubble[data.key]["type"]) + randomindex;
			if (num2 > 5)
			{
				num2 -= 5;
			}
			switch (num2)
			{
			case 1:
				data.key = "A";
				break;
			case 2:
				data.key = "B";
				break;
			case 3:
				data.key = "C";
				break;
			case 4:
				data.key = "D";
				break;
			case 5:
				data.key = "E";
				break;
			}
		}
		else if (int.Parse(Singleton<DataManager>.Instance.dBubble[data.key]["type"]) <= 5 && int.Parse(Singleton<DataManager>.Instance.dBubble[data.key]["img"]) >= 6 && int.Parse(Singleton<DataManager>.Instance.dBubble[data.key]["img"]) <= 10)
		{
			int num3 = int.Parse(Singleton<DataManager>.Instance.dBubble[data.key]["type"]) + randomindex;
			if (num3 > 5)
			{
				num3 -= 5;
			}
			switch (num3)
			{
			case 1:
				data.key = "A1";
				break;
			case 2:
				data.key = "B1";
				break;
			case 3:
				data.key = "C1";
				break;
			case 4:
				data.key = "D1";
				break;
			case 5:
				data.key = "E1";
				break;
			}
		}
		else if (int.Parse(Singleton<DataManager>.Instance.dBubble[data.key]["type"]) <= 5 && int.Parse(Singleton<DataManager>.Instance.dBubble[data.key]["img"]) >= 21 && int.Parse(Singleton<DataManager>.Instance.dBubble[data.key]["img"]) <= 25)
		{
			int num4 = int.Parse(Singleton<DataManager>.Instance.dBubble[data.key]["type"]) + randomindex;
			if (num4 > 5)
			{
				num4 -= 5;
			}
			switch (num4)
			{
			case 1:
				data.key = "AJ";
				break;
			case 2:
				data.key = "BJ";
				break;
			case 3:
				data.key = "CJ";
				break;
			case 4:
				data.key = "DJ";
				break;
			case 5:
				data.key = "EJ";
				break;
			}
		}
		else if (int.Parse(Singleton<DataManager>.Instance.dBubble[data.key]["type"]) <= 5 && int.Parse(Singleton<DataManager>.Instance.dBubble[data.key]["img"]) >= 30 && int.Parse(Singleton<DataManager>.Instance.dBubble[data.key]["img"]) <= 34)
		{
			int num5 = int.Parse(Singleton<DataManager>.Instance.dBubble[data.key]["type"]) + randomindex;
			if (num5 > 5)
			{
				num5 -= 5;
			}
			switch (num5)
			{
			case 1:
				data.key = "AA";
				break;
			case 2:
				data.key = "AB";
				break;
			case 3:
				data.key = "AC";
				break;
			case 4:
				data.key = "AD";
				break;
			case 5:
				data.key = "AE";
				break;
			}
		}
		else if (int.Parse(Singleton<DataManager>.Instance.dBubble[data.key]["type"]) <= 5 && int.Parse(Singleton<DataManager>.Instance.dBubble[data.key]["img"]) >= 35 && int.Parse(Singleton<DataManager>.Instance.dBubble[data.key]["img"]) <= 39)
		{
			int num6 = int.Parse(Singleton<DataManager>.Instance.dBubble[data.key]["type"]) + randomindex;
			if (num6 > 5)
			{
				num6 -= 5;
			}
			switch (num6)
			{
			case 1:
				data.key = "JA";
				break;
			case 2:
				data.key = "JB";
				break;
			case 3:
				data.key = "JC";
				break;
			case 4:
				data.key = "JD";
				break;
			case 5:
				data.key = "JE";
				break;
			}
		}
		component.InitData(data, isReadyBubble: false, Singleton<LevelManager>.Instance.bBossHuang);
	}

	public void GetBubbleRandomKeyStep1()
	{
		GetBubbleShow();
	}

	public string GetBubbleRandomKeyStep2(string _key = "")
	{
		int num = 0;
		for (int i = 0; i < gameBubbleShow.Count; i++)
		{
			BubbleShow bubbleShow = gameBubbleShow[i];
			num += bubbleShow.P;
		}
		string text = string.Empty;
		int num2 = UnityEngine.Random.Range(0, num);
		num = -1;
		for (int j = 0; j < gameBubbleShow.Count; j++)
		{
			BubbleShow bubbleShow2 = gameBubbleShow[j];
			num += bubbleShow2.P;
			if (num >= num2)
			{
				text = bubbleShow2.key;
				break;
			}
		}
		if (text == string.Empty)
		{
			LevelManager.LevelObject levelObject = Singleton<LevelManager>.Instance.gemSpawnChance[0];
			text = levelObject.key;
		}
		if (text + "1" == _key && gameBubbleShow.Count > 1)
		{
			List<BubbleShow> list = new List<BubbleShow>();
			for (int k = 0; k < gameBubbleShow.Count; k++)
			{
				BubbleShow bubbleShow3 = gameBubbleShow[k];
				if (bubbleShow3.key != text)
				{
					list.Add(gameBubbleShow[k]);
				}
			}
			num = 0;
			for (int l = 0; l < list.Count; l++)
			{
				BubbleShow bubbleShow4 = list[l];
				num += bubbleShow4.P;
			}
			text = string.Empty;
			num2 = UnityEngine.Random.Range(0, num);
			num = -1;
			for (int m = 0; m < list.Count; m++)
			{
				BubbleShow bubbleShow5 = list[m];
				num += bubbleShow5.P;
				if (num >= num2)
				{
					text = bubbleShow5.key;
					break;
				}
			}
			if (text == string.Empty)
			{
				LevelManager.LevelObject levelObject2 = Singleton<LevelManager>.Instance.gemSpawnChance[0];
				text = levelObject2.key;
			}
		}
		return text + "1";
	}

	public string GetBubbleRandomKey1()
	{
		GetBubbleShow1();
		GetBubbleShow();
		if (gameBubbleShow1.Count == 0)
		{
			int index = UnityEngine.Random.Range(0, Singleton<LevelManager>.Instance.gemSpawnChance.Count - 1);
			LevelManager.LevelObject levelObject = Singleton<LevelManager>.Instance.gemSpawnChance[index];
			return levelObject.key;
		}
		int index2 = UnityEngine.Random.Range(0, gameBubbleShow1.Count - 1);
		string text = gameBubbleShow1[index2];
		if (text == string.Empty)
		{
			LevelManager.LevelObject levelObject2 = Singleton<LevelManager>.Instance.gemSpawnChance[0];
			text = levelObject2.key;
		}
		if (text == readykey)
		{
			readyindex++;
		}
		else
		{
			readyindex = 1;
		}
		int num = 0;
		int num2 = 0;
		if (readyindex >= 2 && gameBubbleShow.Count > 1)
		{
			List<BubbleShow> list = new List<BubbleShow>();
			for (int i = 0; i < gameBubbleShow.Count; i++)
			{
				BubbleShow bubbleShow = gameBubbleShow[i];
				if (bubbleShow.key != readykey)
				{
					list.Add(gameBubbleShow[i]);
				}
			}
			num = 0;
			for (int j = 0; j < list.Count; j++)
			{
				BubbleShow bubbleShow2 = list[j];
				num += bubbleShow2.P;
			}
			text = string.Empty;
			num2 = UnityEngine.Random.Range(0, num);
			if (num2 == num)
			{
				num2--;
			}
			for (int k = 0; k < list.Count; k++)
			{
				BubbleShow bubbleShow3 = list[k];
				num += bubbleShow3.P;
				if (num >= num2)
				{
					text = bubbleShow3.key;
					break;
				}
			}
			if (text == string.Empty)
			{
				BubbleShow bubbleShow4 = list[0];
				text = bubbleShow4.key;
			}
			if (list.Count == 1)
			{
				BubbleShow bubbleShow5 = list[0];
				text = bubbleShow5.key;
			}
		}
		readykey = text;
		return text;
	}

	public string GetBubbleRandomKey()
	{
		return GetBubbleRandomKey1();
	}

	public void GetBubbleShow1()
	{
		if (gameBubbleShow1 == null)
		{
			gameBubbleShow1 = new List<string>();
		}
		gameBubbleShow1.Clear();
		for (int num = rows - 1; num >= 0; num--)
		{
			for (int i = 0; i < cols - num % 2; i++)
			{
				if (num < MapMoveSpawner.Instance.lowrow - 11)
				{
					return;
				}
				GameObject gameObject = BubbleArray[num, i];
				if (((bool)gameObject && (bool)gameObject.GetComponent<BubbleObj>() && gameObject.GetComponent<BubbleObj>().isFall) || !gameObject)
				{
					continue;
				}
				BubbleObj component = gameObject.GetComponent<BubbleObj>();
				int num2 = 0;
				while (num2 < Singleton<LevelManager>.Instance.gemSpawnChance.Count)
				{
					LevelManager.LevelObject levelObject = Singleton<LevelManager>.Instance.gemSpawnChance[num2];
					if (!(levelObject.key == component.mBubbleData.key))
					{
						LevelManager.LevelObject levelObject2 = Singleton<LevelManager>.Instance.gemSpawnChance[num2];
						if (!(levelObject2.key + "J" == component.mBubbleData.key))
						{
							LevelManager.LevelObject levelObject3 = Singleton<LevelManager>.Instance.gemSpawnChance[num2];
							if (!(levelObject3.key + "1" == component.mBubbleData.key))
							{
								num2++;
								continue;
							}
						}
					}
					List<string> list = gameBubbleShow1;
					LevelManager.LevelObject levelObject4 = Singleton<LevelManager>.Instance.gemSpawnChance[num2];
					list.Add(levelObject4.key);
					break;
				}
			}
		}
	}

	public void GetBubbleShow()
	{
		gameBubbleShow.Clear();
		List<BubbleShow> list = new List<BubbleShow>();
		for (int i = 0; i < Singleton<LevelManager>.Instance.gemSpawnChance.Count; i++)
		{
			BubbleShow item = default(BubbleShow);
			LevelManager.LevelObject levelObject = Singleton<LevelManager>.Instance.gemSpawnChance[i];
			item.key = levelObject.key;
			item.show = false;
			LevelManager.LevelObject levelObject2 = Singleton<LevelManager>.Instance.gemSpawnChance[i];
			item.P = levelObject2.value;
			list.Add(item);
		}
		for (int j = 0; j < list.Count; j++)
		{
			BubbleShow item2 = list[j];
			for (int k = 0; k < rows; k++)
			{
				for (int l = 0; l < cols - k % 2; l++)
				{
					if (Singleton<DataManager>.Instance.dBubble[item2.key] == null)
					{
						continue;
					}
					GameObject gameObject = BubbleArray[k, l];
					if ((bool)gameObject && (bool)gameObject.GetComponent<BubbleObj>() && gameObject.GetComponent<BubbleObj>().isFall)
					{
						continue;
					}
					int num = int.Parse(Singleton<DataManager>.Instance.dBubble[item2.key]["type"]);
					if ((bool)gameObject)
					{
						BubbleObj component = gameObject.GetComponent<BubbleObj>();
						if (num == gameObject.GetComponent<BubbleObj>().mType && !gameBubbleShow.Contains(item2))
						{
							item2.show = true;
							gameBubbleShow.Add(item2);
						}
					}
				}
			}
		}
	}

	public void SetBubbleCheck()
	{
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols - i % 2; j++)
			{
				GameObject gameObject = BubbleArray[i, j];
				if ((bool)gameObject)
				{
					gameObject.GetComponent<BubbleObj>().isCheck = false;
				}
			}
		}
	}

	public void ChangeRandom(bool opendoor = true)
	{
		Instance.GetBubbleRandomKeyStep1();
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols - i % 2; j++)
			{
				GameObject gameObject = BubbleArray[i, j];
				if (!gameObject)
				{
					continue;
				}
				BubbleObj component = gameObject.GetComponent<BubbleObj>();
				if (!(component.mBubbleData.key == string.Empty))
				{
					int num = int.Parse(Singleton<DataManager>.Instance.dBubble[component.mBubbleData.key]["attributes"]);
					if (num == 1 && !component.isCheck)
					{
						component.ChangeToRandom();
					}
					if (opendoor && (bool)component.SubMenObj)
					{
						component.SubDoor();
					}
					component.tianchongRandom();
				}
			}
		}
		GetBubbleShow();
		if ((bool)ready_1)
		{
			bool flag = false;
			for (int k = 0; k < gameBubbleShow.Count; k++)
			{
				BubbleShow bubbleShow = gameBubbleShow[k];
				if (bubbleShow.key == ready_1.GetComponent<BubbleObj>().mBubbleData.key)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				ready_1.GetComponent<BubbleObj>().ReadyBubbleChange();
			}
		}
		if ((bool)ready_2)
		{
			bool flag2 = false;
			for (int l = 0; l < gameBubbleShow.Count; l++)
			{
				BubbleShow bubbleShow2 = gameBubbleShow[l];
				if (bubbleShow2.key == ready_2.GetComponent<BubbleObj>().mBubbleData.key)
				{
					flag2 = true;
				}
			}
			if (!flag2)
			{
				ready_2.GetComponent<BubbleObj>().ReadyBubbleChange();
			}
		}
		if (!ready_3)
		{
			return;
		}
		bool flag3 = false;
		for (int m = 0; m < gameBubbleShow.Count; m++)
		{
			BubbleShow bubbleShow3 = gameBubbleShow[m];
			if (bubbleShow3.key == ready_3.GetComponent<BubbleObj>().mBubbleData.key)
			{
				flag3 = true;
			}
		}
		if (!flag3)
		{
			ready_3.GetComponent<BubbleObj>().ReadyBubbleChange();
		}
	}

	public void DesyReady(GameObject obj)
	{
		StartCoroutine(IEDesyReady(obj));
	}

	public IEnumerator IEDesyReady(GameObject obj)
	{
		int iCount = 0;
		yield return new WaitForSeconds(0.2f);
		while (!obj && iCount <= 3)
		{
			iCount++;
			yield return new WaitForSeconds(0.2f);
		}
		UnityEngine.Debug.Log("------------------ready3 33333333333333333");
		UnityEngine.Object.Destroy(obj);
		obj = null;
	}

	public void initReadyBubble(bool isusekey = true, bool bcut = false)
	{
		if (bcut)
		{
			if (iReadyNum == 2 && Singleton<LevelManager>.Instance.iBubbleCount < 2)
			{
				if (Singleton<LevelManager>.Instance.iBubbleCount == 1)
				{
					UnityEngine.Object.Destroy(ready_2);
					ready_2 = null;
				}
				else
				{
					UnityEngine.Object.Destroy(ready_1);
					UnityEngine.Object.Destroy(ready_2);
					ready_1 = null;
					ready_2 = null;
				}
			}
			if (iReadyNum == 3 && Singleton<LevelManager>.Instance.iBubbleCount < 3)
			{
				if (Singleton<LevelManager>.Instance.iBubbleCount == 2)
				{
					UnityEngine.Object.Destroy(ready_3);
					UnityEngine.Debug.Log("------------------ready3 111111111111111111111");
					ready_3 = null;
				}
				else if (Singleton<LevelManager>.Instance.iBubbleCount == 1)
				{
					UnityEngine.Debug.Log("------------------ready3 1111111111111111111112");
					UnityEngine.Object.Destroy(ready_3);
					UnityEngine.Object.Destroy(ready_2);
					ready_2 = null;
					ready_3 = null;
				}
				else
				{
					UnityEngine.Debug.Log("------------------ready3 1111111111111111111113");
					UnityEngine.Object.Destroy(ready_3);
					UnityEngine.Object.Destroy(ready_2);
					UnityEngine.Object.Destroy(ready_1);
					ready_1 = null;
					ready_2 = null;
					ready_3 = null;
				}
			}
			return;
		}
		if ((bool)GameUI.action)
		{
			GameUI.action.ShowBubbleCountText();
		}
		isReadyMove = false;
		string text = string.Empty;
		string text2 = string.Empty;
		if (Singleton<LevelManager>.Instance.R1 != 0)
		{
			text = GetKey(Singleton<LevelManager>.Instance.R1);
		}
		if (Singleton<LevelManager>.Instance.R2 != 0)
		{
			text2 = GetKey(Singleton<LevelManager>.Instance.R2);
		}
		Singleton<LevelManager>.Instance.R1 = 0;
		Singleton<LevelManager>.Instance.R2 = 0;
		if (iReadyNum == 2)
		{
			if (ready_1 == null)
			{
				ready_1 = createReadyBall(GameObject.Find("ready1").transform.position);
				ready_1.transform.DOScale(1f, 0.01f);
				BUBBLEDATA data = default(BUBBLEDATA);
				if (text != string.Empty)
				{
					data.key = text;
				}
				else
				{
					data.key = Instance.GetBubbleRandomKey();
				}
				data.row = -1;
				data.col = -1;
				data.s = 1;
				data.i = 0;
				ready_1.GetComponent<BubbleObj>().InitData(data, isReadyBubble: true);
				ready_1.GetComponent<ReadyBubble>().enabled = true;
				ready_1.GetComponent<ReadyBubble>().Ready();
			}
			if (ready_2 == null)
			{
				ready_2 = createReadyBall(GameObject.Find("ready2").transform.position);
				BUBBLEDATA data2 = default(BUBBLEDATA);
				if (text2 != string.Empty)
				{
					data2.key = text2;
				}
				else
				{
					data2.key = Instance.GetBubbleRandomKey();
				}
				data2.row = -1;
				data2.col = -1;
				data2.s = 1;
				data2.i = 0;
				ready_2.GetComponent<BubbleObj>().InitData(data2, isReadyBubble: true);
				ready_2.transform.DOScale(0.8f, 0f);
			}
			return;
		}
		if (ready_1 == null)
		{
			if (Singleton<LevelManager>.Instance.iBubbleCount < 1)
			{
				return;
			}
			ready_1 = createReadyBall(GameObject.Find("ready1").transform.position);
			ready_1.transform.DOScale(1f, 0.01f);
			BUBBLEDATA data3 = default(BUBBLEDATA);
			if (text != string.Empty)
			{
				data3.key = text;
			}
			else
			{
				data3.key = Instance.GetBubbleRandomKey();
			}
			data3.row = -1;
			data3.col = -1;
			data3.s = 1;
			data3.i = 0;
			ready_1.GetComponent<BubbleObj>().InitData(data3, isReadyBubble: true);
			ready_1.GetComponent<ReadyBubble>().enabled = true;
			ready_1.GetComponent<ReadyBubble>().Ready();
		}
		if (ready_2 == null)
		{
			if (Singleton<LevelManager>.Instance.iBubbleCount < 2)
			{
				return;
			}
			ready_2 = createReadyBall(GameObject.Find("ready2").transform.position);
			BUBBLEDATA data4 = default(BUBBLEDATA);
			if (text2 != string.Empty)
			{
				data4.key = text2;
			}
			else
			{
				data4.key = Instance.GetBubbleRandomKey();
			}
			data4.row = -1;
			data4.col = -1;
			data4.s = 1;
			data4.i = 0;
			ready_2.GetComponent<BubbleObj>().InitData(data4, isReadyBubble: true);
			ready_2.transform.DOScale(0.8f, 0f);
		}
		if (ready_3 == null && Singleton<LevelManager>.Instance.iBubbleCount >= 3)
		{
			ready_3 = createReadyBall(GameObject.Find("ready3").transform.position);
			BUBBLEDATA data5 = default(BUBBLEDATA);
			data5.key = Instance.GetBubbleRandomKey();
			data5.row = -1;
			data5.col = -1;
			data5.s = 1;
			data5.i = 0;
			ready_3.GetComponent<BubbleObj>().InitData(data5, isReadyBubble: true);
			ready_3.transform.DOScale(0.6f, 0f);
		}
	}

	public void createReadyBubbleBing(GameObject obj)
	{
		BUBBLEDATA mBubbleData = obj.GetComponent<BubbleObj>().mBubbleData;
		ready_1 = createReadyBall(GameObject.Find("ready1").transform.position);
		ready_1.GetComponent<BubbleObj>().InitData(mBubbleData, isReadyBubble: true);
		ready_1.GetComponent<ReadyBubble>().enabled = true;
		ready_1.GetComponent<BubbleObj>().skillbing = obj.GetComponent<BubbleObj>().skillbing;
		ready_1.GetComponent<BubbleObj>().skillmu = obj.GetComponent<BubbleObj>().skillmu;
		ready_1.GetComponent<BubbleObj>().skillhuo = obj.GetComponent<BubbleObj>().skillhuo;
		ready_1.GetComponent<BubbleObj>().skilldian = obj.GetComponent<BubbleObj>().skilldian;
		ready_1.GetComponent<BubbleObj>().createfx();
	}

	public string GetKey(int index)
	{
		string result = "A";
		int num = index + randomindex;
		if (num > 5)
		{
			num -= 5;
		}
		switch (num)
		{
		case 1:
			result = "A";
			break;
		case 2:
			result = "B";
			break;
		case 3:
			result = "C";
			break;
		case 4:
			result = "D";
			break;
		case 5:
			result = "E";
			break;
		}
		return result;
	}

	public void createReadyBubble()
	{
		isReadyMove = true;
		if (iReadyNum == 2)
		{
			if ((bool)ready_2)
			{
				Vector3 endValue = GameObject.Find("ready1").transform.localPosition + new Vector3(0f, 0f, -20f);
				ready_1 = ready_2;
				ready_1.GetComponent<ReadyBubble>().enabled = true;
				ready_1.transform.DOScale(1f, 0f);
				ready_2 = null;
				ready_1.transform.DOLocalMove(endValue, movetime).OnComplete(delegate
				{
					Ready2BubbleRotateEnd();
				});
			}
			return;
		}
		if ((bool)ready_2)
		{
			Vector3 endValue2 = GameObject.Find("ready1").transform.localPosition + new Vector3(0f, 0f, -20f);
			ready_1 = ready_2;
			ready_1.GetComponent<ReadyBubble>().enabled = true;
			ready_1.transform.DOScale(1f, 0f);
			ready_2 = null;
			ready_1.transform.DOLocalMove(endValue2, movetime).OnComplete(delegate
			{
				Ready3BubbleRotateEnd();
			});
		}
		if ((bool)ready_3)
		{
			Vector3 endValue3 = GameObject.Find("ready2").transform.localPosition + new Vector3(0f, 0f, -20f);
			ready_2 = ready_3;
			ready_2.GetComponent<ReadyBubble>().enabled = false;
			ready_3 = null;
			ready_2.transform.DOLocalMove(endValue3, movetime);
			ready_2.transform.DOScale(0.8f, 0f);
		}
	}

	public void Ready2BubbleRotateEnd()
	{
		isReadyMove = false;
		if (Singleton<LevelManager>.Instance.iBubbleCount >= 2)
		{
			ready_2 = createReadyBall(GameObject.Find("ready2").transform.position);
			BUBBLEDATA data = default(BUBBLEDATA);
			if (Singleton<LevelManager>.Instance.R3 != 0)
			{
				data.key = GetKey(Singleton<LevelManager>.Instance.R3);
				Singleton<LevelManager>.Instance.R3 = 0;
			}
			else if (Singleton<LevelManager>.Instance.R4 != 0)
			{
				data.key = GetKey(Singleton<LevelManager>.Instance.R4);
				Singleton<LevelManager>.Instance.R4 = 0;
			}
			else
			{
				data.key = Instance.GetBubbleRandomKey1();
			}
			data.row = -1;
			data.col = -1;
			data.s = 1;
			data.i = 0;
			ready_2.GetComponent<BubbleObj>().InitData(data, isReadyBubble: true);
			ready_2.transform.DOScale(0.8f, 0f);
		}
		else
		{
			ready_2 = null;
		}
	}

	public void Ready3BubbleRotateEnd()
	{
		isReadyMove = false;
		if (Singleton<LevelManager>.Instance.iBubbleCount >= 3)
		{
			if (!ready_3)
			{
				ready_3 = createReadyBall(GameObject.Find("ready3").transform.position);
				ready_3.transform.DOScale(0.6f, 0f);
				BUBBLEDATA data = default(BUBBLEDATA);
				if (Singleton<LevelManager>.Instance.R3 != 0)
				{
					data.key = GetKey(Singleton<LevelManager>.Instance.R3);
					Singleton<LevelManager>.Instance.R3 = 0;
				}
				else if (Singleton<LevelManager>.Instance.R4 != 0)
				{
					data.key = GetKey(Singleton<LevelManager>.Instance.R4);
					Singleton<LevelManager>.Instance.R4 = 0;
				}
				else
				{
					data.key = Instance.GetBubbleRandomKey1();
				}
				data.row = -1;
				data.col = -1;
				data.s = 1;
				data.i = 0;
				ready_3.GetComponent<BubbleObj>().InitData(data, isReadyBubble: true);
			}
		}
		else
		{
			ready_3 = null;
		}
	}

	public void ChangeBubble()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("b_change");
		}
		PassLevel.action.ChangeBubble();
		GameGuide.Instance.changeBubble();
		if ((bool)ready_1 && (ready_1.GetComponent<BubbleObj>().skillbing || ready_1.GetComponent<BubbleObj>().skilldian || ready_1.GetComponent<BubbleObj>().skillhuo || ready_1.GetComponent<BubbleObj>().skillmu))
		{
			return;
		}
		isReadyMove = true;
		if (iReadyNum == 2 || Singleton<LevelManager>.Instance.iBubbleCount <= 2)
		{
			if ((bool)ready_2)
			{
				Vector3 endValue = GameObject.Find("ready1").transform.localPosition + new Vector3(0f, 0f, -20f);
				Vector3 endValue2 = GameObject.Find("ready2").transform.localPosition + new Vector3(0f, 0f, -20f);
				GameObject gameObject = ready_1;
				ready_1 = ready_2;
				if ((bool)ready_1)
				{
					ready_1.GetComponent<ReadyBubble>().enabled = true;
					ready_1.transform.DOLocalMove(endValue, movetime);
					ready_1.transform.DOScale(1f, 0f);
				}
				ready_2 = gameObject;
				if ((bool)ready_2)
				{
					ready_2.GetComponent<ReadyBubble>().enabled = false;
					ready_2.transform.DOLocalMove(endValue2, movetime).OnComplete(delegate
					{
						ChangeBubbleRotateEnd(ready_2);
					});
					ready_2.transform.DOScale(0.8f, 0f);
				}
			}
			else
			{
				isReadyMove = false;
			}
			return;
		}
		Vector3 endValue3 = GameObject.Find("ready1").transform.localPosition + new Vector3(0f, 0f, -20f);
		Vector3 endValue4 = GameObject.Find("ready2").transform.localPosition + new Vector3(0f, 0f, -20f);
		Vector3 endValue5 = GameObject.Find("ready3").transform.localPosition + new Vector3(0f, 0f, -20f);
		GameObject gameObject2 = ready_1;
		ready_1 = ready_2;
		if ((bool)ready_1)
		{
			ready_1.GetComponent<ReadyBubble>().enabled = true;
			ready_1.transform.DOLocalMove(endValue3, movetime);
			ready_1.transform.DOScale(1f, 0f);
		}
		ready_2 = ready_3;
		if ((bool)ready_2)
		{
			ready_2.GetComponent<ReadyBubble>().enabled = false;
			ready_2.transform.DOLocalMove(endValue4, movetime).OnComplete(delegate
			{
				ChangeBubbleRotateEnd(ready_2);
			});
			ready_2.transform.DOScale(0.8f, 0f);
		}
		ready_3 = gameObject2;
		if ((bool)ready_3)
		{
			ready_3.GetComponent<ReadyBubble>().enabled = false;
			ready_3.transform.DOLocalMove(endValue5, movetime).OnComplete(delegate
			{
				ChangeBubbleRotateEnd(ready_2);
			});
			ready_3.transform.DOScale(0.6f, movetime);
		}
	}

	public void ChangeBubbleRotateEnd(GameObject b)
	{
		isReadyMove = false;
	}

	public void ChangeToSkill(int ID, GameObject obj)
	{
		string key = string.Empty;
		switch (ID)
		{
		case 1:
			key = "M";
			break;
		case 2:
			key = "L";
			break;
		case 4:
			key = "O";
			break;
		case 5:
			key = "N";
			break;
		}
		Singleton<UserManager>.Instance.SetPassTask("Gang" + ID);
		Singleton<UserManager>.Instance.SetPassTask1("UseGang");
		//Analytics.Event("UseGang" + ID);
		if ((bool)ready_1)
		{
			ready_1.GetComponent<BubbleObj>().ChangeTo(key);
			GameObject gameObject = UnityEngine.Object.Instantiate(obj, base.transform.position, base.transform.rotation);
			gameObject.transform.parent = ready_1.transform.parent;
			gameObject.transform.position = ready_1.transform.position + new Vector3(0f, 0f, -1f);
		}
		if (ID != 3)
		{
			Singleton<LevelManager>.Instance.LogGangUSE(ID);
		}
	}

	public GameObject createBall(Vector3 vec, bool readyBubble = false, int row = 1)
	{
		GameObject gameObject = null;
		gameObject = UnityEngine.Object.Instantiate(ballPrefab, base.transform.position, base.transform.rotation);
		gameObject.transform.position = new Vector3(vec.x, vec.y, -10f);
		gameObject.transform.parent = BallParent.transform;
		GameObject[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		gameObject.name += array.Length.ToString();
		return gameObject.gameObject;
	}

	public GameObject createBallFly(Vector3 vec, bool readyBubble = false, int row = 1)
	{
		GameObject gameObject = null;
		gameObject = UnityEngine.Object.Instantiate(ballPrefab, base.transform.position, base.transform.rotation);
		gameObject.transform.position = new Vector3(vec.x, vec.y, -10f);
		gameObject.transform.parent = TopBallFlyParent.transform;
		GameObject[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		gameObject.name += array.Length.ToString();
		return gameObject.gameObject;
	}

	public GameObject createReadyBall(Vector3 vec)
	{
		GameObject gameObject = null;
		gameObject = UnityEngine.Object.Instantiate(ballPrefab, base.transform.position, base.transform.rotation);
		gameObject.transform.position = new Vector3(vec.x, vec.y, -20f);
		gameObject.transform.parent = BallParent.transform;
		GameObject[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		gameObject.name += array.Length.ToString();
		gameObject.transform.parent = ReadyBubbleParent.transform;
		return gameObject.gameObject;
	}

	public GameObject GetSquare(int row, int col)
	{
		return squares[row, col];
	}

	public Vector2 GetPosByRowAndCol(int row, int col)
	{
		float num = 32f;
		float num2 = (float)(col * 2) * num + num + (float)(row % 2) * num - 352f;
		float num3 = (float)(-row * 2) * num * Mathf.Sin(1.04666674f) + 640f - num;
		return new Vector2(num2 / 100f, num3 / 100f);
	}

	public IEnumerator checkMove()
	{
		while (isCheckMove)
		{
			int count = 0;
			yield return new WaitForSeconds(0.1f);
			IEnumerator enumerator = Instance.RemoveParent.transform.GetEnumerator();
			try
			{
				if (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.Current;
					count++;
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
			IEnumerator enumerator2 = PassLevel.action.AddAndCutObj.transform.GetEnumerator();
			try
			{
				if (enumerator2.MoveNext())
				{
					Transform transform2 = (Transform)enumerator2.Current;
					count++;
				}
			}
			finally
			{
				IDisposable disposable;
				IDisposable disposable3 = disposable = (enumerator2 as IDisposable);
				if (disposable != null)
				{
					disposable3.Dispose();
				}
			}
			yield return new WaitForSeconds(0.08f);
			if (count == 0)
			{
				isCheckMove = false;
				if (Singleton<LevelManager>.Instance.iBubbleCount <= 0)
				{
					StartCoroutine(GameUI.action.OpenBuyBubbleUI());
				}
			}
		}
	}

	public IEnumerator HitAnim(GameObject obj, BUBBLEDATA data, Vector3 endpos, string key = "")
	{
		RemoveController.Instance.CheckRemove(obj, key);
		if (!obj || !obj.GetComponent<BubbleObj>() || obj.GetComponent<BubbleObj>().isFall || !(key == string.Empty))
		{
			yield break;
		}
		Vector3 position = GetSquare(data.row, data.col).transform.position;
		float x = position.x;
		Vector3 position2 = GetSquare(data.row, data.col).transform.position;
		Vector3 endpos3 = new Vector3(x, position2.y, -11f);
		Vector3 position3 = GetSquare(data.row, data.col).transform.position;
		float x2 = position3.x;
		Vector3 position4 = GetSquare(data.row, data.col).transform.position;
		Vector3 endpos2 = new Vector3(x2, position4.y, -10f);
		Sequence ts = DOTween.Sequence();
		ts.Append(obj.transform.DOMove(endpos, 0.05f)).Append(obj.transform.DOMove(endpos3, 0.15f)).Append(obj.transform.DOMove(endpos2, 0.01f));
		List<Vector2> vecRowCol3 = GetAround(data.row, data.col);
		for (int i = 0; i < vecRowCol3.Count; i++)
		{
			GameObject[,] bubbleArray = BubbleArray;
			Vector2 vector = vecRowCol3[i];
			int num = (int)vector.x;
			Vector2 vector2 = vecRowCol3[i];
			GameObject gameObject = bubbleArray[num, (int)vector2.y];
			if ((bool)gameObject)
			{
				PlayHitAnim(obj, gameObject, 1.6f, 0.1f, 0.35f);
			}
		}
		List<Vector2> vecRowCol2 = new List<Vector2>();
		GetAnimList(vecRowCol2, vecRowCol3, data.row, data.col);
		for (int j = 0; j < vecRowCol2.Count; j++)
		{
			GameObject[,] bubbleArray2 = BubbleArray;
			Vector2 vector3 = vecRowCol2[j];
			int num2 = (int)vector3.x;
			Vector2 vector4 = vecRowCol2[j];
			GameObject gameObject2 = bubbleArray2[num2, (int)vector4.y];
			if ((bool)gameObject2)
			{
				PlayHitAnim(obj, gameObject2, 0.8f, 0.12f, 0.3f);
			}
		}
		yield return new WaitForSeconds(0.2f);
	}

	public void PlayHitAnim(GameObject obj1, GameObject obj2, float elasticity, float time1, float time2)
	{
		Vector3 vector = obj1.transform.position - obj2.transform.position;
		double num = Math.Atan2(vector.y, vector.x);
		BUBBLEDATA mBubbleData = obj2.GetComponent<BubbleObj>().mBubbleData;
		Vector3 position = GetSquare(mBubbleData.row, mBubbleData.col).transform.position;
		float x = position.x;
		Vector3 position2 = GetSquare(mBubbleData.row, mBubbleData.col).transform.position;
		float y = position2.y;
		float num2 = (float)Math.Cos(num) * 0.15f * elasticity;
		float num3 = (float)Math.Sin(num) * 0.15f * elasticity;
		float num4 = (float)Math.Cos(num) * 0.15f * elasticity;
		float num5 = (float)Math.Sin(num) * 0.15f * elasticity;
		float num6 = (float)Math.Cos(num) * 0.15f * elasticity;
		float num7 = (float)Math.Sin(num) * 0.15f * elasticity;
		if ((bool)obj2 && (bool)obj2.GetComponent<BubbleObj>() && !obj2.GetComponent<BubbleObj>().isFall && !obj2.GetComponent<BubbleObj>().isRemove)
		{
			Sequence sequence = DOTween.Sequence();
			Sequence s = sequence;
			Transform transform = obj2.transform;
			float x2 = x - num2;
			float y2 = y - num3;
			Vector3 position3 = ballPrefab.transform.position;
			Sequence s2 = s.Append(transform.DOMove(new Vector3(x2, y2, position3.z), time1));
			Transform transform2 = obj2.transform;
			float x3 = x;
			float y3 = y;
			Vector3 position4 = ballPrefab.transform.position;
			s2.Append(transform2.DOMove(new Vector3(x3, y3, position4.z), time2));
		}
	}

	public void GetAnimAllList(List<Vector2> vecPos, List<Vector2> _list, int row, int col)
	{
		for (int i = 0; i < _list.Count; i++)
		{
			Vector2 vector = _list[i];
			int nRow = (int)vector.x;
			Vector2 vector2 = _list[i];
			List<Vector2> around = GetAround(nRow, (int)vector2.y);
			for (int j = 0; j < around.Count; j++)
			{
				Vector2 vector3 = around[j];
				if ((int)vector3.x == row)
				{
					Vector2 vector4 = around[j];
					if ((int)vector4.y == col)
					{
						continue;
					}
				}
				Vector2 vector5 = around[j];
				float x = (int)vector5.x;
				Vector2 vector6 = around[j];
				if (!_list.Contains(new Vector2(x, (int)vector6.y)))
				{
					Vector2 vector7 = around[j];
					float x2 = (int)vector7.x;
					Vector2 vector8 = around[j];
					if (!vecPos.Contains(new Vector2(x2, (int)vector8.y)))
					{
						Vector2 vector9 = around[j];
						float x3 = (int)vector9.x;
						Vector2 vector10 = around[j];
						vecPos.Add(new Vector2(x3, (int)vector10.y));
					}
				}
			}
		}
	}

	public void GetAnimList(List<Vector2> vecPos, List<Vector2> _list, int row, int col)
	{
		for (int i = 0; i < _list.Count; i++)
		{
			GameObject[,] bubbleArray = BubbleArray;
			Vector2 vector = _list[i];
			int num = (int)vector.x;
			Vector2 vector2 = _list[i];
			GameObject exists = bubbleArray[num, (int)vector2.y];
			if (!exists)
			{
				continue;
			}
			Vector2 vector3 = _list[i];
			int nRow = (int)vector3.x;
			Vector2 vector4 = _list[i];
			List<Vector2> around = GetAround(nRow, (int)vector4.y);
			for (int j = 0; j < around.Count; j++)
			{
				GameObject[,] bubbleArray2 = BubbleArray;
				Vector2 vector5 = around[j];
				int num2 = (int)vector5.x;
				Vector2 vector6 = around[j];
				GameObject exists2 = bubbleArray2[num2, (int)vector6.y];
				if (!exists2)
				{
					continue;
				}
				Vector2 vector7 = around[j];
				if ((int)vector7.x == row)
				{
					Vector2 vector8 = around[j];
					if ((int)vector8.y == col)
					{
						continue;
					}
				}
				Vector2 vector9 = around[j];
				float x = (int)vector9.x;
				Vector2 vector10 = around[j];
				if (!_list.Contains(new Vector2(x, (int)vector10.y)))
				{
					Vector2 vector11 = around[j];
					float x2 = (int)vector11.x;
					Vector2 vector12 = around[j];
					if (!vecPos.Contains(new Vector2(x2, (int)vector12.y)))
					{
						Vector2 vector13 = around[j];
						float x3 = (int)vector13.x;
						Vector2 vector14 = around[j];
						vecPos.Add(new Vector2(x3, (int)vector14.y));
					}
				}
			}
		}
	}

	public List<Vector2> GetAround(int nRow, int nCol)
	{
		List<Vector2> list = new List<Vector2>();
		if (!IsValidPos(nRow, nCol))
		{
			return list;
		}
		if (IsValidPos(nRow, nCol - 1))
		{
			list.Add(new Vector2(nRow, nCol - 1));
		}
		if (IsValidPos(nRow, nCol + 1))
		{
			list.Add(new Vector2(nRow, nCol + 1));
		}
		if (IsValidPos(nRow - 1, nCol))
		{
			list.Add(new Vector2(nRow - 1, nCol));
		}
		if (IsValidPos(nRow + 1, nCol))
		{
			list.Add(new Vector2(nRow + 1, nCol));
		}
		int num = (nRow % 2 != 0) ? (nCol + 1) : (nCol - 1);
		if (IsValidPos(nRow - 1, num))
		{
			list.Add(new Vector2(nRow - 1, num));
		}
		if (IsValidPos(nRow + 1, num))
		{
			list.Add(new Vector2(nRow + 1, num));
		}
		return list;
	}

	public void WinFallBubble()
	{
		InitGame.Action.StartCoroutine(IEWinFallBubble());
	}

	public IEnumerator IEWinFallBubble()
	{
		yield return new WaitForSeconds(0.1f);
		if ((bool)SoundFireController.action)
		{
			SoundFireController.action.stop();
		}
		IEnumerator enumerator = Instance.BallParent.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				if ((bool)transform.GetComponent<BubbleObj>() && !transform.GetComponent<BubbleObj>().isRemove)
				{
					transform.GetComponent<BubbleObj>().bwin100fen = true;
					transform.GetComponent<BubbleObj>().isRemoveByDBHUO = true;
					transform.GetComponent<BubbleObj>().RemoveBubble();
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
		IEnumerator enumerator2 = Instance.BallParent.transform.GetEnumerator();
		try
		{
			while (enumerator2.MoveNext())
			{
				Transform transform2 = (Transform)enumerator2.Current;
				UnityEngine.Object.Destroy(transform2.gameObject);
			}
		}
		finally
		{
			IDisposable disposable;
			IDisposable disposable3 = disposable = (enumerator2 as IDisposable);
			if (disposable != null)
			{
				disposable3.Dispose();
			}
		}
		RemoveController.Instance.isFallBubbleCheck = true;
		RemoveController.Instance.checkFall();
		StartCoroutine(GameOverShootBubble());
	}

	public void useSkill1()
	{
		StartCoroutine(skill1());
	}

	public bool isCanUseSkill()
	{
		if (ready_1 != null)
		{
			if (!ready_1.GetComponent<ReadyBubble>().move)
			{
				return true;
			}
			return false;
		}
		return false;
	}

	private IEnumerator skill1()
	{
		bool isok = false;
		while (!isok)
		{
			yield return new WaitForSeconds(0.1f);
			if (initReady)
			{
				isok = true;
				useyanchangxian = true;
			}
		}
	}

	public void useSkill2()
	{
		StartCoroutine(skill2());
	}

	private IEnumerator skill2()
	{
		bool isok = false;
		while (!isok)
		{
			yield return new WaitForSeconds(0.1f);
			if (initReady)
			{
				isok = true;
				iReadyNum = 3;
				if ((bool)ready_3)
				{
					UnityEngine.Object.Destroy(ready_3.gameObject);
				}
				initReadyBubble();
			}
		}
	}

	public void useSkill3()
	{
		StartCoroutine(skill3());
	}

	private IEnumerator skill3()
	{
		bool isok = false;
		while (!isok)
		{
			yield return new WaitForSeconds(0.1f);
			if (!initReady)
			{
				continue;
			}
			isok = true;
			if ((bool)ready_1)
			{
				if ((bool)SoundController.action)
				{
					SoundController.action.playNow("ui_target_item");
				}
				ready_1.GetComponent<BubbleObj>().ChangeTo("BB");
			}
		}
	}

	private IEnumerator GameOverShootBubble()
	{
		yield return new WaitForSeconds(0.1f);
		iOverShootBubbleCount = Singleton<LevelManager>.Instance.iBubbleCount;
		iiOverShootBubbleCountIndex = -5;
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex > 10000)
		{
			StartCoroutine(PassLevel.action.IECheckWin());
			yield break;
		}
		if ((bool)ready_3)
		{
			Vector3[] array = new Vector3[3];
			array[0] = ready_3.transform.localPosition;
			array[2] = GameObject.Find("ready1").transform.localPosition;
			array[1] = GetCenterPost(array[0], array[2], 5f);
			ready_3.transform.DOLocalPath(array, 0.2f, PathType.CatmullRom, PathMode.TopDown2D, 20).SetEase(Ease.Linear).OnComplete(delegate
			{
				ready_3.GetComponent<BubbleObj>().GameOverShootBubble(iiOverShootBubbleCountIndex++, iOverShootBubbleCount);
				iiOverShootBubbleCountIndex++;
			});
		}
		if ((bool)ready_2)
		{
			Vector3[] array2 = new Vector3[3];
			array2[0] = ready_2.transform.localPosition;
			array2[2] = GameObject.Find("ready1").transform.localPosition;
			array2[1] = GetCenterPost(array2[0], array2[2], 5f);
			ready_2.transform.DOLocalPath(array2, 0.2f, PathType.CatmullRom, PathMode.TopDown2D, 20).SetEase(Ease.Linear).OnComplete(delegate
			{
				ready_2.GetComponent<BubbleObj>().GameOverShootBubble(iiOverShootBubbleCountIndex++, iOverShootBubbleCount);
				iiOverShootBubbleCountIndex++;
			});
		}
		if ((bool)ready_1)
		{
			ready_1.GetComponent<BubbleObj>().GameOverShootBubble(iiOverShootBubbleCountIndex++, iOverShootBubbleCount);
			iiOverShootBubbleCountIndex++;
		}
		bool isOK = false;
		SoundController.action.playNow("FireworksShoot");
		bool bReadBubbleCount = true;
		while (!isOK)
		{
			if (bReadBubbleCount)
			{
				bReadBubbleCount = false;
				Singleton<LevelManager>.Instance.iBubbleCountOver = Singleton<LevelManager>.Instance.iBubbleCount;
			}
			if (Singleton<LevelManager>.Instance.iBubbleCount == 0)
			{
				WinPanmove.action.moveready();
				StartCoroutine(PassLevel.action.IECheckWin());
				isOK = true;
			}
			else if (bmoveaniState)
			{
				moveani();
			}
			yield return new WaitForSeconds(0.001f);
		}
	}

	public void moveani()
	{
		bmoveaniState = false;
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("b_shoot");
		}
		Singleton<LevelManager>.Instance.CutBubble();
		GameObject ready = createReadyBall(GameObject.Find("ready2").transform.position);
		BUBBLEDATA data = default(BUBBLEDATA);
		int index = UnityEngine.Random.Range(0, Singleton<LevelManager>.Instance.gemSpawnChance.Count);
		LevelManager.LevelObject levelObject = Singleton<LevelManager>.Instance.gemSpawnChance[index];
		data.key = levelObject.key;
		data.row = -1;
		data.col = -1;
		data.s = 1;
		data.i = 0;
		ready.GetComponent<BubbleObj>().InitData(data, isReadyBubble: true);
		Vector3[] array = new Vector3[3];
		array[0] = ready.transform.localPosition;
		array[2] = GameObject.Find("ready1").transform.localPosition;
		array[1] = GetCenterPost(array[0], array[2], 5f);
		Sequence s = DOTween.Sequence();
		s.Append(ready.transform.DOScale(new Vector2(0.7f, 0.7f), 0.01f).SetEase(Ease.InOutSine)).Append(ready.transform.DOScale(new Vector2(1f, 1f), 0.03f).SetEase(Ease.OutSine).SetDelay(0.01f));
		ready.transform.DOLocalPath(array, 0.05f, PathType.CatmullRom, PathMode.TopDown2D, 20).SetEase(Ease.Linear).OnComplete(delegate
		{
			bmoveaniState = true;
			if (iiOverShootBubbleCountIndex >= 5)
			{
				biiOverShootBubbleCountIndexstate = false;
			}
			if (iiOverShootBubbleCountIndex <= -5)
			{
				biiOverShootBubbleCountIndexstate = true;
			}
			if (biiOverShootBubbleCountIndexstate)
			{
				ready.GetComponent<BubbleObj>().GameOverShootBubble(iiOverShootBubbleCountIndex++, iOverShootBubbleCount);
				iiOverShootBubbleCountIndex++;
			}
			else
			{
				ready.GetComponent<BubbleObj>().GameOverShootBubble(iiOverShootBubbleCountIndex--, iOverShootBubbleCount);
				iiOverShootBubbleCountIndex--;
			}
		});
	}

	public Vector3 GetCenterPost(Vector3 Start, Vector3 End, float r = 0f, bool _bOrientation = false)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = (Start.x + End.x) / 2f;
		float num4 = (Start.y + End.y) / 2f;
		num = num3 - Start.x;
		num2 = num4 - Start.y;
		float num5 = 0f;
		num5 = num;
		if (_bOrientation)
		{
			num = num2 / r;
			num2 = (0f - num5) / r;
		}
		else
		{
			num = (0f - num2) / r;
			num2 = num5 / r;
		}
		num = num3 - num;
		num2 = num4 - num2;
		return new Vector3(num, num2, Start.z);
	}

	public bool isCanUseGanSkill(int index)
	{
		if ((bool)ready_1)
		{
			if (index == 1 && ready_1.GetComponent<BubbleObj>().skilldian)
			{
				return false;
			}
			if (index == 2 && ready_1.GetComponent<BubbleObj>().skillhuo)
			{
				return false;
			}
			if (index == 4 && ready_1.GetComponent<BubbleObj>().skillmu)
			{
				return false;
			}
			if (index == 5 && ready_1.GetComponent<BubbleObj>().skillbing)
			{
				return false;
			}
			if (ready_1.GetComponent<BubbleObj>().skillzhadan)
			{
				return false;
			}
			if (ready_1.GetComponent<BubbleObj>().skilljiang)
			{
				return false;
			}
			if (ready_1.GetComponent<BubbleObj>().skilljingling)
			{
				return false;
			}
		}
		return true;
	}

	public bool isCanUseDownSkill()
	{
		if ((bool)ready_3 && (ready_3.GetComponent<BubbleObj>().skillbing || ready_3.GetComponent<BubbleObj>().skilldian || ready_3.GetComponent<BubbleObj>().skillhuo || ready_3.GetComponent<BubbleObj>().skillmu || ready_3.GetComponent<BubbleObj>().skillzhadan || ready_3.GetComponent<BubbleObj>().skilljiang || ready_3.GetComponent<BubbleObj>().skilljingling))
		{
			return false;
		}
		if ((bool)ready_2 && (ready_2.GetComponent<BubbleObj>().skillbing || ready_2.GetComponent<BubbleObj>().skilldian || ready_2.GetComponent<BubbleObj>().skillhuo || ready_2.GetComponent<BubbleObj>().skillmu || ready_2.GetComponent<BubbleObj>().skillzhadan || ready_2.GetComponent<BubbleObj>().skilljiang || ready_2.GetComponent<BubbleObj>().skilljingling))
		{
			return false;
		}
		if ((bool)ready_1 && (ready_1.GetComponent<BubbleObj>().skillbing || ready_1.GetComponent<BubbleObj>().skilldian || ready_1.GetComponent<BubbleObj>().skillhuo || ready_1.GetComponent<BubbleObj>().skillmu || ready_1.GetComponent<BubbleObj>().skillzhadan || ready_1.GetComponent<BubbleObj>().skilljiang || ready_1.GetComponent<BubbleObj>().skilljingling))
		{
			return false;
		}
		return true;
	}

	public Vector3 MoveToPos(Vector3 nowPos)
	{
		Vector3 result = new Vector3(0f, 0f, 0f);
		if ((double)nowPos.x > 3.6 - (double)Instance.offsetStep)
		{
			float num = 3.6f - Instance.offsetStep;
			if (nowPos.x < num * 3f)
			{
				float x = num * 2f - nowPos.x;
				result = new Vector3(x, nowPos.y, nowPos.z);
			}
			else if (nowPos.x > num * 3f && nowPos.x < num * 5f)
			{
				float x2 = nowPos.x - num * 4f;
				result = new Vector3(x2, nowPos.y, nowPos.z);
			}
			else if (nowPos.x > num * 5f && nowPos.x < num * 7f)
			{
				float x3 = num * 6f - nowPos.x;
				result = new Vector3(x3, nowPos.y, nowPos.z);
			}
			else if (nowPos.x > num * 7f && nowPos.x < num * 9f)
			{
				float x4 = nowPos.x - num * 8f;
				result = new Vector3(x4, nowPos.y, nowPos.z);
			}
		}
		else
		{
			if (!((double)nowPos.x < -3.6 + (double)Instance.offsetStep))
			{
				return nowPos;
			}
			float num2 = 3.6f - Instance.offsetStep;
			if (nowPos.x > (0f - num2) * 3f)
			{
				float x5 = (0f - num2) * 2f - nowPos.x;
				result = new Vector3(x5, nowPos.y, nowPos.z);
			}
			else if (nowPos.x < (0f - num2) * 3f && nowPos.x > (0f - num2) * 5f)
			{
				float x6 = nowPos.x + num2 * 4f;
				result = new Vector3(x6, nowPos.y, nowPos.z);
			}
			else if (nowPos.x < (0f - num2) * 5f && nowPos.x > (0f - num2) * 7f)
			{
				float x7 = (0f - num2) * 6f - nowPos.x;
				result = new Vector3(x7, nowPos.y, nowPos.z);
			}
			else if (nowPos.x < (0f - num2) * 7f && nowPos.x > (0f - num2) * 9f)
			{
				float x8 = nowPos.x + num2 * 8f;
				result = new Vector3(x8, nowPos.y, nowPos.z);
			}
		}
		return result;
	}

	public bool IsValidPos(int nRow, int nCol)
	{
		if (nRow < 0 || nCol < 0)
		{
			return false;
		}
		if (nRow >= rows || nCol >= cols - nRow % 2)
		{
			return false;
		}
		return true;
	}

	public void useSkill1_in()
	{
		Singleton<DataManager>.Instance.byaping = true;
		StartCoroutine(skill1_in());
	}

	public void useSkill3_in()
	{
		Singleton<DataManager>.Instance.byaping = true;
		StartCoroutine(skill3_in());
	}

	private IEnumerator skill1_in()
	{
		bool isok = false;
		while (!isok)
		{
			yield return new WaitForSeconds(0.1f);
			if (!initReady)
			{
				continue;
			}
			isok = true;
			if ((bool)ready_1)
			{
				if ((bool)SoundController.action)
				{
					SoundController.action.playNow("item_use");
				}
				ready_1.GetComponent<BubbleObj>().ChangeTo("JZ");
			}
		}
	}

	private IEnumerator skill3_in()
	{
		bool isok = false;
		while (!isok)
		{
			yield return new WaitForSeconds(0.1f);
			if (!initReady)
			{
				continue;
			}
			isok = true;
			if ((bool)ready_1)
			{
				if ((bool)SoundController.action)
				{
					SoundController.action.playNow("ui_target_item");
				}
				ready_1.GetComponent<BubbleObj>().ChangeTo("JL");
			}
		}
	}
}
