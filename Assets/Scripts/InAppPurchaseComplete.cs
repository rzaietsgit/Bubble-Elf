using System.Collections;
using System.Collections.Generic;
using EasyMobile;
using ITSoft;
using UnityEngine;

public class InAppPurchaseComplete : MonoBehaviour
{
    public static InAppPurchaseComplete instance = null;
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            Destroy(gameObject);
        }


        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

    }
    void OnEnable()
    {
        IAPManager.PurchaseCompleted += PurchaseCompletedHandler;
        IAPManager.PurchaseFailed += PurchaseFailedHandler;
    }
    // Unsubscribe when the game object is disabled
    void OnDisable()
    {
        IAPManager.PurchaseCompleted -= PurchaseCompletedHandler;
        IAPManager.PurchaseFailed -= PurchaseFailedHandler;
    }

    // Successful purchase handler
    void PurchaseCompletedHandler(IAPProduct product)
    {

    switch (product.Name)
        {
            case EM_IAPConstants.Product_bubble_lb1:
                InitAndroid.action.doChainePay("Bubble_LB1");
                break;
            case EM_IAPConstants.Product_bubble_lb2:
                InitAndroid.action.doChainePay("Bubble_LB2");
                break;
            case EM_IAPConstants.Product_bubble_lb3:
                InitAndroid.action.doChainePay("Bubble_LB3");
                break;
            case EM_IAPConstants.Product_bubble_gold4:
                InitAndroid.action.doChainePay("Bubble_GOLD4");
                break;
            case EM_IAPConstants.Product_bubble_gold5:
                InitAndroid.action.doChainePay("Bubble_GOLD5");
                break;
            case EM_IAPConstants.Product_adsfree:
                InitAndroid.action.doChainePay("Adsfree");
                AdsManager.RemoveAds();
                break;
            case EM_IAPConstants.Product_bubble_gold1:
                InitAndroid.action.doChainePay("Bubble_GOLD1");
                break;
            case EM_IAPConstants.Product_bubble_gold2:
                InitAndroid.action.doChainePay("Bubble_GOLD2");
                break;
            case EM_IAPConstants.Product_bubble_gold3:
                InitAndroid.action.doChainePay("Bubble_GOLD3");
                break;
            case EM_IAPConstants.Product_fivelives:
                InitAndroid.action.doChainePay("Fivelives");
                break;
            case EM_IAPConstants.Product_twohourunlimitedlives:
                InitAndroid.action.doChainePay("TwoHourUnlimitedLives");
                break;
            case EM_IAPConstants.Product_buybubble2:
                InitAndroid.action.doChainePay("BuyBubble2");
                break;
            case EM_IAPConstants.Product_bubble_lb4:
                InitAndroid.action.doChainePay("Bubble_LB4");
                break;
            case EM_IAPConstants.Product_buybubble1:
                InitAndroid.action.doChainePay("BuyBubble1");
                break;
            case EM_IAPConstants.Product_yiyuantehuilibao:
                InitAndroid.action.doChainePay("yiyuantehuilibao");
                break;
            case EM_IAPConstants.Product_vipplayer:
                InitAndroid.action.doChainePay("VipPlayer");
                break;
            case EM_IAPConstants.Product_gameskill1:
                InitAndroid.action.doChainePay("GameSkill1");
                break;
            case EM_IAPConstants.Product_gameskill2:
                InitAndroid.action.doChainePay("GameSkill2");
                break;
            case EM_IAPConstants.Product_gameskill3:
                InitAndroid.action.doChainePay("GameSkill3");
                break;
            case EM_IAPConstants.Product_gameskill4:
                InitAndroid.action.doChainePay("GameSkill4");
                break;
            case EM_IAPConstants.Product_gameskill5:
                InitAndroid.action.doChainePay("GameSkill5");
                break;
            case EM_IAPConstants.Product_gameskill6:
                InitAndroid.action.doChainePay("GameSkill6");
                break;
            case EM_IAPConstants.Product_buygang1:
                InitAndroid.action.doChainePay("BuyGang1");
                break;
            case EM_IAPConstants.Product_buygang2:
                InitAndroid.action.doChainePay("BuyGang2");
                break;
            case EM_IAPConstants.Product_buygang3:
                InitAndroid.action.doChainePay("BuyGang3");
                break;
            case EM_IAPConstants.Product_buygang4:
                InitAndroid.action.doChainePay("BuyGang4");
                break;
            case EM_IAPConstants.Product_first_pay:
                InitAndroid.action.doChainePay("First_Pay");
                break;
            case EM_IAPConstants.Product_live_pack:
                InitAndroid.action.doChainePay("Live_Pack");
                break;
            case EM_IAPConstants.Product_any_way_pack_1:
                InitAndroid.action.doChainePay("Any_Way_Pack_1");
                break;
            case EM_IAPConstants.Product_any_way_pack_2:
                InitAndroid.action.doChainePay("Any_Way_Pack_2");
                break;
            case EM_IAPConstants.Product_any_way_pack_3:
                InitAndroid.action.doChainePay("Any_Way_Pack_3");
                break;
            case EM_IAPConstants.Product_pack_pass_12:
                InitAndroid.action.doChainePay("Pack_Pass_12");
                break;
            case EM_IAPConstants.Product_pack_pass_30:
                InitAndroid.action.doChainePay("Pack_Pass_30");
                break;
            case EM_IAPConstants.Product_pack_pass_68:
                InitAndroid.action.doChainePay("Pack_Pass_68");
                break;
            case EM_IAPConstants.Product_pack_pass_128:
                InitAndroid.action.doChainePay("Pack_Pass_128");
                break;
            case EM_IAPConstants.Product_pack_pass_328:
                InitAndroid.action.doChainePay("Pack_Pass_328");
                break;
            case EM_IAPConstants.Product_pack_pass_double_6:
                InitAndroid.action.doChainePay("Pack_Pass_Double_6");
                break;
            case EM_IAPConstants.Product_pack_pass_double_12:
                InitAndroid.action.doChainePay("Pack_Pass_Double_12");
                break;
            case EM_IAPConstants.Product_pack_pass_double_30:
                InitAndroid.action.doChainePay("Pack_Pass_Double_30");
                break;
            case EM_IAPConstants.Product_pack_pass_double_68:
                InitAndroid.action.doChainePay("Pack_Pass_Double_68");
                break;
            case EM_IAPConstants.Product_pack_pass_double_128:
                InitAndroid.action.doChainePay("Pack_Pass_Double_128");
                break;
            case EM_IAPConstants.Product_pack_pass_double_328:
                InitAndroid.action.doChainePay("Pack_Pass_Double_328");
                break;
        }
    }
    // Failed purchase handler
    void PurchaseFailedHandler(IAPProduct product)
    {
        Debug.Log("The purchase of product " + product.Name + " has failed.");
    }
}
