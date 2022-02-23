using UnityEditor;
using System;

namespace SBaier.DI
{
	public class EditorQuitDetector : QuitDetector
	{
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
	}
}
