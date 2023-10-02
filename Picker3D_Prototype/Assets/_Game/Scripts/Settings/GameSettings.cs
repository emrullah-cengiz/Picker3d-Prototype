using Assets._Game.Scripts.Actors;
using Assets.Scripts.Abstracts;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameSettings), menuName = "Settings/" + nameof(GameSettings))]
public class GameSettings : SingletonScriptableObject<GameSettings>
{
    [Header("UI")]
    public string uiPanelsResourcePath = "Prefabs/UI/UIPanels";
    public string uiPanelPrefabNameFormat = "Panel_{0}";

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
    public string collectableBallTag = "CollectableBall";
}

[Serializable]
public struct MovementSettings
{
    public float ZMovementSpeed;
    public float XMovementSpeed;

    public float XClamp;
}
