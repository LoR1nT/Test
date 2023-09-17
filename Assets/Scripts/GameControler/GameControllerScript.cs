using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameControler
{
    public class GameControllerScript : MonoBehaviour
    {
        [SerializeField] private GameControllerModel _gameControllerModel;
        private GameController _gameController;

        private void Awake()
        {
            _gameController = new GameController(_gameControllerModel);
        }

        void Start()
        {
            _gameController.Initialize();
        }

        private void FixedUpdate()
        {
            _gameController.Update();
        }
    }
}