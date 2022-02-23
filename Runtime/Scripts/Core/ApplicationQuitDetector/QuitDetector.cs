using System;
using UnityEngine;

namespace SBaier.DI
{
    public abstract class QuitDetector : MonoBehaviour
    {
        public event Action OnQuit;
		public bool ApplicationIsQuitting { get; private set; } = false;

		protected void OnApplicationQuitting()
		{
			ApplicationIsQuitting = true;
			OnQuit?.Invoke();
		}
	}
}
