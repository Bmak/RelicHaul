using System.Threading;
using Cysharp.Threading.Tasks;

namespace RelicHaul.Core
{
    public interface ISaveService
    {
        PlayerSaveData Data { get; }
        UniTask LoadAsync(CancellationToken cancellationToken = default);
        UniTask SaveAsync(CancellationToken cancellationToken = default);
        void MarkDirty();
    }
}
