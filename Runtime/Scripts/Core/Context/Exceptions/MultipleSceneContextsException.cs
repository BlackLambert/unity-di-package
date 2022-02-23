using System;

namespace SBaier.DI
{
	public class MultipleSceneContextsException : Exception
	{
		private const string message = "There are multiple scene contexts in the current scene.";
		private const string messageWithSceneName = "There are multiple scene contexts in the scene '{0}'.";

		public MultipleSceneContextsException() : base(message)
		{
		}

		public MultipleSceneContextsException(string sceneName) : base(string.Format(messageWithSceneName, sceneName))
		{
		}
	}
}
