using System;

namespace SBaier.DI
{
	[Serializable]
	public class AppContextAlreadyInitializedException : ContextAlreadyInitializedException
	{
		public AppContextAlreadyInitializedException(string contextObjectName) : 
			base(typeof(GameObjectContext), contextObjectName)
		{
		}
	}
}