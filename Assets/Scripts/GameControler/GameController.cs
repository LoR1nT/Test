using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.GameControler
{
    public class GameController
    {
        private GameControllerModel _gameControllerModel;
        private PlayComponentScript _playComponentScript;
        private StartScreenScript _startScreenScript;
        private Bar _statusBar;

        private GameObject _canvas;

        private RestartScript _restartScript;
        
        private bool _isFinishLevelScreenSpawned = false;

        public GameController(GameControllerModel gameControllerModel)
        {
            _gameControllerModel = gameControllerModel;
        }

        public void Initialize()
        {            
            SpawnBasic();
            GetScrits();
        }

        private void SpawnBasic()
        {
            Object.Instantiate(_gameControllerModel.TablePrefab, _gameControllerModel.GameControllerObject.transform);
            _canvas = Object.Instantiate(_gameControllerModel.CanvasPrefab, _gameControllerModel.GameControllerObject.transform);
        }

        private void GetScrits()
        {
            _playComponentScript = PlayComponentScript.instance;
            _startScreenScript = StartScreenScript.instence;
        }

        public void Update()
        {
            CheckState();
            CheckLevelVictory();
        }

        private void CheckState()
        {
            if (_playComponentScript != null)
            {
                if (_playComponentScript.IsPlaying())
                {
                    _startScreenScript.Kill();
                    PlayGame();
                }
            }
            else if(_restartScript != null)
            {
                if (_restartScript.PlayGame)
                {
                    _restartScript.Kill();
                    PlayGame();
                }
            }
        }

        private void PlayGame()
        {
            Object.Instantiate(_gameControllerModel.GamePrefab, _gameControllerModel.GameControllerObject.transform);
            _statusBar = Object.Instantiate(_gameControllerModel.StatusBarPrefab, _canvas.transform).GetComponent<Bar>();
            _isFinishLevelScreenSpawned = false;
        }

        private void CheckLevelVictory()
        {
            bool levelFinish = false;
            int count = GamePlayController.instence.GetData(out levelFinish);
            if (levelFinish && !_isFinishLevelScreenSpawned)
            {
                if (count > 5)
                {
                    _restartScript = Object.Instantiate(_gameControllerModel.VictoryPrefab, _canvas.transform).GetComponent<RestartScript>();
                    Object.Destroy(_statusBar.gameObject);
                    _isFinishLevelScreenSpawned = true;
                }
                else if (count < 5)
                {
                    _restartScript = Object.Instantiate(_gameControllerModel.LostPrefab, _canvas.transform).GetComponent<RestartScript>();
                    Object.Destroy(_statusBar.gameObject);
                    _isFinishLevelScreenSpawned = true;
                }
            }
        }

    }
}
