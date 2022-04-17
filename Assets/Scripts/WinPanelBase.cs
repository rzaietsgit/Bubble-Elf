using Spine.Unity;
using UnityEngine.UI;

public class WinPanelBase : BasePanel
{
	public WinPanelDetail detail;

	private void Start()
	{
	}

	public void SetAllMemberValue()
	{
		detail.LevelText_Text = base.transform.Find("Top/WinLevelTitle/LevelText").gameObject.GetComponent<Text>();
		detail.LevelText_Shadow = base.transform.Find("Top/WinLevelTitle/LevelText").gameObject.GetComponent<Shadow>();
		detail.LevelText_ContentSizeFitter = base.transform.Find("Top/WinLevelTitle/LevelText").gameObject.GetComponent<ContentSizeFitter>();
		detail.WinLevelTitle_Image = base.transform.Find("Top/WinLevelTitle").gameObject.GetComponent<Image>();
		detail.WinUITitle_Text = base.transform.Find("Top/CenterImg/CenterImgTitle/WinUITitle").gameObject.GetComponent<Text>();
		detail.WinUITitle_Gradient = base.transform.Find("Top/CenterImg/CenterImgTitle/WinUITitle").gameObject.GetComponent<Gradient>();
		detail.WinUITitle_Shadow = base.transform.Find("Top/CenterImg/CenterImgTitle/WinUITitle").gameObject.GetComponent<Shadow>();
		detail.WinUITitle_ContentSizeFitter = base.transform.Find("Top/CenterImg/CenterImgTitle/WinUITitle").gameObject.GetComponent<ContentSizeFitter>();
		detail.CenterImgTitle_Image = base.transform.Find("Top/CenterImg/CenterImgTitle").gameObject.GetComponent<Image>();
		detail.CenterImg_Image = base.transform.Find("Top/CenterImg").gameObject.GetComponent<Image>();
		detail.starbg1_Image = base.transform.Find("Top/StarPanel/starbg1").gameObject.GetComponent<Image>();
		detail.starbg2_Image = base.transform.Find("Top/StarPanel/starbg2").gameObject.GetComponent<Image>();
		detail.starbg3_Image = base.transform.Find("Top/StarPanel/starbg3").gameObject.GetComponent<Image>();
		detail.star1_Image = base.transform.Find("Top/StarPanel/star1").gameObject.GetComponent<Image>();
		detail.star2_Image = base.transform.Find("Top/StarPanel/star2").gameObject.GetComponent<Image>();
		detail.star3_Image = base.transform.Find("Top/StarPanel/star3").gameObject.GetComponent<Image>();
		detail.ScoreBgWinUILevelScoreText_Text = base.transform.Find("Top/ScorePanel/ScoreBg/ScoreBgWinUILevelScoreText").gameObject.GetComponent<Text>();
		detail.ScoreBgWinUILevelScoreText_Shadow = base.transform.Find("Top/ScorePanel/ScoreBg/ScoreBgWinUILevelScoreText").gameObject.GetComponent<Shadow>();
		detail.ScoreBgWinUILevelScoreText_ContentSizeFitter = base.transform.Find("Top/ScorePanel/ScoreBg/ScoreBgWinUILevelScoreText").gameObject.GetComponent<ContentSizeFitter>();
		detail.ScoreBgWinScoreText_Text = base.transform.Find("Top/ScorePanel/ScoreBg/ScoreBgWinScoreText").gameObject.GetComponent<Text>();
		detail.ScoreBgWinScoreText_Shadow = base.transform.Find("Top/ScorePanel/ScoreBg/ScoreBgWinScoreText").gameObject.GetComponent<Shadow>();
		detail.ScoreBg_Image = base.transform.Find("Top/ScorePanel/ScoreBg").gameObject.GetComponent<Image>();
		detail.ScoreBgChinaWinUILevelScoreText_Text = base.transform.Find("Top/ScorePanel/ScoreBgChina/ScoreBgChinaWinUILevelScoreText").gameObject.GetComponent<Text>();
		detail.ScoreBgChinaWinUILevelScoreText_Shadow = base.transform.Find("Top/ScorePanel/ScoreBgChina/ScoreBgChinaWinUILevelScoreText").gameObject.GetComponent<Shadow>();
		detail.ScoreBgChinaWinScoreText_Text = base.transform.Find("Top/ScorePanel/ScoreBgChina/ScoreBgChinaWinScoreText").gameObject.GetComponent<Text>();
		detail.ScoreBgChinaWinScoreText_Shadow = base.transform.Find("Top/ScorePanel/ScoreBgChina/ScoreBgChinaWinScoreText").gameObject.GetComponent<Shadow>();
		detail.ScoreBgChinaAddGB_Text = base.transform.Find("Top/ScorePanel/ScoreBgChina/ScoreBgChinaAddGB").gameObject.GetComponent<Text>();
		detail.ScoreBgChinaAddGB_Shadow = base.transform.Find("Top/ScorePanel/ScoreBgChina/ScoreBgChinaAddGB").gameObject.GetComponent<Shadow>();
		detail.ScoreBgChinaImage_Image = base.transform.Find("Top/ScorePanel/ScoreBgChina/ScoreBgChinaImage").gameObject.GetComponent<Image>();
		detail.ScoreBgChina_Image = base.transform.Find("Top/ScorePanel/ScoreBgChina").gameObject.GetComponent<Image>();
		detail.ScoreBgChinaHuaWinUILevelScoreText_Text = base.transform.Find("Top/ScorePanel/ScoreBgChinaHua/ScoreBgChinaHuaWinUILevelScoreText").gameObject.GetComponent<Text>();
		detail.ScoreBgChinaHuaWinUILevelScoreText_Shadow = base.transform.Find("Top/ScorePanel/ScoreBgChinaHua/ScoreBgChinaHuaWinUILevelScoreText").gameObject.GetComponent<Shadow>();
		detail.ScoreBgChinaHuaWinScoreText_Text = base.transform.Find("Top/ScorePanel/ScoreBgChinaHua/ScoreBgChinaHuaWinScoreText").gameObject.GetComponent<Text>();
		detail.ScoreBgChinaHuaWinScoreText_Shadow = base.transform.Find("Top/ScorePanel/ScoreBgChinaHua/ScoreBgChinaHuaWinScoreText").gameObject.GetComponent<Shadow>();
		detail.ScoreBgChinaHuaImage_Image = base.transform.Find("Top/ScorePanel/ScoreBgChinaHua/ScoreBgChinaHuaImage").gameObject.GetComponent<Image>();
		detail.ScoreBgChinaHuaAddGB_Text = base.transform.Find("Top/ScorePanel/ScoreBgChinaHua/ScoreBgChinaHuaAddGB").gameObject.GetComponent<Text>();
		detail.ScoreBgChinaHuaAddGB_Shadow = base.transform.Find("Top/ScorePanel/ScoreBgChinaHua/ScoreBgChinaHuaAddGB").gameObject.GetComponent<Shadow>();
		detail.ScoreBgChinaHuaImage2_Image = base.transform.Find("Top/ScorePanel/ScoreBgChinaHua/ScoreBgChinaHuaImage2").gameObject.GetComponent<Image>();
		detail.ScoreBgChinaHuaAddHua_Text = base.transform.Find("Top/ScorePanel/ScoreBgChinaHua/ScoreBgChinaHuaAddHua").gameObject.GetComponent<Text>();
		detail.ScoreBgChinaHuaAddHua_Shadow = base.transform.Find("Top/ScorePanel/ScoreBgChinaHua/ScoreBgChinaHuaAddHua").gameObject.GetComponent<Shadow>();
		detail.ScoreBgChinaHua_Image = base.transform.Find("Top/ScorePanel/ScoreBgChinaHua").gameObject.GetComponent<Image>();
		detail.NextBtnfinger_SkeletonAnimation = base.transform.Find("Top/NextBtn/NextBtnfinger").gameObject.GetComponent<SkeletonAnimation>();
		detail.NextText_Text = base.transform.Find("Top/NextBtn/NextText").gameObject.GetComponent<Text>();
		detail.NextText_Shadow = base.transform.Find("Top/NextBtn/NextText").gameObject.GetComponent<Shadow>();
		detail.NextText_ContentSizeFitter = base.transform.Find("Top/NextBtn/NextText").gameObject.GetComponent<ContentSizeFitter>();
		detail.NextBtn_Image = base.transform.Find("Top/NextBtn").gameObject.GetComponent<Image>();
		detail.NextBtn_Button = base.transform.Find("Top/NextBtn").gameObject.GetComponent<Button>();
		detail.Close_Image = base.transform.Find("Top/Close").gameObject.GetComponent<Image>();
		detail.Close_Button = base.transform.Find("Top/Close").gameObject.GetComponent<Button>();
		detail.haoping_Image = base.transform.Find("Top/haoping").gameObject.GetComponent<Image>();
		detail.haoping_Button = base.transform.Find("Top/haoping").gameObject.GetComponent<Button>();
		detail.Top_Image = base.transform.Find("Top").gameObject.GetComponent<Image>();
		BtnAnimationBase btnAnimationBase = detail.NextBtn_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase.SetType(NewBtnType.NONE);
		btnAnimationBase.SetAction(OnNextBtn);
		BtnAnimationBase btnAnimationBase2 = detail.Close_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase2.SetType(NewBtnType.NONE);
		btnAnimationBase2.SetAction(OnClose);
		BtnAnimationBase btnAnimationBase3 = detail.haoping_Button.gameObject.AddComponent<BtnAnimationBase>();
		btnAnimationBase3.SetType(NewBtnType.NONE);
		btnAnimationBase3.SetAction(Onhaoping);
	}

	public virtual void InitUI()
	{
	}

	public virtual void OnNextBtn()
	{
	}

	public virtual void OnClose()
	{
	}

	public virtual void Onhaoping()
	{
	}
}
