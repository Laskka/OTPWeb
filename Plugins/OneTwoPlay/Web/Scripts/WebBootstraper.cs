using UnityEngine;
using OneTwoPlay.Web.Ads;
using OneTwoPlay.Web.CheckOrientation;
using OneTwoPlay.Web.GameStatus;

namespace OneTwoPlay.Web
{
    public class WebBootstraper : MonoBehaviour
    {
        [SerializeField] private string _gameKey = "YOUR_GAME_KEY_HERE";
        [SerializeField] private ADSManager _adsManager;
        [SerializeField] private OrientationChecker _orientation;
        [SerializeField] private GameStatusChecker _gameStatus;
        [SerializeField] private GameDistribution _gameDistribution;

        [Header("Ads Settings")]
        [SerializeField] private bool _interLoop = true;
        [SerializeField] private float _interDelay = 90f;
        
        [Header("Mobile Orientation")]
        [SerializeField] private OrientationChecker.Orientation _orientationScreen = OrientationChecker.Orientation.Portrait;
        
        private void Awake()
        {
#if PLATFORM_WEBGL
            _gameStatus = Instantiate(_gameStatus);
            _gameDistribution = Instantiate(_gameDistribution);
            _adsManager = Instantiate(_adsManager);
            _orientation = Instantiate(_orientation);

            _gameStatus.Init();
            _gameDistribution.Init(_gameKey);
            _adsManager.Init(_gameDistribution, _interLoop, _interDelay);
            _orientation.Init(_orientationScreen);
#endif
        }
    }
}
