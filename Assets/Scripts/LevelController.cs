using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

//this class will hold the necessary things for level
public class LevelController : MonoBehaviour{
    public static LevelController instance;

    public FloatingJoystick joystick;
    public Transform finishLine;
    [HideInInspector]public int killCount = 0;
    [HideInInspector][FormerlySerializedAs("isDead")] public bool isFinished = false;
    [HideInInspector]public bool isPlayerDead = false;
    private bool _check = false;
    
    [SerializeField] private Button startButton;
    
    [SerializeField] private TextMeshProUGUI killCountText;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private TextMeshProUGUI countDownForStart;

    private CanvasGroup _buttonCg;
    private CanvasGroup _countDownTextCg;

    private float _countDown = 5f;
    private bool _isStarted = true;


    private void Awake(){
        #region Singleton
        if (instance != null)
        {
            return;

        }
        instance = this;
        #endregion
    }

    private void Start(){
        StartOverride();
    }

    private void Update(){
        UpdateOverride();
    }

    private void StartOverride(){
        GameEventSystem.instance.UIUpdater += UpdateUIText;
        _buttonCg = startButton.GetComponent<CanvasGroup>();
        _countDownTextCg = countDownForStart.GetComponent<CanvasGroup>();
        Time.timeScale = 0;
    }

    private void UpdateOverride(){
        StartingGame();
        if (isFinished && !_check){
            if (!isPlayerDead){//if player is managed to finish game we wont trigger this event
                GameEventSystem.instance.afterGameFinished.SetPersistentListenerState(0, UnityEventCallState.Off);
            }
            GameEventSystem.instance.afterGameFinished.AddListener(EndGameUI);
            GameEventSystem.instance.afterGameFinished.Invoke();
            _check = true;
        }
    }

    private void StartingGame(){
        startButton.onClick.AddListener(GameStart);
        if (!_isStarted){
            _countDown -= Time.unscaledDeltaTime;
            countDownForStart.text = " " + (int)_countDown;
            if (_countDown <= 0){
                Time.timeScale = 1;
                CanvasGroupUICloser(false, _countDownTextCg);
                _isStarted = true;
            }
        }
        
    }

    private void UpdateUIText(){
        killCount += 1; 
        killCountText.text = "Kill Count: " + killCount;
    }

    private void GameStart(){
        _isStarted = false;
        CanvasGroupUICloser(false, _buttonCg);
        CanvasGroupUICloser(true, _countDownTextCg);
        startButton.onClick.RemoveListener(GameStart);
    }

    //Canvas group added UI elements thats why we can close and open UI without using setactive method
    private void CanvasGroupUICloser(bool check, CanvasGroup cg){
        float _temp;
        if (check){
            _temp = 1;
        }else{
            _temp = 0;
        }
        cg.alpha = _temp;
        cg.interactable = check;
        cg.blocksRaycasts = check;
    }
    
    //After finish game we will see this UI Elements
    private void EndGameUI(){
        countDownForStart.text = isPlayerDead ? "LOST" : "WIN";

        buttonText.text = "Restart to Touch";
        
        CanvasGroupUICloser(true, _buttonCg);
        CanvasGroupUICloser(true, _countDownTextCg);
        
        startButton.onClick.AddListener(RestartGame);
    }

    private void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private void OnDestroy(){
        GameEventSystem.instance.UIUpdater -= UpdateUIText;
    }
}
