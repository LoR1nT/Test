using System;
using UnityEngine;

namespace Assets.Scripts.GameControler
{
    [Serializable]
    public class GameControllerModel
    {
        [SerializeField] private GameObject _gameControllerObject;
        [SerializeField] private GameObject _canvasPrefab;
        [SerializeField] private GameObject _startScreenPrefab;
        [SerializeField] private GameObject _tablePrefab;
        [SerializeField] private GameObject _gamePrefab;
        [SerializeField] private GameObject _lostPrefab;
        [SerializeField] private GameObject _victoryPrefab;
        [SerializeField] private GameObject _statusBarPrefab;

        public GameObject GameControllerObject { get { return _gameControllerObject; } }
        public GameObject CanvasPrefab { get { return _canvasPrefab; } }
        public GameObject StartScreenPrefab { get { return _startScreenPrefab; } }
        public GameObject TablePrefab { get { return _tablePrefab; } }
        public GameObject GamePrefab { get { return _gamePrefab; } }
        public GameObject LostPrefab { get { return _lostPrefab; } }
        public GameObject VictoryPrefab { get { return _victoryPrefab; } }
        public GameObject StatusBarPrefab { get { return _statusBarPrefab; } }
    }
}
