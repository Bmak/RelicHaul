using Cysharp.Threading.Tasks;

namespace RelicHaul.Core
{
    public interface IRemoteConfigService
    {
        float EnemyHpMultiplier { get; }
        bool IsReady { get; }
        UniTask FetchAsync(System.Threading.CancellationToken cancellationToken = default);
    }
}
