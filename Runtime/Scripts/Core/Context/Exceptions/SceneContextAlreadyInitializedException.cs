using System;

namespace SBaier.DI
{
	[Serializable]
	public class SceneContextAlreadyInitializedException : ContextAlreadyInitializedException
	{
		public SceneContextAlreadyInitializedException(string contextObjectName) : 
			base(typeof(SceneContext), contextObjectName)
		{
		}
	}
}