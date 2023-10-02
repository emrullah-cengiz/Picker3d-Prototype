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

public class PoolObject : Actor<GameManager>
{
    private PoolData Data;

    [SerializeField] private TMP_Text _ballCountText;

    [SerializeField] private Transform[] _barriers;
    [SerializeField] private Transform _ground;

    string _ballTag;
    string _playerTag;
    uint _collectedBallCount;

    bool _isPlayerReached;
    bool _availableForBallCounting;

    private void Awake()
    {
        _availableForBallCounting = true;
        _ballTag = GameSettings.Instance.collectableBallTag;
        _playerTag = GameSettings.Instance.playerTag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_ballTag))
            BallInteraction(true);

        else if (other.CompareTag(_playerTag))
            PlayerInteraction();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_ballTag))
            BallInteraction(false);
    }

    public void Setup(PoolData data)
    {
        Data = data;
        UpdateText();
    }

    private void PlayerInteraction()
    {
        _isPlayerReached = true;

        CoreSignals.Instance.onReachedToPool?.Invoke();

        DOVirtual.DelayedCall(GameSettings.Instance.poolWaitDuration, () => Close());
    }

    private void BallInteraction(bool status)
    {
        if (!_availableForBallCounting)
            return;

        _collectedBallCount += (uint)(status ? 1 : -1);

        CoreSignals.Instance.onPoolInteractedWithBall?.Invoke(status);

        UpdateText();
    }

    private void UpdateText()
    {
        _ballCountText.text = $"{_collectedBallCount} / {Data.RequiredBallCount}";
    }

    private void Close()
    {
        if (!CheckCompletion())
        {
            CoreSignals.Instance.onPoolClosed?.Invoke(false);
            return;
        }

        Sequence sequence = DOTween.Sequence();

        sequence.Join(_ground.DOLocalMoveY(-.3f, .5f))
                .AppendInterval(.5f);

        foreach (var barrier in _barriers)
            sequence.Join(barrier.DOLocalRotate(Vector3.forward * 70, .5f, RotateMode.LocalAxisAdd));

        sequence.OnComplete(() => 
                CoreSignals.Instance.onPoolClosed?.Invoke(true) );
    }

    private bool CheckCompletion() {
        return _collectedBallCount >= Data.RequiredBallCount;
    }

}
