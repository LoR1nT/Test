using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayComponentScript : MonoBehaviour
{
    [SerializeField] private Button _playButton;

    public static PlayComponentScript instance;

    private bool _playGame = false;

    public PlayComponentScript()
    {
        instance = this;
    }


    void Start()
    {
        _playButton.onClick.AddListener(SetState);
    }

    private void SetState()
    {
        _playGame = true;
    }

    public bool IsPlaying()
    {
        if (_playGame)
        {
            return _playGame;
        }

        return false;
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveAllListeners();
    }
}
