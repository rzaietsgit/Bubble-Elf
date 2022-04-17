using UnityEngine;

public class StartTipPanel : StartTipPanelBase
{
	public static StartTipPanel panel;

	public Sprite[] btnsp;

	private bool b1;

	private bool b2;

	public override void InitUI()
	{
		panel = this;
		BaseUIAnimation.action.SetLanguageFont("gameinfo1", detail.ltite_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("gameinfo2", detail.imok1_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("gameinfo3", detail.imok2_Text, string.Empty);
		BaseUIAnimation.action.SetLanguageFont("gameinfo3", detail.okbtn_Text, string.Empty);
		CheckBtn();
	}

	public override void OnS_btn1()
	{
	}

	public void CheckBtn()
	{
		if (b1 && b2)
		{
			detail.okokbtn_Image.sprite = btnsp[0];
		}
		else
		{
			detail.okokbtn_Image.sprite = btnsp[1];
		}
	}

	public override void OnS_btn001()
	{
		if (b1)
		{
			b1 = false;
			detail.btn1img_Image.gameObject.SetActive(value: false);
		}
		else
		{
			detail.btn1img_Image.gameObject.SetActive(value: true);
			b1 = true;
		}
		CheckBtn();
	}

	public override void OnS_btn002()
	{
		if (b2)
		{
			b2 = false;
			detail.btn2img_Image.gameObject.SetActive(value: false);
		}
		else
		{
			b2 = true;
			detail.btn2img_Image.gameObject.SetActive(value: true);
		}
		CheckBtn();
	}

	public override void OnS_btn2()
	{
	}

	public override void Onokokbtn()
	{
		if (b1 && b2)
		{
			Singleton<TestScript>.Instance.SetInt("DB_AgreeServer", 1);
			UI.Instance.ClosePanel();
		}
	}

	public override void OnExit()
	{
		LoginScene.action.twoStart();
	}
}
