using UnityEngine;

namespace SBaier.DI.Tests
{
    public class TestQuitDetector : QuitDetector
    {
        public void SetQuitting()
		{
            OnApplicationQuitting();
        }
    }
}
