using Lean;
using UnityEngine;

public class SimpleRotateScale : MonoBehaviour
{
	protected virtual void LateUpdate()
	{
		if (!Singleton<DataManager>.Instance.bGrilMoveing && !Singleton<DataManager>.Instance.bUiIsOpen && !SettingPanelUI.bSettingPanelUIOpen && UI.Instance.GetPanelCount() <= 0)
		{
			LeanTouch.ScaleObject(base.transform, LeanTouch.PinchScale);
			float nowMapSaleMin = MapManagerUI.action.NowMapSaleMin;
			float nowMapSaleMax = MapManagerUI.action.NowMapSaleMax;
			Vector3 localScale = base.transform.localScale;
			if (localScale.x < nowMapSaleMin)
			{
				base.transform.localScale = new Vector2(nowMapSaleMin, nowMapSaleMin);
			}
			Vector3 localScale2 = base.transform.localScale;
			if (localScale2.x >= nowMapSaleMax)
			{
				base.transform.localScale = new Vector2(nowMapSaleMax, nowMapSaleMax);
			}
		}
	}
}
