using System;

namespace SBaier.DI
{
	public class MissingSceneContextException : Exception
	{
		private const string message = "A scene context is missing in the current scene.";
		private const string messageWithSceneName = "A scene context is missing in the scene '{0}'.";

		public MissingSceneContextException() : base(message)
		{
		}

		public MissingSceneContextException(string sceneName) : base(string.Format(messageWithSceneName, sceneName))
		{
		}
	}
}
