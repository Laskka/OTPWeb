using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;
using UnityEngine.Events;
namespace OneTwoPlay.Web.CheckOrientation
{
	public class OrientationChecker : MonoBehaviour
	{
		private static UnityEvent<ScreenOrientation> onOrientationChangeStaticCallback;

		[DllImport("__Internal", EntryPoint="ScreenOrientationWebGL_Start")]
		public static extern void Play(ref ScreenOrientation orientation);

		[DllImport("__Internal", EntryPoint="ScreenOrientationWebGL_Stop")]
		public static extern void Stop();

		[DllImport("__Internal", EntryPoint="ScreenOrientationWebGL_setUnityFunctions")]
		private static extern void setUnityFunctions(Action<int> _onOrientationChange);

		[DllImport("__Internal")]
		private static extern bool IsMobile();

		public ScreenOrientation CurrentScreenOrientation => _currentOrientationScreen;

		public UnityEvent<ScreenOrientation> onOrientationChangeCallback = new UnityEvent<ScreenOrientation>();
		[SerializeField] private Transform panelOrientation;
		
		private ScreenOrientation _currentOrientationScreen;
		private ScreenOrientation _targetOrientationScreen;
		private bool isMobile;
		private bool _decision;

		public void Init(ScreenOrientation orientationScreen)
		{
			DontDestroyOnLoad(this);
			panelOrientation.gameObject.SetActive(false);
		#if !UNITY_EDITOR
			onOrientationChangeCallback.AddListener(ChangeOrientation);
			onOrientationChangeStaticCallback = onOrientationChangeCallback;
			setUnityFunctions(_onOrientationChangeCallback);
			_targetOrientationScreen = orientationScreen;
			panelOrientation.gameObject.SetActive(false);
			try
			{
				isMobile = IsMobile();
			}
			catch (Exception e)
			{
				Debug.Log("This application was not launched in a mobile browser!");
			}
			Play(ref _currentOrientationScreen);
		#endif
		}

		private void ChangeOrientation(ScreenOrientation orientation)
		{
			if (IsMobile() == false)
				return;
			panelOrientation.gameObject.SetActive(orientation != _targetOrientationScreen);
			print($"{orientation.ToString()} --- {_targetOrientationScreen.ToString()} = {orientation != _targetOrientationScreen}");
		}
		
		[MonoPInvokeCallback(typeof(Action<int>))]
		private static void _onOrientationChangeCallback(int orientation) {
			ScreenOrientation orient = (ScreenOrientation) orientation;
			onOrientationChangeStaticCallback?.Invoke(orient);
		}
		
		public enum ScreenOrientation 
		{
			Portrait,
			PortraitUpsideDown, 
			LandscapeLeft,
			LandscapeRight 
		}
	}
}
