using System;

namespace SBaier.DI
{
    public class AlreadyBoundException : InvalidOperationException
    {
        public AlreadyBoundException(Type bindType) : base($"The type {bindType} is already bound")
		{

		}
    }
}