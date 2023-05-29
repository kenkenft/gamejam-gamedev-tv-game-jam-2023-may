using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    private Dictionary<string, Canvas> _canvasesDict = new Dictionary<string, Canvas>(){};
    private Canvas _playerOverlayCanvas, _titleCanvas, _pauseCanvas, _resultsCanvas;
    private List<Canvas> _canvasList = new List<Canvas>();
    public Image ResultsPanelImage;

    [SerializeField] bool _isPlaying = false, _isPaused = false, _isFinalLevelCompleted = false;

    private Sprite[] _endResultImages;

    [SerializeField] private GameObject[] _instructionTextArray;
    private int _textIndexPointer = 0; 
    [SerializeField] private Text[] _resultsUITextArray;

    [HideInInspector] public delegate void OnSomeEvent();
    [HideInInspector] public static OnSomeEvent StartGameSetUp;
    [HideInInspector] public static OnSomeEvent NextLevelRequested;

    [HideInInspector] public delegate int IntValueGet();
    [HideInInspector] public static IntValueGet LevelValueRequested;
    [HideInInspector] public delegate void SetValueEvent(int value);
    [HideInInspector] public static SetValueEvent LevelCompleted;
    [HideInInspector] public delegate bool BoolValueGet();
    [HideInInspector] public static BoolValueGet IsFinalLevelRequested;
    [HideInInspector] public delegate void OnPlaySFX(string audioName);
    [HideInInspector] public static OnPlaySFX PlaySFX;
    
    void OnEnable()
    {
        PlayerMain.RestartButtonPressed += ContinueToNextLevel;
        PlayerMain.CheckIsPlaying += GetIsPlaying;
        EndZone.EndZoneEntered += TriggerEndLevel;
        EndZone.IsFinalLevelCompleted += SetIsFinalLevelCompleted;
        LevelManager.TitleSwitchOccurred += AdvanceInstructionText;
    }
    void Disable()
    {
        PlayerMain.RestartButtonPressed -= ContinueToNextLevel;
        PlayerMain.CheckIsPlaying -= GetIsPlaying;
        EndZone.EndZoneEntered -= TriggerEndLevel;
        EndZone.IsFinalLevelCompleted -= SetIsFinalLevelCompleted;
        LevelManager.TitleSwitchOccurred -= AdvanceInstructionText;
    }

    void Start()
    {
        SetCanvasRefs();
        ContinueToNextLevel();            
        TriggerTitleCanvas();
    }

    void SetCanvasRefs()
    {
        Canvas[] tempCanvasArray = GetComponentsInChildren<Canvas>();
        foreach(Canvas canvas in tempCanvasArray)
        {
            switch(canvas.gameObject.name)
            {
                case "PlayerOverlayCanvas":
                {
                    // _canvasesDict.Add("playerOverlayCanvas", canvas);
                    _playerOverlayCanvas = canvas;
                    _canvasList.Add(_playerOverlayCanvas);
                    break;
                }
                case "TitleCanvas":
                {
                    // _canvasesDict.Add("titleCanvas", canvas);
                    _titleCanvas = canvas;
                    _canvasList.Add(_titleCanvas);
                    break;
                }
                case "PauseCanvas":
                {
                    // _canvasesDict.Add("pauseCanvas", canvas);
                    _pauseCanvas = canvas;
                    _canvasList.Add(_pauseCanvas);
                    break;
                }
                case "ResultsCanvas":
                {
                    // _canvasesDict.Add("resultsCanvas", canvas);
                    _resultsCanvas = canvas;
                    _canvasList.Add(_resultsCanvas);
                    break;
                }
                default:
                    break;
            }
        }
    }
    

    void ToggleCanvas(string targetCanvasName)
    {
        // Toggles off other canvases except target canvas
        foreach(Canvas canvas in _canvasList)
        {
            if(canvas.gameObject.name == targetCanvasName)
                canvas.gameObject.SetActive(true);
            else
                canvas.gameObject.SetActive(false);
        }
    }

    public void AdvanceInstructionText()
    {
        if(_textIndexPointer >= _instructionTextArray.Length-1)
            _textIndexPointer = 0;
        else
            _textIndexPointer++;
        EnableInstructionText(_textIndexPointer);
    }

    void EnableInstructionText(int targetTextNum)
    {
        for(int i = 0; i < _instructionTextArray.Length; i++)
        {
            if(i == targetTextNum)
                _instructionTextArray[i].SetActive(true);
            else
                _instructionTextArray[i].SetActive(false);
        }
    }

    public void ContinueToNextLevel()
    {
        Time.timeScale = 1;
        _isPlaying = true;

        // Debug.Log("Play button pressed!");
        if(LevelValueRequested.Invoke() != 0)
            ToggleCanvas("PlayerOverlayCanvas");
        else
            TriggerTitleCanvas();

        _isPaused = false;

        NextLevelRequested?.Invoke();

        // PlaySFX?.Invoke("Start");
    }

    public void TogglePauseGame()
    { 
            if(!_isPaused)
            {    
                _pauseCanvas.gameObject.SetActive(true);
                Time.timeScale = 0;
                _isPaused = true;
            }
            else
            {
                _pauseCanvas.gameObject.SetActive(false);
                Time.timeScale = 1;
                _isPaused = false;
            }
    }

    public bool GetIsPaused()
    {
        return _isPaused;
    }

    public bool GetIsPlaying()
    {
        return _isPlaying;
    }
    
    public void TriggerEndLevel()
    {
        // Debug.Log("TriggerEndLevel called");
        // ToggleCanvas("ResultsCanvas");
        // Debug.Log("LevelValueRequested before IsFinal Check: " + LevelValueRequested.Invoke());
        // Debug.Log("IsFinalLevelRequested.Invoke(): " + IsFinalLevelRequested.Invoke());
        // if(IsFinalLevelRequested.Invoke())
        if(_isFinalLevelCompleted)
        {
            _isPlaying = false;
            ToggleCanvas("ResultsCanvas");
            // LevelCompleted?.Invoke(0);
            // PlaySFX?.Invoke("FinalLevelComplete");
        }                
        else
        {
            // Debug.Log("LevelValueRequested.Invoke(): " + LevelValueRequested.Invoke());
            // LevelCompleted?.Invoke(LevelValueRequested.Invoke());
            ContinueToNextLevel();
            // PlaySFX?.Invoke("LevelComplete");
        }
        

    }


    public void TriggerTitleCanvas()
    {
        ToggleCanvas("TitleCanvas");
        _isPlaying = true;
        _isPaused = false;
        _textIndexPointer = 0;
        AdvanceInstructionText();
        // PlaySFX?.Invoke("coinPickup");
    }

    void SetIsFinalLevelCompleted(bool state)
    {
        _isFinalLevelCompleted = state;
    }

}
