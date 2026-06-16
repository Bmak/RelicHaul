using UnityEngine;

namespace RelicHaul.Core
{
    [CreateAssetMenu(fileName = "GameBalanceConfig", menuName = "Relic Haul/Game Balance Config")]
    public sealed class GameBalanceConfig : ScriptableObject
    {
        [Header("Session")]
        [Min(1)] public int RelicsRequiredForExtraction = 5;
        [Min(1)] public float SessionDurationSeconds = 480f;

        [Header("Player")]
        [Min(1f)] public float PlayerMaxHealth = 100f;
        [Min(0f)] public float PlayerMoveSpeed = 6f;
        [Min(0f)] public float DodgeCooldownSeconds = 1.2f;
        [Min(0f)] public float AttackCooldownSeconds = 0.45f;
        [Min(0f)] public float AttackDamage = 25f;

        [Header("Remote Config Defaults")]
        [Min(0.1f)] public float DefaultEnemyHpMultiplier = 1f;
    }
}
