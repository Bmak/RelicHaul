using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace RelicHaul.Core
{
	public class AddressablesService : IAddressablesService
	{
		string _activeDungeonKey = "dungeon_week1";

		public string ActiveDungeonKey => _activeDungeonKey;

		public void SetActiveDungeonKey(string dungeonKey)
		{
			if (!string.IsNullOrWhiteSpace(dungeonKey))
			{
				_activeDungeonKey = dungeonKey;
			}
		}

		public async UniTask<T> LoadAssetAsync<T>(string key, CancellationToken cancellationToken = default) where T : Object
		{
			AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
			await handle.ToUniTask(cancellationToken: cancellationToken);

			if (handle.Status != AsyncOperationStatus.Succeeded)
			{
				throw new System.InvalidOperationException($"Failed to load addressable asset '{key}'.");
			}

			return handle.Result;
		}

		public async UniTask<SceneInstance> LoadSceneAsync(
			string sceneKey,
			LoadSceneMode mode,
			CancellationToken cancellationToken = default)
		{
			AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneKey, mode);
			await handle.ToUniTask(cancellationToken: cancellationToken);

			if (handle.Status != AsyncOperationStatus.Succeeded)
			{
				throw new System.InvalidOperationException($"Failed to load addressable scene '{sceneKey}'.");
			}

			return handle.Result;
		}

		public async UniTask UnloadSceneAsync(SceneInstance sceneInstance, CancellationToken cancellationToken = default)
		{
			AsyncOperationHandle<SceneInstance> handle = Addressables.UnloadSceneAsync(sceneInstance);
			await handle.ToUniTask(cancellationToken: cancellationToken);
		}

		public void Release(Object asset)
		{
			if (asset != null)
			{
				Addressables.Release(asset);
			}
		}
	}

}
