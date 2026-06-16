using System.Threading;
using Cysharp.Threading.Tasks;

namespace RelicHaul.Core
{
    public interface IRelicCloudSaveService
    {
        UniTask SyncBestScoreAsync(int bestScore, CancellationToken cancellationToken = default);
        UniTask<int> LoadBestScoreAsync(CancellationToken cancellationToken = default);
    }
}
