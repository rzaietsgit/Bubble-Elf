using System.Runtime.InteropServices;
using UnityEngine;

public abstract class MetaUnity : MonoBehaviour
{
	internal const int YF_INIT_PLUGIN = 1118481;

	internal const int YF_SHOW_BANNER = 1;

	internal const int YF_HIDE_BANNER = 2;

	internal const int YF_SET_BANNER_POSITION = 3;

	internal const int YF_GET_IS_BANNER_SHOW = 4;

	internal const int YF_HAS_BANNER = 5;

	internal const int YF_GET_IS_INTERSTITIAL_SHOW = 1048577;

	internal const int YF_SHOW_INTERSTITIAL_COMMON = 1048578;

	internal const int YF_SHOW_INTERSTITIAL_SPECIAL = 1048579;

	internal const int YF_HAS_INTERSTITIAL_GIFT = 1048580;

	internal const int YF_SHOW_INTERSTITIAL_GIFT = 1048581;

	internal const int YF_SHOW_TYPE_INTERSTITIAL = 1048582;

	internal const int YF_SHOW_TYPE_INTERSTITIAL_SPECIAL = 1048583;

	internal const int YF_HAS_VIDEO = 2097153;

	internal const int YF_SHOW_VIDEO = 2097154;

	internal const int YF_SET_UNITY_ZONE_ID = 2097155;

	internal const int YF_GET_IS_VIDEO_SHOW = 2097156;

	internal const int YF_HAS_VIDEO_OR_TASK = 2097157;

	internal const int YF_HAS_NATIVE = 3145729;

	internal const int YF_SHOW_NATIVE = 3145730;

	internal const int YF_HIDE_NATIVE = 3145731;

	internal const int YF_SELF_NATIVE_CLICK = 3145732;

	internal const int YF_GET_NATIVE_DATA = 3145733;

	internal const int YF_SET_NATIVE_BG_ENABLE = 3145734;

	internal const int YF_GET_NATIVE_DATAS = 3145735;

	internal const int YF_NATIVE_CLICK = 3145736;

	internal const int YF_SET_SCALE_ENABLE = 3145737;

	internal const int YF_SET_ICON = 4194305;

	internal const int YF_HAS_ICON = 4194306;

	internal const int YF_SHOW_ICON = 4194307;

	internal const int YF_HIDE_ICON = 4194308;

	internal const int YF_ICON_CLICK = 4194309;

	internal const int YF_SET_ICON_SCALE_ENABLE = 4194310;

	internal const int YF_HAS_MORE = 5242881;

	internal const int YF_SHOW_MORE = 5242882;

	internal const int YF_HAS_OFFER = 6291457;

	internal const int YF_SHOW_OFFER = 6291458;

	internal const int YF_SET_COIN_UNIT = 6291459;

	internal const int YF_SET_COIN_CURRENCY = 6291460;

	internal const int YF_SET_EXE_TASK_REWARD = 6291461;

	internal const int YF_SHOW_TASK = 6291462;

	internal const int YF_HAS_FOLLOW_TASK = 7340033;

	internal const int YF_HAS_FOLLOW_TASK_FOR_FEATURE = 7340034;

	internal const int YF_CLICK_FOLLOW_TASK_FOR_FEATURE = 7340035;

	internal const int YF_SET_UMENG = 8388609;

	internal const int YF_VN_UMENG_COUNTEVENT = 8388611;

	internal const int YF_VN_UMENG_LEVELSTART = 8388612;

	internal const int YF_VN_UMENG_LEVELEND = 8388613;

	internal const int YF_VN_UMENG_LEVELFAIL = 8388614;

	internal const int YF_SET_FACEBOOK_TRACK_APPID = 9437185;

	internal const int YF_SET_ADJUST_WITH_APPTOKEN = 10485761;

	internal const int YF_GET_CHECK_CTRL = 11534337;

	internal const int YF_SET_LEVEL = 11534338;

	internal const int YF_GET_ONLINE_PARAM = 11534339;

	internal const int YF_SET_AUTO_ROTATE_ENABLE = 11534340;

	internal const int YF_SET_POSITION_OF_L_P = 11534341;

	internal const int YF_GET_AREA_CODE = 11534343;

	internal const int YF_SET_PUSH_ENABLE = 11534344;

	internal const int YF_CLEAR_FOLLOW = 12582913;

	internal const int YF_SET_ADMOB_TEST_ID = 12582914;

	internal const int YF_SET_FACEBOOK_TEST_ID = 12582915;

	internal const int YF_CLEAR_INSTALLED_APP = 12582916;

	internal const int YF_CLEAR_DEBUG_HELPER = 12582917;

	internal const int YF_IAP_RESTORE = 13631489;

	internal const int YF_IAP_BUY_ITEM = 13631490;

	internal const int YF_RATE = 13631491;

	internal const int YF_CAN_SHARE_TO = 13631492;

	internal const int YF_SHARE_TO = 13631493;


	private static extern string _invokeMeta(int type, string msg);

	public static string invokeMeta(int type, string msg = "")
	{
		return string.Empty;
	}

	internal abstract void invokeMetaCallback(int type, string msg);

	private void invokeMetaCallback(string msg)
	{
		int num = msg.IndexOf(',');
		int type = int.Parse(msg.Substring(0, num));
		string msg2 = msg.Substring(num + 1);
		invokeMetaCallback(type, msg2);
	}
}
