using DG.Tweening;
using Spine.Unity;
using System.Collections;
using UnityEngine;

public class MuTong : MonoBehaviour
{
	public GameObject rudai;

	public GameObject MT;

	public GameObject _mt1;

	public GameObject _mt2;

	public GameObject _mt4;

	public GameObject _mt5;

	public GameObject juneng;

	public GameObject fx_gangfire;

	public GameObject par_jinenglight;

	private GameObject jinenglightprefabe;

	public GameObject shouzhi;

	public GameObject maskObj;

	public GameObject mask;

	public GameObject Rudai_fz;

	public int mtIndex;

	private GameObject gangfireprefabe;

	public int mtScore;

	public int mtState;

	public int iScore = 800;

	public bool isSkill;

	private SkeletonAnimation skelet;

	private float time;

	private int iWinScore = 1000;

	private void Start()
	{
		StartCoroutine(init());
	}

	private IEnumerator init()
	{
		yield return new WaitForSeconds(0.1f);
		isSkill = Singleton<LevelManager>.Instance.GetGangSkill(mtIndex);
		if (isSkill)
		{
			StartCoroutine(PlayMoveY((float)UnityEngine.Random.Range(0, 100) * 0.01f));
		}
		else
		{
			maskObj.SetActive(value: false);
		}
	}

	private IEnumerator PlayMoveY(float time)
	{
		yield return new WaitForSeconds(time);
		maskObj.transform.DOLocalMoveY(0.8f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
	}

	public void RuDai(Vector3 pos)
	{
		GameObject gameObject = null;
		gameObject = UnityEngine.Object.Instantiate(rudai, base.transform.position, base.transform.rotation);
		gameObject.transform.parent = base.gameObject.transform.parent;
		gameObject.transform.position = pos;
		UnityEngine.Object.Destroy(gameObject, 1.2f);
		AddScore();
	}

	public void AddScore()
	{
		if (Time.time - time > 0.2f)
		{
			time = Time.time;
			GameObject gameObject = UnityEngine.Object.Instantiate(Rudai_fz);
			gameObject.transform.SetParent(base.transform, worldPositionStays: false);
			UnityEngine.Object.Destroy(gameObject, 2f);
		}
		float num = 1f;
		if ((bool)BubbleSpawner.Instance && BubbleSpawner.Instance.Combo >= 5 && BubbleSpawner.Instance.Combo < 10)
		{
			num = 2f;
		}
		else if ((bool)BubbleSpawner.Instance && BubbleSpawner.Instance.Combo >= 10)
		{
			num = 3f;
		}
		int num2 = (int)(100f * num);
		if (PassLevel.bWin)
		{
			num2 = iWinScore;
			iWinScore += 1000;
		}
		mtScore += num2;
		if (!Singleton<LevelManager>.Instance.bBossHuang)
		{
			GameUI.action.ShowScore(num2, base.gameObject, isMuTong: true);
		}
		if (isSkill && !PassLevel.bWin)
		{
			if (mtScore >= iScore)
			{
				SoundController.action.playNow("b_enter_buchet_1");
				mask.transform.localPosition = new Vector3(0f, 0.66f, 1f);
				if (jinenglightprefabe == null)
				{
					GameGuide.Instance.SkillOk();
					jinenglightprefabe = UnityEngine.Object.Instantiate(par_jinenglight, base.transform.position, base.transform.rotation);
					jinenglightprefabe.transform.parent = maskObj.transform;
					jinenglightprefabe.transform.localPosition = new Vector3(0f, 0f, 0f);
					MT.GetComponent<MuTong2>().lizifly.SetActive(value: true);
				}
			}
			else
			{
				mask.transform.localPosition = new Vector3(0f, (float)mtScore / 800f * 0.66f, 1f);
			}
		}
		SoundController.action.playNow("b_enter_buchet");
	}

	public void animStatic()
	{
	}

	public void AddSkill()
	{
		mtScore = 800;
		mtState = 5;
		if ((bool)SoundFireController.action)
		{
			SoundFireController.action.play();
		}
		mask.transform.localPosition = new Vector3(0f, 0.66f, 1f);
		if (jinenglightprefabe == null)
		{
			jinenglightprefabe = UnityEngine.Object.Instantiate(par_jinenglight, base.transform.position, base.transform.rotation);
			jinenglightprefabe.transform.parent = maskObj.transform;
			jinenglightprefabe.transform.localPosition = new Vector3(0f, 0f, 0f);
			MT.GetComponent<MuTong2>().lizifly.SetActive(value: true);
		}
	}

	public void UseSkill()
	{
		aliyunlog.UseGang(mtIndex);
		UnityEngine.Object.Destroy(jinenglightprefabe);
		GameGuide.Instance.useSkill();
		shouzhi.SetActive(value: false);
		MT.GetComponent<MuTong2>().lizifly.SetActive(value: false);
		mask.transform.DOLocalMoveY(0f, 1f);
		mtScore = 0;
	}
}
