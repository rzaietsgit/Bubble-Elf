using Spine.Unity;
using UnityEngine;

public class shangdian : MonoBehaviour
{
	public static shangdian action;

	public GameObject IconObj;

	public GameObject discoun_icon;

	private void Start()
	{
		action = this;
		BuyRes();
	}

	public void BuyRes()
	{
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_DoublePay");
		if (@int == 1)
		{
			SkeletonAnimation component = discoun_icon.GetComponent<SkeletonAnimation>();
			component.skeleton.SetSkin("icon2");
		}
	}

	private void Update()
	{
	}
}
