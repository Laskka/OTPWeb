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
		public void Init(Orientation orientationScreen)
		{
			_orientationScreen = orientationScreen;
			DontDestroyOnLoad(this);
			panelOrientation.gameObject.SetActive(false);
			isMobile = IsMobile();
		}

		private void Update()
		{
			if (isMobile == false)
				return;
			switch (_orientationScreen)
			{
				case Orientation.Landscape:
					panelOrientation.gameObject.SetActive(Screen.orientation == ScreenOrientation.Portrait);
					break;
				case Orientation.Portrait:
					panelOrientation.gameObject.SetActive(Screen.orientation != ScreenOrientation.Portrait);
					break;
			}
		}

		public enum Orientation
		{ 
			Landscape,
			Portrait
		}
	}
}
