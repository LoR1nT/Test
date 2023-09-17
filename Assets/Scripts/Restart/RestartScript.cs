using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartScript : MonoBehaviour
{
    [SerializeField] private Button _restartButton;

    public bool PlayGame = false;

    void Start()
    {
        _restartButton.onClick.AddListener(() =>
        {
            PlayGame = true;
        });
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
