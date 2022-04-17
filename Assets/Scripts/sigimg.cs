using UnityEngine;
using UnityEngine.UI;

public class sigimg : MonoBehaviour
{
	public Sprite nullSp;

	public int sid;

	private void Start()
	{
	}

	public void ClickShow()
	{
		SignRewardUIPanel.panel.ClickSignShowBox(sid);
	}

	public void SetId(int index)
	{
		sid = index;
	}

	public void SetNull()
	{
		if (sid != 6 && sid != 14 && sid != 27)
		{
			GetComponent<Image>().sprite = nullSp;
		}
	}

	private void Update()
	{
	}
}
