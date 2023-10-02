using Assets._Game.Scripts.Actors;
using Assets.Scripts.Abstracts;
using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = nameof(GameSettings), menuName = "Settings/" + nameof(GameSettings))]
public class GameSettings : SingletonScriptableObject<GameSettings>
{
    [Header("UI")]
    public string uiPanelsResourcePath = "Prefabs/UI/UIPanels";
    public string uiPanelPrefabNameFormat = "Panel_{0}";
    public Color filledPoolIndicatorColor;
    public Image poolIndicatorUiPrefab;

    [Header("Levels")]
    public string levelPrefabsResourcePath = "Prefabs/Levels";
    public string levelPrefabNameFormat = "Level_{0}";

    public string levelDataResourcePath = "Data/Levels";
    public string levelDataNameFormat = "Level_{0}";

    public uint maxLevelNumber = 10;

    [Header("Player")]
    public string playerTag = "Player";
    public string playerDataFileName = "PlayerData";
    public MovementSettings movementSettings;

    [Header("Pools")]
    public string poolTag = "Pool";
    public float poolWaitDuration = 3;

    [Header("Collectables")]
    public LayerMask collectableBallLayer;
    public string collectableBallTag = "CollectableBall";
    public Vector3 ballForceValue = new(0, 8, 10f);
}

[Serializable]
public struct MovementSettings
{
    public float ZMovementSpeed;
    public float XMovementSpeed;

    public float XClamp;
}
