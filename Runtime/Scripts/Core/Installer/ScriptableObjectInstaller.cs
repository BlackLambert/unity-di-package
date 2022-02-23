using UnityEngine;

namespace SBaier.DI
{
	public abstract class ScriptableObjectInstaller : ScriptableObject, Installer
	{
		public abstract void InstallBindings(Binder binder);
	}
}
