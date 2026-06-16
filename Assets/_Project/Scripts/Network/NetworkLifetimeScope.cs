using RelicHaul.Core;
using VContainer;
using VContainer.Unity;

namespace RelicHaul.Network
{
	public class NetworkLifetimeScope : LifetimeScope
	{
		protected override LifetimeScope FindParent() => FindAnyObjectByType<GameLifetimeScope>();

		protected override void Configure(IContainerBuilder builder)
		{
			// Фаза 3: NetworkGameManager, NetworkLobbyService, …
		}
	}
}