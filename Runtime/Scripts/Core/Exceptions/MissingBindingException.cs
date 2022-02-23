using System;

namespace SBaier.DI
{
    public class MissingBindingException : InvalidOperationException
    {
        public MissingBindingException(string message) : base(message) { }
    }
}