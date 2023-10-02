using Assets._Game.Scripts.Data;
using Assets._Game.Scripts.Managers;
using Assets._Game.Scripts.Signals;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using TMPro;
using UnityEngine;

public class FinishArea : Actor<GameManager>
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameSettings.Instance.playerTag))
            PlayerInteraction();
    }

    private void PlayerInteraction() => CoreSignals.Instance.onReachedToFinishArea?.Invoke();
}
