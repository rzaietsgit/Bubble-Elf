using DG.Tweening;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using Thinksquirrel.CShake;
using UnityEngine;

public class BubbleObj : MonoBehaviour
{
	public BUBBLEDATA mBubbleData;

	public GameObject render;

	public GameObject fx_skill_fire;

	public GameObject fx_skill_jiguang;

	public GameObject fx_fire_remove;

	public GameObject fx_fire_removejiguang;

	public GameObject fx_skill_zap;

	public GameObject fx_zap_remove;

	public GameObject fx_skill_ice;

	public GameObject fx_ice_remove;

	public GameObject fx_skill_wood;

	public GameObject fx_wood_remove;

	public GameObject fx_bianse;

	public GameObject fx_prop4;

	public GameObject fx_prop5;

	public GameObject fx_prop6;

	public GameObject fx_mfjl_remove;

	public GameObject fx_bing_remove;

	public GameObject fx_jingxiang_remove;

	public GameObject obj_fx_jingxiang_remove;

	public GameObject fx_kongqi_remove;

	public GameObject fx_shitou_remove;

	public GameObject fx_guadian_remove;

	public GameObject fx_muqiu_remove;

	public GameObject fx_3skill_ball;

	public GameObject fx_3skill_remove;

	public GameObject fx_3skill_bg;

	public GameObject fx_3skill_remove2;

	public GameObject fx_4skill_remove;

	public GameObject fx_4skill_ball;

	public GameObject fx_4skill_bg;

	public GameObject fx_4skill_remove2;

	public GameObject fx_jingxian;

	public GameObject fx_tieci;

	public GameObject fx_bigElf_remove;

	public GameObject fx_elfin_remove;

	public GameObject fx_bomb_remove;

	public GameObject fx_end_pop_boom;

	public GameObject fx_bigElfObj;

	public GameObject fx_elfinObj;

	public GameObject fx_bigElf;

	public GameObject fx_elfin;

	public GameObject fx_BossHuangChong;

	public GameObject snail_bglight;

	public GameObject fx_cloud;

	public GameObject fx_cloud_obj;

	public GameObject fx_suo;

	public GameObject fx_suo_1_remove;

	public GameObject fx_suo_2_remove;

	public GameObject fx_suo_obj;

	public bool isSuoZhe = true;

	public GameObject fx_DianDian_obj;

	public GameObject fx_dost;

	public GameObject fx_dost_remove;

	public GameObject fx_dost_fly;

	public GameObject fx_lian;

	public GameObject fx_lian_obj;

	public GameObject fx_tianchong;

	public GameObject fx_tianchong_obj;

	public List<int> ganList;

	public int iTianChongIndex;

	public GameObject fx_ganran;

	public GameObject fx_ganran_obj;

	public bool fxganranIsRemove;

	public GameObject fx_ganran_remove;

	public GameObject fx_zhizhu;

	public GameObject fx_zhizhu_obj;

	public GameObject fx_zhizhu_play;

	public GameObject fx_zhizhu_play_obj;

	public GameObject fx_ganran_dye_obj;

	public GameObject fx_ganran_dye;

	public GameObject[] fx_ranse_remove;

	public GameObject[] fx_ranse_ball;

	private GameObject fx_mfjz;

	public GameObject fx_mfjz_obj;

	public GameObject fx_mofajian_remove;

	private GameObject fx_mfjl;

	public GameObject fx_mfjl_obj;

	private GameObject fx_zd;

	public GameObject fx_zd_obj;

	public GameObject fx_cloud_remove;

	public bool fxCloudIsRemove;

	public bool isRemoveBy2he;

	public bool isRemoveBy3he;

	public bool isRemoveBy4he;

	public bool isRemoveByShangdian;

	public bool isRemoveByMuQiu;

	public bool isRemoveByJingling;

	public bool isRemoveByBing;

	public bool isRemoveByHuo;

	public bool isRemoveByTiechi;

	public bool isRemoveByzhadan;

	public bool isRemoveBybbb;

	public bool isRemoveByJian;

	public bool skillhuo;

	public bool skillmu;

	public bool skilldian;

	public bool skillbing;

	public bool skillbbb;

	public bool skillzhadan;

	public bool skilljingling;

	public bool skilljiang;

	public int mType;

	public bool isReadyRemove;

	public bool isRemove;

	public bool isFall;

	public bool isCheck;

	public GameObject fx_pop;

	private GameObject fx_skillhuo;

	private GameObject fx_skilljiguang;

	private GameObject fx_skillmu;

	private GameObject fx_skilldian;

	private GameObject fx_skillbing;

	private GameObject fx_skilljingxiang;

	public GameObject fx_skill_3;

	private GameObject fx_skill_3skill;

	public GameObject fx_skill_4;

	private GameObject fx_skill_4skill;

	public GameObject fx_suiji;

	public int iRemoveIndex;

	public float skill3heTime = 1.3f;

	public float skill3hebgTime = 2.67f;

	public float skill4heTime = 1.3f;

	public bool isAddScore = true;

	public bool isRemoveByDBHUO;

	public int iTianChong = 1;

	public bool isTianChong;

	public bool b_BossHuangChong;

	public GameObject fx_mofa_light;

	private GameObject G_fx_mofa_light;

	public GameObject fx_mofa_elf_select;

	public bool BbossInit;

	public GameObject DownBubble_energy_glow;

	public GameObject DownBubble_energy_glowObj;

	public GameObject DownBubble_energy_glowFly;

	public GameObject[] DownBubble_energy_glowFlyObj;

	public GameObject SubMenObj;

	public GameObject SubMen;

	public bool bflyobjstate;

	public bool bflyobjstatePeople;

	public GameObject fx_flyobj;

	public GameObject fx_flyobj_;

	public bool bMenOpen;

	private int iDownType;

	public bool bwin100fen;

	public bool bmuBoss5 = true;

	private GameObject fx_mofa_elf_selectObj;

	public void InitDataFly(BUBBLEDATA data)
	{
		int num = 10;
		if (data.i == 0)
		{
			render.GetComponent<SpriteRenderer>().sprite = BubbleSpawner.Instance.BubbleSprite[num];
		}
		render.SetActive(value: false);
		bflyobjstate = false;
		bflyobjstatePeople = true;
		mBubbleData = data;
		fx_flyobj_ = UnityEngine.Object.Instantiate(fx_flyobj, base.transform.position, base.transform.rotation);
		fx_flyobj_.transform.parent = base.gameObject.transform;
	}

	public void InitData(BUBBLEDATA data, bool isReadyBubble = false, bool bBossHuang = false)
	{
		if (isReadyBubble)
		{
			bflyobjstate = false;
		}
		else
		{
			bflyobjstate = true;
		}
		BbossInit = false;
		if (bBossHuang)
		{
			BbossInit = bBossHuang;
		}
		skillzhadan = false;
		skillhuo = false;
		skillbbb = false;
		skillmu = false;
		skilldian = false;
		skillbing = false;
		iRemoveIndex = 0;
		fx_skillhuo = null;
		fx_skillmu = null;
		fx_skilldian = null;
		fx_skillbing = null;
		fx_skilljingxiang = null;
		isRemove = false;
		isFall = false;
		isCheck = false;
		isReadyRemove = false;
		isRemoveByShangdian = false;
		isRemoveByMuQiu = false;
		skilljiang = false;
		mBubbleData = data;
		obj_fx_jingxiang_remove = UnityEngine.Object.Instantiate(fx_jingxiang_remove, base.transform.position, base.transform.rotation);
		Transform transform = obj_fx_jingxiang_remove.transform;
		Vector3 position = base.transform.position;
		float x = position.x;
		Vector3 position2 = base.transform.position;
		float y = position2.y;
		Vector3 position3 = base.gameObject.transform.position;
		transform.position = new Vector3(x, y, position3.z - 10f);
		obj_fx_jingxiang_remove.transform.parent = base.gameObject.transform.parent;
		obj_fx_jingxiang_remove.SetActive(value: false);
		mType = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["type"]);
		int num = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["img"]) - 1;
		if (data.i == 0)
		{
			ShowElfCenter(mBubbleData.key);
			if (mBubbleData.key == "BBB")
			{
				UnityEngine.Object.Destroy(render);
			}
			else
			{
				render.GetComponent<SpriteRenderer>().sprite = BubbleSpawner.Instance.BubbleSprite[num];
			}
			int num2 = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["attributes"]);
			switch (num2)
			{
			case 10:
			{
				GameObject original4 = fx_skill_fire;
				Vector3 position13 = base.transform.position;
				float x5 = position13.x;
				Vector3 position14 = base.transform.position;
				float y5 = position14.y;
				Vector3 position15 = base.transform.position;
				fx_skillhuo = UnityEngine.Object.Instantiate(original4, new Vector3(x5, y5, -3f + position15.z), base.transform.rotation);
				fx_skillhuo.transform.parent = base.gameObject.transform;
				break;
			}
			case 11:
			{
				GameObject original3 = fx_skill_zap;
				Vector3 position10 = base.transform.position;
				float x4 = position10.x;
				Vector3 position11 = base.transform.position;
				float y4 = position11.y;
				Vector3 position12 = base.transform.position;
				fx_skilldian = UnityEngine.Object.Instantiate(original3, new Vector3(x4, y4, -5f + position12.z), base.transform.rotation);
				fx_skilldian.transform.parent = base.gameObject.transform;
				break;
			}
			case 12:
			{
				GameObject original2 = fx_skill_ice;
				Vector3 position7 = base.transform.position;
				float x3 = position7.x;
				Vector3 position8 = base.transform.position;
				float y3 = position8.y;
				Vector3 position9 = base.transform.position;
				fx_skillbing = UnityEngine.Object.Instantiate(original2, new Vector3(x3, y3, -2f + position9.z), base.transform.rotation);
				fx_skillbing.transform.parent = base.gameObject.transform;
				break;
			}
			case 13:
			{
				GameObject original = fx_skill_wood;
				Vector3 position4 = base.transform.position;
				float x2 = position4.x;
				Vector3 position5 = base.transform.position;
				float y2 = position5.y;
				Vector3 position6 = base.transform.position;
				fx_skillmu = UnityEngine.Object.Instantiate(original, new Vector3(x2, y2, -4f + position6.z), base.transform.rotation);
				fx_skillmu.transform.parent = base.gameObject.transform;
				break;
			}
			case 2:
				fx_skilljingxiang = UnityEngine.Object.Instantiate(fx_jingxian, base.transform.position, base.transform.rotation);
				fx_skilljingxiang.transform.parent = base.gameObject.transform;
				break;
			case 20:
				fx_zd = UnityEngine.Object.Instantiate(fx_zd_obj, base.transform.position, base.transform.rotation);
				fx_zd.transform.parent = base.gameObject.transform;
				skillzhadan = true;
				break;
			case 14:
				fx_mfjz = UnityEngine.Object.Instantiate(fx_mfjz_obj, base.transform.position, base.transform.rotation);
				fx_mfjz.transform.parent = base.gameObject.transform;
				skilljiang = true;
				break;
			case 15:
				fx_mfjl = UnityEngine.Object.Instantiate(fx_mfjl_obj, base.transform.position, base.transform.rotation);
				fx_mfjl.transform.parent = base.gameObject.transform;
				skilljingling = true;
				break;
			case 9:
				fx_ganran_dye_obj = UnityEngine.Object.Instantiate(fx_ganran_dye, base.transform.position, base.transform.rotation);
				fx_ganran_dye_obj.transform.parent = base.gameObject.transform;
				break;
			case 30:
				fx_zhizhu_obj = UnityEngine.Object.Instantiate(fx_zhizhu, base.transform.position, base.transform.rotation);
				fx_zhizhu_obj.transform.parent = base.gameObject.transform;
				SwitchZhiZhu(fx_zhizhu_obj, "static");
				break;
			}
			switch (num2)
			{
			case 32:
			{
				GameObject original5 = fx_skill_jiguang;
				Vector3 position16 = base.transform.position;
				float x6 = position16.x;
				Vector3 position17 = base.transform.position;
				float y6 = position17.y;
				Vector3 position18 = base.transform.position;
				fx_skilljiguang = UnityEngine.Object.Instantiate(original5, new Vector3(x6, y6, -3f + position18.z), base.transform.rotation);
				fx_skilljiguang.transform.parent = base.gameObject.transform;
				break;
			}
			case 31:
				isTianChong = true;
				fx_tianchong_obj = UnityEngine.Object.Instantiate(fx_tianchong, base.transform.position, base.transform.rotation);
				fx_tianchong_obj.transform.parent = base.gameObject.transform;
				ganList = new List<int>();
				if (Singleton<LevelManager>.Instance.G1 == 1)
				{
					ganList.Add(1);
				}
				if (Singleton<LevelManager>.Instance.G2 == 1)
				{
					ganList.Add(2);
				}
				if (Singleton<LevelManager>.Instance.G4 == 1)
				{
					ganList.Add(3);
				}
				if (Singleton<LevelManager>.Instance.G5 == 1)
				{
					ganList.Add(4);
				}
				tianchongRandom();
				break;
			}
		}
		else
		{
			render.GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	public void tianchongRandom()
	{
		if (isTianChong)
		{
			int index = 0;
			int num = UnityEngine.Random.Range(0, ganList.Count * 100);
			if (num < 100)
			{
				index = 0;
			}
			else if (num < 200)
			{
				index = 1;
			}
			else if (num < 300)
			{
				index = 2;
			}
			else if (num < 400)
			{
				index = 3;
			}
			iTianChongIndex = ganList[index];
			if (ganList.Count > 0)
			{
				PlayFX(fx_bianse, 1f, isRemove: false);
				SkeletonAnimation component = fx_tianchong_obj.GetComponent<SkeletonAnimation>();
				component.skeleton.SetSkin(ganList[index] + string.Empty);
			}
		}
	}

	public void SwitchZhiZhu(GameObject obj, string AniName)
	{
		SkeletonAnimation component = obj.GetComponent<SkeletonAnimation>();
		component.Initialize(overwrite: true);
		component.loop = false;
		component.state.SetAnimation(0, AniName, loop: false);
		component.state.End += delegate
		{
			int num = UnityEngine.Random.Range(0, 100);
			if (num < 30)
			{
				SwitchZhiZhu(obj, "static");
			}
			else if (num < 60)
			{
				SwitchZhiZhu(obj, "pose1");
			}
			else
			{
				SwitchZhiZhu(obj, "pose2");
			}
		};
	}

	public void PlayZhiZhuSi(BubbleObj obj)
	{
		GameObject b = UnityEngine.Object.Instantiate(fx_zhizhu_play, base.transform.position, base.transform.rotation);
		b.transform.parent = base.gameObject.transform.parent;
		b.transform.localPosition = base.gameObject.transform.localPosition;
		Vector3 localPosition = obj.gameObject.transform.localPosition;
		float num = Vector2.Distance(base.gameObject.transform.localPosition, obj.gameObject.transform.localPosition);
		float num2 = 0f;
		num2 = num / 5f;
		b.transform.DOMove(localPosition, num2).OnComplete(delegate
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.play("b_spider_web_attach");
			}
			obj.AddTop();
			UnityEngine.Object.DestroyObject(b);
		});
	}

	public void InitTop(BUBBLEDATA data)
	{
		if (data.key == "S")
		{
			GameObject original = fx_cloud;
			Vector3 position = base.transform.position;
			float x = position.x;
			Vector3 position2 = base.transform.position;
			float y = position2.y;
			Vector3 position3 = base.transform.position;
			fx_cloud_obj = UnityEngine.Object.Instantiate(original, new Vector3(x, y, -3f + position3.z), base.transform.rotation);
			fx_cloud_obj.transform.parent = base.gameObject.transform;
			fx_cloud_obj.transform.localPosition = new Vector3(0f, 0f, -1f);
		}
		else if (data.key == "L")
		{
			GameObject original2 = fx_lian;
			Vector3 position4 = base.transform.position;
			float x2 = position4.x;
			Vector3 position5 = base.transform.position;
			float y2 = position5.y;
			Vector3 position6 = base.transform.position;
			fx_lian_obj = UnityEngine.Object.Instantiate(original2, new Vector3(x2, y2, -3f + position6.z), base.transform.rotation);
			fx_lian_obj.transform.parent = base.gameObject.transform;
			fx_lian_obj.transform.localPosition = new Vector3(0f, 0f, -1f);
		}
		else if (data.key == "Suo")
		{
			GameObject original3 = fx_suo;
			Vector3 position7 = base.transform.position;
			float x3 = position7.x;
			Vector3 position8 = base.transform.position;
			float y3 = position8.y;
			Vector3 position9 = base.transform.position;
			fx_suo_obj = UnityEngine.Object.Instantiate(original3, new Vector3(x3, y3, -3f + position9.z), base.transform.rotation);
			fx_suo_obj.transform.parent = base.gameObject.transform;
			fx_suo_obj.transform.localPosition = new Vector3(0f, 0f, -1f);
		}
		else if (data.key == "YunDian")
		{
			GameObject original4 = fx_dost;
			Vector3 position10 = base.transform.position;
			float x4 = position10.x;
			Vector3 position11 = base.transform.position;
			float y4 = position11.y;
			Vector3 position12 = base.transform.position;
			fx_DianDian_obj = UnityEngine.Object.Instantiate(original4, new Vector3(x4, y4, -3f + position12.z), base.transform.rotation);
			fx_DianDian_obj.transform.parent = base.gameObject.transform;
			fx_DianDian_obj.transform.localPosition = new Vector3(0f, 0f, -1f);
			Switchdost(fx_DianDian_obj);
		}
		else if (data.key == "Men" || data.key == "Men1")
		{
			GameObject subMen = SubMen;
			Vector3 position13 = base.transform.position;
			float x5 = position13.x;
			Vector3 position14 = base.transform.position;
			float y5 = position14.y;
			Vector3 position15 = base.transform.position;
			SubMenObj = UnityEngine.Object.Instantiate(subMen, new Vector3(x5, y5, -3f + position15.z), base.transform.rotation);
			SubMenObj.transform.parent = base.gameObject.transform;
			SubMenObj.transform.localPosition = new Vector3(0f, 0f, -1f);
			if (data.key == "Men1")
			{
				SubDoor(bopen: false, binit: true);
			}
			else
			{
				SubDoor(bopen: true, binit: true);
			}
		}
	}

	public void SubDoor(bool bopen = false, bool binit = false)
	{
		if (!SubMenObj)
		{
			return;
		}
		SkeletonAnimation component = SubMenObj.GetComponent<SkeletonAnimation>();
		if (binit)
		{
			if (bopen)
			{
				component.state.AddAnimation(0, "open_static", loop: false, 0f);
				bMenOpen = false;
			}
			else
			{
				component.state.AddAnimation(0, "close_static", loop: false, 0f);
				bMenOpen = true;
			}
		}
		else if (bopen)
		{
			component.state.AddAnimation(0, "open", loop: false, 0f);
			bMenOpen = true;
		}
		else if (bMenOpen)
		{
			component.state.AddAnimation(0, "close", loop: false, 0f);
			bMenOpen = false;
		}
		else
		{
			component.state.AddAnimation(0, "open", loop: false, 0f);
			bMenOpen = true;
		}
	}

	public void InitDown(BUBBLEDATA data)
	{
		GameObject downBubble_energy_glow = DownBubble_energy_glow;
		Vector3 position = base.transform.position;
		float x = position.x;
		Vector3 position2 = base.transform.position;
		float y = position2.y;
		Vector3 position3 = base.transform.position;
		DownBubble_energy_glowObj = UnityEngine.Object.Instantiate(downBubble_energy_glow, new Vector3(x, y, -3f + position3.z), base.transform.rotation);
		DownBubble_energy_glowObj.transform.parent = base.gameObject.transform;
		DownBubble_energy_glowObj.transform.localPosition = new Vector3(0f, 0f, -1f);
		GameObject gameObject = DownBubble_energy_glowObj.transform.Find("fx_energy").gameObject;
		SkeletonAnimation component = gameObject.GetComponent<SkeletonAnimation>();
		if (data.key == "MF1")
		{
			iDownType = 1;
			component.Skeleton.SetSkin("skill1");
		}
		else if (data.key == "MF2")
		{
			iDownType = 2;
			component.Skeleton.SetSkin("skill2");
		}
		else if (data.key == "MF3")
		{
			iDownType = 3;
			component.Skeleton.SetSkin("skill3");
		}
		else if (data.key == "MF4")
		{
			iDownType = 4;
			component.Skeleton.SetSkin("skill4");
		}
	}

	public void AddTop()
	{
		if (!fx_ganran_obj)
		{
			fxganranIsRemove = false;
			GameObject original = fx_ganran;
			Vector3 position = base.transform.position;
			float x = position.x;
			Vector3 position2 = base.transform.position;
			float y = position2.y;
			Vector3 position3 = base.transform.position;
			fx_ganran_obj = UnityEngine.Object.Instantiate(original, new Vector3(x, y, -3f + position3.z), base.transform.rotation);
			fx_ganran_obj.transform.parent = base.gameObject.transform;
			fx_ganran_obj.transform.localPosition = new Vector3(0f, 0f, -1f);
		}
	}

	public void PlayRanse(int type)
	{
		if (type <= 5)
		{
			PlayFX(fx_ranse_ball[type - 1], 1f, isRemove: false);
		}
	}

	public void PlayRanseRemove()
	{
		if (mType <= 5)
		{
			PlayFX(fx_ranse_remove[mType - 1], 1f, isRemove: false);
		}
	}

	public void Ranse(string changeKey = "")
	{
		if (!(changeKey == "A") && !(changeKey == "B") && !(changeKey == "C") && !(changeKey == "D") && !(changeKey == "E"))
		{
			changeKey = BubbleSpawner.Instance.GetBubbleRandomKey();
		}
		ChangeTo(changeKey);
		List<Vector2> around = BubbleSpawner.Instance.GetAround(mBubbleData.row, mBubbleData.col);
		List<Vector2> list = new List<Vector2>();
		BubbleSpawner.Instance.GetAnimAllList(list, around, mBubbleData.row, mBubbleData.col);
		for (int i = 0; i < around.Count; i++)
		{
			GameObject[,] bubbleArray = BubbleSpawner.Instance.BubbleArray;
			Vector2 vector = around[i];
			int num = (int)vector.x;
			Vector2 vector2 = around[i];
			GameObject _obj = bubbleArray[num, (int)vector2.y];
			if ((bool)_obj && !_obj.GetComponent<BubbleObj>().isRemove && _obj.GetComponent<BubbleObj>().mType <= 5)
			{
				int num2 = int.Parse(Singleton<DataManager>.Instance.dBubble[_obj.GetComponent<BubbleObj>().mBubbleData.key]["img"]);
				if (num2 <= 5)
				{
					_obj.transform.DOScale(1f, 0f).SetDelay(0.2f).OnComplete(delegate
					{
						_obj.GetComponent<BubbleObj>().PlayRanse(GetComponent<BubbleObj>().mType);
					});
					_obj.transform.DOScale(1f, 0f).SetDelay(0.7f).OnComplete(delegate
					{
						_obj.GetComponent<BubbleObj>().PlayRemoveCloud();
						_obj.GetComponent<BubbleObj>().ChangeTo(changeKey);
					});
				}
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			GameObject[,] bubbleArray2 = BubbleSpawner.Instance.BubbleArray;
			Vector2 vector3 = list[j];
			int num3 = (int)vector3.x;
			Vector2 vector4 = list[j];
			GameObject _obj2 = bubbleArray2[num3, (int)vector4.y];
			if (!_obj2 || _obj2.GetComponent<BubbleObj>().isRemove || _obj2.GetComponent<BubbleObj>().mType > 5)
			{
				continue;
			}
			if (mBubbleData.row % 2 != 0)
			{
				Vector2 vector5 = list[j];
				if ((int)vector5.y == mBubbleData.col)
				{
					Vector2 vector6 = list[j];
					if ((int)vector6.x == mBubbleData.row + 2)
					{
						continue;
					}
				}
				Vector2 vector7 = list[j];
				if ((int)vector7.y == mBubbleData.col)
				{
					Vector2 vector8 = list[j];
					if ((int)vector8.x == mBubbleData.row - 2)
					{
						continue;
					}
				}
				Vector2 vector9 = list[j];
				if ((int)vector9.x == mBubbleData.row + 1)
				{
					Vector2 vector10 = list[j];
					if ((int)vector10.y == mBubbleData.col - 1)
					{
						continue;
					}
				}
				Vector2 vector11 = list[j];
				if ((int)vector11.x == mBubbleData.row + 1)
				{
					Vector2 vector12 = list[j];
					if ((int)vector12.y == mBubbleData.col + 2)
					{
						continue;
					}
				}
				Vector2 vector13 = list[j];
				if ((int)vector13.x == mBubbleData.row - 1)
				{
					Vector2 vector14 = list[j];
					if ((int)vector14.y == mBubbleData.col - 1)
					{
						continue;
					}
				}
				Vector2 vector15 = list[j];
				if ((int)vector15.x == mBubbleData.row - 1)
				{
					Vector2 vector16 = list[j];
					if ((int)vector16.y == mBubbleData.col + 2)
					{
						continue;
					}
				}
			}
			else
			{
				Vector2 vector17 = list[j];
				if ((int)vector17.y == mBubbleData.col)
				{
					Vector2 vector18 = list[j];
					if ((int)vector18.x == mBubbleData.row + 2)
					{
						continue;
					}
				}
				Vector2 vector19 = list[j];
				if ((int)vector19.y == mBubbleData.col)
				{
					Vector2 vector20 = list[j];
					if ((int)vector20.x == mBubbleData.row - 2)
					{
						continue;
					}
				}
				Vector2 vector21 = list[j];
				if ((int)vector21.x == mBubbleData.row + 1)
				{
					Vector2 vector22 = list[j];
					if ((int)vector22.y == mBubbleData.col - 2)
					{
						continue;
					}
				}
				Vector2 vector23 = list[j];
				if ((int)vector23.x == mBubbleData.row + 1)
				{
					Vector2 vector24 = list[j];
					if ((int)vector24.y == mBubbleData.col + 1)
					{
						continue;
					}
				}
				Vector2 vector25 = list[j];
				if ((int)vector25.x == mBubbleData.row - 1)
				{
					Vector2 vector26 = list[j];
					if ((int)vector26.y == mBubbleData.col - 2)
					{
						continue;
					}
				}
				Vector2 vector27 = list[j];
				if ((int)vector27.x == mBubbleData.row - 1)
				{
					Vector2 vector28 = list[j];
					if ((int)vector28.y == mBubbleData.col + 1)
					{
						continue;
					}
				}
			}
			int num4 = int.Parse(Singleton<DataManager>.Instance.dBubble[_obj2.GetComponent<BubbleObj>().mBubbleData.key]["img"]);
			if (num4 <= 5)
			{
				_obj2.transform.DOScale(1f, 0f).SetDelay(0.32f).OnComplete(delegate
				{
					_obj2.GetComponent<BubbleObj>().PlayRanse(GetComponent<BubbleObj>().mType);
				});
				_obj2.transform.DOScale(1f, 0f).SetDelay(0.72f).OnComplete(delegate
				{
					_obj2.GetComponent<BubbleObj>().PlayRemoveCloud();
					_obj2.GetComponent<BubbleObj>().ChangeTo(changeKey);
				});
			}
		}
		PlayRanseRemove();
	}

	public void ShowElfCenter(string key)
	{
		switch (int.Parse(Singleton<DataManager>.Instance.dBubble[key]["belf"]))
		{
		case 1:
			fx_elfinObj = UnityEngine.Object.Instantiate(fx_elfin);
			fx_elfinObj.transform.parent = base.transform.transform;
			fx_elfinObj.transform.localPosition = new Vector3(0f, 0f, -10f);
			SwitchAni(fx_elfinObj, "elf_shake");
			break;
		case 2:
			fx_bigElfObj = UnityEngine.Object.Instantiate(fx_bigElf, base.transform.position, base.transform.rotation);
			fx_bigElfObj.transform.parent = base.transform.transform;
			fx_bigElfObj.transform.localPosition = new Vector3(0f, -0.2f, 0f);
			SwitchAniMax(fx_bigElfObj, "elf_shake");
			break;
		case 3:
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(fx_BossHuangChong, base.transform.position, base.transform.rotation);
			gameObject.transform.parent = base.transform.transform;
			gameObject.transform.localPosition = new Vector3(-0.071f, -0.491f, 0f);
			GameObject gameObject2 = UnityEngine.Object.Instantiate(snail_bglight, base.transform.position, base.transform.rotation);
			gameObject2.transform.parent = base.transform.transform;
			gameObject2.transform.localPosition = new Vector3(0f, 0f, 0f);
			BubbleSpawner.Instance.Bossobj = gameObject;
			SwitchAniMax(BubbleSpawner.Instance.Bossobj, "idle", bboss: true);
			b_BossHuangChong = true;
			break;
		}
		}
	}

	public void BossSkill3()
	{
		SwitchAniMax(BubbleSpawner.Instance.Bossobj, "skill", bboss: true);
	}

	public void Boss3Change()
	{
		int num = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["type"]);
		if (num >= 1 && num <= 5)
		{
			string text = "A_B_C_D_E";
			string[] array = text.Split('_');
			int num2 = UnityEngine.Random.Range(0, 5);
			mBubbleData.key = array[num2];
			mType = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["type"]);
			int num3 = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["img"]) - 1;
			render.GetComponent<SpriteRenderer>().sprite = BubbleSpawner.Instance.BubbleSprite[num3];
		}
		else if (mBubbleData.key == "F" || mBubbleData.key == "G" || mBubbleData.key == "H" || mBubbleData.key == "I" || mBubbleData.key == "J" || mBubbleData.key == "K")
		{
			string text2 = "F_G_H_I_J_K";
			string[] array2 = text2.Split('_');
			int num4 = UnityEngine.Random.Range(0, 6);
			string text3 = array2[num4];
			while (text3 == mBubbleData.key)
			{
				num4 = UnityEngine.Random.Range(0, 6);
				text3 = array2[num4];
			}
			ChangeTo(text3, bBossChange: true);
		}
		else if (mBubbleData.key == "O" || mBubbleData.key == "L" || mBubbleData.key == "M" || mBubbleData.key == "N")
		{
			string text4 = "O_L_M_N";
			string[] array3 = text4.Split('_');
			int num5 = UnityEngine.Random.Range(0, 4);
			string text5 = array3[num5];
			while (text5 == mBubbleData.key)
			{
				num5 = UnityEngine.Random.Range(0, 4);
				text5 = array3[num5];
			}
			ChangeTo(text5, bBossChange: true);
		}
	}

	public void SwitchAniMax(GameObject obj, string AniName, bool bboss = false)
	{
		if (obj == null || (!bboss && Singleton<LevelManager>.Instance.bBossHuang))
		{
			return;
		}
		if (Singleton<LevelManager>.Instance.bBossHuang)
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
		else
		{
			SkeletonAnimation component2 = obj.GetComponent<SkeletonAnimation>();
			component2.Initialize(overwrite: true);
			component2.loop = false;
			component2.state.SetAnimation(0, AniName, loop: false);
			component2.state.End += delegate
			{
				int num = UnityEngine.Random.Range(0, 100);
				if (num < 50)
				{
					SwitchAniMax(obj, "elf_shake");
				}
				else
				{
					SwitchAniMax(obj, "elf_shake");
				}
			};
		}
	}

	public void Switchdost(GameObject obj)
	{
		SkeletonAnimation component = obj.GetComponent<SkeletonAnimation>();
		component.Initialize(overwrite: true);
		component.loop = false;
		int num = UnityEngine.Random.Range(1, 5);
		component.state.SetAnimation(0, "anim" + num, loop: true);
	}

	public void SwitchAni(GameObject obj, string AniName)
	{
		SkeletonAnimation skelet = obj.GetComponent<SkeletonAnimation>();
		skelet.Initialize(overwrite: true);
		skelet.loop = false;
		skelet.state.SetAnimation(0, AniName, loop: false);
		skelet.state.End += delegate
		{
			if (skelet.AnimationName == "elf_S1toS2")
			{
				SwitchAni(obj, "elf_shake");
			}
			else if (skelet.AnimationName == "elf_S1toS2")
			{
				SwitchAni(obj, "elf_shake");
			}
			else if (skelet.AnimationName == "elf_S1toS2")
			{
				SwitchAni(obj, "elf_shake");
			}
			else if (skelet.AnimationName == "elf_S1toS2")
			{
				SwitchAni(obj, "elf_shake");
			}
			else
			{
				int num = UnityEngine.Random.Range(0, 100);
				if (num < 50)
				{
					if (skelet.AnimationName == "elf_shake")
					{
						SwitchAni(obj, "elf_S1toS2");
					}
					else
					{
						SwitchAni(obj, "elf_shake");
					}
				}
				else if (num < 90)
				{
					if (skelet.AnimationName == "elf_shake")
					{
						SwitchAni(obj, "elf_S1toS2");
					}
					else if (skelet.AnimationName == "elf_shake")
					{
						SwitchAni(obj, "elf_S1toS2");
					}
					else
					{
						SwitchAni(obj, "elf_shake");
					}
				}
				else if (skelet.AnimationName == "elf_shake")
				{
					SwitchAni(obj, "elf_S1toS2");
				}
				else
				{
					SwitchAni(obj, "elf_shake");
				}
			}
		};
	}

	public void ElfPlayMp3(string sAniname)
	{
	}

	public void createfx()
	{
		if (skillbing && fx_skillbing == null)
		{
			GameObject original = fx_skill_ice;
			Vector3 position = base.transform.position;
			float x = position.x;
			Vector3 position2 = base.transform.position;
			float y = position2.y;
			Vector3 position3 = base.transform.position;
			fx_skillbing = UnityEngine.Object.Instantiate(original, new Vector3(x, y, -2f + position3.z), base.transform.rotation);
			fx_skillbing.transform.parent = base.gameObject.transform;
		}
		if (skillmu && fx_skillmu == null)
		{
			GameObject original2 = fx_skill_wood;
			Vector3 position4 = base.transform.position;
			float x2 = position4.x;
			Vector3 position5 = base.transform.position;
			float y2 = position5.y;
			Vector3 position6 = base.transform.position;
			fx_skillmu = UnityEngine.Object.Instantiate(original2, new Vector3(x2, y2, -4f + position6.z), base.transform.rotation);
			fx_skillmu.transform.parent = base.gameObject.transform;
		}
		if (skillbbb && fx_skilljiguang == null)
		{
			GameObject original3 = fx_skill_jiguang;
			Vector3 position7 = base.transform.position;
			float x3 = position7.x;
			Vector3 position8 = base.transform.position;
			float y3 = position8.y;
			Vector3 position9 = base.transform.position;
			fx_skilljiguang = UnityEngine.Object.Instantiate(original3, new Vector3(x3, y3, -3f + position9.z), base.transform.rotation);
			fx_skilljiguang.transform.parent = base.gameObject.transform;
		}
		if (skillhuo && fx_skillhuo == null)
		{
			GameObject original4 = fx_skill_fire;
			Vector3 position10 = base.transform.position;
			float x4 = position10.x;
			Vector3 position11 = base.transform.position;
			float y4 = position11.y;
			Vector3 position12 = base.transform.position;
			fx_skillhuo = UnityEngine.Object.Instantiate(original4, new Vector3(x4, y4, -3f + position12.z), base.transform.rotation);
			fx_skillhuo.transform.parent = base.gameObject.transform;
		}
		if (skilldian && fx_skilldian == null)
		{
			GameObject original5 = fx_skill_zap;
			Vector3 position13 = base.transform.position;
			float x5 = position13.x;
			Vector3 position14 = base.transform.position;
			float y5 = position14.y;
			Vector3 position15 = base.transform.position;
			fx_skilldian = UnityEngine.Object.Instantiate(original5, new Vector3(x5, y5, -5f + position15.z), base.transform.rotation);
			fx_skilldian.transform.parent = base.gameObject.transform;
		}
	}

	public void ResetPos()
	{
		base.gameObject.transform.DOKill();
		Vector3 position = BubbleSpawner.Instance.GetSquare(mBubbleData.row, mBubbleData.col).transform.position;
		float x = position.x;
		Vector3 position2 = BubbleSpawner.Instance.GetSquare(mBubbleData.row, mBubbleData.col).transform.position;
		Vector3 position3 = new Vector3(x, position2.y, -10f);
		base.gameObject.transform.position = position3;
	}

	public void SetBubble(int row, int col)
	{
		mBubbleData.row = row;
		mBubbleData.col = col;
		BubbleSpawner.Instance.BubbleArray[row, col] = base.gameObject;
	}

	public void RemoveBubble(bool isFallBubble = false, float delaytime = 0f, bool bskill = false, bool bboss3Kill = true)
	{
		if (isRemove)
		{
			return;
		}
		if (isRemoveByJian || isRemoveByTiechi || skillbing || skillhuo || skillbbb || skillmu || skilldian || isRemoveBy2he || isRemoveBy3he || isRemoveBy4he || isRemoveByShangdian || isRemoveByMuQiu || isRemoveByJingling || isRemoveByBing || isRemoveByHuo || isRemoveByzhadan)
		{
		}
		if (!PassLevel.bWin)
		{
			if (mBubbleData.key == "HBoss")
			{
				if (bskill)
				{
					GameUI.action.CutHp(5);
					SwitchAniMax(BubbleSpawner.Instance.Bossobj, "hit", bboss: true);
					GameUI.action.ShowScore(5, base.gameObject);
				}
				return;
			}
		}
		else if (mBubbleData.key == "HBoss" && (bool)SoundController.action)
		{
			SoundController.action.playNow("sfx_snails_scared");
		}
		if (mBubbleData.row == -1 && mBubbleData.col == -1)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		if (isTianChong && !isFallBubble)
		{
			PassLevel.action.playTianChong(base.gameObject, iTianChongIndex);
		}
		if ((bool)fx_lian_obj)
		{
			if ((bool)fx_mofa_elf_selectObj)
			{
				UnityEngine.Object.Destroy(fx_mofa_elf_selectObj);
			}
			UnityEngine.Object.DestroyObject(fx_lian_obj);
			PlayFX(fx_suo_2_remove, 1f, isRemove: false);
			if ((bool)SoundController.action)
			{
				SoundController.action.play("b_lock_destroy");
			}
			if (!isFallBubble && !isRemoveByJian && !isRemoveByTiechi && !skillbing && !skillhuo && !skillbbb && !skillmu && !skilldian && !isRemoveBy2he && !isRemoveBy3he && !isRemoveBy4he && !isRemoveByShangdian && !isRemoveByMuQiu && !isRemoveByJingling && !isRemoveByBing && !isRemoveByHuo && !isRemoveByzhadan)
			{
				return;
			}
		}
		if ((bool)fx_suo_obj)
		{
			UnityEngine.Object.DestroyObject(fx_suo_obj);
			PlayFX(fx_suo_1_remove, 1f, isRemove: false);
			if ((bool)fx_mofa_elf_selectObj)
			{
				UnityEngine.Object.Destroy(fx_mofa_elf_selectObj);
			}
		}
		if ((bool)fx_DianDian_obj && !isFallBubble)
		{
			UnityEngine.Object.DestroyObject(fx_DianDian_obj);
			diandian();
		}
		if ((bool)DownBubble_energy_glowObj && !isFallBubble)
		{
			UnityEngine.Object.DestroyObject(DownBubble_energy_glowObj);
			AddMofaball(iDownType);
		}
		Vector3 position = BubbleSpawner.Instance.GetSquare(mBubbleData.row, mBubbleData.col).transform.position;
		float x = position.x;
		Vector3 position2 = BubbleSpawner.Instance.GetSquare(mBubbleData.row, mBubbleData.col).transform.position;
		Vector3 position3 = new Vector3(x, position2.y, -10f);
		base.transform.position = position3;
		isRemove = true;
		int num = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["shape"]);
		if (num == 2)
		{
			if (mBubbleData.i == 0)
			{
				List<Vector2> around = BubbleSpawner.Instance.GetAround(mBubbleData.row, mBubbleData.col);
				for (int i = 0; i < around.Count; i++)
				{
					GameObject[,] bubbleArray = BubbleSpawner.Instance.BubbleArray;
					Vector2 vector = around[i];
					int num2 = (int)vector.x;
					Vector2 vector2 = around[i];
					GameObject gameObject = bubbleArray[num2, (int)vector2.y];
					if ((bool)gameObject && !gameObject.GetComponent<BubbleObj>().isRemove)
					{
						gameObject.GetComponent<BubbleObj>().RemoveBubble();
					}
				}
			}
			else
			{
				List<Vector2> around2 = BubbleSpawner.Instance.GetAround(mBubbleData.row, mBubbleData.col);
				for (int j = 0; j < around2.Count; j++)
				{
					GameObject[,] bubbleArray2 = BubbleSpawner.Instance.BubbleArray;
					Vector2 vector3 = around2[j];
					int num3 = (int)vector3.x;
					Vector2 vector4 = around2[j];
					GameObject gameObject2 = bubbleArray2[num3, (int)vector4.y];
					if ((bool)gameObject2 && !gameObject2.GetComponent<BubbleObj>().isRemove && gameObject2.GetComponent<BubbleObj>().mBubbleData.i == 0 && gameObject2.GetComponent<BubbleObj>().mBubbleData.key == mBubbleData.key)
					{
						gameObject2.GetComponent<BubbleObj>().RemoveBubble();
					}
				}
			}
		}
		base.gameObject.transform.parent = BubbleSpawner.Instance.RemoveParent.transform;
		if (isFallBubble)
		{
			if ((bool)fx_skillhuo)
			{
				UnityEngine.Object.Destroy(fx_skillhuo.gameObject);
			}
			if ((bool)fx_skilljiguang)
			{
				UnityEngine.Object.Destroy(fx_skilljiguang.gameObject);
			}
			if ((bool)fx_skillmu)
			{
				UnityEngine.Object.Destroy(fx_skillmu.gameObject);
			}
			if ((bool)fx_skilldian)
			{
				UnityEngine.Object.Destroy(fx_skilldian.gameObject);
			}
			if ((bool)fx_skillbing)
			{
				UnityEngine.Object.Destroy(fx_skillbing.gameObject);
			}
			if ((bool)fx_skilljingxiang)
			{
				UnityEngine.Object.Destroy(fx_skilljingxiang.gameObject);
			}
			if ((bool)fx_skill_3skill)
			{
				UnityEngine.Object.Destroy(fx_skill_3skill.gameObject);
			}
			if ((bool)fx_skill_4skill)
			{
				UnityEngine.Object.Destroy(fx_skill_4skill.gameObject);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
		else
		{
			BubbleSpawner.Instance.BubbleArray[mBubbleData.row, mBubbleData.col] = null;
			UnityEngine.Object.Destroy(base.gameObject.GetComponent<Rigidbody2D>());
			StartCoroutine(remove(delaytime, bboss3Kill));
		}
	}

	public void removesuo()
	{
		if ((bool)fx_suo_obj)
		{
			UnityEngine.Object.DestroyObject(fx_suo_obj);
			PlayFX(fx_suo_1_remove, 1f, isRemove: false);
		}
	}

	public void removemen()
	{
		if ((bool)SubMenObj && bMenOpen)
		{
			bMenOpen = false;
			UnityEngine.Object.DestroyObject(SubMenObj);
		}
	}

	public void AddMofaball(int iDownType_)
	{
		DDOLSingleton<CoroutineController>.Instance.StartCoroutine(IEAddMofaball(iDownType_));
	}

	public IEnumerator IEAddMofaball(int iDownType_)
	{
		GameObject downBubble_energy_glowFly = DownBubble_energy_glowFly;
		Vector3 position = base.transform.position;
		float x = position.x;
		Vector3 position2 = base.transform.position;
		float y = position2.y;
		Vector3 position3 = base.transform.position;
		GameObject TmpDownBubble_energy_glowFly = UnityEngine.Object.Instantiate(downBubble_energy_glowFly, new Vector3(x, y, -3f + position3.z), base.transform.rotation);
		TmpDownBubble_energy_glowFly.transform.parent = base.gameObject.transform;
		TmpDownBubble_energy_glowFly.transform.localPosition = new Vector3(0f, 0f, 0f);
		GameObject fx_energyObj = TmpDownBubble_energy_glowFly.transform.Find("fx_energy").gameObject;
		SkeletonAnimation skelet = fx_energyObj.GetComponent<SkeletonAnimation>();
		skelet.state.SetAnimation(0, "remover", loop: false);
		UnityEngine.Object.Destroy(TmpDownBubble_energy_glowFly, 2f);
		GameObject original = DownBubble_energy_glowFlyObj[iDownType_ - 1];
		Vector3 position4 = base.transform.position;
		float x2 = position4.x;
		Vector3 position5 = base.transform.position;
		float y2 = position5.y;
		Vector3 position6 = base.transform.position;
		GameObject TmepDownBubble_energy_glowObj = UnityEngine.Object.Instantiate(original, new Vector3(x2, y2, -3f + position6.z), base.transform.rotation);
		TmepDownBubble_energy_glowObj.transform.parent = base.gameObject.transform;
		TmepDownBubble_energy_glowObj.transform.localPosition = new Vector3(0f, 0f, -1f);
		TmepDownBubble_energy_glowObj.SetActive(value: false);
		if (iDownType_ == 1)
		{
			TmepDownBubble_energy_glowObj.transform.parent = GameGuide.Instance.MT1.transform;
			TmpDownBubble_energy_glowFly.transform.parent = GameGuide.Instance.MT1.transform;
			skelet.Skeleton.SetSkin("skill1");
		}
		else if (iDownType_ == 2)
		{
			TmepDownBubble_energy_glowObj.transform.parent = GameGuide.Instance.MT2.transform;
			TmpDownBubble_energy_glowFly.transform.parent = GameGuide.Instance.MT2.transform;
			skelet.Skeleton.SetSkin("skill2");
		}
		else if (iDownType_ == 3)
		{
			TmepDownBubble_energy_glowObj.transform.parent = GameGuide.Instance.MT4.transform;
			TmpDownBubble_energy_glowFly.transform.parent = GameGuide.Instance.MT4.transform;
			skelet.Skeleton.SetSkin("skill3");
		}
		else if (iDownType_ == 4)
		{
			TmepDownBubble_energy_glowObj.transform.parent = GameGuide.Instance.MT5.transform;
			TmpDownBubble_energy_glowFly.transform.parent = GameGuide.Instance.MT5.transform;
			skelet.Skeleton.SetSkin("skill4");
		}
		TmepDownBubble_energy_glowObj.transform.localScale = new Vector3(1f, 1f, 1f);
		yield return new WaitForSeconds(1f);
		TmepDownBubble_energy_glowObj.SetActive(value: true);
		bool b = true;
        UnityEngine.Random.Range(0, 100);
        UnityEngine.Random.Range(10, 20);
		Vector3 localPosition = TmepDownBubble_energy_glowObj.transform.localPosition;
		if (localPosition.x < 0f)
		{
			b = false;
		}
		Vector3 TopVect = TmepDownBubble_energy_glowObj.transform.localPosition + new Vector3(1f, -2f, 0f);
		if (!b)
		{
			TopVect = TmepDownBubble_energy_glowObj.transform.localPosition + new Vector3(-1f, -2f, 0f);
		}
		ShortcutExtensions.DOLocalPath(path: new Vector3[3]
		{
			TmepDownBubble_energy_glowObj.transform.localPosition,
			TopVect,
			new Vector3(0f, 0.8f, 0f)
		}, target: TmepDownBubble_energy_glowObj.transform, duration: 1f, pathType: PathType.CatmullRom, pathMode: PathMode.TopDown2D, resolution: 20).SetEase(Ease.InSine).OnComplete(delegate
		{
			if (iDownType_ == 1)
			{
				GameGuide.Instance.MT1.GetComponent<MuTong>().AddScore();
			}
			else if (iDownType_ == 2)
			{
				GameGuide.Instance.MT2.GetComponent<MuTong>().AddScore();
			}
			else if (iDownType_ == 3)
			{
				GameGuide.Instance.MT4.GetComponent<MuTong>().AddScore();
			}
			else if (iDownType_ == 4)
			{
				GameGuide.Instance.MT5.GetComponent<MuTong>().AddScore();
			}
			closelizi(TmepDownBubble_energy_glowObj);
		});
	}

	public void closelizi(GameObject liziobj)
	{
		ParticleSystem component = liziobj.transform.GetComponent<ParticleSystem>();
		//component.emission.enabled = false;
		ParticleSystem component2 = liziobj.transform.Find("pt_xixi").GetComponent<ParticleSystem>();
		//component2.emission.enabled = false;
		UnityEngine.Object.Destroy(liziobj, 2f);
	}

	public void diandian()
	{
		DDOLSingleton<CoroutineController>.Instance.StartCoroutine(IEdiandian());
	}

	public IEnumerator IEdiandian()
	{
		yield return new WaitForSeconds(0.01f);
		int mrow = 0;
		IEnumerator enumerator = BubbleSpawner.Instance.BallParent.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				if ((bool)transform.GetComponent<BubbleObj>())
				{
					int row = transform.GetComponent<BubbleObj>().mBubbleData.row;
					if (row > mrow)
					{
						mrow = row;
					}
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
		List<GameObject> SelectObj = new List<GameObject>();
		int iRandomRR = UnityEngine.Random.Range(0, 100);
		IEnumerator enumerator2 = BubbleSpawner.Instance.BallParent.transform.GetEnumerator();
		try
		{
			while (enumerator2.MoveNext())
			{
				Transform transform2 = (Transform)enumerator2.Current;
				if ((bool)transform2.GetComponent<BubbleObj>())
				{
					int row2 = transform2.GetComponent<BubbleObj>().mBubbleData.row;
					if (mrow - 11 < row2)
					{
						string key = transform2.GetComponent<BubbleObj>().mBubbleData.key;
						int num = int.Parse(Singleton<DataManager>.Instance.dBubble[key]["attributes"]);
						if (!transform2.GetComponent<BubbleObj>().SubMenObj && !(key == "H") && !transform2.GetComponent<BubbleObj>().fx_lian_obj && num != 4 && num != 5 && num != 2 && num != 3 && num != 8 && num != 9 && num != 6)
						{
							if (iRandomRR > 50)
							{
								if (iDownType == 0 && (num == 0 || num == 1))
								{
									SelectObj.Add(transform2.gameObject);
								}
							}
							else if (iDownType != 0)
							{
								SelectObj.Add(transform2.gameObject);
							}
							else if (key == "BBB")
							{
								SelectObj.Add(transform2.gameObject);
							}
							else
							{
								switch (num)
								{
								case 100:
									SelectObj.Add(transform2.gameObject);
									break;
								case 31:
									SelectObj.Add(transform2.gameObject);
									break;
								case 6:
								case 10:
								case 11:
								case 12:
								case 13:
									SelectObj.Add(transform2.gameObject);
									break;
								default:
									if ((bool)transform2.GetComponent<BubbleObj>().DownBubble_energy_glowObj)
									{
										SelectObj.Add(transform2.gameObject);
									}
									else if ((bool)transform2.GetComponent<BubbleObj>().fx_suo_obj)
									{
										SelectObj.Add(transform2.gameObject);
									}
									else if (num == 7)
									{
										SelectObj.Add(transform2.gameObject);
									}
									break;
								}
							}
						}
					}
				}
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
		if (SelectObj.Count <= 0)
		{
			IEnumerator enumerator3 = BubbleSpawner.Instance.BallParent.transform.GetEnumerator();
			try
			{
				while (enumerator3.MoveNext())
				{
					Transform transform3 = (Transform)enumerator3.Current;
					if ((bool)transform3.GetComponent<BubbleObj>())
					{
						int row3 = transform3.GetComponent<BubbleObj>().mBubbleData.row;
						if (mrow - 11 < row3)
						{
							string key2 = transform3.GetComponent<BubbleObj>().mBubbleData.key;
							int num2 = int.Parse(Singleton<DataManager>.Instance.dBubble[key2]["attributes"]);
							if (!transform3.GetComponent<BubbleObj>().SubMenObj && !(key2 == "H") && !transform3.GetComponent<BubbleObj>().fx_lian_obj && num2 != 4 && num2 != 5 && num2 != 2 && num2 != 3 && num2 != 8 && num2 != 9 && num2 != 6)
							{
								SelectObj.Add(transform3.gameObject);
								UnityEngine.Debug.Log("add SelectObj = attributes = " + num2);
							}
						}
					}
				}
			}
			finally
			{
				IDisposable disposable4;
				if ((disposable4 = (enumerator3 as IDisposable)) != null)
				{
					disposable4.Dispose();
				}
			}
		}
		if (SelectObj.Count > 0)
		{
			GameObject original = fx_dost_fly;
			Vector3 position = base.transform.position;
			float x = position.x;
			Vector3 position2 = base.transform.position;
			float y = position2.y;
			Vector3 position3 = base.transform.position;
			GameObject fx_dost_flyObj = UnityEngine.Object.Instantiate(original, new Vector3(x, y, -3f + position3.z), base.transform.rotation);
			fx_dost_flyObj.transform.parent = base.gameObject.transform;
			fx_dost_flyObj.transform.localPosition = new Vector3(0f, 0f, -1f);
			int iRandom = UnityEngine.Random.Range(0, SelectObj.Count);
			GameObject TmepObj = SelectObj[iRandom];
			fx_dost_flyObj.transform.parent = TmepObj.transform;
			SkeletonAnimation component = fx_dost_flyObj.GetComponent<SkeletonAnimation>();
			component.state.SetAnimation(0, "add", loop: false);
			component.state.End += delegate
			{
				fx_dostFlyObjFly(fx_dost_flyObj);
				fx_dost_flyObj.transform.localScale = new Vector3(1f, 1f, 1f);
				bool bOrientation = true;
				iRandom = UnityEngine.Random.Range(0, 100);
				if (iRandom < 50)
				{
					bOrientation = false;
				}
				Vector3 centerPost = GetCenterPost(fx_dost_flyObj.transform.localPosition, new Vector3(0f, 0f, 0f), 15f, bOrientation);
				Vector3[] path = new Vector3[2]
				{
					centerPost,
					new Vector3(0f, 0f, 0f)
				};
				fx_dost_flyObj.transform.DOLocalPath(path, 0.5f, PathType.CatmullRom, PathMode.TopDown2D, 20).SetEase(Ease.InSine).OnComplete(delegate
				{
					UnityEngine.Object.Destroy(fx_dost_flyObj);
					GameObject original2 = fx_dost_remove;
					Vector3 position4 = TmepObj.transform.position;
					float x2 = position4.x;
					Vector3 position5 = TmepObj.transform.position;
					float y2 = position5.y;
					Vector3 position6 = TmepObj.transform.position;
					GameObject gameObject = UnityEngine.Object.Instantiate(original2, new Vector3(x2, y2, -3f + position6.z), TmepObj.transform.rotation);
					gameObject.transform.parent = TmepObj.transform;
					gameObject.transform.localPosition = new Vector3(0f, 0f, -1f);
					if ((bool)TmepObj.GetComponent<BubbleObj>().fx_cloud_obj)
					{
						TmepObj.GetComponent<BubbleObj>().PlayRemoveCloud();
						TmepObj.GetComponent<BubbleObj>().RemoveBubble();
						RemoveController.Instance.FallBubble(opendoor: false);
					}
					else if ((bool)TmepObj.GetComponent<BubbleObj>().fx_suo_obj)
					{
						TmepObj.GetComponent<BubbleObj>().removesuo();
					}
					else if ((bool)TmepObj.GetComponent<BubbleObj>().SubMenObj)
					{
						TmepObj.GetComponent<BubbleObj>().removemen();
					}
					else
					{
						TmepObj.GetComponent<BubbleObj>().RemoveBubble();
						RemoveController.Instance.FallBubble(opendoor: false);
					}
				});
			};
		}
		yield return new WaitForSeconds(2f);
		RemoveController.Instance.FallBubble(opendoor: false);
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

	public void fx_dostFlyObjFly(GameObject obj)
	{
		SkeletonAnimation component = obj.GetComponent<SkeletonAnimation>();
		component.Initialize(overwrite: true);
		component.loop = false;
		component.state.SetAnimation(0, "fly", loop: true);
	}

	public void BossKillBubbleOther()
	{
	}

	private IEnumerator PlayRemoveGround(float time)
	{
		yield return new WaitForSeconds(time);
		PlayRemoveCloud();
		PlayRemoveGanran();
	}

	public void CheckBoosCutHp(int iNumber)
	{
		GameObject gameObject = bGetBoss();
		if ((bool)gameObject)
		{
			GameUI.action.CutHp(iNumber);
			SwitchAniMax(BubbleSpawner.Instance.Bossobj, "hit", bboss: true);
			GameUI.action.ShowScore(iNumber, gameObject);
		}
	}

	public GameObject bGetBoss()
	{
		List<Vector2> around = BubbleSpawner.Instance.GetAround(mBubbleData.row, mBubbleData.col);
		for (int i = 0; i < around.Count; i++)
		{
			GameObject[,] bubbleArray = BubbleSpawner.Instance.BubbleArray;
			Vector2 vector = around[i];
			int num = (int)vector.x;
			Vector2 vector2 = around[i];
			GameObject gameObject = bubbleArray[num, (int)vector2.y];
			if ((bool)gameObject && gameObject.GetComponent<BubbleObj>().mBubbleData.key == "HBoss")
			{
				return gameObject;
			}
		}
		return null;
	}

	private IEnumerator remove(float time = 0f, bool bBoss3Remove = true)
	{
		yield return new WaitForSeconds(time);
		if (isRemoveByJingling)
		{
			ReturnYaping();
		}
		List<Vector2> vecAround = BubbleSpawner.Instance.GetAround(mBubbleData.row, mBubbleData.col);
		for (int i = 0; i < vecAround.Count; i++)
		{
			GameObject[,] bubbleArray = BubbleSpawner.Instance.BubbleArray;
			Vector2 vector = vecAround[i];
			int num = (int)vector.x;
			Vector2 vector2 = vecAround[i];
			GameObject gameObject = bubbleArray[num, (int)vector2.y];
			if ((bool)gameObject && !gameObject.GetComponent<BubbleObj>().isRemove && mBubbleData.key != "G")
			{
				BubbleObj component = gameObject.GetComponent<BubbleObj>();
				component.PlayRemoveCloud();
				component.PlayRemoveGanran();
			}
		}
		if (mBubbleData.key == "RS")
		{
			Ranse(string.Empty);
			yield return new WaitForSeconds(0.3f);
		}
		float removetirm = (float)iRemoveIndex * 0.05f;
		float fxtime = 0.25f;
		StartCoroutine(PlayRemoveGround(removetirm - 0.02f));
		if ((bool)PassLevel.action)
		{
			PassLevel.action.SaveKillBubble();
		}
		if ((bool)fx_skillhuo)
		{
			UnityEngine.Object.Destroy(fx_skillhuo.gameObject);
		}
		if ((bool)fx_skilljiguang)
		{
			UnityEngine.Object.Destroy(fx_skilljiguang.gameObject);
		}
		if ((bool)fx_skillmu)
		{
			UnityEngine.Object.Destroy(fx_skillmu.gameObject);
		}
		if ((bool)fx_skilldian)
		{
			UnityEngine.Object.Destroy(fx_skilldian.gameObject);
		}
		if ((bool)fx_skillbing)
		{
			UnityEngine.Object.Destroy(fx_skillbing.gameObject);
		}
		if ((bool)fx_skilljingxiang)
		{
			UnityEngine.Object.Destroy(fx_skilljingxiang.gameObject);
		}
		if ((bool)fx_skill_3skill)
		{
			UnityEngine.Object.Destroy(fx_skill_3skill.gameObject);
		}
		if ((bool)fx_skill_4skill)
		{
			UnityEngine.Object.Destroy(fx_skill_4skill.gameObject);
		}
		checkSkill();
		float multiple = 1f;
		if (BubbleSpawner.Instance.Combo >= 5 && BubbleSpawner.Instance.Combo < 10)
		{
			multiple = 2f;
		}
		else if (BubbleSpawner.Instance.Combo >= 10)
		{
			multiple = 3f;
		}
		int score = 0;
		if (isRemoveByShangdian || isRemoveByMuQiu || isRemoveByBing || isRemoveByHuo || isRemoveBy3he || isRemoveBy4he)
		{
			score = (int)(250f * multiple);
		}
		else
		{
			score = (int)(10f * multiple);
		}
		if (isRemoveByShangdian || isRemoveByMuQiu || isRemoveByBing || isRemoveByHuo)
		{
		}
		if (isRemoveBy3he)
		{
			PlayFX(fx_3skill_ball, skill3heTime, isRemove: false);
			yield return new WaitForSeconds(skill3heTime);
		}
		if ((bool)fx_zhizhu_obj)
		{
			SwitchZhiZhu(fx_zhizhu_obj, "remove");
			yield return new WaitForSeconds(0.333f);
		}
		if (isRemoveBy4he)
		{
			base.gameObject.transform.DOScale(0f, 0f).SetDelay(1f).OnComplete(delegate
			{
				PlayFX(fx_4skill_ball, skill4heTime, isRemove: false);
			});
			yield return new WaitForSeconds(skill4heTime + 1f);
		}
		int attributes = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["attributes"]);
		if (attributes == 10 || attributes == 11 || attributes == 12 || attributes == 13 || skillhuo || skillmu || skilldian || skillbing)
		{
			score = (int)(250f * multiple);
			AddScore(score);
		}
		else if (attributes == 100 || attributes == 101)
		{
			AddScore(score);
			base.gameObject.transform.DOScale(0f, 0f).SetDelay(removetirm).OnComplete(delegate
			{
				if (mBubbleData.i == 0)
				{
					this.gameObject.transform.DOScale(0f, 0f);
					PassLevel.action.CreateFlyBubbleObj(this.gameObject, attributes);
					if (attributes == 100)
					{
						FXELFIN(isbig: false);
					}
					else
					{
						FXELFIN(isbig: true);
					}
					UnityEngine.Object.Destroy(this.gameObject);
				}
				else
				{
					UnityEngine.Object.Destroy(this.gameObject);
				}
			});
		}
		else if (mType == 11 && !isRemoveByHuo && !isRemoveByBing)
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("b_ice");
			}
			fxtime = 0.867f;
			base.gameObject.transform.DOScale(0f, 0f);
			PlayFX(fx_bing_remove, fxtime);
			AddScore(score);
		}
		else if (mType == 7 && !isRemoveByHuo && !isRemoveByBing)
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("b_air");
			}
			fxtime = 0.5f;
			base.gameObject.transform.DOScale(0f, 0f);
			PlayFX(fx_kongqi_remove, fxtime);
			AddScore(score);
		}
		else if (mType == 8 && !isRemoveByHuo && !isRemoveByBing)
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("b_stone");
			}
			fxtime = 0.933f;
			base.gameObject.transform.DOScale(0f, 0f);
			PlayFX(fx_shitou_remove, fxtime);
			AddScore(score);
		}
		else if (mType == 10 && !isRemoveByHuo && !isRemoveByBing)
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("b_guadian");
			}
			fxtime = 1f;
			base.gameObject.transform.DOScale(0f, 0f);
			PlayFX(fx_guadian_remove, fxtime);
			AddScore(score);
		}
		else if (mType == 9 && !isRemoveByHuo && !isRemoveByBing)
		{
			AddScore(score);
			fxtime = 0.93f;
			base.gameObject.transform.DOScale(0f, 0f);
			PlayFX(fx_tieci, fxtime);
		}
		else if (mType == 19 && !isRemoveByHuo && !isRemoveByBing)
		{
			AddScore(score);
			fxtime = 3f;
			base.gameObject.transform.DOScale(1f, 0f).SetDelay(0.5f).OnComplete(delegate
			{
				this.gameObject.transform.DOScale(0f, 0f);
			});
			PlayFX(fx_mfjl_remove, fxtime);
		}
		else
		{
			if (bBoss3Remove)
			{
				CheckBoosCutHp(2);
			}
			if (mType == 17)
			{
				score = (int)(250f * multiple);
			}
			base.gameObject.transform.DOScale(0f, 0f).SetDelay(removetirm).OnComplete(delegate
			{
				AddScore(score);
				if (attributes == 7)
				{
					PassLevel.action.AddBushu(this.gameObject);
				}
				else if (attributes == 8)
				{
					PassLevel.action.UseBushu(this.gameObject);
				}
				if (time != 0f && !isRemoveByShangdian && !isRemoveByMuQiu && !isRemoveByJingling && !isRemoveByzhadan)
				{
					UnityEngine.Object.Destroy(this.gameObject);
				}
				else if (isRemoveByMuQiu)
				{
					if ((bool)SoundController.action && !isRemoveByDBHUO && !isRemoveByShangdian)
					{
						SoundController.action.playNow("b_boom", NowPlay: false, 10);
					}
					fxtime = 0.733f;
					PlayFX(fx_muqiu_remove, fxtime);
				}
				else
				{
					if ((bool)SoundController.action)
					{
						if (isRemoveByTiechi)
						{
							SoundController.action.playNow("b_spikey");
						}
						else if (!isRemoveByJingling && !isRemoveByDBHUO && !isRemoveByShangdian)
						{
							SoundController.action.playNow("b_boom", NowPlay: false, 10);
						}
					}
					fxtime = 0.6f;
					PlayFX(fx_pop, fxtime);
				}
			});
		}
	}

	public void AddScore(int score)
	{
		if (score > 0)
		{
			if (bwin100fen)
			{
				score = 100;
			}
			if (isAddScore && !Singleton<LevelManager>.Instance.bBossHuang)
			{
				GameUI.action.ShowScore(score, base.gameObject);
			}
		}
	}

	public void checkSkill()
	{
		int num = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["attributes"]);
		float num2 = 1.52f;
		float num3 = 1.16f;
		float fxtimedian = 0.733f;
		float num4 = 1.44f;
		float fxtimemu = 1.2f;
		float num5 = 1.6f;
		float num6 = 2f;
		float num7 = 0.6f;
		if (skillzhadan)
		{
			if (num == 20)
			{
				MapMoveSpawner.Instance.delayTime = num7;
				RemoveZhadan();
				base.gameObject.transform.DOScale(0f, 0f);
				PlayFX(fx_bomb_remove, num7);
			}
			return;
		}
		if (!skillhuo && !skillmu && !skilldian && !skillbing)
		{
			switch (num)
			{
			case 10:
				MapMoveSpawner.Instance.delayTime = num2;
				RemoveHuo();
				base.gameObject.transform.DOScale(0f, 0f);
				PlayFX(fx_fire_remove, num2);
				break;
			case 11:
				MapMoveSpawner.Instance.delayTime = fxtimedian;
				RemoveShandian();
				base.gameObject.transform.DOScale(0f, 0f);
				PlayFX(fx_zap_remove, fxtimedian);
				break;
			case 12:
				MapMoveSpawner.Instance.delayTime = num4;
				RemoveBing();
				base.gameObject.transform.DOScale(0f, 0f);
				PlayFX(fx_ice_remove, num4);
				break;
			case 13:
				MapMoveSpawner.Instance.delayTime = fxtimemu;
				RemoveMu();
				base.gameObject.transform.DOScale(0f, 0f);
				PlayFX(fx_wood_remove, fxtimemu);
				break;
			case 32:
				MapMoveSpawner.Instance.delayTime = num3;
				PlayFX(fx_fire_removejiguang, num3);
				MapMoveSpawner.Instance.delayTime = num3;
				base.gameObject.transform.DOScale(0f, 0f);
				RemoveBongbongbong();
				break;
			}
			return;
		}
		if (skillhuo && !skillmu && !skilldian && !skillbing)
		{
			if (num == 10)
			{
				MapMoveSpawner.Instance.delayTime = num2;
				RemoveHuo();
				base.gameObject.transform.DOScale(0f, 0f);
				PlayFX(fx_fire_remove, num2);
			}
			return;
		}
		if (!skillhuo && !skillmu && skilldian && !skillbing)
		{
			if (num == 11)
			{
				MapMoveSpawner.Instance.delayTime = fxtimedian;
				RemoveShandian();
				base.gameObject.transform.DOScale(0f, 0f);
				PlayFX(fx_zap_remove, fxtimedian);
			}
			return;
		}
		if (!skillhuo && !skillmu && !skilldian && skillbing)
		{
			if (num == 12)
			{
				MapMoveSpawner.Instance.delayTime = num4;
				RemoveBing();
				base.gameObject.transform.DOScale(0f, 0f);
				PlayFX(fx_ice_remove, num4);
			}
			return;
		}
		if (!skillhuo && skillmu && !skilldian && !skillbing)
		{
			if (num == 13)
			{
				MapMoveSpawner.Instance.delayTime = fxtimemu;
				RemoveMu();
				base.gameObject.transform.DOScale(0f, 0f);
				PlayFX(fx_wood_remove, fxtimemu);
			}
			return;
		}
		if (skillhuo && !skillmu && !skilldian && skillbing)
		{
			Singleton<UserManager>.Instance.SetPassTask("Gang2Skill");
			scaleFX();
			MapMoveSpawner.Instance.delayTime = num2;
			RemoveHuo();
			base.gameObject.transform.DOScale(0f, 0f);
			PlayFX(fx_fire_remove, num2);
			return;
		}
		if (!skillhuo && skillmu && !skilldian && skillbing)
		{
			Singleton<UserManager>.Instance.SetPassTask("Gang2Skill");
			scaleFX();
			MapMoveSpawner.Instance.delayTime = num4 + fxtimemu;
			RemoveBing();
			PlayFX(fx_ice_remove, num4, isRemove: false);
			base.gameObject.transform.DOScale(0f, 0f).SetDelay(num4).OnComplete(delegate
			{
				RemoveMu();
				PlayFX(fx_wood_remove, fxtimemu);
			});
			return;
		}
		if (!skillhuo && !skillmu && skilldian && skillbing)
		{
			Singleton<UserManager>.Instance.SetPassTask("Gang2Skill");
			scaleFX();
			MapMoveSpawner.Instance.delayTime = num4 + fxtimedian;
			RemoveBing();
			PlayFX(fx_ice_remove, num4, isRemove: false);
			base.gameObject.transform.DOScale(0f, 0f).SetDelay(num4).OnComplete(delegate
			{
				RemoveShandian();
				PlayFX(fx_zap_remove, fxtimedian);
			});
			return;
		}
		if (skillhuo && skillmu && !skilldian && !skillbing)
		{
			Singleton<UserManager>.Instance.SetPassTask("Gang2Skill");
			scaleFX();
			MapMoveSpawner.Instance.delayTime = num2 + fxtimemu;
			RemoveHuo();
			PlayFX(fx_fire_remove, num2, isRemove: false);
			base.gameObject.transform.DOScale(0f, 0f).SetDelay(num2).OnComplete(delegate
			{
				RemoveMu();
				PlayFX(fx_wood_remove, fxtimemu);
			});
			return;
		}
		if (skillhuo && !skillmu && skilldian && !skillbing)
		{
			Singleton<UserManager>.Instance.SetPassTask("Gang2Skill");
			scaleFX();
			MapMoveSpawner.Instance.delayTime = num2 + fxtimedian;
			RemoveHuo();
			PlayFX(fx_fire_remove, num2, isRemove: false);
			base.gameObject.transform.DOScale(0f, 0f).SetDelay(num2).OnComplete(delegate
			{
				RemoveShandian();
				PlayFX(fx_zap_remove, fxtimedian);
			});
			return;
		}
		if (!skillhuo && skillmu && skilldian && !skillbing)
		{
			Singleton<UserManager>.Instance.SetPassTask("Gang2Skill");
			scaleFX();
			MapMoveSpawner.Instance.delayTime = fxtimedian + fxtimemu;
			RemoveShandian();
			PlayFX(fx_zap_remove, fxtimedian, isRemove: false);
			base.gameObject.transform.DOScale(0f, 0f).SetDelay(fxtimedian).OnComplete(delegate
			{
				RemoveMu();
				PlayFX(fx_wood_remove, fxtimemu);
			});
			return;
		}
		int num8 = 0;
		if (GetComponent<BubbleObj>().skillbing)
		{
			num8++;
		}
		if (GetComponent<BubbleObj>().skillmu)
		{
			num8++;
		}
		if (GetComponent<BubbleObj>().skilldian)
		{
			num8++;
		}
		if (GetComponent<BubbleObj>().skillhuo)
		{
			num8++;
		}
		switch (num8)
		{
		case 3:
		{
			bool flag2 = true;
			Singleton<UserManager>.Instance.SetPassTask("Gang2Skill");
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("skill_super_1", NowPlay: true);
			}
			MapMoveSpawner.Instance.delayTime = num5 + 0.5f;
			base.gameObject.transform.DOScale(0f, 0f);
			PlayFX(fx_3skill_bg, skill3hebgTime);
			PlayFX(fx_3skill_remove, num5);
			for (int j = mBubbleData.row; j <= mBubbleData.row + 3; j++)
			{
				if (j >= BubbleSpawner.rows)
				{
					continue;
				}
				for (int k = 0; k < BubbleSpawner.cols - mBubbleData.row % 2; k++)
				{
					GameObject gameObject2 = BubbleSpawner.Instance.BubbleArray[j, k];
					if ((bool)gameObject2 && !gameObject2.GetComponent<BubbleObj>().isRemove)
					{
						gameObject2.GetComponent<BubbleObj>().isRemoveBy3he = true;
						BubbleObj component2 = gameObject2.GetComponent<BubbleObj>();
						if (component2.mBubbleData.key == "HBoss" && flag2)
						{
							flag2 = false;
							gameObject2.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, 0.5f, bskill: true);
						}
						else
						{
							gameObject2.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, 0.5f);
						}
					}
				}
			}
			for (int num13 = mBubbleData.row - 1; num13 >= mBubbleData.row - 3; num13--)
			{
				if (num13 >= 0)
				{
					for (int l = 0; l < BubbleSpawner.cols - num13 % 2; l++)
					{
						GameObject gameObject3 = BubbleSpawner.Instance.BubbleArray[num13, l];
						if ((bool)gameObject3 && !gameObject3.GetComponent<BubbleObj>().isRemove)
						{
							gameObject3.GetComponent<BubbleObj>().isRemoveBy3he = true;
							BubbleObj component3 = gameObject3.GetComponent<BubbleObj>();
							if (component3.mBubbleData.key == "HBoss" && flag2)
							{
								flag2 = false;
								gameObject3.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, 0.5f, bskill: true);
							}
							else
							{
								gameObject3.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, 0.5f);
							}
						}
					}
				}
			}
			break;
		}
		case 4:
		{
			bool flag = true;
			Singleton<UserManager>.Instance.SetPassTask("Gang2Skill");
			Singleton<UserManager>.Instance.SetPassTask("Gang4Skill");
			Singleton<UserManager>.Instance.SetPassTask1("Skill4");
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("skill_super_2", NowPlay: true);
			}
			MapMoveSpawner.Instance.delayTime = num6 + 0.5f;
			base.gameObject.transform.DOScale(0f, 0f);
			PlayFX(fx_4skill_remove, num6 + 5f);
			base.gameObject.transform.DOScale(0f, 0f).SetDelay(1.06f).OnComplete(delegate
			{
				PlayFX(fx_4skill_bg, 3f, isRemove: false);
			});
			float num9 = float.MaxValue;
			int num10 = 0;
			IEnumerator enumerator = BubbleSpawner.Instance.BallParent.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.Current;
					Vector3 position = transform.position;
					if (position.y < num9 && (bool)transform.GetComponent<BubbleObj>() && (transform.GetComponent<BubbleObj>().mBubbleData.row != mBubbleData.row || transform.GetComponent<BubbleObj>().mBubbleData.col != mBubbleData.col))
					{
						Vector3 position2 = transform.position;
						num9 = position2.y;
						num10 = transform.GetComponent<BubbleObj>().mBubbleData.row;
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
			int num11 = 0;
			for (int num12 = num10; num12 >= mBubbleData.row - 11; num12--)
			{
				if (num12 >= 0)
				{
					num11++;
					for (int i = 0; i < BubbleSpawner.cols; i++)
					{
						GameObject gameObject = BubbleSpawner.Instance.BubbleArray[num12, i];
						if ((bool)gameObject && !gameObject.GetComponent<BubbleObj>().isRemove)
						{
							gameObject.GetComponent<BubbleObj>().isRemoveBy4he = true;
							gameObject.GetComponent<BubbleObj>().iRemoveIndex = num11;
							BubbleObj component = gameObject.GetComponent<BubbleObj>();
							if (component.mBubbleData.key == "HBoss" && flag)
							{
								flag = false;
								gameObject.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, 0.5f, bskill: true);
							}
							else
							{
								gameObject.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, 0.5f);
							}
						}
					}
				}
			}
			break;
		}
		}
	}

	public void RemoveZhadan()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("item_boom");
		}
		bool flag = true;
		List<Vector2> around = BubbleSpawner.Instance.GetAround(mBubbleData.row, mBubbleData.col);
		List<Vector2> vecPos = new List<Vector2>();
		BubbleSpawner.Instance.GetAnimAllList(vecPos, around, mBubbleData.row, mBubbleData.col);
		for (int i = 0; i < around.Count; i++)
		{
			GameObject[,] bubbleArray = BubbleSpawner.Instance.BubbleArray;
			Vector2 vector = around[i];
			int num = (int)vector.x;
			Vector2 vector2 = around[i];
			GameObject gameObject = bubbleArray[num, (int)vector2.y];
			if (!gameObject || gameObject.GetComponent<BubbleObj>().isRemove)
			{
				continue;
			}
			gameObject.GetComponent<BubbleObj>().isRemoveByzhadan = true;
			BubbleObj component = gameObject.GetComponent<BubbleObj>();
			if (component.mBubbleData.key == "HBoss" && flag)
			{
				flag = false;
				gameObject.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, 0.5f, bskill: true);
				continue;
			}
			float num2 = 1f;
			if (BubbleSpawner.Instance.Combo >= 5 && BubbleSpawner.Instance.Combo < 10)
			{
				num2 = 2f;
			}
			else if (BubbleSpawner.Instance.Combo >= 10)
			{
				num2 = 3f;
			}
			int score = (int)(250f * num2);
			gameObject.GetComponent<BubbleObj>().isAddScore = true;
			gameObject.GetComponent<BubbleObj>().AddScore(score);
			gameObject.GetComponent<BubbleObj>().isAddScore = false;
			gameObject.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, 0.5f);
		}
	}

	public void RemoveHuo()
	{
		bool flag = true;
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("b_fire");
		}
		List<Vector2> around = BubbleSpawner.Instance.GetAround(mBubbleData.row, mBubbleData.col);
		List<Vector2> list = new List<Vector2>();
		BubbleSpawner.Instance.GetAnimAllList(list, around, mBubbleData.row, mBubbleData.col);
		for (int i = 0; i < around.Count; i++)
		{
			GameObject[,] bubbleArray = BubbleSpawner.Instance.BubbleArray;
			Vector2 vector = around[i];
			int num = (int)vector.x;
			Vector2 vector2 = around[i];
			GameObject gameObject = bubbleArray[num, (int)vector2.y];
			if ((bool)gameObject && !gameObject.GetComponent<BubbleObj>().isRemove)
			{
				gameObject.GetComponent<BubbleObj>().isRemoveByHuo = true;
				gameObject.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, 0.5f);
				BubbleObj component = gameObject.GetComponent<BubbleObj>();
				if (component.mBubbleData.key == "HBoss" && flag)
				{
					flag = false;
					gameObject.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, 0.5f, bskill: true);
				}
				else
				{
					gameObject.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, 0.5f);
				}
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			GameObject[,] bubbleArray2 = BubbleSpawner.Instance.BubbleArray;
			Vector2 vector3 = list[j];
			int num2 = (int)vector3.x;
			Vector2 vector4 = list[j];
			GameObject gameObject2 = bubbleArray2[num2, (int)vector4.y];
			if ((bool)gameObject2 && !gameObject2.GetComponent<BubbleObj>().isRemove)
			{
				gameObject2.GetComponent<BubbleObj>().isRemoveByHuo = true;
				BubbleObj component2 = gameObject2.GetComponent<BubbleObj>();
				if (component2.mBubbleData.key == "HBoss" && flag)
				{
					flag = false;
					gameObject2.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, 0.7f, bskill: true);
				}
				else
				{
					gameObject2.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, 0.7f);
				}
			}
		}
	}

	public void RemoveBongbongbong()
	{
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("sfx_laser");
		}
		CameraShake component = Camera.main.GetComponent<CameraShake>();
		component.numberOfShakes = 1;
		component.shakeAmount = new Vector3(0.1f, 0.3f, 0f);
		component.rotationAmount = new Vector3(0f, 0f, 0f);
		component.distance = 0.1f;
		component.speed = 55f;
		component.decay = 0.6f;
		component.Shake();
		List<Vector2> around = BubbleSpawner.Instance.GetAround(mBubbleData.row, mBubbleData.col);
		List<Vector2> vecPos = new List<Vector2>();
		BubbleSpawner.Instance.GetAnimAllList(vecPos, around, mBubbleData.row, mBubbleData.col);
		for (int i = 0; i < around.Count; i++)
		{
			GameObject[,] bubbleArray = BubbleSpawner.Instance.BubbleArray;
			Vector2 vector = around[i];
			int num = (int)vector.x;
			Vector2 vector2 = around[i];
			GameObject gameObject = bubbleArray[num, (int)vector2.y];
			if ((bool)gameObject && !gameObject.GetComponent<BubbleObj>().isRemove)
			{
				gameObject.GetComponent<BubbleObj>().isRemoveBybbb = true;
				gameObject.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, 0.2f);
			}
		}
	}

	private void scaleFX()
	{
		base.gameObject.transform.DOScale(0f, 0f);
		if ((bool)fx_skillhuo)
		{
			fx_skillhuo.transform.DOScale(0f, 0f);
		}
		if ((bool)fx_skillmu)
		{
			fx_skillmu.transform.DOScale(0f, 0f);
		}
		if ((bool)fx_skilldian)
		{
			fx_skilldian.transform.DOScale(0f, 0f);
		}
		if ((bool)fx_skillbing)
		{
			fx_skillbing.transform.DOScale(0f, 0f);
		}
		if ((bool)fx_skilljiguang)
		{
			fx_skilljiguang.transform.DOScale(0f, 0f);
		}
	}

	public void RemoveShandian()
	{
		bool flag = true;
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("b_lightning");
		}
		for (int i = 0; i < BubbleSpawner.cols - mBubbleData.row % 2; i++)
		{
			GameObject gameObject = BubbleSpawner.Instance.BubbleArray[mBubbleData.row, i];
			if ((bool)gameObject && !gameObject.GetComponent<BubbleObj>().isRemove)
			{
				gameObject.GetComponent<BubbleObj>().isRemoveByShangdian = true;
				BubbleObj component = gameObject.GetComponent<BubbleObj>();
				if (component.mBubbleData.key == "HBoss" && flag)
				{
					flag = false;
					gameObject.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, 0.5f, bskill: true);
				}
				else
				{
					gameObject.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, 0.5f);
				}
			}
		}
	}

	public void RemoveBing()
	{
		bool flag = true;
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("b_bingshuang");
		}
		List<Vector2> around = BubbleSpawner.Instance.GetAround(mBubbleData.row, mBubbleData.col);
		List<Vector2> vecPos = new List<Vector2>();
		BubbleSpawner.Instance.GetAnimAllList(vecPos, around, mBubbleData.row, mBubbleData.col);
		for (int i = 0; i < around.Count; i++)
		{
			GameObject[,] bubbleArray = BubbleSpawner.Instance.BubbleArray;
			Vector2 vector = around[i];
			int num = (int)vector.x;
			Vector2 vector2 = around[i];
			GameObject gameObject = bubbleArray[num, (int)vector2.y];
			if ((bool)gameObject && !gameObject.GetComponent<BubbleObj>().isRemove && !gameObject.GetComponent<BubbleObj>().isReadyRemove)
			{
				gameObject.GetComponent<BubbleObj>().isRemoveByBing = true;
				BubbleObj component = gameObject.GetComponent<BubbleObj>();
				if (component.mBubbleData.key == "HBoss" && flag)
				{
					flag = false;
					gameObject.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, 0.7f, bskill: true);
				}
				else
				{
					gameObject.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, 0.7f);
				}
			}
		}
	}

	public void RemoveMu()
	{
		bmuBoss5 = true;
		if ((bool)SoundController.action)
		{
			SoundController.action.playNow("b_wood");
		}
		int num = 0;
		float num2 = 0.2f;
		for (int num3 = mBubbleData.row - 1; num3 >= mBubbleData.row - 5; num3--)
		{
			if (num3 >= 0)
			{
				num++;
				if (mBubbleData.row % 2 == 1)
				{
					if (num3 % 2 == 0)
					{
						GameObject gameObject = BubbleSpawner.Instance.BubbleArray[num3, mBubbleData.col];
						if ((bool)gameObject && !gameObject.GetComponent<BubbleObj>().isRemove)
						{
							gameObject.GetComponent<BubbleObj>().isRemoveByMuQiu = true;
							RemoveBoss5mu(gameObject, (float)num * num2);
						}
						if (mBubbleData.col + 1 <= BubbleSpawner.cols)
						{
							GameObject gameObject2 = BubbleSpawner.Instance.BubbleArray[num3, mBubbleData.col + 1];
							if ((bool)gameObject2 && !gameObject2.GetComponent<BubbleObj>().isRemove)
							{
								gameObject2.GetComponent<BubbleObj>().isRemoveByMuQiu = true;
								RemoveBoss5mu(gameObject2, (float)num * num2);
							}
						}
					}
					else
					{
						GameObject gameObject3 = BubbleSpawner.Instance.BubbleArray[num3, mBubbleData.col];
						if ((bool)gameObject3 && !gameObject3.GetComponent<BubbleObj>().isRemove)
						{
							gameObject3.GetComponent<BubbleObj>().isRemoveByMuQiu = true;
							RemoveBoss5mu(gameObject3, (float)num * num2);
						}
					}
				}
				else if (num3 % 2 == 1)
				{
					if (mBubbleData.col - 1 >= 0)
					{
						GameObject gameObject4 = BubbleSpawner.Instance.BubbleArray[num3, mBubbleData.col - 1];
						if ((bool)gameObject4 && !gameObject4.GetComponent<BubbleObj>().isRemove)
						{
							gameObject4.GetComponent<BubbleObj>().isRemoveByMuQiu = true;
							RemoveBoss5mu(gameObject4, (float)num * num2);
						}
					}
					GameObject gameObject5 = BubbleSpawner.Instance.BubbleArray[num3, mBubbleData.col];
					if ((bool)gameObject5 && !gameObject5.GetComponent<BubbleObj>().isRemove)
					{
						gameObject5.GetComponent<BubbleObj>().isRemoveByMuQiu = true;
						RemoveBoss5mu(gameObject5, (float)num * num2);
					}
				}
				else
				{
					GameObject gameObject6 = BubbleSpawner.Instance.BubbleArray[num3, mBubbleData.col];
					if ((bool)gameObject6 && !gameObject6.GetComponent<BubbleObj>().isRemove)
					{
						gameObject6.GetComponent<BubbleObj>().isRemoveByMuQiu = true;
						RemoveBoss5mu(gameObject6, (float)num * num2);
					}
				}
			}
		}
		RemoveController.Instance.isFallBubbleCheck = true;
	}

	public void RemoveBoss5mu(GameObject obj, float fdelay)
	{
		BubbleObj component = obj.GetComponent<BubbleObj>();
		if (component.mBubbleData.key == "HBoss" && bmuBoss5)
		{
			bmuBoss5 = false;
			obj.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, fdelay, bskill: true);
		}
		else
		{
			obj.GetComponent<BubbleObj>().RemoveBubble(isFallBubble: false, fdelay);
		}
	}

	public void PlayRemoveGanran()
	{
		if ((bool)fx_ganran_obj && !fxganranIsRemove && mBubbleData.key != "G")
		{
			PlayFX(fx_ganran_remove, 1f, isRemove: false);
			UnityEngine.Object.DestroyObject(fx_ganran_obj);
			fx_ganran_obj = null;
			fxganranIsRemove = true;
			if ((bool)SoundController.action)
			{
				SoundController.action.play("b_spider_web_destroy");
			}
		}
	}

	public void PlayRemoveCloud()
	{
		if ((bool)fx_cloud_obj && !fxCloudIsRemove)
		{
			UnityEngine.Object.Destroy(fx_cloud_obj.gameObject);
			fxCloudIsRemove = true;
			GameObject b = UnityEngine.Object.Instantiate(fx_cloud_remove, base.transform.position, base.transform.rotation);
			b.transform.parent = base.gameObject.transform;
			b.transform.localPosition = new Vector3(0f, 0f, -1f);
			b.transform.DOScale(1f, 0f).SetDelay(0.876f).OnComplete(delegate
			{
				UnityEngine.Object.Destroy(b.gameObject);
			});
		}
	}

	public void PlayFX(GameObject obj, float fxtime, bool isRemove = true)
	{
		StartCoroutine(_PlayFX(obj, fxtime, isRemove));
	}

	private IEnumerator _PlayFX(GameObject obj, float fxtime, bool isRemove = true)
	{
		GameObject b = UnityEngine.Object.Instantiate(obj, base.transform.position, base.transform.rotation);
		yield return new WaitForSeconds(0.02f);
		int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["attributes"]);
		Vector3 pos = (mBubbleData.row != -1 || mBubbleData.col != -1) ? BubbleSpawner.Instance.GetSquare(mBubbleData.row, mBubbleData.col).transform.position : new Vector3(0f, 0f, 0f);
		if (obj == fx_zap_remove)
		{
			b.transform.position = new Vector3(0f, pos.y, -20f);
		}
		else if (obj == fx_jingxiang_remove)
		{
			b.transform.position = new Vector3(pos.x, pos.y, -20f);
		}
		else if (obj == fx_4skill_bg)
		{
			Transform transform = b.transform;
			Vector3 position = BubbleSpawner.Instance.skill4BubbleParent.transform.position;
			float y = position.y;
			Vector3 position2 = base.gameObject.transform.position;
			transform.position = new Vector3(0f, y, position2.z);
		}
		else if (obj == fx_4skill_remove || obj == fx_4skill_remove2)
		{
			Transform transform2 = b.transform;
			Vector3 position3 = BubbleSpawner.Instance.skill4BubbleParent.transform.position;
			float y2 = position3.y;
			Vector3 position4 = base.gameObject.transform.position;
			transform2.position = new Vector3(0f, y2, position4.z);
		}
		else if (obj == fx_3skill_bg)
		{
			Transform transform3 = b.transform;
			Vector3 position5 = BubbleSpawner.Instance.skill4BubbleParent.transform.position;
			float y3 = position5.y;
			Vector3 position6 = base.gameObject.transform.position;
			transform3.position = new Vector3(0f, y3, position6.z);
		}
		else if (obj == fx_prop4 || obj == fx_prop5 || obj == fx_prop6 || obj == fx_4skill_ball)
		{
			Transform transform4 = b.transform;
			Vector3 position7 = base.transform.position;
			float x = position7.x;
			Vector3 position8 = base.transform.position;
			float y4 = position8.y;
			Vector3 position9 = base.gameObject.transform.position;
			transform4.position = new Vector3(x, y4, position9.z + 10f);
		}
		else
		{
			b.transform.position = new Vector3(pos.x, pos.y, -20f);
		}
		b.transform.parent = BubbleSpawner.Instance.FXParent.transform;
		UnityEngine.Object.Destroy(b, fxtime);
		yield return new WaitForSeconds(0.02f);
		if (obj == fx_3skill_remove)
		{
			fxtime = 1.567f;
			UnityEngine.Object.Destroy(b, fxtime);
			float delay = 0.5f + skill3heTime;
			base.gameObject.transform.DOScale(0f, 0f).SetDelay(delay).OnComplete(delegate
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(fx_3skill_remove2, this.transform.position, this.transform.rotation);
				Transform transform5 = gameObject.transform;
				Vector3 position10 = this.gameObject.transform.position;
				float y5 = position10.y;
				Vector3 position11 = this.gameObject.transform.position;
				transform5.position = new Vector3(0f, y5, position11.z);
				fxtime = 1.667f;
				UnityEngine.Object.Destroy(gameObject, fxtime);
				UnityEngine.Object.Destroy(this.gameObject, 0f);
			});
		}
		else if (obj == fx_3skill_bg)
		{
			UnityEngine.Object.Destroy(b, fxtime);
		}
		else if (obj == fx_prop4)
		{
			UnityEngine.Object.Destroy(b, fxtime);
		}
		else
		{
			UnityEngine.Object.Destroy(b, fxtime);
			if (isRemove)
			{
				UnityEngine.Object.Destroy(base.gameObject, 0f);
			}
		}
	}

	public void FXELFIN(bool isbig)
	{
		GameObject gameObject = (!isbig) ? UnityEngine.Object.Instantiate(fx_elfin_remove, base.transform.position, base.transform.rotation) : UnityEngine.Object.Instantiate(fx_bigElf_remove, base.transform.position, base.transform.rotation);
		Vector3 position = BubbleSpawner.Instance.GetSquare(mBubbleData.row, mBubbleData.col).transform.position;
		gameObject.transform.position = new Vector3(position.x, position.y, -20f);
		gameObject.transform.parent = base.gameObject.transform.parent.transform.parent;
		UnityEngine.Object.Destroy(gameObject, 1.5f);
	}

	public void FXPOP()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(fx_pop, base.transform.position, base.transform.rotation);
		Vector3 position = BubbleSpawner.Instance.GetSquare(mBubbleData.row, mBubbleData.col).transform.position;
		gameObject.transform.position = new Vector3(position.x, position.y, -20f);
		gameObject.transform.parent = base.gameObject.transform.parent.transform.parent;
		UnityEngine.Object.Destroy(gameObject, 1.5f);
	}

	public void ChangeTo(string key, bool bBossChange = false)
	{
		if (mBubbleData.key == key)
		{
			return;
		}
		if (key == "BB")
		{
			render.transform.position += new Vector3(0f, 0.05f, 0f);
		}
		if (key == "JL")
		{
			render.SetActive(value: false);
		}
		if (bBossChange)
		{
			skillhuo = false;
			skilldian = false;
			skillbing = false;
			skillmu = false;
			skillmu = false;
			if ((bool)fx_skill_3skill)
			{
				UnityEngine.Object.Destroy(fx_skill_3skill.gameObject);
			}
			if ((bool)fx_skillhuo)
			{
				UnityEngine.Object.Destroy(fx_skillhuo);
			}
			if ((bool)fx_skilldian)
			{
				UnityEngine.Object.Destroy(fx_skilldian);
			}
			if ((bool)fx_skillbing)
			{
				UnityEngine.Object.Destroy(fx_skillbing);
			}
			if ((bool)fx_skillmu)
			{
				UnityEngine.Object.Destroy(fx_skillmu);
			}
			if ((bool)fx_skilljingxiang)
			{
				UnityEngine.Object.Destroy(fx_skilljingxiang);
			}
			if ((bool)fx_zd)
			{
				UnityEngine.Object.Destroy(fx_zd);
			}
			if ((bool)fx_mfjz)
			{
				UnityEngine.Object.Destroy(fx_mfjz);
			}
			if ((bool)fx_mfjl)
			{
				UnityEngine.Object.Destroy(fx_mfjl);
			}
		}
		if ((bool)fx_skilljingxiang)
		{
			obj_fx_jingxiang_remove.SetActive(value: true);
			SkeletonAnimation component = obj_fx_jingxiang_remove.GetComponent<SkeletonAnimation>();
			component.Initialize(overwrite: true);
			component.loop = false;
			component.state.SetAnimation(0, "add", loop: false);
			component.state.End += delegate
			{
				obj_fx_jingxiang_remove.SetActive(value: false);
			};
		}
		else if (int.Parse(Singleton<DataManager>.Instance.dBubble[key]["type"]) > 5)
		{
			if (key == "BB")
			{
				PlayFX(fx_prop4, 10f, isRemove: false);
			}
			else if (key == "JZ")
			{
				PlayFX(fx_prop5, 10f, isRemove: false);
			}
			else if (key == "JL")
			{
				PlayFX(fx_prop6, 10f, isRemove: false);
			}
		}
		if ((bool)fx_skilljingxiang)
		{
			UnityEngine.Object.Destroy(fx_skilljingxiang.gameObject);
		}
		isCheck = true;
		mBubbleData.key = key;
		mType = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["type"]);
		int num = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["img"]) - 1;
		render.GetComponent<SpriteRenderer>().sprite = BubbleSpawner.Instance.BubbleSprite[num];
		int num2 = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["attributes"]);
		switch (num2)
		{
		case 10:
		{
			GameObject original5 = fx_skill_fire;
			Vector3 position13 = base.transform.position;
			float x5 = position13.x;
			Vector3 position14 = base.transform.position;
			float y5 = position14.y;
			Vector3 position15 = base.transform.position;
			fx_skillhuo = UnityEngine.Object.Instantiate(original5, new Vector3(x5, y5, -3f + position15.z), base.transform.rotation);
			fx_skillhuo.transform.parent = base.gameObject.transform;
			skillhuo = true;
			break;
		}
		case 32:
		{
			GameObject original4 = fx_skill_fire;
			Vector3 position10 = base.transform.position;
			float x4 = position10.x;
			Vector3 position11 = base.transform.position;
			float y4 = position11.y;
			Vector3 position12 = base.transform.position;
			fx_skillhuo = UnityEngine.Object.Instantiate(original4, new Vector3(x4, y4, -3f + position12.z), base.transform.rotation);
			fx_skillhuo.transform.parent = base.gameObject.transform;
			skillbbb = true;
			break;
		}
		case 11:
		{
			GameObject original3 = fx_skill_zap;
			Vector3 position7 = base.transform.position;
			float x3 = position7.x;
			Vector3 position8 = base.transform.position;
			float y3 = position8.y;
			Vector3 position9 = base.transform.position;
			fx_skilldian = UnityEngine.Object.Instantiate(original3, new Vector3(x3, y3, -5f + position9.z), base.transform.rotation);
			fx_skilldian.transform.parent = base.gameObject.transform;
			skilldian = true;
			break;
		}
		case 12:
		{
			BubbleSpawner.Instance.skillBingCount = 1;
			GameObject original2 = fx_skill_ice;
			Vector3 position4 = base.transform.position;
			float x2 = position4.x;
			Vector3 position5 = base.transform.position;
			float y2 = position5.y;
			Vector3 position6 = base.transform.position;
			fx_skillbing = UnityEngine.Object.Instantiate(original2, new Vector3(x2, y2, -2f + position6.z), base.transform.rotation);
			fx_skillbing.transform.parent = base.gameObject.transform;
			skillbing = true;
			break;
		}
		case 13:
		{
			GameObject original = fx_skill_wood;
			Vector3 position = base.transform.position;
			float x = position.x;
			Vector3 position2 = base.transform.position;
			float y = position2.y;
			Vector3 position3 = base.transform.position;
			fx_skillmu = UnityEngine.Object.Instantiate(original, new Vector3(x, y, -4f + position3.z), base.transform.rotation);
			fx_skillmu.transform.parent = base.gameObject.transform;
			skillmu = true;
			break;
		}
		case 20:
			fx_zd = UnityEngine.Object.Instantiate(fx_zd_obj, base.transform.position, base.transform.rotation);
			fx_zd.transform.parent = base.gameObject.transform;
			fx_zd.transform.localScale = new Vector3(0.64f, 0.64f, 0.64f);
			skillzhadan = true;
			break;
		case 14:
			skilljiang = true;
			fx_mfjz = UnityEngine.Object.Instantiate(fx_mfjz_obj, base.transform.position, base.transform.rotation);
			fx_mfjz.transform.parent = base.gameObject.transform;
			fx_mfjz.transform.localScale = new Vector3(0.64f, 0.64f, 0.64f);
			break;
		case 15:
			skilljingling = true;
			fx_mfjl = UnityEngine.Object.Instantiate(fx_mfjl_obj, base.transform.position, base.transform.rotation);
			fx_mfjl.transform.parent = base.gameObject.transform;
			fx_mfjl.transform.localScale = new Vector3(0.64f, 0.64f, 0.64f);
			break;
		}
		if (num2 == 15 || num2 == 14 || num2 == 20)
		{
			G_fx_mofa_light = UnityEngine.Object.Instantiate(fx_mofa_light, base.transform.position, base.transform.rotation);
			G_fx_mofa_light.transform.parent = base.gameObject.transform;
			G_fx_mofa_light.transform.localScale = new Vector3(0.64f, 0.64f, 0.64f);
		}
		int num3 = 0;
		if (skillhuo)
		{
			num3++;
		}
		if (skilldian)
		{
			num3++;
		}
		if (skillbing)
		{
			num3++;
		}
		if (skillmu)
		{
			num3++;
		}
		switch (num3)
		{
		case 4:
		{
			if ((bool)fx_skillhuo)
			{
				UnityEngine.Object.Destroy(fx_skillhuo.gameObject);
			}
			if ((bool)fx_skillmu)
			{
				UnityEngine.Object.Destroy(fx_skillmu.gameObject);
			}
			if ((bool)fx_skilldian)
			{
				UnityEngine.Object.Destroy(fx_skilldian.gameObject);
			}
			if ((bool)fx_skillbing)
			{
				UnityEngine.Object.Destroy(fx_skillbing.gameObject);
			}
			if ((bool)fx_skill_3skill)
			{
				UnityEngine.Object.Destroy(fx_skill_3skill.gameObject);
			}
			GameObject original7 = fx_skill_4;
			Vector3 position19 = base.transform.position;
			float x7 = position19.x;
			Vector3 position20 = base.transform.position;
			float y7 = position20.y;
			Vector3 position21 = base.transform.position;
			fx_skill_4skill = UnityEngine.Object.Instantiate(original7, new Vector3(x7, y7, -2f + position21.z), base.transform.rotation);
			fx_skill_4skill.transform.parent = base.gameObject.transform;
			break;
		}
		case 3:
		{
			if ((bool)fx_skillhuo)
			{
				UnityEngine.Object.Destroy(fx_skillhuo.gameObject);
			}
			if ((bool)fx_skillmu)
			{
				UnityEngine.Object.Destroy(fx_skillmu.gameObject);
			}
			if ((bool)fx_skilldian)
			{
				UnityEngine.Object.Destroy(fx_skilldian.gameObject);
			}
			if ((bool)fx_skillbing)
			{
				UnityEngine.Object.Destroy(fx_skillbing.gameObject);
			}
			GameObject original6 = fx_skill_3;
			Vector3 position16 = base.transform.position;
			float x6 = position16.x;
			Vector3 position17 = base.transform.position;
			float y6 = position17.y;
			Vector3 position18 = base.transform.position;
			fx_skill_3skill = UnityEngine.Object.Instantiate(original6, new Vector3(x6, y6, -2f + position18.z), base.transform.rotation);
			fx_skill_3skill.transform.parent = base.gameObject.transform;
			break;
		}
		}
	}

	public void ChangeToRandom()
	{
		PlayFX(fx_bianse, 1f, isRemove: false);
		isCheck = true;
		mBubbleData.key = BubbleSpawner.Instance.GetBubbleRandomKeyStep2(mBubbleData.key);
		mType = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["type"]);
		int num = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["img"]) - 1;
		render.GetComponent<SpriteRenderer>().sprite = BubbleSpawner.Instance.BubbleSprite[num];
	}

	public void addfx_mofajian_remove(float z)
	{
		DDOLSingleton<CoroutineController>.Instance.StartCoroutine(Hidebg());
	}

	public IEnumerator Hidebg()
	{
		yield return new WaitForSeconds(1f);
		GameUI.action.GameBG.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
	}

	public void ReadyBubbleChange()
	{
		if (mBubbleData.key != null)
		{
			int num = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["attributes"]);
			if (num == 10 || num == 11 || num == 11 || num == 12 || num == 13 || num == 20 || num == 14 || num == 15)
			{
				return;
			}
		}
		mBubbleData.key = BubbleSpawner.Instance.GetBubbleRandomKey();
		mType = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["type"]);
		int num2 = int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["img"]) - 1;
		render.GetComponent<SpriteRenderer>().sprite = BubbleSpawner.Instance.BubbleSprite[num2];
	}

	public void FallBubble(bool bisFlyLevel = false)
	{
		isFall = true;
		base.transform.DOKill();
		PlayRemoveCloud();
		PlayRemoveGanran();
		try
		{
			BubbleSpawner.Instance.BubbleArray[mBubbleData.row, mBubbleData.col] = null;
		}
		catch (Exception arg)
		{
			UnityEngine.Debug.LogError("jyerr= " + arg);
		}
		base.gameObject.transform.parent = BubbleSpawner.Instance.FallParent.transform;
		Transform transform = base.transform;
		Vector3 position = base.transform.position;
		float x = position.x;
		Vector3 position2 = base.transform.position;
		transform.position = new Vector3(x, position2.y, -8f);
		Collider2D component = base.gameObject.GetComponent<Collider2D>();
		component.sharedMaterial = BubbleSpawner.Instance.fallPhysicsMaterial2D;
		base.gameObject.layer = 9;
		base.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
		base.gameObject.GetComponent<Rigidbody2D>().gravityScale = 11f;
		base.gameObject.GetComponent<Rigidbody2D>().mass = 0.5f;
		base.gameObject.GetComponent<Rigidbody2D>().drag = 0.3f;
		base.gameObject.GetComponent<Rigidbody2D>().fixedAngle = false;
		if (bisFlyLevel)
		{
			Transform transform2 = base.transform;
			Vector3 position3 = base.transform.position;
			transform2.DOMoveY(position3.y + 1.5f, 0.2f).SetEase(Ease.OutSine);
			base.gameObject.GetComponent<Rigidbody2D>().velocity = base.gameObject.GetComponent<Rigidbody2D>().velocity + new Vector2(UnityEngine.Random.Range(0, 5), 0f) - new Vector2(UnityEngine.Random.Range(0, 5), 0f);
		}
		else
		{
			base.gameObject.GetComponent<Rigidbody2D>().velocity = base.gameObject.GetComponent<Rigidbody2D>().velocity + new Vector2(UnityEngine.Random.Range(0, 2), 0f) - new Vector2(UnityEngine.Random.Range(0, 2), 0f);
		}
		base.gameObject.GetComponent<CircleCollider2D>().enabled = true;
		base.gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
		base.gameObject.GetComponent<CircleCollider2D>().radius = 0.3f;
		base.gameObject.GetComponent<BubbleFall>().enabled = true;
	}

	public void GameOverShootBubble(int ivelocity, int ivelocityCount)
	{
		isFall = true;
		while (ivelocity > 5)
		{
			ivelocity -= 5;
		}
		base.gameObject.transform.parent = BubbleSpawner.Instance.FallParent.transform;
		Transform transform = base.transform;
		Vector3 position = base.transform.position;
		float x = position.x;
		Vector3 position2 = base.transform.position;
		transform.position = new Vector3(x, position2.y, -8f);
		Collider2D component = base.gameObject.GetComponent<Collider2D>();
		component.sharedMaterial = BubbleSpawner.Instance.fallPhysicsMaterial2D;
		base.gameObject.layer = 9;
		base.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
		base.gameObject.GetComponent<Rigidbody2D>().gravityScale = 11f;
		base.gameObject.GetComponent<Rigidbody2D>().mass = 0.5f;
		base.gameObject.GetComponent<Rigidbody2D>().drag = 0.3f;
		base.gameObject.GetComponent<Rigidbody2D>().fixedAngle = false;
		if (ivelocityCount < 3)
		{
			base.gameObject.GetComponent<Rigidbody2D>().velocity = base.gameObject.GetComponent<Rigidbody2D>().velocity + new Vector2(UnityEngine.Random.Range(0, 5), 12f) - new Vector2(UnityEngine.Random.Range(0, 5), 0f);
		}
		else
		{
			base.gameObject.GetComponent<Rigidbody2D>().velocity = base.gameObject.GetComponent<Rigidbody2D>().velocity + new Vector2(ivelocity, 12f);
		}
		base.gameObject.GetComponent<CircleCollider2D>().enabled = true;
		base.gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
		base.gameObject.GetComponent<CircleCollider2D>().radius = 0.3f;
		base.gameObject.GetComponent<BubbleFall>().enabled = true;
		int num = UnityEngine.Random.Range(20, 90);
		StartCoroutine(IEDesc((float)num / 100f));
	}

	private IEnumerator IEDesc(float random)
	{
		yield return new WaitForSeconds(random);
		GameUI.action.ShowScore(Singleton<LevelManager>.Instance.ijiesuanScore, base.gameObject);
		Singleton<LevelManager>.Instance.ijiesuanScore += 1000;
		if (Singleton<LevelManager>.Instance.ijiesuanScore >= 9999)
		{
			Singleton<LevelManager>.Instance.ijiesuanScore = 9999;
		}
		GameObject b = UnityEngine.Object.Instantiate(fx_end_pop_boom, base.transform.position, base.transform.rotation);
		int.Parse(Singleton<DataManager>.Instance.dBubble[mBubbleData.key]["attributes"]);
		Transform transform = b.transform;
		Vector3 position = base.transform.position;
		float x = position.x;
		Vector3 position2 = base.transform.position;
		transform.position = new Vector3(x, position2.y, -20f);
		b.transform.parent = BubbleSpawner.Instance.FXParent.transform;
		yield return new WaitForSeconds(0.03f);
		render.SetActive(value: false);
		yield return new WaitForSeconds(1.6f);
		UnityEngine.Object.Destroy(b);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void Update()
	{
		if (!isFall)
		{
			base.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		}
	}

	public void fx_mofa_elf_selectFunc()
	{
		fx_mofa_elf_selectObj = UnityEngine.Object.Instantiate(fx_mofa_elf_select, base.transform.position, base.transform.rotation);
		fx_mofa_elf_selectObj.transform.parent = base.gameObject.transform;
		fx_mofa_elf_selectObj.transform.localScale = new Vector3(0.64f, 0.64f, 0.64f);
	}

	public void DelG_fx_mofa_light()
	{
		if ((bool)G_fx_mofa_light)
		{
			if (Singleton<DataManager>.Instance.byaping)
			{
				GameUI.action.GameBG.GetComponent<SpriteRenderer>().color = new Color(20f / 51f, 20f / 51f, 20f / 51f, 1f);
			}
			UnityEngine.Object.Destroy(G_fx_mofa_light);
		}
	}

	public void ReturnYaping()
	{
		if (Singleton<DataManager>.Instance.byaping)
		{
			Singleton<DataManager>.Instance.byaping = false;
			GameUI.action.GameBG.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
		}
	}

	public void PlayAni()
	{
		SkeletonAnimation component = GameUI.action.FlyBg.transform.Find("elfin_gezhao").GetComponent<SkeletonAnimation>();
		if (!(component.name == "elfin_gezhao_hit"))
		{
			component.Initialize(overwrite: true);
			component.loop = false;
			component.state.SetAnimation(0, "elfin_gezhao_hit", loop: false);
			component.state.End += delegate
			{
				PlayAniStatic();
			};
		}
	}

	public void PlayAniStatic()
	{
		SkeletonAnimation component = GameUI.action.FlyBg.transform.Find("elfin_gezhao").GetComponent<SkeletonAnimation>();
		component.Initialize(overwrite: true);
		component.loop = false;
		component.state.SetAnimation(0, "elfin_gezhao_Static", loop: true);
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (isFall || PassLevel.bWin || !Singleton<LevelManager>.Instance.bstartbubble || bflyobjstatePeople || !RemoveController.bwhileturestart || !(coll.gameObject.tag == "flybg"))
		{
			return;
		}
		if (bflyobjstate)
		{
			if ((bool)SoundController.action)
			{
				SoundController.action.playNow("sfx_bubbles_deadlline");
			}
			FallBubble(bisFlyLevel: true);
			PlayAni();
			RemoveController.Instance.isFallBubbleCheck = true;
		}
		else
		{
			bflyobjstate = true;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
	}

	public void BubbleFlyAnimationFlyStop()
	{
		SkeletonAnimation component = fx_flyobj_.GetComponent<SkeletonAnimation>();
		component.Initialize(overwrite: true);
		component.loop = false;
		component.state.SetAnimation(0, "elfin_hit", loop: false);
		component.state.End += delegate
		{
			BubbleFlyAnimationStatic();
		};
	}

	public void BubbleFlyAnimationStatic()
	{
		SkeletonAnimation component = fx_flyobj_.GetComponent<SkeletonAnimation>();
		component.Initialize(overwrite: true);
		component.loop = false;
		component.state.SetAnimation(0, "elfin_Static", loop: true);
	}

	public void BubbleFlyAnimationflying()
	{
		SkeletonAnimation component = fx_flyobj_.GetComponent<SkeletonAnimation>();
		component.Initialize(overwrite: true);
		component.loop = false;
		component.state.SetAnimation(0, "elfin_Fly", loop: true);
	}

	public void FlyReady()
	{
		UnityEngine.Debug.Log("FlyReady");
		SkeletonAnimation component = fx_flyobj_.GetComponent<SkeletonAnimation>();
		component.Initialize(overwrite: true);
		component.loop = false;
		component.state.SetAnimation(0, "elfin_ready", loop: false);
		component.state.End += delegate
		{
			Fly2();
		};
	}

	public void Fly2()
	{
		UnityEngine.Debug.Log("Fly2");
		SkeletonAnimation component = fx_flyobj_.GetComponent<SkeletonAnimation>();
		component.Initialize(overwrite: true);
		component.loop = false;
		component.state.SetAnimation(0, "elfin_Fly", loop: false);
		FindPath.action.FlyGrid();
	}

	public void Fly3()
	{
		UnityEngine.Debug.Log("Fly3");
		SkeletonAnimation component = fx_flyobj_.GetComponent<SkeletonAnimation>();
		component.Initialize(overwrite: true);
		component.loop = false;
		component.state.SetAnimation(0, "elfin_Fly", loop: false);
		FindPath.action.FlyGrid();
	}
}
