using RelicHaul.Core;
using VContainer;
using VContainer.Unity;

namespace RelicHaul.UI
{
	public class UiLifetimeScope : LifetimeScope
	{
		protected override LifetimeScope FindParent() => FindAnyObjectByType<GameLifetimeScope>();

		protected override void Configure(IContainerBuilder builder)
		{
			// Фаза 2: RegisterComponentInHierarchy<MainMenuController>, GameHudController, …
		}
	}
}