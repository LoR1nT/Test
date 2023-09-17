using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public RectTransform _barFill;

    void Update()
    {
        UpdateBar();
    }

    private void UpdateBar()
    {
        bool canUpdate = false;
        float persentSliced = GamePlayController.instence.PersentSliced(out canUpdate);

        if (canUpdate)
        {
            _barFill.sizeDelta = new Vector2((int)Math.Round(_barFill.sizeDelta.x - ((persentSliced / 100) * 900)), _barFill.sizeDelta.y);
            canUpdate = false;
        }        
    }
}
