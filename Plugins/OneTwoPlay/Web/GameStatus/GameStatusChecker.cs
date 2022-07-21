using UnityEngine;

namespace OneTwoPlay.Web.GameStatus
{
    public class GameStatusChecker : MonoBehaviour
    {
        private bool _isPaused;
        private bool _isAdsPaused;

        public void Init()
        {
            DontDestroyOnLoad(this);
            GameDistribution.OnResumeGame += OnResumeGame;
            GameDistribution.OnPauseGame += OnPauseGame;
        }

        private void OnResumeGame()
        {
            Application.ExternalEval("window.focus();");
            Time.timeScale = 1;

            AudioListener.pause = false;
            _isAdsPaused = false;
        }

        private void OnPauseGame()
        {
            Time.timeScale = 0.00001f;
            AudioListener.pause = true;
            _isAdsPaused = true;
        } 
    
        private void OnGUI()
        {
            if (_isAdsPaused == false)
            {
                if (_isPaused)
                {
                    Time.timeScale = 0.00001f;
                    AudioListener.pause = true;
                }
                else
                {
                    Time.timeScale = 1;
                    AudioListener.pause = false;
                }
            }
        }
        private void OnApplicationFocus(bool hasFocus) => 
            _isPaused = !hasFocus;

        private void OnApplicationPause(bool pauseStatus) => 
            _isPaused = pauseStatus;
    
    }
}
