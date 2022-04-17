using DG.Tweening;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PassLevel : MonoBehaviour
{
	public static PassLevel action;

	public GameObject FlyFatherObj;

	public GameObject AddAndCutObj;

	public GameObject BubbleElfObj;

	public bool bGameStart;

	public GameObject jlxzObj;

	public GameObject hanObj;

	private GameObject _hanObj;

	public GameObject FlyToObj;

	public GameObject mibiaoIconObj;

	public GameObject elf_starObj;

	public GameObject elfin_diaoluo;

	public GameObject SkillElfFly;

	private List<GameObject> elfin_diaoluoObj;

	public bool bOver;

	public TextMeshProUGUI mubiaoCountText;

	public GameObject tianchongflyparent;

	public GameObject tianchongfly1;

	public GameObject tianchongfly2;

	public GameObject tianchongfly3;

	public GameObject tianchongfly4;

	public GameObject levelUP;

	public int iNowMubiaoCount;

	public int iMubiaoSum;

	public int iKillBubbleCount;

	public int iDownGangCount;

	public int iMubiaoType;

	public int iNowMubiaoTmpCount;

	public static bool bWin;

	private List<Vector2> LdiaoluoPos;

	public GameObject fx_BossHuangChongObj;

	private int userlevel;

	private int iElfPlayMp3Count;

	private int iElfPlayMp3PlayTime = 10;

	private int iUpdateWaitCount = 10;

	public int iNowUpdateWait = -15;

	private bool bThisOnePlayAni;

	private int ivo_role_shoot;

	private bool bGirlAni_lively1;

	private int iFaultCombo;

	private int icount;

	private int i_elfin_diaoluoObjCount;

	private int idiaoluoCount;

	private bool bLasfault;

	private bool bPlaying;

	public GameObject fx_prop3_addBubble;

	private List<string> dd_all_list = new List<string>();

	public void Initelfin_diaoluoObj(int iCount)
	{
		elfin_diaoluoObj = new List<GameObject>();
	}

	public void FxBossInit(GameObject obj)
	{
		fx_BossHuangChongObj = new GameObject();
		fx_BossHuangChongObj = obj;
	}

	private void Start()
	{
		action = this;
		bWin = false;
		iMubiaoSum = Singleton<LevelManager>.Instance.iLevelCount;
		iMubiaoType = Singleton<LevelManager>.Instance.iLevelType;
		if (iMubiaoType == 0)
		{
			iMubiaoType = 1;
		}
		InitMubiao();
		StartCoroutine(UpdateWait());
		InitUserlevel();
		InitLdiaoluoPos();
	}

	private void InitLdiaoluoPos()
	{
		LdiaoluoPos = new List<Vector2>();
		LdiaoluoPos.Add(new Vector2(-411f, -849f));
		LdiaoluoPos.Add(new Vector2(-461f, -867f));
		LdiaoluoPos.Add(new Vector2(-379f, -866f));
		LdiaoluoPos.Add(new Vector2(-420f, -907f));
		LdiaoluoPos.Add(new Vector2(-467f, -932f));
		LdiaoluoPos.Add(new Vector2(-377f, -931f));
		LdiaoluoPos.Add(new Vector2(-509f, -829f));
		LdiaoluoPos.Add(new Vector2(-325f, -844f));
		LdiaoluoPos.Add(new Vector2(-511f, -907f));
		LdiaoluoPos.Add(new Vector2(-329f, -906f));
		LdiaoluoPos.Add(new Vector2(-330f, -906f));
		LdiaoluoPos.Add(new Vector2(-331f, -906f));
		LdiaoluoPos.Add(new Vector2(-332f, -906f));
		LdiaoluoPos.Add(new Vector2(-333f, -906f));
		LdiaoluoPos.Add(new Vector2(-334f, -906f));
		LdiaoluoPos.Add(new Vector2(-3235f, -906f));
		LdiaoluoPos.Add(new Vector2(-336f, -906f));
		LdiaoluoPos.Add(new Vector2(-328f, -916f));
		LdiaoluoPos.Add(new Vector2(-328f, -926f));
	}

	public void InitUserlevel()
	{
		userlevel = Singleton<UserLevelManager>.Instance.GetUserLevel();
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex < 31)
		{
			userlevel = 0;
		}
	}

	public void ElfPlayMp3()
	{
		if (!MapMoveSpawner.Instance.isHaveElf || !SoundController.action)
		{
			return;
		}
		if (iElfPlayMp3Count == 0)
		{
			int num = UnityEngine.Random.Range(0, 100);
			if (num > 50)
			{
				SoundController.action.playNow("role_elf_cry");
			}
			else
			{
				SoundController.action.playNow("role_elf_struggle");
			}
		}
		iElfPlayMp3Count++;
		if (iElfPlayMp3Count > 10)
		{
			iElfPlayMp3Count = 0;
		}
	}

	private IEnumerator IEElfPlayMp3()
	{
		yield return new WaitForSeconds(5f);
		bool b = true;
		while (b)
		{
			yield return new WaitForSeconds(1f);
			if (bWin)
			{
				b = false;
			}
			else
			{
				ElfPlayMp3();
			}
		}
	}

	public void SaveKillBubble()
	{
		iKillBubbleCount++;
	}

	public void SaveDownGang()
	{
		iDownGangCount++;
	}

	private IEnumerator UpdateWait()
	{
		yield return new WaitForSeconds(0.1f);
		bool b = true;
		while (b)
		{
			if (bWin)
			{
				b = false;
				continue;
			}
			iNowUpdateWait++;
			if (iNowUpdateWait == iUpdateWaitCount && Singleton<LevelManager>.Instance.iBubbleCount > 5)
			{
				SwitchoverElfAni("to_static", bLoop: false);
			}
			yield return new WaitForSeconds(1f);
		}
	}

	public void BubbleFire()
	{
		if (Singleton<LevelManager>.Instance.iBubbleCount <= 5)
		{
			SwitchoverElfAni("fire", bLoop: false);
		}
		else
		{
			SwitchoverElfAni("fire", bLoop: false);
		}
		iNowUpdateWait = 0;
		bThisOnePlayAni = false;
		bGirlAni_lively1 = false;
	}

	public void vo_role_shoot()
	{
		ivo_role_shoot++;
		if (ivo_role_shoot == 1)
		{
			int num = UnityEngine.Random.Range(1, 100);
			if (num < 80)
			{
				num = UnityEngine.Random.Range(1, 5);
				if ((bool)SoundController.action)
				{
					SoundController.action.playNow("vo_role_shoot" + num);
				}
			}
		}
		else if (ivo_role_shoot == 2)
		{
			int num2 = UnityEngine.Random.Range(1, 100);
			if (num2 < 30)
			{
				num2 = UnityEngine.Random.Range(1, 5);
				if ((bool)SoundController.action)
				{
					SoundController.action.playNow("vo_role_shoot" + num2);
				}
			}
		}
		else
		{
			if (ivo_role_shoot == 3)
			{
				return;
			}
			if (ivo_role_shoot == 4)
			{
				int num3 = UnityEngine.Random.Range(1, 100);
				if (num3 < 50)
				{
					num3 = UnityEngine.Random.Range(1, 5);
					if ((bool)SoundController.action)
					{
						SoundController.action.playNow("vo_role_shoot" + num3);
					}
				}
			}
			else if (ivo_role_shoot == 5)
			{
				int num4 = UnityEngine.Random.Range(1, 100);
				if (num4 < 50)
				{
					num4 = UnityEngine.Random.Range(1, 5);
					if ((bool)SoundController.action)
					{
						SoundController.action.playNow("vo_role_shoot" + num4);
					}
				}
			}
			else if (ivo_role_shoot == 6)
			{
				ivo_role_shoot = 0;
			}
		}
	}

	public void FileResult()
	{
	}

	public void BubbleReady()
	{
		if (Singleton<LevelManager>.Instance.iBubbleCount <= 5)
		{
			SwitchoverElfAni("ready", bLoop: false);
		}
		else
		{
			SwitchoverElfAni("ready", bLoop: false);
		}
		iNowUpdateWait = 0;
	}

	public void CancelFireBubble()
	{
		if (Singleton<LevelManager>.Instance.iBubbleCount <= 5)
		{
			SwitchoverElfAni("ready_to_start", bLoop: false);
		}
		else
		{
			SwitchoverElfAni("ready_to_start", bLoop: false);
		}
		iNowUpdateWait = 0;
	}

	public void ChangeBubble()
	{
		if (Singleton<LevelManager>.Instance.iBubbleCount <= 5)
		{
			SwitchoverElfAni("change", bLoop: false);
		}
		else
		{
			SwitchoverElfAni("change", bLoop: false);
		}
		iNowUpdateWait = 0;
	}

	public void GirlAni_lively1(bool b = false)
	{
		if (b)
		{
			bGirlAni_lively1 = true;
		}
		else if (!bThisOnePlayAni)
		{
			if (Singleton<LevelManager>.Instance.iBubbleCount <= 5)
			{
				SwitchoverElfAni("happy2", bLoop: false);
			}
			else
			{
				SwitchoverElfAni("happy2", bLoop: false);
			}
			iNowUpdateWait = 0;
		}
	}

	public void GirlAni_happy2(bool b = false)
	{
		if (b)
		{
			if (bGirlAni_lively1 && !bThisOnePlayAni)
			{
			}
		}
		else
		{
			if (bThisOnePlayAni)
			{
				return;
			}
			if (Singleton<LevelManager>.Instance.iBubbleCount <= 5)
			{
				SwitchoverElfAni("happy2", bLoop: false);
			}
			else
			{
				SwitchoverElfAni("happy2", bLoop: false);
				int num = UnityEngine.Random.Range(1, 100);
				if (num < Singleton<DataManager>.Instance.ivo_role_cheers && (bool)SoundController.action)
				{
					SoundController.action.playNow("vo_role_cheers");
				}
			}
			iNowUpdateWait = 0;
		}
	}

	public void GirlAni_fault(bool b = false)
	{
		if (b)
		{
			iFaultCombo = 0;
			if (Singleton<LevelManager>.Instance.iBubbleCount <= 5)
			{
				SwitchoverElfAni("fire_to_start", bLoop: false);
			}
			else
			{
				SwitchoverElfAni("fire_to_start", bLoop: false);
			}
			return;
		}
		iFaultCombo++;
		bool flag = false;
		int num = UnityEngine.Random.Range(1, 100);
		if (iFaultCombo == 1)
		{
			if (num < 10)
			{
				flag = true;
			}
		}
		else if (iFaultCombo == 2)
		{
			if (num < 30)
			{
				flag = true;
			}
		}
		else if (num < 50)
		{
			flag = true;
		}
		if (!flag)
		{
			if (Singleton<LevelManager>.Instance.iBubbleCount <= 5)
			{
				SwitchoverElfAni("fire_to_start", bLoop: false);
			}
			else
			{
				SwitchoverElfAni("fire_to_start", bLoop: false);
			}
			return;
		}
		if (Singleton<LevelManager>.Instance.iBubbleCount <= 5)
		{
			StartCoroutine(IESwitchoverElfAni("loser"));
		}
		else
		{
			StartCoroutine(IESwitchoverElfAni("loser"));
		}
		iNowUpdateWait = 0;
	}

	private IEnumerator IESwitchoverElfAni(string SAniName)
	{
		yield return new WaitForSeconds(0.5f);
		SwitchoverElfAni(SAniName, bLoop: false);
	}

	public void InitMubiao()
	{
		iMubiaoSum = Singleton<LevelManager>.Instance.iLevelCount;
		Initelfin_diaoluoObj(iMubiaoSum);
		iMubiaoType = Singleton<LevelManager>.Instance.iLevelType;
		mubiaoCountText.SetText("0/" + iMubiaoSum);
		if (iMubiaoSum < 10)
		{
			mubiaoCountText.transform.localPosition = mubiaoCountText.transform.localPosition - new Vector3(20f, 0f, 0f);
		}
		if (iMubiaoType == 1)
		{
			mibiaoIconObj.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/gameui/game_icon_elf_1", 54, 58);
		}
		else
		{
			mibiaoIconObj.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/gameui/game_icon_elf_2", 66, 62);
		}
	}

	public void UpdateMubiao()
	{
		iNowMubiaoCount++;
		mubiaoCountText.SetText(iNowMubiaoCount + "/" + Singleton<LevelManager>.Instance.iLevelCount);
		if (iNowMubiaoCount >= iMubiaoSum || iNowMubiaoTmpCount >= iMubiaoSum)
		{
			mubiaoCountText.SetText(Singleton<LevelManager>.Instance.iLevelCount + "/" + Singleton<LevelManager>.Instance.iLevelCount);
			FlyOver();
		}
		else if (bWin)
		{
			mubiaoCountText.SetText(Singleton<LevelManager>.Instance.iLevelCount + "/" + Singleton<LevelManager>.Instance.iLevelCount);
			FlyOver();
		}
	}

	public void FlyOver()
	{
		SwitchoverElfAni("happy1", bLoop: true);
		if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex > 10000 && Singleton<LevelManager>.Instance.iNowSelectLevelIndex < 20000)
		{
			BubbleSpawner.Instance.WinFallBubble();
			return;
		}
		UI.Instance.OpenPanel(UIPanelType.TipWin);
		GameUI.action.WinCombo();
		if ((bool)MusicController.action)
		{
			MusicController.action.BG_Combo();
		}
	}

	private IEnumerator IEStarGame()
	{
		bool b = true;
		if (!LevelManager.bWwwDataFlag)
		{
			yield break;
		}
		while (b)
		{
			yield return new WaitForSeconds(0.1f);
			if (Singleton<LevelManager>.Instance.bLoadOver)
			{
				b = false;
				Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.GameScene);
			}
		}
	}

	public IEnumerator IECheckWin()
	{
		yield return new WaitForSeconds(0.01f);
		bool bWhile = true;
		while (true)
		{
			if (!bWhile)
			{
				yield break;
			}
			yield return new WaitForSeconds(0.01f);
			icount++;
			if (!CheckDown() && icount <= 10)
			{
				continue;
			}
			bWhile = false;
			if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex > 10000 && Singleton<LevelManager>.Instance.iNowSelectLevelIndex < 20000)
			{
				break;
			}
			if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 13 || Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 17 || Singleton<LevelManager>.Instance.iNowSelectLevelIndex == 27)
			{
				int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iMaxGuide_" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex);
				Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "DB_iMaxGuide_" + Singleton<LevelManager>.Instance.iNowSelectLevelIndex, 1);
				if (@int == 0)
				{
					if (!InitGame.bChinaVersion)
					{
						UI.Instance.OpenPanel(UIPanelType.SkillTip);
					}
					else
					{
						huangbosswin();
					}
				}
				else if (FaceBookApi.Action.bLoginState())
				{
					CheckFaceBookRank();
				}
				else
				{
					huangbosswin();
				}
			}
			else if (FaceBookApi.Action.bLoginState())
			{
				CheckFaceBookRank();
			}
			else
			{
				huangbosswin();
			}
		}
		GameObject levelUPObj = UnityEngine.Object.Instantiate(levelUP);
		levelUPObj.transform.parent = base.gameObject.transform;
		levelUPObj.transform.DOScale(1f, 1.5f).OnComplete(delegate
		{
			UnityEngine.Object.Destroy(levelUPObj);
		});
		yield return new WaitForSeconds(1.5f);
		Singleton<LevelManager>.Instance.dareIndex++;
		Singleton<DataManager>.Instance.iDareBushu = Singleton<LevelManager>.Instance.iBubbleCount;
		Singleton<DataManager>.Instance.iDareScore = Singleton<LevelManager>.Instance.iNowScore;
		if (Singleton<LevelManager>.Instance.dareIndex == 4)
		{
			Singleton<UIManager>.Instance.OpenUI(EnumUIType.DareWinUI);
			yield break;
		}
		Singleton<LevelManager>.Instance.iNowSelectLevelIndex = 10000 + Singleton<LevelManager>.Instance.dareLevels[Singleton<LevelManager>.Instance.dareIndex];
		if (LevelManager.bWwwDataFlag)
		{
			Singleton<LevelManager>.Instance.bLoadOver = false;
			DataManager.SelectLevel = 0;
			Singleton<LevelManager>.Instance.LoadLevelData();
			StartCoroutine(IEStarGame());
		}
		else
		{
			Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.GameScene);
		}
	}

	public IEnumerator IEUpdateWin()
	{
		yield return new WaitForSeconds(0.2f);
	}

	public void huangbosswin()
	{
		if (Singleton<LevelManager>.Instance.bBossHuang)
		{
			if (Singleton<LevelManager>.Instance.bBossHuangReward)
			{
				Singleton<LevelManager>.Instance.bBossHuangReward = false;
				HuaGame.action.AddJianKangDu(50);
				GameUI.action.BossReward();
				if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "HuangchongbossFirst") == 0)
				{
					Singleton<TestScript>.Instance.SetInt(DataManager.SDBNO + "HuangchongbossFirst", 1);
				}
			}
		}
		else
		{
			WinPanmove.action.movepanel();
		}
	}

	public void CheckFaceBookRank()
	{
		if ((bool)FireBase.Action)
		{
			Dictionary<string, int> tempFriendListRank = FireBase.Action._TempFriendListRank;
			if (tempFriendListRank != null && tempFriendListRank.Count > 0)
			{
				int num = 0;
				int num2 = 1;
				bool flag = false;
				foreach (string key in tempFriendListRank.Keys)
				{
					if (flag)
					{
						FaceBookApi.Action.Rank1FID = key;
						FaceBookApi.Action.Rank1Score = tempFriendListRank[key].ToString();
						try
						{
							FaceBookApi.Action.Rank1Name = FaceBookApi.Action.dFacebookName[key];
						}
						catch (Exception)
						{
						}
						break;
					}
					if (key == FaceBookApi.Action.UserId)
					{
						num = num2;
						flag = true;
					}
					num2++;
				}
				if (num > 0)
				{
					tempFriendListRank.Remove(FaceBookApi.Action.UserId);
					tempFriendListRank.Add(FaceBookApi.Action.UserId, Singleton<LevelManager>.Instance.iNowScore);
					Dictionary<string, int> dictionary = FireBase.Action.DictionarySort(tempFriendListRank);
					int num3 = 0;
					num2 = 1;
					bool flag2 = false;
					foreach (string key2 in dictionary.Keys)
					{
						if (flag2)
						{
							FaceBookApi.Action.Rank1FID = key2;
							try
							{
								FaceBookApi.Action.Rank1Name = FaceBookApi.Action.dFacebookName[key2];
							}
							catch (Exception)
							{
							}
							FaceBookApi.Action.Rank1Score = dictionary[key2].ToString();
							break;
						}
						if (key2 == FaceBookApi.Action.UserId)
						{
							num3 = num2;
							flag2 = true;
						}
						num2++;
					}
					if (num3 < dictionary.Count && num3 < num)
					{
						FaceBookApi.Action.Rank2FID = FaceBookApi.Action.UserId;
						FaceBookApi.Action.Rank2Name = FaceBookApi.Action.MyFaceBookName;
						FaceBookApi.Action.Rank2Score = Singleton<LevelManager>.Instance.iNowScore.ToString();
						FaceBookApi.Action.Rank2Rank = num3.ToString();
						Singleton<UIManager>.Instance.OpenUI(EnumUIType.FaceBookRankOpenUI);
						return;
					}
				}
				else
				{
					tempFriendListRank.Add(FaceBookApi.Action.UserId, Singleton<LevelManager>.Instance.iNowScore);
					Dictionary<string, int> dictionary2 = FireBase.Action.DictionarySort(tempFriendListRank);
					int num4 = 0;
					num2 = 1;
					bool flag3 = false;
					foreach (string key3 in dictionary2.Keys)
					{
						if (flag3)
						{
							FaceBookApi.Action.Rank1FID = key3;
							try
							{
								FaceBookApi.Action.Rank1Name = FaceBookApi.Action.dFacebookName[key3];
							}
							catch (Exception)
							{
							}
							FaceBookApi.Action.Rank1Score = dictionary2[key3].ToString();
							break;
						}
						if (key3 == FaceBookApi.Action.UserId)
						{
							num4 = num2;
							flag3 = true;
						}
						num2++;
					}
					if (num4 < dictionary2.Count)
					{
						FaceBookApi.Action.Rank2FID = FaceBookApi.Action.UserId;
						FaceBookApi.Action.Rank2Name = FaceBookApi.Action.MyFaceBookName;
						FaceBookApi.Action.Rank2Score = Singleton<LevelManager>.Instance.iNowScore.ToString();
						FaceBookApi.Action.Rank2Rank = num.ToString();
						Singleton<UIManager>.Instance.OpenUI(EnumUIType.FaceBookRankOpenUI);
						return;
					}
				}
			}
		}
		huangbosswin();
	}

	public void SaveUserTask()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iKillBubbleCount");
		int int2 = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iDownGangCount");
		Singleton<DataManager>.Instance.SaveUserDate("DB_iKillBubbleCount", @int + iKillBubbleCount);
		Singleton<DataManager>.Instance.SaveUserDate("DB_iDownGangCount", int2 + iDownGangCount);
		Singleton<UserManager>.Instance.SetPassTask("KillBubble", iKillBubbleCount);
		Singleton<UserManager>.Instance.SetPassTask("CollectBubble", iDownGangCount);
		Singleton<UserManager>.Instance.SetPassTask1("KillBubble", iKillBubbleCount);
		Singleton<UserManager>.Instance.SetPassTask1("jingling", iMubiaoSum);
	}

	public void playaaa(GameObject obj, string anSName)
	{
		SkeletonAnimation component = obj.GetComponent<SkeletonAnimation>();
		component.Initialize(overwrite: true);
		component.loop = true;
		component.state.SetAnimation(0, anSName, loop: false);
	}

	public void diaoluo(GameObject obj)
	{
		SkeletonAnimation component = obj.GetComponent<SkeletonAnimation>();
		component.Initialize(overwrite: true);
		component.loop = true;
		component.state.SetAnimation(0, "down", loop: false);
		component.state.End += delegate
		{
			playaaa(obj, "fly");
		};
		Vector3 localPosition = obj.transform.localPosition;
		float y = localPosition.y;
		int index = idiaoluoCount;
		idiaoluoCount++;
		Vector2 vector = LdiaoluoPos[index];
		float x = vector.x;
		Vector2 vector2 = LdiaoluoPos[index];
		float y2 = vector2.y;
		Vector3 localPosition2 = obj.transform.localPosition;
		Vector3 vector3 = new Vector3(x, y2, localPosition2.z);
		float num = Vector3.Distance(vector3, obj.transform.localPosition);
		num = (num - 300f) / 2000f;
		Vector3 localPosition3 = obj.transform.localPosition;
		float x2 = localPosition3.x - 20f;
		Vector3 localPosition4 = obj.transform.localPosition;
		float y3 = localPosition4.y + 10f;
		Vector3 localPosition5 = obj.transform.localPosition;
		Vector3 vector4 = new Vector3(x2, y3, localPosition5.z);
		Vector3 centerPost = GetCenterPost(vector4, vector3, 5f);
		Vector3[] path = new Vector3[4]
		{
			obj.transform.localPosition,
			vector4,
			centerPost,
			vector3
		};
		obj.transform.DOLocalPath(path, 2.5f + num, PathType.CatmullRom, PathMode.TopDown2D, 20).SetEase(Ease.InSine).OnComplete(delegate
		{
			diaoluo2(obj);
		});
	}

	public void playTianChong(GameObject _obj, int index)
	{
		GameObject gameObject = null;
		if (index == 1)
		{
			gameObject = GameGuide.Instance.MT1;
		}
		else if (index == 2)
		{
			gameObject = GameGuide.Instance.MT2;
		}
		else if (index == 3)
		{
			gameObject = GameGuide.Instance.MT4;
		}
		else if (index == 4)
		{
			gameObject = GameGuide.Instance.MT5;
		}
		GameObject obj = UnityEngine.Object.Instantiate(tianchongfly1);
		obj.transform.parent = tianchongflyparent.transform;
		obj.transform.position = _obj.transform.position;
		Vector3 localPosition = obj.transform.localPosition;
		float y = localPosition.y;
		int num = idiaoluoCount;
		idiaoluoCount++;
		Vector3 localPosition2 = gameObject.transform.localPosition;
		float num2 = Vector3.Distance(localPosition2, obj.transform.localPosition);
		num2 = (num2 - 300f) / 2000f;
		Vector3 localPosition3 = obj.transform.localPosition;
		float x = localPosition3.x - 0.2f;
		Vector3 localPosition4 = obj.transform.localPosition;
		float y2 = localPosition4.y + 0.1f;
		Vector3 localPosition5 = obj.transform.localPosition;
		Vector3 vector = new Vector3(x, y2, localPosition5.z);
		Vector3 centerPost = GetCenterPost(vector, localPosition2, 5f);
		Vector3[] path = new Vector3[4]
		{
			obj.transform.localPosition,
			vector,
			centerPost,
			localPosition2
		};
		obj.transform.DOLocalPath(path, 1.5f + num2, PathType.CatmullRom, PathMode.TopDown2D, 20).SetEase(Ease.InSine).OnComplete(delegate
		{
			playTianChong2(obj, index);
		});
	}

	public void playTianChong2(GameObject obj, int index)
	{
		Transform transform = obj.transform;
		Vector3 localPosition = obj.transform.localPosition;
		transform.DOLocalMoveY(localPosition.y, 0.2f).SetEase(Ease.OutSine).OnComplete(delegate
		{
			playTianChong3(obj, index);
		});
	}

	public void playTianChong3(GameObject obj, int index)
	{
		UnityEngine.Object.Destroy(obj.gameObject);
		GameObject gameObject = null;
		switch (index)
		{
		case 1:
			gameObject = GameGuide.Instance.MT1;
			break;
		case 2:
			gameObject = GameGuide.Instance.MT2;
			break;
		case 3:
			gameObject = GameGuide.Instance.MT4;
			break;
		case 4:
			gameObject = GameGuide.Instance.MT5;
			break;
		}
		if ((bool)gameObject)
		{
			gameObject.GetComponent<MuTong>().mtState = 4;
			gameObject.GetComponent<MuTong>().animStatic();
		}
	}

	public void diaoluo2(GameObject obj)
	{
		Transform transform = obj.transform;
		Vector3 localPosition = obj.transform.localPosition;
		transform.DOLocalMoveY(localPosition.y + 25f, 0.2f).SetEase(Ease.OutSine).OnComplete(delegate
		{
			diaoluoOverStatic(obj);
		});
	}

	public void diaoluoOverStatic(GameObject obj)
	{
		SkeletonAnimation component = obj.GetComponent<SkeletonAnimation>();
		component.loop = true;
		if (bWin)
		{
			component.Initialize(overwrite: true);
			component.loop = true;
			component.state.SetAnimation(0, "dance", loop: true);
		}
		else
		{
			int num = UnityEngine.Random.Range(0, 100);
			if (component.AnimationName == null || component.AnimationName == "down")
			{
				component.Initialize(overwrite: true);
				component.state.SetAnimation(0, "static", loop: false);
			}
			else if (num < 20)
			{
				component.Initialize(overwrite: true);
				component.state.SetAnimation(0, "static", loop: false);
			}
			else if (num < 40)
			{
				component.Initialize(overwrite: true);
				component.state.SetAnimation(0, "see", loop: false);
			}
			else if (num < 60)
			{
				component.Initialize(overwrite: true);
				component.state.SetAnimation(0, "dahaqian", loop: false);
			}
			else if (num < 80)
			{
				component.Initialize(overwrite: true);
				component.state.SetAnimation(0, "dahu", loop: false);
			}
			else
			{
				component.Initialize(overwrite: true);
				component.state.SetAnimation(0, "play", loop: false);
			}
		}
		component.state.End += delegate
		{
			diaoluoOverStatic(obj);
		};
	}

	public void Debug2(string str)
	{
		UnityEngine.Debug.Log("str = " + str);
	}

	public void diaoluowin()
	{
		for (int i = 0; i < elfin_diaoluoObj.Count; i++)
		{
			if (elfin_diaoluoObj[i] != null)
			{
				SkeletonAnimation component = elfin_diaoluoObj[i].GetComponent<SkeletonAnimation>();
				component.Initialize(overwrite: true);
				component.loop = true;
				component.state.SetAnimation(0, "dance", loop: true);
			}
		}
	}

	public void CreateElfSkillFlyFx(GameObject StartObj, GameObject ToObj)
	{
		StartCoroutine(IECreateElfSkillFlyFx(StartObj, ToObj));
	}

	private IEnumerator IECreateElfSkillFlyFx(GameObject StartObj, GameObject ToObj)
	{
		yield return new WaitForSeconds(1.2f);
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_save");
		}
		GameObject Skillfxobj = UnityEngine.Object.Instantiate(SkillElfFly);
		int iRx = UnityEngine.Random.Range(-30, 30);
		int iRy = UnityEngine.Random.Range(-30, 30);
		Transform transform = Skillfxobj.transform;
		Vector3 position = Skillfxobj.transform.position;
		float x = position.x + (float)iRx * 0.01f;
		Vector3 position2 = Skillfxobj.transform.position;
		float y = position2.y + (float)iRy * 0.01f;
		Vector3 position3 = Skillfxobj.transform.position;
		transform.position = new Vector3(x, y, position3.z);
		SkillFly(Skillfxobj, ToObj);
	}

	private void SkillFly(GameObject obj, GameObject ToObj)
	{
		SkeletonAnimation component = obj.GetComponent<SkeletonAnimation>();
		component.Initialize(overwrite: true);
		component.loop = true;
		component.state.SetAnimation(0, "fly", loop: true);
		Vector3 localPosition = ToObj.transform.localPosition;
		BallFlyObj component2 = obj.GetComponent<BallFlyObj>();
		Vector3 localPosition2 = obj.transform.localPosition;
		float y = localPosition2.y;
		Vector3 localPosition3 = obj.transform.localPosition;
		float x = localPosition3.x;
		Vector3 centerPost = GetCenterPost(ToObj.transform.localPosition, localPosition, 9f);
		Vector3[] path = new Vector3[3]
		{
			obj.transform.localPosition,
			centerPost,
			localPosition
		};
		obj.transform.DOLocalPath(path, 1.8f, PathType.CatmullRom, PathMode.TopDown2D).SetEase(Ease.InQuart).OnComplete(delegate
		{
			DescSKillObj(obj);
		});
	}

	public void CreateFlyBubbleObj(GameObject obj, int attributes)
	{
		try
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("ui_save");
			}
			int num = UnityEngine.Random.Range(0, 100);
			num = UnityEngine.Random.Range(1, 4);
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("vo_elf_save" + num);
			}
			bThisOnePlayAni = true;
			GameObject gameObject = UnityEngine.Object.Instantiate(BubbleElfObj);
			gameObject.transform.parent = FlyFatherObj.transform;
			gameObject.transform.position = obj.transform.position;
			Vector3 localPosition = FlyToObj.transform.localPosition;
			Vector3 localPosition2 = gameObject.transform.localPosition;
			if (localPosition2.x > localPosition.x)
			{
				gameObject.transform.position = gameObject.transform.position + new Vector3(-0.2f, -0.2f, 0f);
			}
			else
			{
				gameObject.transform.position = gameObject.transform.position + new Vector3(0.2f, -0.2f, 0f);
			}
			BallFlyObj component = gameObject.GetComponent<BallFlyObj>();
			component.SetType(attributes);
			SkeletonAnimation component2 = component.ElfObj.GetComponent<SkeletonAnimation>();
			component2.Initialize(overwrite: true);
			component2.loop = false;
			component2.state.SetAnimation(0, "elf_shake", loop: false);
			Fly(gameObject);
			i_elfin_diaoluoObjCount++;
		}
		catch (Exception arg)
		{
			bWin = true;
			UnityEngine.Debug.Log("CreateFlyBubbleObj  diaoluo err   " + arg);
		}
	}

	public void Fly(GameObject obj)
	{
		iNowMubiaoTmpCount++;
		if (iNowMubiaoTmpCount >= iMubiaoSum)
		{
			bWin = true;
			SoundController.action.playNow("FireworkBurst");
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("ui_game_complete");
			}
			diaoluowin();
		}
		Vector3 vector = FlyToObj.transform.localPosition;
		float x = vector.x;
		float y = vector.y;
		Vector3 localPosition = obj.transform.localPosition;
		vector = new Vector3(x, y, localPosition.z);
		BallFlyObj component = obj.GetComponent<BallFlyObj>();
		SkeletonAnimation component2 = component.ElfObj.GetComponent<SkeletonAnimation>();
		component2.Initialize(overwrite: true);
		component2.loop = true;
		component2.state.SetAnimation(0, "elf_shake", loop: false);
		Vector3 localPosition2 = obj.transform.localPosition;
		float y2 = localPosition2.y;
		Vector3 localPosition3 = obj.transform.localPosition;
		float x2 = localPosition3.x;
		obj.transform.DOScale(new Vector2(200f, 200f), 0.3f).SetEase(Ease.OutSine);
		obj.transform.DOScale(new Vector2(100f, 100f), 0.3f).SetEase(Ease.OutSine).SetDelay(0.3f);
		obj.transform.DOScale(new Vector2(180f, 180f), 0.2f).SetEase(Ease.OutSine).SetDelay(0.6f);
		obj.transform.DOScale(new Vector2(100f, 100f), 0.1f).SetEase(Ease.OutSine).SetDelay(0.8f);
		float num = Mathf.Abs(y2 + 85f) / 554f * 0.5f;
		y2 = y2 * -1f / 14f * 0.5f;
		x2 = x2 * -1f / 12f * 5f;
		Vector3 localPosition4 = obj.transform.localPosition;
		float x3 = localPosition4.x + x2;
		Vector3 localPosition5 = obj.transform.localPosition;
		float y3 = localPosition5.y + y2;
		Vector3 localPosition6 = obj.transform.localPosition;
		Vector3 vector2 = new Vector3(x3, y3, localPosition6.z);
		Vector3 centerPost = GetCenterPost(vector2, vector, 9f);
		Vector3 localPosition7 = obj.transform.localPosition;
		if (localPosition7.x > vector.x)
		{
			x2 = (50f - x2) * 1f;
			Vector3 localPosition8 = obj.transform.localPosition;
			float x4 = localPosition8.x - x2;
			Vector3 localPosition9 = obj.transform.localPosition;
			float y4 = localPosition9.y + y2;
			Vector3 localPosition10 = obj.transform.localPosition;
			vector2 = new Vector3(x4, y4, localPosition10.z);
			centerPost = GetCenterPost(vector2, vector, 3f, _bOrientation: true);
		}
		Vector3[] path = new Vector3[4]
		{
			obj.transform.localPosition,
			vector2,
			centerPost,
			vector
		};
		obj.transform.DOLocalPath(path, 0.8f + num, PathType.CatmullRom, PathMode.TopDown2D).SetEase(Ease.InQuart).OnComplete(delegate
		{
			DescObj(obj);
		});
	}

	public void CreateFlyBubbleObj2()
	{
		GameObject obj = BubbleSpawner.Instance.BubbleFlyObj.GetComponent<BubbleObj>().fx_flyobj_;
		obj.transform.parent = FlyFatherObj.transform;
		Vector3[] array = new Vector3[1]
		{
			FlyToObj.transform.position
		};
		obj.transform.DOMove(FlyToObj.transform.position, 1f).OnComplete(delegate
		{
			DescObj2_flyLevel(obj);
		});
	}

	public void DescObj2_flyLevel(GameObject obj)
	{
		mubiaoCountText.SetText("1/1");
		BaseUIAnimation.action.MubiaoIconAniFly2(GameUI.action.mubiaoiCon2);
		SwitchoverElfAni("happy1", bLoop: true);
		iNowUpdateWait = 0;
		GameObject gameObject = UnityEngine.Object.Instantiate(elf_starObj);
		gameObject.transform.parent = mibiaoIconObj.transform;
		gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
		gameObject.transform.localScale = new Vector3(100f, 100f, 100f);
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_collect");
		}
		UnityEngine.Object.Destroy(gameObject, 3f);
		UnityEngine.Object.Destroy(obj);
	}

	private void DescSKillObj(GameObject obj)
	{
		UnityEngine.Object.Destroy(obj);
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

	public void DescObj(GameObject obj)
	{
		UpdateMubiao();
		BaseUIAnimation.action.MubiaoIconAni(mibiaoIconObj);
		if (Singleton<LevelManager>.Instance.iBubbleCount <= 5)
		{
			SwitchoverElfAni("happy2", bLoop: false);
		}
		else
		{
			SwitchoverElfAni("happy2", bLoop: false);
		}
		if (iNowMubiaoTmpCount != iMubiaoSum)
		{
			int num = UnityEngine.Random.Range(0, 100);
			if (num < Singleton<DataManager>.Instance.ivo_role_save && (bool)SoundController.action)
			{
				SoundController.action.playNow("vo_role_save");
			}
		}
		iNowUpdateWait = 0;
		GameObject gameObject = UnityEngine.Object.Instantiate(elf_starObj);
		gameObject.transform.parent = mibiaoIconObj.transform;
		gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
		gameObject.transform.localScale = new Vector3(100f, 100f, 100f);
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("ui_collect");
		}
		UnityEngine.Object.Destroy(gameObject, 3f);
		UnityEngine.Object.Destroy(obj);
	}

	private void Update()
	{
		if ((bool)_hanObj)
		{
			SkeletonAnimation component = jlxzObj.GetComponent<SkeletonAnimation>();
			_hanObj.transform.localPosition = new Vector3(component.skeleton.FindBone("head_1").worldX, component.skeleton.FindBone("head_1").worldY + 0.5f, 0f);
		}
	}

	public void Liuhan()
	{
		if (!_hanObj)
		{
			_hanObj = UnityEngine.Object.Instantiate(hanObj);
			_hanObj.transform.parent = jlxzObj.transform;
			_hanObj.transform.localPosition = new Vector3(0f, 4f, 0f);
			bbliuhan();
			BaseUIAnimation.action.Bubble_5();
		}
	}

	public void AddBushu(GameObject obj)
	{
		UnityEngine.Debug.Log("加步数");
		GameObject _CopyObj = UnityEngine.Object.Instantiate(obj);
		_CopyObj.transform.parent = AddAndCutObj.transform;
		_CopyObj.transform.position = obj.transform.position;
		_CopyObj.transform.localScale = new Vector2(0f, 0f);
		BubbleObj component = _CopyObj.GetComponent<BubbleObj>();
		component.render.GetComponent<SpriteRenderer>().sprite = Util.GetResourcesSprite("Img/bubble_add3_3", 64, 64);
		component.render.GetComponent<SpriteRenderer>().sortingOrder = 100;
		Sequence s = DOTween.Sequence();
		s.Append(_CopyObj.transform.DOScale(new Vector2(200f, 200f), 0.25f).SetEase(Ease.InOutSine)).Append(_CopyObj.transform.DOScale(new Vector2(80f, 80f), 0.22f).SetEase(Ease.OutSine)).Append(_CopyObj.transform.DOScale(new Vector2(100f, 100f), 0.13f).SetEase(Ease.OutSine))
			.OnComplete(delegate
			{
				addMove(_CopyObj, 3);
			});
		Singleton<LevelManager>.Instance.iBubbleCount = Singleton<LevelManager>.Instance.iBubbleCount + 3;
		if (Singleton<LevelManager>.Instance.iBubbleCount > 10)
		{
			GameUI.action.KillNowBuyBubble();
		}
	}

	public void UseBushu(GameObject obj)
	{
		UnityEngine.Debug.Log(" 减步数 ");
		GameObject _CopyObj = UnityEngine.Object.Instantiate(obj);
		_CopyObj.transform.parent = AddAndCutObj.transform;
		_CopyObj.transform.position = obj.transform.position;
		_CopyObj.transform.localScale = new Vector2(100f, 100f);
		BubbleObj component = _CopyObj.GetComponent<BubbleObj>();
		component.render.GetComponent<SpriteRenderer>().sprite = Util.GetResourcesSprite("Img/bubble_minus3_3", 64, 64);
		component.render.GetComponent<SpriteRenderer>().sortingOrder = 100;
		Sequence s = DOTween.Sequence();
		s.Append(_CopyObj.transform.DOScale(new Vector2(200f, 200f), 0.25f).SetEase(Ease.InOutSine)).Append(_CopyObj.transform.DOScale(new Vector2(80f, 80f), 0.22f).SetEase(Ease.OutSine)).Append(_CopyObj.transform.DOScale(new Vector2(100f, 100f), 0.13f).SetEase(Ease.OutSine))
			.OnComplete(delegate
			{
				addMove(_CopyObj, -3);
			});
		Singleton<LevelManager>.Instance.iBubbleCount = Singleton<LevelManager>.Instance.iBubbleCount - 3;
		if (Singleton<LevelManager>.Instance.iBubbleCount < 0)
		{
			Singleton<LevelManager>.Instance.iBubbleCount = 0;
		}
	}

	public void addMove(GameObject _CopyObj, int ibubblecount)
	{
		BubbleFly(_CopyObj, ibubblecount);
	}

	public void BubbleFly(GameObject obj, int imoveCount)
	{
		Vector3 localPosition = FlyToObj.transform.localPosition;
		localPosition = new Vector3(-233f, -932f, 1600f);
		Vector3 localPosition2 = obj.transform.localPosition;
		float y = localPosition2.y;
		Vector3 localPosition3 = obj.transform.localPosition;
		float x = localPosition3.x;
		float num = Mathf.Abs(y + 85f) / 554f * 0.5f;
		y = y * -1f / 14f * 0f;
		x = x * -1f / 12f * 0f;
		Vector3 localPosition4 = obj.transform.localPosition;
		float x2 = localPosition4.x + x;
		Vector3 localPosition5 = obj.transform.localPosition;
		float y2 = localPosition5.y + y;
		Vector3 localPosition6 = obj.transform.localPosition;
		Vector3 vector = new Vector3(x2, y2, localPosition6.z);
		Vector3 centerPost = GetCenterPost(vector, localPosition, 9f);
		Vector3 localPosition7 = obj.transform.localPosition;
		if (localPosition7.x < localPosition.x)
		{
			x = (50f - x) * 1f;
			Vector3 localPosition8 = obj.transform.localPosition;
			float x3 = localPosition8.x - x;
			Vector3 localPosition9 = obj.transform.localPosition;
			float y3 = localPosition9.y + y;
			Vector3 localPosition10 = obj.transform.localPosition;
			vector = new Vector3(x3, y3, localPosition10.z);
			centerPost = GetCenterPost(vector, localPosition, 3f, _bOrientation: true);
		}
		Vector3[] array = new Vector3[4]
		{
			obj.transform.localPosition,
			vector,
			centerPost,
			localPosition
		};
		DesBubbleFlyObj(obj, imoveCount);
	}

	private void DesBubbleFlyObj(GameObject obj, int imoveCount)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(fx_prop3_addBubble);
		gameObject.transform.parent = obj.transform.parent;
		gameObject.transform.position = obj.transform.position;
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		if (imoveCount > 0)
		{
			BubbleSpawner.Instance.initReadyBubble();
		}
		else
		{
			BubbleSpawner.Instance.initReadyBubble(isusekey: true, bcut: true);
		}
		GameUI.action.BubbleCountText.text = Singleton<LevelManager>.Instance.iBubbleCount.ToString();
		UnityEngine.Object.Destroy(obj);
		UnityEngine.Object.Destroy(gameObject, 0.5f);
	}

	public void bbliuhan()
	{
	}

	public void returnbbhappy()
	{
		for (int i = 0; i < elfin_diaoluoObj.Count; i++)
		{
			if (!(elfin_diaoluoObj[i] != null))
			{
				continue;
			}
			SkeletonAnimation component = elfin_diaoluoObj[i].GetComponent<SkeletonAnimation>();
			if (component.AnimationName == "worry")
			{
				component.Initialize(overwrite: true);
				component.loop = true;
				int num = UnityEngine.Random.Range(0, 100);
				if (num < 20)
				{
					component.state.SetAnimation(0, "static", loop: true);
				}
				else if (num < 40)
				{
					component.state.SetAnimation(0, "see", loop: true);
				}
				else if (num < 60)
				{
					component.state.SetAnimation(0, "dahaqian", loop: true);
				}
				else if (num < 80)
				{
					component.state.SetAnimation(0, "dahu", loop: true);
				}
				else
				{
					component.state.SetAnimation(0, "play", loop: true);
				}
			}
		}
	}

	public void KillLiuhan()
	{
		if ((bool)_hanObj)
		{
			UnityEngine.Object.Destroy(_hanObj.gameObject);
		}
		returnbbhappy();
		BaseUIAnimation.action.Bubble_10();
	}

	public IEnumerator playAnim(string sAniName, bool bLoop)
	{
		yield return new WaitForSeconds(0.1f);
		SwitchoverElfAni(sAniName, bLoop);
	}

	public void SwitchoverElfAni(string sAniName, bool bLoop)
	{
		SkeletonAnimation component = jlxzObj.GetComponent<SkeletonAnimation>();
		if (sAniName == "to_static")
		{
			if (BubbleSpawner.Instance.Combo < 5 && Singleton<LevelManager>.Instance.iBubbleCount >= 5)
			{
				component.state.AddAnimation(0, "static", loop: true, 0f);
			}
		}
		else if (sAniName == "to_start")
		{
			sAniName = ((Singleton<LevelManager>.Instance.iBubbleCount >= 5) ? "fire_to_start" : "fire_to_start");
		}
		else
		{
			if (component.AnimationName == "happy1" || component.AnimationName == "cry" || (sAniName == "start_to_static" && (component.AnimationName == "worry" || component.AnimationName == "fire" || component.AnimationName == "fire")))
			{
				return;
			}
			if (sAniName == "fire_to_start" && component.AnimationName == "fire")
			{
				component.state.AddAnimation(0, "fire_to_start", loop: false, 0f);
				if (BubbleSpawner.Instance.Combo >= 5)
				{
					component.state.AddAnimation(0, "start", loop: true, 0f);
				}
				else
				{
					component.state.AddAnimation(0, "start", loop: true, 0f);
				}
				return;
			}
			component.loop = bLoop;
			PlayGirlMp3(sAniName);
			UnityEngine.Debug.Log("Switch JLXZ  sAniName=" + sAniName + "    ," + bLoop);
			component.state.SetAnimation(0, sAniName, bLoop);
			switch (sAniName)
			{
			case "fire_to_start":
			case "loser":
			case "happy2":
			case "change":
			case "ready_to_start":
				if (BubbleSpawner.Instance.Combo >= 5)
				{
					component.state.AddAnimation(0, "start", loop: true, 0f);
				}
				else
				{
					component.state.AddAnimation(0, "start", loop: true, 0f);
				}
				break;
			}
			if (sAniName == "start_to_static")
			{
				component.state.AddAnimation(0, "static", loop: true, 0f);
			}
		}
	}

	public void PlayGirlMp3(string sAniname)
	{
		if (!SoundController.action)
		{
			return;
		}
		if (sAniname == "happy")
		{
			SoundController.action.playNow("role_girl_win", NowPlay: true);
		}
		else if (sAniname == "wait")
		{
			SoundController.action.playNow("role_girl_wait");
		}
		else if (sAniname == "cry")
		{
			SoundController.action.playNow("role_girl_cry");
		}
		else if (!(sAniname == "worry"))
		{
			if (sAniname == "win")
			{
				SoundController.action.playNow("role_girl_win", NowPlay: true);
			}
			else if ("loser" == sAniname || "loser" == sAniname)
			{
				SoundController.action.playNow("vo_role_despond");
			}
			else if ("fire" == sAniname || "fire" == sAniname)
			{
				vo_role_shoot();
			}
		}
	}

	public void SwitchoverElfAniWin(string sAniName, bool bLoop)
	{
		SkeletonAnimation component = jlxzObj.GetComponent<SkeletonAnimation>();
		component.Initialize(overwrite: true);
		component.loop = bLoop;
		component.state.SetAnimation(0, sAniName, bLoop);
		component.state.End += delegate
		{
		};
	}

	public bool CheckDown()
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
		IEnumerator enumerator2 = BubbleSpawner.Instance.FallParent.transform.GetEnumerator();
		try
		{
			if (enumerator2.MoveNext())
			{
				Transform transform2 = (Transform)enumerator2.Current;
				num++;
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
		if (num > 0)
		{
			return false;
		}
		return true;
	}
}
