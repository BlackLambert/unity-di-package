using System;
using UnityEngine;

namespace SBaier.DI
{
	public class ApplicationQuitDetector : QuitDetector
	{
		private void Start()
		{
			Application.quitting += OnApplicationQuitting;
		}

		private void OnDestroy()
		{
			Application.quitting -= OnApplicationQuitting;
		}
	}
}
