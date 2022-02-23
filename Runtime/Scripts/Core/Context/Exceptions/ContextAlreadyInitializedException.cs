using System;

namespace SBaier.DI
{
	[Serializable]
	public abstract class ContextAlreadyInitializedException : InvalidOperationException
	{
		public ContextAlreadyInitializedException(Type contextType, string contextObjectName) : 
			base($"The {contextType} with name {contextObjectName} has already been initialized")
		{
		}
	}
}