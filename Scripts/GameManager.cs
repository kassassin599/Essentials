using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public enum QualityLevel
  {
    Low,
    Medium,
    High,
    VeryHigh
  }

  public enum StadiumMode
  {
    Morning,
    Afternoon,
    Evening,
    Dusk,
    Night
  }

  public enum StadiumTags
  {
    //Stadium_Wankhede,
    //Stadium_Lords,
    //Stadium_Melbourne,
    Stadium_Capetown,
    //Stadium_Pune

    // Stadium_Perth
  }

  public string[] StadiumSKUs =
    {
      "Stadium_Wankhede",
      "Stadium_Lords",
      "Stadium_Melbourne",
      "Stadium_Capetown",
      "Stadium_Pune",

      "Stadium_Perth"
    };

  public enum BattingCam
  {
    Normal,
    //BroadCast Cam
    Nets,
    //Close Cam
    Helmet,
    //Pro Cam
    TOTAL
  }

  public enum BowlingCam
  {
    Normal,
    Nets,
    TOTAL
  }

  public static ControlType currentControlType { private set; get; }

  private string toastString;
  private string input;
  //private AndroidJavaObject currentActivity = null;
  //private AndroidJavaClass UnityPlayer = null;
  //private AndroidJavaObject context = null;
  //Game Controls Setting....
  [HideInInspector]
  public QualityLevel qualityLevel = QualityLevel.High;

  [HideInInspector]
  public bool bIsCommentaryLanguageEnglish = true;
  [HideInInspector]
  public bool bIsVibration = true;
  [HideInInspector]
  public bool bIsShowBallType = true;
  public bool bIsPlayCommentary = false;
  [HideInInspector]
  public bool IsSwipeControlActive = false;
  [HideInInspector]
  public bool IsProControlActive = false;

  private GamePlayController gamePlayController;

  //[HideInInspector]
  //public int iGameControlIndex;
  private List<TeamInfo> m_TeamInfo;

  public List<TeamInfo> TeamInfo { get { return m_TeamInfo; } }

  [HideInInspector]
  public Defines.GameMode GameMode;

  [HideInInspector]
  public Defines.MatchType matchType = Defines.MatchType.NORMAL;

  [HideInInspector]
  public Defines.TournamentType TournamentType;

  private UINetsMatchSettings.Settings m_NetsSettings;

  [HideInInspector]
  public UINetsMatchSettings.Settings NetsSettings { get { return m_NetsSettings; } set { m_NetsSettings = value; } }

  [HideInInspector]
  public bool isUserWonTheToss = false;
  //Game Entry related RMS variables
  [HideInInspector]
  public int userTeam;
  [HideInInspector]
  public int winnerTeam;
  [HideInInspector]
  public int opponentTeam;
  [HideInInspector]
  public UIMatchSettings.Settings matchSettings;
  [HideInInspector]
  public bool isUserBatting = false;
  [HideInInspector]
  public bool isMatchActive = false;
  [HideInInspector]
  public Defines.TossResult tossResult;
  [HideInInspector]
  public List<int> userSelectedSquadIndices;
  [HideInInspector]
  public GameEntryPrefs gameEntryPrefs;
  [HideInInspector]
  public bool isTossScreenShown;
  [HideInInspector]
  public bool isLastGameSeen;
  [HideInInspector]
  public bool comingFromMainMenu;

  // for nets
  [HideInInspector]
  public int iNetsBatsmanType;
  [HideInInspector]
  public int iNetsBatsmanHand;
  [HideInInspector]
  public int iNetsBowlerType;
  [HideInInspector]
  public int iNetsBowlerHand;
  [HideInInspector]
  public int iNetsBowlerside;
  [HideInInspector]
  public int iNetsBattingNBowlingIndex;
  [HideInInspector]
  public float iNumberOfdaysGameInstall = 1.0f;
  [HideInInspector]
  public bool IsComeFromToss = false;
  [HideInInspector]
  public int iScoreInLastBall;

  [HideInInspector]
  public string stadiumName { get { return StadiumDescriptions[matchSettings.stadium]; } }

  private string[] StadiumDescriptions = { "LIVE FROM MUMBAI, INDIA", "LIVE FROM LONDON, UK", "LIVE FROM MELBOURNE, AUSTRALIA", "LIVE FROM CAPE TOWN, SOUTH AFRICA", "LIVE FROM PUNE, INDIA", "LIVE FROM WELLINGTON, NEW ZEALAND" };

  [HideInInspector]
  public int iSelectSuperOverBowler = 0;
  //// for cheat.
  //public float fTotalSessionCount = 0.0f;
  //public long fTotalHoursInGame;
  //public float fTotalGoals=0.0f;
  private UILoadingScreen m_UILoadingScreen = null;

  public UILoadingScreen UILoadingScreen
  {
    get
    {
      return m_UILoadingScreen;
    }
  }

  private bool increaseTimeScale = false;
  private float finalTimeScale = 0;
  [HideInInspector]
  public BattingCam BattingCamSelected = BattingCam.Normal;
  [HideInInspector]
  public BowlingCam BowlingCamSelected = BowlingCam.Normal;

  [HideInInspector]
  public GameEntryPrefs.PitchType pitchType;
  [HideInInspector]
  public bool IS_DEBUG_BUILD = true;
  [HideInInspector]
  public bool IsSignedOut = true;
  [HideInInspector]
  public bool IsShowPauseScreen = false;
  // press back button
  [HideInInspector]
  public bool IsShowDRS = false;
  // press back button
  [HideInInspector]
  public bool IsShowCutScene = false;
  // press back button
  [HideInInspector]
  public bool IsClassicControlsActive = false;

  [HideInInspector]
  public float UserRunRate = 0f;

  [HideInInspector]
  public bool IsPlaneShadowActive = false;

  public static bool isInitialized { private set; get; }

  public Mesh[] PlayerFaceMeshes;
  public Mesh PlayerBodyMesh;

  private float rewardMultiplier = 1.0f;

  private SoundManager soundManager;

  [SerializeField]
  private GameObject developerOverlay;

  public void setRewardMultiplier()
  {
    rewardMultiplier = FirebaseRemoteConfigHandler.Instance.GetRewardMultiplier();

    MainMenu mainMenu = FindObjectOfType<MainMenu>();

    if (mainMenu != null)
      mainMenu.updateRewardMultiplier();
  }

  public float RewardMultiplier
  {
    get
    {
      return rewardMultiplier;
    }
  }

  //    public float RewardMultiplier()
  //    {
  //        //return (float)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("reward_multiplier").DoubleValue;
  //#if !UNITY_EDITOR
  //                if (AzureManager.Instance.isInternetConnectionAvailable(false))
  //                    return (float)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("reward_multiplier").DoubleValue;
  //                else
  //                    return 1.0f;
  //#endif
  //        return 1.0f;
  //    }

  private static GameManager _instance = null;

  public static GameManager Instance
  {
    get
    {
      if (_instance == null)
      {
        LoggerUtil.LogError("GameManager is not initialized!");
        return null;
      }
      else
      {
        return _instance;

      }
    }
  }

  public StatsManager statsManager;
  public PlayerAnalytics playerAnalytics;
  private bool isGamePaused;
  private float timeScale;

  private int targetFPS = 60;

  [SerializeField]
  GameObject _permissionPopup;

  [SerializeField]
  Button _okPermissionButton;
  [SerializeField]
  Button _privacyPolicyButton;


  [SerializeField]
  GameObject _locationPermissionPopup;

  [SerializeField]
  Button _okLocationPermissionButton;

  [SerializeField]
  GameObject _locationPermissionDeniedPopup;
  [SerializeField]
  Button _okLocationPermissionDeniedButton;
  [SerializeField]
  Button _continueToGameLocationPermissionDeniedButton;
  [SerializeField]
  Button _settingsButton;

  void Awake()
  {
    if (_instance == null)
    {
      _instance = this;
      gameObject.name = "[Singleton]GameManager";
      DontDestroyOnLoad(gameObject);
      Initialize();
    }
    else
    {
      DestroyImmediate(this.gameObject);
      return;
    }
    _permissionPopup.SetActive(false);
#if !UNITY_EDITOR && UNITY_ANDROID
    //CheckForLocationPermission();
    CheckForPermission();
#endif
    _okLocationPermissionButton.onClick.AddListener(() =>
    {
      //PlayerPrefs.SetInt("OnDenyAndNeverAskAgainLocation", 1);
      _locationPermissionPopup.SetActive(false);
      LocationPermission();
    });
    _okPermissionButton.onClick.AddListener(() =>
    {
      PlayerPrefs.SetInt("OnDenyAndNeverAskAgain", 1);
      if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation))
        CheckForLocationPermission();
      _permissionPopup.SetActive(false);
    });

    _okLocationPermissionDeniedButton.onClick.AddListener(() =>
    {
      LocationPermission();
    });

    _continueToGameLocationPermissionDeniedButton.onClick.AddListener(() =>
    {
      //CheckForPermission();
      _locationPermissionDeniedPopup.SetActive(false);
    });
    _privacyPolicyButton.onClick.AddListener(() => Application.OpenURL("https://www.nautilusmobile.com/privacy-policy"));
    _settingsButton.onClick.AddListener(() =>
    {
      using var unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
      using AndroidJavaObject currentActivityObject = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
      string packageName = currentActivityObject.Call<string>("getPackageName");
      using var uriClass = new AndroidJavaClass("android.net.Uri");
      using AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("fromParts", "package", packageName, null);
      using var intentObject = new AndroidJavaObject("android.content.Intent", "android.settings.APPLICATION_DETAILS_SETTINGS", uriObject);
      intentObject.Call<AndroidJavaObject>("addCategory", "android.intent.category.DEFAULT");
      intentObject.Call<AndroidJavaObject>("setFlags", 0x10000000);
      currentActivityObject.Call("startActivity", intentObject);
    });
    if (PlayerPrefs.GetInt("OnDenyAndNeverAskAgain") == 1)
    {
      if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation))
        CheckForLocationPermission();
    }
  }

  internal void PermissionCallbacks_PermissionDeniedAndDontAskAgain(string permissionName)
  {
    _locationPermissionDeniedPopup.SetActive(true);
    _okLocationPermissionDeniedButton.gameObject.SetActive(false);
    _settingsButton.gameObject.SetActive(true);
    Debug.Log($"{permissionName} PermissionDeniedAndDontAskAgain");
  }

  internal void PermissionCallbacks_PermissionGranted(string permissionName)
  {
    //CheckForPermission();
    _locationPermissionDeniedPopup.SetActive(false);
    Debug.Log($"{permissionName} PermissionCallbacks_PermissionGranted");
  }

  internal void PermissionCallbacks_PermissionDenied(string permissionName)
  {
    _locationPermissionDeniedPopup.SetActive(true);
    Debug.Log($"{permissionName} PermissionCallbacks_PermissionDenied");
  }

  void LocationPermission()
  {
    if (Permission.HasUserAuthorizedPermission(Permission.CoarseLocation))
    {
      _locationPermissionPopup.SetActive(false);
      PlayerPrefs.SetInt("OnDenyAndNeverAskAgainLocation", 1);
    }
    else
    {
      bool useCallbacks = true;     //Using callbacks to trigger next action an enable disable popups 
      if (!useCallbacks)
      {
        Permission.RequestUserPermission(Permission.CoarseLocation);
      }
      else
      {
        var callbacks = new PermissionCallbacks();
        callbacks.PermissionDenied += PermissionCallbacks_PermissionDenied;
        callbacks.PermissionGranted += PermissionCallbacks_PermissionGranted;
        callbacks.PermissionDeniedAndDontAskAgain += PermissionCallbacks_PermissionDeniedAndDontAskAgain;
        Permission.RequestUserPermission(Permission.CoarseLocation, callbacks);
      }
    }
  }


  private void CheckForPermission()
  {
    Debug.Log("CheckForPermission Called!!!!");
    if (PlayerPrefs.HasKey("OnDenyAndNeverAskAgain"))
    {
      if (PlayerPrefs.GetInt("OnDenyAndNeverAskAgain") == 1)
      {
        _permissionPopup.SetActive(false);
        Debug.LogError("GET KEY Called!!!!");
        return;
      }
      else
      {
        _permissionPopup.SetActive(true);
        return;
      }
    }
    else
    {
      _permissionPopup.SetActive(true);
      return;
    }
  }

  private void CheckForLocationPermission()
  {
    Debug.Log("CheckForPermission Called!!!!");
    if (PlayerPrefs.HasKey("OnDenyAndNeverAskAgainLocation"))
    {
      if (PlayerPrefs.GetInt("OnDenyAndNeverAskAgainLocation") == 1)
      {
        _locationPermissionPopup.SetActive(false);
        Debug.LogError("GET KEY Called!!!!");
        return;
      }
      else
      {
        _locationPermissionPopup.SetActive(true);
        return;
      }
    }
    else
    {
      _locationPermissionPopup.SetActive(true);
      return;
    }
  }

  public void showToastOnUiThread(string toastString)
  {
    //#if UNITY_ANDROID && !UNITY_EDITOR
    //        if (currentActivity != null && UnityPlayer != null)
    //        {
    //            this.toastString = toastString;
    //            currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(showToast));
    //        }
    //#elif UNITY_IOS || UNITY_EDITOR
    if (toastString != null && !string.IsNullOrEmpty(toastString) && ToastUIHandler.Instance != null)
      ToastUIHandler.Instance.ShowMessage(toastString.ToUpper());
    //#endif
  }

  public void showToastWithLongLength(string toastString)
  {
    //#if UNITY_ANDROID && !UNITY_EDITOR
    //        if (currentActivity != null && UnityPlayer != null)
    //        {
    //            this.toastString = toastString;
    //            currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(showToastLongLength));
    //        }
    //#elif UNITY_IOS || UNITY_EDITOR
    if (!string.IsNullOrEmpty(toastString))
      ToastUIHandler.Instance.ShowMessage(toastString.ToUpper());
    //#endif
  }

  //    void showToast()
  //    {
  //        Debug.Log(this + ": Running on UI thread");
  //#if UNITY_ANDROID && !UNITY_EDITOR
  //        AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
  //        AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", toastString);
  //        AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject>("makeText", context, javaString, Toast.GetStatic<int>("LENGTH_SHORT"));
  //        toast.Call("show");
  //#endif
  //    }

  //    void showToastLongLength()
  //    {
  //        Debug.Log(this + ": Running on UI thread");
  //#if UNITY_ANDROID && !UNITY_EDITOR
  //        AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
  //        AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", toastString);
  //        AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject>("makeText", context, javaString, Toast.GetStatic<int>("LENGTH_LONG"));
  //        toast.Call("show");
  //#endif
  //    }

#if SIMULATE_FPS_DROPS
	float fpsChangeTimer = 0;
#endif
  void Update()
  {
    if (increaseTimeScale)
    {
      Time.timeScale += 0.5f * Time.unscaledDeltaTime;
      Time.fixedDeltaTime = Defines.TimeStamp * Time.timeScale;
      if (Time.timeScale >= finalTimeScale)
      {
        increaseTimeScale = false;
        Time.timeScale = finalTimeScale;
        Time.fixedDeltaTime = Defines.TimeStamp * Time.timeScale;
      }
    }


#if SIMULATE_FPS_DROPS
		fpsChangeTimer -= Time.unscaledDeltaTime;
		if (fpsChangeTimer <= 0)
		{
			fpsChangeTimer = UnityEngine.Random.Range(0.01f, 5f);
			Application.targetFrameRate = UnityEngine.Random.Range(1, targetFPS);
		}
#else

    Application.targetFrameRate = targetFPS;
#endif
    MainThreadDispatcher.Instance.Tick();
    SoundManager.Instance.Tick();
  }

  public void GameControllsInitialize()
  {

    setGameControls(PlayerPrefs.GetInt("GameControlls", 3));

    if (PlayerPrefs.GetInt("CommentaryLanguage", 0) == 0)
      bIsCommentaryLanguageEnglish = true;


    if (PlayerPrefs.GetInt("Vibration", 1) != 0)
      bIsVibration = true;
    else
      bIsVibration = false;

    if (PlayerPrefs.GetInt("BowlType_", 1) != 0)
      bIsShowBallType = true;
    else
      bIsShowBallType = false;

    if (PlayerPrefs.GetInt("PlaySound", 1) != 0)
      soundManager.isSoundButtonOn = true;
    else
      soundManager.isSoundButtonOn = false;

  }

  public void Initialize()
  {
#if DEBUG_BUILD
    Instantiate(developerOverlay);
#endif
    Application.backgroundLoadingPriority = ThreadPriority.Normal;

    //Does nothing, but to create and instance(Very Heay Since lot of ResourceLoad Which are synchronus) and keep ready, 
    //called here so that it wont create lagspikes when user is going between 
    // UI scenes and panels
    //GameDataReader.Instance.Touch();



    soundManager = SoundManager.Instance;

    try
    {
      TextAsset jsonContent = (TextAsset)Resources.Load("Data/TeamsInfo", typeof(TextAsset));
      m_TeamInfo = JsonConvert.DeserializeObject<List<TeamInfo>>(jsonContent.text);
    }
    catch (Exception e)
    {
      LoggerUtil.LogError("Failed to read TeamsInfo");
      Debug.LogException(e);
    }

    //isLastGameSeen = false;	//Since Resume screen in tournament was showing.

    m_NetsSettings = null;

    gameEntryPrefs = GamePrefsManager.Instance.Load<GameEntryPrefs>(string.Empty, false, true);

    if (gameEntryPrefs != null)
    {
      userTeam = gameEntryPrefs.userTeam;
      opponentTeam = gameEntryPrefs.opponentTeam;
      matchSettings = gameEntryPrefs.matchSettings;
      isUserBatting = gameEntryPrefs.isUserBatting;
      isMatchActive = gameEntryPrefs.isMatchActive;
      tossResult = gameEntryPrefs.tossResult;
      userSelectedSquadIndices = gameEntryPrefs.userSelectedSquad;
      isTossScreenShown = gameEntryPrefs.isTossScreenShown;
      isUserWonTheToss = gameEntryPrefs.isUserWonTheToss;
      //Debug.Log("Game Entry prefs  found!");
    }
    else
    {
      userTeam = -1;
      opponentTeam = -1;
      matchSettings = null;
      isUserBatting = false;
      isMatchActive = false;
      tossResult = Defines.TossResult.kNone;
      userSelectedSquadIndices = new List<int>();
      isTossScreenShown = false;
      isUserWonTheToss = false;
      //Debug.Log("Game Entry prefs not found!");
    }

    BattingCamSelected = (BattingCam)PlayerPrefs.GetInt("BattingCam", 0);
    BowlingCamSelected = (BowlingCam)PlayerPrefs.GetInt("BowlingCam", 0);

    Screen.sleepTimeout = SleepTimeout.NeverSleep;
#if DEBUG_BUILD
    //GameObject SRDebugger = (GameObject)Resources.Load("Prefabs/SRDebugger.Init", typeof(GameObject));
    //SRDebugger = (GameObject)Instantiate(SRDebugger, Vector3.zero, Quaternion.identity);
    //SRDebugger.name = "(singleton) SRDebugger.Init";

    //GameObject fpsDisplay = (GameObject)Resources.Load("Prefabs/FPSDisplay", typeof(GameObject));
    //fpsDisplay = (GameObject)Instantiate(fpsDisplay, Vector3.zero, Quaternion.identity);
    //fpsDisplay.name = "(singleton) FPSDisplay";

#endif



    OnApplyDeviceConfiguration();

    //#if UNITY_ANDROID && !UNITY_EDITOR
    //            UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    //            if(UnityPlayer != null)
    //                currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    //            if(currentActivity != null)
    //                context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");

    //#endif

    setRewardMultiplier();
    GameControllsInitialize();
    statsManager = new StatsManager();
    playerAnalytics = new PlayerAnalytics();

    isInitialized = true;

    StoreTransactionHandler.Instance.Touch();
    //  PlayerAnalytics.Instance.Touch();
    CommentaryManager.Instance.Touch();

    if (SystemInfo.systemMemorySize <= 512)
    {
      targetFPS = 30;
    }
    //Debug.Log("GameManager Initialized Successfully");
  }

  public TeamInfo GetTeamInfo(int teamId)
  {
    //A Patch Since Code is hairy to resolve m_TeamInfo being set to null at all times.
    if (m_TeamInfo == null)
    {
      TextAsset jsonContent = (TextAsset)Resources.Load("Data/TeamsInfo", typeof(TextAsset));
      m_TeamInfo = JsonConvert.DeserializeObject<List<TeamInfo>>(jsonContent.text);
    }
    return m_TeamInfo.Find(x => x.TeamID == (int)teamId);
  }

  public void LoadLevelAsync(Defines.SceneID sceneID, bool bIsLoginToMainMenu = false)
  {
    StartCoroutine(OnLoadLevelAsync(sceneID, bIsLoginToMainMenu));
  }

  private IEnumerator OnLoadLevelAsync(Defines.SceneID sceneID, bool showBannerAd)
  {
    if (sceneID == Defines.SceneID.GAMEPLAY || sceneID == Defines.SceneID.MAIN_MENU || sceneID == Defines.SceneID.UI_TEAMSELECTION
        || sceneID == Defines.SceneID.UI_MATCHREWARD || sceneID == Defines.SceneID.AUCTION_SCENE || sceneID == Defines.SceneID.UI_TOURNAMENT_MENU)
      ShowLoadingScreen(sceneID, showBannerAd);
    //	yield return new WaitForSeconds(0.5f); //mithil, added a delay because of a crash while adding scene probably because of instantiating Loading Screen

    ////Temp fixing since everything is temp fixes 
    ////To Fix null reference if loadlevel async is called before the awake of this component.
    //if (GameManager.Instance == null)
    //{
    //  GameManager.Instance = this;
    //}
    if (sceneID == Defines.SceneID.GAMEPLAY)
    {
      StadiumTags stadiumTag = (StadiumTags)GameManager.Instance.matchSettings.stadium;
      StadiumMode stadiumMode = (StadiumMode)GameManager.Instance.matchSettings.stadiumMode;

      Defines.SceneID sceneID2 = Defines.SceneID.STADIUM_WANKHEDE_DAY;

      //if (stadiumTag == StadiumTags.Stadium_Wankhede)
      //{
      //  if (stadiumMode == StadiumMode.Morning)
      //    sceneID2 = Defines.SceneID.STADIUM_WANKHEDE_DAY;
      //  else
      //    sceneID2 = Defines.SceneID.STADIUM_WANKHEDE_NIGHT;
      //}
      //else if (stadiumTag == StadiumTags.Stadium_Lords)
      //{
      //  if (stadiumMode == StadiumMode.Morning)
      //    sceneID2 = Defines.SceneID.STADIUM_LORDS_DAY;
      //  else
      //    sceneID2 = Defines.SceneID.STADIUM_LORDS_NIGHT;
      //}
      //else if (stadiumTag == StadiumTags.Stadium_Melbourne)
      //{
      //  if (stadiumMode == StadiumMode.Morning)
      //    sceneID2 = Defines.SceneID.STADIUM_MELBOURNE_DAY;
      //  else
      //    sceneID2 = Defines.SceneID.STADIUM_MELBOURNE_NIGHT;
      //}
      //else 
      if (stadiumTag == StadiumTags.Stadium_Capetown)
      {
        if (stadiumMode == StadiumMode.Morning)
          sceneID2 = Defines.SceneID.STADIUM_CAPETOWN_DAY;
        else
          sceneID2 = Defines.SceneID.STADIUM_CAPETOWN_NIGHT;
      }
      //else if (stadiumTag == StadiumTags.Stadium_Pune)
      //{
      //  if (stadiumMode == StadiumMode.Morning)
      //    sceneID2 = Defines.SceneID.STADIUM_PUNE_DAY;
      //  else
      //    sceneID2 = Defines.SceneID.STADIUM_PUNE_NIGHT;
      //}

      AsyncOperation asyncOperationStadium = SceneManager.LoadSceneAsync(Defines.SceneNames[(int)sceneID2], LoadSceneMode.Single);
      while (!asyncOperationStadium.isDone)
      {
        yield return null;
        //GetUILoadingScreen(true, bIsLoginToMainMenu).SetProgressionOfLoading(asyncOperationStadium.progress * 0.5f, "LOADING STADIUM", false);
        //GetUILoadingScreen(true, bIsLoginToMainMenu).SetProgressionForGamePlayScene(asyncOperationStadium.progress, false);
      }

      AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(Defines.SceneNames[(int)sceneID], LoadSceneMode.Additive);
      while (!asyncOperation.isDone)
      {
        yield return null;
        //GetUILoadingScreen(true, bIsLoginToMainMenu).SetProgressionOfLoading(asyncOperation.progress + 0.5f, "LOADING PLAYERS", false);
        GetUILoadingScreen(true, showBannerAd).SetProgressionForGamePlayScene(asyncOperation.progress, false);
      }
      yield return new WaitUntil(() => GamePlayController.Instance);
      gamePlayController = GamePlayController.Instance;
      gamePlayController.OnStadiumLoaded();
      soundManager.StopMainMenuSound();

      if (m_UILoadingScreen != null)
      {
        Destroy(m_UILoadingScreen.gameObject);
        m_UILoadingScreen = null;

        if (!showBannerAd && AdsManager.Instance != null)
        {
          AdsManager.Instance.DestroyBanner(AdsManager.BannerAdsPosition.BannerAd_Loading);
          LoggerUtil.Log("AdsTesting GAMEPLAY");
        }
      }
    }
    else
    {
      AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(Defines.SceneNames[(int)sceneID], LoadSceneMode.Single);

      while (!asyncOperation.isDone)
      {
        if (sceneID == Defines.SceneID.MAIN_MENU || sceneID == Defines.SceneID.UI_TEAMSELECTION || sceneID == Defines.SceneID.UI_MATCHREWARD || sceneID == Defines.SceneID.AUCTION_SCENE
            || sceneID == Defines.SceneID.UI_TOURNAMENT_MENU || sceneID == Defines.SceneID.QUIZ)
          GetUILoadingScreen(false, showBannerAd).SetProgressionOfLoading(asyncOperation.progress);

        yield return null;
      }
      if (asyncOperation.isDone)
      {
        if (sceneID == Defines.SceneID.UI_TEAMSELECTION || sceneID == Defines.SceneID.MAIN_MENU || sceneID == Defines.SceneID.UI_MATCHREWARD || sceneID == Defines.SceneID.AUCTION_SCENE || sceneID == Defines.SceneID.UI_TOURNAMENT_MENU || sceneID == Defines.SceneID.QUIZ)
        {
          if (m_UILoadingScreen != null)
          {
            yield return null;
            Destroy(m_UILoadingScreen.gameObject);
            m_UILoadingScreen = null;

            if (!showBannerAd && AdsManager.Instance != null)
            {
              LoggerUtil.Log("AdsTesting Other scene " + sceneID);
              AdsManager.Instance.DestroyBanner(AdsManager.BannerAdsPosition.BannerAd_Loading);
            }
          }
        }
      }
    }

    if (sceneID == Defines.SceneID.GAMEPLAY)
      ReviveMananger.Instance.Init();//Initialize the Revive again after loading the gameplay
  }

  private void ShowLoadingScreen(Defines.SceneID sceneID, bool bIsLoginToMainMenu)
  {
    GetUILoadingScreen(false, bIsLoginToMainMenu).SetActiveLoadingScreen();
  }

  public void SaveGameEntryPrefs()
  {
    if (gameEntryPrefs == null)
      gameEntryPrefs = new GameEntryPrefs();

    gameEntryPrefs.userTeam = userTeam;
    gameEntryPrefs.opponentTeam = opponentTeam;
    gameEntryPrefs.matchSettings = matchSettings;
    gameEntryPrefs.isUserBatting = isUserBatting;
    gameEntryPrefs.isMatchActive = isMatchActive;
    gameEntryPrefs.tossResult = tossResult;
    gameEntryPrefs.userSelectedSquad = userSelectedSquadIndices;
    gameEntryPrefs.isTossScreenShown = isTossScreenShown;
    gameEntryPrefs.isUserWonTheToss = isUserWonTheToss;
    GamePrefsManager.Instance.Save(gameEntryPrefs, string.Empty, false, true);
  }

  public void Reset()
  {
    GamePrefsManager.Instance.Delete<GameEntryPrefs>();
    GamePrefsManager.Instance.Delete<InGameDataPrefs>(GameMode.ToString());
    GamePrefsManager.Instance.Delete<RainController.DLPrefs>();
    isInitialized = false;
    Initialize();
  }

  public void SetTimeScale(float timeScale, bool isSlow = false)
  {

    if (timeScale > Time.timeScale && isSlow)
    {
      increaseTimeScale = true;
      finalTimeScale = timeScale;
    }
    else
    {
      increaseTimeScale = false;
      Time.timeScale = timeScale;
      Time.fixedDeltaTime = Defines.TimeStamp * Time.timeScale;
    }
  }

  public UILoadingScreen GetUILoadingScreen(bool bIsGamePlayScene = false, bool bIsLoginToMainMenu = false)
  {
    if (m_UILoadingScreen == null)
    {
      m_UILoadingScreen = ((GameObject)Resources.Load("Prefabs/UI/LoadingUICanvas", typeof(GameObject))).GetComponent<UILoadingScreen>();
      m_UILoadingScreen = Instantiate(m_UILoadingScreen) as UILoadingScreen;
      m_UILoadingScreen.ResetLoading(bIsGamePlayScene);
      m_UILoadingScreen.gameObject.SetActive(false);

      if (!bIsLoginToMainMenu && AdsManager.Instance != null)
      {
        AdsManager.Instance.RequestBanner(AdsManager.BannerAdsPosition.BannerAd_Loading);

        LoggerUtil.Log("AdsTesting GetUILoadingScreen");
      }
    }

    return m_UILoadingScreen;
  }

  public void PauseGame()
  {
    isGamePaused = true;
    timeScale = Time.timeScale;
    GameManager.Instance.SetTimeScale(0f);
    SoundManager.Instance.PauseGameSound();
    CommentaryManager.Instance.PauseCommentarySound();
  }

  public void ResumeGame()
  {
    GameManager.Instance.SetTimeScale(timeScale);             //Assigns time scale which was remembered on pause.
    SoundManager.Instance.ResumeGameSound();
    CommentaryManager.Instance.ResumeCommentarySound();
  }

  private void OnApplyDeviceConfiguration()
  {
    int autoDeviceConfig = PlayerPrefs.GetInt("AutoDeviceConfig", -1);

    if (autoDeviceConfig == -1)
    {
      GameManager.Instance.qualityLevel = GetRecommendedQualityLevel();
      //GameManager.Instance.qualityLevel = QualityLevel.Low;
      PlayerPrefs.SetInt("AutoDeviceConfig", (int)GameManager.Instance.qualityLevel);
    }
    else
    {
      int customDeviceConfig = PlayerPrefs.GetInt("UserCustomDeviceConfig", -1);

      if (customDeviceConfig == -1)
        GameManager.Instance.qualityLevel = (QualityLevel)autoDeviceConfig;
      else
        GameManager.Instance.qualityLevel = (QualityLevel)customDeviceConfig;

      PlayerPrefs.SetInt("UserCustomDeviceConfig", (int)GameManager.Instance.qualityLevel);
    }
  }

  public QualityLevel GetRecommendedQualityLevel()
  {
    int deviceMemory = SystemInfo.systemMemorySize;
    int screenWidth = Screen.width;
    string deviceName = SystemInfo.deviceName;

    //Debug.Log("[GetRecommendedQualityLevel] deviceMemory: " + deviceMemory + "  screenWidth: " + screenWidth);
    /*#if UNITY_ANDROID
				//3GB or more: Very High
				if (deviceMemory > 4096)
				{
					return QualityLevel.VeryHigh;
				}
				//Upto 3GB: High
				else if (deviceMemory > 2048 && screenWidth >= 1920)
				{
					return QualityLevel.High;
				}
				//Upto 2GB: Medium
				else if (deviceMemory > 1024 && screenWidth >= 1280)
				{
					return QualityLevel.Medium;
				}
				//1GB or Less: Low
				else
				{
					return QualityLevel.Low;
				}
#elif UNITY_IOS || UNITY_EDITOR
		if (deviceMemory > 2048)
								return QualityLevel.VeryHigh;
						else if (deviceMemory > 1024 && screenWidth >= 1920)
								return QualityLevel.High;
						else if (deviceMemory > 512 && screenWidth >= 1280)
								return QualityLevel.Low;
						else
								return QualityLevel.Low;
#endif
		*/

    return QualityLevel.Low;
  }

  public void setGameControls(int type)
  {
    currentControlType = (ControlType)type;
    //IsSwipeControlActive = false;
    //NOOB_CONTROLS = false;
    //IsProControlActive = false;
    if (type == 0)
    {
      IsProControlActive = false;
      IsSwipeControlActive = false;
      IsClassicControlsActive = false;
    }
    else if (type == 1)
    {
      IsSwipeControlActive = true;
      IsClassicControlsActive = true;
      IsProControlActive = false;//Done by Ashish for classic swipe joystick bug.
    }
    else if (type == 2)
    {
      IsSwipeControlActive = true;
      IsClassicControlsActive = false;
      IsProControlActive = false;//Done by Ashish for classic swipe joystick bug.
    }
    else if (type == 3)
    {
      IsProControlActive = true;
      IsSwipeControlActive = false;
      IsClassicControlsActive = false;
    }
  }

  public void SetFaceMeshAndMaterial(Transform targetFace, int faceNum, Material faceMaterial, bool isCutscene = false)
  {
    if (targetFace.childCount > 1)
    {
      string faceMeshName = "face_" + faceNum;
      foreach (Transform child in targetFace)
        if (child.gameObject.name == faceMeshName)
        {
          child.gameObject.SetActive(true);
          child.GetComponent<SkinnedMeshRenderer>().sharedMaterial = faceMaterial;
        }
        else
          child.gameObject.SetActive(false);
    }
    else
    {
      Transform face = targetFace.GetChild(0);
      face.gameObject.layer = targetFace.gameObject.layer;
      SkinnedMeshRenderer meshRenderer = face.GetComponent<SkinnedMeshRenderer>();
      meshRenderer.sharedMesh = PlayerFaceMeshes[faceNum];
      meshRenderer.sharedMaterial = faceMaterial;
    }
  }

  public void SetBodyMeshMesh(Transform targetBody, bool isCutscene = false)
  {
    //SkinnedMeshRenderer meshRenderer = targetBody.GetComponent<SkinnedMeshRenderer>();
    //meshRenderer.sharedMesh = PlayerBodyMesh;
  }

  public void SetHairAndMaterial(Transform hairGroup, int num, Material hairMaterial, bool isCutscene = false, bool isCapActive = false)
  {
    foreach (Transform child in hairGroup)
      Destroy(child.gameObject);

    bool isCapOn = isCapActive;

    string path = "Prefabs/HairGroup/Others/" + (isCapOn ? "WithCap/cap_hair_01" : ("WithoutCap/hairs_" + num));

    GameObject pref = Resources.Load<GameObject>(path);

    if (pref != null && num != 0)
    {
      GameObject hairObj = GameObject.Instantiate(pref, hairGroup, false) as GameObject;

      if (hairObj != null)
      {
        hairObj.layer = hairGroup.gameObject.layer;
        hairObj.transform.GetComponent<MeshRenderer>().sharedMaterial = hairMaterial;
        hairObj.gameObject.SetActive(true);
      }
    }
  }

  public bool isDomesticTournament()
  {
    return false;
    //		if (GameManager.Instance.GameMode == Defines.GameMode.TOURNAMENT && (TournamentType == Defines.TournamentType.AUSSIE_T20 || TournamentType == Defines.TournamentType.AUCTION_PREMIER_LEAGUE
    //		|| TournamentType == Defines.TournamentType.BANGLADESH_SUPER_LEAGUE || TournamentType == Defines.TournamentType.ENGLISH_BASH || TournamentType == Defines.TournamentType.WEST_INDIES_LEAGUE
    //		|| TournamentType == Defines.TournamentType.PAKISTAN_PREMIER_LEAGUE || TournamentType == Defines.TournamentType.PREMIER_LEAGUE))
    //			return true;
    //		else
    //			return false;
  }

  public bool isInternationalTournament()
  {
    if (GameManager.Instance.GameMode == Defines.GameMode.TOURNAMENT && (TournamentType == Defines.TournamentType.ASSOSCIATES || TournamentType == Defines.TournamentType.U19_WORLDCUP
        || TournamentType == Defines.TournamentType.ASIA_CUP || TournamentType == Defines.TournamentType.MASTERS_CUP || TournamentType == Defines.TournamentType.CHAMPIONS_CUP
        || TournamentType == Defines.TournamentType.WORLD_CUP))
      return true;
    else
      return false;
  }

  public bool isModeTournament()
  {
    if (GameManager.Instance.GameMode == Defines.GameMode.SUPER_OVER || ((GameManager.Instance.GameMode == Defines.GameMode.TOURNAMENT) && (TournamentType == Defines.TournamentType.TRI_SERIES || TournamentType == Defines.TournamentType.AUCTION_PREMIER_LEAGUE
        || TournamentType == Defines.TournamentType.CRUSADE)))
      return true;
    else
      return false;
  }

  public bool isTournament()
  {
    if (GameManager.Instance.GameMode == Defines.GameMode.TOURNAMENT)
      return true;
    else
      return false;
  }

  private void OnApplicationFocus(bool focus)
  {
#if !UNITY_EDITOR && UNITY_ANDROID
    if (focus)
    {
      //CheckForPermission();
    }
#endif
  }

  private void OnApplicationPause(bool pause)
  {
#if !UNITY_EDITOR && UNITY_ANDROID
    if (!pause)
    {
      //CheckForPermission();
    }
#endif
  }
}
