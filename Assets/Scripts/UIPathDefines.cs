using System;

public static class UIPathDefines
{
	public const string UI_PREFAB = "Prefabs/UI/";

	public const string UI_CONTROLS_PREFAB = "UIPrefab/Control/";

	public const string UI_SUBUI_PREFAB = "UIPrefab/SubUI/";

	public const string UI_IOCN_PATH = "UI/Icon/";

	public static string GetPrefabPathByType(EnumUIType _uiType)
	{
		string result = string.Empty;
		switch (_uiType)
		{
		case EnumUIType.WinUI:
			result = "Prefabs/UI/WinUI";
			break;
		case EnumUIType.TestOne:
			result = "Prefabs/UI/TestUIOne";
			break;
		case EnumUIType.TestTwo:
			result = "Prefabs/UI/TestUITwo";
			break;
		case EnumUIType.MainSceneUI:
			result = "Prefabs/UI/MainSceneUI";
			break;
		case EnumUIType.MapSceneUI:
			result = "Prefabs/UI/MapSceneUI";
			break;
		case EnumUIType.GameSceneUI:
			result = "Prefabs/UI/GameSceneUI";
			break;
		case EnumUIType.LoadingbyOut:
			result = "Prefabs/UI/LoadingUIByOut";
			break;
		case EnumUIType.LoadingbyIn:
			result = "Prefabs/UI/LoadingUIByIn";
			break;
		case EnumUIType.PlayUI:
			result = "Prefabs/UI/PlayUI";
			break;
		case EnumUIType.SettingUI:
			result = "Prefabs/UI/SettingUI";
			break;
		case EnumUIType.PauseUI:
			result = "Prefabs/UI/PauseUI";
			break;
		case EnumUIType.BuyBubbleUI:
			result = "Prefabs/UI/BuyBubbleUI";
			break;
		case EnumUIType.BuyDaojuUI:
			result = "Prefabs/UI/BuyDaojuUI";
			break;
		case EnumUIType.cdkeyUI:
			result = "Prefabs/UI/cdkeyUI";
			break;
		case EnumUIType.Reward24UI:
			result = "Prefabs/UI/Reward24UI";
			break;
		case EnumUIType.Vip7UI:
			result = "Prefabs/UI/Vip7UI";
			break;
		case EnumUIType.PackSkillIconUI:
			result = "Prefabs/UI/PackSkillIconUI";
			break;
		case EnumUIType.NewTaskUI:
			result = "Prefabs/UI/NewTaskUI";
			break;
		case EnumUIType.NewTask1UI:
			result = "Prefabs/UI/NewTask1UI";
			break;
		case EnumUIType.BuySkillUIChina:
			result = "Prefabs/UI/BuySkillUIChina";
			break;
		case EnumUIType.LevelRewardUI:
			result = "Prefabs/UI/LevelRewardUI";
			break;
		case EnumUIType.LotteryUI:
			result = "Prefabs/UI/LotteryUI";
			break;
		case EnumUIType.BuyLivesChinaUI:
			result = "Prefabs/UI/BuyLivesChinaUI";
			break;
		case EnumUIType.DayTaskUI:
			result = "Prefabs/UI/DayTaskUI";
			break;
		case EnumUIType.MapRewardUI:
			result = "Prefabs/UI/MapRewardUI";
			break;
		case EnumUIType.MapRewardChinaUI:
			result = "Prefabs/UI/MapRewardChinaUI";
			break;
		case EnumUIType.RewardBtnUI:
			result = "Prefabs/UI/RewardBtnUI";
			break;
		case EnumUIType.cdKeyRewardUI:
			result = "Prefabs/UI/cdKeyRewardUI";
			break;
		case EnumUIType.NowBuyBubbleUI:
			result = "Prefabs/UI/NowBuyBubbleUI";
			break;
		case EnumUIType.InviteFriendsUI:
			result = "Prefabs/UI/InviteFriendsUI";
			break;
		case EnumUIType.SkillTipUI:
			result = "Prefabs/UI/SkillTipUI";
			break;
		case EnumUIType.RateUsUI:
			result = "Prefabs/UI/RateUsUI";
			break;
		case EnumUIType.DareUI:
			result = "Prefabs/UI/DareUI";
			break;
		case EnumUIType.HuaShopUI:
			result = "Prefabs/UI/HuaShopUI";
			break;
		case EnumUIType.HuaZhanbu:
			result = "Prefabs/UI/HuaZhanbuUI";
			break;
		case EnumUIType.HuaHelpUI:
			result = "Prefabs/UI/HuaHelpUI";
			break;
		case EnumUIType.HuaShowUI:
			result = "Prefabs/UI/HuaShowUI";
			break;
		case EnumUIType.DareWinUI:
			result = "Prefabs/UI/DareWinUI";
			break;
		case EnumUIType.DareLoseUI:
			result = "Prefabs/UI/DareLoseUI";
			break;
		case EnumUIType.FaceBookRankOpenUI:
			result = "Prefabs/UI/FaceBookRankOpenUI";
			break;
		case EnumUIType.AdAwardUI:
			result = "Prefabs/UI/AdAwardUI";
			break;
		case EnumUIType.QuitUI:
			result = "Prefabs/UI/QuitUI";
			break;
		case EnumUIType.LoseUI:
			result = "Prefabs/UI/LoseUI";
			break;
		case EnumUIType.GGqiandaoUI:
			result = "Prefabs/UI/GGqiandao";
			break;
		case EnumUIType.BuyGoldUI:
			result = "Prefabs/UI/BuyGoldUI";
			break;
		case EnumUIType.BuyGoldActivityUI:
			result = "Prefabs/UI/BuyGoldActivityUI";
			break;
		case EnumUIType.BuyLivesUI:
			result = "Prefabs/UI/BuyLivesUI";
			break;
		case EnumUIType.GoodRepuationUI:
			result = "Prefabs/UI/GoodRepuationUI";
			break;
		case EnumUIType.GuideMaxUI:
			result = "Prefabs/UI/GuideMaxUI";
			break;
		case EnumUIType.SaleAdUI:
			result = "Prefabs/UI/SaleAdUI";
			break;
		case EnumUIType.aboutChinaUI:
			result = "Prefabs/UI/aboutChinaUI";
			break;
		case EnumUIType.GuideMinUI:
			result = "Prefabs/UI/GuideMinUI";
			break;
		case EnumUIType.BuyGangUI:
			result = "Prefabs/UI/BuyGangUI";
			break;
		case EnumUIType.BuySaleUI:
			result = "Prefabs/UI/BuySaleUI";
			break;
		case EnumUIType.TipFailUI:
			result = "Prefabs/UI/TipFailUI";
			break;
		case EnumUIType.BuyGoldSaleUI:
			result = "Prefabs/UI/BuyGoldSaleUI";
			break;
		case EnumUIType.GamePanelUI:
			result = "Prefabs/UI/GamePanelUI";
			break;
		case EnumUIType.ReadyGoUI:
			result = "Prefabs/UI/ReadyGoUI";
			break;
		case EnumUIType.ReadyWinUI:
			result = "Prefabs/UI/ReadyWinUI";
			break;
		case EnumUIType.TipWinUI:
			result = "Prefabs/UI/TipWinUI";
			break;
		case EnumUIType.BuySkillUI:
			result = "Prefabs/UI/BuySkillUI";
			break;
		case EnumUIType.LanguageUI:
			result = "Prefabs/UI/LanguageUI";
			break;
		case EnumUIType.LoadingSceneUI:
			result = "Prefabs/UI/LoadingSceneUI";
			break;
		case EnumUIType.LivesUI:
			result = "Prefabs/UI/LivesUI";
			break;
		case EnumUIType.AskLivesUI:
			result = "Prefabs/UI/AskLivesUI";
			break;
		case EnumUIType.SendLivesUI:
			result = "Prefabs/UI/SendLivesUI";
			break;
		case EnumUIType.MessageUI:
			result = "Prefabs/UI/MessageUI";
			break;
		case EnumUIType.BuyGoldUIByDiamonds:
			result = "Prefabs/UI/BuyGoldSonByDiamonds/BuyGoldUIByDiamonds";
			break;
		case EnumUIType.SigninUI:
			result = "Prefabs/UI/Signin/SigninUI";
			break;
		case EnumUIType.SignRewardUI:
			result = "Prefabs/UI/SignRewardUI";
			break;
		case EnumUIType.SignReward7UI:
			result = "Prefabs/UI/SignReward7UI";
			break;
		case EnumUIType.ShopUI:
			result = "Prefabs/UI/Shop/ShopUI";
			break;
		case EnumUIType.ChinaShopUI:
			result = "Prefabs/UI/ChinaShopUI";
			break;
		case EnumUIType.NovicepacksUI:
			result = "Prefabs/UI/Novicepacks/NovicepacksUI";
			break;
		case EnumUIType.FirstPacksUI:
			result = "Prefabs/UI/FirstPacks/FirstPacks";
			break;
		case EnumUIType.BuyDiamondUI:
			result = "Prefabs/UI/BuyDiamondUI/BuyDiamondUI";
			break;
		case EnumUIType.EnSuperSale:
			result = "Prefabs/UI/SuperSale/SuperSale";
			break;
		case EnumUIType.EnBuyLives:
			result = "Prefabs/UI/BuyLives/BuyLives";
			break;
		case EnumUIType.EnConnectFacebook:
			result = "Prefabs/UI/ConnectFacebook/ConnectFacebook";
			break;
		case EnumUIType.EnFirstInvite:
			result = "Prefabs/UI/FirstInvite/FirstInvite";
			break;
		case EnumUIType.EnSendLives:
			result = "Prefabs/UI/SendLives/SendLives";
			break;
		}
		return result;
	}

	public static Type GetUIScriptByType(EnumUIType _uiType)
	{
		return null;
	}
}
