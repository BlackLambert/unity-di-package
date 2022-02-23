using System;

namespace SBaier.DI
{
	[Serializable]
	public class GameObjectContextAlreadyInitializedException : ContextAlreadyInitializedException
	{
		public GameObjectContextAlreadyInitializedException(string contextObjectName) : 
			base(typeof(GameObjectContext), contextObjectName)
		{
		}
	}
}