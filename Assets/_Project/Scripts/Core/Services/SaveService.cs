using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RelicHaul.Core
{
    public sealed class SaveService : ISaveService
    {
        const string SaveFileName = "player_save.json";

        readonly string _savePath;
        bool _isDirty;

        public PlayerSaveData Data { get; private set; } = new();

        public SaveService()
        {
            _savePath = Path.Combine(Application.persistentDataPath, SaveFileName);
        }

        public async UniTask LoadAsync(CancellationToken cancellationToken = default)
        {
            await UniTask.SwitchToMainThread(cancellationToken);

            if (!File.Exists(_savePath))
            {
                Data = new PlayerSaveData();
                return;
            }

            var json = await UniTask.RunOnThreadPool(
                () => File.ReadAllText(_savePath),
                cancellationToken: cancellationToken);

            Data = JsonUtility.FromJson<PlayerSaveData>(json) ?? new PlayerSaveData();
            _isDirty = false;
        }

        public async UniTask SaveAsync(CancellationToken cancellationToken = default)
        {
            if (!_isDirty)
            {
                return;
            }

            var json = JsonUtility.ToJson(Data, true);
            await UniTask.RunOnThreadPool(
                () => File.WriteAllText(_savePath, json),
                cancellationToken: cancellationToken);

            _isDirty = false;
        }

        public void MarkDirty() => _isDirty = true;
    }
}
