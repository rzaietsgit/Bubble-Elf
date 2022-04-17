using TMPro;
using UnityEngine;

public class BuyGoldSon : MonoBehaviour
{
	public GameObject BuyIconObj;

	public GameObject BuyADFree;

	public GameObject AwardIcon;

	public GameObject BuyBtnType;

	public TextMeshProUGUI BuyPrice;

	public TextMeshProUGUI MoneyViewText;

	public TextMeshProUGUI SongMoneyObj;

	public TextMeshProUGUI SongMoneyViewText;

	public GameObject PayBtn;

	private string PayKey;

	public Sprite[] LBuyIconObj;

	public Sprite[] LAwardIcon;

	public Sprite[] LBuyBtnType;

	public void ClickPay()
	{
		if ((bool)PayManager.action)
		{
			PayManager.action.Pay(PayKey);
		}
	}

	private void Start()
	{
		BaseUIAnimation.action.CreateButton(PayBtn.gameObject);
	}

	private void Update()
	{
	}
}
