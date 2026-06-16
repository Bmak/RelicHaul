using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;

namespace RelicHaul.Core
{
	public class RelicCloudSaveService : IRelicCloudSaveService
	{
		const string BestScoreKey = "best_score";

		public async UniTask<int> LoadBestScoreAsync(CancellationToken cancellationToken = default)
		{
			try
			{
				var keys = new HashSet<string> { BestScoreKey };
				Dictionary<string, Item> result = await CloudSaveService.Instance.Data.Player.LoadAsync(keys);

				if (result.TryGetValue(BestScoreKey, out var item))
				{
					return item.Value.GetAs<int>();
				}
			}
			catch (System.Exception exception)
			{
				Debug.LogWarning($"Cloud Save load failed: {exception.Message}");
			}

			await UniTask.Yield(cancellationToken);
			return PlayerPrefs.GetInt(BestScoreKey, 0);  // fallback без облака
		}

		public async UniTask SyncBestScoreAsync(int bestScore, CancellationToken cancellationToken = default)
		{
			try
			{
				var data = new Dictionary<string, object> { { BestScoreKey, bestScore } };
				await CloudSaveService.Instance.Data.Player.SaveAsync(data);
			}
			catch (System.Exception exception)
			{
				Debug.LogWarning($"Cloud Save sync failed: {exception.Message}");
				await UniTask.Yield(cancellationToken);
			}
		}
	}

}
