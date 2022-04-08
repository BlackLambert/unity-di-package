using System.Collections.Generic;
using System;

namespace SBaier.DI
{
    public class DisposablesContainer 
    {
        private HashSet<IDisposable> _disposibles = new HashSet<IDisposable>();

        public void Add(IDisposable disposable)
        {
            _disposibles.Add(disposable);
        }

        public void Clear()
        {
            _disposibles.Clear();
        }

		internal void Dispose()
		{
            foreach (IDisposable disposable in _disposibles)
                disposable.Dispose();
        }
	}
}
