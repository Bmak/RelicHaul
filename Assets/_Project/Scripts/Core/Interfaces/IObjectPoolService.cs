using UnityEngine;

namespace RelicHaul.Core
{
    public interface IObjectPoolService
    {
        GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null);
        void Despawn(GameObject instance);
        void Prewarm(GameObject prefab, int count, Transform parent = null);
    }
}
