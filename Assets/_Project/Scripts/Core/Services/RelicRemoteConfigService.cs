using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Services.RemoteConfig;
using UnityEngine;

namespace RelicHaul.Core
{
    public sealed class RelicRemoteConfigService : IRemoteConfigService
    {
        readonly GameBalanceConfig _balanceConfig;

        public float EnemyHpMultiplier { get; private set; } = 1f;
        public bool IsReady { get; private set; }

        public RelicRemoteConfigService(GameBalanceConfig balanceConfig)
        {
            _balanceConfig = balanceConfig;
            EnemyHpMultiplier = balanceConfig != null
                ? balanceConfig.DefaultEnemyHpMultiplier
                : 1f;
        }

        public async UniTask FetchAsync(CancellationToken cancellationToken = default)
        {
            await UniTask.SwitchToMainThread(cancellationToken);

            try
            {
                await RemoteConfigService.Instance
                    .FetchConfigsAsync(new UserAttributes(), new AppAttributes())
                    .AsUniTask()
                    .AttachExternalCancellation(cancellationToken);

                EnemyHpMultiplier = RemoteConfigService.Instance.appConfig.GetFloat(
                    "enemy_hp_multiplier",
                    _balanceConfig != null ? _balanceConfig.DefaultEnemyHpMultiplier : 1f);
            }
            catch (System.Exception exception)
            {
                Debug.LogWarning($"Remote Config fetch failed, using defaults. {exception.Message}");
                EnemyHpMultiplier = _balanceConfig != null
                    ? _balanceConfig.DefaultEnemyHpMultiplier
                    : 1f;
            }

            IsReady = true;
        }

        struct UserAttributes { }

        struct AppAttributes { }
    }
}
