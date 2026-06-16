using Unity.Services.CloudSave;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace RelicHaul.Core
{
	public sealed class GameLifetimeScope : LifetimeScope
	{
		[SerializeField] GameBalanceConfig _balanceConfig;

		protected override void Awake()
		{
			DontDestroyOnLoad(gameObject);
			base.Awake();
		}

		protected override void Configure(IContainerBuilder builder)
		{
			builder.Register<IObjectPoolService, ObjectPoolService>(Lifetime.Singleton);
			builder.Register<ISaveService, SaveService>(Lifetime.Singleton);
			builder.Register<IAddressablesService, AddressablesService>(Lifetime.Singleton);
			builder.Register<IRelicAnalyticsService, RelicAnalyticsService>(Lifetime.Singleton);
			builder.Register<IRelicCloudSaveService, RelicCloudSaveService>(Lifetime.Singleton);
			builder.Register<IGameSessionEvents, GameSessionEvents>(Lifetime.Singleton);
			builder.Register<QualitySettingsManager>(Lifetime.Singleton);

			builder.RegisterInstance(_balanceConfig);
			builder.Register<IRemoteConfigService, RelicRemoteConfigService>(Lifetime.Singleton);

			builder.RegisterEntryPoint<BootSequence>();
		}
	}
}

