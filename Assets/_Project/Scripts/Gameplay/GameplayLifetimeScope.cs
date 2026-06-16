using RelicHaul.Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace RelicHaul.Gameplay
{
	public class GameplayLifetimeScope : LifetimeScope
	{
		[SerializeField] GameBalanceConfig _balanceConfig;

		protected override LifetimeScope FindParent() => FindAnyObjectByType<GameLifetimeScope>();

		protected override void Configure(IContainerBuilder builder)
		{
			// Фаза 2: PlayerController, GameSessionController, …
		}
	}
}