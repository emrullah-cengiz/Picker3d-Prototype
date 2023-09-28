using Assets._Game.Scripts.Actors;
using Assets.Scripts.Abstracts;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameSettings), menuName = "Settings/" + nameof(GameSettings))]
public class GameSettings : SingletonScriptableObject<GameSettings>
{
    [Header("Assets")]
    public string uiPanelsResourcePath = "Prefabs/UI/Panels";
    public string uiPanelPrefabNameFormat = "Panel_{0}";

    public string levelPrefabsResourcePath = "Prefabs/Levels";
    public string levelPrefabNameFormat = "Level_{0}";

    public uint maxLevelNumber = 10;
    public string playerDataFileName = "PlayerData";
}
