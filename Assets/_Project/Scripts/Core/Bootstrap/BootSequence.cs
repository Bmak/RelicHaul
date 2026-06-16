using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace RelicHaul.Core
{
    public sealed class BootSequence : IAsyncStartable
    {
        const string StartupSceneName = "Boot";
        const string MainMenuSceneName = "MainMenu";

        readonly ISaveService _saveService;
        readonly IRemoteConfigService _remoteConfigService;
        readonly IRelicAnalyticsService _analyticsService;
        readonly IAddressablesService _addressablesService;
        readonly QualitySettingsManager _qualitySettingsManager;

        public BootSequence(
            ISaveService saveService,
            IRemoteConfigService remoteConfigService,
            IRelicAnalyticsService analyticsService,
            IAddressablesService addressablesService,
            QualitySettingsManager qualitySettingsManager)
        {
            _saveService = saveService;
            _remoteConfigService = remoteConfigService;
            _analyticsService = analyticsService;
            _addressablesService = addressablesService;
            _qualitySettingsManager = qualitySettingsManager;
        }

        public async UniTask StartAsync(CancellationToken cancellationToken)
        {
            _qualitySettingsManager.ApplySavedTier();

            await InitializeUnityServicesAsync(cancellationToken);
            await _saveService.LoadAsync(cancellationToken);
            await _remoteConfigService.FetchAsync(cancellationToken);

            _addressablesService.SetActiveDungeonKey(_saveService.Data.LastDungeonKey);
            _analyticsService.TrackSessionStart();

            if (SceneManager.GetActiveScene().name == StartupSceneName)
            {
                SceneManager.LoadScene(MainMenuSceneName);
            }
        }

        static async UniTask InitializeUnityServicesAsync(CancellationToken cancellationToken)
        {
            try
            {
                if (Unity.Services.Core.UnityServices.State ==
                    Unity.Services.Core.ServicesInitializationState.Uninitialized)
                {
                    await Unity.Services.Core.UnityServices.InitializeAsync()
                        .AsUniTask()
                        .AttachExternalCancellation(cancellationToken);
                }

                if (!AuthenticationService.Instance.IsSignedIn)
                {
                    await AuthenticationService.Instance.SignInAnonymouslyAsync()
                        .AsUniTask()
                        .AttachExternalCancellation(cancellationToken);
                }
            }
            catch (System.Exception exception)
            {
                Debug.LogWarning($"Unity Services init skipped: {exception.Message}");
                await UniTask.Yield(cancellationToken);
            }
        }
    }
}
