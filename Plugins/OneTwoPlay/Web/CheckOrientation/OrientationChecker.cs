using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace OneTwoPlay.Web.CheckOrientation
{
	public class OrientationChecker : MonoBehaviour
	{
		[SerializeField] private Transform panelOrientation;
		private Orientation _orientationScreen;
	
		[DllImport("__Internal")]
		private static extern bool IsMobile();
		
		private bool isMobile;
		private bool _decision;
		public void Init(Orientation orientationScreen)
		{
			_orientationScreen = orientationScreen;
			DontDestroyOnLoad(this);
			panelOrientation.gameObject.SetActive(false);
			try
			{
				isMobile = IsMobile();
			}
			catch (Exception e)
			{
				Debug.Log("This application was not launched in a mobile browser!");
			}
		}

		private void Update()
		{
			if (isMobile == false)
				return;
			_decision = _orientationScreen switch
			{
				Orientation.Landscape when Screen.orientation != ScreenOrientation.Portrait => false,
				Orientation.Landscape when Screen.width > Screen.height => false,
				Orientation.Landscape => true,
				Orientation.Portrait when Screen.orientation == ScreenOrientation.Portrait => false,
				Orientation.Portrait when Screen.width < Screen.height => false,
				Orientation.Portrait => true,
				_ => _decision
			};

			panelOrientation.gameObject.SetActive(_decision);
		}

		public enum Orientation
		{ 
			Landscape,
			Portrait
		}
	}
}
