using System;
using System.Collections;
using System.Collections.Generic;
//using MEC;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static GameController _instantie;
    public static bool spellon;
    public static bool spelloff = true;
    public static bool isTouch = false;
    [Header("prefab Ref")] 
    public GameObject _playerPrefab;
    public GameObject _masterCanvasPrefab;
    public GameObject _enemyManagerPrefab;
    public GameObject _endgamePanel;
    
    
    
    [Header("View Private Componet")]
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _masterCanvas;
    [SerializeField] private PlayerModel _playerModel;
    [SerializeField] private PlayerController _playerController;
    private SkillController _skillController;
    public Indicator _indicator;

    public GameData _gameData { get; set; }
    //[SerializeField]private bool _isGameover = false;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (_instantie == null)
            _instantie = this;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        initialize();
    }
    
    public void initialize()
    {
        _gameData = SaveLoadSystem._instance._gameData;
        initUI();
        
        initPlayer();
        initChildManage();
        CameraControl._instance.Initialize();
        initSkill();
    }
    public void initPlayer()
    {
        if (_gameData != null)
        {
            _playerModel = new PlayerModel(_gameData);
            _playerModel?.Initialize();
        }
        else
        {
            _playerModel = new PlayerModel();
            _playerModel?.Initialize();
        }

        if (_player == null)
        {
            _player = Instantiate(_playerPrefab, new Vector3(10,-50,300), Quaternion.identity);
        }
        _playerView = _player.gameObject.GetComponent<PlayerView>();
        _playerView.initialize();

        _playerController = new PlayerController();
        _playerController?.initialize();
        _playerController?.setData(_playerModel);
        _playerController?.setView(_playerView);
        _indicator = _player.transform.GetComponentInChildren<Indicator>();
    }
    public void initUI()
    {
        if(_masterCanvas == null)
            _masterCanvas = Instantiate(_masterCanvasPrefab);
        _endgamePanel = GameObject.FindWithTag("EndGamePanel");
        _endgamePanel.SetActive(false);
    }

    public void initChildManage()
    {
        Instantiate(_enemyManagerPrefab, gameObject.transform);
    }
    public void initSkill()
    {
        _skillController = FindObjectOfType<SkillController>();
        if (_playerModel != null)
        {
            _skillController.setData(_playerModel);
        }
    }

    private void Update()
    {
        _playerController.onUpdate();
        CameraControl._instance.HandleCamera(_playerController.getCameraInputFromPlayer().x);
        GameOver();
    }

    public PlayerModel getPlayerModel => _playerModel;
    public PlayerView getPlayerView => _playerView;
    public PlayerController getPlayerController => _playerController;
    public void SpellOn()
    {
        spellon = true;
    }

    public void SpellOff()
    {
        spelloff = false;
    }

    public void Touch()
    {
        isTouch = true;
    }
    public void UnTouch()
    {
        isTouch = false;
    }
    #region GameLoop

    public void GameOver()
    {
        if (_playerModel._currentHealth <=0)
        {
            StartCoroutine(Gameover());
        }
    }
    public IEnumerator Gameover()
        {
            yield return new WaitForSeconds(1f);
            _endgamePanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }

    #endregion
    
    public void SaveData()
    {
        GameData _GD = new GameData(_playerModel,1);
        SaveLoadSystem._instance.SaveData(_GD);
    }
    
}    

