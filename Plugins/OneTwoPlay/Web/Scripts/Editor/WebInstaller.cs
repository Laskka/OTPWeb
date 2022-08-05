using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace OneTwoPlay.Web
{
#if UNITY_EDITOR
    [InitializeOnLoad]
    static class WebInstaller
    {
        static WebInstaller()
        {
            AssetDatabase.importPackageCompleted += packageName =>
            {
                if (Application.platform != RuntimePlatform.WebGLPlayer) 
                    SwitchTo(BuildTarget.WebGL);

                PlayerSettings.WebGL.template = "OneTwoPlayTemplate";
                
                PlayerSettings.defaultWebScreenHeight = 600;
                PlayerSettings.defaultWebScreenWidth = 800;
                PlayerSettings.colorSpace = ColorSpace.Gamma;
                PlayerSettings.SetGraphicsAPIs(BuildTarget.WebGL, new GraphicsDeviceType[]
                     {
                         GraphicsDeviceType.OpenGLES2,
                         GraphicsDeviceType.OpenGLES3
                     }
                );
                
                
                
                PlayerSettings.stripEngineCode = true;
                PlayerSettings.SetManagedStrippingLevel(BuildTargetGroup.WebGL, ManagedStrippingLevel.Low);
                
                PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Disabled;
                PlayerSettings.WebGL.nameFilesAsHashes = true;
                PlayerSettings.WebGL.exceptionSupport = WebGLExceptionSupport.ExplicitlyThrownExceptionsOnly;
                Debug.Log( $"{packageName} installed!");
            };
        }
        
        public static void SwitchTo(BuildTarget targetPlatform)
        {
            var currentPlatform = EditorUserBuildSettings.activeBuildTarget;
            if (currentPlatform == targetPlatform)
                return;

            // Don't switch when compiling
            if (EditorApplication.isCompiling)
            {
                Debug.LogWarning("Could not switch platform because unity is compiling");
                return;
            }

            // Don't switch while playing
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                Debug.LogWarning("Could not switch platform because unity is in playMode");
                return;
            }

            Debug.Log("Switching platform from " + currentPlatform + " to " + targetPlatform);
            
            EditorUserBuildSettings.SwitchActiveBuildTarget(targetPlatform);
            Debug.Log("Platform switched to " + targetPlatform);
        }
        
    }
#endif
}
