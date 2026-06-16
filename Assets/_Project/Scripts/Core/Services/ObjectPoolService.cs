using RelicHaul.Core;
using System.Collections.Generic;
using UnityEngine;

namespace RelicHaul.Core
{

	public class ObjectPoolService : IObjectPoolService
	{
		readonly Dictionary<EntityId, Queue<GameObject>> _pools = new();
		readonly Transform _root;

		public ObjectPoolService()
		{
			var rootObject = new GameObject("[ObjectPool]");
			Object.DontDestroyOnLoad(rootObject);
			_root = rootObject.transform;
		}

		public void Prewarm(GameObject prefab, int count, Transform parent = null)
		{
			if (prefab == null || count <= 0)
			{
				return;
			}

			RegisterPrefab(prefab);
			var pool = GetPool(prefab);

			for (var i = 0; i < count; i++)
			{
				var instance = CreateInstance(prefab, parent ?? _root);
				instance.SetActive(false);
				pool.Enqueue(instance);
			}
		}

		public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
		{
			RegisterPrefab(prefab);
			var pool = GetPool(prefab);

			while (pool.Count > 0)
			{
				var instance = pool.Dequeue();
				if (instance != null)
				{
					instance.transform.SetParent(parent, false);
					instance.transform.SetPositionAndRotation(position, rotation);
					instance.SetActive(true);
					return instance;
				}
			}

			var created = CreateInstance(prefab, parent ?? _root);
			created.transform.SetPositionAndRotation(position, rotation);
			created.SetActive(true);
			return created;
		}

		public void Despawn(GameObject instance)
		{
			if (instance == null)
			{
				return;
			}

			var poolable = instance.GetComponent<PoolableObject>();
			var prefabId = poolable != null ? poolable.PrefabId : instance.GetEntityId();

			if (!_pools.TryGetValue(prefabId, out var pool))
			{
				Object.Destroy(instance);
				return;
			}

			instance.SetActive(false);
			instance.transform.SetParent(_root, false);
			pool.Enqueue(instance);
		}

		static GameObject CreateInstance(GameObject prefab, Transform parent)
		{
			var instance = Object.Instantiate(prefab, parent);
			var poolable = instance.GetComponent<PoolableObject>();
			if (poolable == null)
			{
				poolable = instance.AddComponent<PoolableObject>();
			}

			poolable.PrefabId = prefab.GetEntityId();
			return instance;
		}

		void RegisterPrefab(GameObject prefab)
		{
			var id = prefab.GetEntityId();
			if (!_pools.ContainsKey(id))
			{
				_pools[id] = new Queue<GameObject>();
			}
		}

		Queue<GameObject> GetPool(GameObject prefab) => _pools[prefab.GetEntityId()];
	}
}
