using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace RelicHaul.Core
{
    public interface IAddressablesService
    {
        string ActiveDungeonKey { get; }
        UniTask<T> LoadAssetAsync<T>(string key, CancellationToken cancellationToken = default) where T : Object;
        UniTask<SceneInstance> LoadSceneAsync(string sceneKey, LoadSceneMode mode, CancellationToken cancellationToken = default);
        UniTask UnloadSceneAsync(SceneInstance sceneInstance, CancellationToken cancellationToken = default);
        void Release(Object asset);
        void SetActiveDungeonKey(string dungeonKey);
    }
}
