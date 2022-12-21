using System;
using System.Collections;
using System.Collections.Generic;
//using Firebase;
//using Firebase.Analytics;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener,
    IUnityAdsShowListener
{
    public static AdsManager Instance;

    [Header("Admob IDs")] public string admob_AppID;

    public string admob_BannerID;
    public string admob_MediumRectBannerID;

    public string admob_SplashInterID;
    public string admob_MainMenuInterID;
    public string admob_GamePlayInterID;

    public string admobID_RewardedVideo;

    private BannerView smallBannerView;
    private BannerView largeBannerView;

    private InterstitialAd interstitialSplash;
    private InterstitialAd interstitialGamePlay;
    public InterstitialAd interstitialMainMenu;

    public RewardedAd rewardedAd;

    [Header("Test Ads")] public bool useTestAds;

    [Header("Unity")] [SerializeField] string androidGameID;
    private string _unityId;

    [Header("Unity Placement IDs")] [SerializeField]
    string unityInterstitialPlacementID = "Interstitial_Android";

    [SerializeField] string unityRewardedPlacementID = "Rewarded_Android";

    [Header("Unity Events")] public UnityEvent onAdLoadedEvent;
    public UnityEvent onAdFailedToLoadEvent;
    public UnityEvent onAdOpeningEvent;
    public UnityEvent onAdFailedToShowEvent;
    public UnityEvent onUserEarnedRewardEvent;
    public UnityEvent onAdClosedEvent;


    public delegate void RewardCompleteEvent();

    private RewardCompleteEvent OnAdCompleteEvent_Rewarded;

    private bool isInternetAvailable = false;
    private bool isAdsInitialized = false;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        if (useTestAds)
        {
            admob_AppID = "ca-app-pub-3940256099942544~3347511713";
            admob_BannerID = "ca-app-pub-3940256099942544/6300978111";
            admob_MediumRectBannerID = "ca-app-pub-3940256099942544/6300978111";
            admob_SplashInterID = "ca-app-pub-3940256099942544/1033173712";
            admob_MainMenuInterID = "ca-app-pub-3940256099942544/1033173712";
            admob_GamePlayInterID = "ca-app-pub-3940256099942544/1033173712";
            admobID_RewardedVideo = "ca-app-pub-3940256099942544/5224354917";
        }
    }

    void Start()
    {
        MobileAds.SetiOSAppPauseOnBackground(true);

        if (IsInternetConnection())
        {
            InitializeAds();
            RequestAndLoadRewardedAd();
            //RequestAndLoadSplashInterstitial();
            RequestAndLoadMainMenuInterstitial();
           // RequestAndLoadGameplayInterstitial();
            RequestBannerAdTop(GoogleMobileAds.Api.AdPosition.Top);


            Init_Firebase();
        }
        else
        {
            isAdsInitialized = false;
        }
    }

    #region  Ads Initialization

    public void InitializeAds()
    {
        try
        {
            MobileAds.Initialize(HandleInitCompleteAction);
            if (SystemInfo.systemMemorySize > 4000)
            {
               // InitializeUnityAds();
            }

            isAdsInitialized = true;
        }
        catch (Exception e)
        {
            return;
        }
    }

    private void HandleInitCompleteAction(InitializationStatus initstatus)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (PlayerPrefs.GetInt("RemoveAds") == 0)
            {
                RequestAndLoadSplashInterstitial();
            }
        });
    }

    public void InitializeUnityAds()
    {
        _unityId = androidGameID;
        Advertisement.Initialize(_unityId, useTestAds, this);
    }

    void Init_Firebase()
    {
        //FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        //{
            //FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        //});
    }

    public bool CheckInitialization()
    {
        if (isAdsInitialized)
        {
            isAdsInitialized = true;
            return isAdsInitialized;
        }
        else
        {
            isAdsInitialized = false;
            InitializeAds();
            return false;
        }
    }

    public bool IsInternetConnection()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable ||
            Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork ||
            Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            isInternetAvailable = true;
        }
        else
            isInternetAvailable = false;

        return isInternetAvailable;
    }

    #endregion

    #region Helper Method

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();
    }

    #endregion

    #region Small Banner

    public void RequestBannerAdTop(AdPosition adPosition)
    {
        try
        {
            if (smallBannerView != null)
            {
                smallBannerView.Destroy();
            }

            smallBannerView = new BannerView(admob_BannerID, AdSize.Banner, adPosition);

            smallBannerView.OnAdLoaded += (sender, args) => { onAdLoadedEvent.Invoke(); };
            smallBannerView.OnAdFailedToLoad += (sender, args) => { onAdFailedToLoadEvent.Invoke(); };
            smallBannerView.OnAdOpening += (sender, args) => { onAdOpeningEvent.Invoke(); };
            smallBannerView.OnAdClosed += (sender, args) => { onAdClosedEvent.Invoke(); };
            smallBannerView.OnPaidEvent += (sender, args) =>
            {
                string msg = string.Format("{0} (currency: {1}, value: {2}",
                    "Banner ad received a paid event.",
                    args.AdValue.CurrencyCode,
                    args.AdValue.Value);
            };

            if (PlayerPrefs.GetInt("RemoveAds") != 1)
            {
                smallBannerView.LoadAd(CreateAdRequest());
            }

           //FirebaseAnalytics.LogEvent("Small_Banner_Requested_And_Showed");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DestroyBannerAdTop()
    {
        if (smallBannerView != null)
        {
            smallBannerView.Destroy();
            //FirebaseAnalytics.LogEvent("Small_Banner_Destroyed");
        }
    }

    #endregion

    #region Large Banner

    public void RequestBannerAdMedium(AdPosition adPosition)
    {
        try
        {
            if (largeBannerView != null)
            {
                largeBannerView.Destroy();
            }

            largeBannerView = new BannerView(admob_MediumRectBannerID, AdSize.MediumRectangle, adPosition);

            largeBannerView.OnAdLoaded += (sender, args) => { onAdLoadedEvent.Invoke(); };
            largeBannerView.OnAdFailedToLoad += (sender, args) => { onAdFailedToLoadEvent.Invoke(); };
            largeBannerView.OnAdOpening += (sender, args) => { onAdOpeningEvent.Invoke(); };
            largeBannerView.OnAdClosed += (sender, args) => { onAdClosedEvent.Invoke(); };
            largeBannerView.OnPaidEvent += (sender, args) =>
            {
                string msg = string.Format("{0} (currency: {1}, value: {2}",
                    "Banner ad received a paid event.",
                    args.AdValue.CurrencyCode,
                    args.AdValue.Value);
            };

            if (PlayerPrefs.GetInt("RemoveAds") != 1)
            {
                largeBannerView.LoadAd(CreateAdRequest());
            }

          //  FirebaseAnalytics.LogEvent("Large_Banner_Requested_And_Showed");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DestroyBannerAdMedium()
    {
        if (largeBannerView != null)
        {
            largeBannerView.Destroy();
            //FirebaseAnalytics.LogEvent("Large_Banner_Destroyed");
        }
    }

    #endregion

    #region Splash Interstitial

    public void RequestAndLoadSplashInterstitial()
    {
        try
        {
            if (interstitialSplash != null)
            {
                interstitialSplash.Destroy();
            }

            interstitialSplash = new InterstitialAd(admob_SplashInterID);

            interstitialSplash.OnAdLoaded += (sender, args) => { onAdLoadedEvent.Invoke(); };
            interstitialSplash.OnAdFailedToLoad += (sender, args) => { onAdFailedToLoadEvent.Invoke(); };
            interstitialSplash.OnAdOpening += (sender, args) => { onAdOpeningEvent.Invoke(); };
            interstitialSplash.OnAdClosed += (sender, args) => { onAdClosedEvent.Invoke(); };
            interstitialSplash.OnAdDidRecordImpression += (sender, args) => { };
            interstitialSplash.OnAdFailedToShow += (sender, args) => { };
            interstitialSplash.OnPaidEvent += (sender, args) =>
            {
                string msg = string.Format("{0} (currency: {1}, value: {2}",
                    "Interstitial ad received a paid event.",
                    args.AdValue.CurrencyCode,
                    args.AdValue.Value);
            };

            if (PlayerPrefs.GetInt("RemoveAds") == 0)
            {
                interstitialSplash.LoadAd(CreateAdRequest());
            }

         //   FirebaseAnalytics.LogEvent("Admob_Splash_Interstitial_Requested");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void ShowSplashInterstitialAd()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {

            if (IsInternetConnection())
            {
                if (CheckInitialization())
                {
                    try
                    {
                        if (interstitialSplash != null && interstitialSplash.IsLoaded())
                        {
                            interstitialSplash.Show();

                           // FirebaseAnalytics.LogEvent("Admob_Splash_Interstitial_Showed");
                        }

                        else
                            return;
                    }
                    catch (Exception e)
                    {
                        return;
                    }
                }
            }
        }
    }

    public void DestroySplashInterstitialAd()
    {
        if (interstitialSplash != null)
        {
            interstitialSplash.Destroy();
        }
    }

    #endregion

     #region Main Menu Interstitial

    public void RequestAndLoadMainMenuInterstitial()
    {
        if (interstitialMainMenu != null)
        {
            interstitialMainMenu.Destroy();
        }

        interstitialMainMenu = new InterstitialAd(admob_MainMenuInterID);

        interstitialMainMenu.OnAdLoaded += (sender, args) => { onAdLoadedEvent.Invoke(); };
        interstitialMainMenu.OnAdFailedToLoad += (sender, args) => {

            //FindObjectOfType<First_Screen_Handle>().loadindscreenoff();

            //if (startmenu.shopad== true)
            //{
            //    FindObjectOfType<startmenu>().Openshop();
            //    RequestAndLoadMainMenuInterstitial();
            //}
           
            //else if (startmenu.playad == true)
            //{
            //    FindObjectOfType<startmenu>().gamelevelon();
            //    RequestAndLoadMainMenuInterstitial();
            //}
            //else if (startmenu.selectlevelads == true)
            //{
            //    FindObjectOfType<startmenu>().selectlevelpanel();
                RequestAndLoadMainMenuInterstitial();
            //}


        };
        interstitialMainMenu.OnAdOpening += (sender, args) => { onAdOpeningEvent.Invoke(); };
        //interstitialMainMenu.OnAdClosed += (sender, args) => { onAdClosedEvent.Invoke(); };
        interstitialMainMenu.OnAdClosed += (sender, args) => {

            //FindObjectOfType<First_Screen_Handle>().loadindscreenoff();
            //if (startmenu.shopad == true)
            //{
            //    FindObjectOfType<startmenu>().Openshop();
            //    RequestAndLoadMainMenuInterstitial();
            //}
           
            //else if (startmenu.playad == true)
            //{
            //    FindObjectOfType<startmenu>().gamelevelon();
            //    RequestAndLoadMainMenuInterstitial();
            //}
            //else if (startmenu.selectlevelads == true)
            //{
            //    FindObjectOfType<startmenu>().selectlevelpanel();
                RequestAndLoadMainMenuInterstitial();
   
            //}






        };
        interstitialMainMenu.OnAdDidRecordImpression += (sender, args) => { };
        interstitialMainMenu.OnAdFailedToShow += (sender, args) => { };
        interstitialMainMenu.OnPaidEvent += (sender, args) =>
        {
            string msg = string.Format("{0} (currency: {1}, value: {2}",
                "Interstitial ad received a paid event.",
                args.AdValue.CurrencyCode,
                args.AdValue.Value);
        };

        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            interstitialMainMenu.LoadAd(CreateAdRequest());
        }

      // FirebaseAnalytics.LogEvent("Admob_MainMenu_Interstitial_Requested");
    }

    public void ShowMainMenuInterstitial()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            
            if (interstitialMainMenu != null && interstitialMainMenu.IsLoaded())
            {


                if (IsInternetConnection())
                {
                    if (CheckInitialization())
                    {
                        try
                        {
                            if (interstitialMainMenu != null && interstitialMainMenu.IsLoaded())
                            {
                                interstitialMainMenu.Show();

                                //FirebaseAnalytics.LogEvent("Admob_MainMenu_Interstitial_Showed");
                            }

                            else
                                return;
                        }
                        catch (Exception e)
                        {
                            return;
                        }
                    }


                    else
                    {
                        //FindObjectOfType<First_Screen_Handle>().loadindscreenoff();

                        //if (startmenu.shopad == true)
                        //{
                        //    FindObjectOfType<startmenu>().Openshop();
                        //    RequestAndLoadMainMenuInterstitial();
                        //}
                       
                        //else if (startmenu.playad == true)
                        //{
                        //    FindObjectOfType<startmenu>().gamelevelon();
                        //    RequestAndLoadMainMenuInterstitial();
                        //}
                        //else if (startmenu.selectlevelads == true)
                        //{
                        //    FindObjectOfType<startmenu>().selectlevelpanel();
                            RequestAndLoadMainMenuInterstitial();
                        //}
                    }

                }
                else
                {
                    //FindObjectOfType<First_Screen_Handle>().loadindscreenoff();

                    //if (startmenu.shopad == true)
                    //{
                    //    FindObjectOfType<startmenu>().Openshop();
                    //    RequestAndLoadMainMenuInterstitial();
                    //}
                   
                    //else if (startmenu.playad == true)
                    //{
                    //    FindObjectOfType<startmenu>().gamelevelon();
                    //    RequestAndLoadMainMenuInterstitial();
                    //}
                    //else if (startmenu.selectlevelads == true)
                    //{
                    //    FindObjectOfType<startmenu>().selectlevelpanel();
                        RequestAndLoadMainMenuInterstitial();
                    //}


                }
            }
            else
            {
                //FindObjectOfType<First_Screen_Handle>().loadindscreenoff();

                //if (startmenu.shopad == true)
                //{
                //    FindObjectOfType<startmenu>().Openshop();
                //    RequestAndLoadMainMenuInterstitial();
                //}
               
                //else if (startmenu.playad == true)
                //{
                //    FindObjectOfType<startmenu>().gamelevelon();
                //    RequestAndLoadMainMenuInterstitial();
                //}
                //else if (startmenu.selectlevelads == true)
                //{
                //    FindObjectOfType<startmenu>().selectlevelpanel();
                    RequestAndLoadMainMenuInterstitial();
                //}

            }
        }
    }

    public void DestroyMainMenuInterstitial()
    {
        if (interstitialMainMenu != null)
        {
            interstitialMainMenu.Destroy();
        }
    }

    #endregion

    #region Gameplay Interstitial

  /*  public void RequestAndLoadGameplayInterstitial()
    {
        try
        {
            if (interstitialGamePlay != null)
            {
                interstitialGamePlay.Destroy();
            }

            interstitialGamePlay = new InterstitialAd(admob_GamePlayInterID);

            interstitialGamePlay.OnAdLoaded += (sender, args) => { onAdLoadedEvent.Invoke(); };
            interstitialGamePlay.OnAdFailedToLoad += (sender, args) => {
                FindObjectOfType<gameplay>().loadindscreenoff();
                if (gameplay.winads == true)
                {
                    FindObjectOfType<gameplay>().Nextlevel();
                    RequestAndLoadGameplayInterstitial();
                }
                else if (gameplay.failads == true)
                {
                    FindObjectOfType<gameplay>().Gamevover();
                    RequestAndLoadGameplayInterstitial();
                }
                else if (gameplay.pauseads == true)
                {
                    FindObjectOfType<gameplay>().pause();
                    RequestAndLoadGameplayInterstitial();
                }

            };
            interstitialGamePlay.OnAdOpening += (sender, args) => { onAdOpeningEvent.Invoke(); };
            //interstitialGamePlay.OnAdClosed += (sender, args) => { onAdClosedEvent.Invoke(); };
            interstitialGamePlay.OnAdClosed += (sender, args) => {
                FindObjectOfType<gameplay>().loadindscreenoff();
                if (gameplay.winads == true)
                {
                    FindObjectOfType<gameplay>().Nextlevel();
                    RequestAndLoadGameplayInterstitial();
                }
                else if (gameplay.failads == true)
                {
                    FindObjectOfType<gameplay>().Gamevover();
                    RequestAndLoadGameplayInterstitial();
                }
                else if (gameplay.pauseads == true)
                {
                    FindObjectOfType<gameplay>().pause();
                    RequestAndLoadGameplayInterstitial();
                }

            };
            interstitialGamePlay.OnAdDidRecordImpression += (sender, args) => { };
            interstitialGamePlay.OnAdFailedToShow += (sender, args) => { };
            interstitialGamePlay.OnPaidEvent += (sender, args) =>
            {
                string msg = string.Format("{0} (currency: {1}, value: {2}",
                    "Interstitial ad received a paid event.",
                    args.AdValue.CurrencyCode,
                    args.AdValue.Value);
            };

            if (PlayerPrefs.GetInt("RemoveAds") == 0)
            {
                interstitialGamePlay.LoadAd(CreateAdRequest());
            }

           //FirebaseAnalytics.LogEvent("Admob_Gameplay_Interstitial_Requested");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void ShowGameplayInterstitial()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            if (interstitialGamePlay != null && interstitialGamePlay.IsLoaded())
            {


                if (IsInternetConnection())
                {
                    if (CheckInitialization())
                    {
                        try
                        {
                            if (interstitialGamePlay != null && interstitialGamePlay.IsLoaded())
                            {
                                interstitialGamePlay.Show();
                                 //FirebaseAnalytics.LogEvent("Admob_GamePlay_Interstitial_Showed");
                            }

                            else
                                return;
                        }
                        catch (Exception e)
                        {
                            return;
                        }
                    }

                    else
                    {
                        FindObjectOfType<gameplay>().loadindscreenoff();
                        if (gameplay.winads == true)
                        {
                            FindObjectOfType<gameplay>().Nextlevel();
                            RequestAndLoadGameplayInterstitial();
                        }
                        else if (gameplay.failads == true)
                        {
                            FindObjectOfType<gameplay>().Gamevover();
                            RequestAndLoadGameplayInterstitial();
                        }
                        else if (gameplay.pauseads == true)
                        {
                            FindObjectOfType<gameplay>().pause();
                            RequestAndLoadGameplayInterstitial();
                        }
                    }
                }
                else
                {
                    FindObjectOfType<gameplay>().loadindscreenoff();
                    if (gameplay.winads == true)
                    {
                        FindObjectOfType<gameplay>().Nextlevel();
                        RequestAndLoadGameplayInterstitial();
                    }
                    else if (gameplay.failads == true)
                    {
                        FindObjectOfType<gameplay>().Gamevover();
                        RequestAndLoadGameplayInterstitial();
                    }
                    else if (gameplay.pauseads == true)
                    {
                        FindObjectOfType<gameplay>().pause();
                        RequestAndLoadGameplayInterstitial();
                    }
                }
            }
            else
            {
                FindObjectOfType<gameplay>().loadindscreenoff();
                if (gameplay.winads == true)
                {
                    FindObjectOfType<gameplay>().Nextlevel();
                    RequestAndLoadGameplayInterstitial();
                }
                else if (gameplay.failads == true)
                {
                    FindObjectOfType<gameplay>().Gamevover();
                    RequestAndLoadGameplayInterstitial();
                }
                else if (gameplay.pauseads == true)
                {
                    FindObjectOfType<gameplay>().pause();
                    RequestAndLoadGameplayInterstitial();
                }
            }
        }
    }

    public void DestroyGameplayInterstitial()
    {
        if (interstitialGamePlay != null)
        {
            interstitialGamePlay.Destroy();
        }
    }*/

    #endregion

    #region Rewarded Ads

    public void RequestAndLoadRewardedAd()
    {
        rewardedAd = new RewardedAd(admobID_RewardedVideo);

       // rewardedAd.OnAdLoaded += (sender, args) => { onAdLoadedEvent.Invoke();gameplay.claimcheck = true; };
        rewardedAd.OnAdFailedToLoad += (sender, args) => { onAdFailedToLoadEvent.Invoke(); RequestAndLoadRewardedAd(); };
        rewardedAd.OnAdOpening += (sender, args) => { onAdOpeningEvent.Invoke(); };
        rewardedAd.OnAdFailedToShow += (sender, args) => { onAdFailedToShowEvent.Invoke();  };
        // rewardedAd.OnAdClosed += (sender, args) => { onAdClosedEvent.Invoke(); };
        rewardedAd.OnAdClosed += (sender, args) => { RequestAndLoadRewardedAd(); };
        // rewardedAd.OnUserEarnedReward += (sender, args) => { onUserEarnedRewardEvent.Invoke(); };
       // rewardedAd.OnUserEarnedReward += (sender, args) => { FindObjectOfType<gameplay>().claimcoins(); };
        rewardedAd.OnAdDidRecordImpression += (sender, args) => { };
        rewardedAd.OnPaidEvent += (sender, args) =>
        {
            string msg = string.Format("{0} (currency: {1}, value: {2}",
                "Rewarded ad received a paid event.",
                args.AdValue.CurrencyCode,
                args.AdValue.Value);
        };

        rewardedAd.LoadAd(CreateAdRequest());
        

       // FirebaseAnalytics.LogEvent("Admob_RewardBased_Video_Requested");
    }
    //private void ShowRewarded(RewardCompleteEvent unityEvent)
    private void ShowRewarded()
    {
        print("reward add show function called");
        // OnAdCompleteEvent_Rewarded = unityEvent;
        if (rewardedAd != null && rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
            print("reward add show");
            //FirebaseAnalytics.LogEvent("Admob_RewardBased_Video_Showed");
        }
        else
        {
            //  DoubleMoneyScript.rewardedAdButtonClicked = false;
            //GUI_MainMenu_Script.rewardedAdButtonClicked = false;
           
           // FirebaseAnalytics.LogEvent("Admob_RewardBased_Video_Failed_And_Requested_Again");
        }
    }

    public void RewardUser()
    {
        OnAdCompleteEvent_Rewarded?.Invoke();
    }

    public void ShowAdmobRewarded()
    {
       // ShowRewarded(GiveReward);
        ShowRewarded();
    }

    #endregion

    #region AD Inspector

    public void OpenAdInspector()
    {
        MobileAds.OpenAdInspector((error) =>
        {
            if (error != null)
            {
//                PrintStatus("ad Inspector failed to open with error: " + error);
            }
            else
            {
//                PrintStatus("Ad Inspector opened successfully.");
            }
        });
    }

    #endregion

    #region Unity Ads

    public void LoadUnityInterstitial()
    {
        Advertisement.Load(unityInterstitialPlacementID, this);
    }

    public void ShowUnityInterstitial()
    {
        Advertisement.Show(unityInterstitialPlacementID, this);
    }

    public void LoadUnityRewarded()
    {
        Advertisement.Load(unityRewardedPlacementID, this);
    }

    public void ShowUnityRewarded()
    {
        Advertisement.Show(unityRewardedPlacementID, this);
    }

    #endregion

    #region  Unity Initialization

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        if (PlayerPrefs.GetInt("RemoveAds") != 1)
        {
            LoadUnityInterstitial();
        }

        LoadUnityRewarded();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    #endregion

    #region Unity CallBacks

    public void OnUnityAdsAdLoaded(string placementId)
    {
        //Advertisement.Show(placementId, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");

      //  DoubleMoneyScript.rewardedAdButtonClicked = false;
       // GUI_MainMenu_Script.rewardedAdButtonClicked = false;
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("OnUnityAdsShowFailure");
       // DoubleMoneyScript.rewardedAdButtonClicked = false;
      // GUI_MainMenu_Script.rewardedAdButtonClicked = false;
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("OnUnityAdsShowStart");
        Time.timeScale = 0.0f;
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("OnUnityAdsShowClick");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("OnUnityAdsShowComplete: " + showCompletionState);
        if (placementId.Equals(unityRewardedPlacementID) &&
            showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Reward Player");
           // GiveReward();
            LoadUnityRewarded();
        }
        else
        {
            LoadUnityInterstitial();
        }

        Time.timeScale = 1.0f;
    }

    #endregion

    private void GiveReward()
    {
        //if (DoubleMoneyScript.rewardedAdButtonClicked)
        //{
        //    DoubleMoneyScript.rewardedAdSeen = true;
        //    DoubleMoneyScript.rewardedAdButtonClicked = false;
        //    GUI_RewardedAdShowPanelScript.ShowAdOnce = false;
        //}
        //else if (GUI_MainMenu_Script.rewardedAdButtonClicked)
        //{
        //    GUI_MainMenu_Script.freeCashRewardedSeen = true;
        //    GUI_MainMenu_Script.rewardedAdButtonClicked = false;
        //    GUI_RewardedAdShowPanelScript.ShowAdOnce = false;
        //}
    }

//    public void ShowUnityAdmobRewarded()
//    {
//        if (IsInternetConnection())
//        {
//            if (CheckInitialization())
//            {
//                try
//                {
//                    if (rewardedAd != null && rewardedAd.IsLoaded())
//                    {
//                        ShowAdmobRewarded();
//
//                        FirebaseAnalytics.LogEvent("Admob_RewardBased_Video_Showed");
//                    }
//                    else if (Advertisement.isInitialized)
//                    {
//                        ShowUnityRewarded();
//
//                        FirebaseAnalytics.LogEvent("Unity_Rewarded_Showed_and_Admob_RewardBased_Video_Ad_Requested");
//                    }
//
//                    else
//                        return;
//                }
//                catch (Exception e)
//                {
//                    return;
//                }
//            }
//        }
//    }

    public void ShowUnityAdmobGameplay()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            if (IsInternetConnection())
            {
                if (CheckInitialization())
                {
                    try
                    {
                        if (interstitialGamePlay != null && interstitialGamePlay.IsLoaded())
                        {
                            interstitialGamePlay.Show();

                         //   FirebaseAnalytics.LogEvent("Admob_GamePlay_Interstitial_Showed");
                        }
                        else if (Advertisement.isInitialized)
                        {
                            ShowUnityInterstitial();
                            LoadUnityInterstitial();

                         //   FirebaseAnalytics.LogEvent(
                               // "Unity_Gameplay_Interstitial_Showed_and_New_Unity_Interstitial_Requested");
                        }

                        else
                            return;
                    }
                    catch (Exception e)
                    {
                        return;
                    }
                }
            }
        }
    }

    public void ShowUnityAdmobMain()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            if (IsInternetConnection())
            {
                if (CheckInitialization())
                {
                    try
                    {
                        if (interstitialMainMenu != null && interstitialMainMenu.IsLoaded())
                        {
                            interstitialMainMenu.Show();

                          //  FirebaseAnalytics.LogEvent("Admob_MainMenu_Interstitial_Showed");
                        }
                        else if (Advertisement.isInitialized)
                        {
                            ShowUnityInterstitial();
                            LoadUnityInterstitial();

                           // FirebaseAnalytics.LogEvent(
                               // "Unity_MainMenu_Interstitial_Showed_and_New_Unity_Interstitial_Requested");
                        }

                        else
                            return;
                    }
                    catch (Exception e)
                    {
                        return;
                    }
                }
            }
        }
    }
}