#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SBaier.DI
{
	public class EditorQuitDetector : QuitDetector
	{
#if UNITY_EDITOR
		private void Start()
		{
			EditorApplication.playModeStateChanged += OnPlaymodeStateChanged;
		}

		private void OnDestroy()
		{
			EditorApplication.playModeStateChanged -= OnPlaymodeStateChanged;
		}

		private void OnPlaymodeStateChanged(PlayModeStateChange change)
		{

			if (change == PlayModeStateChange.ExitingPlayMode)
				OnApplicationQuitting();
		}
#endif
	}
}
