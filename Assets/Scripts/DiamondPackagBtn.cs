using UnityEngine;
using UnityEngine.UI;

public class DiamondPackagBtn : MonoBehaviour
{
	public int index;

	public GameObject obj_tl;

	public GameObject obj_bs;

	public GameObject obj_zd;

	public GameObject obj_jz;

	public GameObject obj_jl;

	public GameObject obj_zk;

	public GameObject buyBtn;

	private void Start()
	{
		if ((bool)obj_tl)
		{
			GameObject gameObject = obj_tl.transform.Find("Text").gameObject;
			if (index == 3)
			{
				gameObject.GetComponent<Text>().text = "1H";
			}
			else if (index == 4)
			{
				gameObject.GetComponent<Text>().text = "1H";
			}
			else if (index == 5)
			{
				gameObject.GetComponent<Text>().text = "2H";
			}
		}
		if ((bool)obj_bs)
		{
			GameObject gameObject2 = obj_bs.transform.Find("Text").gameObject;
			if (index == 1)
			{
				gameObject2.GetComponent<Text>().text = "x3";
			}
			if (index == 2)
			{
				gameObject2.GetComponent<Text>().text = "x3";
			}
			if (index == 3 || index == 4)
			{
				gameObject2.GetComponent<Text>().text = "x4";
			}
			if (index == 5)
			{
				gameObject2.GetComponent<Text>().text = "x8";
			}
		}
		if ((bool)obj_zd)
		{
			GameObject gameObject3 = obj_zd.transform.Find("Text").gameObject;
			if (index == 2)
			{
				gameObject3.GetComponent<Text>().text = "x3";
			}
			if (index == 3 || index == 4)
			{
				gameObject3.GetComponent<Text>().text = "x4";
			}
			if (index == 5)
			{
				gameObject3.GetComponent<Text>().text = "x8";
			}
		}
		if ((bool)obj_jz)
		{
			GameObject gameObject4 = obj_jz.transform.Find("Text").gameObject;
			if (index == 3 || index == 4)
			{
				gameObject4.GetComponent<Text>().text = "x4";
			}
			if (index == 5)
			{
				gameObject4.GetComponent<Text>().text = "x6";
			}
		}
		if ((bool)obj_jl)
		{
			GameObject gameObject5 = obj_jl.transform.Find("Text").gameObject;
			gameObject5.GetComponent<Text>().text = "x" + index.ToString();
			if (index == 4)
			{
				gameObject5.GetComponent<Text>().text = "x2";
			}
			if (index == 5)
			{
				gameObject5.GetComponent<Text>().text = "x6";
			}
		}
		if ((bool)obj_zk)
		{
			GameObject gameObject6 = obj_zk.transform.Find("Text").gameObject;
			if (index == 1)
			{
				gameObject6.GetComponent<Text>().text = "-20%";
			}
			if (index == 2)
			{
				gameObject6.GetComponent<Text>().text = "-35%";
			}
			if (index == 3)
			{
				gameObject6.GetComponent<Text>().text = "-50%";
			}
			if (index == 4)
			{
				gameObject6.GetComponent<Text>().text = "-50%";
			}
			if (index == 5)
			{
				gameObject6.GetComponent<Text>().text = "-52%";
			}
		}
	}

	public void buyPackag()
	{
		if (index == 1)
		{
			PayManager.action.Pay("PACKAGE0199");
		}
		else if (index == 2)
		{
			PayManager.action.Pay("PACKAGE299");
		}
		else if (index == 3)
		{
			PayManager.action.Pay("PACKAGE699");
		}
		else if (index == 4)
		{
			PayManager.action.Pay("PACKAGE999");
		}
		else if (index == 5)
		{
			PayManager.action.Pay("PACKAGE1999");
		}
	}

	private void Update()
	{
	}
}
