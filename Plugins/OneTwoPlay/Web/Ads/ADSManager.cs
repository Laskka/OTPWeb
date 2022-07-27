using System;
using System.Collections;
using UnityEngine;
using OneTwoPlay.Tools;

namespace OneTwoPlay.Web.Ads
{
    public class ADSManager : Singleton<ADSManager>
    {
        public bool RewardLoaded => _gameDistribution.IsRewardedVideoLoaded();
        
        private GameDistribution _gameDistribution;
        private Action _actionAfterReward;
        private bool _isPlaying;
        private float _interDelay = 90;
        private bool _prerollShowed;

        public void Init(GameDistribution gameDistribution, bool interLoop, float interDelay)
        {
            DontDestroyOnLoad(this);
            
            _interDelay = interDelay;
            _gameDistribution = gameDistribution;
            
            GameDistribution.OnRewardedVideoSuccess += OnRewardedVideoSuccess;
            GameDistribution.OnResumeGame += OnResumeGame;
            
            if(interLoop)
                _gameDistribution.StartCoroutine(InterLoop(_interDelay));
            _gameDistribution.PreloadRewardedAd();
        }

        public bool ShowReward(Action getReward)
        {
#if UNITY_EDITOR
            getReward?.Invoke();
            return true;
#endif
            if(_isPlaying)
                return false;
            _isPlaying = true;
            _gameDistribution.ShowRewardedAd();
            _actionAfterReward = getReward;
            return true;
        }

        public void ShowPreRoll()
        {
            if (_prerollShowed) return;
            _prerollShowed = true;
            ShowInter();
        }

        public void ShowInter()
        {
            _gameDistribution.ShowAd();
        }

        private IEnumerator InterLoop(float delay)
        {
            Debug.Log("!!!!Start Pre-roll");
            yield return new WaitForSeconds(1);
            Debug.Log("!!!!Show Pre-roll");
            ShowPreRoll();
            Debug.Log("!!!!Start mid-roll loop");//Pre-roll
            while (true)
            {
                yield return new WaitForSeconds(delay);
                Debug.Log("!!!!Show mid-roll");
                ShowInter(); //mid-roll
            }
        }

        private void OnResumeGame()
        {
            Application.ExternalEval("window.focus();");
        }

        private void OnRewardedVideoSuccess()
        {
            _actionAfterReward?.Invoke();
            _gameDistribution.PreloadRewardedAd();
            _isPlaying = false;
            _actionAfterReward = null;
        }
    }
}