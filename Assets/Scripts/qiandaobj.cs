using UnityEngine;
using UnityEngine.UI;

public class qiandaobj : MonoBehaviour
{
	public GameObject okimg1;

	public GameObject okimg2;

	public GameObject patObj;

	public GameObject LineIconObj;

	public Text NbText1;

	public Text NbText2;

	public void Initqiandao(int index)
	{
		index = 8 - index;
		NbText1.text = index.ToString();
		NbText2.text = index.ToString();
		int @int = Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_iqiandao7" + index);
		if (@int == 1)
		{
			IsOk();
		}
		string text = "1|30F1|15F1|20F1|5";
		text = Singleton<DataManager>.Instance.dDataSignmap7[index.ToString()]["reward"];
		for (int i = 0; i < 3; i++)
		{
			string text2 = text.Split('F')[i];
			int num = int.Parse(text2.Split('|')[0]);
			int num2 = int.Parse(text2.Split('|')[1]);
			GameObject gameObject = UnityEngine.Object.Instantiate(LineIconObj);
			gameObject.transform.SetParent(patObj.gameObject.transform, worldPositionStays: false);
			gameObject.GetComponent<Image>().sprite = Util.GetResourcesSprite("Img/SigninUI/signin_icon_" + num, 138, 114);
			gameObject.SetActive(value: true);
			gameObject.transform.Find("Number").GetComponent<Text>().text = "x" + num2;
		}
		if (index > 5)
		{
			GridLayoutGroup component = patObj.GetComponent<GridLayoutGroup>();
		}
	}

	public void IsOk()
	{
		okimg1.SetActive(value: true);
		okimg2.SetActive(value: true);
	}
}
